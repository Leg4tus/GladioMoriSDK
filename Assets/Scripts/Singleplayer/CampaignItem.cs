
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "CampaignItem", menuName = "Scriptable Objects/CampaignItem")]
public class CampaignItem : ScriptableObject
{
    
    public string campaignName;
    
    public List<FightItem> fightItems = new List<FightItem>();

}
