using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour {

    public Animator Anim;
    
    public void Death()
    {
        if(Anim)
        {
            Anim.SetTrigger("Death");
        }
        else
        {
            Debug.LogWarning("Animator not set - Die.cs");
        }
        SendMessage("PlayerDied");
    }

    public void PlayerDied()
    {
        Debug.Log("PlayerDied");
    }
}
