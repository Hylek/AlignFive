using UI.Core;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

namespace UI.Panels
{
    public class SettingsPanel : BasePanel
    {
        public SettingsPanel(VisualElement parent, bool startHidden = true, bool hasBackButton = false)
            : base(parent, startHidden, hasBackButton)
        {
            Subscribe<OpenSettingsPanelMessage>(OnOpenSettings);
        }

        protected override void OnBackButton(ClickEvent evt)
        {
            Close();
            Locator.EventHub.Publish(new OpenHomePanelMessage());
        }

        public override void AcquireReferences()
        {
            // todo
        }
        
        private void OnOpenSettings(OpenSettingsPanelMessage panelMessage)
        {
            Debug.Log("Open Settings Panel");
            Open();
        }
    }
}