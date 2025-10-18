using Godot;
using JamTemplate.Util.FSM;

namespace JamTemplate.Components.Enemy.Hedgehog.States;

public class WalkState(Hedgehog hedgehog) : AnimatedState
{
  protected override void _PhysicsProcess(double delta, double time, int frame, float playSpeed)
  {
    // handle interrupting the animation if the player is near the enemy
    if (hedgehog.CheckForPlayer()) return;

    hedgehog.Move(delta, hedgehog.Speed);
    if (Mathf.Abs(hedgehog.Position.X - hedgehog.StartPosition.X) > hedgehog.PatrolDistance)
    {
      hedgehog.FlipH();
      Next("idle");
    }
  }
}