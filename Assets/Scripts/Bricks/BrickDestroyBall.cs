using UnityEngine;

namespace Pong
{
    [RequireComponent(typeof(Rigidbody2D), typeof(Collider))]
    public class BrickDestroyBall : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D collision)
        {
            var other = collision.collider.gameObject;
            if (other.tag == "Ball")
            {
                var ball = other.GetComponent<Ball>();
                if (ball) ball.Deactivate();
            }
        }
    }
}
