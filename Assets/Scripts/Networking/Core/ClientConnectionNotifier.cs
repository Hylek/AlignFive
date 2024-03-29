using System;
using Unity.Netcode;

namespace Networking.Core
{
    public enum ConnectionStatus
    {
        Connected,
        Disconnected
    }
    
    /// <summary>
    /// Used by both the Host and Connecting Players to determine their connection status.
    /// </summary>
    public class ClientConnectionNotifier : IDisposable
    {
        public event Action<ulong, ConnectionStatus> OnClientConnectionNotification;

        public ClientConnectionNotifier() => Init();

        private void Init()
        {
            NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnectCallback;
        }

        private void OnClientConnectedCallback(ulong id)
        {
            OnClientConnectionNotification?.Invoke(id, ConnectionStatus.Connected);
        }
        
        private void OnClientDisconnectCallback(ulong id)
        {
            OnClientConnectionNotification?.Invoke(id, ConnectionStatus.Disconnected);
        }

        public void Dispose()
        {
            NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnectedCallback;
            NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnectCallback;
        }
    }
}