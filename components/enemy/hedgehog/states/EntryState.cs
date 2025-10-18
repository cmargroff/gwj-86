
using JamTemplate.Util.FSM;

namespace JamTemplate.Components.Enemy.Hedgehog.States;

public class EntryState : AnimatedState
{
  protected override void OnEnter()
  {
    Next("idle");
  }
}