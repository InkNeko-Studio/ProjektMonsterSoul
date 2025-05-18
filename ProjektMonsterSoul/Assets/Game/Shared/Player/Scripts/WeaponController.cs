using System;
using Framework.ItemSystem;
using Framework.SaveSystem;
using Game.Shared.Enemy;
using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class WeaponController : MonoBehaviour
    {
        public SpriteRenderer spriteRenderer;
        private WeaponId _lastWeapon;
        
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
            if (other.CompareTag("Damageable"))
            {
                var damageable = other.GetComponent<IDamageable>();
                var weapon = AllWeapon.GetWeapon(SaveController.CurrentSave.playerData.equippedWeapon);
                damageable.TakeDamage(weapon.weaponDamage);
            }
        }

        public void SetSprite()
        {
            if (SaveController.CurrentSave.playerData.equippedWeapon != _lastWeapon)
            {
                var weapon = AllWeapon.GetWeapon(SaveController.CurrentSave.playerData.equippedWeapon);
                spriteRenderer.sprite = weapon.weaponSprite;
                _lastWeapon = SaveController.CurrentSave.playerData.equippedWeapon;
            }
        }
    }
}
