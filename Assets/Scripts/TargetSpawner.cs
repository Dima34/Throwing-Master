using UnityEngine;

namespace DefaultNamespace
{
    public class TargetSpawner : MonoBehaviour
    {
        [SerializeField] private Target _targetToSpawn;

        private void Start() =>
            SpawnTarget();

        private void SpawnTarget()
        {
            Debug.Log($"Call SpawnTargetFrom {transform.GetInstanceID()}");
            Target target = Instantiate(_targetToSpawn, transform);
            target.transform.position = transform.position + transform.up * (target.transform.lossyScale.y / 2);
        }

        private void CleanupSpawner()
        {
            foreach (Transform spawnerChild in transform) 
                Destroy(spawnerChild.gameObject);
        }

        public void ResetSpawner()
        {
            CleanupSpawner();
            SpawnTarget();
        }
    }
}