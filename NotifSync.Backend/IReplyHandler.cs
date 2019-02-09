using System.Threading.Tasks;

namespace NotifSync.Backend
{
    public interface IReplyHandler
    {
        Task<string> GetReplyFromUser();
    }
}