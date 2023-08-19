using System;
using UnityEngine;

public class ThrowOpportunity : MonoBehaviour
{
    [SerializeField] private ThrowableObject objectObject;
    [SerializeField] private float _distanceFromCameraToTargets;
    [SerializeField] private LineRenderer _lineRenderer;

    private Vector3 _endPosition;

    private void Start()
    {
        SetRandomRotation();
    }

    private void SetRandomRotation()
    {
    }

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
        ThrowableObject @object = Instantiate(objectObject);
        
        @object.transform.position = transform.position + transform.forward * transform.localScale.z;
        @object.transform.forward = throwVector;
        
        @object.Throw(throwVector);
    }

}