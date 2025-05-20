using System;
using System.Collections.Generic;
using Godot;

namespace JamTemplate.PlayerManager;

public partial class Player
{
  public int DeviceId { get; private set; }
  public bool IsConnected { get; private set; } = false;
  /// <summary>
  /// Signal emitted when the connection state changes.
  /// </summary>
  public event Action<bool> ConnectionChangedEventHandler;
  public StringName SELECT_ACTION;
  public StringName CANCEL_ACTION;
  public StringName PAUSE_ACTION;
  public StringName JUMP_ACTION;
  public StringName MOVE_LEFT_ACTION;
  public StringName MOVE_RIGHT_ACTION;
  public StringName MOVE_UP_ACTION;
  public StringName MOVE_DOWN_ACTION;
  private string _actionPrefix;
  private List<StringName> _actions;

  /// <summary>
  /// Constructor for the Player class.
  /// Initializes the player with a device ID and a list of available game actions.
  /// </summary>
  public Player(long deviceId, List<StringName> actions)
  {
    _actions = actions;
    DeviceId = (int)deviceId;
    _actionPrefix = $"p{DeviceId}_";
    MapActions();
    CacheActions();
  }
  public void ConnectionStateChanged(bool connected)
  {
    IsConnected = connected;
    ConnectionChangedEventHandler?.Invoke(connected);
  }
  private void MapActions()
  {
    foreach (var name in _actions)
    {
      StringName mappedActionName = $"p{DeviceId}_{name}";
      var events = InputMap.ActionGetEvents(name);
      InputMap.AddAction(mappedActionName);
      foreach (var evt in events)
      {
        evt.Device = DeviceId;
        InputMap.ActionAddEvent(mappedActionName, evt);
      }
    }
  }
  public Action ResetActions => MapActions;
  private StringName Prefix(string name)
  {
    return _actionPrefix + name;
  }
  private void CacheActions()
  {
    SELECT_ACTION = Prefix("select");
    CANCEL_ACTION = Prefix("cancel");
    PAUSE_ACTION = Prefix("pause");
    JUMP_ACTION = Prefix("jump");
    MOVE_LEFT_ACTION = Prefix("move_left");
    MOVE_RIGHT_ACTION = Prefix("move_right");
    MOVE_UP_ACTION = Prefix("move_up");
    MOVE_DOWN_ACTION = Prefix("move_down");
  }
  public float PlayerJumpStrength()
  {
    return Input.GetActionStrength(JUMP_ACTION);
  }
  public float PlayerCancelStrength()
  {
    return Input.GetActionStrength(CANCEL_ACTION);
  }
  public float PlayerPauseStrength()
  {
    return Input.GetActionStrength(PAUSE_ACTION);
  }
  public float PlayerSelectStrength()
  {
    return Input.GetActionStrength(SELECT_ACTION);
  }
  public float PlayerMoveLeftStrength()
  {
    return Input.GetActionStrength(MOVE_LEFT_ACTION);
  }
  public float PlayerMoveRightStrength()
  {
    return Input.GetActionStrength(MOVE_RIGHT_ACTION);
  }
  public float PlayerMoveUpStrength()
  {
    return Input.GetActionStrength(MOVE_UP_ACTION);
  }
  public float PlayerMoveDownStrength()
  {
    return Input.GetActionStrength(MOVE_DOWN_ACTION);
  }
  public Vector2 GetMoveVector()
  {
    return Input.GetVector(MOVE_RIGHT_ACTION, MOVE_RIGHT_ACTION, MOVE_UP_ACTION, MOVE_DOWN_ACTION);
  }
  public bool AnyDirectionPressed()
  {
    return GetMoveVector().Length() > Mathf.Epsilon;
  }
  public void Rumble(float weakMagnitude, float strongMagnitude, float duration = 0.1f)
  {
    Input.StartJoyVibration(DeviceId, weakMagnitude, strongMagnitude, duration);
  }
}