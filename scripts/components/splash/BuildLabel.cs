using Godot;
using JamTemplate.Managers;
using JamTemplate.Util;

public partial class BuildLabel : Label
{
  [FromServices]
  public void Inject(ConfigManager config)
  {
    Text = $"GWJ #{config.GetValue("game", "GWJ_NUMBER")}";
  }
}
