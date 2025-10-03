using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class RestartScene6 : MonoBehaviour
{
//    private Rigidbody body;
//    public GameObject sphere;
//    private float collisionTime;
//    private Vector3 zeroVelocity;
//    private float delay = 2f;
//    private float timer;
//    private bool hasCollided = false;
//    private int tap = 1;
//    public GameObject cornertrig;


private void Start()
{
    //body = GetComponent<Rigidbody>();
    //zeroVelocity = Vector3.zero;
}



private void Update()
{

    if (Input.GetKey("r"))
    {
        Restart(1);
            Debug.Log("restart");
    }
    else if(Input.GetKey("m"))
    {
            Restart(0);
    }

}

    public void Restart(int next)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        StaticValsReach7.Set(next);
    }
}
