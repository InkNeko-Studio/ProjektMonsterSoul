using System;
using Game.Scenes.Map.Scripts;
using Game.Shared.Enemy;
using UnityEngine;
using Random = System.Random;

namespace Game.Shared.Enemies.Slime.Scripts
{
    public class SlimeController : EnemyController
    {
        [Header("Animators")]
        public Animator movementAnimator;
        public Animator spriteAnimator;

        [Header("Variable")]
        public float distanceToCloseAttack;
        
        private Transform _playerTransform;

        private float _timer = 0f;
        
        private void Awake()
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void FixedUpdate()
        {
            _timer -= Time.fixedDeltaTime;

            if (_timer <= 0f)
            {

                if (Vector3.Distance(_playerTransform.position, transform.position) <= distanceToCloseAttack)
                {
                    _timer = 10f;
                    
                    movementAnimator.SetTrigger("JumpCenter");
                    spriteAnimator.SetTrigger("JumpCenter");
                }
                else
                {
                    _timer = 10f;

                    if (new Random().Next(0, 2) == 0)
                    {
                        movementAnimator.SetTrigger("Spin");
                        spriteAnimator.SetTrigger("Spin");
                    }
                    else
                    {
                        movementAnimator.SetTrigger("JumpEdge");
                        spriteAnimator.SetTrigger("JumpEdge");
                    }
                    //spriteAnimator.SetTrigger("SpinAttack");                    
                }
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
    }
}
