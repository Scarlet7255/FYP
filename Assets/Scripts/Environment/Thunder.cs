using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class Thunder : MonoBehaviour
{
    public Light lightSource;
    public AudioSource thunderAudioSource;
    public float gap;
    public float audioDelay;
    public AnimationCurve lightingCurve;

    public Color nonThunder;
    public Color onThunder;

    private float _gapTimer = 0.0f;
    public float _effectTimer = 0.0f;
    private bool thundering = false;

    public void Sample()
    {
        float p = Mathf.Pow(10, 10);
        float[] sampler = new float[64];
        thunderAudioSource.GetSpectrumData(sampler, 1, FFTWindow.BlackmanHarris);
        float timeGap = thunderAudioSource.clip.length/64;
        
        for (int i = 0; i < 64; ++i)
        {
            lightingCurve.AddKey(i*timeGap, Mathf.Min(1f,sampler[i]*p));
        }
    }

    private void Update()
    {
        if (!thundering)
        {
            _gapTimer += Time.deltaTime;
            _effectTimer = 0.0f;
            thundering = _gapTimer >= gap;
            if(thundering) thunderAudioSource.PlayDelayed(audioDelay);
        }
        

       if (thundering)
       {
           Color mid = (onThunder - nonThunder) * lightingCurve.Evaluate(_effectTimer) + nonThunder;
           lightSource.color = mid;
           _effectTimer += Time.deltaTime;
           thundering = _effectTimer < thunderAudioSource.clip.length;
           if (!thundering) _gapTimer = 0f;
       }
       
    }

    private void Lighting()
    {
        
    }
}
