

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class Blade : MonoBehaviour {
    public ConfigurableJoint joint;
    public ConfigurableJoint boneJoint;
    public Rigidbody weaponRigidbody;
    public GameObject dragPoint;
    public Collider bladeTriggerCollider;
    public Weapon weapon;
    
    
    


    public float stabMultiplier = 2;
    public float slashMultiplier = 1;

    public float stabBoneMultiplier = 2;
    public float slashBoneMultiplier = 2;
    public Transform bladeTip;

    
    
    public BladePainter[] bladePainters;
    //public BladePainter[] allBladePainters;
   


}
