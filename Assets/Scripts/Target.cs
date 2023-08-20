using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private Material _crossSectionMaterial;

    public Material CrossSectionMaterial
    {
        get => _crossSectionMaterial;
        set => _crossSectionMaterial = value;
    }

    public void InitializeFromTarget(Target target)
    {
        _crossSectionMaterial = target.CrossSectionMaterial;
    }
}