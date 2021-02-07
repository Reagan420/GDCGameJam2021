using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainSlugScript : MonoBehaviour
{
    
    public Vector3 direction;
    public GameObject floor;
    public float xMax;
    public float xMin;
    public float zMax;
    public float zMin;
    public float speed;
    public float maxSpeed = 10;

    public Vector3 mousepos;
    public GameObject host;
    public GameObject previousHost;
    // Start is called before the first frame update
    void Start()
    {
        floor = GameObject.FindGameObjectWithTag("Floor");

        xMax = floor.GetComponent<Renderer>().bounds.extents.x;
        xMin = -floor.GetComponent<Renderer>().bounds.extents.x;

        zMax = floor.GetComponent<Renderer>().bounds.extents.z;
        zMin = -floor.GetComponent<Renderer>().bounds.extents.z;

        speed = 0.1f;
    }

    // Update is called once per frame
    void Update()
    {

        Fling();

        //checkBounds();


    }

    private void checkBounds()
    {

        //boundaries
        if (transform.position.x > xMax)
        {

            transform.position = new Vector3(xMax - 1, this.transform.position.y, this.transform.position.z);
        }

        if (transform.position.x < xMin)
        {

            transform.position = new Vector3(-xMin + 1, this.transform.position.y, this.transform.position.z);
        }

        if (transform.position.z > zMax)
        {

            transform.position = new Vector3(this.transform.position.x, this.transform.position.y, zMax - 1);

        }

        if (transform.position.z < zMin)
        {

            transform.position = new Vector3(this.transform.position.x, this.transform.position.y, zMin + 1);
        }
    }


    void Fling()
    {

        if (Input.GetMouseButtonDown(1))
        {
            speed = 0.1f;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            direction = Vector3.zero;
            this.GetComponent<Collider>().isTrigger = true;
        }
        if (Input.GetMouseButton(1))
        {
            
            
            if (speed < 1)
            {
                speed += Time.deltaTime/3;
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            previousHost = host;
            host.GetComponent<npctest>().remChild();//make the npc remove this as a child
            this.transform.parent = null;
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            direction = Vector3.zero;

            
            RaycastHit hit;
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //int layerMask = 1 << 3;

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                direction = new Vector3(hit.point.x, this.transform.position.y, hit.point.z) - new Vector3(transform.position.x, this.transform.position.y, transform.position.z);

                    //direction.Normalize();
                    Debug.Log("direction:" + direction);
            }
            this.GetComponent<Rigidbody>().AddForce(direction * (speed * maxSpeed), ForceMode.Impulse);
            Debug.Log(hit.rigidbody);
        }

        if(this.gameObject.GetComponent<Collider>().isTrigger == false && this.gameObject.GetComponent<Rigidbody>().velocity.magnitude < 0.1)
        {
            makeOldHostMParent();
            
            this.GetComponent<Collider>().isTrigger = true;
            this.GetComponent<Rigidbody>().useGravity = false;
            this.transform.position = previousHost.transform.position;
            previousHost.GetComponent<npctest>().StopAllCoroutines();
            previousHost.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            previousHost.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    public void makemeparent(GameObject parent)
    {
        host = parent;
    }

    void makeOldHostMParent()
    {
        this.transform.parent = previousHost.transform;
        host = previousHost;
    }

    

}
