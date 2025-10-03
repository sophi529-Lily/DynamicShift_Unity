using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidScript7 : MonoBehaviour
{
    //[HideInInspector]
    public Vector3[] Sinusoid;
    //[HideInInspector]
    public float[] zTarget;
    //[HideInInspector]
    public float[] yTarget;
    //[HideInInspector]
    public Mesh Sphere;
    //[HideInInspector]
    public Material greyLine;
    //[HideInInspector]
    public Material transparent;
    private int trial;
    [HideInInspector]
    public int breakpoint;
    public int[] rval;
    //[TextArea]
    //[Tooltip("1 = up, 2 = down")]
    [HideInInspector]
    public int dir; // 1 = up, 2 = down
                    //[TextArea]
                    //[Tooltip("1 = control, 2 = experimental")]
    [HideInInspector]
    public int cnd; // 1 = control, 2 == experimental
    //[HideInInspector]
    public GameObject runwayend;
    //[HideInInspector]
    public GameObject parms;
    [HideInInspector]
    public int block;
    //public SphereCollider spcollider;
    // Start is called before the first frame update
    void Start()
    {
        Sinusoid = new Vector3[158];
        zTarget = new float[158];
        yTarget = new float[158];
        //cnd = 2;
        //dir = 2;
        Vector3 p = runwayend.transform.position;

        // read in parameters from InputParameters 
        cnd = parms.GetComponent<InputParameters>().condition;
        block = parms.GetComponent<InputParameters>().block;

        // sinusoid is made up of 252 small spheres, this method creates them and places them in the correct location
        MakeObjects();
    }

    

    void MakeObjects()
    {

        int i = 0;
        while (i < 252)
        {
            //calculate position of ith object based on sin function
            Vector3 pos = new Vector3(0, Mathf.Sin(i * 0.05f) * .1f, i * 0.001992032f);


            // create object
            GameObject go1 = new GameObject();
            // name object
            go1.name = "go" + i;
            // add shape to object
            go1.AddComponent<MeshFilter>().mesh = Sphere;
            // add material/color to object
            go1.AddComponent<MeshRenderer>().material = greyLine;
            // add collider (for object interactions) to object
            SphereCollider sc  = go1.AddComponent(typeof(SphereCollider)) as SphereCollider;
            // specify layer of object (for future identification)
            go1.layer = 5;
            // arrange xyz coordinates accurately
            Vector3 rotatepos = new Vector3(pos.z, pos.y, pos.x);
            go1.transform.position = rotatepos;
            // set size of object
            go1.transform.localScale = new Vector3(.005f, .005f, .005f);

            i++;
            
        }
    }
     
   
}
