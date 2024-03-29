using Core;
using UI.Core;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

namespace UI.Panels
{
    public class HomePanel : BasePanel
    {
        private Button _hostButton;
        private Button _joinButton;
        private Button _settingsButton;
        private Button _quitButton;

        public HomePanel(VisualElement parent, bool startHidden = true, bool hasBackButton = false)
            : base(parent, startHidden, hasBackButton)
        {
            Subscribe<OpenHomePanelMessage>(OnOpen);
        }

        public override void AcquireReferences()
        {
            _hostButton = Root.Q<Button>("HostGameButton");
            _joinButton = Root.Q<Button>("JoinGameButton");
            _settingsButton = Root.Q<Button>("SettingsButton");
            _quitButton = Root.Q<Button>("QuitGameButton");
            
            _hostButton.RegisterCallback<ClickEvent>(OnHostButton);
            _joinButton.RegisterCallback<ClickEvent>(OnJoinButton);
            _settingsButton.RegisterCallback<ClickEvent>(OnSettingsButton);
            _quitButton.RegisterCallback<ClickEvent>(OnQuitButton);

        }
        
        private void OnOpen(OpenHomePanelMessage message)
        {
            Debug.Log("Open Home Panel");
            Open();
        }

        private void OnHostButton(ClickEvent evt)
        {
            AlignFiveApp.Instance.AppMode = AppMode.Host;
            
            Publish(new OpenEnterNamePanelMessage());
            Close();
        }
        
        private void OnJoinButton(ClickEvent evt)
        {
            AlignFiveApp.Instance.AppMode = AppMode.Join;
            
            Publish(new OpenEnterNamePanelMessage());
            Close();
        }
        
        private void OnSettingsButton(ClickEvent evt)
        {
            Publish(new OpenSettingsPanelMessage());
            Close();
        }
        
        private void OnQuitButton(ClickEvent evt)
        {
            Application.Quit();
        }
    }
}