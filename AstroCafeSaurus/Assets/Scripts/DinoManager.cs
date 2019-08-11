using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DinoManager : MonoBehaviour
{

    [Tooltip("The spawn position for dinosaurs")]
    [SerializeField] GameObject DinosaurSpawn;
    [Tooltip("Maximum amount of Customers to be active at once")]
    [SerializeField] private int maxDinos = 2;
    [Tooltip("The Amount of customers in this level")]
    [SerializeField] public int Customers = 2;
    [Tooltip("Specifies the location where the dino's go to die")]
    [SerializeField] public Transform Despawn;

    private List<DinosaurScript> Dinos = new List<DinosaurScript>();
    private UnityEngine.Object[] Dinosaurs;

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
    public static DinoManager Instance { get; private set; }

    //Setup for dinsoaur adds a first batch of dinosaurs to the list and spawns them all at once
    public void Setup()
    {
        Dinosaurs = Resources.LoadAll("Prefabs/Dinosaurs");
        for(int i = 0; i < (maxDinos-1); i++)
        {
            DinosaurScript dinosaur = NewCustomer(i*2).GetComponent<DinosaurScript>();
            Dinos.Add(dinosaur);
            dinosaur.startSequence();
        }
    }

    //Spawns a new Customer at the spawnlocation if there is enough space, and starts the sequence of the customer 
    public GameObject NewCustomer(int distance)
    {
        Debug.Log("23");
        if (Dinos.Count < (maxDinos - 1))
        {
            GameObject Dinosaur = (GameObject)Instantiate(Dinosaurs[UnityEngine.Random.Range(0, Dinosaurs.Length)], DinosaurSpawn.transform.position+new Vector3(0,0,distance), DinosaurSpawn.transform.rotation);
            return Dinosaur;
        }
        return null;
    }

    //Destroys the Dino that requests so, also deletes it from the list and spawns a new customer
    public void DestroyDino (DinosaurScript dino)
    {
        Debug.Log("22");
        Dinos.Remove(dino);
        Destroy(dino.gameObject);
        GameObject Dinosaur = NewCustomer(Dinos.Count);
        if (Dinosaur != null)
        {
            DinosaurScript dinoScript = Dinosaur.GetComponent<DinosaurScript>();
            Dinos.Add(dinoScript);
            dinoScript.startSequence();
        }
    }
}
