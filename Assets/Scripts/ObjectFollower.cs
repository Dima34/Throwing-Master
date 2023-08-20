using UnityEngine;

public class ObjectFollower : MonoBehaviour
{
    private Transform _transformToFollow;

    public void SetFollowTransform(Transform transformToFollow) =>
        _transformToFollow = transformToFollow;

    private void Update()
    {
        if (_transformToFollow != null)
            transform.position = _transformToFollow.transform.position;
    }
}