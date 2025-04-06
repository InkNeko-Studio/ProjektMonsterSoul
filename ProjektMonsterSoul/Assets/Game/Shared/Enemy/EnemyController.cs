using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Shared.Enemy
{
    public class EnemyController : MonoBehaviour, IDamageable
    {
        public EnemyState currentState;
        public int health;
        
        public Animator animator;
        private static readonly int Dead = Animator.StringToHash("Dead");
        private static readonly int Attack1 = Animator.StringToHash("Attack1");
        private static readonly int Attack2 = Animator.StringToHash("Attack2");
        private static readonly int Attack3 = Animator.StringToHash("Attack3");

        private void Start()
        {
            currentState = EnemyState.Passive;
        }

        private void SetState(EnemyState state)
        {
            if (currentState == EnemyState.Dead) return;
            currentState = state;
            
            switch (state)
            {
                case EnemyState.Aggressive:
                    SetAttack();
                    break;
                case EnemyState.Dead:
                    SetDie();
                    break;
                case EnemyState.Passive: break;
            }
        }

        private void SetAttack()
        {
            int attack = Random.Range(0, 2);
            switch (attack)
            {
                case 0: animator.SetTrigger(Attack1); break;
                case 1: animator.SetTrigger(Attack2); break;
                case 2: animator.SetTrigger(Attack3); break;                
            }
        }

        private void SetDie()
        {
            animator.SetBool(Dead, true);
        }

        public void TakeDamage(int damage)
        {
            health -= damage;
            if (health <= 0) SetState(EnemyState.Dead);
            SetState(EnemyState.Aggressive);
        }
    }
}
