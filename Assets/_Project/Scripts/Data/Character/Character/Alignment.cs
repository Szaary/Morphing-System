using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ALI_", menuName = "Statistics/Alignment")]
public class Alignment : ScriptableObject
{
    public int id;
    public int Id
    {
        get => id;
        private set => id = value;
    }
}