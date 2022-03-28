using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pong
{
    public class GameLoopManager : MonoBehaviour
    {
        private int _currentLife;
        [SerializeField] private int _startAmoutOfLife;
        
        // Respawn next Level + Respawn ball + Reset Life
        public void Win()
        {
            PrepareGame(true);
        }

        public void LoseLife()
        {
            _currentLife -= 1;
            if (_currentLife == 0)
            {
                Lose();
            }
            else
            {
                Locator.Instance.BallHandleManager.ActivateMainBall();
            }
        }

        private void Start()
        {
            _currentLife = _startAmoutOfLife;
            PrepareGame(false);
        }


        // Respawn level + Respawn ball + Reset life
        private void Lose()
        {
            PrepareGame(false);
        }

        private void PrepareGame(bool nextLevel)
        {
            _currentLife = _startAmoutOfLife;
            Locator.Instance.BallHandleManager.Prepare();

            if (nextLevel)
            {
                Locator.Instance.LevelManager.NextLevel();
            }
            else
            {
                Locator.Instance.LevelManager.ResetLevel();
            }
        }
    }
}
