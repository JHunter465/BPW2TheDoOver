using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageBin : MonoBehaviour
{
    [SerializeField] private List<string> Disposable = new List<string>();

    public void PlayerInteraction(GameObject item)
    {
        if(item != null)
        {
            foreach (string tag in Disposable)
            {
                if (tag == item.tag)
                {
                    Destroy(item);
                }
            }
            if (item.tag == "Plate")
            {
                item.GetComponent<PlateOrder>().CleanYourself();
            }
        }
    }
}
