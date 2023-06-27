using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            PlayerLogic playerLogic = other.GetComponent<PlayerLogic>();
            if(playerLogic)
            {
                playerLogic.Die();
            }
        }
    }
}
