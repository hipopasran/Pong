using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class LevelManager : MonoBehaviour
    {
        private int _currentLevelNumber;
        private Level _currentLevel;

        [SerializeField] private Transform _levelRoot;
        [SerializeField] private List<Level> _levels;

        public void NextLevel()
        {
            _currentLevelNumber += 1;
            if(_currentLevelNumber >= _levels.Count)
            {
                _currentLevelNumber = Random.Range(0, _levels.Count);
            }
            SpawnLevel(_currentLevelNumber);
        }

        public void ResetLevel()
        {
            if (_currentLevel)
            {
                _currentLevel.ResetValue();
            }
            else
            {
                SpawnLevel(_currentLevelNumber);
            }
        }

        private void SpawnLevel(int level)
        {
            if (_currentLevel) Destroy(_currentLevel);

            _currentLevel = Instantiate(_levels[level], new Vector2(0, 0), Quaternion.identity);
            _currentLevel.transform.SetParent(_levelRoot);
        }
    }
}
