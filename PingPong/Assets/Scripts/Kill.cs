using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour {

    private Movement move;

    private void Start()
    {
        move = GetComponent<Movement>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hero"))
        {
            collision.gameObject.GetComponent<Die>().Death();
            bool player1 = (collision.gameObject.name == "Hero") ? true:false ;
            if (move.Anim1)
            {
                move.VisualActive(move.Frente, true);
                move.VisualActive(move.Tras, false);
                if (player1)
                {
                    move.Anim1.SetTrigger("death1");
                }
                else
                {
                    move.Anim1.SetTrigger("death2");
                }
            }
            else
            {
                Debug.LogWarning("Animator not set - Kill.cs");
            }
        }
    }
}
