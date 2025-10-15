using System;
using Godot;

namespace JamTemplate.Components.Player;

public partial class Eyes : Sprite2D
{
  private double _blinkTimer = 0;
  private RandomNumberGenerator _rng = new RandomNumberGenerator();
  public override void _Ready()
  {
  }
  public void Blink()
  {
    var tl = CreateTween();
    tl.TweenProperty(this, "scale", new Vector2(1, 0f), 0.1f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
    tl.TweenProperty(this, "scale", new Vector2(1, 1f), 0.1f).SetTrans(Tween.TransitionType.Sine).SetEase(Tween.EaseType.InOut);
    _blinkTimer = _rng.RandfRange(0.1f, 4f);
  }

  public override void _Process(double delta)
  {
    _blinkTimer -= delta;
    if (_blinkTimer <= 0)
    {
      Blink();
    }
  }
}