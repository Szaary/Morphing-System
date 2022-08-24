using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UISoundManager : MonoBehaviour
{
    [Inject]
    public void Construct(SoundManager soundManager, SoundManager.Settings settings)
    {
        
    }
}
