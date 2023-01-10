using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class sccscomputevoxelrev1 : MonoBehaviour
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
    private mapofints[][] datawidthfacedim;
    private mapofints[][] dataheightfacedim;
    private mapofints[][] datadepthfacedim;

    int levelsizex = 1;
    int levelsizey = 1;
    int levelsizez = 1;
    int mapx = 10;
    int mapy = 10;
    int mapz = 10;

    public ComputeShader computeShaderForMap;
    public ComputeShader computeVertexes;

    public Material mat1;
    public Material mat;


    // Start is called before the first frame update
    void Start()
    {
        mapdata = new mapbytes[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertxtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertytop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertztop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datawidthfacedim = new mapofints[levelsizex * levelsizey * levelsizez][];
        dataheightfacedim = new mapofints[levelsizex * levelsizey * levelsizez][];
        datadepthfacedim = new mapofints[levelsizex * levelsizey * levelsizez][];


        GameObject emptyobjectparent0 = new GameObject();
        GameObject emptyobjectparent1 = new GameObject();

        for (int mx = 0; mx < levelsizex; mx++)
        {
            for (int my = 0; my < levelsizey; my++)
            {
                for (int mz = 0; mz < levelsizez; mz++)
                {
                    int mindex = mx + levelsizex * (my + levelsizey * mz);

                    Vector3 chunkmainpos = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);


                    //int totalSize = mapx * mapy * mapz;
                    mapdata[mindex] = new mapbytes[mapx * mapy * mapz];
                    datamapfirstvertxtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertytop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertztop[mindex] = new mapofints[mapx * mapy * mapz];
                    datawidthfacedim[mindex] = new mapofints[mapx * mapy * mapz];
                    dataheightfacedim[mindex] = new mapofints[mapx * mapy * mapz];
                    datadepthfacedim[mindex] = new mapofints[mapx * mapy * mapz];

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

                                datawidthfacedim[mindex][index] = new mapofints();
                                datawidthfacedim[mindex][index].thebyte = 0;

                                dataheightfacedim[mindex][index] = new mapofints();
                                dataheightfacedim[mindex][index].thebyte = 0;

                                datadepthfacedim[mindex][index] = new mapofints();
                                datadepthfacedim[mindex][index].thebyte = 0;
                            }
                        }
                    }







                    int thebytesize = sizeof(int) * 4;
                    int vector3Size = sizeof(float) * 3;
                    int totalSize = thebytesize + vector3Size;

                    ComputeBuffer mapsbuffer = new ComputeBuffer(mapdata[mindex].Length, totalSize);

                    mapsbuffer.SetData(mapdata[mindex]);

                    computeShaderForMap.SetBuffer(0, "themap", mapsbuffer);
                    computeShaderForMap.Dispatch(0, (mapx * mapy * mapz)/ 10, 1, 1);

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







                    ComputeBuffer maps0buffer = new ComputeBuffer(mapdata[mindex].Length, totalSize);
                    maps0buffer.SetData(mapdata[mindex]);

                    ComputeBuffer mapvertlocbufferx = new ComputeBuffer(datamapfirstvertxtop[mindex].Length, 4);
                    mapvertlocbufferx.SetData(datamapfirstvertxtop[mindex]);

                    ComputeBuffer mapvertlocbuffery = new ComputeBuffer(datamapfirstvertytop[mindex].Length, 4);
                    mapvertlocbuffery.SetData(datamapfirstvertytop[mindex]);

                    ComputeBuffer mapvertlocbufferz = new ComputeBuffer(datamapfirstvertztop[mindex].Length, 4);
                    mapvertlocbufferz.SetData(datamapfirstvertztop[mindex]);

                    ComputeBuffer mapwidthfacedim = new ComputeBuffer(datawidthfacedim[mindex].Length, 4);
                    mapwidthfacedim.SetData(datawidthfacedim[mindex]);

                    ComputeBuffer mapheightfacedim = new ComputeBuffer(dataheightfacedim[mindex].Length, 4);
                    mapheightfacedim.SetData(dataheightfacedim[mindex]);

                    ComputeBuffer mapdepthfacedim = new ComputeBuffer(datadepthfacedim[mindex].Length, 4);
                    mapdepthfacedim.SetData(datadepthfacedim[mindex]);


                    computeVertexes.SetBuffer(0, "themap", maps0buffer);
                    computeVertexes.SetBuffer(0, "mapfirstvertxloc", mapvertlocbufferx);
                    //computeVertexes.SetBuffer(0, "mapfirstvertyloc", mapvertlocbuffery);
                    computeVertexes.SetBuffer(0, "mapfirstvertzloc", mapvertlocbufferz);

                    computeVertexes.SetBuffer(0, "widthfacedim", mapwidthfacedim);
                    computeVertexes.SetBuffer(0, "heightfacedim", mapheightfacedim);
                    computeVertexes.SetBuffer(0, "depthfacedim", mapdepthfacedim);


                    computeVertexes.Dispatch(0, 1, 1, 1);



                    mapvertlocbufferx.GetData(datamapfirstvertxtop[mindex]);
                    //mapvertlocbuffery.GetData(datamapfirstvertytop[mindex]);
                    mapvertlocbufferz.GetData(datamapfirstvertztop[mindex]);

                    mapwidthfacedim.GetData(datawidthfacedim[mindex]);
                    mapheightfacedim.GetData(dataheightfacedim[mindex]);
                    mapdepthfacedim.GetData(datadepthfacedim[mindex]);



                    List<Vector3> vertices = new List<Vector3>();
                    List<int> triangles = new List<int>();


                    for (int x = 0; x < mapx; x++)
                    {
                        for (int y = 0; y < mapy; y++)
                        {
                            for (int z = 0; z < mapz; z++)
                            {
                                int index = x + mapx * (y + mapy * z);

                                //mapint[index] = data[mindex][index].thebyte;

                                //Debug.Log("map:" + data[index].thebyte);



                                if (dataheightfacedim[mindex][index].thebyte == 0)//datamapfirstvertxtop[mindex][index].thebyte == 0 && datamapfirstvertytop[mindex][index].thebyte == 0 && datamapfirstvertztop[mindex][index].thebyte == 0)
                                {

                                }
                                else
                                {
                                    //if (mapint[index] == 1)//IsTransparent(x, y+1,z, originalmapdata[mindex]) && blockExistsInArray(x, y - 1 ,z) == 1)// mapint[index] == 1) //mapdata[mindex]
                                    {
                                        int indexofvert0 = vertices.Count;
                                        Vector3 firstvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightfacedim[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                        int indexofvert1 = vertices.Count + 1;
                                        Vector3 secondvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthfacedim[mindex][index].thebyte), dataheightfacedim[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                        int indexofvert2 = vertices.Count + 2;
                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightfacedim[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthfacedim[mindex][index].thebyte));

                                        int indexofvert3 = vertices.Count + 3;
                                        Vector3 fourthvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthfacedim[mindex][index].thebyte), dataheightfacedim[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthfacedim[mindex][index].thebyte));


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







                    maps0buffer.Dispose();
                    mapvertlocbufferx.Dispose();
                    mapvertlocbufferz.Dispose();
                    mapwidthfacedim.Dispose();
                    mapheightfacedim.Dispose();
                    mapdepthfacedim.Dispose();
                    // _tempChunkArraybuffer.Dispose();





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
                    emptyobject.GetComponent<MeshRenderer>().material = mat1;

                    emptyobject.transform.parent = emptyobjectparent0.transform;









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
                    emptyobject1.transform.parent = emptyobjectparent1.transform;





                }
            }
        }





    }









    // Update is called once per frame
    void Update()
    {
        
    }
}
