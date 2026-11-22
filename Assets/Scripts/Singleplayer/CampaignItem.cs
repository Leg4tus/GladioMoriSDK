
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "CampaignItem", menuName = "Singleplayer/CampaignItem")]
public class CampaignItem : ScriptableObject
{
    
    
    
    public List<FightItem> fightItems = new List<FightItem>();

}
