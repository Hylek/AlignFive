using DC.MessageService;

namespace Networking.Core
{
    public class LoginSuccessMessage : ITinyMessage
    {
        public object Sender { get; }
    }
    
    public class LoginFailedMessage : ITinyMessage
    {
        public object Sender { get; }
    }
    
    public class StartHostSuccessMessage : ITinyMessage
    {
        public object Sender { get; }
    }
    
    public class StartHostFailedMessage : ITinyMessage
    {
        public object Sender { get; }
    }
    
    public class JoinGameSuccessMessage : ITinyMessage
    {
        public object Sender { get; }
    }

    public class JoinGameFailedMessage : ITinyMessage
    {
        public object Sender { get; }
    }

    public class LocalClientDisconnectMessage : ITinyMessage
    {
        public object Sender { get; }
    }
}