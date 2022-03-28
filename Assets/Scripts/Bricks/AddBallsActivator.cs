using UnityEngine;

namespace Pong
{
    public class AddBallsActivator : MonoBehaviour
    {
        [SerializeField] private int _ballCount;

        public void Activate()
        {
            for(int i = 0; i < _ballCount; i++)
            {
                Locator.Instance.BallHandleManager.ActivateBall(transform);
            }
        }
    }
}
