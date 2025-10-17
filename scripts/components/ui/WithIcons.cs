using System.Text.RegularExpressions;
using Godot;

namespace JamTemplate.UI;

[GlobalClass]
public partial class WithIcons : Node
{
  // TODO: cache loaded icons
  public override void _EnterTree()
  {
    var parent = GetParent();
    if (parent is RichTextLabel rtl)
    {
      ReplaceIcons(rtl);
      rtl.Connect(RichTextLabel.SignalName.Finished, Callable.From(() => ReplaceIcons(rtl)));
    }
  }
  public void ReplaceIcons(RichTextLabel rtl)
  {
    var text = rtl.Text;
    var matches = IconRegex().Matches(text);
    foreach (Match match in matches)
    {
      var path = "res://assets/sprites/ui/icons/atlas.sprites/" + match.Groups[1].Value + ".tres";
      if (!ResourceLoader.Exists(path)) continue; // dont rewrite tag if icon doesnt exist
      var size = rtl.GetThemeFontSize("theme_override_font_sizes/normal_font_size");
      var iconTag = $"[img=c,c,height={size}]{path}[/img]";
      text = text.Replace(match.Value, iconTag);
    }
    rtl.Text = text;
  }
  [GeneratedRegex("<icon:([^>]+)>")]
  private static partial Regex IconRegex();
}
