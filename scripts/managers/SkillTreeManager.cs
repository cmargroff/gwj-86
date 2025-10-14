using Godot;
using JamTemplate.Util;
using System;
namespace JamTemplate.Managers;

public partial class SkillTreeManager
{
	private StatsManager _statsManager;
	private SkillsManager _skillsManager;

	[FromServices]
	public void Inject(StatsManager statsManager, SkillsManager skillsManager)
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
