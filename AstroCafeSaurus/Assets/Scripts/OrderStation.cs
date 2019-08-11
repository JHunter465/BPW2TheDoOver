using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderStation : MonoBehaviour
{

    private bool DinoCollided = false;
    private bool PlayerCollided = false;
    private GameObject theDino = null;

    //Checks if both the player and dino are inside the collision circle and assigns theDino to the dino colliding
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("COLLISION!" + other.gameObject.name);
        if (other.gameObject.layer == 13)
        {
            //Debug.Log("TheDinoEntered");
            DinoCollided = true;
            theDino = other.gameObject;
        }
        if(other.gameObject.layer == 8)
        {
            PlayerCollided = true;
        }
    }

    //Checks if either the player or the dino have left the collision circle and nulls theDino
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 13)
        {
            //Debug.Log("TheDinoHasLeft");
            DinoCollided = false;
            theDino = null;
        }
        if(other.gameObject.layer == 8)
        {
            PlayerCollided = false;
        }
    }

    //when the player interacts with this object while nearby, with dino nearby aswell, the dino can create an order.
    public void PlayerInteraction()
    {
        if(DinoCollided && PlayerCollided)
        {
            Debug.Log("1");
            theDino.GetComponent<DinosaurScript>().Ordering();
        }
    }
}
