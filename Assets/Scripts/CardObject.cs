using System;
using EzySlice;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    public event Action OnCardTriggered;

    private void OnTriggerEnter(Collider collider)
    {
        if (ObjectIsTarget(collider))
            SliceCollider(collider);

        FireOnCardTriggerEvent();
    }

    private void SliceCollider(Collider collider)
    {
        Target target = collider.GetComponent<Target>();
        Vector3 slicePosition = GetSlicePosition();
        GameObject[] hulls =
            collider.gameObject.SliceInstantiate(slicePosition, transform.right, new TextureRegion(),
                target.CrossSectionMaterial);

        bool isSliced = hulls != null;
        if (isSliced)
        {
            SetupHulls(hulls, collider.gameObject);
            DestroyOldCollider(collider);
        }
    }

    private void SetupHulls(GameObject[] hulls, GameObject slicedObject)
    {
        foreach (GameObject hull in hulls)
            SetupHull(hull, slicedObject);
    }

    private void FireOnCardTriggerEvent() =>
        OnCardTriggered?.Invoke();
    
    private static void DestroyOldCollider(Collider collider) =>
        Destroy(collider.gameObject);

    private static bool ObjectIsTarget(Collider collider) =>
        collider.tag == Tags.TARGET_TAG;
    
    private Vector3 GetSlicePosition() =>
        transform.position + transform.forward * transform.localScale.x;

    private void SetupHull(GameObject hull, GameObject originObject)
    {
        SetOriginsObjectParent(hull, originObject);
        SetTargetAttributes(hull, originObject);
        Rigidbody rb = CreateAndSetupRigidbody(hull);
        CreateAndSetupCollider(hull);
        CreateAndSetupTriggerCollider(hull);
        AddExposionForce(rb);
    }

    private void SetOriginsObjectParent(GameObject hull, GameObject originObject) =>
        hull.transform.SetParent(originObject.transform.parent);

    private void AddExposionForce(Rigidbody rb) =>
        rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, 3);

    private void CreateAndSetupTriggerCollider(GameObject hull)
    {
        MeshCollider meshCollider = hull.AddComponent<MeshCollider>();
        meshCollider.convex = true;
        meshCollider.isTrigger = true;
    }

    private void SetTargetAttributes(GameObject hull, GameObject slicedObject)
    {
        hull.tag = Tags.TARGET_TAG;

        CreateAndSetupNewTarget(hull, slicedObject);
    }

    private static void CreateAndSetupNewTarget(GameObject hull, GameObject slicedObject)
    {
        Target oldTarget = slicedObject.GetComponent<Target>();
        hull.AddComponent<Target>().InitializeFromTarget(oldTarget);
    }

    private static void CreateAndSetupCollider(GameObject hull)
    {
        MeshCollider meshCollider = hull.AddComponent<MeshCollider>();
        meshCollider.convex = true;
    }

    private static Rigidbody CreateAndSetupRigidbody(GameObject hull)
    {
        Rigidbody rb = hull.AddComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        return rb;
    }
}