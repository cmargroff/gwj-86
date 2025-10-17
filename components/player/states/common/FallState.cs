using Godot;
using JamTemplate.Enum;
using JamTemplate.Managers;
using JamTemplate.Util.FSM;

namespace JamTemplate.Player.States;

public class FallState(Components.Player.Player _player, StatsManager _stats) : AnimatedState
{
  protected override void _PhysicsProcess(double delta, double time, int frame, float playSpeed)
  {
    if (_player.IsOnFloor())
    {
      Next("idle");
      _player.ResetJumps();
      return;
    }
    if (Input.IsActionJustPressed("jump") && _player.CanJump())
    {
      if (_player.IsOnWallOnly())
      {
        Next("jumpWall");
        return;
      }
      else
      {
        Next("jumpAir");
        return;
      }
    }
    _player.Move(delta, _player.GetMoveVector(), _stats._stats[StatType.AirSpeed].Value);
  }
}