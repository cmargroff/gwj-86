using Godot;
using Game;

public partial class GameManager : Node
{
  [Export]
  public string InitialNodePath;
  public override void _Ready()
  {
    GD.Print(GetType().Name, " Started");
    if (InitialNodePath != null)
    {
      SceneManager.Instance.ChangeScene(InitialNodePath);
    }
  }
}
