
using Godot;
using JamTemplate.Util.FSM;
using Microsoft.Extensions.DependencyInjection;

namespace JamTemplate.Components.Enemy.Hedgehog.States;

public class ChaseState([FromKeyedServices("PlayerDetection")] RayCast2D playerDetection, Hedgehog hedgehog) : AnimatedState
{
  private float _chaseRemaining = 0f;
  protected override void OnEnter()
  {
    _chaseRemaining = 1f; // chase for 2 seconds after losing sight
  }
  protected override void _PhysicsProcess(double delta, double time, int frame, float playSpeed)
  {
    if (playerDetection.IsColliding())
    {
      _chaseRemaining = 1f; // reset chase timer
    }
    else
    {
      _chaseRemaining -= (float)delta;
      if (_chaseRemaining <= 0f)
      {
        Next("idle");
        return;
      }
    }
    hedgehog.Move(delta, hedgehog.Speed);
  }
}