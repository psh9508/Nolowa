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
    }

    public class SignalRService : ServiceBase, ISignalRService
    {
        public override string ParentEndPoint => "SignalR";

        public async Task<IEnumerable<DirectMessageDialogItem>> GetDialog(long senderId, long receiverId)
        {
            var response = await DoGet<IEnumerable<DirectMessageDialogItem>>($"chat/dialog/{senderId}/{receiverId}");

            return response.IsSuccess ? response.ResponseData : Enumerable.Empty<DirectMessageDialogItem>();
        }
    }
}
