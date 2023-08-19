using UnityEngine;

public class ThrowableObject : MonoBehaviour
{
    [SerializeField] private int _lifetime;
    [SerializeField] private float _flySpeed;
    
    private Vector3 _flyVector;

    private void Update() =>
        transform.position += _flyVector * _flySpeed * Time.deltaTime;

    public void Throw(Vector3 to)
    {
        _flyVector = to.normalized;
        Destroy(gameObject, _lifetime);
    }
}