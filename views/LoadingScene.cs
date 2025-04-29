using Godot;

namespace JamTemplate.Views;

public partial class LoadingScene : Control
{
  public override void _EnterTree()
  {
    (FindChild("AnimationPlayer") as AnimationPlayer).Play("spin");
  }
}
