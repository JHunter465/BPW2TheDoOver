using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingBoard : MonoBehaviour
{
    [SerializeField] private List<string> Cuttable = new List<string>();
    [SerializeField] private List<string> keys = new List<string>();

    private List<string> randomKeys = new List<string>();

    private List<string> theOrder = new List<string>();

    private GameObject theFood;

    private Coroutine FirstKeyRoutine;
    private Coroutine SecondKeyRoutine;
    private Coroutine ThirdKeyRoutine;
    private Coroutine FourthKeyRoutine;

    private const string Key1 = "Key1";
    private const string Key2 = "Key2";
    private const string Key3 = "Key3";
    private const string Key4 = "Key4";

    private bool firstkey = false;
    private bool secondkey = false;
    private bool thirdkey = false;
    private bool fourthkey = false;

    private string pressed;

    private Coroutine setPressedRoutine;

    private bool busy = false;

    private void Awake()
    {
        foreach (string key in keys)
        {
            randomKeys.Add(key);
        }
    }

    public void PlayerInteraction(GameObject item)
    {
        if(item != null)
        {
            foreach (string tag in Cuttable)
            {
                if (tag == item.tag)
                {
                    if (!item.GetComponent<FoodItem>().combinable)
                    {
                        startCutting();
                        theFood = item;
                    }
                }
            }
        }
    }

    private void startCutting()
    {
        if (theOrder.Count > 0)
        {
            theOrder.Clear();
        }
        for (int i = 0; i <= 3; i++)
        {
            theOrder.Add(randomKeys[Random.Range(0, randomKeys.Count)]);
        }
        UIManager.Instance.AddKeyCombo(this.gameObject, theOrder);
        firstkey = true;
        Debug.Log(theOrder[0]);
        Debug.Log(theOrder[1]);
        Debug.Log(theOrder[2]);
        Debug.Log(theOrder[3]);
    }

    private void OnGUI()
    {
        pressed = "";
        Event e = Event.current;
        if (e.isKey)
        {
            string keys = e.keyCode.ToString();
            pressed = "";
            if (keys == "UpArrow")
            {
                StartPressed(Key1);
            }
            else if (keys == "DownArrow")
            {
                StartPressed(Key2);
            }
            else if (keys == "LeftArrow")
            {
                StartPressed(Key3);
            }
            else if (keys == "RightArrow")
            {
                StartPressed(Key4);
            }
            else { pressed = ""; }
        }
    }
    public void StartPressed(string keys)
    {
        if (setPressedRoutine != null) { StopCoroutine(setPressedRoutine); }
        setPressedRoutine = StartCoroutine(setPressed(keys));
    }

    IEnumerator setPressed(string keys)
    {
        yield return new WaitForSeconds(.05f);
        pressed = keys;
    }

    private void Update()
    {
        if (firstkey == true)
        {
            if (Input.GetButtonDown(theOrder[0]))
            {
                Debug.Log("First Succes!");
                secondkey = true;
                pressed = "";
                firstkey = false;
            }
            else if (theOrder[0] == Key1)
            {
                if (pressed == "Key2" || pressed == "Key3" || pressed == "Key4")
                {
                    Debug.Log("FAIL" + pressed);
                    firstkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
            else if (theOrder[0] == Key2)
            {
                if (pressed == "Key1" || pressed == "Key3" || pressed == "Key4")
                {
                    Debug.Log("FAIL" + pressed);
                    firstkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
            else if (theOrder[0] == Key3)
            {
                if (pressed == "Key1" || pressed == "Key2" || pressed == "Key4")
                {
                    Debug.Log("FAIL" + pressed);
                    firstkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
            else if (theOrder[0] == Key4)
            {
                if (pressed == "Key2" || pressed == "Key3" || pressed == "Key1")
                {
                    Debug.Log("FAIL" + pressed);
                    firstkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
        }
        else if (secondkey == true)
        {
            if (Input.GetButtonDown(theOrder[1]))
            {
                Debug.Log("Second Succes!");
                thirdkey = true;
                pressed = "";
                secondkey = false;
                Debug.Log(pressed);
            }
            else if (theOrder[1] == Key1)
            {
                if (pressed == "Key2" || pressed == "Key3" || pressed == "Key4")
                {
                    Debug.Log("FAIL" + pressed);
                    secondkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
            else if (theOrder[1] == Key2)
            {
                if (pressed == "Key1" || pressed == "Key3" || pressed == "Key4")
                {
                    Debug.Log("FAIL" + pressed);
                    secondkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
            else if (theOrder[1] == Key3)
            {
                if (pressed == "Key1" || pressed == "Key2" || pressed == "Key4")
                {
                    Debug.Log("FAIL" + pressed);
                    secondkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
            else if (theOrder[1] == Key4)
            {
                if (pressed == "Key2" || pressed == "Key3" || pressed == "Key1")
                {
                    Debug.Log("FAIL" + pressed);
                    secondkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
        }
        else if (thirdkey == true)
        {
            if (Input.GetButtonDown(theOrder[2]))
            {
                Debug.Log("Third Succes!");
                fourthkey = true;
                pressed = "";
                thirdkey = false;
                Debug.Log(pressed);
            }
            else if (theOrder[2] == Key1)
            {
                if (pressed == "Key2" || pressed == "Key3" || pressed == "Key4")
                {
                    Debug.Log("FAIL" + pressed);
                    thirdkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
            else if (theOrder[2] == Key2)
            {
                if (pressed == "Key1" || pressed == "Key3" || pressed == "Key4")
                {
                    Debug.Log("FAIL" + pressed);
                    thirdkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
            else if (theOrder[2] == Key3)
            {
                if (pressed == "Key1" || pressed == "Key2" || pressed == "Key4")
                {
                    Debug.Log("FAIL" + pressed);
                    thirdkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
            else if (theOrder[2] == Key4)
            {
                if (pressed == "Key2" || pressed == "Key3" || pressed == "Key1")
                {
                    Debug.Log("FAIL" + pressed);
                    thirdkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
        }
        else if (fourthkey == true)
        {
            if (Input.GetButtonDown(theOrder[3]))
            {
                Debug.Log("Last Succes!");
                theFood.GetComponent<FoodItem>().changeState(2);
                pressed = "";
                fourthkey = false;
                Debug.Log(pressed);
            }
            else if (theOrder[3] == Key1)
            {
                if (pressed == "Key2" || pressed == "Key3" || pressed == "Key4")
                {
                    Debug.Log("FAIL" + pressed);
                    fourthkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
            else if (theOrder[3] == Key2)
            {
                if (pressed == "Key1" || pressed == "Key3" || pressed == "Key4")
                {
                    Debug.Log("FAIL" + pressed);
                    fourthkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
            else if (theOrder[3] == Key3)
            {
                if (pressed == "Key1" || pressed == "Key2" || pressed == "Key4")
                {
                    Debug.Log("FAIL" + pressed);
                    fourthkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
            else if (theOrder[3] == Key4)
            {
                if (pressed == "Key2" || pressed == "Key3" || pressed == "Key1")
                {
                    Debug.Log("FAIL" + pressed);
                    fourthkey = false;
                    startCutting();
                    UIManager.Instance.FailedCombo();
                }
            }
        }
    }
}