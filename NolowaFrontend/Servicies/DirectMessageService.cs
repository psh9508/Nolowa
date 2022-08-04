using NolowaFrontend.Servicies.Base;
using NolowaFrontend.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NolowaFrontend.Servicies
{
    public interface IDirectMessageService
    {
        Task<IEnumerable<DirectMessageDialogItem>> GetDialog(long senderId, long receiverId);
        Task<IEnumerable<PreviousDirectMessageDialogItem>> GetPreviousDialogListAsync(long senderId);
        Task<int> GetUnreadMessageCount(long userId);
        Task<int> SetReadAllMessageAsync(long senderId, long receiverId);
    }

    public class DirectMessageService : ServiceBase, IDirectMessageService
    {
        public override string ParentEndPoint => "DirectMessage";

        public async Task<IEnumerable<DirectMessageDialogItem>> GetDialog(long senderId, long receiverId)
        {
            var response = await DoGet<IEnumerable<DirectMessageDialogItem>>($"chat/dialog/{senderId}/{receiverId}");

            return response.IsSuccess ? response.ResponseData : Enumerable.Empty<DirectMessageDialogItem>();
        }

        public async Task<IEnumerable<PreviousDirectMessageDialogItem>> GetPreviousDialogListAsync(long senderId)
        {
            var response = await DoGet<IEnumerable<PreviousDirectMessageDialogItem>>($"chat/previousDialogList/{senderId}");

            return response.IsSuccess ? response.ResponseData : Enumerable.Empty<PreviousDirectMessageDialogItem>();
        }

        public async Task<int> GetUnreadMessageCount(long userId)
        {
            var response = await DoGet<int>($"chat/unreadmessagecount/{userId}");

            return response.IsSuccess ? response.ResponseData : 0;
        }

        public async Task<int> SetReadAllMessageAsync(long senderId, long receiverId)
        {
            string param = Newtonsoft.Json.JsonConvert.SerializeObject(new {
                senderId = senderId,
                receiverId = receiverId,
            });

            var response = await DoPatch<int>($"chat/dialog/readmessage", param);

            return response.IsSuccess ? response.ResponseData : 0;
        }
    }
}
