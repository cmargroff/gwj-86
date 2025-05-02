
namespace JamTemplate.Stores;

public partial class PlayerDataStore : BaseSavedStore
{
  public new bool Encrypted = true;
  public string PlayerName = "Player";
  public int Hp = 100;
}
