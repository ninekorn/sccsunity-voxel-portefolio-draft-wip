using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using static sccschunkfacesbuilder;

public class sccscomputevoxelALLFACES : MonoBehaviour
{

    public int mindexx = 0;
    public int mindexy = 0;
    public int mindexz = 0;

    public int extremityiswl = 0;
    public int extremityiswr = 0;

    public int extremityishl = 0;
    public int extremityishr = 0;

    public int extremityisdl = 0;
    public int extremityisdr = 0;

    public int mainminx = 0;
    public int mainmaxx = 0;

    public int mainminy = 0;
    public int mainmaxy = 0;

    public int mainminz = 0;
    public int mainmaxz = 0;


    public class mainChunk
    {
        public int[] thebytemap;
        public List<Vector3> vertices;
        public List<int> triangles;
    }

    mainChunk[] blockers;




    public int schunkwl = 1;
    public int schunkwr = 0;

    public int schunkhl = 1;
    public int schunkhr = 0;

    public int schunkdl = 1;
    public int schunkdr = 0;





    public static sccscomputevoxelALLFACES currentsccscomputevoxelALLFACES;
    public struct mapbytes
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
    };
    public struct mapofvertints
    {
        public Vector4 vertpos;
    };

    private mapbytes[][] mapdata;

    private mapofvertints[][] datamapfirstvertxyztop;

    /*private mapofints[][] datamapfirstvertxtop;
    private mapofints[][] datamapfirstvertytop;
    private mapofints[][] datamapfirstvertztop;*/

    /* private mapofints[][] datawidthdimtop;
     private mapofints[][] dataheightdimtop;
     private mapofints[][] datadepthdimtop;*/

    private mapofvertints[][] datadims;



    /*
    public int levelsizex = 1;
    public int levelsizey = 1;
    public int levelsizez = 1;*/




    public int mapx = 10;
    public int mapy = 10;
    public int mapz = 10;

    public ComputeShader computeShaderForMap;
    public ComputeShader computeVertexesfacetype;
    //public ComputeShader computeVertexestwo;
    /*public ComputeShader computeVertexesLEFT;
    public ComputeShader computeVertexesRIGHT;
    public ComputeShader computeVertexesFRONT;*/

    /*public Material frontfacemat;
    public Material rightfacemat;
    public Material mat2;
    public Material mat1;*/
    public Material mat;

    public int threadmulx = 1;
    public int threadmuly = 1;
    public int threadmulz = 1;

    public float planesize = 0.1f;

    int reducedverttrigswtc = 0;

    // Start is called before the first frame update

    ComputeShader swapcomputetop;
    ComputeShader swapcomputeleft;
    ComputeShader swapcomputeright;
    ComputeShader swapcomputefront;
    ComputeShader swapcomputeback;
    ComputeShader swapcomputebottom;


   public int maxarrayssize = 0;

    public void Awake()
    {
        currentsccscomputevoxelALLFACES = this;

       maxarrayssize = (schunkwl + schunkwr + 1) * (schunkhl + schunkhr + 1) * (schunkdl + schunkdr + 1);

        blockers = new mainChunk[maxarrayssize];
    }



    public void CreateTheShaders(int facetype)
    {

        if (facetype == 0) //&& computeVertexesALLFACES == null
        {
            computeVertexesfacetype = (ComputeShader)Resources.Load("Compute/computevertexTOP");//ComputeShader.Find("Transparent/Diffuse");
            swapcomputetop = computeVertexesfacetype;
        }
        else
        {
            if (swapcomputetop != null)
            {
                computeVertexesfacetype = swapcomputetop;
                //swapcompute = 
            }
        }



        if (facetype == 1)// && computeVertexesfacetype == null)
        {
            computeVertexesfacetype = (ComputeShader)Resources.Load("Compute/computevertexLEFT");//ComputeShader.Find("Transparent/Diffuse");
            swapcomputeleft = computeVertexesfacetype;
        }
        else
        {
            if (swapcomputeleft != null)
            {
                computeVertexesfacetype = swapcomputeleft;
                //swapcompute = 
            }
        }




        if (facetype == 2)// && computeVertexesfacetype == null)
        {
            computeVertexesfacetype = (ComputeShader)Resources.Load("Compute/computevertexRIGHT");//ComputeShader.Find("Transparent/Diffuse");
            swapcomputeright = computeVertexesfacetype;
        }
        else
        {
            if (swapcomputeright != null)
            {
                computeVertexesfacetype = swapcomputeright;
                //swapcompute = 
            }
        }




        if (facetype == 3)// && computeVertexesfacetype == null)
        {
            computeVertexesfacetype = (ComputeShader)Resources.Load("Compute/computevertexFRONT");//ComputeShader.Find("Transparent/Diffuse");
            swapcomputefront = computeVertexesfacetype;
        }
        else
        {
            if (swapcomputefront != null)
            {
                computeVertexesfacetype = swapcomputefront;
                //swapcompute = 
            }
        }
        if (facetype == 4)// && computeVertexesfacetype == null)
        {
            computeVertexesfacetype = (ComputeShader)Resources.Load("Compute/computevertexBACK");//ComputeShader.Find("Transparent/Diffuse");
            swapcomputeback = computeVertexesfacetype;
        }
        else
        {
            if (swapcomputeback != null)
            {
                computeVertexesfacetype = swapcomputeback;
                //swapcompute = 
            }
        }
        if (facetype == 5)// && computeVertexesfacetype == null)
        {
            computeVertexesfacetype = (ComputeShader)Resources.Load("Compute/computevertexBOTTOM");//ComputeShader.Find("Transparent/Diffuse");
            swapcomputebottom = computeVertexesfacetype;
        }
        else
        {
            if (swapcomputebottom != null)
            {
                computeVertexesfacetype = swapcomputebottom;
                //swapcompute = 
            }
        }
    }



    //public Vector3 chunkoriginpos;
    public void CreateTheArrays(int facetype,Vector3 chunkoriginpos_, int mainindexx,int mainindexy,int mainindexz)
    {

        //chunkoriginpos = chunkoriginpos_;


        //int facetype = 5;

        mapdata = new mapbytes[maxarrayssize][];// levelsizex * levelsizey * levelsizez][];

        datamapfirstvertxyztop = new mapofvertints[maxarrayssize][];//levelsizex * levelsizey * levelsizez][];

        /*datamapfirstvertxtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertytop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertztop = new mapofints[levelsizex * levelsizey * levelsizez][];*/
        /*datawidthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        dataheightdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datadepthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];*/

        datadims = new mapofvertints[maxarrayssize][];//levelsizex * levelsizey * levelsizez][];




        /*
        GameObject emptyobjectparent1 = new GameObject();
        GameObject emptyobjectparentleftfaces = new GameObject();
        emptyobjectparentleftfaces.gameObject.name = "leftfacesmain";

        GameObject emptyobjectparentrightfaces = new GameObject();
        emptyobjectparentrightfaces.gameObject.name = "rightfacesmain";

        GameObject emptyobjectparentfrontfaces = new GameObject();
        emptyobjectparentfrontfaces.gameObject.name = "frontfacesmain";*/


        /*for (int mx = 0; mx < levelsizex; mx++)
        {
            for (int my = 0; my < levelsizey; my++)
            {
                for (int mz = 0; mz < levelsizez; mz++)
                {
                    int mindex = mx + levelsizex * (my + levelsizey * mz);
        */



        for (int x = -schunkwl; x <= schunkwr; x++)
        {
            for (int y = -schunkhl; y <= schunkhr; y++)
            {
                for (int z = -schunkdl; z <= schunkdr; z++)
                {
                    int xx = x;
                    int yy = y;
                    int zz = z;

                    if (xx < 0)
                    {
                        xx *= -1;
                        xx = xx + schunkwr;
                    }
                    if (yy < 0)
                    {
                        yy *= -1;
                        yy = yy + schunkhr;
                    }
                    if (zz < 0)
                    {
                        zz *= -1;
                        zz = zz + schunkdr;
                    }

                    int indexflat = xx + (schunkwl + schunkwr + 1) * (yy + (schunkhl + schunkhr + 1) * zz);

                    //Vector3 chunkmainpos = new Vector3(x * mapx * planesize, y * mapy * planesize, z * mapz * planesize) + chunkoriginpos_;
                    //Vector3 chunkmainpos = new Vector3(x * (schunkwl + schunkwr + 1) * planesize, y * (schunkhl + schunkhr + 1) * planesize, z * (schunkdl + schunkdr + 1) * planesize) + chunkoriginpos_;
                    Vector3 chunkmainpos = new Vector3(x * (schunkwl + schunkwr + 1) * mapx * planesize, y * (schunkhl + schunkhr + 1) * mapy * planesize, z * (schunkdl + schunkdr + 1) * mapz * planesize) + chunkoriginpos_;


                    //int totalSize = mapx * mapy * mapz;
                    mapdata[indexflat] = new mapbytes[mapx * mapy * mapz];

                    datamapfirstvertxyztop[indexflat] = new mapofvertints[mapx * mapy * mapz];

                    /*datamapfirstvertxtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertytop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertztop[mindex] = new mapofints[mapx * mapy * mapz];*/
                    /*datawidthdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    dataheightdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datadepthdimtop[mindex] = new mapofints[mapx * mapy * mapz];*/

                    datadims[indexflat] = new mapofvertints[mapx * mapy * mapz];



                    for (int xmap = 0; xmap < mapx; xmap++)
                    {
                        for (int ymap = 0; ymap < mapy; ymap++)
                        {
                            for (int zmap = 0; zmap < mapz; zmap++)
                            {
                                int index = xmap + mapx * (ymap + mapy * zmap);

                                mapdata[indexflat][index] = new mapbytes();
                                mapdata[indexflat][index].thebyte = 0;
                                mapdata[indexflat][index].position = new Vector3(x * mapx * planesize, y * mapy * planesize, z * mapz * planesize) ;
                                mapdata[indexflat][index].ix = xmap;
                                mapdata[indexflat][index].iy = ymap;
                                mapdata[indexflat][index].iz = zmap;




                                datamapfirstvertxyztop[indexflat][index] = new mapofvertints();
                                //datamapfirstvertxyztop[indexflat][index].thebyte = 0;

                                datamapfirstvertxyztop[indexflat][index].vertpos = new Vector4(0, 0, 0, 0);



                                /*datamapfirstvertxtop[indexflat][index] = new mapofints();
                                datamapfirstvertxtop[indexflat][index].thebyte = 0;

                                datamapfirstvertytop[indexflat][index] = new mapofints();
                                datamapfirstvertytop[indexflat][index].thebyte = 0;

                                datamapfirstvertztop[indexflat][index] = new mapofints();
                                datamapfirstvertztop[indexflat][index].thebyte = 0;
                                */
                                /*datawidthdimtop[indexflat][index] = new mapofints();
                                datawidthdimtop[indexflat][index].thebyte = 0;

                                dataheightdimtop[indexflat][index] = new mapofints();
                                dataheightdimtop[indexflat][index].thebyte = 0;

                                datadepthdimtop[indexflat][index] = new mapofints();
                                datadepthdimtop[indexflat][index].thebyte = 0;
                                */


                                datadims[indexflat][index] = new mapofvertints();
                                datadims[indexflat][index].vertpos = new Vector4(0, 0, 0, 0);


                            }
                        }
                    }

                }
            }
        }
    }




    public void CreateTheMaps(int facetype)
    {

        /*
        for (int mx = 0; mx < levelsizex; mx++)
        {
            for (int my = 0; my < levelsizey; my++)
            {
                for (int mz = 0; mz < levelsizez; mz++)
                {
                    int mindex = mx + levelsizex * (my + levelsizey * mz);
        */


        for (int x = -schunkwl; x <= schunkwr; x++)
        {
            for (int y = -schunkhl; y <= schunkhr; y++)
            {
                for (int z = -schunkdl; z <= schunkdr; z++)
                {
                    int xx = x;
                    int yy = y;
                    int zz = z;

                    if (xx < 0)
                    {
                        xx *= -1;
                        xx = xx + schunkwr;
                    }
                    if (yy < 0)
                    {
                        yy *= -1;
                        yy = yy + schunkhr;
                    }
                    if (zz < 0)
                    {
                        zz *= -1;
                        zz = zz + schunkdr;
                    }

                    int indexflat = xx + (schunkwl + schunkwr + 1) * (yy + (schunkhl + schunkhr + 1) * zz);


                    Vector3 chunkmainpos = new Vector3(x * mapx * planesize, y * mapy * planesize, z * mapz * planesize);

                    int thebytesize = sizeof(int) * 4;
                    int vector3Size = sizeof(float) * 3;
                    int totalSize = thebytesize + vector3Size;

                    ComputeBuffer mapsbuffer = new ComputeBuffer(mapdata[indexflat].Length, totalSize);

                    mapsbuffer.SetData(mapdata[indexflat]);

                    computeShaderForMap.SetBuffer(0, "themap", mapsbuffer);
                    computeShaderForMap.Dispatch(0, (mapx * mapy * mapz) / 10, 1, 1);

                    mapsbuffer.GetData(mapdata[indexflat]);

                    int[] mapint = new int[mapx * mapy * mapz];
                    for (int xmap = 0; xmap < mapx; xmap++)
                    {
                        for (int ymap = 0; ymap < mapy; ymap++)
                        {
                            for (int zmap = 0; zmap < mapz; zmap++)
                            {
                                int index = xmap + mapx * (ymap + mapy * zmap);

                                mapint[index] = mapdata[indexflat][index].thebyte;

                                //Debug.Log("map:" + data[index].thebyte);
                            }
                        }
                    }

                    blockers[indexflat] = new mainChunk();
                    blockers[indexflat].thebytemap = mapint;

                    mapsbuffer.Release();
                    mapsbuffer.Dispose();
                }
            }
        }
    }



    public void ComputeTheVertexes()
    {
        /*
       for (int mx = 0; mx < levelsizex; mx++)
       {
           for (int my = 0; my < levelsizey; my++)
           {
               for (int mz = 0; mz < levelsizez; mz++)
               {
                   int mindex = mx + levelsizex * (my + levelsizey * mz);
       */


        for (int x = -schunkwl; x <= schunkwr; x++)
        {
            for (int y = -schunkhl; y <= schunkhr; y++)
            {
                for (int z = -schunkdl; z <= schunkdr; z++)
                {
                    int xx = x;
                    int yy = y;
                    int zz = z;

                    if (xx < 0)
                    {
                        xx *= -1;
                        xx = xx + schunkwr;
                    }
                    if (yy < 0)
                    {
                        yy *= -1;
                        yy = yy + schunkhr;
                    }
                    if (zz < 0)
                    {
                        zz *= -1;
                        zz = zz + schunkdr;
                    }

                    int indexflat = xx + (schunkwl + schunkwr + 1) * (yy + (schunkhl + schunkhr + 1) * zz);

                    Vector3 chunkmainpos = new Vector3(x * mapx * planesize, y * mapy * planesize, z * mapz * planesize);




                    int thebytesize = sizeof(int) * 4;
                    int vector3Size = sizeof(float) * 3;
                    int totalSize = thebytesize + vector3Size;


                    ComputeBuffer maps0buffer = new ComputeBuffer(mapdata[indexflat].Length, totalSize);
                    maps0buffer.SetData(mapdata[indexflat]);


                    ComputeBuffer mapvertlocbufferxyz = new ComputeBuffer(datamapfirstvertxyztop[indexflat].Length, 16);
                    mapvertlocbufferxyz.SetData(datamapfirstvertxyztop[indexflat]);


                    /*ComputeBuffer mapvertlocbufferx = new ComputeBuffer(datamapfirstvertxtop[mindex].Length, 4);
                    mapvertlocbufferx.SetData(datamapfirstvertxtop[mindex]);

                    ComputeBuffer mapvertlocbuffery = new ComputeBuffer(datamapfirstvertytop[mindex].Length, 4);
                    mapvertlocbuffery.SetData(datamapfirstvertytop[mindex]);

                    ComputeBuffer mapvertlocbufferz = new ComputeBuffer(datamapfirstvertztop[mindex].Length, 4);
                    mapvertlocbufferz.SetData(datamapfirstvertztop[mindex]);*/

                    /*
                    ComputeBuffer mapwidthdimtop = new ComputeBuffer(datawidthdimtop[mindex].Length, 4);
                    mapwidthdimtop.SetData(datawidthdimtop[mindex]);

                    ComputeBuffer mapheightdimtop = new ComputeBuffer(dataheightdimtop[mindex].Length, 4);
                    mapheightdimtop.SetData(dataheightdimtop[mindex]);

                    ComputeBuffer mapdepthdimtop = new ComputeBuffer(datadepthdimtop[mindex].Length, 4);
                    mapdepthdimtop.SetData(datadepthdimtop[mindex]);
                    */

                    ComputeBuffer mapdims = new ComputeBuffer(datadims[indexflat].Length, 16);
                    mapdims.SetData(datadims[indexflat]);



                    if (reducedverttrigswtc == 0)
                    {
                        computeVertexesfacetype.SetBuffer(0, "themap", maps0buffer);

                        computeVertexesfacetype.SetBuffer(0, "mapfirstvertxyztop", mapvertlocbufferxyz);

                        /*computeVertexesfacetype.SetBuffer(0, "mapfirstvertxtop", mapvertlocbufferx);
                        computeVertexesfacetype.SetBuffer(0, "mapfirstvertytop", mapvertlocbuffery);
                        computeVertexesfacetype.SetBuffer(0, "mapfirstvertztop", mapvertlocbufferz);*/
                        /*
                        computeVertexesfacetype.SetBuffer(0, "widthdimtop", mapwidthdimtop);
                        computeVertexesfacetype.SetBuffer(0, "heightdimtop", mapheightdimtop);
                        computeVertexesfacetype.SetBuffer(0, "depthdimtop", mapdepthdimtop);*/


                        computeVertexesfacetype.SetBuffer(0, "dims", mapdims);




                        computeVertexesfacetype.Dispatch(0, threadmulx, threadmuly, threadmulz);











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

                    /*
                    mapvertlocbufferx.GetData(datamapfirstvertxtop[mindex]);
                    mapvertlocbuffery.GetData(datamapfirstvertytop[mindex]);
                    mapvertlocbufferz.GetData(datamapfirstvertztop[mindex]);*/

                    mapvertlocbufferxyz.GetData(datamapfirstvertxyztop[indexflat]);


                    mapdims.GetData(datadims[indexflat]);


                    maps0buffer.Release();
                    maps0buffer.Dispose();

                    mapvertlocbufferxyz.Release();
                    mapvertlocbufferxyz.Dispose();


                    mapdims.Release();
                    mapdims.Dispose();

                }
            }
        }

    }



    public void CreateTheVerticesAndTriangles(int facetype,out List<Vector3>[] vertices, out List<int>[] triangles)

    //void Start()
    {
        vertices = new List<Vector3>[maxarrayssize];// levelsizex* levelsizey * levelsizez];
        triangles = new List<int>[maxarrayssize];//[levelsizex * levelsizey * levelsizez];

        /*
       for (int mx = 0; mx < levelsizex; mx++)
       {
           for (int my = 0; my < levelsizey; my++)
           {
               for (int mz = 0; mz < levelsizez; mz++)
               {
                   int mindex = mx + levelsizex * (my + levelsizey * mz);
       */




        int sextremityxl = 0;
        int sextremityxr = 0;

        int sextremityyl = 0;
        int sextremityyr = 0;

        int sextremityzl = 0;
        int sextremityzr = 0;



        for (int x = -schunkwl; x <= schunkwr; x++)
        {
            for (int y = -schunkhl; y <= schunkhr; y++)
            {
                for (int z = -schunkdl; z <= schunkdr; z++)
                {


                    sextremityxl = 0;
                    sextremityxr = 0;

                    sextremityyl = 0;
                    sextremityyr = 0;

                    sextremityzl = 0;
                    sextremityzr = 0;


                    int xx = x;
                    int yy = y;
                    int zz = z;

                    if (xx < 0)
                    {
                        xx *= -1;
                        xx = xx + schunkwr;
                    }
                    if (yy < 0)
                    {
                        yy *= -1;
                        yy = yy + schunkhr;
                    }
                    if (zz < 0)
                    {
                        zz *= -1;
                        zz = zz + schunkdr;
                    }

                    int indexflat = xx + (schunkwl + schunkwr + 1) * (yy + (schunkhl + schunkhr + 1) * zz);

                    Vector3 chunkmainpos = new Vector3(x * mapx * planesize, y * mapy * planesize, z * mapz * planesize);


                    if (x == -schunkwl)
                    {
                        sextremityxl = 1;
                    }

                    if (x == schunkwr)
                    {
                        sextremityxr = 1;
                    }

                    if (y == -schunkhl)
                    {
                        sextremityyl = 1;
                    }
                    if (y == schunkhr)
                    {
                        sextremityyr = 1;
                    }

                    if (z == -schunkdl)
                    {
                        sextremityzl = 1;
                    }
                    if (z == schunkdr)
                    {
                        sextremityzr = 1;
                    }




                    /*
                    mapwidthdimtop.GetData(datawidthdimtop[mindex]);
                    mapheightdimtop.GetData(dataheightdimtop[mindex]);
                    mapdepthdimtop.GetData(datadepthdimtop[mindex]);*/



                    /*List<Vector3> vertices = new List<Vector3>();
                    List<int> triangles = new List<int>();
                    */



                    vertices[indexflat] = new List<Vector3>();
                    triangles[indexflat] = new List<int>();

                    
                    for (int xmap = 0; xmap < mapx; xmap++)
                    {
                        for (int ymap = 0; ymap < mapy; ymap++)
                        {
                            for (int zmap = 0; zmap < mapz; zmap++)
                            {
                                int index = xmap + mapx * (ymap + mapy * zmap);

                                //mapint[index] = data[mindex][index].thebyte;

                                //Debug.Log("map:" + data[index].thebyte);



                                if (datadims[indexflat][index].vertpos.y == 0)//datamapfirstvertxtop[mindex][index].thebyte == 0 && datamapfirstvertytop[mindex][index].thebyte == 0 && datamapfirstvertztop[mindex][index].thebyte == 0)
                                {

                                }
                                else
                                {

                                    //TOPFACE
                                    //TOPFACE
                                    //TOPFACE
                                    if (extremityishr == 1)
                                    {
                                        var planetdivtothetop = sccschunkfacesbuilder.instance.getplanetdiv((int)(mindexx), (int)(mindexy + 1), (int)(mindexz));

                                        if (planetdivtothetop != null)
                                        {
                                            var planetdivchunk = planetdivtothetop.getChunk(x, -schunkhl, z);

                                            if (planetdivchunk != null)
                                            {
                                                if (IsTransparent(xmap, 0, zmap, planetdivchunk.thebytemap))
                                                {
                                                    if (facetype == 0)
                                                    {
                                                        int indexofvert00 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        int indexofvert10 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        int indexofvert20 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        int indexofvert30 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));


                                                        vertices[indexflat].Add(firstvertofface0);
                                                        vertices[indexflat].Add(secondvertofface0);
                                                        vertices[indexflat].Add(thirdvertofface0);
                                                        vertices[indexflat].Add(fourthvertofface0);

                                                        triangles[indexflat].Add(indexofvert20);
                                                        triangles[indexflat].Add(indexofvert10);
                                                        triangles[indexflat].Add(indexofvert00);
                                                        triangles[indexflat].Add(indexofvert10);
                                                        triangles[indexflat].Add(indexofvert20);
                                                        triangles[indexflat].Add(indexofvert30);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap, ymap + 1, zmap, null))
                                                {
                                                    if (facetype == 0)
                                                    {
                                                        int indexofvert00 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        int indexofvert10 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        int indexofvert20 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        int indexofvert30 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));


                                                        vertices[indexflat].Add(firstvertofface0);
                                                        vertices[indexflat].Add(secondvertofface0);
                                                        vertices[indexflat].Add(thirdvertofface0);
                                                        vertices[indexflat].Add(fourthvertofface0);

                                                        triangles[indexflat].Add(indexofvert20);
                                                        triangles[indexflat].Add(indexofvert10);
                                                        triangles[indexflat].Add(indexofvert00);
                                                        triangles[indexflat].Add(indexofvert10);
                                                        triangles[indexflat].Add(indexofvert20);
                                                        triangles[indexflat].Add(indexofvert30);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {


                                            if (sextremityyr == 1)
                                            {
                                                if (ymap == mapy - 1)
                                                {
                                                    var planetdivchunk = getChunk(x, y + 1, z);

                                                    if (planetdivchunk != null)
                                                    {
                                                        if (IsTransparent(xmap, 0, zmap, planetdivchunk.thebytemap))
                                                        {
                                                            if (facetype == 0)
                                                            {
                                                                int indexofvert00 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                int indexofvert10 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                int indexofvert20 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                int indexofvert30 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));


                                                                vertices[indexflat].Add(firstvertofface0);
                                                                vertices[indexflat].Add(secondvertofface0);
                                                                vertices[indexflat].Add(thirdvertofface0);
                                                                vertices[indexflat].Add(fourthvertofface0);

                                                                triangles[indexflat].Add(indexofvert20);
                                                                triangles[indexflat].Add(indexofvert10);
                                                                triangles[indexflat].Add(indexofvert00);
                                                                triangles[indexflat].Add(indexofvert10);
                                                                triangles[indexflat].Add(indexofvert20);
                                                                triangles[indexflat].Add(indexofvert30);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (IsTransparent(xmap, ymap + 1, zmap, null))
                                                        {
                                                            if (facetype == 0)
                                                            {
                                                                int indexofvert00 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                int indexofvert10 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                int indexofvert20 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                int indexofvert30 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));


                                                                vertices[indexflat].Add(firstvertofface0);
                                                                vertices[indexflat].Add(secondvertofface0);
                                                                vertices[indexflat].Add(thirdvertofface0);
                                                                vertices[indexflat].Add(fourthvertofface0);

                                                                triangles[indexflat].Add(indexofvert20);
                                                                triangles[indexflat].Add(indexofvert10);
                                                                triangles[indexflat].Add(indexofvert00);
                                                                triangles[indexflat].Add(indexofvert10);
                                                                triangles[indexflat].Add(indexofvert20);
                                                                triangles[indexflat].Add(indexofvert30);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (IsTransparent(xmap, ymap + 1, zmap, null))
                                                    {
                                                        if (facetype == 0)
                                                        {
                                                            int indexofvert00 = vertices[indexflat].Count;
                                                            Vector3 firstvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            int indexofvert10 = vertices[indexflat].Count + 1;
                                                            Vector3 secondvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            int indexofvert20 = vertices[indexflat].Count + 2;
                                                            Vector3 thirdvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            int indexofvert30 = vertices[indexflat].Count + 3;
                                                            Vector3 fourthvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));


                                                            vertices[indexflat].Add(firstvertofface0);
                                                            vertices[indexflat].Add(secondvertofface0);
                                                            vertices[indexflat].Add(thirdvertofface0);
                                                            vertices[indexflat].Add(fourthvertofface0);

                                                            triangles[indexflat].Add(indexofvert20);
                                                            triangles[indexflat].Add(indexofvert10);
                                                            triangles[indexflat].Add(indexofvert00);
                                                            triangles[indexflat].Add(indexofvert10);
                                                            triangles[indexflat].Add(indexofvert20);
                                                            triangles[indexflat].Add(indexofvert30);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap, ymap + 1, zmap, null))
                                                {
                                                    if (facetype == 0)
                                                    {
                                                        int indexofvert00 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        int indexofvert10 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        int indexofvert20 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        int indexofvert30 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));


                                                        vertices[indexflat].Add(firstvertofface0);
                                                        vertices[indexflat].Add(secondvertofface0);
                                                        vertices[indexflat].Add(thirdvertofface0);
                                                        vertices[indexflat].Add(fourthvertofface0);

                                                        triangles[indexflat].Add(indexofvert20);
                                                        triangles[indexflat].Add(indexofvert10);
                                                        triangles[indexflat].Add(indexofvert00);
                                                        triangles[indexflat].Add(indexofvert10);
                                                        triangles[indexflat].Add(indexofvert20);
                                                        triangles[indexflat].Add(indexofvert30);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (extremityishr == 0)
                                    {

                                        var planetdivtothetop = sccschunkfacesbuilder.instance.getplanetdiv((int)(mindexx), (int)(mindexy + 1), (int)(mindexz));

                                        if (planetdivtothetop!= null)
                                        {



                                            if (sextremityyr == 1)
                                            {
                                                if (ymap == mapy - 1)
                                                {
                                                    var planetdivchunk = planetdivtothetop.getChunk(x, -schunkhl, z);

                                                    if (planetdivchunk != null)
                                                    {
                                                        if (IsTransparent(xmap, 0, zmap, planetdivchunk.thebytemap))
                                                        {
                                                            if (facetype == 0)
                                                            {
                                                                int indexofvert00 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                int indexofvert10 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                int indexofvert20 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                int indexofvert30 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));


                                                                vertices[indexflat].Add(firstvertofface0);
                                                                vertices[indexflat].Add(secondvertofface0);
                                                                vertices[indexflat].Add(thirdvertofface0);
                                                                vertices[indexflat].Add(fourthvertofface0);

                                                                triangles[indexflat].Add(indexofvert20);
                                                                triangles[indexflat].Add(indexofvert10);
                                                                triangles[indexflat].Add(indexofvert00);
                                                                triangles[indexflat].Add(indexofvert10);
                                                                triangles[indexflat].Add(indexofvert20);
                                                                triangles[indexflat].Add(indexofvert30);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (IsTransparent(xmap, ymap + 1, zmap, null))
                                                    {
                                                        if (facetype == 0)
                                                        {
                                                            int indexofvert00 = vertices[indexflat].Count;
                                                            Vector3 firstvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            int indexofvert10 = vertices[indexflat].Count + 1;
                                                            Vector3 secondvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            int indexofvert20 = vertices[indexflat].Count + 2;
                                                            Vector3 thirdvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            int indexofvert30 = vertices[indexflat].Count + 3;
                                                            Vector3 fourthvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));


                                                            vertices[indexflat].Add(firstvertofface0);
                                                            vertices[indexflat].Add(secondvertofface0);
                                                            vertices[indexflat].Add(thirdvertofface0);
                                                            vertices[indexflat].Add(fourthvertofface0);

                                                            triangles[indexflat].Add(indexofvert20);
                                                            triangles[indexflat].Add(indexofvert10);
                                                            triangles[indexflat].Add(indexofvert00);
                                                            triangles[indexflat].Add(indexofvert10);
                                                            triangles[indexflat].Add(indexofvert20);
                                                            triangles[indexflat].Add(indexofvert30);
                                                        }
                                                    }
                                                }
                                               
                                                //Destroy(planetdivtothetop.transform.gameObject);
                                                //planetdivtothetop.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap, ymap + 1, zmap, null))
                                                {
                                                    if (facetype == 0)
                                                    {
                                                        int indexofvert00 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        int indexofvert10 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        int indexofvert20 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        int indexofvert30 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface0 = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));


                                                        vertices[indexflat].Add(firstvertofface0);
                                                        vertices[indexflat].Add(secondvertofface0);
                                                        vertices[indexflat].Add(thirdvertofface0);
                                                        vertices[indexflat].Add(fourthvertofface0);

                                                        triangles[indexflat].Add(indexofvert20);
                                                        triangles[indexflat].Add(indexofvert10);
                                                        triangles[indexflat].Add(indexofvert00);
                                                        triangles[indexflat].Add(indexofvert10);
                                                        triangles[indexflat].Add(indexofvert20);
                                                        triangles[indexflat].Add(indexofvert30);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //TOPFACE
                                    //TOPFACE
                                    //TOPFACE



















                                    //BOTTOMFACE
                                    //BOTTOMFACE
                                    //BOTTOMFACE
                                    if (extremityishl == 1)
                                    {
                                        var planetdivtothetop = sccschunkfacesbuilder.instance.getplanetdiv((int)(mindexx), (int)(mindexy - 1), (int)(mindexz));

                                        if (planetdivtothetop != null)
                                        {
                                            var planetdivchunk = planetdivtothetop.getChunk(x, schunkhr, z);

                                            if (planetdivchunk != null)
                                            {
                                                if (IsTransparent(xmap, mapy-1, zmap, planetdivchunk.thebytemap))
                                                {
                                                    if (facetype == 5)
                                                    {

                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        firstvertofface.y -= (1 * 1.0f);

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                        swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;

                                                        secondvertofface.y -= (1 * 1.0f);
                                                      
                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                        //thirdvertofface.z -= (1 * 1.0f);
                                                        thirdvertofface.y -= (1 * 1.0f);
                                                        swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;

                                 
                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        //fourthvertofface.z -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);

                                                        swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;


                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap, ymap - 1, zmap, null))
                                                {
                                                    if (facetype == 5)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        firstvertofface.y -= (1 * 1.0f);

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                        swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;

                                                        secondvertofface.y -= (1 * 1.0f);

                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                        //thirdvertofface.z -= (1 * 1.0f);
                                                        thirdvertofface.y -= (1 * 1.0f);
                                                        swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;


                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        //fourthvertofface.z -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);

                                                        swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;


                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {


                                            if (sextremityyl == 1)
                                            {
                                                if (ymap == 0) //mapy - 1
                                                {
                                                    var planetdivchunk = getChunk(x, y - 1, z);

                                                    if (planetdivchunk != null)
                                                    {
                                                        if (IsTransparent(xmap, mapy-1, zmap, planetdivchunk.thebytemap))
                                                        {
                                                            if (facetype == 5)
                                                            {
                                                                int indexofvert0 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                float swapx = firstvertofface.x;
                                                                float swapy = firstvertofface.y;
                                                                float swapz = firstvertofface.z;

                                                                firstvertofface.y -= (1 * 1.0f);

                                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                                swapx = secondvertofface.x;
                                                                swapy = secondvertofface.y;
                                                                swapz = secondvertofface.z;

                                                                secondvertofface.y -= (1 * 1.0f);

                                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                                //thirdvertofface.z -= (1 * 1.0f);
                                                                thirdvertofface.y -= (1 * 1.0f);
                                                                swapx = thirdvertofface.x;
                                                                swapy = thirdvertofface.y;
                                                                swapz = thirdvertofface.z;


                                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                //fourthvertofface.z -= (1 * 1.0f);
                                                                fourthvertofface.y -= (1 * 1.0f);

                                                                swapx = fourthvertofface.x;
                                                                swapy = fourthvertofface.y;
                                                                swapz = fourthvertofface.z;


                                                                vertices[indexflat].Add(firstvertofface);
                                                                vertices[indexflat].Add(secondvertofface);
                                                                vertices[indexflat].Add(thirdvertofface);
                                                                vertices[indexflat].Add(fourthvertofface);

                                                                triangles[indexflat].Add(indexofvert0);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert3);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert1);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (IsTransparent(xmap, ymap - 1, zmap, null))
                                                        {
                                                            if (facetype == 5)
                                                            {
                                                                int indexofvert0 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                float swapx = firstvertofface.x;
                                                                float swapy = firstvertofface.y;
                                                                float swapz = firstvertofface.z;

                                                                firstvertofface.y -= (1 * 1.0f);

                                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                                swapx = secondvertofface.x;
                                                                swapy = secondvertofface.y;
                                                                swapz = secondvertofface.z;

                                                                secondvertofface.y -= (1 * 1.0f);

                                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                                //thirdvertofface.z -= (1 * 1.0f);
                                                                thirdvertofface.y -= (1 * 1.0f);
                                                                swapx = thirdvertofface.x;
                                                                swapy = thirdvertofface.y;
                                                                swapz = thirdvertofface.z;


                                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                //fourthvertofface.z -= (1 * 1.0f);
                                                                fourthvertofface.y -= (1 * 1.0f);

                                                                swapx = fourthvertofface.x;
                                                                swapy = fourthvertofface.y;
                                                                swapz = fourthvertofface.z;


                                                                vertices[indexflat].Add(firstvertofface);
                                                                vertices[indexflat].Add(secondvertofface);
                                                                vertices[indexflat].Add(thirdvertofface);
                                                                vertices[indexflat].Add(fourthvertofface);

                                                                triangles[indexflat].Add(indexofvert0);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert3);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert1);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (IsTransparent(xmap, ymap - 1, zmap, null))
                                                    {
                                                        if (facetype == 5)
                                                        {
                                                            int indexofvert0 = vertices[indexflat].Count;
                                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            float swapx = firstvertofface.x;
                                                            float swapy = firstvertofface.y;
                                                            float swapz = firstvertofface.z;

                                                            firstvertofface.y -= (1 * 1.0f);

                                                            int indexofvert1 = vertices[indexflat].Count + 1;
                                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                            swapx = secondvertofface.x;
                                                            swapy = secondvertofface.y;
                                                            swapz = secondvertofface.z;

                                                            secondvertofface.y -= (1 * 1.0f);

                                                            int indexofvert2 = vertices[indexflat].Count + 2;
                                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                            //thirdvertofface.z -= (1 * 1.0f);
                                                            thirdvertofface.y -= (1 * 1.0f);
                                                            swapx = thirdvertofface.x;
                                                            swapy = thirdvertofface.y;
                                                            swapz = thirdvertofface.z;


                                                            int indexofvert3 = vertices[indexflat].Count + 3;
                                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            //fourthvertofface.z -= (1 * 1.0f);
                                                            fourthvertofface.y -= (1 * 1.0f);

                                                            swapx = fourthvertofface.x;
                                                            swapy = fourthvertofface.y;
                                                            swapz = fourthvertofface.z;


                                                            vertices[indexflat].Add(firstvertofface);
                                                            vertices[indexflat].Add(secondvertofface);
                                                            vertices[indexflat].Add(thirdvertofface);
                                                            vertices[indexflat].Add(fourthvertofface);

                                                            triangles[indexflat].Add(indexofvert0);
                                                            triangles[indexflat].Add(indexofvert1);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert3);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert1);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap, ymap - 1, zmap, null))
                                                {
                                                    if (facetype == 5)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        firstvertofface.y -= (1 * 1.0f);

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                        swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;

                                                        secondvertofface.y -= (1 * 1.0f);

                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                        //thirdvertofface.z -= (1 * 1.0f);
                                                        thirdvertofface.y -= (1 * 1.0f);
                                                        swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;


                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        //fourthvertofface.z -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);

                                                        swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;


                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (extremityishl == 0)
                                    {

                                        var planetdivtothetop = sccschunkfacesbuilder.instance.getplanetdiv((int)(mindexx), (int)(mindexy - 1), (int)(mindexz));

                                        if (planetdivtothetop != null)
                                        {



                                            if (sextremityyl == 1)
                                            {
                                                if (ymap == 0) //mapy - 1
                                                {
                                                    var planetdivchunk = planetdivtothetop.getChunk(x, schunkhr, z);

                                                    if (planetdivchunk != null)
                                                    {
                                                        if (IsTransparent(xmap, mapy-1, zmap, planetdivchunk.thebytemap))
                                                        {
                                                            if (facetype == 5)
                                                            {
                                                                int indexofvert0 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                float swapx = firstvertofface.x;
                                                                float swapy = firstvertofface.y;
                                                                float swapz = firstvertofface.z;

                                                                firstvertofface.y -= (1 * 1.0f);

                                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                                swapx = secondvertofface.x;
                                                                swapy = secondvertofface.y;
                                                                swapz = secondvertofface.z;

                                                                secondvertofface.y -= (1 * 1.0f);

                                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                                //thirdvertofface.z -= (1 * 1.0f);
                                                                thirdvertofface.y -= (1 * 1.0f);
                                                                swapx = thirdvertofface.x;
                                                                swapy = thirdvertofface.y;
                                                                swapz = thirdvertofface.z;


                                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                //fourthvertofface.z -= (1 * 1.0f);
                                                                fourthvertofface.y -= (1 * 1.0f);

                                                                swapx = fourthvertofface.x;
                                                                swapy = fourthvertofface.y;
                                                                swapz = fourthvertofface.z;


                                                                vertices[indexflat].Add(firstvertofface);
                                                                vertices[indexflat].Add(secondvertofface);
                                                                vertices[indexflat].Add(thirdvertofface);
                                                                vertices[indexflat].Add(fourthvertofface);

                                                                triangles[indexflat].Add(indexofvert0);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert3);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert1);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (IsTransparent(xmap, ymap - 1, zmap, null))
                                                    {
                                                        if (facetype == 5)
                                                        {
                                                            int indexofvert0 = vertices[indexflat].Count;
                                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            float swapx = firstvertofface.x;
                                                            float swapy = firstvertofface.y;
                                                            float swapz = firstvertofface.z;

                                                            firstvertofface.y -= (1 * 1.0f);

                                                            int indexofvert1 = vertices[indexflat].Count + 1;
                                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                            swapx = secondvertofface.x;
                                                            swapy = secondvertofface.y;
                                                            swapz = secondvertofface.z;

                                                            secondvertofface.y -= (1 * 1.0f);

                                                            int indexofvert2 = vertices[indexflat].Count + 2;
                                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                            //thirdvertofface.z -= (1 * 1.0f);
                                                            thirdvertofface.y -= (1 * 1.0f);
                                                            swapx = thirdvertofface.x;
                                                            swapy = thirdvertofface.y;
                                                            swapz = thirdvertofface.z;


                                                            int indexofvert3 = vertices[indexflat].Count + 3;
                                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            //fourthvertofface.z -= (1 * 1.0f);
                                                            fourthvertofface.y -= (1 * 1.0f);

                                                            swapx = fourthvertofface.x;
                                                            swapy = fourthvertofface.y;
                                                            swapz = fourthvertofface.z;


                                                            vertices[indexflat].Add(firstvertofface);
                                                            vertices[indexflat].Add(secondvertofface);
                                                            vertices[indexflat].Add(thirdvertofface);
                                                            vertices[indexflat].Add(fourthvertofface);

                                                            triangles[indexflat].Add(indexofvert0);
                                                            triangles[indexflat].Add(indexofvert1);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert3);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert1);
                                                        }
                                                    }
                                                }

                                                //Destroy(planetdivtothetop.transform.gameObject);
                                                //planetdivtothetop.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap, ymap - 1, zmap, null))
                                                {
                                                    if (facetype == 5)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        firstvertofface.y -= (1 * 1.0f);

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                        swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;

                                                        secondvertofface.y -= (1 * 1.0f);

                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                        //thirdvertofface.z -= (1 * 1.0f);
                                                        thirdvertofface.y -= (1 * 1.0f);
                                                        swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;


                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        //fourthvertofface.z -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);

                                                        swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;


                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //BOTTOMFACE
                                    //BOTTOMFACE
                                    //BOTTOMFACE
































                                    //RIGHTFACE
                                    //RIGHTFACE
                                    //RIGHTFACE
                                    if (extremityiswr == 1)
                                    {
                                        var planetdivtothetop = sccschunkfacesbuilder.instance.getplanetdiv((int)(mindexx + 1), (int)(mindexy ), (int)(mindexz));

                                        if (planetdivtothetop != null)
                                        {
                                            var planetdivchunk = planetdivtothetop.getChunk(-schunkwl, y, z);

                                            if (planetdivchunk != null)
                                            {
                                                if (IsTransparent(0, ymap, zmap, planetdivchunk.thebytemap))
                                                {
                                                    if (facetype == 2)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        secondvertofface.x -= (1 * 1.0f);
                                                        secondvertofface.y -= (1 * 1.0f);


                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.x -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);

                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap + 1, ymap , zmap, null))
                                                {
                                                    if (facetype == 2)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        secondvertofface.x -= (1 * 1.0f);
                                                        secondvertofface.y -= (1 * 1.0f);


                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.x -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);

                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {


                                            if (sextremityxr == 1)
                                            {
                                                if (xmap == mapx - 1)
                                                {
                                                    var planetdivchunk = getChunk(x + 1, y , z);

                                                    if (planetdivchunk != null)
                                                    {
                                                        if (IsTransparent(0, ymap, zmap, planetdivchunk.thebytemap))
                                                        {
                                                            if (facetype == 2)
                                                            {
                                                                int indexofvert0 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                float swapx = firstvertofface.x;
                                                                float swapy = firstvertofface.y;
                                                                float swapz = firstvertofface.z;

                                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                secondvertofface.x -= (1 * 1.0f);
                                                                secondvertofface.y -= (1 * 1.0f);


                                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                fourthvertofface.x -= (1 * 1.0f);
                                                                fourthvertofface.y -= (1 * 1.0f);

                                                                vertices[indexflat].Add(firstvertofface);
                                                                vertices[indexflat].Add(secondvertofface);
                                                                vertices[indexflat].Add(thirdvertofface);
                                                                vertices[indexflat].Add(fourthvertofface);

                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert0);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert3);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (IsTransparent(xmap + 1, ymap , zmap, null))
                                                        {
                                                            if (facetype == 2)
                                                            {
                                                                int indexofvert0 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                float swapx = firstvertofface.x;
                                                                float swapy = firstvertofface.y;
                                                                float swapz = firstvertofface.z;

                                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                secondvertofface.x -= (1 * 1.0f);
                                                                secondvertofface.y -= (1 * 1.0f);


                                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                fourthvertofface.x -= (1 * 1.0f);
                                                                fourthvertofface.y -= (1 * 1.0f);

                                                                vertices[indexflat].Add(firstvertofface);
                                                                vertices[indexflat].Add(secondvertofface);
                                                                vertices[indexflat].Add(thirdvertofface);
                                                                vertices[indexflat].Add(fourthvertofface);

                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert0);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert3);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (IsTransparent(xmap + 1, ymap , zmap, null))
                                                    {
                                                        if (facetype == 2)
                                                        {
                                                            int indexofvert0 = vertices[indexflat].Count;
                                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            float swapx = firstvertofface.x;
                                                            float swapy = firstvertofface.y;
                                                            float swapz = firstvertofface.z;

                                                            int indexofvert1 = vertices[indexflat].Count + 1;
                                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            secondvertofface.x -= (1 * 1.0f);
                                                            secondvertofface.y -= (1 * 1.0f);


                                                            int indexofvert2 = vertices[indexflat].Count + 2;
                                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            int indexofvert3 = vertices[indexflat].Count + 3;
                                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            fourthvertofface.x -= (1 * 1.0f);
                                                            fourthvertofface.y -= (1 * 1.0f);

                                                            vertices[indexflat].Add(firstvertofface);
                                                            vertices[indexflat].Add(secondvertofface);
                                                            vertices[indexflat].Add(thirdvertofface);
                                                            vertices[indexflat].Add(fourthvertofface);

                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert1);
                                                            triangles[indexflat].Add(indexofvert0);
                                                            triangles[indexflat].Add(indexofvert1);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert3);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap + 1, ymap , zmap, null))
                                                {
                                                    if (facetype == 2)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        secondvertofface.x -= (1 * 1.0f);
                                                        secondvertofface.y -= (1 * 1.0f);


                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.x -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);

                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (extremityiswr == 0)
                                    {

                                        var planetdivtothetop = sccschunkfacesbuilder.instance.getplanetdiv((int)(mindexx + 1), (int)(mindexy ), (int)(mindexz));

                                        if (planetdivtothetop != null)
                                        {



                                            if (sextremityxr == 1)
                                            {
                                                if (xmap == mapx - 1)
                                                {
                                                    var planetdivchunk = planetdivtothetop.getChunk(-schunkwl, y, z);

                                                    if (planetdivchunk != null)
                                                    {
                                                        if (IsTransparent(0, ymap, zmap, planetdivchunk.thebytemap))
                                                        {
                                                            if (facetype == 2)
                                                            {
                                                                int indexofvert0 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                float swapx = firstvertofface.x;
                                                                float swapy = firstvertofface.y;
                                                                float swapz = firstvertofface.z;

                                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                secondvertofface.x -= (1 * 1.0f);
                                                                secondvertofface.y -= (1 * 1.0f);


                                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                fourthvertofface.x -= (1 * 1.0f);
                                                                fourthvertofface.y -= (1 * 1.0f);

                                                                vertices[indexflat].Add(firstvertofface);
                                                                vertices[indexflat].Add(secondvertofface);
                                                                vertices[indexflat].Add(thirdvertofface);
                                                                vertices[indexflat].Add(fourthvertofface);

                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert0);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert3);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (IsTransparent(xmap + 1, ymap , zmap, null))
                                                    {
                                                        if (facetype == 2)
                                                        {
                                                            int indexofvert0 = vertices[indexflat].Count;
                                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            float swapx = firstvertofface.x;
                                                            float swapy = firstvertofface.y;
                                                            float swapz = firstvertofface.z;

                                                            int indexofvert1 = vertices[indexflat].Count + 1;
                                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            secondvertofface.x -= (1 * 1.0f);
                                                            secondvertofface.y -= (1 * 1.0f);


                                                            int indexofvert2 = vertices[indexflat].Count + 2;
                                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            int indexofvert3 = vertices[indexflat].Count + 3;
                                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            fourthvertofface.x -= (1 * 1.0f);
                                                            fourthvertofface.y -= (1 * 1.0f);

                                                            vertices[indexflat].Add(firstvertofface);
                                                            vertices[indexflat].Add(secondvertofface);
                                                            vertices[indexflat].Add(thirdvertofface);
                                                            vertices[indexflat].Add(fourthvertofface);

                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert1);
                                                            triangles[indexflat].Add(indexofvert0);
                                                            triangles[indexflat].Add(indexofvert1);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert3);
                                                        }
                                                    }
                                                }

                                                //Destroy(planetdivtothetop.transform.gameObject);
                                                //planetdivtothetop.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap + 1, ymap, zmap, null))
                                                {
                                                    if (facetype == 2)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        secondvertofface.x -= (1 * 1.0f);
                                                        secondvertofface.y -= (1 * 1.0f);


                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.x -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);

                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //RIGHTFACE
                                    //RIGHTFACE
                                    //RIGHTFACE



























                                    //LEFTFACE
                                    //LEFTFACE
                                    //LEFTFACE
                                    if (extremityiswl == 1)
                                    {
                                        var planetdivtothetop = sccschunkfacesbuilder.instance.getplanetdiv((int)(mindexx - 1), (int)(mindexy ), (int)(mindexz));

                                        if (planetdivtothetop != null)
                                        {
                                            var planetdivchunk = planetdivtothetop.getChunk(schunkwr, y, z);

                                            if (planetdivchunk != null)
                                            {
                                                if (IsTransparent(mapx - 1, ymap, zmap, planetdivchunk.thebytemap))
                                                {
                                                    if (facetype == 1)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        secondvertofface.x -= (1 * 1.0f);
                                                        secondvertofface.y -= (1 * 1.0f);

                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.x -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);

                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap - 1, ymap, zmap, null))
                                                {
                                                    if (facetype == 1)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        secondvertofface.x -= (1 * 1.0f);
                                                        secondvertofface.y -= (1 * 1.0f);

                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.x -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);

                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {


                                            if (sextremityxl == 1)
                                            {
                                                if (xmap == 0) //mapy - 1
                                                {
                                                    var planetdivchunk = getChunk(x - 1, y , z);

                                                    if (planetdivchunk != null)
                                                    {
                                                        if (IsTransparent(mapx - 1, ymap, zmap, planetdivchunk.thebytemap))
                                                        {
                                                            if (facetype == 1)
                                                            {
                                                                int indexofvert0 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                float swapx = firstvertofface.x;
                                                                float swapy = firstvertofface.y;
                                                                float swapz = firstvertofface.z;

                                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                secondvertofface.x -= (1 * 1.0f);
                                                                secondvertofface.y -= (1 * 1.0f);

                                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                fourthvertofface.x -= (1 * 1.0f);
                                                                fourthvertofface.y -= (1 * 1.0f);

                                                                vertices[indexflat].Add(firstvertofface);
                                                                vertices[indexflat].Add(secondvertofface);
                                                                vertices[indexflat].Add(thirdvertofface);
                                                                vertices[indexflat].Add(fourthvertofface);

                                                                triangles[indexflat].Add(indexofvert0);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert3);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert1);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (IsTransparent(xmap - 1, ymap , zmap, null))
                                                        {
                                                            if (facetype == 1)
                                                            {
                                                                int indexofvert0 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                float swapx = firstvertofface.x;
                                                                float swapy = firstvertofface.y;
                                                                float swapz = firstvertofface.z;

                                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                secondvertofface.x -= (1 * 1.0f);
                                                                secondvertofface.y -= (1 * 1.0f);

                                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                fourthvertofface.x -= (1 * 1.0f);
                                                                fourthvertofface.y -= (1 * 1.0f);

                                                                vertices[indexflat].Add(firstvertofface);
                                                                vertices[indexflat].Add(secondvertofface);
                                                                vertices[indexflat].Add(thirdvertofface);
                                                                vertices[indexflat].Add(fourthvertofface);

                                                                triangles[indexflat].Add(indexofvert0);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert3);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert1);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (IsTransparent(xmap - 1, ymap, zmap, null))
                                                    {
                                                        if (facetype == 1)
                                                        {
                                                            int indexofvert0 = vertices[indexflat].Count;
                                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            float swapx = firstvertofface.x;
                                                            float swapy = firstvertofface.y;
                                                            float swapz = firstvertofface.z;

                                                            int indexofvert1 = vertices[indexflat].Count + 1;
                                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            secondvertofface.x -= (1 * 1.0f);
                                                            secondvertofface.y -= (1 * 1.0f);

                                                            int indexofvert2 = vertices[indexflat].Count + 2;
                                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            int indexofvert3 = vertices[indexflat].Count + 3;
                                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            fourthvertofface.x -= (1 * 1.0f);
                                                            fourthvertofface.y -= (1 * 1.0f);

                                                            vertices[indexflat].Add(firstvertofface);
                                                            vertices[indexflat].Add(secondvertofface);
                                                            vertices[indexflat].Add(thirdvertofface);
                                                            vertices[indexflat].Add(fourthvertofface);

                                                            triangles[indexflat].Add(indexofvert0);
                                                            triangles[indexflat].Add(indexofvert1);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert3);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert1);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap - 1, ymap, zmap, null))
                                                {
                                                    if (facetype == 1)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        secondvertofface.x -= (1 * 1.0f);
                                                        secondvertofface.y -= (1 * 1.0f);

                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.x -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);

                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (extremityiswl == 0)
                                    {

                                        var planetdivtothetop = sccschunkfacesbuilder.instance.getplanetdiv((int)(mindexx - 1), (int)(mindexy ), (int)(mindexz));

                                        if (planetdivtothetop != null)
                                        {



                                            if (sextremityxl == 1)
                                            {
                                                if (xmap == 0) //mapy - 1
                                                {
                                                    var planetdivchunk = planetdivtothetop.getChunk(schunkwr,y , z);

                                                    if (planetdivchunk != null)
                                                    {
                                                        if (IsTransparent(mapx - 1, ymap, zmap, planetdivchunk.thebytemap))
                                                        {
                                                            if (facetype == 1)
                                                            {
                                                                int indexofvert0 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                float swapx = firstvertofface.x;
                                                                float swapy = firstvertofface.y;
                                                                float swapz = firstvertofface.z;

                                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                secondvertofface.x -= (1 * 1.0f);
                                                                secondvertofface.y -= (1 * 1.0f);

                                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                fourthvertofface.x -= (1 * 1.0f);
                                                                fourthvertofface.y -= (1 * 1.0f);

                                                                vertices[indexflat].Add(firstvertofface);
                                                                vertices[indexflat].Add(secondvertofface);
                                                                vertices[indexflat].Add(thirdvertofface);
                                                                vertices[indexflat].Add(fourthvertofface);

                                                                triangles[indexflat].Add(indexofvert0);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert3);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert1);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (IsTransparent(xmap - 1, ymap , zmap, null))
                                                    {
                                                        if (facetype == 1)
                                                        {
                                                            int indexofvert0 = vertices[indexflat].Count;
                                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            float swapx = firstvertofface.x;
                                                            float swapy = firstvertofface.y;
                                                            float swapz = firstvertofface.z;

                                                            int indexofvert1 = vertices[indexflat].Count + 1;
                                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            secondvertofface.x -= (1 * 1.0f);
                                                            secondvertofface.y -= (1 * 1.0f);

                                                            int indexofvert2 = vertices[indexflat].Count + 2;
                                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            int indexofvert3 = vertices[indexflat].Count + 3;
                                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            fourthvertofface.x -= (1 * 1.0f);
                                                            fourthvertofface.y -= (1 * 1.0f);

                                                            vertices[indexflat].Add(firstvertofface);
                                                            vertices[indexflat].Add(secondvertofface);
                                                            vertices[indexflat].Add(thirdvertofface);
                                                            vertices[indexflat].Add(fourthvertofface);

                                                            triangles[indexflat].Add(indexofvert0);
                                                            triangles[indexflat].Add(indexofvert1);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert3);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert1);
                                                        }
                                                    }
                                                }

                                                //Destroy(planetdivtothetop.transform.gameObject);
                                                //planetdivtothetop.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap - 1, ymap , zmap, null))
                                                {
                                                    if (facetype == 1)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        secondvertofface.x -= (1 * 1.0f);
                                                        secondvertofface.y -= (1 * 1.0f);

                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.x -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);

                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //LEFTFACE
                                    //LEFTFACE
                                    //LEFTFACE






















                                    //FRONTFACE
                                    //FRONTFACE
                                    //FRONTFACE
                                    if (extremityisdr == 1)
                                    {
                                        var planetdivtothetop = sccschunkfacesbuilder.instance.getplanetdiv((int)(mindexx), (int)(mindexy ), (int)(mindexz + 1));

                                        if (planetdivtothetop != null)
                                        {
                                            var planetdivchunk = planetdivtothetop.getChunk(x, y, -schunkwl);

                                            if (planetdivchunk != null)
                                            {
                                                if (IsTransparent(xmap, ymap, 0, planetdivchunk.thebytemap))
                                                {
                                                    if (facetype == 3)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;


                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                        swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;

       

                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                        thirdvertofface.z -= (1 * 1.0f);
                                                        thirdvertofface.y -= (1 * 1.0f);
                                                        swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.z -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);


                                                        swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;


                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap, ymap , zmap + 1, null))
                                                {
                                                    if (facetype == 3)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;


                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                        swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;



                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                        thirdvertofface.z -= (1 * 1.0f);
                                                        thirdvertofface.y -= (1 * 1.0f);
                                                        swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.z -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);


                                                        swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;


                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {


                                            if (sextremityzr == 1)
                                            {
                                                if (zmap == mapz - 1)
                                                {
                                                    var planetdivchunk = getChunk(x, y , z + 1);

                                                    if (planetdivchunk != null)
                                                    {
                                                        if (IsTransparent(xmap, ymap, 0, planetdivchunk.thebytemap))
                                                        {
                                                            if (facetype == 3)
                                                            {
                                                                int indexofvert0 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                float swapx = firstvertofface.x;
                                                                float swapy = firstvertofface.y;
                                                                float swapz = firstvertofface.z;


                                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                                swapx = secondvertofface.x;
                                                                swapy = secondvertofface.y;
                                                                swapz = secondvertofface.z;



                                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                                thirdvertofface.z -= (1 * 1.0f);
                                                                thirdvertofface.y -= (1 * 1.0f);
                                                                swapx = thirdvertofface.x;
                                                                swapy = thirdvertofface.y;
                                                                swapz = thirdvertofface.z;

                                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                fourthvertofface.z -= (1 * 1.0f);
                                                                fourthvertofface.y -= (1 * 1.0f);


                                                                swapx = fourthvertofface.x;
                                                                swapy = fourthvertofface.y;
                                                                swapz = fourthvertofface.z;


                                                                vertices[indexflat].Add(firstvertofface);
                                                                vertices[indexflat].Add(secondvertofface);
                                                                vertices[indexflat].Add(thirdvertofface);
                                                                vertices[indexflat].Add(fourthvertofface);

                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert0);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert3);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (IsTransparent(xmap, ymap , zmap + 1, null))
                                                        {
                                                            if (facetype == 3)
                                                            {
                                                                int indexofvert0 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                float swapx = firstvertofface.x;
                                                                float swapy = firstvertofface.y;
                                                                float swapz = firstvertofface.z;


                                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                                swapx = secondvertofface.x;
                                                                swapy = secondvertofface.y;
                                                                swapz = secondvertofface.z;



                                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                                thirdvertofface.z -= (1 * 1.0f);
                                                                thirdvertofface.y -= (1 * 1.0f);
                                                                swapx = thirdvertofface.x;
                                                                swapy = thirdvertofface.y;
                                                                swapz = thirdvertofface.z;

                                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                fourthvertofface.z -= (1 * 1.0f);
                                                                fourthvertofface.y -= (1 * 1.0f);


                                                                swapx = fourthvertofface.x;
                                                                swapy = fourthvertofface.y;
                                                                swapz = fourthvertofface.z;


                                                                vertices[indexflat].Add(firstvertofface);
                                                                vertices[indexflat].Add(secondvertofface);
                                                                vertices[indexflat].Add(thirdvertofface);
                                                                vertices[indexflat].Add(fourthvertofface);

                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert0);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert3);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (IsTransparent(xmap, ymap , zmap + 1, null))
                                                    {
                                                        if (facetype == 3)
                                                        {
                                                            int indexofvert0 = vertices[indexflat].Count;
                                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            float swapx = firstvertofface.x;
                                                            float swapy = firstvertofface.y;
                                                            float swapz = firstvertofface.z;


                                                            int indexofvert1 = vertices[indexflat].Count + 1;
                                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                            swapx = secondvertofface.x;
                                                            swapy = secondvertofface.y;
                                                            swapz = secondvertofface.z;



                                                            int indexofvert2 = vertices[indexflat].Count + 2;
                                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                            thirdvertofface.z -= (1 * 1.0f);
                                                            thirdvertofface.y -= (1 * 1.0f);
                                                            swapx = thirdvertofface.x;
                                                            swapy = thirdvertofface.y;
                                                            swapz = thirdvertofface.z;

                                                            int indexofvert3 = vertices[indexflat].Count + 3;
                                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            fourthvertofface.z -= (1 * 1.0f);
                                                            fourthvertofface.y -= (1 * 1.0f);


                                                            swapx = fourthvertofface.x;
                                                            swapy = fourthvertofface.y;
                                                            swapz = fourthvertofface.z;


                                                            vertices[indexflat].Add(firstvertofface);
                                                            vertices[indexflat].Add(secondvertofface);
                                                            vertices[indexflat].Add(thirdvertofface);
                                                            vertices[indexflat].Add(fourthvertofface);

                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert1);
                                                            triangles[indexflat].Add(indexofvert0);
                                                            triangles[indexflat].Add(indexofvert1);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert3);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap, ymap , zmap + 1, null))
                                                {
                                                    if (facetype == 3)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;


                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                        swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;



                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                        thirdvertofface.z -= (1 * 1.0f);
                                                        thirdvertofface.y -= (1 * 1.0f);
                                                        swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.z -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);


                                                        swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;


                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (extremityisdr == 0)
                                    {

                                        var planetdivtothetop = sccschunkfacesbuilder.instance.getplanetdiv((int)(mindexx), (int)(mindexy ), (int)(mindexz + 1));

                                        if (planetdivtothetop != null)
                                        {



                                            if (sextremityzr == 1)
                                            {
                                                if (zmap == mapz - 1)
                                                {
                                                    var planetdivchunk = planetdivtothetop.getChunk(x, y, -schunkdl);

                                                    if (planetdivchunk != null)
                                                    {
                                                        if (IsTransparent(xmap, ymap, 0, planetdivchunk.thebytemap))
                                                        {
                                                            if (facetype == 3)
                                                            {
                                                                int indexofvert0 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                float swapx = firstvertofface.x;
                                                                float swapy = firstvertofface.y;
                                                                float swapz = firstvertofface.z;


                                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                                swapx = secondvertofface.x;
                                                                swapy = secondvertofface.y;
                                                                swapz = secondvertofface.z;



                                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                                thirdvertofface.z -= (1 * 1.0f);
                                                                thirdvertofface.y -= (1 * 1.0f);
                                                                swapx = thirdvertofface.x;
                                                                swapy = thirdvertofface.y;
                                                                swapz = thirdvertofface.z;

                                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                fourthvertofface.z -= (1 * 1.0f);
                                                                fourthvertofface.y -= (1 * 1.0f);


                                                                swapx = fourthvertofface.x;
                                                                swapy = fourthvertofface.y;
                                                                swapz = fourthvertofface.z;


                                                                vertices[indexflat].Add(firstvertofface);
                                                                vertices[indexflat].Add(secondvertofface);
                                                                vertices[indexflat].Add(thirdvertofface);
                                                                vertices[indexflat].Add(fourthvertofface);

                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert0);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert3);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (IsTransparent(xmap, ymap , zmap + 1, null))
                                                    {
                                                        if (facetype == 3)
                                                        {
                                                            int indexofvert0 = vertices[indexflat].Count;
                                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            float swapx = firstvertofface.x;
                                                            float swapy = firstvertofface.y;
                                                            float swapz = firstvertofface.z;


                                                            int indexofvert1 = vertices[indexflat].Count + 1;
                                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                            swapx = secondvertofface.x;
                                                            swapy = secondvertofface.y;
                                                            swapz = secondvertofface.z;



                                                            int indexofvert2 = vertices[indexflat].Count + 2;
                                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                            thirdvertofface.z -= (1 * 1.0f);
                                                            thirdvertofface.y -= (1 * 1.0f);
                                                            swapx = thirdvertofface.x;
                                                            swapy = thirdvertofface.y;
                                                            swapz = thirdvertofface.z;

                                                            int indexofvert3 = vertices[indexflat].Count + 3;
                                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            fourthvertofface.z -= (1 * 1.0f);
                                                            fourthvertofface.y -= (1 * 1.0f);


                                                            swapx = fourthvertofface.x;
                                                            swapy = fourthvertofface.y;
                                                            swapz = fourthvertofface.z;


                                                            vertices[indexflat].Add(firstvertofface);
                                                            vertices[indexflat].Add(secondvertofface);
                                                            vertices[indexflat].Add(thirdvertofface);
                                                            vertices[indexflat].Add(fourthvertofface);

                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert1);
                                                            triangles[indexflat].Add(indexofvert0);
                                                            triangles[indexflat].Add(indexofvert1);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert3);
                                                        }
                                                    }
                                                }

                                                //Destroy(planetdivtothetop.transform.gameObject);
                                                //planetdivtothetop.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap, ymap , zmap + 1, null))
                                                {
                                                    if (facetype == 3)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;


                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                        swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;



                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                        thirdvertofface.z -= (1 * 1.0f);
                                                        thirdvertofface.y -= (1 * 1.0f);
                                                        swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.z -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);


                                                        swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;


                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //FRONTFACE
                                    //FRONTFACE
                                    //FRONTFACE














































                                    //BACKFACE
                                    //BACKFACE
                                    //BACKFACE
                                    if (extremityisdl == 1)
                                    {
                                        var planetdivtothetop = sccschunkfacesbuilder.instance.getplanetdiv((int)(mindexx), (int)(mindexy ), (int)(mindexz - 1));

                                        if (planetdivtothetop != null)
                                        {
                                            var planetdivchunk = planetdivtothetop.getChunk(x, y, schunkdr);

                                            if (planetdivchunk != null)
                                            {
                                                if (IsTransparent(xmap, ymap, mapz - 1, planetdivchunk.thebytemap))
                                                {
                                                    if (facetype == 4)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                        swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;

                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                        thirdvertofface.z -= (1 * 1.0f);
                                                        thirdvertofface.y -= (1 * 1.0f);
                                                        swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.z -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);


                                                        swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;

                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap, ymap , zmap - 1, null))
                                                {
                                                    if (facetype == 4)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                        swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;

                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                        thirdvertofface.z -= (1 * 1.0f);
                                                        thirdvertofface.y -= (1 * 1.0f);
                                                        swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.z -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);


                                                        swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;

                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {


                                            if (sextremityzl == 1)
                                            {
                                                if (zmap == 0) //mapy - 1
                                                {
                                                    var planetdivchunk = getChunk(x, y , z - 1);

                                                    if (planetdivchunk != null)
                                                    {
                                                        if (IsTransparent(xmap, ymap, mapz - 1, planetdivchunk.thebytemap))
                                                        {
                                                            if (facetype == 4)
                                                            {
                                                                int indexofvert0 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                float swapx = firstvertofface.x;
                                                                float swapy = firstvertofface.y;
                                                                float swapz = firstvertofface.z;

                                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                                swapx = secondvertofface.x;
                                                                swapy = secondvertofface.y;
                                                                swapz = secondvertofface.z;

                                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                                thirdvertofface.z -= (1 * 1.0f);
                                                                thirdvertofface.y -= (1 * 1.0f);
                                                                swapx = thirdvertofface.x;
                                                                swapy = thirdvertofface.y;
                                                                swapz = thirdvertofface.z;

                                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                fourthvertofface.z -= (1 * 1.0f);
                                                                fourthvertofface.y -= (1 * 1.0f);


                                                                swapx = fourthvertofface.x;
                                                                swapy = fourthvertofface.y;
                                                                swapz = fourthvertofface.z;

                                                                vertices[indexflat].Add(firstvertofface);
                                                                vertices[indexflat].Add(secondvertofface);
                                                                vertices[indexflat].Add(thirdvertofface);
                                                                vertices[indexflat].Add(fourthvertofface);

                                                                triangles[indexflat].Add(indexofvert0);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert3);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert1);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (IsTransparent(xmap, ymap , zmap - 1, null))
                                                        {
                                                            if (facetype == 4)
                                                            {
                                                                int indexofvert0 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                float swapx = firstvertofface.x;
                                                                float swapy = firstvertofface.y;
                                                                float swapz = firstvertofface.z;

                                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                                swapx = secondvertofface.x;
                                                                swapy = secondvertofface.y;
                                                                swapz = secondvertofface.z;

                                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                                thirdvertofface.z -= (1 * 1.0f);
                                                                thirdvertofface.y -= (1 * 1.0f);
                                                                swapx = thirdvertofface.x;
                                                                swapy = thirdvertofface.y;
                                                                swapz = thirdvertofface.z;

                                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                fourthvertofface.z -= (1 * 1.0f);
                                                                fourthvertofface.y -= (1 * 1.0f);


                                                                swapx = fourthvertofface.x;
                                                                swapy = fourthvertofface.y;
                                                                swapz = fourthvertofface.z;

                                                                vertices[indexflat].Add(firstvertofface);
                                                                vertices[indexflat].Add(secondvertofface);
                                                                vertices[indexflat].Add(thirdvertofface);
                                                                vertices[indexflat].Add(fourthvertofface);

                                                                triangles[indexflat].Add(indexofvert0);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert3);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert1);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (IsTransparent(xmap, ymap , zmap - 1, null))
                                                    {
                                                        if (facetype == 4)
                                                        {
                                                            int indexofvert0 = vertices[indexflat].Count;
                                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            float swapx = firstvertofface.x;
                                                            float swapy = firstvertofface.y;
                                                            float swapz = firstvertofface.z;

                                                            int indexofvert1 = vertices[indexflat].Count + 1;
                                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                            swapx = secondvertofface.x;
                                                            swapy = secondvertofface.y;
                                                            swapz = secondvertofface.z;

                                                            int indexofvert2 = vertices[indexflat].Count + 2;
                                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                            thirdvertofface.z -= (1 * 1.0f);
                                                            thirdvertofface.y -= (1 * 1.0f);
                                                            swapx = thirdvertofface.x;
                                                            swapy = thirdvertofface.y;
                                                            swapz = thirdvertofface.z;

                                                            int indexofvert3 = vertices[indexflat].Count + 3;
                                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            fourthvertofface.z -= (1 * 1.0f);
                                                            fourthvertofface.y -= (1 * 1.0f);


                                                            swapx = fourthvertofface.x;
                                                            swapy = fourthvertofface.y;
                                                            swapz = fourthvertofface.z;

                                                            vertices[indexflat].Add(firstvertofface);
                                                            vertices[indexflat].Add(secondvertofface);
                                                            vertices[indexflat].Add(thirdvertofface);
                                                            vertices[indexflat].Add(fourthvertofface);

                                                            triangles[indexflat].Add(indexofvert0);
                                                            triangles[indexflat].Add(indexofvert1);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert3);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert1);
                                                        }
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap, ymap, zmap - 1, null))
                                                {
                                                    if (facetype == 4)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                        swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;

                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                        thirdvertofface.z -= (1 * 1.0f);
                                                        thirdvertofface.y -= (1 * 1.0f);
                                                        swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.z -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);


                                                        swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;

                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    else if (extremityisdl == 0)
                                    {

                                        var planetdivtothetop = sccschunkfacesbuilder.instance.getplanetdiv((int)(mindexx), (int)(mindexy), (int)(mindexz - 1));

                                        if (planetdivtothetop != null)
                                        {



                                            if (sextremityzl == 1)
                                            {
                                                if (zmap == 0) //mapy - 1
                                                {
                                                    var planetdivchunk = planetdivtothetop.getChunk(x, y, schunkdr);

                                                    if (planetdivchunk != null)
                                                    {
                                                        if (IsTransparent(xmap,ymap, mapz - 1, planetdivchunk.thebytemap))
                                                        {
                                                            if (facetype == 4)
                                                            {
                                                                int indexofvert0 = vertices[indexflat].Count;
                                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                                float swapx = firstvertofface.x;
                                                                float swapy = firstvertofface.y;
                                                                float swapz = firstvertofface.z;

                                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                                swapx = secondvertofface.x;
                                                                swapy = secondvertofface.y;
                                                                swapz = secondvertofface.z;

                                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                                thirdvertofface.z -= (1 * 1.0f);
                                                                thirdvertofface.y -= (1 * 1.0f);
                                                                swapx = thirdvertofface.x;
                                                                swapy = thirdvertofface.y;
                                                                swapz = thirdvertofface.z;

                                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                                fourthvertofface.z -= (1 * 1.0f);
                                                                fourthvertofface.y -= (1 * 1.0f);


                                                                swapx = fourthvertofface.x;
                                                                swapy = fourthvertofface.y;
                                                                swapz = fourthvertofface.z;

                                                                vertices[indexflat].Add(firstvertofface);
                                                                vertices[indexflat].Add(secondvertofface);
                                                                vertices[indexflat].Add(thirdvertofface);
                                                                vertices[indexflat].Add(fourthvertofface);

                                                                triangles[indexflat].Add(indexofvert0);
                                                                triangles[indexflat].Add(indexofvert1);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert3);
                                                                triangles[indexflat].Add(indexofvert2);
                                                                triangles[indexflat].Add(indexofvert1);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (IsTransparent(xmap, ymap , zmap - 1, null))
                                                    {
                                                        if (facetype == 4)
                                                        {
                                                            int indexofvert0 = vertices[indexflat].Count;
                                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                            float swapx = firstvertofface.x;
                                                            float swapy = firstvertofface.y;
                                                            float swapz = firstvertofface.z;

                                                            int indexofvert1 = vertices[indexflat].Count + 1;
                                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                            swapx = secondvertofface.x;
                                                            swapy = secondvertofface.y;
                                                            swapz = secondvertofface.z;

                                                            int indexofvert2 = vertices[indexflat].Count + 2;
                                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                            thirdvertofface.z -= (1 * 1.0f);
                                                            thirdvertofface.y -= (1 * 1.0f);
                                                            swapx = thirdvertofface.x;
                                                            swapy = thirdvertofface.y;
                                                            swapz = thirdvertofface.z;

                                                            int indexofvert3 = vertices[indexflat].Count + 3;
                                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                            fourthvertofface.z -= (1 * 1.0f);
                                                            fourthvertofface.y -= (1 * 1.0f);


                                                            swapx = fourthvertofface.x;
                                                            swapy = fourthvertofface.y;
                                                            swapz = fourthvertofface.z;

                                                            vertices[indexflat].Add(firstvertofface);
                                                            vertices[indexflat].Add(secondvertofface);
                                                            vertices[indexflat].Add(thirdvertofface);
                                                            vertices[indexflat].Add(fourthvertofface);

                                                            triangles[indexflat].Add(indexofvert0);
                                                            triangles[indexflat].Add(indexofvert1);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert3);
                                                            triangles[indexflat].Add(indexofvert2);
                                                            triangles[indexflat].Add(indexofvert1);
                                                        }
                                                    }
                                                }

                                                //Destroy(planetdivtothetop.transform.gameObject);
                                                //planetdivtothetop.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                                            }
                                            else
                                            {
                                                if (IsTransparent(xmap, ymap , zmap - 1, null))
                                                {
                                                    if (facetype == 4)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                        swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;

                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                        thirdvertofface.z -= (1 * 1.0f);
                                                        thirdvertofface.y -= (1 * 1.0f);
                                                        swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        fourthvertofface.z -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);


                                                        swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;

                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    //BACKFACE
                                    //BACKFACE
                                    //BACKFACE




















                                    /*
                                    ////TOPFACE
                                    ////TOPFACE
                                    ////TOPFACE
                                    if (extremityishr == 1)
                                    {
                                        /*var planetdivtothetop = sccschunkfacesbuilder.instance.getplanetdiv((int)(mindexx), (int)(mindexy + 1), (int)(mindexz));

                                        if (planetdivtothetop != null)
                                        {

                                        }
                                        else
                                        {

                                        }

                                        var planetdivtop = sccschunkfacesbuilder.instance.getplanetdiv((int)(mindexx), (int)(0), (int)(mindexz));

                                        //Debug.Log("chunk top check");

                                        if (planetdivtop != null)
                                        {
                                            //float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                                            //float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                                            //float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

                                            var planetdivchunk = planetdivtop.getChunk(x, y + 1, z);

                                            if (planetdivchunk != null)
                                            {
                                                //Debug.Log("! null but map is null");

                                                /*var comp = planetdivchunk.;

                                                if (comp != null)
                                                {
                                                   
                                                }

                                                if (planetdivtop.IsTransparent(xmap, 0, zmap, planetdivchunk.thebytemap))
                                                {
                                                    if (facetype == 0)
                                                    {
                                                        int indexofvert0 = vertices[indexflat].Count;
                                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        int indexofvert1 = vertices[indexflat].Count + 1;
                                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                        int indexofvert2 = vertices[indexflat].Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                        int indexofvert3 = vertices[indexflat].Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));


                                                        vertices[indexflat].Add(firstvertofface);
                                                        vertices[indexflat].Add(secondvertofface);
                                                        vertices[indexflat].Add(thirdvertofface);
                                                        vertices[indexflat].Add(fourthvertofface);

                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert0);
                                                        triangles[indexflat].Add(indexofvert1);
                                                        triangles[indexflat].Add(indexofvert2);
                                                        triangles[indexflat].Add(indexofvert3);

                                                    }
                                                }
                                            }












                                        }
                                        else
                                        {
                                            int indexofvert0 = vertices[indexflat].Count;
                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                            int indexofvert1 = vertices[indexflat].Count + 1;
                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                            int indexofvert2 = vertices[indexflat].Count + 2;
                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                            int indexofvert3 = vertices[indexflat].Count + 3;
                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));


                                            vertices[indexflat].Add(firstvertofface);
                                            vertices[indexflat].Add(secondvertofface);
                                            vertices[indexflat].Add(thirdvertofface);
                                            vertices[indexflat].Add(fourthvertofface);

                                            triangles[indexflat].Add(indexofvert2);
                                            triangles[indexflat].Add(indexofvert1);
                                            triangles[indexflat].Add(indexofvert0);
                                            triangles[indexflat].Add(indexofvert1);
                                            triangles[indexflat].Add(indexofvert2);
                                            triangles[indexflat].Add(indexofvert3);



                                            /*
                                            var planetdivchunk = getChunk(x, y + 0, z);
                                            if (planetdivtop.IsTransparent(xmap, ymap + 1, zmap, planetdivchunk.thebytemap))
                                            {
                                                if (facetype == 0)
                                                {
                                                    int indexofvert0 = vertices[indexflat].Count;
                                                    Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                    int indexofvert1 = vertices[indexflat].Count + 1;
                                                    Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                    int indexofvert2 = vertices[indexflat].Count + 2;
                                                    Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                    int indexofvert3 = vertices[indexflat].Count + 3;
                                                    Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));


                                                    vertices[indexflat].Add(firstvertofface);
                                                    vertices[indexflat].Add(secondvertofface);
                                                    vertices[indexflat].Add(thirdvertofface);
                                                    vertices[indexflat].Add(fourthvertofface);

                                                    triangles[indexflat].Add(indexofvert2);
                                                    triangles[indexflat].Add(indexofvert1);
                                                    triangles[indexflat].Add(indexofvert0);
                                                    triangles[indexflat].Add(indexofvert1);
                                                    triangles[indexflat].Add(indexofvert2);
                                                    triangles[indexflat].Add(indexofvert3);

                                                }
                                            }
                                            */



                                    /*
                                    var planetdivchunk = getChunk(x, y+0, z);

                                    if (planetdivchunk != null)
                                    {
                                        //Debug.Log("! null but map is null");

                                        /*var comp = planetdivchunk.;

                                        if (comp != null)
                                        {

                                        }

                                        if (planetdivtop.IsTransparent(xmap, ymap + 1, zmap, planetdivchunk.thebytemap))
                                        {
                                            if (facetype == 0)
                                            {
                                                int indexofvert0 = vertices[indexflat].Count;
                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));


                                                vertices[indexflat].Add(firstvertofface);
                                                vertices[indexflat].Add(secondvertofface);
                                                vertices[indexflat].Add(thirdvertofface);
                                                vertices[indexflat].Add(fourthvertofface);

                                                triangles[indexflat].Add(indexofvert2);
                                                triangles[indexflat].Add(indexofvert1);
                                                triangles[indexflat].Add(indexofvert0);
                                                triangles[indexflat].Add(indexofvert1);
                                                triangles[indexflat].Add(indexofvert2);
                                                triangles[indexflat].Add(indexofvert3);

                                            }
                                        }
                                    }

                                }
                            }
                            else
                            {

                                /*int indexofvert0 = vertices[indexflat].Count;
                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                int indexofvert1 = vertices[indexflat].Count + 1;
                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                int indexofvert2 = vertices[indexflat].Count + 2;
                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                int indexofvert3 = vertices[indexflat].Count + 3;
                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));


                                vertices[indexflat].Add(firstvertofface);
                                vertices[indexflat].Add(secondvertofface);
                                vertices[indexflat].Add(thirdvertofface);
                                vertices[indexflat].Add(fourthvertofface);

                                triangles[indexflat].Add(indexofvert2);
                                triangles[indexflat].Add(indexofvert1);
                                triangles[indexflat].Add(indexofvert0);
                                triangles[indexflat].Add(indexofvert1);
                                triangles[indexflat].Add(indexofvert2);
                                triangles[indexflat].Add(indexofvert3);*/


                                    /*
                                    var planetdiv = sccschunkfacesbuilder.instance.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz));

                                    if (planetdiv != null)
                                    {
                                        float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                                        float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                                        float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

                                        //var planetdivchunk = planetdivright.getChunk();

                                        var chunkdata = planetdiv.getChunk((int)(mainChunk.indexposx), (int)(mainChunk.indexposy + 1), (int)(mainChunk.indexposz));
                                        if (chunkdata != null)
                                        {
                                            var comp = chunkdata.sccsplanetchunkrev12;

                                            if (comp != null)
                                            {
                                                if (comp.IsTransparent(x, 0, z, chunkdata))
                                                {
                                                    offset1 = Vector3.forward * planeSize;
                                                    offset2 = Vector3.right * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.up * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //RIGHTFACE
                                            if (IsTransparent(x, y + 1, z, mainChunk))
                                            {
                                                offset1 = Vector3.forward * planeSize;
                                                offset2 = Vector3.right * planeSize;
                                                mainChunk = DrawFace(start + Vector3.up * planeSize, offset1, offset2, mainChunk);
                                            }
                                        }
                                    }
                                }
                                ////TOPFACE
                                ////TOPFACE
                                ////TOPFACE

                                */













































                                    /*
                                    //RIGHTFACE
                                    //RIGHTFACE
                                    //RIGHTFACE
                                    if (mainChunk.extremitywr == 1)
                                    {

                                        var planetdivright = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx + 1), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz));

                                        if (planetdivright != null)
                                        {
                                            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                                            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                                            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;


                                            //var planetdivchunk = planetdivright.getChunk();

                                            var chunkdata = planetdivright.getChunk((int)(0), (int)(mainChunk.indexposy), (int)(mainChunk.indexposz));
                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(0, y, z, chunkdata))
                                                    {
                                                        offset1 = Vector3.up * planeSize;
                                                        offset2 = Vector3.forward * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //RIGHTFACE
                                                if (IsTransparent(x + 1, y, z, mainChunk))
                                                {
                                                    offset1 = Vector3.up * planeSize;
                                                    offset2 = Vector3.forward * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }


                                        }
                                        else
                                        {
                                            var planetdiv = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz));

                                            var chunkdata = planetdiv.getChunk((int)(mainChunk.indexposx + 1), (int)(mainChunk.indexposy), (int)(mainChunk.indexposz));
                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(0, y, z, chunkdata))
                                                    {
                                                        offset1 = Vector3.up * planeSize;
                                                        offset2 = Vector3.forward * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //RIGHTFACE
                                                if (IsTransparent(x + 1, y, z, mainChunk))
                                                {
                                                    offset1 = Vector3.up * planeSize;
                                                    offset2 = Vector3.forward * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }
                                        }


                                    }
                                    else
                                    {

                                        var planetdiv = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz));

                                        if (planetdiv != null)
                                        {
                                            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                                            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                                            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;


                                            //var planetdivchunk = planetdivright.getChunk();

                                            var chunkdata = planetdiv.getChunk((int)(mainChunk.indexposx + 1), (int)(mainChunk.indexposy), (int)(mainChunk.indexposz));
                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(0, y, z, chunkdata))
                                                    {
                                                        offset1 = Vector3.up * planeSize;
                                                        offset2 = Vector3.forward * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //RIGHTFACE
                                                if (IsTransparent(x + 1, y, z, mainChunk))
                                                {
                                                    offset1 = Vector3.up * planeSize;
                                                    offset2 = Vector3.forward * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }
                                        }
                                    }
                                    //RIGHTFACE
                                    //RIGHTFACE
                                    //RIGHTFACE



                                    //LEFTFACE
                                    //LEFTFACE
                                    //LEFTFACE
                                    if (mainChunk.extremitywl == 1)
                                    {

                                        var planetdivright = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx - 1), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz));

                                        if (planetdivright != null)
                                        {
                                            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                                            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                                            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;


                                            //var planetdivchunk = planetdivright.getChunk();

                                            var chunkdata = planetdivright.getChunk((int)(sccsproceduralplanetbuilderrev12.sccsproceduralplanetbuilderrev12script.ChunkWidth_R), (int)(mainChunk.indexposy), (int)(mainChunk.indexposz));
                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(width - 1, y, z, chunkdata))
                                                    {
                                                        offset1 = Vector3.back * planeSize;
                                                        offset2 = Vector3.down * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //RIGHTFACE
                                                if (IsTransparent(x - 1, y, z, mainChunk))
                                                {
                                                    offset1 = Vector3.back * planeSize;
                                                    offset2 = Vector3.down * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }


                                        }
                                        else
                                        {
                                            var planetdiv = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz));

                                            var chunkdata = planetdiv.getChunk((int)(mainChunk.indexposx - 1), (int)(mainChunk.indexposy), (int)(mainChunk.indexposz));
                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(width - 1, y, z, chunkdata))
                                                    {
                                                        offset1 = Vector3.back * planeSize;
                                                        offset2 = Vector3.down * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //LEFTFACE
                                                if (IsTransparent(x - 1, y, z, mainChunk))
                                                {
                                                    offset1 = Vector3.back * planeSize;
                                                    offset2 = Vector3.down * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }
                                        }


                                    }
                                    else
                                    {

                                        var planetdiv = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz));

                                        if (planetdiv != null)
                                        {
                                            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                                            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                                            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;


                                            //var planetdivchunk = planetdivright.getChunk();

                                            var chunkdata = planetdiv.getChunk((int)(mainChunk.indexposx - 1), (int)(mainChunk.indexposy), (int)(mainChunk.indexposz));
                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(width - 1, y, z, chunkdata))
                                                    {
                                                        offset1 = Vector3.back * planeSize;
                                                        offset2 = Vector3.down * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //LEFTFACE
                                                if (IsTransparent(x - 1, y, z, mainChunk))
                                                {
                                                    offset1 = Vector3.back * planeSize;
                                                    offset2 = Vector3.down * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }
                                        }
                                    }
                                    //LEFTFACE
                                    //LEFTFACE
                                    //LEFTFACE




                                    //FRONTFACE
                                    //FRONTFACE
                                    //FRONTFACE
                                    if (mainChunk.extremitydl == 1)
                                    {

                                        var planetdivright = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz - 1));

                                        if (planetdivright != null)
                                        {
                                            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                                            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                                            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;


                                            //var planetdivchunk = planetdivright.getChunk();
                                            sccsproceduralplanetbuilderrev12.mainChunk chunkdata = sccsproceduralplanetbuilderrev12.sccsproceduralplanetbuilderrev12script.getChunk((int)(mainChunk.indexposx), (int)(mainChunk.indexposy), (int)(sccsproceduralplanetbuilderrev12.sccsproceduralplanetbuilderrev12script.ChunkDepth_R));

                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(x, y, depth - 1, chunkdata))
                                                    {
                                                        offset1 = Vector3.left * planeSize;
                                                        offset2 = Vector3.up * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                                else
                                                {
                                                    //FRONTFACE
                                                    if (IsTransparent(x, y, z - 1, mainChunk))
                                                    {
                                                        offset1 = Vector3.left * planeSize;
                                                        offset2 = Vector3.up * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //FRONTFACE
                                                if (IsTransparent(x, y, z - 1, mainChunk))
                                                {
                                                    offset1 = Vector3.left * planeSize;
                                                    offset2 = Vector3.up * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }


                                        }
                                        else
                                        {
                                            var planetdiv = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz));
                                            sccsproceduralplanetbuilderrev12.mainChunk chunkdata = sccsproceduralplanetbuilderrev12.sccsproceduralplanetbuilderrev12script.getChunk((int)(mainChunk.indexposx), (int)(mainChunk.indexposy), (int)(mainChunk.indexposz - 1));

                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(x, y, depth - 1, chunkdata))
                                                    {
                                                        offset1 = Vector3.left * planeSize;
                                                        offset2 = Vector3.up * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                                else
                                                {
                                                    //FRONTFACE
                                                    if (IsTransparent(x, y, z - 1, mainChunk))
                                                    {
                                                        offset1 = Vector3.left * planeSize;
                                                        offset2 = Vector3.up * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //FRONTFACE
                                                if (IsTransparent(x, y, z - 1, mainChunk))
                                                {
                                                    offset1 = Vector3.left * planeSize;
                                                    offset2 = Vector3.up * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }
                                        }


                                    }
                                    else
                                    {

                                        var planetdiv = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz));
                                        sccsproceduralplanetbuilderrev12.mainChunk chunkdata = sccsproceduralplanetbuilderrev12.sccsproceduralplanetbuilderrev12script.getChunk((int)(mainChunk.indexposx), (int)(mainChunk.indexposy), (int)(mainChunk.indexposz - 1));

                                        if (planetdiv != null)
                                        {
                                            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                                            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                                            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;


                                            //var planetdivchunk = planetdivright.getChunk();

                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(x, y, depth - 1, chunkdata))
                                                    {
                                                        offset1 = Vector3.left * planeSize;
                                                        offset2 = Vector3.up * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                                else
                                                {
                                                    //FRONTFACE
                                                    if (IsTransparent(x, y, z - 1, mainChunk))
                                                    {
                                                        offset1 = Vector3.left * planeSize;
                                                        offset2 = Vector3.up * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //FRONTFACE
                                                if (IsTransparent(x, y, z - 1, mainChunk))
                                                {
                                                    offset1 = Vector3.left * planeSize;
                                                    offset2 = Vector3.up * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }
                                        }
                                    }
                                    //FRONTFACE
                                    //FRONTFACE
                                    //FRONTFACE




                                    //BACKFACE
                                    //BACKFACE
                                    //BACKFACE
                                    if (mainChunk.extremitydr == 1)
                                    {
                                        var planetdivright = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz + 1));

                                        if (planetdivright != null)
                                        {
                                            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                                            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                                            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;


                                            //var planetdivchunk = planetdivright.getChunk();

                                            var chunkdata = planetdivright.getChunk((int)(mainChunk.indexposx), (int)(mainChunk.indexposy), (int)(0));
                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(x, y, 0, chunkdata))
                                                    {
                                                        offset1 = Vector3.right * planeSize;
                                                        offset2 = Vector3.up * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.forward * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //RIGHTFACE
                                                if (IsTransparent(x, y, z + 1, mainChunk))
                                                {
                                                    offset1 = Vector3.right * planeSize;
                                                    offset2 = Vector3.up * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.forward * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }


                                        }
                                        else
                                        {
                                            var planetdiv = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz));

                                            var chunkdata = planetdiv.getChunk((int)(mainChunk.indexposx), (int)(mainChunk.indexposy), (int)(mainChunk.indexposz + 1));
                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(x, y, 0, chunkdata))
                                                    {
                                                        offset1 = Vector3.right * planeSize;
                                                        offset2 = Vector3.up * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.forward * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //RIGHTFACE
                                                if (IsTransparent(x, y, z + 1, mainChunk))
                                                {
                                                    offset1 = Vector3.right * planeSize;
                                                    offset2 = Vector3.up * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.forward * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }
                                        }


                                    }
                                    else
                                    {

                                        var planetdiv = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz));

                                        if (planetdiv != null)
                                        {
                                            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                                            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                                            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

                                            //var planetdivchunk = planetdivright.getChunk();

                                            var chunkdata = planetdiv.getChunk((int)(mainChunk.indexposx), (int)(mainChunk.indexposy), (int)(mainChunk.indexposz + 1));
                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(x, y, 0, chunkdata))
                                                    {
                                                        offset1 = Vector3.right * planeSize;
                                                        offset2 = Vector3.up * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.forward * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //RIGHTFACE
                                                if (IsTransparent(x, y, z + 1, mainChunk))
                                                {
                                                    offset1 = Vector3.right * planeSize;
                                                    offset2 = Vector3.up * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.forward * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }
                                        }
                                    }
                                    //BACKFACE
                                    //BACKFACE
                                    //BACKFACE









                                    ////TOPFACE
                                    ////TOPFACE
                                    ////TOPFACE
                                    if (mainChunk.extremityhr == 1)
                                    {
                                        var planetdivright = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy + 1), (int)(mainChunk.mindexposz));

                                        if (planetdivright != null)
                                        {
                                            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                                            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                                            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;


                                            //var planetdivchunk = planetdivright.getChunk();

                                            var chunkdata = planetdivright.getChunk((int)(mainChunk.indexposx), (int)(0), (int)(mainChunk.indexposz));
                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(x, 0, z, chunkdata))
                                                    {
                                                        offset1 = Vector3.forward * planeSize;
                                                        offset2 = Vector3.right * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.up * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //RIGHTFACE
                                                if (IsTransparent(x, y + 1, z, mainChunk))
                                                {
                                                    offset1 = Vector3.forward * planeSize;
                                                    offset2 = Vector3.right * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.up * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }


                                        }
                                        else
                                        {
                                            var planetdiv = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz));

                                            var chunkdata = planetdiv.getChunk((int)(mainChunk.indexposx), (int)(mainChunk.indexposy + 1), (int)(mainChunk.indexposz));
                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(x, 0, z, chunkdata))
                                                    {
                                                        offset1 = Vector3.forward * planeSize;
                                                        offset2 = Vector3.right * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.up * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //RIGHTFACE
                                                if (IsTransparent(x, y + 1, z, mainChunk))
                                                {
                                                    offset1 = Vector3.forward * planeSize;
                                                    offset2 = Vector3.right * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.up * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }
                                        }


                                    }
                                    else
                                    {

                                        var planetdiv = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz));

                                        if (planetdiv != null)
                                        {
                                            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                                            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                                            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

                                            //var planetdivchunk = planetdivright.getChunk();

                                            var chunkdata = planetdiv.getChunk((int)(mainChunk.indexposx), (int)(mainChunk.indexposy + 1), (int)(mainChunk.indexposz));
                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(x, 0, z, chunkdata))
                                                    {
                                                        offset1 = Vector3.forward * planeSize;
                                                        offset2 = Vector3.right * planeSize;
                                                        mainChunk = DrawFace(start + Vector3.up * planeSize, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //RIGHTFACE
                                                if (IsTransparent(x, y + 1, z, mainChunk))
                                                {
                                                    offset1 = Vector3.forward * planeSize;
                                                    offset2 = Vector3.right * planeSize;
                                                    mainChunk = DrawFace(start + Vector3.up * planeSize, offset1, offset2, mainChunk);
                                                }
                                            }
                                        }
                                    }
                                    ////TOPFACE
                                    ////TOPFACE
                                    ////TOPFACE







                                    ////BOTTOMFACE
                                    ////BOTTOMFACE
                                    ////BOTTOMFACE
                                    if (mainChunk.extremityhl == 1)
                                    {

                                        var planetdivright = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy - 1), (int)(mainChunk.mindexposz));

                                        if (planetdivright != null)
                                        {
                                            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                                            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                                            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;


                                            //var planetdivchunk = planetdivright.getChunk();

                                            var chunkdata = planetdivright.getChunk((int)(mainChunk.indexposx), (int)(sccsproceduralplanetbuilderrev12.sccsproceduralplanetbuilderrev12script.ChunkHeight_R), (int)(mainChunk.indexposz));
                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(x, height - 1, z, chunkdata))
                                                    {
                                                        offset1 = Vector3.right * planeSize;
                                                        offset2 = Vector3.forward * planeSize;
                                                        mainChunk = DrawFace(start, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //RIGHTFACE
                                                if (IsTransparent(x, y - 1, z, mainChunk))
                                                {
                                                    offset1 = Vector3.right * planeSize;
                                                    offset2 = Vector3.forward * planeSize;
                                                    mainChunk = DrawFace(start, offset1, offset2, mainChunk);
                                                }
                                            }


                                        }
                                        else
                                        {
                                            var planetdiv = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz));

                                            var chunkdata = planetdiv.getChunk((int)(mainChunk.indexposx), (int)(mainChunk.indexposy - 1), (int)(mainChunk.indexposz));
                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(x, height - 1, z, chunkdata))
                                                    {
                                                        offset1 = Vector3.right * planeSize;
                                                        offset2 = Vector3.forward * planeSize;
                                                        mainChunk = DrawFace(start, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //LEFTFACE
                                                if (IsTransparent(x, y - 1, z, mainChunk))
                                                {
                                                    offset1 = Vector3.right * planeSize;
                                                    offset2 = Vector3.forward * planeSize;
                                                    mainChunk = DrawFace(start, offset1, offset2, mainChunk);
                                                }
                                            }
                                        }


                                    }
                                    else
                                    {

                                        var planetdiv = sccsplanetdivbuilder.currentsccsplanetbuilder.getplanetdiv((int)(mainChunk.mindexposx), (int)(mainChunk.mindexposy), (int)(mainChunk.mindexposz));

                                        if (planetdiv != null)
                                        {
                                            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                                            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                                            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;


                                            //var planetdivchunk = planetdivright.getChunk();

                                            var chunkdata = planetdiv.getChunk((int)(mainChunk.indexposx), (int)(mainChunk.indexposy - 1), (int)(mainChunk.indexposz));
                                            if (chunkdata != null)
                                            {
                                                var comp = chunkdata.sccsplanetchunkrev12;

                                                if (comp != null)
                                                {
                                                    if (comp.IsTransparent(x, height - 1, z, chunkdata))
                                                    {
                                                        offset1 = Vector3.right * planeSize;
                                                        offset2 = Vector3.forward * planeSize;
                                                        mainChunk = DrawFace(start, offset1, offset2, mainChunk);
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                //LEFTFACE
                                                if (IsTransparent(x, y - 1, z, mainChunk))
                                                {
                                                    offset1 = Vector3.right * planeSize;
                                                    offset2 = Vector3.forward * planeSize;
                                                    mainChunk = DrawFace(start, offset1, offset2, mainChunk);
                                                }
                                            }
                                        }
                                    }
                                    ////BOTTOMFACE
                                    ////BOTTOMFACE
                                    ////BOTTOMFACE
                                    */



















                                    int swtcforbuildfacestotal = 0;










                                    if (swtcforbuildfacestotal == 1)
                                    {


                                        //if (mapint[index] == 1)//IsTransparent(x, y+1,z, originalmapdata[mindex]) && blockExistsInArray(x, y - 1 ,z) == 1)// mapint[index] == 1) //mapdata[mindex]
                                        {


                                            if (facetype == 0)
                                            {
                                                int indexofvert0 = vertices[indexflat].Count;
                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));


                                                vertices[indexflat].Add(firstvertofface);
                                                vertices[indexflat].Add(secondvertofface);
                                                vertices[indexflat].Add(thirdvertofface);
                                                vertices[indexflat].Add(fourthvertofface);

                                                triangles[indexflat].Add(indexofvert2);
                                                triangles[indexflat].Add(indexofvert1);
                                                triangles[indexflat].Add(indexofvert0);
                                                triangles[indexflat].Add(indexofvert1);
                                                triangles[indexflat].Add(indexofvert2);
                                                triangles[indexflat].Add(indexofvert3);

                                            }
                                            else if (facetype == 1)
                                            {

                                                int indexofvert0 = vertices[indexflat].Count;
                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                float swapx = firstvertofface.x;
                                                float swapy = firstvertofface.y;
                                                float swapz = firstvertofface.z;

                                                //firstvertofface.x = swapy;
                                                //firstvertofface.y = datawidthdimtop[indexflat][index].thebyte;

                                                //firstvertofface.x -= (1 * 1.0f);
                                                //firstvertofface.y -= (1 * 1.0f);


                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                /*swapx = secondvertofface.x;
                                                swapy = secondvertofface.y;
                                                swapz = secondvertofface.z;

                                                secondvertofface.x = swapy;
                                                secondvertofface.y = datawidthdimtop[indexflat][index].thebyte; ///swapx;*/
                                                secondvertofface.x -= (1 * 1.0f);
                                                secondvertofface.y -= (1 * 1.0f);


                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                //thirdvertofface.x -= (1 * 1.0f);
                                                //thirdvertofface.y -= (1 * 1.0f);
                                                /*swapx = thirdvertofface.x;
                                                swapy = thirdvertofface.y;
                                                swapz = thirdvertofface.z;

                                                thirdvertofface.x = swapy;
                                                thirdvertofface.y = swapx;*/

                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                fourthvertofface.x -= (1 * 1.0f);
                                                fourthvertofface.y -= (1 * 1.0f);


                                                /*swapx = fourthvertofface.x;
                                                swapy = fourthvertofface.y;
                                                swapz = fourthvertofface.z;

                                                fourthvertofface.x = swapy;
                                                fourthvertofface.y = swapx;*/
                                                /*
                                                int indexofvert0 = vertices[indexflat].Count;
                                                Vector3 firstvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte, datamapfirstvertztop[indexflat][index].thebyte);

                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                Vector3 secondvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte + (datawidthdimtop[indexflat][index].thebyte), datamapfirstvertztop[indexflat][index].thebyte);

                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[indexflat][index].thebyte, dataheightdimtop[indexflat][index].thebyte, datamapfirstvertztop[indexflat][index].thebyte + (datadepthdimtop[indexflat][index].thebyte));

                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                Vector3 fourthvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte + (datawidthdimtop[indexflat][index].thebyte),datamapfirstvertztop[indexflat][index].thebyte + (datadepthdimtop[indexflat][index].thebyte));
                                                */



                                                vertices[indexflat].Add(firstvertofface);
                                                vertices[indexflat].Add(secondvertofface);
                                                vertices[indexflat].Add(thirdvertofface);
                                                vertices[indexflat].Add(fourthvertofface);

                                                triangles[indexflat].Add(indexofvert0);
                                                triangles[indexflat].Add(indexofvert1);
                                                triangles[indexflat].Add(indexofvert2);
                                                triangles[indexflat].Add(indexofvert3);
                                                triangles[indexflat].Add(indexofvert2);
                                                triangles[indexflat].Add(indexofvert1);
                                            }
                                            else if (facetype == 2)
                                            {

                                                int indexofvert0 = vertices[indexflat].Count;
                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                float swapx = firstvertofface.x;
                                                float swapy = firstvertofface.y;
                                                float swapz = firstvertofface.z;

                                                //firstvertofface.x = swapy;
                                                //firstvertofface.y = datawidthdimtop[indexflat][index].thebyte;

                                                //firstvertofface.x -= (1 * 1.0f);
                                                //firstvertofface.y -= (1 * 1.0f);


                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                /*swapx = secondvertofface.x;
                                                swapy = secondvertofface.y;
                                                swapz = secondvertofface.z;

                                                secondvertofface.x = swapy;
                                                secondvertofface.y = datawidthdimtop[indexflat][index].thebyte; ///swapx;*/
                                                secondvertofface.x -= (1 * 1.0f);
                                                secondvertofface.y -= (1 * 1.0f);


                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                //thirdvertofface.x -= (1 * 1.0f);
                                                //thirdvertofface.y -= (1 * 1.0f);
                                                /*swapx = thirdvertofface.x;
                                                swapy = thirdvertofface.y;
                                                swapz = thirdvertofface.z;

                                                thirdvertofface.x = swapy;
                                                thirdvertofface.y = swapx;*/

                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                fourthvertofface.x -= (1 * 1.0f);
                                                fourthvertofface.y -= (1 * 1.0f);


                                                /*swapx = fourthvertofface.x;
                                                swapy = fourthvertofface.y;
                                                swapz = fourthvertofface.z;

                                                fourthvertofface.x = swapy;
                                                fourthvertofface.y = swapx;*/
                                                /*
                                                int indexofvert0 = vertices[indexflat].Count;
                                                Vector3 firstvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte, datamapfirstvertztop[indexflat][index].thebyte);

                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                Vector3 secondvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte + (datawidthdimtop[indexflat][index].thebyte), datamapfirstvertztop[indexflat][index].thebyte);

                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[indexflat][index].thebyte, dataheightdimtop[indexflat][index].thebyte, datamapfirstvertztop[indexflat][index].thebyte + (datadepthdimtop[indexflat][index].thebyte));

                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                Vector3 fourthvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte + (datawidthdimtop[indexflat][index].thebyte),datamapfirstvertztop[indexflat][index].thebyte + (datadepthdimtop[indexflat][index].thebyte));
                                                */



                                                vertices[indexflat].Add(firstvertofface);
                                                vertices[indexflat].Add(secondvertofface);
                                                vertices[indexflat].Add(thirdvertofface);
                                                vertices[indexflat].Add(fourthvertofface);

                                                triangles[indexflat].Add(indexofvert2);
                                                triangles[indexflat].Add(indexofvert1);
                                                triangles[indexflat].Add(indexofvert0);
                                                triangles[indexflat].Add(indexofvert1);
                                                triangles[indexflat].Add(indexofvert2);
                                                triangles[indexflat].Add(indexofvert3);
                                            }
                                            else if (facetype == 3)
                                            {

                                                int indexofvert0 = vertices[indexflat].Count;
                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                float swapx = firstvertofface.x;
                                                float swapy = firstvertofface.y;
                                                float swapz = firstvertofface.z;

                                                //firstvertofface.x = swapy;
                                                //firstvertofface.y = datawidthdimtop[indexflat][index].thebyte;

                                                /*firstvertofface.z -= (1 * 1.0f);
                                                firstvertofface.y -= (1 * 1.0f);*/
                                                //firstvertofface.z = swapy;
                                                //firstvertofface.y = swapz;

                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                swapx = secondvertofface.x;
                                                swapy = secondvertofface.y;
                                                swapz = secondvertofface.z;

                                                /*secondvertofface.x = swapy;
                                                secondvertofface.y = datawidthdimtop[indexflat][index].thebyte; ///swapx;*/
                                                /*secondvertofface.x -= (1 * 1.0f);
                                                secondvertofface.y -= (1 * 1.0f);*/


                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                thirdvertofface.z -= (1 * 1.0f);
                                                thirdvertofface.y -= (1 * 1.0f);
                                                swapx = thirdvertofface.x;
                                                swapy = thirdvertofface.y;
                                                swapz = thirdvertofface.z;

                                                /*thirdvertofface.z = swapy;
                                                thirdvertofface.y = swapz;*/

                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

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
                                                int indexofvert0 = vertices[indexflat].Count;
                                                Vector3 firstvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte, datamapfirstvertztop[indexflat][index].thebyte);

                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                Vector3 secondvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte + (datawidthdimtop[indexflat][index].thebyte), datamapfirstvertztop[indexflat][index].thebyte);

                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[indexflat][index].thebyte, dataheightdimtop[indexflat][index].thebyte, datamapfirstvertztop[indexflat][index].thebyte + (datadepthdimtop[indexflat][index].thebyte));

                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                Vector3 fourthvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte + (datawidthdimtop[indexflat][index].thebyte),datamapfirstvertztop[indexflat][index].thebyte + (datadepthdimtop[indexflat][index].thebyte));
                                                */



                                                vertices[indexflat].Add(firstvertofface);
                                                vertices[indexflat].Add(secondvertofface);
                                                vertices[indexflat].Add(thirdvertofface);
                                                vertices[indexflat].Add(fourthvertofface);

                                                triangles[indexflat].Add(indexofvert2);
                                                triangles[indexflat].Add(indexofvert1);
                                                triangles[indexflat].Add(indexofvert0);
                                                triangles[indexflat].Add(indexofvert1);
                                                triangles[indexflat].Add(indexofvert2);
                                                triangles[indexflat].Add(indexofvert3);
                                            }
                                            else if (facetype == 4)
                                            {

                                                /*
                                                int indexofvert0 = vertices[indexflat].Count;
                                                Vector3 firstvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte,datamapfirstvertztop[indexflat][index].thebyte);

                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                Vector3 secondvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte + (datawidthdimtop[indexflat][index].thebyte), datamapfirstvertztop[indexflat][index].thebyte);

                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                Vector3 thirdvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte, datamapfirstvertztop[indexflat][index].thebyte + (datadepthdimtop[indexflat][index].thebyte));

                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                Vector3 fourthvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte + (datawidthdimtop[indexflat][index].thebyte), datamapfirstvertztop[indexflat][index].thebyte + (datadepthdimtop[indexflat][index].thebyte));
                                                */



                                                int indexofvert0 = vertices[indexflat].Count;
                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                float swapx = firstvertofface.x;
                                                float swapy = firstvertofface.y;
                                                float swapz = firstvertofface.z;

                                                //firstvertofface.x = swapy;
                                                //firstvertofface.y = datawidthdimtop[indexflat][index].thebyte;

                                                /*firstvertofface.z -= (1 * 1.0f);
                                                firstvertofface.y -= (1 * 1.0f);*/
                                                //firstvertofface.z = swapy;
                                                //firstvertofface.y = swapz;

                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                swapx = secondvertofface.x;
                                                swapy = secondvertofface.y;
                                                swapz = secondvertofface.z;

                                                /*secondvertofface.x = swapy;
                                                secondvertofface.y = datawidthdimtop[indexflat][index].thebyte; ///swapx;*/
                                                /*secondvertofface.x -= (1 * 1.0f);
                                                secondvertofface.y -= (1 * 1.0f);*/


                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                thirdvertofface.z -= (1 * 1.0f);
                                                thirdvertofface.y -= (1 * 1.0f);
                                                swapx = thirdvertofface.x;
                                                swapy = thirdvertofface.y;
                                                swapz = thirdvertofface.z;

                                                /*thirdvertofface.z = swapy;
                                                thirdvertofface.y = swapz;*/

                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

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
                                                int indexofvert0 = vertices[indexflat].Count;
                                                Vector3 firstvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte, datamapfirstvertztop[indexflat][index].thebyte);

                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                Vector3 secondvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte + (datawidthdimtop[indexflat][index].thebyte), datamapfirstvertztop[indexflat][index].thebyte);

                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[indexflat][index].thebyte, dataheightdimtop[indexflat][index].thebyte, datamapfirstvertztop[indexflat][index].thebyte + (datadepthdimtop[indexflat][index].thebyte));

                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                Vector3 fourthvertofface = new Vector3(dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxtop[indexflat][index].thebyte + (datawidthdimtop[indexflat][index].thebyte),datamapfirstvertztop[indexflat][index].thebyte + (datadepthdimtop[indexflat][index].thebyte));
                                                */



                                                vertices[indexflat].Add(firstvertofface);
                                                vertices[indexflat].Add(secondvertofface);
                                                vertices[indexflat].Add(thirdvertofface);
                                                vertices[indexflat].Add(fourthvertofface);

                                                triangles[indexflat].Add(indexofvert0);
                                                triangles[indexflat].Add(indexofvert1);
                                                triangles[indexflat].Add(indexofvert2);
                                                triangles[indexflat].Add(indexofvert3);
                                                triangles[indexflat].Add(indexofvert2);
                                                triangles[indexflat].Add(indexofvert1);
                                            }
                                            else if (facetype == 5)
                                            {

                                                int indexofvert0 = vertices[indexflat].Count;
                                                Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                                float swapx = firstvertofface.x;
                                                float swapy = firstvertofface.y;
                                                float swapz = firstvertofface.z;

                                                firstvertofface.y -= (1 * 1.0f);

                                                //firstvertofface.x = swapy;
                                                //firstvertofface.y = datawidthdimtop[indexflat][index].thebyte;

                                                /*firstvertofface.z -= (1 * 1.0f);
                                                firstvertofface.y -= (1 * 1.0f);*/
                                                //firstvertofface.z = swapy;
                                                //firstvertofface.y = swapz;

                                                int indexofvert1 = vertices[indexflat].Count + 1;
                                                Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z);
                                                swapx = secondvertofface.x;
                                                swapy = secondvertofface.y;
                                                swapz = secondvertofface.z;

                                                secondvertofface.y -= (1 * 1.0f);
                                                /*secondvertofface.x = swapy;
                                                secondvertofface.y = datawidthdimtop[indexflat][index].thebyte; ///swapx;*/
                                                /*secondvertofface.x -= (1 * 1.0f);
                                                secondvertofface.y -= (1 * 1.0f);*/


                                                int indexofvert2 = vertices[indexflat].Count + 2;
                                                Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));
                                                //thirdvertofface.z -= (1 * 1.0f);
                                                thirdvertofface.y -= (1 * 1.0f);
                                                swapx = thirdvertofface.x;
                                                swapy = thirdvertofface.y;
                                                swapz = thirdvertofface.z;

                                                /*thirdvertofface.z = swapy;
                                                thirdvertofface.y = swapz;*/

                                                int indexofvert3 = vertices[indexflat].Count + 3;
                                                Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datadims[indexflat][index].vertpos.x), datadims[indexflat][index].vertpos.y, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadims[indexflat][index].vertpos.z));

                                                //fourthvertofface.z -= (1 * 1.0f);
                                                fourthvertofface.y -= (1 * 1.0f);


                                                swapx = fourthvertofface.x;
                                                swapy = fourthvertofface.y;
                                                swapz = fourthvertofface.z;


                                                vertices[indexflat].Add(firstvertofface);
                                                vertices[indexflat].Add(secondvertofface);
                                                vertices[indexflat].Add(thirdvertofface);
                                                vertices[indexflat].Add(fourthvertofface);

                                                triangles[indexflat].Add(indexofvert0);
                                                triangles[indexflat].Add(indexofvert1);
                                                triangles[indexflat].Add(indexofvert2);
                                                triangles[indexflat].Add(indexofvert3);
                                                triangles[indexflat].Add(indexofvert2);
                                                triangles[indexflat].Add(indexofvert1);
                                            }

                                            blockers[indexflat].vertices = vertices[indexflat];
                                            blockers[indexflat].triangles = triangles[indexflat];


                                            /*
                                            int indexofvert0 = vertices[indexflat].Count;
                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                            int indexofvert1 = vertices[indexflat].Count + 1;
                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datawidthdimtop[indexflat][index].thebyte), dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxyztop[indexflat][index].vertpos.z);

                                            int indexofvert2 = vertices[indexflat].Count + 2;
                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x, dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadepthdimtop[indexflat][index].thebyte));

                                            int indexofvert3 = vertices[indexflat].Count + 3;
                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[indexflat][index].vertpos.x + (datawidthdimtop[indexflat][index].thebyte), dataheightdimtop[indexflat][index].thebyte, datamapfirstvertxyztop[indexflat][index].vertpos.z + (datadepthdimtop[indexflat][index].thebyte));


                                            vertices[indexflat].Add(firstvertofface);
                                            vertices[indexflat].Add(secondvertofface);
                                            vertices[indexflat].Add(thirdvertofface);
                                            vertices[indexflat].Add(fourthvertofface);

                                            triangles[indexflat].Add(indexofvert2);
                                            triangles[indexflat].Add(indexofvert1);
                                            triangles[indexflat].Add(indexofvert0);
                                            triangles[indexflat].Add(indexofvert1);
                                            triangles[indexflat].Add(indexofvert2);
                                            triangles[indexflat].Add(indexofvert3);
                                            */




                                            /*int indexofvert0 = vertices[indexflat].Count;
                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxtop[indexflat][index].thebyte, dataheightdimtop[indexflat][index].thebyte, datamapfirstvertztop[indexflat][index].thebyte);

                                            int indexofvert1 = vertices[indexflat].Count + 1;
                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxtop[indexflat][index].thebyte + (datawidthdimtop[indexflat][index].thebyte), dataheightdimtop[indexflat][index].thebyte, datamapfirstvertztop[indexflat][index].thebyte);

                                            int indexofvert2 = vertices[indexflat].Count + 2;
                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[indexflat][index].thebyte, dataheightdimtop[indexflat][index].thebyte, datamapfirstvertztop[indexflat][index].thebyte + (datadepthdimtop[indexflat][index].thebyte));

                                            int indexofvert3 = vertices[indexflat].Count + 3;
                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxtop[indexflat][index].thebyte + (datawidthdimtop[indexflat][index].thebyte), dataheightdimtop[indexflat][index].thebyte, datamapfirstvertztop[indexflat][index].thebyte + (datadepthdimtop[indexflat][index].thebyte));


                                            vertices[indexflat].Add(firstvertofface);
                                            vertices[indexflat].Add(secondvertofface);
                                            vertices[indexflat].Add(thirdvertofface);
                                            vertices[indexflat].Add(fourthvertofface);

                                            triangles[indexflat].Add(indexofvert2);
                                            triangles[indexflat].Add(indexofvert1);
                                            triangles[indexflat].Add(indexofvert0);
                                            triangles[indexflat].Add(indexofvert1);
                                            triangles[indexflat].Add(indexofvert2);
                                            triangles[indexflat].Add(indexofvert3);*/

















                                            //Instantiate(visualobject0, firstvertofface * 0.1f, Quaternion.identity);
                                            //Instantiate(visualobject1, secondvertofface * 0.1f, Quaternion.identity);
                                            //Instantiate(visualobject2, thirdvertofface * 0.1f, Quaternion.identity);
                                            //Instantiate(visualobject3, fourthvertofface * 0.1f, Quaternion.identity);


                                            /*GameObject vert0 = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);

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
                                            vert3.transform.rotation = Quaternion.identity;
                                            */
                                        }
                                    }
                                }
                            }
                        }
                    }





                }
            }
        }

    }





    public void CreateTheMesh(int facetype, List<Vector3>[] vertices, List<int>[] triangles, Vector3 chunkoriginpos)
    {
        GameObject emptyobjectparent0 = this.transform.gameObject;
        //GameObject emptyobjectparent0 = new GameObject();
        emptyobjectparent0.gameObject.name = "mainobjforface:" + facetype;

        emptyobjectparent0.transform.position = chunkoriginpos;
        /*
       for (int mx = 0; mx < levelsizex; mx++)
       {
           for (int my = 0; my < levelsizey; my++)
           {
               for (int mz = 0; mz < levelsizez; mz++)
               {
                   int mindex = mx + levelsizex * (my + levelsizey * mz);
       */

        /*
        for (int mx = 0; mx < levelsizex; mx++)
        {
            for (int my = 0; my < levelsizey; my++)
            {
                for (int mz = 0; mz < levelsizez; mz++)
                {
                    int mindex = mx + levelsizex * (my + levelsizey * mz);

                    Vector3 chunkmainpos = new Vector3(mx * mapx * planesize, my * mapy * planesize, mz * mapz * planesize) + chunkoriginpos;
        */




        for (int x = -schunkwl; x <= schunkwr; x++)
        {
            for (int y = -schunkhl; y <= schunkhr; y++)
            {
                for (int z = -schunkdl; z <= schunkdr; z++)
                {
                    int xx = x;
                    int yy = y;
                    int zz = z;

                    if (xx < 0)
                    {
                        xx *= -1;
                        xx = xx + schunkwr;
                    }
                    if (yy < 0)
                    {
                        yy *= -1;
                        yy = yy + schunkhr;
                    }
                    if (zz < 0)
                    {
                        zz *= -1;
                        zz = zz + schunkdr;
                    }

                    int indexflat = xx + (schunkwl + schunkwr + 1) * (yy + (schunkhl + schunkhr + 1) * zz);


                    Vector3 chunkmainpos = new Vector3(x * mapx * planesize, y * mapy * planesize, z * mapz * planesize) + chunkoriginpos;
                    //Vector3 chunkmainpos = new Vector3(x * (schunkwl + schunkwr + 1) * planesize, y * (schunkhl + schunkhr + 1) * planesize, z * (schunkdl + schunkdr + 1) * planesize) + chunkoriginpos;
                    //Vector3 chunkmainpos = new Vector3(x * (schunkwl + schunkwr + 1) * mapx * planesize, y * (schunkhl + schunkhr + 1) * mapy * planesize, z * (schunkdl + schunkdr + 1) * mapz * planesize) + chunkoriginpos;




                    GameObject emptyobject = this.transform.gameObject.GetComponent<NewObjectPoolerScript>().GetPooledObject();

                    emptyobject.transform.parent = this.transform;
                    emptyobject.SetActive(true);

                    //GameObject emptyobject = new GameObject();
                    /*var meshfilt = emptyobject.AddComponent<MeshFilter>();
                    var meshrend = emptyobject.AddComponent<MeshRenderer>();*/

                    /*Mesh thenewmesh = new Mesh();
                    thenewmesh.vertices[mindex] = vertices[mindex].ToArray();
                    thenewmesh.triangles[mindex] = triangles[mindex].ToArray();

                    emptyobject.GetComponent<MeshFilter>().mesh = thenewmesh;
                    *///_testChunk.GetComponent<MeshRenderer>().material = _mat;



                    Mesh mesh = new Mesh();
                    emptyobject.gameObject.GetComponent<MeshFilter>().mesh.Clear();
                    emptyobject.gameObject.GetComponent<MeshFilter>().mesh = null;

                    emptyobject.gameObject.GetComponent<MeshFilter>().mesh = mesh;
                    emptyobject.GetComponent<MeshFilter>().sharedMesh = mesh;




                    /*
                    var verts = vertices[mindex].ToArray();
                    var tris = triangles[mindex].ToArray();*/

                    emptyobject.GetComponent<MeshFilter>().mesh.Clear();
                    emptyobject.GetComponent<MeshFilter>().mesh.vertices = vertices[indexflat].ToArray();// vertices[mindex].ToArray();
                    emptyobject.GetComponent<MeshFilter>().mesh.triangles = triangles[indexflat].ToArray();// triangles[mindex].ToArray();
                    //meshCollider.sharedMesh = null;
                    //meshCollider.sharedMesh = mesh;
                    emptyobject.GetComponent<MeshFilter>().mesh.RecalculateBounds();
                    emptyobject.GetComponent<MeshFilter>().mesh.RecalculateNormals();



                    //Debug.Log("sccscomputevoxelTOP:" + "/triangles[mindex]:" + triangles[mindex].Count);

                    emptyobject.transform.position = chunkmainpos;
                    emptyobject.transform.rotation = Quaternion.identity;

                    emptyobject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    emptyobject.GetComponent<MeshRenderer>().material = mat;

                    emptyobject.transform.parent = emptyobjectparent0.transform;

                    /*if (facetype == 0)
                    {
                     
                    }*/
                    emptyobject.gameObject.name = "faces:" + facetype;
                    emptyobject.transform.name = "faces:" + facetype;



                    emptyobject.tag = "collisionObject";
                    emptyobject.layer = 8;


                    if (emptyobject.gameObject.GetComponent<MeshCollider>() == null)
                    {
                        emptyobject.gameObject.AddComponent<MeshCollider>();
                    }
                    else
                    {
                        Destroy(emptyobject.gameObject.GetComponent<MeshCollider>());
                        emptyobject.gameObject.AddComponent<MeshCollider>();

                    }

                }
            }
        }
    }








    public mainChunk getChunk(int x, int y, int z)
    {



        


        if ((x < -schunkwl) || (y < -schunkhl) || (z < -schunkdl) || (x >= schunkwr + 1) || (y >= (schunkhr + 1)) || (z >= (schunkdr + 1)))
        {
            return null;
        }

        if (x < 0)
        {
            x *= -1;
            x = (schunkwr) + x;
        }
        if (y < 0)
        {
            y *= -1;
            y = (schunkhr) + y;
        }
        if (z < 0)
        {
            z *= -1;
            z = (schunkdr) + z;
        }

        int _index = x + (schunkwl + schunkwr + 1) * (y + (schunkhl + schunkhr + 1) * z);

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


        //return blockers[]
    }

    /*
    public bool IsTransparent(int x, int y, int z, int[] bytemap)
    {
        int indexOf = x + mapx * (y + mapy * z);

        if ((x < 0) || (y < 0) || (z < 0) || (x >= mapx) || (y >= mapy) || (z >= mapz)) return true;
        {
            return bytemap[indexOf] == 0;
            //return map[x + width * (y + depth * z)] == 0;
        }
    }*/


    public bool IsTransparent(int x, int y, int z, int[] bytemap)
    {
        if (bytemap == null)
        {
            return true;
        }

        int indexOf = x + mapx * (y + mapy * z);

        if ((x < 0) || (y < 0) || (z < 0) || (x >= mapx) || (y >= mapy) || (z >= mapz)) return true;
        {
            return bytemap[indexOf] == 0;
            //return map[x + width * (y + depth * z)] == 0;
        }
    }







    public GameObject theplayer;


    // Update is called once per frame
    void Update()
    {
        /*if (theplayer == null)
        {
            theplayer = GameObject.FindGameObjectWithTag("Player");

            theplayer.GetComponent<sccsplayer>().theplanet = this.gameObject;


        }*/
    }
}
