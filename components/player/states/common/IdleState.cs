using Godot;
using JamTemplate.Util.FSM;

namespace JamTemplate.Player.States;

public class IdleState(Components.Player.Player _player) : AnimatedState
{
  protected override void _PhysicsProcess(double delta, double time, int frame, float playSpeed)
  {

    // TODO: combat states

    if (Input.IsActionJustPressed("jump"))
    {
      Next("jump");
      return;
    }

    var vector = _player.GetMoveVector();
    var moveStrength = vector.Length();
    if (moveStrength > 0.2f)
    {
      if (moveStrength > 0.85f)
      {
        Next("run");
      }
      else
      {
        Next("walk");
      }
      return;
    }
  }
}