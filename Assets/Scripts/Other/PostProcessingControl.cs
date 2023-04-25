using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;


public class PostProcessingControl : MonoBehaviour
{
    public PostProcessVolume volume;

    [Range(0,1)]
    public float vigIntensity;
    public Color vigColor;
    public bool enableEffect = false;
    
    private Vignette target;
    
    private void Start()
    {
         target = volume.profile.GetSetting<Vignette>();
    }

    private void Update()
    {
        target.active = enableEffect;
        target.intensity.value = vigIntensity;
        target.color.value = vigColor;
    }
}
