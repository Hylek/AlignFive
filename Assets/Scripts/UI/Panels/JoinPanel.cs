using Core;
using Networking.Core;
using UI.Core;
using UnityEngine.UIElements;
using Utils;

namespace UI.Panels
{
    public class JoinPanel : BasePanel
    {
        private Button _submitButton;
        private TextField _inputField;
        
        public JoinPanel(VisualElement parent, bool startHidden = true, bool hasBackButton = false) : base(parent, startHidden, hasBackButton)
        {
            Subscribe<OpenJoinPanelMessage>(OnOpen);
        }

        public override void AcquireReferences()
        {
            _submitButton = Root.Q<Button>("SubmitJoinCodeButton");
            _inputField = Root.Q<TextField>("JoinCodeField");
            
            _submitButton.RegisterCallback<ClickEvent>(OnSubmit);
        }
        
        private void OnOpen(OpenJoinPanelMessage obj) => Open();

        private async void OnSubmit(ClickEvent evt)
        {
            if (string.IsNullOrEmpty(_inputField.value))
            {
                Locator.EventHub.Publish(new ShowDialoguePopupMessage(this, 
                    "Please enter a valid join code."));
            }

            var submittedCode = _inputField.value;
            
            var success = await AlignFiveApp.Instance.RelayHandler.JoinGame(submittedCode);
            if (success)
            {
                Close();
                Locator.EventHub.Publish(new JoinGameSuccessMessage());
                Locator.EventHub.Publish(new OpenLobbyPanelMessage());
            }
            else
            {
                Locator.EventHub.Publish(new ShowDialoguePopupMessage(this, 
                    "Something went wrong when trying to join host game. Please check your join code is correct."));
            }
        }
    }
}