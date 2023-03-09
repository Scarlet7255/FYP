using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    private string Tip;
    private MeshRenderer _renderer;
    private Material[] _materials;
    private Material[] _pMaterials;
     
    private void Awake()
    {
        _renderer = gameObject.GetComponent<MeshRenderer>();
         _pMaterials = _renderer.materials;
        _materials = new Material[_pMaterials.Length + 1];
        
        for (int i = 0; i < _pMaterials.Length; ++i)
        {
            _materials[i] = _pMaterials[i];
        }

        _renderer.materials = _materials;
    }

    public virtual string GetTip()
    {
        return Tip;
    }

    public void Select(Material selMaterial)
    {
        _materials[_materials.Length - 1] = selMaterial;
        _renderer.materials = _materials;
    }

    public void LostFocus()
    {
        _materials[_materials.Length - 1] = null;
        _renderer.materials = _pMaterials;
    }

    public abstract void Action();
}
