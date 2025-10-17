using System;
using System.Collections.Generic;
using JamTemplate.Enum;
using JamTemplate.Models;

namespace JamTemplate.Managers;

public class StatsManager
{
  public event Action<StatType, float> StatChanged;
  public Dictionary<StatType, Stat> Stats = new();
  public StatsManager()
  {
    Stats[StatType.Health] = new Stat(100f);
    Stats[StatType.Exp] = new Stat(0);
    Stats[StatType.Will] = new Stat(0);
    Stats[StatType.Strength] = new Stat(0);

    Stats[StatType.WalkSpeed] = new Stat(120f);
    Stats[StatType.RunSpeed] = new Stat(300f);
    Stats[StatType.AirSpeed] = new Stat(200f);
    Stats[StatType.SpeedMuliplier] = new Stat(1f);
    Stats[StatType.InitialJumpVelocity] = new Stat(-300f);
    Stats[StatType.AirJumpVelocity] = new Stat(-150f);
    Stats[StatType.MaxJumps] = new Stat(2);
    Stats[StatType.FrictionCoefficient] = new Stat(0.15f);

    Stats[StatType.DashDistance] = new Stat(50f);
    Stats[StatType.InvulnWindow] = new Stat(0.5f);

    Stats[StatType.AttackSpeed] = new Stat(1f);
    Stats[StatType.CritChance] = new Stat(0f);
    Stats[StatType.WeaponThrow] = new Stat(0);

    Stats[StatType.HealthRegen] = new Stat(0f);
    Stats[StatType.DashDamage] = new Stat(0f);
  }
  public void ChangeStat(StatChange statChange)
  {
    // TODO: maybe change this to a switch
    if (statChange.Mode == StatChangeMode.Absolute)
    {
      Stats[statChange.Stat].Value = statChange.Amount;
    }
    else
    {
      Stats[statChange.Stat].Value += statChange.Amount;
    }
    // some logic to limit the individual stats like cap water level at 100;
    StatChanged?.Invoke(statChange.Stat, Stats[statChange.Stat].Value);
  }

  public float GetStats(StatType stat)
  {
    return Stats[stat].Value;
  }
  public void BindToStatConfigChange(Action<Stat> action)
  {
    foreach (var stat in Stats.Values)
    {
      stat.OnConfigChange += action;
    }
  }
  public void UnbindFromStatConfigChange(Action<Stat> action)
  {
    foreach (var stat in Stats.Values)
    {
      stat.OnConfigChange -= action;
    }
  }
  public void BindToStatChange(StatType stat, Action<float> action)
  {
    Stats[stat].OnChange += action;
  }
  public void UnbindFromStatChange(StatType stat, Action<float> action)
  {
    Stats[stat].OnChange -= action;
  }
}