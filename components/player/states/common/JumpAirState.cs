using Godot;
using JamTemplate.Managers;
using JamTemplate.Util.FSM;

namespace JamTemplate.Player.States;

public class JumpAirState(Components.Player.Player _player, StatsManager _stats) : AnimatedState
{

  protected override void _PhysicsProcess(double delta, double time, int frame, float playSpeed)
  {
    if (frame == 1)
    {
      if (playSpeed > 0) // to determine if animation is playing forward
      {
        _player.MoveVelocity.Y = _stats.InitialJumpVelocity;
        _player.DecrementJumps();
      }
    }
    if (Input.IsActionJustPressed("jump") && _player.CanJump() && _player.IsOnWallOnly())
    {
      Next("jumpWall");
      return;
    }
    if (_player.MoveVelocity.Y > 0) // player is falling down
    {
      Next("fall");
      return;
    }
  }
}