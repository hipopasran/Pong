using UnityEngine;
using System;

namespace Pong
{
    public class Ball : MonoBehaviour
    {
        public event Action<Ball> OnDisableBall;

        [SerializeField] private Rigidbody2D _rb;

        public Rigidbody2D Rb => _rb;

        public void Deactivate()
        {
            gameObject.SetActive(false);
            _rb.simulated = false;

            OnDisableBall?.Invoke(this);
        }
    }
}
