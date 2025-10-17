using System;
using System.Collections.Generic;
using JamTemplate.Enum;
using JamTemplate.Models;

namespace JamTemplate.Managers;

public class StatsManager
{
  public event Action<StatType, float> StatChanged;
  private Dictionary<StatType, Stat> _stats = new();
  public StatsManager()
  {
    _stats[StatType.Health] = new Stat(100f);
    _stats[StatType.Exp] = new Stat(0);
    _stats[StatType.Will] = new Stat(0);
    _stats[StatType.Strength] = new Stat(0);

    _stats[StatType.WalkSpeed] = new Stat(120f);
    _stats[StatType.RunSpeed] = new Stat(300f);
    _stats[StatType.AirSpeed] = new Stat(200f);
    _stats[StatType.SpeedMuliplier] = new Stat(1f);
    _stats[StatType.InitialJumpVelocity] = new Stat(-300f);
    _stats[StatType.AirJumpVelocity] = new Stat(-150f);
    _stats[StatType.MaxJumps] = new Stat(2);
    _stats[StatType.FrictionCoefficient] = new Stat(0.15f);

    _stats[StatType.DashDistance] = new Stat(50f);
    _stats[StatType.InvulnWindow] = new Stat(0.5f);

    _stats[StatType.AttackSpeed] = new Stat(1f);
    _stats[StatType.CritChance] = new Stat(0f);
    _stats[StatType.WeaponThrow] = new Stat(0);

    _stats[StatType.HealthRegen] = new Stat(0f);
    _stats[StatType.DashDamage] = new Stat(0f);
  }

  public void Reset()
  {
    foreach (var stat in _stats.Values)
    {
      stat.Reset();
    }
  }

  
  
  public void ChangeStat(StatChange statChange)
  {
    // TODO: maybe change this to a switch
    if (statChange.Mode == StatChangeMode.Absolute)
    {
      _stats[statChange.Stat].Value = statChange.Amount;
    }
    else
    {
      _stats[statChange.Stat].Value += statChange.Amount;
    }
    
    StatChanged?.Invoke(statChange.Stat, _stats[statChange.Stat].Value);
  }

  public Stat GetStat(StatType stat)
  {
    return _stats[stat];
  }
  public void BindToStatConfigChange(Action<Stat> action)
  {
    foreach (var stat in _stats.Values)
    {
      stat.OnConfigChange += action;
    }
  }
  public void UnbindFromStatConfigChange(Action<Stat> action)
  {
    foreach (var stat in _stats.Values)
    {
      stat.OnConfigChange -= action;
    }
  }
  public void BindToStatChange(StatType stat, Action<float> action)
  {
    _stats[stat].OnChange += action;
  }
  public void UnbindFromStatChange(StatType stat, Action<float> action)
  {
    _stats[stat].OnChange -= action;
  }
}