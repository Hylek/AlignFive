using Core;
using Networking;
using UI.Core;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UIElements;
using Utils;

namespace UI.Panels
{
    public class LobbyPanel : BasePanel
    {
        private Button _sideASubmitButton, _sideBSubmitButton;
        private Label _sideANameLabel, _sideBNameLabel;
        private Label _sideAStatusLabel, _sideBStatusLabel;
        private VisualElement _popupPanel;
        private Label _popupPanelCodeLabel;
        private Button _popupButton;
        
        public LobbyPanel(VisualElement parent, bool startHidden = true, bool hasBackButton = false)
            : base(parent, startHidden, hasBackButton)
        {
            Subscribe<OpenLobbyPanelMessage>(OnOpen);
        }

        public override void AcquireReferences()
        {
            _sideASubmitButton = Root.Q<Button>("SideAReadyButton");
            _sideBSubmitButton = Root.Q<Button>("SideBReadyButton");
            _sideANameLabel = Root.Q<Label>("SideANameLabel");
            _sideBNameLabel = Root.Q<Label>("SideBNameLabel");
            _sideAStatusLabel = Root.Q<Label>("SideAStatusLabel");
            _sideBStatusLabel = Root.Q<Label>("SideBStatusLabel");
            _popupPanel = Root.Q<VisualElement>("CodePopup");
            _popupPanelCodeLabel = Root.Q<Label>("JoinCodeLabel");
            _popupButton = Root.Q<Button>("ConfirmButton");
            
            _sideASubmitButton.RegisterCallback<ClickEvent>(OnSideAButtonDown);
            _sideBSubmitButton.RegisterCallback<ClickEvent>(OnSideBButtonDown);
            _popupButton.RegisterCallback<ClickEvent>(OnPopupConfirmed);
        }

        private void OnSideAButtonDown(ClickEvent evt)
        {
            // todo: Signal that this player is ready.
        }
        
        private void OnSideBButtonDown(ClickEvent evt)
        {
            // todo: Signal that this player is ready.
        }

        private void OnOpen(OpenLobbyPanelMessage message)
        {
            Open();
            SetupLobby();
        }
        
        protected override void OnBackButton(ClickEvent evt)
        {
            Close();

            if (NetworkManager.Singleton.IsHost || NetworkManager.Singleton.IsClient)
            {
                NetworkManager.Singleton.Shutdown();
            }
            
            Locator.EventHub.Publish(new OpenHomePanelMessage());
        }
        
        private void OnPopupConfirmed(ClickEvent evt)
        {
            _popupPanel.style.display = DisplayStyle.None;
            _popupPanel.AddToClassList("rootpanel-hide");
        }

        private async void SetupLobby()
        {
            Debug.Log("LobbyPanel::SetupLobby()");
            
            _sideASubmitButton.SetEnabled(false);
            _sideBSubmitButton.SetEnabled(false);
            
            Locator.EventHub.Publish(new ShowLoadingPanelMessage());
            
            if (AlignFiveApp.Instance.AppMode == AppMode.None) return;
            
            if (AlignFiveApp.Instance.AppMode == AppMode.Host)
            {
                Debug.Log("LobbyPanel::SetupLobby() -> This is the host");
                _sideANameLabel.text = AlignFiveApp.Instance.DisplayName;
                
                var success = await AlignFiveApp.Instance.RelayHandler.StartHost();

                if (success)
                {
                    Locator.EventHub.Publish(new HideLoadingPanelMessage());
                    var joinCode = AlignFiveApp.Instance.RelayHandler.JoinCode;
                    _popupPanel.style.display = DisplayStyle.Flex;
                    _popupPanel.RemoveFromClassList("rootpanel-hide");
                    _popupPanelCodeLabel.text = joinCode;
                }
            }
            else
            {
                _sideBNameLabel.text = AlignFiveApp.Instance.DisplayName;
            }
        }
    }
}