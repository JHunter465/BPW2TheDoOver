using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHighlight : MonoBehaviour
{
    [Tooltip("This only has to be set manually when the highlight is not a direct child of this object")]
    [SerializeField] private GameObject Highlight;

    private void Start()
    {
        foreach(GameObject HL in GameObject.FindGameObjectsWithTag("Highlight"))
        {
            if (HL.transform.IsChildOf(transform))
            {
                Highlight = HL;
                Highlight.SetActive(false);
                break;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Highlight.SetActive(true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Highlight.SetActive(false);
        }
    }
}
