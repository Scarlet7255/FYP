using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    [RequireComponent(typeof(Outline))]
    public abstract class InteractableObject : MonoBehaviour
    {
        private string Tip;
        private MeshRenderer _renderer;
        public string iconPath ="UI/TipIcon";
        public Outline _outline;
        [SerializeField] private Color outlineColor = Color.white;
        [SerializeField] private float outlineWidth = 4.0f;
        [SerializeField] private Outline.Mode mode;
    
        public GameObject icon;
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
            if(icon) return;
            _outline.enabled = true;
                icon = ObjectPool.Instance.Take(iconPath);
                icon.SetActive(true);
                icon.transform.SetParent(UIManager.Instance.tipObjPanel.transform);
                icon.GetComponent<TipIcon>().interactObj = this;
                
        }
    
        public void LostFocus()
        {
            _outline.enabled = false;
            ObjectPool.Instance.Put(iconPath,icon);
            icon = null;
        }
    
        public abstract void Action();
    
        private void OnTriggerStay(Collider other)
        {
            if(other.tag.Equals("Player")) Select();
        }
    
        private void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                LostFocus();
            }
        }
    }


