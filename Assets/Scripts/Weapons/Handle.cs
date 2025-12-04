using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handle : MonoBehaviour
{

    public Weapon weapon;
    public Equipment equipment;
    public Rigidbody weaponRigidbody;
    public Transform holdPosition;

    
    public Quaternion? startRotation;
    public Quaternion? startRotationGlobal;
    public Vector3 physicalHoldRotation;
    public List<Collider> handleColliders = new List<Collider>();

    public List<Transform> nonGrabbableTransforms = new List<Transform>();
    public List<float> nonGrabbableHandlePositions = new List<float>();

    public bool _isTwoHanded = false;
    public bool IsTwoHanded { get { return _isTwoHanded; } }


   
}
