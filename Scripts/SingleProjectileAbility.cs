using Godot;
using System;

[GlobalClass]
public partial class SingleProjectileAbility : Node, IAbility
{
	[Export] public PackedScene ProjectileScene;
	[Export] public int ProjectileObjectPoolSize = 10;

	[Export] private Node2D[] _projectileObjectPool;

	private double tempTimer = 0.0f;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_projectileObjectPool = new Node2D[ProjectileObjectPoolSize];

		for (int i = 0; i < ProjectileObjectPoolSize; i++)
		{
			_projectileObjectPool[i] = ProjectileScene.Instantiate<Node2D>();
		
			GD.Print(_projectileObjectPool[i].Name);
			GD.Print(_projectileObjectPool[i].IsVisible());
			GD.Print(_projectileObjectPool[i].Position);
			GD.Print(_projectileObjectPool[i].VisibilityLayer);
		}
		
		GD.Print(_projectileObjectPool.Length);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		tempTimer += delta;

		int index = Mathf.FloorToInt(tempTimer) % ProjectileObjectPoolSize;

		if (index < ProjectileObjectPoolSize)
		{
			if (_projectileObjectPool[index].GetParent() == null)
			{
				AddChild(_projectileObjectPool[index]);
				Random rand = new Random();
				_projectileObjectPool[index].Position = new Vector2((float)rand.NextDouble(), (float)rand.NextDouble());
				_projectileObjectPool[index].Show();
				_projectileObjectPool[index].SetProcess(true);
			}
		}
	}

	public void Trigger()
	{
		
	}
}
