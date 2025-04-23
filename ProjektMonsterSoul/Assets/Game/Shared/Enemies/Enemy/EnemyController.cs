using System;
using UnityEngine;

namespace Game.Shared.Enemy
{
    public abstract class EnemyController : MonoBehaviour, IDamageable
    {
        public int health;
        public bool invincible;
        
        public Action OnDeathEvent;
        public void TakeDamage(int damage)
        {
            if (invincible) return;
            
            health -= damage;
            OnTakeDamage(damage);

            if (health <= 0)
            {
                OnDeathEvent?.Invoke();
                OnDeath();
            }
        }

        protected abstract void OnDeath();
        protected abstract void OnTakeDamage(int damage);
    }
}