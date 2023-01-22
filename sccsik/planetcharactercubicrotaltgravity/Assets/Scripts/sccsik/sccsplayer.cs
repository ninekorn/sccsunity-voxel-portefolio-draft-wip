//using SCCoreSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.XR;
using System.Security.Cryptography;
using System.Linq;
using UnityEngine.SocialPlatforms.Impl;
using System.Diagnostics;

public class sccsplayer : MonoBehaviour
{
    float indexofmaxvalueofperfacegravitydot = 0.0f;
    float indexofmaxvalueofperfacegravitynextdot = 0.0f;
    int indexofmaxvalueofperfacegravitynext = -1;
    int indexofmaxvalueofperfacegravitylast = -1;
    int indexofmaxvalueofperfacegravity = -1;

    int lastfacegravityindex = -1;


    public float movementspeed = 15.0f;

    float isgroundedmaxdist = 0.70f;
    float hiptofloordist = 1.0f;

    public GameObject originalcamerapivot;
    public GameObject upperbodypivot;
    public GameObject headpivotpoint;
    public GameObject isgroundedpivotpoint;
    public GameObject pointertarget;

    public LayerMask layerMask;

    Quaternion finalrotationplayerpivot;

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

    // Start is called before the first frame update
    void Start()
    {



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
        hiptofloordist = (isgroundedpivotpoint.transform.position - pointertarget.transform.position).magnitude;
    }



    Vector3 cubicgravityvector;

    // Update is called once per frame
    void Update()
    {

        if (theplanet != null)
        {
            cubicgravityvector = new Vector3(poscubicgravityvisualx, poscubicgravityvisualy, poscubicgravityvisualz) - theplanet.transform.position;

        }

        //var planetdivright = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy - 1), (int)(mainChunk.mindexposz));

        findgravitygriddots();








        //isgrounded
        Ray ray = new Ray(isgroundedpivotpoint.transform.position, isgroundedpivotpoint.transform.forward);


        Debug.DrawRay(isgroundedpivotpoint.transform.position, isgroundedpivotpoint.transform.forward, Color.red, 1.0f);

        // isgroundedpivotpoint.transform.position;// Camera.main.ScreenPointToRay(isgroundedpivotpoint.transform.position);//Camera.main.ScreenPointToRay(Input.mousePosition);
        // Save the info

        RaycastHit theouthit;

        if (Physics.Raycast(ray, out theouthit, layerMask))
        {


            theplanet = GameObject.FindGameObjectWithTag("terrain");
            //theplanet = theouthit.transform.parent.transform.parent.gameObject;


            if (indexofmaxvalueofperfacegravity == 0)
            {

            }
            else if (indexofmaxvalueofperfacegravity == 1)
            {

            }
            else if (indexofmaxvalueofperfacegravity == 2)
            {

            }
            else if (indexofmaxvalueofperfacegravity == 3)
            {

            }
            else if (indexofmaxvalueofperfacegravity == 4)
            {

            }
            else if (indexofmaxvalueofperfacegravity == 5)
            {

            }

            Vector3 theplanettopointgravityUP = new Vector3(poscubicgravityvisualx, poscubicgravityvisualy, poscubicgravityvisualz) - theplanet.transform.position;
            theplanettopointgravityUP.Normalize();
            if (indexofmaxvalueofperfacegravitynextdot > indexofmaxvalueofperfacegravitydot * 0.985f)
            {
                if (indexofmaxvalueofperfacegravity != indexofmaxvalueofperfacegravitynext)
                {
                    if (indexofmaxvalueofperfacegravitynext == 0)
                    {
                        theplanettopointgravityUP = Vector3.up;
                    }
                    else if (indexofmaxvalueofperfacegravitynext == 1)
                    {
                        theplanettopointgravityUP = -Vector3.right;
                    }
                    else if (indexofmaxvalueofperfacegravitynext == 2)
                    {
                        theplanettopointgravityUP = Vector3.right;
                    }
                    else if (indexofmaxvalueofperfacegravitynext == 3)
                    {
                        theplanettopointgravityUP = Vector3.forward;
                    }
                    else if (indexofmaxvalueofperfacegravitynext == 4)
                    {
                        theplanettopointgravityUP = -Vector3.forward;
                    }
                    else if (indexofmaxvalueofperfacegravitynext == 5)
                    {
                        theplanettopointgravityUP = -Vector3.up;
                    }

                }
            }





            var distance = 0.00025f;
            //var dirForward = viewer.transform.rotation * Vector3.forward;
            var dirForward = viewer.transform.rotation * Vector3.forward;

            dirForward.Normalize();

            //var dirofnormal = Quaternion.identity * theouthit.normal;

            Vector3 positioninfrontofplayer = viewer.transform.position + (dirForward * distance);


            /*if (indexofmaxvalueofperfacegravitynextdot <= indexofmaxvalueofperfacegravitydot * 0.985f)
            {
                Vector3 diagonaldown = isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.up;// - isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.up;// new Vector3();

                Vector3 dirdiag = diagonaldown - isgroundedpivotpoint.transform.position;
                dirdiag.Normalize();

                //isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + isgroundedpivotpoint.transform.up

                Ray raychangegravity = new Ray(isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.up, dirdiag * 1.5f);

                RaycastHit theouthitgravitychange;

                if (Physics.Raycast(raychangegravity, out theouthitgravitychange, layerMask))
                {
                    positioninfrontofplayer = viewer.transform.position 
                }
            }*/






            //Vector3 positioninbackofplayer = viewer.transform.position + (-dirofnormal * distance);

            Vector3 dirplanetcoretoplayer = viewer.transform.position - theplanet.transform.position;
            float distcoretoplayer = dirplanetcoretoplayer.magnitude;

            Vector3 dirplanetcoretopointfrontofplayer = positioninfrontofplayer - theplanet.transform.position;
            //float distcoretopointinfrontofplayer = dirplanetcoretopointfrontofplayer.magnitude;
            dirplanetcoretopointfrontofplayer.Normalize();

            Vector3 alwaysuppointofplayercomparedtoplayercore = theplanet.transform.position + (dirplanetcoretopointfrontofplayer * (distcoretoplayer));// positioninfrontofplayer;// theplanet.transform.position + (dirplanetcoretopointfrontofplayer * (distcoretoplayer));

            Vector3 forwarddirtopointfrontplayer = positioninfrontofplayer - viewer.transform.position;// alwaysuppointofplayercomparedtoplayercore - viewer.transform.position;
            forwarddirtopointfrontplayer.Normalize();



            //Vector3 crossvecforward = Vector3.zero;




            //topfacegravity
            /*if (indexofmaxvalueofperfacegravity == 0)
            {
                crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.right);
            }
            else if (indexofmaxvalueofperfacegravity == 1) //l
            {
                crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.forward);
            }
            else if (indexofmaxvalueofperfacegravity == 2) //r
            {
                crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.forward);
            }
            else if (indexofmaxvalueofperfacegravity == 3) //f
            {
                crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.up);
            }
            else if (indexofmaxvalueofperfacegravity == 4) //ba
            {
                crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.up);
            }
            else if (indexofmaxvalueofperfacegravity == 5) //bo
            {
                crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.right);
            }
            */









            /*
            Vector3 crossvec = Vector3.Cross(theplanettopointgravityUP, forwarddirtopointfrontplayer);
            Vector3 crossvecForwardOfplanetgravity = Vector3.Cross(theplanettopointgravityUP, crossvec);
            */








            //Vector3 pointA = 


            /*if (indexofmaxvalueofperfacegravitynextdot > indexofmaxvalueofperfacegravitydot * 0.985f)
            {
                indexofmaxvalueofperfacegravity = indexofmaxvalueofperfacegravitynext;
            }*/

            //indexofmaxvalueofperfacegravity = indexofmaxvalueofperfacegravitynext;




            //forwarddirtopointfrontplayer += crossvec;

            if (indexofmaxvalueofperfacegravitynextdot > indexofmaxvalueofperfacegravitydot * 0.985f)
            {
                if (indexofmaxvalueofperfacegravity != indexofmaxvalueofperfacegravitynext)
                {

                }




                if (indexofmaxvalueofperfacegravitynext == 0)
                {
                    theplanettopointgravityUP = Vector3.up;
                }
                else if (indexofmaxvalueofperfacegravitynext == 1)
                {
                    theplanettopointgravityUP = -Vector3.right;
                }
                else if (indexofmaxvalueofperfacegravitynext == 2)
                {
                    theplanettopointgravityUP = Vector3.right;
                }
                else if (indexofmaxvalueofperfacegravitynext == 3)
                {
                    theplanettopointgravityUP = Vector3.forward;
                }
                else if (indexofmaxvalueofperfacegravitynext == 4)
                {
                    theplanettopointgravityUP = -Vector3.forward;
                }
                else if (indexofmaxvalueofperfacegravitynext == 5)
                {
                    theplanettopointgravityUP = -Vector3.up;
                }


                /*if (indexofmaxvalueofperfacegravitynext == 0)
                {
                    float max_distance = 360;

                    //Vector3 clamped = forwarddirtopointfrontplayer;//hand.transform.position;
                    /*forwarddirtopointfrontplayer.x = Mathf.Clamp(forwarddirtopointfrontplayer.x, alwaysuppointofplayercomparedtoplayercore.x - max_distance, alwaysuppointofplayercomparedtoplayercore.x + max_distance);
                    //forwarddirtopointfrontplayer.x = Mathf.Clamp(forwarddirtopointfrontplayer.x, alwaysuppointofplayercomparedtoplayercore.x - max_distance, alwaysuppointofplayercomparedtoplayercore.x + max_distance);
                    forwarddirtopointfrontplayer.y = 1;
                    forwarddirtopointfrontplayer.z = Mathf.Clamp(forwarddirtopointfrontplayer.z, alwaysuppointofplayercomparedtoplayercore.z - max_distance, alwaysuppointofplayercomparedtoplayercore.z + max_distance);
                    
                    forwarddirtopointfrontplayer.y = 0;
                }
                else if (indexofmaxvalueofperfacegravitynext == 1) //l
                {
                    forwarddirtopointfrontplayer.x = 0;
                }
                else if (indexofmaxvalueofperfacegravitynext == 2) //r
                {
                    forwarddirtopointfrontplayer.x = 0;
                }

                else if (indexofmaxvalueofperfacegravitynext == 3) //f
                {
                    forwarddirtopointfrontplayer.z = 0;
                }
                else if (indexofmaxvalueofperfacegravitynext == 4) //ba
                {
                    forwarddirtopointfrontplayer.z = 0;
                }
                else if (indexofmaxvalueofperfacegravitynext == 5) //bo
                {
                    forwarddirtopointfrontplayer.y = 0;
                }*/
            }


            if (indexofmaxvalueofperfacegravitynextdot <= indexofmaxvalueofperfacegravitydot * 0.985f)
            {
                //Vector3.Cross();

                /*if (indexofmaxvalueofperfacegravity == 0)
                {
                    float max_distance = 360;

                    //Vector3 clamped = forwarddirtopointfrontplayer;//hand.transform.position;
                    /*forwarddirtopointfrontplayer.x = Mathf.Clamp(forwarddirtopointfrontplayer.x, alwaysuppointofplayercomparedtoplayercore.x - max_distance, alwaysuppointofplayercomparedtoplayercore.x + max_distance);
                    //forwarddirtopointfrontplayer.x = Mathf.Clamp(forwarddirtopointfrontplayer.x, alwaysuppointofplayercomparedtoplayercore.x - max_distance, alwaysuppointofplayercomparedtoplayercore.x + max_distance);
                    forwarddirtopointfrontplayer.y = 1;
                    forwarddirtopointfrontplayer.z = Mathf.Clamp(forwarddirtopointfrontplayer.z, alwaysuppointofplayercomparedtoplayercore.z - max_distance, alwaysuppointofplayercomparedtoplayercore.z + max_distance);
                    
                    forwarddirtopointfrontplayer.y = 0;

                }
                else if (indexofmaxvalueofperfacegravity == 1) //l
                {
                    forwarddirtopointfrontplayer.x = 0;

                }
                else if (indexofmaxvalueofperfacegravity == 2) //r
                {
                    forwarddirtopointfrontplayer.x = 0;
                }

                else if (indexofmaxvalueofperfacegravity == 3) //f
                {
                    forwarddirtopointfrontplayer.z = 0;
                }
                else if (indexofmaxvalueofperfacegravity == 4) //ba
                {
                    forwarddirtopointfrontplayer.z = 0;
                }
                else if (indexofmaxvalueofperfacegravity == 5) //bo
                {
                    forwarddirtopointfrontplayer.y = 0;
                }*/

                /*
                if (indexofmaxvalueofperfacegravitynext == 0)
                {
                    float max_distance = 360;

                    float currentvelocity = 10.0f;
                    forwarddirtopointfrontplayer.y = scmaths.SmoothDampVec(0, forwarddirtopointfrontplayer.y, ref currentvelocity, 10.0f, 10.0f, Time.deltaTime);

                }
                else if (indexofmaxvalueofperfacegravitynext == 1) //l
                {
                    //forwarddirtopointfrontplayer.x = 0;
                    float currentvelocity = 10.0f;
                    forwarddirtopointfrontplayer.x = scmaths.SmoothDampVec(0, forwarddirtopointfrontplayer.x, ref currentvelocity, 10.0f, 10.0f, Time.deltaTime);

                }
                else if (indexofmaxvalueofperfacegravitynext == 2) //r
                {
                    //forwarddirtopointfrontplayer.x = 0;
                    float currentvelocity = 10.0f;
                    forwarddirtopointfrontplayer.x = scmaths.SmoothDampVec(0, forwarddirtopointfrontplayer.x, ref currentvelocity, 10.0f, 10.0f, Time.deltaTime);

                }

                else if (indexofmaxvalueofperfacegravitynext == 3) //f
                {
                    float currentvelocity = 10.0f;
                    forwarddirtopointfrontplayer.z = scmaths.SmoothDampVec(0, forwarddirtopointfrontplayer.z, ref currentvelocity, 10.0f, 10.0f, Time.deltaTime);

                    //forwarddirtopointfrontplayer.z = 0;
                }
                else if (indexofmaxvalueofperfacegravitynext == 4) //ba
                {
                    float currentvelocity = 10.0f;
                    forwarddirtopointfrontplayer.z = scmaths.SmoothDampVec(0, forwarddirtopointfrontplayer.z, ref currentvelocity, 10.0f, 10.0f, Time.deltaTime);

                    //forwarddirtopointfrontplayer.z = 0;
                }
                else if (indexofmaxvalueofperfacegravitynext == 5) //bo
                {
                    float currentvelocity = 10.0f;
                    forwarddirtopointfrontplayer.y = scmaths.SmoothDampVec(0, forwarddirtopointfrontplayer.y, ref currentvelocity, 10.0f, 10.0f, Time.deltaTime);

                    //forwarddirtopointfrontplayer.y = 0;
                }*/
            }






            if (indexofmaxvalueofperfacegravity == 0)
            {
                Vector3 currentforwarddir = forwarddirtopointfrontplayer;
                currentforwarddir.y = 0;
                float max_distance = 360;
                forwarddirtopointfrontplayer = Vector3.Lerp(forwarddirtopointfrontplayer, currentforwarddir, 10.0f);

            }
            else if (indexofmaxvalueofperfacegravity == 1) //l
            {
                Vector3 currentforwarddir = forwarddirtopointfrontplayer;
                currentforwarddir.x = 0;
                float max_distance = 360;
                forwarddirtopointfrontplayer = Vector3.Lerp(forwarddirtopointfrontplayer, currentforwarddir, 10.0f);

            }
            else if (indexofmaxvalueofperfacegravity == 2) //r
            {
                Vector3 currentforwarddir = forwarddirtopointfrontplayer;
                currentforwarddir.x = 0;
                float max_distance = 360;
                forwarddirtopointfrontplayer = Vector3.Lerp(forwarddirtopointfrontplayer, currentforwarddir, 10.0f);

            }

            else if (indexofmaxvalueofperfacegravity == 3) //f
            {
                Vector3 currentforwarddir = forwarddirtopointfrontplayer;
                currentforwarddir.z = 0;
                float max_distance = 360;
                forwarddirtopointfrontplayer = Vector3.Lerp(forwarddirtopointfrontplayer, currentforwarddir, 10.0f);

                //forwarddirtopointfrontplayer.z = 0;
            }
            else if (indexofmaxvalueofperfacegravity == 4) //ba
            {
                Vector3 currentforwarddir = forwarddirtopointfrontplayer;
                currentforwarddir.z = 0;
                float max_distance = 360;
                forwarddirtopointfrontplayer = Vector3.Lerp(forwarddirtopointfrontplayer, currentforwarddir, 10.0f);
                //forwarddirtopointfrontplayer.z = 0;
            }
            else if (indexofmaxvalueofperfacegravity == 5) //bo
            {
                Vector3 currentforwarddir = forwarddirtopointfrontplayer;
                currentforwarddir.y = 0;
                float max_distance = 360;
                forwarddirtopointfrontplayer = Vector3.Lerp(forwarddirtopointfrontplayer, currentforwarddir, 10.0f);
                //forwarddirtopointfrontplayer.y = 0;
            }











            float rotate_speed = 25.0f;

            Quaternion rot = new Quaternion();
            //rot.SetLookRotation(forwarddirtopointfrontplayer, -(viewer.transform.position - theplanet.transform.position).normalized); //+ theouthit.normal
            rot.SetLookRotation(forwarddirtopointfrontplayer, (theplanettopointgravityUP).normalized); //+ theouthit.normal


            if (indexofmaxvalueofperfacegravitynextdot > indexofmaxvalueofperfacegravitydot * 0.95f)
            {



                //viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, rot, rotate_speed * Time.deltaTime);                                                                                                        //


                /*if (indexofmaxvalueofperfacegravitynext == 0&& indexofmaxvalueofperfacegravitynext != indexofmaxvalueofperfacegravity)
                {
                    Debug.Log("testing");
                    viewer.transform.rotation = Quaternion.Euler(0, viewer.transform.rotation.eulerAngles.y, 0);
                }*/
                /*else if (indexofmaxvalueofperfacegravitynext == 1&& indexofmaxvalueofperfacegravitynext != indexofmaxvalueofperfacegravity) //l
                {

                    viewer.transform.rotation = Quaternion.Euler(viewer.transform.rotation.eulerAngles.x, 0, 0);
                }
                else if (indexofmaxvalueofperfacegravitynext == 2&& indexofmaxvalueofperfacegravitynext != indexofmaxvalueofperfacegravity) //r
                {

                    viewer.transform.rotation = Quaternion.Euler(viewer.transform.rotation.eulerAngles.x, 0, 0);
                }*/



                /*
                if (indexofmaxvalueofperfacegravitynext == 0 && indexofmaxvalueofperfacegravitynext != indexofmaxvalueofperfacegravity) //f
                {
                    //Debug.Log("testing0");

                    if (indexofmaxvalueofperfacegravitylast == 0)
                    {
                       
                    }
                    else if (indexofmaxvalueofperfacegravitylast == 1)
                    {

                    }
                    else if (indexofmaxvalueofperfacegravitylast == 2)
                    {

                    }
                    else if (indexofmaxvalueofperfacegravitylast == 3)
                    {
                        Debug.Log("testing00");
                        Quaternion eulertobeat = Quaternion.Euler(0, viewer.transform.rotation.eulerAngles.x, 0);
                        rotate_speed = 1500;
                        viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, eulertobeat, rotate_speed * Time.deltaTime);

                    }
                    else if (indexofmaxvalueofperfacegravitylast == 4)
                    {

                    }
                    else if (indexofmaxvalueofperfacegravitylast == 5)
                    {

                    }
                    //forwarddirtopointfrontplayer.z = 0;
                }
                else
                {
                    if (indexofmaxvalueofperfacegravitynext == 0 && indexofmaxvalueofperfacegravity == 0) //f
                    {
                        Debug.Log("testing1");
                        Quaternion eulertobeat = Quaternion.Euler(0, viewer.transform.rotation.eulerAngles.y, 0);
                        rotate_speed = 1500;
                        viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, eulertobeat, rotate_speed * Time.deltaTime);
                    }
                    else
                    {
                        if (indexofmaxvalueofperfacegravitynext == -1 && indexofmaxvalueofperfacegravity == 0) //f
                        {
                            Debug.Log("testing1");
                            Quaternion eulertobeat = Quaternion.Euler(0, viewer.transform.rotation.eulerAngles.y, 0);
                            rotate_speed = 1500;
                            viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, eulertobeat, rotate_speed * Time.deltaTime);
                        }
                    }
                }


                */






                /*
                if (indexofmaxvalueofperfacegravitynext == 3 && indexofmaxvalueofperfacegravitynext != indexofmaxvalueofperfacegravity) //f
                {
                    //Debug.Log("testing0");
                   
                    if (indexofmaxvalueofperfacegravitylast == 0)
                    {
                        Debug.Log("testing0");
                        Quaternion eulertobeat = Quaternion.Euler(viewer.transform.rotation.eulerAngles.y, 90, 90);
                        rotate_speed = 1500;
                        viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, eulertobeat, rotate_speed * Time.deltaTime);

                    }
                    else if (indexofmaxvalueofperfacegravitylast == 1)
                    {

                    }
                    else if (indexofmaxvalueofperfacegravitylast == 2)
                    {

                    }
                    else if (indexofmaxvalueofperfacegravitylast == 3)
                    {

                    }
                    else if (indexofmaxvalueofperfacegravitylast == 4)
                    {

                    }
                    else if (indexofmaxvalueofperfacegravitylast == 5)
                    {

                    }
                    //forwarddirtopointfrontplayer.z = 0;
                }
                else
                {
                    if (indexofmaxvalueofperfacegravitynext == 3 && indexofmaxvalueofperfacegravity== 3) //f
                    {
                        Debug.Log("testing111");
                        Quaternion eulertobeat = Quaternion.Euler(viewer.transform.rotation.eulerAngles.x, 90, 90);
                        rotate_speed = 1500;
                        viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, eulertobeat, rotate_speed * Time.deltaTime);
                    }
                    else
                    {
                        if (indexofmaxvalueofperfacegravitynext == -1 && indexofmaxvalueofperfacegravity == 3) //f
                        {
                            Debug.Log("testing1");
                            Quaternion eulertobeat = Quaternion.Euler(viewer.transform.rotation.eulerAngles.x, 90, 90);
                            rotate_speed = 1500;
                            viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, eulertobeat, rotate_speed * Time.deltaTime);
                        }
                    }
                }*/
                /*else if (indexofmaxvalueofperfacegravitynext == 4&& indexofmaxvalueofperfacegravitynext != indexofmaxvalueofperfacegravity) //ba
                {

                    viewer.transform.rotation = Quaternion.Euler(0, 0, viewer.transform.rotation.eulerAngles.z);

                }
                else if (indexofmaxvalueofperfacegravitynext == 5&& indexofmaxvalueofperfacegravitynext != indexofmaxvalueofperfacegravity) //bo
                {

                    viewer.transform.rotation = Quaternion.Euler(0, viewer.transform.rotation.eulerAngles.y, 0);
                }*/


            }
            else
            {
                Debug.Log("starting sticking feet to ground and up/forward direction.");

            }
            viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, rot, rotate_speed * Time.deltaTime);


            if (indexofmaxvalueofperfacegravity == -1)//&& indexofmaxvalueofperfacegravitynext == -1
            {

            }







            // viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, rot, rotate_speed * Time.deltaTime);







            //Debug.Log("/dot:" + indexofmaxvalueofperfacegravitydot + "/dotnext:" + indexofmaxvalueofperfacegravitynextdot + "/f:" + indexofmaxvalueofperfacegravity + "/fnext:" + indexofmaxvalueofperfacegravitynext);

            /*if (indexofmaxvalueofperfacegravitynextdot > indexofmaxvalueofperfacegravitydot * 0.985f)
            {
                Vector3 diagonaldown = isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + isgroundedpivotpoint.transform.up;// - isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.up;// new Vector3();
                //diagonaldown.y = 1.0f;
                Vector3 dirdiag = isgroundedpivotpoint.transform.position - diagonaldown;

                Ray raychangegravity = new Ray(isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + isgroundedpivotpoint.transform.up, dirdiag);
                RaycastHit theouthitgravitychange;

                Debug.DrawRay(raychangegravity.origin, raychangegravity.direction, Color.red, 10.0f);


                if (Physics.Raycast(raychangegravity, out theouthitgravitychange, layerMask))
                {
                    Debug.Log("Gravitychange");

                    theplanet = GameObject.FindGameObjectWithTag("terrain");
                    //theplanet = theouthitgravitychange.transform.parent.transform.parent.gameObject;




                    theplanettopointgravityUP = theouthitgravitychange.normal;// new Vector3(poscubicgravityvisualx, poscubicgravityvisualy, poscubicgravityvisualz) - theplanet.transform.position;
                    theplanettopointgravityUP.Normalize();



                     distance = 0.00025f;
                    //var dirForward = viewer.transform.rotation * Vector3.forward;
                    dirForward = viewer.transform.rotation * dirdiag;// Vector3.forward;

                    dirForward.Normalize();

                    //var dirofnormal = Quaternion.identity * theouthitgravitychange.normal;


                     positioninfrontofplayer = viewer.transform.position + (dirForward * distance);

                    //Vector3 positioninbackofplayer = viewer.transform.position + (-dirofnormal * distance);

                     dirplanetcoretoplayer = viewer.transform.position - theplanet.transform.position;
                     distcoretoplayer = dirplanetcoretoplayer.magnitude;

                     dirplanetcoretopointfrontofplayer = positioninfrontofplayer - theplanet.transform.position;
                    //float distcoretopointinfrontofplayer = dirplanetcoretopointfrontofplayer.magnitude;
                    dirplanetcoretopointfrontofplayer.Normalize();

                     alwaysuppointofplayercomparedtoplayercore = theplanet.transform.position + (dirplanetcoretopointfrontofplayer * (distcoretoplayer));// positioninfrontofplayer;// theplanet.transform.position + (dirplanetcoretopointfrontofplayer * (distcoretoplayer));

                     forwarddirtopointfrontplayer = positioninfrontofplayer - viewer.transform.position;// alwaysuppointofplayercomparedtoplayercore - viewer.transform.position;

                    forwarddirtopointfrontplayer.Normalize();

                     crossvecforward = Vector3.zero;




                    //topfacegravity
                    /*if (indexofmaxvalueofperfacegravity == 0)
                    {
                        crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.right);
                    }
                    else if (indexofmaxvalueofperfacegravity == 1) //l
                    {
                        crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.forward);
                    }
                    else if (indexofmaxvalueofperfacegravity == 2) //r
                    {
                        crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.forward);
                    }
                    else if (indexofmaxvalueofperfacegravity == 3) //f
                    {
                        crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.up);
                    }
                    else if (indexofmaxvalueofperfacegravity == 4) //ba
                    {
                        crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.up);
                    }
                    else if (indexofmaxvalueofperfacegravity == 5) //bo
                    {
                        crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.right);
                    }
                    */









            /*
            Vector3 crossvec = Vector3.Cross(theplanettopointgravityUP, forwarddirtopointfrontplayer);
            Vector3 crossvecForwardOfplanetgravity = Vector3.Cross(theplanettopointgravityUP, crossvec);












            //forwarddirtopointfrontplayer += crossvec;



            //Vector3.Cross();

            if (indexofmaxvalueofperfacegravitynext == 0)
            {
                float max_distance = 360;

                //Vector3 clamped = forwarddirtopointfrontplayer;//hand.transform.position;
                /*forwarddirtopointfrontplayer.x = Mathf.Clamp(forwarddirtopointfrontplayer.x, alwaysuppointofplayercomparedtoplayercore.x - max_distance, alwaysuppointofplayercomparedtoplayercore.x + max_distance);
                //forwarddirtopointfrontplayer.x = Mathf.Clamp(forwarddirtopointfrontplayer.x, alwaysuppointofplayercomparedtoplayercore.x - max_distance, alwaysuppointofplayercomparedtoplayercore.x + max_distance);
                forwarddirtopointfrontplayer.y = 1;
                forwarddirtopointfrontplayer.z = Mathf.Clamp(forwarddirtopointfrontplayer.z, alwaysuppointofplayercomparedtoplayercore.z - max_distance, alwaysuppointofplayercomparedtoplayercore.z + max_distance);

                forwarddirtopointfrontplayer.y = 0;

            }
            else if (indexofmaxvalueofperfacegravitynext == 1) //l
            {
                forwarddirtopointfrontplayer.x = 0;

            }
            else if (indexofmaxvalueofperfacegravitynext == 2) //r
            {
                forwarddirtopointfrontplayer.x = 0;
            }

            else if (indexofmaxvalueofperfacegravitynext == 3) //f
            {
                forwarddirtopointfrontplayer.z = 0;
            }
            else if (indexofmaxvalueofperfacegravitynext == 4) //ba
            {
                forwarddirtopointfrontplayer.z = 0;
            }
            else if (indexofmaxvalueofperfacegravitynext == 5) //bo
            {
                forwarddirtopointfrontplayer.y = 0;
            }


            rotate_speed = 25.0f;

            rot = new Quaternion();
            //rot.SetLookRotation(forwarddirtopointfrontplayer, -(viewer.transform.position - theplanet.transform.position).normalized); //+ theouthitgravitychange.normal
            rot.SetLookRotation(forwarddirtopointfrontplayer, (theplanettopointgravityUP).normalized); //+ theouthitgravitychange.normal


            viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, rot, rotate_speed * Time.deltaTime);

        }
    }*/


            if (lastfacegravityindex != indexofmaxvalueofperfacegravity)
            {



            }








            //rot.SetLookRotation(forwarddirtopointfrontplayer, (theplanettopointgravityUP).normalized); //+ theouthit.normal
            /*rot.SetLookRotation(crossvecforward, (theouthit.normal).normalized); //+ theouthit.normal

            viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, rot, rotate_speed * Time.deltaTime);
            */

            /*
            if (indexofmaxvalueofperfacegravity == 0)
            {
                Vector3 roteulervec = transform.rotation.eulerAngles;
                roteulervec.x = 0;// Mathf.Clamp(roteulervec.x,0,15);
                roteulervec.z = 0;//Mathf.Clamp(roteulervec.z, 0, 15);
                //roteulervec.y = 0;
                var rotquat = Quaternion.Euler(roteulervec);

                viewer.transform.rotation = rotquat;// Quaternion.Lerp(viewer.transform.rotation, rotquat, rotate_speed * Time.deltaTime);
            }
            else if (indexofmaxvalueofperfacegravity == 1) //l
            {
                Vector3 roteulervec = transform.rotation.eulerAngles;
                //roteulervec.x = 0;// Mathf.Clamp(roteulervec.y, 0, 15);
                //roteulervec.z = 0;//Mathf.Clamp(roteulervec.z, 0, 15);
                //roteulervec.y += 90;
                var rotquat = Quaternion.Euler(roteulervec);

                viewer.transform.rotation = rotquat;//Quaternion.Lerp(viewer.transform.rotation, rotquat, rotate_speed * Time.deltaTime);
            }
            else if (indexofmaxvalueofperfacegravity == 2) //r
            {
                Vector3 roteulervec = transform.rotation.eulerAngles;
                roteulervec.y = 0;// Mathf.Clamp(roteulervec.y, 0, 15);
                roteulervec.z = 0;//Mathf.Clamp(roteulervec.z, 0, 15);
                //roteulervec.x = 0;
                var rotquat = Quaternion.Euler(roteulervec);

                viewer.transform.rotation = rotquat;//Quaternion.Lerp(viewer.transform.rotation, rotquat, rotate_speed * Time.deltaTime);
            }
            else if (indexofmaxvalueofperfacegravity == 3) //f
            {
                Vector3 roteulervec = transform.rotation.eulerAngles;
                roteulervec.y = Mathf.Clamp(roteulervec.y, 0, 15);
                roteulervec.x = Mathf.Clamp(roteulervec.x, 0, 15);
                var rotquat = Quaternion.Euler(roteulervec);

                viewer.transform.rotation = rotquat;//Quaternion.Lerp(viewer.transform.rotation, rotquat, rotate_speed * Time.deltaTime);
            }
            else if (indexofmaxvalueofperfacegravity == 4) //ba
            {
                Vector3 roteulervec = transform.rotation.eulerAngles;
                roteulervec.y = Mathf.Clamp(roteulervec.y, 0, 15);
                roteulervec.x = Mathf.Clamp(roteulervec.x, 0, 15);
                var rotquat = Quaternion.Euler(roteulervec);

                viewer.transform.rotation = rotquat;//Quaternion.Lerp(viewer.transform.rotation, rotquat, rotate_speed * Time.deltaTime);
            }
            else if (indexofmaxvalueofperfacegravity == 5) //bo
            {
                Vector3 roteulervec = transform.rotation.eulerAngles;
                roteulervec.x = Mathf.Clamp(roteulervec.x, 0, 15);
                roteulervec.z = Mathf.Clamp(roteulervec.z, 0, 15);
                var rotquat = Quaternion.Euler(roteulervec);

                viewer.transform.rotation = rotquat;// Quaternion.Lerp(viewer.transform.rotation, rotquat, rotate_speed * Time.deltaTime);
            }
            */














            /*Vector3 clamped = hand.transform.position;
            clamped.x = Mathf.Clamp(clamped.x, target.transform.position.x - max_distance, target.transform.position.x + max_distance);
            clamped.y = Mathf.Clamp(clamped.y, target.transform.position.y - max_distance, target.transform.position.y + max_distance);

            hand.transform.position = clamped;*/





            /*
            Quaternion rotationvec = Quaternion.Euler(forwarddirtopointfrontplayer);
            //rotationvec = forwarddirtopointfrontplayer;
            /*Vector3 somedir = rotationvec * forwarddirtopointfrontplayer;

            rotationvec = Quaternion.Euler(somedir);

            rotationvec.SetLookRotation(forwarddirtopointfrontplayer, (theplanettopointgravityUP).normalized);

            viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, rotationvec, rotate_speed * Time.deltaTime);
            */


            //BITCRUSHEDSWORD
            //BITCRUSHEDSWORD
            //BITCRUSHEDSWORD
            /*Vector3 mov = new Vector3(Input.GetAxis("Mouse X") * Time.deltaTime * speed, Input.GetAxis("Mouse Y") * Time.deltaTime * speed, 0);

            hand.transform.position += mov;

            Vector3 clamped = hand.transform.position;
            clamped.x = Mathf.Clamp(clamped.x, target.transform.position.x - max_distance, target.transform.position.x + max_distance);
            clamped.y = Mathf.Clamp(clamped.y, target.transform.position.y - max_distance, target.transform.position.y + max_distance);

            hand.transform.position = clamped;

            Quaternion rot = new Quaternion();
            rot.SetLookRotation(sword.transform.forward, -(target.transform.position - hand.transform.position).normalized);
            sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, rot, rotate_speed * Time.deltaTime);
            */
            //BITCRUSHEDSWORD
            //BITCRUSHEDSWORD
            //BITCRUSHEDSWORD


            //rot.SetLookRotation(forwarddirtopointfrontplayer, (theouthit.normal).normalized);

            //rot.SetLookRotation(transform.forward, (theouthit.normal).normalized);




            StartCoroutine(CheckMoving());
            StartCoroutine(MovePlayerWithKeyboard());
            StartCoroutine(RotatePlayerMouse());
            StartCoroutine(RotatePlayerWithKeyboard());

            //StartCoroutine(MovePlayerWithKeyboard());
            viewerPosition = viewer.transform.position;

            StartCoroutine(CheckMoving());

            if (hasclickedtomoveplayer == 1)
            {

                if (Mathf.Round(clicktomoveplayerpos.x) == Mathf.Round(viewer.transform.position.x) &&
                    Mathf.Round(clicktomoveplayerpos.y) == Mathf.Round(viewer.transform.position.y) &&
                    Mathf.Round(clicktomoveplayerpos.z) == Mathf.Round(viewer.transform.position.z))

                //if ((Mathf.Round(clicktomoveplayerpos.x * 100) / 100) == (Mathf.Round(currentPosition.x * 100) / 100) &&
                //    (Mathf.Round(clicktomoveplayerpos.y * 100) / 100) == (Mathf.Round(currentPosition.y * 100) / 100) &&
                //    (Mathf.Round(clicktomoveplayerpos.z * 100) / 100) == (Mathf.Round(currentPosition.z * 100) / 100))
                {

                }
                else
                {
                    StartCoroutine(RotatePlayerWithMouseClick());

                    StartCoroutine(MovePlayerWithMouseClick());


                }

            }

        }
        else
        {

            //if (indexofmaxvalueofperfacegravitynextdot > indexofmaxvalueofperfacegravitydot * 0.985f)
            {
                /*Vector3 diagonaldown = isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + isgroundedpivotpoint.transform.up;// - isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.up;// new Vector3();
                //diagonaldown.y = 1.0f;
                Vector3 dirdiag = isgroundedpivotpoint.transform.position - diagonaldown;

                Ray raychangegravity = new Ray(isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + isgroundedpivotpoint.transform.up, dirdiag);
                RaycastHit theouthitgravitychange;

                Debug.DrawRay(raychangegravity.origin, raychangegravity.direction, Color.red, 10.0f);
                */

                Vector3 diagonaldown = isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.up;// - isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.up;// new Vector3();

                Vector3 dirdiag = diagonaldown - isgroundedpivotpoint.transform.position;
                dirdiag.Normalize();

                //isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + isgroundedpivotpoint.transform.up

                Ray raychangegravity = new Ray(isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.up, dirdiag * 1.5f);
                RaycastHit theouthitgravitychange;

                //Debug.DrawRay(raychangegravity.origin, raychangegravity.direction, Color.red, 10.0f);



                if (Physics.Raycast(raychangegravity, out theouthitgravitychange, layerMask))
                {
                    Debug.Log("Gravitychange");

                    theplanet = GameObject.FindGameObjectWithTag("terrain");
                    //theplanet = theouthitgravitychange.transform.parent.transform.parent.gameObject;




                    Vector3 theplanettopointgravityUP = theouthitgravitychange.normal;// new Vector3(poscubicgravityvisualx, poscubicgravityvisualy, poscubicgravityvisualz) - theplanet.transform.position;
                    theplanettopointgravityUP.Normalize();



                    float distance = 0.00025f;
                    //var dirForward = viewer.transform.rotation * Vector3.forward;
                    Vector3 dirForward = viewer.transform.rotation * dirdiag;// Vector3.forward;

                    dirForward.Normalize();

                    //var dirofnormal = Quaternion.identity * theouthitgravitychange.normal;


                    Vector3 positioninfrontofplayer = viewer.transform.position + (dirForward * distance);

                    //Vector3 positioninbackofplayer = viewer.transform.position + (-dirofnormal * distance);

                    Vector3 dirplanetcoretoplayer = viewer.transform.position - theplanet.transform.position;
                    float distcoretoplayer = dirplanetcoretoplayer.magnitude;

                    Vector3 dirplanetcoretopointfrontofplayer = positioninfrontofplayer - theplanet.transform.position;
                    //float distcoretopointinfrontofplayer = dirplanetcoretopointfrontofplayer.magnitude;
                    dirplanetcoretopointfrontofplayer.Normalize();

                    Vector3 alwaysuppointofplayercomparedtoplayercore = theplanet.transform.position + (dirplanetcoretopointfrontofplayer * (distcoretoplayer));// positioninfrontofplayer;// theplanet.transform.position + (dirplanetcoretopointfrontofplayer * (distcoretoplayer));

                    Vector3 forwarddirtopointfrontplayer = positioninfrontofplayer - viewer.transform.position;// alwaysuppointofplayercomparedtoplayercore - viewer.transform.position;

                    forwarddirtopointfrontplayer.Normalize();

                    Vector3 crossvecforward = Vector3.zero;




                    //topfacegravity
                    /*if (indexofmaxvalueofperfacegravity == 0)
                    {
                        crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.right);
                    }
                    else if (indexofmaxvalueofperfacegravity == 1) //l
                    {
                        crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.forward);
                    }
                    else if (indexofmaxvalueofperfacegravity == 2) //r
                    {
                        crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.forward);
                    }
                    else if (indexofmaxvalueofperfacegravity == 3) //f
                    {
                        crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.up);
                    }
                    else if (indexofmaxvalueofperfacegravity == 4) //ba
                    {
                        crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.up);
                    }
                    else if (indexofmaxvalueofperfacegravity == 5) //bo
                    {
                        crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.right);
                    }
                    */









                    /*
                    Vector3 crossvec = Vector3.Cross(theplanettopointgravityUP, forwarddirtopointfrontplayer);
                    Vector3 crossvecForwardOfplanetgravity = Vector3.Cross(theplanettopointgravityUP, crossvec);
                    








                    */


                    //forwarddirtopointfrontplayer += crossvec;



                    //Vector3.Cross();

                    if (indexofmaxvalueofperfacegravitynext == 0)
                    {
                        float max_distance = 360;

                        //Vector3 clamped = forwarddirtopointfrontplayer;//hand.transform.position;
                        /*forwarddirtopointfrontplayer.x = Mathf.Clamp(forwarddirtopointfrontplayer.x, alwaysuppointofplayercomparedtoplayercore.x - max_distance, alwaysuppointofplayercomparedtoplayercore.x + max_distance);
                        //forwarddirtopointfrontplayer.x = Mathf.Clamp(forwarddirtopointfrontplayer.x, alwaysuppointofplayercomparedtoplayercore.x - max_distance, alwaysuppointofplayercomparedtoplayercore.x + max_distance);
                        forwarddirtopointfrontplayer.y = 1;
                        forwarddirtopointfrontplayer.z = Mathf.Clamp(forwarddirtopointfrontplayer.z, alwaysuppointofplayercomparedtoplayercore.z - max_distance, alwaysuppointofplayercomparedtoplayercore.z + max_distance);
                        */
                        forwarddirtopointfrontplayer.y = 0;

                    }
                    else if (indexofmaxvalueofperfacegravitynext == 1) //l
                    {
                        forwarddirtopointfrontplayer.x = 0;

                    }
                    else if (indexofmaxvalueofperfacegravitynext == 2) //r
                    {
                        forwarddirtopointfrontplayer.x = 0;
                    }

                    else if (indexofmaxvalueofperfacegravitynext == 3) //f
                    {
                        forwarddirtopointfrontplayer.z = 0;
                    }
                    else if (indexofmaxvalueofperfacegravitynext == 4) //ba
                    {
                        forwarddirtopointfrontplayer.z = 0;
                    }
                    else if (indexofmaxvalueofperfacegravitynext == 5) //bo
                    {
                        forwarddirtopointfrontplayer.y = 0;
                    }


                    float rotate_speed = 25.0f;

                    Quaternion rot = new Quaternion();
                    //rot.SetLookRotation(forwarddirtopointfrontplayer, -(viewer.transform.position - theplanet.transform.position).normalized); //+ theouthitgravitychange.normal
                    rot.SetLookRotation(forwarddirtopointfrontplayer, (theplanettopointgravityUP).normalized); //+ theouthitgravitychange.normal


                    viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, rot, rotate_speed * Time.deltaTime);

                }
            }

            /*Vector3 diagonaldown = new Vector3(0, 1.0f, -0.5f);

            Debug.Log("Gravitychange");
            Ray raychangegravity = new Ray(isgroundedpivotpoint.transform.position, isgroundedpivotpoint.transform.forward+ diagonaldown);
            RaycastHit theouthitgravitychange;

            if (Physics.Raycast(raychangegravity, out theouthitgravitychange, layerMask))
            {


                theplanet = GameObject.FindGameObjectWithTag("terrain");
                //theplanet = theouthitgravitychange.transform.parent.transform.parent.gameObject;




                Vector3 theplanettopointgravityUP = new Vector3(poscubicgravityvisualx, poscubicgravityvisualy, poscubicgravityvisualz) - theplanet.transform.position;
                theplanettopointgravityUP.Normalize();



                var distance = 0.00025f;
                //var dirForward = viewer.transform.rotation * Vector3.forward;
                var dirForward = viewer.transform.rotation * Vector3.forward;

                dirForward.Normalize();

                //var dirofnormal = Quaternion.identity * theouthitgravitychange.normal;


                Vector3 positioninfrontofplayer = viewer.transform.position + (dirForward * distance);

                //Vector3 positioninbackofplayer = viewer.transform.position + (-dirofnormal * distance);

                Vector3 dirplanetcoretoplayer = viewer.transform.position - theplanet.transform.position;
                float distcoretoplayer = dirplanetcoretoplayer.magnitude;

                Vector3 dirplanetcoretopointfrontofplayer = positioninfrontofplayer - theplanet.transform.position;
                //float distcoretopointinfrontofplayer = dirplanetcoretopointfrontofplayer.magnitude;
                dirplanetcoretopointfrontofplayer.Normalize();

                Vector3 alwaysuppointofplayercomparedtoplayercore = theplanet.transform.position + (dirplanetcoretopointfrontofplayer * (distcoretoplayer));// positioninfrontofplayer;// theplanet.transform.position + (dirplanetcoretopointfrontofplayer * (distcoretoplayer));

                Vector3 forwarddirtopointfrontplayer = positioninfrontofplayer - viewer.transform.position;// alwaysuppointofplayercomparedtoplayercore - viewer.transform.position;

                forwarddirtopointfrontplayer.Normalize();

                Vector3 crossvecforward = Vector3.zero;




                //topfacegravity
                /*if (indexofmaxvalueofperfacegravity == 0)
                {
                    crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.right);
                }
                else if (indexofmaxvalueofperfacegravity == 1) //l
                {
                    crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.forward);
                }
                else if (indexofmaxvalueofperfacegravity == 2) //r
                {
                    crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.forward);
                }
                else if (indexofmaxvalueofperfacegravity == 3) //f
                {
                    crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.up);
                }
                else if (indexofmaxvalueofperfacegravity == 4) //ba
                {
                    crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.up);
                }
                else if (indexofmaxvalueofperfacegravity == 5) //bo
                {
                    crossvecforward = Vector3.Cross(theplanettopointgravityUP, viewer.transform.right);
                }
                */









            /*
            Vector3 crossvec = Vector3.Cross(theplanettopointgravityUP, forwarddirtopointfrontplayer);
            Vector3 crossvecForwardOfplanetgravity = Vector3.Cross(theplanettopointgravityUP, crossvec);









            //Vector3 pointA = 









            //forwarddirtopointfrontplayer += crossvec;



            //Vector3.Cross();

            if (indexofmaxvalueofperfacegravity == 0)
            {
                float max_distance = 360;

                //Vector3 clamped = forwarddirtopointfrontplayer;//hand.transform.position;
                /*forwarddirtopointfrontplayer.x = Mathf.Clamp(forwarddirtopointfrontplayer.x, alwaysuppointofplayercomparedtoplayercore.x - max_distance, alwaysuppointofplayercomparedtoplayercore.x + max_distance);
                //forwarddirtopointfrontplayer.x = Mathf.Clamp(forwarddirtopointfrontplayer.x, alwaysuppointofplayercomparedtoplayercore.x - max_distance, alwaysuppointofplayercomparedtoplayercore.x + max_distance);
                forwarddirtopointfrontplayer.y = 1;
                forwarddirtopointfrontplayer.z = Mathf.Clamp(forwarddirtopointfrontplayer.z, alwaysuppointofplayercomparedtoplayercore.z - max_distance, alwaysuppointofplayercomparedtoplayercore.z + max_distance);

                forwarddirtopointfrontplayer.y = 0;

            }
            else if (indexofmaxvalueofperfacegravity == 1) //l
            {
                forwarddirtopointfrontplayer.x = 0;

            }
            else if (indexofmaxvalueofperfacegravity == 2) //r
            {
                forwarddirtopointfrontplayer.x = 0;
            }

            else if (indexofmaxvalueofperfacegravity == 3) //f
            {
                forwarddirtopointfrontplayer.z = 0;
            }
            else if (indexofmaxvalueofperfacegravity == 4) //ba
            {
                forwarddirtopointfrontplayer.z = 0;
            }
            else if (indexofmaxvalueofperfacegravity == 5) //bo
            {
                forwarddirtopointfrontplayer.y = 0;
            }


            float rotate_speed = 25.0f;

            Quaternion rot = new Quaternion();
            //rot.SetLookRotation(forwarddirtopointfrontplayer, -(viewer.transform.position - theplanet.transform.position).normalized); //+ theouthitgravitychange.normal
            rot.SetLookRotation(forwarddirtopointfrontplayer, (theplanettopointgravityUP).normalized); //+ theouthitgravitychange.normal


            viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, rot, rotate_speed * Time.deltaTime);     

            //
        }*/

        }



        //(int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy - 1), (int)(mainChunk.mindexposz)
        //theplanet = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv(); 













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
            Debug.Log("the planet is null");
        }

        yield return new WaitForSeconds(0.001f);
    }


    Vector3 clicktomoveplayerpos = Vector3.zero;
    Vector3 clicktomoveplayerdirtopos = Vector3.zero;
    Vector3 clicktomoveplayernormalofpos = Vector3.zero;

    int hasclickedtomoveplayer = 0;

    IEnumerator CheckMoving()
    {
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

        Debug.DrawRay(clicktomoveplayerpos, thepositionupofpoint - clicktomoveplayerpos, Color.red, 1.0f);
       
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
            //Debug.DrawRay(positioninfrontofplayer, thepositionupofpoint - positioninfrontofplayer, Color.red, 1.0f);
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
            //Debug.Log("test");
        }

        Debug.DrawRay(clicktomoveplayerpos, thepositionupofpoint - clicktomoveplayerpos, Color.red, 1.0f);
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
                    //Debug.DrawRay(positioninfrontofplayer, thepositionupofpoint - positioninfrontofplayer, Color.red, 1.0f);
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
                    Debug.Log("the planet is null");
                }
            }
        }
        else
        {
            hasclickedtomoveplayer = 0;
            //Debug.Log("test");
        }

        Debug.DrawRay(clicktomoveplayerpos, thepositionupofpoint - clicktomoveplayerpos, Color.red, 1.0f);

        if (theplanet != null)
        {
            viewer.transform.position = MOVEPOSOFFSET;
            lastframeviewerpos = viewer.transform.position;
        }
        yield return new WaitForSeconds(0.001f);

    }











    IEnumerator MovePlayerWithKeyboard()
    {
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
            //https://answers.unity.com/questions/1397510/converting-mouse-position-to-worldpoint-in-3d.html
            float speed = 10.0f;
            // Cast a ray from screen point
            Ray ray = new Ray(isgroundedpivotpoint.transform.position, isgroundedpivotpoint.transform.forward);
            // isgroundedpivotpoint.transform.position;// Camera.main.ScreenPointToRay(isgroundedpivotpoint.transform.position);//Camera.main.ScreenPointToRay(Input.mousePosition);


            RaycastHit theouthit;

            /* if (swtchastriedmovingplayerwithmouseclick == 0)
             {
                 // You successfully $$anonymous$$

             }*/

            if (Physics.Raycast(ray, out theouthit, layerMask))
            {
                // Find the direction to move in
                //Vector3 dir = theouthit.point - isgroundedpivotpoint.transform.position;



                theplanet = GameObject.FindGameObjectWithTag("terrain");
                //theplanet = theouthit.transform.parent.transform.parent.gameObject;


                if (indexofmaxvalueofperfacegravity == 0)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 1)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 2)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 3)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 4)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 5)
                {

                }


                Vector3 theplanettopointgravityUP = new Vector3(poscubicgravityvisualx, poscubicgravityvisualy, poscubicgravityvisualz) - theplanet.transform.position;
                theplanettopointgravityUP.Normalize();
                if (indexofmaxvalueofperfacegravitynextdot > indexofmaxvalueofperfacegravitydot * 0.985f)
                {
                    if (indexofmaxvalueofperfacegravity != indexofmaxvalueofperfacegravitynext)
                    {


                        if (indexofmaxvalueofperfacegravitynext == 0)
                        {
                            theplanettopointgravityUP = Vector3.up;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 1)
                        {
                            theplanettopointgravityUP = -Vector3.right;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 2)
                        {
                            theplanettopointgravityUP = Vector3.right;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 3)
                        {
                            theplanettopointgravityUP = Vector3.forward;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 4)
                        {
                            theplanettopointgravityUP = -Vector3.forward;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 5)
                        {
                            theplanettopointgravityUP = -Vector3.up;
                        }



                    }
                }










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

                Vector3 diagonaldown = isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.up;// - isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.up;// new Vector3();

                Vector3 dirdiag = diagonaldown - isgroundedpivotpoint.transform.position;
                dirdiag.Normalize();

                //isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + isgroundedpivotpoint.transform.up

                Ray raychangegravity = new Ray(isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.up, dirdiag * 1.5f);
                RaycastHit theouthitgravitychange;

                //Debug.DrawRay(raychangegravity.origin, raychangegravity.direction, Color.red, 10.0f);



                //Debug.DrawRay(viewer.transform.position, viewer.transform.forward, Color.blue, 10.0f);



                //Ray raypointtoground = new Ray(positioninfrontofplayer, isgroundedpivotpoint.transform.forward);
                RaycastHit theouthitpointtoground;


                if (Physics.Raycast(raychangegravity.origin, raychangegravity.direction, out theouthitpointtoground, isgroundedmaxdist, layerMask))

                //if (Physics.Raycast(positioninfrontofplayer, isgroundedpivotpoint.transform.forward, out theouthitpointtoground, isgroundedmaxdist, layerMask))
                //if (Physics.Raycast(raypointtoground, out theouthitpointtoground, layerMask))
                {
                    if (theplanet != null)
                    {
                        pointertarget.transform.position = theouthitpointtoground.point;

                        Vector3 currentdirtopointinfrontdir = theouthitpointtoground.point - theouthit.point;

                        //topointforward = theouthitpointtoground.point;
                        //Debug.DrawRay(positioninfrontofplayer, thepositionupofpoint - positioninfrontofplayer, Color.red, 1.0f);
                        MOVEPOSOFFSET = MOVEPOSOFFSET + (currentdirtopointinfrontdir * distance);


                        Vector3 uppoint = theplanettopointgravityUP;// viewer.transform.position - theplanet.transform.position;
                        uppoint.Normalize();

                        Vector3 pointtobeat = theouthitpointtoground.point + (-uppoint * 0.1f);

                        MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime



                        //Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
                        //uppoint.Normalize();

                        //Vector3 pointtobeat = viewer.transform.position + (uppoint * 0.5f);

                        //MOVEPOSOFFSET = MOVEPOSOFFSET;// MOVEPOSOFFSET + (currentdirtopointinfrontdir * distance);
                        //MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime
                    }
                    else
                    {
                        Debug.Log("the planet is null");

                    }
                }
            }
            else
            {
                hasclickedtomoveplayer = 0;
                //Debug.Log("test");
            }

        }

        if (Input.GetKey(KeyCode.Q))
        {
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

            if (Physics.Raycast(ray, out theouthit, layerMask))
            {
                // Find the direction to move in
                //Vector3 dir = theouthit.point - isgroundedpivotpoint.transform.position;

                if (indexofmaxvalueofperfacegravity == 0)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 1)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 2)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 3)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 4)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 5)
                {

                }


                Vector3 theplanettopointgravityUP = new Vector3(poscubicgravityvisualx, poscubicgravityvisualy, poscubicgravityvisualz) - theplanet.transform.position;
                theplanettopointgravityUP.Normalize();
                if (indexofmaxvalueofperfacegravitynextdot > indexofmaxvalueofperfacegravitydot * 0.985f)
                {
                    if (indexofmaxvalueofperfacegravity != indexofmaxvalueofperfacegravitynext)
                    {


                        if (indexofmaxvalueofperfacegravitynext == 0)
                        {
                            theplanettopointgravityUP = Vector3.up;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 1)
                        {
                            theplanettopointgravityUP = -Vector3.right;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 2)
                        {
                            theplanettopointgravityUP = Vector3.right;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 3)
                        {
                            theplanettopointgravityUP = Vector3.forward;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 4)
                        {
                            theplanettopointgravityUP = -Vector3.forward;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 5)
                        {
                            theplanettopointgravityUP = -Vector3.up;
                        }



                    }
                }










                var distance = 0.01f * movementspeed;
                var dirForward = viewer.transform.rotation * -Vector3.right;
                dirForward.Normalize();

                Vector3 positioninfrontofplayer = isgroundedpivotpoint.transform.position + (dirForward * distance);

                //Vector3 dirplanetcoretopoint = theplanet.transform.position - positioninfrontofplayer;
                //Vector3 dirplanetcoretopointnotnorm = dirplanetcoretopoint;
                //float distcoretopointinfrontofplayer = dirplanetcoretopoint.magnitude;

                //dirplanetcoretopoint.Normalize();


                //Vector3 pointinfrontofplayer = 

                //Vector3 dirtopointinfrontofplayer = positioninfrontofplayer - isgroundedpivotpoint.transform.position;
                //distcoretopointinfrontofplayer

                Vector3 diagonaldownorigin = isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.right + isgroundedpivotpoint.transform.forward;// - isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.up;// new Vector3();

                //Vector3 diagonaldownorigin = isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.right;// - isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.up;// new Vector3();


                //Vector3 diagonaldown = isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.right;// - isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.up;// new Vector3();

                Vector3 dirdiag = diagonaldownorigin - isgroundedpivotpoint.transform.position;
                dirdiag.Normalize();

                //isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + isgroundedpivotpoint.transform.up

                Ray raychangegravity = new Ray(isgroundedpivotpoint.transform.position - isgroundedpivotpoint.transform.right, dirdiag * 1.5f);
                RaycastHit theouthitgravitychange;

                Debug.DrawRay(raychangegravity.origin, raychangegravity.direction, Color.magenta, 10.0f);



                //Debug.DrawRay(viewer.transform.position, viewer.transform.forward, Color.blue, 10.0f);



                //Ray raypointtoground = new Ray(positioninfrontofplayer, isgroundedpivotpoint.transform.forward);
                RaycastHit theouthitpointtoground;


                if (Physics.Raycast(raychangegravity.origin, raychangegravity.direction, out theouthitpointtoground, isgroundedmaxdist, layerMask))

                //if (Physics.Raycast(positioninfrontofplayer, isgroundedpivotpoint.transform.forward, out theouthitpointtoground, isgroundedmaxdist, layerMask))
                //if (Physics.Raycast(raypointtoground, out theouthitpointtoground, layerMask))
                {
                    if (theplanet != null)
                    {
                        pointertarget.transform.position = theouthitpointtoground.point;

                        Vector3 currentdirtopointinfrontdir = theouthitpointtoground.point - theouthit.point;

                        //topointforward = theouthitpointtoground.point;
                        //Debug.DrawRay(positioninfrontofplayer, thepositionupofpoint - positioninfrontofplayer, Color.red, 1.0f);
                        MOVEPOSOFFSET = MOVEPOSOFFSET + (currentdirtopointinfrontdir * distance);


                        Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
                        uppoint.Normalize();

                        Vector3 pointtobeat = theouthitpointtoground.point + (-uppoint * 0.1f);

                        MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime
                    }
                    else
                    {
                        Debug.Log("the planet is null");

                    }

                }
            }
            else
            {
                hasclickedtomoveplayer = 0;
                //Debug.Log("test");
            }
        }

        if (Input.GetKey(KeyCode.E))
        {

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

            if (Physics.Raycast(ray, out theouthit, layerMask))
            {
                // Find the direction to move in
                //Vector3 dir = theouthit.point - isgroundedpivotpoint.transform.position;


                if (indexofmaxvalueofperfacegravity == 0)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 1)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 2)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 3)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 4)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 5)
                {

                }


                Vector3 theplanettopointgravityUP = new Vector3(poscubicgravityvisualx, poscubicgravityvisualy, poscubicgravityvisualz) - theplanet.transform.position;
                theplanettopointgravityUP.Normalize();
                if (indexofmaxvalueofperfacegravitynextdot > indexofmaxvalueofperfacegravitydot * 0.985f)
                {
                    if (indexofmaxvalueofperfacegravity != indexofmaxvalueofperfacegravitynext)
                    {


                        if (indexofmaxvalueofperfacegravitynext == 0)
                        {
                            theplanettopointgravityUP = Vector3.up;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 1)
                        {
                            theplanettopointgravityUP = -Vector3.right;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 2)
                        {
                            theplanettopointgravityUP = Vector3.right;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 3)
                        {
                            theplanettopointgravityUP = Vector3.forward;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 4)
                        {
                            theplanettopointgravityUP = -Vector3.forward;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 5)
                        {
                            theplanettopointgravityUP = -Vector3.up;
                        }



                    }
                }










                var distance = 0.01f * movementspeed;
                var dirForward = viewer.transform.rotation * Vector3.right;
                dirForward.Normalize();

                Vector3 positioninfrontofplayer = isgroundedpivotpoint.transform.position + (dirForward * distance);

                //Vector3 dirplanetcoretopoint = theplanet.transform.position - positioninfrontofplayer;
                //Vector3 dirplanetcoretopointnotnorm = dirplanetcoretopoint;
                //float distcoretopointinfrontofplayer = dirplanetcoretopoint.magnitude;

                //dirplanetcoretopoint.Normalize();


                //Vector3 pointinfrontofplayer = 

                //Vector3 dirtopointinfrontofplayer = positioninfrontofplayer - isgroundedpivotpoint.transform.position;
                //distcoretopointinfrontofplayer

                Vector3 diagonaldownorigin = isgroundedpivotpoint.transform.position - isgroundedpivotpoint.transform.right + isgroundedpivotpoint.transform.forward;// - isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.up;// new Vector3();

                //Vector3 diagonaldownorigin = isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.right;// - isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.up;// new Vector3();


                //Vector3 diagonaldown = isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.right;// - isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.up;// new Vector3();

                Vector3 dirdiag = diagonaldownorigin - isgroundedpivotpoint.transform.position;
                dirdiag.Normalize();

                //isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + isgroundedpivotpoint.transform.up

                Ray raychangegravity = new Ray(isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.right, dirdiag * 1.5f);
                RaycastHit theouthitgravitychange;

                Debug.DrawRay(raychangegravity.origin, raychangegravity.direction, Color.cyan, 10.0f);



                //Debug.DrawRay(viewer.transform.position, viewer.transform.forward, Color.blue, 10.0f);



                //Ray raypointtoground = new Ray(positioninfrontofplayer, isgroundedpivotpoint.transform.forward);
                RaycastHit theouthitpointtoground;


                if (Physics.Raycast(raychangegravity.origin, raychangegravity.direction, out theouthitpointtoground, isgroundedmaxdist, layerMask))

                //if (Physics.Raycast(positioninfrontofplayer, isgroundedpivotpoint.transform.forward, out theouthitpointtoground, isgroundedmaxdist, layerMask))
                //if (Physics.Raycast(raypointtoground, out theouthitpointtoground, layerMask))
                {
                    if (theplanet != null)
                    {
                        pointertarget.transform.position = theouthitpointtoground.point;

                        Vector3 currentdirtopointinfrontdir = theouthitpointtoground.point - theouthit.point;

                        //topointforward = theouthitpointtoground.point;
                        //Debug.DrawRay(positioninfrontofplayer, thepositionupofpoint - positioninfrontofplayer, Color.red, 1.0f);
                        MOVEPOSOFFSET = MOVEPOSOFFSET + (currentdirtopointinfrontdir * distance);


                        Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
                        uppoint.Normalize();

                        Vector3 pointtobeat = theouthitpointtoground.point + (-uppoint * 0.1f);

                        MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime

                    }
                    else
                    {
                        Debug.Log("the planet is null");

                    }
                }
            }
            else
            {
                hasclickedtomoveplayer = 0;
                //Debug.Log("test");
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
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

            if (Physics.Raycast(ray, out theouthit, layerMask))
            {
                // Find the direction to move in
                //Vector3 dir = theouthit.point - isgroundedpivotpoint.transform.position;


                if (indexofmaxvalueofperfacegravity == 0)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 1)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 2)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 3)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 4)
                {

                }
                else if (indexofmaxvalueofperfacegravity == 5)
                {

                }


                Vector3 theplanettopointgravityUP = new Vector3(poscubicgravityvisualx, poscubicgravityvisualy, poscubicgravityvisualz) - theplanet.transform.position;
                theplanettopointgravityUP.Normalize();
                if (indexofmaxvalueofperfacegravitynextdot > indexofmaxvalueofperfacegravitydot * 0.985f)
                {
                    if (indexofmaxvalueofperfacegravity != indexofmaxvalueofperfacegravitynext)
                    {


                        if (indexofmaxvalueofperfacegravitynext == 0)
                        {
                            theplanettopointgravityUP = Vector3.up;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 1)
                        {
                            theplanettopointgravityUP = -Vector3.right;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 2)
                        {
                            theplanettopointgravityUP = Vector3.right;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 3)
                        {
                            theplanettopointgravityUP = Vector3.forward;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 4)
                        {
                            theplanettopointgravityUP = -Vector3.forward;
                        }
                        else if (indexofmaxvalueofperfacegravitynext == 5)
                        {
                            theplanettopointgravityUP = -Vector3.up;
                        }



                    }
                }










                var distance = 0.01f * movementspeed;
                var dirForward = viewer.transform.rotation * -Vector3.forward;
                dirForward.Normalize();

                Vector3 positioninfrontofplayer = isgroundedpivotpoint.transform.position + (dirForward * distance);

                //Vector3 dirplanetcoretopoint = theplanet.transform.position - positioninfrontofplayer;
                //Vector3 dirplanetcoretopointnotnorm = dirplanetcoretopoint;
                //float distcoretopointinfrontofplayer = dirplanetcoretopoint.magnitude;

                //dirplanetcoretopoint.Normalize();


                //Vector3 pointinfrontofplayer = 

                //Vector3 dirtopointinfrontofplayer = positioninfrontofplayer - isgroundedpivotpoint.transform.position;
                //distcoretopointinfrontofplayer

                Vector3 diagonaldown = isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + isgroundedpivotpoint.transform.up;// - isgroundedpivotpoint.transform.forward + -isgroundedpivotpoint.transform.up;// new Vector3();

                Vector3 dirdiag = diagonaldown - isgroundedpivotpoint.transform.position;
                dirdiag.Normalize();

                //isgroundedpivotpoint.transform.position + isgroundedpivotpoint.transform.forward + isgroundedpivotpoint.transform.up

                Ray raychangegravity = new Ray(isgroundedpivotpoint.transform.position - isgroundedpivotpoint.transform.up, dirdiag * 1.5f);
                RaycastHit theouthitgravitychange;

                //Debug.DrawRay(raychangegravity.origin, raychangegravity.direction, Color.red, 10.0f);



                //Debug.DrawRay(viewer.transform.position, viewer.transform.forward, Color.blue, 10.0f);



                //Ray raypointtoground = new Ray(positioninfrontofplayer, isgroundedpivotpoint.transform.forward);
                RaycastHit theouthitpointtoground;


                if (Physics.Raycast(raychangegravity.origin, raychangegravity.direction, out theouthitpointtoground, isgroundedmaxdist, layerMask))

                //if (Physics.Raycast(positioninfrontofplayer, isgroundedpivotpoint.transform.forward, out theouthitpointtoground, isgroundedmaxdist, layerMask))
                //if (Physics.Raycast(raypointtoground, out theouthitpointtoground, layerMask))
                {
                    if (theplanet != null)
                    {

                        pointertarget.transform.position = theouthitpointtoground.point;

                        Vector3 currentdirtopointinfrontdir = theouthitpointtoground.point - theouthit.point;

                        //topointforward = theouthitpointtoground.point;
                        //Debug.DrawRay(positioninfrontofplayer, thepositionupofpoint - positioninfrontofplayer, Color.red, 1.0f);
                        MOVEPOSOFFSET = MOVEPOSOFFSET + (currentdirtopointinfrontdir * distance);


                        Vector3 uppoint = viewer.transform.position - theplanet.transform.position;
                        uppoint.Normalize();

                        Vector3 pointtobeat = theouthitpointtoground.point + (-uppoint * 0.1f);

                        MOVEPOSOFFSET = Vector3.Lerp(MOVEPOSOFFSET, pointtobeat, movementspeed * Time.deltaTime); // * Time.deltaTime

                    }
                    else
                    {
                        Debug.Log("the planet is null");

                    }
                }
            }
            else
            {
                hasclickedtomoveplayer = 0;
                //Debug.Log("test");
            }
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
            Cursor.lockState = CursorLockMode.Locked;

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

            Cursor.visible = false;



        }



        if (Input.GetMouseButtonUp(1))
        {

            if (swtcactivatemouselook == 1)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;

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

                    Debug.DrawRay(camera.transform.position, dir * 10.0f, Color.blue, 10.0f);
                    hasclickedtomoveplayer = 1;

                    Debug.Log("hasclickedtomoveplayer");
                    swtchastriedmovingplayerwithmouseclick = 1;
                }
                else
                {
                    hasclickedtomoveplayer = 0;
                    //Debug.Log("test");
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

                        float dot = Vector3.Dot(dirplanetcubicloc, viewer.transform.position - theplanet.transform.position);

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

            Debug.Log("/x:" + posx + "/y:" + posy + "/z:" + posz);
            */

            indexofmaxvalueofperfacegravity = arrayofgravityperfacedot.ToList().IndexOf(arrayofgravityperfacedot.Max());
            poscubicgravityvisualx = arrayofgravityperfacex[indexofmaxvalueofperfacegravity];
            poscubicgravityvisualy = arrayofgravityperfacey[indexofmaxvalueofperfacegravity];
            poscubicgravityvisualz = arrayofgravityperfacez[indexofmaxvalueofperfacegravity];



            indexofmaxvalueofperfacegravitydot = arrayofgravityperfacedot[indexofmaxvalueofperfacegravity];


            targetobjectvisual.transform.position = new Vector3(poscubicgravityvisualx * 5, poscubicgravityvisualy * 5, poscubicgravityvisualz * 5);


            var listofnextgravity = arrayofgravityperfacedot.ToList();
            var listwithnextgravity = listofnextgravity.Remove(arrayofgravityperfacedot.Max());

            indexofmaxvalueofperfacegravitynext = listofnextgravity.IndexOf(listofnextgravity.Max()) + 1;


            /*
            var listofnextgravityx = arrayofgravityperfacex.ToList();
            var listwithnextgravityx = listofnextgravityx.Remove(indexofmaxvalueofperfacegravitynext);
            */



            indexofmaxvalueofperfacegravitynextdot = arrayofgravityperfacedot[indexofmaxvalueofperfacegravitynext];







            lastfacegravityindex = indexofmaxvalueofperfacegravity;




            indexofmaxvalueofperfacegravitylast = indexofmaxvalueofperfacegravity;



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













        }



        /*


        if (answerx == 1 && answery == -1 && answerz == -1)
        {
            Debug.Log("answerx == 1 && answery == -1 && answerz == -1");
        }
        else if (answerx == 1 && answery == 1 && answerz == -1)
        {
            Debug.Log("answerx == 1 && answery == 1 && answerz == -1");
        }
        else if (answerx == 1 && answery == 1 && answerz == 1)
        {
            Debug.Log("answerx == 1 && answery == 1 && answerz == 1");
        }
        else if (answerx == -1 && answery == 1 && answerz == 1)
        {
            Debug.Log("answerx == -1 && answery == 1 && answerz == 1");
        }
        else if (answerx == -1 && answery == -1 && answerz == 1)
        {
            Debug.Log("answerx == -1 && answery == -1 && answerz == 1");
        }
        else if (answerx == 1 && answery == -1 && answerz == 1)
        {
            Debug.Log("answerx == 1 && answery == -1 && answerz == 1");
        }
        else if (answerx == 1 && answery == 1 && answerz == -1)
        {
            Debug.Log("answerx == 1 && answery == 1 && answerz == -1");
        }
        else if (answerx == -1 && answery == -1 && answerz == -1)
        {
            Debug.Log("answerx == -1 && answery == -1 && answerz == -1");
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

            //Debug.Log("dot:" + _dotGoal);
        }*/
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
                Debug.Log("the planet is null");

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
                Debug.Log("the planet is null");

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
