
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[System.Serializable]
public class BluntDamageDealer
{
    public Equipment equipment;
    public Rigidbody rb;
    public BluntDamageType bluntDamageType;

    
    public List<Transform> centerOfMassLine;

    //public List<Vector3> centerOfMassLineLocalPoints;//filled based on centerOfMassLine if empty

   

    public bool overrideMass = false;
    public float overrideMassToUse = 1f;
 


}
