using Godot;
using JamTemplate;
using JamTemplate.Enum;
using JamTemplate.Managers;
using JamTemplate.Resources;
using JamTemplate.Themes;


using Microsoft.Extensions.DependencyInjection;
using System;

public partial class SkillNode : PanelContainer
{
    private Button _button;
    private TextureRect _border;
    private TextureRect _icon;
    private Label _text;
    private Label _cost;

    [Export]
    public SkillResource SkillResource;

    private SkillTreeManager _skillTreeManager;
    private StatsManager _statsManager;
    private SkillsManager _skillsManager;

    public override void _EnterTree()
    {
        _skillTreeManager = Globals.ServiceProvider.GetRequiredService<SkillTreeManager>();
        _statsManager = Globals.ServiceProvider.GetRequiredService<StatsManager>();
        _skillsManager = Globals.ServiceProvider.GetRequiredService<SkillsManager>();

        _button = GetNode<Button>("%Button");
        _border = GetNode<TextureRect>("%Border");
        _icon = GetNode<TextureRect>("%Icon");
        _text = GetNode<Label>("%Text");
        _text.SelfModulate = ThemeColor.HIDE;
        _cost = GetNode<Label>("%Cost");
        _cost.SelfModulate = ThemeColor.HIDE;

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
        if (SkillResource.ExpCost > 0)
            _cost.Text = $"Exp: {exp.Value}/{SkillResource.ExpCost}";

        SetStyle();
    }


    public void SetStyle()
    {
        switch(SkillResource.State) 
        {
            case SkillState.Locked:
                Modulate = ThemeColor.LOCKED;
                break;
            case SkillState.Unlocked:
                Modulate = ThemeColor.UNLOCKED;
                break;
            case SkillState.Activated:
                Modulate = ThemeColor.ACTIVATED;
                _cost.SelfModulate = ThemeColor.HIDE;
                break;
        }

    }


    public void ActivateSkill()
    {
        _skillsManager.ActivateSkill(SkillResource);

    }

    public void ShowText()
    {
        _text.SelfModulate = ThemeColor.SHOW;
        if (SkillResource.State != SkillState.Activated )
            _cost.SelfModulate = ThemeColor.SHOW;
    }

    public void HideText()
    {
        _text.SelfModulate = ThemeColor.HIDE;
        _cost.SelfModulate = ThemeColor.HIDE;
    }

    public Vector2 GetCenter()
    {
        return Size / 2;
    }

    public void Exp_OnChange(float exp) {
        if (SkillResource.ExpCost > 0)
            _cost.Text = $"{exp}/{SkillResource.ExpCost}";
    }
}
