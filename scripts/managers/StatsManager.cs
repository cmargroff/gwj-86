namespace JamTemplate;

public class StatsManager
{
  public float Health { get; set; }
  public float Will { get; set; }
  public float Strength { get; set; }
  public float WalkSpeed { get; set; } = 120.0f;
  public float RunSpeed { get; set; } = 300.0f;
  public float AirSpeed { get; set; } = 200.0f;
  public float InitialJumpVelocity { get; set; } = -300.0f;
  public float AirJumpVelocity { get; set; } = -150.0f;
  public uint MaxJumps { get; set; } = 2;
}