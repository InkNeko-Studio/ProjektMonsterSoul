using System;
using System.Collections;
using Framework.SaveSystem;
using Game.Scenes.Map.Scripts;
using UnityEngine;

namespace Game.Shared.Enemy
{
    public abstract class EnemyController : MonoBehaviour, IDamageable
    {
        [Header("Info")]
        public int health;
        public int damage;
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                SaveController.CurrentSave.playerData.life -= damage;
                if (SaveController.CurrentSave.playerData.life <= 0)
                {
                    WinLoseController.Instance.Lose();
                }
                HitCanvas.SetHit(other.transform.position, damage);
            }
        }
    }
}