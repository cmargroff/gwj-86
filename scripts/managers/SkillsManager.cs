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
            if (skill.Activated && !skill.Enabled)
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
            if (skill.Activated && skill.Enabled)
            {
                skill.Enabled = false;
                _statsManager.ChangeStat(new()
                {
                    Stat = skill.StatName,
                    Amount = -skill.Value,
                    Mode = skill.Mode
                });
            }
        }
    }


}