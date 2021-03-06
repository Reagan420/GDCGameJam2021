using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public float time = 0;
    public int numPeopleInfected = 0;
    private int totalNPCs = 20;
    public bool playerisdead = false;

    public GameObject[] Navs;
    public GameObject[] NPCs;
    // 
    private GameObject walkpos;

    // Ui
    public Text Infection_count;
    public Text Infection_Timer;

    public static GameManager Instance { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        walkpos = new GameObject();
        walkpos.transform.tag = "walkPos";
        //spawnNPCWalkLocations();

        Navs = GameObject.FindGameObjectsWithTag("walkPos");
        NPCs = GameObject.FindGameObjectsWithTag("NPC");

        if (Instance == null) 
        { 
            Instance = this; 
        
        }
    }

    // Update is called once per frame
    void Update()
    {

        //manageGameState();


        time -= Time.deltaTime ;

       // Ui_update();
    }

    // updates the UI 
    void Ui_update()
    {
        float seconds = Mathf.FloorToInt(time % 60);
        float Mins = Mathf.FloorToInt(time / 60);

        Infection_count.text = numPeopleInfected.ToString();
        Infection_Timer.text = string.Format("{0:00}:{1:00}", Mins, seconds);
    }

    public void endGame(bool didwin)
    {
        //this method ends the game and did win tells us if the player has won

        if(didwin == true)
        {
            // load player won 
            Loadscene("3 - Won_game");
        }
        else
        {
            // player lost
            Loadscene("2 -Game_Over");
        }
    }

    private void manageGameState()
    {

        if (totalNPCs == numPeopleInfected)//if all people are infected
        {
            endGame(true);
        }
        else if ((numPeopleInfected == 0 && playerisdead == true) || time <= 0.0f)
        {
            endGame(false);
        }
    }

    // this loads the scene bois
    private void Loadscene(string level)
    {
        SceneManager.LoadScene(level);
    }

    private void spawnNPCWalkLocations()
    {
        for (int i = 0; i < 100; i++)
        {
            trySpawnwalklocation();
        }
    }

    private void trySpawnwalklocation()
    {
        int limit = 220;

        float randomx = Random.Range(-limit, limit);
        float randomz = Random.Range(-limit, limit);

        Vector3 startPos = new Vector3(randomx, 0 , randomz);

        int layerMask = 1 << 8;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(startPos, transform.TransformDirection(Vector3.down), out hit, 200, layerMask))
        {
            if (hit.collider.tag != "road")
            {
                Instantiate(walkpos, hit.transform);
            }
            
        }
    }

    


}
