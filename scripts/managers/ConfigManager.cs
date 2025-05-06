
using Godot;

namespace JamTemplate.Managers;

public partial class ConfigManager : ConfigFile
{
  public ConfigManager()
  {
    Load("res://config.ini");
  }
}