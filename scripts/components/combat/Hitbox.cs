using Godot;

namespace JamTemplate.Components.Combat;

[GlobalClass]
public partial class Hitbox : Area2D
{
  [Export]
  public bool FromScene { get; set; } = false;
  /// <summary>
  /// How much damage the hitbox does
  /// </summary>
  [Export]
  public float Damage { get; set; } = 1f;
  /// <summary>
  /// How hard the BKB is
  /// </summary>
  [Export]
  public float Knockback { get; set; } = 1f;
  [Export]
  public float Scaling { get; set; } = 100f;
  [Export]
  public bool FixedKnockback { get; set; } = false;
  /// <summary>
  /// A global angle relative to right facing
  /// </summary>
  [Export]
  public float Angle { get; set; } = 0f;
  /// <summary>
  /// Is this a player hitbox for setting collision layers
  /// </summary>
  [Export]
  public bool IsPlayer { get; set; } = true;
  /// <summary>
  /// The radius of the hitbox
  /// </summary>
  [Export]
  public float Radius { get; set; } = 1f;
  /// <summary>
  /// The length of the hitbox
  /// 0 will be a circle
  /// </summary>
  [Export]
  public float Height { get; set; } = 0f;
  /// <summary>
  /// The bone id this hitbox is attached to
  /// </summary>
  [Export]
  public int BoneId { get; set; } = 0;
  /// <summary>
  /// Relative position offset from the parent bone
  /// </summary>
  [Export]
  public Vector2 Offset { get; set; } = Vector2.Zero;
  /// <summary>
  /// The lifetime of the hitbox in seconds
  /// </summary>
  [Export]
  public float Lifetime { get; set; } = 0f;
  public override void _EnterTree()
  {
    if (FromScene) return;
    SetCollisionLayerValue(IsPlayer ? 5 : 7, true);
    var shape = CreateShape(this);
    if (shape != null)
    {
      AddChild(shape);
    }

    if (Lifetime <= 0f)
    {
      // infinite lifetime
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