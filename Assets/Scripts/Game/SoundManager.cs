using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    [Header("Audio Source Component")]
    public AudioSource source;
    [Header("Player")]
    public AudioClip PLAYER_SHOOT_SOUND;
    public AudioClip PLAYER_DEATH_SOUND;
    [Header("Enemy-Skull Sounds")]
    public AudioClip ENEMY_DEATH_SOUND;
    void Start(){
        if(instance == null){
            instance = this;
        }else{Destroy(gameObject);}
    }
    public void PLAY_PLAYER_SHOOT(){
        source.PlayOneShot(PLAYER_SHOOT_SOUND);
    }
    public void PLAY_PLAYER_DEATH(){
        source.PlayOneShot(PLAYER_DEATH_SOUND);
    }
    public void PLAY_ENEMY_DEATH(){
        source.PlayOneShot(ENEMY_DEATH_SOUND);
    }
}
