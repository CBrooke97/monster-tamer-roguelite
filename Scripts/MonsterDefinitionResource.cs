using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Godot;

namespace MonsterTamerRoguelite.Scripts;

[Tool]
[GlobalClass]
public partial class MonsterDefinitionResource : Resource
{
	public StringName MonsterName => _monsterName;
	public SpriteFrames SpriteFrames => _monsterSpriteFrames;

	[Export] private StringName _monsterName = "Placeholder";
	[Export] private SpriteFrames _monsterSpriteFrames;
	
	[Export] public Godot.Collections.Dictionary<StringName, float> AttributeMap = new Godot.Collections.Dictionary<StringName, float>
	{
		{"Health", 100.0f },
		{"Attack", 10.0f },
		{"Defence", 10.0f },
		{"MagicAttack", 10.0f },
		{"MagicDefence", 10.0f },
		{"Speed", 10.0f }
	};
}
