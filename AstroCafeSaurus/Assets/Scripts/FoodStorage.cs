using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodStorage : MonoBehaviour
{
    [SerializeField] private GameObject pieceOFood;
    public void PlayerInteraction()
    {
        Debug.Log("3");
        GameObject yourFood = Instantiate(pieceOFood);
        GameManager.Instance.Player.GetComponent<CharacterMovement>().PickUpItem(yourFood);
        Debug.Log(gameObject.name);
        Debug.Log(yourFood.name);
        if(GameManager.Instance.Player.GetComponent<CharacterMovement>().WhatIsItem() != yourFood)
        {
            Destroy(yourFood);
        }
    }
}
