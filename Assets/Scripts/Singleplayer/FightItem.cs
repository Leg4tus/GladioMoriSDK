using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "FightItem", menuName = "Singleplayer/FightItem", order = 1)]
public class FightItem : ScriptableObject
{
    public string fightTitle;
    public string scene;
    public List<FightOpponent> fightOpponents;
    public FightItemTier FightItemTier = FightItemTier.Tier1; // this determines the background for the fight preview image
   
}
