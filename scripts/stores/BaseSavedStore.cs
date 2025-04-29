using Godot;

public partial class BaseSavedStore : Node
{
  public override void _EnterTree()
  {
    AddToGroup(SaveManager.SaveGroup);
  }
}
