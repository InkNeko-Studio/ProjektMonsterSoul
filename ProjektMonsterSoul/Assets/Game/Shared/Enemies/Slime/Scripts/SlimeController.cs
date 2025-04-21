using System;
using Game.Shared.Enemy;
using UnityEngine;

namespace Game.Shared.Enemies.Slime.Scripts
{
    public class SlimeController : EnemyController
    {
        [Header("Animators")]
        public Animator movementAnimator;
        public Animator spriteAnimator;

        [Header("")]
        
        private Transform _playerTransform;

        private float _timer = 0f;
        private bool _invicibility = false;
        
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
                
                // Condition
                movementAnimator.SetTrigger("SpinAttack");
                spriteAnimator.SetTrigger("SpinAttack");
            }
        }

        public void StartAttack()
        {
            _invicibility = true;
        }

        public void EndAttack()
        {
            _invicibility = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Weapon") && !_invicibility)
            {
                spriteAnimator.SetTrigger("TakeDamage");
            }
        }
    }
}
