using System;
using System.Collections.Generic;
using Code.Abstractions;
using Code.Extensions;
using UnityEngine;

namespace Code.Vehicle
{
    public class VehicleView: MonoBehaviour, IDamageable
    {
        public event Action<float> OnDamage;
        
        [SerializeField] private float _speed;
        [SerializeField, ShowOnly] private WheelView[] _wheels;
        [SerializeField, ShowOnly] private Rigidbody _rigidbody;

        public float Speed => _speed;
        public IReadOnlyCollection<WheelView> Wheels => _wheels;
        public Rigidbody Rigidbody => _rigidbody;


        private void Start()
        {
            _wheels = GetComponentsInChildren<WheelView>();
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void AddDamage(float damage)
        {
            OnDamage?.Invoke(damage);
        }
    }
}