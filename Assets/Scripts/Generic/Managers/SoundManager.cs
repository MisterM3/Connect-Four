using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    //Using audiomixers so I can group various audio sources together (although I'm only using 1 background sound for this game)
    [SerializeField] private AudioMixer _masterAudioMixer;

    private void Awake()
    {
        InitializeSingleton(this);
    }

    //Only runs when singleton is only one in scene
    protected override void OnSingletonSucceedInitialize()
    {
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
        float volume = MathFunctions.ConvertRange(percentage, 0, 100, -80, 0);
      //-80f is 0% volume 0f is 100% volume
        _masterAudioMixer.SetFloat("_volume", volume);
    }





}
