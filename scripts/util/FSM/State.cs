using System.Text.RegularExpressions;
using Godot;

namespace JamTemplate.Util.FSM;

public partial class State
{
  private double _progress = 0;
  private int _frame = 0;
  [GeneratedRegex("[Ss]tate$")]
  private static partial Regex StateRegex();
  [GeneratedRegex("([a-z])([A-Z])")]
  private static partial Regex SeparatorRegex();
  protected FiniteStateManager FSM;
  public State() { }
  public State(FiniteStateManager FSM)
  {
    this.FSM = FSM;
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
        name = SeparatorRegex().Replace(name, "$1_$2");
        _stateName = name.ToLower();
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
  public virtual void OnEnter() { }
  public virtual void OnExit() { }
  public void Process(double delta)
  {
    _Process(delta, _progress);
  }
  public void PhysicsProcess(double delta)
  {
    _progress += delta;
    _PhysicsProcess(delta, _progress, _frame);
  }
  public virtual void _Process(double delta, double time) { }
  public virtual void _PhysicsProcess(double delta, double time, int frame) { }
}