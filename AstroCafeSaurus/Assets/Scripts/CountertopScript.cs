using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountertopScript : MonoBehaviour
{
    [Tooltip("List of seats for dinosaurs to sit")]
    [SerializeField] private List<SeatScript> Seats;

    private List<bool> playercollided = new List<bool>();

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
    public static CountertopScript Instance { get; private set; }

    //In this setup the manager sets all seats to empty
    public void Setup()
    {
        foreach(SeatScript seat in Seats)
        {
            seat.taken = false;
        }
    }

    public SeatScript anySeat()
    {
        foreach (SeatScript seat in Seats)
        {
            if (!seat.taken)
            {
                seat.taken = true;
                return seat;
            }
        }
        return null;
    }

    //Lets the manager now a seat is left so it can go back into the pool, this is done by checking if the positions of the seat in the list and from the customer are the same
    public void leftSeat(SeatScript seating)
    {
        Debug.Log("20");
        foreach(SeatScript seat in Seats)
        {
            if(seat.gameObject == seating.gameObject)
            {
                seat.taken = false;
            }
        }
    }

    public GameObject closestChair(Vector3 playerPos, Vector3 seatPos, float distance)
    {
        foreach(SeatScript seat in Seats)
        {
            Debug.Log(distance == Vector3.Distance(playerPos, seat.gameObject.transform.position));
            if(distance == Vector3.Distance(playerPos, seat.gameObject.transform.position))
            {
                Debug.Log(seat.gameObject.name);
                GameObject closest = seat.gameObject;
                return closest;
            }
        }
        return null;
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach(SeatScript seat in Seats)
        {
            if(other.gameObject.tag == "Player")
            {
                playercollided[Seats.IndexOf(seat)] = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach(SeatScript seat in Seats)
        {
            if(other.gameObject.tag == "Player")
            {
                playercollided[Seats.IndexOf(seat)] = false;
            }
        }
    }


    public void PlayerInteraction(GameObject theOrder)
    {
        if(theOrder != null)
        {
            if (theOrder.gameObject.tag == "Plate")
            {
                foreach (SeatScript seat in Seats)
                {
                    if (seat.playerPresent)
                    {
                        theOrder.GetComponent<PlateOrder>().createItemList();
                        seat.playerInteraction(theOrder.GetComponent<PlateOrder>().actualOrder);
                        GameManager.Instance.Player.GetComponent<CharacterMovement>().OrderGiven();
                        break;
                    }
                }
            }
        }
    }
}
