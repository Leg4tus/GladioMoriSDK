using MoveClasses;
using System;
using System.Buffers.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "FightOpponent", menuName = "Singleplayer/FightOpponent", order = 1)]
public class FightOpponent : ScriptableObject
{
    public string enemyName;

    public string defaultMovesetName;

    public string movesetModID; //movesetsModID

    public List<EquippedEquipment> equippedEquipment = new List<EquippedEquipment>();

    

    public string fullMovesetJson;
    


    public CharacterCustomizationSettings characterCustomizationSettings;
}
