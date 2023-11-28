using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Anim : MonoBehaviour
{
    public Animator anim;
    public void OnMove(){
        anim.CrossFade("Move",0,0);
    }
    public void OnIdle(){
        anim.CrossFade("Idle",0,0);
    }
    public void OnDeath_Player(){
        anim.CrossFade("Death",0,0);
    }
}
