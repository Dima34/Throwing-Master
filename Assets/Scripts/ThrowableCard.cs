using System;
using UnityEngine;

public class ThrowableCard : MonoBehaviour
{
    [SerializeField] private int _lifetime;
    [SerializeField] private float _flySpeed;
    [SerializeField] private Card _card;
    
    private Vector3 _flyVector;

    private void Update() =>
        transform.position += _flyVector * _flySpeed * Time.deltaTime;

    public void SetCardRotation(Quaternion newRotation) =>
        _card.transform.localRotation = newRotation;

    public void Throw(Vector3 to)
    {
        _flyVector = to.normalized;
        Destroy(gameObject, _lifetime);
    }
}