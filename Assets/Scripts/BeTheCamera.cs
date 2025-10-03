using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeTheCamera : MonoBehaviour
{
    public GameObject camra;
    public GameObject rig;
    public bool moved;
    public Quaternion newrot;
    public Vector3 tarpos;
    public GameObject sphere;
    // Start is called before the first frame update
    void Start()
    {
        moved = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (camra.transform.position != new Vector3(0f, 0f, 0f) & moved == false)
        {
            this.transform.position = camra.transform.position;
            this.transform.rotation = camra.transform.rotation;
            moved = true;
        }
        if (Input.GetKeyUp(KeyCode.J))
        {
            newrot = Quaternion.Inverse(this.transform.rotation) * this.transform.rotation;
            rig.transform.rotation = Quaternion.Inverse(this.transform.rotation) * rig.transform.rotation;
            this.transform.rotation =  newrot;
            //tarpos = new Vector3(0.3f, -0.071f, -0.716f);
            //previous setting
            //tarpos = new Vector3(0.3f, -0.04f, -0.716f);
            //
            //tarpos = new Vector3(0.26f, -0.07f, -0.716f);
            
            //09/23
            //tarpos = new Vector3(0.3f, -0.07f, -0.716f);
            //
            tarpos = new Vector3(.11f, -0.00f, -0.716f);
            sphere.transform.position = tarpos;
            //Vector3 posdiff = tarpos - this.transform.position;
            //rig.transform.Translate(posdiff,Space.World);
        }
    }
}
