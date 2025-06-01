using System;
using Godot;

namespace MonsterTamerRoguelite.Scripts;

[Tool]
[GlobalClass]
public partial class Monster : Node2D
{
	[Signal] public delegate void OnMonsterDefinitionChangedEventHandler(MonsterDefinitionResource newDefinition);
	
	[Export]
	public MonsterDefinitionResource? Definition
	{
		get => _definition;
		set
		{
			_definition = value;
			EmitSignal(SignalName.OnMonsterDefinitionChanged, _definition);
			OnDefinitionChanged();
				// Your custom logic here
			}
	}
	
	private MonsterDefinitionResource? _definition;

	public StringName MonsterName => Definition?.MonsterName;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	private void OnDefinitionChanged()
	{
		GD.Print("OnDefinitionChanged fired.");

		// Prevent the crash
		if (_definition == null)
		{
			GD.PrintErr("Definition is null in OnDefinitionChanged_Editor.");
			return;
		}

		AnimatedSprite2D sprite2D = GetNodeOrNull<AnimatedSprite2D>("AnimatedSprite2D");

		if (sprite2D != null)
		{
			GD.Print("Sprite was not null");
			sprite2D.SetSpriteFrames(_definition.SpriteFrames);
		}
		else
		{
			GD.Print("Sprite was null.");
		}
	}
}
