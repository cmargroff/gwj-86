using Godot;

namespace JamTemplate.Stores;

public partial class SettingsStore : BaseSavedStore
{

  // refactor this code to be automatic
  public float MainVol
  {
    get
    {
      return Mathf.Clamp((float)_configFile.GetValue("volume", "MainVol", 1f), 0f, 1f);
    }
    set
    {
      _configFile.SetValue("volume", "MainVol", value);
    }
  }
  public float SFXVol
  {
    get
    {
      return Mathf.Clamp((float)_configFile.GetValue("volume", "SFXVol", 1f), 0f, 1f);
    }
    set
    {
      _configFile.SetValue("volume", "SFXVol", value);
    }
  }
  public float BGMVol
  {
    get
    {
      return Mathf.Clamp((float)_configFile.GetValue("volume", "BGMVol", 1f), 0f, 1f);
    }
    set
    {
      _configFile.SetValue("volume", "BGMVol", value);
    }
  }
  public float VoiceVol
  {
    get
    {
      return Mathf.Clamp((float)_configFile.GetValue("volume", "VoiceVol", 1f), 0f, 1f);
    }
    set
    {
      _configFile.SetValue("volume", "VoiceVol", value);
    }
  }
}
