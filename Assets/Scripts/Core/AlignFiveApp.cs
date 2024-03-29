using System;
using Networking.Core;
using Networking.Services;
using Utils;
// ReSharper disable InconsistentNaming

// Made by Daniel Cumbor in 2024.

namespace Core
{
    public enum AppMode
    {
        None,
        Host,
        Join
    }
    
    public class AlignFiveApp : Singleton<AlignFiveApp>
    {
        public AuthHandler AuthHandler;
        public RelayHandler RelayHandler;
        public AppMode AppMode = AppMode.None;
        public string DisplayName = "";

        public override void Awake()
        {
            base.Awake();

            AuthHandler = new AuthHandler();
            RelayHandler = new RelayHandler();
        }

        private async void Start()
        {
            Locator.EventHub.Publish(new ShowLoadingPanelMessage());
            var result = await AuthHandler.Login();
            if (result)
            {
                Locator.EventHub.Publish(new LoginSuccessMessage());
            }
            else
            {
                Locator.EventHub.Publish(new LoginFailedMessage());
            }
            Locator.EventHub.Publish(new HideLoadingPanelMessage());
        }

        public void EnterNameComplete()
        {
            if (AppMode == AppMode.None) return;
            
            if (AppMode == AppMode.Host)
            {
                Locator.EventHub.Publish(new OpenLobbyPanelMessage());
            }
            else
            {
                Locator.EventHub.Publish(new OpenJoinPanelMessage());
            }
        }

        private void OnDestroy()
        {
            
        }
    }
}