using System.Collections.Generic;
using Godot;
using Microsoft.Extensions.DependencyInjection;

namespace JamTemplate.Util.FSM;

public partial class FiniteStateManager : Node
{
  public ServiceProvider StateProvider;
  public string InitialStateName = "entry";
  protected Dictionary<string, State> _stateMap;

  protected State _currentState;
  protected bool _skipExit = false;
  private bool _paused = false;
  private string _queuedState;

  public override void _Ready()
  {
    Next(InitialStateName);
  }
  public override void _EnterTree()
  {
    _stateMap = new();
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
  public void Interrupt(string name)
  {
    _skipExit = true;
    Next(name);
  }
  public virtual void Next(string name)
  {
    if (_currentState != null)
    {
      _currentState.Exit();
    }
    var nextState = GetFiniteState(name);
    if (nextState != null)
    {
      _queuedState = name;
    }
    else
    {
      throw new System.Exception($"Next State {name} was not found");
    }
  }
  private void DefaultStateProcess(double delta) { }
  private void DefaultStatePhysicsProcess(double delta) { }

  public virtual void Process(double delta)
  {
    _currentState?.Process(delta);
  }
  public virtual void PhysicsProcess(double delta)
  {
    if (_queuedState != null)
    {
      _currentState.Enter();
      _queuedState = null;
    }
    _currentState?.PhysicsProcess(delta);
  }
}
