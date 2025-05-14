using System;
using Game.Scenes.Map.Scripts;
using Game.Shared.Enemy;
using UnityEngine;
using Random = System.Random;

namespace Game.Shared.Enemies.Slime.Scripts
{
    public class MultikubeController : EnemyController
    {
        [Header("Animators")]
        public Animator movementAnimator;
        public Animator spriteAnimator;

        [Header("Variable")]
        public float distanceToCloseAttack;
        
        private Transform _playerTransform;

        private float _timer = 10f;

        private bool mood;
        private void Awake()
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void FixedUpdate()
        {
            _timer -= Time.fixedDeltaTime;
            
            if (_timer <= 0f)
            {
                _timer = 10f;
                
                
            }
        }

        public void StartAttack()
        {
            invincible = true;
        }

        public void EndAttack()
        {
            invincible = false;
        }

        protected override void OnDeath()
        {
            spriteAnimator.SetTrigger("Die");
            
            WinLoseController.Instance.Win();
        }

        protected override void OnTakeDamage(int damage)
        {
            HitCanvas.SetHit(transform.position, damage);
            spriteAnimator.SetTrigger("TakeDamage");
        }

        public void ChekZone(bool zone)
        {
            if (zone)
            {
                mood = true;
            }
            else
            {
                mood = false;
            }

        }

        private void BattleMode()
        {
            if (mood)
            {
                movementAnimator.SetTrigger("Jump");
                spriteAnimator.SetTrigger("Jump");
                Debug.Log("Battle");
            }
        }

        private void RestingMode()
        {
            Debug.Log("OutBattle");
        }
    }
}
