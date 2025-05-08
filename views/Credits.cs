using Godot;

public partial class Credits : Control
{
  [Export]
  public string CopyResourcePath;
  public override void _EnterTree()
  {
    if (CopyResourcePath != null)
    {
      var handle = FileAccess.Open(CopyResourcePath, FileAccess.ModeFlags.Read);
      var copy = handle.GetAsText();
      GetNode<RichTextLabel>("%BodyCopy").Text = copy;
    }
  }
}
