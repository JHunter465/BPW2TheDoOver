using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateOrder : MonoBehaviour
{
    public List<GameObject> Order = new List<GameObject>();

    const int STATE_CLEAN = 1;
    const int STATE_DIRTY = 2;

    int currentState = 1;

    public bool clean = true;
    public bool dirty = false;

    public List<int> actualOrder = new List<int>();

    [SerializeField] private Transform lettuce;
    [SerializeField] private Transform tomato;
    [SerializeField] private Transform fish;
    [SerializeField] private Transform bun;

    public void CleanYourself()
    {
        foreach(GameObject ingredient in Order)
        {
            Destroy(ingredient);
        }
        Order.Clear();
    }

    public GameObject PlaceOnPlate(GameObject item)
    {
        if (clean)
        {
            if (item.GetComponent<FoodItem>().combinable)
            {
                Order.Add(item);
                item.transform.parent = transform;
                UpdatePlate();
                return item;
            }
            else return null;
        }
        else return null;
    }

    public void changeState(int state)
    {
        if (currentState == state)
        {
            return;
        }
        switch (state)
        {
            case STATE_CLEAN:
                clean = true;
                dirty = false;
                Debug.Log("Plate Clean");
                break;
            case STATE_DIRTY:
                dirty = true;
                clean = false;
                Debug.Log("Plate Dirty");
                break;
        }
        currentState = state;
    }

    public void createItemList()
    {
        actualOrder.Clear();
        foreach(GameObject ingredient in Order)
        {
            if(ingredient.tag == "Lettuce")
            {
                actualOrder.Add(0);
            }
            if(ingredient.tag == "Fish")
            {
                actualOrder.Add(1);
            }
            if(ingredient.tag == "Tomato")
            {
                actualOrder.Add(2);
            }
            if(ingredient.tag == "Bun")
            {
                actualOrder.Add(3);
            }
        }
    }

    public void UpdatePlate()
    {
        foreach (GameObject ingredient in Order)
        {
            if (ingredient.tag == "Lettuce")
            {
                ingredient.transform.position = lettuce.position;
            }
            if (ingredient.tag == "Fish")
            {
                ingredient.transform.position = fish.position;
            }
            if (ingredient.tag == "Tomato")
            {
                ingredient.transform.position = tomato.position;
            }
            if (ingredient.tag == "Bun")
            {
                ingredient.transform.position = bun.position;
            }
        }
    }
}
