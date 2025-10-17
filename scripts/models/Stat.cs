
using System;

namespace JamTemplate.Models;

public class Stat
{
  private float _startingValue = 0;
  public float StartingValue { get { return _startingValue; } set { _startingValue = value; OnConfigChange?.Invoke(this); } }
  private float _minValue = 0;
  public float MinValue { get { return _minValue; } set { _minValue = value; OnConfigChange?.Invoke(this); } }
  private float _maxValue = 1;
  public float MaxValue { get { return _maxValue; } set { _maxValue = value; OnConfigChange?.Invoke(this); } }
  private float _value;
  public float Value { get { return _value; } set { _value = value; OnChange?.Invoke(_value); } }
  public event Action<float> OnChange;
  public event Action<Stat> OnConfigChange;
  public Stat(float startingValue)
  {
    _startingValue = startingValue;
    _value = startingValue;
  }

  public void Reset()
  {
    Value = _startingValue;
  }
}