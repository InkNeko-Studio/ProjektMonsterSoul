using System;
using UnityEngine;

namespace Game.Shared.Enemy
{
    public class HitboxTest : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        private void Start()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void FixedUpdate()
        {
            _spriteRenderer.color += new Color(1, 1, 1) * Time.fixedDeltaTime;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Weapon"))
            {
                _spriteRenderer.color = Color.red;
                HitCanvas.SetHit(transform.position, 99);
            }
        }
    }
}
