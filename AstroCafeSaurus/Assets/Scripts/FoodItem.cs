using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodItem : MonoBehaviour
{
    const int STATE_PRE = 1;
    const int STATE_COMBINABLE = 2;

    int currentState = 1;

    public bool pre = true;
    public bool combinable = false;

    public void changeState(int state)
    {
        if (currentState == state)
        {
            return;
        }
        switch (state)
        {
            case STATE_PRE:
                pre = true;
                combinable = false;
                Debug.Log("I Changed! " + combinable);
                break;
            case STATE_COMBINABLE:
                combinable = true;
                pre = false;
                Debug.Log("I Changed! " + combinable);
                break;
        }
        currentState = state;
    }
}
