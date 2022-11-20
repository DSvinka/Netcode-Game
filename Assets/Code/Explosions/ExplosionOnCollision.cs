using System;
using System.Security.Cryptography;
using Code.Abstractions;
using Code.Player;
using Unity.Netcode;
using UnityEngine;

namespace Code.Explosions
{
    public class ExplosionOnCollision: NetworkBehaviour
    {
        [SerializeField] private float _damage;
        [SerializeField] private float _damageRadius;
        
        private bool _activated;

        private void OnCollisionEnter(Collision collision)
        {
            if (!IsServer) 
                return;
            
            if (_activated) 
                return;

            _activated = true;
            var hits = Physics.SphereCastAll(transform.position, _damageRadius, Vector3.up);
            if (hits.Length == 0)
            {
                var networkObject = GetComponent<NetworkObject>();
                networkObject.Despawn(true);
            }

            foreach (var hit in hits)
            {
                if (hit.collider.gameObject.TryGetComponent<IDamageable>(out var damageable))
                {
                    damageable.AddDamage((_damage / _damageRadius) * (_damageRadius / (hit.distance+1)));
                }
            }
            
            var obj = GetComponent<NetworkObject>();
            obj.Despawn(true);
        }
    }
}