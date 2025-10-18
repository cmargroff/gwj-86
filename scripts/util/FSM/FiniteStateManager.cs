using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace JamTemplate.Util.FSM;

public class FiniteStateManager
{
  public ServiceProvider StateProvider;
  public string InitialStateName = "entry";
  protected Dictionary<string, State> _stateMap = new();
  protected State _currentState;
  protected bool _skipExit = false;
  private string _queuedState;

  private State GetFiniteState(string stateName)
  {
    _stateMap.TryGetValue(stateName, out State state);
    return state;
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
    _currentState?.PhysicsProcess(delta, 1f);
  }
}
