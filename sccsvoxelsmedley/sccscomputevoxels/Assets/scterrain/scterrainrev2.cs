using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scterrainrev2 : MonoBehaviour
{
    public class chunkdata
    {
        public int indexx;
        public int indexy;
        public int indexz;
        public int theindex;
        public Vector3 position;
        public GameObject thegameobject;
    }




    public int sizelx = 2;
    public int sizerx = 1;
    public int sizeby = 2;
    public int sizety = 1;
    public int sizebz = 2;
    public int sizefz = 1;

    // Start is called before the first frame update









    chunkdata[][] chunkarray;

    void Start()
    {
        int total = (sizelx + sizerx + 1) * (sizeby + sizety + 1) * (sizebz + sizefz + 1);

        chunkarray = new chunkdata[6][];



        for (int f = 0; f < 6; f++)
        {
            int facetype = f;

            chunkarray[facetype] = new chunkdata[total];

            Vector3 parentposchunk = Vector3.zero;

            //Debug.Log("total:"+total);


            GameObject theunqueuedobjectparent = NewObjectPoolerScript.current.GetPooledObject();
            theunqueuedobjectparent.transform.position = parentposchunk;//new Vector3(posx, posy, posz);
            theunqueuedobjectparent.transform.parent = this.transform;

            theunqueuedobjectparent.transform.name = "facetype: " + f;

            var meshrend = theunqueuedobjectparent.GetComponent<MeshRenderer>();
            var meshfilt = theunqueuedobjectparent.GetComponent<MeshFilter>();

            meshrend = null;
            meshfilt = null;



            for (int x = -sizelx; x <= sizelx + sizerx; x++)
            {
                for (int y = -sizeby; y <= sizeby + sizety; y++)
                {
                    for (int z = -sizebz; z <= sizebz + sizefz; z++)
                    {
                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = sizerx + xx;
                        }
                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = sizety + yy;
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = sizefz + zz;
                        }

                        int theindex = xx + (sizelx + sizerx + 1) * (yy + (sizeby + sizety + 1) * zz);

                        float posx = x;
                        float posy = y;
                        float posz = z;

                        Vector3 chunkposition = new Vector3(posx, posy, posz) + parentposchunk;

                        /*chunk thechunk = new chunk();
                        int[] newmap = new int[10*10*10];
                        var meshdata = thechunk.startBuildingArray(chunkposition,xx,yy,zz, newmap);
                        */

                        /*byte[] newmap = new byte[10 * 10 * 10];
                        chunknocompute chunky = new chunknocompute();
                        universenocompute.meshData meshdata = chunky.startBuildingArray(chunkposition, xx, yy, zz, newmap);*/


                        //for (int f = 0; f < 6; f++)
                        {
                            chunkDatascterrainrev2 _chunkData;
                            var _currentChunk = new chunkscterrainrev2(chunkposition, out _chunkData, 10, 10, 10, facetype);

                            GameObject theunqueuedobject = NewObjectPoolerScript.current.GetPooledObject();
                            theunqueuedobject.transform.position = chunkposition;//new Vector3(posx, posy, posz);
                            theunqueuedobject.transform.parent = theunqueuedobjectparent.transform;// this.transform;

                            Mesh mesh = new Mesh();// theunqueuedobject.GetComponent<MeshFilter>().mesh;
                            mesh.Clear();

                            theunqueuedobject.GetComponent<MeshFilter>().mesh = mesh;

                            mesh.vertices = _chunkData._chunkVertices.ToArray();
                            mesh.triangles = _chunkData._chunkTriangles.ToArray();
                            mesh.RecalculateNormals();

                            theunqueuedobject.SetActive(true);


                            //Debug.Log(theindex);

                            chunkarray[facetype][theindex] = new scterrainrev2.chunkdata();
                            chunkarray[facetype][theindex].thegameobject = theunqueuedobject;
                        }

                    }
                }
            }
        }




    }

    // Update is called once per frame
    void Update()
    {

    }
}
