using UnityEngine;

namespace DefaultNamespace
{
    public class DelayedDestroyer : MonoBehaviour
    {
        public void StartDelay(float delayTime)
        {
            Debug.LogWarning("Delayed destroyer set up");
            Destroy(gameObject, delayTime);
        }
    }
}