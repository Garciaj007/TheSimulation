using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class SoundManager : MonoBehaviour {

    public static SoundManager Instance { get; private set; }

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;
    public Slider uiSFXSlider;
    public Slider ambientSlider;
    
    [Header("DisplayValues")]
    public TextMeshProUGUI masterVal;
    public TextMeshProUGUI musicVal;
    public TextMeshProUGUI sfxVal;
    public TextMeshProUGUI uiVal;
    public TextMeshProUGUI ambientVal;

    [Header("AudioMixer")]
    public AudioMixer mixer;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        mixer.SetFloat("MasterVol", Utilities.Map(masterSlider.value / 100.0f, 0.0f, 1.0f, -40.0f, 6.0f));
        mixer.SetFloat("MusicVol", Utilities.Map(musicSlider.value / 100.0f, 0.0f, 1.0f, -40.0f, 6.0f));
        mixer.SetFloat("SFXVol", Utilities.Map(sfxSlider.value / 100.0f, 0.0f, 1.0f, -40.0f, 6.0f));
        mixer.SetFloat("AmbientVol", Utilities.Map(ambientSlider.value / 100.0f, 0.0f, 1.0f, -40.0f, 6.0f));
        mixer.SetFloat("UIVol", Utilities.Map(uiSFXSlider.value / 100.0f, 0.0f, 1.0f, -40.0f, 6.0f));

        masterVal.text = masterSlider.value.ToString();
        musicVal.text = musicSlider.value.ToString();
        sfxVal.text = sfxSlider.value.ToString();
        ambientVal.text = ambientSlider.value.ToString();
        uiVal.text = uiSFXSlider.value.ToString();
    }

    public void MasterVolumeChange()
    {
        float vol = Utilities.Map(masterSlider.value / 100.0f, 0.0f, 1.0f, -40.0f, 6.0f);
        mixer.SetFloat("MasterVol", vol);
        masterVal.text = masterSlider.value.ToString();
        PlayerPrefs.SetFloat("MasterVol", vol);
    }

    public void MusicVolumeChanged()
    {
        float vol = Utilities.Map(musicSlider.value / 100.0f, 0.0f, 1.0f, -40.0f, 6.0f);
        mixer.SetFloat("MusicVol", vol);
        musicVal.text = musicSlider.value.ToString();
        PlayerPrefs.SetFloat("MusicVol", vol);
    }

    public void SFXVolumeChanged()
    {
        float vol = Utilities.Map(sfxSlider.value / 100.0f, 0.0f, 1.0f, -40.0f, 6.0f);
        mixer.SetFloat("SFXVol", vol);
        sfxVal.text = sfxSlider.value.ToString();
        PlayerPrefs.SetFloat("SFXVol", vol);
    }

    public void AmbientVolumeChanged()
    {
        float vol = Utilities.Map(ambientSlider.value / 100.0f, 0.0f, 1.0f, -40.0f, 6.0f);
        mixer.SetFloat("AmbientVol", vol);
        ambientVal.text = ambientSlider.value.ToString();
        PlayerPrefs.SetFloat("AmbientVol", vol);
    }

    public void UIVolumeChanged()
    {
        float vol = Utilities.Map(uiSFXSlider.value / 100.0f, 0.0f, 1.0f, -40.0f, 6.0f);
        mixer.SetFloat("UIVol", vol);
        uiVal.text = uiSFXSlider.value.ToString();
        PlayerPrefs.SetFloat("UIVol", vol);
    }
}
