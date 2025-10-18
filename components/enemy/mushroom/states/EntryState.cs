
using JamTemplate.Util.FSM;

namespace JamTemplate.Components.Enemy.Mushroom.States;

public class EntryState : AnimatedState
{
  protected override void OnEnter()
  {
    Next("idle");
  }
}