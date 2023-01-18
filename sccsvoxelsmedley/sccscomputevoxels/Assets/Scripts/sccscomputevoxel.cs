using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class sccscomputevoxel //: MonoBehaviour
{
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

    private mapbytes[][] mapdata;
    private mapofints[][] datamapfirstvertxtop;
    private mapofints[][] datamapfirstvertytop;
    private mapofints[][] datamapfirstvertztop;
    private mapofints[][] datawidthdimtop;
    private mapofints[][] dataheightdimtop;
    private mapofints[][] datadepthdimtop;

    public int levelsizex = 1;
    public int levelsizey = 1;
    public int levelsizez = 1;
    public int mapx = 10;
    public int mapy = 10;
    public int mapz = 10;

    public ComputeShader computeShaderForMap;
    public ComputeShader computeVertexesTOP;
    //public ComputeShader computeVertexestwo;
    public ComputeShader computeVertexesLEFT;
    public ComputeShader computeVertexesRIGHT;
    public ComputeShader computeVertexesFRONT;

    public Material frontfacemat;
    public Material rightfacemat;
    public Material mat2;
    public Material mat1;
    public Material mat;


    public int threadmulx = 1;
    public int threadmuly = 1;
    public int threadmulz = 1;

    public List<Vector3> vertices = new List<Vector3>();
    public List<int> triangles = new List<int>();





    GameObject emptyobjectparent0;//
    /*
   public  void CreateArrays(Vector3 originchunkpos)
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();




        mapdata = new mapbytes[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertxtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertytop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertztop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datawidthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        dataheightdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datadepthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];


        //emptyobjectparent0 = parentobject;
        //GameObject emptyobjectparent0 = new GameObject();
        /*GameObject emptyobjectparent1 = new GameObject();
        GameObject emptyobjectparentleftfaces = new GameObject();
        emptyobjectparentleftfaces.gameObject.name = "leftfacesmain";

        GameObject emptyobjectparentrightfaces = new GameObject();
        emptyobjectparentrightfaces.gameObject.name = "rightfacesmain";

        GameObject emptyobjectparentfrontfaces = new GameObject();
        emptyobjectparentfrontfaces.gameObject.name = "frontfacesmain";


        for (int mx = 0; mx < levelsizex; mx++)
        {
            for (int my = 0; my < levelsizey; my++)
            {
                for (int mz = 0; mz < levelsizez; mz++)
                {
                    int mindex = mx + levelsizex * (my + levelsizey * mz);

                    chunkpos = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f) + originchunkpos;


                    //int totalSize = mapx * mapy * mapz;
                    mapdata[mindex] = new mapbytes[mapx * mapy * mapz];
                    datamapfirstvertxtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertytop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertztop[mindex] = new mapofints[mapx * mapy * mapz];
                    datawidthdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    dataheightdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datadepthdimtop[mindex] = new mapofints[mapx * mapy * mapz];

                    for (int x = 0; x < mapx; x++)
                    {
                        for (int y = 0; y < mapy; y++)
                        {
                            for (int z = 0; z < mapz; z++)
                            {
                                int index = x + mapx * (y + mapy * z);

                                mapdata[mindex][index] = new mapbytes();
                                mapdata[mindex][index].thebyte = 0;
                                mapdata[mindex][index].position = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);
                                mapdata[mindex][index].ix = x;
                                mapdata[mindex][index].iy = y;
                                mapdata[mindex][index].iz = z;

                                datamapfirstvertxtop[mindex][index] = new mapofints();
                                datamapfirstvertxtop[mindex][index].thebyte = 0;

                                datamapfirstvertytop[mindex][index] = new mapofints();
                                datamapfirstvertytop[mindex][index].thebyte = 0;

                                datamapfirstvertztop[mindex][index] = new mapofints();
                                datamapfirstvertztop[mindex][index].thebyte = 0;

                                datawidthdimtop[mindex][index] = new mapofints();
                                datawidthdimtop[mindex][index].thebyte = 0;

                                dataheightdimtop[mindex][index] = new mapofints();
                                dataheightdimtop[mindex][index].thebyte = 0;

                                datadepthdimtop[mindex][index] = new mapofints();
                                datadepthdimtop[mindex][index].thebyte = 0;
                            }
                        }
                    }

                }
            }
        }
    }*/




    public Vector3 chunkpos;

    int reducedverttrigswtc = 0;
    int[] mapint;
    // Start is called before the first frame update


    void Start()
    //public void WorkOnShaders(Vector3 originchunkpos,GameObject parentobject) //, out List<Vector3> vertices, out List<int> triangles
    {
        vertices = new List<Vector3>();
        triangles = new List<int>();

        mapdata = new mapbytes[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertxtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertytop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertztop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datawidthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        dataheightdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datadepthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];

        //emptyobjectparent0 = parentobject;
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

                    chunkpos = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);

                    //int totalSize = mapx * mapy * mapz;
                    mapdata[mindex] = new mapbytes[mapx * mapy * mapz];
                    datamapfirstvertxtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertytop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertztop[mindex] = new mapofints[mapx * mapy * mapz];
                    datawidthdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    dataheightdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datadepthdimtop[mindex] = new mapofints[mapx * mapy * mapz];

                    for (int x = 0; x < mapx; x++)
                    {
                        for (int y = 0; y < mapy; y++)
                        {
                            for (int z = 0; z < mapz; z++)
                            {
                                int index = x + mapx * (y + mapy * z);

                                mapdata[mindex][index] = new mapbytes();
                                mapdata[mindex][index].thebyte = 0;
                                mapdata[mindex][index].position = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);
                                mapdata[mindex][index].ix = x;
                                mapdata[mindex][index].iy = y;
                                mapdata[mindex][index].iz = z;

                                datamapfirstvertxtop[mindex][index] = new mapofints();
                                datamapfirstvertxtop[mindex][index].thebyte = 0;

                                datamapfirstvertytop[mindex][index] = new mapofints();
                                datamapfirstvertytop[mindex][index].thebyte = 0;

                                datamapfirstvertztop[mindex][index] = new mapofints();
                                datamapfirstvertztop[mindex][index].thebyte = 0;

                                datawidthdimtop[mindex][index] = new mapofints();
                                datawidthdimtop[mindex][index].thebyte = 0;

                                dataheightdimtop[mindex][index] = new mapofints();
                                dataheightdimtop[mindex][index].thebyte = 0;

                                datadepthdimtop[mindex][index] = new mapofints();
                                datadepthdimtop[mindex][index].thebyte = 0;
                            }
                        }
                    }

                }
            }
        }


        for (int mx = 0; mx < levelsizex; mx++)
        {
            for (int my = 0; my < levelsizey; my++)
            {
                for (int mz = 0; mz < levelsizez; mz++)
                {
                    int mindex = mx + levelsizex * (my + levelsizey * mz);

                    chunkpos = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f) ;



                    int thebytesize = sizeof(int) * 4;
                    int vector3Size = sizeof(float) * 3;
                    int totalSize = thebytesize + vector3Size;

                    ComputeBuffer mapsbuffer = new ComputeBuffer(mapdata[mindex].Length, totalSize);

                    mapsbuffer.SetData(mapdata[mindex]);

                    computeShaderForMap.SetBuffer(0, "themap", mapsbuffer);
                    computeShaderForMap.Dispatch(0, (mapx * mapy * mapz)/ 10, 1, 1);

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
                    mapsbuffer.Dispose();






                    ComputeBuffer maps0buffer = new ComputeBuffer(mapdata[mindex].Length, totalSize);
                    maps0buffer.SetData(mapdata[mindex]);

                    ComputeBuffer mapvertlocbufferx = new ComputeBuffer(datamapfirstvertxtop[mindex].Length, 4);
                    mapvertlocbufferx.SetData(datamapfirstvertxtop[mindex]);

                    ComputeBuffer mapvertlocbuffery = new ComputeBuffer(datamapfirstvertytop[mindex].Length, 4);
                    mapvertlocbuffery.SetData(datamapfirstvertytop[mindex]);

                    ComputeBuffer mapvertlocbufferz = new ComputeBuffer(datamapfirstvertztop[mindex].Length, 4);
                    mapvertlocbufferz.SetData(datamapfirstvertztop[mindex]);

                    ComputeBuffer mapwidthdimtop = new ComputeBuffer(datawidthdimtop[mindex].Length, 4);
                    mapwidthdimtop.SetData(datawidthdimtop[mindex]);

                    ComputeBuffer mapheightdimtop = new ComputeBuffer(dataheightdimtop[mindex].Length, 4);
                    mapheightdimtop.SetData(dataheightdimtop[mindex]);

                    ComputeBuffer mapdepthdimtop = new ComputeBuffer(datadepthdimtop[mindex].Length, 4);
                    mapdepthdimtop.SetData(datadepthdimtop[mindex]);






                    if (reducedverttrigswtc == 0)
                    {

                        /*
                        threadmulx = 8;
                        threadmuly = 8;
                        threadmulz = 8;*/


                        computeVertexesTOP.SetBuffer(0, "themap", maps0buffer);

                        computeVertexesTOP.SetBuffer(0, "mapfirstvertxtop", mapvertlocbufferx);
                        computeVertexesTOP.SetBuffer(0, "mapfirstvertytop", mapvertlocbuffery);
                        computeVertexesTOP.SetBuffer(0, "mapfirstvertztop", mapvertlocbufferz);

                        computeVertexesTOP.SetBuffer(0, "widthdimtop", mapwidthdimtop);
                        computeVertexesTOP.SetBuffer(0, "heightdimtop", mapheightdimtop);
                        computeVertexesTOP.SetBuffer(0, "depthdimtop", mapdepthdimtop);

                        computeVertexesTOP.Dispatch(0, threadmulx, threadmuly, threadmulz);




                       






                    }
                    else if (reducedverttrigswtc== 1)
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


                    mapvertlocbufferx.GetData(datamapfirstvertxtop[mindex]);
                    mapvertlocbuffery.GetData(datamapfirstvertytop[mindex]);
                    mapvertlocbufferz.GetData(datamapfirstvertztop[mindex]);

                    mapwidthdimtop.GetData(datawidthdimtop[mindex]);
                    mapheightdimtop.GetData(dataheightdimtop[mindex]);
                    mapdepthdimtop.GetData(datadepthdimtop[mindex]);

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



                                if (dataheightdimtop[mindex][index].thebyte == 0)//datamapfirstvertxtop[mindex][index].thebyte == 0 && datamapfirstvertytop[mindex][index].thebyte == 0 && datamapfirstvertztop[mindex][index].thebyte == 0)
                                {

                                }
                                else
                                {
                                    //if (mapint[index] == 1)//IsTransparent(x, y+1,z, originalmapdata[mindex]) && blockExistsInArray(x, y - 1 ,z) == 1)// mapint[index] == 1) //mapdata[mindex]
                                    {
                                        int indexofvert0 = vertices.Count;
                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                        int indexofvert1 = vertices.Count + 1;
                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                        int indexofvert2 = vertices.Count + 2;
                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                        int indexofvert3 = vertices.Count + 3;
                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));


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


              
                    
                    GameObject emptyobject = new GameObject();
                    var meshfilt = emptyobject.gameObject.AddComponent<MeshFilter>();
                    var meshrend = emptyobject.gameObject.AddComponent<MeshRenderer>();

                    Mesh thenewmesh = new Mesh();
                    thenewmesh.vertices = vertices.ToArray();
                    thenewmesh.triangles = triangles.ToArray();

                    emptyobject.gameObject.GetComponent<MeshFilter>().mesh = thenewmesh;
                    //_testChunk.GetComponent<MeshRenderer>().material = _mat;

                    emptyobject.gameObject.transform.position = chunkpos;
                    emptyobject.gameObject.transform.rotation = Quaternion.identity;

                    emptyobject.gameObject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    //parentobject.gameObject.GetComponent<MeshRenderer>().material = mat1;

                    //parentobject.gameObject.transform.parent = emptyobjectparent0.transform;
                    emptyobject.gameObject.gameObject.name = "top faces";
































                    /*

                    //LEFT FACE

                    mapdata[mindex] = new mapbytes[mapx * mapy * mapz];
                    datamapfirstvertxtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertytop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertztop[mindex] = new mapofints[mapx * mapy * mapz];

                    datawidthdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    dataheightdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datadepthdimtop[mindex] = new mapofints[mapx * mapy * mapz];

                    for (int x = 0; x < mapx; x++)
                    {
                        for (int y = 0; y < mapy; y++)
                        {
                            for (int z = 0; z < mapz; z++)
                            {
                                int index = x + mapx * (y + mapy * z);

                                mapdata[mindex][index] = new mapbytes();
                                mapdata[mindex][index].thebyte = 0;
                                mapdata[mindex][index].position = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);
                                mapdata[mindex][index].ix = x;
                                mapdata[mindex][index].iy = y;
                                mapdata[mindex][index].iz = z;

                                datamapfirstvertxtop[mindex][index] = new mapofints();
                                datamapfirstvertxtop[mindex][index].thebyte = 0;

                                datamapfirstvertytop[mindex][index] = new mapofints();
                                datamapfirstvertytop[mindex][index].thebyte = 0;

                                datamapfirstvertztop[mindex][index] = new mapofints();
                                datamapfirstvertztop[mindex][index].thebyte = 0;

                                datawidthdimtop[mindex][index] = new mapofints();
                                datawidthdimtop[mindex][index].thebyte = 0;

                                dataheightdimtop[mindex][index] = new mapofints();
                                dataheightdimtop[mindex][index].thebyte = 0;

                                datadepthdimtop[mindex][index] = new mapofints();
                                datadepthdimtop[mindex][index].thebyte = 0;
                            }
                        }
                    }


                    /*maps0buffer.Dispose();
                    mapvertlocbufferx.Dispose();
                    mapvertlocbufferz.Dispose();
                    mapwidthdimtop.Dispose();
                    mapheightdimtop.Dispose();
                    mapdepthdimtop.Dispose();
                    // _tempChunkArraybuffer.Dispose();





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
                   mapdepthdimtop.SetData(datadepthdimtop[mindex]);

                    maps0buffer.Release();
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
                    mapdepthdimtop.Dispose();


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
                    mapdepthdimtop.SetData(datadepthdimtop[mindex]);



                    mapvertlocbufferx.SetCounterValue(0);
                    mapvertlocbuffery.SetCounterValue(0);
                    mapvertlocbufferz.SetCounterValue(0);

                    mapwidthdimtop.SetCounterValue(0);
                    mapheightdimtop.SetCounterValue(0);
                    mapdepthdimtop.SetCounterValue(0);


                    computeVertexesLEFT.SetBuffer(0, "themap", maps0buffer);
                    computeVertexesLEFT.SetBuffer(0, "mapfirstvertxtop", mapvertlocbufferx);
                    computeVertexesLEFT.SetBuffer(0, "mapfirstvertytop", mapvertlocbuffery);
                    computeVertexesLEFT.SetBuffer(0, "mapfirstvertztop", mapvertlocbufferz);

                    computeVertexesLEFT.SetBuffer(0, "widthdimtop", mapwidthdimtop);
                    computeVertexesLEFT.SetBuffer(0, "heightdimtop", mapheightdimtop);
                    computeVertexesLEFT.SetBuffer(0, "depthdimtop", mapdepthdimtop);

                    computeVertexesLEFT.Dispatch(0, 2, 2, 2);

               


                    mapvertlocbufferx.GetData(datamapfirstvertxtop[mindex]);
                    mapvertlocbuffery.GetData(datamapfirstvertytop[mindex]);
                    mapvertlocbufferz.GetData(datamapfirstvertztop[mindex]);

                    mapwidthdimtop.GetData(datawidthdimtop[mindex]);
                    mapheightdimtop.GetData(dataheightdimtop[mindex]);
                    mapdepthdimtop.GetData(datadepthdimtop[mindex]);



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



                                if (datawidthdimtop[mindex][index].thebyte == 0)//datamapfirstvertxtop[mindex][index].thebyte == 0 && datamapfirstvertytop[mindex][index].thebyte == 0 && datamapfirstvertztop[mindex][index].thebyte == 0)
                                {

                                }
                                else
                                {
                                    //if (mapint[index] == 1)//IsTransparent(x, y+1,z, originalmapdata[mindex]) && blockExistsInArray(x, y - 1 ,z) == 1)// mapint[index] == 1) //mapdata[mindex]
                                    {


                                        int indexofvert0 = vertices.Count;
                                        Vector3 firstvertofface = new Vector3(datawidthdimtop[mindex][index].thebyte, datamapfirstvertytop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                        int indexofvert1 = vertices.Count + 1;
                                        Vector3 secondvertofface = new Vector3(datawidthdimtop[mindex][index].thebyte, datamapfirstvertytop[mindex][index].thebyte + (dataheightdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte);

                                        int indexofvert2 = vertices.Count + 2;
                                        Vector3 thirdvertofface = new Vector3(datawidthdimtop[mindex][index].thebyte, datamapfirstvertytop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                        int indexofvert3 = vertices.Count + 3;
                                        Vector3 fourthvertofface = new Vector3(datawidthdimtop[mindex][index].thebyte, datamapfirstvertytop[mindex][index].thebyte + (dataheightdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                        
                                        /*
                                        int indexofvert0 = vertices.Count;
                                        Vector3 firstvertofface = new Vector3((datawidthdimtop[mindex][index].thebyte), datamapfirstvertytop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                        int indexofvert1 = vertices.Count + 1;
                                        Vector3 secondvertofface = new Vector3((datawidthdimtop[mindex][index].thebyte), datamapfirstvertytop[mindex][index].thebyte + dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                        int indexofvert2 = vertices.Count + 2;
                                        Vector3 thirdvertofface = new Vector3((datawidthdimtop[mindex][index].thebyte), datamapfirstvertytop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                        int indexofvert3 = vertices.Count + 3;
                                        Vector3 fourthvertofface = new Vector3((datawidthdimtop[mindex][index].thebyte), datamapfirstvertytop[mindex][index].thebyte + dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                        

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
                                        
                                    }
                                }
                            }
                        }
                    }




                    if (vertices.Count > 0)
                    {
                        GameObject emptyobjectLEFT = new GameObject();
                        var meshfiltleft = emptyobjectLEFT.AddComponent<MeshFilter>();
                        var meshrendleft = emptyobjectLEFT.AddComponent<MeshRenderer>();

                        Mesh thenewmeshleft = new Mesh();
                        thenewmeshleft.vertices = vertices.ToArray();
                        thenewmeshleft.triangles = triangles.ToArray();

                        emptyobjectLEFT.GetComponent<MeshFilter>().mesh = thenewmeshleft;
                        //_testChunk.GetComponent<MeshRenderer>().material = _mat;

                        emptyobjectLEFT.transform.position = chunkmainpos;
                        emptyobjectLEFT.transform.rotation = Quaternion.identity;

                        emptyobjectLEFT.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        emptyobjectLEFT.GetComponent<MeshRenderer>().material = mat2;

                        emptyobjectLEFT.transform.parent = emptyobjectparentleftfaces.transform;
                        emptyobjectLEFT.gameObject.name = "left faces";
                    }


                    //LEFT FACE*/






















                    /*
                    //RIGHT FACE

                    mapvertlocbufferx.SetCounterValue(0);
                    mapvertlocbuffery.SetCounterValue(0);
                    mapvertlocbufferz.SetCounterValue(0);

                    mapwidthdimtop.SetCounterValue(0);
                    mapheightdimtop.SetCounterValue(0);
                    mapdepthdimtop.SetCounterValue(0);

                    datamapfirstvertxtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertytop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertztop[mindex] = new mapofints[mapx * mapy * mapz];
                    datawidthdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    dataheightdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datadepthdimtop[mindex] = new mapofints[mapx * mapy * mapz];

                    for (int x = 0; x < mapx; x++)
                    {
                        for (int y = 0; y < mapy; y++)
                        {
                            for (int z = 0; z < mapz; z++)
                            {
                                int index = x + mapx * (y + mapy * z);

                                mapdata[mindex][index] = new mapbytes();
                                mapdata[mindex][index].thebyte = 0;
                                mapdata[mindex][index].position = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);
                                mapdata[mindex][index].ix = x;
                                mapdata[mindex][index].iy = y;
                                mapdata[mindex][index].iz = z;

                                datamapfirstvertxtop[mindex][index] = new mapofints();
                                datamapfirstvertxtop[mindex][index].thebyte = 0;

                                datamapfirstvertytop[mindex][index] = new mapofints();
                                datamapfirstvertytop[mindex][index].thebyte = 0;

                                datamapfirstvertztop[mindex][index] = new mapofints();
                                datamapfirstvertztop[mindex][index].thebyte = 0;

                                datawidthdimtop[mindex][index] = new mapofints();
                                datawidthdimtop[mindex][index].thebyte = 0;

                                dataheightdimtop[mindex][index] = new mapofints();
                                dataheightdimtop[mindex][index].thebyte = 0;

                                datadepthdimtop[mindex][index] = new mapofints();
                                datadepthdimtop[mindex][index].thebyte = 0;
                            }
                        }
                    }




                    

                    computeVertexesRIGHT.SetBuffer(0, "themap", maps0buffer);
                    computeVertexesRIGHT.SetBuffer(0, "mapfirstvertxtop", mapvertlocbufferx);
                    //computeVertexesRIGHT.SetBuffer(0, "mapfirstvertytop", mapvertlocbuffery);
                    computeVertexesRIGHT.SetBuffer(0, "mapfirstvertztop", mapvertlocbufferz);

                    computeVertexesRIGHT.SetBuffer(0, "widthdimtop", mapwidthdimtop);
                    computeVertexesRIGHT.SetBuffer(0, "heightdimtop", mapheightdimtop);
                    computeVertexesRIGHT.SetBuffer(0, "depthdimtop", mapdepthdimtop);

                    computeVertexesRIGHT.Dispatch(0, 2, 2, 2);




                    mapvertlocbufferx.GetData(datamapfirstvertxtop[mindex]);
                    //mapvertlocbuffery.GetData(datamapfirstvertytop[mindex]);
                    mapvertlocbufferz.GetData(datamapfirstvertztop[mindex]);

                    mapwidthdimtop.GetData(datawidthdimtop[mindex]);
                    mapheightdimtop.GetData(dataheightdimtop[mindex]);
                    mapdepthdimtop.GetData(datadepthdimtop[mindex]);



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



                                if (dataheightdimtop[mindex][index].thebyte == 0)//datamapfirstvertxtop[mindex][index].thebyte == 0 && datamapfirstvertytop[mindex][index].thebyte == 0 && datamapfirstvertztop[mindex][index].thebyte == 0)
                                {

                                }
                                else
                                {
                                    //if (mapint[index] == 1)//IsTransparent(x, y+1,z, originalmapdata[mindex]) && blockExistsInArray(x, y - 1 ,z) == 1)// mapint[index] == 1) //mapdata[mindex]
                                    {



                                        int indexofvert0 = vertices.Count;
                                        Vector3 firstvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                        int indexofvert1 = vertices.Count + 1;
                                        Vector3 secondvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte);

                                        int indexofvert2 = vertices.Count + 2;
                                        Vector3 thirdvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                        int indexofvert3 = vertices.Count + 3;
                                        Vector3 fourthvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));


                                        

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
                                        
                                    }
                                }
                            }
                        }
                    }




                    if (vertices.Count > 0)
                    {
                        GameObject emptyobjectright = new GameObject();
                        var meshfiltright = emptyobjectright.AddComponent<MeshFilter>();
                        var meshrendright = emptyobjectright.AddComponent<MeshRenderer>();

                        Mesh thenewmeshright = new Mesh();
                        thenewmeshright.vertices = vertices.ToArray();
                        thenewmeshright.triangles = triangles.ToArray();

                        emptyobjectright.GetComponent<MeshFilter>().mesh = thenewmeshright;
                        //_testChunk.GetComponent<MeshRenderer>().material = _mat;

                        emptyobjectright.transform.position = chunkmainpos;
                        emptyobjectright.transform.rotation = Quaternion.identity;

                        emptyobjectright.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        emptyobjectright.GetComponent<MeshRenderer>().material = rightfacemat;

                        emptyobjectright.transform.parent = emptyobjectparentrightfaces.transform;
                        emptyobjectright.gameObject.name = "right faces";
                    }
                    //RIGHTFACE*/























                    /*
                    //FRONT FACE

                    mapvertlocbufferx.SetCounterValue(0);
                    mapvertlocbuffery.SetCounterValue(0);
                    mapvertlocbufferz.SetCounterValue(0);

                    mapwidthdimtop.SetCounterValue(0);
                    mapheightdimtop.SetCounterValue(0);
                    mapdepthdimtop.SetCounterValue(0);

                    datamapfirstvertxtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertytop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertztop[mindex] = new mapofints[mapx * mapy * mapz];
                    datawidthdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    dataheightdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datadepthdimtop[mindex] = new mapofints[mapx * mapy * mapz];

                    for (int x = 0; x < mapx; x++)
                    {
                        for (int y = 0; y < mapy; y++)
                        {
                            for (int z = 0; z < mapz; z++)
                            {
                                int index = x + mapx * (y + mapy * z);

                                mapdata[mindex][index] = new mapbytes();
                                mapdata[mindex][index].thebyte = 0;
                                mapdata[mindex][index].position = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);
                                mapdata[mindex][index].ix = x;
                                mapdata[mindex][index].iy = y;
                                mapdata[mindex][index].iz = z;

                                datamapfirstvertxtop[mindex][index] = new mapofints();
                                datamapfirstvertxtop[mindex][index].thebyte = 0;

                                datamapfirstvertytop[mindex][index] = new mapofints();
                                datamapfirstvertytop[mindex][index].thebyte = 0;

                                datamapfirstvertztop[mindex][index] = new mapofints();
                                datamapfirstvertztop[mindex][index].thebyte = 0;

                                datawidthdimtop[mindex][index] = new mapofints();
                                datawidthdimtop[mindex][index].thebyte = 0;

                                dataheightdimtop[mindex][index] = new mapofints();
                                dataheightdimtop[mindex][index].thebyte = 0;

                                datadepthdimtop[mindex][index] = new mapofints();
                                datadepthdimtop[mindex][index].thebyte = 0;
                            }
                        }
                    }






                    computeVertexesRIGHT.SetBuffer(0, "themap", maps0buffer);
                    computeVertexesRIGHT.SetBuffer(0, "mapfirstvertxtop", mapvertlocbufferx);
                    //computeVertexesRIGHT.SetBuffer(0, "mapfirstvertytop", mapvertlocbuffery);
                    computeVertexesRIGHT.SetBuffer(0, "mapfirstvertztop", mapvertlocbufferz);

                    computeVertexesRIGHT.SetBuffer(0, "widthdimtop", mapwidthdimtop);
                    computeVertexesRIGHT.SetBuffer(0, "heightdimtop", mapheightdimtop);
                    computeVertexesRIGHT.SetBuffer(0, "depthdimtop", mapdepthdimtop);

                    computeVertexesRIGHT.Dispatch(0, 2, 2, 2);




                    mapvertlocbufferx.GetData(datamapfirstvertxtop[mindex]);
                    //mapvertlocbuffery.GetData(datamapfirstvertytop[mindex]);
                    mapvertlocbufferz.GetData(datamapfirstvertztop[mindex]);

                    mapwidthdimtop.GetData(datawidthdimtop[mindex]);
                    mapheightdimtop.GetData(dataheightdimtop[mindex]);
                    mapdepthdimtop.GetData(datadepthdimtop[mindex]);



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



                                if (dataheightdimtop[mindex][index].thebyte == 0)//datamapfirstvertxtop[mindex][index].thebyte == 0 && datamapfirstvertytop[mindex][index].thebyte == 0 && datamapfirstvertztop[mindex][index].thebyte == 0)
                                {

                                }
                                else
                                {
                                    //if (mapint[index] == 1)//IsTransparent(x, y+1,z, originalmapdata[mindex]) && blockExistsInArray(x, y - 1 ,z) == 1)// mapint[index] == 1) //mapdata[mindex]
                                    {

                                        
                                        int indexofvert0 = vertices.Count;
                                        Vector3 firstvertofface = new Vector3(datamapfirstvertztop[mindex][index].thebyte, (datamapfirstvertxtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte);

                                        int indexofvert1 = vertices.Count + 1;
                                        Vector3 secondvertofface = new Vector3(datamapfirstvertztop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte);

                                        int indexofvert2 = vertices.Count + 2;
                                        Vector3 thirdvertofface = new Vector3( datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte), datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte);

                                        int indexofvert3 = vertices.Count + 3;
                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte), datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte);

                                        /*int indexofvert0 = vertices.Count;
                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte,mapz-1- dataheightdimtop[mindex][index].thebyte);

                                        int indexofvert1 = vertices.Count + 1;
                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte, mapz - 1 - dataheightdimtop[mindex][index].thebyte);

                                        int indexofvert2 = vertices.Count + 2;
                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte), mapz - 1 - dataheightdimtop[mindex][index].thebyte);

                                        int indexofvert3 = vertices.Count + 3;
                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte), mapz - 1 - dataheightdimtop[mindex][index].thebyte);
                                        */

                                        /*int indexofvert0 = vertices.Count;
                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                        int indexofvert1 = vertices.Count + 1;
                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                        int indexofvert2 = vertices.Count + 2;
                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                        int indexofvert3 = vertices.Count + 3;
                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                        

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
                                        
                                    }
                                }
                            }
                        }
                    }




                    if (vertices.Count > 0)
                    {
                        GameObject emptyobjectright = new GameObject();
                        var meshfiltright = emptyobjectright.AddComponent<MeshFilter>();
                        var meshrendright = emptyobjectright.AddComponent<MeshRenderer>();

                        Mesh thenewmeshright = new Mesh();
                        thenewmeshright.vertices = vertices.ToArray();
                        thenewmeshright.triangles = triangles.ToArray();

                        emptyobjectright.GetComponent<MeshFilter>().mesh = thenewmeshright;
                        //_testChunk.GetComponent<MeshRenderer>().material = _mat;

                        emptyobjectright.transform.position = chunkmainpos;
                        emptyobjectright.transform.rotation = Quaternion.identity;

                        emptyobjectright.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                        emptyobjectright.GetComponent<MeshRenderer>().material = rightfacemat;

                        emptyobjectright.transform.parent = emptyobjectparentfrontfaces.transform;
                        emptyobjectright.gameObject.name = "front faces";
                    }
                    //FRONTFACES
                    */


























                
                    maps0buffer.Release();
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
                    mapdepthdimtop.Dispose();
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



        //return mapint;

    }









    // Update is called once per frame
    void Update()
    {
        
    }
}
