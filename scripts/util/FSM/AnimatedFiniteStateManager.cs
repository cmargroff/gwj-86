using Godot;
using Microsoft.Extensions.DependencyInjection;

namespace JamTemplate.Util.FSM;

public partial class AnimatedFiniteStateManager : FiniteStateManager
{
  [Export]
  public AnimationPlayer AnimationPlayer;
  private AnimationNodeStateMachinePlayback _stateMachine;

  public bool Playing
  {
    get
    {
      return AnimationPlayer.SpeedScale != 0f;
    }
  }
  public override void _Ready()
  {
    Next(InitialStateName);
  }
  public override void _EnterTree()
  {
    base._EnterTree();
    var states = StateProvider.GetServices<AnimatedState>();
    foreach (var state in states)
    {
      var stateName = state.StateName;
      if (AnimationPlayer.HasAnimation(stateName))
      {
        var anim = AnimationPlayer.GetAnimation(stateName);
        state.AnimationLength = anim.Length;
        state.AnimationLoopMode = anim.LoopMode;
        _stateMap.Add(stateName, state);
      }
    }
    _stateMachine = (AnimationNodeStateMachinePlayback)Get("parameters/playback");
    AnimationPlayer.AnimationStarted += OnAnimationStarted;
    AnimationPlayer.AnimationFinished += OnAnimationFinished;
  }

  public override void _ExitTree()
  {
    AnimationPlayer.AnimationStarted -= OnAnimationStarted;
    AnimationPlayer.AnimationFinished -= OnAnimationFinished;
  }

  private State GetFiniteState(string stateName)
  {
    _stateMap.TryGetValue(stateName, out State state);
    return state;
  }

  private void OnAnimationStarted(StringName stateName)
  {
    if (_skipExit) _skipExit = false;
    var nextState = GetFiniteState(stateName);
    _currentState = nextState;
    if (nextState != null)
    {
      nextState.Enter();
    }
    else
    {
      throw new System.Exception($"Next State {stateName} was not found");
    }
  }
  private void OnAnimationFinished(StringName stateName)
  {
    if (!_skipExit && stateName == _currentState?.StateName)
    {
      _currentState?.Exit();
      _skipExit = false;
    }
  }
  public override void Next(string name)
  {
    AnimationPlayer.SpeedScale = 1f;
    AnimationPlayer.Play(name);
  }
  public void Pause()
  {
    AnimationPlayer.SpeedScale = 0;
  }
  public void Resume()
  {
    AnimationPlayer.SpeedScale = 1f;
  }
  private void DefaultStateProcess(double delta) { }
  private void DefaultStatePhysicsProcess(double delta) { }

  public override void Process(double delta)
  {
    _currentState?.Process(delta);
  }
  public override void PhysicsProcess(double delta)
  {
    _currentState?.PhysicsProcess(delta);
  }
}
