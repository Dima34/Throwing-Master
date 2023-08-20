using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private CardObject _cardObject;
        [SerializeField] private GameObject _rootObjectToDestroy;
        [SerializeField] private float _randomRotaionAngleLimit;
        [SerializeField] private ObjectFollower _cardFollowTrail;
        [SerializeField] private float _trailAfterDestroyLifeTime;
        
        private ObjectFollower _spawnedTrail;

        private void Start()
        {
            SetRandomZAngle();
            SubscribeToDestroyEvent();
            SpawnAndSetupTrail();
        }

        private void SpawnAndSetupTrail()
        {
            _spawnedTrail = Instantiate(_cardFollowTrail, transform.position, Quaternion.identity);
            _spawnedTrail.SetFollowTransform(transform);
        }

        private void OnDestroy() =>
            UnSubscribeDestroyEvent();

        private void SetRandomZAngle()
        {
            float randomZAngle = Random.Range(-_randomRotaionAngleLimit, _randomRotaionAngleLimit);
            transform.rotation *= Quaternion.Euler(new Vector3(0,0, randomZAngle));
        }

        private void SubscribeToDestroyEvent() =>
            _cardObject.OnCardTriggered += DestroyRootAndSetDeleyedDestroyOfTrail;

        private void UnSubscribeDestroyEvent() =>
            _cardObject.OnCardTriggered -= DestroyRootAndSetDeleyedDestroyOfTrail;

        public void DestroyRootAndSetDeleyedDestroyOfTrail()
        {
            SetupDelayedTrailDestroyer();
            DestroyRoot();
        }

        private void SetupDelayedTrailDestroyer()
        {
            DelayedDestroyer delayedDestroyer = _spawnedTrail.GetComponent<DelayedDestroyer>();
            delayedDestroyer.StartDelay(_trailAfterDestroyLifeTime);
        }

        private void DestroyRoot() =>
            Destroy(_rootObjectToDestroy);
    }
}