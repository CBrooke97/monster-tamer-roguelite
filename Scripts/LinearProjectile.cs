using Godot;
using System;

[GlobalClass]
public partial class LinearProjectile : Node
{
	private Node2D _parent;

	[Export] public float ProjectileSpeed = 10.0f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		//_parent = GetParent<Node2D>();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		if (IsInstanceValid(_parent))
		{
			_parent.Position += _parent.Transform.X * ProjectileSpeed;
		}
	}
}
