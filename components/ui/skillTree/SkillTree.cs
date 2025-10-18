using Godot;
using JamTemplate;
using JamTemplate.Managers;
using JamTemplate.Resources;
using Microsoft.Extensions.DependencyInjection;

public partial class SkillTree : Control
{
    private SkillTreeManager _skillTreeManager;
    private readonly static Color COLOR = new Color(214, 234, 233);

    public override void _EnterTree()
    {
        _skillTreeManager = Globals.ServiceProvider.GetRequiredService<SkillTreeManager>();
        
    }

    public override void _Process(double delta)
    {
        QueueRedraw();
    }
    public override void _Draw()
    {
        foreach (SkillNode skillNode in GetTree().GetNodesInGroup("skills"))
        {
            if (skillNode.SkillResource.UnlockSkills != null)
            {
                foreach (var resource in skillNode.SkillResource.UnlockSkills)
                {
                    var targetNode = GetSkillNode(resource);



                    if (targetNode == null) continue;

                    var sourcePosition = skillNode.GlobalPosition + skillNode.GetCenter();


                    var targetPosition = targetNode.GlobalPosition + targetNode.GetCenter();

                    DrawLine(sourcePosition, targetPosition, COLOR, 4.0f);
                }
            }


        }
    }


    private SkillNode GetSkillNode(SkillResource resource)
    {
        foreach (SkillNode skillNode in GetTree().GetNodesInGroup("skills"))
        {
        if (skillNode.SkillResource == resource)
        {
            return skillNode;
        }
        }
        return null;
    }
}
