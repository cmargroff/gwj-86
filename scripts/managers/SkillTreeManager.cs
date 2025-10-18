using System;
using System.Runtime.CompilerServices;
using JamTemplate.Enum;

namespace JamTemplate.Managers;

public class SkillTreeManager
{
	public event Action SkillTreeUpdated;
	private StatsManager _statsManager;

	public SkillTreeManager(StatsManager statsManager)
	{
		_statsManager = statsManager;
		
	}
	public bool CheckExp(float exp)
	{
		var check = (_statsManager.GetStat(StatType.Exp).Value >= exp) ? true : false;

		if (check)
		{
			_statsManager.GetStat(StatType.Exp).Value -= exp;

		}
		return check;
	}

	public void UpdateTree()
	{
		SkillTreeUpdated?.Invoke();
	}
}
