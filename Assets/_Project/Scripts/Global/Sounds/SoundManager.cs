using System;
using System.Collections;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class SoundManager : MonoBehaviour
{
    private EventInstance _musicEvent;
    private EventInstance _ambientEvent;
    private EventInstance _voiceEvent;
    private EventInstance _interfaceEvent;

    private VCA _effectsVca;
    
    private Bus _master;
    private Bus _voice;
    private Bus _music;
    private Bus _interface;
    private Bus _effects;
    private Bus _ambient;

    [SerializeField] public Settings settings;
    
    public void Start()
    {
        _master = RuntimeManager.GetBus("bus:/Master");
        _voice = RuntimeManager.GetBus("bus:/Master/Voice");
        _music = RuntimeManager.GetBus("bus:/Master/Music");
        _interface = RuntimeManager.GetBus("bus:/Master/Interface");
        _effects = RuntimeManager.GetBus("bus:/Master/Effects");
        _ambient = RuntimeManager.GetBus("bus:/Master/Ambient");

        _effectsVca = RuntimeManager.GetVCA("vca:/Voice");
        
        if (PlayerPrefs.GetString("SoundInitialized") == "True")
        {
            ChangeMasterVolume(PlayerPrefs.GetFloat("masterVolume"));
            ChangeEffects(PlayerPrefs.GetFloat("effectsVolume"));
            ChangeMusicVolume(PlayerPrefs.GetFloat("musicVolume"));
            ChangeVoiceVolume(PlayerPrefs.GetFloat("voiceVolume"));
            ChangeAmbientVolume(PlayerPrefs.GetFloat("ambientVolume"));
            ChangeInterfaceVolume(PlayerPrefs.GetFloat("interfaceVolume"));
        }
    }
    
    public void OnApplicationPause(bool pauseStatus)
    {
        _voice.setPaused(pauseStatus);
        // _music.setPaused(pauseStatus);
    }

    public void StopMusic()
    {
        _musicEvent.stop(STOP_MODE.IMMEDIATE);
    }

    public void StopAllSounds()
    {
        _musicEvent.stop(STOP_MODE.IMMEDIATE);
        _ambientEvent.stop(STOP_MODE.IMMEDIATE);
        _voiceEvent.stop(STOP_MODE.IMMEDIATE);
        _interfaceEvent.stop(STOP_MODE.IMMEDIATE);
    }
    public void PlayLevelSounds(EventReference ambience, EventReference music)
    {
        PlayAmbient(ambience);
        PlayMusicWithFadeOut(music);
    }

    public void PlayAmbient(EventReference sound)
    {
        _ambientEvent.stop(STOP_MODE.ALLOWFADEOUT);
        if (sound.IsNull) return;
        _ambientEvent = RuntimeManager.CreateInstance(sound);
        _ambientEvent.start();
    }
    
    public void PlayMusic(EventReference sound)
    {
        if (sound.IsNull) return;
        _musicEvent = RuntimeManager.CreateInstance(sound);
        _musicEvent.start();
    }
    
    /// <summary>
    /// USE ONLY IF YOU DO NOT WANT TO STOP OTHER VOICE LINES TO PLAY AT THE SAME TIME
    /// </summary>
    /// <param name="sound"></param>
    public void PlayVoice(EventReference sound)
    {
        if (sound.IsNull) return;
        _voiceEvent = RuntimeManager.CreateInstance(sound);
        _voiceEvent.start();
        StartCoroutine(ReturnSoundVolume(0));
    }
    
    public void PlayVoiceWithFadeout(EventReference sound)
    {
        _voiceEvent.stop(STOP_MODE.ALLOWFADEOUT);
        if (sound.IsNull) return;
        _voiceEvent = RuntimeManager.CreateInstance(sound);
        _voiceEvent.start();
        StartCoroutine(ReturnSoundVolume(0));
    }

    public void StopVoice()
    {
        _voiceEvent.stop(STOP_MODE.ALLOWFADEOUT);
        StartCoroutine(ReturnSoundVolume(0));
    }
    
    public void PlayVoiceWithFadeoutThatLowersOtherSounds(EventReference sound, float lenght)
    {
        _effectsVca.setVolume(0.4f);
        _voiceEvent.stop(STOP_MODE.ALLOWFADEOUT);
        if (sound.IsNull) return;
        _voiceEvent = RuntimeManager.CreateInstance(sound);
        _voiceEvent.start();
        StartCoroutine(ReturnSoundVolume(lenght));
    }

    private IEnumerator ReturnSoundVolume(float lenght)
    {
        yield return new WaitForSeconds(lenght);
        _effectsVca.setVolume(1);
    }

    public void PlayInterface(EventReference sound)
    {
        if (sound.IsNull) return;
        _interfaceEvent = RuntimeManager.CreateInstance(sound);
        _interfaceEvent.start();
    }
    
    public void PlayMusicWithFadeOut(EventReference sound)
    {
        _musicEvent.stop(STOP_MODE.ALLOWFADEOUT);
        if (sound.IsNull) return;

        _musicEvent = RuntimeManager.CreateInstance(sound);
        _musicEvent.start();
    }

    public void ChangeMasterVolume(float value)
    {
        _master.setVolume(value);
        settings.masterVolume = value;
    }
    public void ChangeMusicVolume(float value)
    {
        _music.setVolume(value);
        settings.musicVolume = value;
    }   
    public void ChangeVoiceVolume(float value)
    {
        _voice.setVolume(value);
        settings.voiceVolume = value;
    }    
    public void ChangeAmbientVolume(float value)
    {
        _ambient.setVolume(value);
        settings.ambientVolume = value;
    }    
    public void ChangeInterfaceVolume(float value)
    {
        _interface.setVolume(value);
        settings.interfaceVolume = value;
    }
    public void ChangeEffects(float value)
    {
        _effects.setVolume(value);
        settings.effectsVolume = value;
    }


    protected void OnDestroy()
    {
        PlayerPrefs.SetString("SoundInitialized", "True");
        PlayerPrefs.SetFloat("masterVolume", settings.masterVolume);
        PlayerPrefs.SetFloat("effectsVolume", settings.effectsVolume);
        PlayerPrefs.SetFloat("musicVolume", settings.musicVolume);
        PlayerPrefs.SetFloat("voiceVolume", settings.voiceVolume);
        PlayerPrefs.SetFloat("ambientVolume", settings.ambientVolume);
        PlayerPrefs.SetFloat("interfaceVolume", settings.interfaceVolume);
    }


    [Serializable]
    public class Settings
    {
        [Range(0, 1)] public float masterVolume=1;

        [Range(0, 1)] public float effectsVolume=1;

        [Range(0, 1)] public float musicVolume=1;

        [Range(0, 1)] public float voiceVolume=1;
        
        [Range(0, 1)] public float ambientVolume=1;
        
        [Range(0, 1)] public float interfaceVolume=1;
    }
}