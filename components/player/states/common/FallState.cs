using Godot;
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
      Next("jumpAir");
      return;
    }

    _player.Move(delta, _player.GetMoveVector(), _stats.AirSpeed);
  }
}