using UnityEngine;

namespace DefaultNamespace
{
    public class Card : MonoBehaviour
    {
        [SerializeField] private CardObject _cardObject;
        [SerializeField] private GameObject _rootObjectToDestroy;
        [SerializeField] private float _randomRotaionAngleLimit;

        private void Start()
        {
            SetRandomZAngle();
            SubscribeToDestroyEvent();
        }

        private void OnDestroy() =>
            UnSubscribeDestroyEvent();

        private void SetRandomZAngle()
        {
            float randomZAngle = Random.Range(-_randomRotaionAngleLimit, _randomRotaionAngleLimit);
            transform.rotation *= Quaternion.Euler(new Vector3(0,0, randomZAngle));
        }

        private void SubscribeToDestroyEvent() =>
            _cardObject.OnCardTriggered += DestroyRoot;

        private void UnSubscribeDestroyEvent() =>
            _cardObject.OnCardTriggered -= DestroyRoot;

        public void DestroyRoot() =>
            Destroy(_rootObjectToDestroy);
    }
}