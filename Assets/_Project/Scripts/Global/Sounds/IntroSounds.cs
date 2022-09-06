using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using Zenject;

public class IntroSounds : MonoBehaviour
{
    [SerializeField] private EventReference ambient;
    [SerializeField] private EventReference menuMusic;
    private SoundManager _soundManager;


    [Inject]
    public void Construct(SoundManager soundManager)
    {
        _soundManager = soundManager;
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _soundManager.PlayLevelSounds(ambient, menuMusic);
    }

}
