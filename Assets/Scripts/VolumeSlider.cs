using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField] Slider _slider;
    [SerializeField] TextMeshProUGUI volumePercentageText;

    private void Awake()
    {
        int volumePercentage = Settings.LoadIntSetting(Settings.SettingsTypes.MasterSound, 100);
        _slider.value = volumePercentage;
        SetPercentageText(volumePercentage);
    }

    //needs to be float for the slider to see it
    public void UpdateSound(float amount)
    {
        SetPercentageText((int)amount);
        SoundManager.Instance.UpdateVolumeMasterAudioMixer((int)amount);
    }

    private void SetPercentageText(int volumePercentage)
    {
        volumePercentageText.text = volumePercentage + "%";
    }
}
