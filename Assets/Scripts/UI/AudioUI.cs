
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioUI : MonoBehaviour
{
    public AudioMixer volumeController;
    public Slider mainSlider;
    public Slider effectSlider;
    public Slider natureSlider;

    private void Start()
    {
        mainSlider.value = PlayerPrefs.GetFloat("MainVolume", 0f);
        effectSlider.value = PlayerPrefs.GetFloat("EffectVolume", 0f);
        natureSlider.value = PlayerPrefs.GetFloat("NatureVolume", 0f);
        volumeController.SetFloat("MainVolume", PlayerPrefs.GetFloat("MainVolume", 0f));
        volumeController.SetFloat("EffectVolume", PlayerPrefs.GetFloat("EffectVolume", 0f));
        volumeController.SetFloat("NatureVolume", PlayerPrefs.GetFloat("NatureVolume", 0f));
        gameObject.SetActive(false);
    }

    public void SetMainVolume()
    {
        float value = mainSlider.value;
        PlayerPrefs.SetFloat("MainVolume",value);
        PlayerPrefs.Save();
        volumeController.SetFloat("MainVolume", value);
    }

    public void SetEffectVolume()
    {
        float value = effectSlider.value;
        PlayerPrefs.SetFloat("EffectVolume",value);
        PlayerPrefs.Save();
        volumeController.SetFloat("EffectVolume", value);
    }

    public void SetNatureVolume()
    {
        float value = natureSlider.value;
        PlayerPrefs.SetFloat("NatureVolume",value);
        PlayerPrefs.Save();
        volumeController.SetFloat("NatureVolume", value);
    }

    public void Switch()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }
}
