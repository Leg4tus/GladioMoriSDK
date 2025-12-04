
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Weapon : Equipment
{
    
    [Header("Weapon")]
    public Blade bladeTrigger;
    public List<Collider> bladeColliders;
    
    public bool disableLocalLogic = false;

    

    public float weaponMaxDistance = 1.8f;
    public float weaponMinDistance = 1.4f;

    

    public List<WeaponEdgeSection> weaponEdgeSections = null;


   
}
