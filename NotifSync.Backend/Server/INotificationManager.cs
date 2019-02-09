using System.Threading.Tasks;
using Refit;

namespace NotifSync.Backend.Server
{
    public interface INotificationManager
    {
        [Post("/dismissnotification/{id}/{appPackage}")]
        Task DismissNotification(int id, string appPackage);

        [Post("/invokeAction/{id}/{appPackage}/{index}")]
        Task InvokeAction(int id, string appPackage, [AliasAs("index")] int actionIndex);

        [Post("/invokeReply/{id}/{appPackage}/{index}")]
        Task InvokeReply(int id, string appPackage, [AliasAs("index")] int actionIndex, [Body] string content);
    }
}