using System;
using System.Collections.Generic;
using JamTemplate.Enum;
using ShipOfTheseus2025.Models;

namespace JamTemplate.Managers;

public class StatsManager
{
  //   public float Health { get; set; }
  //   public int Exp { get; set; } = 5; 
  //   public float Will { get; set; }
  //   public float Strength { get; set; }
  //   public float WalkSpeed { get; set; } = 120.0f;
  //   public float RunSpeed { get; set; } = 300.0f;
  //   public float AirSpeed { get; set; } = 200.0f;
  //   public float InitialJumpVelocity { get; set; } = -300.0f;
  //   public float AirJumpVelocity { get; set; } = -150.0f;
  //   public uint MaxJumps { get; set; } = 2;
  // 
  public event Action<Stat, float> StatChanged;
  public Dictionary<Stat, float> Stats;
  public StatsManager()
  {
    Stats[Stat.Health] = 100f;
    Stats[Stat.Exp] = 0;
    Stats[Stat.Will] = 0;
    Stats[Stat.Strength] = 0;

    Stats[Stat.WalkSpeed] = 120f;
    Stats[Stat.RunSpeed] = 300f;
    Stats[Stat.AirSpeed] = 200f;
    Stats[Stat.InitialJumpVelocity] = -300f;
    Stats[Stat.AirJumpVelocity] = -150f;
    Stats[Stat.MaxJumps] = 2;
    Stats[Stat.FrictionCoefficient] = 0.15f;

    Stats[Stat.AttackSpeed] = 1;
    Stats[Stat.CritChance] = 0;
    Stats[Stat.WeaponThrow] = 0;
  }
  public void ChangeStat(StatChange statChange)
  {
    // TODO: maybe change this to a switch
    if (statChange.Mode == StatChangeMode.Absolute)
    {
      Stats[statChange.Stat] = statChange.Amount;
    }
    else
    {
      Stats[statChange.Stat] += statChange.Amount;
    }
    // some logic to limit the individual stats like cap water level at 100;
    StatChanged?.Invoke(statChange.Stat, Stats[statChange.Stat]);
  }

  public float GetStats(Stat stat)
  {
    return Stats[stat];
  }
}