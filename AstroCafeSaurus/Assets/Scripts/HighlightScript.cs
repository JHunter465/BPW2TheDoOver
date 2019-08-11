using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighlightScript : MonoBehaviour
{
    //DISCLAIMER This class might be inplemented later in the game as it is not needed right now, it has no function right now


    [Tooltip("Sets the size of the highlight (Should atleast be 1.01, Default is 1.05")]
    [SerializeField] private float HighlightSize = 1.05f;


    
    private void Awake()
    {
        transform.localScale = new Vector3(HighlightSize, HighlightSize, HighlightSize);
    }
}
