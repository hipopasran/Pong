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

        [SerializeField] private List<Ball> _activeBalls = new List<Ball>();
        [SerializeField] private List<Ball> _disableBalls = new List<Ball>();

        [SerializeField] private Ball _ballPrefab;
        [SerializeField] private float _ballSpeed;

        public void Prepare()
        {
            ClearActiveBalls();
            ActivateMainBall();
        }

        public void ActivateBall(Transform point)
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

            ball.Rb.simulated = true;
            var direction = Random.insideUnitCircle.normalized;
            ball.Rb.velocity = direction * _ballSpeed;
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

        public void ActivateMainBall()
        {
            Debug.Log("KEK");

            Ball ball = null;
            if (_disableBalls.Count > 0)
            {
                ball = _disableBalls[0];
                _disableBalls.Remove(ball);
                ball.transform.position = _playerBallHandler.BallStartPoint.position;
                ball.gameObject.SetActive(true);
            }
            else
            {
                ball = Instantiate(_ballPrefab, _playerBallHandler.BallStartPoint.position, Quaternion.identity);
            }

            ball.transform.SetParent(_playerBallHandler.transform);
            _activeBalls.Add(ball);
            _mainBall = ball;

            ball.OnDisableBall += DisableBall;

            _isTrow = false;
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
            if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
            {
                Debug.Log("Touch");

                TrowMainBall();
            }
        }

        private void TrowMainBall()
        {
            _isTrow = true;
            _mainBall.transform.SetParent(null);
            _mainBall.Rb.simulated = true;
            var direction = new Vector2(Random.Range(-0.5f, 0.5f), 0.5f).normalized;
            Debug.Log(direction);
            _mainBall.Rb.velocity = direction * _ballSpeed;
        }
    }
}
