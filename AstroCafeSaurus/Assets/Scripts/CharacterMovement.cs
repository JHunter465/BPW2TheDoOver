using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody m_rb;

    private bool stoveCollision = false;
    private bool garbageBinCollision = false;
    private bool dishWasherCollision = false;
    private bool dirtDishCollision = false;
    private bool counterTopCollision = false;
    private bool orderStationCollision = false;
    private bool cuttingBoardCollision = false;
    private bool foodStorageCollision = false;
    private bool emptyTopCollision = false;

    private GameObject Item;
    private GameObject tempStorage;

    public Vector3 handPosition;


    //Calculates the movement speed and applies it to the rigidbody
    public void MovePlayer(Vector3 MoveForce, float MoveSpeed, Rigidbody rb)
    {
        MoveSpeed = MoveSpeed * .1f;
        Vector3 NewMoveForce = new Vector3(MoveForce.x * MoveSpeed, MoveForce.y * MoveSpeed, MoveForce.z * MoveSpeed);
        rb.AddForce(NewMoveForce, ForceMode.Impulse);
        m_rb = rb;
    }

    //Checks what kind of interaction should be called
    public void Interaction()
    {
        Debug.Log(emptyTopCollision);
        if (stoveCollision)
        {
            GameManager.Instance.stove.PlayerInteraction(Item);
        }
        else if (garbageBinCollision)
        {
            GameManager.Instance.GarbageBin.PlayerInteraction(Item);
        }
        else if (dishWasherCollision)
        {
            GameManager.Instance.DishWasher.PlayerInteraction(Item);
        }
        else if (dirtDishCollision)
        {
            GameManager.Instance.DirtyDishes.PlayerInteraction(Item);
        }
        else if (counterTopCollision)
        {
            CountertopScript.Instance.PlayerInteraction(Item);
        }
        else if (orderStationCollision)
        {
            GameManager.Instance.OrderStation.PlayerInteraction();
        }
        else if (cuttingBoardCollision)
        {
            GameManager.Instance.cuttingboard.PlayerInteraction(Item);
        }
        else if (foodStorageCollision)
        {
            GameManager.Instance.whichStorage(transform.position).PlayerInteraction();
        }
        else
        {
            Debug.Log("ActivatingPlayerInteraction");
            EmptyTopManager.Instance.PlayerInteraction(Item);
        }
    }


    //Sets flags depending on which the player is closest to
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Stove")
        {
            stoveCollision = true;
        }
        if(other.gameObject.tag == "GarbageBin")
        {
            garbageBinCollision = true;
        }
        if (other.gameObject.tag == "DishWasher")
        {
            dishWasherCollision = true;
        }
        if (other.gameObject.tag == "DirtyDishes")
        {
            dirtDishCollision = true;
        }
        if (other.gameObject.tag == "CounterTop")
        {
            counterTopCollision = true;
        }
        if (other.gameObject.tag == "OrderStation")
        {
            orderStationCollision = true;
        }
        if (other.gameObject.tag == "CuttingBoard")
        {
            cuttingBoardCollision = true;
        }
        if (other.gameObject.tag == "FoodStorage")
        {
            foodStorageCollision = true;
        }
        //if (other.gameObject.tag == "EmptyTop")
        //{
        //    emptyTopCollision = true;
        //}
    }

    //Same as before but disables flags
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Stove")
        {
            stoveCollision = false;
        }
        if (other.gameObject.tag == "GarbageBin")
        {
            garbageBinCollision = false;
        }
        if (other.gameObject.tag == "DishWasher")
        {
            dishWasherCollision = false;
        }
        if (other.gameObject.tag == "DirtyDishes")
        {
            dirtDishCollision = false;
        }
        if (other.gameObject.tag == "CounterTop")
        {
            counterTopCollision = false;
        }
        if (other.gameObject.tag == "OrderStation")
        {
            orderStationCollision = false;
        }
        if (other.gameObject.tag == "CuttingBoard")
        {
            cuttingBoardCollision = false;
        }
        if (other.gameObject.tag == "FoodStorage")
        {
            foodStorageCollision = false;
        }
        //if (other.gameObject.tag == "EmptyTop")
        //{
        //    emptyTopCollision = false;
        //}
    }

    public void giveItem(EmptyTop emptyTop)
    {
        Debug.Log("Item van handen naar countertop");
        Item.transform.parent = emptyTop.itemPosition;
        Item = null;
    }

    public void switchItems(EmptyTop topSurface)
    {
        if (topSurface.storedObject.tag == "Plate")
        {
            if (topSurface.storedObject.GetComponent<PlateOrder>().PlaceOnPlate(Item) != null)
            {
                Item = null;
            }
        }
        else if (Item.tag == "Plate")
        {
            if (Item.GetComponent<PlateOrder>().PlaceOnPlate(topSurface.storedObject) != null)
            {
                topSurface.storedObject = null;
                topSurface.StoreItem(Item);
            }
        }
        else
        {
            //tempStorage = topSurface.storedObject;
            //topSurface.storedObject = null;
            //topSurface.StoreItem(Item);
            //Item = tempStorage;
            //tempStorage = null;
        }
    }

    public void PickUpItem (GameObject item)
    {
        Debug.Log("trying to pick this up " + item);
        if(Item == null)
        {
            Debug.Log("Player is not holding anything so getting it from surfacetop" + item);
            Item = item;
            UpdateItem();
        }
    }

    public void UpdateItem()
    {
        Debug.Log("The item is being parented to the player " + Item);
        Item.transform.parent = this.transform;
        Item.transform.position = handPosition;
    }

    public GameObject WhatIsItem()
    {
        return Item;
    }

    public void OrderGiven()
    {
        if(Item != null)
        {
            Destroy(Item);
            Item = null;
        }
        Item = null;
    }
}
