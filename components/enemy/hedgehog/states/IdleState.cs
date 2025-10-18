using JamTemplate.Util.FSM;

namespace JamTemplate.Components.Enemy.Hedgehog.States;

public class IdleState(Hedgehog hedgehog) : AnimatedState
{
  private float _patrolTimer = 0f;
  protected override void OnEnter()
  {
    _patrolTimer = hedgehog.PatrolDelay;
  }
  protected override void _PhysicsProcess(double delta, double time, int frame, float playSpeed)
  {
    if (hedgehog.CheckForPlayer()) return;

    _patrolTimer -= (float)delta;
    if (_patrolTimer <= 0f)
    {
      Next("walk");
    }
  }
}