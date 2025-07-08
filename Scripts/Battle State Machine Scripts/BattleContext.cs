using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;

namespace MonsterTamerRoguelite.Scripts;

public class BattleContext
{
    public List<MTRCharacter> CharacterInstances { get; private set; }
    public List<MTRCharacter> TurnQueue { get; private set; }
    public int CurrentTurnIndex { get; private set; } = 0;
    public int CurrentRound { get; private set; } = 1;
    
    public MTRCharacter ActiveCharacter => TurnQueue[CurrentTurnIndex];

    public BattleContext(List<MTRCharacter> characterInstances)
    {
        CharacterInstances = characterInstances;
        
        GenerateTurnQueue();
        CurrentTurnIndex = 0;
    }
    
    /// <summary>
    /// Builds the turn queue for a round. You can later override this with initiative/speed logic.
    /// </summary>
    private void GenerateTurnQueue()
    {
        TurnQueue = new List<MTRCharacter>(CharacterInstances);

        Random rng = new Random();
        int n = TurnQueue.Count;
        
        for (int i = n - 1; i > 0; i--)
        {
            int rnd = rng.Next(i + 1);  

            var value = TurnQueue[rnd];  
            TurnQueue[rnd] = TurnQueue[i];  
            TurnQueue[i] = value;
        }
    }

    /// <summary>
    /// Advance to next character in queue. Returns true if a new round is needed.
    /// </summary>
    public bool AdvanceTurn()
    {
        CurrentTurnIndex++;

        if (CurrentTurnIndex >= TurnQueue.Count)
        {
            // End of round reached
            return true;
        }

        return false;
    }

    /// <summary>
    /// Starts a new round. Resets turn index and regenerates turn order.
    /// </summary>
    public void StartNewRound()
    {
        CurrentRound++;
        CurrentTurnIndex = 0;

        GenerateTurnQueue();
    }
}