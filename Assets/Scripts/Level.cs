using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    public class Level : MonoBehaviour
    {
        private List<BrickInteraction> _bricks = new List<BrickInteraction>();

        public void ResetValue()
        {
            foreach(var item in _bricks)
            {
                item.gameObject.SetActive(true);
                item.ResetValue();
            }
        }

        private void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            var bricks = GetComponentsInChildren<BrickInteraction>();
            foreach(var item in bricks)
            {
                _bricks.Add(item);
            }
        }
    }
}
