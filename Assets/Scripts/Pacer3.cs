using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pacer3 : MonoBehaviour
{
    //[HideInInspector]
    public Vector3 curpos;
    //[HideInInspector]
    public GameObject Hand;
    ////[HideInInspector]
    public GameObject ghostsphere;
    [HideInInspector]
    public bool colon;
    [HideInInspector]
    public GameObject Gobj;
    //[HideInInspector]
    public GameObject parms;
    //[HideInInspector]
    public Material greyLine;
    //public Material transparent;
    //[HideInInspector]
    public int gnum;
    public GameObject runwayend;
    int frames;
    // Start is called before the first frame update
    void Start()
    {
        //curpos = new Vector3(-.05f, 0.083f, 0);
        //this.transform.position = curpos;
        gnum = 0;
        frames = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        frames = frames + 1;
        //Debug.Log("gnum:" + gnum);
        colon = ghostsphere.GetComponent<GhostSphere2>().collisionon;
        int block = parms.GetComponent<InputParameters>().block;
        //if(curpos.x < .55f & colon)
        //{
        //    //Debug.Log("Time: " + Time.time);
        //    curpos = curpos + new Vector3(Time.deltaTime / 10f, 0f, 0f);
        //    this.transform.position = curpos;
        //}
        //if(Hand.transform.position.x > 0)
        //{
        //    Gobj.transform.position = new Vector3(0f, 0f, 0f);
        //}

        if (Hand.transform.position.x > 0 & colon)
        {
            string Gname = "go" + gnum;
            
            Gobj = GameObject.Find(Gname);
            //Debug.Log(Gobj);
        //if(block == 1)
        //{
        if(Gobj.tag == "step1")
            {
                gnum = gnum + 1;
            }
            else
            {
                if (frames % 10 < 8)
                //if (frames % 1 == 0)
                {
                    //Debug.Log("frame2: " + frames);
                    Gobj.GetComponent<MeshRenderer>().material.color = Color.yellow;
                    gnum = gnum + 1;
                }
            }
            
        //}
        //else
        //{
        //    Gobj.GetComponent<MeshRenderer>().material = greyLine;
        //}

            //gnum = gnum + 1;
        }
        if (gnum == 252)
        {
            runwayend.GetComponent<LineRenderer>().material = greyLine;
            //
            this.enabled = false;
            //
        }

        //if (Hand.transform.position.x > 0.55f)
        //{
        //    Gobj.transform.position = new Vector3(0.55f, 0f, 0f);
        //}

    }
}
