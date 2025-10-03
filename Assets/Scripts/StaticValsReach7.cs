using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using System.Net.Sockets;
using System.IO;

public static class StaticValsReach7
{

    //public static int[] tarlist = { 1,2,3,4,5,6,7,8 };
    //public static int[] errlist = { 0, 1, -1, 2, -2, 3,0,-2 };

    //public static int[] tarlist = { 6, 8, 5, 5, 1, 4, 2, 6, 5, 1, 2, 2, 2, 6, 5, 4, 4, 4, 7, 2, 6, 2, 1, 2, 5, 1, 6, 1, 8, 7, 5, 4, 1, 3, 3, 6, 7, 7, 4, 2, 3, 1, 5, 8, 4, 7, 1, 3, 1, 6 };
    //public static int[] errlist = { 0, 0, -2, 3, 0, 2, 0, 0, 0, -1, -2, 0, 0, 0, 0, 0, 0, 0, 1, 3, 0, 2, 0, 0, 0, 0, -1, 3, 0, 0, 0, 2, 0, 0, 0, 0, 1, 0, 0, 0, 0, -2, 0, 0, 0, 1, 0, -1, 0, 0};


    // public static int[] tarlist = { 6, 8,  5, 5, 1, 4, 2, 6, 5,  1,  2, 2, 2, 6, 5, 4, 4, 4, 7, 2, 6, 2, 1, 2, 5, 1,  6, 1, 8, 7, 5, 4, 1, 3, 3, 6, 7, 7, 4, 2, 3,  1, 5, 8, 4, 7, 1,  3, 1, 6 };
    // Values for error participants
    //public static int[] errlist = { 0, 0, -2, 3, 0, 2, 0, 0, 0, -1, 0, 0, 0, 0, -2, 0, 0, 0, 1, 0, 3, 0, 2, 0, 0, 0, -1, 3, 0, 0, 0, 2, 0, 0, 0, 0, 1, 0, 0, 0, 0, -2, 0, 0, 0, 1, 0, -1, 0, 0 };

    // values for control participants
    // public static int[] errlist = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    public static int[] possiblelocations = { 52, 63, 74, 115, 125, 136, 178, 188, 199 };
    //public static int[] dir = { 1, 2 };
    public static int[] breaklocations_training = { 188, 115, 136, 52, 74, 199, 52, 199, 136, 63, 115, 74, 125, 52, 125, 63, 178, 74, 63, 74, 188, 188, 52, 115, 136, 178, 125, 199, 136, 63, 115, 199, 125, 178, 188, 178 };
    public static int[] dir_training =              { 1,  1,    2,  2,  2,  1,   1,   2,  1,   2,  2,   1,   1,  1,  1,   2,  2,   1,  1,  2,  1,   2,   2,  2,   1,   2,   2,   1,   2,   1,  1,   2,   2,   1,   2,   1 };

    public static int[] breaklocations_post = { 74, 63, 125,   188,    115, 178, 52,  199, 125, 52, 63,  136,    178,    136, 188,115 ,   74,  199 };
    public static int[] dir_post = {            1,  1,   1,     1,      2,   2,   2,   2,   2,   1,  2,   1,       1,     2,   2,  1,      2,   1 };
    // 50 total trials
    // pseudo-randomized to have 30% error trials, that 30 % evenly distributed with all directions

    // errvalmeanings
    // 0 = noerr
    // 1 = 45 degree err
    // -1 = -45 degree err
    // 2 = 90 degree err
    // -2 = -90 degree err
    // 3 = 180 degree err

    //public static int[] errlist;
    public static int curtar;
    public static int curerr;
    public static int curindex = 0;
    public static int indee;
    public static bool restart;
    public static int breakloc;
    public static int direc;
    public static int block;
    //public static int[] myValues;
    public static int[] ranval;
    public static int ran1;
    public static int ran2;
    public static int ran3;

    internal static Boolean socketReadys = false;
    public static TcpClient StartSocket;
    public static NetworkStream StartStream;
    static StreamWriter StartWriter;
    static StreamReader StartReader;
    //String Host = "localhost";
    //public static String Host = "155.246.198.13";
    public static String Host;// = "155.246.83.62";
    public static Int32 StartPort = 55003;

    public static TcpClient EndSocket;
    public static NetworkStream EndStream;
    static StreamWriter EndWriter;
    static StreamReader EndReader;
    public static Int32 EndPort = 55004;

    public static (NetworkStream, NetworkStream) initports()
    {
        // get IP address from different script
        Host = GameObject.Find("Client - OptiTrack").GetComponent<OptitrackStreamingClient>().ServerAddress;

        if (restart)
        {
        }
        else
        {
            // set up tcpIP connections for triggering EEG, EMG, EDA -- only first time script is run
            StartSocket = new TcpClient(Host, StartPort);
            StartStream = StartSocket.GetStream();
            EndSocket = new TcpClient(Host, EndPort);
            EndStream = EndSocket.GetStream();
        }


        return (StartStream, EndStream);
    }
    // Select the three locations of the coins to be presented from the preset list of 9 possibilities (possiblelocations) making sure they're at least 15 game objects apart
    public static int[] Shuffle(){
    System.Random rand = new System.Random();
    var shuffled = possiblelocations.OrderBy(x => rand.Next()).ToArray();

    // Try to select 3 numbers with the minimum difference of 15
    int[] selectedNumbers = new int[3];
    int count = 0;

        for (int i = 0; i<shuffled.Length; i++)
        {
            if (count == 0)
            {
                selectedNumbers[count++] = shuffled[i]; // Pick the first number
            }
            else if (count == 1 && Math.Abs(shuffled[i] - selectedNumbers[0]) >= 15)
            {
                selectedNumbers[count++] = shuffled[i]; // Pick the second number
            }
            else if (count == 2 && Math.Abs(shuffled[i] - selectedNumbers[0]) >= 15 && Math.Abs(shuffled[i] - selectedNumbers[1]) >= 15)
            {
                selectedNumbers[count++] = shuffled[i]; // Pick the third number
            }

            // If we have selected 3 numbers, we can stop
            if (count == 3)
            {
                break;
            }
        }

        // Return the selected numbers
        return selectedNumbers;
    }



   
        public static (int, bool, int, int, int,int,int) Set(int next)
    {
        //Scene scene = SceneManager.GetActiveScene();
        Debug.Log("static");
        block = GameObject.Find("SaveData").GetComponent<InputParameters>().block;
            curindex = curindex + next;
            restart = true;
            breakloc = breaklocations_training[curindex];
            direc = dir_training[curindex];

            var rrvv = Shuffle();
            Debug.Log("rv: " + rrvv[0]);

        int[] ranval = new int[3];
        ranval[0] = rrvv[0];


        //while (rrvv[])
        int j = 1;
        for (int k = 1;k < 3; k++)
        {
            if(k == 1)
            {
                if (Math.Abs(rrvv[j] - ranval[k - 1]) < 12)
                {
                    ranval[k] = rrvv[j + 1];
                    j = j + 2;
                }
                else
                {
                    ranval[k] = rrvv[j];
                    j++;
                }
            }
            else if(k == 2)
            {
                if (Math.Abs(rrvv[k] - ranval[k - 1]) < 12 | Math.Abs(rrvv[k] - ranval[k - 2]) < 12)
                {
                    ranval[k] = rrvv[j + 1];
                    j = j + 2;
                }
                else
                {
                    ranval[k] = rrvv[j];
                    j++;
                }
            }
            
        }
            //ranval[0] = rrvv[0];
 
            //ranval[1] = rrvv[1];
            //ranval[2] = rrvv[2];
        //Debug.Log("ranval1: " + ranval[0]);
        //Debug.Log("ranval2: " + ranval[1]);
        //Debug.Log("ranval3: " + ranval[2]);
        ran1 = ranval[0];
        ran2 = ranval[1];
        ran3 = ranval[2];
        //}
        //else
        //{
        //    breakloc = breaklocations_post[curindex];
        //    direc = dir_post[curindex];
        //}
            Debug.Log("breakloc: " + breakloc);
        Debug.Log("curindex: " + curindex);
        Debug.Log("ranval: " + ranval);

            return (curindex, restart,breakloc,direc,ran1, ran2, ran3);
        
        //}
        //else
        //{
        //    return (0, 0);
        //}

    }
}
