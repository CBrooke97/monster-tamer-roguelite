using Godot;

namespace MonsterTamerRoguelite.Scripts;

public partial class AttributeComponent : Node
{
	private Godot.Collections.Dictionary<EAttributeType, float> AttributeMap { get; }

	public AttributeComponent(Godot.Collections.Dictionary<EAttributeType, float> attributeBase)
	{
		AttributeMap = attributeBase;
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hello from my mac");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}
}
