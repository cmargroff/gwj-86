using Godot;
using JamTemplate;
using JamTemplate.Enum;
using JamTemplate.Managers;
using JamTemplate.Resources;
using JamTemplate.Themes;
using Microsoft.Extensions.DependencyInjection;

public partial class SkillTree : Control
{
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
                    switch(resource.State) 
                    {
                        case SkillState.Locked:
                            DrawLine(sourcePosition, targetPosition, ThemeColor.LOCKED, 3.0f);
                            break;
                        case SkillState.Unlocked:
                            DrawLine(sourcePosition, targetPosition, ThemeColor.UNLOCKED, 3.0f);
                            break;
                        case SkillState.Activated:
                            DrawLine(sourcePosition, targetPosition, ThemeColor.ACTIVATED, 3.0f);
                            break;
                    }
                    
                    
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
