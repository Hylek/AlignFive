using DC.MessageService;
using UI;

namespace Utils
{
    public class OpenLobbyPanelMessage : ITinyMessage
    {
        public object Sender { get; }
    }
    
    public class OpenJoinPanelMessage : ITinyMessage
    {
        public object Sender { get; }
    }
    
    public class OpenSettingsPanelMessage : ITinyMessage
    {
        public object Sender { get; }
    }
    
    public class OpenHomePanelMessage : ITinyMessage
    {
        public object Sender { get; }
    }
    
    public class OpenEnterNamePanelMessage : ITinyMessage
    {
        public object Sender { get; }
    }
    
    public class ShowLoadingPanelMessage : ITinyMessage
    {
        public object Sender { get; }
    }
    
    public class HideLoadingPanelMessage : ITinyMessage
    {
        public object Sender { get; }
    }
    
    public class ShowDialoguePopupMessage : GenericTinyMessage<string>
    {
        public ShowDialoguePopupMessage(object sender, string content) : base(sender, content)
        {
        }
    }
    
    public class HideDialoguePopupMessage : ITinyMessage
    {
        public object Sender { get; }
    }
    
    public class DialoguePopupClosedMessage : ITinyMessage
    {
        public object Sender { get; }
    }
}
