using Godot;
using JamTemplate.Stores;

namespace JamTemplate.Managers;

public class AudioManager
{
  private SettingsStore _settings;
  public AudioManager(SettingsStore settings)
  {
    _settings = settings;
  }
  public float MainVol
  {
    get
    {
      return _settings.MainVol;
    }
    set
    {
      AudioServer.Singleton.SetBusVolumeLinear(0, _settings.MainVol);
      _settings.MainVol = value;
      _settings.Save();
    }
  }
  public float SFXVol
  {
    get
    {
      return _settings.SFXVol;
    }
    set
    {
      AudioServer.Singleton.SetBusVolumeLinear(1, _settings.SFXVol);
      _settings.SFXVol = value;
      _settings.Save();
    }
  }
  public float BGMVol
  {
    get
    {
      return _settings.BGMVol;
    }
    set
    {
      AudioServer.Singleton.SetBusVolumeLinear(1, _settings.BGMVol);
      _settings.BGMVol = value;
      _settings.Save();
    }
  }
  public float VoiceVol
  {
    get
    {
      return _settings.VoiceVol;
    }
    set
    {
      AudioServer.Singleton.SetBusVolumeLinear(1, _settings.VoiceVol);
      _settings.VoiceVol = value;
      _settings.Save();
    }
  }
}