using Godot;
using Godot.Collections;

namespace MonsterTamerRoguelite.Scripts;

[GlobalClass]
public partial class BattleStateMachine : Node
{
    // Debug
    #region Debug
    
    [ExportGroup("Debug")]
    [Export]
    private MonsterTeamComponent? _debugPlayerTeam;
    
    [ExportGroup("Debug")]
    [Export]
    private MonsterTeamComponent? _debugEnemyTeam;

    #endregion
    
    // Monster Setup
    #region MonsterSetup
    [ExportGroup("Monsters")]
    [Export]
    private PackedScene _monsterScene;
    
    [Export] 
    private PackedScene _healthBarScene;

    [Export] private Control HealthBarParentNode;
    
    public readonly Monster[] MonsterInstances = new Monster[4];
    private MonsterTeamComponent? _playerTeam;
    private MonsterTeamComponent? _enemyTeam;
    #endregion
    
    [Export]public Array<CharacterBody2D> turnQueue;
    
    // States
    #region States
    private BattleState? _currentState;
    
    public readonly StartBattleState StartBattleState;
    public readonly StartRoundBattleState StartRoundBattleState;
    public readonly CharacterTurnBattleState CharTurnBattleState;
#endregion
    public BattleStateMachine()
    {
        StartBattleState = new StartBattleState(this);
        StartRoundBattleState = new StartRoundBattleState(this);
        CharTurnBattleState = new CharacterTurnBattleState(this);
    }

    [Export] public CharacterBody2D ActiveChar { get; set; }

    public void StartBattle(MonsterTeamComponent playerTeam, MonsterTeamComponent enemyTeam)
    {
        _playerTeam = playerTeam;
        _enemyTeam = enemyTeam;

        MonsterInstances[0].Definition = playerTeam.MonsterTeam[0];
        MonsterInstances[1].Definition = playerTeam.MonsterTeam[1];

        MonsterInstances[2].Definition = enemyTeam.MonsterTeam[0];
        MonsterInstances[3].Definition = enemyTeam.MonsterTeam[1];
    }

    public override void _Ready()
    {
        base._Ready();
        
        _currentState = StartBattleState;
        _currentState.Enter();
        
        int columns = 2; // You can change this to 3 or more for wider grids
        int total = MonsterInstances.Length;
        int rows = Mathf.CeilToInt((float)total / columns);

        Vector2 viewportSize = GetViewport().GetVisibleRect().Size;
        float xSpacing = viewportSize.X / (columns + 1);
        float ySpacing = viewportSize.Y / (rows + 1);

        for (int i = 0; i < total; i++)
        {
            MonsterInstances[i] = _monsterScene.Instantiate<Monster>();
            AddChild(MonsterInstances[i]);

            TextureProgressBar healthProgBar = _healthBarScene.Instantiate<TextureProgressBar>();
            HealthBarParentNode.AddChild(healthProgBar);
            
            HealthComponent hc = MonsterInstances[i].GetNode<HealthComponent>("HealthComponent");
            hc.OnMaxHealthChanged += healthProgBar.SetMax;
            hc.OnHealthChanged += healthProgBar.SetValue;

            int col = i % columns;
            int row = i / columns;

            MonsterInstances[i].Position = new Vector2(
                xSpacing * (col + 1),
                ySpacing * (row + 1)
            );

            healthProgBar.Position = MonsterInstances[i].Position + new Vector2(-30.0f, -20.0f);
            GD.Print(healthProgBar.Position);
        }
        
        if (_debugPlayerTeam != null && _debugEnemyTeam != null)
        {
            StartBattle(_debugPlayerTeam, _debugEnemyTeam);
        }
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