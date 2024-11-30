using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Visual Slider to change sound with in options
/// </summary>
public class VolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TextMeshProUGUI _volumePercentageText;

    private void Awake()
    {
        //The sounds are saved between 0 - 100 so start with sounds fully active
        int volumePercentage = Settings.LoadIntSetting(Settings.SettingsTypes.MasterSound, 100);
        _slider.value = volumePercentage;
        SetPercentageText(volumePercentage);
    }

    //needs to be float for the slider to see it for a UnityEvent
    public void UpdateSound(float amount)
    {
        SetPercentageText((int)amount);
        SoundManager.Instance.UpdateVolumeMasterAudioMixer((int)amount);
    }

    private void SetPercentageText(int volumePercentage)
    {
        _volumePercentageText.text = volumePercentage + "%";
    }
}
