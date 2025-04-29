using Godot;

public partial class TestSaveButton : Button
{
  private SaveManager saveManager;
  public override void _EnterTree()
  {
    saveManager = GetNode<SaveManager>("/root/SaveManager");
  }
  public override void _Pressed()
  {
    saveManager.SaveAll();
  }
}
