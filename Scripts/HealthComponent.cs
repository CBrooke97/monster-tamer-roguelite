using Godot;
using System;

[GlobalClass]
public partial class HealthComponent : Node
{
	[Signal]
	public delegate void OnDeathEventHandler();
	
	[Export] public float BaseHealth = 100.0f;

	private float _maxHealth;
	private float _currentHealth;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_maxHealth = BaseHealth;
		_currentHealth = BaseHealth;
		
		GD.Print(_currentHealth + " / " + _maxHealth);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void TakeDamage(float amount)
	{
		_currentHealth = Math.Clamp(_currentHealth - amount, 0.0f, _maxHealth);
		
		GD.Print(_currentHealth);

		if (_currentHealth <= 0.0f)
		{
			EmitSignal(SignalName.OnDeath);
		}
	}

	public void RestoreHealth(float amount)
	{
		_currentHealth = Math.Clamp(_currentHealth + amount, 0.0f, _maxHealth);
	}
}
