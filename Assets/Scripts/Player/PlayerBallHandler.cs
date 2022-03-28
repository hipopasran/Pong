using System.Collections;
using UnityEngine;

namespace Pong
{
    public class PlayerBallHandler : MonoBehaviour
    {
        [SerializeField] private Transform _ballStartPoint;

        public Transform BallStartPoint => _ballStartPoint;
    }
}
