using System;

namespace Code.Abstractions
{
    public interface IDamageable
    {
        event Action<float> OnDamage;
        
        void AddDamage(float damage);
    }
}