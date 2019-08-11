using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyDishes : MonoBehaviour
{
    public List<GameObject> dirtyPlates = new List<GameObject>();

    private int maxPlates = 5;
    private GameObject dirtyPlate;

    [SerializeField] private Transform dirtyDishPos;

    public void Setup()
    {
        maxPlates = Mathf.RoundToInt(8 / GameManager.Instance.Difficulty);
        dirtyPlate = GameManager.Instance.Plates;
    }

    public void PlayerInteraction(GameObject item)
    {
        if(dirtyPlates.Count > 0)
        {
            GameManager.Instance.Player.GetComponent<CharacterMovement>().PickUpItem(dirtyPlates[dirtyPlates.Count -1]);
            dirtyPlates.RemoveAt(dirtyPlates.Count -1);
        }
    }

    public void newDishes(int Amount)
    {
        for(int i =0; i< Amount; i++)
        {
            if (maxPlates > dirtyPlates.Count)
            {
                GameObject dirtySpawn = (GameObject)Instantiate(Resources.Load("Prefabs/Plate/Plate"), new Vector3(dirtyDishPos.position.x, dirtyPlates.Count, dirtyDishPos.position.z), this.transform.rotation, transform);
                dirtyPlates.Add(dirtySpawn);
                dirtySpawn.GetComponent<PlateOrder>().changeState(2);
            }
        }
    }
}
