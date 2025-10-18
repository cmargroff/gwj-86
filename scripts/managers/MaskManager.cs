using System;
using System.Collections.Generic;
using Godot;
using JamTemplate.Enum;
using JamTemplate.Models;
using JamTemplate.Resources;

namespace JamTemplate.Managers;

public class MasksManager
{
    private SkillsManager _skillsManager;
    private StatsManager _statsManager;
    private SkillTreeManager _skillTreeManager;
    private Dictionary<MaskType, SkillsSet> _masks = new();
    public MasksManager(SkillsManager skillsManager, StatsManager statsManager, SkillTreeManager skillTreeManager)
    {
        _skillsManager = skillsManager;
        _statsManager = statsManager;
        _statsManager.MaskChanged += Change;
        _skillTreeManager = skillTreeManager;
        _skillTreeManager.SkillTreeUpdated += Update;

        _masks[MaskType.None] = null;
        _masks[MaskType.Rabbit] = GD.Load<SkillsSet>("res://resources/skillsSets/rabbitSkills.tres");
        _masks[MaskType.Stag] = GD.Load<SkillsSet>("res://resources/skillsSets/stagSkills.tres");
        _masks[MaskType.Owl] = null;
        _masks[MaskType.Wolf] = null;

    }

    public void Equip(MaskType mask)
    {
        if (_masks[mask] != null)
        {
            foreach (var skill in _masks[mask].Skills)
            {
                 _skillsManager.ApplySkill(skill);
            }
        }
        
    }

    public void Unequip(MaskType mask)
    {
        if (_masks[mask] != null)
        {
            _skillsManager.RemoveSkills(_masks[mask]);
        }

    }

    public void Update()
    {
        var currentMask = _statsManager.GetMask();
        Equip(currentMask);
    }

    public void Change(MaskType oldMask, MaskType newMask)
    {
        Unequip(oldMask);
        Equip(newMask);
    }
    
    public void Unlock(MaskType mask)
    {
        if (_masks[mask] != null)
        {
            foreach (var skill in _masks[mask].Skills)
            {
                GD.Print(skill.SkillName);
                if (skill.SkillName == "Mask")
                {
                    skill.State = SkillState.Activated;
                    if (skill.UnlockSkills != null)
                    {
                        foreach (var skillNode in skill.UnlockSkills)
                        {
                            _skillsManager.UnlockSkill(skillNode);
                        }
                    }
                    break;
                }
            }
        }
    }
}