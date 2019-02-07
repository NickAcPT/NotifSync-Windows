using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using Nancy;
using Nancy.ModelBinding;
using Newtonsoft.Json;
using NotifSync.Backend.Model;
using NotifSync.Backend.Utils;

namespace NotifSync.Backend.Server
{
    public class HandleNotificationModule : NancyModule
    {
        public HandleNotificationModule()
        {
            Post["/handlenotification"] = HandleNotification;
            Post["/removenotification/{id:int}/{appPackage}"] = RemoveNotification;
        }

        private object RemoveNotification(dynamic args)
        {
            try
            {
                SharedObjects.Instance.NotificationRouter.Remove(args.id, args.appPackage);

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        private object HandleNotification(object arg)
        {
            try
            {
                var jsonBody = GetJsonBody();

                var adapter = new BitmapBase91Adapter(Request.Files.Select(BitmapFromHttpFile).Where(c => c.HasValue).Select(c => (c.Value.Item1, c.Value.Item2)).ToDictionary(tuple => tuple.Item1, tuple1 => tuple1.Item2));

                var notification = JsonConvert.DeserializeObject<RemoteNotification>(jsonBody, new JsonSerializerSettings
                {
                    Converters = {adapter, new AndroidColorAdapter()}
                });

                SharedObjects.Instance.NotificationRouter.Route(notification);

                return HttpStatusCode.OK;
            }
            catch (Exception ex)
            {
                return HttpStatusCode.InternalServerError;
            }
        }

        private string GetJsonBody()
        {
            var contentTypeRegex = new Regex("^multipart/form-data;\\s*boundary=(.*)$", RegexOptions.IgnoreCase);
            System.IO.Stream bodyStream = null;
            if (contentTypeRegex.IsMatch(this.Request.Headers.ContentType))
            {
                var boundary = contentTypeRegex.Match(this.Request.Headers.ContentType).Groups[1].Value;
                var multipart = new HttpMultipart(this.Request.Body, boundary);
                bodyStream = multipart.GetBoundaries().First(b => b.Name.Equals("json")).Value;
            }
            else
            {
                // Regular model binding goes here.
                bodyStream = this.Request.Body;
            }

            var jsonBody = new System.IO.StreamReader(bodyStream).ReadToEnd();
            return jsonBody;
        }

        private static (string, Image)? BitmapFromHttpFile(HttpFile arg)
        {
            try
            {
                return (arg.Name, Image.FromStream(arg.Value));
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}