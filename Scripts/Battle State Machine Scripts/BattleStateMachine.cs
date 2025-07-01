using System.Linq;
using Godot;
using Godot.Collections;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class BattleStateMachine : Node
{
    [Export] public Pathfinder Pathfinder;
    [Export] public Array<Node> CharacterStates;

    // Monster Setup
    #region MonsterSetup
    [ExportGroup("Monsters")]
    [Export]
    private PackedScene _monsterScene;
    
    [Export] 
    private PackedScene _healthBarScene;

    [Export] private Control HealthBarParentNode;
    #endregion
    
    public Array<MTRCharacter> TurnQueue;
    public int CharTurnQueueIndex = 0;
    public MTRCharacter ActiveChar => TurnQueue[CharTurnQueueIndex];

    // States
    #region States
    private BattleState? _currentState;
    
    public readonly StartBattleState StartBattleState;
    public readonly StartRoundBattleState StartRoundBattleState;
    public readonly CharacterTurnBattleState CharTurnBattleState;
    public readonly EndRoundBattleState EndRoundBattleState;
#endregion
    public BattleStateMachine()
    {
        StartBattleState = new StartBattleState(this);
        StartRoundBattleState = new StartRoundBattleState(this);
        CharTurnBattleState = new CharacterTurnBattleState(this);
        EndRoundBattleState = new EndRoundBattleState(this);
    }

    public override void _Ready()
    {
        base._Ready();

        _currentState = StartBattleState;
        _currentState.Enter();
        
        // int columns = 2; // You can change this to 3 or more for wider grids
        // int total = MonsterInstances.Length;
        // int rows = Mathf.CeilToInt((float)total / columns);
        //
        // Vector2 viewportSize = GetViewport().GetVisibleRect().Size;
        // float xSpacing = viewportSize.X / (columns + 1);
        // float ySpacing = viewportSize.Y / (rows + 1);
        //
        // for (int i = 0; i < total; i++)
        // {
        //     MonsterInstances[i] = _monsterScene.Instantiate<MTRCharacter>();
        //     AddChild(MonsterInstances[i]);
        //
        //     TextureProgressBar healthProgBar = _healthBarScene.Instantiate<TextureProgressBar>();
        //     HealthBarParentNode.AddChild(healthProgBar);
        //     
        //     HealthComponent hc = MonsterInstances[i].GetNode<HealthComponent>("HealthComponent");
        //     hc.OnMaxHealthChanged += healthProgBar.SetMax;
        //     hc.OnHealthChanged += healthProgBar.SetValue;
        //
        //     int col = i % columns;
        //     int row = i / columns;
        //
        //     MonsterInstances[i].Position = new Vector2(
        //         xSpacing * (col + 1),
        //         ySpacing * (row + 1)
        //     );
        //
        //     healthProgBar.Position = MonsterInstances[i].Position + new Vector2(-30.0f, -20.0f);
        //     GD.Print(healthProgBar.Position);
        // }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);

        BattleState? newState = _currentState?.Tick();

        if (newState != null)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }
}