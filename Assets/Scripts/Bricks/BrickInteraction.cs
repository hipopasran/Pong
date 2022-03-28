using UnityEngine;
using UnityEngine.Events;

namespace Pong
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider))]
    public class BrickInteraction : MonoBehaviour
    {
        private static int _count;

        private int _currentHealth;

        [SerializeField] protected int _health;
        [SerializeField] protected UnityEvent _onDestroy;

        private void OnEnable()
        {
            _currentHealth = _health;
            _count += 1;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.collider.tag == "Ball")
            {
                _currentHealth -= 1;
                if (_currentHealth == 0) Death();
            }
        }

        private void Death()
        {
            gameObject.SetActive(false);
            _onDestroy?.Invoke();

            _count -= 1;
            if (_count == 0)
            {
                if(Locator.Instance) Locator.Instance.GameLoopManager.Win();
            }
        }
    }
}
