using UnityEngine;

namespace Pong
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider2D))]
    public class PlayerMovement : MonoBehaviour
    {
        private float _minHorizontalPosition;
        private float _maxHorizontalPosition;

        private Collider2D _collider;
        private Rigidbody2D _rb;

        [SerializeField] private Collider2D _rightBound;
        [SerializeField] private Collider2D _leftBound;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _collider = GetComponent<Collider2D>();

            var playerExtent = _collider.bounds.size.x / 2;
            _minHorizontalPosition = _leftBound.transform.position.x + _leftBound.bounds.size.x / 2 + playerExtent;
            _maxHorizontalPosition = _rightBound.transform.position.x - _rightBound.bounds.size.x / 2 - playerExtent;
        }

        private void Start()
        {
            _rb.position = new Vector2(0, _rb.position.y);
        }

        private void Update()
        {
            Movement();
        }

        private void Movement()
        {
            var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var playerPosition = _rb.position;

            playerPosition.x = Mathf.Lerp(playerPosition.x, mousePosition.x, 10);
            playerPosition.x = Mathf.Clamp(playerPosition.x, _minHorizontalPosition, _maxHorizontalPosition);
            _rb.position = playerPosition;
        }
    }
}
