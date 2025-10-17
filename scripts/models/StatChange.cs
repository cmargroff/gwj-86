using JamTemplate.Enum;

namespace JamTemplate.Models;

public struct StatChange
{
  public StatChangeMode Mode;
  public StatType Stat;
  public float Amount;
}