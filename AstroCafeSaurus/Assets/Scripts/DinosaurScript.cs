using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DinosaurScript : MonoBehaviour
{
    [Tooltip("Specifies the distance between the object and the next in front of it")]
    [SerializeField] private float minDistance = 1f;
    [Tooltip("Specifies which layers the dinosaur can interact with to move to the next state")]
    [SerializeField] private LayerMask canInteract;
    [Tooltip("This Specifies the patience of the dinosaur when the game setting is set to EASY")]
    [SerializeField] private float easy = 30f;
    [Tooltip("This Specifies the patience of the dinosaur when the game setting is set to MEDIUM")]
    [SerializeField] private float medium = 20f;
    [Tooltip("This Specifies the patience of the dinosaur when the game setting is set to HARD")]
    [SerializeField] private float hard = 10f;

    private int difficulty = 0;

    const int STATE_ORDERING = 0;
    const int STATE_WALKING = 1;
    const int STATE_SITTING = 2;
    const int STATE_ATTACKING = 3;
    const int STATE_IDLE = 4;

    int currentAnimationState = STATE_IDLE;

    private float distanceToObject = 0f;
    private bool hasOrdered = false;
    private bool sitting = false;
    private float speed = 1f;

    private Vector3 mySpot;
    private SeatScript mySeat;
    private float thePatience = 0f;

    private float currentTime = 0f;

    private List<int> Order;
    private float patience = 0f;
    private float scoreLevel = 0f;

    private Vector3 standingPos;

    private Coroutine WalkForwardRoutine;
    private Coroutine StaringRoutine;


    //This sequence is called when the dino is spawned and when it has ordered, it checks if it can walk towards a chair
    public void startSequence()
    {
        if (!hasOrdered)
        {
            WalkForwardFunction();
        }
        else if (!sitting)
        {
            StopCoroutine(WalkForwardRoutine);
            if (mySeat != null)
            {
                GetComponent<NavMeshAgent>().destination = mySeat.gameObject.transform.GetChild(0).position;
                StartCoroutine(WaitingForMyChair());
            }
        }
    }
    private void WalkForwardFunction()
    {
        if(WalkForwardRoutine != null) { StopCoroutine(WalkForwardRoutine); }
        WalkForwardRoutine = StartCoroutine(WalkForward());
    }

    //Moves the dino forward untill it collides with anything
    IEnumerator WalkForward()
    {
        while (true)
        {
            if (CheckForCollision())
            {
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            yield return new WaitForFixedUpdate();
        }
    }

    //When player takes order this script gives the dinosaur an order if there is enough seating available otherwise a prompt
    public void Ordering()
    {
        mySeat = CountertopScript.Instance.anySeat();
        if (mySeat != null)
        {
            Order = GameManager.Instance.NewOrder();
            hasOrdered = true;
            startSequence();
        }
        else
        {
            UIManager.Instance.noAvailableSeats();
        }
    }

    //This is activated when the dino is navigating towards his chair, it checks if it is in front of the chair and starts seating when so
    IEnumerator WaitingForMyChair()
    {
        bool isWaiting = true;
        while (isWaiting)
        {
            Vector3 chair = new Vector3(mySeat.gameObject.transform.GetChild(0).position.x, 0, mySeat.gameObject.transform.GetChild(0).position.z);
            Vector3 dino = new Vector3(transform.position.x, 0, transform.position.z);
            float distance = Vector3.Distance(chair, dino);
            if (distance < 1.09f)
            {
                transform.parent = mySeat.gameObject.transform.GetChild(0);
                SittingDown();
                isWaiting = false;
            }
            yield return null;
        }
    }

    //Parents the Dino to a chair and starts the timer
    public void SittingDown()
    {
        StopCoroutine(WaitingForMyChair());
        GetComponent<NavMeshAgent>().enabled = false;
        standingPos = transform.position;
        transform.localPosition = new Vector3(0, 5.1f, 0);
        sitting = true;
        StartTimer();
        StartStaring();
    }

    public void StartStaring()
    {
        if (StaringRoutine != null) { StopCoroutine(StaringRoutine); }
        StaringRoutine = StartCoroutine(Staring());
    }

    IEnumerator Staring()
    {
        bool shouldStare = true;
        while (shouldStare)
        {
            var targetRotation = Quaternion.LookRotation(GameManager.Instance.Player.transform.GetChild(1).transform.position - transform.position, new Vector3(0, transform.position.y, 0));
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2.0f);
            yield return null;
        }

    }

    //Function for starting timer if it isn't done yet
    public void StartTimer()
    {
        patience = whatIsDifficulty();
        if (patience != 0)
        {
            StartCoroutine(clock(patience));
        }
    }

    //Actual timer, starts deathsequence if below zero, every second ticks
    IEnumerator clock(float theTime)
    {
        currentTime = theTime;
        while (true)
        {
            if (currentTime <= 0)
            {
                Debug.Log("17");
                LaserEyes();
                break;
            }
            currentTime -= 1f;
            Debug.Log("TIME" + currentTime);
            yield return new WaitForSeconds(1f);
        }
    }

    //The sequence that adds a penaltyscore, unparents from seat and starts deathray in gamemanager
    public void LaserEyes()
    {
        Debug.Log("18");
        float pentaltyScore = 3.7f * difficulty;
        GameManager.Instance.Score(pentaltyScore);
        CountertopScript.Instance.leftSeat(mySeat);
        transform.parent = null;
        transform.position = standingPos;
        GetComponent<NavMeshAgent>().enabled = true;
        GameManager.Instance.DeathRay();
        DinoManager.Instance.DestroyDino(this);
    }

    //The order is confirmed, score is sent back to gamemanager, seat is left and dino walks toward despawnpoint
    public void OrderFinished()
    {
        GameManager.Instance.Score(calculateScore(currentTime));
        CountertopScript.Instance.leftSeat(mySeat);
        transform.parent = null;
        transform.position = standingPos;
        GetComponent<NavMeshAgent>().enabled = true;
        GetComponent<NavMeshAgent>().destination = DinoManager.Instance.Despawn.position;
        sitting = false;
    }

    //Determines what the score is to send to the gamemanager based on the difficulty and time left.
    private float calculateScore(float time)
    {
        if (time <= (patience * (1 / 3)))
        {
            scoreLevel = (1 / 3);
        }
        if (time > (patience * (1 / 3)) && time < (patience * (2 / 3)))
        {
            scoreLevel = (2 / 3);
        }
        if (time >= (patience * (2 / 3)))
        {
            scoreLevel = 1;
        }
        float score = (scoreLevel * ((27 / DinoManager.Instance.Customers) * (100 / 27)));
        return score;
    }

    //Determines what the difficulty is for this level
    public float whatIsDifficulty()
    {
        difficulty = GameManager.Instance.Difficulty;
        if(difficulty == 1) {  thePatience = easy; }
        else if (difficulty == 2) {  thePatience = medium; }
        else if (difficulty == 3) { thePatience = hard; }
        else thePatience = 0f;
        Debug.Log(thePatience);

        return thePatience;
    }

    //This shoots a raycast infront of the dino to check if it collides with someting, determines if the minimum distance is reached. if it touches another dino or interactible it sends out a special queue
    private bool CheckForCollision()
    {
        RaycastHit Hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out Hit, Mathf.Infinity))
        {
            Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
            distanceToObject = Hit.distance;
            if (distanceToObject > minDistance)
            {
                return true;
            }
            else
            {
                if (Hit.collider.gameObject.layer == 12)
                {
                    if (distanceToObject < 0f)
                    {
                        return false;
                    }
                    else return true;
                }
                else
                {
                    return false;
                }
            }
        }
        else
        {
            return false;
        }
    }

    //simple check if two lists match (for the order and finished meal)
    public bool DoListsMatch(List<int> list1)
    {
        bool areListsEqual = true;

        if (list1.Count != Order.Count)
            return false;

        list1.Sort(); // Sort list one
        Order.Sort(); // Sort list two

        for (int i = 0; i < list1.Count; i++)
        {
            if (Order[i] != list1[i])
            {
                areListsEqual = false;
            }
        }

        return areListsEqual;
    }

    //private void changeState(int state)
    //{
    //    if (currentAnimationState == state)
    //    {
    //        return;
    //    }
    //    switch (state)
    //    {
    //        case STATE_IDLE:

    //            break;
    //        case STATE_WALKING:

    //            break;
    //        case STATE_SITTING:

    //            break;
    //        case STATE_ATTACKING:

    //            break;
    //        case STATE_ORDERING:

    //            break;
    //    }
    //    currentAnimationState = state;
    //}
}
