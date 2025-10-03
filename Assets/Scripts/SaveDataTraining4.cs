using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Text;
public class SaveDataTraining4 : MonoBehaviour
{

    //public static GlobalControl Instance;

    //public CollisionsPP LocalCopyOfData;
    //public bool IsSceneBeingLoaded = false;

    public GameObject pacer;
    public string filepathpre;// = @"C:\Users\mcrlab\Documents\DewilFiles\ErrP_Reach\Prep";
    //public string trialnum = "01";
    private string delimiter = ",";
    private string extension = ".csv";
    private string filepath;
    public int trialnum;
    public int direction;
    public bool hit;
    public int hitt;
    public GameObject Background;
    private float Distance;
    public GameObject MainHand;
    int finished;
    float pacerx;

    public GameObject coin1;
    public GameObject coin2;
    public GameObject coin3;

    public GameObject sinusoid;
    public float coin1x;
    public float coin2x;
    public float coin3x;

    public float coin1y;
    public float coin2y;
    public float coin3y;

    public int coin1present;
    public int coin2present;
    public int coin3present;

    public int CoinGet1;
    public int CoinGet2;
    public int CoinGet3;

    public int Coin1go;
    public int Coin2go;
    public int Coin3go;


    // Start is called before the first frame update
    private void Start()
    {
        //trialnum = StaticValsReachPP.trialnum;
        //direction = StaticValsReachPP.curdir;
    }


    private void Awake()
    {
        hitt = 0;
        Debug.Log("tm: " + StaticValsReach7.curindex);
        if (Time.frameCount < 3)
        {
            trialnum = 1;
        }
        else
        {
            trialnum = StaticValsReach7.curindex + 1;
        }
        
        Debug.Log("trialnum:" + trialnum);
        //direction = StaticValsReach2.curdir;
        //if (Instance == null)
        //{
        //    DontDestroyOnLoad(gameObject);
        //    Instance = this;
        //}
        //else if (Instance != this)
        //{
        //    Destroy(gameObject);
        //}

        //Generate Fill file name
        filepath = filepathpre + "Trial" + trialnum + extension;
        //Debug.Log("globaltrialnum: " + trialnum);
        //Debug.Log("globalpath: " + filepath);

        //Generate Stringbuilder to store data
        StringBuilder content = new StringBuilder();
        //Write in header informaiton
        content.AppendLine("Frame, Time,Distance,Posx,Posy,Posz,Pacerx,Hit,Finished,Coin1x, Coin1y, Coin1go, Collected1, Coin2x, Coin2y, Coin2go, Collected2, Coin3x, Coin3y, Coin3go, Collected3");
        //Debug.Log(content);
        File.AppendAllText(filepath, content.ToString());
        //Debug.Log("File Created!");

    }

   

    // Update is called once per frame
    private void Update()
    {
        //Debug.Log(targetstring);
        //string coin = collisionobject.GetComponent<CollisionsPP>().coinname;
        finished = MainHand.GetComponent<OptitrackRigidBody19>().finished;
        Distance = Background.GetComponent<Feedback_color5>().distfeedback;
        hit = MainHand.GetComponent<OptitrackRigidBody19>().holdingsphere;
        if(hit == true)
        {
            hitt = 1;
        }
            
        float posx = MainHand.transform.position.x;
        float posy = MainHand.transform.position.y;
        float posz = MainHand.transform.position.z;
        if (posx > 0 & posx < 0.55f & hitt == 1)
        {
            pacerx = pacer.GetComponent<Pacer3>().Gobj.transform.position.x;
            //pacerx = pacer.transform.position.x;
        }
        else if (posx < 0)
        {
            pacerx = 0f;
        }
        else if (posx > 0.55f){
            pacerx = 0.55f;
        }

        coin1x = coin1.transform.position.x;
        coin2x = coin2.transform.position.x;
        coin3x = coin3.transform.position.x;

        coin1y = coin1.transform.position.y;
        coin2y = coin2.transform.position.y;
        coin3y = coin3.transform.position.y;


        CoinGet1 = coin1.GetComponent<CoinBehaviorAudio>().CoinGet;
        CoinGet2 = coin2.GetComponent<CoinBehaviorAudio>().CoinGet;
        CoinGet3 = coin3.GetComponent<CoinBehaviorAudio>().CoinGet;

        Coin1go = sinusoid.GetComponent<CoinBombScript1>().Coin1go;
        Coin2go = sinusoid.GetComponent<CoinBombScript1>().Coin2go;
        Coin3go = sinusoid.GetComponent<CoinBombScript1>().Coin3go;



        //coin1y = sinusoid.GetComponent<CoinBombScript1>().hity1;
        //coin2y = sinusoid.GetComponent<CoinBombScript1>().hity2;
        //coin3y = sinusoid.GetComponent<CoinBombScript1>().hity3;

        //coin1present = sinusoid.GetComponent<CoinBombScript1>().presented1;
        //coin2present = sinusoid.GetComponent<CoinBombScript1>().presented2;
        //coin3present = sinusoid.GetComponent<CoinBombScript1>().presented3;




        StringBuilder content = new StringBuilder();
        //collect variable information and write to csv
        content.AppendLine(Time.frameCount + delimiter + Time.time + delimiter + Distance + delimiter + posx + delimiter + posy + delimiter + posz +
            delimiter +pacerx + delimiter + hitt + delimiter + finished + delimiter + coin1x + delimiter + coin1y + delimiter + Coin1go + delimiter + CoinGet1 + delimiter + 
            coin2x + delimiter + coin2y + delimiter + Coin2go + delimiter + CoinGet2 + delimiter + coin3x + delimiter + coin3y + delimiter + Coin3go + delimiter + CoinGet3);
        File.AppendAllText(filepath, content.ToString());

        //if (targetcount == 6)
        //{
        //    Debug.Break();
        //}
    }
}
