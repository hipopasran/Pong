using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class Locator : SingletonBehaviour<Locator>
{
        public GameLoopManager GameLoopManager;
        public BallHandleManager BallHandleManager;
        public LevelManager LevelManager;
        public PlayerHandleManager PlayerHandleManager;

        protected void Awake()
        {
            GameLoopManager = GetComponentInChildren<GameLoopManager>();
            BallHandleManager = GetComponentInChildren<BallHandleManager>();
            LevelManager = GetComponentInChildren<LevelManager>();
            PlayerHandleManager = GetComponentInChildren<PlayerHandleManager>();
        }
    }
}
