using UnityEngine;
using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
//using SPINACH.iSCentralDispatch;
using System.Linq;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters;
using System.IO;
using System.Reflection;
using UnityEngine.Tilemaps;


public class universemaponly : MonoBehaviour
{

    public class regionChunker
    {
        regionChunker regionChunku;
        public areaChunker[] areaChunkyArray;
        public Vector3 worldPosition;
        public int regionX;
        public int regionY;
        public int regionZ;
        public GameObject currentRegionChunk = null;
        public regionChunker[] worldChunky;
        public Vector3 worldScenePos;

        public regionChunker()
        {

        }
    }

    public class areaChunker
    {
        areaChunker areaChunku;
        //public bigChunker[,,] bigChunky;
        public bigChunker[] bigChunkyArray;
        //public bigChunk[,,] bigChunkerArray { get; set; }
        //public areaChunker[,,] areaChunkyList = new areaChunker[20, 20, 20];
        public Vector3 worldPosition;
        public int regionX;
        public int regionY;
        public int regionZ;
        public int areaX;
        public int areaY;
        public int areaZ;
        public GameObject currentChunk = null;

        public areaChunker()
        {

        }
    }

    public class bigChunker
    {
        public Vector3 worldAreaPosition;
        public smallChunker[] smallChunkerList;
        areaChunker areaChunky;
        //public bigChunker[,,] bigChunky;
        //public bigChunker[,,] bigChunkyArray;
        public int regionX;
        public int regionY;
        public int regionZ;
        public int areaX;
        public int areaY;
        public int areaZ;
        public int bigX;
        public int bigY;
        public int bigZ;
        public GameObject currentChunk = null;

        public bigChunker()
        {

        }
    }

    public class smallChunker
    {
        public int regionX;
        public int regionY;
        public int regionZ;
        public int areaX;
        public int areaY;
        public int areaZ;
        public int bigX;
        public int bigY;
        public int bigZ;
        public int smallX;
        public int smallY;
        public int smallZ;

        public Vector3 worldPosition;

        public byte[] map;
        smallChunker chuk;

        public Vector3[] positionz;
        public Vector3[] normalz;
        public Vector2[] textureCoordinatez;
        public int[] triangleIndicez;
        //public static smallChunker[] arrayOfSmallChunks = new smallChunker[2 * 2 * 2];
        public static int counter = 0;
        public GameObject currentChunk = null;
        public meshData currentMeshData;
        public bool hasBeenSpawned = false;
        public bool hasMesh = false;
        public bool hasMap = false;

        public bool distanceRejected = false;

        public smallChunker()
        {
            //this.map = mapper;
        }
    }



    public class testerOfNumber
    {




        private ReaderWriterLock rwl = new ReaderWriterLock();
        private ReaderWriterLock rwlock = new ReaderWriterLock();
        private ReaderWriterLock rwlRegionChunkor = new ReaderWriterLock();
        private ReaderWriterLock rwlAreaChunkor = new ReaderWriterLock();
        private ReaderWriterLock rwlBigChunkor = new ReaderWriterLock();
        private ReaderWriterLock rwlSmallChunkor = new ReaderWriterLock();

        private areaChunker[] currentAreaChunk;

        public Vector3 viewerPosition;
        public Vector3 realScenePos;
        public Vector3 currentRegionPos;
        public Vector3 currentAreaPos;
        public Vector3 currentBigPos;
        public Vector3 currentSmallPos;
        public Vector3 maxWorldChunkVision;
        //public int chunkArraySize = 3;

        //static regionChunker[] worldChunky;


        public int[] arrayOfRegionPos;
        public int currentFuckingInt;
        public static Dictionary<Vector3, smallChunker[]> sceneList = new Dictionary<Vector3, smallChunker[]>();
        public int[] regionArrayPos;

        Queue<meshData> testingThreads = new Queue<meshData>();

        private static Mutex mut = new Mutex();

        //public int regionX;
        //public int regionY;
        //public int regionZ;

        /*public int areaX;
        public int areaY;
        public int areaZ;

        public int bigX;
        public int bigY;
        public int bigZ;

        public int smallatorX;
        public int smallatorY;
        public int smallatorZ;*/

        object tempRegionRead = new object();
        object tempRegionWrite = new object();

        object tempAreaRead = new object();
        object tempAreaWrite = new object();

        object tempBigRead = new object();
        object tempBigWrite = new object();

        object tempSmallRead = new object();
        object tempSmallWrite = new object();

        object testRegionObject = new object();

        object testRegionObjectThread0 = new object();
        object testRegionObjectThread1 = new object();
        object testRegionObjectThread2 = new object();

        int worldChunkWidth = 81;
        int regionChunkWidth = 27;
        int areaChunkWidth = 9;
        int bigChunkWidth = 3;
        int smallChunkWidth = 1;

        /*float worldChunkWidth = 8.1f;
        float regionChunkWidth = 2.7f;
        float areaChunkWidth = 0.9f;
        float bigChunkWidth = 0.3f;
        float smallChunkWidth = 0.1f;*/

        /*float worldChunkWidth = 16.2f;
        float regionChunkWidth = 5.4f;
        float areaChunkWidth = 1.8f;
        float bigChunkWidth = 0.6f;
        float smallChunkWidth = .2f;*/

        /*float worldChunkWidth = 0.81f;
        float regionChunkWidth = 0.27f;
        float areaChunkWidth = 0.09f;
        float bigChunkWidth = 0.03f;
        float smallChunkWidth = 0.01f;*/

        /*int worldChunkWidth = 405;
        int regionChunkWidth = 135;
        int areaChunkWidth = 45;
        int bigChunkWidth = 15;
        int smallChunkWidth = 5;*/

        /*int worldChunkWidth = 162;
        int regionChunkWidth = 54;
        int areaChunkWidth = 18;
        int bigChunkWidth = 6;
        int smallChunkWidth = 2;*/

        /*int worldChunkWidth = 324;
        int regionChunkWidth = 108;
        int areaChunkWidth = 36;
        int bigChunkWidth = 12;
        int smallChunkWidth = 4;*/

        //return map[x + width * (y + depth * z)];

        /*float worldChunkWidthY = 8.1f;
        float regionChunkWidthY = 2.7f;
        float areaChunkWidthY = 0.9f;
        float bigChunkWidthY = 0.3f;
        float smallChunkWidthY = 0.1f;*/

        /*float worldChunkWidthY = .81f;
        float regionChunkWidthY = 0.27f;
        float areaChunkWidthY = 0.09f;
        float bigChunkWidthY = 0.03f;
        float smallChunkWidthY = 0.01f;*/

        //int maxWorldChunkWidth = 486;


        float worldChunkWidthY = 81f;
        float regionChunkWidthY = 27f;
        float areaChunkWidthY = 9f;
        float bigChunkWidthY = 3f;
        float smallChunkWidthY = 1f;

        /*float worldChunkWidthY = 16.2f;
        float regionChunkWidthY = 5.4f;
        float areaChunkWidthY = 1.8f;
        float bigChunkWidthY = 0.6f;
        float smallChunkWidthY = .2f;*/

        /*float worldChunkWidthY = 162f;
        float regionChunkWidthY = 54f;
        float areaChunkWidthY = 18f;
        float bigChunkWidthY = 6f;
        float smallChunkWidthY = 2f;*/

        /*float worldChunkWidthY = 162f;
        float regionChunkWidthY = 54f;
        float areaChunkWidthY = 18f;
        float bigChunkWidthY = 6f;
        float smallChunkWidthY = 2f;*/

        /*int worldChunkWidthY = 405;
        int regionChunkWidthY = 135;
        int areaChunkWidthY = 45;
        int bigChunkWidthY = 15;
        float smallChunkWidthY = 5;*/

        public int realChunkWidth = 20;
        public int realChunkHeight = 20;
        public int realChunkDepth = 20;

        public Vector3 realWorldScenePos;

        float planeSize = 0.1f;
        int seed = 3420;
        int detailScale = 5;
        int heightScale = 5;
        public static Queue<checkingBytes> firstQueue = new Queue<checkingBytes>();
        public static Queue<checkingBytes> nextQueue = new Queue<checkingBytes>();


        public static Queue<sccscomputevoxelshrinked> queuecomputeshader = new Queue<sccscomputevoxelshrinked>();




        public static int width = 3;
        public static int height = 3;
        public static int depth = 3;
        public class newScenePos
        {
            public Vector3 scenePos;
            public Vector3 viewerPos;

            public newScenePos(Vector3 scenePoser, Vector3 viewerPos)
            {
                this.scenePos = scenePoser;
                this.viewerPos = viewerPos;
            }
        }
        object sceneObject = new object();



        //public static smallChunker[] smallChunkArray;


        public void AccessGlobalResource(object stateInfo)
        {
            try
            {
                lock (sceneObject)
                {
                    newScenePos newScene = (newScenePos)stateInfo;

                    float sizerX = bigChunkWidth;
                    float sizerY = bigChunkWidth;
                    float sizerZ = bigChunkWidth;

                    /*float sizerX = bigChunkWidth;
                    float sizerY = bigChunkWidth;
                    float sizerZ = bigChunkWidth;*/

                    for (float x = -sizerX; x < sizerX + sizerX; x += bigChunkWidth)
                    {
                        for (float y = -sizerY; y < sizerY + sizerY; y += bigChunkWidth)
                        {
                            for (float z = -sizerZ; z < sizerZ + sizerZ; z += bigChunkWidth)
                            {
                                Vector3 whatever = new Vector3(x, y, z);
                                realWorldScenePos = whatever + new Vector3(newScene.scenePos.x, newScene.scenePos.y, newScene.scenePos.z);
                                //realWorldScenePos =  new Vector3(newScene.scenePos.x, newScene.scenePos.y, newScene.scenePos.z);

                                if (!sceneList.ContainsKey(realWorldScenePos))
                                {
                                    //Debug.Log(realWorldScenePos);
                                    smallChunker[] worldChunky = new smallChunker[width * height * depth];
                                    sceneList.Add(realWorldScenePos, worldChunky);
                                    smallChunkCreator(realWorldScenePos, worldChunky);
                                }
                                else
                                {
                                    smallChunker[] valuer;
                                    sceneList.TryGetValue(realWorldScenePos, out valuer);
                                    smallChunker[] worldChunky = valuer;
                                    smallChunkCreator(realWorldScenePos, worldChunky);
                                }
                            }
                        }
                    }
                }
            }
            catch
            {

            }
            finally
            {

            }
        }



        void smallChunkCreator(Vector3 viewerPosition, smallChunker[] worldChunk)
        {
            //Debug.Log("smallChunkCreator");
            //viewerPosition = data.viewerPos;

            //smallChunkArray = new smallChunker[3 * 3 * 3];

            float sizerX = smallChunkWidth;
            float sizerY = smallChunkWidthY;
            float sizerZ = smallChunkWidth;

            for (float xx = -sizerX; xx < sizerX + sizerX; xx += smallChunkWidth)
            {
                for (float yy = -sizerY; yy < sizerY + sizerY; yy += smallChunkWidthY)
                {
                    for (float zz = -sizerZ; zz < sizerZ + sizerZ; zz += smallChunkWidth)
                    {
                        Vector3 currentPosition = new Vector3(xx, yy, zz);

                        Vector3 realPos = currentPosition + viewerPosition;

                        int smallX;
                        int smallY;
                        int smallZ;

                        getPosSmall(xx, yy, zz, out smallX, out smallY, out smallZ, smallChunkWidth, smallChunkWidthY, smallChunkWidth, 2);

                        int smallerX;
                        int smallerY;
                        int smallerZ;

                        getPosSmall(xx, yy, zz, out smallerX, out smallerY, out smallerZ, smallChunkWidth, smallChunkWidthY, smallChunkWidth, 2);

                        /*GameObject areaChunkPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        areaChunkPoint.transform.position = realPos;
                        Vector3 scaler = areaChunkPoint.transform.localScale;
                        scaler.x *= 2;
                        scaler.z *= 2;
                        scaler.y *= 0.1f;
                        areaChunkPoint.transform.localScale = scaler;*/
                        lock (tempSmallWrite)
                        {
                            if (worldChunk[smallX + width * (smallerY + height * smallZ)] == null)
                            {
                                //Debug.Log(smallerY);

                                worldChunk[smallX + width * (smallerY + height * smallZ)] = new smallChunker();

                                /*GameObject areaChunkPoint = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                areaChunkPoint.transform.position = realPos;
                                Vector3 scaler = areaChunkPoint.transform.localScale;
                                scaler.x *= 0.1f;
                                scaler.z *= 0.1f;
                                scaler.y *= 0.1f;
                                areaChunkPoint.transform.localScale = scaler;*/

                                bool resultsOne = false;
                                bool resultsTwo = false;
                                bool resultsThree = false;

                                checkingBytes checkBytes = new checkingBytes(realPos, smallX, smallerY, smallZ, resultsOne, resultsTwo, resultsThree, worldChunk, null,null);

                                lock (firstQueue)
                                {
                                    firstQueue.Enqueue(checkBytes);
                                }
                            }
                        }
                    }
                }
            }


            //lock (firstQueue)
            {
                ThreadPool.QueueUserWorkItem(new WaitCallback(checkLastBytes), firstQueue);
            }
            //checkLastBytes(firstQueue);
        }





        public class checkingBytes
        {
            public Vector3 smallChunkPos;
            public sccscomputevoxelshrinked sccscomputevoxelshrinkedshrinked;

            //public sccscomputevoxelshrinkedshrinked sccscomputevoxelshrinkedshrinked;
            public int smallX;
            public int smallY;
            public int smallZ;
            //public regionChunker regionChunko;
            //public regionChunker[] worldChunk;
            public Vector3 worldScenePos;
            public bool resultsOne;
            public bool resultsTwo;
            public bool resultsThree;
            public smallChunker[] worldChunk;
            public int[] map;

            public checkingBytes(Vector3 smallChunkPos, int smallX, int smallY, int smallZ, bool resultsOne, bool resultsTwo, bool resultsThree, smallChunker[] worldChunk, int[] map, sccscomputevoxelshrinked sccscomputevoxelshrinkedshrinked_)
            {
                this.smallChunkPos = smallChunkPos;
                this.resultsOne = resultsOne;
                this.resultsTwo = resultsTwo;
                this.resultsThree = resultsThree;

                this.smallX = smallX;
                this.smallY = smallY;
                this.smallZ = smallZ;

                this.worldChunk = worldChunk;
                this.map = map;
                this.sccscomputevoxelshrinkedshrinked = sccscomputevoxelshrinkedshrinked_;
            }
        }

        ComputeShader computeShaderForMap;

        public Material mat1;

        public struct mapbytes
        {
            public int ix;
            public int iy;
            public int iz;
            public int thebyte;
            public Vector3 position;
        }
        public struct MapStruct
        {
            /*public int extrabyte;
            public int cx;
            public int cy;
            public int cz;*/
            public int ix;
            public int iy;
            public int iz;
            public int thebyte;
            public Vector3 position;
        }

        /*public struct mapofints
        {
            public int thebyte;
        };*/

        private MapStruct[][] mapdata;
        int levelsizex = 1;
        int levelsizey = 1;
        int levelsizez = 1;

        int mapx = 10;
        int mapy = 10;
        int mapz = 10;

        int createinit = 0;
        sccscomputevoxelshrinked computevoxels;


        void checkLastBytes(object stateInfo)
        {
            Console.WriteLine("new map checkLastBytes");

            Queue<checkingBytes> queuer = (Queue<checkingBytes>)stateInfo;
            lock (queuer)
            {
                for (int i = 0; i < queuer.Count; i++)
                {



                    checkingBytes byteCheck = queuer.Dequeue();

                    Vector3 smallChunkPos = byteCheck.smallChunkPos;




                    //Debug.Log("new map start");


                    /*if (createinit == 0)
                    {

                        createinit = 1;

                    }*/
                    computevoxels = new sccscomputevoxelshrinked();

                    //computevoxels.CreateMap(smallChunkPos);
                    computevoxels.CreateMapArrays(smallChunkPos);

                    byteCheck.sccscomputevoxelshrinkedshrinked = computevoxels;

                    //byteCheck.sccscomputevoxelshrinkedshrinked.CreateMap(byteCheck.smallChunkPos);
                    byteCheck.resultsOne = false;
                    byteCheck.resultsTwo = false;
                    byteCheck.resultsThree = false;
                    byteCheck.map = null;//

                    lock (nextQueue)
                    {
                        nextQueue.Enqueue(byteCheck);
                    }

                    //Debug.Log("new map done");

                    /*
                    int[] themap = computevoxels.CreateMap(smallChunkPos);


                    Debug.Log("new map done");
                    byteCheck.resultsOne = false;
                    byteCheck.resultsTwo = false;
                    byteCheck.resultsThree = false;
                    byteCheck.map = themap;// byteCheck.map;// realMap;


                    for (int m = 0;m < themap.Length;m++)
                    {
                        Debug.Log("themap" + themap[m]);
                    }


                    lock (nextQueue)
                    {
                        nextQueue.Enqueue(byteCheck);
                    }*/


                    /*int[] themap = computevoxels.CreateMap(smallChunkPos);

                    bool resultsOne = Array.TrueForAll(themap, s => s == 0);
                    bool resultsTwo = Array.TrueForAll(themap, s => s == 1);
                    bool resultsThree = Array.TrueForAll(themap, s => s == 2);

                    byteCheck.resultsOne = resultsOne;
                    byteCheck.resultsTwo = resultsTwo;
                    byteCheck.resultsThree = resultsThree;
                    byteCheck.map = themap;// byteCheck.map;// realMap;

                    lock (nextQueue)
                    {
                        nextQueue.Enqueue(byteCheck);
                    }*/



                    /*GameObject currentChunk = NewObjectPoolerScript.current.GetPooledObject();

                    //sccscomputevoxelshrinked computevoxels =  currentChunk.AddComponent<>();
                    sccscomputevoxelshrinked computevoxels = new sccscomputevoxelshrinked();

                    computevoxels.createchunkfaces(smallChunkPos, currentChunk);*/


                    //sccscomputevoxelshrinked computevoxels = new sccscomputevoxelshrinked();



                    /*
                    sccscomputevoxelshrinked computevoxels = new sccscomputevoxelshrinked();


                    //computevoxels.computeShaderForMap = new ComputeShader();
                    //List<>
                    if (computevoxels.computeShaderForMap != null)
                    {
                        GC.SuppressFinalize(computevoxels.computeShaderForMap);
                        computevoxels.computeShaderForMap = null;
                    }

                    computevoxels.computeShaderForMap = (ComputeShader)Resources.Load("Compute/sccsmap");//ComputeShader.Find("Transparent/Diffuse");




                    byteCheck.map = computevoxels.createchunkfaces(smallChunkPos);

                    /*bool resultsOne = Array.TrueForAll(byteCheck.map, s => s == 0);
                    bool resultsTwo = Array.TrueForAll(byteCheck.map, s => s == 1);
                    bool resultsThree = Array.TrueForAll(byteCheck.map, s => s == 2);

                    byteCheck.resultsOne = resultsOne;
                    byteCheck.resultsTwo = resultsTwo;
                    byteCheck.resultsThree = resultsThree;
                    //byteCheck.map = byteCheck.map;// realMap;

                    lock (nextQueue)
                    {
                        queuecomputeshader.Enqueue(computevoxels);
                    }
                    */








                    /*
                    if (computeShaderForMap == null)
                    {
                        computeShaderForMap = (ComputeShader)Resources.Load("Compute/sccsmap");//ComputeShader.Find("Transparent/Diffuse");

                    }

                    levelsizex = 1;
                    levelsizey = 1;
                    levelsizez = 1;

                    mapdata = new MapStruct[levelsizex * levelsizey * levelsizez][];
                    int mindex = 0;

                    for (int mx = 0; mx < levelsizex; mx++)
                    {
                        for (int my = 0; my < levelsizey; my++)
                        {
                            for (int mz = 0; mz < levelsizez; mz++)
                            {
                                mindex = mx + levelsizex * (my + levelsizey * mz);

                                //int totalSize = mapx * mapy * mapz;

                                mapdata[mindex] = new MapStruct[mapx * mapy * mapz];

                                for (int x = 0; x < mapx; x++)
                                {
                                    for (int y = 0; y < mapy; y++)
                                    {
                                        for (int z = 0; z < mapz; z++)
                                        {
                                            int index = x + mapx * (y + mapy * z);

                                            mapdata[mindex][index] = new MapStruct();
                                            mapdata[mindex][index].thebyte = 0;
                                            mapdata[mindex][index].position = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);
                                            mapdata[mindex][index].ix = x;
                                            mapdata[mindex][index].iy = y;
                                            mapdata[mindex][index].iz = z;
                                        }
                                    }
                                }


                                //mapdata = new mapbytes[1];
                                //mapdata[0].

                                int thebytesize = sizeof(int) * 4;
                                int vector3Size = sizeof(float) * 3;
                                int totalSize = thebytesize + vector3Size;

                                ComputeBuffer mapsbuffer = new ComputeBuffer(mapdata[mindex].Length, totalSize);

                                mapsbuffer.SetData(mapdata[mindex]);

                                computeShaderForMap.SetBuffer(0, "themap", mapsbuffer);
                                computeShaderForMap.Dispatch(0, (mapx * mapy * mapz) / 10, 1, 1);

                                mapsbuffer.GetData(mapdata[mindex]);

                                int[] mapint = new int[mapx * mapy * mapz];
                                for (int x = 0; x < mapx; x++)
                                {
                                    for (int y = 0; y < mapy; y++)
                                    {
                                        for (int z = 0; z < mapz; z++)
                                        {
                                            int index = x + mapx * (y + mapy * z);

                                            mapint[index] = mapdata[mindex][index].thebyte;

                                            //Debug.Log("map:" + data[index].thebyte);
                                        }
                                    }
                                }
                                mapsbuffer.Release();
                                mapsbuffer.Dispose();

                                bool resultsOne = Array.TrueForAll(mapint, s => s == 0);
                                bool resultsTwo = Array.TrueForAll(mapint, s => s == 1);
                                bool resultsThree = Array.TrueForAll(mapint, s => s == 2);

                                byteCheck.resultsOne = resultsOne;
                                byteCheck.resultsTwo = resultsTwo;
                                byteCheck.resultsThree = resultsThree;
                                byteCheck.map = mapint;// realMap;

                                lock (nextQueue)
                                {
                                    nextQueue.Enqueue(byteCheck);
                                }
                            }
                        }
                    }*/


























                    /*
                    int[] map = new int[realChunkWidth * realChunkHeight * realChunkDepth];
                    int[] realMap = new int[realChunkWidth * realChunkHeight * realChunkDepth];
                    */
                    
                    /*
                    for (int xx = 0; xx < realChunkWidth; xx++)
                    {
                        //float noiseX = Math.Abs(((float)(xx * planeSize + smallChunkPos.x + seed) / detailScale) * heightScale);

                        for (int yy = 0; yy < realChunkHeight; yy++)
                        {
                            //float noiseY = Math.Abs(((float)(yy * planeSize + smallChunkPos.y + seed) / detailScale) * heightScale);

                            for (int zz = 0; zz < realChunkDepth; zz++)
                            {
                                //float noiseZ = Math.Abs(((float)(zz * planeSize + smallChunkPos.z + seed) / detailScale) * heightScale);

                                float temporaryY = 10f;

                                temporaryY *= Mathf.PerlinNoise((xx * planeSize + smallChunkPos.x + seed) / detailScale, (zz * planeSize + smallChunkPos.z + seed) / detailScale) * heightScale;

                                float size0 = (1 / planeSize) * smallChunkPos.y;
                                temporaryY -= size0;

                                if ((int)Math.Round(temporaryY) >= yy)
                                {
                                    realMap[xx + realChunkWidth * (yy + realChunkHeight * zz)] = 1;

                                }
                                else
                                {
                                    realMap[xx + realChunkWidth * (yy + realChunkHeight * zz)] = 0;
                                }

                                if ((int)Math.Floor(temporaryY) >= yy + 1)
                                {
                                    map[xx + realChunkWidth * (yy + realChunkHeight * zz)] = 2;

                                }

                                else if ((int)Math.Floor(temporaryY) < yy - 1)
                                {
                                    map[xx + realChunkWidth * (yy + realChunkHeight * zz)] = 1;
                                    //realMap[xx + realChunkWidth * (yy + realChunkHeight * zz)] = 0;
                                }
                                else
                                {
                                    map[xx + realChunkWidth * (yy + realChunkHeight * zz)] = 0;

                                }
                            }
                        }
                    }*/


                    /*
                    bool resultsOne = false;// Array.TrueForAll(map, s => s == 0);
                    bool resultsTwo = false;//Array.TrueForAll(map, s => s == 1);
                    bool resultsThree = false;// Array.TrueForAll(map, s => s == 2);

                    byteCheck.resultsOne = resultsOne;
                    byteCheck.resultsTwo = resultsTwo;
                    byteCheck.resultsThree = resultsThree;
                    byteCheck.map = realMap;

                    lock (nextQueue)
                    {
                        nextQueue.Enqueue(byteCheck);
                    }*/
                }
            }
        }
















        int xxx;
        int yyy;
        int zzz;

        public void getPosSmall(float xi, float yi, float zi, out int xer, out int yer, out int zer, float dividerX, float dividerY, float dividerZ, int chunkSize)
        {
            int x = (Mathf.RoundToInt(xi / dividerX));
            int y = (Mathf.RoundToInt(yi / dividerY));
            int z = (Mathf.RoundToInt(zi / dividerZ));

            //Debug.Log(y);



            if (x < 0)
            {
                int yo = Mathf.Abs(x) % chunkSize;
                int yo1 = yo + (chunkSize - 1);
                xxx = yo1;
            }
            else
            {
                xxx = x % chunkSize;
            }

            if (y < 0)
            {
                int yo = Mathf.Abs(y) % chunkSize;
                //Debug.Log(yo);
                int yo1 = yo + (chunkSize - 1);
                yyy = yo1;
            }
            else
            {
                yyy = y % chunkSize;
            }

            if (z < 0)
            {
                int yo = Mathf.Abs(z) % chunkSize;
                int yo1 = yo + (chunkSize - 1);
                zzz = yo1;
            }
            else
            {
                zzz = z % chunkSize;
            }
            xer = xxx;
            yer = yyy;
            zer = zzz;
        }


    }
    public GameObject viewer;
    Vector3 viewerPosition;
    public GameObject chunker;

    public static int worldChunkWidth = 81;
    public static int regionChunkWidth = 27;
    public static int areaChunkWidth = 9;
    public static int bigChunkWidth = 3;
    public static int smallChunkWidth = 1;


    /*public static float worldChunkWidth = 8.1f;
    public static float regionChunkWidth = 2.7f;
    public static float areaChunkWidth = 0.9f;
    public static float bigChunkWidth = 0.3f;
    public static float smallChunkWidth = 0.1f;*/


    /*public static int worldChunkWidth = 162;
    public static int regionChunkWidth = 54;
    public static int areaChunkWidth = 18;
    public static int bigChunkWidth = 6;
    public static int smallChunkWidth = 2;*/

    /*public static int worldChunkWidth = 324;
    public static int regionChunkWidth = 108;
    public static int areaChunkWidth = 36;
    public static int bigChunkWidth = 12;
    public static int smallChunkWidth = 4;*/


    Vector3 realScenePos;
    public static bool startCheckingChunkData = false;
    int currentChunkCoordX;
    int currentChunkCoordY;
    int currentChunkCoordZ;
    Vector3 currentPosition;

    bool isMoving = false;

    void Start()
    {
        viewerPosition = viewer.transform.position;

        MOVEPOSOFFSET = viewer.transform.position;
        originpositionplayer = viewer.transform.position;


        Quaternion q = this.transform.rotation;

        float x = q.x;
        float y = q.x;
        float z = q.x;
        float w = q.x;

        //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
        float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
        float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
        float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);

        yawcurrent *= (float)(180 / Math.PI);
        pitchcurrent *= (float)(180 / Math.PI);
        rollcurrent *= (float)(180 / Math.PI);

        RotationX = rollcurrent;
        RotationY = rollcurrent;
        RotationZ = rollcurrent;

        beforemouselookrot = viewer.transform.rotation;
    }

    

    

    testerOfNumber Myclass = new testerOfNumber();
    void Update()
    {

        StartCoroutine(RotatePlayerMouse());
        StartCoroutine(RotatePlayerKeyboard());
        StartCoroutine(MovePlayer());
        viewerPosition = viewer.transform.position;

        StartCoroutine(CheckMoving());

        int realSceneX = (int)Mathf.Round(viewerPosition.x / bigChunkWidth);
        int realSceneY = (int)Mathf.Round(viewerPosition.y / bigChunkWidth);
        int realSceneZ = (int)Mathf.Round(viewerPosition.z / bigChunkWidth);
        realScenePos = new Vector3(realSceneX * bigChunkWidth, realSceneY * bigChunkWidth, realSceneZ * bigChunkWidth);

        if (isMoving == true)
        {
            //Debug.Log("test");
            /*Myclass.realScenePos = realScenePos;
            Myclass.viewerPosition = viewerPosition;
            testerOfNumber.newScenePos yo = new testerOfNumber.newScenePos(realScenePos, viewerPosition);
            Myclass.AccessGlobalResource(yo);
            */
            Myclass.realScenePos = realScenePos;
            Myclass.viewerPosition = viewerPosition;
            testerOfNumber.newScenePos yo = new testerOfNumber.newScenePos(realScenePos, viewerPosition);
            ThreadPool.QueueUserWorkItem(new WaitCallback(Myclass.AccessGlobalResource), yo);
        }

        if (testerOfNumber.nextQueue.Count > 0)
        {
            StartCoroutine(buildChunks());
        }
    }

   public GameObject chunktospawn;
    IEnumerator buildChunks()
    {
        for (int i = 0; i < testerOfNumber.nextQueue.Count;i++)
        {
            testerOfNumber.checkingBytes byteCheck = testerOfNumber.nextQueue.Dequeue();
            if (byteCheck != null)
            {
                bool resultsOne = byteCheck.resultsOne;
                bool resultsTwo = byteCheck.resultsTwo;
                bool resultsThree = byteCheck.resultsThree;
                
                if (!resultsOne && !resultsThree && !resultsTwo)
                {
                    //if (byteCheck.worldChunk[byteCheck.smallX + realChunkWidth * (byteCheck.smallY + realChunkHeight * byteCheck.smallZ)] == null)
                    {

                        /*
                        GameObject currentChunk = NewObjectPoolerScript.current.GetPooledObject();

                        //byteCheck.sccscomputevoxelshrinkedshrinked.CreateMap(byteCheck.smallChunkPos);
                        byteCheck.sccscomputevoxelshrinkedshrinked.workonshader(byteCheck.smallChunkPos,currentChunk);
                        */


                        GameObject currentChunk = NewObjectPoolerScript.current.GetPooledObject();

                        int[] themap = byteCheck.sccscomputevoxelshrinkedshrinked.workonshader();


                        chunk chunky = new chunk();
                        meshData meshDator = chunky.startBuildingArray(byteCheck.smallChunkPos, byteCheck.smallX, byteCheck.smallY, byteCheck.smallZ, themap);


                        
                     currentChunk.transform.position = byteCheck.smallChunkPos;// transform.position;
                     currentChunk.transform.rotation = transform.rotation;
                     currentChunk.SetActive(true);
                     currentChunk.transform.parent = (Transform)GameObject.FindGameObjectWithTag("terrain").transform;

                     Mesh mesh = currentChunk.GetComponent<MeshFilter>().mesh;
                     mesh.vertices = meshDator.positions.ToArray();
                     mesh.triangles = meshDator.triangleIndices.ToArray();
                     mesh.RecalculateNormals();
                     

                        /*
                        //byteCheck.worldChunk[byteCheck.smallX + realChunkWidth * (byteCheck.smallY + realChunkHeight * byteCheck.smallZ)] = new smallChunker();
                        chunk chunky = new chunk();
                        meshData meshDator = chunky.startBuildingArray(byteCheck.smallChunkPos, byteCheck.smallX, byteCheck.smallY, byteCheck.smallZ, themap);
                        
                        /*meshData meshDator = new meshData(); 
                        meshDator.smallX = byteCheck.smallX;
                        meshDator.smallY = byteCheck.smallY;
                        meshDator.smallZ = byteCheck.smallZ;*/
                        //meshDator. = byteCheck.map;


                        /*

                        //GameObject chunktospawn = GameObject.FindGameObjectWithTag("chunk");

                        GameObject currentChunk = NewObjectPoolerScript.current.GetPooledObject();
                        */









                        //if (obj == null) return;






                        /*GameObject emptyobject = new GameObject();
                        var meshfilt = emptyobject.AddComponent<MeshFilter>();
                        var meshrend = emptyobject.AddComponent<MeshRenderer>();*/
                        /*
                        Mesh thenewmesh = new Mesh();
                        thenewmesh.vertices = byteCheck.vertices.ToArray();
                        thenewmesh.triangles = byteCheck.triangles.ToArray();

                        currentChunk.GetComponent<MeshFilter>().mesh = thenewmesh;
                        //_testChunk.GetComponent<MeshRenderer>().material = _mat;

                        currentChunk.transform.position = byteCheck.chunkpos;
                        currentChunk.transform.rotation = Quaternion.identity;

                        currentChunk.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        currentChunk.GetComponent<MeshRenderer>().material = new Material((Material)Resources.Load("Materials/New Material"));

                        currentChunk.transform.parent = currentChunk.transform;
                        currentChunk.gameObject.name = "top faces";*/


                        /*
                        GameObject currenthunk = (GameObject)Instantiate(chunktospawn, new Vector3(meshDator.trueRealPos.x, meshDator.trueRealPos.y, meshDator.trueRealPos.z), Quaternion.identity);

                        currentChunk.transform.parent = (Transform)GameObject.FindGameObjectWithTag("terrain").transform;

                        Mesh mesh = currentChunk.GetComponent<MeshFilter>().mesh;
                        mesh.vertices = meshDator.positions.ToArray();
                        mesh.triangles = meshDator.triangleIndices.ToArray();
                        mesh.RecalculateNormals();*/

                        yield return new WaitForSeconds(0.01f);
                    }
                }
            }          
        }      
    }


    Vector3 MOVEPOSOFFSET = Vector3.zero;
    Vector3 originpositionplayer = Vector3.zero;



    public float speed = 10.0f;
    public float rotationSpeed = 100.0f;


    float speedRot = 0.175f;
    float speedPosinit = 0.055f;
    float speedPos = 0.055f;
    float rotx = 0;
    float roty = 0;
    float rotz = 0;

    int canmovecamera = 1;
    public Vector3 movePos = Vector3.zero;
    //Vector3 originPos = new Vector3(0, 2, 0);
    //public Vector3 OFFSETPOS = Vector3.Zero;
    public Vector3 dircamr = Vector3.zero;
    public Vector3 dircamu = Vector3.zero;
    public Vector3 dircamf = Vector3.zero;
    Quaternion somedirquat1;
    Matrix4x4 cammatrix;

    Matrix4x4 cammatrixvr;

    public Vector3 dircamvrr = Vector3.zero;
    public Vector3 dircamvru = Vector3.zero;
    public Vector3 dircamvrf = Vector3.zero;

    int useOculusRift = 0;

    public Matrix4x4 hmdmatrixRot;

    float RotationX = 0;
    float RotationY = 0;
    float RotationZ = 0;




    float mouserotatespeed = 100.5f;

    Quaternion beforemouselookrot = Quaternion.identity;
    int swtcactivatemouselook = 0;

    float oricursorx = 0;
    float oricursory = 0;

    IEnumerator RotatePlayerMouse()
    {

        if (Input.GetMouseButton(1))
        {
            if (swtcactivatemouselook == 0)
            {
                beforemouselookrot = viewer.transform.rotation;
                swtcactivatemouselook = 1;
            }
            Cursor.lockState = CursorLockMode.Locked;

            /*var mouseposition = Input.mousePosition;
            mouseposition.x = 0;
            mouseposition.y = 0;*/

            //Input.mousePosition = mouseposition;

            float mousex = Input.GetAxis("Mouse X") * mouserotatespeed;
            float mousey = Input.GetAxis("Mouse Y") * mouserotatespeed;

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


            RotationX += mousey;
            RotationY += mousex;

            float pitch = RotationX * 0.0174532925f;
            float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
            float roll = RotationZ * 0.0174532925f;


            

            //viewer.transform.rotation = Quaternion.Euler(pitch, yaw, roll);

            //viewer.gameObject.GetComponentInChildren<Camera>().transform.rotation = Quaternion.Euler(pitch, yaw, roll);
            viewer.gameObject.GetComponentInChildren<Camera>().transform.rotation = Quaternion.Lerp(viewer.gameObject.GetComponentInChildren<Camera>().transform.rotation, Quaternion.Euler(pitch, yaw, roll), 0.1f);

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
        else
        {
            if (swtcactivatemouselook == 1)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;


                float pitch = RotationX * 0.0174532925f;
                float yaw = RotationY * 0.0174532925f;  // float yaw = RotationY * (float)Math.PI / 180.0f;
                float roll = RotationZ * 0.0174532925f;


                //viewer.gameObject.GetComponentInChildren<Camera>().transform.rotation = Quaternion.Lerp(viewer.gameObject.GetComponentInChildren<Camera>().transform.rotation, beforemouselookrot,0.1f * Time.deltaTime);

                viewer.gameObject.GetComponentInChildren<Camera>().transform.rotation = beforemouselookrot;


                //viewer.transform.rotation = beforemouselookrot;
                swtcactivatemouselook = 0;
            }
            

        }


        yield return new WaitForSeconds(0.001f);
    }


    IEnumerator RotatePlayerKeyboard()
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

            RotationY -= rotationincrements;
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
            viewer.transform.rotation = Quaternion.Euler(pitch , yaw , roll );
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

            RotationY += rotationincrements;
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


            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
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

            RotationX += rotationincrements;
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


            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            viewer.transform.rotation = Quaternion.Euler(pitch , yaw , roll );
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

            RotationX -= rotationincrements;
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


            //Vector3 up = transform.TransformPoint(Vector3.up, rotationMatrix);
            viewer.transform.rotation = Quaternion.Euler(pitch , yaw , roll );
            //Matrix4x4.Rotate
        }

        //getPan

        yield return new WaitForSeconds(0.001f);
    }



    float getPan(Transform t)
    {
        return t.localEulerAngles.z;
    }

    float getRoll(Transform originalTransform)
    {
        //GameObject tempGO = new GameObject();
        Transform t = viewer.transform;
        t.localRotation = originalTransform.localRotation;

        t.Rotate(0, 0, t.localEulerAngles.z * -1);

        //GameObject.Destroy(tempGO);
        return t.localEulerAngles.x;
    }

    float getTilt(Transform originalTransform)
    {
        //GameObject tempGO = new GameObject();
        Transform t = viewer.transform;
        t.localRotation = originalTransform.localRotation;

        t.Rotate(0, 0, t.localEulerAngles.z * -1);
        t.Rotate(t.localEulerAngles.x * -1, 0, 0);

        //GameObject.Destroy(viewer);
        return t.localEulerAngles.y;
    }


    IEnumerator MovePlayer()
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
        }

        //var currentrotationforward = viewer.transform.rotation.eulerAngles.z;

        MOVEPOSOFFSET = Vector3.Lerp(topointforward, MOVEPOSOFFSET, 0.1f);

        viewer.transform.position = MOVEPOSOFFSET;
        lastframeviewerpos = viewer.transform.position;
    }

    Vector3 lastframeviewerpos = Vector3.zero;





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



    public struct meshData
    {
        //public readonly smallChunk smallChunk;
        //public readonly bigChunk bigChunk;
        //public readonly Vector3 fakePosition;
        public readonly int lengthOfArray;
        public Vector3[] positions;
        public int[] triangleIndices;
        public Vector3 trueRealPos;
        //public GameObject currentChunkObject;
        //public byte[] mapByte;
        /*public int regionX;
        public int regionY;
        public int regionZ;
        public int areaX;
        public int areaY;
        public int areaZ;
        public int bigX;
        public int bigY;
        public int bigZ;*/
        public int smallX;
        public int smallY;
        public int smallZ;
        //public regionChunker regionChunko;
        //public regionChunker[] worldChunk;
        //public areaChunker areaChunko;
        //public bigChunker bigChunko;
        /*public bool hasMapGenerated;
        public bool hasMeshGenerated;
        public bool hasBeenSpawned;
        public bool distanceRejected;
        public Vector3 worldScenePos;*/


        public meshData(Vector3 realPos, int lengthOfArray, Vector3[] positions, int[] triangleIndices, int smallX, int smallY, int smallZ)
        {
            //this.smallChunk = smallChunk;
            //this.bigChunk = bigChunk;
            //this.fakePosition = fakePos;
            this.lengthOfArray = lengthOfArray;
            this.positions = positions;
            this.triangleIndices = triangleIndices;
            this.trueRealPos = realPos;
            //this.currentChunkObject = currentChunk;
            //this.mapByte = meshData;
            //this.regionX = regionX;
            //this.regionY = regionY;
            //this.regionZ = regionZ;
            //this.areaX = areaX;
            /*this.areaY = areaY;
            this.areaZ = areaZ;
            this.bigX = bigX;
            this.bigY = bigY;
            this.bigZ = bigZ;*/
            this.smallX = smallX;
            this.smallY = smallY;
            this.smallZ = smallZ;
            /*this.regionChunko = regionChunko;
            this.hasBeenSpawned = hasSpawned;
            this.hasMapGenerated = hasMap;
            this.hasMeshGenerated = hasMesh;
            this.distanceRejected = distReject;
            this.worldChunk = worldChunk;
            this.worldScenePos = worldScenePos;*/

            //this.areaChunko = areaChunko;
            //this.bigChunko = bigChunko;
        }
    }
}
















