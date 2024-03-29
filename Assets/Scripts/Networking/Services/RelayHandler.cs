using System;
using System.Threading.Tasks;
using Core;
using Networking.Core;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.Networking.Transport.Relay;
using Unity.Services.Relay;
using UnityEngine;
using Utils;

namespace Networking.Services
{
    public class RelayHandler
    {
        public string JoinCode;

        public async Task<bool> StartHost()
        {
            try
            {
                if (!AlignFiveApp.Instance.AuthHandler.LoginStatus) return false;
                
                var allocation = await RelayService.Instance.CreateAllocationAsync(1);
                
                NetworkManager.Singleton.GetComponent<UnityTransport>()
                    .SetRelayServerData(new RelayServerData(allocation, "dtls"));
                
                var joinCode = await RelayService.Instance.GetJoinCodeAsync(allocation.AllocationId);
                if (string.IsNullOrEmpty(joinCode))
                {
                    Debug.LogError("Something went wrong when trying to start host: JoinCode is empty or null!");

                    Locator.EventHub.Publish(new StartHostFailedMessage());
                    
                    return false;
                }

                JoinCode = joinCode;
                
                Locator.EventHub.Publish(new StartHostSuccessMessage());

                return NetworkManager.Singleton.StartHost();
            }
            catch (Exception e)
            {
                Debug.LogError($"Host Exception: {e}");
                
                Locator.EventHub.Publish(new StartHostFailedMessage());

                return false;
            }
        }

        public async Task<bool> JoinGame(string joinCode)
        {
            try
            {
                var joinAllocation = await RelayService.Instance.JoinAllocationAsync(joinCode: joinCode);
            
                NetworkManager.Singleton.GetComponent<UnityTransport>()
                    .SetRelayServerData(new RelayServerData(joinAllocation, "dtls"));
            
                return !string.IsNullOrEmpty(joinCode) && NetworkManager.Singleton.StartClient();
            }
            catch (Exception e)
            {
                Debug.LogError($"Join Game Exception: {e}");
                
                Locator.EventHub.Publish(new JoinGameFailedMessage());

                return false;
            }
        }
    }
}