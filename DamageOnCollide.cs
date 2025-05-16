using Godot;
using System;

[GlobalClass]
public partial class DamageOnCollide : Node
{
	private Area2D _parent;
	[Export] public float Damage = 10.0f;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_parent = GetParent<Area2D>();

		_parent.BodyEntered += OnCollide;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void OnCollide(Node2D body)
	{
		if (!IsInstanceValid(body)) return;
		
		HealthComponent healthComponent = body.GetNodeOrNull<HealthComponent>("HealthComponent");

		if (IsInstanceValid(healthComponent))
		{
			healthComponent.TakeDamage(Damage);
		}
		
		
		_parent.SetProcess(false);
		_parent.Hide();
	}
}
