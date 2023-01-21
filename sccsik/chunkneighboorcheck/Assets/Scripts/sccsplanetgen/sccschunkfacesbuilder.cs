using System.Collections;
using System.Collections.Generic;
using System.IO.Compression;
using UnityEngine;

public class sccschunkfacesbuilder : MonoBehaviour
{
    public static sccschunkfacesbuilder instance;

    public struct chunkdata
    {
        /*public List<List<Vector3>> vertices;
        public List<List<int>> triangles;*/

        public List<Vector3>[] vertices;
        public List<int>[] triangles;

        /*
        public Vector3[][] vertices;
        public int[][] triangles;*/
    }



    sccscomputevoxelALLFACES[][] arrayofchunkdivs;



    chunkdata[] listofchunkdata;




    public int chunkwl = 1;
    public int chunkwr = 0;

    public int chunkhl = 1;
    public int chunkhr = 0;

    public int chunkdl = 1;
    public int chunkdr = 0;
    int maxarrayssize = 0;


    // Start is called before the first frame update
    void Start()
    {
        instance = this;

        maxarrayssize = (chunkwl + chunkwr + 1) * (chunkhl + chunkhr + 1) * (chunkdl + chunkdr + 1);

        /*for (int x = -chunkwl; x <= chunkwr; x++)
        {
            for (int y = -chunkhl; y <= chunkhr; y++)
            {
                for (int z = -chunkdl; z <= chunkdr; z++)
                {
                    int xx = x;
                    int yy = y;
                    int zz = z;

                    if (xx < 0)
                    {
                        xx *= -1;
                        xx = xx + chunkwr;
                    }
                    if (yy < 0)
                    {
                        yy *= -1;
                        yy = yy + chunkhr;
                    }
                    if (zz < 0)
                    {
                        zz *= -1;
                        zz = zz + chunkdr;
                    }

                    int indexflat = xx + (chunkwl + chunkwr + 1) * (yy + (chunkhl + chunkhr + 1) * zz);
                }
            }
        }
        */

        
 



        arrayofchunkdivs = new sccscomputevoxelALLFACES[6][];

        listofchunkdata = new chunkdata[6];


        //emptyobject.GetComponent<sccscomputevoxelALLFACES>();

        for (int f = 0; f < 6; f++)
        {
           

            /*listofchunkdata[0].vertices = new Vector3[sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizex * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizey * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizez][];
            listofchunkdata[0].triangles = new int[sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizex * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizey * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizez][];
            */

            arrayofchunkdivs[f] = new sccscomputevoxelALLFACES[maxarrayssize];


            for (int x = -chunkwl; x <= chunkwr; x++)
            {
                for (int y = -chunkhl; y <= chunkhr; y++)
                {
                    for (int z = -chunkdl; z <= chunkdr; z++)
                    {
                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + chunkwr;
                        }
                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + chunkhr;
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + chunkdr;
                        }

                        int indexflat = xx + (chunkwl + chunkwr + 1) * (yy + (chunkhl + chunkhr + 1) * zz);

                        GameObject emptyobject = planetdivobjectpool.current.GetPooledObject();// this.transform.gameObject.GetComponent<planetdivobjectpool>().GetPooledObject();
                        emptyobject.SetActive(true);

                        arrayofchunkdivs[f][indexflat] = emptyobject.GetComponent<sccscomputevoxelALLFACES>();

                        var incrementsfracx = (chunkwl + chunkwr + 1) / (arrayofchunkdivs[f][indexflat].schunkwl + arrayofchunkdivs[f][indexflat].schunkwr + 1);
                        var incrementsfracy = (chunkhl + chunkhr + 1) / (arrayofchunkdivs[f][indexflat].schunkhl + arrayofchunkdivs[f][indexflat].schunkhr + 1);
                        var incrementsfracz = (chunkdl + chunkdr + 1) / (arrayofchunkdivs[f][indexflat].schunkdl + arrayofchunkdivs[f][indexflat].schunkdr + 1);


                        arrayofchunkdivs[f][indexflat].mainminx = x;
                        arrayofchunkdivs[f][indexflat].mainmaxx = x + incrementsfracx;

                        arrayofchunkdivs[f][indexflat].mainminy = y;
                        arrayofchunkdivs[f][indexflat].mainmaxy = y + incrementsfracy;

                        arrayofchunkdivs[f][indexflat].mainminz = z;
                        arrayofchunkdivs[f][indexflat].mainmaxz = z + incrementsfracz;

                        int maxsize = sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.maxarrayssize;// sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizex * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizey * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizez;

                        //Debug.Log(maxsize);

                        /*float positionx = x * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizex * 0.1f * 10;
                        float positiony = y * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizey * 0.1f * 10;
                        float positionz = z * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizez * 0.1f * 10;
                        */
                        /*float positionx = x * (sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwr + 1) * 0.1f * 10;
                        float positiony = y * (sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhr + 1) * 0.1f * 10;
                        float positionz = z * (sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdr + 1) * 0.1f * 10;
                        */
                        /*
                        float positionx = x * (chunkwl + chunkwr + 1)* (sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwr + 1) * 10 * 0.1f;
                        float positiony = y * (chunkhl + chunkhr + 1)* (sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhr + 1) * 10 * 0.1f;
                        float positionz = z * (chunkdl + chunkdr + 1)* (sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdr + 1) * 10 * 0.1f;
                        */  
                        
                        
                        /*float positionx = x * (chunkwl + chunkwr + 1) * 10 * 0.1f * (sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwr + 1);
                        float positiony = y * (chunkhl + chunkhr + 1) * 10 * 0.1f * (sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhr + 1);
                        float positionz = z * (chunkdl + chunkdr + 1) * 10 * 0.1f * (sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdr + 1);
                        */

                        float positionx = x * (chunkwl + chunkwr + 1) * 10 * 0.1f * 2;
                        float positiony = y * (chunkhl + chunkhr + 1) * 10 * 0.1f * 2;
                        float positionz = z * (chunkdl + chunkdr + 1) * 10 * 0.1f * 2;

                        
                        positionx += ((chunkwl + chunkwr + 1) / 1);
                        positiony += ((chunkhl + chunkhr + 1) / 1);
                        positionz += ((chunkdl + chunkdr + 1) / 1);


                        /*positionx += ((sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwr + 1) * 0.1f * 10);
                        positiony += ((sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhr + 1) * 0.1f * 10);
                        positionz += ((sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdr + 1) * 0.1f * 10);
                        */





                        Vector3 planetdivoriginpos = new Vector3(positionx, positiony, positionz);

                        //arrayofchunkdivs[f][indexflat] = new sccscomputevoxelALLFACES();
                        /*int sizeofarray = arrayofchunkdivs[f].levelsizex * arrayofchunkdivs[f].levelsizey * arrayofchunkdivs[f].levelsizez;
                        listofchunkdata[f].vertices = new List<Vector3>[sizeofarray];
                        listofchunkdata[f].triangles = new List<int>[sizeofarray];*/

                        arrayofchunkdivs[f][indexflat].CreateTheShaders(f);
                        arrayofchunkdivs[f][indexflat].CreateTheArrays(f, planetdivoriginpos,x,y,z);

                        arrayofchunkdivs[f][indexflat].CreateTheMaps(f);


                        arrayofchunkdivs[f][indexflat].mindexx = x;
                        arrayofchunkdivs[f][indexflat].mindexy = y;
                        arrayofchunkdivs[f][indexflat].mindexz = z;


                        if (x == -chunkwl)
                        {
                            arrayofchunkdivs[f][indexflat].extremityiswl = 1;
                        }

                        if (x == chunkwr)
                        {
                            arrayofchunkdivs[f][indexflat].extremityiswr = 1;
                        }

                        if (y == -chunkhl)
                        {
                            arrayofchunkdivs[f][indexflat].extremityishl = 1;
                        }
                        if (y == chunkhr)
                        {
                            arrayofchunkdivs[f][indexflat].extremityishr = 1;
                        }

                        if (z == -chunkdl)
                        {
                            arrayofchunkdivs[f][indexflat].extremityisdl = 1;
                        }
                        if (z == chunkdr)
                        {
                            arrayofchunkdivs[f][indexflat].extremityisdr = 1;
                        }




                        /*
                        arrayofchunkdivs[f][indexflat].ComputeTheVertexes();
                        arrayofchunkdivs[f][indexflat].CreateTheVerticesAndTriangles(f, out listofchunkdata[f].vertices, out listofchunkdata[f].triangles);
                        arrayofchunkdivs[f][indexflat].CreateTheMesh(f, listofchunkdata[f].vertices, listofchunkdata[f].triangles, planetdivoriginpos);


                        arrayofchunkdivs[f][indexflat].mindexx = x;
                        arrayofchunkdivs[f][indexflat].mindexy = y;
                        arrayofchunkdivs[f][indexflat].mindexz = z;


                        if (x == chunkwl)
                        {
                            arrayofchunkdivs[f][indexflat].extremityiswl = 1;
                        }

                        if (x == chunkwr + 1)
                        {
                            arrayofchunkdivs[f][indexflat].extremityiswr = 1;
                        }

                        if (y == chunkhl + 1)
                        {
                            arrayofchunkdivs[f][indexflat].extremityishl = 1;
                        }
                        if (y == chunkhr + 1)
                        {
                            arrayofchunkdivs[f][indexflat].extremityishr = 1;
                        }

                        if (z == chunkdl + 1)
                        {
                            arrayofchunkdivs[f][indexflat].extremityisdl = 1;
                        }
                        if (z == chunkdr + 1)
                        {
                            arrayofchunkdivs[f][indexflat].extremityisdr = 1;
                        }







                        var script = arrayofchunkdivs[f][indexflat];

                        //if (f == 0 && x == 0 && y == 0 && z == 0)
                        //{
                        //    this.transform.position = new Vector3(script.levelsizex * script.mapx * script.planesize * 0.5f, script.levelsizey * script.mapy * script.planesize * 0.5f, script.levelsizez * script.mapz * script.planesize * 0.5f);
                        //}

                        if (f == 0 && x == 0 && y == 0 && z == 0)
                        {
                            //this.transform.position = Vector3.zero;// new Vector3(script.levelsizex * script.mapx * script.planesize * 0.5f, script.levelsizey * script.mapy * script.planesize * 0.5f, script.levelsizez * script.mapz * script.planesize * 0.5f) + planetdivoriginpos;
                        }*/

                        emptyobject.transform.parent = this.transform;

                        //emptyobject.transform.position = planetcoreposition;//planetcoreposition;//
                    }
                }
            }
        }




        for (int f = 0; f < 6; f++)
        {


            /*listofchunkdata[0].vertices = new Vector3[sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizex * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizey * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizez][];
            listofchunkdata[0].triangles = new int[sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizex * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizey * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizez][];
            */

            for (int x = -chunkwl; x <= chunkwr; x++)
            {
                for (int y = -chunkhl; y <= chunkhr; y++)
                {
                    for (int z = -chunkdl; z <= chunkdr; z++)
                    {
                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + chunkwr;
                        }
                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + chunkhr;
                        }
                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + chunkdr;
                        }

                        int indexflat = xx + (chunkwl + chunkwr + 1) * (yy + (chunkhl + chunkhr + 1) * zz);

                        float positionx = x * (chunkwl + chunkwr + 1) * 10 * 0.1f * 2;
                        float positiony = y * (chunkhl + chunkhr + 1) * 10 * 0.1f * 2;
                        float positionz = z * (chunkdl + chunkdr + 1) * 10 * 0.1f * 2;


                        positionx += ((chunkwl + chunkwr + 1) / 1);
                        positiony += ((chunkhl + chunkhr + 1) / 1);
                        positionz += ((chunkdl + chunkdr + 1) / 1);


                        /*positionx += ((sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkwr + 1) * 0.1f * 10);
                        positiony += ((sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkhr + 1) * 0.1f * 10);
                        positionz += ((sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdl + sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.schunkdr + 1) * 0.1f * 10);
                        */

                        Vector3 planetdivoriginpos = new Vector3(positionx, positiony, positionz);

                        
                        arrayofchunkdivs[f][indexflat].ComputeTheVertexes();
                        arrayofchunkdivs[f][indexflat].CreateTheVerticesAndTriangles(f, out listofchunkdata[f].vertices, out listofchunkdata[f].triangles);
                        arrayofchunkdivs[f][indexflat].CreateTheMesh(f, listofchunkdata[f].vertices, listofchunkdata[f].triangles, planetdivoriginpos);
                        

                       




                        var script = arrayofchunkdivs[f][indexflat];

                        /*if (f == 0 && x == 0 && y == 0 && z == 0)
                        {
                            this.transform.position = new Vector3(script.levelsizex * script.mapx * script.planesize * 0.5f, script.levelsizey * script.mapy * script.planesize * 0.5f, script.levelsizez * script.mapz * script.planesize * 0.5f);
                        }*/

                        if (f == 0 && x == 0 && y == 0 && z == 0)
                        {
                            //this.transform.position = Vector3.zero;// new Vector3(script.levelsizex * script.mapx * script.planesize * 0.5f, script.levelsizey * script.mapy * script.planesize * 0.5f, script.levelsizez * script.mapz * script.planesize * 0.5f) + planetdivoriginpos;
                        }

                    }
                }
            }
        }









    }


    public sccscomputevoxelALLFACES getplanetdiv(int x, int y, int z)
    {
        if ((x < -chunkwl) || (y < -chunkhl) || (z < -chunkdl) || (x >= chunkwr + 1) || (y >= (chunkhr + 1)) || (z >= (chunkdr + 1)))
        {
            return null;
        }

        if (x < 0)
        {
            x *= -1;
            x = (chunkwr) + x;
        }
        if (y < 0)
        {
            y *= -1;
            y = (chunkhr) + y;
        }
        if (z < 0)
        {
            z *= -1;
            z = (chunkdr) + z;
        }

        int _index = x + (chunkwl + chunkwr + 1) * (y + (chunkhl + chunkhr + 1) * z);

        return arrayofchunkdivs[0][_index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
