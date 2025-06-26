using Godot;
using Godot.Collections;

namespace MonsterTamerRoguelite.Scripts;

public struct TurnContext
{
    public CharacterBody2D activeChar;
    public Array<CharacterBody2D> friendlyChars;
    public Array<CharacterBody2D> enemyChars;
}