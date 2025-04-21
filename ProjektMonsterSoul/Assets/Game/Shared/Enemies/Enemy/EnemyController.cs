using System;
using UnityEngine;

namespace Game.Shared.Enemy
{
    public abstract class EnemyController : MonoBehaviour
    {
        public Action OnDeath;
    }
}