using Godot;
using Godot.Collections;
using JamTemplate.Resources;
using System;

[GlobalClass]
public partial class SkillsSet : Resource
{
    [Export]
    public Array<SkillResource> Skills { get; set; }
}
