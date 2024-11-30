using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class for all the settings that are saved over playsessions, made to be easilly expandable
/// </summary>
public static class Settings
{
    //I'm using enums together with dictionaries instead of string so I can't mistype them
    public enum SettingsTypes
    {
        MasterSound,
    }
    private static Dictionary<SettingsTypes, string> settings = new Dictionary<SettingsTypes, string>() 
    {
        { SettingsTypes.MasterSound, "MasterSound" }
    };

    //Generic functions to make it easy to expand with more settings
    public static void ChangeIntSetting(SettingsTypes type, int amount)
    {
        PlayerPrefs.SetInt(settings[type], amount);
    }
    public static int LoadIntSetting(SettingsTypes type, int standardAmount)
    {
        return PlayerPrefs.GetInt(settings[type], standardAmount);
    }

}
