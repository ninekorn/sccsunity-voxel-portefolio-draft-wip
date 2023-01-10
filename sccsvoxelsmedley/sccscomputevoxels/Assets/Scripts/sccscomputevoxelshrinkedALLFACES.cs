using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class sccscomputevoxelshrinkedALLFACES// : MonoBehaviour
{
    /* public struct mapbytes
     {
         public int ix;
         public int iy;
         public int iz;
         public int thebyte;
         public Vector3 position;
     }

     public struct mapofints
     {
         public int thebyte;
     };*/

    /*public static mapbytes[][] mapdata;
    public static mapofints[][] datamapfirstvertxtop;
    public static mapofints[][] datamapfirstvertytop;
    public static mapofints[][] datamapfirstvertztop;
    public static mapofints[][] datawidthdimtop;
    public static mapofints[][] dataheightdimtop;
    public static mapofints[][] datadepthdimtop;*/


    public static int numberoffaces = 6;
    //List<Vector3> vertices = new List<Vector3>();
    //List<int> triangles = new List<int>();


    public static int levelsizex = 1;
    public static int levelsizey = 1;
    public static int levelsizez = 1;
    public static int mapx = 10;
    public static int mapy = 10;
    public static int mapz = 10;

    public static int threadmulx = 1;
    public static int threadmuly = 1;
    public static int threadmulz = 1;


    public static ComputeShader computeShaderForMap;
    public static ComputeShader computeVertexesALLFACES;
    public static ComputeShader swapcomputetop;
    public static ComputeShader swapcomputeleft;
    public static ComputeShader swapcomputeright;
    public static ComputeShader swapcomputefront;
    public static ComputeShader swapcomputeback;
    public static ComputeShader swapcomputebottom;

    /*public static ComputeBuffer maps0buffer;

    public static ComputeBuffer mapvertlocbufferx;
    public static ComputeBuffer mapvertlocbuffery;
    public static ComputeBuffer mapvertlocbufferz;

    public static ComputeBuffer mapwidthdimtop;
    public static ComputeBuffer mapheightdimtop;
    public static ComputeBuffer mapdepthdimtop;*/

    public static Vector3 chunkmainpos;


    public static Vector3 firstvertofface = Vector3.zero;
    public static Vector3 secondvertofface = Vector3.zero;
    public static Vector3 thirdvertofface = Vector3.zero;
    public static Vector3 fourthvertofface = Vector3.zero;

    public static int reducedverttrigswtc = 0;
    // Start is called before the first frame update

    public static Material mat;

    public static universemapvert.testerOfNumber.mainChunk CreateMapArrays(Vector3 chunkpos, universemapvert.testerOfNumber.mainChunk mainChunk)
    {





        //mapdata = new mapbytes[levelsizex * levelsizey * levelsizez][];

        //int[] mapint = new int[mapx * mapy * mapz];



        mainChunk.mapdata = new universemapvert.testerOfNumber.mapbytes[levelsizex * levelsizey * levelsizez][];
        mainChunk.datamapfirstvertxtop = new universemapvert.testerOfNumber.mapofints[levelsizex * levelsizey * levelsizez][];
        mainChunk.datamapfirstvertytop = new universemapvert.testerOfNumber.mapofints[levelsizex * levelsizey * levelsizez][];
        mainChunk.datamapfirstvertztop = new universemapvert.testerOfNumber.mapofints[levelsizex * levelsizey * levelsizez][];
        mainChunk.datawidthdimtop = new universemapvert.testerOfNumber.mapofints[levelsizex * levelsizey * levelsizez][];
        mainChunk.dataheightdimtop = new universemapvert.testerOfNumber.mapofints[levelsizex * levelsizey * levelsizez][];
        mainChunk.datadepthdimtop = new universemapvert.testerOfNumber.mapofints[levelsizex * levelsizey * levelsizez][];


        //GameObject emptyobjectparent0 = this.transform.gameObject;
        //GameObject emptyobjectparent0 = new GameObject();
        /*GameObject emptyobjectparent1 = new GameObject();
        GameObject emptyobjectparentleftfaces = new GameObject();
        emptyobjectparentleftfaces.gameObject.name = "leftfacesmain";

        GameObject emptyobjectparentrightfaces = new GameObject();
        emptyobjectparentrightfaces.gameObject.name = "rightfacesmain";

        GameObject emptyobjectparentfrontfaces = new GameObject();
        emptyobjectparentfrontfaces.gameObject.name = "frontfacesmain";*/


        for (int mx = 0; mx < levelsizex; mx++)
        {
            for (int my = 0; my < levelsizey; my++)
            {
                for (int mz = 0; mz < levelsizez; mz++)
                {
                    int mindex = mx + levelsizex * (my + levelsizey * mz);

                    chunkmainpos = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f) + chunkpos;


                    //int totalSize = mapx * mapy * mapz;
                    mainChunk.mapdata[mindex] = new universemapvert.testerOfNumber.mapbytes[mapx * mapy * mapz];
                    mainChunk.datamapfirstvertxtop[mindex] = new universemapvert.testerOfNumber.mapofints[mapx * mapy * mapz];
                    mainChunk.datamapfirstvertytop[mindex] = new universemapvert.testerOfNumber.mapofints[mapx * mapy * mapz];
                    mainChunk.datamapfirstvertztop[mindex] = new universemapvert.testerOfNumber.mapofints[mapx * mapy * mapz];
                    mainChunk.datawidthdimtop[mindex] = new universemapvert.testerOfNumber.mapofints[mapx * mapy * mapz];
                    mainChunk.dataheightdimtop[mindex] = new universemapvert.testerOfNumber.mapofints[mapx * mapy * mapz];
                    mainChunk.datadepthdimtop[mindex] = new universemapvert.testerOfNumber.mapofints[mapx * mapy * mapz];

                    for (int x = 0; x < mapx; x++)
                    {
                        for (int y = 0; y < mapy; y++)
                        {
                            for (int z = 0; z < mapz; z++)
                            {
                                int index = x + mapx * (y + mapy * z);

                                mainChunk.mapdata[mindex][index] = new universemapvert.testerOfNumber.mapbytes();
                                mainChunk.mapdata[mindex][index].thebyte = 0;
                                mainChunk.mapdata[mindex][index].position = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f) + chunkmainpos;
                                mainChunk.mapdata[mindex][index].ix = x;
                                mainChunk.mapdata[mindex][index].iy = y;
                                mainChunk.mapdata[mindex][index].iz = z;

                                mainChunk.datamapfirstvertxtop[mindex][index] = new universemapvert.testerOfNumber.mapofints();
                                mainChunk.datamapfirstvertxtop[mindex][index].thebyte = 0;

                                mainChunk.datamapfirstvertytop[mindex][index] = new universemapvert.testerOfNumber.mapofints();
                                mainChunk.datamapfirstvertytop[mindex][index].thebyte = 0;

                                mainChunk.datamapfirstvertztop[mindex][index] = new universemapvert.testerOfNumber.mapofints();
                                mainChunk.datamapfirstvertztop[mindex][index].thebyte = 0;

                                mainChunk.datawidthdimtop[mindex][index] = new universemapvert.testerOfNumber.mapofints();
                                mainChunk.datawidthdimtop[mindex][index].thebyte = 0;

                                mainChunk.dataheightdimtop[mindex][index] = new universemapvert.testerOfNumber.mapofints();
                                mainChunk.dataheightdimtop[mindex][index].thebyte = 0;

                                mainChunk.datadepthdimtop[mindex][index] = new universemapvert.testerOfNumber.mapofints();
                                mainChunk.datadepthdimtop[mindex][index].thebyte = 0;
                            }
                        }
                    }

                }




                /*maps0buffer.Release();
                mapvertlocbufferx.Release();
                mapvertlocbuffery.Release();
                mapvertlocbufferz.Release();
                mapwidthdimtop.Release();
                mapheightdimtop.Release();
                mapdepthdimtop.Release();



                maps0buffer.Dispose();
                mapvertlocbufferx.Dispose();
                mapvertlocbuffery.Dispose();
                mapvertlocbufferz.Dispose();
                mapwidthdimtop.Dispose();
                mapheightdimtop.Dispose();
                mapdepthdimtop.Dispose();*/
                // _tempChunkArraybuffer.Dispose();







                /*
                GameObject emptyobject1 = new GameObject();

                //chunko thechunk = new chunko();

                emptyobject1.AddComponent<singleChunk>();

                singleChunk thechunk = emptyobject1.GetComponent<singleChunk>();
                thechunk.width = mapx;
                thechunk.height = mapy;
                thechunk.depth = mapz;

                thechunk.widthflat = mapx;
                thechunk.heightflat = mapy;
                thechunk.depthflat = mapz;

                thechunk.vertexlistWidth = thechunk.width + 1;
                thechunk.vertexlistHeight = thechunk.height + 1;
                thechunk.vertexlistDepth = thechunk.depth + 1;

                thechunk.createvars();
                thechunk.map = mapint;
                thechunk._mat = mat;
                thechunk._testChunk = emptyobject1;
                thechunk.startthegen(chunkmainpos);

                emptyobject1.transform.position = new Vector3(0, -0.01f, 0);
                emptyobject1.transform.position += chunkmainpos;
                emptyobject1.transform.rotation = Quaternion.identity;
                emptyobject1.transform.parent = emptyobjectparent1.transform;*/




            }
        }







        return mainChunk;
        //Debug.Log("arraycreated");
        //return mapint;
    }


    //static ComputeBuffer mapsbuffer;


    static int shaderinit = 0;

    static int thebytesize;
    static int vector3Size;
    static int totalSize;


    public static universemapvert.testerOfNumber.mainChunk workonshader(Vector3 chunkpos, GameObject thecurrentchunk, out List<Vector3> vertices, out List<int> triangles, universemapvert.testerOfNumber.mainChunk mainChunk)
    {



        /*if (vertices != null)
        {
            vertices.Clear();
            vertices = new List<Vector3>();
        }*/
        vertices = new List<Vector3>();
        triangles = new List<int>();


        /*if (computeShaderForMap != null)
        {
            GC.SuppressFinalize(computeShaderForMap);
            computeShaderForMap = null;
        }*/

        int mindex = 0;
        int[] mapint;// = new int[mapx * mapy * mapz];


        thebytesize = sizeof(int) * 4;
        vector3Size = sizeof(float) * 3;
        totalSize = thebytesize + vector3Size;


        if (shaderinit == 0)
        {
            computeShaderForMap = (ComputeShader)Resources.Load("Compute/sccsmapallcompute");//ComputeShader.Find("Transparent/Diffuse");
            shaderinit = 1;
        }



        universemapvert.mapsbuffer = new ComputeBuffer(mainChunk.mapdata[mindex].Length, totalSize);

        universemapvert.mapsbuffer.SetCounterValue(0);
        universemapvert.mapsbuffer.SetData(mainChunk.mapdata[mindex]);



        if (computeShaderForMap == null)
        {
            Debug.Log("computeShaderForMap null");
        }
        else
        {
            Debug.Log("computeShaderForMap !null");

        }

        computeShaderForMap.SetBuffer(0, "themap", universemapvert.mapsbuffer);

        computeShaderForMap.Dispatch(0, (mapx * mapy * mapz) / 10, 1, 1);

        universemapvert.mapsbuffer.GetData(mainChunk.mapdata[mindex]);

        mapint = new int[mapx * mapy * mapz];
        for (int x = 0; x < mapx; x++)
        {
            for (int y = 0; y < mapy; y++)
            {
                for (int z = 0; z < mapz; z++)
                {
                    int index = x + mapx * (y + mapy * z);

                    mapint[index] = mainChunk.mapdata[mindex][index].thebyte;

                    //Debug.Log("map:" + data[index].thebyte);
                }
            }
        }
        /*
        mainChunk.mapsbuffer.Release();
        mainChunk.mapsbuffer.Dispose();
        */




        int canmoveforward = 1;
        int canmoveforwardtwo = 1;



        if (canmoveforward == 1)
        {
            //vertices = new List<Vector3>();
            //triangles = new List<int>();


            for (int f = 0; f < numberoffaces; f++)
            {
                /*GameObject emptyobjectparent0 = new GameObject();

                emptyobjectparent0.transform.name = "chunkfacetype-" + f;
                emptyobjectparent0.transform.position = chunkpos;
                emptyobjectparent0.transform.parent = this.transform;*/










                /*
                if (computeVertexesALLFACES != null)
                {
                    GC.SuppressFinalize(computeVertexesALLFACES);
                    computeVertexesALLFACES = null;
                }*/
                /*computeVertexesALLFACES = null;
                if (f == 0) //&& computeVertexesALLFACES == null
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexTOP");//ComputeShader.Find("Transparent/Diffuse");
                }
                if (f == 1)// && computeVertexesALLFACES == null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexLEFT");//ComputeShader.Find("Transparent/Diffuse");
                }
                if (f == 2)// && computeVertexesALLFACES == null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexRIGHT");//ComputeShader.Find("Transparent/Diffuse");
                }

                if (f == 3)// && computeVertexesALLFACES == null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexFRONT");//ComputeShader.Find("Transparent/Diffuse");
                }
                if (f == 4)// && computeVertexesALLFACES == null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexBACK");//ComputeShader.Find("Transparent/Diffuse");
                }
                if (f == 5)// && computeVertexesALLFACES == null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexBOTTOM");//ComputeShader.Find("Transparent/Diffuse");
                }*/


                
                if (f == 0) //&& computeVertexesALLFACES == null
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexTOP");//ComputeShader.Find("Transparent/Diffuse");
                    swapcomputetop = computeVertexesALLFACES;
                }
                else
                {
                    if (swapcomputetop != null)
                    {
                        computeVertexesALLFACES = swapcomputetop;
                        //swapcompute = 
                    }
                }



                if (f == 1)// && computeVertexesALLFACES == null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexLEFT");//ComputeShader.Find("Transparent/Diffuse");
                    swapcomputeleft = computeVertexesALLFACES;
                }
                else
                {
                    if (swapcomputeleft != null)
                    {
                        computeVertexesALLFACES = swapcomputeleft;
                        //swapcompute = 
                    }
                }




                if (f == 2)// && computeVertexesALLFACES == null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexRIGHT");//ComputeShader.Find("Transparent/Diffuse");
                    swapcomputeright = computeVertexesALLFACES;
                }
                else
                {
                    if (swapcomputeright != null)
                    {
                        computeVertexesALLFACES = swapcomputeright;
                        //swapcompute = 
                    }
                }




                if (f == 3)// && computeVertexesALLFACES == null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexFRONT");//ComputeShader.Find("Transparent/Diffuse");
                    swapcomputefront = computeVertexesALLFACES;
                }
                else
                {
                    if (swapcomputefront != null)
                    {
                        computeVertexesALLFACES = swapcomputefront;
                        //swapcompute = 
                    }
                }

                if (f == 4)// && computeVertexesALLFACES == null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexBACK");//ComputeShader.Find("Transparent/Diffuse");
                    swapcomputeback = computeVertexesALLFACES;
                }
                else
                {
                    if (swapcomputeback != null)
                    {
                        computeVertexesALLFACES = swapcomputeback;
                        //swapcompute = 
                    }
                }
                if (f == 5)// && computeVertexesALLFACES == null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexBOTTOM");//ComputeShader.Find("Transparent/Diffuse");
                    swapcomputebottom = computeVertexesALLFACES;
                }
                else
                {
                    if (swapcomputebottom != null)
                    {
                        computeVertexesALLFACES = swapcomputebottom;
                        //swapcompute = 
                    }
                }


                int dotherest = 0;
                int dotherest1 = 0;


                if (dotherest == 0)
                {


                    for (int mx = 0; mx < levelsizex; mx++)
                    {
                        for (int my = 0; my < levelsizey; my++)
                        {
                            for (int mz = 0; mz < levelsizez; mz++)
                            {
                                mindex = mx + levelsizex * (my + levelsizey * mz);

                                Vector3 chunkmainpos = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);




                                //if (f == 0 && mindex == 0)
                                {


                                    /*int thebytesize = sizeof(int) * 4;
                                    int vector3Size = sizeof(float) * 3;
                                    int totalSize = thebytesize + vector3Size;

                                    ComputeBuffer mapsbuffer = new ComputeBuffer(mapdata[mindex].Length, totalSize);

                                    mapsbuffer.SetData(mapdata[mindex]);


                                    if (computeShaderForMap == null)
                                    {

                                    }

                                    computeShaderForMap = (ComputeShader)Resources.Load("Compute/sccsmap");//ComputeShader.Find("Transparent/Diffuse");
                                    */
                                    /*


                                    computeShaderForMap.SetBuffer(0, "themap", mapsbuffer);
                                    computeShaderForMap.Dispatch(0, mapdata[mindex].Length / 10, 1, 1);

                                    mapsbuffer.GetData(mapdata[mindex]);

                                    mapint = new int[mapx * mapy * mapz];
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
                                    mapsbuffer.Dispose();*/




                                    /*maps0buffer.SetCounterValue(0);
                                    mapvertlocbufferx.SetCounterValue(0);
                                    mapvertlocbuffery.SetCounterValue(0);
                                    mapvertlocbufferz.SetCounterValue(0);

                                    mapwidthdimtop.SetCounterValue(0);
                                    mapheightdimtop.SetCounterValue(0);
                                    mapdepthdimtop.SetCounterValue(0);*/



                                    if (mainChunk.maps0buffer != null)
                                    {
                                        mainChunk.maps0buffer.Release();
                                        mainChunk.maps0buffer.Dispose();

                                    }
                                    if (mainChunk.mapvertlocbufferx != null)
                                    {
                                        mainChunk.mapvertlocbufferx.Release();
                                        mainChunk.mapvertlocbufferx.Dispose();

                                    }

                                    if (mainChunk.mapvertlocbuffery != null)
                                    {
                                        mainChunk.mapvertlocbuffery.Release();
                                        mainChunk.mapvertlocbuffery.Dispose();

                                    }

                                    if (mainChunk.mapvertlocbufferz != null)
                                    {
                                        mainChunk.mapvertlocbufferz.Release();
                                        mainChunk.mapvertlocbufferz.Dispose();

                                    }
                                    if (mainChunk.mapwidthdimtop != null)
                                    {
                                        mainChunk.mapwidthdimtop.Release();
                                        mainChunk.mapwidthdimtop.Dispose();

                                    }
                                    if (mainChunk.mapheightdimtop != null)
                                    {
                                        mainChunk.mapheightdimtop.Release();
                                        mainChunk.mapheightdimtop.Dispose();

                                    }
                                    if (mainChunk.mapdepthdimtop != null)
                                    {
                                        mainChunk.mapdepthdimtop.Release();
                                        mainChunk.mapdepthdimtop.Dispose();
                                    }


                                    mainChunk.maps0buffer = new ComputeBuffer(mainChunk.mapdata[mindex].Length, totalSize);
                                    mainChunk.maps0buffer.SetData(mainChunk.mapdata[mindex]);

                                    mainChunk.mapvertlocbufferx = new ComputeBuffer(mainChunk.datamapfirstvertxtop[mindex].Length, 4);
                                    mainChunk.mapvertlocbufferx.SetData(mainChunk.datamapfirstvertxtop[mindex]);

                                    mainChunk.mapvertlocbuffery = new ComputeBuffer(mainChunk.datamapfirstvertytop[mindex].Length, 4);
                                    mainChunk.mapvertlocbuffery.SetData(mainChunk.datamapfirstvertytop[mindex]);

                                    mainChunk.mapvertlocbufferz = new ComputeBuffer(mainChunk.datamapfirstvertztop[mindex].Length, 4);
                                    mainChunk.mapvertlocbufferz.SetData(mainChunk.datamapfirstvertztop[mindex]);

                                    mainChunk.mapwidthdimtop = new ComputeBuffer(mainChunk.datawidthdimtop[mindex].Length, 4);
                                    mainChunk.mapwidthdimtop.SetData(mainChunk.datawidthdimtop[mindex]);

                                    mainChunk.mapheightdimtop = new ComputeBuffer(mainChunk.dataheightdimtop[mindex].Length, 4);
                                    mainChunk.mapheightdimtop.SetData(mainChunk.dataheightdimtop[mindex]);

                                    mainChunk.mapdepthdimtop = new ComputeBuffer(mainChunk.datadepthdimtop[mindex].Length, 4);
                                    mainChunk.mapdepthdimtop.SetData(mainChunk.datadepthdimtop[mindex]);
                                }
                                //else
                                {
                                    /*int thebytesize = sizeof(int) * 4;
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
                                    */


                                    /*
                                    //maps0buffer.SetCounterValue(0);
                                    mapvertlocbufferx.SetCounterValue(0);
                                    mapvertlocbuffery.SetCounterValue(0);
                                    mapvertlocbufferz.SetCounterValue(0);

                                    mapwidthdimtop.SetCounterValue(0);
                                    mapheightdimtop.SetCounterValue(0);
                                    mapdepthdimtop.SetCounterValue(0);


                                    maps0buffer.SetData(mapdata[mindex]);
                                    mapvertlocbufferx.SetData(datamapfirstvertxtop[mindex]);
                                    mapvertlocbuffery.SetData(datamapfirstvertytop[mindex]);
                                    mapvertlocbufferz.SetData(datamapfirstvertztop[mindex]);
                                    mapwidthdimtop.SetData(datawidthdimtop[mindex]);
                                    mapheightdimtop.SetData(dataheightdimtop[mindex]);
                                    mapdepthdimtop.SetData(datadepthdimtop[mindex]);

                                    */


                                }









                                if (dotherest1 == 0)
                                {




                                    if (reducedverttrigswtc == 0)
                                    {



                                        /*// Create a material with transparent diffuse shader
                                        Material material = new Material(Shader.Find("Transparent/Diffuse"));
                                        material.color = Color.green;

                                        // assign the material to the renderer
                                        GetComponent<Renderer>().material = material;*/






                                        computeVertexesALLFACES.SetBuffer(0, "themap", mainChunk.maps0buffer);
                                        computeVertexesALLFACES.SetBuffer(0, "mapfirstvertxtop", mainChunk.mapvertlocbufferx);
                                        computeVertexesALLFACES.SetBuffer(0, "mapfirstvertytop", mainChunk.mapvertlocbuffery);
                                        computeVertexesALLFACES.SetBuffer(0, "mapfirstvertztop", mainChunk.mapvertlocbufferz);

                                        computeVertexesALLFACES.SetBuffer(0, "widthdimtop", mainChunk.mapwidthdimtop);
                                        computeVertexesALLFACES.SetBuffer(0, "heightdimtop", mainChunk.mapheightdimtop);
                                        computeVertexesALLFACES.SetBuffer(0, "depthdimtop", mainChunk.mapdepthdimtop);

                                        computeVertexesALLFACES.Dispatch(0, threadmulx, threadmuly, threadmulz);











                                    }
                                    else if (reducedverttrigswtc == 1)
                                    {
                                        /*
                                        computeVertexestwo.SetBuffer(0, "themap", maps0buffer);
                                        computeVertexestwo.SetBuffer(0, "mapfirstvertxtop", mapvertlocbufferx);
                                        //computeVertexes.SetBuffer(0, "mapfirstvertytop", mapvertlocbuffery);
                                        computeVertexestwo.SetBuffer(0, "mapfirstvertztop", mapvertlocbufferz);

                                        computeVertexestwo.SetBuffer(0, "widthdimtop", mapwidthdimtop);
                                        computeVertexestwo.SetBuffer(0, "heightdimtop", mapheightdimtop);
                                        computeVertexestwo.SetBuffer(0, "depthdimtop", mapdepthdimtop);


                                        computeVertexestwo.Dispatch(0, 1, 1, 1);*/


                                    }


                                    mainChunk.mapvertlocbufferx.GetData(mainChunk.datamapfirstvertxtop[mindex]);
                                    mainChunk.mapvertlocbuffery.GetData(mainChunk.datamapfirstvertytop[mindex]);
                                    mainChunk.mapvertlocbufferz.GetData(mainChunk.datamapfirstvertztop[mindex]);

                                    mainChunk.mapwidthdimtop.GetData(mainChunk.datawidthdimtop[mindex]);
                                    mainChunk.mapheightdimtop.GetData(mainChunk.dataheightdimtop[mindex]);
                                    mainChunk.mapdepthdimtop.GetData(mainChunk.datadepthdimtop[mindex]);

                                    vertices = new List<Vector3>();
                                    triangles = new List<int>();


                                    for (int x = 0; x < mapx; x++)
                                    {
                                        for (int y = 0; y < mapy; y++)
                                        {
                                            for (int z = 0; z < mapz; z++)
                                            {
                                                int index = x + mapx * (y + mapy * z);

                                                //mapint[index] = data[mindex][index].thebyte;

                                                //Debug.Log("map:" + data[index].thebyte);



                                                if (mainChunk.dataheightdimtop[mindex][index].thebyte == 0)//datamapfirstvertxtop[mindex][index].thebyte == 0 && datamapfirstvertytop[mindex][index].thebyte == 0 && datamapfirstvertztop[mindex][index].thebyte == 0)
                                                {

                                                }
                                                else
                                                {
                                                    //if (mapint[index] == 1)//IsTransparent(x, y+1,z, originalmapdata[mindex]) && blockExistsInArray(x, y - 1 ,z) == 1)// mapint[index] == 1) //mapdata[mindex]
                                                    {


                                                        if (f == 0)
                                                        {




                                                            int indexofvert0 = vertices.Count;
                                                            firstvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                                            int indexofvert1 = vertices.Count + 1;
                                                            secondvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                                            int indexofvert2 = vertices.Count + 2;
                                                            thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                                            int indexofvert3 = vertices.Count + 3;
                                                            fourthvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));


                                                            vertices.Add(firstvertofface);
                                                            vertices.Add(secondvertofface);
                                                            vertices.Add(thirdvertofface);
                                                            vertices.Add(fourthvertofface);

                                                            triangles.Add(indexofvert2);
                                                            triangles.Add(indexofvert1);
                                                            triangles.Add(indexofvert0);
                                                            triangles.Add(indexofvert1);
                                                            triangles.Add(indexofvert2);
                                                            triangles.Add(indexofvert3);
                                                        }
                                                        else if (f == 1)
                                                        {

                                                            int indexofvert0 = vertices.Count;
                                                            firstvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                                            float swapx = firstvertofface.x;
                                                            float swapy = firstvertofface.y;
                                                            float swapz = firstvertofface.z;

                                                            //firstvertofface.x = swapy;
                                                            //firstvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte;

                                                            //firstvertofface.x -= (1 * 1.0f);
                                                            //firstvertofface.y -= (1 * 1.0f);


                                                            int indexofvert1 = vertices.Count + 1;
                                                            secondvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);
                                                            /*swapx = secondvertofface.x;
                                                            swapy = secondvertofface.y;
                                                            swapz = secondvertofface.z;

                                                            secondvertofface.x = swapy;
                                                            secondvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                                            secondvertofface.x -= (1 * 1.0f);
                                                            secondvertofface.y -= (1 * 1.0f);


                                                            int indexofvert2 = vertices.Count + 2;
                                                            thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                                            //thirdvertofface.x -= (1 * 1.0f);
                                                            //thirdvertofface.y -= (1 * 1.0f);
                                                            /*swapx = thirdvertofface.x;
                                                            swapy = thirdvertofface.y;
                                                            swapz = thirdvertofface.z;

                                                            thirdvertofface.x = swapy;
                                                            thirdvertofface.y = swapx;*/

                                                            int indexofvert3 = vertices.Count + 3;
                                                            fourthvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                                            fourthvertofface.x -= (1 * 1.0f);
                                                            fourthvertofface.y -= (1 * 1.0f);


                                                            /*swapx = fourthvertofface.x;
                                                            swapy = fourthvertofface.y;
                                                            swapz = fourthvertofface.z;

                                                            fourthvertofface.x = swapy;
                                                            fourthvertofface.y = swapx;*/
                                                            /*
                                                            int indexofvert0 = vertices.Count;
                                                            Vector3 firstvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                                            int indexofvert1 = vertices.Count + 1;
                                                            Vector3 secondvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                                            int indexofvert2 = vertices.Count + 2;
                                                            Vector3 thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                                            int indexofvert3 = vertices.Count + 3;
                                                            Vector3 fourthvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte),mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                                            */



                                                            vertices.Add(firstvertofface);
                                                            vertices.Add(secondvertofface);
                                                            vertices.Add(thirdvertofface);
                                                            vertices.Add(fourthvertofface);

                                                            triangles.Add(indexofvert0);
                                                            triangles.Add(indexofvert1);
                                                            triangles.Add(indexofvert2);
                                                            triangles.Add(indexofvert3);
                                                            triangles.Add(indexofvert2);
                                                            triangles.Add(indexofvert1);
                                                        }
                                                        else if (f == 2)
                                                        {


                                                            int indexofvert0 = vertices.Count;
                                                            firstvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                                            float swapx = firstvertofface.x;
                                                            float swapy = firstvertofface.y;
                                                            float swapz = firstvertofface.z;

                                                            //firstvertofface.x = swapy;
                                                            //firstvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte;

                                                            //firstvertofface.x -= (1 * 1.0f);
                                                            //firstvertofface.y -= (1 * 1.0f);


                                                            int indexofvert1 = vertices.Count + 1;
                                                            secondvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);
                                                            /*swapx = secondvertofface.x;
                                                            swapy = secondvertofface.y;
                                                            swapz = secondvertofface.z;

                                                            secondvertofface.x = swapy;
                                                            secondvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                                            secondvertofface.x -= (1 * 1.0f);
                                                            secondvertofface.y -= (1 * 1.0f);


                                                            int indexofvert2 = vertices.Count + 2;
                                                            thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                                            //thirdvertofface.x -= (1 * 1.0f);
                                                            //thirdvertofface.y -= (1 * 1.0f);
                                                            /*swapx = thirdvertofface.x;
                                                            swapy = thirdvertofface.y;
                                                            swapz = thirdvertofface.z;

                                                            thirdvertofface.x = swapy;
                                                            thirdvertofface.y = swapx;*/

                                                            int indexofvert3 = vertices.Count + 3;
                                                            fourthvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                                            fourthvertofface.x -= (1 * 1.0f);
                                                            fourthvertofface.y -= (1 * 1.0f);


                                                            /*swapx = fourthvertofface.x;
                                                            swapy = fourthvertofface.y;
                                                            swapz = fourthvertofface.z;

                                                            fourthvertofface.x = swapy;
                                                            fourthvertofface.y = swapx;*/
                                                            /*
                                                            int indexofvert0 = vertices.Count;
                                                            Vector3 firstvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                                            int indexofvert1 = vertices.Count + 1;
                                                            Vector3 secondvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                                            int indexofvert2 = vertices.Count + 2;
                                                            Vector3 thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                                            int indexofvert3 = vertices.Count + 3;
                                                            Vector3 fourthvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte),mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                                            */



                                                            vertices.Add(firstvertofface);
                                                            vertices.Add(secondvertofface);
                                                            vertices.Add(thirdvertofface);
                                                            vertices.Add(fourthvertofface);

                                                            triangles.Add(indexofvert2);
                                                            triangles.Add(indexofvert1);
                                                            triangles.Add(indexofvert0);
                                                            triangles.Add(indexofvert1);
                                                            triangles.Add(indexofvert2);
                                                            triangles.Add(indexofvert3);
                                                        }
                                                        else if (f == 3)
                                                        {

                                                            int indexofvert0 = vertices.Count;
                                                            firstvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                                            float swapx = firstvertofface.x;
                                                            float swapy = firstvertofface.y;
                                                            float swapz = firstvertofface.z;

                                                            //firstvertofface.x = swapy;
                                                            //firstvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte;

                                                            /*firstvertofface.z -= (1 * 1.0f);
                                                            firstvertofface.y -= (1 * 1.0f);*/
                                                            //firstvertofface.z = swapy;
                                                            //firstvertofface.y = swapz;

                                                            int indexofvert1 = vertices.Count + 1;
                                                            secondvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);
                                                            swapx = secondvertofface.x;
                                                            swapy = secondvertofface.y;
                                                            swapz = secondvertofface.z;

                                                            /*secondvertofface.x = swapy;
                                                            secondvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                                            /*secondvertofface.x -= (1 * 1.0f);
                                                            secondvertofface.y -= (1 * 1.0f);*/


                                                            int indexofvert2 = vertices.Count + 2;
                                                            thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                                            thirdvertofface.z -= (1 * 1.0f);
                                                            thirdvertofface.y -= (1 * 1.0f);
                                                            swapx = thirdvertofface.x;
                                                            swapy = thirdvertofface.y;
                                                            swapz = thirdvertofface.z;

                                                            /*thirdvertofface.z = swapy;
                                                            thirdvertofface.y = swapz;*/

                                                            int indexofvert3 = vertices.Count + 3;
                                                            fourthvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                                            fourthvertofface.z -= (1 * 1.0f);
                                                            fourthvertofface.y -= (1 * 1.0f);


                                                            swapx = fourthvertofface.x;
                                                            swapy = fourthvertofface.y;
                                                            swapz = fourthvertofface.z;

                                                            /*fourthvertofface.y = swapz;
                                                            fourthvertofface.z = swapy;*/


                                                            /*fourthvertofface.x = swapy;
                                                            fourthvertofface.y = swapx;*/
                                                            /*
                                                            int indexofvert0 = vertices.Count;
                                                            Vector3 firstvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                                            int indexofvert1 = vertices.Count + 1;
                                                            Vector3 secondvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                                            int indexofvert2 = vertices.Count + 2;
                                                            Vector3 thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                                            int indexofvert3 = vertices.Count + 3;
                                                            Vector3 fourthvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte),mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                                            */



                                                            vertices.Add(firstvertofface);
                                                            vertices.Add(secondvertofface);
                                                            vertices.Add(thirdvertofface);
                                                            vertices.Add(fourthvertofface);

                                                            triangles.Add(indexofvert2);
                                                            triangles.Add(indexofvert1);
                                                            triangles.Add(indexofvert0);
                                                            triangles.Add(indexofvert1);
                                                            triangles.Add(indexofvert2);
                                                            triangles.Add(indexofvert3);
                                                        }
                                                        else if (f == 4)
                                                        {

                                                            int indexofvert0 = vertices.Count;
                                                            firstvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                                            float swapx = firstvertofface.x;
                                                            float swapy = firstvertofface.y;
                                                            float swapz = firstvertofface.z;

                                                            //firstvertofface.x = swapy;
                                                            //firstvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte;

                                                            /*firstvertofface.z -= (1 * 1.0f);
                                                            firstvertofface.y -= (1 * 1.0f);*/
                                                            //firstvertofface.z = swapy;
                                                            //firstvertofface.y = swapz;

                                                            int indexofvert1 = vertices.Count + 1;
                                                            secondvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);
                                                            swapx = secondvertofface.x;
                                                            swapy = secondvertofface.y;
                                                            swapz = secondvertofface.z;

                                                            /*secondvertofface.x = swapy;
                                                            secondvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                                            /*secondvertofface.x -= (1 * 1.0f);
                                                            secondvertofface.y -= (1 * 1.0f);*/


                                                            int indexofvert2 = vertices.Count + 2;
                                                            thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                                            thirdvertofface.z -= (1 * 1.0f);
                                                            thirdvertofface.y -= (1 * 1.0f);
                                                            swapx = thirdvertofface.x;
                                                            swapy = thirdvertofface.y;
                                                            swapz = thirdvertofface.z;

                                                            /*thirdvertofface.z = swapy;
                                                            thirdvertofface.y = swapz;*/

                                                            int indexofvert3 = vertices.Count + 3;
                                                            fourthvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                                            fourthvertofface.z -= (1 * 1.0f);
                                                            fourthvertofface.y -= (1 * 1.0f);


                                                            swapx = fourthvertofface.x;
                                                            swapy = fourthvertofface.y;
                                                            swapz = fourthvertofface.z;

                                                            /*fourthvertofface.y = swapz;
                                                            fourthvertofface.z = swapy;*/


                                                            /*fourthvertofface.x = swapy;
                                                            fourthvertofface.y = swapx;*/
                                                            /*
                                                            int indexofvert0 = vertices.Count;
                                                            Vector3 firstvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                                            int indexofvert1 = vertices.Count + 1;
                                                            Vector3 secondvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                                            int indexofvert2 = vertices.Count + 2;
                                                            Vector3 thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                                            int indexofvert3 = vertices.Count + 3;
                                                            Vector3 fourthvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte),mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                                            */



                                                            vertices.Add(firstvertofface);
                                                            vertices.Add(secondvertofface);
                                                            vertices.Add(thirdvertofface);
                                                            vertices.Add(fourthvertofface);

                                                            triangles.Add(indexofvert0);
                                                            triangles.Add(indexofvert1);
                                                            triangles.Add(indexofvert2);
                                                            triangles.Add(indexofvert3);
                                                            triangles.Add(indexofvert2);
                                                            triangles.Add(indexofvert1);

                                                        }
                                                        else if (f == 5)
                                                        {
                                                            int indexofvert0 = vertices.Count;



                                                            firstvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                                            float swapx = firstvertofface.x;
                                                            float swapy = firstvertofface.y;
                                                            float swapz = firstvertofface.z;

                                                            firstvertofface.y -= (1 * 1.0f);

                                                            //firstvertofface.x = swapy;
                                                            //firstvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte;

                                                            /*firstvertofface.z -= (1 * 1.0f);
                                                            firstvertofface.y -= (1 * 1.0f);*/
                                                            //firstvertofface.z = swapy;
                                                            //firstvertofface.y = swapz;


                                                            int indexofvert1 = vertices.Count + 1;
                                                            secondvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);
                                                            swapx = secondvertofface.x;
                                                            swapy = secondvertofface.y;
                                                            swapz = secondvertofface.z;

                                                            secondvertofface.y -= (1 * 1.0f);
                                                            /*secondvertofface.x = swapy;
                                                            secondvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                                            /*secondvertofface.x -= (1 * 1.0f);
                                                            secondvertofface.y -= (1 * 1.0f);*/

                                                            int indexofvert2 = vertices.Count + 2;
                                                            thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                                            //thirdvertofface.z -= (1 * 1.0f);
                                                            thirdvertofface.y -= (1 * 1.0f);
                                                            swapx = thirdvertofface.x;
                                                            swapy = thirdvertofface.y;
                                                            swapz = thirdvertofface.z;

                                                            /*thirdvertofface.z = swapy;
                                                            thirdvertofface.y = swapz;*/

                                                            int indexofvert3 = vertices.Count + 3;
                                                            fourthvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                                            //fourthvertofface.z -= (1 * 1.0f);
                                                            fourthvertofface.y -= (1 * 1.0f);


                                                            swapx = fourthvertofface.x;
                                                            swapy = fourthvertofface.y;
                                                            swapz = fourthvertofface.z;


                                                            vertices.Add(firstvertofface);
                                                            vertices.Add(secondvertofface);
                                                            vertices.Add(thirdvertofface);
                                                            vertices.Add(fourthvertofface);

                                                            triangles.Add(indexofvert0);
                                                            triangles.Add(indexofvert1);
                                                            triangles.Add(indexofvert2);
                                                            triangles.Add(indexofvert3);
                                                            triangles.Add(indexofvert2);
                                                            triangles.Add(indexofvert1);
                                                        }


                                                        //Instantiate(visualobject0, firstvertofface * 0.1f, Quaternion.identity);
                                                        //Instantiate(visualobject1, secondvertofface * 0.1f, Quaternion.identity);
                                                        //Instantiate(visualobject2, thirdvertofface * 0.1f, Quaternion.identity);
                                                        //Instantiate(visualobject3, fourthvertofface * 0.1f, Quaternion.identity);













                                                        /*
                                                        GameObject vert0 = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);

                                                        vert0.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);

                                                        vert0.transform.position = firstvertofface * 0.1f;
                                                        vert0.transform.position += chunkmainpos;
                                                        vert0.transform.localScale = new Vector3(0.085f, 0.05f, 0.05f);
                                                        vert0.transform.rotation = Quaternion.identity;
                                                        //vert0.transform.gameObject.GetComponent<MeshFilter>()
                                                        //vert0.transform.gameObject.AddComponent<MeshRenderer>().material = new Material();


                                                        GameObject vert1 = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
                                                        vert1.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);

                                                        vert1.transform.position = secondvertofface * 0.1f;
                                                        vert1.transform.position += chunkmainpos;
                                                        vert1.transform.localScale = new Vector3(0.065f, 0.075f, 0.065f);
                                                        vert1.transform.rotation = Quaternion.identity;


                                                        GameObject vert2 = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
                                                        vert2.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.yellow);

                                                        vert2.transform.position = thirdvertofface * 0.1f;
                                                        vert2.transform.position += chunkmainpos;
                                                        vert2.transform.localScale = new Vector3(0.05f, 0.05f, 0.1f);
                                                        vert2.transform.rotation = Quaternion.identity;


                                                        GameObject vert3 = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
                                                        vert3.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);

                                                        vert3.transform.position = fourthvertofface * 0.1f;
                                                        vert3.transform.position += chunkmainpos;
                                                        vert3.transform.localScale = new Vector3(0.05f, 0.085f, 0.05f);
                                                        vert3.transform.rotation = Quaternion.identity;*/

                                                    }
                                                }
                                            }
                                        }
                                    }




                                    //GameObject emptyobject = new GameObject();
                                    //var meshfilt = thecurrentchunk.AddComponent<MeshFilter>();
                                    //var meshrend = thecurrentchunk.AddComponent<MeshRenderer>();
                                    /*
                                    Mesh thenewmesh = new Mesh();
                                    thenewmesh.vertices = vertices.ToArray();
                                    thenewmesh.triangles = triangles.ToArray();

                                    thecurrentchunk.GetComponent<MeshFilter>().mesh = thenewmesh;
                                    //_testChunk.GetComponent<MeshRenderer>().material = _mat;

                                    thecurrentchunk.transform.position = chunkmainpos;
                                    thecurrentchunk.transform.rotation = Quaternion.identity;

                                    thecurrentchunk.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                    //thecurrentchunk.GetComponent<MeshRenderer>().material = mat;

                                    //thecurrentchunk.transform.parent = emptyobjectparent0.transform;
                                    thecurrentchunk.gameObject.name = "faces type:" + f;*/


                                }

                            }

















































                            /*maps0buffer.Release();
                            mapvertlocbufferx.Release();
                            mapvertlocbuffery.Release();
                            mapvertlocbufferz.Release();
                            mapwidthdimtop.Release();
                            mapheightdimtop.Release();
                            mapdepthdimtop.Release();



                            maps0buffer.Dispose();
                            mapvertlocbufferx.Dispose();
                            mapvertlocbuffery.Dispose();
                            mapvertlocbufferz.Dispose();
                            mapwidthdimtop.Dispose();
                            mapheightdimtop.Dispose();
                            mapdepthdimtop.Dispose();*/
                            // _tempChunkArraybuffer.Dispose();







                            /*
                            GameObject emptyobject1 = new GameObject();

                            //chunko thechunk = new chunko();

                            emptyobject1.AddComponent<singleChunk>();

                            singleChunk thechunk = emptyobject1.GetComponent<singleChunk>();
                            thechunk.width = mapx;
                            thechunk.height = mapy;
                            thechunk.depth = mapz;

                            thechunk.widthflat = mapx;
                            thechunk.heightflat = mapy;
                            thechunk.depthflat = mapz;

                            thechunk.vertexlistWidth = thechunk.width + 1;
                            thechunk.vertexlistHeight = thechunk.height + 1;
                            thechunk.vertexlistDepth = thechunk.depth + 1;

                            thechunk.createvars();
                            thechunk.map = mapint;
                            thechunk._mat = mat;
                            thechunk._testChunk = emptyobject1;
                            thechunk.startthegen(chunkmainpos);

                            emptyobject1.transform.position = new Vector3(0, -0.01f, 0);
                            emptyobject1.transform.position += chunkmainpos;
                            emptyobject1.transform.rotation = Quaternion.identity;
                            emptyobject1.transform.parent = emptyobjectparent1.transform;*/




                        }
                    }
                }
            }
        }

        return mainChunk;
    }






    public static universemapvert.testerOfNumber.mainChunk CSCreateMapBuffer(universemapvert.testerOfNumber.mainChunk mainChunk) //sccsproceduralplanetbuilderrev4.mainChunk mainChunk
    {
        int mindex = 0;
        //int[] mapint;// = new int[mapx * mapy * mapz];


        int thebytesize = sizeof(int) * 4;
        int vector3Size = sizeof(float) * 3;
        int totalSize = thebytesize + vector3Size;

        if (universemapvert.mapsbuffer != null)
        {
            /*mainChunk.mapsbuffer.Release();
            mainChunk.mapsbuffer.Dispose();

            mainChunk.mapsbuffer = new ComputeBuffer(mainChunk.mapdata[mindex].Length, totalSize);
            */

        }
        else
        {

            universemapvert.mapsbuffer = new ComputeBuffer(mainChunk.mapdata[mindex].Length, totalSize);

        }
        

        if (shaderinit == 0)
        {
            computeShaderForMap = universemapvert.sccsmapallcompute;

            //computeShaderForMap = (ComputeShader)Resources.Load("Compute/sccsmapallcompute");//ComputeShader.Find("Transparent/Diffuse");

            shaderinit = 1;
        }

        return mainChunk;
    }



    public static universemapvert.testerOfNumber.mainChunk CSWorkOnMapOnly(universemapvert.testerOfNumber.mainChunk mainChunk) //sccsproceduralplanetbuilderrev4.mainChunk mainChunk
    {
        int mindex = 0;
        //int[] mapint;//

        /*int mindex = 0;
        int[] mapint;// = new int[mapx * mapy * mapz];


        int thebytesize = sizeof(int) * 4;
        int vector3Size = sizeof(float) * 3;
        int totalSize = thebytesize + vector3Size;


        if (shaderinit == 0)
        {
            mapsbuffer = new ComputeBuffer(mapdata[mindex].Length, totalSize);



            computeShaderForMap = (ComputeShader)Resources.Load("Compute/sccsmapallcompute");//ComputeShader.Find("Transparent/Diffuse");
            
            shaderinit = 1;
        }*/






        if (mainChunk.swtc == 0)
        {

            universemapvert.mapsbuffer.SetCounterValue(0);
            universemapvert.mapsbuffer.SetData(mainChunk.mapdata[mindex]);



            if (computeShaderForMap == null)
            {
                Debug.Log("computeShaderForMap null");
            }
            else
            {
                //Debug.Log("computeShaderForMap !null");

            }

            computeShaderForMap.SetBuffer(0, "themap", universemapvert.mapsbuffer);
            computeShaderForMap.Dispatch(0, (mapx * mapy * mapz) / 10, 1, 1);

            universemapvert.mapsbuffer.GetData(mainChunk.mapdata[mindex]);

            mainChunk.map = new int[mapx * mapy * mapz];
            for (int x = 0; x < mapx; x++)
            {
                for (int y = 0; y < mapy; y++)
                {
                    for (int z = 0; z < mapz; z++)
                    {
                        int index = x + mapx * (y + mapy * z);

                        mainChunk.map[index] = mainChunk.mapdata[mindex][index].thebyte;

                        //Debug.Log("map:" + data[index].thebyte);
                    }
                }
            }
            mainChunk.swtc = 1;

            /*
            mainChunk.mapsbuffer.SetCounterValue(0);
            mainChunk.mapsbuffer.SetData(mainChunk.mapdata[mindex]);



            if (computeShaderForMap == null)
            {
                Debug.Log("computeShaderForMap null");
            }
            else
            {
                //Debug.Log("computeShaderForMap !null");

            }

            computeShaderForMap.SetBuffer(0, "themap", mainChunk.mapsbuffer);
            computeShaderForMap.Dispatch(0, (mapx * mapy * mapz) / 10, 1, 1);

            mainChunk.mapsbuffer.GetData(mainChunk.mapdata[mindex]);

            mainChunk.map = new int[mapx * mapy * mapz];
            for (int x = 0; x < mapx; x++)
            {
                for (int y = 0; y < mapy; y++)
                {
                    for (int z = 0; z < mapz; z++)
                    {
                        int index = x + mapx * (y + mapy * z);

                        mainChunk.map[index] = mainChunk.mapdata[mindex][index].thebyte;

                        //Debug.Log("map:" + data[index].thebyte);
                    }
                }
            }
            mainChunk.swtc = 1;*/
        }

        /*mainChunk.mapsbuffer.Release();
        mainChunk.mapsbuffer.Dispose();*/

        return mainChunk;
    }

















    static int matswtc = 0;
    public static universemapvert.testerOfNumber.mainChunk workontherest(Vector3 chunkpos, GameObject thecurrentchunk, out List<Vector3> vertices, out List<int> triangles, int mx, int my, int mz, universemapvert.testerOfNumber.mainChunk mainChunk)
    {
        if (matswtc == 0)
        {
            mat = (Material)Resources.Load("Materials/New Material 1");//ComputeShader.Find("Transparent/Diffuse");

            matswtc = 1;
        }

        vertices = new List<Vector3>();
        triangles = new List<int>();

        thebytesize = sizeof(int) * 4;
        vector3Size = sizeof(float) * 3;
        totalSize = thebytesize + vector3Size;
        int mindex = 0;
        /*int mx = 0;
        int my = 0;
        int mz = 0;*/

        Vector3 chunkmainpos = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f) + mainChunk.smallChunkPos;




        //if (f == 0 && mindex == 0)
        {


            /*int thebytesize = sizeof(int) * 4;
            int vector3Size = sizeof(float) * 3;
            int totalSize = thebytesize + vector3Size;

            ComputeBuffer mapsbuffer = new ComputeBuffer(mapdata[mindex].Length, totalSize);

            mapsbuffer.SetData(mapdata[mindex]);


            if (computeShaderForMap == null)
            {

            }

            computeShaderForMap = (ComputeShader)Resources.Load("Compute/sccsmap");//ComputeShader.Find("Transparent/Diffuse");
            */
            /*


            computeShaderForMap.SetBuffer(0, "themap", mapsbuffer);
            computeShaderForMap.Dispatch(0, mapdata[mindex].Length / 10, 1, 1);

            mapsbuffer.GetData(mapdata[mindex]);

            mapint = new int[mapx * mapy * mapz];
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
            mapsbuffer.Dispose();*/




            /*maps0buffer.SetCounterValue(0);
            mapvertlocbufferx.SetCounterValue(0);
            mapvertlocbuffery.SetCounterValue(0);
            mapvertlocbufferz.SetCounterValue(0);

            mapwidthdimtop.SetCounterValue(0);
            mapheightdimtop.SetCounterValue(0);
            mapdepthdimtop.SetCounterValue(0);*/


            /*
            if (maps0buffer != null)
            {
                maps0buffer.Release();
                maps0buffer.Dispose();

            }
            if (mapvertlocbufferx != null)
            {
                mapvertlocbufferx.Release();
                mapvertlocbufferx.Dispose();

            }

            if (mapvertlocbuffery != null)
            {
                mapvertlocbuffery.Release();
                mapvertlocbuffery.Dispose();

            }

            if (mapvertlocbufferz != null)
            {
                mapvertlocbufferz.Release();
                mapvertlocbufferz.Dispose();

            }
            if (mapwidthdimtop != null)
            {
                mapwidthdimtop.Release();
                mapwidthdimtop.Dispose();

            }
            if (mapheightdimtop != null)
            {
                mapheightdimtop.Release();
                mapheightdimtop.Dispose();

            }
            if (mapdepthdimtop != null)
            {
                mapdepthdimtop.Release();
                mapdepthdimtop.Dispose();
            }


            maps0buffer = new ComputeBuffer(mapdata[mindex].Length, totalSize);
            maps0buffer.SetData(mapdata[mindex]);

            mapvertlocbufferx = new ComputeBuffer(datamapfirstvertxtop[mindex].Length, 4);
            mapvertlocbufferx.SetData(datamapfirstvertxtop[mindex]);

            mapvertlocbuffery = new ComputeBuffer(datamapfirstvertytop[mindex].Length, 4);
            mapvertlocbuffery.SetData(datamapfirstvertytop[mindex]);

            mapvertlocbufferz = new ComputeBuffer(datamapfirstvertztop[mindex].Length, 4);
            mapvertlocbufferz.SetData(datamapfirstvertztop[mindex]);

            mapwidthdimtop = new ComputeBuffer(datawidthdimtop[mindex].Length, 4);
            mapwidthdimtop.SetData(datawidthdimtop[mindex]);

            mapheightdimtop = new ComputeBuffer(dataheightdimtop[mindex].Length, 4);
            mapheightdimtop.SetData(dataheightdimtop[mindex]);

            mapdepthdimtop = new ComputeBuffer(datadepthdimtop[mindex].Length, 4);
            mapdepthdimtop.SetData(datadepthdimtop[mindex]);*/
        }
        //else
        {
            /*int thebytesize = sizeof(int) * 4;
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
            */


            /*
            //maps0buffer.SetCounterValue(0);
            mapvertlocbufferx.SetCounterValue(0);
            mapvertlocbuffery.SetCounterValue(0);
            mapvertlocbufferz.SetCounterValue(0);

            mapwidthdimtop.SetCounterValue(0);
            mapheightdimtop.SetCounterValue(0);
            mapdepthdimtop.SetCounterValue(0);


            maps0buffer.SetData(mapdata[mindex]);
            mapvertlocbufferx.SetData(datamapfirstvertxtop[mindex]);
            mapvertlocbuffery.SetData(datamapfirstvertytop[mindex]);
            mapvertlocbufferz.SetData(datamapfirstvertztop[mindex]);
            mapwidthdimtop.SetData(datawidthdimtop[mindex]);
            mapheightdimtop.SetData(dataheightdimtop[mindex]);
            mapdepthdimtop.SetData(datadepthdimtop[mindex]);

            */


        }


        if (mainChunk.maps0buffer != null)
        {
            mainChunk.maps0buffer.Release();
            mainChunk.maps0buffer.Dispose();
        }


       
        /*
        if (mainChunk.mapvertlocbufferx != null)
        {
            mainChunk.mapvertlocbufferx.Release();
            mainChunk.mapvertlocbufferx.Dispose();

        }

        if (mainChunk.mapvertlocbuffery != null)
        {
            mainChunk.mapvertlocbuffery.Release();
            mainChunk.mapvertlocbuffery.Dispose();

        }

        if (mainChunk.mapvertlocbufferz != null)
        {
            mainChunk.mapvertlocbufferz.Release();
            mainChunk.mapvertlocbufferz.Dispose();

        }
        if (mainChunk.mapwidthdimtop != null)
        {
            mainChunk.mapwidthdimtop.Release();
            mainChunk.mapwidthdimtop.Dispose();

        }
        if (mainChunk.mapheightdimtop != null)
        {
            mainChunk.mapheightdimtop.Release();
            mainChunk.mapheightdimtop.Dispose();

        }
        if (mainChunk.mapdepthdimtop != null)
        {
            mainChunk.mapdepthdimtop.Release();
            mainChunk.mapdepthdimtop.Dispose();
        }*/
        



        if (mainChunk.maps0buffer == null)
        {
            mainChunk.maps0buffer = new ComputeBuffer(mainChunk.mapdata[mindex].Length, totalSize);
            mainChunk.maps0buffer.SetData(mainChunk.mapdata[mindex]);

        }
        if (mainChunk.mapvertlocbufferx == null)
        {
            mainChunk.mapvertlocbufferx = new ComputeBuffer(mainChunk.datamapfirstvertxtop[mindex].Length, 4);
            mainChunk.mapvertlocbufferx.SetData(mainChunk.datamapfirstvertxtop[mindex]);

        }
        if (mainChunk.mapvertlocbuffery == null)
        {

            mainChunk.mapvertlocbuffery = new ComputeBuffer(mainChunk.datamapfirstvertytop[mindex].Length, 4);
            mainChunk.mapvertlocbuffery.SetData(mainChunk.datamapfirstvertytop[mindex]);

        }
        if (mainChunk.mapvertlocbufferz == null)
        {

            mainChunk.mapvertlocbufferz = new ComputeBuffer(mainChunk.datamapfirstvertztop[mindex].Length, 4);
            mainChunk.mapvertlocbufferz.SetData(mainChunk.datamapfirstvertztop[mindex]);

        }
        if (mainChunk.mapwidthdimtop == null)
        {
            mainChunk.mapwidthdimtop = new ComputeBuffer(mainChunk.datawidthdimtop[mindex].Length, 4);
            mainChunk.mapwidthdimtop.SetData(mainChunk.datawidthdimtop[mindex]);

        }
        if (mainChunk.mapheightdimtop == null)
        {
            mainChunk.mapheightdimtop = new ComputeBuffer(mainChunk.dataheightdimtop[mindex].Length, 4);
            mainChunk.mapheightdimtop.SetData(mainChunk.dataheightdimtop[mindex]);

        }
        if (mainChunk.mapdepthdimtop == null)
        {
            mainChunk.mapdepthdimtop = new ComputeBuffer(mainChunk.datadepthdimtop[mindex].Length, 4);
            mainChunk.mapdepthdimtop.SetData(mainChunk.datadepthdimtop[mindex]);

        }



        //mainChunk.maps0buffer.SetCounterValue(0);
        /*mainChunk.mapvertlocbufferx.SetCounterValue(0);
        mainChunk.mapvertlocbuffery.SetCounterValue(0);
        mainChunk.mapvertlocbufferz.SetCounterValue(0);
        mainChunk.mapwidthdimtop.SetCounterValue(0);
        mainChunk.mapheightdimtop.SetCounterValue(0);
        mainChunk.mapdepthdimtop.SetCounterValue(0);
        */
        mainChunk.maps0buffer.SetCounterValue(0);
        mainChunk.mapvertlocbufferx.SetCounterValue(0);
        mainChunk.mapvertlocbuffery.SetCounterValue(0);
        mainChunk.mapvertlocbufferz.SetCounterValue(0);
        mainChunk.mapwidthdimtop.SetCounterValue(0);
        mainChunk.mapheightdimtop.SetCounterValue(0);
        mainChunk.mapdepthdimtop.SetCounterValue(0);

        for (int f = 0; f < numberoffaces; f++)
        {
            /*if (mainChunk.maps0buffer != null)
            {
                mainChunk.maps0buffer.Release();
                mainChunk.maps0buffer.Dispose();
            }


            if (mainChunk.maps0buffer == null)
            {
                mainChunk.maps0buffer = new ComputeBuffer(mainChunk.mapdata[mindex].Length, totalSize);
                mainChunk.maps0buffer.SetData(mainChunk.mapdata[mindex]);

            }*/
            //mainChunk.maps0buffer.SetCounterValue(0);
            /*mainChunk.mapvertlocbufferx.SetCounterValue(0);
            mainChunk.mapvertlocbuffery.SetCounterValue(0);
            mainChunk.mapvertlocbufferz.SetCounterValue(0);
            mainChunk.mapwidthdimtop.SetCounterValue(0);
            mainChunk.mapheightdimtop.SetCounterValue(0);
            mainChunk.mapdepthdimtop.SetCounterValue(0);*/


            /*GameObject emptyobjectparent0 = new GameObject();

            emptyobjectparent0.transform.name = "chunkfacetype-" + f;
            emptyobjectparent0.transform.position = chunkmainpos;
            emptyobjectparent0.transform.parent = thecurrentchunk.transform.parent;
            */


            /*
            mainChunk.maps0buffer.SetData(mainChunk.mapdata[mindex]);

            
            mainChunk.mapvertlocbufferx.SetData(mainChunk.datamapfirstvertxtop[mindex]);
            mainChunk.mapvertlocbuffery.SetData(mainChunk.datamapfirstvertytop[mindex]);
            mainChunk.mapvertlocbufferz.SetData(mainChunk.datamapfirstvertztop[mindex]);

            mainChunk.mapwidthdimtop.SetData(mainChunk.datawidthdimtop[mindex]);
            mainChunk.mapheightdimtop.SetData(mainChunk.dataheightdimtop[mindex]);
            mainChunk.mapdepthdimtop.SetData(mainChunk.datadepthdimtop[mindex]);*/



            
            /*if (computeVertexesALLFACES != null)
            {
                GC.SuppressFinalize(computeVertexesALLFACES);
            }*/

            if (f == 0) //&& computeVertexesALLFACES == null
            {
                computeVertexesALLFACES = universemapvert.computevertexTOP;//(ComputeShader)Resources.Load("Compute/computevertexTOP");//ComputeShader.Find("Transparent/Diffuse");
            }
            if (f == 1)// && computeVertexesALLFACES == null)
            {
                computeVertexesALLFACES = universemapvert.computevertexLEFT;// (ComputeShader)Resources.Load("Compute/computevertexLEFT");//ComputeShader.Find("Transparent/Diffuse");
            }
            if (f == 2)// && computeVertexesALLFACES == null)
            {
                computeVertexesALLFACES = universemapvert.computevertexRIGHT;//(ComputeShader)Resources.Load("Compute/computevertexRIGHT");//ComputeShader.Find("Transparent/Diffuse");
            }

            if (f == 3)// && computeVertexesALLFACES == null)
            {
                computeVertexesALLFACES = universemapvert.computevertexFRONT;//(ComputeShader)Resources.Load("Compute/computevertexFRONT");//ComputeShader.Find("Transparent/Diffuse");
            }
            if (f == 4)// && computeVertexesALLFACES == null)
            {
                computeVertexesALLFACES = universemapvert.computevertexBACK;//(ComputeShader)Resources.Load("Compute/computevertexBACK");//ComputeShader.Find("Transparent/Diffuse");
            }
            if (f == 5)// && computeVertexesALLFACES == null)
            {
                computeVertexesALLFACES = universemapvert.computevertexBOTTOM;// (ComputeShader)Resources.Load("Compute/computevertexBOTTOM");//ComputeShader.Find("Transparent/Diffuse");
            }
            

            /*
            if (f == 0) //&& computeVertexesALLFACES == null
            {
                if (swapcomputetop == null)
                {
                    computeVertexesALLFACES = universemapvert.computevertexTOP;

                    //computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexTOP");//ComputeShader.Find("Transparent/Diffuse");
                    swapcomputetop = computeVertexesALLFACES;
                }
                else
                {
                    computeVertexesALLFACES = swapcomputetop;
                }
            }
            else
            {
                if (swapcomputetop != null)
                {
                    computeVertexesALLFACES = swapcomputetop;
                    //swapcompute = 
                }
            }



            if (f == 1)// && computeVertexesALLFACES == null)
            {
                if (swapcomputeleft == null)
                {
                    computeVertexesALLFACES = universemapvert.computevertexLEFT;
                    //computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexLEFT");//ComputeShader.Find("Transparent/Diffuse");
                    swapcomputeleft = computeVertexesALLFACES;
                }
                else
                {
                    computeVertexesALLFACES = swapcomputeleft;
                }


            }
            else
            {
                if (swapcomputeleft != null)
                {
                    computeVertexesALLFACES = swapcomputeleft;
                    //swapcompute = 
                }
            }




            if (f == 2)// && computeVertexesALLFACES == null)
            {
                if (swapcomputeright == null)
                {
                    computeVertexesALLFACES = universemapvert.computevertexRIGHT;
                    //computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexRIGHT");//ComputeShader.Find("Transparent/Diffuse");
                    swapcomputeright = computeVertexesALLFACES;
                }
                else
                {
                    computeVertexesALLFACES = swapcomputeright;
                }

            }
            else
            {
                if (swapcomputeright != null)
                {
                    computeVertexesALLFACES = swapcomputeright;
                    //swapcompute = 
                }
            }




            if (f == 3)// && computeVertexesALLFACES == null)
            {
                if (swapcomputefront == null)
                {
                    computeVertexesALLFACES = universemapvert.computevertexFRONT;

                    //computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexFRONT");//ComputeShader.Find("Transparent/Diffuse");
                    swapcomputefront = computeVertexesALLFACES;
                }
                else
                {
                    computeVertexesALLFACES = swapcomputefront;
                }


            }
            else
            {
                if (swapcomputefront != null)
                {
                    computeVertexesALLFACES = swapcomputefront;
                    //swapcompute = 
                }
            }

            if (f == 4)// && computeVertexesALLFACES == null)
            {
                if (swapcomputeback == null)
                {
                    computeVertexesALLFACES = universemapvert.computevertexBACK;

                    //computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexBACK");//ComputeShader.Find("Transparent/Diffuse");
                    swapcomputeback = computeVertexesALLFACES;
                }
                else
                {
                    computeVertexesALLFACES = swapcomputeback;
                }


            }
            else
            {
                if (swapcomputeback != null)
                {
                    computeVertexesALLFACES = swapcomputeback;
                    //swapcompute = 
                }
            }
            if (f == 5)// && computeVertexesALLFACES == null)
            {
                if (swapcomputebottom == null)
                {
                    computeVertexesALLFACES = universemapvert.computevertexBOTTOM;

                    //computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexBOTTOM");//ComputeShader.Find("Transparent/Diffuse");
                    swapcomputebottom = computeVertexesALLFACES;
                }
                else
                {
                    computeVertexesALLFACES = swapcomputebottom;
                }

            }
            else
            {
                if (swapcomputebottom != null)
                {
                    computeVertexesALLFACES = swapcomputebottom;
                    //swapcompute = 
                }
            }

            */



            if (f == 0)
            {


                //computeVertexesALLFACES = null;
                /*if (computeVertexesALLFACES != null)
                {
                    GC.SuppressFinalize(computeVertexesALLFACES);
                }*/


                /*if (maps0buffer != null)
                {
                    maps0buffer.Release();
                    maps0buffer.Dispose();

                }
                if (mapvertlocbufferx != null)
                {
                    mapvertlocbufferx.Release();
                    mapvertlocbufferx.Dispose();

                }

                if (mapvertlocbuffery != null)
                {
                    mapvertlocbuffery.Release();
                    mapvertlocbuffery.Dispose();

                }

                if (mapvertlocbufferz != null)
                {
                    mapvertlocbufferz.Release();
                    mapvertlocbufferz.Dispose();

                }
                if (mapwidthdimtop != null)
                {
                    mapwidthdimtop.Release();
                    mapwidthdimtop.Dispose();

                }
                if (mapheightdimtop != null)
                {
                    mapheightdimtop.Release();
                    mapheightdimtop.Dispose();

                }
                if (mapdepthdimtop != null)
                {
                    mapdepthdimtop.Release();
                    mapdepthdimtop.Dispose();
                }*/


                /*
                if (mainChunk.maps0buffer != null)
                {
                    mainChunk.maps0buffer.Release();
                    mainChunk.maps0buffer.Dispose();
                }
                if (mainChunk.mapvertlocbufferx != null)
                {
                    mainChunk.mapvertlocbufferx.Release();
                    mainChunk.mapvertlocbufferx.Dispose();

                }

                if (mainChunk.mapvertlocbuffery != null)
                {
                    mainChunk.mapvertlocbuffery.Release();
                    mainChunk.mapvertlocbuffery.Dispose();

                }

                if (mainChunk.mapvertlocbufferz != null)
                {
                    mainChunk.mapvertlocbufferz.Release();
                    mainChunk.mapvertlocbufferz.Dispose();

                }
                if (mainChunk.mapwidthdimtop != null)
                {
                    mainChunk.mapwidthdimtop.Release();
                    mainChunk.mapwidthdimtop.Dispose();

                }
                if (mainChunk.mapheightdimtop != null)
                {
                    mainChunk.mapheightdimtop.Release();
                    mainChunk.mapheightdimtop.Dispose();

                }
                if (mainChunk.mapdepthdimtop != null)
                {
                    mainChunk.mapdepthdimtop.Release();
                    mainChunk.mapdepthdimtop.Dispose();
                }*/







                /*


                maps0buffer = new ComputeBuffer(mapdata[mindex].Length, totalSize);
                maps0buffer.SetData(mapdata[mindex]);

                mapvertlocbufferx = new ComputeBuffer(datamapfirstvertxtop[mindex].Length, 4);
                mapvertlocbufferx.SetData(datamapfirstvertxtop[mindex]);

                mapvertlocbuffery = new ComputeBuffer(datamapfirstvertytop[mindex].Length, 4);
                mapvertlocbuffery.SetData(datamapfirstvertytop[mindex]);

                mapvertlocbufferz = new ComputeBuffer(datamapfirstvertztop[mindex].Length, 4);
                mapvertlocbufferz.SetData(datamapfirstvertztop[mindex]);

                mapwidthdimtop = new ComputeBuffer(datawidthdimtop[mindex].Length, 4);
                mapwidthdimtop.SetData(datawidthdimtop[mindex]);

                mapheightdimtop = new ComputeBuffer(dataheightdimtop[mindex].Length, 4);
                mapheightdimtop.SetData(dataheightdimtop[mindex]);

                mapdepthdimtop = new ComputeBuffer(datadepthdimtop[mindex].Length, 4);
                mapdepthdimtop.SetData(datadepthdimtop[mindex]);*/
            }
            else
            {




                /*mapvertlocbufferx.SetData(datamapfirstvertxtop[mindex]);
                mapvertlocbuffery.SetData(datamapfirstvertytop[mindex]);
                mapvertlocbufferz.SetData(datamapfirstvertztop[mindex]);
                mapwidthdimtop.SetData(datawidthdimtop[mindex]);
                mapheightdimtop.SetData(dataheightdimtop[mindex]);
                mapdepthdimtop.SetData(datadepthdimtop[mindex]);*/

            }





            int dotherest1 = 1;


            if (dotherest1 == 1)
            {




                if (reducedverttrigswtc == 0)
                {



                    /*// Create a material with transparent diffuse shader
                    Material material = new Material(Shader.Find("Transparent/Diffuse"));
                    material.color = Color.green;

                    // assign the material to the renderer
                    GetComponent<Renderer>().material = material;*/


                    /*
                    if (f == 0) //&& computeVertexesALLFACES == null
                    {
                        computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexTOP");//ComputeShader.Find("Transparent/Diffuse");
                    }
                    if (f == 1)// && computeVertexesALLFACES == null)
                    {
                        computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexLEFT");//ComputeShader.Find("Transparent/Diffuse");
                    }
                    if (f == 2)// && computeVertexesALLFACES == null)
                    {
                        computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexRIGHT");//ComputeShader.Find("Transparent/Diffuse");
                    }

                    if (f == 3)// && computeVertexesALLFACES == null)
                    {
                        computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexFRONT");//ComputeShader.Find("Transparent/Diffuse");
                    }
                    if (f == 4)// && computeVertexesALLFACES == null)
                    {
                        computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexBACK");//ComputeShader.Find("Transparent/Diffuse");
                    }
                    if (f == 5)// && computeVertexesALLFACES == null)
                    {
                        computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexBOTTOM");//ComputeShader.Find("Transparent/Diffuse");
                    }*/



















                    computeVertexesALLFACES.SetBuffer(0, "themap", mainChunk.maps0buffer);
                    computeVertexesALLFACES.SetBuffer(0, "mapfirstvertxtop", mainChunk.mapvertlocbufferx);
                    computeVertexesALLFACES.SetBuffer(0, "mapfirstvertytop", mainChunk.mapvertlocbuffery);
                    computeVertexesALLFACES.SetBuffer(0, "mapfirstvertztop", mainChunk.mapvertlocbufferz);

                    computeVertexesALLFACES.SetBuffer(0, "widthdimtop", mainChunk.mapwidthdimtop);
                    computeVertexesALLFACES.SetBuffer(0, "heightdimtop", mainChunk.mapheightdimtop);
                    computeVertexesALLFACES.SetBuffer(0, "depthdimtop", mainChunk.mapdepthdimtop);

                    computeVertexesALLFACES.Dispatch(0, threadmulx, threadmuly, threadmulz);









                }
                else if (reducedverttrigswtc == 1)
                {
                    /*
                    computeVertexestwo.SetBuffer(0, "themap", maps0buffer);
                    computeVertexestwo.SetBuffer(0, "mapfirstvertxtop", mapvertlocbufferx);
                    //computeVertexes.SetBuffer(0, "mapfirstvertytop", mapvertlocbuffery);
                    computeVertexestwo.SetBuffer(0, "mapfirstvertztop", mapvertlocbufferz);

                    computeVertexestwo.SetBuffer(0, "widthdimtop", mapwidthdimtop);
                    computeVertexestwo.SetBuffer(0, "heightdimtop", mapheightdimtop);
                    computeVertexestwo.SetBuffer(0, "depthdimtop", mapdepthdimtop);


                    computeVertexestwo.Dispatch(0, 1, 1, 1);*/


                }


                mainChunk.mapvertlocbufferx.GetData(mainChunk.datamapfirstvertxtop[mindex]);
                mainChunk.mapvertlocbuffery.GetData(mainChunk.datamapfirstvertytop[mindex]);
                mainChunk.mapvertlocbufferz.GetData(mainChunk.datamapfirstvertztop[mindex]);

                mainChunk.mapwidthdimtop.GetData(mainChunk.datawidthdimtop[mindex]);
                mainChunk.mapheightdimtop.GetData(mainChunk.dataheightdimtop[mindex]);
                mainChunk.mapdepthdimtop.GetData(mainChunk.datadepthdimtop[mindex]);






                vertices = new List<Vector3>();
                triangles = new List<int>();







                int dotheresttwo = 1;


                if (dotheresttwo == 1)
                {



                    for (int x = 0; x < mapx; x++)
                    {
                        for (int y = 0; y < mapy; y++)
                        {
                            for (int z = 0; z < mapz; z++)
                            {
                                int index = x + mapx * (y + mapy * z);

                                //mapint[index] = data[mindex][index].thebyte;

                                //Debug.Log("map:" + data[index].thebyte);



                                if (mainChunk.dataheightdimtop[mindex][index].thebyte == 0)//datamapfirstvertxtop[mindex][index].thebyte == 0 && datamapfirstvertytop[mindex][index].thebyte == 0 && datamapfirstvertztop[mindex][index].thebyte == 0)
                                {

                                }
                                else
                                {
                                    //if (mapint[index] == 1)//IsTransparent(x, y+1,z, originalmapdata[mindex]) && blockExistsInArray(x, y - 1 ,z) == 1)// mapint[index] == 1) //mapdata[mindex]
                                    {


                                        if (f == 0)
                                        {




                                            int indexofvert0 = vertices.Count;
                                            firstvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert1 = vertices.Count + 1;
                                            secondvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert2 = vertices.Count + 2;
                                            thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                            int indexofvert3 = vertices.Count + 3;
                                            fourthvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));


                                            vertices.Add(firstvertofface);
                                            vertices.Add(secondvertofface);
                                            vertices.Add(thirdvertofface);
                                            vertices.Add(fourthvertofface);

                                            triangles.Add(indexofvert2);
                                            triangles.Add(indexofvert1);
                                            triangles.Add(indexofvert0);
                                            triangles.Add(indexofvert1);
                                            triangles.Add(indexofvert2);
                                            triangles.Add(indexofvert3);
                                        }
                                        else if (f == 1)
                                        {

                                            int indexofvert0 = vertices.Count;
                                            firstvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                            float swapx = firstvertofface.x;
                                            float swapy = firstvertofface.y;
                                            float swapz = firstvertofface.z;

                                            //firstvertofface.x = swapy;
                                            //firstvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte;

                                            //firstvertofface.x -= (1 * 1.0f);
                                            //firstvertofface.y -= (1 * 1.0f);


                                            int indexofvert1 = vertices.Count + 1;
                                            secondvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);
                                            /*swapx = secondvertofface.x;
                                            swapy = secondvertofface.y;
                                            swapz = secondvertofface.z;

                                            secondvertofface.x = swapy;
                                            secondvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                            secondvertofface.x -= (1 * 1.0f);
                                            secondvertofface.y -= (1 * 1.0f);


                                            int indexofvert2 = vertices.Count + 2;
                                            thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                            //thirdvertofface.x -= (1 * 1.0f);
                                            //thirdvertofface.y -= (1 * 1.0f);
                                            /*swapx = thirdvertofface.x;
                                            swapy = thirdvertofface.y;
                                            swapz = thirdvertofface.z;

                                            thirdvertofface.x = swapy;
                                            thirdvertofface.y = swapx;*/

                                            int indexofvert3 = vertices.Count + 3;
                                            fourthvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                            fourthvertofface.x -= (1 * 1.0f);
                                            fourthvertofface.y -= (1 * 1.0f);


                                            /*swapx = fourthvertofface.x;
                                            swapy = fourthvertofface.y;
                                            swapz = fourthvertofface.z;

                                            fourthvertofface.x = swapy;
                                            fourthvertofface.y = swapx;*/
                                            /*
                                            int indexofvert0 = vertices.Count;
                                            Vector3 firstvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert1 = vertices.Count + 1;
                                            Vector3 secondvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert2 = vertices.Count + 2;
                                            Vector3 thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                            int indexofvert3 = vertices.Count + 3;
                                            Vector3 fourthvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte),mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                            */



                                            vertices.Add(firstvertofface);
                                            vertices.Add(secondvertofface);
                                            vertices.Add(thirdvertofface);
                                            vertices.Add(fourthvertofface);

                                            triangles.Add(indexofvert0);
                                            triangles.Add(indexofvert1);
                                            triangles.Add(indexofvert2);
                                            triangles.Add(indexofvert3);
                                            triangles.Add(indexofvert2);
                                            triangles.Add(indexofvert1);
                                        }
                                        else if (f == 2)
                                        {


                                            int indexofvert0 = vertices.Count;
                                            firstvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                            float swapx = firstvertofface.x;
                                            float swapy = firstvertofface.y;
                                            float swapz = firstvertofface.z;

                                            //firstvertofface.x = swapy;
                                            //firstvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte;

                                            //firstvertofface.x -= (1 * 1.0f);
                                            //firstvertofface.y -= (1 * 1.0f);


                                            int indexofvert1 = vertices.Count + 1;
                                            secondvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);
                                            /*swapx = secondvertofface.x;
                                            swapy = secondvertofface.y;
                                            swapz = secondvertofface.z;

                                            secondvertofface.x = swapy;
                                            secondvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                            secondvertofface.x -= (1 * 1.0f);
                                            secondvertofface.y -= (1 * 1.0f);


                                            int indexofvert2 = vertices.Count + 2;
                                            thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                            //thirdvertofface.x -= (1 * 1.0f);
                                            //thirdvertofface.y -= (1 * 1.0f);
                                            /*swapx = thirdvertofface.x;
                                            swapy = thirdvertofface.y;
                                            swapz = thirdvertofface.z;

                                            thirdvertofface.x = swapy;
                                            thirdvertofface.y = swapx;*/

                                            int indexofvert3 = vertices.Count + 3;
                                            fourthvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                            fourthvertofface.x -= (1 * 1.0f);
                                            fourthvertofface.y -= (1 * 1.0f);


                                            /*swapx = fourthvertofface.x;
                                            swapy = fourthvertofface.y;
                                            swapz = fourthvertofface.z;

                                            fourthvertofface.x = swapy;
                                            fourthvertofface.y = swapx;*/
                                            /*
                                            int indexofvert0 = vertices.Count;
                                            Vector3 firstvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert1 = vertices.Count + 1;
                                            Vector3 secondvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert2 = vertices.Count + 2;
                                            Vector3 thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                            int indexofvert3 = vertices.Count + 3;
                                            Vector3 fourthvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte),mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                            */



                                            vertices.Add(firstvertofface);
                                            vertices.Add(secondvertofface);
                                            vertices.Add(thirdvertofface);
                                            vertices.Add(fourthvertofface);

                                            triangles.Add(indexofvert2);
                                            triangles.Add(indexofvert1);
                                            triangles.Add(indexofvert0);
                                            triangles.Add(indexofvert1);
                                            triangles.Add(indexofvert2);
                                            triangles.Add(indexofvert3);
                                        }
                                        else if (f == 3)
                                        {

                                            int indexofvert0 = vertices.Count;
                                            firstvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                            float swapx = firstvertofface.x;
                                            float swapy = firstvertofface.y;
                                            float swapz = firstvertofface.z;

                                            //firstvertofface.x = swapy;
                                            //firstvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte;

                                            /*firstvertofface.z -= (1 * 1.0f);
                                            firstvertofface.y -= (1 * 1.0f);*/
                                            //firstvertofface.z = swapy;
                                            //firstvertofface.y = swapz;

                                            int indexofvert1 = vertices.Count + 1;
                                            secondvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);
                                            swapx = secondvertofface.x;
                                            swapy = secondvertofface.y;
                                            swapz = secondvertofface.z;

                                            /*secondvertofface.x = swapy;
                                            secondvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                            /*secondvertofface.x -= (1 * 1.0f);
                                            secondvertofface.y -= (1 * 1.0f);*/


                                            int indexofvert2 = vertices.Count + 2;
                                            thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                            thirdvertofface.z -= (1 * 1.0f);
                                            thirdvertofface.y -= (1 * 1.0f);
                                            swapx = thirdvertofface.x;
                                            swapy = thirdvertofface.y;
                                            swapz = thirdvertofface.z;

                                            /*thirdvertofface.z = swapy;
                                            thirdvertofface.y = swapz;*/

                                            int indexofvert3 = vertices.Count + 3;
                                            fourthvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                            fourthvertofface.z -= (1 * 1.0f);
                                            fourthvertofface.y -= (1 * 1.0f);


                                            swapx = fourthvertofface.x;
                                            swapy = fourthvertofface.y;
                                            swapz = fourthvertofface.z;

                                            /*fourthvertofface.y = swapz;
                                            fourthvertofface.z = swapy;*/


                                            /*fourthvertofface.x = swapy;
                                            fourthvertofface.y = swapx;*/
                                            /*
                                            int indexofvert0 = vertices.Count;
                                            Vector3 firstvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert1 = vertices.Count + 1;
                                            Vector3 secondvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert2 = vertices.Count + 2;
                                            Vector3 thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                            int indexofvert3 = vertices.Count + 3;
                                            Vector3 fourthvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte),mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                            */



                                            vertices.Add(firstvertofface);
                                            vertices.Add(secondvertofface);
                                            vertices.Add(thirdvertofface);
                                            vertices.Add(fourthvertofface);

                                            triangles.Add(indexofvert2);
                                            triangles.Add(indexofvert1);
                                            triangles.Add(indexofvert0);
                                            triangles.Add(indexofvert1);
                                            triangles.Add(indexofvert2);
                                            triangles.Add(indexofvert3);
                                        }
                                        else if (f == 4)
                                        {

                                            int indexofvert0 = vertices.Count;
                                            firstvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                            float swapx = firstvertofface.x;
                                            float swapy = firstvertofface.y;
                                            float swapz = firstvertofface.z;

                                            //firstvertofface.x = swapy;
                                            //firstvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte;

                                            /*firstvertofface.z -= (1 * 1.0f);
                                            firstvertofface.y -= (1 * 1.0f);*/
                                            //firstvertofface.z = swapy;
                                            //firstvertofface.y = swapz;

                                            int indexofvert1 = vertices.Count + 1;
                                            secondvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);
                                            swapx = secondvertofface.x;
                                            swapy = secondvertofface.y;
                                            swapz = secondvertofface.z;

                                            /*secondvertofface.x = swapy;
                                            secondvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                            /*secondvertofface.x -= (1 * 1.0f);
                                            secondvertofface.y -= (1 * 1.0f);*/


                                            int indexofvert2 = vertices.Count + 2;
                                            thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                            thirdvertofface.z -= (1 * 1.0f);
                                            thirdvertofface.y -= (1 * 1.0f);
                                            swapx = thirdvertofface.x;
                                            swapy = thirdvertofface.y;
                                            swapz = thirdvertofface.z;

                                            /*thirdvertofface.z = swapy;
                                            thirdvertofface.y = swapz;*/

                                            int indexofvert3 = vertices.Count + 3;
                                            fourthvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                            fourthvertofface.z -= (1 * 1.0f);
                                            fourthvertofface.y -= (1 * 1.0f);


                                            swapx = fourthvertofface.x;
                                            swapy = fourthvertofface.y;
                                            swapz = fourthvertofface.z;

                                            /*fourthvertofface.y = swapz;
                                            fourthvertofface.z = swapy;*/


                                            /*fourthvertofface.x = swapy;
                                            fourthvertofface.y = swapx;*/
                                            /*
                                            int indexofvert0 = vertices.Count;
                                            Vector3 firstvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert1 = vertices.Count + 1;
                                            Vector3 secondvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert2 = vertices.Count + 2;
                                            Vector3 thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                            int indexofvert3 = vertices.Count + 3;
                                            Vector3 fourthvertofface = new Vector3(mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte),mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                            */



                                            vertices.Add(firstvertofface);
                                            vertices.Add(secondvertofface);
                                            vertices.Add(thirdvertofface);
                                            vertices.Add(fourthvertofface);

                                            triangles.Add(indexofvert0);
                                            triangles.Add(indexofvert1);
                                            triangles.Add(indexofvert2);
                                            triangles.Add(indexofvert3);
                                            triangles.Add(indexofvert2);
                                            triangles.Add(indexofvert1);

                                        }
                                        else if (f == 5)
                                        {
                                            int indexofvert0 = vertices.Count;



                                            firstvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);

                                            float swapx = firstvertofface.x;
                                            float swapy = firstvertofface.y;
                                            float swapz = firstvertofface.z;

                                            firstvertofface.y -= (1 * 1.0f);

                                            //firstvertofface.x = swapy;
                                            //firstvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte;

                                            /*firstvertofface.z -= (1 * 1.0f);
                                            firstvertofface.y -= (1 * 1.0f);*/
                                            //firstvertofface.z = swapy;
                                            //firstvertofface.y = swapz;


                                            int indexofvert1 = vertices.Count + 1;
                                            secondvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte);
                                            swapx = secondvertofface.x;
                                            swapy = secondvertofface.y;
                                            swapz = secondvertofface.z;

                                            secondvertofface.y -= (1 * 1.0f);
                                            /*secondvertofface.x = swapy;
                                            secondvertofface.y = mainChunk.datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                            /*secondvertofface.x -= (1 * 1.0f);
                                            secondvertofface.y -= (1 * 1.0f);*/

                                            int indexofvert2 = vertices.Count + 2;
                                            thirdvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte, mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));
                                            //thirdvertofface.z -= (1 * 1.0f);
                                            thirdvertofface.y -= (1 * 1.0f);
                                            swapx = thirdvertofface.x;
                                            swapy = thirdvertofface.y;
                                            swapz = thirdvertofface.z;

                                            /*thirdvertofface.z = swapy;
                                            thirdvertofface.y = swapz;*/

                                            int indexofvert3 = vertices.Count + 3;
                                            fourthvertofface = new Vector3(mainChunk.datamapfirstvertxtop[mindex][index].thebyte + (mainChunk.datawidthdimtop[mindex][index].thebyte), mainChunk.dataheightdimtop[mindex][index].thebyte, mainChunk.datamapfirstvertztop[mindex][index].thebyte + (mainChunk.datadepthdimtop[mindex][index].thebyte));

                                            //fourthvertofface.z -= (1 * 1.0f);
                                            fourthvertofface.y -= (1 * 1.0f);


                                            swapx = fourthvertofface.x;
                                            swapy = fourthvertofface.y;
                                            swapz = fourthvertofface.z;


                                            vertices.Add(firstvertofface);
                                            vertices.Add(secondvertofface);
                                            vertices.Add(thirdvertofface);
                                            vertices.Add(fourthvertofface);

                                            triangles.Add(indexofvert0);
                                            triangles.Add(indexofvert1);
                                            triangles.Add(indexofvert2);
                                            triangles.Add(indexofvert3);
                                            triangles.Add(indexofvert2);
                                            triangles.Add(indexofvert1);
                                        }
                                        //Instantiate(visualobject0, firstvertofface * 0.1f, Quaternion.identity);
                                        //Instantiate(visualobject1, secondvertofface * 0.1f, Quaternion.identity);
                                        //Instantiate(visualobject2, thirdvertofface * 0.1f, Quaternion.identity);
                                        //Instantiate(visualobject3, fourthvertofface * 0.1f, Quaternion.identity);













                                        /*
                                        GameObject vert0 = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);

                                        vert0.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.red);

                                        vert0.transform.position = firstvertofface * 0.1f;
                                        vert0.transform.position += chunkmainpos;
                                        vert0.transform.localScale = new Vector3(0.085f, 0.05f, 0.05f);
                                        vert0.transform.rotation = Quaternion.identity;
                                        //vert0.transform.gameObject.GetComponent<MeshFilter>()
                                        //vert0.transform.gameObject.AddComponent<MeshRenderer>().material = new Material();


                                        GameObject vert1 = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
                                        vert1.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.blue);

                                        vert1.transform.position = secondvertofface * 0.1f;
                                        vert1.transform.position += chunkmainpos;
                                        vert1.transform.localScale = new Vector3(0.065f, 0.075f, 0.065f);
                                        vert1.transform.rotation = Quaternion.identity;


                                        GameObject vert2 = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
                                        vert2.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.yellow);

                                        vert2.transform.position = thirdvertofface * 0.1f;
                                        vert2.transform.position += chunkmainpos;
                                        vert2.transform.localScale = new Vector3(0.05f, 0.05f, 0.1f);
                                        vert2.transform.rotation = Quaternion.identity;


                                        GameObject vert3 = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);
                                        vert3.GetComponent<MeshRenderer>().material.SetColor("_Color", Color.green);

                                        vert3.transform.position = fourthvertofface * 0.1f;
                                        vert3.transform.position += chunkmainpos;
                                        vert3.transform.localScale = new Vector3(0.05f, 0.085f, 0.05f);
                                        vert3.transform.rotation = Quaternion.identity;*/

                                    }
                                }
                            }
                        }
                    }

                }

                
                GameObject emptyobject = new GameObject();
                var meshfilt = emptyobject.AddComponent<MeshFilter>();
                var meshrend = emptyobject.AddComponent<MeshRenderer>();

                Mesh thenewmesh = new Mesh();
                thenewmesh.vertices = vertices.ToArray();
                thenewmesh.triangles = triangles.ToArray();

                emptyobject.GetComponent<MeshFilter>().mesh = thenewmesh;
                //_testChunk.GetComponent<MeshRenderer>().material = _mat;

                emptyobject.transform.position = chunkmainpos;
                emptyobject.transform.rotation = Quaternion.identity;

                emptyobject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                emptyobject.GetComponent<MeshRenderer>().material = mat;


                emptyobject.transform.parent = thecurrentchunk.transform;
                emptyobject.gameObject.name = "faces type:" + f;
                

            }

        }

        return mainChunk;
    }











    // Update is called once per frame
    /*void Update()
    {

    }*/
}
