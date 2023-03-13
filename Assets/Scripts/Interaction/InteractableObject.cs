using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Outline))]
public abstract class InteractableObject : MonoBehaviour
{
    private string Tip;
    private MeshRenderer _renderer;
    public Outline _outline;
    [SerializeField] private Color outlineColor = Color.white;
    [SerializeField] private float outlineWidth = 4.0f;
    [SerializeField] private Outline.Mode mode;
    private void Awake()
    {
        _outline = gameObject.GetComponent<Outline>();
        _outline.OutlineColor = outlineColor;
        _outline.OutlineWidth = outlineWidth;
        _outline.OutlineMode = mode;
        _outline.enabled = false;
    }

    public virtual string GetTip()
    {
        return Tip;
    }

    public void Select()
    {
        _outline.enabled = true;
    }

    public void LostFocus()
    {
        _outline.enabled = false;
    }

    public abstract void Action();
}
