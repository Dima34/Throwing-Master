using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class TargetRespawner : MonoBehaviour
    {
        [SerializeField] private Button _respawnTargetButton;

        private void Start() =>
            _respawnTargetButton.onClick.AddListener(RespawnTargets);

        private void RespawnTargets()
        {
            TargetSpawner[] targetSpawners = FindObjectsOfType<TargetSpawner>();

            foreach (TargetSpawner targetSpawner in targetSpawners) 
                targetSpawner.ResetSpawner();
        }
    }
}