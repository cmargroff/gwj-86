using Godot;
using JamTemplate.Enum;
using JamTemplate.Managers;
using JamTemplate.Util.FSM;

namespace JamTemplate.Player.States;

public class WalkState(Components.Player.Player _player, StatsManager _stats) : AnimatedState
{
  protected override void _PhysicsProcess(double delta, double time, int frame, float playSpeed)
  {

    if (Input.IsActionJustPressed("jump"))
    {
      Next("jump");
    }

    var vector = _player.GetMoveVector();
    var moveStrength = vector.Length();

    if (moveStrength < 0.2f)
    {
      Next("idle");
      return;
    }
    else if (moveStrength > 0.85f)
    {
      Next("run");
      return;
    }

    _player.Move(delta, vector, _stats.GetStat(StatType.WalkSpeed).Value);
  }
}