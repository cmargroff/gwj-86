using Godot;
using JamTemplate;
using JamTemplate.Enum;
using JamTemplate.Managers;
using JamTemplate.Resources;
using Microsoft.Extensions.DependencyInjection;
using System;

public partial class SkillNode : PanelContainer
{
    private Button _button;
    private TextureRect _border;
    private TextureRect _icon;
    private Label _text;
    private Label _cost;
    private readonly static Color LOCKED = new Color(0.5f, 0.1f, 0, 1f);
    private readonly static Color UNLOCKED = new Color(0.5f, 0.5f, 0.5f);
    private readonly static Color ACTIVATED = new Color(1, 1, 1);
    private readonly static Color SHOW = new Color(1, 1, 1, 1);
    private readonly static Color HIDE = new Color(1, 1, 1, 0);
    [Export]
    public SkillResource SkillResource;

    private SkillTreeManager _skillTreeManager;
    private StatsManager _statsManager;

    public override void _EnterTree()
    {
        _skillTreeManager = Globals.ServiceProvider.GetRequiredService<SkillTreeManager>();
        _statsManager = Globals.ServiceProvider.GetRequiredService<StatsManager>();

        _button = GetNode<Button>("%Button");
        _border = GetNode<TextureRect>("%Border");
        _icon = GetNode<TextureRect>("%Icon");
        _text = GetNode<Label>("%Text");
        _text.SelfModulate = new Color(1, 1, 1, 0);
        _cost = GetNode<Label>("%Cost");
        _cost.SelfModulate = new Color(1, 1, 1, 0);

        _skillTreeManager.SkillTreeUpdated += SetStyle;
    }

    public override void _Ready()
    {
        if (SkillResource == null) return;

        AddToGroup("skills");
        _border.Texture = SkillResource.BorderTexture;
        _icon.Texture = SkillResource.IconTexture;
        _text.Text = SkillResource.Description;

        var exp = _statsManager.GetStat(StatType.Exp);
        exp.OnChange += Exp_OnChange;
        _cost.Text = $"Exp: {exp.Value}/{SkillResource.ExpCost}";

        SetStyle();
    }


    public void SetStyle()
    {
        if (SkillResource.Unlocked && !SkillResource.Activated)
        {
            Modulate = UNLOCKED;
        }
        else if (SkillResource.Unlocked && SkillResource.Activated)
        {
            Modulate = ACTIVATED;
        }
        else
        {
            Modulate = LOCKED;
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
            _cost.SelfModulate = HIDE;

            if (SkillResource.UnlockSkills != null)
            {
                foreach (var skill in SkillResource.UnlockSkills)
                {
                    UnlockSkill(skill);
                }
            }
        }
        SetStyle();
    }

    public void ShowText()
    {
        _text.SelfModulate = SHOW;
        if (!SkillResource.Activated)
            _cost.SelfModulate = SHOW;
    }

    public void HideText()
    {
        _text.SelfModulate = HIDE;
        _cost.SelfModulate = HIDE;
    }

    public Vector2 GetCenter()
    {
        return Size / 2;
    }

    public void Exp_OnChange(float exp) {
        _cost.Text = $"{exp}/{SkillResource.ExpCost}";
    }
}
