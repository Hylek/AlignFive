using Core;
using UI.Core;
using UnityEngine.UIElements;
using Utils;

namespace UI.Panels
{
    public class EnterNamePanel : BasePanel
    {
        private Button _submitButton;
        private TextField _nameField;
        
        public EnterNamePanel(VisualElement parent, bool startHidden = true, bool hasBackButton = false)
            : base(parent, startHidden, hasBackButton)
        {
            Subscribe<OpenEnterNamePanelMessage>(OnOpen);
        }

        public override void AcquireReferences()
        {
            _submitButton = Root.Q<Button>("SubmitButton");
            _nameField = Root.Q<TextField>("NameField");
            
            _submitButton.RegisterCallback<ClickEvent>(OnSubmitButton);
        }
        
        protected override void OnBackButton(ClickEvent evt)
        {
            Close();
            Locator.EventHub.Publish(new OpenHomePanelMessage());
        }

        private void OnSubmitButton(ClickEvent evt)
        {
            if (string.IsNullOrEmpty(_nameField.value))
            {
                Locator.EventHub.Publish(new ShowDialoguePopupMessage(this, 
                    "You must enter a valid name to continue."));

                return;
            }
            AlignFiveApp.Instance.DisplayName = _nameField.value;
            AlignFiveApp.Instance.EnterNameComplete();
            Close();
        }

        private void OnOpen(OpenEnterNamePanelMessage message) => Open();
    }
}