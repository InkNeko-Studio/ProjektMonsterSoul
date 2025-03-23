using Framework.SaveSystem;
using Game.Shared.Player.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Framework.AreaSystem
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class AreaBorder : MonoBehaviour
    {
        public Area area;
        public Vector3 offset;
        
        private BoxCollider2D _collider;

        private void Awake()
        {
            _collider = GetComponent<BoxCollider2D>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponentInParent<PlayerController>() != null)
            {
                SceneManager.LoadScene(area.ToString());
            }
        }

        public void SetPlayerPosition(Transform playerTransform)
        {
            playerTransform.position = transform.position + offset;
        }
    }
}
