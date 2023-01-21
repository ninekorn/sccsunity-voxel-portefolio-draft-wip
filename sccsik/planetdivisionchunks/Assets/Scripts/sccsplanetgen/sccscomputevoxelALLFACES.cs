using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class sccscomputevoxelALLFACES : MonoBehaviour
{
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

    public int levelsizex = 1;
    public int levelsizey = 1;
    public int levelsizez = 1;

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



    public void Awake()
    {
        currentsccscomputevoxelALLFACES = this;

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



    public Vector3 chunkoriginpos;
    public void CreateTheArrays(int facetype,Vector3 chunkoriginpos_)
    {

        chunkoriginpos = chunkoriginpos_;


        //int facetype = 5;

        mapdata = new mapbytes[levelsizex * levelsizey * levelsizez][];

        datamapfirstvertxyztop = new mapofvertints[levelsizex * levelsizey * levelsizez][];

        /*datamapfirstvertxtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertytop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertztop = new mapofints[levelsizex * levelsizey * levelsizez][];*/
        /*datawidthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        dataheightdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datadepthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];*/

        datadims = new mapofvertints[levelsizex * levelsizey * levelsizez][];




        /*
        GameObject emptyobjectparent1 = new GameObject();
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

                    Vector3 chunkmainpos = new Vector3(mx * mapx * planesize, my * mapy * planesize, mz * mapz * planesize) + chunkoriginpos;


                    //int totalSize = mapx * mapy * mapz;
                    mapdata[mindex] = new mapbytes[mapx * mapy * mapz];

                    datamapfirstvertxyztop[mindex] = new mapofvertints[mapx * mapy * mapz];

                    /*datamapfirstvertxtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertytop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertztop[mindex] = new mapofints[mapx * mapy * mapz];*/
                    /*datawidthdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    dataheightdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datadepthdimtop[mindex] = new mapofints[mapx * mapy * mapz];*/

                    datadims[mindex] = new mapofvertints[mapx * mapy * mapz];



                    for (int x = 0; x < mapx; x++)
                    {
                        for (int y = 0; y < mapy; y++)
                        {
                            for (int z = 0; z < mapz; z++)
                            {
                                int index = x + mapx * (y + mapy * z);

                                mapdata[mindex][index] = new mapbytes();
                                mapdata[mindex][index].thebyte = 0;
                                mapdata[mindex][index].position = new Vector3(mx * mapx * planesize, my * mapy * planesize, mz * mapz * planesize) + chunkmainpos;
                                mapdata[mindex][index].ix = x;
                                mapdata[mindex][index].iy = y;
                                mapdata[mindex][index].iz = z;




                                datamapfirstvertxyztop[mindex][index] = new mapofvertints();
                                //datamapfirstvertxyztop[mindex][index].thebyte = 0;

                                datamapfirstvertxyztop[mindex][index].vertpos = new Vector4(0, 0, 0, 0);



                                /*datamapfirstvertxtop[mindex][index] = new mapofints();
                                datamapfirstvertxtop[mindex][index].thebyte = 0;

                                datamapfirstvertytop[mindex][index] = new mapofints();
                                datamapfirstvertytop[mindex][index].thebyte = 0;

                                datamapfirstvertztop[mindex][index] = new mapofints();
                                datamapfirstvertztop[mindex][index].thebyte = 0;
                                */
                                /*datawidthdimtop[mindex][index] = new mapofints();
                                datawidthdimtop[mindex][index].thebyte = 0;

                                dataheightdimtop[mindex][index] = new mapofints();
                                dataheightdimtop[mindex][index].thebyte = 0;

                                datadepthdimtop[mindex][index] = new mapofints();
                                datadepthdimtop[mindex][index].thebyte = 0;
                                */


                                datadims[mindex][index] = new mapofvertints();
                                datadims[mindex][index].vertpos = new Vector4(0, 0, 0, 0);


                            }
                        }
                    }

                }
            }
        }
    }




    public void CreateTheMaps(int facetype)
    {
        for (int mx = 0; mx < levelsizex; mx++)
        {
            for (int my = 0; my < levelsizey; my++)
            {
                for (int mz = 0; mz < levelsizez; mz++)
                {
                    int mindex = mx + levelsizex * (my + levelsizey * mz);

                    Vector3 chunkmainpos = new Vector3(mx * mapx * planesize, my * mapy * planesize, mz * mapz * planesize);

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
                }
            }
        }
    }



    public void ComputeTheVertexes()
    {
        for (int mx = 0; mx < levelsizex; mx++)
        {
            for (int my = 0; my < levelsizey; my++)
            {
                for (int mz = 0; mz < levelsizez; mz++)
                {
                    int mindex = mx + levelsizex * (my + levelsizey * mz);

                    Vector3 chunkmainpos = new Vector3(mx * mapx * planesize, my * mapy * planesize, mz * mapz * planesize);




                    int thebytesize = sizeof(int) * 4;
                    int vector3Size = sizeof(float) * 3;
                    int totalSize = thebytesize + vector3Size;


                    ComputeBuffer maps0buffer = new ComputeBuffer(mapdata[mindex].Length, totalSize);
                    maps0buffer.SetData(mapdata[mindex]);


                    ComputeBuffer mapvertlocbufferxyz = new ComputeBuffer(datamapfirstvertxyztop[mindex].Length, 16);
                    mapvertlocbufferxyz.SetData(datamapfirstvertxyztop[mindex]);


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

                    ComputeBuffer mapdims = new ComputeBuffer(datadims[mindex].Length, 16);
                    mapdims.SetData(datadims[mindex]);



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

                    mapvertlocbufferxyz.GetData(datamapfirstvertxyztop[mindex]);


                    mapdims.GetData(datadims[mindex]);


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
        vertices = new List<Vector3>[levelsizex* levelsizey * levelsizez];
        triangles = new List<int>[levelsizex * levelsizey * levelsizez];

        for (int mx = 0; mx < levelsizex; mx++)
        {
            for (int my = 0; my < levelsizey; my++)
            {
                for (int mz = 0; mz < levelsizez; mz++)
                {
                    int mindex = mx + levelsizex * (my + levelsizey * mz);

                    Vector3 chunkmainpos = new Vector3(mx * mapx * planesize, my * mapy * planesize, mz * mapz * planesize);



                    /*
                    mapwidthdimtop.GetData(datawidthdimtop[mindex]);
                    mapheightdimtop.GetData(dataheightdimtop[mindex]);
                    mapdepthdimtop.GetData(datadepthdimtop[mindex]);*/



                    /*List<Vector3> vertices = new List<Vector3>();
                    List<int> triangles = new List<int>();
                    */



                    vertices[mindex] = new List<Vector3>();
                    triangles[mindex] = new List<int>();

                    
                    for (int x = 0; x < mapx; x++)
                    {
                        for (int y = 0; y < mapy; y++)
                        {
                            for (int z = 0; z < mapz; z++)
                            {
                                int index = x + mapx * (y + mapy * z);

                                //mapint[index] = data[mindex][index].thebyte;

                                //Debug.Log("map:" + data[index].thebyte);



                                if (datadims[mindex][index].vertpos.y == 0)//datamapfirstvertxtop[mindex][index].thebyte == 0 && datamapfirstvertytop[mindex][index].thebyte == 0 && datamapfirstvertztop[mindex][index].thebyte == 0)
                                {

                                }
                                else
                                {









                                    /*
                                    ////TOPFACE
                                    ////TOPFACE
                                    ////TOPFACE
                                    if (mainChunk.extremityhr == 1)
                                    {
                                        


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
                                    ////TOPFACE*/















































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































                                    //if (mapint[index] == 1)//IsTransparent(x, y+1,z, originalmapdata[mindex]) && blockExistsInArray(x, y - 1 ,z) == 1)// mapint[index] == 1) //mapdata[mindex]
                                    {


                                        if (facetype == 0)
                                        {
                                            int indexofvert0 = vertices[mindex].Count;
                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x, datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z);

                                            int indexofvert1 = vertices[mindex].Count + 1;
                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x + (datadims[mindex][index].vertpos.x), datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z);

                                            int indexofvert2 = vertices[mindex].Count + 2;
                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x, datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z + (datadims[mindex][index].vertpos.z));

                                            int indexofvert3 = vertices[mindex].Count + 3;
                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x + (datadims[mindex][index].vertpos.x), datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z + (datadims[mindex][index].vertpos.z));


                                            vertices[mindex].Add(firstvertofface);
                                            vertices[mindex].Add(secondvertofface);
                                            vertices[mindex].Add(thirdvertofface);
                                            vertices[mindex].Add(fourthvertofface);

                                            triangles[mindex].Add(indexofvert2);
                                            triangles[mindex].Add(indexofvert1);
                                            triangles[mindex].Add(indexofvert0);
                                            triangles[mindex].Add(indexofvert1);
                                            triangles[mindex].Add(indexofvert2);
                                            triangles[mindex].Add(indexofvert3);

                                        }
                                        else if (facetype == 1)
                                        {

                                            int indexofvert0 = vertices[mindex].Count;
                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x, datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z);

                                            float swapx = firstvertofface.x;
                                            float swapy = firstvertofface.y;
                                            float swapz = firstvertofface.z;

                                            //firstvertofface.x = swapy;
                                            //firstvertofface.y = datawidthdimtop[mindex][index].thebyte;

                                            //firstvertofface.x -= (1 * 1.0f);
                                            //firstvertofface.y -= (1 * 1.0f);


                                            int indexofvert1 = vertices[mindex].Count + 1;
                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x + (datadims[mindex][index].vertpos.x), datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z);
                                            /*swapx = secondvertofface.x;
                                            swapy = secondvertofface.y;
                                            swapz = secondvertofface.z;

                                            secondvertofface.x = swapy;
                                            secondvertofface.y = datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                            secondvertofface.x -= (1 * 1.0f);
                                            secondvertofface.y -= (1 * 1.0f);


                                            int indexofvert2 = vertices[mindex].Count + 2;
                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x, datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z + (datadims[mindex][index].vertpos.z));
                                            //thirdvertofface.x -= (1 * 1.0f);
                                            //thirdvertofface.y -= (1 * 1.0f);
                                            /*swapx = thirdvertofface.x;
                                            swapy = thirdvertofface.y;
                                            swapz = thirdvertofface.z;

                                            thirdvertofface.x = swapy;
                                            thirdvertofface.y = swapx;*/

                                            int indexofvert3 = vertices[mindex].Count + 3;
                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x + (datadims[mindex][index].vertpos.x), datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z + (datadims[mindex][index].vertpos.z));

                                            fourthvertofface.x -= (1 * 1.0f);
                                            fourthvertofface.y -= (1 * 1.0f);


                                            /*swapx = fourthvertofface.x;
                                            swapy = fourthvertofface.y;
                                            swapz = fourthvertofface.z;

                                            fourthvertofface.x = swapy;
                                            fourthvertofface.y = swapx;*/
                                            /*
                                            int indexofvert0 = vertices[mindex].Count;
                                            Vector3 firstvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert1 = vertices[mindex].Count + 1;
                                            Vector3 secondvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert2 = vertices[mindex].Count + 2;
                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                            int indexofvert3 = vertices[mindex].Count + 3;
                                            Vector3 fourthvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte),datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                            */



                                            vertices[mindex].Add(firstvertofface);
                                            vertices[mindex].Add(secondvertofface);
                                            vertices[mindex].Add(thirdvertofface);
                                            vertices[mindex].Add(fourthvertofface);

                                            triangles[mindex].Add(indexofvert0);
                                            triangles[mindex].Add(indexofvert1);
                                            triangles[mindex].Add(indexofvert2);
                                            triangles[mindex].Add(indexofvert3);
                                            triangles[mindex].Add(indexofvert2);
                                            triangles[mindex].Add(indexofvert1);
                                        }
                                        else if (facetype == 2)
                                        {

                                            int indexofvert0 = vertices[mindex].Count;
                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x, datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z);

                                            float swapx = firstvertofface.x;
                                            float swapy = firstvertofface.y;
                                            float swapz = firstvertofface.z;

                                            //firstvertofface.x = swapy;
                                            //firstvertofface.y = datawidthdimtop[mindex][index].thebyte;

                                            //firstvertofface.x -= (1 * 1.0f);
                                            //firstvertofface.y -= (1 * 1.0f);


                                            int indexofvert1 = vertices[mindex].Count + 1;
                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x + (datadims[mindex][index].vertpos.x), datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z);
                                            /*swapx = secondvertofface.x;
                                            swapy = secondvertofface.y;
                                            swapz = secondvertofface.z;

                                            secondvertofface.x = swapy;
                                            secondvertofface.y = datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                            secondvertofface.x -= (1 * 1.0f);
                                            secondvertofface.y -= (1 * 1.0f);


                                            int indexofvert2 = vertices[mindex].Count + 2;
                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x, datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z + (datadims[mindex][index].vertpos.z));
                                            //thirdvertofface.x -= (1 * 1.0f);
                                            //thirdvertofface.y -= (1 * 1.0f);
                                            /*swapx = thirdvertofface.x;
                                            swapy = thirdvertofface.y;
                                            swapz = thirdvertofface.z;

                                            thirdvertofface.x = swapy;
                                            thirdvertofface.y = swapx;*/

                                            int indexofvert3 = vertices[mindex].Count + 3;
                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x + (datadims[mindex][index].vertpos.x), datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z + (datadims[mindex][index].vertpos.z));

                                            fourthvertofface.x -= (1 * 1.0f);
                                            fourthvertofface.y -= (1 * 1.0f);


                                            /*swapx = fourthvertofface.x;
                                            swapy = fourthvertofface.y;
                                            swapz = fourthvertofface.z;

                                            fourthvertofface.x = swapy;
                                            fourthvertofface.y = swapx;*/
                                            /*
                                            int indexofvert0 = vertices[mindex].Count;
                                            Vector3 firstvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert1 = vertices[mindex].Count + 1;
                                            Vector3 secondvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert2 = vertices[mindex].Count + 2;
                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                            int indexofvert3 = vertices[mindex].Count + 3;
                                            Vector3 fourthvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte),datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                            */



                                            vertices[mindex].Add(firstvertofface);
                                            vertices[mindex].Add(secondvertofface);
                                            vertices[mindex].Add(thirdvertofface);
                                            vertices[mindex].Add(fourthvertofface);

                                            triangles[mindex].Add(indexofvert2);
                                            triangles[mindex].Add(indexofvert1);
                                            triangles[mindex].Add(indexofvert0);
                                            triangles[mindex].Add(indexofvert1);
                                            triangles[mindex].Add(indexofvert2);
                                            triangles[mindex].Add(indexofvert3);
                                        }
                                        else if (facetype == 3)
                                        {

                                            int indexofvert0 = vertices[mindex].Count;
                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x, datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z);

                                            float swapx = firstvertofface.x;
                                            float swapy = firstvertofface.y;
                                            float swapz = firstvertofface.z;

                                            //firstvertofface.x = swapy;
                                            //firstvertofface.y = datawidthdimtop[mindex][index].thebyte;

                                            /*firstvertofface.z -= (1 * 1.0f);
                                            firstvertofface.y -= (1 * 1.0f);*/
                                            //firstvertofface.z = swapy;
                                            //firstvertofface.y = swapz;

                                            int indexofvert1 = vertices[mindex].Count + 1;
                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x + (datadims[mindex][index].vertpos.x), datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z);
                                            swapx = secondvertofface.x;
                                            swapy = secondvertofface.y;
                                            swapz = secondvertofface.z;

                                            /*secondvertofface.x = swapy;
                                            secondvertofface.y = datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                            /*secondvertofface.x -= (1 * 1.0f);
                                            secondvertofface.y -= (1 * 1.0f);*/


                                            int indexofvert2 = vertices[mindex].Count + 2;
                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x, datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z + (datadims[mindex][index].vertpos.z));
                                            thirdvertofface.z -= (1 * 1.0f);
                                            thirdvertofface.y -= (1 * 1.0f);
                                            swapx = thirdvertofface.x;
                                            swapy = thirdvertofface.y;
                                            swapz = thirdvertofface.z;

                                            /*thirdvertofface.z = swapy;
                                            thirdvertofface.y = swapz;*/

                                            int indexofvert3 = vertices[mindex].Count + 3;
                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x + (datadims[mindex][index].vertpos.x), datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z + (datadims[mindex][index].vertpos.z));

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
                                            int indexofvert0 = vertices[mindex].Count;
                                            Vector3 firstvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert1 = vertices[mindex].Count + 1;
                                            Vector3 secondvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert2 = vertices[mindex].Count + 2;
                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                            int indexofvert3 = vertices[mindex].Count + 3;
                                            Vector3 fourthvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte),datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                            */



                                            vertices[mindex].Add(firstvertofface);
                                            vertices[mindex].Add(secondvertofface);
                                            vertices[mindex].Add(thirdvertofface);
                                            vertices[mindex].Add(fourthvertofface);

                                            triangles[mindex].Add(indexofvert2);
                                            triangles[mindex].Add(indexofvert1);
                                            triangles[mindex].Add(indexofvert0);
                                            triangles[mindex].Add(indexofvert1);
                                            triangles[mindex].Add(indexofvert2);
                                            triangles[mindex].Add(indexofvert3);
                                        }
                                        else if (facetype == 4)
                                        {

                                            /*
                                            int indexofvert0 = vertices[mindex].Count;
                                            Vector3 firstvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte,datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert1 = vertices[mindex].Count + 1;
                                            Vector3 secondvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert2 = vertices[mindex].Count + 2;
                                            Vector3 thirdvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                            int indexofvert3 = vertices[mindex].Count + 3;
                                            Vector3 fourthvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                            */



                                            int indexofvert0 = vertices[mindex].Count;
                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x, datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z);

                                            float swapx = firstvertofface.x;
                                            float swapy = firstvertofface.y;
                                            float swapz = firstvertofface.z;

                                            //firstvertofface.x = swapy;
                                            //firstvertofface.y = datawidthdimtop[mindex][index].thebyte;

                                            /*firstvertofface.z -= (1 * 1.0f);
                                            firstvertofface.y -= (1 * 1.0f);*/
                                            //firstvertofface.z = swapy;
                                            //firstvertofface.y = swapz;

                                            int indexofvert1 = vertices[mindex].Count + 1;
                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x + (datadims[mindex][index].vertpos.x), datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z);
                                            swapx = secondvertofface.x;
                                            swapy = secondvertofface.y;
                                            swapz = secondvertofface.z;

                                            /*secondvertofface.x = swapy;
                                            secondvertofface.y = datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                            /*secondvertofface.x -= (1 * 1.0f);
                                            secondvertofface.y -= (1 * 1.0f);*/


                                            int indexofvert2 = vertices[mindex].Count + 2;
                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x, datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z + (datadims[mindex][index].vertpos.z));
                                            thirdvertofface.z -= (1 * 1.0f);
                                            thirdvertofface.y -= (1 * 1.0f);
                                            swapx = thirdvertofface.x;
                                            swapy = thirdvertofface.y;
                                            swapz = thirdvertofface.z;

                                            /*thirdvertofface.z = swapy;
                                            thirdvertofface.y = swapz;*/

                                            int indexofvert3 = vertices[mindex].Count + 3;
                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x + (datadims[mindex][index].vertpos.x), datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z + (datadims[mindex][index].vertpos.z));

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
                                            int indexofvert0 = vertices[mindex].Count;
                                            Vector3 firstvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert1 = vertices[mindex].Count + 1;
                                            Vector3 secondvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte);

                                            int indexofvert2 = vertices[mindex].Count + 2;
                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                            int indexofvert3 = vertices[mindex].Count + 3;
                                            Vector3 fourthvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte),datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                            */



                                            vertices[mindex].Add(firstvertofface);
                                            vertices[mindex].Add(secondvertofface);
                                            vertices[mindex].Add(thirdvertofface);
                                            vertices[mindex].Add(fourthvertofface);

                                            triangles[mindex].Add(indexofvert0);
                                            triangles[mindex].Add(indexofvert1);
                                            triangles[mindex].Add(indexofvert2);
                                            triangles[mindex].Add(indexofvert3);
                                            triangles[mindex].Add(indexofvert2);
                                            triangles[mindex].Add(indexofvert1);
                                        }
                                        else if (facetype == 5)
                                        {

                                            int indexofvert0 = vertices[mindex].Count;
                                            Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x, datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z);

                                            float swapx = firstvertofface.x;
                                            float swapy = firstvertofface.y;
                                            float swapz = firstvertofface.z;

                                            firstvertofface.y -= (1 * 1.0f);

                                            //firstvertofface.x = swapy;
                                            //firstvertofface.y = datawidthdimtop[mindex][index].thebyte;

                                            /*firstvertofface.z -= (1 * 1.0f);
                                            firstvertofface.y -= (1 * 1.0f);*/
                                            //firstvertofface.z = swapy;
                                            //firstvertofface.y = swapz;

                                            int indexofvert1 = vertices[mindex].Count + 1;
                                            Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x + (datadims[mindex][index].vertpos.x), datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z);
                                            swapx = secondvertofface.x;
                                            swapy = secondvertofface.y;
                                            swapz = secondvertofface.z;

                                            secondvertofface.y -= (1 * 1.0f);
                                            /*secondvertofface.x = swapy;
                                            secondvertofface.y = datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                            /*secondvertofface.x -= (1 * 1.0f);
                                            secondvertofface.y -= (1 * 1.0f);*/


                                            int indexofvert2 = vertices[mindex].Count + 2;
                                            Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x, datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z + (datadims[mindex][index].vertpos.z));
                                            //thirdvertofface.z -= (1 * 1.0f);
                                            thirdvertofface.y -= (1 * 1.0f);
                                            swapx = thirdvertofface.x;
                                            swapy = thirdvertofface.y;
                                            swapz = thirdvertofface.z;

                                            /*thirdvertofface.z = swapy;
                                            thirdvertofface.y = swapz;*/

                                            int indexofvert3 = vertices[mindex].Count + 3;
                                            Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x + (datadims[mindex][index].vertpos.x), datadims[mindex][index].vertpos.y, datamapfirstvertxyztop[mindex][index].vertpos.z + (datadims[mindex][index].vertpos.z));

                                            //fourthvertofface.z -= (1 * 1.0f);
                                            fourthvertofface.y -= (1 * 1.0f);


                                            swapx = fourthvertofface.x;
                                            swapy = fourthvertofface.y;
                                            swapz = fourthvertofface.z;


                                            vertices[mindex].Add(firstvertofface);
                                            vertices[mindex].Add(secondvertofface);
                                            vertices[mindex].Add(thirdvertofface);
                                            vertices[mindex].Add(fourthvertofface);

                                            triangles[mindex].Add(indexofvert0);
                                            triangles[mindex].Add(indexofvert1);
                                            triangles[mindex].Add(indexofvert2);
                                            triangles[mindex].Add(indexofvert3);
                                            triangles[mindex].Add(indexofvert2);
                                            triangles[mindex].Add(indexofvert1);
                                        }


                                        /*
                                        int indexofvert0 = vertices[mindex].Count;
                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x, dataheightdimtop[mindex][index].thebyte, datamapfirstvertxyztop[mindex][index].vertpos.z);

                                        int indexofvert1 = vertices[mindex].Count + 1;
                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertxyztop[mindex][index].vertpos.z);

                                        int indexofvert2 = vertices[mindex].Count + 2;
                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x, dataheightdimtop[mindex][index].thebyte, datamapfirstvertxyztop[mindex][index].vertpos.z + (datadepthdimtop[mindex][index].thebyte));

                                        int indexofvert3 = vertices[mindex].Count + 3;
                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxyztop[mindex][index].vertpos.x + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertxyztop[mindex][index].vertpos.z + (datadepthdimtop[mindex][index].thebyte));


                                        vertices[mindex].Add(firstvertofface);
                                        vertices[mindex].Add(secondvertofface);
                                        vertices[mindex].Add(thirdvertofface);
                                        vertices[mindex].Add(fourthvertofface);

                                        triangles[mindex].Add(indexofvert2);
                                        triangles[mindex].Add(indexofvert1);
                                        triangles[mindex].Add(indexofvert0);
                                        triangles[mindex].Add(indexofvert1);
                                        triangles[mindex].Add(indexofvert2);
                                        triangles[mindex].Add(indexofvert3);
                                        */




                                        /*int indexofvert0 = vertices[mindex].Count;
                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                        int indexofvert1 = vertices[mindex].Count + 1;
                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                        int indexofvert2 = vertices[mindex].Count + 2;
                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                        int indexofvert3 = vertices[mindex].Count + 3;
                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));


                                        vertices[mindex].Add(firstvertofface);
                                        vertices[mindex].Add(secondvertofface);
                                        vertices[mindex].Add(thirdvertofface);
                                        vertices[mindex].Add(fourthvertofface);

                                        triangles[mindex].Add(indexofvert2);
                                        triangles[mindex].Add(indexofvert1);
                                        triangles[mindex].Add(indexofvert0);
                                        triangles[mindex].Add(indexofvert1);
                                        triangles[mindex].Add(indexofvert2);
                                        triangles[mindex].Add(indexofvert3);*/

















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





    public void CreateTheMesh(int facetype, List<Vector3>[] vertices, List<int>[] triangles, Vector3 chunkoriginpos)
    {
        GameObject emptyobjectparent0 = this.transform.gameObject;
        //GameObject emptyobjectparent0 = new GameObject();
        emptyobjectparent0.gameObject.name = "mainobjforface:" + facetype;

        for (int mx = 0; mx < levelsizex; mx++)
        {
            for (int my = 0; my < levelsizey; my++)
            {
                for (int mz = 0; mz < levelsizez; mz++)
                {
                    int mindex = mx + levelsizex * (my + levelsizey * mz);

                    Vector3 chunkmainpos = new Vector3(mx * mapx * planesize, my * mapy * planesize, mz * mapz * planesize) + chunkoriginpos;




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
                    emptyobject.GetComponent<MeshFilter>().mesh.vertices = vertices[mindex].ToArray();// vertices[mindex].ToArray();
                    emptyobject.GetComponent<MeshFilter>().mesh.triangles = triangles[mindex].ToArray();// triangles[mindex].ToArray();
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
