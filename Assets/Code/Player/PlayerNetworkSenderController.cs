using Code.Singletons;
using Code.Vehicle;
using Newtonsoft.Json;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.Player
{
    public class PlayerNetworkSenderController: NetworkBehaviour
    {
        [ServerRpc]
        public void SendMovementInputServerRPC(Vector2 movement)
        {
            if (NetworkManager.ConnectedClients.ContainsKey(OwnerClientId))
            {
                var client = NetworkManager.ConnectedClients[OwnerClientId];
                if (client.PlayerObject.gameObject.TryGetComponent<VehicleController>(out var vehicleController))
                {
                    vehicleController.UpdateInput(movement);
                }
            }
        }
        
        [ServerRpc]
        public void SendPlayerInfoServerRPC(string playerName, float health)
        {
            if (NetworkManager.ConnectedClients.ContainsKey(OwnerClientId))
            {
                var client = NetworkManager.ConnectedClients[OwnerClientId];
                if (client.PlayerObject.gameObject.TryGetComponent<VehicleController>(out var vehicleController))
                {
                    vehicleController.UpdateInfo(playerName, health);
                }
            }
        }

        [ClientRpc]
        public void SendDamageClientRPC(ulong ownerClientId, float damage)
        {
            if (PlayerDataManager.ClientId == ownerClientId)
            {
                PlayerDataManager.Health -= damage;

                var client = NetworkManager.ConnectedClients[PlayerDataManager.ClientId];
                if (client.PlayerObject.gameObject.TryGetComponent<VehicleController>(out var vehicleController))
                {
                    SendPlayerInfoServerRPC(PlayerDataManager.PlayerName, PlayerDataManager.Health);
                }

                if (PlayerDataManager.Health <= 0)
                {
                    NetworkManager.DisconnectClient(ownerClientId);
                    if (IsHost && IsServer)
                        NetworkManager.Shutdown();
                    
                    SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
                }
            }
        }
    }
}