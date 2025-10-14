namespace JamTemplate.Managers;

public class SkillTreeManager
{
	private StatsManager _statsManager;
	private SkillsManager _skillsManager;
	public SkillTreeManager(StatsManager statsManager, SkillsManager skillsManager)
	{
		_statsManager = statsManager;
		_skillsManager = skillsManager;
	}
	public bool CheckExp(int exp)
	{
		var check = (_statsManager.Exp >= exp) ? true : false;
		return check;
	}
}
