using System.Threading.Tasks;
using Unity.Services.Authentication;
using Unity.Services.Core;
using UnityEngine;

namespace Networking.Services
{
    public class AuthHandler
    {
        public bool LoginStatus;
        public string PlayerID;

        public AuthHandler()
        {
            if (UnityServices.State == ServicesInitializationState.Uninitialized)
            {
                UnityServices.InitializeAsync();
            }
        }

        public async Task<bool> Login()
        {
            try
            {
                await AuthenticationService.Instance.SignInAnonymouslyAsync();

                PlayerID = AuthenticationService.Instance.PlayerId;
                
                Debug.Log("Sign in anonymously succeeded!");
                Debug.Log($"PlayerID: {AuthenticationService.Instance.PlayerId}");

                LoginStatus = true; 
                
                return true;
            }
            catch (AuthenticationException ex)
            {
                Debug.LogException(ex);

                LoginStatus = false;
                
                return false;
            }
            catch (RequestFailedException ex)
            {
                Debug.LogException(ex);

                LoginStatus = false;
                
                return false;
            }
        }
    }
}