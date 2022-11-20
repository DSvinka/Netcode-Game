using System;
using Code.Abstractions;
using Code.Consts;
using Code.Extensions;
using Code.Singletons;
using Unity.Netcode;
using UnityEngine;

namespace Code.Vehicle
{
    public class VehicleController: NetworkBehaviour
    {
        [SerializeField, ShowOnly] private VehicleView _vehicleView;
        [SerializeField, ShowOnly] private VehicleHudView _vehicleHudView;

        private Vector2 _movement;
        private float _health;
        private string _playerName;

        public float Health => _health;
        public string PlayerName => _playerName;

        public VehicleView VehicleView => _vehicleView;

        private void Start()
        {
            _vehicleView = GetComponentInChildren<VehicleView>();
            _vehicleHudView = GetComponentInChildren<VehicleHudView>();
        }

        public void UpdateInput(Vector2 movement)
        {
            if (!IsServer)
                return;
            
            _movement = movement;
        }

        public void UpdateInfo(string playerName, float health)
        {
            _vehicleHudView.SetHealth(health);
            _vehicleHudView.SetPlayerName(playerName);
        }

        private void Update()
        {
            if (!IsServer)
                return;

            foreach (var wheel in _vehicleView.Wheels)
            {
                if (_movement.y > 0 || _movement.y < 0)
                {
                    if (wheel.IsMotor)
                    {
                        wheel.Wheel.motorTorque = _movement.y * _vehicleView.Speed * 100;
                    }
                }
                
                if (_movement.x > 0 || _movement.x < 0)
                {
                    if (wheel.IsSteering)
                    {
                        wheel.Wheel.steerAngle = _movement.x * 45;
                    }
                }
            }
        }
        
        private void OnCollisionEnter(Collision collision)
        {
            if (!IsServer) 
                return;

            if (_vehicleView.Rigidbody.velocity.magnitude <= PlayerSettings.MinimalRigidbodyMagnitudeForDamage)
                return;

            _vehicleView.AddDamage(_vehicleView.Rigidbody.velocity.magnitude * PlayerSettings.DamageOnCollisionModificator);
        }
    }
}