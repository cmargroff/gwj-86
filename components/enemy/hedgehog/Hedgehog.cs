
using Godot;
using JamTemplate.Components.Enemy.Hedgehog.States;
using JamTemplate.Util.FSM;
using Microsoft.Extensions.DependencyInjection;

namespace JamTemplate.Components.Enemy.Hedgehog;

public partial class Hedgehog : Node2D
{
  [Export]
  public float Speed = 35f;
  [Export]
  public float PatrolDelay = 2f;
  [Export]
  public float PatrolDistance = 200f;
  [Export]
  public float ChaseSpeed = 90f;
  public Vector2 StartPosition;
  private AnimatedFiniteStateManager _fsm;
  private RayCast2D _playerDetection;
  public override void _EnterTree()
  {
    StartPosition = Position;
    _playerDetection = GetNode<RayCast2D>("%PlayerDetection");
    var services = new ServiceCollection();
    services.AddSingleton(this)
    .AddKeyedSingleton("PlayerDetection", _playerDetection)
    .AddSingleton<AnimatedState, EntryState>()
    .AddSingleton<AnimatedState, IdleState>()
    .AddSingleton<AnimatedState, WalkState>()
    .AddSingleton<AnimatedState, ChaseState>()
    ;
    _fsm = new();
    _fsm.AnimationPlayer = GetNode<AnimationPlayer>("%AnimationPlayer");
    _fsm.AddStates(services.BuildServiceProvider());
  }
  public override void _Ready()
  {
    _fsm.Start();
  }
  public void Move(double delta, float speed)
  {
    Position += Vector2.Left * speed * (float)delta;
  }
  public void FlipH()
  {
    Scale = new Vector2(-Scale.X, Scale.Y);
    Speed = -Speed;
  }
  public bool CheckForPlayer()
  {
    if (_playerDetection.IsColliding())
    {
      _fsm.Interrupt("chase");
      return true;
    }
    return false;
  }
  public override void _Process(double delta)
  {
    _fsm.Process(delta);
  }
  public override void _PhysicsProcess(double delta)
  {
    _fsm.PhysicsProcess(delta);
  }
}
