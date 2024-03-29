using Networking.Core;
using UI.Core;
using UnityEngine.UIElements;
using Utils;

namespace UI.Panels
{
    public class DialoguePopup : BasePanel
    {
        private Label _bodyLabel;
        private Button _closeButton;
        
        public DialoguePopup(VisualElement parent, bool startHidden = true, bool hasBackButton = false) : base(parent, startHidden, hasBackButton)
        {
            // For manual activation
            Subscribe<ShowDialoguePopupMessage>(OnDisplayPopup);
            Subscribe<HideDialoguePopupMessage>(OnHidePopup);
            
            // Automated error handling
            Subscribe<LoginFailedMessage>(OnLoginFailed);
            Subscribe<StartHostFailedMessage>(OnHostFailed);
            Subscribe<JoinGameFailedMessage>(OnJoinFailed);
        }

        private void OnJoinFailed(JoinGameFailedMessage obj)
        {
            ReportFailure("Join Failure: Could not join the game, check join code is correct and try again.");
        }

        private void OnHostFailed(StartHostFailedMessage message)
        {
            ReportFailure("Host Failure: Could not create a new game. Please try again.");
        }

        private void OnLoginFailed(LoginFailedMessage message)
        {
            ReportFailure("Login Failure: Are you connected to the internet?");
        }

        private void ReportFailure(string message)
        {
            _bodyLabel.text = message;
            Open();
            Locator.EventHub.Publish(new OpenHomePanelMessage());
        }

        public override void AcquireReferences()
        {
            _bodyLabel = Root.Q<Label>("DialogueLabel");
            _closeButton = Root.Q<Button>("DialogueButton");
            _closeButton.RegisterCallback<ClickEvent>(OnCloseButton);
        }

        private void OnCloseButton(ClickEvent evt) => OnHidePopup(null);

        private void OnDisplayPopup(ShowDialoguePopupMessage message)
        {
            _bodyLabel.text = message.Content;
            Open();
        }
        
        private void OnHidePopup(HideDialoguePopupMessage obj)
        {
            Close();
            Locator.EventHub.Publish(new DialoguePopupClosedMessage());
        }
    }
}