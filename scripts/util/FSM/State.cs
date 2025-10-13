using System;
using System.Text.RegularExpressions;
using Godot;

namespace JamTemplate.Util.FSM;

public partial class State
{
  private double _progress = 0;
  private float _frame = 0;
  [GeneratedRegex("[Ss]tate$")]
  private static partial Regex StateRegex();
  [GeneratedRegex("([a-z])([A-Z])")]
  private static partial Regex SeparatorRegex();
  public FiniteStateManager FSM;
  public State() { }
  public State(FiniteStateManager FSM)
  {
    this.FSM = FSM;
  }
  protected void Next(string name)
  {
    FSM.Next(name);
  }
  private string _stateName;
  public string StateName
  {
    get
    {
      if (_stateName == null)
      {
        var name = GetType().Name;
        name = StateRegex().Replace(name, "");
        name = Char.ToLowerInvariant(name[0]) + name.Substring(1);
        _stateName = name;
      }
      return _stateName;
    }
  }
  public void Enter()
  {
    _progress = 0f;
    _frame = 0;
    GD.Print(GetType().Name + " entered");
    OnEnter();
  }
  public void Exit()
  {
    GD.Print(GetType().Name + " exited");
    OnExit();
  }
  protected virtual void OnEnter() { }
  protected virtual void OnExit() { }
  public void Process(double delta)
  {
    _Process(delta, _progress);
  }
  public void PhysicsProcess(double delta, float playSpeed)
  {
    _progress += delta;
    _frame += playSpeed;
    _PhysicsProcess(delta, _progress, (int)_frame, playSpeed);
  }
  protected virtual void _Process(double delta, double time) { }
  protected virtual void _PhysicsProcess(double delta, double time, int frame, float playSpeed) { }
}