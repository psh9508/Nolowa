using NolowaNetwork.Models.Message;

namespace NolowaFrontend.Models.IF
{
    public class LoginReq : NetMessageBase
    {
        public string Id { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
