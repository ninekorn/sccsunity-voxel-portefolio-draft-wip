using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class sccscomputevoxelshrinked : MonoBehaviour
{
    public struct mapbytes
    {
        public int ix;
        public int iy;
        public int iz;
        public int thebyte;
        public Vector3 position;
    }




    private mapbytes[][] mapdata;

    public int levelsizex = 1;
    public int levelsizey = 1;
    public int levelsizez = 1;
    public int mapx = 10;
    public int mapy = 10;
    public int mapz = 10;

    ComputeShader computeShaderForMap;

    // Start is called before the first frame update
   public void CreateMapArrays(Vector3 chunkpos)
    {
        mapdata = new mapbytes[levelsizex * levelsizey * levelsizez][];

        int[] mapint = new int[mapx * mapy * mapz];
        for (int mx = 0; mx < levelsizex; mx++)
        {
            for (int my = 0; my < levelsizey; my++)
            {
                for (int mz = 0; mz < levelsizez; mz++)
                {
                    int mindex = mx + levelsizex * (my + levelsizey * mz);

                    chunkpos += new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);

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
                                mapdata[mindex][index].position = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f) + chunkpos;
                                mapdata[mindex][index].ix = x;
                                mapdata[mindex][index].iy = y;
                                mapdata[mindex][index].iz = z;

                            }
                        }
                    }


                    






                }
            }
        }

        //Debug.Log("arraycreated");
        //return mapint;
    }


    ComputeBuffer mapsbuffer;


    int shaderinit = 0;
    public int[] workonshader()
    {



        /*if (computeShaderForMap != null)
        {
            GC.SuppressFinalize(computeShaderForMap);
            computeShaderForMap = null;
        }*/

        int mindex = 0;
        int[] mapint = new int[mapx * mapy * mapz];


        int thebytesize = sizeof(int) * 4;
        int vector3Size = sizeof(float) * 3;
        int totalSize = thebytesize + vector3Size;


        if (shaderinit == 0)
        {
            mapsbuffer = new ComputeBuffer(mapdata[mindex].Length, totalSize);



            computeShaderForMap = (ComputeShader)Resources.Load("Compute/sccsmap");//ComputeShader.Find("Transparent/Diffuse");
            //shaderinit = 1;
        }
        mapsbuffer.SetCounterValue(0);
        mapsbuffer.SetData(mapdata[mindex]);



        if (computeShaderForMap == null)
        {
            Debug.Log("computeShaderForMap null");
        }
        else
        {
            //Debug.Log("computeShaderForMap !null");

        }

        computeShaderForMap.SetBuffer(0, "themap", mapsbuffer);
        computeShaderForMap.Dispatch(0, (mapx * mapy * mapz) / 10, 1, 1);

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

        return mapint;
    }







    // Update is called once per frame
    void Update()
    {
        
    }
}
