using Godot;
using Godot.Collections;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class AttributeComponent : Node
{
	[Export] private HealthComponent? _healthComponent;
	
	public Dictionary<EAttributeType, float> BaseAttributeMap { get; private set; }

	private Dictionary<EAttributeType, float> BonusAttributeMap { get; set; } = new();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GD.Print("Hello from my mac");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void SetBaseAttributes(Dictionary<EAttributeType, float> newBaseAttributes)
	{
		BaseAttributeMap = newBaseAttributes;
	}

	public void OnMonsterDefinitionChanged(MonsterDefinitionResource newDefinition)
	{
		BaseAttributeMap = new Dictionary<EAttributeType, float>(newDefinition.AttributeMap);

		BonusAttributeMap.Clear();
		foreach(EAttributeType attribute in BaseAttributeMap.Keys)
		{
			BonusAttributeMap.Add(attribute, 0.0f);
		}

		if (_healthComponent != null)
		{
			_healthComponent.Init(BaseAttributeMap[EAttributeType.Health]);
		}
	}

	public void ApplyAttributeModifer(EAttributeType attributeType, float amount)
	{
		BonusAttributeMap[attributeType] = Mathf.Max(BonusAttributeMap[attributeType] + amount, 0.0f);
	}
}
