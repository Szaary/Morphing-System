using System.Collections;
using System.Collections.Generic;
using StarterAssets;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "MSE_", menuName = "Statistics/Movement")]
public class MovementSettings : ScriptableObject
{
    public float speed=3.5f;
    public float acceleration=8;
    public float radius=0.5f;
    public float height=2;
}
