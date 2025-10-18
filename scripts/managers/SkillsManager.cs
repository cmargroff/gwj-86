using JamTemplate.Enum;

namespace JamTemplate.Managers;

public class SkillsManager
{
    private StatsManager _statsManager;
    public SkillsManager(StatsManager statsManager)
    {
        _statsManager = statsManager;

    }

    public void ApplySkills(SkillsSet skillsSet)
    {
        foreach (var skill in skillsSet.Skills)
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


}