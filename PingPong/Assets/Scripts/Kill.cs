using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour {

    public Animator Anim;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Die>().Death();
            if (Anim)
            {
                Anim.SetTrigger("Death");
            }
            else
            {
                Debug.LogWarning("Animator not set - Die.cs");
            }
        }
    }
}
