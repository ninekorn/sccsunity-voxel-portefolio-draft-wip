//using SCCoreSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.XR;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;
using System.Diagnostics;

using Debug = UnityEngine.Debug;

public class sccsplayerthree : MonoBehaviour
{
    public float indexofmaxvalueofperfacegravitydot = 0.0f;
    public float indexofmaxvalueofperfacegravitynextdot = 0.0f;
    public int indexofmaxvalueofperfacegravitynext = -1;
    public int indexofmaxvalueofperfacegravitylast = -1;
    public int indexofmaxvalueofperfacegravity = -1;

    int lastfacegravityindex = -1;

    public float rotationspeed = 1.5f;
    public float movementspeed = 3.5f;

    float isgroundedmaxdist = 0.70f;
    float hiptofloordist = 1.0f;

    public GameObject originalcamerapivot;
    public GameObject upperbodypivot;
    public GameObject headpivotpoint;
    public GameObject isgroundedpivotpoint;
    public GameObject pointertarget;

    public LayerMask layerMask;

    Quaternion finalrotationplayerpivot;

    public GameObject gravityplayerpointertarget;

    public GameObject gravityplayerpointertargetforearm;
    public GameObject gravityplayerpointertargetupperarm;
    public GameObject gravityplayerpointertargetshoulder;
    public GameObject gravityplayerpointertargethand;

    float RotationPlayerPivotX = 0;
    float RotationPlayerPivotY = 0;
    float RotationPlayerPivotZ = 0;

    float RotationkeyboardX = 0;
    float RotationkeyboardY = 0;
    float RotationkeyboardZ = 0;

    float RotationX = 0;
    float RotationY = 0;
    float RotationZ = 0;

    float upperbodypivotRotationX = 0;
    float upperbodypivotRotationY = 0;
    float upperbodypivotRotationZ = 0;

    float MouseRotationX = 0;
    float MouseRotationY = 0;
    float MouseRotationZ = 0;

    float MouseOriginalRotationX = 0;
    float MouseOriginalRotationY = 0;
    float MouseOriginalRotationZ = 0;

    public GameObject camera;

    Vector3 MOVEPOSOFFSET = Vector3.zero;
    public GameObject viewer;
    Vector3 viewerPosition;

    public float speedlerprotation = 0.9995f;

    int currentChunkCoordX;
    int currentChunkCoordY;
    int currentChunkCoordZ;
    int worldChunkWidth = 81;
    int regionChunkWidth = 27;
    int areaChunkWidth = 9;
    int bigChunkWidth = 3;
    int smallChunkWidth = 1;

    Vector3 currentPosition;
    bool isMoving = false;
    Vector3 lastframeviewerpos = Vector3.zero;
    int swtcactivatemouselook = 0;
    Quaternion beforemouselookrot = Quaternion.identity;
    float mouserotatespeed = 100.5f;
    float oricursorx = 0;
    float oricursory = 0;

    int canmovecamera = 1;

    Quaternion originalcamerarot;

    public int isgrounded;

    private Camera cam;
    sccsikplayer ikplayercurrent;

    Vector3 handtargetpositiongravity;
    Vector3 upperarmpositiongravity;


    float hiptofloordistmul = 1.0f; //0.65f

    // Start is called before the first frame update
    void Start()
    {
        /*Quaternion q = viewer.transform.rotation;

        float x = q.x;
        float y = q.y;
        float z = q.z;
        float w = q.w;

        //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
        float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
        float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
        float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


        RotationX = scmaths.RadianToDegree(pitchcurrent);
        RotationY = scmaths.RadianToDegree(yawcurrent);
        RotationZ = scmaths.RadianToDegree(rollcurrent);
        */






        /*
        gravityplayerpointertargetshoulder = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gravityplayerpointertargetshoulder.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        gravityplayerpointertargetshoulder.GetComponent<MeshRenderer>().material.color = Color.green;

        gravityplayerpointertargetupperarm = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gravityplayerpointertargetupperarm.transform.localScale = new Vector3(0.1f, 0.1f, 0.55f);
        gravityplayerpointertargetupperarm.GetComponent<MeshRenderer>().material.color = Color.green;

        gravityplayerpointertargetforearm = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gravityplayerpointertargetforearm.transform.localScale = new Vector3(0.1f, 0.1f, 0.45f);
        gravityplayerpointertargetforearm.GetComponent<MeshRenderer>().material.color = Color.green;

        gravityplayerpointertargethand = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gravityplayerpointertargethand.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        gravityplayerpointertargethand.GetComponent<MeshRenderer>().material.color = Color.green;
        */


        gravityplayerpointertarget = GameObject.CreatePrimitive(PrimitiveType.Cube);
        gravityplayerpointertarget.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        gravityplayerpointertarget.GetComponent<MeshRenderer>().material.color = Color.green;

      
      

  

        /*
        GameObject gravityplayerpointertargetforearm;
        GameObject gravityplayerpointertargetupperarm;
        GameObject gravityplayerpointertargetshoulder;
        GameObject gravityplayerpointertargethand;*/





        handtargetpositiongravity = viewer.transform.position + viewer.transform.forward;
        upperarmpositiongravity = viewer.transform.position + (viewer.transform.forward * 0.5f);







        ikplayercurrent = sccsikplayer.currentsccsikplayer;

        cam = Camera.main;
        originalcamerarot = camera.transform.rotation;

        MOVEPOSOFFSET = viewer.transform.position;


        Quaternion q = viewer.transform.rotation;

        float x = q.x;
        float y = q.x;
        float z = q.x;
        float w = q.x;

        //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
        float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
        float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
        float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


        //rollcurrent = rollcurrent * Mathf.PI / 180;
        //pitchcurrent = pitchcurrent * Mathf.PI / 180;
        //yawcurrent = yawcurrent * Mathf.PI / 180;

        RotationX = scmaths.RadianToDegree(pitchcurrent);// rotationincrements;
        RotationY = scmaths.RadianToDegree(yawcurrent);// rotationincrements;
        RotationZ = scmaths.RadianToDegree(rollcurrent);// rotationincrements;


        //viewer.transform.rotation = Quaternion.Euler(RotationX, RotationY, RotationZ); //viewer.transform.rotation * 













        upperbodypivotRotationX = scmaths.RadianToDegree(pitchcurrent);// rotationincrements;
        upperbodypivotRotationY = scmaths.RadianToDegree(yawcurrent);// rotationincrements;
        upperbodypivotRotationZ = scmaths.RadianToDegree(rollcurrent);// rotationincrements;

        RotationPlayerPivotX = scmaths.RadianToDegree(pitchcurrent);// rotationincrements;
        RotationPlayerPivotY = scmaths.RadianToDegree(yawcurrent);// rotationincrements;
        RotationPlayerPivotZ = scmaths.RadianToDegree(rollcurrent);// rotationincrements;

        Quaternion cameraq = camera.transform.rotation;

         x = cameraq.x;
         y = cameraq.y;
         z = cameraq.z;
         w = cameraq.w;

        //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
        rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
        pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
        yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


        MouseOriginalRotationX = RotationX + 25;// scmaths.RadianToDegree(pitchcurrent);// rotationincrements;
        MouseOriginalRotationY = RotationY;//scmaths.RadianToDegree(yawcurrent);// rotationincrements;
        MouseOriginalRotationZ = RotationZ;//scmaths.RadianToDegree(rollcurrent);// rotationincrements;



        MouseRotationX = scmaths.RadianToDegree(pitchcurrent);// rotationincrements;
        MouseRotationY = scmaths.RadianToDegree(yawcurrent);// rotationincrements;
        MouseRotationZ = scmaths.RadianToDegree(rollcurrent);// rotationincrements;




        isgroundedmaxdist = (isgroundedpivotpoint.transform.position - pointertarget.transform.position).magnitude * 3.0f;
        //hiptofloordist = (isgroundedpivotpoint.transform.position - pointertarget.transform.position).magnitude;
        //hiptofloordist = (isgroundedpivotpoint.transform.position - viewer.transform.position).magnitude;

        hiptofloordist = sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().totalArmLength + (sccsikplayer.currentsccsikplayer.pelvisrenderer.transform.localScale.y * 0.5f) + (sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().shoulderrenderer.transform.localScale.y * 1.0f) + (sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().handrenderer.transform.localScale.y * 0.5f);




        theplanet = GameObject.FindGameObjectWithTag("terrain");

        timewatch.Restart();
        haschangedsidesactivatewatch.Restart();
    }



    int swtcforchangegravityeulersonceforswap = 0;


    Vector3 cubicgravityvector;

    int swtcfordontchangefacegravity = 0;

    int swtchaschangedsidesactivatewatch = 0;
    Stopwatch haschangedsidesactivatewatch = new Stopwatch();

    int timeswtc = 0;
    Stopwatch timewatch = new Stopwatch();


    int hasactivateddenygravitychangeswtc = 0;

    int hasgotgravitydot = 0;

    Vector3 pointtobeat;

    float lastdot = 0;
    // Update is called once per frame
    void Update()
    {

        if (theplanet != null)
        {
            cubicgravityvector = new Vector3(poscubicgravityvisualx, poscubicgravityvisualy, poscubicgravityvisualz) - theplanet.transform.position;
        }

        //var planetdivright = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy - 1), (int)(mainChunk.mindexposz));




        StartCoroutine(CheckMoving());



        //if (isMoving)
        {
            findgravitygriddots();


        }



        /*
        
        Vector3 currentfaceposcubic1 = Vector3.zero;

        if (indexofmaxvalueofperfacegravity == 0) //t
        {
            currentfaceposcubic1 = Vector3.up;
        }
        else if (indexofmaxvalueofperfacegravity == 1) //l
        {
            currentfaceposcubic1 = -Vector3.right;
        }
        else if (indexofmaxvalueofperfacegravity == 2) //r
        {
            currentfaceposcubic1 = Vector3.right;
        }
        else if (indexofmaxvalueofperfacegravity == 3) //fr
        {
            currentfaceposcubic1 = Vector3.forward;
        }
        else if (indexofmaxvalueofperfacegravity == 4) //ba
        {
            currentfaceposcubic1 = -Vector3.forward;
        }
        else if (indexofmaxvalueofperfacegravity == 5) //bo
        {
            currentfaceposcubic1 = -Vector3.up;
        }
        */

        /*
        if (Mathf.Abs(Mathf.Abs(indexofmaxvalueofperfacegravitydot) - Mathf.Abs(indexofmaxvalueofperfacegravitynextdot)) < 0.15f && indexofmaxvalueofperfacegravitydot > 0.995f)
        {
            indexofmaxvalueofperfacegravity = indexofmaxvalueofperfacegravitylast;

            if (indexofmaxvalueofperfacegravity == 0)
            {
                poscubicgravityvisualx = 0;
                poscubicgravityvisualy = 1;
                poscubicgravityvisualz = 0;
            }
            else if (indexofmaxvalueofperfacegravity == 1)
            {
                poscubicgravityvisualx = -1;
                poscubicgravityvisualy = 0;
                poscubicgravityvisualz = 0;
            }
            else if (indexofmaxvalueofperfacegravity == 2)
            {
                poscubicgravityvisualx = 1;
                poscubicgravityvisualy = 0;
                poscubicgravityvisualz = 0;
            }
            else if (indexofmaxvalueofperfacegravity == 3)
            {
                poscubicgravityvisualx = 0;
                poscubicgravityvisualy = 0;
                poscubicgravityvisualz = 1;
            }
            else if (indexofmaxvalueofperfacegravity == 4)
            {
                poscubicgravityvisualx = 0;
                poscubicgravityvisualy = 0;
                poscubicgravityvisualz = -1;
            }
            else if (indexofmaxvalueofperfacegravity == 5)
            {
                poscubicgravityvisualx = 0;
                poscubicgravityvisualy = -1;
                poscubicgravityvisualz = 0;
            }
        }*/


        //if (indexofmaxvalueofperfacegravity == 0 && indexofmaxvalueofperfacegravitylast != indexofmaxvalueofperfacegravity) //t











        StartCoroutine(RotatePlayerMouse());
        StartCoroutine(MovePlayerWithKeyboard());

        StartCoroutine(RotatePlayerWithKeyboard());










        Vector3 nextfaceposcubic0 = Vector3.zero;

        if (indexofmaxvalueofperfacegravitynext == 0) //t
        {
            nextfaceposcubic0 = Vector3.up;
        }
        else if (indexofmaxvalueofperfacegravitynext == 1) //l
        {
            nextfaceposcubic0 = -Vector3.right;
        }
        else if (indexofmaxvalueofperfacegravitynext == 2) //r
        {
            nextfaceposcubic0 = Vector3.right;
        }
        else if (indexofmaxvalueofperfacegravitynext == 3) //fr
        {
            nextfaceposcubic0 = Vector3.forward;
        }
        else if (indexofmaxvalueofperfacegravitynext == 4) //ba
        {
            nextfaceposcubic0 = -Vector3.forward;
        }
        else if (indexofmaxvalueofperfacegravitynext == 5) //bo
        {
            nextfaceposcubic0 = -Vector3.up;
        }

        Vector3 currentfaceposcubic1 = Vector3.zero;

        if (indexofmaxvalueofperfacegravity == 0) //t
        {
            currentfaceposcubic1 = Vector3.up;
        }
        else if (indexofmaxvalueofperfacegravity == 1) //l
        {
            currentfaceposcubic1 = -Vector3.right;
        }
        else if (indexofmaxvalueofperfacegravity == 2) //r
        {
            currentfaceposcubic1 = Vector3.right;
        }
        else if (indexofmaxvalueofperfacegravity == 3) //fr
        {
            currentfaceposcubic1 = Vector3.forward;
        }
        else if (indexofmaxvalueofperfacegravity == 4) //ba
        {
            currentfaceposcubic1 = -Vector3.forward;
        }
        else if (indexofmaxvalueofperfacegravity == 5) //bo
        {
            currentfaceposcubic1 = -Vector3.up;
        }



        Vector3 forwardplayer = lastposition - currentPosition;
        forwardplayer.Normalize();


        Vector3 theplanettopointgravityUP = new Vector3(poscubicgravityvisualx, poscubicgravityvisualy, poscubicgravityvisualz); // - theplanet.transform.position
        theplanettopointgravityUP.Normalize();




        float mingravitydotdiff = 0.01f;

        float indexofmaxvalueofperfacegravitydotinvert = indexofmaxvalueofperfacegravitydot;

        if (indexofmaxvalueofperfacegravitydotinvert < 0)
        {
            indexofmaxvalueofperfacegravitydotinvert *= -1;
            indexofmaxvalueofperfacegravitydotinvert = indexofmaxvalueofperfacegravitydotinvert + 1.0f;
        }

        float indexofmaxvalueofperfacegravitynextdotinvert = indexofmaxvalueofperfacegravitynextdot;

        if (indexofmaxvalueofperfacegravitynextdotinvert < 0)
        {
            indexofmaxvalueofperfacegravitynextdotinvert *= -1;
            indexofmaxvalueofperfacegravitynextdotinvert = indexofmaxvalueofperfacegravitynextdotinvert + 1.0f;
        }






        Vector3 playertoplanet1 = viewer.transform.position - theplanet.transform.position;
        float magofplayertoplanet1 = playertoplanet1.magnitude;

        Vector3 pointtofaceofgravity1 = theplanet.transform.position + (currentfaceposcubic1 * magofplayertoplanet1);

        Vector3 dirplayertomidpointofgravity1 = pointtofaceofgravity1 - viewer.transform.position;
        dirplayertomidpointofgravity1.Normalize();

        //float thedotplayertogravityside1 = Vector3.Dot(viewer.transform.forward, dirplayertomidpointofgravity1);
        float thedotplayertogravityside1 = Vector3.Dot(forwardplayer, dirplayertomidpointofgravity1);





        Vector3 playertoplanet0 = viewer.transform.position - theplanet.transform.position;
        float magofplayertoplanet0 = playertoplanet0.magnitude;

        Vector3 pointtofaceofgravity0 = theplanet.transform.position + (nextfaceposcubic0 * magofplayertoplanet0);

        Vector3 dirplayertomidpointofgravity0 = pointtofaceofgravity0 - viewer.transform.position;
        dirplayertomidpointofgravity0.Normalize();

        //float thedotplayertogravityside0 = Vector3.Dot(viewer.transform.forward, dirplayertomidpointofgravity0);
        float thedotplayertogravityside0 = Vector3.Dot(forwardplayer, dirplayertomidpointofgravity0);

        Debug.Log("/currentdot:" + indexofmaxvalueofperfacegravitydot + "/dotnext:" + thedotplayertogravityside0 + "/dottocurrentpointonface:" + thedotplayertogravityside1);

        if (thedotplayertogravityside0 > 0 && indexofmaxvalueofperfacegravitydot < 0)
        {

            Debug.Log("here0");
            //timeswtc = 1;

        }
        else
        {


            /*if (!isMoving)
            {
                indexofmaxvalueofperfacegravity = indexofmaxvalueofperfacegravitylast;

                if (indexofmaxvalueofperfacegravity == 0)
                {
                    poscubicgravityvisualx = 0;
                    poscubicgravityvisualy = 1;
                    poscubicgravityvisualz = 0;
                }
                else if (indexofmaxvalueofperfacegravity == 1)
                {
                    poscubicgravityvisualx = -1;
                    poscubicgravityvisualy = 0;
                    poscubicgravityvisualz = 0;
                }
                else if (indexofmaxvalueofperfacegravity == 2)
                {
                    poscubicgravityvisualx = 1;
                    poscubicgravityvisualy = 0;
                    poscubicgravityvisualz = 0;
                }
                else if (indexofmaxvalueofperfacegravity == 3)
                {
                    poscubicgravityvisualx = 0;
                    poscubicgravityvisualy = 0;
                    poscubicgravityvisualz = 1;
                }
                else if (indexofmaxvalueofperfacegravity == 4)
                {
                    poscubicgravityvisualx = 0;
                    poscubicgravityvisualy = 0;
                    poscubicgravityvisualz = -1;
                }
                else if (indexofmaxvalueofperfacegravity == 5)
                {
                    poscubicgravityvisualx = 0;
                    poscubicgravityvisualy = -1;
                    poscubicgravityvisualz = 0;
                }
            }*/






















            if (indexofmaxvalueofperfacegravity != indexofmaxvalueofperfacegravitylast)
            {

                if (thedotplayertogravityside0 >= 0 && indexofmaxvalueofperfacegravitydot >= 0)
                {
                    Debug.Log("here0");

                    float invertgravitydot = indexofmaxvalueofperfacegravitydot;

                    if (invertgravitydot < 0 )
                    {
                        invertgravitydot *= -1;
                        invertgravitydot = invertgravitydot + 1.0f;
                    }

                    float invertgravitydotnext = indexofmaxvalueofperfacegravitynextdot;

                    if (invertgravitydotnext < 0)
                    {
                        invertgravitydotnext *= -1;
                        invertgravitydotnext = invertgravitydotnext + 1.0f;
                    }

                    float theresultabs = Mathf.Abs(invertgravitydot - invertgravitydotnext);

                    Debug.Log("/theresultabs:" + theresultabs);

                    if (theresultabs < 0.05f)
                    {

                        if (thedotplayertogravityside1 < 0)
                        {

                            if (indexofmaxvalueofperfacegravitylast == indexofmaxvalueofperfacegravitynext)
                            {
                                Debug.Log("/here0");

                               /* indexofmaxvalueofperfacegravity = indexofmaxvalueofperfacegravitylast;

                                if (indexofmaxvalueofperfacegravity == 0)
                                {
                                    poscubicgravityvisualx = 0;
                                    poscubicgravityvisualy = 1;
                                    poscubicgravityvisualz = 0;
                                }
                                else if (indexofmaxvalueofperfacegravity == 1)
                                {
                                    poscubicgravityvisualx = -1;
                                    poscubicgravityvisualy = 0;
                                    poscubicgravityvisualz = 0;
                                }
                                else if (indexofmaxvalueofperfacegravity == 2)
                                {
                                    poscubicgravityvisualx = 1;
                                    poscubicgravityvisualy = 0;
                                    poscubicgravityvisualz = 0;
                                }
                                else if (indexofmaxvalueofperfacegravity == 3)
                                {
                                    poscubicgravityvisualx = 0;
                                    poscubicgravityvisualy = 0;
                                    poscubicgravityvisualz = 1;
                                }
                                else if (indexofmaxvalueofperfacegravity == 4)
                                {
                                    poscubicgravityvisualx = 0;
                                    poscubicgravityvisualy = 0;
                                    poscubicgravityvisualz = -1;
                                }
                                else if (indexofmaxvalueofperfacegravity == 5)
                                {
                                    poscubicgravityvisualx = 0;
                                    poscubicgravityvisualy = -1;
                                    poscubicgravityvisualz = 0;
                                }*/
                            }
                            else
                            {
                                Debug.Log("/here1");

                            }

                            //thedotplayertogravityside1

                            /**/
                        }
                        else
                        {
                            if (indexofmaxvalueofperfacegravitylast == indexofmaxvalueofperfacegravitynext)
                            {
                                Debug.Log("/here0");

                                /*indexofmaxvalueofperfacegravity = indexofmaxvalueofperfacegravitylast;

                                if (indexofmaxvalueofperfacegravity == 0)
                                {
                                    poscubicgravityvisualx = 0;
                                    poscubicgravityvisualy = 1;
                                    poscubicgravityvisualz = 0;
                                }
                                else if (indexofmaxvalueofperfacegravity == 1)
                                {
                                    poscubicgravityvisualx = -1;
                                    poscubicgravityvisualy = 0;
                                    poscubicgravityvisualz = 0;
                                }
                                else if (indexofmaxvalueofperfacegravity == 2)
                                {
                                    poscubicgravityvisualx = 1;
                                    poscubicgravityvisualy = 0;
                                    poscubicgravityvisualz = 0;
                                }
                                else if (indexofmaxvalueofperfacegravity == 3)
                                {
                                    poscubicgravityvisualx = 0;
                                    poscubicgravityvisualy = 0;
                                    poscubicgravityvisualz = 1;
                                }
                                else if (indexofmaxvalueofperfacegravity == 4)
                                {
                                    poscubicgravityvisualx = 0;
                                    poscubicgravityvisualy = 0;
                                    poscubicgravityvisualz = -1;
                                }
                                else if (indexofmaxvalueofperfacegravity == 5)
                                {
                                    poscubicgravityvisualx = 0;
                                    poscubicgravityvisualy = -1;
                                    poscubicgravityvisualz = 0;
                                }*/
                            }
                            else
                            {
                                Debug.Log("/here1");

                            }
                        }
                        
                    }

                }
                else if (thedotplayertogravityside0 < 0 && indexofmaxvalueofperfacegravitydot >= 0)
                {
                    Debug.Log("here1");
                }
                else if (thedotplayertogravityside0 < 0 && indexofmaxvalueofperfacegravitydot < 0)
                {
                    Debug.Log("here2");
                }
                else if (thedotplayertogravityside0 >= 0 && indexofmaxvalueofperfacegravitydot < 0)
                {
                    Debug.Log("here3");
                }







                /*if (thedotplayertogravityside1 < 0)
                {
                    if (Mathf.Abs(indexofmaxvalueofperfacegravitydotinvert) - (indexofmaxvalueofperfacegravitynextdotinvert) < 0.75f)
                    {
                        Debug.Log("here11");
                        indexofmaxvalueofperfacegravity = indexofmaxvalueofperfacegravitylast;

                        if (indexofmaxvalueofperfacegravity == 0)
                        {
                            poscubicgravityvisualx = 0;
                            poscubicgravityvisualy = 1;
                            poscubicgravityvisualz = 0;
                        }
                        else if (indexofmaxvalueofperfacegravity == 1)
                        {
                            poscubicgravityvisualx = -1;
                            poscubicgravityvisualy = 0;
                            poscubicgravityvisualz = 0;
                        }
                        else if (indexofmaxvalueofperfacegravity == 2)
                        {
                            poscubicgravityvisualx = 1;
                            poscubicgravityvisualy = 0;
                            poscubicgravityvisualz = 0;
                        }
                        else if (indexofmaxvalueofperfacegravity == 3)
                        {
                            poscubicgravityvisualx = 0;
                            poscubicgravityvisualy = 0;
                            poscubicgravityvisualz = 1;
                        }
                        else if (indexofmaxvalueofperfacegravity == 4)
                        {
                            poscubicgravityvisualx = 0;
                            poscubicgravityvisualy = 0;
                            poscubicgravityvisualz = -1;
                        }
                        else if (indexofmaxvalueofperfacegravity == 5)
                        {
                            poscubicgravityvisualx = 0;
                            poscubicgravityvisualy = -1;
                            poscubicgravityvisualz = 0;
                        }
                    }
                    //indexofmaxvalueofperfacegravity = indexofmaxvalueofperfacegravitylast;


                }
                else
                {

                }*/
            }















            if (thedotplayertogravityside0 >= 0 && indexofmaxvalueofperfacegravitydot >= 0)
            {
                //Debug.Log("here11");

                if (thedotplayertogravityside1 < 0)
                {
                    if (Mathf.Abs(indexofmaxvalueofperfacegravitydotinvert) - (indexofmaxvalueofperfacegravitynextdotinvert) > 0.15f)
                    {

                        /*if (indexofmaxvalueofperfacegravity == indexofmaxvalueofperfacegravitylast)
                        {


                            Debug.Log("here11");
                            indexofmaxvalueofperfacegravity = indexofmaxvalueofperfacegravitylast;

                            if (indexofmaxvalueofperfacegravity == 0)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = 1;
                                poscubicgravityvisualz = 0;
                            }
                            else if (indexofmaxvalueofperfacegravity == 1)
                            {
                                poscubicgravityvisualx = -1;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = 0;
                            }
                            else if (indexofmaxvalueofperfacegravity == 2)
                            {
                                poscubicgravityvisualx = 1;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = 0;
                            }
                            else if (indexofmaxvalueofperfacegravity == 3)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = 1;
                            }
                            else if (indexofmaxvalueofperfacegravity == 4)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = -1;
                            }
                            else if (indexofmaxvalueofperfacegravity == 5)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = -1;
                                poscubicgravityvisualz = 0;
                            }
                        }*/
                    }
                }
                else
                {
                    
                    if (indexofmaxvalueofperfacegravity != indexofmaxvalueofperfacegravitylast)
                    {
                        if (Mathf.Abs(indexofmaxvalueofperfacegravitydotinvert) - (indexofmaxvalueofperfacegravitynextdotinvert) > 0.15f)
                        {
                            Debug.Log("here12");

                            /*indexofmaxvalueofperfacegravity = indexofmaxvalueofperfacegravitylast;

                            if (indexofmaxvalueofperfacegravity == 0)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = 1;
                                poscubicgravityvisualz = 0;
                            }
                            else if (indexofmaxvalueofperfacegravity == 1)
                            {
                                poscubicgravityvisualx = -1;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = 0;
                            }
                            else if (indexofmaxvalueofperfacegravity == 2)
                            {
                                poscubicgravityvisualx = 1;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = 0;
                            }
                            else if (indexofmaxvalueofperfacegravity == 3)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = 1;
                            }
                            else if (indexofmaxvalueofperfacegravity == 4)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = -1;
                            }
                            else if (indexofmaxvalueofperfacegravity == 5)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = -1;
                                poscubicgravityvisualz = 0;
                            }*/
                        }
                        else
                        {
                            Debug.Log("here13");

                            /*indexofmaxvalueofperfacegravity = indexofmaxvalueofperfacegravitylast;

                            if (indexofmaxvalueofperfacegravity == 0)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = 1;
                                poscubicgravityvisualz = 0;
                            }
                            else if (indexofmaxvalueofperfacegravity == 1)
                            {
                                poscubicgravityvisualx = -1;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = 0;
                            }
                            else if (indexofmaxvalueofperfacegravity == 2)
                            {
                                poscubicgravityvisualx = 1;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = 0;
                            }
                            else if (indexofmaxvalueofperfacegravity == 3)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = 1;
                            }
                            else if (indexofmaxvalueofperfacegravity == 4)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = -1;
                            }
                            else if (indexofmaxvalueofperfacegravity == 5)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = -1;
                                poscubicgravityvisualz = 0;
                            }*/

                        }
                    }
                }
                

                /*if (indexofmaxvalueofperfacegravitydot > 0.5f)
                {


                    if (thedotplayertogravityside1 > 0.5f)
                    {

                        if (Mathf.Abs(indexofmaxvalueofperfacegravitydotinvert) - (indexofmaxvalueofperfacegravitynextdotinvert) < 0.5f)
                        //if (Mathf.Abs(Mathf.Abs(indexofmaxvalueofperfacegravitynextdot) - Mathf.Abs(indexofmaxvalueofperfacegravitydot)) < 0.25f)
                        {
                            indexofmaxvalueofperfacegravity = indexofmaxvalueofperfacegravitylast;

                            if (indexofmaxvalueofperfacegravity == 0)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = 1;
                                poscubicgravityvisualz = 0;
                            }
                            else if (indexofmaxvalueofperfacegravity == 1)
                            {
                                poscubicgravityvisualx = -1;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = 0;
                            }
                            else if (indexofmaxvalueofperfacegravity == 2)
                            {
                                poscubicgravityvisualx = 1;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = 0;
                            }
                            else if (indexofmaxvalueofperfacegravity == 3)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = 1;
                            }
                            else if (indexofmaxvalueofperfacegravity == 4)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = 0;
                                poscubicgravityvisualz = -1;
                            }
                            else if (indexofmaxvalueofperfacegravity == 5)
                            {
                                poscubicgravityvisualx = 0;
                                poscubicgravityvisualy = -1;
                                poscubicgravityvisualz = 0;
                            }
                        }
                    }
                    /*else
                    {

                    }

                }
                else
                {

                }*/
            }
            else if (thedotplayertogravityside0 < 0 && indexofmaxvalueofperfacegravitydot < 0)
            {
                Debug.Log("here2");
            }
            else if (thedotplayertogravityside0 < 0 && indexofmaxvalueofperfacegravitydot >= 0)
            {


                if (indexofmaxvalueofperfacegravitydot > 0.995f)
                {
                    Debug.Log("here22");

                }
                else if (indexofmaxvalueofperfacegravitydot >= 0.75f && indexofmaxvalueofperfacegravitydot <= 0.995f)
                {
                    Debug.Log("here23");
                    if (Mathf.Abs(indexofmaxvalueofperfacegravitydotinvert) - (indexofmaxvalueofperfacegravitynextdotinvert) < 0.15f)
                    //if (Mathf.Abs(Mathf.Abs(indexofmaxvalueofperfacegravitynextdot) - Mathf.Abs(indexofmaxvalueofperfacegravitydot)) < 0.25f)
                    {
                        /*indexofmaxvalueofperfacegravity = indexofmaxvalueofperfacegravitylast;

                        if (indexofmaxvalueofperfacegravity == 0)
                        {
                            poscubicgravityvisualx = 0;
                            poscubicgravityvisualy = 1;
                            poscubicgravityvisualz = 0;
                        }
                        else if (indexofmaxvalueofperfacegravity == 1)
                        {
                            poscubicgravityvisualx = -1;
                            poscubicgravityvisualy = 0;
                            poscubicgravityvisualz = 0;
                        }
                        else if (indexofmaxvalueofperfacegravity == 2)
                        {
                            poscubicgravityvisualx = 1;
                            poscubicgravityvisualy = 0;
                            poscubicgravityvisualz = 0;
                        }
                        else if (indexofmaxvalueofperfacegravity == 3)
                        {
                            poscubicgravityvisualx = 0;
                            poscubicgravityvisualy = 0;
                            poscubicgravityvisualz = 1;
                        }
                        else if (indexofmaxvalueofperfacegravity == 4)
                        {
                            poscubicgravityvisualx = 0;
                            poscubicgravityvisualy = 0;
                            poscubicgravityvisualz = -1;
                        }
                        else if (indexofmaxvalueofperfacegravity == 5)
                        {
                            poscubicgravityvisualx = 0;
                            poscubicgravityvisualy = -1;
                            poscubicgravityvisualz = 0;
                        }*/
                    }

                }





                /*
                Debug.Log("here3");
                if (indexofmaxvalueofperfacegravitydot > 0.5f)
                {
                    

                    if (thedotplayertogravityside1 > 0.5f)
                    {

                        
                    }
                    /*else
                    {

                    }

                }
                else
                {

                }*/

            }
            else
            {

            }


            //timeswtc = 1;
        }







        //cubicrotation.instance.setcubicrotation(indexofmaxvalueofperfacegravity, indexofmaxvalueofperfacegravitylast, viewer);
        setcubicrotation(indexofmaxvalueofperfacegravity, indexofmaxvalueofperfacegravitylast, viewer);






        /*
       


        Vector3 playertoplanet = viewer.transform.position - theplanet.transform.position;
        float magofplayertoplanet = playertoplanet.magnitude;

        Vector3 pointtofaceofgravity = theplanet.transform.position + (theplanettopointgravityUP * magofplayertoplanet);

        Vector3 dirplayertomidpointofgravity = pointtofaceofgravity - viewer.transform.position;
        dirplayertomidpointofgravity.Normalize();

        float thedotplayertogravityside = Vector3.Dot(viewer.transform.forward, dirplayertomidpointofgravity);

        //UnityEngine.//Debug.Log("dot:" + thedotplayertogravityside);

        lastdot = thedotplayertogravityside;





        //UnityEngine.////Debug.Log("/cubicx:" + poscubicgravityvisualx + "/cubicy:" + poscubicgravityvisualy + "/cubicz:" + poscubicgravityvisualz);

        var distance = 0.00025f;
        //var dirForward = viewer.transform.rotation * Vector3.forward;
        var dirForward = viewer.transform.rotation * Vector3.forward;
        dirForward.Normalize();

        //var dirofnormal = Quaternion.identity * theouthit.normal;
        Vector3 positioninfrontofplayer = viewer.transform.position + (dirForward * distance);

        Vector3 forwarddirtopointfrontplayer = positioninfrontofplayer - viewer.transform.position;// alwaysuppointofplayercomparedtoplayercore - viewer.transform.position;
        forwarddirtopointfrontplayer.Normalize();

        Vector3 planettoplayer = viewer.transform.position - theplanet.transform.position;
        float planettoplayermag = planettoplayer.magnitude;
        //planettoplayer.Normalize();

        Vector3 dirplanettopointfrontplayer = positioninfrontofplayer - theplanet.transform.position;
        dirplanettopointfrontplayer.Normalize();






        Vector3 radiuscurvedpositionfrontplayer = theplanet.transform.position + (dirplanettopointfrontplayer * planettoplayermag);

        Vector3 forwardplayer = radiuscurvedpositionfrontplayer - viewer.transform.position;
        forwardplayer.Normalize();
        */


        //Debug.Log("facetype:" + indexofmaxvalueofperfacegravity + "/lastfacetype:" + indexofmaxvalueofperfacegravitylast);




        /*if (timeswtc == 1)
        {
            if (timewatch.ElapsedMilliseconds > 500)
            //if (swtcfordontchangefacegravity == 0)
            {
                timewatch.Restart();
                timeswtc = 0;
            }
            //timeswtc = 1;
        }*/
        /*
        Quaternion q0 = viewer.transform.rotation;

        float x0 = q0.x;
        float y0 = q0.y;
        float z0 = q0.z;
        float w0 = q0.w;

        //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
        float rollcurrent0 = Mathf.Atan2(2 * y0 * w0 - 2 * x0 * z0, 1 - 2 * y0 * y0 - 2 * z0 * z0);
        float pitchcurrent0 = Mathf.Atan2(2 * x0 * w0 - 2 * y0 * z0, 1 - 2 * x0 * x0 - 2 * z0 * z0);
        float yawcurrent0 = Mathf.Asin(2 * x0 * y0 + 2 * z0 * w0);


        RotationX = scmaths.RadianToDegree(pitchcurrent0);
        RotationY = scmaths.RadianToDegree(yawcurrent0);
        RotationZ = scmaths.RadianToDegree(rollcurrent0);
        */
        ////Debug.Log("/rotx:" + RotationX + "/roty:" + RotationY + "/rotz:" + RotationZ);








        Vector3 planetgravity = -(viewer.transform.position - theplanet.transform.position);
        planetgravity.Normalize();












        // && Mathf.Abs(Mathf.Abs(indexofmaxvalueofperfacegravitydot) - Mathf.Abs(indexofmaxvalueofperfacegravitynextdot)) < 0.15f



        if (indexofmaxvalueofperfacegravity != indexofmaxvalueofperfacegravitylast && indexofmaxvalueofperfacegravitylast != -1 ) // && indexofmaxvalueofperfacegravitydot > 0.985f
        {


            //if (Mathf.Abs(Mathf.Abs(indexofmaxvalueofperfacegravitydotinvert) - Mathf.Abs(indexofmaxvalueofperfacegravitynextdotinvert)) > 0.5f)
            {

                //if (indexofmaxvalueofperfacegravitydot > 0.985f)
                {
                    //if (timeswtc == 0)
                    {
                        //if (timewatch.ElapsedMilliseconds > 250)
                        {



                            timeswtc = 1;

                            
                            float rotate_speed = 25.0f;

                            ////Debug.Log("***********");
                            Vector3 pointforward = isgroundedpivotpoint.transform.position + (viewer.transform.forward * 0.00125f);

                            float lengthofarm = hiptofloordist * hiptofloordistmul;// sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().totalArmLength + (sccsikplayer.currentsccsikplayer.pelvisrenderer.transform.localScale.y * 0.5f) + (sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().shoulderrenderer.transform.localScale.y * 1.0f) + (sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().handrenderer.transform.localScale.y * 0.5f);

                            //for topface
                            //Vector3 pointforwardnextface = viewer.transform.position + (viewer.transform.forward * lengthofarm) + (-viewer.transform.up * 0.00125f);// + (-viewer.transform.up * 0.1f);
                            Vector3 pointforwardnextfacenooffset = viewer.transform.position + (-viewer.transform.up * 0.00125f);

                            Vector3 pointforwardnextface = viewer.transform.position + (viewer.transform.forward * lengthofarm) + (-viewer.transform.up * 0.00125f);
                            //Vector3 pointforwardnextface = isgroundedpivotpoint.transform.position + (viewer.transform.forward * lengthofarm) + (-viewer.transform.up * 0.00125f);
                            //Vector3 pointforwardnextface = viewer.transform.position + (viewer.transform.forward * 0.15f) + (-viewer.transform.up * 0.15f);




                            /*
                            //Vector3 pointforwardthatway = pointforwardnextface  + (viewer.transform.forward * 0.00125f);

                            Vector3 dirtothatpoint = pointforwardnextface - pointforward;
                            dirtothatpoint.Normalize();

                            Vector3 crossprod = Vector3.Cross(dirtothatpoint, theplanettopointgravityUP);
                            crossprod.Normalize();

                            Vector3 crossofcrossprodforward = -Vector3.Cross(crossprod, theplanettopointgravityUP);
                            crossofcrossprodforward.Normalize();

                            Debug.DrawRay(pointforwardnextface, dirtothatpoint, Color.cyan, 5.0f);

                            Debug.DrawRay(pointforwardnextface, faceposcubic, Color.yellow, 5.0f);
                            Debug.DrawRay(pointforwardnextface, -crossprod, Color.red, 5.0f);
                            Debug.DrawRay(pointforwardnextface, crossofcrossprodforward, Color.blue, 5.0f);
                            Debug.DrawRay(pointforwardnextface, theplanettopointgravityUP, Color.green, 5.0f);

                            //crossofcrossprodforward.z = 1;

                            Quaternion rot = new Quaternion();
                            //rot.SetLookRotation(forwarddirtopointfrontplayer, faceposcubic.normalized);
                            //rot.SetLookRotation(dirForward, faceposcubic.normalized);
                            rot.SetLookRotation(crossofcrossprodforward, theplanettopointgravityUP.normalized);
                            rot.Normalize();
                            //rot.SetLookRotation(forwarddirtopointfrontplayer, -(theplanet.transform.position - viewer.transform.position).normalized);
                            //rot.SetLookRotation(forwardplayer, -(theplanet.transform.position - viewer.transform.position).normalized);
                            //dirForward.z = 0;
                            //viewer.transform.rotation = Quaternion.LookRotation(forwarddirtopointfrontplayer);// Quaternion.Lerp(viewer.transform.rotation, rot, rotate_speed * Time.deltaTime);

                            //for (int i = 0;i < 10;i++)
                            {
                                viewer.transform.rotation = rot;// Quaternion.Lerp(viewer.transform.rotation, rot, 100.0f * Time.deltaTime);
                                
                                //viewer.transform.localRotation = Quaternion.Euler(new Vector3(rot.eulerAngles.x, rot.eulerAngles.y, rot.eulerAngles.z));


                            }
                            */
                            swtchaschangedsidesactivatewatch = 1;



                            /*var diffx = (rot.eulerAngles.x - viewer.transform.rotation.eulerAngles.x);
                            var diffy = (rot.eulerAngles.y - viewer.transform.rotation.eulerAngles.y);
                            var diffz = (rot.eulerAngles.z - viewer.transform.rotation.eulerAngles.z);

                            Vector3 neweulers = new Vector3(diffx,diffy, diffz);


                            viewer.transform.Rotate(neweulers, Space.Self);
                            */



                            /*
                            //MOVEPOSOFFSET = viewer.transform.position;
                            Vector3 playerforward = viewer.transform.rotation * Vector3.forward;
                            Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
                            uppoint.Normalize();

                            //Vector3 pointtobeat = pointforwardnextface + (viewer.transform.forward * 0.025f * 1.0f * 50.0f); // + (-uppoint * 0.1f) //10.95f //* movementspeed

                            pointtobeat = pointforwardnextface + (viewer.transform.forward * 0.00125f * 1.0f * 10.0f); // + (-uppoint * 0.1f) //10.95f //* movementspeed
                            viewer.transform.position = pointtobeat;// Vector3.Lerp(viewer.transform.position, pointtobeat, movementspeed); // * Time.deltaTime //* Time.deltaTime * 100.0f
                            //viewer.transform.position = MOVEPOSOFFSET;
                            */


                            timewatch.Restart();


                            swtcfordontchangefacegravity = 1;
                        }
                        /*else
                        {

                        }*/
                    }
                    /*else
                    {

                    }*/
                }
                /*else
                {
                    //swtcfordontchangefacegravity = 0;
                    float rotate_speed = 25.0f;

                    ////Debug.Log("***********");
                    Vector3 pointforward = isgroundedpivotpoint.transform.position + (viewer.transform.forward * 0.00125f);

                    float lengthofarm = hiptofloordist * hiptofloordistmul;// sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().totalArmLength + (sccsikplayer.currentsccsikplayer.pelvisrenderer.transform.localScale.y * 0.5f) + (sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().shoulderrenderer.transform.localScale.y * 1.0f) + (sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().handrenderer.transform.localScale.y * 0.5f);

                    //for topface
                    Vector3 pointforwardnextface = viewer.transform.position + (viewer.transform.forward * lengthofarm) + (-viewer.transform.up * 0.00125f);
                    Vector3 pointforwardnextfacenooffset = viewer.transform.position + (-viewer.transform.up * 0.00125f);


                    //Vector3 pointforwardthatway = pointforwardnextface  + (viewer.transform.forward * 0.00125f);

                    Vector3 dirtothatpoint = pointforwardnextface - pointforward;
                    dirtothatpoint.Normalize();

                    Vector3 crossprod = Vector3.Cross(dirtothatpoint, theplanettopointgravityUP);
                    crossprod.Normalize();

                    Vector3 crossofcrossprodforward = -Vector3.Cross(crossprod, theplanettopointgravityUP);
                    crossofcrossprodforward.Normalize();
                    /*
                    Debug.DrawRay(pointforwardnextface, dirtothatpoint, Color.cyan, 100.0f);

                    Debug.DrawRay(pointforwardnextface, faceposcubic, Color.yellow, 100.0f);
                    Debug.DrawRay(pointforwardnextface, crossprod, Color.red, 100.0f);
                    Debug.DrawRay(pointforwardnextface, crossofcrossprodforward, Color.blue, 100.0f);
                    Debug.DrawRay(pointforwardnextface, nextfaceposcubic, Color.green, 100.0f);

                    //crossofcrossprodforward.z = 1;
                    

                    Quaternion rot = new Quaternion();
                    //rot.SetLookRotation(forwarddirtopointfrontplayer, faceposcubic.normalized);
                    //rot.SetLookRotation(dirForward, faceposcubic.normalized);
                    rot.SetLookRotation(viewer.transform.forward, theplanettopointgravityUP.normalized);
                    ////rot.Normalize();
                    //rot.SetLookRotation(forwarddirtopointfrontplayer, -(theplanet.transform.position - viewer.transform.position).normalized);
                    //rot.SetLookRotation(forwardplayer, -(theplanet.transform.position - viewer.transform.position).normalized);
                    //dirForward.z = 0;
                    //viewer.transform.rotation = Quaternion.LookRotation(forwarddirtopointfrontplayer);// Quaternion.Lerp(viewer.transform.rotation, rot, rotate_speed * Time.deltaTime);
                    viewer.transform.rotation = rot;// Quaternion.Lerp(viewer.transform.rotation, rot, rotate_speed * Time.deltaTime);


                    timeswtc = 0;
                }*/
            }
            /*else
            {
                indexofmaxvalueofperfacegravity = indexofmaxvalueofperfacegravitylast;

                if (indexofmaxvalueofperfacegravity == 0)
                {
                    poscubicgravityvisualx = 0;
                    poscubicgravityvisualy = 1;
                    poscubicgravityvisualz = 0;
                }
                else if (indexofmaxvalueofperfacegravity == 1)
                {
                    poscubicgravityvisualx = -1;
                    poscubicgravityvisualy = 0;
                    poscubicgravityvisualz = 0;
                }
                else if (indexofmaxvalueofperfacegravity == 2)
                {
                    poscubicgravityvisualx = 1;
                    poscubicgravityvisualy = 0;
                    poscubicgravityvisualz = 0;
                }
                else if (indexofmaxvalueofperfacegravity == 3)
                {
                    poscubicgravityvisualx = 0;
                    poscubicgravityvisualy = 0;
                    poscubicgravityvisualz = 1;
                }
                else if (indexofmaxvalueofperfacegravity == 4)
                {
                    poscubicgravityvisualx = 0;
                    poscubicgravityvisualy = 0;
                    poscubicgravityvisualz = -1;
                }
                else if (indexofmaxvalueofperfacegravity == 5)
                {
                    poscubicgravityvisualx = 0;
                    poscubicgravityvisualy = -1;
                    poscubicgravityvisualz = 0;
                }
            }*/
        }
        else
        {
            /*
            //swtcfordontchangefacegravity = 0;
            float rotate_speed = 25.0f;

            ////Debug.Log("***********");
            Vector3 pointforward = isgroundedpivotpoint.transform.position + (viewer.transform.forward * 0.00125f);

            float lengthofarm = hiptofloordist * hiptofloordistmul;// sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().totalArmLength + (sccsikplayer.currentsccsikplayer.pelvisrenderer.transform.localScale.y * 0.5f) + (sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().shoulderrenderer.transform.localScale.y * 1.0f) + (sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().handrenderer.transform.localScale.y * 0.5f);

            //for topface
            Vector3 pointforwardnextface = viewer.transform.position + (viewer.transform.forward * lengthofarm) + (-viewer.transform.up * 0.00125f);
            Vector3 pointforwardnextfacenooffset = viewer.transform.position + (-viewer.transform.up * 0.00125f);


            //Vector3 pointforwardthatway = pointforwardnextface  + (viewer.transform.forward * 0.00125f);

            Vector3 dirtothatpoint = pointforwardnextface - pointforward;
            dirtothatpoint.Normalize();

            Vector3 crossprod = Vector3.Cross(dirtothatpoint, theplanettopointgravityUP);
            crossprod.Normalize();

            Vector3 crossofcrossprodforward = -Vector3.Cross(crossprod, theplanettopointgravityUP);
            crossofcrossprodforward.Normalize();
            /*
            Debug.DrawRay(pointforwardnextface, dirtothatpoint, Color.cyan, 100.0f);

            Debug.DrawRay(pointforwardnextface, faceposcubic, Color.yellow, 100.0f);
            Debug.DrawRay(pointforwardnextface, crossprod, Color.red, 100.0f);
            Debug.DrawRay(pointforwardnextface, crossofcrossprodforward, Color.blue, 100.0f);
            Debug.DrawRay(pointforwardnextface, nextfaceposcubic0, Color.green, 100.0f);
            */
            //crossofcrossprodforward.z = 1;
            
            /*
            Quaternion rot = new Quaternion();
            //rot.SetLookRotation(forwarddirtopointfrontplayer, faceposcubic.normalized);
            //rot.SetLookRotation(dirForward, faceposcubic.normalized);
            rot.SetLookRotation(viewer.transform.forward, theplanettopointgravityUP.normalized);
            ////rot.Normalize();
            //rot.SetLookRotation(forwarddirtopointfrontplayer, -(theplanet.transform.position - viewer.transform.position).normalized);
            //rot.SetLookRotation(forwardplayer, -(theplanet.transform.position - viewer.transform.position).normalized);
            //dirForward.z = 0;
            //viewer.transform.rotation = Quaternion.LookRotation(forwarddirtopointfrontplayer);// Quaternion.Lerp(viewer.transform.rotation, rot, rotate_speed * Time.deltaTime);
            viewer.transform.rotation = rot;// Quaternion.Lerp(viewer.transform.rotation, rot, rotate_speed * Time.deltaTime);
            */


            timeswtc = 0;

        }


























        int hasusingray = 1;

        if (hasusingray == 1)
        {
            // new Vector3(rayposforinnerchunkx + (remnantsx * planeSize) + offsetposx, rayposforinnerchunky + (remnantsy * planeSize) + offsetposy, rayposforinnerchunkz + (remnantsz * planeSize) + offsetposz);

            Vector3 currentposofleftfoot = sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().shoulder.GetComponent<playerInteractionrev11>().raypositionmovingforwardtargethit;
            Vector3 currentposofrightfoot = sccsikplayer.currentsccsikplayer.arrayofikarms[3].GetComponent<sccsikarm>().shoulder.GetComponent<playerInteractionrev11>().raypositionmovingforwardtargethit;

            Vector3 positionofpivotlegleft = sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().shoulder.GetComponent<playerInteractionrev11>().transform.position;
            Vector3 positionofpivotlegright = sccsikplayer.currentsccsikplayer.arrayofikarms[3].GetComponent<sccsikarm>().shoulder.GetComponent<playerInteractionrev11>().transform.position;

            Vector3 dirpivotleglefttotargetleft = currentposofleftfoot - positionofpivotlegleft;
            float maglegleft = dirpivotleglefttotargetleft.magnitude;
            dirpivotleglefttotargetleft.Normalize();

            Vector3 dirpivotlegrighttotargetright = currentposofrightfoot - positionofpivotlegright;
            float maglegright = dirpivotlegrighttotargetright.magnitude;
            dirpivotlegrighttotargetright.Normalize();

            float diffinmags = Mathf.Abs(maglegright - maglegright);

            float totalarmlength = sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().totalArmLength;// + (sccsikplayer.currentsccsikplayer.pelvisrenderer.transform.localScale.y * 0.5f);

            //if (maglegleft > totalarmlength || maglegright > totalarmlength)
            {
                ////Debug.Log(" > totalarmlength");
                /*if (maglegleft > totalarmlength)
                {

                }
                else if (maglegleft <= totalarmlength)
                {

                }

                if (maglegright > totalarmlength)
                {

                }
                else if (maglegright <= totalarmlength)
                {

                }*/

            }
            //else
            {



                if (swtchaschangedsidesactivatewatch == 1)
                {
                    haschangedsidesactivatewatch.Restart();
                    swtchaschangedsidesactivatewatch = 2;

                }

                if (swtchaschangedsidesactivatewatch == 2)
                {
                    if (haschangedsidesactivatewatch.ElapsedMilliseconds >= 25) // && isMoving //25
                    {

                        swtchaschangedsidesactivatewatch = 0;
                    }
                }




                if (swtchaschangedsidesactivatewatch == 0) // && hasgotgravitydot == 1 //0
                {


                    Vector3 midpointfoot = currentposofleftfoot - currentposofrightfoot;
                    float magmidpointfoot = midpointfoot.magnitude;
                    magmidpointfoot *= 0.5f;
                    midpointfoot.Normalize();

                    Vector3 posofmidpoint = currentposofleftfoot + (midpointfoot * magmidpointfoot);
                    //viewer.transform.position = (posofmidpoint + (viewer.transform.up * (totalarmlength * 0.hiptofloordistmul)));








                    float planeSize = 0.1f;



                    //ADJUST POSITION 
                    float lengthofarm = hiptofloordist * hiptofloordistmul * 1.0f;// sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().totalArmLength + (sccsikplayer.currentsccsikplayer.pelvisrenderer.transform.localScale.y * 0.5f) + (sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().shoulderrenderer.transform.localScale.y * 1.0f) + (sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().handrenderer.transform.localScale.y * 0.5f);

                    //Vector3 pointforwardnextface = isgroundedpivotpoint.transform.position + (viewer.transform.forward * lengthofarm) + (-viewer.transform.up * 0.00125f);
                    //Vector3 pointforwardnextface = isgroundedpivotpoint.transform.position + (viewer.transform.forward * 0.1f) + (-viewer.transform.up * 0.1f);
                    //Vector3 pointforwardnextface = viewer.transform.position + (viewer.transform.forward * 1.75f) + (-viewer.transform.up * 0.15f);
                    Vector3 pointforwardnextface = viewer.transform.position + (viewer.transform.forward * lengthofarm) + (-viewer.transform.up * 0.00125f);


                    //MOVEPOSOFFSET = viewer.transform.position;
                    //Vector3 playerforward = viewer.transform.rotation * Vector3.forward;
                    //Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
                    //uppoint.Normalize();

                    //Vector3 pointtobeat = pointforwardnextface + (viewer.transform.forward * 0.025f * 1.0f * 50.0f); // + (-uppoint * 0.1f) //10.95f //* movementspeed

                    Vector3 pointtobeat = Vector3.zero;// pointforwardnextface + (viewer.transform.forward * 0.0125f * 1.0f * 10.0f); // + (-uppoint * 0.1f) //10.95f //* movementspeed


                    if (timeswtc == 1)
                    {
                        pointtobeat = pointforwardnextface + (viewer.transform.forward * 0.0125f * 1.0f * 25.0f); // + (-uppoint * 0.1f) //10.95f //* movementspeed

                    }
                    else
                    {
                        pointtobeat = isgroundedpivotpoint.transform.position;
                    }




                    Vector3 rayposition = pointtobeat;// isgroundedpivotpoint.transform.position;// sccsikplayer.currentsccsikplayer.pelvisemptygameobject.transform.position;
                    Vector3 raypositionforward = rayposition - new Vector3(0, 0, 0);
                    Vector3 raypositionforwardinit = rayposition - new Vector3(0, 0, 0);

                    //raypositionforward?.x = Mathf.Floor((raypositionforward.x * 10) / 10);
                    //raypositionforward?.y = Mathf.Floor((raypositionforward.y * 10) / 10);
                    //raypositionforward?.z = Mathf.Floor((raypositionforward.z * 10) / 10);


                    Vector3 raydirection = -viewer.transform.up;

                    int swtcdontlookfurther = 0;

                    ////UnityEngine.//Debug.Log("/rayposition:" + rayposition.x + "/rayposition:" + rayposition.y + "/rayposition:" + rayposition.z);

                    int hasfoundblock = 0;

                    //THE RAY LOOP - INCREMENTING THE RAY THE SIZE OF EACH BYTES EVERY FRAMES FROM THE HIP JOINT OF EACH LEGS.
                    for (int y = 0; y < 25; y++)
                    {
                        raypositionforward = raypositionforward + (raydirection * planeSize);

                        //if ((raypositionforwardinit - raypositionforward).magnitude < hiptofloordist * hiptofloordistmul)
                        {
                            int chunksizex = sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwr + 1;
                            int chunksizey = sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhr + 1;
                            int chunksizez = sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdr + 1;

                            int sizex = chunksizex * 10;
                            int sizey = chunksizey * 10;
                            int sizez = chunksizez * 10;

                            int planetdivsizex = sccschunkfacesbuilder.instance.chunkwl + sccschunkfacesbuilder.instance.chunkwr + 1;
                            int planetdivsizey = sccschunkfacesbuilder.instance.chunkhl + sccschunkfacesbuilder.instance.chunkhr + 1;
                            int planetdivsizez = sccschunkfacesbuilder.instance.chunkdl + sccschunkfacesbuilder.instance.chunkdr + 1;

                            //sizex /= planetdivsizex;
                            //sizey /= planetdivsizey;
                            //sizez /= planetdivsizez;

                            int raypositionx = (int)Mathf.Floor(raypositionforward.x / 10.0f) * 10; //((((int)Mathf.Floor(raypositionforward.x * 10) / 10)) / 1);// + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl;// + sccschunkfacesbuilder.instance.chunkwl;// * (sccschunkfacesbuilder.instance.chunkwl + sccschunkfacesbuilder.instance.chunkwr + 1);
                            int raypositiony = (int)Mathf.Floor(raypositionforward.y / 10.0f) * 10; //((((int)Mathf.Floor(raypositionforward.y * 10) / 10)) / 1);// + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhl;// + sccschunkfacesbuilder.instance.chunkhl;// * (sccschunkfacesbuilder.instance.chunkhl + sccschunkfacesbuilder.instance.chunkhr + 1);
                            int raypositionz = (int)Mathf.Floor(raypositionforward.z / 10.0f) * 10; //((((int)Mathf.Floor(raypositionforward.z * 10) / 10)) / 1);// + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdl;// + sccschunkfacesbuilder.instance.chunkdl;// * (sccschunkfacesbuilder.instance.chunkdl + sccschunkfacesbuilder.instance.chunkdr + 1);

                            raypositionx /= 4;
                            raypositiony /= 4;
                            raypositionz /= 4;

                            float rayposoffsetx = sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl;
                            float rayposoffsety = sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhl;
                            float rayposoffsetz = sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdl;

                            raypositionx /= 2;
                            raypositiony /= 2;
                            raypositionz /= 2;

                            int rayposforinnerchunkx = (int)Mathf.Floor(raypositionforward.x);
                            int rayposforinnerchunky = (int)Mathf.Floor(raypositionforward.y);
                            int rayposforinnerchunkz = (int)Mathf.Floor(raypositionforward.z);

                            int rayposforinnerchunkbytesx = (int)(Mathf.Floor(raypositionforward.x * 100.0f) / 10.0f); //((((int)Mathf.Floor(raypositionforward.x * 10) / 10)) / 1);// + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl;// + sccschunkfacesbuilder.instance.chunkwl;// * (sccschunkfacesbuilder.instance.chunkwl + sccschunkfacesbuilder.instance.chunkwr + 1);
                            int rayposforinnerchunkbytesy = (int)(Mathf.Floor(raypositionforward.y * 100.0f) / 10.0f); //((((int)Mathf.Floor(raypositionforward.y * 10) / 10)) / 1);// + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhl;// + sccschunkfacesbuilder.instance.chunkhl;// * (sccschunkfacesbuilder.instance.chunkhl + sccschunkfacesbuilder.instance.chunkhr + 1);
                            int rayposforinnerchunkbytesz = (int)(Mathf.Floor(raypositionforward.z * 100.0f) / 10.0f); //((((int)Mathf.Floor(raypositionforward.z * 10) / 10)) / 1);// + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdl;// + sccschunkfacesbuilder.instance.chunkdl;// * (sccschunkfacesbuilder.instance.chunkdl + sccschunkfacesbuilder.instance.chunkdr + 1);

                            if (raypositionforward.x < 0)
                            {

                            }

                            if (raypositionforward.y < 0)
                            {

                            }

                            if (raypositionforward.z < 0)
                            {

                            }

                            float somevaldiv = 1.0f / ((0.01f));

                            if (swtcdontlookfurther == 0)
                            {
                                var planetdiv = sccschunkfacesbuilder.instance.getplanetdiv(raypositionx, raypositiony, raypositionz);

                                if (planetdiv != null)
                                {
                                    float sometestx = 0;
                                    float sometesty = 0;
                                    float sometestz = 0;

                                    if (raypositionforward.x >= 0)
                                    {
                                        sometestx = rayposforinnerchunkx - rayposoffsetx;
                                    }
                                    else
                                    {
                                        sometestx = rayposforinnerchunkx + rayposoffsetx;
                                    }

                                    if (raypositionforward.y >= 0)
                                    {
                                        sometesty = rayposforinnerchunky - rayposoffsety;
                                    }
                                    else
                                    {
                                        sometesty = rayposforinnerchunky + rayposoffsety;
                                    }

                                    if (raypositionforward.z >= 0)
                                    {
                                        sometestz = rayposforinnerchunkz - rayposoffsetz;
                                    }
                                    else
                                    {
                                        sometestz = rayposforinnerchunkz + rayposoffsetz;
                                    }

                                    int someremainsx = 0;
                                    int totalTimesx = 0;

                                    int someremainsy = 0;
                                    int totalTimesy = 0;

                                    int someremainsz = 0;
                                    int totalTimesz = 0;

                                    int raypositionforwardclampedx = (int)Mathf.Floor(raypositionforward.x * 10.0f) / 10;
                                    int raypositionforwardclampedy = (int)Mathf.Floor(raypositionforward.y * 10.0f) / 10;
                                    int raypositionforwardclampedz = (int)Mathf.Floor(raypositionforward.z * 10.0f) / 10;

                                    /*
                                    //UnityEngine.//Debug.Log("/x:" + raypositionforwardclampedx + "/y:" + raypositionforwardclampedy + "/z:" + raypositionforwardclampedz);

                                    //UnityEngine.//Debug.Log("/someremainsx:" + someremainsx + "/someremainsy:" + someremainsy + "/someremainsz:" + someremainsz);
                                    ////UnityEngine.//Debug.Log("/totalTimesx:" + totalTimesx + "/totalTimesy:" + totalTimesy + "/totalTimesz:" + totalTimesz);
                                    */
                                    /*
                                    //UnityEngine.//Debug.Log("/x:" + rayposforinnerchunkx + "/y:" + rayposforinnerchunky + "/z:" + rayposforinnerchunkz);
                                    //UnityEngine.//Debug.Log("/x:" + sometestx + "/y:" + sometesty + "/z:" + sometestz);
                                    */
                                    //var thechunk = planetdiv.getChunk(rayposforinnerchunkx, rayposforinnerchunky, rayposforinnerchunkz);
                                    var thechunk = planetdiv.getChunk((int)sometestx, (int)sometesty, (int)sometestz);

                                    if (thechunk != null)
                                    {

                                        //targetikfoot.transform.position = new Vector3(thechunk.chunkpos.x, thechunk.chunkpos.y, thechunk.chunkpos.z);

                                        if (thechunk.thebytemap != null)
                                        {
                                            ////Debug.Log("thechunk.thebytemap != null");

                                            var thebytemap = thechunk.thebytemap;

                                            //rayposforinnerchunkbytesx
                                            int multipleofx = 0;
                                            int remnantsx = 0;

                                            int multipleofy = 0;
                                            int remnantsy = 0;

                                            int multipleofz = 0;
                                            int remnantsz = 0;


                                            someremainsx = 0;
                                            totalTimesx = 0;

                                            someremainsy = 0;
                                            totalTimesy = 0;

                                            someremainsz = 0;
                                            totalTimesz = 0;


                                            raypositionforwardclampedx = (int)Mathf.Floor(raypositionforward.x * 100.0f);
                                            raypositionforwardclampedy = (int)Mathf.Floor(raypositionforward.y * 100.0f);
                                            raypositionforwardclampedz = (int)Mathf.Floor(raypositionforward.z * 100.0f);

                                            float theremainsz = 0;



                                            if (raypositionforward.x >= 0)
                                            {
                                                raypositionforwardclampedx = (int)Mathf.Floor(raypositionforward.x * 100.0f) / 10;

                                                someremainsx = (int)Mathf.Floor((raypositionforwardclampedx / 10.0f)) * 10;
                                                totalTimesx = (int)(raypositionforwardclampedx - someremainsx);
                                            }
                                            else
                                            {

                                                raypositionforwardclampedx = Mathf.FloorToInt((int)Mathf.Floor(raypositionforward.x * 100.0f) / 10.0f);


                                                someremainsx = (int)(Mathf.Floor((rayposforinnerchunkbytesx / 10.0f)) * 10.0f);

                                                totalTimesx = (int)(raypositionforwardclampedx - someremainsx);

                                                //totalTimesz *= -1;

                                                if (totalTimesx < 0)
                                                {
                                                    totalTimesx *= -1;
                                                    totalTimesx = 10 - totalTimesx;
                                                }

                                            }



                                            if (raypositionforward.y >= 0)
                                            {
                                                raypositionforwardclampedy = (int)Mathf.Floor(raypositionforward.y * 100.0f) / 10;
                                                someremainsy = (int)Mathf.Floor((raypositionforwardclampedy / 10.0f)) * 10;
                                                totalTimesy = (int)(raypositionforwardclampedy - someremainsy);
                                            }
                                            else
                                            {



                                                raypositionforwardclampedy = Mathf.FloorToInt((int)Mathf.Floor(raypositionforward.y * 100.0f) / 10.0f);


                                                someremainsy = (int)(Mathf.Floor((rayposforinnerchunkbytesy / 10.0f)) * 10.0f);

                                                totalTimesy = (int)(raypositionforwardclampedy - someremainsy);

                                                //totalTimesz *= -1;

                                                if (totalTimesy < 0)
                                                {
                                                    totalTimesy *= -1;
                                                    totalTimesy = 10 - totalTimesy;

                                                }



                                            }


                                            if (raypositionforward.z >= 0)
                                            {
                                                raypositionforwardclampedz = (int)Mathf.Floor(raypositionforward.z * 100.0f) / 10;
                                                someremainsz = (int)Mathf.Floor((raypositionforwardclampedz / 10.0f)) * 10;
                                                totalTimesz = (int)(raypositionforwardclampedz - someremainsz);
                                            }
                                            else
                                            {


                                                raypositionforwardclampedz = Mathf.FloorToInt((int)Mathf.Floor(raypositionforward.z * 100.0f) / 10.0f);


                                                someremainsz = (int)(Mathf.Floor((rayposforinnerchunkbytesz / 10.0f)) * 10.0f);

                                                totalTimesz = (int)(raypositionforwardclampedz - someremainsz);

                                                //totalTimesz *= -1;

                                                if (totalTimesz < 0)
                                                {
                                                    totalTimesz *= -1;
                                                    totalTimesz = 10 - totalTimesz;

                                                }

                                                //totalTimesz = 10 - 1 - totalTimesz;

                                            }



                                            /*

                                            //UnityEngine.//Debug.Log("/totalTimesx:" + totalTimesx + "/totalTimesy:" + totalTimesy + "/totalTimesz:" + totalTimesz + "/theremainsz:" + theremainsz + "/raypositionforwardclampedz:" + raypositionforwardclampedz + "/raypositionforward.z:" + raypositionforward.z);
                                            */


                                            remnantsx = totalTimesx;
                                            remnantsy = totalTimesy;
                                            remnantsz = totalTimesz;




                                            /*
                                            //UnityEngine.//Debug.Log("/chunkposx:" + thechunk.chunkpos.x + "/chunkposy:" + thechunk.chunkpos.y + "/chunkposz:" + thechunk.chunkpos.z);
                                            */


                                            int indexofchunkbytemap = remnantsx + (10) * (remnantsy + (10) * remnantsz);

                                            if (indexofchunkbytemap >= 0 && indexofchunkbytemap < 10 * 10 * 10)
                                            {

                                                if (thechunk.thebytemap[indexofchunkbytemap] == 1)
                                                {

                                                    hasfoundblock = 1;

                                                    swtcdontlookfurther = 1;
                                                    var sccsplayerscript = sccsikplayer.currentsccsikplayer.themovementplayerscript;// arrayofikarms[2].GetComponent<sccsplayer>();

                                                    if (sccsplayerscript != null)
                                                    {
                                                        if (sccsplayerscript.indexofmaxvalueofperfacegravity == 0) //TOPFACE FAKE GRAVITY-ISH CHARACTER ROTATION
                                                        {
                                                            float offsetposx = 0.05f;
                                                            float offsetposy = 0.05f;
                                                            float offsetposz = 0.05f;

                                                            offsetposy += 0.10f;

                                                            Vector3 position = new Vector3((float)rayposforinnerchunkx + ((float)remnantsx * planeSize) + offsetposx, (float)rayposforinnerchunky + ((float)remnantsy * planeSize) + offsetposy, ((float)rayposforinnerchunkz) + ((float)remnantsz * planeSize) + offsetposz);
                                                            //Vector3 position = new Vector3((float)rayposforinnerchunkx, (float)rayposforinnerchunky , ((float)rayposforinnerchunkz));
                                                            /*
                                                            if (whichsiderayselect == 0)
                                                            {
                                                                sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().handTarget.transform.position = position;// new Vector3(rayposforinnerchunkx + (remnantsx * planeSize) + offsetposx, rayposforinnerchunky + (remnantsy * planeSize) + offsetposy, rayposforinnerchunkz + (remnantsz * planeSize) + offsetposz);
                                                            }
                                                            else if (whichsiderayselect == 1)
                                                            {
                                                                sccsikplayer.currentsccsikplayer.arrayofikarms[3].GetComponent<sccsikarm>().handTarget.transform.position = position;// new Vector3(rayposforinnerchunkx + (remnantsx * planeSize) + offsetposx, rayposforinnerchunky + (remnantsy * planeSize) + offsetposy, rayposforinnerchunkz + (remnantsz * planeSize) + offsetposz);

                                                            }*/


                                                            //UnityEngine.//Debug.Log(position + "/z:" + (((float)rayposforinnerchunkz) + ((float)remnantsz * planeSize) + offsetposz));
                                                        }
                                                        else if (sccsplayerscript.indexofmaxvalueofperfacegravity == 1) //LEFTFACE FAKE GRAVITY-ISH CHARACTER ROTATION
                                                        {
                                                            float offsetposx = 0.05f;
                                                            float offsetposy = 0.05f;
                                                            float offsetposz = 0.05f;

                                                            offsetposx += 0.10f;

                                                            Vector3 position = new Vector3((float)rayposforinnerchunkx + ((float)remnantsx * planeSize) + offsetposx, (float)rayposforinnerchunky + ((float)remnantsy * planeSize) + offsetposy, ((float)rayposforinnerchunkz) + ((float)remnantsz * planeSize) + offsetposz);
                                                            //Vector3 position = new Vector3((float)rayposforinnerchunkx, (float)rayposforinnerchunky , ((float)rayposforinnerchunkz));

                                                            /*if (whichsiderayselect == 0)
                                                            {
                                                                sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().handTarget.transform.position = position;// new Vector3(rayposforinnerchunkx + (remnantsx * planeSize) + offsetposx, rayposforinnerchunky + (remnantsy * planeSize) + offsetposy, rayposforinnerchunkz + (remnantsz * planeSize) + offsetposz);
                                                            }
                                                            else if (whichsiderayselect == 1)
                                                            {
                                                                sccsikplayer.currentsccsikplayer.arrayofikarms[3].GetComponent<sccsikarm>().handTarget.transform.position = position;// new Vector3(rayposforinnerchunkx + (remnantsx * planeSize) + offsetposx, rayposforinnerchunky + (remnantsy * planeSize) + offsetposy, rayposforinnerchunkz + (remnantsz * planeSize) + offsetposz);

                                                            }*/


                                                            //UnityEngine.//Debug.Log(position + "/z:" + (((float)rayposforinnerchunkz) + ((float)remnantsz * planeSize) + offsetposz));
                                                        }
                                                        else if (sccsplayerscript.indexofmaxvalueofperfacegravity == 2) //LEFTFACE FAKE GRAVITY-ISH CHARACTER ROTATION
                                                        {
                                                            float offsetposx = 0.05f;
                                                            float offsetposy = 0.05f;
                                                            float offsetposz = 0.05f;

                                                            offsetposx += 0.10f;

                                                            Vector3 position = new Vector3((float)rayposforinnerchunkx + ((float)remnantsx * planeSize) + offsetposx, (float)rayposforinnerchunky + ((float)remnantsy * planeSize) + offsetposy, ((float)rayposforinnerchunkz) + ((float)remnantsz * planeSize) + offsetposz);
                                                            //Vector3 position = new Vector3((float)rayposforinnerchunkx, (float)rayposforinnerchunky , ((float)rayposforinnerchunkz));

                                                            /*if (whichsiderayselect == 0)
                                                            {
                                                                sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().handTarget.transform.position = position;// new Vector3(rayposforinnerchunkx + (remnantsx * planeSize) + offsetposx, rayposforinnerchunky + (remnantsy * planeSize) + offsetposy, rayposforinnerchunkz + (remnantsz * planeSize) + offsetposz);
                                                            }
                                                            else if (whichsiderayselect == 1)
                                                            {
                                                                sccsikplayer.currentsccsikplayer.arrayofikarms[3].GetComponent<sccsikarm>().handTarget.transform.position = position;// new Vector3(rayposforinnerchunkx + (remnantsx * planeSize) + offsetposx, rayposforinnerchunky + (remnantsy * planeSize) + offsetposy, rayposforinnerchunkz + (remnantsz * planeSize) + offsetposz);

                                                            }
                                                            */
                                                            //UnityEngine.//Debug.Log(position + "/z:" + (((float)rayposforinnerchunkz) + ((float)remnantsz * planeSize) + offsetposz));
                                                        }
                                                        else if (sccsplayerscript.indexofmaxvalueofperfacegravity == 3) //LEFTFACE FAKE GRAVITY-ISH CHARACTER ROTATION
                                                        {
                                                            float offsetposx = 0.05f;
                                                            float offsetposy = 0.05f;
                                                            float offsetposz = 0.05f;

                                                            offsetposz += 0.10f;

                                                            Vector3 position = new Vector3((float)rayposforinnerchunkx + ((float)remnantsx * planeSize) + offsetposx, (float)rayposforinnerchunky + ((float)remnantsy * planeSize) + offsetposy, ((float)rayposforinnerchunkz) + ((float)remnantsz * planeSize) + offsetposz);
                                                            //Vector3 position = new Vector3((float)rayposforinnerchunkx, (float)rayposforinnerchunky , ((float)rayposforinnerchunkz));

                                                            /*if (whichsiderayselect == 0)
                                                            {
                                                                sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().handTarget.transform.position = position;// new Vector3(rayposforinnerchunkx + (remnantsx * planeSize) + offsetposx, rayposforinnerchunky + (remnantsy * planeSize) + offsetposy, rayposforinnerchunkz + (remnantsz * planeSize) + offsetposz);
                                                            }
                                                            else if (whichsiderayselect == 1)
                                                            {
                                                                sccsikplayer.currentsccsikplayer.arrayofikarms[3].GetComponent<sccsikarm>().handTarget.transform.position = position;// new Vector3(rayposforinnerchunkx + (remnantsx * planeSize) + offsetposx, rayposforinnerchunky + (remnantsy * planeSize) + offsetposy, rayposforinnerchunkz + (remnantsz * planeSize) + offsetposz);

                                                            }*/

                                                            //UnityEngine.//Debug.Log(position + "/z:" + (((float)rayposforinnerchunkz) + ((float)remnantsz * planeSize) + offsetposz));
                                                        }
                                                        else if (sccsplayerscript.indexofmaxvalueofperfacegravity == 4) //LEFTFACE FAKE GRAVITY-ISH CHARACTER ROTATION
                                                        {
                                                            float offsetposx = 0.05f;
                                                            float offsetposy = 0.05f;
                                                            float offsetposz = 0.05f;

                                                            offsetposz -= 0.10f;

                                                            Vector3 position = new Vector3((float)rayposforinnerchunkx + ((float)remnantsx * planeSize) + offsetposx, (float)rayposforinnerchunky + ((float)remnantsy * planeSize) + offsetposy, ((float)rayposforinnerchunkz) + ((float)remnantsz * planeSize) + offsetposz);
                                                            //Vector3 position = new Vector3((float)rayposforinnerchunkx, (float)rayposforinnerchunky , ((float)rayposforinnerchunkz));

                                                            /*if (whichsiderayselect == 0)
                                                            {
                                                                sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().handTarget.transform.position = position;// new Vector3(rayposforinnerchunkx + (remnantsx * planeSize) + offsetposx, rayposforinnerchunky + (remnantsy * planeSize) + offsetposy, rayposforinnerchunkz + (remnantsz * planeSize) + offsetposz);
                                                            }
                                                            else if (whichsiderayselect == 1)
                                                            {
                                                                sccsikplayer.currentsccsikplayer.arrayofikarms[3].GetComponent<sccsikarm>().handTarget.transform.position = position;// new Vector3(rayposforinnerchunkx + (remnantsx * planeSize) + offsetposx, rayposforinnerchunky + (remnantsy * planeSize) + offsetposy, rayposforinnerchunkz + (remnantsz * planeSize) + offsetposz);

                                                            }*/
                                                            //UnityEngine.//Debug.Log(position + "/z:" + (((float)rayposforinnerchunkz) + ((float)remnantsz * planeSize) + offsetposz));
                                                        }
                                                        else if (sccsplayerscript.indexofmaxvalueofperfacegravity == 5) //LEFTFACE FAKE GRAVITY-ISH CHARACTER ROTATION
                                                        {
                                                            float offsetposx = 0.05f;
                                                            float offsetposy = 0.05f;
                                                            float offsetposz = 0.05f;

                                                            offsetposy -= 0.10f;

                                                            Vector3 position = new Vector3((float)rayposforinnerchunkx + ((float)remnantsx * planeSize) + offsetposx, (float)rayposforinnerchunky + ((float)remnantsy * planeSize) + offsetposy, ((float)rayposforinnerchunkz) + ((float)remnantsz * planeSize) + offsetposz);
                                                            //Vector3 position = new Vector3((float)rayposforinnerchunkx, (float)rayposforinnerchunky , ((float)rayposforinnerchunkz));

                                                            /*if (whichsiderayselect == 0)
                                                            {
                                                                sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().handTarget.transform.position = position;// new Vector3(rayposforinnerchunkx + (remnantsx * planeSize) + offsetposx, rayposforinnerchunky + (remnantsy * planeSize) + offsetposy, rayposforinnerchunkz + (remnantsz * planeSize) + offsetposz);
                                                            }
                                                            else if (whichsiderayselect == 1)
                                                            {
                                                                sccsikplayer.currentsccsikplayer.arrayofikarms[3].GetComponent<sccsikarm>().handTarget.transform.position = position;// new Vector3(rayposforinnerchunkx + (remnantsx * planeSize) + offsetposx, rayposforinnerchunky + (remnantsy * planeSize) + offsetposy, rayposforinnerchunkz + (remnantsz * planeSize) + offsetposz);

                                                            }*/

                                                            //UnityEngine.//Debug.Log(position + "/z:" + (((float)rayposforinnerchunkz) + ((float)remnantsz * planeSize) + offsetposz));
                                                        }
                                                    }

                                                    break;


                                                }
                                                else
                                                {

                                                }
                                            }
                                        }
                                    }
                                    else
                                    {

                                    }
                                }
                            }
                        }
                        /*else
                        {
                            break;
                        }*/
                    }





                    //else
                    {
                        float themag = (raypositionforwardinit - raypositionforward).magnitude;

                        if (themag < hiptofloordist * hiptofloordistmul * 0.125f) // 0.1f
                        {
                            //UnityEngine.//Debug.Log("has reached crouching pos");
                            //raypositionforward = raypositionforwardinit + (raydirection.normalized * hiptofloordist * 0hiptofloordistmul75f);

                            raypositionforward = raypositionforward + (-raydirection.normalized * hiptofloordist * hiptofloordistmul * 0.125f);

                            viewer.transform.position = raypositionforward;// Vector3.Lerp(viewer.transform.position, (raypositionforward), 10.0f);// + (-viewer.transform.up * (totalarmlength)));



                            //raypositionforward = 

                            /*Vector3 diffdir = raypositionforward - raypositionforwardinit;
                            diffdir.Normalize();
                            diffdir = diffdir * (hiptofloordist * 0.75f);

                            Vector3 newpos = raypositionforward + diffdir;

                            viewer.transform.position = Vector3.Lerp(viewer.transform.position, newpos, 10.0f);*/
                        }
                        else
                        {

                            if (hasfoundblock == 1)
                            {

                                raypositionforward = raypositionforward + (-raydirection.normalized * hiptofloordist * hiptofloordistmul * 0.125f);


                                viewer.transform.position = raypositionforward;// Vector3.Lerp(viewer.transform.position, (raypositionforward), 10.0f);//
                                                                               //UnityEngine.//Debug.Log("has reached too high pos");

                            }
                            else
                            {

                                /*raypositionforward = raypositionforward + (-raydirection.normalized * hiptofloordist * hiptofloordistmul);

                                UnityEngine.//Debug.Log("has reached too high pos");


                                viewer.transform.position = Vector3.Lerp(viewer.transform.position, (raypositionforward), 0.5f);//
                                */
                            }



                            /*raypositionforward = raypositionforward + (-raydirection.normalized * hiptofloordist * hiptofloordistmul);

                            viewer.transform.position = Vector3.Lerp(viewer.transform.position, (raypositionforward), 0.15f);// + (-viewer.transform.up * (totalarmlength)));
                            */




                            //raypositionforward = raypositionforward + (-raydirection.normalized * hiptofloordist * 0.5f);

                            //viewer.transform.position = Vector3.Lerp(viewer.transform.position, (raypositionforward), 0.5f);// + (-viewer.transform.up * (totalarmlength)));

                            //UnityEngine.//Debug.Log("has reached too high pos");

                            /*Vector3 diffdir = raypositionforward - raypositionforwardinit;
                            diffdir.Normalize();
                            diffdir = diffdir * (hiptofloordist * 0.75f);

                            Vector3 newpos = raypositionforward + diffdir;

                            viewer.transform.position = Vector3.Lerp(viewer.transform.position, newpos, 10.0f);
                            */





                            /*if (indexofmaxvalueofperfacegravitylast != indexofmaxvalueofperfacegravity)
                            {
                                Vector3 diffdir = raypositionforward - raypositionforwardinit;
                                diffdir.Normalize();
                                diffdir = diffdir * (hiptofloordist * 0.75f);

                                Vector3 newpos = raypositionforward + diffdir;

                                viewer.transform.position = Vector3.Lerp(viewer.transform.position, newpos, 10.0f);
                            }
                            */

                            /*
                            Vector3 diffdir = raypositionforward - raypositionforwardinit;
                            diffdir.Normalize();
                            diffdir = diffdir * (hiptofloordist * 0.75f);

                            Vector3 newpos = raypositionforward + diffdir;

                            viewer.transform.position = Vector3.Lerp(viewer.transform.position, newpos, 10.0f);
                            */

                            /*
                            //UnityEngine.//Debug.Log("!has reached crouching pos");
                            //raypositionforward = raypositionforwardinit + (raydirection.normalized * hiptofloordist * 0.75f);
                            viewer.transform.position = Vector3.Lerp(viewer.transform.position, (viewer.transform.position + diffdir), 0.01f);// + (-viewer.transform.up * (totalarmlength)));
                            */





                        }
                    }
                }

            }




            indexofmaxvalueofperfacegravitylast = indexofmaxvalueofperfacegravity;

            hasgotgravitydot = 0;
        }

        lastposition = currentPosition;

    }















    Vector3 dirtoplanetcore = Vector3.zero;

    public GameObject theplanet;







    Vector3 thepositionupofpoint;


    IEnumerator RotatePlayerWithMouseClick()
    {


        if (theplanet != null)
        {
            var dirright = viewer.transform.rotation * Vector3.right;
            dirright.Normalize();
            clicktomoveplayerdirtopos.Normalize();

            float thedot = Vector3.Dot(dirright, clicktomoveplayerdirtopos);

            float rotationincrements = 50.0f;

            var distance = 0.025f;
            var dirForward = viewer.transform.rotation * Vector3.forward;
            dirForward.Normalize();


            Vector3 positioninfrontofplayer = viewer.transform.position + (dirForward * distance);

            Vector3 positioninbackofplayer = viewer.transform.position + (-dirForward * distance);


            Vector3 dirplanetcoretoplayer = viewer.transform.position - theplanet.transform.position;
            float distcoretoplayer = dirplanetcoretoplayer.magnitude;


            Vector3 dirplanetcoretopointfrontofplayer = positioninfrontofplayer - theplanet.transform.position;
            //float distcoretopointinfrontofplayer = dirplanetcoretopointfrontofplayer.magnitude;
            dirplanetcoretopointfrontofplayer.Normalize();

            Vector3 alwaysuppointofplayercomparedtoplayercore = theplanet.transform.position + (dirplanetcoretopointfrontofplayer * (distcoretoplayer));

            var dirUp = viewer.transform.rotation * Vector3.up;
            dirUp.Normalize();
            thepositionupofpoint = clicktomoveplayerpos + (dirUp * 0.0012345f);

            Vector3 forwarddirtopointfrontplayer = thepositionupofpoint - viewer.transform.position;

            forwarddirtopointfrontplayer.Normalize();

            float rotate_speed = 5.0f;

            Quaternion rot = new Quaternion();
            rot.SetLookRotation(forwarddirtopointfrontplayer, -(theplanet.transform.position - viewer.transform.position).normalized);
            viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, rot, rotate_speed * Time.deltaTime);


            if (thedot > 0.00123f)
            {
                /*RotationY += rotationincrements;//


                // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
                float pitch = RotationX * 0.0174532925f;
                float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
                float roll = RotationZ * 0.0174532925f;

                //Vector3 lookatlocal = transform.TransformDirection(lookAt);

                //https://answers.unity.com/questions/1611821/rotation-yaw-roll-pitch.html
                //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
                //viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
                viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);*/
            }
            else if (thedot < -0.00123f)
            {
                /*RotationY -= rotationincrements;//


                /// Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
                float pitch = RotationX * 0.0174532925f;
                float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
                float roll = RotationZ * 0.0174532925f;

                //Vector3 lookatlocal = transform.TransformDirection(lookAt);

                //https://answers.unity.com/questions/1611821/rotation-yaw-roll-pitch.html
                //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
                //viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
                viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);*/
            }

        }
        else
        {
            ////Debug.Log("the planet is null");
        }

        yield return new WaitForSeconds(0.001f);
    }


    Vector3 clicktomoveplayerpos = Vector3.zero;
    Vector3 clicktomoveplayerdirtopos = Vector3.zero;
    Vector3 clicktomoveplayernormalofpos = Vector3.zero;

    int hasclickedtomoveplayer = 0;

    Vector3 lastposition = Vector3.zero;

    IEnumerator CheckMoving()
    {
        //swtcfordontchangefacegravity = 0;


        currentChunkCoordX = Mathf.RoundToInt(viewer.transform.position.x / (smallChunkWidth * 0.5f));
        currentChunkCoordY = Mathf.RoundToInt(viewer.transform.position.y / (smallChunkWidth * 0.5f));
        currentChunkCoordZ = Mathf.RoundToInt(viewer.transform.position.z / (smallChunkWidth * 0.5f));

        //float currentChunkCoordX = viewer.transform.position.x;
        //float currentChunkCoordY = viewer.transform.position.y ;
        //float currentChunkCoordZ = viewer.transform.position.z ;

        currentPosition = new Vector3(currentChunkCoordX, currentChunkCoordY, currentChunkCoordZ);

        Vector3 startPos = currentPosition;
        yield return new WaitForSeconds(0.001f);
        Vector3 finalPos = currentPosition;

        if (startPos.x != finalPos.x || startPos.y != finalPos.y
            || startPos.z != finalPos.z)
        {
            isMoving = true;
        }

        else if (startPos.x == finalPos.x && startPos.y == finalPos.y
             && startPos.z == finalPos.z)
        {
            isMoving = false;
        }
    }






    int swtchastriedmovingplayerwithmouseclick = 0;
    IEnumerator MovePlayerWithMouseClick()
    {



        //MOVEPOSOFFSET = viewer.transform.position;
        /*
        currentChunkCoordX = Mathf.RoundToInt(viewer.transform.position.x / (smallChunkWidth * 0.5f));
        currentChunkCoordY = Mathf.RoundToInt(viewer.transform.position.y / (smallChunkWidth * 0.5f));
        currentChunkCoordZ = Mathf.RoundToInt(viewer.transform.position.z / (smallChunkWidth * 0.5f));

        //float currentChunkCoordX = viewer.transform.position.x;
        //float currentChunkCoordY = viewer.transform.position.y ;
        //float currentChunkCoordZ = viewer.transform.position.z ;

        currentPosition = new Vector3(currentChunkCoordX, currentChunkCoordY, currentChunkCoordZ);

        Vector3 startPos = currentPosition;
        yield return new WaitForSeconds(0.001f);
        Vector3 finalPos = currentPosition;*/


        /*
        var dirUp = viewer.transform.rotation * Vector3.up;
        dirUp.Normalize();


        //var dirUp = theplanet.transform.position - viewer.transform.position;
        //dirUp.Normalize();

        Vector3 topointforward = MOVEPOSOFFSET;// clicktomoveplayerpos;


        //clicktomoveplayerpos

        float clickmovespeed = movementspeed * 0.1f;

        /*thepositionupofpoint = clicktomoveplayerpos + (dirUp * 0.0012345f);

        //Debug.DrawRay(clicktomoveplayerpos, thepositionupofpoint - clicktomoveplayerpos, Color.red, 1.0f);
       
        MOVEPOSOFFSET = Vector3.Lerp(topointforward, thepositionupofpoint, 0.01f);

        viewer.transform.position = MOVEPOSOFFSET;
        lastframeviewerpos = viewer.transform.position;


        //https://answers.unity.com/questions/1397510/converting-mouse-position-to-worldpoint-in-3d.html
        float speed = 10.0f;
        // Cast a ray from screen point
        Ray ray = new Ray(isgroundedpivotpoint.transform.position, isgroundedpivotpoint.transform.forward);// isgroundedpivotpoint.transform.position;// Camera.main.ScreenPointToRay(isgroundedpivotpoint.transform.position);//Camera.main.ScreenPointToRay(Input.mousePosition);
                                                                                                           // Save the info
        RaycastHit theouthit;

        /* if (swtchastriedmovingplayerwithmouseclick == 0)
         {
             // You successfully $$anonymous$$

         }

        if (Physics.Raycast(ray, out theouthit, layerMask))
        {
            // Find the direction to move in
            //Vector3 dir = theouthit.point - isgroundedpivotpoint.transform.position;

            var distance = 0.01f * movementspeed;
            var dirForward = viewer.transform.rotation * Vector3.forward;
            dirForward.Normalize();

            Vector3 positioninfrontofplayer = isgroundedpivotpoint.transform.position + (dirForward * distance);

            //Vector3 dirplanetcoretopoint = theplanet.transform.position - positioninfrontofplayer;
            //Vector3 dirplanetcoretopointnotnorm = dirplanetcoretopoint;
            //float distcoretopointinfrontofplayer = dirplanetcoretopoint.magnitude;

            //dirplanetcoretopoint.Normalize();


            //Vector3 pointinfrontofplayer = 

            //Vector3 dirtopointinfrontofplayer = positioninfrontofplayer - isgroundedpivotpoint.transform.position;
            //distcoretopointinfrontofplayer


            pointertarget.transform.position = clicktomoveplayerpos;

            Vector3 currentdirtopointinfrontdir = clicktomoveplayerpos - theouthit.point;
            currentdirtopointinfrontdir.Normalize();
            //topointforward = theouthitpointtoground.point;
            ////Debug.DrawRay(positioninfrontofplayer, thepositionupofpoint - positioninfrontofplayer, Color.red, 1.0f);
            Vector3 movepos = MOVEPOSOFFSET + (currentdirtopointinfrontdir * distance);


            Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
            uppoint.Normalize();

            //Vector3 pointtobeat = clicktomoveplayerpos + (-uppoint * 0.1f);

            MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, movepos, clickmovespeed * Time.deltaTime); // * Time.deltaTime



            //Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
            //uppoint.Normalize();

            //Vector3 pointtobeat = viewer.transform.position + (uppoint * 0.5f);

            //MOVEPOSOFFSET = MOVEPOSOFFSET;// MOVEPOSOFFSET + (currentdirtopointinfrontdir * distance);
            //MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime

            Ray raypointtoground = new Ray(positioninfrontofplayer, isgroundedpivotpoint.transform.forward);
            RaycastHit theouthitpointtoground;


            if (Physics.Raycast(positioninfrontofplayer, isgroundedpivotpoint.transform.forward, out theouthitpointtoground, isgroundedmaxdist, layerMask))
            //if (Physics.Raycast(raypointtoground, out theouthitpointtoground, layerMask))
            {




            }
        }
        else
        {
            hasclickedtomoveplayer = 0;
            //////Debug.Log("test");
        }

        //Debug.DrawRay(clicktomoveplayerpos, thepositionupofpoint - clicktomoveplayerpos, Color.red, 1.0f);
        //MOVEPOSOFFSET = Vector3.Lerp(topointforward, thepositionupofpoint, 0.01f);

        viewer.transform.position = MOVEPOSOFFSET;
        lastframeviewerpos = viewer.transform.position;*/







        //https://answers.unity.com/questions/1397510/converting-mouse-position-to-worldpoint-in-3d.html
        float speed = 10.0f;
        // Cast a ray from screen point
        Ray ray = new Ray(isgroundedpivotpoint.transform.position, isgroundedpivotpoint.transform.forward);// isgroundedpivotpoint.transform.position;// Camera.main.ScreenPointToRay(isgroundedpivotpoint.transform.position);//Camera.main.ScreenPointToRay(Input.mousePosition);
                                                                                                           // Save the info
        RaycastHit theouthit;

        /* if (swtchastriedmovingplayerwithmouseclick == 0)
         {
             // You successfully $$anonymous$$

         }*/


        //var something = 1 << 8; layermaskunitystuff 



        if (Physics.Raycast(ray, out theouthit, layerMask))
        {
            // Find the direction to move in
            //Vector3 dir = theouthit.point - isgroundedpivotpoint.transform.position;

            //Vector3 currentdirtopointclick = clicktomoveplayerpos - theouthit.point;
            //currentdirtopointclick.Normalize();

            Vector3 dirForward = clicktomoveplayerpos - theouthit.point;
            dirForward.Normalize();

            var distance = 0.01f * movementspeed;
            /*var dirForward = viewer.transform.rotation * Vector3.forward;
            dirForward.Normalize();
            */


            //Vector3 currentdirtopointclick = clicktomoveplayerpos - theouthit.point;
            //currentdirtopointclick.Normalize();

            Vector3 positioninfrontofplayer = isgroundedpivotpoint.transform.position + (dirForward * distance);
            
            
            /*
            MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, positioninfrontofplayer, movementspeed * Time.deltaTime); // * Time.deltaTime

            */
            //Vector3 dirplanetcoretopoint = theplanet.transform.position - positioninfrontofplayer;
            //Vector3 dirplanetcoretopointnotnorm = dirplanetcoretopoint;
            //float distcoretopointinfrontofplayer = dirplanetcoretopoint.magnitude;

            //dirplanetcoretopoint.Normalize();


            //Vector3 pointinfrontofplayer = 

            //Vector3 dirtopointinfrontofplayer = positioninfrontofplayer - isgroundedpivotpoint.transform.position;
            //distcoretopointinfrontofplayer



            Ray raypointtoground = new Ray(positioninfrontofplayer, isgroundedpivotpoint.transform.forward);
            RaycastHit theouthitpointtoground;


            if (Physics.Raycast(positioninfrontofplayer, isgroundedpivotpoint.transform.forward, out theouthitpointtoground, isgroundedmaxdist, layerMask))
            //if (Physics.Raycast(raypointtoground, out theouthitpointtoground, layerMask))
            {




                if (theplanet != null)
                {

                    pointertarget.transform.position = theouthitpointtoground.point;

                    Vector3 currentdirtopointinfrontdir = theouthitpointtoground.point - theouthit.point;
                    currentdirtopointinfrontdir.Normalize();


                    Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
                    uppoint.Normalize();

                    Vector3 pointtobeat = theouthitpointtoground.point + (-uppoint * 0.1f);
                    MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime



                    //MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime


                    //topointforward = theouthitpointtoground.point;
                    ////Debug.DrawRay(positioninfrontofplayer, thepositionupofpoint - positioninfrontofplayer, Color.red, 1.0f);
                    //MOVEPOSOFFSET = MOVEPOSOFFSET + (currentdirtopointinfrontdir * distance); //

                    /*
                    Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
                    uppoint.Normalize();

                    Vector3 pointtobeat = theouthitpointtoground.point + (-uppoint * 0.1f);

                    MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime
                    */


                    //Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
                    //uppoint.Normalize();

                    //Vector3 pointtobeat = viewer.transform.position + (uppoint * 0.5f);

                    //MOVEPOSOFFSET = MOVEPOSOFFSET;// MOVEPOSOFFSET + (currentdirtopointinfrontdir * distance);
                    //MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime
                }
                else
                {
                    ////Debug.Log("the planet is null");
                }
            }
        }
        else
        {
            hasclickedtomoveplayer = 0;
            //////Debug.Log("test");
        }

        //Debug.DrawRay(clicktomoveplayerpos, thepositionupofpoint - clicktomoveplayerpos, Color.red, 1.0f);

        if (theplanet != null)
        {
            viewer.transform.position = MOVEPOSOFFSET;
            lastframeviewerpos = viewer.transform.position;
        }
        yield return new WaitForSeconds(0.001f);

    }











    IEnumerator MovePlayerWithKeyboard()
    {


        var sccsikarmtargetfootl = sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().handTarget;
        var sccsikarmtargetfootr = sccsikplayer.currentsccsikplayer.arrayofikarms[3].GetComponent<sccsikarm>().handTarget;



        var pelvisgameobject = sccsikplayer.currentsccsikplayer.GetComponent<sccsikplayer>().pelvisemptygameobject;





        Vector3 currentposofleftfoot = sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().shoulder.GetComponent<playerInteractionrev11>().raypositionmovingforwardtargethit;
        Vector3 currentposofrightfoot = sccsikplayer.currentsccsikplayer.arrayofikarms[3].GetComponent<sccsikarm>().shoulder.GetComponent<playerInteractionrev11>().raypositionmovingforwardtargethit;

        Vector3 positionofpivotlegleft = sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().shoulder.GetComponent<playerInteractionrev11>().transform.position;
        Vector3 positionofpivotlegright = sccsikplayer.currentsccsikplayer.arrayofikarms[3].GetComponent<sccsikarm>().shoulder.GetComponent<playerInteractionrev11>().transform.position;

        Vector3 dirpivotleglefttotargetleft = currentposofleftfoot - positionofpivotlegleft;
        float maglegleft = dirpivotleglefttotargetleft.magnitude;
        dirpivotleglefttotargetleft.Normalize();

        Vector3 dirpivotlegrighttotargetright = currentposofrightfoot - positionofpivotlegright;
        float maglegright = dirpivotlegrighttotargetright.magnitude;
        dirpivotlegrighttotargetright.Normalize();

        float diffinmags = Mathf.Abs(maglegright - maglegright);

        float totalarmlength = sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().totalArmLength + (sccsikplayer.currentsccsikplayer.pelvisrenderer.transform.localScale.y * 0.5f);

        /*if (maglegleft > totalarmlength || maglegright > totalarmlength)
        {
            //Debug.Log(" > totalarmlength");
            if (maglegleft > totalarmlength)
            {

            }
            else if (maglegleft <= totalarmlength)
            {

            }

            if (maglegright > totalarmlength)
            {

            }
            else if (maglegright <= totalarmlength)
            {

            }
        }
        else
        {
            Vector3 midpointfoot = currentposofleftfoot - currentposofrightfoot;
            float magmidpointfoot = midpointfoot.magnitude;
            magmidpointfoot *= 0.5f;
            midpointfoot.Normalize();

            Vector3 posofmidpoint = currentposofleftfoot + (midpointfoot * magmidpointfoot);


            viewer.transform.position = (posofmidpoint + (viewer.transform.up * (totalarmlength * 0.75f)));
        }*/















        //MOVEPOSOFFSET = viewer.transform.position;

        /*currentChunkCoordX = Mathf.RoundToInt(viewer.transform.position.x / (smallChunkWidth * 0.5f));
        currentChunkCoordY = Mathf.RoundToInt(viewer.transform.position.y / (smallChunkWidth * 0.5f));
        currentChunkCoordZ = Mathf.RoundToInt(viewer.transform.position.z / (smallChunkWidth * 0.5f));

        //float currentChunkCoordX = viewer.transform.position.x;
        //float currentChunkCoordY = viewer.transform.position.y ;
        //float currentChunkCoordZ = viewer.transform.position.z ;

        currentPosition = new Vector3(currentChunkCoordX, currentChunkCoordY, currentChunkCoordZ);

        Vector3 startPos = currentPosition;
        yield return new WaitForSeconds(0.001f);
        Vector3 finalPos = currentPosition;
        */

        Vector3 topointforward = MOVEPOSOFFSET;
       
        if (Input.GetKey(KeyCode.W))
        {

            Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
            uppoint.Normalize();

            Vector3 pointtobeat = viewer.transform.position + (viewer.transform.forward * 0.75f * movementspeed); // + (-uppoint * 0.1f)



            MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime

            //UnityEngine.////Debug.Log("has tried moving w");





            /*pointertarget.transform.position = theouthitpointtoground.point;

            Vector3 currentdirtopointinfrontdir = theouthitpointtoground.point - theouthit.point;

            //topointforward = theouthitpointtoground.point;
            ////Debug.DrawRay(positioninfrontofplayer, thepositionupofpoint - positioninfrontofplayer, Color.red, 1.0f);
            MOVEPOSOFFSET = MOVEPOSOFFSET + (currentdirtopointinfrontdir * distance);


            Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
            uppoint.Normalize();

            Vector3 pointtobeat = theouthitpointtoground.point + (-uppoint * 0.1f);

            MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime*/

        }

        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
            uppoint.Normalize();

            Vector3 pointtobeat = viewer.transform.position + (-viewer.transform.right * 0.75f * movementspeed); // + (-uppoint * 0.1f)



            MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime

        }

        if (Input.GetKey(KeyCode.E))
        {
            Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
            uppoint.Normalize();

            Vector3 pointtobeat = viewer.transform.position + (viewer.transform.right * 0.75f * movementspeed); // + (-uppoint * 0.1f)



            MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime

        }

        if (Input.GetKey(KeyCode.S))
        {
            Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
            uppoint.Normalize();

            Vector3 pointtobeat = viewer.transform.position + (-viewer.transform.forward * 0.75f * movementspeed); // + (-uppoint * 0.1f)



            MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime

        }

        if (Input.GetKey(KeyCode.R))
        {
            var distance = 0.1f;
            //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
            var dirForward = viewer.transform.rotation * Vector3.up;
            dirForward.Normalize();
            MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);
            //topointforward = MOVEPOSOFFSET + pointforward;
        }


        if (Input.GetKey(KeyCode.F))
        {
            var distance = 0.1f;
            //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
            var dirForward = viewer.transform.rotation * -Vector3.up;
            dirForward.Normalize();
            MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);
            //topointforward = MOVEPOSOFFSET + pointforward;
        }

        //var currentrotationforward = viewer.transform.rotation.eulerAngles.z;

        //MOVEPOSOFFSET.y = topointforward.y;



        /*
        Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
        uppoint.Normalize();

        Vector3 pointtobeat = viewer.transform.position + (uppoint * 0.5f);

        MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime
        */



        //MOVEPOSOFFSET = Vector3.Lerp(topointforward, MOVEPOSOFFSET, movementspeed * Time.deltaTime); // * Time.deltaTime




        //viewer.transform.Translate(MOVEPOSOFFSET, Space.World);






        //hiptofloordist
        if (theplanet != null)
        {
            viewer.transform.position = MOVEPOSOFFSET;
            lastframeviewerpos = viewer.transform.position;
        }

        yield return new WaitForSeconds(0.001f);
    }




    float mousex = 0;
    float mousey = 0;

    IEnumerator RotatePlayerMouse()
    {

       

        if (Input.GetMouseButton(1))
        {
            if (swtcactivatemouselook == 0)
            {
                beforemouselookrot = camera.transform.rotation;
                swtcactivatemouselook = 1;
            }
            UnityEngine.Cursor.lockState = CursorLockMode.Locked;

            /*var mouseposition = Input.mousePosition;
            mouseposition.x = 0;
            mouseposition.y = 0;*/

            //Input.mousePosition = mouseposition;

            mousex = Input.GetAxis("Mouse X") * mouserotatespeed;
            mousey = Input.GetAxis("Mouse Y") * mouserotatespeed;

            oricursorx = mousex;
            oricursory = mousey;


            
            Quaternion q = viewer.transform.rotation;

            float x = q.x;
            float y = q.y;
            float z = q.z;
            float w = q.w;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


            RotationX = scmaths.RadianToDegree(pitchcurrent);
            RotationY = scmaths.RadianToDegree(yawcurrent);
            RotationZ = scmaths.RadianToDegree(rollcurrent);


            MouseRotationX += mousey;
            MouseRotationY += mousex;

            float pitch = (MouseRotationX + RotationX) * 0.0174532925f;
            float yaw = (MouseRotationY + RotationY) * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = (RotationZ) * 0.0174532925f;


            camera.transform.rotation = upperbodypivot.transform.rotation * Quaternion.Euler(pitch, yaw, roll);

            UnityEngine.Cursor.visible = false;



        }



        if(Input.GetMouseButtonUp(1))
        {

            if (swtcactivatemouselook == 1)
            {
                UnityEngine.Cursor.visible = true;
                UnityEngine.Cursor.lockState = CursorLockMode.None;

                Quaternion q = viewer.transform.rotation;

                float x = q.x;
                float y = q.y;
                float z = q.z;
                float w = q.w;

                //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
                float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
                float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
                float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


                //rollcurrent = rollcurrent * Mathf.PI / 180;
                //pitchcurrent = pitchcurrent * Mathf.PI / 180;
                //yawcurrent = yawcurrent * Mathf.PI / 180;

                float pitch = scmaths.RadianToDegree(pitchcurrent);// rotationincrements;
                float yaw = scmaths.RadianToDegree(yawcurrent);// rotationincrements;
                float roll = scmaths.RadianToDegree(rollcurrent);// rotationincrements;


                //camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, beforemouselookrot,0.1f * Time.deltaTime);

                //camera.transform.rotation = beforemouselookrot;
                camera.transform.rotation = upperbodypivot.transform.rotation;// Quaternion.Euler(pitch, yaw, roll);// Quaternion.Lerp(camera.transform.rotation, Quaternion.Euler(pitch, yaw, roll), 0.1f);
                //camera.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, Quaternion.Euler(pitch, yaw, roll), 0.1f);
                
                MouseRotationX = 0;
                MouseRotationY = 0;
                MouseRotationZ = 0;

                //viewer.transform.rotation = beforemouselookrot;
                swtcactivatemouselook = 0;
            }
        }











        if (Input.GetMouseButton(0))
        {
            //https://answers.unity.com/questions/1397510/converting-mouse-position-to-worldpoint-in-3d.html
            float speed = 10.0f;
            // Cast a ray from screen point
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            // Save the info
            RaycastHit theouthit;

            if (swtchastriedmovingplayerwithmouseclick == 0)
            {
                // You successfully $$anonymous$$
                if (Physics.Raycast(ray, out theouthit, layerMask))
                {
                    // Find the direction to move in
                    Vector3 dir = theouthit.point - camera.transform.position;

                   
                    clicktomoveplayerpos = theouthit.point;
                    clicktomoveplayerdirtopos = dir;
                    clicktomoveplayernormalofpos = theouthit.normal;

                    //Debug.DrawRay(camera.transform.position, dir * 10.0f, Color.blue, 10.0f);
                    hasclickedtomoveplayer = 1;

                    ////Debug.Log("hasclickedtomoveplayer");
                    swtchastriedmovingplayerwithmouseclick = 1;
                }
                else
                {
                    hasclickedtomoveplayer = 0;
                    //////Debug.Log("test");
                }
            }


        }


        if (Input.GetMouseButtonUp(0))
        {
            hasclickedtomoveplayer = 0;
            swtchastriedmovingplayerwithmouseclick = 0;
        }





        yield return new WaitForSeconds(0.001f);
    }



    public GameObject targetobjectvisual;
    int targetobjectvisualinitswtc = 0;

    int swtchwaypointtype = 0;
    float dotGoal = 0;
    int answerx = 0;
    int answery = 0;
    int answerz = 0;


    int[] arrayofcubiclocx = new int[27];
    int[] arrayofcubiclocy = new int[27];
    int[] arrayofcubiclocz = new int[27];

    float[] arrayofcubiclocdot = new float[27];

    int[] arrayofgravityperfacex = new int[6];
    int[] arrayofgravityperfacey = new int[6];
    int[] arrayofgravityperfacez = new int[6];

    float[] arrayofgravityperfacedot = new float[6];

    float poscubicgravityvisualx = 0;
    float poscubicgravityvisualy = 0;
    float poscubicgravityvisualz = 0;


    float poscubicgravityvisualnextx = 0;
    float poscubicgravityvisualnexty = 0;
    float poscubicgravityvisualnextz = 0;

    public void findgravitygriddots() //try and incorporate my 3d compass/gimball dot products here.
    {

        if (targetobjectvisualinitswtc == 0)
        {
            targetobjectvisual = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            targetobjectvisual.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            targetobjectvisual.transform.position = this.transform.position;// new Vector3(0, 0, 0.0f);
                                                                            //pelvisrenderer.transform.parent = pelvisemptygameobject.transform;
            targetobjectvisual.transform.name = "pelvisrenderer";
            targetobjectvisual.GetComponent<Renderer>().material.color = Color.black;



            targetobjectvisualinitswtc = 1;
        }




        /*
        if (theplanet != null)
        {
            //if (swtchwaypointtype == 0)
            {
                Vector2 dirbulletprimerright = new Vector2(theplanet.transform.right.x, theplanet.transform.right.y);
                dirbulletprimerright.Normalize();

                Vector2 dirprimertonorthpoletransform = new Vector2(viewer.transform.position.x, viewer.transform.position.y) - new Vector2(theplanet.transform.position.x, theplanet.transform.position.y);
                dirprimertonorthpoletransform.Normalize();

                dotGoal = scmaths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);

                //dotGoal *= -1;
                if (dotGoal >= 0) // 0.001f
                {
                    answerx = 1;
                }
                else if (dotGoal < 0) //-0.001f
                {
                    answerx = -1;
                }
            }
            //else if (swtchwaypointtype == 1)
            {
                Vector2 dirbulletprimerright = new Vector2(-theplanet.transform.right.z, theplanet.transform.right.x);
                dirbulletprimerright.Normalize();

                Vector2 dirprimertonorthpoletransform = new Vector2(viewer.transform.position.x, viewer.transform.position.z) - new Vector2(theplanet.transform.position.x, theplanet.transform.position.z);
                dirprimertonorthpoletransform.Normalize();

                dotGoal = scmaths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);

                //dotGoal *= -1;

                if (dotGoal >= 0) // 0.001f
                {
                    answery = 1;
                }
                else if (dotGoal < 0)//-0.001f
                {
                    answery = -1;
                }
            }
            //else if (swtchwaypointtype == 2)
            {
                Vector2 dirbulletprimerright = new Vector2(theplanet.transform.forward.z, theplanet.transform.forward.y);
                dirbulletprimerright.Normalize();

                Vector2 dirprimertonorthpoletransform = new Vector2(theplanet.transform.position.z, theplanet.transform.position.y) - new Vector2(viewer.transform.position.z, viewer.transform.position.y);
                dirprimertonorthpoletransform.Normalize();

                dotGoal = scmaths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);
                /*
                if (_dotGoal >= 0 && _dotGoal < 0.50f) // 0.001f
                {
                    answer = 1;
                }
                else if (_dotGoal > -0.50f && _dotGoal < 0) //-0.001f
                {
                    answer = -1;
                }
                dotGoal *= -1;
                if (dotGoal >= 0) // 0.001f
                {
                    answerz = 1;
                }
                else if (dotGoal < 0) //-0.001f
                {
                    answerz = -1;
                }
            }

        }*/



        if (theplanet != null)
        {
            float dirx = viewer.transform.position.x - theplanet.transform.position.x;
            float diry = viewer.transform.position.y - theplanet.transform.position.y;
            float dirz = viewer.transform.position.z - theplanet.transform.position.z;

            Vector3 dirratio = new Vector3(dirx, diry, dirz);
            dirratio.Normalize();

            float dirrationormx = dirratio.x;// 1 / dirx;
            float dirrationormy = dirratio.y;//1 / diry;
            float dirrationormz = dirratio.z;// 1 / dirz;



          
            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    for (int z = -1; z <= 1; z++)
                    {
                        //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), new Vector3(x, y, z), Quaternion.identity);
                        Vector3 cubiclocation = new Vector3(x, y, z);
                        Vector3 dirplanetcubicloc = (theplanet.transform.position + cubiclocation) - theplanet.transform.position;
                        dirplanetcubicloc.Normalize();

                        Vector3 dirnorm = viewer.transform.position - theplanet.transform.position;
                        dirnorm.Normalize();

                        float dot = Vector3.Dot(dirplanetcubicloc, dirnorm);

                        /*if (dot > 0.3333333337f)
                        {

                        }*/

                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + 1;
                        }

                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + 1;
                        }

                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + 1;
                        }

                        int indexflat = xx + (3) * (yy + (3) * zz);

                        arrayofcubiclocdot[indexflat] = dot;

                        arrayofcubiclocx[indexflat] = x;
                        arrayofcubiclocy[indexflat] = y;
                        arrayofcubiclocz[indexflat] = z;

                        //topface 0 1 0
                        //frontface 0 0 1
                        //rightface 1 0 0
                        //leftface -1 0 0
                        //backface 0 0 -1
                        //bottomface 0 -1 0


                        //TOPFACE
                        if (x == 0 && y == 1 && z == 0)
                        {
                            arrayofgravityperfacex[0] = x;
                            arrayofgravityperfacey[0] = y;
                            arrayofgravityperfacez[0] = z;

                            arrayofgravityperfacedot[0] = dot;
                        }
                        //LEFTFACE
                        else if (x == -1 && y == 0 && z == 0)
                        {
                            arrayofgravityperfacex[1] = x;
                            arrayofgravityperfacey[1] = y;
                            arrayofgravityperfacez[1] = z;
                            arrayofgravityperfacedot[1] = dot;

                        }
                        //RIGHFACE
                        else if (x == 1 && y == 0 && z == 0)
                        {
                            arrayofgravityperfacex[2] = x;
                            arrayofgravityperfacey[2] = y;
                            arrayofgravityperfacez[2] = z;
                            arrayofgravityperfacedot[2] = dot;
                        }
                        //FRONTFACE
                        else if (x == 0 && y == 0 && z == 1)
                        {
                            arrayofgravityperfacex[3] = x;
                            arrayofgravityperfacey[3] = y;
                            arrayofgravityperfacez[3] = z;
                            arrayofgravityperfacedot[3] = dot;
                        }
                        //BACKFACE
                        else if (x == 0 && y == 0 && z == -1)
                        {
                            arrayofgravityperfacex[4] = x;
                            arrayofgravityperfacey[4] = y;
                            arrayofgravityperfacez[4] = z;
                            arrayofgravityperfacedot[4] = dot;
                        }
                        //BOTTOMFACE
                        else if (x == 0 && y == -1 && z == 0)
                        {
                            arrayofgravityperfacex[5] = x;
                            arrayofgravityperfacey[5] = y;
                            arrayofgravityperfacez[5] = z;
                            arrayofgravityperfacedot[5] = dot;
                        }
                    }
                }
            }




            var indexofmaxvalue = arrayofcubiclocdot.ToList().IndexOf(arrayofcubiclocdot.Max());
            float posx = arrayofcubiclocx[indexofmaxvalue];
            float posy = arrayofcubiclocy[indexofmaxvalue];
            float posz = arrayofcubiclocz[indexofmaxvalue];

            /*targetobjectvisual.transform.position = new Vector3(posx * 5, posy * 5, posz * 5);

            ////Debug.Log("/x:" + posx + "/y:" + posy + "/z:" + posz);
            */
            indexofmaxvalueofperfacegravity = arrayofgravityperfacedot.ToList().IndexOf(arrayofgravityperfacedot.Max());

            indexofmaxvalueofperfacegravitydot = arrayofgravityperfacedot[indexofmaxvalueofperfacegravity];

            var listofnextgravity = arrayofgravityperfacedot.ToList();
            var listofnextgravitynext = arrayofgravityperfacedot.ToList();
            var maxindex = listofnextgravity.IndexOf(listofnextgravity.Max());
            var listwithnextgravity = listofnextgravitynext.Remove(arrayofgravityperfacedot.Max());
            var maxindexnext = listofnextgravity.IndexOf(listofnextgravitynext.Max());

            indexofmaxvalueofperfacegravitynext = maxindexnext;
            indexofmaxvalueofperfacegravitynextdot = arrayofgravityperfacedot[indexofmaxvalueofperfacegravitynext];



            float indexofmaxvalueofperfacegravitydotinvert = indexofmaxvalueofperfacegravitydot;

            if (indexofmaxvalueofperfacegravitydotinvert < 0)
            {
                indexofmaxvalueofperfacegravitydotinvert *= -1;
                indexofmaxvalueofperfacegravitydotinvert = indexofmaxvalueofperfacegravitydotinvert + 1.0f;
            }

            float indexofmaxvalueofperfacegravitynextdotinvert = indexofmaxvalueofperfacegravitynextdot;

            if (indexofmaxvalueofperfacegravitynextdotinvert < 0)
            {
                indexofmaxvalueofperfacegravitynextdotinvert *= -1;
                indexofmaxvalueofperfacegravitynextdotinvert = indexofmaxvalueofperfacegravitynextdotinvert + 1.0f;
            }




            //Debug.Log("dotinvert0:" + indexofmaxvalueofperfacegravitydotinvert +"/nextdotinvert:" + +indexofmaxvalueofperfacegravitynextdotinvert);
            float resultabs = Mathf.Abs(indexofmaxvalueofperfacegravitydotinvert - indexofmaxvalueofperfacegravitynextdotinvert);
            //Debug.Log("dotinvert0:" + indexofmaxvalueofperfacegravitydotinvert + "/nextdotinvert:" + +indexofmaxvalueofperfacegravitynextdotinvert + "/abs:" + resultabs);

            //if (resultabs > 0.25f)
            {


                poscubicgravityvisualx = arrayofgravityperfacex[indexofmaxvalueofperfacegravity];
                poscubicgravityvisualy = arrayofgravityperfacey[indexofmaxvalueofperfacegravity];
                poscubicgravityvisualz = arrayofgravityperfacez[indexofmaxvalueofperfacegravity];


                targetobjectvisual.transform.position = new Vector3(poscubicgravityvisualx * 5, poscubicgravityvisualy * 5, poscubicgravityvisualz * 5);



            }






            lastfacegravityindex = indexofmaxvalueofperfacegravity;



            /*if (maxindex <= maxindexnext)
            {
                indexofmaxvalueofperfacegravitynext = maxindexnext + 1;
            }
            else
            {
                indexofmaxvalueofperfacegravitynext = maxindexnext;
            }*/














            /*
            var indexofmax = listofnextgravity.IndexOf(listofnextgravity.Max());

           
            for (int i = 0; i < listofnextgravity.Count; i++)
            {
                for (int j = 1; j < listofnextgravity.Count - 1; j++)
                {

                }
            }*/




















            //var listwithnextgravity = listofnextgravity.Remove(arrayofgravityperfacedot.Max());

            /*
            listofnextgravity.SelectMany()


            if ()
            {

            }*/

            //var val = listofnextgravity.IndexOf();

            /*var val = listofnextgravity.Skip(listofnextgravity.IndexOf(listofnextgravity.Max()) - 2).Take(1).ToList();
            //val[0]


            var anotherindex = listofnextgravity.IndexOf(val[0]);

            indexofmaxvalueofperfacegravitynext = anotherindex;// listofnextgravity.IndexOf(listofnextgravity.Max());
            */






            //indexofmaxvalueofperfacegravitynext

            //UnityEngine.////Debug.Log("typeofface:" + indexofmaxvalueofperfacegravity + "/facetypenext:" + indexofmaxvalueofperfacegravitynext);

            /*
            var listofnextgravityx = arrayofgravityperfacex.ToList();
            var listwithnextgravityx = listofnextgravityx.Remove(indexofmaxvalueofperfacegravitynext);
            */






            //indexofmaxvalueofperfacegravitylast = indexofmaxvalueofperfacegravity;



            //topface 0 1 0
            //frontface 0 0 1
            //rightface 1 0 0
            //leftface -1 0 0
            //backface 0 0 -1
            //bottomface 0 -1 0


















            //scmaths.trying_ellipsoid_with_sc_sebastian_lague_check_distanceconvertedto3dkinda();

            /*
            if (dirrationormx > 0.3333333337f && dirrationormy > 0.3333333337f && dirrationormz > 0.3333333337f)
            {
                answerx = 1;
                answery = 1;
                answerz = 1;
            }
            else if (dirrationormx < -0.3333333337f && dirrationormy < -0.3333333337f && dirrationormz > 0.3333333337f)
            {
                answerx = -1;
                answery = -1;
                answerz = 1;
            }
            else if (dirrationormx < -0.3333333337f && dirrationormy > 0.3333333337f && dirrationormz > 0.3333333337f)
            {
                answerx = -1;
                answery = 1;
                answerz = 1;
            }
            else if (dirrationormx > 0.3333333337f && dirrationormy < -0.3333333337f && dirrationormz > 0.3333333337f)
            {
                answerx = 1;
                answery = -1;
                answerz = 1;
            }
            else if (dirrationormx > 0.3333333337f && dirrationormy < -0.3333333337f && dirrationormz < -0.3333333337f)
            {
                answerx = 1;
                answery = -1;
                answerz = -1;
            }
            else if (dirrationormx < -0.3333333337f && dirrationormy < -0.3333333337f && dirrationormz < -0.3333333337f)
            {
                answerx = -1;
                answery = -1;
                answerz = -1;
            }
            else if (dirrationormx < -0.3333333337f && dirrationormy > 0.3333333337f && dirrationormz < -0.3333333337f)
            {
                answerx = -1;
                answery = 1;
                answerz = -1;
            }
            else if (dirrationormx > 0.3333333337f && dirrationormy > 0.3333333337f && dirrationormz < -0.3333333337f)
            {
                answerx = 1;
                answery = 1;
                answerz = -1;
            }

            */






            /*
            if (dirrationormx > 0.3333333337f && dirrationormy > 0.3333333337f && dirrationormz > 0.3333333337f)
            {
                answerx = 1;
                answery = 1;
                answerz = 1;
            }
            else if (dirrationormx < -0.3333333337f && dirrationormy < -0.3333333337f && dirrationormz > 0.3333333337f)
            {
                answerx = -1;
                answery = -1;
                answerz = 1;
            }
            else if (dirrationormx < -0.3333333337f && dirrationormy > 0.3333333337f && dirrationormz > 0.3333333337f)
            {
                answerx = -1;
                answery = 1;
                answerz = 1;
            }
            else if (dirrationormx > 0.3333333337f && dirrationormy < -0.3333333337f && dirrationormz > 0.3333333337f)
            {
                answerx = 1;
                answery = -1;
                answerz = 1;
            }
            else if (dirrationormx > 0.3333333337f && dirrationormy < -0.3333333337f && dirrationormz < -0.3333333337f)
            {
                answerx = 1;
                answery = -1;
                answerz = -1;
            }
            else if (dirrationormx < -0.3333333337f && dirrationormy < -0.3333333337f && dirrationormz < -0.3333333337f)
            {
                answerx = -1;
                answery = -1;
                answerz = -1;
            }
            else if (dirrationormx < -0.3333333337f && dirrationormy > 0.3333333337f && dirrationormz < -0.3333333337f)
            {
                answerx = -1;
                answery = 1;
                answerz = -1;
            }
            else if (dirrationormx > 0.3333333337f && dirrationormy > 0.3333333337f && dirrationormz < -0.3333333337f)
            {
                answerx = 1;
                answery = 1;
                answerz = -1;
            }*/











            hasgotgravitydot = 1;

        }



        /*


        if (answerx == 1 && answery == -1 && answerz == -1)
        {
            ////Debug.Log("answerx == 1 && answery == -1 && answerz == -1");
        }
        else if (answerx == 1 && answery == 1 && answerz == -1)
        {
            ////Debug.Log("answerx == 1 && answery == 1 && answerz == -1");
        }
        else if (answerx == 1 && answery == 1 && answerz == 1)
        {
            ////Debug.Log("answerx == 1 && answery == 1 && answerz == 1");
        }
        else if (answerx == -1 && answery == 1 && answerz == 1)
        {
            ////Debug.Log("answerx == -1 && answery == 1 && answerz == 1");
        }
        else if (answerx == -1 && answery == -1 && answerz == 1)
        {
            ////Debug.Log("answerx == -1 && answery == -1 && answerz == 1");
        }
        else if (answerx == 1 && answery == -1 && answerz == 1)
        {
            ////Debug.Log("answerx == 1 && answery == -1 && answerz == 1");
        }
        else if (answerx == 1 && answery == 1 && answerz == -1)
        {
            ////Debug.Log("answerx == 1 && answery == 1 && answerz == -1");
        }
        else if (answerx == -1 && answery == -1 && answerz == -1)
        {
            ////Debug.Log("answerx == -1 && answery == -1 && answerz == -1");
        }


        targetobjectvisual.transform.position = new Vector3(answerz * 5, answery * 5, answerx * 5);

        */








        /*
        if (swtchwaypointtype == 0)
        {
            Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.right.x, compasspivot.transform.right.y);
            dirbulletprimerright.Normalize();

            Vector2 dirprimertonorthpoletransform = new Vector2(northpoletransform.position.x, northpoletransform.position.y) - new Vector2(compasspivot.position.x, compasspivot.position.y);
            dirprimertonorthpoletransform.Normalize();

            _dotGoal = sc_maths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);


            if (_dotGoal >= 0) // 0.001f
            {
                answer = 1;
            }
            else if (_dotGoal < 0) //-0.001f
            {
                answer = -1;
            }
        }
        else if (swtchwaypointtype == 1)
        {
            Vector2 dirbulletprimerright = new Vector2(-compasspivot.transform.right.z, compasspivot.transform.right.x);
            dirbulletprimerright.Normalize();

            Vector2 dirprimertonorthpoletransform = new Vector2(northpoletransform.position.x, northpoletransform.position.z) - new Vector2(compasspivot.position.x, compasspivot.position.z);
            dirprimertonorthpoletransform.Normalize();

            _dotGoal = sc_maths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);

            if (_dotGoal >= 0) // 0.001f
            {
                answer = 1;
            }
            else if (_dotGoal < 0)//-0.001f
            {
                answer = -1;
            }
        }
        else if (swtchwaypointtype == 2)
        {
            Vector2 dirbulletprimerright = new Vector2(compasspivot.transform.forward.z, compasspivot.transform.forward.y);
            dirbulletprimerright.Normalize();

            Vector2 dirprimertonorthpoletransform = new Vector2(compasspivot.position.z, compasspivot.position.y) - new Vector2(northpoletransform.position.z, northpoletransform.position.y);
            dirprimertonorthpoletransform.Normalize();

            _dotGoal = sc_maths.Dot(dirbulletprimerright.x, dirbulletprimerright.y, dirprimertonorthpoletransform.x, dirprimertonorthpoletransform.y);
            /*
            if (_dotGoal >= 0 && _dotGoal < 0.50f) // 0.001f
            {
                answer = 1;
            }
            else if (_dotGoal > -0.50f && _dotGoal < 0) //-0.001f
            {
                answer = -1;
            }

            if (_dotGoal >= 0) // 0.001f
            {
                answer = 1;
            }
            else if (_dotGoal < 0) //-0.001f
            {
                answer = -1;
            }

            //////Debug.Log("dot:" + _dotGoal);
        }*/
    }












    public void setcubicrotation(int indexofmaxvalueofperfacegravity, int indexofmaxvalueofperfacegravitylast, GameObject viewer)
    {

        //if (Mathf.Abs(Mathf.Abs(indexofmaxvalueofperfacegravitydotinvert) - Mathf.Abs(indexofmaxvalueofperfacegravitynextdotinvert)) > 0.5f)


        Vector3 faceposcubic = Vector3.zero;

        if (indexofmaxvalueofperfacegravity == 0 && indexofmaxvalueofperfacegravitylast != indexofmaxvalueofperfacegravity) //t
        {

            Vector3 eulerangles = new Vector3(viewer.transform.localEulerAngles.x, viewer.transform.localEulerAngles.y, viewer.transform.localEulerAngles.z);

            Quaternion q = viewer.transform.rotation;

            float x = q.x;
            float y = q.y;
            float z = q.z;
            float w = q.w;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


            float pitchdeg = scmaths.RadianToDegree(pitchcurrent);
            float yawdeg = scmaths.RadianToDegree(yawcurrent);
            float rolldeg = scmaths.RadianToDegree(rollcurrent);

            ////Debug.Log("pitch:" + pitchdeg);
            //Debug.Log("yawdeg start:" + yawdeg);
            //Debug.Log("rolldeg:" + rolldeg);

            float altyaw = 0;

            if (indexofmaxvalueofperfacegravitylast == 1)
            {
                altyaw = yawdeg;
            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                altyaw = yawdeg;
            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                altyaw = yawdeg;
            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {
                altyaw = yawdeg;
            }




            if (pitchdeg >= 0)
            {
                if (yawdeg >= 0 & yawdeg < 90)
                {
                    altyaw = yawdeg;
                }

                if (yawdeg < 0 & yawdeg >= -90)
                {
                    altyaw = yawdeg;

                    altyaw *= -1;

                    altyaw = 360 - altyaw;
                }
            }

            if (pitchdeg < 0)
            {
                if (yawdeg >= 0 & yawdeg < 90)
                {
                    altyaw = yawdeg;

                    altyaw = 180 - altyaw;
                }

                if (yawdeg < 0 & yawdeg >= -90)
                {
                    altyaw = yawdeg;

                    altyaw *= -1;

                    altyaw = 180 + altyaw;
                }
            }


            if (altyaw >= 0 && altyaw < 90 ||
                altyaw >= 90 && altyaw < 180)
            {
                eulerangles.x = 0;
                eulerangles.z = 0;
            }
            else
            {
                eulerangles.x = 0;
                eulerangles.z = 0;
            }





            if (indexofmaxvalueofperfacegravitylast == 1)
            {
                viewer.transform.rotation = Quaternion.Euler(0, altyaw - 90, 0);
            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                viewer.transform.rotation = Quaternion.Euler(0, altyaw + 90, 0);
            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                viewer.transform.rotation = Quaternion.Euler(0, altyaw, 0);
            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {
                viewer.transform.rotation = Quaternion.Euler(0, altyaw + 180, 0);
            }



            faceposcubic = Vector3.up;
        }
        else if (indexofmaxvalueofperfacegravity == 1 && indexofmaxvalueofperfacegravity != indexofmaxvalueofperfacegravitylast
        )
        //l
        {

            Vector3 eulerangles = new Vector3(viewer.transform.localEulerAngles.x, viewer.transform.localEulerAngles.y, viewer.transform.localEulerAngles.z);


            Quaternion q = viewer.transform.rotation;

            float x = q.x;
            float y = q.y;
            float z = q.z;
            float w = q.w;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


            float pitchdeg = scmaths.RadianToDegree(pitchcurrent);
            float yawdeg = scmaths.RadianToDegree(yawcurrent);
            float rolldeg = scmaths.RadianToDegree(rollcurrent);

            //Debug.Log("pitchdeg:" + pitchdeg);
            //Debug.Log("yawdeg:" + yawdeg);
            //Debug.Log("rolldeg:" + rolldeg);

            float altyaw = 0;



            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                altyaw = rolldeg;

                if (pitchdeg >= 0)
                {



                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }


                }


            }

            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                altyaw = yawdeg;//

                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }



            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {

                altyaw = yawdeg;//

                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }



            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {
                altyaw = rolldeg;//



                if (pitchdeg >= 0)
                {

                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                }

                if (pitchdeg < 0)
                {
                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }


                }

            }



            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                if (altyaw >= 0 && altyaw < 90 ||
                 altyaw >= 90 && altyaw < 180)
                {
                    //Debug.Log("here00");


                    if (altyaw >= 0 && altyaw < 90)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = 0;
                        //Debug.Log("here0");
                        altyaw = 90 - altyaw;
                        altyaw += 90;
                        altyaw += 180;
                    }
                    else if (altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = 0;
                        //Debug.Log("here1");
                        altyaw = 180 - altyaw;
                        altyaw += 180;
                    }
                    else
                    {
                        //Debug.Log("here111");


                    }
                }
                else
                {

                    eulerangles.z = 90;
                    eulerangles.y = 0;

                    if (altyaw >= 180 && altyaw < 270)
                    {
                        //Debug.Log("here2");
                        altyaw = 270 - altyaw;
                        altyaw += 90;
                    }
                    else if (altyaw >= 270 && altyaw < 360)
                    {
                        //Debug.Log("here3");
                        altyaw = 360 - altyaw;


                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {

                if (pitchdeg >= 0)
                {



                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;

                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                            altyaw -= 90;

                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw += 180;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw += 90;


                        }
                    }
                }
                else
                {

                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {


                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;

                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                            altyaw -= 90;

                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;

                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw += 180;

                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;

                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw += 90;


                        }
                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {








                if (pitchdeg >= 0)
                {



                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = 0;
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;

                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                            altyaw -= 90;

                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw += 180;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw += 90;
                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = 0;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;

                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                            altyaw -= 90;

                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;

                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw += 180;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;

                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw += 90;

                        }
                    }
                }


            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {


                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                 altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = 0;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw += 180;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");

                            altyaw = 180 - altyaw;
                            altyaw -= 90;
                            altyaw = 90 - altyaw;
                            altyaw += 180;

                        }
                    }
                    else
                    {

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("reached2");

                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                            altyaw += 180;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //eulerangles.x = 90;
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("reached3");

                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw -= 90;
                            altyaw += 180;

                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {
                        //eulerangles.x = 90;
                        eulerangles.z = 90;
                        eulerangles.y = 0;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw += 180;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");

                            altyaw = 180 - altyaw;
                            altyaw -= 90;
                            altyaw = 90 - altyaw;

                            altyaw += 180;
                        }
                    }
                    else
                    {

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            eulerangles.z = 90;
                            eulerangles.y = 0;

                            altyaw = 270 - altyaw;
                            altyaw = 90 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;

                            //Debug.Log("reached3");

                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 90;

                        }
                    }
                }

            }






            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);

            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);

            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);

            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);
            }

            faceposcubic = -Vector3.right;
        }
        else if (indexofmaxvalueofperfacegravity == 2 && indexofmaxvalueofperfacegravitylast != indexofmaxvalueofperfacegravity) //r
        {

            Vector3 eulerangles = new Vector3(viewer.transform.localEulerAngles.x, viewer.transform.localEulerAngles.y, viewer.transform.localEulerAngles.z);


            Quaternion q = viewer.transform.rotation;

            float x = q.x;
            float y = q.y;
            float z = q.z;
            float w = q.w;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


            float pitchdeg = scmaths.RadianToDegree(pitchcurrent);
            float yawdeg = scmaths.RadianToDegree(yawcurrent);
            float rolldeg = scmaths.RadianToDegree(rollcurrent);

            //Debug.Log("pitchdeg:" + pitchdeg);
            //Debug.Log("yawdeg:" + yawdeg);
            //Debug.Log("rolldeg:" + rolldeg);

            float altyaw = 0;



            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                altyaw = rolldeg;//

                if (pitchdeg >= 0)
                {



                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (rolldeg >= 0 & rolldeg < 90)
                    {
                        altyaw = rolldeg;
                        altyaw = 180 - altyaw;
                    }

                    if (rolldeg < 0 & rolldeg >= -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }


                }



            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {

                altyaw = yawdeg;//

                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }



            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {


                altyaw = yawdeg;//

                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {
                altyaw = rolldeg;//


                if (pitchdeg >= 0)
                {



                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }


                }
            }




            //Debug.Log("altyaw0:" + altyaw);




            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                if (altyaw >= 0 && altyaw < 90 ||
                 altyaw >= 90 && altyaw < 180)
                {
                    eulerangles.z = -90;
                    eulerangles.y = 0;

                    if (altyaw >= 0 && altyaw < 90)
                    {
                        //Debug.Log("here0");

                    }
                    else if (altyaw >= 90 && altyaw < 180)
                    {
                        //Debug.Log("here1");
                        altyaw = 180 - altyaw;
                        altyaw -= 90;
                        altyaw = 90 - altyaw;


                    }
                }
                else
                {

                    if (altyaw >= 180 && altyaw < 270)
                    {
                        eulerangles.z = -90;
                        eulerangles.y = 0;
                        //Debug.Log("here2");
                        altyaw = 270 - altyaw;

                        altyaw = 90 - altyaw;
                        altyaw += 180;
                    }
                    else if (altyaw >= 270 && altyaw < 360)
                    {
                        eulerangles.z = -90;
                        eulerangles.y = 0;

                        //Debug.Log("here3");
                        altyaw = 360 - altyaw;

                        altyaw = 90 - altyaw;
                        altyaw += 180;
                        altyaw += 90;

                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                if (pitchdeg >= 0)
                {

                    if (altyaw >= 0 && altyaw < 90 ||
                altyaw >= 90 && altyaw < 180)
                    {

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;

                        }
                    }
                    else
                    {

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                            altyaw += 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;

                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {


                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                            altyaw = 90 - altyaw;

                            altyaw += 180;

                        }
                    }
                    else
                    {


                        if (altyaw >= 180 && altyaw < 270)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                            altyaw += 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;

                        }
                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {

                if (pitchdeg >= 0)
                {

                    if (altyaw >= 0 && altyaw < 90 ||
                altyaw >= 90 && altyaw < 180)
                    {

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;

                        }
                    }
                    else
                    {
                        eulerangles.z = -90;
                        eulerangles.y = 0;
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                            altyaw += 90;

                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;
                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {


                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                            altyaw = 90 - altyaw;

                            altyaw += 180;

                        }
                    }
                    else
                    {
                        eulerangles.z = -90;
                        eulerangles.y = 0;

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                            altyaw += 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;
                        }
                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {




                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                 altyaw >= 90 && altyaw < 180)
                    {

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("reached0");

                            altyaw = 90 - altyaw;
                            altyaw += 90;

                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("reached1");

                            altyaw = 180 - altyaw;

                        }
                    }
                    else
                    {
                        eulerangles.z = -90;
                        eulerangles.y = 0;


                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");

                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");

                            altyaw = 360 - altyaw;
                            altyaw += 180;

                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("reached0");

                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("reached1");

                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        eulerangles.z = -90;
                        eulerangles.y = 0;


                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");

                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");

                            altyaw = 360 - altyaw;
                            altyaw += 180;

                        }
                    }
                }

            }


            //Debug.Log("altyaw1:" + altyaw);

            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);

            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);

            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);

            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);
            }




            faceposcubic = Vector3.right;
        }
        else if (indexofmaxvalueofperfacegravity == 3 && indexofmaxvalueofperfacegravitylast != indexofmaxvalueofperfacegravity) //fr
        {
            Vector3 eulerangles = new Vector3(viewer.transform.rotation.eulerAngles.x, viewer.transform.rotation.eulerAngles.y, viewer.transform.rotation.eulerAngles.z);

            Quaternion q = viewer.transform.rotation;

            float x = q.x;
            float y = q.y;
            float z = q.z;
            float w = q.w;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


            float pitchdeg = scmaths.RadianToDegree(pitchcurrent);
            float yawdeg = scmaths.RadianToDegree(yawcurrent);
            float rolldeg = scmaths.RadianToDegree(rollcurrent);
            //yawdeg = rolldeg;


            //Debug.Log("pitch:" + pitchdeg);
            //Debug.Log("yawdeg:" + yawdeg);
            //Debug.Log("rolldeg:" + rolldeg);

            //float altyaw = (360 - yawdeg) - 90;
            float altyaw = 0;// yawdeg;

            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                altyaw = rolldeg;//
                if (pitchdeg >= 0)
                {

                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                }

                if (pitchdeg < 0)
                {
                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 1)
            {
                altyaw = yawdeg;//
                if (pitchdeg >= 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                    }


                    if (yawdeg < -90 & yawdeg >= -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

                if (pitchdeg < 0)
                {


                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                    }


                    if (yawdeg < -90 & yawdeg >= -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                altyaw = yawdeg;//


                if (pitchdeg >= 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                    }


                    if (yawdeg < -90 & yawdeg >= -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

                if (pitchdeg < 0)
                {


                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                    }


                    if (yawdeg < -90 & yawdeg >= -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {
                altyaw = rolldeg;//

                if (pitchdeg >= 0)
                {
                    if (rolldeg >= 0 & rolldeg < 90)
                    {
                        altyaw = rolldeg;
                    }


                    if (rolldeg < -90 & rolldeg >= -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (rolldeg < 0 & rolldeg >= -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

                if (pitchdeg < 0)
                {


                    if (rolldeg >= 0 & rolldeg < 90)
                    {
                        altyaw = rolldeg;
                    }


                    if (rolldeg < -90 & rolldeg >= -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (rolldeg < 0 & rolldeg >= -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }
            }



            //Debug.Log("altyaw:" + altyaw);


            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                {
                    eulerangles.z = 90;
                    eulerangles.y = 90;

                    if (altyaw >= 0 && altyaw < 90)
                    {
                        //Debug.Log("here0");
                        altyaw = 90 - altyaw;
                    }
                    else if (altyaw >= 90 && altyaw < 180)
                    {
                        //Debug.Log("here1");
                        altyaw = 180 - altyaw;
                        altyaw += 270;
                    }
                }
                else
                {


                    eulerangles.z = -90;
                    eulerangles.y = -90;


                    if (altyaw >= 180 && altyaw < 270)
                    {
                        //Debug.Log("here2");
                        altyaw = 270 - altyaw;
                        altyaw = 90 - altyaw;
                        altyaw += 270;

                    }
                    else if (altyaw >= 270 && altyaw < 360)
                    {
                        //Debug.Log("here3");
                        altyaw = 360 - altyaw;

                        altyaw = 90 - altyaw;

                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 1)
            {
                if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                {

                    if (altyaw >= 0 && altyaw < 90)
                    {
                        if (rolldeg >= 0)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 90;
                            //Debug.Log("here00");
                            altyaw -= 90;
                        }
                        else
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 90;
                            //Debug.Log("here01");

                            altyaw = 90 - altyaw;
                        }

                    }
                    else if (altyaw >= 90 && altyaw < 180)
                    {
                        //Debug.Log("here1");
                        altyaw = 180 - altyaw;
                        altyaw -= 90;

                    }
                }
                else
                {


                    eulerangles.z = -90;
                    eulerangles.y = -90;


                    if (altyaw >= 180 && altyaw < 270)
                    {

                        altyaw = 270 - altyaw;

                        if (rolldeg >= 0)
                        {
                            //Debug.Log("here21");
                            altyaw = 90 - altyaw;
                            altyaw += 270;


                        }
                        else
                        {
                            //Debug.Log("here22");
                        }

                    }
                    else if (altyaw >= 270 && altyaw < 360)
                    {
                        //Debug.Log("here3");
                        altyaw = 360 - altyaw;

                        altyaw = 90 - altyaw;

                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {

                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = 90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {


                        eulerangles.z = -90;
                        eulerangles.y = -90;

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw -= 270;
                            altyaw = 90 - altyaw;
                            altyaw += 90;

                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = 90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("here0");
                            altyaw -= 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        eulerangles.z = -90;
                        eulerangles.y = -90;

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                            altyaw = 270 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw -= 270;
                            altyaw = 90 - altyaw;
                            altyaw += 90;

                        }
                    }
                }

            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {

                if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                {
                    eulerangles.z = 90;
                    eulerangles.y = 90;

                    if (altyaw >= 0 && altyaw < 90)
                    {
                        //Debug.Log("reached0");
                        altyaw += 90;
                    }
                    else if (altyaw >= 90 && altyaw < 180)
                    {
                        if (rolldeg >= 0)
                        {
                            //Debug.Log("reached12");
                            altyaw = 180 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 180;

                        }
                        else
                        {
                            //Debug.Log("reached13");
                            altyaw = 180 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                            altyaw += 90;
                        }

                    }
                }
                else
                {


                    eulerangles.z = -90;
                    eulerangles.y = -90;

                    if (altyaw >= 180 && altyaw < 270)
                    {


                        //Debug.Log("reached2");
                        altyaw = 270 - altyaw;
                        altyaw = 90 - altyaw;
                        if (rolldeg >= 0)
                        {
                            //Debug.Log("reached21");
                        }
                        else
                        {
                            altyaw += 90;

                            //Debug.Log("reached22");
                        }

                    }
                    else if (altyaw >= 270 && altyaw < 360)
                    {
                        //Debug.Log("reached3");
                        altyaw = 360 - altyaw;

                        altyaw = 90 - altyaw;

                    }
                }
            }







            viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);


            faceposcubic = Vector3.forward;
        }
        else if (indexofmaxvalueofperfacegravity == 4 && indexofmaxvalueofperfacegravitylast != indexofmaxvalueofperfacegravity)
        //ba
        {
            Vector3 eulerangles = new Vector3(viewer.transform.rotation.eulerAngles.x, viewer.transform.rotation.eulerAngles.y, viewer.transform.rotation.eulerAngles.z);

            Quaternion q = viewer.transform.rotation;

            float x = q.x;
            float y = q.y;
            float z = q.z;
            float w = q.w;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


            float pitchdeg = scmaths.RadianToDegree(pitchcurrent);
            float yawdeg = scmaths.RadianToDegree(yawcurrent);
            float rolldeg = scmaths.RadianToDegree(rollcurrent);
            //yawdeg = rolldeg;

            //Debug.Log("pitch:" + pitchdeg);
            //Debug.Log("yawdeg:" + yawdeg);
            //Debug.Log("rolldeg:" + rolldeg);

            //float altyaw = (360 - yawdeg) - 90;
            float altyaw = 0;// yawdeg;



            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                altyaw = rolldeg;//
                yawdeg = rolldeg;


                if (pitchdeg >= 0)
                {
                    if (rolldeg >= 0 & rolldeg < 90)
                    {
                        altyaw = rolldeg;
                    }

                    if (rolldeg < 0 & rolldeg >= -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 360 - altyaw;

                    }

                    if (rolldeg < -90 & rolldeg >= -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 360 - altyaw;
                    }


                }

                if (pitchdeg < 0)
                {
                    if (rolldeg >= 0 & rolldeg < 90)
                    {
                        altyaw = rolldeg;
                        altyaw = 180 - altyaw;
                    }

                    if (rolldeg < 0 & rolldeg >= -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

            }
            else if (indexofmaxvalueofperfacegravitylast == 1)
            {
                altyaw = yawdeg;//


                if (pitchdeg >= 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 360 - altyaw;

                    }
                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }
                }


            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                altyaw = yawdeg;//


                if (pitchdeg >= 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 360 - altyaw;

                    }
                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }
                }

            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {
                altyaw = rolldeg;//

                if (pitchdeg >= 0)
                {
                    if (rolldeg >= 0 & rolldeg < 90)
                    {
                        altyaw = rolldeg;
                    }


                    if (rolldeg < -90 & rolldeg >= -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (rolldeg < 0 & rolldeg >= -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

                if (pitchdeg < 0)
                {


                    if (rolldeg >= 0 & rolldeg < 90)
                    {
                        altyaw = rolldeg;
                    }


                    if (rolldeg < -90 & rolldeg >= -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (rolldeg < 0 & rolldeg >= -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

            }









            //Debug.Log("altyaw0:" + altyaw);

            if (indexofmaxvalueofperfacegravitylast == 0)
            {


                if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                {
                    //Debug.Log("here00");
                    eulerangles.z = 90;
                    eulerangles.y = -90;

                    if (altyaw >= 0 && altyaw < 90)
                    {
                        //Debug.Log("here0");
                        altyaw = 90 - altyaw;
                        altyaw += 180;//

                    }
                    else if (altyaw >= 90 && altyaw < 180)
                    {
                        //Debug.Log("here2");
                        altyaw = 180 - altyaw;
                        altyaw += 90;//

                    }
                }
                else
                {
                    //Debug.Log("here11");

                    eulerangles.z = 90;
                    eulerangles.y = -90;


                    if (altyaw >= 180 && altyaw < 270)
                    {
                        //Debug.Log("here2");//
                        altyaw = 270 - altyaw;

                    }
                    else if (altyaw >= 270 && altyaw < 360)
                    {
                        //Debug.Log("here3");
                        altyaw = 360 - altyaw;
                        altyaw += 270;//
                    }
                }

            }
            else if (indexofmaxvalueofperfacegravitylast == 1)
            {
                //Debug.Log("altyaw0:" + altyaw);

                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        ////Debug.Log("reached0");

                        eulerangles.z = 90;
                        eulerangles.y = -90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached00");
                            altyaw = 90 - altyaw;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached01");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        ////Debug.Log("reached1");


                        eulerangles.z = 90;
                        eulerangles.y = -90;


                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached02");

                            altyaw = 180 - altyaw;
                            altyaw += 180;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached03");

                            altyaw = 360 - altyaw;
                            altyaw += 90;
                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        ////Debug.Log("reached2");

                        eulerangles.z = 90;
                        eulerangles.y = -90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached21");

                            altyaw = 90 - altyaw;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached22");
                            altyaw = 180 - altyaw;
                            altyaw += 180;
                        }

                        altyaw += 90;
                    }
                    else
                    {

                        ////Debug.Log("reached3");

                        eulerangles.z = 90;
                        eulerangles.y = -90;

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached31");

                            altyaw = 270 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached32");
                            altyaw = 360 - altyaw;
                        }
                        altyaw += 180;



                    }
                }




            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                ////Debug.Log("altyaw0:" + altyaw);

                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        ////Debug.Log("reached0");
                        eulerangles.z = -90;
                        eulerangles.y = 90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached00");
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached01");
                            altyaw = 180 - altyaw;
                        }

                    }
                    else
                    {
                        ////Debug.Log("reached1");

                        eulerangles.z = -90;
                        eulerangles.y = 90;

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached02");

                            altyaw = 180 - altyaw;
                            altyaw += 180;

                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached03");

                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;

                        }



                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        ////Debug.Log("reached2");

                        eulerangles.z = -90;
                        eulerangles.y = 90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached21");

                            altyaw = 90 - altyaw;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached22");

                            altyaw = 180 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                        }

                    }
                    else
                    {

                        ////Debug.Log("reached3");

                        eulerangles.z = -90;
                        eulerangles.y = 90;

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached31");

                            altyaw = 270 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached32");
                            altyaw = 360 - altyaw;

                        }
                        altyaw += 180;



                    }
                }


            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {


                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = -90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw -= 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            if (rolldeg >= 0)
                            {
                                //Debug.Log("reached1");
                                altyaw = 180 - altyaw;
                                altyaw = 90 - altyaw;
                            }
                            else
                            {
                                //Debug.Log("reached1");
                                altyaw = 180 - altyaw;
                                altyaw = 90 - altyaw;
                                altyaw += 90;
                            }

                        }
                    }
                    else
                    {


                        eulerangles.z = 90;
                        eulerangles.y = -90;


                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                            altyaw += 180;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;
                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = -90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw -= 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");

                            if (rolldeg >= 0)
                            {
                                //Debug.Log("reached11");
                                altyaw -= 90;

                            }
                            else
                            {
                                //Debug.Log("reached12");
                                altyaw = 180 - altyaw;
                                altyaw = 90 - altyaw;
                                altyaw += 90;
                            }
                        }
                    }
                    else
                    {


                        eulerangles.z = 90;
                        eulerangles.y = -90;


                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                            altyaw += 180;

                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;
                        }
                    }
                }

            }



            ////Debug.Log("altyaw:" + altyaw);




            viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z); //Quaternion.Euler(eulerangles);



            faceposcubic = -Vector3.forward;
        }
        else if (indexofmaxvalueofperfacegravity == 5 && indexofmaxvalueofperfacegravitylast != indexofmaxvalueofperfacegravity) //bo
        {
            Vector3 eulerangles = new Vector3(viewer.transform.localEulerAngles.x, viewer.transform.localEulerAngles.y, viewer.transform.localEulerAngles.z);

            Quaternion q = viewer.transform.rotation;

            float x = q.x;
            float y = q.y;
            float z = q.z;
            float w = q.w;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


            float pitchdeg = scmaths.RadianToDegree(pitchcurrent);
            float yawdeg = scmaths.RadianToDegree(yawcurrent);
            float rolldeg = scmaths.RadianToDegree(rollcurrent);

            //Debug.Log("pitch:" + pitchdeg);
            //Debug.Log("yawdeg start:" + yawdeg);
            //Debug.Log("rolldeg:" + rolldeg);

            float altyaw = 0;

            if (indexofmaxvalueofperfacegravitylast == 1)
            {
                altyaw = yawdeg;

                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }


                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                altyaw = yawdeg;

                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                altyaw = yawdeg;


                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }


                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {
                altyaw = yawdeg;

                altyaw = yawdeg;


                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

            }


















            //Debug.Log("altyaw0:" + altyaw);


            if (altyaw >= 0 && altyaw < 90 ||
                altyaw >= 90 && altyaw < 180)
            {
                eulerangles.x = 0;
                eulerangles.z = 180;
            }
            else
            {
                eulerangles.x = 0;
                eulerangles.z = 180;
            }



            if (indexofmaxvalueofperfacegravitylast == 1)
            {
                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;
                        }
                    }


                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;

                        }
                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;
                        }
                    }


                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;
                        }
                    }

                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;
                        }
                    }


                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;

                        }
                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {
                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;

                        }
                    }


                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;

                        }
                    }
                }
            }

            if (indexofmaxvalueofperfacegravitylast == 1)
            {
                viewer.transform.rotation = Quaternion.Euler(eulerangles.x, altyaw - 90, eulerangles.z);
            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                viewer.transform.rotation = Quaternion.Euler(eulerangles.x, altyaw + 90, eulerangles.z);
            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                viewer.transform.rotation = Quaternion.Euler(eulerangles.x, altyaw, eulerangles.z);
            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {
                viewer.transform.rotation = Quaternion.Euler(eulerangles.x, altyaw + 180, eulerangles.z);
            }
            faceposcubic = -Vector3.up;
        }


    }




    IEnumerator RotatePlayerWithKeyboard()
    {
        


        if (canmovecamera == 1)
        {
           

        }


        //hmdmatrixRot = viewer.transform.rotation;

        float rotationincrements = 25.75f;

        if (Input.GetKey(KeyCode.A))
        {

            if (theplanet != null)
            {
                Vector3 upcoreplayer = cubicgravityvector;// viewer.transform.position - theplanet.transform.position;
                upcoreplayer.Normalize();

                rotationincrements = -2.5f;
                viewer.transform.RotateAround(viewer.transform.position, upcoreplayer, rotationincrements);
            }
            else
            {
                ////Debug.Log("the planet is null");

            }
            

        }
        //* Time.deltaTime
        if (Input.GetKey(KeyCode.D))
        {
            if (theplanet != null)
            {
                Vector3 upcoreplayer = cubicgravityvector;// viewer.transform.position - theplanet.transform.position;
                upcoreplayer.Normalize();

                rotationincrements = +2.5f;
                viewer.transform.RotateAround(viewer.transform.position, upcoreplayer, rotationincrements);

            }
            else
            {
                ////Debug.Log("the planet is null");

            }

          

        }



        if (Input.GetKey(KeyCode.T))
        {
            
            upperbodypivotRotationX += rotationincrements;
            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = upperbodypivotRotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = upperbodypivotRotationZ * 0.0174532925f;


            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            //upperbodypivot.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            upperbodypivot.transform.rotation = viewer.transform.rotation * Quaternion.Euler(pitch, yaw, roll);


            //Matrix4x4.Rotate
        }

        if (Input.GetKey(KeyCode.G))
        {
            
            upperbodypivotRotationX -= rotationincrements;
            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = upperbodypivotRotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = upperbodypivotRotationZ * 0.0174532925f;
            

            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            //upperbodypivot.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            //Matrix4x4.Rotate
            upperbodypivot.transform.rotation = viewer.transform.rotation * Quaternion.Euler(pitch, yaw, roll);

        }

        //getPan

        yield return new WaitForSeconds(0.001f);
    }

}
