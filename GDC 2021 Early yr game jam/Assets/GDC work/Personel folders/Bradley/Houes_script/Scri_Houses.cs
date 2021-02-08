using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scri_Houses : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Houses;
    public Renderer rend;

    void Start()
    {
        Instantiate(Houses, new Vector3(transform.position.x+2.0f, transform.position.y-5.0f, transform.position.z - 2.0f), Quaternion.identity);
        rend = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        
        rend.enabled = false;
    }
}
