using Godot;

namespace JamTemplate.Util.FSM;

public partial class AnimatedState : State
{
  public AnimatedFiniteStateManager AFSM;
  public float AnimationLength;
  public Animation.LoopModeEnum AnimationLoopMode;
}

public class AnimatedState<T> : AnimatedState { }