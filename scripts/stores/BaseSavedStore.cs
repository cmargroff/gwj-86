using Godot;
using JamTemplate.Managers;

namespace JamTemplate.Stores;

public partial class BaseSavedStore : Node
{
  public override void _EnterTree()
  {
    AddToGroup(SaveManager.SaveGroup);
  }
}
