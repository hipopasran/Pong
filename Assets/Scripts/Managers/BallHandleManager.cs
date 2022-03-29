using UnityEngine;
using System;
using Random = UnityEngine.Random;
using System.Collections.Generic;

namespace Pong
{
    public class BallHandleManager : MonoBehaviour
    {
        public event Action OnAllBallDestroy;

        private bool _isTrow;
        private Ball _mainBall;
        private PlayerBallHandler _playerBallHandler;

        private List<Ball> _activeBalls = new List<Ball>();
        private List<Ball> _disableBalls = new List<Ball>();

        [SerializeField] private Ball _ballPrefab;
        [SerializeField] private float _ballSpeed;

        public void Prepare()
        {
            ClearActiveBalls();
            ActivateBall(_playerBallHandler.BallStartPoint, true);
        }

        public void SpawnBall(Transform point)
        {
            ActivateBall(point);
        }


        private void ClearActiveBalls()
        {
            foreach(var item in _activeBalls)
            {
                item.gameObject.SetActive(false);
                item.Rb.simulated = false;
                _disableBalls.Add(item);
            }

            _activeBalls.Clear();
        }

        private void ActivateBall(Transform point, bool mainBall = false)
        {
            Ball ball = null;
            if (_disableBalls.Count > 0)
            {
                ball = _disableBalls[0];
                _disableBalls.Remove(ball);
                ball.transform.position = point.position;
                ball.gameObject.SetActive(true);
            }
            else
            {
                ball = Instantiate(_ballPrefab, point.position, Quaternion.identity);
            }

            _activeBalls.Add(ball);
            ball.OnDisableBall += DisableBall;

            if (mainBall)
            {
                ball.transform.SetParent(_playerBallHandler.transform);
                ball.transform.localPosition = _playerBallHandler.BallStartPoint.localPosition;
                _mainBall = ball;
                _isTrow = false;
            }
            else
            {
                ball.Rb.simulated = true;
                var direction = Random.insideUnitCircle.normalized;
                ball.Rb.velocity = direction * _ballSpeed;
            }
        }

        private void DisableBall(Ball ball)
        {
            _activeBalls.Remove(ball);
            _disableBalls.Add(ball);

            ball.OnDisableBall -= DisableBall;

            if (_activeBalls.Count == 0) Locator.Instance.GameLoopManager.LoseLife();
        }

        private void Awake()
        {
            _playerBallHandler = Locator.Instance.PlayerHandleManager.PlayerBallHandler;

            CreatePoolBall();
        }

        private void CreatePoolBall()
        {
            for (int i = 0; i < 10; i++)
            {
                var ball = Instantiate(_ballPrefab, _playerBallHandler.BallStartPoint.position, Quaternion.identity);
                ball.gameObject.SetActive(false);
                _disableBalls.Add(ball);
            }
        }

        private void Update()
        {
            if (_isTrow) return;
            if ( Input.GetMouseButtonDown(0))
            {
                TrowMainBall();
            }
        }

        private void TrowMainBall()
        {
            _isTrow = true;
            _mainBall.transform.SetParent(null);
            _mainBall.Rb.simulated = true;
            var direction = new Vector2(Random.Range(-0.5f, 0.5f), 0.5f).normalized;
            _mainBall.Rb.velocity = direction * _ballSpeed;
        }
    }
}
