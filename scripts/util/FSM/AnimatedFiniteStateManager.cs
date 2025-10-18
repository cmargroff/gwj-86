using Godot;
using Microsoft.Extensions.DependencyInjection;

namespace JamTemplate.Util.FSM;

public class AnimatedFiniteStateManager : FiniteStateManager
{
  public AnimationPlayer AnimationPlayer;

  public bool Playing
  {
    get
    {
      return AnimationPlayer.SpeedScale != 0f;
    }
  }
  public void Start()
  {
    Next(InitialStateName);
  }

  public void AddStates(ServiceProvider statesProvider)
  {
    StateProvider = statesProvider;
    Build();
  }
  public void Build()
  {

    foreach (var state in StateProvider.GetServices<AnimatedState>())
    {
      var stateName = state.StateName;
      if (AnimationPlayer.HasAnimation(stateName))
      {
        var anim = AnimationPlayer.GetAnimation(stateName);
        state.AnimationLength = anim.Length;
        state.AnimationLoopMode = anim.LoopMode;
        state.FSM = this;
        state.AFSM = this;
        _stateMap.Add(stateName, state);
      }
    }
    AnimationPlayer.AnimationStarted += OnAnimationStarted;
    AnimationPlayer.AnimationFinished += OnAnimationFinished;
  }

  public void Dispose()
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
    _currentState?.PhysicsProcess(delta, AnimationPlayer.GetPlayingSpeed());
  }
  public void SetSpeedScale(float scale)
  {
    AnimationPlayer.SpeedScale = scale;
  }
}
