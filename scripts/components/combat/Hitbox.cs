using Godot;

namespace JamTemplate.Components.Combat;

public partial class Hitbox : Area2D
{
  /// <summary>
  /// How much damage the hitbox does
  /// </summary>
  public float Damage { get; set; } = 1f;
  /// <summary>
  /// How hard the BKB is
  /// </summary>
  public float Knockback { get; set; } = 1f;
  /// <summary>
  /// A global angle relative to right facing
  /// </summary>
  public float Angle { get; set; } = 0f;
  /// <summary>
  /// Is this a player hitbox for setting collision layers
  /// </summary>
  public bool IsPlayer { get; set; } = true;
  /// <summary>
  /// The radius of the hitbox
  /// </summary>
  public float Radius { get; set; } = 1f;
  /// <summary>
  /// The length of the hitbox
  /// 0 will be a circle
  /// </summary>
  public float Height { get; set; } = 0f;
  /// <summary>
  /// The bone id this hitbox is attached to
  /// </summary>
  public int BoneId { get; set; } = 0;
  /// <summary>
  /// Relative position offset from the parent bone
  /// </summary>
  public Vector2 Offset { get; set; } = Vector2.Zero;
  /// <summary>
  /// The lifetime of the hitbox in seconds
  /// </summary>
  public float Lifetime { get; set; } = 1f / 60f;
  public override void _EnterTree()
  {
    SetCollisionLayerValue(IsPlayer ? 5 : 7, true);
    var shape = CreateShape(this);
    if (shape != null)
    {
      AddChild(shape);
    }
  }
  public static CollisionShape2D CreateShape(Hitbox hitbox)
  {
    if (hitbox.Height > 0)
    {
      // Create a capsule shape
      var capsule = new CapsuleShape2D
      {
        Radius = hitbox.Radius,
        Height = hitbox.Height
      };
      return new CollisionShape2D { Shape = capsule };
    }
    else
    {
      // Create a circle shape
      var circle = new CircleShape2D
      {
        Radius = hitbox.Radius
      };
      return new CollisionShape2D { Shape = circle };
    }
  }
}