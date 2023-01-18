using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
using System.Threading;

using mainChunk = sccsproceduralplanetbuilderrev12.mainChunk;
using TMPro;
using System.Reflection;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Unity.Jobs;
using Unity.Collections;
using Unity;

public class sccsproceduralplanetbuilderrev12 : MonoBehaviour
{
    public int mindexposx;
    public int mindexposy;
    public int mindexposz;

    public static sccsproceduralplanetbuilderrev12 sccsproceduralplanetbuilderrev12script;

    public Queue<mainChunk> QueueMainChunk = new Queue<mainChunk>();
    public Queue<MapThreadInfo<mainChunk>> queueofmapdatacallback = new Queue<MapThreadInfo<mainChunk>>();
    public Queue<MapThreadInfo<mainChunk>> queueofmeshdatacallback = new Queue<MapThreadInfo<mainChunk>>();

    public Queue<MapThreadInfo<mainChunk>> queueofmapdatacallbacktwo = new Queue<MapThreadInfo<mainChunk>>();
    public Queue<MapThreadInfo<mainChunk>> queueofmeshdatacallbacktwo = new Queue<MapThreadInfo<mainChunk>>();


    public class mainChunk
    {
        public int extremitywl;
        public int extremitywr;

        public int extremityhl;
        public int extremityhr;

        public int extremitydl;
        public int extremitydr;
  
        public int mindexposx;
        public int mindexposy;
        public int mindexposz;

        public List<Vector3> verts;
        public List<int> tris;
        public int xindex;
        public int yindex;
        public int zindex;

        public Vector3 parentPosition;
        public int swtc;
        public Vector3 worldPosition;
        public Vector3 gridbasedposition;

        public GameObject planetchunk;
        public sccsplanetchunkrev12 sccsplanetchunkrev12;
        public byte[] bytemap;

        public int indexposx;
        public int indexposy;

        public int indexposz;

        public mainChunk(Vector3 worldPosition, GameObject planetchunk_, int swtc_, sccsplanetchunkrev12 sccsplanetchunkrev12_,
            byte[] bytemap, Vector3 parentPosition, int xindex, int yindex, int zindex, List<Vector3> verts, List<int> tris,Vector3 gridbasedposition,int indexposx, int indexposy, int indexposz, int mindexposx, int mindexposy, int mindexposz)
        {
            this.worldPosition = worldPosition;
            planetchunk = planetchunk_;
            swtc = swtc_;
            sccsplanetchunkrev12 = sccsplanetchunkrev12_;
            this.bytemap = bytemap;
            this.parentPosition = parentPosition;
            this.xindex = xindex;
            this.yindex = yindex;
            this.zindex = zindex;
            this.verts = verts;
            this.tris = tris;
            this.gridbasedposition = gridbasedposition;
            this.indexposx = indexposx;
            this.indexposy = indexposy;
            this.indexposz = indexposz;

            this.mindexposx = mindexposx;
            this.mindexposy = mindexposy;
            this.mindexposz = mindexposz;
            


        }

        /*public mainChunk getChunk(int x, int y, int z)
        {
            if ((x < -ChunkWidth_L) || (y < -ChunkHeight_L) || (z < -ChunkDepth_L) || (x >= ChunkWidth_R + 1) || (y >= (ChunkHeight_R + 1)) || (z >= (ChunkDepth_R + 1)))
            {
                return null;
            }

            if (x < 0)
            {
                x *= -1;
                x = (ChunkWidth_R) + x;
            }
            if (y < 0)
            {
                y *= -1;
                y = (ChunkHeight_R) + y;
            }
            if (z < 0)
            {
                z *= -1;
                z = (ChunkDepth_R) + z;
            }

            int _index = x + (ChunkWidth_L + ChunkWidth_R + 1) * (y + (ChunkHeight_L + ChunkHeight_R + 1) * z);

            return blockers[_index];

            //return map[_index] == 0;
            /*if ((x < -planetwidth) || (y < -planetheight) || (z < -planetdepth) || (y >= planetwidth) || (x >= planetheight) || (z >= planetdepth))
            {
                return null;
            }

            return blockers[x, y, z];*/




            /*if ((x < -planetwidth) || (y < -planetheight) || (z < -planetdepth) || (y >= planetwidth) || (x >= planetheight) || (z >= planetdepth))
            {
                return null;
            }
            if (blockers[x, y, z] == null)
            {
                return null;
            }
            return blockers[x, y, z];
        }*/

    }
    //byte[,,] blocks;
    public mainChunk[] blockers;

    //byte block;
    int realplanetwidth = 4;
    public Transform cube;
    Vector3[] myArray;

    //static int planetwidth = 16;
    //static int planetheight = 16;
    //static int planetdepth = 16;

    public int ChunkWidth_L = 10;
    public int ChunkWidth_R = 9;

    public int ChunkHeight_L = 10;
    public int ChunkHeight_R = 9;

    public int ChunkDepth_L = 10;
    public int ChunkDepth_R = 9;

    public static float noiseX;
    public static float noiseY;
    public static float noiseZ;

    public float planeSize = 0.1f;
    //int planetwidth = 32;
    //int planetheight = 32;
    //int planetdepth = 32;
    //public GameObject hingerelease;


    int _max = 0;


    void Fire()
    {
        GameObject obj = NewObjectPoolerScript.current.GetPooledObject();

        if (obj == null) return;

        obj.transform.position = transform.position;
        obj.transform.rotation = transform.rotation;
        obj.SetActive(true);

        /*GetComponent<shadowBullet>().bullseyedirection = gunEnd.transform.forward;
        GetComponent<shadowBullet>().firing_ship = player.gameObject;
        GetComponent<shadowBullet>().gunEnd = gunEnd.gameObject;
        GetComponent<shadowBullet>().enabled = true;
        GetComponent<shadowBullet>().shadowObject = obj;// this.transform.FindChild("shadowbullet").gameObject;*/
    }


    //public float delayOrTime = 0.15f; //2.5f
    //public float repeatrate = 0.15f; //2.5f

    int othermaxx = 0;
    int othermaxy = 0;
    int othermaxz = 0;


    public int width = 10;
    public int height = 10;
    public int depth = 10;

    int blocksize = 1;

    //void Awake()

    public int forwaitplanetchunkshavebuiltswtc = 0;
    public int forwaitplanetchunkshavebuiltswtccounter = 0;
    public int forwaitplanetchunkshavebuiltswtcmain = 0;


    public void StartScript()
    {
        /*width = 16;
        width = 16;
        width = 16;*/

        sccsproceduralplanetbuilderrev12script = this;

        ix = -ChunkWidth_L;
        iy = -ChunkHeight_L;
        iz = -ChunkDepth_L;

        _max = (ChunkWidth_L + ChunkWidth_R + 1) * (ChunkHeight_L + ChunkHeight_R + 1) * (ChunkDepth_L + ChunkDepth_R + 1);

        othermaxx = ((ChunkWidth_L + ChunkWidth_R + 1) / blocksize);
        othermaxy = ((ChunkHeight_L + ChunkHeight_R + 1) / blocksize);
        othermaxz = ((ChunkDepth_L + ChunkDepth_R + 1) / blocksize);

        waitforseconds = new WaitForSeconds(0);
        //buildplanetchunk();


        blockers = new mainChunk[_max];
        ////Debug.Log(_max);

        //InvokeRepeating("buildplanetchunk", delayOrTime, repeatrate);



        /*var xx = ix;
        var yy = iy;
        var zz = iz;

        if (xx < 0)
        {
            xx *= -1;
            xx = (ChunkWidth_R) + xx;
        }
        if (yy < 0)
        {
            yy *= -1;
            yy = (ChunkHeight_R) + yy;
        }
        if (zz < 0)
        {
            zz *= -1;
            zz = (ChunkDepth_R) + zz;
        }

        int _index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);
        ti++;
        iz ++;
        if (iz >= ChunkDepth_R)
        {
            iy ++;
            iz = -ChunkDepth_L;
        }
        if (iy >= ChunkHeight_R)
        {
            ix ++;
            iy = -ChunkHeight_L;
        }
        if (ix >= ChunkWidth_R)
        {
            iy = -ChunkHeight_L;
            iz = -ChunkDepth_L;
            ix = -ChunkWidth_L;
            ti = 0;
        }*/

    }



    public int iterateloopmap = 1;
    public int iterateloopmesh = 1;
    public int iterateonthreadloop = 1;


    //public int iteratequeueofmapdatacb = 0;

    //System.NonSerialized

    //[HideInInspector]
    public int queueofmapdatacallbackcounter = 0;
    //[HideInInspector]
    public int queueofmapdatacallbackcounterswtc = 0;

    //[HideInInspector]
    public int queueofmapdatacallbacktwocounter = 0;
    //[HideInInspector]
    public int queueofmapdatacallbacktwocounterswtc = 0;

    //[HideInInspector]
    public int queueofmeshdatacallbackcounter = 0;

    //[HideInInspector]
    public int queueofmeshdatacallbackcounterswtc = 0;

    //[ReadOnly(false)] //(typeof(ReadOnlyAttribute))
    public int queueofmeshdatacallbacktwocounter = 0;
   
    //[NonSerialized]
    ///public int queueofmeshdatacallbacktwocounterswtc = 0;

    //[SerializeField]
    //private int queueofmeshdatacallbacktwocounterswtc = 0;

    [SerializeField]
    private string MyString = "This is displayed as text";

    [SerializeField]
    [HideInInspector] 
    public string MyString2 = "This is displayed as text 2";

    public string thestring
    {
        get
        {
            return MyString;
        }
    }


    //[UnityEditor.read.Collections.ReadOnly]//[SerializeField] //Unity.Collections.ReadOnly
    //public string MyString = "This is displayed as text";



    private void Update()
    {



        if (iterateloopmap < 1)
        {
            iterateloopmap = 1;
        }
        if (iterateloopmesh < 1)
        {
            iterateloopmesh = 1;
        }



        for (int mi = 0; mi < iterateonthreadloop; mi++)
        {


            if (swtcstopgen == 1)
            {

                for (int i = 0; i < iterateloopmap; i++) //iterateloopmap
                {
                    buildplanetchunk();


                }

                swtcstopgen = 2;






                //StartCoroutine(buildplanetchunk());


            }
            else if (swtcstopgen == 2)
            {

                /*if (listofthreadmapworker.Count > 0)
                {
                    for (int t = 0;t < listofthreadmapworker.Count;t++)
                    {
                        listofthreadmapworker[t].Start();
                    }
                }
                */


                if (queueofmapdatacallbackcounterswtc == 0)
                {


                    if (queueofmapdatacallback.Count > 0)
                    {

                        //for (int q = 0; q < queueofmapdatacallback.Count; q++)
                        {
                            ////Debug.Log("dequeuing");
                            var dequeued = queueofmapdatacallback.Dequeue();
                            dequeued.callback(dequeued.parameter);
                            int index = dequeued.parameter.xindex + (ChunkWidth_L + ChunkWidth_R + 1) * (dequeued.parameter.yindex + (ChunkHeight_L + ChunkHeight_R + 1) * dequeued.parameter.zindex);
                            //blockers[index] = dequeued.parameter;
                            queueofmapdatacallbackcounter++;

                        }



                    }

                    if (queueofmapdatacallbackcounter >= othermaxx * othermaxy * othermaxz)
                    {
                        ix = -ChunkWidth_L;
                        iy = -ChunkHeight_L;
                        iz = -ChunkDepth_L;
                        //Debug.Log("finished building maps");
                        queueofmapdatacallbackcounter = 0;
                        queueofmapdatacallbackcounterswtc = 1;
                    }
                }






                if (queueofmapdatacallbackcounterswtc == 1)
                {
                    if (queueofmapdatacallbacktwo.Count > 0)
                    {


                       
                      
                        var dequeued = queueofmapdatacallbacktwo.Dequeue();

                        int index = dequeued.parameter.xindex + (ChunkWidth_L + ChunkWidth_R + 1) * (dequeued.parameter.yindex + (ChunkHeight_L + ChunkHeight_R + 1) * dequeued.parameter.zindex);

                        ////Debug.Log(index + "/max:" + _max);
                        blockers[index] = dequeued.parameter;

                        RequestMapData(blockers[index], OnMapDataReceivedFlipSwtcWorkOnMesh);
                        ////Debug.Log("index:" + index);







                        queueofmapdatacallbacktwocounter++;
                    }

                    ////Debug.Log("_max:" + _max + "/ " + queueofmapdatacallbacktwocounter);
                    if (queueofmapdatacallbacktwocounter >= othermaxx * othermaxy * othermaxz)
                    {
                        //Debug.Log("finished counting that all maps have been built");
                        //swtcstopgen = 3;
                        queueofmapdatacallbacktwocounterswtc = 1;
                        queueofmapdatacallbacktwocounter = 0;
                    }


                    if (queueofmapdatacallbacktwocounterswtc == 1 && forwaitplanetchunkshavebuiltswtc == 1)
                    {
                        //Debug.Log("finished counting that all maps have been built sent here");
                        if (queueofmeshdatacallback.Count > 0)
                        {
                            //Debug.Log("dequeuing queueofmeshdatacallback");
                            //datadequeued.callback(datadequeued.parameter);
                            //for (int q = 0; q < queueofmeshdatacallback.Count; q++)
                            {
                                ////Debug.Log("dequeuing");
                                var dequeued = queueofmeshdatacallback.Dequeue();
                                dequeued.callback(dequeued.parameter);
                                int index = dequeued.parameter.xindex + (ChunkWidth_L + ChunkWidth_R + 1) * (dequeued.parameter.yindex + (ChunkHeight_L + ChunkHeight_R + 1) * dequeued.parameter.zindex);
                                //blockers[index] = dequeued.parameter;
                                queueofmeshdatacallbackcounter++;

                            }

                        }

                        if (queueofmeshdatacallbackcounter >= othermaxx * othermaxy * othermaxz)
                        {
                            //Debug.Log("queueofmeshdatacallback dequeued. the vertices and triangles have been all built");
                            queueofmapdatacallbacktwocounterswtc = 2;
                            queueofmeshdatacallbackcounter = 0;
                        }


                        /*
                        for (int i = 0; i < iterateloopmesh; i++)
                        {
                            buildplanetchunkvertex();
                        }*/
                        //queueofmapdatacallbacktwocounterswtc = 1;
                    }

                    if (queueofmapdatacallbacktwocounterswtc == 2)
                    {


                        ////Debug.Log("queueofmapdatacallbacktwocounterswtc == 2");


                        if (queueofmeshdatacallbacktwo.Count > 0)
                        {
                            //Debug.Log("finished counting that all vertex and triangles have been built");

                            var dequeued = queueofmeshdatacallbacktwo.Dequeue();
                            //datadequeued.callback(datadequeued.parameter);


                            int index = dequeued.parameter.xindex + (ChunkWidth_L + ChunkWidth_R + 1) * (dequeued.parameter.yindex + (ChunkHeight_L + ChunkHeight_R + 1) * dequeued.parameter.zindex);

                            ////Debug.Log(index + "/max:" + _max);
                            blockers[index] = dequeued.parameter;


                            //RequestMapData(datadequeued.parameter, OnMapDataReceivedFlipSwtcWorkOnMesh);


                            queueofmeshdatacallbacktwocounter++;
                        }



                        if (queueofmeshdatacallbacktwocounter >= othermaxx * othermaxy * othermaxz)
                        {
                            //Debug.Log("queueofmeshdatacallbacktwocounter:" + queueofmeshdatacallbacktwocounter);
                            for (int i = 0; i < iterateloopmesh; i++)
                            {
                                buildplanetchunkfinalmesh();
                            }
                            //queueofmapdatacallbacktwocounterswtc = 3;
                            //queueofmeshdatacallbacktwocounter = 0;
                        }


                        /*for (int i = 0; i < iterateloopmesh; i++)
                        {
                            buildplanetchunkvertex();
                        }*/
                        //queueofmapdatacallbacktwocounterswtc = 3;
                    }


                }



            }
        }

    }
    int swtcstopgen = 1;


    // Update is called once per frame
    void ShootTheChunk() //FireShadowBullet
    {
        /*hingerelease = sccshingereleasepooler.current.GetPooledObject(); //this.transform.gameObject;// 

        if (hingerelease == null) return;

        hingerelease.transform.position = this.transform.position;// gunEnd.transform.position;// transform.position;
        hingerelease.transform.rotation = this.transform.rotation;//shadowProjectile.transform.rotation;// transform.rotation;
        hingerelease.transform.gameObject.SetActive(true);*/
    }



    WaitForSeconds waitforseconds;// = new WaitForSeconds();

    int ix = 0;
    int iy = 0;
    int iz = 0;
    int ti = 0;

    void buildplanetchunk()
    {

        int swtchXi = 0;
        int swtchYi = 0;
        int swtchZi = 0;


        /*int Xi = -ChunkWidth_L;
        int Yi = -ChunkHeight_L;
        int Zi = -ChunkDepth_L;*/

        for (int m = 0; m < 1; m++) // leaving it at 1 so that i ask myself the question wtf later. ive coded this already. im not doing it again. i need to find where i put it. peace of shit code. for brain grinders only
        {
        
        }

        //blockers = new mainChunk[(planetwidth * planetheight * planetdepth) + (planetwidth * planetheight * planetdepth)];
        //blockers = new mainChunk[(planetwidth + planetwidth) * (planetheight + planetheight) * (planetdepth + planetdepth)];

        Vector3 center = this.transform.position;// Vector3.zero;

        /*int x = ix;
        int y = iy;
        int z = iz;*/

        /*float posX = (ix);
        float posY = (iy);
        float posZ = (iz);

        //Vector3 planetchunkpos = new Vector3(posX, posY, posZ);

        var xx = ix;
        var yy = iy;
        var zz = iz;

        if (xx < 0)
        {
            xx *= -1;
            xx = (ChunkWidth_R) + xx;
        }
        if (yy < 0)
        {
            yy *= -1;
            yy = (ChunkHeight_R) + yy;
        }
        if (zz < 0)
        {
            zz *= -1;
            zz = (ChunkDepth_R) + zz;
        }

        int _index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);
        */

        int xi = -ChunkWidth_L;
        int yi = -ChunkHeight_L;
        int zi = -ChunkDepth_L;

        for (int x = -ChunkWidth_L, xe = 0; x <= ChunkWidth_R; x ++, xe++)
        {
            for (int y = -ChunkHeight_L, ye = 0; y <= ChunkHeight_R; y ++, ye++)
            {
                for (int z = -ChunkDepth_L, ze = 0; z <= ChunkDepth_R; z ++, ze++)
                {
                    float posX = (x);
                    float posY = (y);
                    float posZ = (z);

                    ///Vector3 planetchunkpos = new Vector3(posX, posY, posZ);

                    var xx = x;
                    var yy = y;
                    var zz = z;

                    /*
                    float posX = (x);
                    float posY = (y);
                    float posZ = (z);
                    */
                    if (xx < 0)
                    {
                        xx *= -1;
                        xx = (ChunkWidth_R) + xx;
                    }
                    if (yy < 0)
                    {
                        yy *= -1;
                        yy = (ChunkHeight_R) + yy;
                    }
                    if (zz < 0)
                    {
                        zz *= -1;
                        zz = (ChunkDepth_R) + zz;
                    }


                    var xxi = xi;
                    var yyi = yi;
                    var zzi = zi;

                    if (xxi < 0)
                    {
                        xxi *= -1;
                        xxi = (ChunkWidth_R) + xxi;
                    }
                    if (yyi < 0)
                    {
                        yyi *= -1;
                        yyi = (ChunkHeight_R) + yyi;
                    }
                    if (zzi < 0)
                    {
                        zzi *= -1;
                        zzi = (ChunkDepth_R) + zzi;
                    }


                    //int _index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);
                    

                    int _index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);



                    int index3d = xxi + (ChunkWidth_L + ChunkWidth_R + 1) * (yyi + (ChunkHeight_L + ChunkHeight_R + 1) * zzi);



                    Vector3 planetchunkpos = new Vector3(xi, yi, zi) + center;




                    //Transform yo = Instantiate(cube, planetchunkpos, Quaternion.identity);




                    if (blockers[_index] == null)
                    {
                        GameObject objectfrompool = this.transform.gameObject.GetComponent<NewObjectPoolerScript>().GetPooledObject();

                        //objectfrompool.AddComponent<sccsplanetchunkrev12>()
                        //var sccsplanetchunkrev12script = new sccsplanetchunkrev12();

                        sccsplanetchunkrev12 sccsplanetchunkrev12script = objectfrompool.GetComponent<sccsplanetchunkrev12>();

                        sccsplanetchunkrev12script.width = width;
                        sccsplanetchunkrev12script.height = height;
                        sccsplanetchunkrev12script.depth = depth;
                        sccsplanetchunkrev12script.planeSize = planeSize;


                        sccsplanetchunkrev12script.chunkpos = new Vector3(x * (width * planeSize), y * (height * planeSize), z * (depth * planeSize)) + center;


                        Vector3 gridposition = new Vector3(x, y, z) + center;
                        //objectfrompool.GetComponent<sccsplanetchunkrev12>().buildchunkmap();
                        blockers[_index] = new mainChunk(planetchunkpos, objectfrompool.gameObject, 0, sccsplanetchunkrev12script, new byte[width * height * depth], this.transform.position, xxi, yyi, zzi, new List<Vector3>(), new List<int>(), gridposition, x, y, z, mindexposx, mindexposy, mindexposz);
                        blockers[_index].sccsplanetchunkrev12 = sccsplanetchunkrev12script;// objectfrompool.GetComponent<sccsplanetchunkrev12>();



                        if (x == -ChunkWidth_L)
                        {
                            blockers[_index].extremitywl = 1;

                        }

                        if (x == ChunkWidth_R)
                        {
                            blockers[_index].extremitywr = 1;
                        }



                        if (y == -ChunkHeight_L)
                        {
                            blockers[_index].extremityhl = 1;

                        }

                        if (y == ChunkHeight_R)
                        {
                            blockers[_index].extremityhr = 1;
                        }



                        if (z == -ChunkDepth_L)
                        {
                            blockers[_index].extremitydl = 1;

                        }

                        if (z == ChunkDepth_R)
                        {
                            blockers[_index].extremitydr = 1;
                        }


                        /*
                        blockers[_index].extremityhl = 0;
                        blockers[_index].extremityhr = 0;
                        blockers[_index].extremitydl = 0;
                        blockers[_index].extremitydr = 0;*/



                        //TO OPTIMIZE LATER
                        RequestMapData(blockers[_index], OnMapDataReceivedFlipSwtcWorkOnMesh);

                        //blockers[_index].sccsplanetchunkrev12.buildchunkmap();
                        //Queue
                        //blockers[_index].swtc = 1;
                        objectfrompool.transform.position = new Vector3(x * (width* planeSize), y * (height * planeSize), z * (depth * planeSize)) + center;
                        objectfrompool.SetActive(true);
                        objectfrompool.transform.parent = transform;
                        //objectfrompool.AddComponent<sccsplanetchunkrev12>() = (sccsplanetchunkrev12script);






                    }




                    /*if (objectfrompool.GetComponent<sccsplanetchunkrev12>() != null)
                    {
                        ////Debug.Log("!null");
                        //objectfrompool.GetComponent<sccsplanetchunkrev12>().buildchunkmap();
                        blockers[_index] = new mainChunk(planetchunkpos, objectfrompool.gameObject);
                    }
                    else
                    {
                        ////Debug.Log("null");
                    }*/
                    //blockers[_index].planetchunk.GetComponent<sccsplanetchunkrev12>().buildchunkmap();



                    // yield return waitforseconds;


                    zi++;
                    if(zi > ChunkDepth_R)
                    {
                        yi++;
                        zi = -ChunkDepth_L;
                    }
                    if (yi > ChunkHeight_R)
                    {
                        xi++;
                        yi = -ChunkHeight_L;
                    }
                    if (xi > ChunkWidth_R)
                    {
                        xi = -ChunkWidth_L;
                    }
                }
            }
        }



        
        try
        {

        }
        catch (Exception ex)
        {
            //Debug.Log(ex.ToString());
        }

        ti++;
        iz ++;
        if (iz > ChunkDepth_R)
        {
            iy ++;
            iz = -ChunkDepth_L;
        }
        if (iy > ChunkHeight_R)
        {
            ix ++;
            iy = -ChunkHeight_L;
        }
        if (ix > ChunkWidth_R)
        {
            //iy = -ChunkHeight_L;
            //iz = -ChunkDepth_L;
            ti = 0;

            swtcstopgen = 2;
            ix = -ChunkWidth_L;
        }

    }



    //REFERENCED FROM SEBASTIAN LAGUE'S MIT - PROCEDURAL LANDMASS GENERATION TUTORIAL - dual threadstarts pumping tasks architecture
    //REFERENCED FROM SEBASTIAN LAGUE'S MIT - PROCEDURAL LANDMASS GENERATION TUTORIAL - dual threadstarts pumping tasks architecture
    //REFERENCED FROM SEBASTIAN LAGUE'S MIT - PROCEDURAL LANDMASS GENERATION TUTORIAL - dual threadstarts pumping tasks architecture
    public struct MapThreadInfo<T>
    {
        public readonly Action<T> callback;
        public readonly T parameter;

        public MapThreadInfo(Action<T> callback, T parameter)
        {
            this.callback = callback;
            this.parameter = parameter;
        }

    }

    public List<Thread> listofthreadmapworker = new List<Thread>();
    public void RequestMapData(mainChunk mapData, Action<mainChunk> callback)
    {


        ThreadStart threadStart = delegate {
            /*if (enabledebug == 1)
            {
                Console.WriteLine("RequestMapData=>MapDataThread " + mapData.facetype_ + " /xe:" + mapData.xe + "/ye:" + mapData.ye + "/ze:" + mapData.ze);

            }*/

            MapDataThread(mapData, callback);
        };

        //new Thread(threadStart).Start();

        Thread test = new Thread(threadStart);//
        test.Start();
        //test.Join();


        //listofthreadmapworker.Add(test);
        //return newthrea
        //return theaction;

    }





    sccsproceduralplanetbuilderrev12.mainChunk mapData;
    void MapDataThread(mainChunk mapData, Action<mainChunk> callback)
    {
        ////Debug.Log("MapDataThread");

        if (mapData.swtc == 0)
        {
            mapData = mapData.sccsplanetchunkrev12.buildchunkmap(mapData);
            mapData.swtc = 1;

            lock (queueofmapdatacallback)
            {
                //Console.WriteLine("ENQUEUEING DATA " + queueofthreaddatacallback.Count);
                queueofmapdatacallback.Enqueue(new MapThreadInfo<mainChunk>(callback, mapData));
            }
        }
        else if (mapData.swtc == 2)
        {
            //Debug.Log("Regenerate");
            mapData = mapData.sccsplanetchunkrev12.Regenerate(mapData);
            mapData.swtc = 3;

            lock (queueofmeshdatacallback)
            {
                //Console.WriteLine("ENQUEUEING DATA " + queueofthreaddatacallback.Count);
                queueofmeshdatacallback.Enqueue(new MapThreadInfo<mainChunk>(callback, mapData));
            }
        }

        //Console.WriteLine("callback0 " + mapData.facetype_ + " <= facetype **MapDataThread**");
        /*int novalue = 0;
        if (enabledebug == 1)
        {
            Console.WriteLine("MapDataThread " + " /xe:" + mapData.xe + "/ye:" + mapData.ye + "/ze:" + mapData.ze);
        }
        mapData.tutorialcubeaschunkinst.createthechunkinstmapsofint(mapData.facetype_, mapData.minx, mapData.miny, mapData.minz, mapData.maxx, mapData.maxy, mapData.maxz, mapData.leveldisivionoriginposition, mapData.xe, mapData.ye, mapData.ze, mapData.thevoxelindex);
        mapData.threadworkswtc = 1;

        sccslevelgen.thecallbackstructdata[mapData.thevoxelindex][mapData.facetype_][mapData.theindexdivofdivlevel] = mapData;

        //Console.WriteLine("####completed callback0");

        lock (queueofmapdatacallback)
        {
            //Console.WriteLine("ENQUEUEING DATA " + queueofthreaddatacallback.Count);
            queueofmapdatacallback.Enqueue(new MapThreadInfo<sccslevelgen.callbackstructdata>(callback, mapData));
        }*/
    }

    void OnMapDataReceivedFlipSwtcWorkOnMesh(mainChunk mapData)
    {
        ////Debug.Log("OnMapDataReceivedFlipSwtcWorkOnMesh");

        //mapData = mapData.sccsplanetchunkrev12.Regenerate(mapData);
        if (mapData.swtc == 1)
        {
            mapData.swtc = 2;

            lock (queueofmapdatacallbacktwo)
            {
                queueofmapdatacallbacktwo.Enqueue(new MapThreadInfo<mainChunk>(null, mapData));

            }
        }
        if (mapData.swtc == 3)
        {
            mapData.swtc ++;

            lock (queueofmeshdatacallbacktwo)
            {
                queueofmeshdatacallbacktwo.Enqueue(new MapThreadInfo<mainChunk>(null, mapData));

            }
        }




        /*sccslevelgen.thecallbackstructdata[mapData.thevoxelindex][mapData.facetype_][mapData.theindexdivofdivlevel].threadworkswtc = 2;
        if (enabledebug == 1)
        {
            Console.WriteLine("OnMapDataReceivedFlipSwtcWorkOnMesh " + " /xe:" + mapData.xe + "/ye:" + mapData.ye + "/ze:" + mapData.ze);
        }

        lock (queueofmapdatacallbacktwo)
        {
            //Console.WriteLine("ENQUEUEING DATA " + queueofthreaddatacallback.Count);
            queueofmapdatacallbacktwo.Enqueue(new MapThreadInfo<sccslevelgen.callbackstructdata>(null, sccslevelgen.thecallbackstructdata[mapData.thevoxelindex][mapData.facetype_][mapData.theindexdivofdivlevel]));
        }
        */
        /*sccslevelgen.callbackstructdata thedata = sccslevelgen.thecallbackstructdata[mapData.thevoxelindex][mapData.facetype_][mapData.theindexdivofdivlevel]

        Action theaction = new Action(() =>
        {
            RequestMapData(thedata, OnMapDataReceived);

            //Console.WriteLine("within action " + thedata.facetype_ + " <=facetype");
        });

        queueofthreadmeshcallback.Enqueue(theaction);
        */

        //Console.WriteLine("completed callback1 " + mapData.facetype_ + " <= facetype **OnMapDataReceived**");

        //return mapData;
        //Console.WriteLine("completed callback1");
    }








    int canstopcounting = 0;
    void buildplanetchunkfinalmesh()
    {



        /*
        var xx = ix;
        var yy = iy;
        var zz = iz;

        if (xx < 0)
        {
            xx *= -1;
            xx = (ChunkWidth_R) + xx;
        }
        if (yy < 0)
        {
            yy *= -1;
            yy = (ChunkHeight_R) + yy;
        }
        if (zz < 0)
        {
            zz *= -1;
            zz = (ChunkDepth_R) + zz;
        }

        int _index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);

        // yield return waitforseconds;

        //for (int x = -ChunkWidth_L; x <= ChunkWidth_R; x ++)
        {
            //for (int y = -ChunkHeight_L; y <= ChunkHeight_R; y ++)
            {
                //for (int z = -ChunkDepth_L; z <= ChunkDepth_R; z ++)
                {

                    if (blockers[_index].swtc =++)
                    {
                        ////Debug.Log("index:" + _index + "/buildingmesh");
                        //blockers[_index].sccsplanetchunkrev12.Regenerate(this, blockers[_index]);
                        blockers[_index] = blockers[_index].sccsplanetchunkrev12.buildMesh(blockers[_index]);

                        canstopcounting++;

                        blockers[_index].swtc = 5;
                    }

                    //yield return new WaitForSeconds(0);
                    //yield return waitforseconds;
                }
            }
        }




        ti++;
        iz ++;
        if (iz > ChunkDepth_R)
        {
            iy ++;
            iz = -ChunkDepth_L;
        }
        if (iy > ChunkHeight_R)
        {
            ix ++;
            iy = -ChunkHeight_L;
        }
        if (ix > ChunkWidth_R)
        {
            //iy = -ChunkHeight_L;
            //iz = -ChunkDepth_L;
            ix = -ChunkWidth_L;
            ti = 0;


            if (canstopcounting < _max)
            {
                swtcstopgen = 2;
                canstopcounting = 0;
            }
            else
            {
                queueofmapdatacallbacktwocounterswtc = 2;
                //queueofmapdatacallbackcounterswtc = 2;
                swtcstopgen = 3;
            }

        }
        */



        for (int x = -ChunkWidth_L, xe = 0; x <= ChunkWidth_R; x ++, xe++)
        {
            for (int y = -ChunkHeight_L, ye = 0; y <= ChunkHeight_R; y ++, ye++)
            {
                for (int z = -ChunkDepth_L, ze = 0; z <= ChunkDepth_R; z ++, ze++)
                {
                    float posX = (x);
                    float posY = (y);
                    float posZ = (z);

                    //Vector3 planetchunkpos = new Vector3(posX, posY, posZ);

                    var xx = x;
                    var yy = y;
                    var zz = z;

                    
                    /*float posX = (x);
                    float posY = (y);
                    float posZ = (z);

                    var xx = x;
                    var yy = y;
                    var zz = z;*/

                    if (xx < 0)
                    {
                        xx *= -1;
                        xx = (ChunkWidth_R) + xx;
                    }
                    if (yy < 0)
                    {
                        yy *= -1;
                        yy = (ChunkHeight_R) + yy;
                    }
                    if (zz < 0)
                    {
                        zz *= -1;
                        zz = (ChunkDepth_R) + zz;
                    }


                    int _index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);

                    if (blockers[_index].swtc == 4)
                    {
                        ////Debug.Log("index:" + _index + "/buildingmesh");
                        //blockers[_index].sccsplanetchunkrev12.Regenerate(this, blockers[_index]);
                        blockers[_index] = blockers[_index].sccsplanetchunkrev12.buildMesh(blockers[_index]);

                        canstopcounting++;

                        blockers[_index].swtc = 5;
                    }


                }
            }
        }

        if (canstopcounting < _max)
        {
            swtcstopgen = 2;
            canstopcounting = 0;
        }
        else
        {
            queueofmapdatacallbacktwocounterswtc = 2;
            //queueofmapdatacallbackcounterswtc = 2;
            swtcstopgen = 3;
        }
    }










    public mainChunk getChunk(int x, int y, int z)
    {
        if ((x < -ChunkWidth_L) || (y < -ChunkHeight_L) || (z < -ChunkDepth_L) || (x >= ChunkWidth_R + 1) || (y >= (ChunkHeight_R + 1)) || (z >= (ChunkDepth_R + 1)))
        {
            return null;
        }

        if (x < 0)
        {
            x *= -1;
            x = (ChunkWidth_R) + x;
        }
        if (y < 0)
        {
            y *= -1;
            y = (ChunkHeight_R) + y;
        }
        if (z < 0)
        {
            z *= -1;
            z = (ChunkDepth_R) + z;
        }

        int _index = x + (ChunkWidth_L + ChunkWidth_R + 1) * (y + (ChunkHeight_L + ChunkHeight_R + 1) * z);

        return blockers[_index];

        //return map[_index] == 0;
        /*if ((x < -planetwidth) || (y < -planetheight) || (z < -planetdepth) || (y >= planetwidth) || (x >= planetheight) || (z >= planetdepth))
        {
            return null;
        }

        return blockers[x, y, z];*/




        /*if ((x < -planetwidth) || (y < -planetheight) || (z < -planetdepth) || (y >= planetwidth) || (x >= planetheight) || (z >= planetdepth))
        {
            return null;
        }
        if (blockers[x, y, z] == null)
        {
            return null;
        }
        return blockers[x, y, z];*/
    }












    /*
    public mainChunk getChunk(int x, int y, int z)
    {
        if ((x < -ChunkWidth_L) || (y < -ChunkHeight_L) || (z < -ChunkDepth_L) || (x >= ChunkWidth_R + 1) || (y >= (ChunkHeight_R + 1)) || (z >= (ChunkDepth_R + 1)))
        {
            return null;
        }

        if (x < 0)
        {
            x *= -1;
            x = (ChunkWidth_R) + x;
        }
        if (y < 0)
        {
            y *= -1;
            y = (ChunkHeight_R) + y;
        }
        if (z < 0)
        {
            z *= -1;
            z = (ChunkDepth_R) + z;
        }

        int _index = x + (ChunkWidth_L + ChunkWidth_R + 1) * (y + (ChunkHeight_L + ChunkHeight_R + 1) * z);

        return blockers[_index];

        //return map[_index] == 0;
        /*if ((x < -planetwidth) || (y < -planetheight) || (z < -planetdepth) || (y >= planetwidth) || (x >= planetheight) || (z >= planetdepth))
        {
            return null;
        }

        return blockers[x, y, z];*/




        /*if ((x < -planetwidth) || (y < -planetheight) || (z < -planetdepth) || (y >= planetwidth) || (x >= planetheight) || (z >= planetdepth))
        {
            return null;
        }
        if (blockers[x, y, z] == null)
        {
            return null;
        }
        return blockers[x, y, z];
    }*/







public void drawBrick(int x, int y, int z)
{
    Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);
}

/*public byte GetByte(mainChunk chuk,int x, int y, int z)
{
    chuk.chunker.get

    if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
    {
        return 0;
    }
    return blocks[x, y, z];
}*/
    }



/*var xx = Xi;
var yy = Yi;
var zz = Zi;

if (xx < 0)
{
    xx *= -1;
    xx = (ChunkWidth_R) + xx;
}
if (yy < 0)
{
    yy *= -1;
    yy = (ChunkHeight_R) + yy;
}
if (zz < 0)
{
    zz *= -1;
    zz = (ChunkDepth_R) + zz;
}

int _index = xx + (ChunkWidth_L + ChunkWidth_R + 1) * (yy + (ChunkHeight_L + ChunkHeight_R + 1) * zz);


if (swtchZi == 0)
{
    if (Zi <= ChunkDepth_R)
    {
        Zi+=4;
    }

    if (Zi > ChunkDepth_R) // prob an else if here instead
    {
        Zi = 0;
        swtchZi = 1;
        swtchYi = 1;
    }
}
if (swtchYi == 1)
{
    if (Yi <= ChunkHeight_R)
    {
        Yi ++;
        swtchYi = 0;
        swtchZi = 0;
    }

    if (Yi > ChunkHeight_R) // prob an else if here instead
    {
        Yi = 0;
        swtchYi = 0;
        swtchXi = 1;
    }
}*/
/*if (swtchXi == 1)
{
    if (Xi <= ChunkWidth_R)
    {
        Xi ++;
        swtchXi = 0;
        swtchYi = 0;
        swtchZi = 0;       
    }

    if (Xi > ChunkWidth_R) // prob an else if here instead
    {
        swtchYi = 0;
        swtchXi = 1;
    }
}*/






/*if (swtchXi == 0)
{
    if(Xi<= ChunkWidth_R)
    {
        Xi+=4;
    }

    if (Xi > ChunkWidth_R)
    {
        Xi = 0;
        swtchXi = 1;
        swtchYi = 1;
    }
}
if (swtchYi == 1)
{
    if (Yi <= ChunkHeight_R)
    {
        Yi+=4;
    }

    if (Yi > ChunkHeight_R)
    {
        Yi = 0;
        swtchYi = 2;
        swtchZi = 1;
    }
}

if (swtchZi == 1)
{
    if (Zi <= ChunkDepth_R)
    {
        Zi+=4;
    }

    if (Zi > ChunkDepth_R)
    {
        Zi = 0;
        swtchZi = 0;
        swtchXi = 0;
    }
}*/

////Debug.Log("max: " + _max + " x: " + Xi + " y: " + Yi + " z: " + Zi);





























//yield return waitforseconds;


/*
for (int x = -planetwidth; x < planetwidth; x ++)
{
    for (int y = -planetheight; y < planetheight; y ++)
    {
        for (int z = -planetdepth; z < planetdepth; z ++)
        {
            Vector3 position = new Vector3(x, y, z);
            Transform yo = Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);

            yo.transform.parent = transform;

            if (x < 0)
            {
                x *= -1;
                x = (planetwidth - 1) + x;
            }
            if (y < 0)
            {
                y *= -1;
                y = (planetheight - 1) + y;
            }
            if (z < 0)
            {
                z *= -1;
                z = (planetdepth - 1) + z;
            }

            int _index = x + (planetwidth + planetwidth) * (y + (planetheight + planetheight) * z);



            blockers[_index] = new mainChunk(new Vector3(x, y, z), yo.gameObject);
        }
    }
}*/
