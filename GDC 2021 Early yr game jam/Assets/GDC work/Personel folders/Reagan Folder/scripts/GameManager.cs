using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public double time = 0;
    public int numPeopleInfected = 0;
    private int totalNPCs = 20;
    public bool playerisdead = false;


    public static GameManager Instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null) 
        { 
            Instance = this; 
        
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (totalNPCs == numPeopleInfected)//if all people are infected
        {
            endGame(true);
        }
        else if (numPeopleInfected == 0 && playerisdead == true)
        {
            endGame(false);
        }
    }

    public void endGame(bool didwin)
    {
        //this method ends the game and did win tells us if the player has won
    }

}
