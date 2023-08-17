using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class Throw : MonoBehaviour
{
    [SerializeField] private ThrowableCard _cardObject;
    [SerializeField] private float _distanceFromCameraToTargets;
    [SerializeField] private LineRenderer _lineRenderer;

    private Vector3 _endPosition;
    
    private const float MAX_ROTATION_DEGREE = 70;
    

    private void OnMouseDrag()
    {
        Vector3 mouseInWorldPosition = MouseInWorldPosition(Input.mousePosition);
        
        SetAimLineToTarget(mouseInWorldPosition);
        _endPosition = -(transform.position - mouseInWorldPosition); 
    }

    private void OnMouseUp()
    {
        RemoveAimLine();
        ThowCard(_endPosition);
    }

    private void SetAimLineToTarget(Vector3 mouseInWorldPosition) =>
        _lineRenderer.SetPosition(1, mouseInWorldPosition);

    private void RemoveAimLine() =>
        _lineRenderer.SetPosition(1, transform.position);

    private Vector3 MouseInWorldPosition(Vector3 mousePosition) =>
        Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, _distanceFromCameraToTargets));

    private void ThowCard(Vector3 throwVector)
    {
        ThrowableCard card = Instantiate(_cardObject);
        
        card.transform.position = transform.position + transform.forward * transform.localScale.z;
        card.transform.forward = throwVector;
        
        card.SetCardRotation(GetRandomZRotation());
        card.Throw(throwVector);
    }

    private Quaternion GetRandomZRotation()
    {
        float randomZ = Random.Range(-MAX_ROTATION_DEGREE, MAX_ROTATION_DEGREE);
        return Quaternion.Euler(new Vector3(0,0,randomZ));
    }

}