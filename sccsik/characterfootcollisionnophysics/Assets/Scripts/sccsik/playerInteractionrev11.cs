using UnityEngine;
using System.Collections;
using System.Diagnostics;


public class playerInteractionrev11 : MonoBehaviour
{



    GameObject targetikfoot;// =  GameObject.CreatePrimitive(PrimitiveType.Cube);


    public Transform upperleg;
    public Transform lowerleg;
    public Transform foot;
    public Transform foottarget;
    public Transform legstaticpivot;

    float upperleglength = 0;
    float lowerleglength = 0;
    float footlength = 0;
    float totallegLength = 0;

    public LayerMask layerMask;

    Vector3 IdleStandingTargetPositionVariableLength;
    Vector3 IdleStandingTargetPositionMax;
    Vector3 IdleStandingTargetPositionMin;

    public int swtcForTypeOfInteract = 0;
    //0 == 

    public Transform footTarget;


    public Transform planetmanager;

    public byte activeBlockType = 0;
    public Transform retAdd, retDel;

    Mesh mesh;

    public float planeSize = 0.1f; //0.25f

    public Transform sphere;
    Vector3 yoMan;
    Vector3 yoMan1;

    float diameter;
    float fraction;
    float radius;
    float whatever;

    int roundedX;
    int roundedY;
    int roundedZ;

    private Camera cam;


    int multiplicator = 3;
    int multiplicatorReticle = 5;
    int tileSize = 4;
    int suppressorPos = 4;



    public int whichsiderayselect = 0;

    void Start()
    {

        targetikfoot = GameObject.CreatePrimitive(PrimitiveType.Cube);

        targetikfoot.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);




        if (swtcForTypeOfInteract == 2)
        {
            upperleglength = upperleg.localScale.z;
            lowerleglength = lowerleg.localScale.z;
            footlength = foot.localScale.z;
            totallegLength = upperleglength + lowerleglength + footlength;
            //totallegLength = upperleglength + lowerleglength; //+ footlength

            IdleStandingTargetPositionMax = transform.position + ((transform.forward * upperleglength) + (transform.forward * lowerleglength) + (transform.forward * footlength));
             IdleStandingTargetPositionMin = transform.position + ((transform.forward * (upperleglength)) + (transform.forward * (lowerleglength)) + (transform.forward * (footlength)) * 0.5f);
            
            /*IdleStandingTargetPositionMax = transform.position + ((transform.forward * upperleglength) + (transform.forward * lowerleglength) ); //+ (transform.forward * footlength)
            IdleStandingTargetPositionMin = transform.position + ((transform.forward * (upperleglength)) + (transform.forward * (lowerleglength) ) * 0.5f); //+ (transform.forward * (footlength))
            */




        }

        //retAdd.localScale = retAdd.localScale * planeSize;
        //retDel.localScale = retDel.localScale * planeSize;
        fraction = 1 / planeSize;
        radius = planeSize / 2;
        diameter = 0.1f;
        whatever = 1 / (diameter * 2);
        cam = Camera.main;
    }


    void OnGUI()
    {
        /*Vector3 point = new Vector3();
        Event currentEvent = Event.current;
        Vector2 mousePos = new Vector2();

        // Get the mouse position from Event.
        // Note that the y position from Event is inverted.
        mousePos.x = currentEvent.mousePosition.x;
        mousePos.y = cam.pixelHeight - currentEvent.mousePosition.y;

        point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, cam.nearClipPlane));

        GUILayout.BeginArea(new Rect(20, 20, 250, 120));
        GUILayout.Label("Screen pixels: " + cam.pixelWidth + ":" + cam.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + point.ToString("F3"));
        GUILayout.EndArea();*/
    }

    int counterForByteChange = 0;
    int counterForByteChangeMax = 1;


    /*
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("object hit:" + other.transform.name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //var closestPointOnBounds = other.ClosestPointOnBounds(transform.position);


        ////Debug.Log("col:" + other.transform.name);
        var sccsfracturescript = other.transform.gameObject.GetComponent<Fracture4>();

        if (sccsfracturescript != null)
        {
            sccsfracturescript.enabled = true;
        }

        //Ray ray = new Ray(transform.position, transform.forward);// Camera.main.ScreenPointToRay(Input.mousePosition);
        //RaycastHit hit;
        ////Debug.DrawRay(transform.position, transform.forward * 25, Color.green, 0.001f);

        //var someTouch0 = Input.GetTouch(0);
        ////Debug.Log(""+ someTouch0);

        //bool buttonPressedLeft = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.LTouch);
        //bool buttonPressedRight = OVRInput.Get(OVRInput.Button.PrimaryHandTrigger, OVRInput.Controller.RTouch);

        //if (buttonPressedLeft)
        {
            //Debug.Log("buttonPressedLeft:" + buttonPressedLeft);
        }
        //if (buttonPressedRight)
        {
            //Debug.Log("buttonPressedRight:" + buttonPressedRight);
        }

        //if (counterForByteChange >= counterForByteChangeMax)
        {
            //if (buttonPressedLeft || buttonPressedRight)// Input.GetMouseButton(0))
            {
                //if (Physics.Raycast(ray, out hit))
                {
                    if (collision.transform.tag == "collisionObject")
                    {
                        var chunkX = (int)(Mathf.Round(collision.transform.position.x * tileSize) / tileSize);
                        var chunkY = (int)(Mathf.Round(collision.transform.position.y * tileSize) / tileSize);
                        var chunkZ = (int)(Mathf.Round(collision.transform.position.z * tileSize) / tileSize);

                        ////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

                        if (planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)collision.transform.position.x, (int)collision.transform.position.y, (int)collision.transform.position.z) != null)
                        {
                            ////Debug.Log("==count==");
                            mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)collision.transform.position.x, (int)collision.transform.position.y, (int)collision.transform.position.z);

                            ////Debug.Log("x: " + collision.normal.x + " y: " + collision.normal.y + " z: " + collision.normal.z);
                            if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == -1) //FRONT FACE
                            {
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y + (planeSize * 0.5f), (z + (planeSize * 0.5f)));

                                //retAdd.position = retAddPos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                                ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                float itx = 0.0f;
                                float ity = 0.0f;
                                float itz = 0.0f;

                                int indexX = 0;
                                int indexY = 0;
                                int indexZ = 0;

                                for (itx = 0; itx < remainsx; itx += planeSize)
                                {
                                    if (itx >= remainsx)
                                    {
                                        break;
                                    }
                                    indexX++;
                                }
                                for (ity = 0; ity < remainsy; ity += planeSize)
                                {
                                    if (ity >= remainsy)
                                    {
                                        break;
                                    }
                                    indexY++;
                                }
                                for (itz = 0; itz < remainsz; itz += planeSize)
                                {
                                    if (itz >= remainsz)
                                    {
                                        break;
                                    }
                                    indexZ++;
                                }

                                indexX -= 1;
                                indexY -= 1;
                                indexZ -= 1;

                                ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = retAddPos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);

                            }
                            else if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == 1) //BACK FACE
                            {
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                retAdd.position = retAddPos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                                ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                float itx = 0.0f;
                                float ity = 0.0f;
                                float itz = 0.0f;

                                int indexX = 0;
                                int indexY = 0;
                                int indexZ = 0;

                                for (itx = 0; itx < remainsx; itx += planeSize)
                                {
                                    if (itx >= remainsx)
                                    {
                                        break;
                                    }
                                    indexX++;
                                }
                                for (ity = 0; ity < remainsy; ity += planeSize)
                                {
                                    if (ity >= remainsy)
                                    {
                                        break;
                                    }
                                    indexY++;
                                }
                                for (itz = 0; itz < remainsz; itz += planeSize)
                                {
                                    if (itz >= remainsz)
                                    {
                                        break;
                                    }
                                    indexZ++;
                                }

                                indexX -= 1;
                                indexY -= 1;
                                indexZ -= 1;

                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);
                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);


                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = retAddPos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);
                            }
                            else if (collision.contacts[0].normal.x == -1 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == 0) //BACK FACE
                            {
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                Vector3 retAddPos = new Vector3(x + (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                retAdd.position = retAddPos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                                ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                float itx = 0.0f;
                                float ity = 0.0f;
                                float itz = 0.0f;

                                int indexX = 0;
                                int indexY = 0;
                                int indexZ = 0;

                                for (itx = 0; itx < remainsx; itx += planeSize)
                                {
                                    if (itx >= remainsx)
                                    {
                                        break;
                                    }
                                    indexX++;
                                }
                                for (ity = 0; ity < remainsy; ity += planeSize)
                                {
                                    if (ity >= remainsy)
                                    {
                                        break;
                                    }
                                    indexY++;
                                }
                                for (itz = 0; itz < remainsz; itz += planeSize)
                                {
                                    if (itz >= remainsz)
                                    {
                                        break;
                                    }
                                    indexZ++;
                                }

                                indexX -= 1;
                                indexY -= 1;
                                indexZ -= 1;

                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = retAddPos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);
                            }
                            else if (collision.contacts[0].normal.x == 1 && collision.contacts[0].normal.y == 0 && collision.contacts[0].normal.z == 0) //BACK FACE
                            {
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y - (planeSize * 0.5f), (z - (planeSize * 0.5f)));

                                retAdd.position = retAddPos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                                ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                float itx = 0.0f;
                                float ity = 0.0f;
                                float itz = 0.0f;

                                int indexX = 0;
                                int indexY = 0;
                                int indexZ = 0;

                                for (itx = 0; itx < remainsx; itx += planeSize)
                                {
                                    if (itx >= remainsx)
                                    {
                                        break;
                                    }
                                    indexX++;
                                }
                                for (ity = 0; ity < remainsy; ity += planeSize)
                                {
                                    if (ity >= remainsy)
                                    {
                                        break;
                                    }
                                    indexY++;
                                }
                                for (itz = 0; itz < remainsz; itz += planeSize)
                                {
                                    if (itz >= remainsz)
                                    {
                                        break;
                                    }
                                    indexZ++;
                                }

                                indexX -= 1;
                                indexY -= 1;
                                indexZ -= 1;

                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = retAddPos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);
                            }
                            else if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == -1 && collision.contacts[0].normal.z == 0) //BACK FACE
                            {
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), y, (z - (planeSize * 0.5f)));

                                retAdd.position = retAddPos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                                ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                float itx = 0.0f;
                                float ity = 0.0f;
                                float itz = 0.0f;

                                int indexX = 0;
                                int indexY = 0;
                                int indexZ = 0;

                                for (itx = 0; itx < remainsx; itx += planeSize)
                                {
                                    if (itx >= remainsx)
                                    {
                                        break;
                                    }
                                    indexX++;
                                }
                                for (ity = 0; ity < remainsy; ity += planeSize)
                                {
                                    if (ity >= remainsy)
                                    {
                                        break;
                                    }
                                    indexY++;
                                }
                                for (itz = 0; itz < remainsz; itz += planeSize)
                                {
                                    if (itz >= remainsz)
                                    {
                                        break;
                                    }
                                    indexZ++;
                                }

                                indexX -= 1;
                                indexY -= 1;
                                indexZ -= 1;

                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = retAddPos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);
                            }
                            else if (collision.contacts[0].normal.x == 0 && collision.contacts[0].normal.y == 1 && collision.contacts[0].normal.z == 0) //TOP FACE
                            {
                                //Debug.Log("top face");
                                Vector3 p = collision.contacts[0].point;

                                var x = (Mathf.Round(p.x * tileSize) / tileSize);
                                var y = (Mathf.Round(p.y * tileSize) / tileSize);
                                var z = (Mathf.Round(p.z * tileSize) / tileSize);

                                Vector3 retAddPos = new Vector3(x - (planeSize * 0.5f), (y - (planeSize * 0.5f)), (z - (planeSize * 0.5f)));

                                retAdd.position = retAddPos;
                                var remainsx = Mathf.Abs(collision.transform.position.x - retAddPos.x);
                                var remainsy = Mathf.Abs(collision.transform.position.y - retAddPos.y);
                                var remainsz = Mathf.Abs(collision.transform.position.z - retAddPos.z);

                                ////Debug.Log("x: " + (remainsx) + " y: " + (remainsy) + " z: " + (remainsz));

                                float itx = 0.0f;
                                float ity = 0.0f;
                                float itz = 0.0f;

                                int indexX = 0;
                                int indexY = 0;
                                int indexZ = 0;

                                for (itx = 0; itx < remainsx; itx += planeSize)
                                {
                                    if (itx >= remainsx)
                                    {
                                        break;
                                    }
                                    indexX++;
                                }
                                for (ity = 0; ity < remainsy; ity += planeSize)
                                {
                                    if (ity >= remainsy)
                                    {
                                        break;
                                    }
                                    indexY++;
                                }
                                for (itz = 0; itz < remainsz; itz += planeSize)
                                {
                                    if (itz >= remainsz)
                                    {
                                        break;
                                    }
                                    indexZ++;
                                }

                                indexX -= 1;
                                indexY -= 1;
                                indexZ -= 1;

                                ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().SetByte((int)indexX, (int)indexY, (int)indexZ, activeBlockType);

                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().Regenerate();
                                currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().buildMesh();

                                setAdjacentChunks(currentChunk, collision.transform.position, indexX, indexY, indexZ);

                                var unityTutorialObjectPool = this.gameObject.GetComponent<NewObjectPoolerScript>();
                                var UnityTutorialPooledObject = unityTutorialObjectPool.GetPooledObject();
                                UnityTutorialPooledObject.transform.position = retAddPos;

                                UnityTutorialPooledObject.GetComponent<Fracture4>().enabled = true;
                                UnityTutorialPooledObject.SetActive(true);
                            }
                        }
                        else
                        {

                        }
                    }
                }
            }
            counterForByteChange = 0;
        }
        counterForByteChange++;
    }*/



    Ray ray;
    Vector3 positionThisObject;
    Vector3 directionForwardOfThisObject;

    void Update()
    {
        if (swtcForTypeOfInteract == 0)
        {
            
        }
        else if (swtcForTypeOfInteract == 1)
        {

        }
        else if (swtcForTypeOfInteract == 2)
        {




            var sccsikarmtargetfootl = sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().handTarget;
            var sccsikarmtargetfootr = sccsikplayer.currentsccsikplayer.arrayofikarms[3].GetComponent<sccsikarm>().handTarget;

            var lengthofarm = sccsikplayer.currentsccsikplayer.arrayofikarms[3].GetComponent<sccsikarm>().totalArmLength;

            Vector3 rayposition  = this.transform.position;
            Vector3 raypositionforward = rayposition - new Vector3(0,0,0);

            Vector3 raydirection = -this.transform.up;

            int swtcdontlookfurther = 0;



            //UnityEngine.Debug.Log("/rayposition:" + rayposition.x + "/rayposition:" + rayposition.y + "/rayposition:" + rayposition.z);

            //THE RAY LOOP - INCREMENTING THE RAY THE SIZE OF EACH BYTES EVERY FRAMES FROM THE HIP JOINT OF EACH LEGS.
            for (int y = 0; y < 15; y++)
            {

                raypositionforward = raypositionforward + (raydirection * planeSize);

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
                
                
                /*
                int rayposforinnerchunkx = raypositionx; //((((int)Mathf.Floor(raypositionforward.x * 10) / 10)) / 1);// + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl;// + sccschunkfacesbuilder.instance.chunkwl;// * (sccschunkfacesbuilder.instance.chunkwl + sccschunkfacesbuilder.instance.chunkwr + 1);
                int rayposforinnerchunky = raypositiony; //((((int)Mathf.Floor(raypositionforward.y * 10) / 10)) / 1);// + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhl;// + sccschunkfacesbuilder.instance.chunkhl;// * (sccschunkfacesbuilder.instance.chunkhl + sccschunkfacesbuilder.instance.chunkhr + 1);
                int rayposforinnerchunkz = raypositionz; //((((int)Mathf.Floor(raypositionforward.z * 10) / 10)) / 1);// + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdl;// + sccschunkfacesbuilder.instance.chunkdl;// * (sccschunkfacesbuilder.instance.chunkdl + sccschunkfacesbuilder.instance.chunkdr + 1);
                */


                raypositionx /= 4;
                raypositiony /= 4;
                raypositionz /= 4;



                /*
                UnityEngine.Debug.Log("/raypositionx:" + raypositionx + "/rayposraypositionyition:" + raypositiony + "/raypositionz:" + raypositionz);
                */


                float rayposoffsetx = sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl;
                float rayposoffsety = sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhl;
                float rayposoffsetz = sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdl;



                raypositionx /= 2;
                raypositiony /= 2;
                raypositionz /= 2;

             

                //UnityEngine.Debug.Log("/rayposoffsetx:" + rayposoffsetx + "/rayposoffsety:" + rayposoffsety + "/rayposoffsetz:" + rayposoffsetz);



                /*int rayposforinnerchunkx = ((((int)Mathf.Floor(raypositionforward.x * 10) / 10)) / 1);//
                int rayposforinnerchunky = ((((int)Mathf.Floor(raypositionforward.y * 10) / 10)) / 1);//
                int rayposforinnerchunkz = ((((int)Mathf.Floor(raypositionforward.z * 10) / 10)) / 1);//
                */

                /*float rayposforinnerchunkx = (float)Mathf.Floor(raypositionforward.x * 10.0f) / 10; //((((int)Mathf.Floor(raypositionforward.x * 10) / 10)) / 1);// + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl;// + sccschunkfacesbuilder.instance.chunkwl;// * (sccschunkfacesbuilder.instance.chunkwl + sccschunkfacesbuilder.instance.chunkwr + 1);
                 float rayposforinnerchunky = (float)Mathf.Floor(raypositionforward.y * 10.0f) / 10; //((((int)Mathf.Floor(raypositionforward.y * 10) / 10)) / 1);// + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhl;// + sccschunkfacesbuilder.instance.chunkhl;// * (sccschunkfacesbuilder.instance.chunkhl + sccschunkfacesbuilder.instance.chunkhr + 1);
                 float rayposforinnerchunkz = (float)Mathf.Floor(raypositionforward.z * 10.0f) / 10; //((((int)Mathf.Floor(raypositionforward.z * 10) / 10)) / 1);// + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdl;// + sccschunkfacesbuilder.instance.chunkdl;// * (sccschunkfacesbuilder.instance.chunkdl + sccschunkfacesbuilder.instance.chunkdr + 1);
                */

                int rayposforinnerchunkx = (int)Mathf.Floor(raypositionforward.x);
                int rayposforinnerchunky = (int)Mathf.Floor(raypositionforward.y);
                int rayposforinnerchunkz = (int)Mathf.Floor(raypositionforward.z);






                /*
                rayposforinnerchunkx /= 2;
                rayposforinnerchunky /= 2;
                rayposforinnerchunkz /= 2;*/





                /*
                rayposforinnerchunkx /= 1;
                rayposforinnerchunky /= 1;
                rayposforinnerchunkz /= 1;*/

                /*int rayposforinnerchunkbytesx = ((((int)Mathf.Floor(raypositionforward.x * 100) / 10)) / 1);//
                int rayposforinnerchunkbytesy = ((((int)Mathf.Floor(raypositionforward.y * 100) / 10)) / 1);//
                int rayposforinnerchunkbytesz = ((((int)Mathf.Floor(raypositionforward.z * 100) / 10)) / 1);//
                */
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












                /*
                 Debug.Log("x:" + sizex + "/y:" + sizey + ":z/" + sizez);
                */

                /*int raypositionx = ((((int)Mathf.Floor(raypositionforward.x * sizex) / sizex)) / planetdivsizex);// + sccschunkfacesbuilder.instance.chunkwl;// * (sccschunkfacesbuilder.instance.chunkwl + sccschunkfacesbuilder.instance.chunkwr + 1);
                int raypositiony = ((((int)Mathf.Floor(raypositionforward.y * sizey) / sizey)) / planetdivsizey);// + sccschunkfacesbuilder.instance.chunkhl;// * (sccschunkfacesbuilder.instance.chunkhl + sccschunkfacesbuilder.instance.chunkhr + 1);
                int raypositionz = ((((int)Mathf.Floor(raypositionforward.z * sizez) / sizez)) / planetdivsizez);//+ sccschunkfacesbuilder.instance.chunkdl;// * (sccschunkfacesbuilder.instance.chunkdl + sccschunkfacesbuilder.instance.chunkdr + 1);
                */
                /*
                int raypositionx = ((((int)Mathf.Floor(raypositionforward.x * 10) / 10)) / 1);// + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl;// + sccschunkfacesbuilder.instance.chunkwl;// * (sccschunkfacesbuilder.instance.chunkwl + sccschunkfacesbuilder.instance.chunkwr + 1);
                int raypositiony = ((((int)Mathf.Floor(raypositionforward.y * 10) / 10)) / 1);// + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhl;// + sccschunkfacesbuilder.instance.chunkhl;// * (sccschunkfacesbuilder.instance.chunkhl + sccschunkfacesbuilder.instance.chunkhr + 1);
                int raypositionz = ((((int)Mathf.Floor(raypositionforward.z * 10) / 10)) / 1);// + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdl;// + sccschunkfacesbuilder.instance.chunkdl;// * (sccschunkfacesbuilder.instance.chunkdl + sccschunkfacesbuilder.instance.chunkdr + 1);
                
                int rayposforinnerchunkx = ((((int)Mathf.Floor(raypositionforward.x * 10) / 10)) / 1);//
                int rayposforinnerchunky = ((((int)Mathf.Floor(raypositionforward.y * 10) / 10)) / 1);//
                int rayposforinnerchunkz = ((((int)Mathf.Floor(raypositionforward.z * 10) / 10)) / 1);//

                int rayposforinnerchunkbytesx = ((((int)Mathf.Floor(raypositionforward.x * 100) / 10)) / 1);//
                int rayposforinnerchunkbytesy = ((((int)Mathf.Floor(raypositionforward.y * 100) / 10)) / 1);//
                int rayposforinnerchunkbytesz = ((((int)Mathf.Floor(raypositionforward.z * 100) / 10)) / 1);//

                raypositionx /= 4;
                raypositiony /= 4;
                raypositionz /= 4;
                
                /*
                raypositionx = (int)(raypositionforward.x / 4);
                raypositiony = (int)(raypositionforward.y / 4);
                raypositionz = (int)(raypositionforward.z / 4);
                
                float rayposoffsetx = raypositionx + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl;
                float rayposoffsety = raypositiony + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhl;
                float rayposoffsetz = raypositionz + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdl;


                UnityEngine.Debug.Log("/raypositionx:" + raypositionx + "/raypositiony:" + raypositiony + "/raypositionz:" + raypositionz);
                */






                /*
                raypositionx += sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl;//
                raypositiony += sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhl;//
                raypositionz += sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdl;//*/

                //Debug.Log("x:" + raypositionx + "/y:" + raypositiony + ":z/" + raypositionz);
                /*Debug.Log("x:" + rayposforinnerchunkbytesx + "/y:" + rayposforinnerchunkbytesy + ":z/" + rayposforinnerchunkbytesz);
                Debug.Log("x:" + raypositionforward.x + "/y:" + raypositionforward.y + ":z/" + raypositionforward.z);
                */




                /*
                int raypositionx = ((((int)Mathf.Floor(raypositionforward.x * 10) / 10)));// * (sccschunkfacesbuilder.instance.chunkwl + sccschunkfacesbuilder.instance.chunkwr + 1);
                int raypositiony = ((((int)Mathf.Floor(raypositionforward.y * 10) / 10)));// * (sccschunkfacesbuilder.instance.chunkhl + sccschunkfacesbuilder.instance.chunkhr + 1);
                int raypositionz = ((((int)Mathf.Floor(raypositionforward.z * 10) / 10)));// * (sccschunkfacesbuilder.instance.chunkdl + sccschunkfacesbuilder.instance.chunkdr + 1);
                */

                /*int totalTimesx = 0;
                int someremainsx = 0;

                if (raypositionforward.x >= 0)
                {
                    someremainsx = (int)Mathf.Floor((raypositionx / 10)) * 10;
                    totalTimesx = (int)(raypositionx - someremainsx);
                }
                else
                {
                    someremainsx = (int)Mathf.Floor((raypositionx / 10)) * 10;
                    totalTimesx = -10 + (int)(someremainsx - raypositionx) + 10;
                    totalTimesx *= -1;
                }
                Debug.Log("remainsx" + someremainsx + "/totalTimesx:" + totalTimesx);
                */






                /*float raypositionx = raypositionforward.x * planetdivsizex * 0.1f;
                float raypositiony = raypositionforward.y * planetdivsizey * 0.1f;
                float raypositionz = raypositionforward.z * planetdivsizez * 0.1f;
                */


                /*int raypositionx = ((((int)Mathf.Floor(raypositionforward.x * 20) / 20) / chunksizex) * planetdivsizex);// * (sccschunkfacesbuilder.instance.chunkwl + sccschunkfacesbuilder.instance.chunkwr + 1);
                int raypositiony = ((((int)Mathf.Floor(raypositionforward.y * 20) / 20) / chunksizey) * planetdivsizey);// * (sccschunkfacesbuilder.instance.chunkhl + sccschunkfacesbuilder.instance.chunkhr + 1);
                int raypositionz = ((((int)Mathf.Floor(raypositionforward.z * 20) / 20) / chunksizez) * planetdivsizez);// * (sccschunkfacesbuilder.instance.chunkdl + sccschunkfacesbuilder.instance.chunkdr + 1);
                */
                /*
                int raypositionx = ((((int)Mathf.Floor(raypositionforward.x * 10) / 10)));// * (sccschunkfacesbuilder.instance.chunkwl + sccschunkfacesbuilder.instance.chunkwr + 1);
                int raypositiony = ((((int)Mathf.Floor(raypositionforward.y * 10) / 10)));// * (sccschunkfacesbuilder.instance.chunkhl + sccschunkfacesbuilder.instance.chunkhr + 1);
                int raypositionz = ((((int)Mathf.Floor(raypositionforward.z * 10) / 10)));// * (sccschunkfacesbuilder.instance.chunkdl + sccschunkfacesbuilder.instance.chunkdr + 1);
                */


                float somevaldiv = 1.0f / ((0.01f));


                //Console.WriteLine(somevaldiv);
                //0.01f == 4 face == 1 chunk => 0.0025f for 1 face

                //float someposfootx = (float)((Math.Floor(pickaxetippoint.X / somevaldiv) * somevaldiv)) / (4);
                //float someposfooty = (float)((Math.Floor(pickaxetippoint.Y / somevaldiv) * somevaldiv)) / (4);
                //float someposfootz = (float)((Math.Floor(pickaxetippoint.Z / somevaldiv) * somevaldiv)) / (4);









                /*
                int someposfootx = (int)(((float)Mathf.Floor(raypositionforward.x * somevaldiv) / somevaldiv));// / (0.01f)); ;// / (0.01f));
                int someposfooty = (int)(((float)Mathf.Floor(raypositionforward.y * somevaldiv) / somevaldiv));// / (0.01f)); ;// / (0.01f));
                int someposfootz = (int)(((float)Mathf.Floor(raypositionforward.z * somevaldiv) / somevaldiv));// / (0.01f)); ;/// (0.01f));

                Debug.Log("/ix:" + (someposfootx) + "/iy:" + (someposfooty) + "/iz:" + (someposfootz));

                //int someposfootx = (int)(((float)Math.Floor(pickaxetippoint.X * somevaldiv) / somevaldiv) / (tutorialcubeaschunkinst.currenttutorialcubeaschunkinst.somelevelgenprimglobals.planeSize));
                //int someposfooty = (int)(((float)Math.Floor(pickaxetippoint.Y * somevaldiv) / somevaldiv) / (tutorialcubeaschunkinst.currenttutorialcubeaschunkinst.somelevelgenprimglobals.planeSize));
                //int someposfootz = (int)(((float)Math.Floor(pickaxetippoint.Z * somevaldiv) / somevaldiv) / (tutorialcubeaschunkinst.currenttutorialcubeaschunkinst.somelevelgenprimglobals.planeSize));
                //someposfooty -= 1;

                int totalTimesx = 0;
                int totalTimesy = 0;
                int totalTimesz = 0;

                int someremainsx = 0;
                int someremainsy = 0;
                int someremainsz = 0;

                if (raypositionforward.x >= 0)
                {
                    someremainsx = (int)Mathf.Floor((someposfootx / 4.0f)) * 4;
                    totalTimesx = (int)(someposfootx - someremainsx);
                }
                else
                {
                    someremainsx = (int)Mathf.Floor((someposfootx / 4.0f)) * 4;
                    totalTimesx = -4 + (int)(someremainsx - someposfootx) + 4;
                    totalTimesx *= -1;
                }

                if (raypositionforward.y >= 0)
                {
                    someremainsy = (int)Mathf.Floor((someposfooty / 4.0f)) * 4;
                    totalTimesy = (int)(someposfooty - someremainsy);
                }
                else
                {
                    someremainsy = (int)Mathf.Floor((someposfooty / 4.0f)) * 4;
                    totalTimesy = -4 + (int)(someremainsy - someposfooty) + 4;
                    totalTimesy *= -1;
                }

                if (raypositionforward.z >= 0)
                {
                    someremainsz = (int)Mathf.Floor((someposfootz / 4.0f)) * 4;
                    totalTimesz = (int)(someposfootz - someremainsz);
                }
                else
                {
                    someremainsz = (int)Mathf.Floor((someposfootz / 4.0f)) * 4;
                    //Console.WriteLine(someremainsz);

                    totalTimesz = -4 + (int)(someremainsz - someposfootz) + 4;
                    totalTimesz *= -1;
                }

                int totaltimesforonepartschunksx = (someremainsx / 4);
                int totaltimesforonepartschunksy = (someremainsy / 4);
                int totaltimesforonepartschunksz = (someremainsz / 4);
                */


                //Console.WriteLine("x:" + someremainsx + "/y:" + someremainsy + ":z/" + someremainsz);

                //Console.WriteLine("index:" + someindexmap + "/0x:" + someposfootx + "/0y:" + someposfooty + "/0z:" + someposfootz + "/ix:" + indexx + "/iy:" + indexy + "/iz:" + indexz + " " + sometotaltimesx + " " + sometotaltimesy + " " + sometotaltimesz + "/ttx:" + totalTimesx + "/tty:" + totalTimesy + "/ttz:" + totalTimesz + "/index:" + someindexmap);
                /*
                Debug.Log("/0x:" + someposfootx + "/0y:" + someposfooty + "/0z:" + someposfootz + "/ttx:" + totalTimesx + "/tty:" + totalTimesy + "/ttz:" + totalTimesz);


                Debug.Log("x:" + totaltimesforonepartschunksx + "/y:" + totaltimesforonepartschunksy + ":z/" + totaltimesforonepartschunksz);
                */
                /*
                int totaltimesforonepartschunksx = (someposfootx / 4) / 2;// (someremainsx / 8);
                int totaltimesforonepartschunksy = (someposfooty / 4) / 2;//(someremainsy / 8);
                int totaltimesforonepartschunksz = (someposfootz / 4) / 2;//(someremainsz / 8);*/

                //Console.WriteLine("x:" + someremainsx + "/y:" + someremainsy + ":z/" + someremainsz);





                /*
                int thechunkinthebundlex = (someremainsx / 4);
                int thechunkinthebundley = (someremainsy / 4);
                int thechunkinthebundlez = (someremainsz / 4);


                float raypositionx = totalTimesx;
                float raypositiony = totalTimesy;
                float raypositionz = totalTimesz;

                */

                /*
                int raypositionx = (((int)Mathf.Floor(raypositionforward.x * 10) / 10) ) * ;// * (sccschunkfacesbuilder.instance.chunkwl + sccschunkfacesbuilder.instance.chunkwr + 1);
                int raypositiony = (((int)Mathf.Floor(raypositionforward.y * 10) / 10) );// * (sccschunkfacesbuilder.instance.chunkhl + sccschunkfacesbuilder.instance.chunkhr + 1);
                int raypositionz = (((int)Mathf.Floor(raypositionforward.z * 10) / 10) );// * (sccschunkfacesbuilder.instance.chunkdl + sccschunkfacesbuilder.instance.chunkdr + 1);
                */

                //sccschunkfacesbuilder.instance.chunkwl
                //sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl





                //Debug.Log(chunksizex);



                /*
                if (raypositionx< 0)
                {
                    raypositionx *= -1;

                    raypositionx = raypositionx + sccschunkfacesbuilder.instance.chunkwr;
                }

                if (raypositiony < 0)
                {
                    raypositiony *= -1;

                    raypositiony = raypositiony + sccschunkfacesbuilder.instance.chunkhr;
                }


                if (raypositionz < 0)
                {
                    raypositionz *= -1;

                    raypositionz = raypositionz + sccschunkfacesbuilder.instance.chunkdr;
                }*/


                /*targetikfoot.transform.position = new Vector3(raypositionx, raypositiony, raypositionz);
                */


                if (swtcdontlookfurther == 0)
                {

                    //UnityEngine.Debug.Log("/x:" + sometestx + "/y:" + sometesty + "/z:" + sometestz);


                 

                    
                    UnityEngine.Debug.Log("/raypositionx:" + raypositionx + "/raypositiony:" + raypositiony + "/raypositionz:" + raypositionz);
                    
                    
                    var planetdiv = sccschunkfacesbuilder.instance.getplanetdiv(raypositionx, raypositiony, raypositionz);

                    if (planetdiv != null)
                    {

                        /*rayposforinnerchunkx /= 2;
                        rayposforinnerchunky /= 2;
                        rayposforinnerchunkz /= 2;*/
                        float sometestx = 0;
                        float sometesty = 0;
                        float sometestz = 0;

                        /*
                        sometestx = rayposforinnerchunkx - rayposoffsetx;
                        sometesty = rayposforinnerchunky - rayposoffsety;
                        sometestz = rayposforinnerchunkz - rayposoffsetz;
                        */
                        

                        
                        
                        if (raypositionforward.x >= 0)
                        {
                            sometestx = rayposforinnerchunkx - rayposoffsetx;
                        }
                        else
                        {
                            sometestx = rayposforinnerchunkx + rayposoffsetx;

                            //sometestx = rayposforinnerchunkx + rayposoffsetx;

                            //sometestx =  rayposoffsetx - rayposforinnerchunkx;

                        }
                        
                        if (raypositionforward.y >= 0)
                        {
                            sometesty = rayposforinnerchunky - rayposoffsety;
                        }
                        else
                        {
                            sometesty = rayposforinnerchunky + rayposoffsety;

                            //sometesty = rayposoffsety - rayposforinnerchunky;
                        }

                        if (raypositionforward.z >= 0)
                        {
                            sometestz = rayposforinnerchunkz - rayposoffsetz;
                        }
                        else
                        {
                            //sometestz = rayposoffsetz - rayposforinnerchunkz;
                            sometestz = rayposforinnerchunkz + rayposoffsetz;
                        }


                        /*
                        if (raypositionforward.x >= 0)
                        {
                            sometestx =((int)Mathf.Floor((rayposforinnerchunkx / 10.0f)) * 10) - rayposoffsetx;
                            //totalTimesx = (int)(rayposforinnerchunkx - someremainsx);
                        }
                        else
                        {
                        
                        }



                        if (raypositionforward.y >= 0)
                        {
                            sometesty = ((int)Mathf.Floor((rayposforinnerchunky / 10.0f)) * 10) - rayposoffsety;

                        }
                        else
                        {
                            
                        }


                        if (raypositionforward.z >= 0)
                        {
                            sometestz = ((int)Mathf.Floor((rayposforinnerchunkz / 10.0f)) * 10) - rayposoffsetz;

                        }
                        else
                        {
                            
                        }*/





                        /*
                        UnityEngine.Debug.Log("/x:" + sometestx + "/y:" + sometesty + "/z:" + sometestz);
                        */


                        /*

                        UnityEngine.Debug.Log("/x:" + sometestx + "/y:" + sometesty + "/z:" + sometestz);
                        
                        */



                        /*if (rayposforinnerchunkx < 0)
                        {
                            sometestx = rayposforinnerchunkx - -rayposoffsetx;

                        }
                        else
                        {
                            sometestx = rayposforinnerchunkx - rayposoffsetx;
                        }

                        if (rayposforinnerchunky < 0)
                        {
                            sometesty = rayposforinnerchunky - -rayposoffsety;

                        }
                        else
                        {
                            sometesty = rayposforinnerchunky - rayposoffsety;

                        }
                        if (rayposforinnerchunkz < 0)
                        {
                            sometestz = rayposforinnerchunkz - -rayposoffsetz;

                        }
                        else
                        {
                            sometestz = rayposforinnerchunkz -rayposoffsetz;

                        } */


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
                        if (raypositionforward.x >= 0)
                        {
                            someremainsx = (int)Mathf.Floor((raypositionforwardclampedx / 10.0f)) * 10;
                            totalTimesx = (int)(raypositionforwardclampedx - someremainsx);
                        }
                        else
                        {
                            /*someremainsx = (int)Mathf.Floor((raypositionforwardclampedx / 10.0f)) * 10;
                            totalTimesx = -10 + (int)(someremainsx - raypositionforwardclampedx) + 10;
                            totalTimesx *= -1;*/

                            /*someremainsx = (int)(Mathf.Floor(raypositionforward.x / 10.0f) * 10.0f);

                            totalTimesx = ((int)((raypositionforward.x - someremainsx) * 10.0f)) / 10;// (int)Mathf.Floor((raypositionforward.z - (someremainsz)) / 10.0f);// ( * 100.0f)
                            

                            someremainsx = (raypositionforwardclampedx / 10) - 1;
                            totalTimesx = (raypositionforwardclampedx - (someremainsx * 10)) - 1;
                            //totalTimesx = (int)(totalTimesx - rayposoffsetx);
                        }



                        if (raypositionforward.y >= 0)
                        {
                            someremainsy = (int)Mathf.Floor((raypositionforwardclampedy / 10.0f)) * 10;
                            totalTimesy = (int)(raypositionforwardclampedy - someremainsy);
                        }
                        else
                        {
                            /*someremainsy = (int)Mathf.Floor((raypositionforwardclampedy / 10.0f)) * 10;
                            totalTimesy = -10 + (int)(raypositionforwardclampedy - rayposforinnerchunkbytesy) + 10;
                            totalTimesy *= -1;*/


                            /*someremainsy = (int)(Mathf.Floor(raypositionforward.y / 10.0f) * 10.0f);

                            totalTimesy = ((int)((raypositionforward.y - someremainsy) * 10.0f)) / 10;// (int)Mathf.Floor((raypositionforward.z - (someremainsz)) / 10.0f);// ( * 100.0f)
                            
                            someremainsy = (raypositionforwardclampedy / 10) - 1;
                            totalTimesy = (raypositionforwardclampedy - (someremainsy * 10)) - 1;
                            //totalTimesy = (int)(totalTimesy - rayposoffsety);
                        }


                        if (raypositionforward.z >= 0)
                        {
                            someremainsz = (int)Mathf.Floor((raypositionforwardclampedz / 10.0f)) * 10;
                            totalTimesz = (int)(raypositionforwardclampedz - someremainsz);
                        }
                        else
                        {
                            /*
                             someremainsz = (int)Mathf.Floor((rayposforinnerchunkbytesz / 10.0f)) * 10;
                             totalTimesz = -10 + (int)(someremainsz - rayposforinnerchunkbytesz) + 10;
                             totalTimesz *= -1;*/
                            /*
                            someremainsz = ((int)Mathf.Floor((rayposforinnerchunkz / 10.0f)) * 10);
                            totalTimesz = (int)(rayposforinnerchunkz - someremainsz);
                            totalTimesz *= -1;*/

                            /*someremainsz = (int)(Mathf.Floor(raypositionforward.z / 10.0f) * 10.0f);

                            totalTimesz = ((int)((raypositionforward.z - someremainsz) * 10.0f)) / 10;// (int)Mathf.Floor((raypositionforward.z - (someremainsz)) / 10.0f);// ( * 100.0f)
                            

                            someremainsz = (raypositionforwardclampedz / 10) - 1;
                            totalTimesz = (raypositionforwardclampedz - (someremainsz * 10)) - 1;


                            //totalTimesz = (int)(totalTimesz - rayposoffsetz);
                            //totalTimesz = 10 - 1 - totalTimesz;

                        }*/









                        /*
                        UnityEngine.Debug.Log("/x:" + raypositionforwardclampedx + "/y:" + raypositionforwardclampedy + "/z:" + raypositionforwardclampedz);

                        UnityEngine.Debug.Log("/someremainsx:" + someremainsx + "/someremainsy:" + someremainsy + "/someremainsz:" + someremainsz);
                        //UnityEngine.Debug.Log("/totalTimesx:" + totalTimesx + "/totalTimesy:" + totalTimesy + "/totalTimesz:" + totalTimesz);
                        */
                        UnityEngine.Debug.Log("/x:" + rayposforinnerchunkx + "/y:" + rayposforinnerchunky + "/z:" + rayposforinnerchunkz);
                        UnityEngine.Debug.Log("/x:" + sometestx + "/y:" + sometesty + "/z:" + sometestz);
                        
                        //var thechunk = planetdiv.getChunk(rayposforinnerchunkx, rayposforinnerchunky, rayposforinnerchunkz);
                        var thechunk = planetdiv.getChunk((int)sometestx, (int)sometesty, (int)sometestz);

                        if (thechunk != null)
                        {

                            targetikfoot.transform.position = new Vector3(thechunk.chunkpos.x, thechunk.chunkpos.y, thechunk.chunkpos.z);



                            if (thechunk.thebytemap != null)
                            {



                                //Debug.Log("thechunk.thebytemap != null");

                                var thebytemap = thechunk.thebytemap;

                                //rayposforinnerchunkbytesx
                                int multipleofx = 0;
                                int remnantsx = 0;

                                int multipleofy = 0;
                                int remnantsy = 0;

                                int multipleofz = 0;
                                int remnantsz = 0;
                                /*
                                if (rayposition.x < 0)
                                {
                                    //UnityEngine.Debug.Log("rayposition.z < 0");

                                    /*multipleofx = (rayposforinnerchunkbytesx/ 10);
                                    remnantsx= rayposforinnerchunkbytesx - ((multipleofx * 10));

                                    //remnantsz *= -1;
                                    remnantsx = 10 + remnantsx;// - 1 + remnantsz;
                                }
                                else
                                {
                                    multipleofx = (rayposforinnerchunkbytesx / 10);
                                    remnantsx = rayposforinnerchunkbytesx - ((multipleofx * 10));

                                }


                                if (rayposition.y < 0)
                                {
                                    //UnityEngine.Debug.Log("rayposition.z < 0");

                                    /*multipleofy = (rayposforinnerchunkbytesy / 10);
                                    remnantsy = rayposforinnerchunkbytesy - ((multipleofy * 10));

                                    //remnantsz *= -1;
                                    remnantsy = 10 + remnantsy;// - 1 + remnantsz;
                                }
                                else
                                {
                                    multipleofy = (rayposforinnerchunkbytesy / 10);
                                    remnantsy = rayposforinnerchunkbytesy - ((multipleofy * 10));

                                }


                                if (rayposition.z < 0)
                                {
                                    //UnityEngine.Debug.Log("rayposition.z < 0");

                                    /*multipleofz = (rayposforinnerchunkbytesz / 10);
                                    remnantsz = rayposforinnerchunkbytesz - ((multipleofz * 10));

                                    //remnantsz *= -1;
                                    remnantsz = 10 + remnantsz;// - 1 + remnantsz;

                                }
                                else
                                {
                                    multipleofz = (rayposforinnerchunkbytesz / 10);
                                    remnantsz = rayposforinnerchunkbytesz - ((multipleofz * 10));


                                }*/

                                /*
                                //var someremainstest = (int)Mathf.Floor((-9 / 10.0f)) * 10;
                                var someremainstest = (int)Mathf.Floor(-0.1f);

                                UnityEngine.Debug.Log("/raypositionforwardclampedz:" + raypositionforwardclampedz + "/someremainsz:" + someremainsz + "/someremainstest:" + someremainstest + "/rayposforinnerchunkbytesz:" + rayposforinnerchunkbytesz + "/raypositionforward.z:" + raypositionforward.z);
                                */





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
                                    totalTimesx = (int)(raypositionforwardclampedx - someremainsx) ;
                                }
                                else
                                {
                                    /*someremainsx = (int)Mathf.Floor((raypositionforwardclampedx / 10.0f)) * 10;
                                    totalTimesx = -10 + (int)(someremainsx - raypositionforwardclampedx) + 10;
                                    totalTimesx *= -1;*/

                                    /*someremainsx = (int)(Mathf.Floor(raypositionforward.x / 10.0f) * 10.0f);

                                    totalTimesx = ((int)((raypositionforward.x - someremainsx) * 10.0f)) / 10;// (int)Mathf.Floor((raypositionforward.z - (someremainsz)) / 10.0f);// ( * 100.0f)
                                    */

                                    /*someremainsx = (raypositionforwardclampedx / 10) - 1;
                                    totalTimesx = (raypositionforwardclampedx - (someremainsx * 10)) - 1;*/




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
                                    totalTimesy = (int)(raypositionforwardclampedy - someremainsy) ;
                                }
                                else
                                {
                                    /*someremainsy = (int)Mathf.Floor((raypositionforwardclampedy / 10.0f)) * 10;
                                    totalTimesy = -10 + (int)(raypositionforwardclampedy - rayposforinnerchunkbytesy) + 10;
                                    totalTimesy *= -1;*/


                                    /*someremainsy = (int)(Mathf.Floor(raypositionforward.y / 10.0f) * 10.0f);

                                    totalTimesy = ((int)((raypositionforward.y - someremainsy) * 10.0f)) / 10;// (int)Mathf.Floor((raypositionforward.z - (someremainsz)) / 10.0f);// ( * 100.0f)
                                    */
                                    /* someremainsy = (raypositionforwardclampedy / 10) - 1;
                                     totalTimesy = (raypositionforwardclampedy - (someremainsy * 10)) - 1;
                                    */



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
                                    totalTimesz = (int)(raypositionforwardclampedz - someremainsz) ;
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













                                    /*
                                     someremainsz = (int)Mathf.Floor((rayposforinnerchunkbytesz / 10.0f)) * 10;
                                     totalTimesz = -10 + (int)(someremainsz - rayposforinnerchunkbytesz) + 10;
                                     totalTimesz *= -1;*/
                                    /*
                                    someremainsz = ((int)Mathf.Floor((raypositionforwardclampedz / 10.0f)) * 10);
                                    totalTimesz = (int)(raypositionforwardclampedz - someremainsz);
                                    totalTimesz *= -1;*/

                                    /*someremainsz = (int)(Mathf.Floor(raypositionforward.z / 10.0f) * 10.0f);

                                    totalTimesz = ((int)((raypositionforward.z - someremainsz) * 10.0f)) / 10;// (int)Mathf.Floor((raypositionforward.z - (someremainsz)) / 10.0f);// ( * 100.0f)
                                    */

                                    /*
                                    float theresultontop = (Mathf.Floor(raypositionforwardclampedz / 10.0f));

                                    //theremainsz = 10 -(10 - raypositionforwardclampedz - ((raypositionforwardclampedz / 10.0f) - 1) - 10);

                                    theremainsz  = (raypositionforwardclampedz - theresultontop);

                                    theremainsz /= 10;
                                    float theotherresultontop = (float)Mathf.Floor(theremainsz / 10.0f) - 1;

                                    theremainsz = theremainsz - theotherresultontop;



                                    theremainsz *= -1;
                                    //theremainsz = 10 - theremainsz;

                                    //someremainsz = (raypositionforwardclampedz / 10) - 1;
                                    totalTimesz = (int)(theremainsz);// (raypositionforwardclampedz - (someremainsz * 10)) - 1;


                                    UnityEngine.Debug.Log("/theresultontop:" + theresultontop);
                                    */









                                    //totalTimesz = 10 - 1 - totalTimesz;

                                }





                                UnityEngine.Debug.Log("/totalTimesx:" + totalTimesx + "/totalTimesy:" + totalTimesy + "/totalTimesz:" + totalTimesz + "/theremainsz:" + theremainsz + "/raypositionforwardclampedz:" + raypositionforwardclampedz + "/raypositionforward.z:" + raypositionforward.z);



                                remnantsx = totalTimesx;
                                remnantsy = totalTimesy;
                                remnantsz = totalTimesz;





                                /*
                                if (remnantsx < 0)
                                {
                                    remnantsx *= -1;
                                    remnantsx = 10 - 1 - remnantsx;
                                }
                                if (remnantsy < 0)
                                {
                                    remnantsy *= -1;
                                    remnantsy = 10 - 1 - remnantsy;

                                }
                                if (remnantsz < 0)
                                {
                                    remnantsz *= -1;
                                    remnantsz = 10 - 1 - remnantsz;

                                }*/




                                /*
                                UnityEngine.Debug.Log("/chunkposx:" + thechunk.chunkpos.x + "/chunkposy:" + thechunk.chunkpos.y + "/chunkposz:" + thechunk.chunkpos.z);
                                */


                                int indexofchunkbytemap = remnantsx + (10) * (remnantsy + (10) * remnantsz);


                                if (indexofchunkbytemap >= 0 && indexofchunkbytemap < 10 * 10 * 10)
                                {

                                    if (thechunk.thebytemap[indexofchunkbytemap] == 1)
                                    {
                                        float offsetposx = 0.05f;
                                        float offsetposy = 0.05f;
                                        float offsetposz = 0.05f;

                                        offsetposy += 0.10f;

                                        /*if (whichsiderayselect == 0)
                                        {
                                            sccsikarmtargetfootl.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                        }
                                        else if (whichsiderayselect == 1)
                                        {
                                            sccsikarmtargetfootr.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                        }*/


                                        Vector3 position = new Vector3((float)rayposforinnerchunkx + ((float)remnantsx * planeSize) + offsetposx, (float)rayposforinnerchunky + ((float)remnantsy * planeSize) + offsetposy, ((float)rayposforinnerchunkz) + ((float)remnantsz * planeSize) + offsetposz);
                                        //Vector3 position = new Vector3((float)rayposforinnerchunkx, (float)rayposforinnerchunky , ((float)rayposforinnerchunkz));

                                        if (whichsiderayselect == 0)
                                        {
                                            sccsikplayer.currentsccsikplayer.arrayofikarms[2].GetComponent<sccsikarm>().handTarget.transform.position = position;// new Vector3(rayposforinnerchunkx + (remnantsx * planeSize) + offsetposx, rayposforinnerchunky + (remnantsy * planeSize) + offsetposy, rayposforinnerchunkz + (remnantsz * planeSize) + offsetposz);
                                        }
                                        else if (whichsiderayselect == 1)
                                        {
                                            sccsikplayer.currentsccsikplayer.arrayofikarms[3].GetComponent<sccsikarm>().handTarget.transform.position = position;// new Vector3(rayposforinnerchunkx + (remnantsx * planeSize) + offsetposx, rayposforinnerchunky + (remnantsy * planeSize) + offsetposy, rayposforinnerchunkz + (remnantsz * planeSize) + offsetposz);

                                        }


                                        UnityEngine.Debug.Log(position + "/z:" + (((float)rayposforinnerchunkz) + ((float)remnantsz * planeSize) + offsetposz));


                                        break;
                                        /*
                                        UnityEngine.Debug.Log("/bytex:" + remnantsx + "/bytey:" + remnantsy + "/bytez:" + remnantsz);
                                        */
                                        // Debug.Log("/bytex:" + remnantsx + "/bytey:" + remnantsy + "/bytez:" + remnantsz);






                                        /*
                                        swtcdontlookfurther = 1;
                                        var sccsplayerscript = sccsikplayer.currentsccsikplayer.themovementplayerscript;// arrayofikarms[2].GetComponent<sccsplayer>();

                                        if (sccsplayerscript != null)
                                        {
                                            //Debug.Log("sccsplayerscript != null");

                                            //UnityEngine.Debug.Log("faceindex:" + sccsplayerscript.indexofmaxvalueofperfacegravity);

                                            if (sccsplayerscript.indexofmaxvalueofperfacegravity == 0)
                                            {
                                                //UnityEngine.Debug.Log("indexofmaxvalueofperfacegravity == 0");
                                                float offsetposx = 0.05f;
                                                float offsetposy = 0.05f;
                                                float offsetposz = 0.05f;

                                                offsetposy += 0.10f;

                                                if (whichsiderayselect == 0)
                                                {
                                                    sccsikarmtargetfootl.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                                else if (whichsiderayselect == 1)
                                                {
                                                    sccsikarmtargetfootr.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                            }
                                            else if (sccsplayerscript.indexofmaxvalueofperfacegravity == 1)
                                            {
                                                float offsetposx = 0.05f;
                                                float offsetposy = 0.05f;
                                                float offsetposz = 0.05f;

                                                offsetposx += 0.10f;

                                                if (whichsiderayselect == 0)
                                                {
                                                    sccsikarmtargetfootl.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                                else if (whichsiderayselect == 1)
                                                {
                                                    sccsikarmtargetfootr.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                            }
                                            else if (sccsplayerscript.indexofmaxvalueofperfacegravity == 2)
                                            {
                                                float offsetposx = 0.05f;
                                                float offsetposy = 0.05f;
                                                float offsetposz = 0.05f;

                                                offsetposx += 0.10f;

                                                if (whichsiderayselect == 0)
                                                {
                                                    sccsikarmtargetfootl.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                                else if (whichsiderayselect == 1)
                                                {
                                                    sccsikarmtargetfootr.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                            }
                                            else if (sccsplayerscript.indexofmaxvalueofperfacegravity == 3)
                                            {
                                                float offsetposx = 0.05f;
                                                float offsetposy = 0.05f;
                                                float offsetposz = 0.05f;

                                                offsetposz += 0.10f;

                                                if (whichsiderayselect == 0)
                                                {
                                                    sccsikarmtargetfootl.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                                else if (whichsiderayselect == 1)
                                                {
                                                    sccsikarmtargetfootr.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                            }
                                            else if (sccsplayerscript.indexofmaxvalueofperfacegravity == 4)
                                            {
                                                float offsetposx = 0.05f;
                                                float offsetposy = 0.05f;
                                                float offsetposz = 0.05f;

                                                offsetposz += 0.10f;

                                                if (whichsiderayselect == 0)
                                                {
                                                    sccsikarmtargetfootl.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                                else if (whichsiderayselect == 1)
                                                {
                                                    sccsikarmtargetfootr.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                            }
                                            else if (sccsplayerscript.indexofmaxvalueofperfacegravity == 5)
                                            {
                                                float offsetposx = 0.05f;
                                                float offsetposy = 0.05f;
                                                float offsetposz = 0.05f;

                                                offsetposy += 0.10f;

                                                if (whichsiderayselect == 0)
                                                {
                                                    sccsikarmtargetfootl.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                                else if (whichsiderayselect == 1)
                                                {
                                                    sccsikarmtargetfootr.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                            }

                                        }
                                        break;*/

                                    }
                                    else
                                    {




                                        /*
                                        var sccsplayerscript = sccsikplayer.currentsccsikplayer.themovementplayerscript;

                                        if (sccsplayerscript != null)
                                        {
                                            if (sccsplayerscript.indexofmaxvalueofperfacegravity == 0)
                                            {
                                                float offsetposx = 0.05f;
                                                float offsetposy = 0.05f;
                                                float offsetposz = 0.05f;

                                                offsetposy += 0.10f;

                                                if (whichsiderayselect == 0)
                                                {


                                                    Vector3 tempDir = legstaticpivot.position - foottarget.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                                                    /*if (tempDir.magnitude >= totallegLength)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                                                    {
                                                        foottarget.position = IdleStandingTargetPositionMax;
                                                        tempDir.Normalize();
                                                        //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                                                        Vector3 tempVect = (legstaticpivot.position + (tempDir * ((totallegLength * 0.5f)))) + (-tempDir * foot.localScale.y);
                                                        //MOVINGPOINTER.X = tempVect.X;
                                                        //MOVINGPOINTER.Y = tempVect.Y;
                                                        //MOVINGPOINTER.Z = tempVect.Z;
                                                        foottarget.position = tempVect;// hit.point;
                                                    }
                                                    else
                                                    {
                                                        foottarget.position = IdleStandingTargetPositionMax;
                                                        //foottarget.position = hit.point + (tempDir * foot.localScale.y);
                                                    }




                                                    sccsikarmtargetfootl.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                                else if (whichsiderayselect == 1)
                                                {
                                                    sccsikarmtargetfootr.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                            }
                                            else if (sccsplayerscript.indexofmaxvalueofperfacegravity == 1)
                                            {
                                                float offsetposx = 0.05f;
                                                float offsetposy = 0.05f;
                                                float offsetposz = 0.05f;

                                                offsetposx += 0.10f;

                                                if (whichsiderayselect == 0)
                                                {
                                                    sccsikarmtargetfootl.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                                else if (whichsiderayselect == 1)
                                                {
                                                    sccsikarmtargetfootr.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                            }
                                            else if (sccsplayerscript.indexofmaxvalueofperfacegravity == 2)
                                            {
                                                float offsetposx = 0.05f;
                                                float offsetposy = 0.05f;
                                                float offsetposz = 0.05f;

                                                offsetposx += 0.10f;

                                                if (whichsiderayselect == 0)
                                                {
                                                    sccsikarmtargetfootl.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                                else if (whichsiderayselect == 1)
                                                {
                                                    sccsikarmtargetfootr.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                            }
                                            else if (sccsplayerscript.indexofmaxvalueofperfacegravity == 3)
                                            {
                                                float offsetposx = 0.05f;
                                                float offsetposy = 0.05f;
                                                float offsetposz = 0.05f;

                                                offsetposz += 0.10f;

                                                if (whichsiderayselect == 0)
                                                {
                                                    sccsikarmtargetfootl.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                                else if (whichsiderayselect == 1)
                                                {
                                                    sccsikarmtargetfootr.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                            }
                                            else if (sccsplayerscript.indexofmaxvalueofperfacegravity == 4)
                                            {
                                                float offsetposx = 0.05f;
                                                float offsetposy = 0.05f;
                                                float offsetposz = 0.05f;

                                                offsetposz += 0.10f;

                                                if (whichsiderayselect == 0)
                                                {
                                                    sccsikarmtargetfootl.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                                else if (whichsiderayselect == 1)
                                                {
                                                    sccsikarmtargetfootr.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                            }
                                            else if (sccsplayerscript.indexofmaxvalueofperfacegravity == 5)
                                            {
                                                float offsetposx = 0.05f;
                                                float offsetposy = 0.05f;
                                                float offsetposz = 0.05f;

                                                offsetposy += 0.10f;

                                                if (whichsiderayselect == 0)
                                                {
                                                    sccsikarmtargetfootl.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                                else if (whichsiderayselect == 1)
                                                {
                                                    sccsikarmtargetfootr.transform.position = new Vector3(thechunk.chunkpos.x + (remnantsx * planeSize) + offsetposx, thechunk.chunkpos.y + (remnantsy * planeSize) + offsetposy, thechunk.chunkpos.z + (remnantsz * planeSize) + offsetposz);
                                                }
                                            }

                                        }*/

                                    }


                                }





                            }
                        }
                        else
                        {

                        }
                    }




                }


                /*if ()
                {

                }*/




            }





            /*
            if (counterForIkFootPlacement <= counterForIkFootPlacementMax)
            {
                var ray = new Ray(transform.position, -transform.up);

                RaycastHit hit;
                Debug.DrawRay(transform.position,-transform.up * 1, Color.green, 0.001f);

                ray = new Ray(legstaticpivot.position, -transform.up);

                //RaycastHit hittwo;
                //Debug.DrawRay(transform.position, transform.forward * 1, Color.green, 0.001f);

                if (Physics.Raycast(ray, out hit, totallegLength, layerMask))
                {
                    if (hit.transform.tag == "collisionObject")
                    {
                        Vector3 tempDir = legstaticpivot.position - foottarget.position;// (current_rotation_of_torso_pivot_forward * 1.5f);// - _player_rght_upper_arm[0][0]._arrayOfInstances[_iterator]._ELBOWPOSITION;

                        //IdleStandingTargetPositionVariableLength


                        if (tempDir.magnitude >= totallegLength)// * connectorOfHandOffsetMul && lengthOfLowerArmRight != 0)
                        {
                            foottarget.position = IdleStandingTargetPositionMax;
                            tempDir.Normalize();
                            //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                            Vector3 tempVect = (legstaticpivot.position + (tempDir * ((totallegLength * 0.5f)))) + (-tempDir * foot.localScale.y);
                            //MOVINGPOINTER.X = tempVect.X;
                            //MOVINGPOINTER.Y = tempVect.Y;
                            //MOVINGPOINTER.Z = tempVect.Z;
                            foottarget.position = tempVect;// hit.point;
                        }
                        else
                        {
                            foottarget.position = hit.point + (tempDir * foot.localScale.y);
                        }
                    }
                }
                else
                {
                    Vector3 tempDir = legstaticpivot.position - foottarget.position;//
                    //foottarget.position = IdleStandingTargetPositionMax;
                    //tempDir.Normalize();
                    //var somePosOfSHLDR = legstaticpivot.position;// new Vector3(_player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M41, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M42, _player_rght_shldr[0][0]._arrayOfInstances[_iterator].current_pos.M43);
                    Vector3 tempVect = (legstaticpivot.position + (-legstaticpivot.up * ((totallegLength * 0.5f)))) + (-legstaticpivot.up * foot.localScale.y);
                    //MOVINGPOINTER.X = tempVect.X;
                    //MOVINGPOINTER.Y = tempVect.Y;
                    //MOVINGPOINTER.Z = tempVect.Z;
                    foottarget.position = tempVect;// hit.point;
                }
                counterForIkFootPlacement = 0;
            }*/
            counterForIkFootPlacement++;
        }
    }

    int counterForIkFootPlacement = 0;
    int counterForIkFootPlacementMax = 100;
    int counterForIkFootPlacementSwtc = 0;




    /*||
   hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0 ||
   hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0 ||
   hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1 ||
   hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0 ||
   hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0*/
    /*public void setAdjacentChunks(sccsproceduralplanetbuilderrev11.mainChunk currentChunk, Vector3 pos, int indexX, int indexY, int indexZ)
    {
        int width = currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().width;
        int height = currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().height;
        int depth = currentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().depth;

        ////Debug.Log("x: " + (indexX) + " y: " + (indexY) + " z: " + (indexZ));

        if (indexX == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)pos.x - 4, (int)pos.y, (int)pos.z) != null)
            {
                sccsproceduralplanetbuilderrev11.mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)pos.x - 4, (int)pos.y, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().GetByte((int)width - 1, (int)indexY, (int)indexZ) == 1)
                {
                    ////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().SetByte((int)width - 1, (int)indexY, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().buildMesh();
                }
            }
        }

        if (indexX == width - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)pos.x + 4, (int)pos.y, (int)pos.z) != null)
            {
                sccsproceduralplanetbuilderrev11.mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)pos.x + 4, (int)pos.y, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().GetByte((int)0, (int)indexY, (int)indexZ) == 1)
                {
                    ////Debug.Log("adjacent chunk right exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().SetByte((int)0, (int)indexY, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().buildMesh();
                }
            }
        }

        if (indexY == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)pos.x, (int)pos.y - 4, (int)pos.z) != null)
            {
                sccsproceduralplanetbuilderrev11.mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)pos.x, (int)pos.y - 4, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().GetByte((int)indexX, (int)height - 1, (int)indexZ) == 1)
                {
                    ////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().SetByte((int)indexX, (int)height - 1, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().buildMesh();
                }
            }
        }

        if (indexY == height - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)pos.x, (int)pos.y + 4, (int)pos.z) != null)
            {
                sccsproceduralplanetbuilderrev11.mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)pos.x, (int)pos.y + 4, (int)pos.z);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().GetByte((int)indexX, (int)0, (int)indexZ) == 1)
                {
                    ////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().SetByte((int)indexX, (int)0, (int)indexZ, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().buildMesh();
                }
            }
        }

        if (indexZ == 0)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)pos.x, (int)pos.y, (int)pos.z - 4) != null)
            {
                sccsproceduralplanetbuilderrev11.mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)pos.x, (int)pos.y, (int)pos.z - 4);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().GetByte((int)indexX, (int)indexY, (int)depth - 1) == 1)
                {
                    ////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().SetByte((int)indexX, (int)indexY, (int)depth - 1, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().buildMesh();
                }
            }
        }

        if (indexZ == depth - 1)
        {
            if (planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)pos.x, (int)pos.y, (int)pos.z + 4) != null)
            {
                sccsproceduralplanetbuilderrev11.mainChunk adjacentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)pos.x, (int)pos.y, (int)pos.z + 4);

                if (adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().GetByte((int)indexX, (int)indexY, (int)0) == 1)
                {
                    ////Debug.Log("adjacent chunk left exists");
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().SetByte((int)indexX, (int)indexY, (int)0, 1);
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().Regenerate();
                    adjacentChunk.planetchunk.GetComponent<sccsplanetchunkrev11>().buildMesh();
                }
            }
        }
    }*/
}


//by robertbu
/*//https://answers.unity.com/questions/540888/converting-mouse-position-to-world-stationary-came.html 
if (Input.GetMouseButtonDown(0))
{
    ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out hit))
    {
        //hit.transform.GetComponent<MeshRenderer>().material.color = Color.red;
        if (planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y,(int)hit.transform.position.z) != null)
        {
            var chunkX = (int)(Mathf.Round(hit.point.x * planeSize) / planeSize);
            var chunkY = (int)(Mathf.Round(hit.point.y * planeSize) / planeSize);
            var chunkZ = (int)(Mathf.Round(hit.point.z * planeSize) / planeSize);

            //Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);
            retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
            //yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * -2);
            //retDel.position = p;
        }
    }
}*/



/*Vector3 p = hit.point - hit.normal / 4;
float offset = planeSize / 2;
//float offset = 0;


float offset2 = planeSize / 2;

if (hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0)
{
    ////Debug.Log("yo0");

    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset * multiplicator, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    //retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * suppressorPos) / suppressorPos) + offset, (Mathf.Round(p.y * suppressorPos) / suppressorPos) + offset * multiplicator, (Mathf.Ceil(p.z * suppressorPos) / suppressorPos) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset * multiplicatorReticle, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * suppressorPos) / suppressorPos) + offset, (Mathf.Round(p.y * suppressorPos) / suppressorPos) + offset * 5, (Mathf.Ceil(p.z * suppressorPos) / suppressorPos) + offset);
}

if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == -1)
{
    ////Debug.Log("yo1");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 5);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
}

if (hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0)
{
    ////Debug.Log("yo2");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
}


if (hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0)
{
    ////Debug.Log("yo2");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 3, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
}

if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1)
{
    ////Debug.Log("yo1");

    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 3);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 5);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 5);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 6);

}


if (hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0)
{
    ////Debug.Log("yo1");
    retDel.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 3, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

    retAdd.position = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
    yoMan1 = new Vector3((Mathf.Round(p.x * tileSize) / tileSize) + offset, (Mathf.Round(p.y * tileSize) / tileSize) - offset * 5, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

}



var chunkX = (int)(Mathf.Round(hit.transform.position.x * tileSize) / tileSize);
var chunkY = (int)(Mathf.Round(hit.transform.position.y * tileSize) / tileSize);
var chunkZ = (int)(Mathf.Round(hit.transform.position.z * tileSize) / tileSize);

////Debug.Log("x: " + chunkX + " y: " + chunkY + " z: " + chunkZ);

if (planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z) != null)
{

    ////Debug.Log("==count==");
    mainChunk currentChunk = planetmanager.GetComponent<sccsproceduralplanetbuilderrev11>().getChunk((int)hit.transform.position.x, (int)hit.transform.position.y, (int)hit.transform.position.z);

    if (Input.GetMouseButtonDown(0))
    {
        int x = (int)(((yoMan1.x * 1) / 1) / 1); //WORKING
        int y = (int)(((yoMan1.y * 1) / 1) / 1);//WORKING
        int z = (int)(((yoMan1.z * 1) / 1) / 1);//WORKING
                                           //terrain1.GetChunk(x, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, activeBlockType);

        var planetchunk = hit.transform;
        planetchunk.GetComponent<sccsplanetchunkrev11>().SetByte((int)x, (int)y, (int)z, activeBlockType);

        planetchunk.GetComponent<sccsplanetchunkrev11>().Regenerate();
        planetchunk.GetComponent<sccsplanetchunkrev11>().buildMesh();

        setAdjacentChunks(currentChunk, hit, x, y, z);
    }


    if (Input.GetMouseButtonDown(1))
    {
        ////Debug.Log(hit.normal);
        ////Debug.DrawRay(hit.point, Vector3.up * 10, Color.red, 0.1f);

        int x = (int)(((yoMan.x * 1) / 1) / 1); //WORKING
        int y = (int)(((yoMan.y * 1) / 1) / 1);//WORKING
        int z = (int)(((yoMan.z * 1) / 1) / 1);//WORKING

        //terrain1.GetChunk(x, y, z).SetBrick(x / planeSize, y / planeSize, z / planeSize, 0);
        var planetchunk = hit.transform;
        planetchunk.GetComponent<sccsplanetchunkrev11>().SetByte((int)x, (int)y, (int)z, activeBlockType);

        planetchunk.GetComponent<sccsplanetchunkrev11>().Regenerate();
        planetchunk.GetComponent<sccsplanetchunkrev11>().buildMesh();

        setAdjacentChunks(currentChunk, hit, x, y, z);
    }

}*/






















