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
        public Sprite iconSprite;
        
        [HideInInspector]public GameObject icon;
        [HideInInspector]public CharacterAgent source;
        public bool selectable = true;
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
            if(icon || !selectable) return;
            _outline.enabled = true;
                icon = ObjectPool.Instance.Take(iconPath);
                icon.transform.SetParent(UIManager.Instance.tipObjPanel.transform);
                icon.GetComponent<TipIcon>().SetIconInfo(this,iconSprite);
                icon.SetActive(true);
        }
    
        public void LostFocus()
        {
            _outline.enabled = false;
            ObjectPool.Instance.Put(iconPath,icon);
            icon = null;
        }
    
        public abstract void Action(CharacterAgent source);
    
        protected void OnTriggerStay(Collider other)
        {
            if(other.tag.Equals("Player") || other.tag.Equals("Detector")) Select();
        }
    
        protected virtual void OnTriggerExit(Collider other)
        {
            if (other.tag.Equals("Player")||other.tag.Equals("Detector"))
            {
                LostFocus();
            }
        }

        protected void OnDisable()
        {
            LostFocus();
        }
    }


