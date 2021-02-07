using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlScript : MonoBehaviour
{
    public float speed = 5;
    public bool parkour;

    
    public Vector3 startLocation;
    public Vector3 destination;

    public GameObject host;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (host)
        {
            Controls();
            MouseControls();
        }
    }

    void Controls()
    {
        if (Input.GetKey(KeyCode.W))
        {
            host.transform.position += new Vector3(0, 0, speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.S))
        {
            host.transform.position += new Vector3(0, 0, -speed * Time.deltaTime);
        }

        if(Input.GetKey(KeyCode.A))
        {
            host.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            host.transform.position += new Vector3(speed * Time.deltaTime, 0, 0);
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
                    destination = new Vector3(hit.point.x, host.transform.position.y, hit.point.z);
                }
            }

            if (Vector3.Distance(host.transform.position, destination) > 0.11)
            {
                host.transform.position = Vector3.MoveTowards(host.transform.position, destination, speed * Time.deltaTime);
            }
        }
    }

}
