using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public class PlayerCombat : MonoBehaviour
    {
        private static readonly int Slash = Animator.StringToHash("Slash");

        [Header("References")]
        [SerializeField] private Transform direction;
        [SerializeField] private Animator animator;
        [SerializeField] private WeaponController weapon;
        
        private PlayerController _playerController;
        private PlayerMovement _playerMovement;
        
        private void Awake()
        {
            _playerController = GetComponent<PlayerController>();
            _playerMovement = GetComponent<PlayerMovement>();
        }

        private void OnEnable()
        {
            _playerController.OnAttack += OnAttack; 
            
        }
        private void OnDisable() { _playerController.OnAttack -= OnAttack; }

        private void OnAttack()
        {
            if (weapon.attacking) return;
            
            switch (_playerMovement.direction)
            {
                case Direction.Up:
                    direction.rotation = Quaternion.Euler(0f, 0f, 0f);
                    break;
                case Direction.Down:
                    direction.rotation = Quaternion.Euler(0f, 0f, 180f);
                    break;
                case Direction.Left:
                    direction.rotation = Quaternion.Euler(0f, 0f, 90f);
                    break;
                case Direction.Right:
                    direction.rotation = Quaternion.Euler(0f, 0f, 270f);
                    break;
            }
            animator.SetTrigger(Slash);
            SoundManager.Instance.PlaySFX(1);
        }
    }
}
