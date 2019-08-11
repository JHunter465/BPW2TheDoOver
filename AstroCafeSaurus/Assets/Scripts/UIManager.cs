using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI KeyComboText;
    [SerializeField] Image Panel

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
    public static UIManager Instance { get; private set; }

    //should probably create some sort of UI elements?
    public void Setup()
    {

    }

    //adds an order to the UI
    public void AddOrder(List<int> Order)
    {

    }

    //Updates the score visible in the UI
    public void UpdateScore(float score)
    {

    }

    //Popup that says no available seats left!
    public void noAvailableSeats()
    {

    }

    public void AddKeyCombo(GameObject cookingTop, List<string> theOrder)
    {
        List<string> keyCombo = DecodeList(theOrder);
        KeyComboText.text = keyCombo[0] + " + " + keyCombo[1] + " + " + keyCombo[2] + " + " + keyCombo[3];
    }

    private List<string> DecodeList (List<string> Order)
    {
        List<string> returnList = new List<string>();
        foreach(string keycode in Order)
        {
            if(keycode == "Key1")
            {
                returnList.Add("Up");
            }
            if(keycode == "Key2")
            {
                returnList.Add("Down");
            }
            if(keycode == "Key3")
            {
                returnList.Add("Left");
            }
            if(keycode == "Key4")
            {
                returnList.Add("Right");
            }
        }
        return returnList;
    }



    public void FailedCombo()
    {

    }

    public void SuccesCombo()
    {

    }
}
