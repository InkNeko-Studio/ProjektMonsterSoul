using UnityEngine;

namespace Game.Shared.Player.Scripts
{
    public enum Direction
    {
        Up, Down, Left, Right
    }
    
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Values")]
        [SerializeField] private float moveSpeed;

        [HideInInspector] public Direction direction;
        
        private Rigidbody2D _rigidbody;
        private Vector2 _movement;
    
        private PlayerController _playerController;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _playerController = GetComponent<PlayerController>();
            direction = Direction.Down;
        }
        private void OnEnable() { _playerController.OnMovement += OnMovement; }
        private void OnDisable() { _playerController.OnMovement -= OnMovement; }

        private void OnMovement(Vector2 movement)
        {
            _movement = movement * moveSpeed;
            if (_movement.x > 0.0f) direction = Direction.Right;
            if (_movement.x < 0.0f) direction = Direction.Left;
            if (_movement.y > 0.0f) direction = Direction.Up;
            if (_movement.y < 0.0f) direction = Direction.Down;
        }

        private void FixedUpdate()
        {
            _rigidbody.MovePosition(_rigidbody.position + _movement * Time.fixedDeltaTime);
        }
    }
}
