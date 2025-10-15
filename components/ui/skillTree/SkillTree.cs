using Godot;
using JamTemplate;
using JamTemplate.Managers;
using JamTemplate.Resources;
using Microsoft.Extensions.DependencyInjection;

public partial class SkillTree : Control
{
    private SkillTreeManager _skillTreeManager;

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
        foreach (SkillIcon skillNode in GetTree().GetNodesInGroup("skills"))
        {
            if (skillNode.SkillResource.UnlockSkills != null)
            {
                foreach (var resource in skillNode.SkillResource.UnlockSkills)
                {
                    var targetNode = GetSkillNode(resource);



                    if (targetNode == null) continue;

                    var sourcePosition = skillNode.GlobalPosition + skillNode.GetCenter();


                    var targetPosition = targetNode.GlobalPosition + targetNode.GetCenter();
                    var color = new Color(255, 255, 255);

                    DrawLine(sourcePosition, targetPosition, color, -7.0f);
                }
            }


        }
    }


    private SkillIcon GetSkillNode(SkillResource resource)
    {
        foreach (SkillIcon skillNode in GetTree().GetNodesInGroup("skills"))
        {
        if (skillNode.SkillResource == resource)
        {
            return skillNode;
        }
        }
        return null;
    }
}
