using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostSphere2 : MonoBehaviour
{
    //public TMP_Text messagetext;
    // Start is called before the first frame update
    //public float SetDepth;
    public bool collisionon;
    public Material FadedMat;
    public Material PresentMat;
    public GameObject Hand;
    Vector3 pos;
    float depth;
    public GameObject savedata;
    public GameObject ghostsphere;
    float g1;
    float g2;
    //public Vector3 allpos;
    //Vector3 MotiveInput;
    void Start()
    {
        collisionon = false;
        //if (savedata.GetComponent<InputParameters>().block == 2)
        //{
        //    //ghostsphere.SetActive(false);
        //    g1 = .0522f;
        //    g2 = -0.0346f;
        //}
        //else
        {
            //ghostsphere.SetActive(true);
            g1 = .1f;
            g2 = -.1f;
        }
        
    }

    //Update is called once per frame
    void Update()
    {
        //Debug.Log("collision: " + collisionon);
        if (collisionon)
        {
            pos = new Vector3(Hand.transform.position.x, Hand.transform.position.y, 0f);
            depth = Hand.transform.position.z;
            this.transform.position = pos;
            //Debug.Log("Depth: " + depth);
            //if(Mathf.Abs(depth) > .1)
                if(depth > g1 | depth < g2)
            {
                //Debug.Log("baddepth");
                this.GetComponent<MeshRenderer>().material = PresentMat;
            }
            else
            {
                this.GetComponent<MeshRenderer>().material = FadedMat;
            }
        }
    }

    public void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("triogend");
        if (collision.gameObject.tag == "Sphere")
        {

            this.GetComponent<MeshRenderer>().material = FadedMat;

            collisionon = true;
            

        }

    }
}
