//using SCCoreSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.XR;
using System.Security.Cryptography;

public class sccsplayer : MonoBehaviour
{
    public float movementspeed = 5.0f;

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





    // Update is called once per frame
    void Update()
    {


        //var planetdivright = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy - 1), (int)(mainChunk.mindexposz));






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

            Vector3 forwarddirtopointfrontplayer = alwaysuppointofplayercomparedtoplayercore - viewer.transform.position;

            forwarddirtopointfrontplayer.Normalize();







            float rotate_speed = 25.0f;

            Quaternion rot = new Quaternion();
            rot.SetLookRotation(forwarddirtopointfrontplayer, -(theplanet.transform.position - viewer.transform.position).normalized);
            viewer.transform.rotation = Quaternion.Lerp(viewer.transform.rotation, rot, rotate_speed * Time.deltaTime);



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



                Ray raypointtoground = new Ray(positioninfrontofplayer, isgroundedpivotpoint.transform.forward);
                RaycastHit theouthitpointtoground;


                if (Physics.Raycast(positioninfrontofplayer, isgroundedpivotpoint.transform.forward, out theouthitpointtoground, isgroundedmaxdist, layerMask))
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



                Ray raypointtoground = new Ray(positioninfrontofplayer, isgroundedpivotpoint.transform.forward);
                RaycastHit theouthitpointtoground;


                if (Physics.Raycast(positioninfrontofplayer, isgroundedpivotpoint.transform.forward, out theouthitpointtoground, isgroundedmaxdist, layerMask))
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



                Ray raypointtoground = new Ray(positioninfrontofplayer, isgroundedpivotpoint.transform.forward);
                RaycastHit theouthitpointtoground;


                if (Physics.Raycast(positioninfrontofplayer, isgroundedpivotpoint.transform.forward, out theouthitpointtoground, isgroundedmaxdist, layerMask))
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



                Ray raypointtoground = new Ray(positioninfrontofplayer, isgroundedpivotpoint.transform.forward);
                RaycastHit theouthitpointtoground;


                if (Physics.Raycast(positioninfrontofplayer, isgroundedpivotpoint.transform.forward, out theouthitpointtoground, isgroundedmaxdist, layerMask))
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


            camera.transform.rotation = viewer.transform.rotation * Quaternion.Euler(pitch, yaw, roll);

            Cursor.visible = false;



        }



        if(Input.GetMouseButtonUp(1))
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
                camera.transform.rotation = originalcamerapivot.transform.rotation;// Quaternion.Euler(pitch, yaw, roll);// Quaternion.Lerp(camera.transform.rotation, Quaternion.Euler(pitch, yaw, roll), 0.1f);
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
                Vector3 upcoreplayer = viewer.transform.position - theplanet.transform.position;
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
                Vector3 upcoreplayer = viewer.transform.position - theplanet.transform.position;
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
            upperbodypivot.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
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
            upperbodypivot.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            //Matrix4x4.Rotate
        }

        //getPan

        yield return new WaitForSeconds(0.001f);
    }

}
