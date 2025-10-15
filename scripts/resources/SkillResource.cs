using Godot;
using Godot.Collections;
using JamTemplate.Enum;
using System;
using System.ComponentModel;

namespace JamTemplate.Resources;

[GlobalClass]
public partial class SkillResource : Resource
{
    [Export]
    public string SkillName { get; set; }
    [Export]
    public string Description { get; set; }
    [Export]
    public Texture2D BorderTexture { get; set; }
    [Export]
    public Texture2D IconTexture { get; set; }
    [Export]
    public int ExpCost { get; set; }
    [Export]
    public bool Unlocked { get; set; }
    [Export]
    public bool Activated { get; set; }
    [Export]
    public bool Enabled { get; set; }
    [Export]
    public Stat StatName;
    [Export]
    public float Value;
    [Export]
    public StatChangeMode Mode;
    [Export]
    public Array<SkillResource> UnlockSkills { get; set; }
}
