using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SimplexNoise;
using System.Runtime.Remoting.Messaging;
/*using CoherentNoise;
using CoherentNoise.Generation;
using CoherentNoise.Generation.Displacement;
using CoherentNoise.Generation.Fractal;
using CoherentNoise.Generation.Modification;
using CoherentNoise.Generation.Patterns;
using CoherentNoise.Texturing;*/

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
//[RequireComponent(typeof(MeshCollider))]

public class sccsplanetchunkrev5// : MonoBehaviour
{

    private float radiusplanetcorestart = 0.0f;
    private float radiusplanetcoreend = 5.0f;
    private float radiusplanetcavesstart = 5.0f;
    private float radiusplanetcavesend = 9.0f;
    private float radiusplanetcruststart = 9.0f;
    private float radiusplanetcrustend = 11.0f;
    private float radiusplanetmountainstart = 11.0f;
    private float radiusplanetmountainend = 20.0f;


    public int width = 16;
    public int height = 16;
    public int depth = 16;

    //public byte[] map;
    //public byte[] map;
    public Mesh mesh;
    //public List<Vector3> verts = new List<Vector3>();
    //public List<int> tris = new List<int>();
    public List<Vector2> uv = new List<Vector2>();
    public MeshCollider meshCollider;
    public float planeSize = 0.25f;

    //public Transform sphere;
    float seed;
    byte block;

    float nodeDiameter;
    float chunkRadius;
    float fraction;
    float chunkSize;

    int divider = 10;
    //public Transform cube;
    float noiseValue0;


    public float detailScale = 5;
    public float heightScale = 5;
    public int heightScale1 = 5;
    public int detailScale1 = 5;

    float whatever1 = 10;
    float whatever2 = 10;

    //sccsproceduralplanetbuilderrev5 componentParent;
    //Transform parentObject;


    //GameObject thegameobject;

    public Vector3 chunkpos;

    public sccsproceduralplanetbuilderrev5.mainChunk buildchunkmap(sccsproceduralplanetbuilderrev5.mainChunk mainChunk)
    {

        //thegameobject = mainChunk.planetchunk;

        chunkpos = mainChunk.worldPosition;


        //this.gameObject.tag = "collisionObject";

        //this.gameObject.layer = 8; //"collisionObject"

        //parentObject = this.thegameobject.parent;

        //componentParent = parentObject.gameObject.GetComponent<sccsproceduralplanetbuilderrev5>();

        //noise = new PerlinNoise(Random.Range(1000000, 10000000));

        nodeDiameter = planeSize;
        chunkRadius = planeSize / 2;
        fraction = (int)(1 / (planeSize));
        chunkSize = 1f;

        //meshCollider = GetComponent<MeshCollider>();
        //thegameobject.localScale *= planeSize;
        
        //map = new byte[width*height*depth];
        seed = 3420;
        //seed = Random.Range(3000, 4000);

        //seed = 0;
        //checkBytePos();
        int radius = 5;
        Vector3 center = Vector3.zero;

        //if (chunkpos.y >= 3)
        //{
        //mainChunk.bytemap = new byte[width*height* depth];


        float offsetDist = 0;

        Vector3 position1 = mainChunk.parentPosition;
        float distance1 = Vector3.Distance(position1, center);

        if (chunkpos.x < 0 || chunkpos.y < 0 || chunkpos.z < 0)
        {
            offsetDist = distance1;
        }



        /*mesh = new Mesh();
        this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
        this.gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
        */

        for (int x = 0; x < width; x++)
        {
            float noiseX = Mathf.Abs(((float)(x * planeSize + chunkpos.x + seed) / detailScale) * heightScale);

            for (int y = 0; y < height; y++)
            {
                float noiseY = Mathf.Abs(((float)(y * planeSize + chunkpos.y + seed) / detailScale) * heightScale);

                for (int z = 0; z < depth; z++)
                {
                    float noiseZ = Mathf.Abs(((float)(z * planeSize + chunkpos.z + seed) / detailScale) * heightScale);

                    float posX = x * planeSize + chunkpos.x;
                    float posY = y * planeSize + chunkpos.y;
                    float posZ = z * planeSize + chunkpos.z;

                    Vector3 position = new Vector3(posX, posY, posZ);

                    float distance = Vector3.Distance(position, center);

                    int indexOf = x + width * (y + depth * z);

                    /*float temporaryY = 0.1f;
                    float temporaryZ = 0.1f;
                    float temporaryX = 0.1f;

                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);
                    float size0 = (1 / planeSize) * chunkpos.y;
                    temporaryY -= size0;


                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);
                    float size1 = (1 / planeSize) * chunkpos.x;
                    temporaryX -= size1;

                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);
                    float size2 = (1 / planeSize) * chunkpos.z;
                    temporaryZ -= size2;


                    if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
                    {
                        map[x, y, z] = 1;
                    }*/
                    //map[x, y, z] = 1;

                    /*float temporaryY = 1f;
                    float temporaryZ = 0.1f;
                    float temporaryX = 0.1f;


                    temporaryY *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

                    float size0 = (1 / planeSize) * chunkpos.y;
                    temporaryY -= size0;


                    temporaryX *= (Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

                    float size1 = (1 / planeSize) * chunkpos.x;
                    temporaryX -= size1;

                    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);

                    float size2 = (1 / planeSize) * chunkpos.z;
                    temporaryZ -= size2;*/


                    /*if ((int)Mathf.Round(temporaryY) >= y )
                    {
                        map[x, y, z] = 1;
                    }*/

                    /*if ((int)Mathf.Round(temporaryY) >= 0)
                    {
                        map[x, y, z] = 1;
                    }*/



                    //if (distance1 >= 0 && distance1 < 19 )
                    {
                        if (distance <= radiusplanetcoreend)
                        {
                            mainChunk.bytemap[indexOf] = 1;
                        }

                        else if (distance > radiusplanetcoreend && distance <= radiusplanetcavesend)
                        {
                            float noiseValue0 = Noise.Generate(noiseX, noiseY, noiseZ);
                            if (noiseValue0 > 0.2f)
                            {
                                mainChunk.bytemap[indexOf] = 1;
                            }
                        }

                        else if (distance >= radiusplanetcavesend && distance <= radiusplanetcrustend)
                        {
                            mainChunk.bytemap[indexOf] = 1;
                        }

                        else if (distance > radiusplanetcrustend && distance < radiusplanetmountainend + offsetDist)
                        {


                            float temporaryY = 10;
                            float temporaryZ = 10;
                            float temporaryX = 10;

                            if (chunkpos.y < 0 && chunkpos.x < 0 && chunkpos.z < 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);
                                float size0 = (1 / planeSize) * chunkpos.y;
                                temporaryY -= size0;

                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);
                                float size1 = (1 / planeSize) * chunkpos.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);
                                float size2 = (1 / planeSize) * chunkpos.z;
                                temporaryZ -= size2;

                                if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    mainChunk.bytemap[indexOf] = 1;
                                }
                            }

                            else if (chunkpos.y >= 0 && chunkpos.x >= 0 && chunkpos.z >= 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * chunkpos.y;
                                temporaryY -= size0;


                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * chunkpos.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * chunkpos.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
                                {
                                    mainChunk.bytemap[indexOf] = 1;
                                }
                            }

                            else if (chunkpos.y >= 0 && chunkpos.x < 0 && chunkpos.z >= 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * chunkpos.y;
                                temporaryY -= size0;

                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * chunkpos.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * chunkpos.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) >= z)
                                {
                                    mainChunk.bytemap[indexOf] = 1;
                                }
                            }


                            else if (chunkpos.y >= 0 && chunkpos.x >= 0 && chunkpos.z < 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * chunkpos.y;
                                temporaryY -= size0;


                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * chunkpos.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * chunkpos.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    mainChunk.bytemap[indexOf] = 1;
                                }
                            }





                            else if (chunkpos.y >= 0 && chunkpos.x < 0 && chunkpos.z < 0)
                            {
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * chunkpos.y;
                                temporaryY -= size0;


                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * chunkpos.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * chunkpos.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    mainChunk.bytemap[indexOf] = 1;
                                }
                            }



                            else if (chunkpos.y < 0 && chunkpos.x >= 0 && chunkpos.z < 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * chunkpos.y;
                                temporaryY -= size0;


                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * chunkpos.x;
                                temporaryX -= size1;

                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * chunkpos.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) < z)
                                {
                                    mainChunk.bytemap[indexOf] = 1;
                                }
                            }





                            else if (chunkpos.y < 0 && chunkpos.x >= 0 && chunkpos.z >= 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);
                                float size0 = (1 / planeSize) * chunkpos.y;
                                temporaryY -= size0;

                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);
                                float size1 = (1 / planeSize) * chunkpos.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);
                                float size2 = (1 / planeSize) * chunkpos.z;
                                temporaryZ -= size2;


                                if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
                                {
                                    mainChunk.bytemap[indexOf] = 1;
                                }
                            }





                            else if (chunkpos.y < 0 && chunkpos.x < 0 && chunkpos.z >= 0)
                            {
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

                                float size0 = (1 / planeSize) * chunkpos.y;
                                temporaryY -= size0;


                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

                                float size1 = (1 / planeSize) * chunkpos.x;
                                temporaryX -= size1;

                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);

                                float size2 = (1 / planeSize) * chunkpos.z;
                                temporaryZ -= size2;

                                if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) >= z)
                                {
                                    mainChunk.bytemap[indexOf] = 1;
                                }
                            }
                            else
                            {
                                mainChunk.bytemap[indexOf] = 0;

                            }


                            ////(chunkpos.y < 0 && chunkpos.x < 0 && chunkpos.z < 0)
                            ////chunkpos.y >= 0 && chunkpos.x >= 0 && chunkpos.z >= 0)
                            ////chunkpos.y >= 0 && chunkpos.x < 0 && chunkpos.z >= 0)
                            ////(chunkpos.y >= 0 && chunkpos.x >= 0 && chunkpos.z < 0)
                            ////(chunkpos.y >= 0 && chunkpos.x < 0 && chunkpos.z < 0)
                            ////(chunkpos.y < 0 && chunkpos.x >= 0 && chunkpos.z < 0)
                            ////(chunkpos.y < 0 && chunkpos.x >= 0 && chunkpos.z >= 0)
                            ////(chunkpos.y < 0 && chunkpos.x < 0 && chunkpos.z >= 0)














                            /*if (chunkpos.y < 0)
                            {
                                float size0 = (1 / planeSize) * chunkpos.y;
                                temporaryY -= size0;
                                temporaryY *= -(Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);
                                if ((int)Mathf.Round(temporaryY) <= y)
                                {
                                    mainChunk.bytemap[x, y, z] = 1;
                                }
                            }
                            else
                            {
                                float size0 = (1 / planeSize) * chunkpos.y;
                                temporaryY -= size0;
                                temporaryY *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);
                                if ((int)Mathf.Round(temporaryY) >= y )
                                {
                                    mainChunk.bytemap[x, y, z] = 1;
                                }
                            }

                            if (chunkpos.x < 0)
                            {
                                float size1 = (1 / planeSize) * chunkpos.x;
                                temporaryX -= size1;
                                temporaryX *= -(Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);
                                if ((int)Mathf.Round(temporaryX) <= x)
                                {
                                    mainChunk.bytemap[x, y, z] = 1;
                                }
                            }
                            else
                            {
                                float size1 = (1 / planeSize) * chunkpos.x;
                                temporaryX -= size1;
                                temporaryX *= (Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);
                                if ((int)Mathf.Round(temporaryX) <= x)
                                {
                                    mainChunk.bytemap[x, y, z] = 1;
                                }
                            }



                            if (chunkpos.z < 0)
                            {
                                float size2 = (1 / planeSize) * chunkpos.z;
                                temporaryZ -= size2;
                                temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);
                                if ((int)Mathf.Round(temporaryZ) <= z)
                                {
                                    mainChunk.bytemap[x, y, z] = 1;
                                }
                            }
                            else
                            {
                                float size2 = (1 / planeSize) * chunkpos.z;
                                temporaryZ -= size2;
                                temporaryZ *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);
                                if ((int)Mathf.Round(temporaryZ) >= z)
                                {
                                    mainChunk.bytemap[x, y, z] = 1;
                                }
                            }*/

                       }
                   }
                }
            }
        }

        /*mesh = new Mesh();
       this.gameObject.GetComponent<MeshFilter>().mesh = mesh;
       this.gameObject.GetComponent<MeshFilter>().sharedMesh = mesh;
       */


        //GetComponent<sccsplanetchunkrev5>().enabled = false;

        return mainChunk;
    }




    public sccsproceduralplanetbuilderrev5.mainChunk buildMesh(sccsproceduralplanetbuilderrev5.mainChunk mainChunk)
    {
        mesh = new Mesh();
        mainChunk.planetchunk.gameObject.GetComponent<MeshFilter>().mesh = mesh;
        mainChunk.planetchunk.GetComponent<MeshFilter>().sharedMesh = mesh;


        mainChunk.planetchunk.GetComponent<MeshFilter>().mesh.Clear();
        mainChunk.planetchunk.GetComponent<MeshFilter>().mesh.vertices = mainChunk.verts.ToArray();
        mainChunk.planetchunk.GetComponent<MeshFilter>().mesh.triangles = mainChunk.tris.ToArray();
        //meshCollider.sharedMesh = null;
        //meshCollider.sharedMesh = mesh;
        mainChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateBounds();
        mainChunk.planetchunk.GetComponent<MeshFilter>().mesh.RecalculateNormals();

        //to readd
        //to readd
        //to readd
        /*if (this.gameObject.GetComponent<MeshCollider>() == null)
        {
            this.gameObject.AddComponent<MeshCollider>();
        }
        else
        {
            Destroy(this.gameObject.GetComponent<MeshCollider>());
            this.gameObject.AddComponent<MeshCollider>();

        }*/
        //to readd
        //to readd
        //to readd
        return mainChunk;
    }



    //sccsproceduralplanetbuilderrev5 sccsproceduralplanetbuilderrev5script,
    public sccsproceduralplanetbuilderrev5.mainChunk Regenerate(sccsproceduralplanetbuilderrev5.mainChunk mainChunk)
    {
        mainChunk.verts.Clear();
        mainChunk.tris.Clear();

        //verts = new List<Vector3>();
        //tris = new List<int>();




        /*if (this.gameObject.GetComponent<MeshFilter>()!= null)
        {
            Destroy(this.gameObject.GetComponent<MeshFilter>());
        }

        if (this.gameObject.GetComponent<MeshRenderer>() != null)
        {
            Destroy(this.gameObject.GetComponent<MeshRenderer>());
        }

        if (this.gameObject.GetComponent<MeshFilter>() == null)
        {
            this.gameObject.AddComponent<MeshFilter>();
        }

        if (this.gameObject.GetComponent<MeshRenderer>() == null)
        {
            this.gameObject.AddComponent<MeshRenderer>();
        }*/
        //originalMesh.vertices = modifiedVertices; //7
        //originalMesh.RecalculateNormals();



        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    //block = mainChunk.bytemap[x + width * (y + depth * z)];
                    int indexOf = x + width * (y + depth * z);
                    block = mainChunk.bytemap[indexOf];

                    if (block == 0) continue;
                    {
                        mainChunk = DrawBrick(x, y, z, mainChunk);
                    }
                    //Instantiate(sphere, new Vector3(x*planeSize, y * planeSize, z * planeSize) +chunkpos, Quaternion.identity);
                }
            }
        }


        return mainChunk;
    }

    //float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
    //float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
    //float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;
    //Instantiate(sphere, new Vector3(xx, yy, zz) + terrain.getChunkPos(chuk.chunkpos.x, chuk.chunkpos.y, chuk.chunkpos.z).chunkpos, Quaternion.identity);

    public sccsproceduralplanetbuilderrev5.mainChunk DrawBrick(int x, int y, int z, sccsproceduralplanetbuilderrev5.mainChunk mainChunk)
    {
        //Debug.Log(map[x,y,z]);

        Vector3 start = new Vector3(x * planeSize, y * planeSize, z * planeSize);
        Vector3 offset1, offset2;


        /*
        //TOPFACE
        if (IsTransparent(x, y + 1, z))
        {
            offset1 = Vector3.forward * planeSize;
            offset2 = Vector3.right * planeSize;
            DrawFace(start + Vector3.up * planeSize, offset1, offset2);
        }

        //LEFTFACE
        if (IsTransparent(x - 1, y, z))
        {
            offset1 = Vector3.back * planeSize;
            offset2 = Vector3.down * planeSize;
            DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2);
        }

        //RIGHTFACE
        if (IsTransparent(x + 1, y, z))
        {
            offset1 = Vector3.up * planeSize;
            offset2 = Vector3.forward * planeSize;
            DrawFace(start + Vector3.right * planeSize, offset1, offset2);
        }
        //FRONTFACE
        if (IsTransparent(x, y, z - 1))
        {
            offset1 = Vector3.left * planeSize;
            offset2 = Vector3.up * planeSize;
            DrawFace(start + Vector3.right * planeSize, offset1, offset2);
        }
        //BACKFACE
        if (IsTransparent(x, y, z + 1))
        {
            offset1 = Vector3.right * planeSize;
            offset2 = Vector3.up * planeSize;
            DrawFace(start + Vector3.forward * planeSize, offset1, offset2);
        }
        //BOTTOMFACE
        if (IsTransparent(x, y - 1, z))
        {
            offset1 = Vector3.right * planeSize;
            offset2 = Vector3.forward * planeSize;
            DrawFace(start, offset1, offset2);
        }*/














        /*
        //RIGHTFACE
        if (IsTransparent(x + 1, y, z, mainChunk))
        {
            offset1 = Vector3.up * planeSize;
            offset2 = Vector3.forward * planeSize;
            mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
        }
        //LEFTFACE
        if (IsTransparent(x - 1, y, z, mainChunk))
        {
            offset1 = Vector3.back * planeSize;
            offset2 = Vector3.down * planeSize;
            mainChunk = DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2, mainChunk);
        }
        //FRONTFACE
        if (IsTransparent(x, y, z - 1, mainChunk))
        {
            offset1 = Vector3.left * planeSize;
            offset2 = Vector3.up * planeSize;
            mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
        }
        //BACKFACE
        if (IsTransparent(x, y, z + 1, mainChunk))
        {
            offset1 = Vector3.right * planeSize;
            offset2 = Vector3.up * planeSize;
            mainChunk = DrawFace(start + Vector3.forward * planeSize, offset1, offset2, mainChunk);
        }
        //TOPFACE
        if (IsTransparent(x, y + 1, z, mainChunk))
        {
            offset1 = Vector3.forward * planeSize;
            offset2 = Vector3.right * planeSize;
            mainChunk = DrawFace(start + Vector3.up * planeSize, offset1, offset2, mainChunk);
        }
        //BOTTOMFACE
        if (IsTransparent(x, y - 1, z, mainChunk))
        {
            offset1 = Vector3.right * planeSize;
            offset2 = Vector3.forward * planeSize;
            mainChunk = DrawFace(start, offset1, offset2, mainChunk);
        }*/

        
        //RIGHTFACE
        if (x != width - 1)
        {
            //RIGHTFACE
            if (IsTransparent(x + 1, y, z,mainChunk))
            {
                offset1 = Vector3.up * planeSize;
                offset2 = Vector3.forward * planeSize;
                mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
            }
        }
        else if (x == width - 1)
        {
            if (sccsproceduralplanetbuilderrev5.sccsproceduralplanetbuilderrev5script.getChunk((int)(mainChunk.worldPosition.x + 4), (int)(mainChunk.worldPosition.y), (int)(mainChunk.worldPosition.z)) != null)
            {
              sccsproceduralplanetbuilderrev5.mainChunk chunkdata = sccsproceduralplanetbuilderrev5.sccsproceduralplanetbuilderrev5script.getChunk((int)(mainChunk.worldPosition.x + 4), (int)(mainChunk.worldPosition.y), (int)(mainChunk.worldPosition.z));

                float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

                if (chunkdata != null)
                {
                    var comp = chunkdata.sccsplanetchunkrev5;

                    if (comp != null)
                    {
                        if (comp.IsTransparent(0, y, z, mainChunk))
                        {
                            offset1 = Vector3.up * planeSize;
                            offset2 = Vector3.forward * planeSize;
                            mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                        }
                    }
                }
            }
        }

        //LEFTFACE
        if (x != 0)
        {
            //LEFTFACE
            if (IsTransparent(x - 1, y, z, mainChunk))
            {
                offset1 = Vector3.back * planeSize;
                offset2 = Vector3.down * planeSize;
                mainChunk = DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2, mainChunk);
            }
        }
        else if (x == 0)
        {
            if (sccsproceduralplanetbuilderrev5.sccsproceduralplanetbuilderrev5script.getChunk((int)(mainChunk.worldPosition.x - 4), (int)(mainChunk.worldPosition.y), (int)(mainChunk.worldPosition.z)) != null)
            {
                sccsproceduralplanetbuilderrev5.mainChunk chunkdata = sccsproceduralplanetbuilderrev5.sccsproceduralplanetbuilderrev5script.getChunk((int)(mainChunk.worldPosition.x - 4), (int)(mainChunk.worldPosition.y), (int)(mainChunk.worldPosition.z));

                float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
                float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
                float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

                if (chunkdata != null)
                {
                    var comp = chunkdata.sccsplanetchunkrev5;

                    if (comp != null)
                    {
                        if (comp.IsTransparent(width - 1, y, z, mainChunk))
                        {
                            offset1 = Vector3.back * planeSize;
                            offset2 = Vector3.down * planeSize;
                            mainChunk = DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2, mainChunk);
                        }
                    }
                }
            }
        }










        //FRONTFACE
        if (z == 0 && sccsproceduralplanetbuilderrev5.sccsproceduralplanetbuilderrev5script.getChunk((int)(mainChunk.worldPosition.x), (int)(mainChunk.worldPosition.y), (int)(mainChunk.worldPosition.z - 4)) != null)
        {
            sccsproceduralplanetbuilderrev5.mainChunk chunkdata = sccsproceduralplanetbuilderrev5.sccsproceduralplanetbuilderrev5script.getChunk((int)(mainChunk.worldPosition.x), (int)(mainChunk.worldPosition.y), (int)(mainChunk.worldPosition.z - 4));

            if (chunkdata != null)
            {
                var comp = chunkdata.sccsplanetchunkrev5;

                if (comp != null)
                {
                    if (comp.IsTransparent(x, y, depth - 1, mainChunk))
                    {
                        offset1 = Vector3.left * planeSize;
                        offset2 = Vector3.up * planeSize;
                        mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
                    }
                }
            }
        }

        else if (z != 0)
        {
            //FRONTFACE
            if (IsTransparent(x, y, z - 1, mainChunk))
            {
                offset1 = Vector3.left * planeSize;
                offset2 = Vector3.up * planeSize;
                mainChunk = DrawFace(start + Vector3.right * planeSize, offset1, offset2, mainChunk);
            }
        }

        //BACKFACE
        if (z == width - 1 && sccsproceduralplanetbuilderrev5.sccsproceduralplanetbuilderrev5script.getChunk((int)(mainChunk.worldPosition.x), (int)(mainChunk.worldPosition.y), (int)(mainChunk.worldPosition.z + 4)) != null)
        {
            sccsproceduralplanetbuilderrev5.mainChunk chunkdata = sccsproceduralplanetbuilderrev5.sccsproceduralplanetbuilderrev5script.getChunk((int)(mainChunk.worldPosition.x), (int)(mainChunk.worldPosition.y), (int)(mainChunk.worldPosition.z + 4));

            float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
            float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
            float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

            if (chunkdata != null)
            {
                var comp = chunkdata.sccsplanetchunkrev5;

                if (comp != null)
                {
                    if (comp.IsTransparent(x, y, 0, mainChunk))
                    {
                        offset1 = Vector3.right * planeSize;
                        offset2 = Vector3.up * planeSize;
                        mainChunk = DrawFace(start + Vector3.forward * planeSize, offset1, offset2, mainChunk);
                    }
                }
            }
        }

        else if (z != width - 1)
        {
            //BACKFACE
            if (IsTransparent(x, y, z + 1, mainChunk))
            {
                offset1 = Vector3.right * planeSize;
                offset2 = Vector3.up * planeSize;
                mainChunk = DrawFace(start + Vector3.forward * planeSize, offset1, offset2, mainChunk);
            }
        }






        //TOPFACE
        if (y == height - 1 && sccsproceduralplanetbuilderrev5.sccsproceduralplanetbuilderrev5script.getChunk((int)(mainChunk.worldPosition.x), (int)(mainChunk.worldPosition.y + 4), (int)(mainChunk.worldPosition.z)) != null)
        {
            sccsproceduralplanetbuilderrev5.mainChunk chunkdata = sccsproceduralplanetbuilderrev5.sccsproceduralplanetbuilderrev5script.getChunk((int)(mainChunk.worldPosition.x), (int)(mainChunk.worldPosition.y + 4), (int)(mainChunk.worldPosition.z));

            if (chunkdata != null)
            {
                var comp = chunkdata.sccsplanetchunkrev5;

                if (comp != null)
                {
                    if (comp.IsTransparent(x, 0, z, mainChunk))
                    {
                        offset1 = Vector3.forward * planeSize;
                        offset2 = Vector3.right * planeSize;
                        mainChunk = DrawFace(start + Vector3.up * planeSize, offset1, offset2, mainChunk);
                    }
                }
            }
        }

        else if (y != height - 1)
        {
            //TOPFACE
            if (IsTransparent(x, y + 1, z, mainChunk))
            {
                offset1 = Vector3.forward * planeSize;
                offset2 = Vector3.right * planeSize;
                mainChunk = DrawFace(start + Vector3.up * planeSize, offset1, offset2, mainChunk);
            }
        }

        //BOTTOMFACE
        if (y == 0 && sccsproceduralplanetbuilderrev5.sccsproceduralplanetbuilderrev5script.getChunk((int)(mainChunk.worldPosition.x), (int)(mainChunk.worldPosition.y - 4), (int)(mainChunk.worldPosition.z)) != null)
        {
            sccsproceduralplanetbuilderrev5.mainChunk chunkdata = sccsproceduralplanetbuilderrev5.sccsproceduralplanetbuilderrev5script.getChunk((int)(mainChunk.worldPosition.x), (int)(mainChunk.worldPosition.y - 4), (int)(mainChunk.worldPosition.z));

            if (chunkdata != null)
            {
                var comp = chunkdata.sccsplanetchunkrev5;

                if (comp != null)
                {
                    if (comp.IsTransparent(x, height - 1, z, mainChunk))
                    {
                        offset1 = Vector3.right * planeSize;
                        offset2 = Vector3.forward * planeSize;
                        mainChunk = DrawFace(start, offset1, offset2, mainChunk);
                    }
                }
            }
        }
        else if (y != 0)
        {
            //BOTTOMFACE
            if (IsTransparent(x, y - 1, z, mainChunk))
            {
                offset1 = Vector3.right * planeSize;
                offset2 = Vector3.forward * planeSize;
                mainChunk = DrawFace(start, offset1, offset2, mainChunk);
            }
        }

        return mainChunk;
    }

    public sccsproceduralplanetbuilderrev5.mainChunk DrawFace(Vector3 start, Vector3 offset1, Vector3 offset2, sccsproceduralplanetbuilderrev5.mainChunk mainChunk)
    {
        int index = mainChunk.verts.Count;

        mainChunk.verts.Add(start);
        mainChunk.verts.Add(start + offset1);
        mainChunk.verts.Add(start + offset2);
        mainChunk.verts.Add(start + offset1 + offset2);

        mainChunk.tris.Add(index + 0);
        mainChunk.tris.Add(index + 1);
        mainChunk.tris.Add(index + 2);
        mainChunk.tris.Add(index + 3);
        mainChunk.tris.Add(index + 2);
        mainChunk.tris.Add(index + 1);
        
        return mainChunk;
    }

    public void SetByte(int x, int y, int z, byte block,sccsproceduralplanetbuilderrev5.mainChunk mainChunk)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
        {
            //Debug.Log("out of range");
            return;
        }
        int indexOf = x + width * (y + depth * z);

        mainChunk.bytemap[indexOf] = block;

        /*if (this.gameObject.GetComponent<MeshCollider>() != null)
        {
            Destroy(this.gameObject.GetComponent<MeshCollider>());
        }*/
        //Destroy(this.gameObject.GetComponent<MeshFilter>());

        //verts.Clear();
        //tris.Clear();

        //Regenerate();

        //return map[x + width * (y + depth * z)];
    }





    public void SetBrick(int x, int y, int z, byte block)
    {
        //Debug.Log(x + " " + y + " " + z);

        if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= width) || (z >= width))
        {
            return;
        }
        //Debug.Log(x + " " + y + " " + z);

        /*if (x > 0 && x < width)
        {
            if (map[x, y, z] != block)
            {
                map[x, y, z] = block;          
                Regenerate();
            }
        }*/

        /*if (map[x, y, z] != block)
        {
            map[x, y, z] = block;
            Regenerate();
        }

        if (x == width - 1)
        {
            if (terrain.getChunk(chunkpos.x + 1, chunkpos.y, chunkpos.z) != null)
            {
                chunky chuk = terrain.getChunk(chunkpos.x + 1, chunkpos.y, chunkpos.z);
                chuk.chunker.GetComponent<chunk>().Regenerate();
            }
        }
        if (x == 0)
        {
            if (terrain.getChunk(chunkpos.x - 1, chunkpos.y, chunkpos.z) != null)
            {
                chunky chuk = terrain.getChunk(chunkpos.x - 1, chunkpos.y, chunkpos.z);
                chuk.chunker.GetComponent<chunk>().Regenerate();
            }
        }
        if (z == width - 1)
        {
            if (terrain.getChunk(chunkpos.x, chunkpos.y, chunkpos.z + 1) != null)
            {
                chunky chuk = terrain.getChunk(chunkpos.x, chunkpos.y, chunkpos.z + 1);
                chuk.chunker.GetComponent<chunk>().Regenerate();
            }
        }
        if (z == 0)
        {
            if (terrain.getChunk(chunkpos.x, chunkpos.y, chunkpos.z - 1) != null)
            {
                chunky chuk = terrain.getChunk(chunkpos.x, chunkpos.y, chunkpos.z - 1);
                chuk.chunker.GetComponent<chunk>().Regenerate();
            }
        }*/
    }




    public bool IsTransparent(int x, int y, int z, sccsproceduralplanetbuilderrev5.mainChunk mainChunk)
    {
        int indexOf = x + width * (y + depth * z);

        if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= height) || (z >= depth)) return true;
        {
            return mainChunk.bytemap[indexOf] == 0;
            //return map[x + width * (y + depth * z)] == 0;
        }
    }


    public byte GetByte(int x, int y, int z, sccsproceduralplanetbuilderrev5.mainChunk mainChunk)
    {
        int indexOf = x + width * (y + depth * z);

        if ((x < 0) || (y < 0) || (z < 0) || (y >= width) || (x >= height) || (z >= depth))
        {
            return 0;
        }
        return mainChunk.bytemap[indexOf];
        //return map[x + width * (y + depth * z)];
    }







    /*void Update()
    {
        //Debug.Log(mesh.vertices.Length);
        /*if (mesh.vertices.Length > 65000)
        {
            map = new byte[(int)width, (int)width, (int)width];
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Regenerate();
        }
    }*/


   /* void checkBytePos()
    {
        /*for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int z = 0; z < depth; z++)
                {
                    Instantiate(cube, new Vector3(x, y, z) * planeSize, Quaternion.identity);

                }
            }
        }
    }*/



    void checkBytePos()
    {
        float xPosition = (chunkpos.x);
        float yPosition = (chunkpos.y);
        float zPosition = (chunkpos.z);

        int xPose;
        int yPose;
        int zPose;

        if (xPosition < 0)
        {
            xPose = (int)Mathf.Ceil(xPosition);
        }

        else
        {
            xPose = (int)Mathf.Floor(xPosition);
        }

        if (yPosition < 0)
        {
            yPose = (int)Mathf.Ceil(yPosition);
        }

        else
        {
            yPose = (int)Mathf.Floor(yPosition);
        }

        if (zPosition < 0)
        {
            zPose = (int)Mathf.Ceil(zPosition);
        }

        else
        {
            zPose = (int)Mathf.Floor(zPosition);
        }




        /*if (xPosition < 0)
        {
            xPose = (int)Mathf.Round(xPosition);
            if (xPose % 2 != 0)
            {
                xPose -= currentWidth;
            }
        }

        else
        {
            xPose = (int)Mathf.Floor(xPosition);
            if (xPose % 2 != 0)
            {
                xPose += currentWidth;
            }
        }

        if (yPosition < 0)
        {
            yPose = (int)Mathf.Round(yPosition);
            if (yPose % 2 != 0)
            {
                yPose -= currentWidth;
            }
        }

        else
        {
            yPose = (int)Mathf.Floor(yPosition);
            if (yPose % 2 != 0)
            {
                yPose += currentWidth;
            }
        }

        if (zPosition < 0)
        {
            zPose = (int)Mathf.Round(zPosition);
            if (zPose % 2 != 0)
            {
                zPose -= currentWidth;
            }
        }

        else
        {
            zPose = (int)Mathf.Floor(zPosition);
            if (zPose % 2 != 0)
            {
                zPose += currentWidth;
            }
        }*/

        for (int x = xPose-width/2; x <  xPose+width/2 ; x++)
        {
            for (int y = yPose - width / 2; y < yPose + width / 2; y++)
            {
                for (int z = zPose - width / 2; z < zPose + width / 2; z++)
                {
                    int xPos = (x);
                    int yPos = (y);
                    int zPos = (z);

                    //Instantiate(cube, new Vector3(x, y, z), Quaternion.identity);

                    if (x < 0)
                    {
                        int yo = (int)Mathf.Ceil(-x / divider);
                        int yo1 = x + (yo * divider);
                        xPos = divider;
                        xPos += -yo;
                    }
                    else
                    {
                        int yo = (int)Mathf.Floor(x / divider);
                        xPos = x - (yo * divider);
                    }
                    if (y < 0)
                    {
                        int yo = (int)Mathf.Ceil(-y / divider);
                        int yo1 = y + (yo * divider);
                        yPos = divider;
                        yPos += -yo1;
                    }
                    else
                    {
                        int yo = (int)Mathf.Floor(y / divider);
                        yPos = y - (yo * divider);
                    }
                    if (z < 0)
                    {
                        int yo = (int)Mathf.Ceil(-z / divider);
                        int yo1 = z + (yo * divider);
                        zPos = divider;
                        zPos += -yo1;
                    }
                    else
                    {
                        int yo = (int)Mathf.Floor(z / divider);
                        zPos = z - (yo * divider);
                    }

                    //Debug.Log(xPos + " " + yPos + " " + zPos);
                    //Instantiate(cube, new Vector3(xPos,yPos,zPos)+chunkpos, Quaternion.identity);

                }
            }
        }
    }















    //Flat[x + WIDTH * (y + DEPTH * z)]










    /*void OnDrawGizmos()
    {

        if (mesh.vertices == null)
        {
            return;
        }

        Gizmos.color = Color.black;
        for (int i = 0; i < mesh.vertices.Length; i++)
        {
            Gizmos.DrawSphere(new Vector3(mesh.vertices[i].x + chunkpos.x, mesh.vertices[i].y + chunkpos.y, mesh.vertices[i].z + chunkpos.z), 0.01f);
        }


    }*/
}






/*if (x == 0)
        {
            Instantiate(sphere, new Vector3(x * planeSize + chunkpos.x, y * planeSize + chunkpos.y, z * planeSize + chunkpos.z), Quaternion.identity);
        }
        else if (y == 0)
        {
            Instantiate(sphere, new Vector3(x * planeSize + chunkpos.x, y * planeSize + chunkpos.y, z * planeSize + chunkpos.z), Quaternion.identity);
        }
        else if(z == 0)
        {
            Instantiate(sphere, new Vector3(x * planeSize + chunkpos.x, y * planeSize + chunkpos.y, z * planeSize + chunkpos.z), Quaternion.identity);
        }


        else if (x == width-1)
        {
            Instantiate(sphere, new Vector3(x * planeSize + chunkpos.x, y * planeSize + chunkpos.y, z * planeSize + chunkpos.z), Quaternion.identity);
        }
        else if (y == width - 1)
        {
            Instantiate(sphere, new Vector3(x * planeSize + chunkpos.x, y * planeSize + chunkpos.y, z * planeSize + chunkpos.z), Quaternion.identity);
        }
        else if (z == width - 1)
        {
            Instantiate(sphere, new Vector3(x * planeSize + chunkpos.x, y * planeSize + chunkpos.y, z * planeSize + chunkpos.z), Quaternion.identity);
        }*/







/*public void SetBrick(int x, int y, int z, byte block)
{
    //int x = Mathf.RoundToInt();

    //x -= (int)Mathf.RoundToInt(chunkpos.x);
    //y -= (int)Mathf.RoundToInt(chunkpos.y);
    //z -= (int)Mathf.RoundToInt(chunkpos.z);

    //x -= (int)Mathf.RoundToInt(chunkpos.x);
    //y -= (int)Mathf.RoundToInt(chunkpos.y);
    //z -= (int)Mathf.RoundToInt(chunkpos.z);

    // int x = 


    //Debug.Log(xx);
    //Debug.Log(yy);
    //Debug.Log(zz);

    Debug.Log("yo");

    if ((x < 0) || (y < 0) || (z < 0) || (x >= width) || (y >= width) || (z >= width))
    {
        return;
    }
    if (map[x, y, z] != block)
    {
        map[x, y, z] = block;
        Regenerate();
    }
}*/









/*if (x == width - 1)
{
    Debug.Log("yo0");

    chunk chuk = terrain.getChunkPos(chunkpos.x + 1, chunkpos.y, chunkpos.z);

    if (chuk.GetByte(0, y, z) == 1)
    {
        Debug.Log("yo1");

        if (chuk.map[width - 1, y, z] != block)
        {
            Debug.Log("yo2");

            chuk.map[0, y, z] = block;
            chuk.Regenerate();
        }
    }           
}*/







/*//RIGHTFACE
   if (x == width - 1 && terrain.getChunkPos(chunkpos.x + 1, chunkpos.y, chunkpos.z) != null)
   {
       chunk chuk = terrain.getChunkPos(chunkpos.x + 1, chunkpos.y, chunkpos.z);

       float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
       float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
       float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

       if (chuk.GetByte(0, y, z) == 0)
       {
           //Instantiate(sphere, new Vector3(xx, yy, zz) + terrain.getChunkPos(chunkpos.x, chunkpos.y, chunkpos.z).chunkpos, Quaternion.identity);
           //RIGHTFACE
           if (IsTransparent(x + 1, y, z))
           {
               offset1 = Vector3.up * planeSize;
               offset2 = Vector3.forward * planeSize;
               DrawFace(start + Vector3.right * planeSize, offset1, offset2, block);
           }             
       }         
   }
   else if (x != width - 1)
   {
       //RIGHTFACE
       if (IsTransparent(x + 1, y, z))
       {
           offset1 = Vector3.up * planeSize;
           offset2 = Vector3.forward * planeSize;
           DrawFace(start + Vector3.right * planeSize, offset1, offset2, block);
       }
   }*/

/*//LEFTFACE
if (x == 0 && terrain.getChunkPos(chunkpos.x - 1, chunkpos.y, chunkpos.z) != null)
{
    chunk chuk = terrain.getChunkPos(chunkpos.x - 1, chunkpos.y, chunkpos.z);

    //float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
    //float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
    //float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;
    if (chuk.GetByte(width-1, y, z) == 0)
    {
        //LEFTFACE
        if (IsTransparent(x - 1, y, z))
        {
            offset1 = Vector3.back * planeSize;
            offset2 = Vector3.down * planeSize;
            DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2, block);
        }
    }

}
else if (x != 0)
{
    //LEFTFACE
    if (IsTransparent(x - 1, y, z))
    {
        offset1 = Vector3.back * planeSize;
        offset2 = Vector3.down * planeSize;
        DrawFace(start + Vector3.up * planeSize + Vector3.forward * planeSize, offset1, offset2, block);
    }
}*/


/*//FRONTFACE
if (z == 0 && terrain.getChunkPos(chunkpos.x , chunkpos.y, chunkpos.z-1) != null)
{
    chunk chuk = terrain.getChunkPos(chunkpos.x , chunkpos.y, chunkpos.z-1);

    float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
    float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
    float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

    if (chuk.GetByte(x, y, width-1) == 0)
    {
    //float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
    //float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
    //float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;
        //Instantiate(sphere, new Vector3(xx, yy, zz) + terrain.getChunkPos(chunkpos.x, chunkpos.y, chunkpos.z).chunkpos, Quaternion.identity);
        //FRONTFACE
        if (IsTransparent(x, y, z - 1))
        {
            offset1 = Vector3.left * planeSize;
            offset2 = Vector3.up * planeSize;
            DrawFace(start + Vector3.right * planeSize, offset1, offset2, block);
        }
    }
}
else if (z != 0)
{
    //FRONTFACE
    if (IsTransparent(x, y, z - 1))
    {
        offset1 = Vector3.left * planeSize;
        offset2 = Vector3.up * planeSize;
        DrawFace(start + Vector3.right * planeSize, offset1, offset2, block);
    }
}


//BACKFACE
if (z == width-1 && terrain.getChunkPos(chunkpos.x, chunkpos.y, chunkpos.z + 1) != null)
{
    chunk chuk = terrain.getChunkPos(chunkpos.x, chunkpos.y, chunkpos.z + 1);

    float xx = (Mathf.Floor(start.x * fraction) / fraction) + chunkRadius;
    float yy = (Mathf.Floor(start.y * fraction) / fraction) + chunkRadius;
    float zz = (Mathf.Floor(start.z * fraction) / fraction) + chunkRadius;

    if (chuk.GetByte(x, y, 0) == 0)
    {
        //BACKFACE
        if (IsTransparent(x, y, z + 1))
        {
            offset1 = Vector3.right * planeSize;
            offset2 = Vector3.up * planeSize;
            DrawFace(start + Vector3.forward * planeSize, offset1, offset2, block);
        }
    }
}
else if (z != width - 1)
{
    //BACKFACE
    if (IsTransparent(x, y, z + 1))
    {
        offset1 = Vector3.right * planeSize;
        offset2 = Vector3.up * planeSize;
        DrawFace(start + Vector3.forward * planeSize, offset1, offset2, block);
    }
}*/



/*//BOTTOMFACE
if (IsTransparent(x, y - 1, z))
{
    offset1 = Vector3.right * planeSize;
    offset2 = Vector3.forward * planeSize;
    DrawFace(start, offset1, offset2, block);
}*/






//Generate basic terrain sine
/*  int[] terrainContour;
                    int Widther = 8;
                    int Heighter = 8;
                    terrainContour = new int[Widther * Heighter];

                    //Make Random Numbers
                    //double rand1 = randomizer.NextDouble() + 1;
                    //double rand2 = randomizer.NextDouble() + 2;
                    // double rand3 = randomizer.NextDouble() + 3;
                    //double rand1 = Random.Range(1, 10);
                    //double rand2 = Random.Range(1, 10);
                    //double rand3 = Random.Range(1, 10);


                    double rand1 = Mathf.Round(Noise.Generate(noiseX, noiseY, noiseZ));
                    double rand2 = Mathf.Round(Noise.Generate(noiseY, noiseZ, noiseX));
                    double rand3 = Mathf.Round(Noise.Generate(noiseZ, noiseX, noiseY));
  //Variables, Play with these for unique results!
                    //float peakheight = 20;
                    //float flatness = 50;
                    //int offset = 30;

                    float peakheight = 1;
                    float flatness = 25;
                    int offset = 15;
 * 
 * for (int xxx = 0; xxx < Widther; xxx++)
{
    double height = peakheight / rand1 * Mathf.Sin((float)(xxx / flatness * rand1 + rand1));
    height += peakheight / rand2 * Mathf.Sin((float)(xxx / flatness * rand2 + rand2));
    height += peakheight / rand3 * Mathf.Sin((float)(xxx / flatness * rand3 + rand3));

    height += offset;

    terrainContour[x] = (int)height;
}

if (y < terrainContour[x])
    map[x, y, z] = 1;
else
    map[x, y, z] = 0;*/


/*for (int xxxx = 0; xxxx < Widther; xxxx++)
{
    for (int yyyy = 0; yyyy < Heighter; yyyy++)
    {

        ///tiles[x, y] = Blank Tile
    }
}*/











/*float noiseValue0 = Noise.Generate(noiseX, noiseY, noiseZ);

if (noiseValue0 > 0.2f)
{
if (Mathf.Round(noiseValue0) + y == y && Mathf.Round(noiseValue0) + x == x && Mathf.Round(noiseValue0) + z == z)
{
    map[x, y, z] = 1;
}
}*/






/*if (test.getChunk(chunkpos.x, chunkpos.y + 1, chunkpos.z) != null)
{
float noiseXX = Mathf.Abs(((float)(x * planeSize + chunkpos.x + seed) / detailScale) * heightScale);
float noiseYY = Mathf.Abs(((float)(y * planeSize + chunkpos.y + 1 + seed) / detailScale) * heightScale);
float noiseZZ = Mathf.Abs(((float)(z * planeSize + chunkpos.z + seed) / detailScale) * heightScale);
float heightingo = Noise.Generate(noiseXX, noiseYY, noiseZZ);
heightingo += (10f - (float)y) / 10;
heightingo /= (float)y / 5;
mainChunk chuk = test.getChunk(chunkpos.x, chunkpos.y + 1, chunkpos.z);
if (heightingo >= 0.2f)
{
chuk.chunker.GetComponent<chunku>().map[x, y, z] = 1;
}
}*/





/*temporaryX *= (Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);
                           temporaryZ *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);
                           temporaryY *= -(Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);


                           float size0 = (1 / planeSize) * chunkpos.y;
                           temporaryY -= size0;

                           /*float size1 = (1 / planeSize) * chunkpos.x;
                           temporaryX -= size1;

                           float size2 = (1 / planeSize) * chunkpos.z;
                           temporaryZ -= size2;

                           if ((int)Mathf.Round(temporaryY) < y )
                           {
                               map[x, y, z] = 1;
                           }*/












/*if (chunkpos.y < 0)
{
    float size0 = (1 / planeSize) * chunkpos.y;
    temporaryY -= size0;
    temporaryY *= -(Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryY) <= y)
    {
        map[x, y, z] = 1;
    }
}
else
{
    float size0 = (1 / planeSize) * chunkpos.y;
    temporaryY -= size0;
    temporaryY *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryY) >= y )
    {
        map[x, y, z] = 1;
    }
}

if (chunkpos.x < 0)
{
    float size1 = (1 / planeSize) * chunkpos.x;
    temporaryX -= size1;
    temporaryX *= -(Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryX) <= x)
    {
        map[x, y, z] = 1;
    }
}
else
{
    float size1 = (1 / planeSize) * chunkpos.x;
    temporaryX -= size1;
    temporaryX *= (Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryX) <= x)
    {
        map[x, y, z] = 1;
    }
}



if (chunkpos.z < 0)
{
    float size2 = (1 / planeSize) * chunkpos.z;
    temporaryZ -= size2;
    temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryZ) <= z)
    {
        map[x, y, z] = 1;
    }
}
else
{
    float size2 = (1 / planeSize) * chunkpos.z;
    temporaryZ -= size2;
    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);
    if ((int)Mathf.Round(temporaryZ) >= z)
    {
        map[x, y, z] = 1;
    }
}*/



/*if (chunkpos.y < 0 || chunkpos.x < 0 || chunkpos.z < 0)
{
    temporaryY *= -(Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

    float size0 = (1 / planeSize) * chunkpos.y;
    temporaryY -= size0;

    temporaryX *= -(Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

    float size1 = (1 / planeSize) * chunkpos.x;
    temporaryX -= size1;
    temporaryZ *= -(Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);

    float size2 = (1 / planeSize) * chunkpos.z;
    temporaryZ -= size2;
    if ((int)Mathf.Round(temporaryY) < y && (int)Mathf.Round(temporaryX) < x && (int)Mathf.Round(temporaryZ) < z)
    {
        map[x, y, z] = 1;
    }                            
}*/

/*if (chunkpos.y >= 0 || chunkpos.x >= 0 || chunkpos.z >= 0)
{
    temporaryY *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

    float size0 = (1 / planeSize) * chunkpos.y;
    temporaryY -= size0;


    temporaryX *= (Mathf.PerlinNoise((y * planeSize + chunkpos.y + seed) / detailScale1, (z * planeSize + chunkpos.z + seed) / detailScale1) * heightScale1);

    float size1 = (1 / planeSize) * chunkpos.x;
    temporaryX -= size1;

    temporaryZ *= (Mathf.PerlinNoise((x * planeSize + chunkpos.x + seed) / detailScale1, (y * planeSize + chunkpos.y + seed) / detailScale1) * heightScale1);

    float size2 = (1 / planeSize) * chunkpos.z;
    temporaryZ -= size2;


    if ((int)Mathf.Round(temporaryY) >= y && (int)Mathf.Round(temporaryX) >= x && (int)Mathf.Round(temporaryZ) >= z)
    {
        map[x, y, z] = 1;
    }
}*/


















