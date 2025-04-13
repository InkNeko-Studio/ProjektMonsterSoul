using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class WeaponController : MonoBehaviour
    {
        [HideInInspector] public bool attacking;

        public void StartAttacking()
        {
            attacking = true;
        }

        public void StopAttacking()
        {
            attacking = false;
        }
    }
}
