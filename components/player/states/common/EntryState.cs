using JamTemplate.Util.FSM;

namespace JamTemplate.Player.States;

public class EntryState() : AnimatedState
{
  protected override void OnEnter()
  {
    Next("idle");
  }
}