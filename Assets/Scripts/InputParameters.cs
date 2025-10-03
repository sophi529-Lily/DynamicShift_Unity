using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputParameters : MonoBehaviour
{
    [Tooltip("1 = proactive, 2 = reactive, 3 = baseline, 4 = proactive + audio, 5 = reactive + audio")]
    public int condition;
    //[Tooltip("1 = up, 2 = down")]
    //public int direction;
    [Tooltip("cm")]
    public int armlength;
    [Tooltip("1 = pre, 2 = train, 3 = post")]
    public int block;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
