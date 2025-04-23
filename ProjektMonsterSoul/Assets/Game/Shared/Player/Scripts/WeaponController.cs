using System;
using Framework.SaveSystem;
using Game.Shared.Enemy;
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

        private void OnTriggerEnter2D(Collider2D other)
        {
            Debug.Log(other.gameObject.name);
            if (other.CompareTag("Damageable"))
            {
                Debug.Log($"ISDAMAGEABLE {other.gameObject.name}");
                var damageable = other.GetComponent<IDamageable>();
                damageable.TakeDamage(SaveController.CurrentSave.playerData.equippedWeapon.damage);
                HitCanvas.SetHit(other.transform.position, SaveController.CurrentSave.playerData.equippedWeapon.damage);
            }
        }
    }
}
