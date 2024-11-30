using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{

    [SerializeField] private AudioMixer _masterAudioMixer;

    private void Awake()
    {
        InitializeSingleton(this);
        int volumePercentage = Settings.LoadIntSetting(Settings.SettingsTypes.MasterSound, 100);
        SetVolumeMasterAudioMixer(volumePercentage);
    }

    public void UpdateVolumeMasterAudioMixer(int percentage)
    {
        SetVolumeMasterAudioMixer(percentage);
        Settings.ChangeIntSetting(Settings.SettingsTypes.MasterSound, percentage);
    }

    private void SetVolumeMasterAudioMixer(int percentage)
    {
        float volume = ConvertRange(percentage, 0, 100, -80, 0);
        //-80f is 0% volume 0f is 100% volume
        _masterAudioMixer.SetFloat("_volume", volume);
    }

    /// <summary>
    /// Changes a range of values, into a different range. For Example (0 - 100) to (-1 - 1)
    /// </summary>
    /// <returns>Updated Value</returns>
    private float ConvertRange(float value, float oldMin, float oldMax, float newMin = 0, float newMax = 1)
    {
        float oldRange = (oldMax - oldMin);
        float newRange = (newMax - newMin);
        return (((value - oldMin) * newRange) / oldRange) + newMin;
    }



}
