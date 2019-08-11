using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Player and Difficulty")]
    [Tooltip("Assign the player object to this to ensure dinosaurs will stare your player to death")]
    [SerializeField] public GameObject Player;
    [Tooltip("Determines the difficulty of the game by changing the patience of the customers and increasing the penalty whenever an order is not right")]
    [SerializeField] public int Difficulty = 1;
    [Header("CookingTops")]
    [Tooltip("Specifies which cuttingboard can be used")]
    [SerializeField] public CuttingBoard cuttingboard;
    [Tooltip("Specifies which stove can be used")]
    [SerializeField] public Stove stove;
    [SerializeField] public GarbageBin GarbageBin;
    [SerializeField] public DishWasher DishWasher;
    [SerializeField] public DirtyDishes DirtyDishes;
    [SerializeField] public OrderStation OrderStation;
    [SerializeField] public List<FoodStorage> FoodStorages = new List<FoodStorage>();
    [SerializeField] public GameObject Plates;


    [Header("Recipes and Stuff")]
    [Space]
    [Header("|LETTUCE = 0| FISH = 1| TOMATO = 2| BUN = 3|")]
    [Space(25)]
    [SerializeField] private List<int> RecipeA;
    [SerializeField] private List<int> RecipeB;
    [SerializeField] private List<int> RecipeC;
    [SerializeField] private List<int> RecipeD;
    [SerializeField] private List<int> RecipeE;


    private List<List<int>> Recipes = new List<List<int>>();

    const int LETTUCE = 0;
    const int FISH = 1;
    const int TOMATO = 2;
    const int BUN = 3;

    private float currentScore = 0f;
    
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

        //Setup before the game begins
        CreateRecipes();
        currentScore = 0f;
        CountertopScript.Instance.Setup();
        DinoManager.Instance.Setup();
        UIManager.Instance.Setup();
        DirtyDishes.Setup();
        DirtyDishes.newDishes(10);
    }
    public static GameManager Instance { get; private set; }

    //Adds all usermade recipes to a List to randomly pick a recipe from upon request from customer
    private void CreateRecipes()
    {
        Recipes.Add(RecipeA);
        Recipes.Add(RecipeB);
        Recipes.Add(RecipeC);
        Recipes.Add(RecipeD);
        Recipes.Add(RecipeE);
    }

    //Creates a random order from list of recipes, returns order to dinosaur and sends list to UIManager to be displayed for the player
    public List<int> NewOrder()
    {
        List<int> Order = Recipes[UnityEngine.Random.Range(0, Recipes.Count)];

        foreach(int ingredient in Order)
        {
            Debug.Log(ingredient);
        }
        UIManager.Instance.AddOrder(Order);

        return Order;
    }

    //Determines the current score of the player, needs an amount to add or subtract the score
    public float Score(float amount)
    {
        Debug.Log("19");
        float newscore = currentScore + amount;
        if(newscore < 0)
        {
            gameOver();
        }
        currentScore = newscore;
        UIManager.Instance.UpdateScore(currentScore);
        return currentScore;
    }

    //Chooses a place to put on fire while knowing where the player is in relation to the fire extinguisher so that the player can always reach it to put out the fire
    public void DeathRay()
    {
        Debug.Log("21");
        Debug.Log("There should be a fire somewhere now");
    }

    //Goes to the endgame screen with GameOver and final score. Resets all level interactibles
    private void gameOver()
    {

    }

    public FoodStorage whichStorage(Vector3 position)
    {
        Debug.Log("2");
        float foodA = Vector3.Distance(position, FoodStorages[0].GetComponent<Transform>().position);
        float foodB = Vector3.Distance(position, FoodStorages[1].GetComponent<Transform>().position);
        float foodC = Vector3.Distance(position, FoodStorages[2].GetComponent<Transform>().position);
        float foodD = Vector3.Distance(position, FoodStorages[3].GetComponent<Transform>().position);
        if (foodA < foodB && foodA < foodC && foodA < foodD)
        {
            return FoodStorages[0];
        }
        else if (foodB < foodA && foodB < foodC && foodB < foodD)
        {
            return FoodStorages[1];
        }
        else if (foodC < foodA && foodC < foodB && foodC < foodD)
        {
            return FoodStorages[2];
        }
        else if (foodD < foodA && foodD < foodC && foodD < foodB)
        {
            return FoodStorages[3];
        }
        else { return null; }
    }
}
