using Godot;

namespace JamTemplate.Resources;

[GlobalClass]
public partial class SplashWildcard : Resource
{
  [Export]
  public bool Enabled = false;
  [Export]
  public string Title = "";
  [Export]
  public string Description = "";
  [Export]
  public Texture2D Icon;
}
