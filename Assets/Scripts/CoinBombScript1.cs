using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBombScript1 : MonoBehaviour
{
    //float[] xlocations;

    public bool manualpresentation;

    public GameObject sphere;
    public GameObject coin1;

    public GameObject coin2;

    public GameObject coin3;

    //public Mesh coin;

    public float hitx;
    public float hitx1;
    public float hitx2;
    public float hitx3;

    public float hity1;
    public float hity2;
    public float hity3;

    public float localy1;
    public float localy2;
    public float localy3;

    public float prehitx1;
    public float prehitx2;
    public float prehitx3;
    public int[] manylocations;
    public int manylocations1;
    public int manylocations2;
    public int manylocations3;
    public int[] ydirs;
    public GameObject savedata;
    public int condition;
    //public Material transparent;
    //public Material coincolor;
    public GameObject Gobj1;
    public GameObject Gobj2;
    public GameObject Gobj3;

    public GameObject[] coinarray;
    public Material orange;

    public string location1;
    public string location2;
    public string location3;

    public int presented1;
    public int presented2;
    public int presented3;

    public int chosenloc1;
    public int chosenloc2;
    public int chosenloc3;

    public int chosendir1;
    public int chosendir2;
    public int chosendir3;

    public int Coin1go;
    public int Coin2go;
    public int Coin3go;

    // Start is called before the first frame update
    void Start()
    {
       condition = savedata.GetComponent<InputParameters>().condition;
        // possiblelocations = { 52, 63, 74, 115, 125, 136, 178, 188, 199 };


        if (manualpresentation)
        {

            Coin1go = chosenloc1;
            Coin2go = chosenloc2;
            Coin3go = chosenloc3;

            ManuallyChooseLocations();

        }
        else
        {

            if (Time.frameCount < 3)
            {
                manylocations1 = 52;
                manylocations2 = 125;
                manylocations3 = 199;
            }
            else
            {
                manylocations1 = StaticValsReach7.ran1;
                manylocations2 = StaticValsReach7.ran2;
                manylocations3 = StaticValsReach7.ran3;
            }

            Coin1go = manylocations1;
            Coin2go = manylocations2;
            Coin3go = manylocations3;

            ChooseLocations();
        }



        MoveCoins();
    }

    void ManuallyChooseLocations()
    {
        //int[] manylocations = new int[4];
        ydirs = new int[3];
        ydirs[0] = chosendir1;
        ydirs[1] = chosendir2;
        ydirs[2] = chosendir3;


        location1 = "go" + chosenloc1;
        location2 = "go" + chosenloc2;
        location3 = "go" + chosenloc3;


        Gobj1 = GameObject.Find(location1);
        Gobj2 = GameObject.Find(location2);
        Gobj3 = GameObject.Find(location3);
        if (condition == 1 | condition == 4)
        {
            Gobj1.GetComponent<MeshRenderer>().material = orange;
            Gobj2.GetComponent<MeshRenderer>().material = orange;
            Gobj3.GetComponent<MeshRenderer>().material = orange;

            Gobj1.transform.localScale = new Vector3(.01f, .01f, .01f);
            Gobj2.transform.localScale = new Vector3(.01f, .01f, .01f);
            Gobj3.transform.localScale = new Vector3(.01f, .01f, .01f);

            Gobj1.tag = "step1";
            Gobj2.tag = "step1";
            Gobj3.tag = "step1";
        }


      

        prehitx1 = Gobj1.transform.position.x - .1f;
        hitx1 = Gobj1.transform.position.x;
        localy1 = Gobj1.transform.position.y;
        prehitx2 = Gobj2.transform.position.x - .1f;
        hitx2 = Gobj2.transform.position.x;
        localy2 = Gobj2.transform.position.y;
        prehitx3 = Gobj3.transform.position.x - .1f;
        hitx3 = Gobj3.transform.position.x;
        localy3 = Gobj3.transform.position.y;

        hity1 = localy1 + (ydirs[0] * .2f);
        hity2 = localy2 + (ydirs[1] * .2f);
        hity3 = localy3 + (ydirs[2] * .2f);

    }
    void ChooseLocations()
    {
        //int[] manylocations = new int[4];
        ydirs = new int[3];
        //int onelocation = StaticValsReach6.ranval[1];
        //Debug.Log("locations1" + onelocation);

        location1 = "go" + manylocations1;
        location2 = "go" + manylocations2;
        location3 = "go" + manylocations3;

        //GameObject Gobj1 = GameObject.Find(location1);
        //GameObject Gobj2 = GameObject.Find(location2);
        //GameObject Gobj3 = GameObject.Find(location3);
        Gobj1 = GameObject.Find(location1);
        Gobj2 = GameObject.Find(location2);
        Gobj3 = GameObject.Find(location3);
        if(condition == 1 | condition == 4)
        {
            Gobj1.GetComponent<MeshRenderer>().material = orange;
            Gobj2.GetComponent<MeshRenderer>().material = orange;
            Gobj3.GetComponent<MeshRenderer>().material = orange;

            Gobj1.transform.localScale = new Vector3(.01f, .01f, .01f);
            Gobj2.transform.localScale = new Vector3(.01f, .01f, .01f);
            Gobj3.transform.localScale = new Vector3(.01f, .01f, .01f);

            Gobj1.tag = "step1";
            Gobj2.tag = "step1";
            Gobj3.tag = "step1";
        }
        

        Debug.Log("Gobj1: " + Gobj1);
        var f = new System.Random();
        for (int i = 0; i < 3; i++)
        {
            //var f = new System.Random();
            if (f.Next(-1, 2) < 1)
            {
                ydirs[i] = -1;
            }
            else
            {
                ydirs[i] = 1;
            }
        }
        Debug.Log("ydir1: " + ydirs[0]);
        Debug.Log("ydir2: " + ydirs[1]);
        Debug.Log("ydir3: " + ydirs[2]);

        prehitx1 = Gobj1.transform.position.x - .1f;
        hitx1 = Gobj1.transform.position.x;
        localy1 = Gobj1.transform.position.y;
        prehitx2 = Gobj2.transform.position.x - .1f;
        hitx2 = Gobj2.transform.position.x;
        localy2 = Gobj2.transform.position.y;
        prehitx3 = Gobj3.transform.position.x - .1f;
        hitx3 = Gobj3.transform.position.x;
        localy3 = Gobj3.transform.position.y;

        hity1 = localy1 + (ydirs[0] * .2f);
        hity2 = localy2 + (ydirs[1] * .2f);
        hity3 = localy3 + (ydirs[2] * .2f);

    }
    void MoveCoins()
    {

        if (condition == 1 | condition == 4)
            {
            coin1.transform.position = new Vector3(hitx1, localy1 + (ydirs[0] * .2f), 0f);
            coin2.transform.position = new Vector3(hitx2, localy2 + (ydirs[1] * .2f), 0f);
            coin3.transform.position = new Vector3(hitx3, localy3 + (ydirs[2] * .2f), 0f);

            presented1 = 1;
            presented2 = 1;
            presented3 = 1;


        }
        

        

        coinarray = new GameObject[3];
        
        coinarray[0] = coin1;
        coinarray[1] = coin2;
        coinarray[2] = coin3;

       
    }
    public void presentObjects(int numob)
    {

        if(numob == 0)
        {
            coin1.transform.position = new Vector3(hitx1, localy1 + (ydirs[0] * .2f), 0f);
            Gobj1.GetComponent<MeshRenderer>().material = orange;
            Gobj1.tag = "step1";

            Gobj1.transform.localScale = new Vector3(.01f, .01f, .01f);
            presented1 = 1;
        }
        else if(numob == 1)
        {
            coin2.transform.position = new Vector3(hitx2, localy2 + (ydirs[1] * .2f), 0f);
            Gobj2.GetComponent<MeshRenderer>().material = orange;
            Gobj2.transform.localScale = new Vector3(.01f, .01f, .01f);
            Gobj2.tag = "step1";
            presented2 = 1;

        }
        else
        {
            coin3.transform.position = new Vector3(hitx3, localy3 + (ydirs[2] * .2f), 0f);
            Gobj3.GetComponent<MeshRenderer>().material = orange;
            Gobj3.transform.localScale = new Vector3(.01f, .01f, .01f);
            Gobj3.tag = "step1";
            presented3 = 1;
        }
        

    }
   
}
