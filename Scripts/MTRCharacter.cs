using System;
using Godot;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class MTRCharacter : CharacterBody2D
{
	private MonsterDefinitionResource? _definition;
	public TurnController? TurnController { get; private set; }

	public void Init(MonsterDefinitionResource definition, TurnController turnController)
	{
		_definition = definition;
		TurnController = turnController;
	}
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
