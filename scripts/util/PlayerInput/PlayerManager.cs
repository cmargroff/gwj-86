using System;
using System.Collections.Generic;
using System.Linq;
using Godot;

namespace JamTemplate.Util.PlayerInput;

public partial class PlayerManager
{
  private List<StringName> _gameActions;
  public List<Player> Players;
  public event Action<int> PlayerAdded;
  public event Action<int> PlayerRemoved;
  public PlayerManager()
  {
    Players = new();
    _gameActions = new();
    MakeActionList();
    Input.Singleton.JoyConnectionChanged += Input_JoyConnectionChanged;
  }
  private void MakeActionList()
  {
    _gameActions = InputMap.GetActions()
    .Where(name => !name.ToString().StartsWith("ui_"))
    .ToList();
  }
  private void Input_JoyConnectionChanged(long deviceId, bool connected)
  {
    var player = GetPlayerByDeviceId(deviceId);
    if (player != null)
    {
      player.ConnectionStateChanged(connected);
    }
    else
    {
      var newPlayer = new Player(deviceId, _gameActions);
      Players.Add(newPlayer);
      PlayerAdded?.Invoke((int)deviceId);
      newPlayer.ConnectionStateChanged(connected);
    }
  }
  private Player GetPlayerByDeviceId(long deviceId)
  {
    return Players.Where((player) => player.DeviceId == (int)deviceId).FirstOrDefault();
  }
  public void RemovePlayer(int index)
  {
    Players.RemoveAt(index);
    PlayerRemoved?.Invoke(index);
  }
}
