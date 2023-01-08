using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class sccsverttrigreducedmapcs : MonoBehaviour
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

    public int levelsizex = 2;
    public int levelsizey = 1;
    public int levelsizez = 2;
    public int mapx = 40;
    public int mapy = 40;
    public int mapz = 40;

    public ComputeShader computeShaderForMap;


    public Material mat;


    public int threadmulx = 2;
    public int threadmuly = 2;
    public int threadmulz = 2;


    int reducedverttrigswtc = 0;

    // Start is called before the first frame update
    void Start()
    {
        mapdata = new mapbytes[levelsizex * levelsizey * levelsizez][];


        GameObject emptyobjectparent0 = this.transform.gameObject;
        emptyobjectparent0.gameObject.name = "verttrigreducednocs";



        for (int mx = 0; mx < levelsizex; mx++)
        {
            for (int my = 0; my < levelsizey; my++)
            {
                for (int mz = 0; mz < levelsizez; mz++)
                {
                    int mindex = mx + levelsizex * (my + levelsizey * mz);

                    Vector3 chunkmainpos = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f) + this.transform.position ;


                    //int totalSize = mapx * mapy * mapz;
                    mapdata[mindex] = new mapbytes[mapx * mapy * mapz];

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
                    mapsbuffer.Release();
                    mapsbuffer.Dispose();








                    
                    
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
                    emptyobject1.transform.parent = emptyobjectparent0.transform;
                    emptyobject1.transform.gameObject.name = "verttrigreducednocs";



                }
            }
        }





    }









    // Update is called once per frame
    void Update()
    {
        
    }
}
