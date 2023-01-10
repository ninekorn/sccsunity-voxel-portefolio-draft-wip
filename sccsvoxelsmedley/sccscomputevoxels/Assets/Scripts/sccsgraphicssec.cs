using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

//https://forum.unity.com/threads/how-can-we-access-thread-group-and-global-variables-from-threads-in-compute-shader.468306/
public class sccsgraphicssec : MonoBehaviour
{
    public struct MapStruct
    {
        public int extrabyte;
        public int cx;
        public int cy;
        public int cz;
        public int ix;
        public int iy;
        public int iz;
        public int thebyte;
        public Vector3 position;
    }



    public struct ResultStructSwitches
    {
        public int swtc;
        public int tindex;
        public int cx;
        public int cy;
        public int cz;
        public int ti;
    }


    struct mapofints
    {
        public int thebyte;
    };

    public GameObject visualobject0;
    public GameObject visualobject1;
    public GameObject visualobject2;
    public GameObject visualobject3;


    private MapStruct[][] originalmapdata;
    private MapStruct[][] mapdata;
    private mapofints[][] datamapfirstvertxtop;
    private mapofints[][] datamapfirstvertytop;
    private mapofints[][] datamapfirstvertztop;
    private mapofints[][] datawidthdimtop;
    private mapofints[][] dataheightdimtop;
    private mapofints[][] datadepthdimtop;
    private mapofints[][] datatemparray;


    int levelsizex = 1;
    int levelsizey = 1;
    int levelsizez = 1;

    int mapx = 10;
    int mapy = 10;
    int mapz = 10;

    private int groupSize = 10;

    public Material mat;

    public ComputeShader computeShaderForMap;
    public ComputeShader compute;
    private ComputeBuffer buffer;
    public ComputeBuffer buffer1;

    ComputeBuffer triCountBuffer;

    private void Start()
    {
        originalmapdata = new MapStruct[levelsizex * levelsizey * levelsizez][];
        mapdata = new MapStruct[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertxtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertytop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertztop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datawidthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        dataheightdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datadepthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datatemparray = new mapofints[levelsizex * levelsizey * levelsizez][];

        for (int mx = 0; mx < levelsizex; mx++)
        {
            for (int my = 0; my < levelsizey; my++)
            {
                for (int mz = 0; mz < levelsizez; mz++)
                {
                    int mindex = mx + levelsizex * (my + levelsizey * mz);

                    //int totalSize = mapx * mapy * mapz;

                    originalmapdata[mindex] = new MapStruct[mapx * mapy * mapz];
                    mapdata[mindex] = new MapStruct[mapx * mapy * mapz];



                    datamapfirstvertxtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertytop[mindex] = new mapofints[mapx * mapy * mapz];
                    datamapfirstvertztop[mindex] = new mapofints[mapx * mapy * mapz];
                    datawidthdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    dataheightdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datadepthdimtop[mindex] = new mapofints[mapx * mapy * mapz];
                    datatemparray[mindex] = new mapofints[mapx * mapy * mapz];


                    for (int x = 0; x < mapx; x++)
                    {
                        for (int y = 0; y < mapy; y++)
                        {
                            for (int z = 0; z < mapz; z++)
                            {
                                int index = x + mapx * (y + mapy * z);



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


                                datatemparray[mindex][index] = new mapofints();
                                datatemparray[mindex][index].thebyte = 0;






                                originalmapdata[mindex][index] = new MapStruct();
                                originalmapdata[mindex][index].thebyte = 0;
                                originalmapdata[mindex][index].position = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);
                                originalmapdata[mindex][index].ix = x;
                                originalmapdata[mindex][index].iy = y;
                                originalmapdata[mindex][index].iz = z;


                                mapdata[mindex][index] = new MapStruct();
                                mapdata[mindex][index].thebyte = 0;
                                mapdata[mindex][index].position = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);
                                mapdata[mindex][index].ix = x;
                                mapdata[mindex][index].iy = y;
                                mapdata[mindex][index].iz = z;
                            }
                        }
                    }

                }
            }
        }

        int thebytesize = sizeof(int) * 8;
        int vector3Size = sizeof(float) * 3;
        int totalSize = thebytesize + vector3Size;

        //for (int mx = 0; mx < levelsizex; mx++)
        {
            //for (int my = 0; my < levelsizey; my++)
            {
                //for (int mz = 0; mz < levelsizez; mz++)
                {
                    int mx = 0;
                    int my = 0;
                    int mz = 0;

                    int mindex = mx + levelsizex * (my + levelsizey * mz);

                    Vector3 chunkmainpos = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);

                    //int totalSize = mapx * mapy * mapz;
                    //data[mindex] = new MapStruct[mapx * mapy * mapz];

                    ComputeBuffer mapsbuffer = new ComputeBuffer(mapdata[mindex].Length, totalSize);

                    mapsbuffer.SetData(mapdata[mindex]);

                    computeShaderForMap.SetBuffer(0, "themap", mapsbuffer);
                    //computeShader.SetFloat("resolution", data.Length);
                    //computeShader.SetFloat("repetitions", repetitions);
                    computeShaderForMap.Dispatch(0, mapdata[mindex].Length / 10, 1, 1);



                    mapdata[mindex] = new MapStruct[mapx * mapy * mapz];
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


                    ComputeBuffer mapwidthdimtop = new ComputeBuffer(datawidthdimtop[mindex].Length, 4);
                    mapwidthdimtop.SetData(datawidthdimtop[mindex]);


                    ComputeBuffer mapheightdimtop = new ComputeBuffer(dataheightdimtop[mindex].Length, 4);
                    mapheightdimtop.SetData(dataheightdimtop[mindex]);


                    ComputeBuffer mapdepthdimtop = new ComputeBuffer(datadepthdimtop[mindex].Length, 4);
                    mapdepthdimtop.SetData(datadepthdimtop[mindex]);



                    ComputeBuffer _tempChunkArraybuffer = new ComputeBuffer(datatemparray[mindex].Length, 4);
                    _tempChunkArraybuffer.SetData(datatemparray[mindex]);










                    groupSize = 10;

                    int[] thegroupsize = new int[1];

                    for (int i = 0; i < thegroupsize.Length; i++)
                    {
                        thegroupsize[i] = 0;
                    }

                    buffer = new ComputeBuffer(groupSize, sizeof(int) * thegroupsize.Length);



                    //buffer = new ComputeBuffer(groupSize, sizeof(int));

                    ResultStructSwitches[] ResultStructSwitches = new ResultStructSwitches[1];
                    ResultStructSwitches[0] = new ResultStructSwitches();
                    ResultStructSwitches[0].swtc = 0;
                    ResultStructSwitches[0].tindex = 0;
                    ResultStructSwitches[0].cx = 0;
                    ResultStructSwitches[0].cy = 0;
                    ResultStructSwitches[0].cz = 0;
                    ResultStructSwitches[0].ti = 0;

                    buffer1 = new ComputeBuffer(groupSize, sizeof(int) * 6);



                    compute.SetBuffer(0, "themap", maps0buffer);
                    //compute.SetBuffer(0, "vertdatabuffer", thevertdatabuffer0);
                    //compute.SetBuffer(0, "_Results", groupshaderbuffer);


                    compute.SetBuffer(0, "mapfirstvertxtop", mapvertlocbufferx);
                    //computeShaderVertexMaps.SetBuffer(0, "mapfirstvertytop", mapvertlocbuffery);
                    compute.SetBuffer(0, "mapfirstvertztop", mapvertlocbufferz);

                    compute.SetBuffer(0, "widthdimtop", mapwidthdimtop);
                    compute.SetBuffer(0, "heightdimtop", mapheightdimtop);
                    compute.SetBuffer(0, "depthdimtop", mapdepthdimtop);

                    compute.SetBuffer(0, "_tempChunkArray", _tempChunkArraybuffer);


                    //compute.SetBuffer(0, "_Results", buffer);
                    compute.SetBuffer(0, "ResultStructSwitchesBuffer", buffer1);


                    /*triCountBuffer = new ComputeBuffer(1, sizeof(int), ComputeBufferType.Raw);

                    int maxVerticesCount = (mapx * mapy * mapz) * 6 * 4;
                    ComputeBuffer verticesBuffer;
                    verticesBuffer = new ComputeBuffer(maxVerticesCount, sizeof(float) * 3, ComputeBufferType.Append);
                    verticesBuffer.SetCounterValue(0);
                    compute.SetBuffer(0, "vertexlist", verticesBuffer);
                    */




                    compute.Dispatch(0, 10, 1, 1); //mapdata[mindex].Length/10 //mapdata[mindex].Length / 





                    /*
                    ComputeBuffer.CopyCount(verticesBuffer, triCountBuffer, 0);
                    int[] triCountArray = { 0};
                    triCountBuffer.GetData(triCountArray);
                    int numVerts = triCountArray[0];

                    Debug.Log("vertices:" + numVerts);*/




                    /*
                    Vector3[] vertices = new Vector3[numVerts];
                    verticesBuffer.GetData(vertices, 0, 0, numVerts);
                    Debug.Log("vertices:" + vertices.Length);*/



                    //buffer.SetCounterValue(0);

                    var results1 = new ResultStructSwitches[groupSize];
                    buffer1.GetData(results1);

                    var results = new int[groupSize];
                    buffer.GetData(results);

                    var message = "";
                    foreach (var result in results)
                    {
                        message = message + result.ToString() + " ";
                    }

                    Debug.Log(message);

                    // Debug.Log("results swtc:" + results1[0].swtc + "/results index:" + results1[0].tindex);



                    foreach (var result in results1)
                    {
                        message = message + result.swtc.ToString() + " " + result.tindex.ToString() + " ";
                    }
                    Debug.Log(message);







                    buffer.Release();
                    buffer.Dispose();





                    mapvertlocbufferx.GetData(datamapfirstvertxtop[mindex]);
                    mapvertlocbufferz.GetData(datamapfirstvertztop[mindex]);

                    mapwidthdimtop.GetData(datawidthdimtop[mindex]);
                    mapheightdimtop.GetData(dataheightdimtop[mindex]);
                    mapdepthdimtop.GetData(datadepthdimtop[mindex]);



                    //for (int i = 0; i < datamapfirstvertxtop[mindex].Length; i++)
                    //{
                    //    Debug.Log(datamapfirstvertxtop[mindex][i].thebyte);
                    //}

                    //thevertdatabuffer.GetData(thevertdata);
                    //Debug.Log(thevertdata[0].foundvert0);




                    /*
                    int firstvertblock = themap[xi + width * (yi + height * zi)].thebyte;
                    */






                    for (int x = 0; x < mapx; x++)
                    {
                        for (int y = 0; y < mapy; y++)
                        {
                            for (int z = 0; z < mapz; z++)
                            {
                                int index = x + mapx * (y + mapy * z);

                                //mapint[index] = data[mindex][index].thebyte;

                                //Debug.Log("map:" + data[index].thebyte);




                                if (mapint[index] == 1)//IsTransparent(x, y+1,z, originalmapdata[mindex]) && blockExistsInArray(x, y - 1 ,z) == 1)// mapint[index] == 1) //mapdata[mindex]
                                {
                                    Vector3 firstvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                    Vector3 secondvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                    Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                    Vector3 fourthvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));



                                    Instantiate(visualobject0, firstvertofface * 0.1f, Quaternion.identity);
                                    Instantiate(visualobject1, secondvertofface * 0.1f, Quaternion.identity);
                                    Instantiate(visualobject2, thirdvertofface * 0.1f, Quaternion.identity);
                                    Instantiate(visualobject3, fourthvertofface * 0.1f, Quaternion.identity);
                                }
                            }
                        }
                    }







                    maps0buffer.Dispose();
                    mapvertlocbufferx.Dispose();
                    mapvertlocbufferz.Dispose();
                    mapwidthdimtop.Dispose();
                    mapheightdimtop.Dispose();
                    mapdepthdimtop.Dispose();
                    _tempChunkArraybuffer.Dispose();





                    GameObject emptyobject = new GameObject();

                    //chunko thechunk = new chunko();

                    emptyobject.AddComponent<singleChunk>();

                    singleChunk thechunk = emptyobject.GetComponent<singleChunk>();
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
                    thechunk._testChunk = emptyobject;
                    thechunk.startthegen(chunkmainpos);

                    emptyobject.transform.position = chunkmainpos;
                    emptyobject.transform.rotation = Quaternion.identity;


                }
            }
        }














        /*
        buffer = new ComputeBuffer(groupSize, sizeof(int));
        compute.SetBuffer(0, "_Results", buffer);
        compute.Dispatch(0, 1, 1, 1);

        var results = new int[groupSize];
        buffer.GetData(results);
        buffer.Release();

        var message = "";
        foreach (var result in results)
        {
            message = message + result.ToString() + " ";
        }
        Debug.Log(message);*/
    }

    int blockExistsInArray(int _x, int _y, int _z)
    {
        if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= mapx) || (_y >= mapy) || (_z >= mapz))
        {
            return 0;
        }
        /*else if (_chunkArray[_x + width * (_y + height * _z)]==0)
        {
            return false;
        }*/
        else
        {
            return 1;
        }
        //return _chunkArray[_x + width * (_y + height * _z)] == 0;
    }


    bool IsTransparent(int _x, int _y, int _z, MapStruct[] themap)
    {
        if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= mapx) || (_y >= mapy) || (_z >= mapz)) return true;
        return themap[_x + mapx * (_y + mapy * _z)].thebyte == 0;
    }

    /*
    int IsTransparent(int _x, int _y, int _z, MapStruct[] themap)
    {
        if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= mapx) || (_y >= mapy) || (_z >= mapz))
        {
            return -1;
        }
        else
        {
            return themap[_x + mapx * (_y + mapy * _z)].thebyte;
        }
    }*/
}
