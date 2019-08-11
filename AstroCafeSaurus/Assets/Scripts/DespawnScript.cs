using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnScript : MonoBehaviour
{
    //When Dino collides here it sends the command to DinoManager to Destroy this Dinosaur
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 13)
        {
            DinoManager.Instance.DestroyDino(other.gameObject.GetComponent<DinosaurScript>());
        }
    }
}
