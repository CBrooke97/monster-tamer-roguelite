using System.Collections.Generic;
using Godot;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class AttributeComponent : Node
{
	[Export] private HealthComponent? _healthComponent;
	
	private Godot.Collections.Dictionary<EAttributeType, float> BaseAttributeMap { get; set; }

	private System.Collections.Generic.Dictionary<EAttributeType, List<AttributeModifier>> AttributeModifiers = new();

	public float GetModifiedAttribute(EAttributeType attributeType)
	{
		if (BaseAttributeMap.ContainsKey(attributeType))
		{
			return 0.0f;
		}

		float modifiedValue = BaseAttributeMap[attributeType];

		var modifiers = AttributeModifiers[attributeType];
		
		foreach (var modifier in modifiers)
		{
			modifiedValue = modifier.Handle(modifiedValue);
		}

		return modifiedValue;
	}

	public bool AddModifier(EAttributeType attributeType, AttributeModifier modifier)
	{
		bool hasAttribute = BaseAttributeMap.ContainsKey(attributeType);

		if (hasAttribute)
		{
			if (AttributeModifiers.TryGetValue(attributeType, out var modifiers))
			{
				modifiers.Add(modifier);
			}
			else
			{
				AttributeModifiers.Add(attributeType, new List<AttributeModifier>{ modifier });
			}
		}

		return hasAttribute;
	}

	public void TickModifiers()
	{
		foreach (EAttributeType key in AttributeModifiers.Keys)
		{
			List<AttributeModifier> modifiers = AttributeModifiers[key];

			for (int i = modifiers.Count - 1; i >= 0; i--)
			{
				modifiers[i].Tick();

				if (modifiers[i].MarkForRemoval)
				{
					modifiers.RemoveAt(i);
				}
			}
		}
	}

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		foreach (EAttributeType attributeType in BaseAttributeMap.Keys)
		{
			AttributeModifiers.Add(attributeType, new());
		}
	}

	public void OnMonsterDefinitionChanged(MonsterDefinitionResource newDefinition)
	{
		BaseAttributeMap = new Godot.Collections.Dictionary<EAttributeType, float>(newDefinition.AttributeMap);

		if (_healthComponent != null)
		{
			_healthComponent.Init(BaseAttributeMap[EAttributeType.Health]);
		}
	}
}
