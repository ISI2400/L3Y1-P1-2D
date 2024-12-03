using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playeranimator : MonoBehaviour
{

    public Animation anim;


    void Update(){

        if (anim.isPlaying){
            return;
        }

        if (Input.GetKeyDown(KeyCode.D)){
            Debug.Log("moving right");
        }

        if (Input.GetKeyDown(KeyCode.A)){
            Debug.Log("moving left");
        }


    }




}
