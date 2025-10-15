using Godot;
using JamTemplate;
using JamTemplate.Managers;
using JamTemplate.Resources;
using Microsoft.Extensions.DependencyInjection;
using System;

public partial class SkillIcon : PanelContainer
{
    private Button _button;
    private TextureRect _border;
    private TextureRect _icon;
    private Color _lockedColor = new Color(0.5f, 0.1f, 0, 1f);
    private Color _unlockedColor = new Color(0.5f, 0.5f, 0.5f);
    private Color _activatedColor = new Color(1, 1, 1);
    [Export]
    public SkillResource SkillResource;

    private SkillTreeManager _skillTreeManager;

    public override void _EnterTree()
    {
        _skillTreeManager = Globals.ServiceProvider.GetRequiredService<SkillTreeManager>();

        _button = GetNode<Button>("%Button");
        _border = GetNode<TextureRect>("%Border");
        _icon = GetNode<TextureRect>("%Icon");

        _skillTreeManager.SkillTreeUpdated += SetStyle;
    }

    public override void _Ready()
    {
        if (SkillResource == null) return;

        AddToGroup("skills");
        _border.Texture = SkillResource.BorderTexture;
        _icon.Texture = SkillResource.IconTexture;

        SetStyle();
    }

    public void SetStyle()
    {
        if (SkillResource.Unlocked && !SkillResource.Activated)
        {
            Modulate = _unlockedColor;
        }
        else if (SkillResource.Unlocked && SkillResource.Activated)
        {
            Modulate = _activatedColor;
        }
        else
        {
            Modulate = _lockedColor;
        }

    }

    public void UnlockSkill(SkillResource skillResource)
    {
        skillResource.Unlocked = true;
        _skillTreeManager.UpdateTree();
    }

    public void ActivateSkill()
    {
        if (SkillResource.Unlocked && !SkillResource.Activated && _skillTreeManager.CheckExp(SkillResource.ExpCost))
        {
            SkillResource.Activated = true;
        }
        if (SkillResource.UnlockSkills != null)
        {
            foreach (var skill in SkillResource.UnlockSkills)
            {
                UnlockSkill(skill);
            }
        }
        
        SetStyle();
    }
    
    public Vector2 GetCenter()
    {
        return CustomMinimumSize / 2;
    }
}
