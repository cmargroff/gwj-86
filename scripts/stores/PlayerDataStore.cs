using Godot;

namespace JamTemplate.Stores;

public partial class PlayerDataStore : BaseSavedStore
{
  [Export]
  public int Hp = 100;
}
