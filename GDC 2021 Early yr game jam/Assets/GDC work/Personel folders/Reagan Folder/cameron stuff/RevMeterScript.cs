using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RevMeterScript : MonoBehaviour
{
    public GameObject mainCamera;
    public GameObject revMeter;
    public BrainSlugScript slug;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(mainCamera.transform);
        revMeter.transform.localScale = new Vector3(revMeter.transform.localScale.x, slug.speed, revMeter.transform.localScale.z);
        //revMeter.transform.rotation = Quaternion.LookRotation(slug.direction);

        
    }
}
