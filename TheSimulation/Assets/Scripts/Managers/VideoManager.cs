using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.PostProcessing;
using TMPro;

public class VideoManager : MonoBehaviour {

    public static VideoManager Instance { get; private set; }

    [Header("Profile")]
    public PostProcessProfile profile;
    [Header("Sliders")]
    public Slider brightnessSlider;
    public Slider contrastSlider;
    public Slider bloomSlider;
    public Slider motionBlurSlider;
    public Slider FOVSlider;
    [Header("DisplayValues")]
    public TextMeshProUGUI brightnessVal;
    public TextMeshProUGUI contrastVal;
    public TextMeshProUGUI bloomVal;
    public TextMeshProUGUI motionVal;
    public TextMeshProUGUI FOVVal;

    private void Awake()
    {
        Instance = this;

        profile = Camera.main.GetComponent<PostProcessVolume>().profile;
    }

    private void Start()
    {
        brightnessVal.text = brightnessSlider.value.ToString();
        contrastVal.text = contrastSlider.value.ToString();
        bloomVal.text = bloomSlider.value.ToString();
        motionVal.text = motionBlurSlider.value.ToString();
        FOVVal.text = FOVSlider.value.ToString();
    }

    public void OnBrightnessChanged()
    {
        profile.GetSetting<ColorGrading>().postExposure.value = brightnessSlider.value / 100.0f;
        brightnessVal.text = brightnessSlider.value.ToString();
    }

    public void OnContrastChanged()
    {
        profile.GetSetting<ColorGrading>().contrast.value = contrastSlider.value;
        contrastVal.text = contrastSlider.value.ToString();
    }

    public void OnBloomChanged()
    {
        profile.GetSetting<Bloom>().intensity.value = Utilities.Map(bloomSlider.value, 0.0f, 100.0f, 0.0f, 2.0f);
        bloomVal.text = bloomSlider.value.ToString();
    }

    public void OnMotionBlurChanged()
    {
        profile.GetSetting<MotionBlur>().sampleCount.value = (int)motionBlurSlider.value;
        motionVal.text = motionBlurSlider.value.ToString();
    }

    public void OnFOVChanged()
    {
        Camera.main.fieldOfView = FOVSlider.value;
        FOVVal.text = FOVSlider.value.ToString();
    }
}
