using System;
using System.Runtime.CompilerServices;

namespace JamTemplate.Managers;

public class SkillTreeManager
{
	public event Action SkillTreeUpdated;
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

		if (check)
		{
			_statsManager.Exp -= exp;
			
			//call function to apply skill 
		}
		return check;
	}
	
	public void UpdateTree()
    {
        SkillTreeUpdated?.Invoke();
    }
}
