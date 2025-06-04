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

        private bool mood;
        public GameObject _HitController;
        private void Awake()
        {
            _playerTransform = GameObject.FindWithTag("Player").transform;
        }

        private void FixedUpdate()
        {
            
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
                Invoke("SetHit",2);
            }
        }

        private void RestingMode()
        {
            if (!mood)
            {
                movementAnimator.SetTrigger("Move");
                spriteAnimator.SetTrigger("Move");
            }
        }

        public void StopAndHit(string id)
        {
            movementAnimator.enabled = false;
            spriteAnimator.SetTrigger(id);
            Invoke("ReturnToMove",3);
           
        }

        private void ReturnToMove()
        {
            movementAnimator.enabled = true;
        }

        private void SetHit()
        {
            _HitController.SetActive(true);
        }

        public void AreaHit()
        {
            spriteAnimator.SetTrigger("Area");
        }
    }
}
