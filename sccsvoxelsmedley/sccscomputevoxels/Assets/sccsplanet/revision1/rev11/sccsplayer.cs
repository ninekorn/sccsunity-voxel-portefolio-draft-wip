using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccsplayer : MonoBehaviour
{
    public GameObject originalcamerapivot;
    public GameObject upperbodypivot;
    public GameObject headpivotpoint;
    public GameObject isgroundedpivotpoint;
    public GameObject pointertarget;

    public LayerMask layerMask;

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


         


    }





    // Update is called once per frame
    void Update()
    {
        StartCoroutine(RotatePlayerMouse());
        StartCoroutine(RotatePlayerWithKeyboard());
        StartCoroutine(MovePlayerWithKeyboard());
        viewerPosition = viewer.transform.position;

        StartCoroutine(CheckMoving());

        if (hasclickedtomoveplayer == 1)
        {
            StartCoroutine(RotatePlayerWithMouseClick());

            StartCoroutine(MovePlayerWithMouseClick());

            //clicktomoveplayerdirtopos
            //clicktomoveplayerpos


            //move player to point

       

        }





        /*
        dirtoplanetcore = theplanet.transform.position - viewer.transform.position;
        dirtoplanetcore.Normalize();*/


        //viewer.transform.up = -dirtoplanetcore;





        /*
        dirtoplanetcore = theplanet.transform.position - viewer.transform.position;
        dirtoplanetcore.Normalize();
        //viewer.transform.up = -dirtoplanetcore;


        var dirright = viewer.transform.rotation * Vector3.right;
        dirright.Normalize();
        clicktomoveplayerdirtopos.Normalize();

        float thedot = Vector3.Dot(dirtoplanetcore, viewer.transform.up);

        float rotationincrements = 50.0f;


        var rotation = viewer.transform.rotation.eulerAngles;
        viewer.transform.LookAt(dirtoplanetcore);
        viewer.transform.eulerAngles = new Vector3(rotation.x, viewer.transform.eulerAngles.y, rotation.z);
        */

        //Debug.Log(thedot);
        //viewer.transform.Rotate(90.0f, 0.0f, 0.0f, Space.World);


        //viewer.transform.ro

        dirtoplanetcore = theplanet.transform.position - viewer.transform.position;
        dirtoplanetcore.Normalize();


        var dirright = viewer.transform.rotation * Vector3.up;
        dirright.Normalize();
        clicktomoveplayerdirtopos.Normalize();

        float thedot = Vector3.Dot(dirright, dirtoplanetcore);

        float rotationincrements = 50.0f;


        /*if (thedot > 0.75f)
        {

            RotationX += rotationincrements;//


            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = RotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = RotationZ * 0.0174532925f;

            //Vector3 lookatlocal = transform.TransformDirection(lookAt);

            //https://answers.unity.com/questions/1611821/rotation-yaw-roll-pitch.html
            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            //viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
        }*/
        /*else if (thedot > -0.50f)
        {
            RotationX -= rotationincrements;//


            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = RotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = RotationZ * 0.0174532925f;

            //Vector3 lookatlocal = transform.TransformDirection(lookAt);

            //https://answers.unity.com/questions/1611821/rotation-yaw-roll-pitch.html
            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            //viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
        }*/


    }















    Vector3 dirtoplanetcore = Vector3.zero;

    public GameObject theplanet;







    Vector3 thepositionupofpoint;


    IEnumerator RotatePlayerWithMouseClick()
    {
        var dirright = viewer.transform.rotation * Vector3.right;
        dirright.Normalize();
        clicktomoveplayerdirtopos.Normalize();

        float thedot = Vector3.Dot(dirright, clicktomoveplayerdirtopos);

        float rotationincrements = 50.0f;

       
        if (thedot > 0.00123f)
        {



            RotationY += rotationincrements;//


            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = RotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = RotationZ * 0.0174532925f;

            //Vector3 lookatlocal = transform.TransformDirection(lookAt);

            //https://answers.unity.com/questions/1611821/rotation-yaw-roll-pitch.html
            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            //viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
        }
        else if (thedot < -0.00123f)
        {
            RotationY -= rotationincrements;//


            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = RotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = RotationZ * 0.0174532925f;

            //Vector3 lookatlocal = transform.TransformDirection(lookAt);

            //https://answers.unity.com/questions/1611821/rotation-yaw-roll-pitch.html
            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            //viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
        }



        /*var dirForward = viewer.transform.rotation * Vector3.forward;
        dirForward.Normalize();
        clicktomoveplayerdirtopos.Normalize();

        float thedot = Vector3.Dot(dirForward, clicktomoveplayerdirtopos);

        var dirUp = viewer.transform.rotation * Vector3.up;
        dirUp.Normalize();
        //Debug.Log(thedot);

        thepositionupofpoint = clicktomoveplayerpos + (dirUp * 2.0f);

        Debug.DrawRay(clicktomoveplayerpos, thepositionupofpoint - clicktomoveplayerpos, Color.red,1.0f);
        */



        //this.transform.Rotate()







        //hmdmatrixRot = viewer.transform.rotation;

        /*float rotationincrements = 25.75f;

        if (Input.GetKey(KeyCode.A))
        {
           
            RotationY -= rotationincrements;//


            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = RotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = RotationZ * 0.0174532925f;

            //Vector3 lookatlocal = transform.TransformDirection(lookAt);

            //https://answers.unity.com/questions/1611821/rotation-yaw-roll-pitch.html
            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            //viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
        }
        //* Time.deltaTime
        if (Input.GetKey(KeyCode.D))
        {
            RotationY += rotationincrements;//


            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = RotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = RotationZ * 0.0174532925f;

            //Vector3 lookatlocal = transform.TransformDirection(lookAt);
            //https://answers.unity.com/questions/1611821/rotation-yaw-roll-pitch.html
            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            //viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            //Matrix4x4.Rotate

        }



        if (Input.GetKey(KeyCode.T))
        {
          

            upperbodypivotRotationX += rotationincrements;
            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = upperbodypivotRotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = upperbodypivotRotationZ * 0.0174532925f;
           

            //Vector3 lookatlocal = transform.TransformDirection(lookAt);


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
        }*/

        //getPan

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



        var dirUp = viewer.transform.rotation * Vector3.up;
        dirUp.Normalize();


        Vector3 topointforward = MOVEPOSOFFSET;// clicktomoveplayerpos;


        thepositionupofpoint = clicktomoveplayerpos + (dirUp * 0.0012345f);

        Debug.DrawRay(clicktomoveplayerpos, thepositionupofpoint - clicktomoveplayerpos, Color.red, 1.0f);





        /* if (startPos.x != finalPos.x || startPos.y != finalPos.y
             || startPos.z != finalPos.z)
         {
             isMoving = true;
         }

         else if (startPos.x == finalPos.x && startPos.y == finalPos.y
              && startPos.z == finalPos.z)
         {
             isMoving = false;
         }*/

        /*if (Input.GetKey(KeyCode.W))
        {
            var distance = 0.1f;
            //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
            var dirForward = viewer.transform.rotation * Vector3.forward;
            dirForward.Normalize();
            MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);
            //topointforward = MOVEPOSOFFSET + pointforward;
        }

        if (Input.GetKey(KeyCode.Q))
        {
            var distance = 0.1f;
            //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
            var dirForward = viewer.transform.rotation * -Vector3.right;
            dirForward.Normalize();
            MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);
            //topointforward = MOVEPOSOFFSET + pointforward;
        }

        if (Input.GetKey(KeyCode.E))
        {
            var distance = 0.1f;
            //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
            var dirForward = viewer.transform.rotation * Vector3.right;
            dirForward.Normalize();
            MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);
            //topointforward = MOVEPOSOFFSET + pointforward;
        }

        if (Input.GetKey(KeyCode.S))
        {
            var distance = 0.1f;
            //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
            var dirForward = viewer.transform.rotation * -Vector3.forward;
            dirForward.Normalize();
            MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);
            //topointforward = MOVEPOSOFFSET + pointforward;
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
        }*/

        //var currentrotationforward = viewer.transform.rotation.eulerAngles.z;

        MOVEPOSOFFSET = Vector3.Lerp(topointforward, thepositionupofpoint, 0.01f);

        viewer.transform.position = MOVEPOSOFFSET;
        lastframeviewerpos = viewer.transform.position;
    }











    IEnumerator MovePlayerWithKeyboard()
    {
        //MOVEPOSOFFSET = viewer.transform.position;

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

        Vector3 topointforward = MOVEPOSOFFSET;
        /* if (startPos.x != finalPos.x || startPos.y != finalPos.y
             || startPos.z != finalPos.z)
         {
             isMoving = true;
         }

         else if (startPos.x == finalPos.x && startPos.y == finalPos.y
              && startPos.z == finalPos.z)
         {
             isMoving = false;
         }*/





        














        if (Input.GetKey(KeyCode.W))
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
                Vector3 dir = theouthit.point - isgroundedpivotpoint.transform.position;

                var distance = 0.025f;
                var dirForward = viewer.transform.rotation * Vector3.forward;
                dirForward.Normalize();

                Vector3 positioninfrontofplayer = isgroundedpivotpoint.transform.position + (dirForward * distance);

                Vector3 dirplanetcoretopoint = theplanet.transform.position - positioninfrontofplayer;
                Vector3 dirplanetcoretopointnotnorm = dirplanetcoretopoint;
                float distcoretopointinfrontofplayer = dirplanetcoretopoint.magnitude;

                dirplanetcoretopoint.Normalize();


                //Vector3 pointinfrontofplayer = 

                Vector3 dirtopointinfrontofplayer = positioninfrontofplayer - isgroundedpivotpoint.transform.position;
                //distcoretopointinfrontofplayer




                //Debug.DrawRay(isgroundedpivotpoint.transform.position, dirtopointinfrontofplayer * 5.0f, Color.cyan, 10.0f);






                Ray raypointtoground = new Ray(positioninfrontofplayer, isgroundedpivotpoint.transform.forward);
                RaycastHit theouthitpointtoground;



                if (Physics.Raycast(raypointtoground, out theouthitpointtoground, layerMask))
                {
                    //Vector3 dirpointtoground = theouthitpointtoground.point - viewer.transform.position;//

                   // Debug.DrawRay(theouthitpointtoground.point, isgroundedpivotpoint.transform.forward * 3.0f, Color.red, 10.0f);


                    /*Vector3 dirpointtoground = theouthitpointtoground.point - viewer.transform.position;// positioninfrontofplayer;

                    Vector3 currentdirtopointinfrontdir = theouthitpointtoground.point - theouthit.point;
                    */



                    //Debug.DrawRay(theouthitpointtoground.point, dirplanetcoretopoint * 1.0f, Color.red, 10.0f);




                    pointertarget.transform.position = theouthitpointtoground.point;



                    Vector3 currentdirtopointinfrontdir = theouthitpointtoground.point - theouthit.point;







                    // Make it so that its only in x and y axis
                    //dir.y = 0; // No vertical movement

                    // Now move your character in world space 
                    //transform.Translate(dir * Time.deltaTime * speed, Space.World);
                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), thetouchpoint, Quaternion.identity);
                    //transform.Translate (dir * Time.deltaTime * speed); // Try t$$anonymous$$s if it doesn't work

                    /* var distance = 0.1f;
                    //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
                    var dirForward = viewer.transform.rotation * Vector3.forward;
                    dirForward.Normalize();
                    MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);*/


                    ///Debug.DrawRay(viewer.transform.position, dirpointtoground * 1.0f, Color.magenta, 10.0f);





                    //var dirUp = viewer.transform.rotation * Vector3.up;
                    //dirUp.Normalize();

                    /*topointforward = positioninfrontofplayer;// clicktomoveplayerpos;

                    thepositionupofpoint = positioninfrontofplayer + (dirplanetcoretopoint * 0.0012345f);
                    */
                    
                    
                    topointforward = theouthitpointtoground.point;
                    //Debug.DrawRay(positioninfrontofplayer, thepositionupofpoint - positioninfrontofplayer, Color.red, 1.0f);
                    MOVEPOSOFFSET = MOVEPOSOFFSET + (currentdirtopointinfrontdir * distance);
                    

                    //MOVEPOSOFFSET = Vector3.Lerp(topointforward, thepositionupofpoint, 0.01f);

                    //viewer.transform.position = MOVEPOSOFFSET;
                    //lastframeviewerpos = viewer.transform.position;



                }




















                /*clicktomoveplayerpos = theouthit.point;
                clicktomoveplayerdirtopos = dir;
                clicktomoveplayernormalofpos = theouthit.normal;

                Debug.DrawRay(camera.transform.position, dir * 10.0f, Color.blue, 10.0f);
                hasclickedtomoveplayer = 1;

                Debug.Log("hasclickedtomoveplayer");
                swtchastriedmovingplayerwithmouseclick = 1;*/
            }
            else
            {
                hasclickedtomoveplayer = 0;
                //Debug.Log("test");
            }



            /*
            var distance = 0.1f;
            //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
            var dirForward = viewer.transform.rotation * Vector3.forward;
            dirForward.Normalize();
            MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);
            //topointforward = MOVEPOSOFFSET + pointforward;*/






        }

        if (Input.GetKey(KeyCode.Q))
        {
            /*var distance = 0.1f;
            //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
            var dirForward = viewer.transform.rotation * -Vector3.right;
            dirForward.Normalize();
            MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);
            //topointforward = MOVEPOSOFFSET + pointforward;*/

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
                Vector3 dir = theouthit.point - isgroundedpivotpoint.transform.position;

                var distance = 0.025f;
                var dirForward = viewer.transform.rotation * Vector3.right;
                dirForward.Normalize();

                Vector3 positioninfrontofplayer = isgroundedpivotpoint.transform.position + (-dirForward * distance);

                Vector3 dirplanetcoretopoint = theplanet.transform.position - positioninfrontofplayer;
                Vector3 dirplanetcoretopointnotnorm = dirplanetcoretopoint;
                float distcoretopointinfrontofplayer = dirplanetcoretopoint.magnitude;

                dirplanetcoretopoint.Normalize();


                //Vector3 pointinfrontofplayer = 

                Vector3 dirtopointinfrontofplayer = positioninfrontofplayer - isgroundedpivotpoint.transform.position;
                //distcoretopointinfrontofplayer
                //Debug.DrawRay(isgroundedpivotpoint.transform.position, dirtopointinfrontofplayer * 5.0f, Color.cyan, 10.0f);


                Ray raypointtoground = new Ray(positioninfrontofplayer, isgroundedpivotpoint.transform.forward);
                RaycastHit theouthitpointtoground;



                if (Physics.Raycast(raypointtoground, out theouthitpointtoground, layerMask))
                {

                    pointertarget.transform.position = theouthitpointtoground.point;
                    Vector3 currentdirtopointinfrontdir = theouthitpointtoground.point - theouthit.point;

                    topointforward = theouthitpointtoground.point;
                    //Debug.DrawRay(positioninfrontofplayer, thepositionupofpoint - positioninfrontofplayer, Color.red, 1.0f);
                    MOVEPOSOFFSET = MOVEPOSOFFSET + (currentdirtopointinfrontdir * distance);

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
            /*var distance = 0.1f;
            //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
            var dirForward = viewer.transform.rotation * Vector3.right;
            dirForward.Normalize();
            MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);
            //topointforward = MOVEPOSOFFSET + pointforward;*/

            /*var distance = 0.1f;
            //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
            var dirForward = viewer.transform.rotation * -Vector3.right;
            dirForward.Normalize();
            MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);
            //topointforward = MOVEPOSOFFSET + pointforward;*/

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
                Vector3 dir = theouthit.point - isgroundedpivotpoint.transform.position;

                var distance = 0.025f;
                var dirForward = viewer.transform.rotation * Vector3.right;
                dirForward.Normalize();

                Vector3 positioninfrontofplayer = isgroundedpivotpoint.transform.position + (dirForward * distance);

                Vector3 dirplanetcoretopoint = theplanet.transform.position - positioninfrontofplayer;
                Vector3 dirplanetcoretopointnotnorm = dirplanetcoretopoint;
                float distcoretopointinfrontofplayer = dirplanetcoretopoint.magnitude;

                dirplanetcoretopoint.Normalize();


                //Vector3 pointinfrontofplayer = 

                Vector3 dirtopointinfrontofplayer = positioninfrontofplayer - isgroundedpivotpoint.transform.position;
                //distcoretopointinfrontofplayer
                //Debug.DrawRay(isgroundedpivotpoint.transform.position, dirtopointinfrontofplayer * 5.0f, Color.cyan, 10.0f);


                Ray raypointtoground = new Ray(positioninfrontofplayer, isgroundedpivotpoint.transform.forward);
                RaycastHit theouthitpointtoground;



                if (Physics.Raycast(raypointtoground, out theouthitpointtoground, layerMask))
                {

                    pointertarget.transform.position = theouthitpointtoground.point;
                    Vector3 currentdirtopointinfrontdir = theouthitpointtoground.point - theouthit.point;

                    topointforward = theouthitpointtoground.point;
                    //Debug.DrawRay(positioninfrontofplayer, thepositionupofpoint - positioninfrontofplayer, Color.red, 1.0f);
                    MOVEPOSOFFSET = MOVEPOSOFFSET + (currentdirtopointinfrontdir * distance);

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
            /*var distance = 0.1f;
            //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
            var dirForward = viewer.transform.rotation * -Vector3.forward;
            dirForward.Normalize();
            MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);
            //topointforward = MOVEPOSOFFSET + pointforward;*/


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
                Vector3 dir = theouthit.point - isgroundedpivotpoint.transform.position;

                var distance = 0.025f;
                var dirForward = viewer.transform.rotation * Vector3.forward;
                dirForward.Normalize();

                Vector3 positioninfrontofplayer = isgroundedpivotpoint.transform.position + (-dirForward * distance);

                Vector3 dirplanetcoretopoint = theplanet.transform.position - positioninfrontofplayer;
                Vector3 dirplanetcoretopointnotnorm = dirplanetcoretopoint;
                float distcoretopointinfrontofplayer = dirplanetcoretopoint.magnitude;

                dirplanetcoretopoint.Normalize();


                //Vector3 pointinfrontofplayer = 

                Vector3 dirtopointinfrontofplayer = positioninfrontofplayer - isgroundedpivotpoint.transform.position;
                //distcoretopointinfrontofplayer
                //Debug.DrawRay(isgroundedpivotpoint.transform.position, dirtopointinfrontofplayer * 5.0f, Color.cyan, 10.0f);


                Ray raypointtoground = new Ray(positioninfrontofplayer, isgroundedpivotpoint.transform.forward);
                RaycastHit theouthitpointtoground;



                if (Physics.Raycast(raypointtoground, out theouthitpointtoground, layerMask))
                {
                   
                    pointertarget.transform.position = theouthitpointtoground.point;
                    Vector3 currentdirtopointinfrontdir = theouthitpointtoground.point - theouthit.point;

                    topointforward = theouthitpointtoground.point;
                    //Debug.DrawRay(positioninfrontofplayer, thepositionupofpoint - positioninfrontofplayer, Color.red, 1.0f);
                    MOVEPOSOFFSET = MOVEPOSOFFSET + (currentdirtopointinfrontdir * distance);

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

        MOVEPOSOFFSET = Vector3.Lerp(topointforward, MOVEPOSOFFSET, 0.1f);

        viewer.transform.position = MOVEPOSOFFSET;
        lastframeviewerpos = viewer.transform.position;
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


            /*if (mousex != 0)
            {
                RotationX += mousex;

                float pitch = RotationX * 0.0174532925f;
                float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
                float roll = RotationZ * 0.0174532925f;
                viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);

            }



            if (mousey != 0)
            {
                RotationY = mousey;

                float pitch = RotationX * 0.0174532925f;
                float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
                float roll = RotationZ * 0.0174532925f;
                viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            }*/


            MouseRotationX += mousey;
            MouseRotationY += mousex;

            float pitch = (MouseRotationX + RotationX) * 0.0174532925f;
            float yaw = (MouseRotationY + RotationY) * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = (MouseOriginalRotationZ) * 0.0174532925f;






            /*
            MouseRotationX += mousey;
            MouseRotationY += mousex;


            float pitch = (MouseRotationX) * 0174532925f;
            float yaw = (MouseRotationY) * 0174532925f;
            float roll = 0;// (RotationZ) * 0174532925f;

            roll = MouseOriginalRotationZ * 0.0174532925f;
            */


            //viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);

            //camera.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            camera.transform.rotation = Quaternion.Euler(pitch, yaw, roll);// Quaternion.Lerp(camera.transform.rotation, Quaternion.Euler(pitch, yaw, roll), speedlerprotation);





            Cursor.visible = false;



            /*if (swtcactivatemouselook == 0)
            {
                beforemouselookrot = viewer.transform.rotation;
                swtcactivatemouselook = 1;
            }
            Vector3 mov = new Vector3(Input.GetAxis("Mouse X") * mouserotatespeed, Input.GetAxis("Mouse Y") * mouserotatespeed, 0);

            RotationX += mov.x;
            RotationY += mov.y;



            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = RotationX;// * 0.0174532925f;
            float yaw = RotationY;// * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = RotationZ;//* 0.0174532925f;
            /*this.transform.rotation.ToAngleAxis(out angle, out axis);


            Quaternion q = this.transform.rotation;

            float x = q.x;
            float y = q.x;
            float z = q.x;
            float w = q.x;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);
            


            //Vector3 lookatlocal = transform.TransformDirection(lookAt);

            //https://answers.unity.com/questions/1611821/rotation-yaw-roll-pitch.html
            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);*/
        }



        if(Input.GetMouseButtonUp(1))
        {

            if (swtcactivatemouselook == 1)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;




                /*
                float pitch = MouseOriginalRotationX * 0.0174532925f;
                float yaw = MouseOriginalRotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
                float roll = MouseOriginalRotationZ * 0.0174532925f;
                */

                /*
                float pitch = RotationX * 0.0174532925f;
                float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
                float roll = RotationZ * 0.0174532925f;
                */


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

                    // Make it so that its only in x and y axis
                    //dir.y = 0; // No vertical movement

                    // Now move your character in world space 
                    //transform.Translate(dir * Time.deltaTime * speed, Space.World);
                    //Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), thetouchpoint, Quaternion.identity);
                    //transform.Translate (dir * Time.deltaTime * speed); // Try t$$anonymous$$s if it doesn't work
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


            /*float mousex = Input.GetAxis("Mouse X") * mouserotatespeed;
            float mousey = Input.GetAxis("Mouse Y") * mouserotatespeed;
            var point = cam.ScreenToWorldPoint(new Vector3(mousex, mousey, cam.nearClipPlane));

            Vector3 thetouchpoint = Camera.main.ScreenToWorldPoint(point);

            Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), thetouchpoint, Quaternion.identity);
            */
            /*Vector3 point = new Vector3();
            Event currentEvent = Event.current;
            Vector2 mousePos = new Vector2();

            // Get the mouse position from Event.
            // Note that the y position from Event is inverted.
            mousePos.x = currentEvent.mousePosition.x;
            mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

            point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

            Instantiate(GameObject.CreatePrimitive(PrimitiveType.Sphere), point, Quaternion.identity);*/

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
        /*//https://docs.unity3d.com/ScriptReference/Input.GetAxis.html
        // Get the horizontal and vertical axis.
        // By default they are mapped to the arrow keys.
        // The value is in the range -1 to 1
        float translation = Input.GetAxis("Vertical") * speed;
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed;

        // Make it move 10 meters per second instead of 10 meters per frame...
        translation *= Time.deltaTime;
        rotation *= Time.deltaTime;

        // Move translation along the object's z-axis
        viewer.transform.Translate(0, 0, translation);

        // Rotate around our y-axis
        viewer.transform.Rotate(0, rotation, 0);
        */




        /*float horizontalSpeed = 2.0f;
        float verticalSpeed = 2.0f;

        void Update()
        {
            // Get the mouse delta. This is not in the range -1...1
            float h = horizontalSpeed * Input.GetAxis("Mouse X");
            float v = verticalSpeed * Input.GetAxis("Mouse Y");

            viewer.transform.Rotate(v, h, 0);
        }*/



        if (canmovecamera == 1)
        {
            /*if (keyboardinput._KeyboardState != null && keyboardinput._KeyboardState.PressedKeys.Contains(Key.A))
            {
                if (useOculusRift == 0)
                {
                    roty -= speedRot;
                }
                else if (useOculusRift == 1)
                {
                    roty += speedRot;
                }
                //Console.WriteLine("pressed A");

            }
            else if (keyboardinput._KeyboardState != null && keyboardinput._KeyboardState.PressedKeys.Contains(Key.D))
            {
                if (useOculusRift == 0)
                {
                    roty += speedRot;
                }
                else if (useOculusRift == 1)
                {
                    roty -= speedRot;

                }
                //Console.WriteLine("pressed D");
            }
            else if (keyboardinput._KeyboardState != null && keyboardinput._KeyboardState.PressedKeys.Contains(Key.R))
            {
                rotx -= speedRot;
                //Console.WriteLine("pressed R");
            }
            else if (keyboardinput._KeyboardState != null && keyboardinput._KeyboardState.PressedKeys.Contains(Key.F))
            {
                //Console.WriteLine("pressed F");
                rotx += speedRot;
            }

            var somerot = camera.GetRotation();
            camera.SetRotation(rotx, roty, somerot.Z);*/




        }


        //hmdmatrixRot = viewer.transform.rotation;

        float rotationincrements = 25.75f;

        if (Input.GetKey(KeyCode.A))
        {
            /*var distance = 0.1f;
            //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
            var dirForward = viewer.transform.rotation * Vector3.forward;
            dirForward.Normalize();
            MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);
            //topointforward = MOVEPOSOFFSET + pointforward;
            */
            /*var eulers = transform.eulerAngles;

            eulers.y -= 0.05f;

            //transform.eulerAngles = eulers;

            viewer.transform.Rotate(eulers, Space.Self);*/

            /*
            Quaternion q = viewer.transform.rotation;

            float x = q.x;
            float y = q.x;
            float z = q.x;
            float w = q.x;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);
            */





            /*
            pitchcurrent = scmaths.RadianToDegree(pitchcurrent);
            yawcurrent = scmaths.RadianToDegree(yawcurrent);
            rollcurrent = scmaths.RadianToDegree(rollcurrent);

            rollcurrent = rollcurrent * Mathf.PI / 180;
            pitchcurrent = pitchcurrent * Mathf.PI / 180;
            yawcurrent = yawcurrent * Mathf.PI / 180;
            
            RotationX = pitchcurrent;// rotationincrements;
            RotationY = yawcurrent - rotationincrements;// rotationincrements;
            RotationZ = rollcurrent;// rotationincrements;


            yawcurrent = RotationY;

            float pitch = RotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = RotationZ * 0.0174532925f;*/

            RotationY -= rotationincrements;//


            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = RotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = RotationZ * 0.0174532925f;
            /*this.transform.rotation.ToAngleAxis(out angle, out axis);


            Quaternion q = this.transform.rotation;

            float x = q.x;
            float y = q.x;
            float z = q.x;
            float w = q.x;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);
            */


            //Vector3 lookatlocal = transform.TransformDirection(lookAt);

            //https://answers.unity.com/questions/1611821/rotation-yaw-roll-pitch.html
            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            //viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);


            //camera.transform.rotation = camera.transform.rotation + viewer.transform.rotation;








            //Matrix4x4.Rotate

        }
        //* Time.deltaTime
        if (Input.GetKey(KeyCode.D))
        {
            /*var distance = rotationincrements;
            //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
            var dirForward = viewer.transform.rotation * -Vector3.right;
            dirForward.Normalize();
            MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);
            //topointforward = MOVEPOSOFFSET + pointforward;
            */

            /*var eulers = transform.eulerAngles;

            eulers.y += 0.05f;

            //transform.eulerAngles = eulers;

            viewer.transform.Rotate(eulers, Space.Self);*/

            /*RotationY += rotationincrements;
            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = RotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = RotationZ * 0.0174532925f;*/
            /*this.transform.rotation.ToAngleAxis(out angle, out axis);


            Quaternion q = this.transform.rotation;

            float x = q.x;
            float y = q.x;
            float z = q.x;
            float w = q.x;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);
            */


            //Vector3 lookatlocal = transform.TransformDirection(lookAt);

            /*
            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            //Matrix4x4.Rotate*/


            /*Quaternion q = viewer.transform.rotation;

            float x = q.x;
            float y = q.x;
            float z = q.x;
            float w = q.x;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


            rollcurrent = rollcurrent * Mathf.PI / 180;
            pitchcurrent = pitchcurrent * Mathf.PI / 180;
            yawcurrent = yawcurrent * Mathf.PI / 180;



            RotationX = pitchcurrent;// rotationincrements;
            RotationY = yawcurrent;// rotationincrements;
            RotationZ = rollcurrent;// rotationincrements;

            RotationY += rotationincrements;

            yawcurrent = RotationY;*/


            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            /*float pitch = RotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = RotationZ * 0.0174532925f;
            *//*this.transform.rotation.ToAngleAxis(out angle, out axis);


            Quaternion q = this.transform.rotation;

            float x = q.x;
            float y = q.x;
            float z = q.x;
            float w = q.x;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);
            */


            //Vector3 lookatlocal = transform.TransformDirection(lookAt);

            //https://answers.unity.com/questions/1611821/rotation-yaw-roll-pitch.html
            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            //viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            //viewer.transform.rotation = Quaternion.Euler(pitchcurrent, yawcurrent, rollcurrent);



            RotationY += rotationincrements;//


            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = RotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = RotationZ * 0.0174532925f;
            /*this.transform.rotation.ToAngleAxis(out angle, out axis);


            Quaternion q = this.transform.rotation;

            float x = q.x;
            float y = q.x;
            float z = q.x;
            float w = q.x;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);
            */


            //Vector3 lookatlocal = transform.TransformDirection(lookAt);

            //https://answers.unity.com/questions/1611821/rotation-yaw-roll-pitch.html
            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            //viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            //Matrix4x4.Rotate

        }



        if (Input.GetKey(KeyCode.T))
        {
            /*var distance = rotationincrements;
            //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
            var dirForward = viewer.transform.rotation * Vector3.forward;
            dirForward.Normalize();
            MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);
            //topointforward = MOVEPOSOFFSET + pointforward;
            */
            /*var eulers = transform.eulerAngles;

            eulers.x += 0.05f;

           //viewer.transform.Rotate(eulers, Space.Self);
            viewer.transform.RotateAround(transform.position, Vector3.right, 0.01f);*/

            ///transform.eulerAngles = eulers;
            ///

            upperbodypivotRotationX += rotationincrements;
            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = upperbodypivotRotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = upperbodypivotRotationZ * 0.0174532925f;
            /*this.transform.rotation.ToAngleAxis(out angle, out axis);


            Quaternion q = this.transform.rotation;

            float x = q.x;
            float y = q.x;
            float z = q.x;
            float w = q.x;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);
            */


            //Vector3 lookatlocal = transform.TransformDirection(lookAt);


            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            upperbodypivot.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            //Matrix4x4.Rotate
        }

        if (Input.GetKey(KeyCode.G))
        {
            /*var distance = rotationincrements;
            //https://answers.unity.com/questions/251619/rotation-to-direction-vector.html
            var dirForward = viewer.transform.rotation * -Vector3.right;
            dirForward.Normalize();
            MOVEPOSOFFSET = MOVEPOSOFFSET + (dirForward * distance);
            //topointforward = MOVEPOSOFFSET + pointforward;
            */

            /* var eulers = transform.eulerAngles;

             eulers.x -= 0.05f;

             //transform.eulerAngles = eulers;
             //viewer.transform.Rotate(eulers, Space.Self);

             viewer.transform.RotateAround(transform.forward,-Vector3.right,0.01f);*/

            upperbodypivotRotationX -= rotationincrements;
            // Set the yaw (Y axis), pitch (X axis), and roll (Z axis) rotations in radians.
            float pitch = upperbodypivotRotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = upperbodypivotRotationZ * 0.0174532925f;
            /*this.transform.rotation.ToAngleAxis(out angle, out axis);


            Quaternion q = this.transform.rotation;

            float x = q.x;
            float y = q.x;
            float z = q.x;
            float w = q.x;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);
            */


            //Vector3 lookatlocal = transform.TransformDirection(lookAt);


            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            upperbodypivot.transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            //Matrix4x4.Rotate
        }

        //getPan

        yield return new WaitForSeconds(0.001f);
    }

}
