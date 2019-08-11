using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTop : MonoBehaviour
{
    [SerializeField] public Transform itemPosition;

    public GameObject storedObject;

    public bool playercollision = false;
    public void PlayerInteraction(GameObject item)
    {
        if(storedObject != null)
        {
            Debug.Log("there is a stored Object" + storedObject);
            if(item == null)
            {
                Debug.Log("Player not carryin Item" + item);
                GameManager.Instance.Player.GetComponent<CharacterMovement>().PickUpItem(storedObject);
                storedObject = null;
            }
            else
            {
                Debug.Log("Player is carrying item, going to switch" + item);
                GameManager.Instance.Player.GetComponent<CharacterMovement>().switchItems(this);
            }
        }
        else
        {
            StoreItem(item);
            Debug.Log("Stored object is null, storing player Item" + item);
        }
    }

    public void StoreItem(GameObject item)
    {
        if(item != null)
        {
            if (storedObject == null)
            {
                GameManager.Instance.Player.GetComponent<CharacterMovement>().giveItem(this);
                storedObject = item;
                storedObject.transform.position = itemPosition.position;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playercollision = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            playercollision = false;
        }
    }
}
