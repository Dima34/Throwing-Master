using System;
using EzySlice;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    [SerializeField] private Material _crossSectionMaterial;
    [SerializeField] private float _explosionForce;
    [SerializeField] private float _explosionRadius;

    private const string TARGET_TAG = "Target";
    public event Action OnCardTriggered;

    private void OnTriggerEnter(Collider collider)
    {
        if (ObjectIsTarget(collider))
            SliceCollider(collider);

        FireOnCardTriggerEvent();
    }

    private void SliceCollider(Collider collider)
    {
        Vector3 slicePosition = GetSlicePosition();
        GameObject[] hulls =
            collider.gameObject.SliceInstantiate(slicePosition, transform.right, new TextureRegion(),
                _crossSectionMaterial);

        bool isSliced = hulls != null;
        if (isSliced)
        {
            SetupHulls(hulls);
            DestroyOldCollider(collider);
        }
    }

    private void SetupHulls(GameObject[] hulls)
    {
        foreach (GameObject hull in hulls)
            SetupHull(hull);
    }

    private void FireOnCardTriggerEvent() =>
        OnCardTriggered?.Invoke();
    
    private static void DestroyOldCollider(Collider collider) =>
        Destroy(collider.gameObject);

    private static bool ObjectIsTarget(Collider collider) =>
        collider.tag == TARGET_TAG;
    
    private Vector3 GetSlicePosition() =>
        transform.position + transform.forward * transform.localScale.x;

    private void SetupHull(GameObject hull)
    {
        SetTargetTag(hull);
        Rigidbody rb = CreateAndSetupRigidbody(hull);
        CreateAndSetupCollider(hull);
        CreateAndSetupTriggerCollider(hull);
        AddExposionForce(rb);
    }

    private void AddExposionForce(Rigidbody rb) =>
        rb.AddExplosionForce(_explosionForce, transform.position, _explosionRadius, 3);

    private void CreateAndSetupTriggerCollider(GameObject hull)
    {
        MeshCollider meshCollider = hull.AddComponent<MeshCollider>();
        meshCollider.convex = true;
        meshCollider.isTrigger = true;
    }

    private void SetTargetTag(GameObject hull) =>
        hull.tag = TARGET_TAG;

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