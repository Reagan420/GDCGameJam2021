using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npctest : MonoBehaviour
{
    //public GameObject[] Navs;
    UnityEngine.AI.NavMeshAgent agent;
    bool waiting = false;

    GameObject gamemanager;

    bool Infected = false;

    public Material textInfected;
    public Material textHealthy;

    public GameObject child;
    private bool isNPC = true;


    private float speed = 5;
    private Vector3 destination;

    private GameObject cam;

    private bool pathing = false;

    void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        gamemanager = GameObject.FindGameObjectWithTag("GameManager");

    }

    void Update()
    {
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (this.transform.childCount != 0)
        {
            setInfected();
        }
        if (this.transform.childCount == 0)//if the npc is npc do npc things
        {
            if (agent.velocity.magnitude == 0f)
            {
                if (waiting == false)
                {
                    StartCoroutine(waiter());
                }
            }
        }
        else
        {
            if (pathing)
            {
                StopCoroutine(waiter());
                pathing = false;
            }
            Controls();
            MouseControls();

            
        }
    }


    void MouseControls()
    {
        if (Input.GetMouseButton(0))
        {
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody != null)
                {
                    destination = new Vector3(hit.point.x, this.transform.position.y, hit.point.z);
                }
            }

            if (Vector3.Distance(this.transform.position, destination) > 0.11)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, destination, speed * Time.deltaTime);
            }
        }
    }
    void Controls()
    {
       cam.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        if (Input.GetKey(KeyCode.W))
        {
            this.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.S))
        {
            this.transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.A))
        {
            this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            this.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
        }

    }



    IEnumerator waiter()
    {
        pathing = true;
        //Wait for 4 seconds
        yield return new WaitForSeconds(1);
        agent.SetDestination(gamemanager.GetComponent<GameManager>().Navs[Random.Range(0, gamemanager.GetComponent<GameManager>().Navs.Length)].transform.position);
        waiting = false;
    }


    void setInfected()
    {
        Infected = true;
        isNPC = false;
        agent.speed = 1;
        this.gameObject.GetComponent<MeshRenderer>().material = textInfected;
        
    }

    void setHealthy()
    {
        isNPC = true;
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            setInfected();
            collision.gameObject.transform.parent = this.transform;
            collision.gameObject.GetComponent<Collider>().isTrigger = true;
            StopAllCoroutines();
            collision.gameObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
            collision.gameObject.GetComponent<Rigidbody>().useGravity = false;
            collision.gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            collision.gameObject.gameObject.transform.localPosition = new Vector3(0, 0, 0);
            isNPC = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<Rigidbody>().useGravity = true;
        other.GetComponent<Collider>().isTrigger = false;
    }

    private void OnTriggerEnter(Collider other)
    {

        StopAllCoroutines();
    }

    public void remChild()
    {
        child = null;
        isNPC = true;

    }


}
