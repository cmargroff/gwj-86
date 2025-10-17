using Godot;
using JamTemplate;
using JamTemplate.Enum;
using JamTemplate.Managers;
using JamTemplate.Models;
using Microsoft.Extensions.DependencyInjection;

public partial class HudStat : HBoxContainer
{
  [Export]
  public StatType StatType;
  [Export]
  public Color BarColor = Colors.White;
  private RichTextLabel Label;
  private ProgressBar ProgressBar;
  public string Text { get { return Label?.Text; } set { if (Label != null) Label.Text = value; } }
  private StatsManager _statsManager;
  public override void _Ready()
  {
    Label = GetNode<RichTextLabel>("%Label");
    Label.Text = $"<icon:{Tr("Stat/" + StatType.ToString())}>";
    ProgressBar = GetNode<ProgressBar>("%ProgressBar");
    ProgressBar.AddThemeColorOverride("bg_color", BarColor);
    _statsManager = Globals.ServiceProvider.GetRequiredService<StatsManager>();
    var stat = _statsManager._stats[StatType];
    ProgressBar.MinValue = stat.MinValue;
    ProgressBar.MaxValue = stat.MaxValue;
    ProgressBar.Value = stat.Value;
    stat.OnChange += OnStatChanged;
    stat.OnConfigChange += OnStatConfigChanged;
  }

  private void OnStatChanged(float newValue)
  {
    ProgressBar.Value = newValue;
  }

  private void OnStatConfigChanged(Stat stat)
  {
    ProgressBar.MinValue = stat.MinValue;
    ProgressBar.MaxValue = stat.MaxValue;
  }
}
