using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pong
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class ChangeColorByHealth : MonoBehaviour
    {
        private int _step = 0;
        private SpriteRenderer _spriteRenderer;

        [SerializeField] private List<Color> _colors;

        public void Hit()
        {
            _step += 1;
            if(_step < _colors.Count) _spriteRenderer.color = _colors[_step];
        }

        public void ResetValue()
        {
            _step = 0;
            _spriteRenderer.color = _colors[_step];
        }

        private void OnEnable()
        {
            ResetValue();
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
