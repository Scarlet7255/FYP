
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OcclusionObject : MonoBehaviour
{
   private MeshRenderer _renderer;
   private Material[] _sharedMaterials;
   public float alpha;

   private int originLayer;
   public int occlusionLayer;
   
   private void Awake()
   {
      _renderer = gameObject.GetComponent<MeshRenderer>();
      _sharedMaterials = new Material[_renderer.sharedMaterials.Length];
      var oriMaterials = _renderer.sharedMaterials;
      
      for (int i = 0; i < _sharedMaterials.Length; ++i)
      {
         _sharedMaterials[i] = Instantiate(oriMaterials[i]);
      }

      _renderer.sharedMaterials = _sharedMaterials;

      originLayer = gameObject.layer;
   }

   public void HandleOcculsion()
   {
      foreach (var m in _sharedMaterials)
      {
         Color c = m.color;
         c.a = alpha;
         m.color = c;
      }
      _renderer.sharedMaterials = _sharedMaterials;
      gameObject.layer = occlusionLayer;
   }

   public void SetBack()
   {
      foreach (var m in _sharedMaterials)
      {
         Color c = m.color;
         c.a = 1f;
         m.color = c;
      }
      _renderer.sharedMaterials = _sharedMaterials;
      gameObject.layer = originLayer;
   }

}
