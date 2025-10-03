/* 
Copyright © 2016 NaturalPoint Inc.

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at

http://www.apache.org/licenses/LICENSE-2.0

Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License. 
*/

using System;
using UnityEngine;
using System.Collections;
using System.Net.Sockets;
using System.IO;


/// <summary>
/// Implements live tracking of streamed OptiTrack rigid body data onto an object.
/// </summary>
public class OptitrackRigidBody19 : MonoBehaviour
{
    [Tooltip("The object containing the OptiTrackStreamingClient script.")]
    [HideInInspector]
    public OptitrackStreamingClient StreamingClient;

    [Tooltip("The Streaming ID of the rigid body in Motive")]
    //[HideInInspector]
    public Int32 RigidBodyId;

    [Tooltip("Subscribes to this asset when using Unicast streaming.")]
    [HideInInspector]
    public bool NetworkCompensation = true;
    //[HideInInspector]
    public GameObject maybeparent;
    //[HideInInspector]
    public Color grn;
    public Material greenmat;
    public Shader grnshader;
    //[HideInInspector]
    public GameObject background;
    //[HideInInspector]
    public GameObject savedata;
    //[HideInInspector]
    public Material transparentmat;
    //[HideInInspector]
    public int finished;

    float depth;
    [HideInInspector]
    public float MaxReach;
    [HideInInspector]
    public int P_type;
    [HideInInspector]
    public int level;
    [HideInInspector]
    public bool triggeron;
    [HideInInspector]
    public Vector3 motivedat;

    Vector3 MotiveChange;
    Vector3 StartPos;
    Vector3 StartMotive;
    [HideInInspector]
    public Vector3 targetPosition;
    Vector3 rbposchange;
    float HoldDepth;
    String QuadName;
    GameObject QuadObj;
    Vector3 fullstep;
    Vector3 stepbystep;
    double startx;
    double endx;
    float step;
    Vector3 lastpos;
    OptitrackRigidBodyState rbState;
    bool restart;
    bool temprestart;
    Vector3 Perturbation;
    Vector3 Rotation;
    float waitit;
    float waittime = 500f;
    bool ff_on = false;
    Vector3 postrans;
    UnityEngine.Vector3 movement;
    //[HideInInspector]
    public float hitx;
    public float hitx1;
    public float hitx2;
    public float hitx3;

    public float localy1;
    public float localy2;
    public float localy3;

    public float prehitx1;
    public float prehitx2;
    public float prehitx3;


    //[HideInInspector]
    public GameObject sinusoid;

    int breakloc;
    bool streamingon;
    //[HideInInspector]
    public bool holdingsphere;
    //[HideInInspector]
    public GameObject ghostsphere;
    //[HideInInspector]
    public bool hitquad;
    Vector3 holdpos;
    public int[] manylocations;

    internal Boolean socketReadys = false;
    TcpClient StartSocket;
    NetworkStream StartStream;
    StreamWriter StartWriter;
    StreamReader StartReader;
    String Host = "localhost";
    Int32 StartPort = 55003;

    TcpClient EndSocket;
    NetworkStream EndStream;
    StreamWriter EndWriter;
    StreamReader EndReader;
    Int32 EndPort = 55004;
    public int direction;
    public int block;
    public int moved;
    int rot;
    void Start()
    {
        // If the user didn't explicitly associate a client, find a suitable default.
        if (this.StreamingClient == null)
        {
            this.StreamingClient = OptitrackStreamingClient.FindDefaultClient();

            // If we still couldn't find one, disable this component.
            if (this.StreamingClient == null)
            {
                Debug.LogError(GetType().FullName + ": Streaming client not set, and no " + typeof(OptitrackStreamingClient).FullName + " components found in scene; disabling this component.", this);
                this.enabled = false;
                return;
            }
        }

        MaxReach = savedata.GetComponent<InputParameters>().armlength;
        P_type = savedata.GetComponent<InputParameters>().condition;
        block = savedata.GetComponent<InputParameters>().block;
        rot = 0;
        depth = MaxReach / -200;
        StartPos = new Vector3(0.1338f, -0.1996f, depth);
        maybeparent.transform.position = new Vector3(0f,0f,0f);
        maybeparent.transform.position = new Vector3(0f,0f,0f);
        rbposchange = new Vector3(0, Time.deltaTime, 0);
        triggeron = false;
        restart = StaticValsReach7.restart;
        temprestart = restart;
        //works
        StaticValsReach7.initports();


        StartStream = StaticValsReach7.StartStream;
        EndStream = StaticValsReach7.EndStream;
        //
        Debug.Log("optorestart: " + temprestart);
        this.StreamingClient.RegisterRigidBody(this, RigidBodyId);

        postrans = new Vector3(0, 0, 0);
        Perturbation = new Vector3(0f, 0f, 0f);
        waitit = 0f;
        finished = 0;
        holdingsphere = false;

            level = savedata.GetComponent<SaveDataTraining4>().trialnum;

        ff_on = false;
        moved = 0;
    }


#if UNITY_2017_1_OR_NEWER
    void OnEnable()
    {
        Application.onBeforeRender += OnBeforeRender;
    }


    void OnDisable()
    {
        Application.onBeforeRender -= OnBeforeRender;
    }


    void OnBeforeRender()
    {
        UpdatePose();
    }
#endif


    void Update()
    {

        UpdatePose();
    }

    
    void UpdatePose()
    {
        holdingsphere = ghostsphere.GetComponent<GhostSphere2>().collisionon;
        //level = StaticValsReach4.curindex;

        streamingon = StreamingClient.StreamingOn;

        rbState = StreamingClient.GetLatestRigidBodyState(RigidBodyId, NetworkCompensation);

        motivedat = rbState.Pose.Position;

        motivedat = new Vector3(-motivedat.z, -motivedat.y, motivedat.x);
        if (Time.frameCount == 2)
        {

            StartMotive = motivedat;
        }
        else if (temprestart & streamingon & motivedat != new Vector3(0.00f,0.00f,0.00f))
        {
            temprestart = false;
            Debug.Log("temprestart: " + temprestart);

  
                StartMotive = motivedat;
            
                       
        }
        prehitx1 = sinusoid.GetComponent<CoinBombScript1>().prehitx1;
        prehitx2 = sinusoid.GetComponent<CoinBombScript1>().prehitx2;
        prehitx3 = sinusoid.GetComponent<CoinBombScript1>().prehitx3;

        hitx1 = sinusoid.GetComponent<CoinBombScript1>().hitx1;
        hitx2 = sinusoid.GetComponent<CoinBombScript1>().hitx2;
        hitx3 = sinusoid.GetComponent<CoinBombScript1>().hitx3;

        if(holdingsphere & (P_type == 2 | P_type == 5) & block == 2)
        {
            Debug.Log("ready to present coins");
            if (prehitx1 <= this.transform.position.x && this.transform.position.x <= hitx1)
            {
                Debug.Log("poofcoins1");
                sinusoid.GetComponent<CoinBombScript1>().presentObjects(0);
            }
            else if (prehitx2 <= this.transform.position.x && this.transform.position.x <= hitx2)
            {
                Debug.Log("poofcoins2");

                sinusoid.GetComponent<CoinBombScript1>().presentObjects(1);
            }
            else if (prehitx3 <= this.transform.position.x && this.transform.position.x <= hitx3)
            {
                Debug.Log("poofcoins3");

                sinusoid.GetComponent<CoinBombScript1>().presentObjects(2);
            }
        }
        

            if (this.transform.position.x > hitx & holdingsphere & moved == 0)
        {


                //working
                Byte[] sendBytes = BitConverter.GetBytes(Time.realtimeSinceStartup);
                //

                //StartSocket.GetStream().Write(sendBytes, 0, sendBytes.Length);
                
                // working
                StartStream.Write(sendBytes, 0, sendBytes.Length);
                //

                //sinusoid.GetComponent<SinusoidScript6>().moveSinusoid(breakloc);
                moved = 1;
                


        }
        if(this.transform.position.x > 0.5f & holdingsphere & finished == 0)
        {
            background.GetComponent<Renderer>().material.color = Color.grey;


            finished = 1;

            //works
           Byte[] sendBytes = BitConverter.GetBytes(Time.realtimeSinceStartup);
            //
            //EndSocket.GetStream().Write(sendBytes, 0, sendBytes.Length);
            
            //works
            EndStream.Write(sendBytes, 0, sendBytes.Length);
            //

            Debug.Log("done: " + finished);

        }
        else if(this.transform.position.x < 0f)
        {

        }
        //if(finished == 1)
        //{
        //    background.GetComponent<Renderer>().material.color = Color.green;
        //}


        MotiveChange = motivedat - StartMotive;
        Debug.Log("Motivedat: " + motivedat);



        targetPosition = StartPos - MotiveChange;



        

       
        transform.localPosition = targetPosition;


    }
    
}
    
