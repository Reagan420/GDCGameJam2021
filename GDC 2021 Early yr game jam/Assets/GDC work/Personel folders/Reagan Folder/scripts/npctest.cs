using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npctest : MonoBehaviour
{
    public GameObject[] Navs;
    UnityEngine.AI.NavMeshAgent agent;
    bool waiting = false;

    bool Infected = false;

    public Material textInfected;
    public Material textHealthy;

    void Start()
    {
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        
    }

    void Update()
    {
        if (agent.velocity.magnitude == 0f)
        {
            if (waiting == false)
            {
                StartCoroutine(waiter());
                
                
                waiting = true;
            }
            
        }
    }

    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSeconds(1);
        toggleInfection();
        agent.SetDestination(Navs[Random.Range(0, Navs.Length)].transform.position);
        waiting = false;
    }


    void setInfected()
    {
        agent.speed = 1;
        this.gameObject.GetComponent<MeshRenderer>().material = textInfected;
    }

    void setHealthy()
    {
        agent.speed = 3;
        this.gameObject.GetComponent<MeshRenderer>().material = textHealthy;
    }

    void toggleInfection()
    {
        if (Infected == false)
        {
            setInfected();
            Infected = true;
        }
        else
        {
            setHealthy();
            Infected = false;
        }
    }


}
