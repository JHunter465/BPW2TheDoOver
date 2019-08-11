using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeatScript : MonoBehaviour
{
    [SerializeField] public bool taken = false;

    private bool dinoPresent = false;
    public bool playerPresent = false;

    private DinosaurScript dino;

    //When both the player and dino are nearby and the player presses the interact button. It checks if the order corresponds with the meal the player has and finishes it or destroys the restaurant
    public void playerInteraction(List<int> playerOrder)
    {
        if(dinoPresent && playerPresent)
        {
            if (dino.DoListsMatch(playerOrder))
            {
                dino.OrderFinished();
            }
            else
            {
                dino.LaserEyes();
            }
        }
    }

    //Checks if either the player or the dino have entered the collision circle and assigns dino to the colliding dino
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 13)
        {
            dino = other.gameObject.GetComponent<DinosaurScript>();
            dinoPresent = true;
        }
        if(other.gameObject.layer == 8)
        {
            playerPresent = true;
        }
    }

    //Checks if either the player or the dino have left the collision circle and nulls dino
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.layer == 13)
        {
            dino = null;
            dinoPresent = false;
        }
        if(other.gameObject.layer == 8)
        {
            playerPresent = false;
        }
    }
}
