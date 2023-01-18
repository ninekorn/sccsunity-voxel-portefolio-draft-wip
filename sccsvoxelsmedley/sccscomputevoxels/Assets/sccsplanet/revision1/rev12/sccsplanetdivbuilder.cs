using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccsplanetdivbuilder : MonoBehaviour
{
    public sccsproceduralplanetbuilderrev12[] arrayofplanetdiv;

    //public GameObject theplanetdivtospawn;


    public static sccsplanetdivbuilder currentsccsplanetbuilder;

    public int chunkwleft = 1;
    public int chunkwright = 0;
    public int chunkhleft = 1;
    public int chunkhright = 0;
    public int chunkdleft = 1;
    public int chunkdright = 0;

    int max = 0;
    // Start is called before the first frame update

    private void Awake()
    {
        currentsccsplanetbuilder = this;
    }
    
    void Start()
    {
        max = (chunkwleft + chunkwright + 1) * (chunkhleft + chunkhright + 1) * (chunkdleft + chunkdright + 1);


        arrayofplanetdiv = new sccsproceduralplanetbuilderrev12[max];

        for (int x = -chunkwleft; x <= chunkwright;x++)
        {
            for (int y = -chunkhleft; y <= chunkhright; y++)
            {
                for (int z = -chunkdleft; z <= chunkdright; z++)
                {
                    int xx = x;
                    int yy = y;
                    int zz = z;

                    if (xx < 0)
                    {
                        xx *= -1;
                        xx = xx + chunkwright;
                    }

                    if (yy < 0)
                    {
                        yy *= -1;
                        yy = yy + chunkhright;
                    }


                    if (zz < 0)
                    {
                        zz *= -1;
                        zz = zz + chunkdright;
                    }

                    int theindex = xx + (chunkwleft + chunkwright + 1) * (yy + (chunkhleft + chunkhright + 1) * zz);

                    


                    GameObject objectfrompool = this.transform.gameObject.GetComponent<planetdivobjectpool>().GetPooledObject();
                    objectfrompool.SetActive(true);

                    //objectfrompool.AddComponent<sccsplanetchunkrev12>()
                    //var sccsplanetchunkrev12script = new sccsplanetchunkrev12();

                    //sccsproceduralplanetbuilderrev12 sccsplanetchunkrev12script = objectfrompool.GetComponent<sccsproceduralplanetbuilderrev12>();

                    //arrayofplanetdiv[theindex] = new sccsproceduralplanetbuilderrev12();

                    arrayofplanetdiv[theindex] = objectfrompool.GetComponent<sccsproceduralplanetbuilderrev12>();

                    arrayofplanetdiv[theindex].mindexposx = x;
                    arrayofplanetdiv[theindex].mindexposy = y;
                    arrayofplanetdiv[theindex].mindexposz = z;

                    arrayofplanetdiv[theindex].transform.position = new Vector3(x * 256 * 0.1f, y * 256 * 0.1f, z * 256 * 0.1f);

                    arrayofplanetdiv[theindex].ChunkWidth_L = 2;
                    arrayofplanetdiv[theindex].ChunkWidth_R = 1;

                    arrayofplanetdiv[theindex].ChunkHeight_L = 2;
                    arrayofplanetdiv[theindex].ChunkHeight_R = 1;

                    arrayofplanetdiv[theindex].ChunkDepth_L = 2;
                    arrayofplanetdiv[theindex].ChunkDepth_R = 1;

                    arrayofplanetdiv[theindex].width = 64;
                    arrayofplanetdiv[theindex].height = 64;
                    arrayofplanetdiv[theindex].depth = 64;

                    arrayofplanetdiv[theindex].iterateloopmap = 1;
                    arrayofplanetdiv[theindex].iterateloopmesh = 1;
                    arrayofplanetdiv[theindex].iterateonthreadloop = 1;

                    arrayofplanetdiv[theindex].queueofmapdatacallbackcounter = 0;
                    arrayofplanetdiv[theindex].queueofmapdatacallbackcounterswtc = 0;

                    arrayofplanetdiv[theindex].queueofmapdatacallbacktwocounter = 0;
                    arrayofplanetdiv[theindex].queueofmapdatacallbacktwocounterswtc = 0;

                    arrayofplanetdiv[theindex].queueofmeshdatacallbackcounter = 0;
                    arrayofplanetdiv[theindex].queueofmeshdatacallbackcounterswtc = 0;

                    arrayofplanetdiv[theindex].queueofmeshdatacallbacktwocounter = 0;

                    arrayofplanetdiv[theindex].StartScript();


                }
            }
        }

        startswtc = 1;
    }

    int startswtc = 0;


    int counterofplanetdivbuiltchunkmaps = 0;
    int counterofplanetdivbuiltchunkmapstwo = 0;

    // Update is called once per frame
    void Update()
    {
        //check if all divs have their bytemaps built

        if (startswtc == 1)
        {

            for (int x = -chunkwleft; x <= chunkwright; x++)
            {
                for (int y = -chunkhleft; y <= chunkhright; y++)
                {
                    for (int z = -chunkdleft; z <= chunkdright; z++)
                    {
                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + chunkwright;
                        }

                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + chunkhright;
                        }


                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + chunkdright;
                        }

                        int theindex = xx + (chunkwleft + chunkwright + 1) * (yy + (chunkhleft + chunkhright + 1) * zz);

                        if (arrayofplanetdiv[theindex].forwaitplanetchunkshavebuiltswtcmain == 0 && arrayofplanetdiv[theindex].queueofmapdatacallbacktwocounterswtc == 1)
                        {
                            counterofplanetdivbuiltchunkmaps++;
                            arrayofplanetdiv[theindex].forwaitplanetchunkshavebuiltswtcmain = 1;
                        }
                    }
                }
            }

            if (counterofplanetdivbuiltchunkmaps >= max)
            {
                startswtc = 2;
            }
        }

        if (startswtc == 2)
        {

            for (int x = -chunkwleft; x <= chunkwright; x++)
            {
                for (int y = -chunkhleft; y <= chunkhright; y++)
                {
                    for (int z = -chunkdleft; z <= chunkdright; z++)
                    {
                        int xx = x;
                        int yy = y;
                        int zz = z;

                        if (xx < 0)
                        {
                            xx *= -1;
                            xx = xx + chunkwright;
                        }

                        if (yy < 0)
                        {
                            yy *= -1;
                            yy = yy + chunkhright;
                        }


                        if (zz < 0)
                        {
                            zz *= -1;
                            zz = zz + chunkdright;
                        }

                        int theindex = xx + (chunkwleft + chunkwright + 1) * (yy + (chunkhleft + chunkhright + 1) * zz);

                        if (arrayofplanetdiv[theindex].forwaitplanetchunkshavebuiltswtc == 0 && arrayofplanetdiv[theindex].queueofmapdatacallbacktwocounterswtc == 1)
                        {
                            counterofplanetdivbuiltchunkmapstwo++;
                            arrayofplanetdiv[theindex].forwaitplanetchunkshavebuiltswtc = 1;
                        }
                    }
                }
            }

            if (counterofplanetdivbuiltchunkmapstwo >= max)
            {
                startswtc = 3;
            }
        }




        if (theplayer == null)
        {
            theplayer = GameObject.FindGameObjectWithTag("Player");

            theplayer.GetComponent<sccsplayer>().theplanet = this.gameObject;


        }






    }



    public GameObject theplayer; 









    public sccsproceduralplanetbuilderrev12 getplanetdiv(int x, int y, int z)
    {
        if ((x < -chunkwleft) || (y < -chunkhleft) || (z < -chunkdleft) || (x >= chunkwright + 1) || (y >= (chunkhright + 1)) || (z >= (chunkdright + 1)))
        {
            return null;
        }

        if (x < 0)
        {
            x *= -1;
            x = (chunkwright) + x;
        }
        if (y < 0)
        {
            y *= -1;
            y = (chunkhright) + y;
        }
        if (z < 0)
        {
            z *= -1;
            z = (chunkdright) + z;
        }

        int _index = x + (chunkwleft + chunkwright + 1) * (y + (chunkhleft + chunkhright + 1) * z);

        return arrayofplanetdiv[_index];
    }

}
