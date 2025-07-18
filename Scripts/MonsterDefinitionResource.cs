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
	
	#region SpriteData
	public Texture2D Texture2D => _texture2D;
	public int HFrames => _hFrames;
	public int VFrames => _vFrames;
	
	[ExportGroup("Sprite Settings")]
	[Export] private Texture2D _texture2D;
	[Export] private int _hFrames = 1;
	[Export] private int _vFrames = 1;
	#endregion

	[Export] public Godot.Collections.Dictionary<EAttributeType, float> AttributeMap = new()
	{
		{ EAttributeType.Health, 100.0f },
		{ EAttributeType.Attack, 10.0f },
		{ EAttributeType.Defence, 10.0f },
		{ EAttributeType.MagicAttack, 10.0f },
		{ EAttributeType.MagicDefence, 10.0f },
		{ EAttributeType.Speed, 10.0f }
	};
}
