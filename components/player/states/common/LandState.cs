using JamTemplate.Util.FSM;

namespace JamTemplate.Player.States;

public class LandState() : AnimatedState
{
  protected override void OnExit()
  {
    Next("idle");

  }

}