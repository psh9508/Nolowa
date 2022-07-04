using NolowaFrontend.Servicies.Base;
using NolowaFrontend.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NolowaFrontend.Servicies
{
    public interface ISignalRService
    {
        Task<IEnumerable<DirectMessageDialogItem>> GetDialog(long senderId, long receiverId);
        Task<IEnumerable<PreviousDirectMessageDialogItem>> GetPreviousDialogListAsync(long senderId);
    }

    public class SignalRService : ServiceBase, ISignalRService
    {
        public override string ParentEndPoint => "SignalR";

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
    }
}
