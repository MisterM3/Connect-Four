using System.Collections.Generic;
using UnityEngine;

public static class Settings
{
    //I'm using enums togheter with dictionaries instead of string so I can't mistype them
    public enum SettingsTypes
    {
        MasterSound,
    }
    private static Dictionary<SettingsTypes, string> settings = new Dictionary<SettingsTypes, string>() 
    {
        { SettingsTypes.MasterSound, "MasterSound" }
    };

    public static void ChangeIntSetting(SettingsTypes type, int amount)
    {
        PlayerPrefs.SetInt(settings[type], amount);
    }

    public static int LoadIntSetting(SettingsTypes type, int standardAmount)
    {
        return PlayerPrefs.GetInt(settings[type], standardAmount);
    }

}
