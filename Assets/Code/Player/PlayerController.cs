using System;
using Code.Abstractions;
using Code.Extensions;
using Code.Singletons;
using Code.Vehicle;
using Unity.Netcode;
using UnityEngine;

namespace Code.Player
{
    public class PlayerController: NetworkBehaviour, IDamageable
    {
        public event Action<float> OnDamage;
        
        [SerializeField, ShowOnly] private PlayerNetworkSenderController _playerNetworkSender;
        [SerializeField, ShowOnly] private VehicleController _vehicleController;
        [SerializeField, ShowOnly] private Vector2 _input;

        private void Start()
        {
            if (!IsClient && !IsOwner)
                return;

            _vehicleController = GetComponentInChildren<VehicleController>();
            _vehicleController.VehicleView.OnDamage += AddDamage;
            
            _playerNetworkSender = GetComponentInChildren<PlayerNetworkSenderController>();
            _playerNetworkSender.SendPlayerInfoServerRPC(PlayerDataManager.PlayerName, PlayerDataManager.Health);
        }

        private void Update()
        {
            if (!IsClient && !IsOwner)
                return;
            
            _input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            _playerNetworkSender.SendMovementInputServerRPC(_input);
        }

        public void AddDamage(float damage)
        {
            if (!IsServer)
                return;

            _playerNetworkSender.SendDamageClientRPC(OwnerClientId, damage);

            OnDamage?.Invoke(damage);
        }
    }
}