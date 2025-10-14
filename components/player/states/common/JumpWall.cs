using JamTemplate.Managers;
using JamTemplate.Util.FSM;

namespace JamTemplate.Player.States;

public class JumpWallState(Components.Player.Player _player, StatsManager _stats) : AnimatedState
{
  protected override void _PhysicsProcess(double delta, double time, int frame, float playSpeed)
  {
    if (frame == 1)
    {
      _player.MoveVelocity.Y = _stats.AirJumpVelocity;
      var normal = _player.GetWallNormal();
      _player.MoveVelocity.X = normal.X * _stats.AirSpeed;
    }
    if (_player.MoveVelocity.Y > 0) // player is falling down
    {
      Next("fall");
      return;
    }
  }
}