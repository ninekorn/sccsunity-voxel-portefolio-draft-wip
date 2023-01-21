using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccschunkfacesbuilder : MonoBehaviour
{

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

        GameObject emptyobject = planetdivobjectpool.current.GetPooledObject();// this.transform.gameObject.GetComponent<planetdivobjectpool>().GetPooledObject();
        emptyobject.SetActive(true);

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

                        arrayofchunkdivs[f][indexflat] = emptyobject.GetComponent<sccscomputevoxelALLFACES>();

                        int maxsize = sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizex * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizey * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizez;

                        float positionx = x * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizex * 0.1f * 10;
                        float positiony = y * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizey * 0.1f * 10;
                        float positionz = z * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizez * 0.1f * 10;

                        Vector3 planetdivoriginpos = new Vector3(positionx, positiony, positionz);

                        //arrayofchunkdivs[f][indexflat] = new sccscomputevoxelALLFACES();
                        /*int sizeofarray = arrayofchunkdivs[f].levelsizex * arrayofchunkdivs[f].levelsizey * arrayofchunkdivs[f].levelsizez;
                        listofchunkdata[f].vertices = new List<Vector3>[sizeofarray];
                        listofchunkdata[f].triangles = new List<int>[sizeofarray];*/

                        arrayofchunkdivs[f][indexflat].CreateTheShaders(f);
                        arrayofchunkdivs[f][indexflat].CreateTheArrays(f, planetdivoriginpos);

                        arrayofchunkdivs[f][indexflat].CreateTheMaps(f);



                        arrayofchunkdivs[f][indexflat].ComputeTheVertexes();
                        arrayofchunkdivs[f][indexflat].CreateTheVerticesAndTriangles(f, out listofchunkdata[f].vertices, out listofchunkdata[f].triangles);
                        arrayofchunkdivs[f][indexflat].CreateTheMesh(f, listofchunkdata[f].vertices, listofchunkdata[f].triangles, planetdivoriginpos);

                        var script = arrayofchunkdivs[f][indexflat];

                        if (f == 0 && x == 0 && y == 0 && z == 0)
                        {
                            this.transform.position = new Vector3(script.levelsizex * script.mapx * script.planesize * 0.5f, script.levelsizey * script.mapy * script.planesize * 0.5f, script.levelsizez * script.mapz * script.planesize * 0.5f);
                        }



                        //emptyobject.transform.position = planetcoreposition;//planetcoreposition;//
                    }
                }


            }
        }

        emptyobject.transform.parent = this.transform;

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
