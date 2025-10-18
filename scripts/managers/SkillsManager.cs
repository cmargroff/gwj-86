using Godot;
using JamTemplate.Enum;
using JamTemplate.Resources;

namespace JamTemplate.Managers;

public class SkillsManager
{
    private StatsManager _statsManager;
    private SkillTreeManager _skillTreeManager;
    public SkillsManager(StatsManager statsManager, SkillTreeManager skillTreeManager)
    {
        _statsManager = statsManager;
        _skillTreeManager = skillTreeManager;

    }

    public void ApplySkill(SkillResource skill)
    {
        if (skill.State == SkillState.Activated && !skill.Enabled)
        {
            skill.Enabled = true;
            _statsManager.ChangeStat(new()
            {
                Stat = skill.StatName,
                Amount = skill.Value,
                Mode = skill.Mode
            });
        }
        
    }

    public void RemoveSkills(SkillsSet skillsSet)
    {
        foreach (var skill in skillsSet.Skills)
        {
            skill.Enabled = false;
            var stat = _statsManager.GetStat(skill.StatName);
            stat.Reset();
        }

    }

    public void UnlockSkill(SkillResource skillResource)
    {
        skillResource.State = SkillState.Unlocked;
        _skillTreeManager.UpdateTree();
    }

    public void ActivateSkill(SkillResource skillResource)
    {
        if (skillResource.State == SkillState.Unlocked && _skillTreeManager.CheckExp(skillResource.ExpCost))
        {
            skillResource.State = SkillState.Activated;
            _skillTreeManager.UpdateTree();

            if (skillResource.UnlockSkills != null)
            {
                foreach (var skill in skillResource.UnlockSkills)
                {
                    UnlockSkill(skill);
                }
            }
        }
        
    }
}