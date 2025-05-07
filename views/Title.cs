using Godot;
using System;

public partial class Title : Control
{
  private Control _menu;
  private Control _options;
  private bool _optionsShown = false;
  public override void _EnterTree()
  {
    _menu = GetNode<Control>("%Menu");
    _options = GetNode<Control>("%Options");
  }

  public void Start() { }
  public void ToggleOptions()
  {
    _menu.Visible = _optionsShown;
    _optionsShown = !_optionsShown;
    _options.Visible = _optionsShown;
  }
  public void Credits() { }
  public void Quit() { }
}
