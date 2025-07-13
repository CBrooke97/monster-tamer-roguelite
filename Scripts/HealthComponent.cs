using System;
using Godot;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class HealthComponent : Node
{
	[Signal] public delegate void OnDeathEventHandler();

	[Signal]
	public delegate void OnHealthChangedEventHandler(double newValue);
	
	[Signal]
	public delegate void OnMaxHealthChangedEventHandler(double newValue);
	
	[Export] public double BaseHealth = 100.0f;

	public double MaxHealth => _maxHealth;
	public double CurrentHealth => _currentHealth;
	
	private double _maxHealth;
	private double _currentHealth;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Init(float baseHealth)
	{
		_maxHealth = baseHealth;
		_currentHealth = _maxHealth;

		EmitSignal(SignalName.OnHealthChanged, _currentHealth);
		EmitSignal(SignalName.OnMaxHealthChanged, _maxHealth);
	}

	public void TakeDamage(double amount)
	{
		_currentHealth = Math.Clamp(_currentHealth - amount, 0.0f, _maxHealth);

		EmitSignal(SignalName.OnHealthChanged, _currentHealth);
		
		if (_currentHealth <= 0.0f)
		{
			EmitSignal(SignalName.OnDeath);
		}
	}

	public void RestoreHealth(float amount)
	{
		_currentHealth = Math.Clamp(_currentHealth + amount, 0.0f, _maxHealth);
		
		EmitSignal(SignalName.OnHealthChanged, _currentHealth);
		
		if (_currentHealth <= 0.0f)
		{
			EmitSignal(SignalName.OnDeath);
		}
	}
}