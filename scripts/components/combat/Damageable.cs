using Godot;
using Godot.Collections;
using JamTemplate.Util;

namespace JamTemplate.Components.Combat;

[GlobalClass]
public partial class Damageable : Node
{
  [Signal]
  public delegate void HitEventHandler(Hitbox hitbox, float knockback);
  [Signal]
  public delegate void KnockedOutEventHandler();
  [Export]
  public float MaxHealth { get; set; } = 10f;
  private float _currentHealth;
  public float CurrentHealth { get; }
  [Export]
  public float Weight { get; set; } = 1f;
  public float SuperArmorAmount { get; set; } = 0f;
  private Array<Hurtbox> _hurtboxes { get; set; } = new();
  public override void _Ready()
  {
    // Search local scene for hurtboxes
    var parent = GetParent();
    if (parent is not null)
    {
      foreach (var hurtbox in parent.FindChildren<Hurtbox>(typeof(Hurtbox), true))
      {
        if (hurtbox is Hurtbox hb)
        {
          _hurtboxes.Add(hb);
          hb.Connect(Area2D.SignalName.AreaEntered, new Callable(this, nameof(Hurtbox_AreaEntered)));
        }
      }
    }
  }
  public void SetVulnerable(bool vulnerable)
  {
    foreach (var hurtbox in _hurtboxes)
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
    if (hitbox.Damage > 0)
    {
      _currentHealth -= hitbox.Damage;
      if (_currentHealth < 0)
      {
        _currentHealth = 0;
        EmitSignal(SignalName.KnockedOut);
      }
    }
    EmitSignal(SignalName.Hit, hitbox, CalculateKnockback(hitbox));
  }
  private float CalculateKnockback(Hitbox hitbox)
  {
    SuperArmorAmount -= hitbox.Knockback;
    if (SuperArmorAmount < 0)
    {
      SuperArmorAmount = 0;
    }
    else if (SuperArmorAmount > 0)
    {
      return 0f;
    }

    var percent = 1f;

    return (percent / 10f + percent * hitbox.Damage / 20f) * 200f / Weight + 100f + 1.4f + 18f + hitbox.Scaling + hitbox.Knockback;
  }
}