using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviorAudio : MonoBehaviour
{

    AudioClip sfx_clip;
    public AudioSource AudioData;
    public GameObject savedata;
    int condition;
    public int CoinGet;
    //public GameObject AudioGO;
    // Start is called before the first frame update
    void Start()
    {
        //AudioData = AudioGO.GetComponent<AudioSource>();
        sfx_clip = AudioData.GetComponent<AudioClip>();
        condition = savedata.GetComponent<InputParameters>().condition;
        CoinGet = 0;
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnTriggerEnter(Collider collision)
    {
        //Debug.Log("triogend");
        if (collision.gameObject.tag == "Sphere")
        {

            //this.GetComponent<MeshRenderer>().material = transparent;
            //this.transform.position = new Vector3(-9f, 100f, 24.2f);
            if (condition == 1) // no coins
            {

            }
            else if (condition == 2) // just coins
            {

            }
            else if (condition == 3) // coins + haptic
            {

            }
            else if (condition == 4) // coins + audio
            {
                AudioData.PlayOneShot(sfx_clip);
                AudioData.Play();
            }
            else if (condition == 5) // coins + haptic + audio
            {
                AudioData.PlayOneShot(sfx_clip);
                AudioData.Play();
            }

            CoinGet = 1;

            //working before 09/30, updated for data saving purposes
            this.gameObject.SetActive(false);
            //
            //Debug.Log("collision");
            //this.gameObject.transform.position = new Vector3(-9, 100, 24.2f);
            //collisionon = true;


        }

    }
}
