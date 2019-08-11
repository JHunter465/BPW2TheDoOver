using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyTopManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> emptyTops = new List<GameObject>();

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }
    }
    public static EmptyTopManager Instance { get; private set; }
    public void PlayerInteraction(GameObject item)
    {
        GameObject Item = item;
        foreach (GameObject emptytop in emptyTops)
        {
            EmptyTop emptytopScript = emptytop.GetComponent<EmptyTop>();
            if (emptytopScript.playercollision)
            {
                Debug.Log(emptytop.name);
                emptytopScript.PlayerInteraction(Item);
                break;
            }
        }
    }
}
