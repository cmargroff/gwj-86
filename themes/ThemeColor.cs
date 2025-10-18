using Godot;
using System;

namespace JamTemplate.Themes;

public class ThemeColor
{
    public readonly static Color LOCKED = new Color(0.5f, 0.1f, 0, 1f);
    public readonly static Color UNLOCKED = new Color(0.5f, 0.5f, 0.5f);
    public readonly static Color ACTIVATED = new Color(0.83f, 0.91f, 0.91f);
    public readonly static Color SHOW = new Color(0.83f, 0.91f, 0.91f, 1);
    public readonly static Color HIDE = new Color(0.83f, 0.91f, 0.91f, 0);
}
