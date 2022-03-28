using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class PlayerHandleManager : MonoBehaviour
    {
        [SerializeField] private PlayerBallHandler _playerBallHandler;
        [SerializeField] private PlayerMovement _playerMovement;

        public PlayerBallHandler PlayerBallHandler => _playerBallHandler;
        public PlayerMovement PlayerMovement => _playerMovement;
    }
}
