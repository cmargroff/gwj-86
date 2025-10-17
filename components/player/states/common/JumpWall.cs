using JamTemplate.Enum;
using JamTemplate.Managers;
using JamTemplate.Util.FSM;

namespace JamTemplate.Player.States;

public class JumpWallState(Components.Player.Player _player, StatsManager _stats) : AnimatedState
{
  protected override void _PhysicsProcess(double delta, double time, int frame, float playSpeed)
  {
    if (frame == 1)
    {
      _player.MoveVelocity.Y = _stats.Stats[StatType.AirJumpVelocity].Value;
      var normal = _player.GetWallNormal();
      _player.MoveVelocity.X = normal.X * _stats.Stats[StatType.AirSpeed].Value;
    }
    if (_player.MoveVelocity.Y > 0) // player is falling down
    {
      Next("fall");
      return;
    }
  }
}