using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterMovement))]
public class PlayerController : MonoBehaviour
{
    [Tooltip("How fast the player moves")]
    [SerializeField] private float movementspeed = 0f;
    [Tooltip("The rigidbody Attached to the player")]
    [SerializeField] private Rigidbody rb;
    [Tooltip("The Script responsible for the actual movement")]
    [SerializeField] private CharacterMovement cM;
    [Tooltip("This vector determines where the items are held")]
    [SerializeField] private Transform handPosition;

    private float HorizontalMove = 0f;
    private float VerticalMove = 0f;
    private float jumpValue = 0f;
    private Vector3 MoveForce = Vector3.zero;

    private void Awake()
    {
        cM.handPosition = handPosition.position;
    }

    private void Update()
    {
        HorizontalMove = Input.GetAxisRaw("Horizontal");
        VerticalMove = Input.GetAxisRaw("Vertical");
        Vector2 movementVector = new Vector2(HorizontalMove, VerticalMove);
        MoveForce = new Vector3(movementVector.x, jumpValue, movementVector.y);
        if(HorizontalMove != 0 || VerticalMove != 0)
        {
            transform.forward = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")));
        }
        if(Input.GetButtonDown("Interact"))
        {
            Debug.Log("We hebben de toets gevoeld auw");
            cM.Interaction();
        }
    }

    private void FixedUpdate()
    {
        cM.MovePlayer(MoveForce, movementspeed, rb);
        cM.handPosition = handPosition.position;
    }
}
