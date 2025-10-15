using Godot;
using Godot.Collections;

namespace JamTemplate.Components.Combat;

[GlobalClass]
public partial class Damageable : Node
{
  [Signal]
  public delegate void HitEventHandler(Hitbox hitbox);
  public Array<Hurtbox> Hurtboxes { get; set; } = new();
  public override void _Ready()
  {
    // Search local scene for hurtboxes
    var parent = GetParent();
    if (parent is not null)
    {
      foreach (var hurtbox in parent.FindChildren("*", nameof(Hurtbox), true))
      {
        if (hurtbox is Hurtbox hb)
        {
          Hurtboxes.Add(hb);
          hb.Connect(Area2D.SignalName.AreaEntered, new Callable(this, nameof(Hurtbox_AreaEntered)));
        }
      }
    }
  }
  public void SetVulnerable(bool vulnerable)
  {
    foreach (var hurtbox in Hurtboxes)
    {
      hurtbox.Monitoring = vulnerable;
    }
  }
  private void Hurtbox_AreaEntered(Area2D area)
  {
    if (area is Hitbox hitbox)
    {
      HandleHit(hitbox);
    }
  }
  private void HandleHit(Hitbox hitbox)
  {
    EmitSignal(SignalName.Hit, hitbox);
  }
}