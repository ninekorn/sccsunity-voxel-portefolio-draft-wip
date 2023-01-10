using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class sccscomputeUNIVERSEALLFACES : MonoBehaviour
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

    public mapbytes[][] mapdata;
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

    ComputeShader computeShaderForMap;
    ComputeShader computeVertexesALLFACES;
    
    //public ComputeShader computeVertexesTOP;
    //public ComputeShader computeVertexestwo;
    //public ComputeShader computeVertexesLEFT;
    //public ComputeShader computeVertexesRIGHT;
    //public ComputeShader computeVertexesFRONT;

    /*public Material frontfacemat;
    public Material rightfacemat;
    public Material mat2;
    public Material mat1;*/
    public Material mat;


    public int threadmulx = 1;
    public int threadmuly = 1;
    public int threadmulz = 1;


    int reducedverttrigswtc = 0;


    public int numberoffaces = 6;

    ComputeBuffer maps0buffer;

    ComputeBuffer mapvertlocbufferx;
    ComputeBuffer mapvertlocbuffery;
    ComputeBuffer mapvertlocbufferz;

    ComputeBuffer mapwidthdimtop;
    ComputeBuffer mapheightdimtop;
    ComputeBuffer mapdepthdimtop;

    Vector3 firstvertofface = Vector3.zero;
    Vector3 secondvertofface = Vector3.zero;
    Vector3 thirdvertofface = Vector3.zero;
    Vector3 fourthvertofface = Vector3.zero;


    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();


    ComputeShader swapcomputetop;
    ComputeShader swapcomputeleft;
    ComputeShader swapcomputeright;
    ComputeShader swapcomputefront;
    ComputeShader swapcomputeback;
    ComputeShader swapcomputebottom;




    public void WorkOnShader(Vector3 originchunkpos, GameObject parentobj)
    {

        for (int f = 0; f < numberoffaces; f++)
        {

            /*
            if (computeVertexesALLFACES != null)
            {
                GC.SuppressFinalize(computeVertexesALLFACES);
                computeVertexesALLFACES = null;
            }*/


            if (f == 0 && computeVertexesALLFACES == null)
            {
                computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexTOP");//ComputeShader.Find("Transparent/Diffuse");
                swapcomputetop = computeVertexesALLFACES;
            }
            else
            {
                if (swapcomputetop != null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexTOP");//ComputeShader.Find("Transparent/Diffuse");

                    //computeVertexesALLFACES = swapcomputetop;
                    //swapcompute = 
                }
            }



            if (f == 1 && computeVertexesALLFACES == null)
            {
                computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexLEFT");//ComputeShader.Find("Transparent/Diffuse");
                swapcomputeleft = computeVertexesALLFACES;
            }
            else
            {
                if (swapcomputeleft != null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexLEFT");//ComputeShader.Find("Transparent/Diffuse");

                    //computeVertexesALLFACES = swapcomputeleft;
                    //swapcompute = 
                }
            }




            if (f == 2 && computeVertexesALLFACES == null)
            {
                computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexRIGHT");//ComputeShader.Find("Transparent/Diffuse");
                swapcomputeright = computeVertexesALLFACES;
            }
            else
            {
                if (swapcomputeright != null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexRIGHT");//ComputeShader.Find("Transparent/Diffuse");

                    //computeVertexesALLFACES = swapcomputeright;
                    //swapcompute = 
                }
            }




            if (f == 3 && computeVertexesALLFACES == null)
            {
                computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexFRONT");//ComputeShader.Find("Transparent/Diffuse");
                swapcomputefront = computeVertexesALLFACES;
            }
            else
            {
                if (swapcomputefront != null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexFRONT");//ComputeShader.Find("Transparent/Diffuse");

                    //computeVertexesALLFACES = swapcomputefront;
                    //swapcompute = 
                }
            }
            if (f == 4 && computeVertexesALLFACES == null)
            {
                computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexBACK");//ComputeShader.Find("Transparent/Diffuse");
                swapcomputeback = computeVertexesALLFACES;
            }
            else
            {
                if (swapcomputeback != null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexBACK");//ComputeShader.Find("Transparent/Diffuse");

                    //computeVertexesALLFACES = swapcomputeback;
                    //swapcompute = 
                }
            }
            if (f == 5 && computeVertexesALLFACES == null)
            {
                computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexBOTTOM");//ComputeShader.Find("Transparent/Diffuse");
                swapcomputebottom = computeVertexesALLFACES;
            }
            else
            {
                if (swapcomputebottom != null)
                {
                    computeVertexesALLFACES = (ComputeShader)Resources.Load("Compute/computevertexBOTTOM");//ComputeShader.Find("Transparent/Diffuse");

                    //computeVertexesALLFACES = swapcomputebottom;
                    //swapcompute = 
                }
            }


            int dotherest = 0;
            int dotherest1 = 0;


            if (dotherest == 1)
            {


                for (int mx = 0; mx < levelsizex; mx++)
                {
                    for (int my = 0; my < levelsizey; my++)
                    {
                        for (int mz = 0; mz < levelsizez; mz++)
                        {
                            int mindex = mx + levelsizex * (my + levelsizey * mz);

                            Vector3 chunkmainpos = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);




                            //if (f == 0 && mindex == 0)
                            {


                                int thebytesize = sizeof(int) * 4;
                                int vector3Size = sizeof(float) * 3;
                                int totalSize = thebytesize + vector3Size;

                                ComputeBuffer mapsbuffer = new ComputeBuffer(mapdata[mindex].Length, totalSize);

                                mapsbuffer.SetData(mapdata[mindex]);


                                //if (computeShaderForMap == null)
                                {
                                    computeShaderForMap = (ComputeShader)Resources.Load("Compute/sccsmapallcompute");//ComputeShader.Find("Transparent/Diffuse");

                                }





                                computeShaderForMap.SetBuffer(0, "themap", mapsbuffer);
                                computeShaderForMap.Dispatch(0, mapdata[mindex].Length/10, 1, 1);

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




                                /*maps0buffer.SetCounterValue(0);
                                mapvertlocbufferx.SetCounterValue(0);
                                mapvertlocbuffery.SetCounterValue(0);
                                mapvertlocbufferz.SetCounterValue(0);

                                mapwidthdimtop.SetCounterValue(0);
                                mapheightdimtop.SetCounterValue(0);
                                mapdepthdimtop.SetCounterValue(0);*/



                                if (maps0buffer != null)
                                {
                                    maps0buffer.Release();
                                    maps0buffer.Dispose();

                                }
                                if (mapvertlocbufferx != null)
                                {
                                    mapvertlocbufferx.Release();
                                    mapvertlocbufferx.Dispose();

                                }

                                if (mapvertlocbuffery != null)
                                {
                                    mapvertlocbuffery.Release();
                                    mapvertlocbuffery.Dispose();

                                }

                                if (mapvertlocbufferz != null)
                                {
                                    mapvertlocbufferz.Release();
                                    mapvertlocbufferz.Dispose();

                                }
                                if (mapwidthdimtop != null)
                                {
                                    mapwidthdimtop.Release();
                                    mapwidthdimtop.Dispose();

                                }
                                if (mapheightdimtop != null)
                                {
                                    mapheightdimtop.Release();
                                    mapheightdimtop.Dispose();

                                }
                                if (mapdepthdimtop != null)
                                {
                                    mapdepthdimtop.Release();
                                    mapdepthdimtop.Dispose();
                                }


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
                            }


                            /*else
                            {
                                /*int thebytesize = sizeof(int) * 4;
                                int vector3Size = sizeof(float) * 3;
                                int totalSize = thebytesize + vector3Size;

                                ComputeBuffer mapsbuffer = new ComputeBuffer(mapdata[mindex].Length, totalSize);

                                mapsbuffer.SetData(mapdata[mindex]);

                                computeShaderForMap.SetBuffer(0, "themap", mapsbuffer);
                                computeShaderForMap.Dispatch(0, (mapx * mapy * mapz) / 10, 1, 1);

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




                                //maps0buffer.SetCounterValue(0);
                                mapvertlocbufferx.SetCounterValue(0);
                                mapvertlocbuffery.SetCounterValue(0);
                                mapvertlocbufferz.SetCounterValue(0);

                                mapwidthdimtop.SetCounterValue(0);
                                mapheightdimtop.SetCounterValue(0);
                                mapdepthdimtop.SetCounterValue(0);


                                maps0buffer.SetData(mapdata[mindex]);
                                mapvertlocbufferx.SetData(datamapfirstvertxtop[mindex]);
                                mapvertlocbuffery.SetData(datamapfirstvertytop[mindex]);
                                mapvertlocbufferz.SetData(datamapfirstvertztop[mindex]);
                                mapwidthdimtop.SetData(datawidthdimtop[mindex]);
                                mapheightdimtop.SetData(dataheightdimtop[mindex]);
                                mapdepthdimtop.SetData(datadepthdimtop[mindex]);




                            }*/









                            if (dotherest1 == 0)
                            {




                                if (reducedverttrigswtc == 0)
                                {



                                    /*// Create a material with transparent diffuse shader
                                    Material material = new Material(Shader.Find("Transparent/Diffuse"));
                                    material.color = Color.green;

                                    // assign the material to the renderer
                                    GetComponent<Renderer>().material = material;*/






                                    computeVertexesALLFACES.SetBuffer(0, "themap", maps0buffer);
                                    computeVertexesALLFACES.SetBuffer(0, "mapfirstvertxtop", mapvertlocbufferx);
                                    computeVertexesALLFACES.SetBuffer(0, "mapfirstvertytop", mapvertlocbuffery);
                                    computeVertexesALLFACES.SetBuffer(0, "mapfirstvertztop", mapvertlocbufferz);

                                    computeVertexesALLFACES.SetBuffer(0, "widthdimtop", mapwidthdimtop);
                                    computeVertexesALLFACES.SetBuffer(0, "heightdimtop", mapheightdimtop);
                                    computeVertexesALLFACES.SetBuffer(0, "depthdimtop", mapdepthdimtop);

                                    computeVertexesALLFACES.Dispatch(0, threadmulx, threadmuly, threadmulz);











                                }
                                else if (reducedverttrigswtc == 1)
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


                                                    if (f == 0)
                                                    {




                                                        int indexofvert0 = vertices.Count;
                                                        firstvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                                        int indexofvert1 = vertices.Count + 1;
                                                        secondvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                                        int indexofvert2 = vertices.Count + 2;
                                                        thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                                        int indexofvert3 = vertices.Count + 3;
                                                        fourthvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));


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
                                                    }
                                                    else if (f == 1)
                                                    {

                                                        int indexofvert0 = vertices.Count;
                                                        firstvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        //firstvertofface.x = swapy;
                                                        //firstvertofface.y = datawidthdimtop[mindex][index].thebyte;

                                                        //firstvertofface.x -= (1 * 1.0f);
                                                        //firstvertofface.y -= (1 * 1.0f);


                                                        int indexofvert1 = vertices.Count + 1;
                                                        secondvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);
                                                        /*swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;

                                                        secondvertofface.x = swapy;
                                                        secondvertofface.y = datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                                        secondvertofface.x -= (1 * 1.0f);
                                                        secondvertofface.y -= (1 * 1.0f);


                                                        int indexofvert2 = vertices.Count + 2;
                                                        thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                                        //thirdvertofface.x -= (1 * 1.0f);
                                                        //thirdvertofface.y -= (1 * 1.0f);
                                                        /*swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;

                                                        thirdvertofface.x = swapy;
                                                        thirdvertofface.y = swapx;*/

                                                        int indexofvert3 = vertices.Count + 3;
                                                        fourthvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                                        fourthvertofface.x -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);


                                                        /*swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;

                                                        fourthvertofface.x = swapy;
                                                        fourthvertofface.y = swapx;*/
                                                        /*
                                                        int indexofvert0 = vertices.Count;
                                                        Vector3 firstvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                                        int indexofvert1 = vertices.Count + 1;
                                                        Vector3 secondvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte);

                                                        int indexofvert2 = vertices.Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                                        int indexofvert3 = vertices.Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte),datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                                        */



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
                                                    }
                                                    else if (f == 2)
                                                    {


                                                        int indexofvert0 = vertices.Count;
                                                        firstvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        //firstvertofface.x = swapy;
                                                        //firstvertofface.y = datawidthdimtop[mindex][index].thebyte;

                                                        //firstvertofface.x -= (1 * 1.0f);
                                                        //firstvertofface.y -= (1 * 1.0f);


                                                        int indexofvert1 = vertices.Count + 1;
                                                        secondvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);
                                                        /*swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;

                                                        secondvertofface.x = swapy;
                                                        secondvertofface.y = datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                                        secondvertofface.x -= (1 * 1.0f);
                                                        secondvertofface.y -= (1 * 1.0f);


                                                        int indexofvert2 = vertices.Count + 2;
                                                        thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                                        //thirdvertofface.x -= (1 * 1.0f);
                                                        //thirdvertofface.y -= (1 * 1.0f);
                                                        /*swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;

                                                        thirdvertofface.x = swapy;
                                                        thirdvertofface.y = swapx;*/

                                                        int indexofvert3 = vertices.Count + 3;
                                                        fourthvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                                        fourthvertofface.x -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);


                                                        /*swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;

                                                        fourthvertofface.x = swapy;
                                                        fourthvertofface.y = swapx;*/
                                                        /*
                                                        int indexofvert0 = vertices.Count;
                                                        Vector3 firstvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                                        int indexofvert1 = vertices.Count + 1;
                                                        Vector3 secondvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte);

                                                        int indexofvert2 = vertices.Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                                        int indexofvert3 = vertices.Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte),datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                                        */



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
                                                    }
                                                    else if (f == 3)
                                                    {

                                                        int indexofvert0 = vertices.Count;
                                                        firstvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        //firstvertofface.x = swapy;
                                                        //firstvertofface.y = datawidthdimtop[mindex][index].thebyte;

                                                        /*firstvertofface.z -= (1 * 1.0f);
                                                        firstvertofface.y -= (1 * 1.0f);*/
                                                        //firstvertofface.z = swapy;
                                                        //firstvertofface.y = swapz;

                                                        int indexofvert1 = vertices.Count + 1;
                                                        secondvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);
                                                        swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;

                                                        /*secondvertofface.x = swapy;
                                                        secondvertofface.y = datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                                        /*secondvertofface.x -= (1 * 1.0f);
                                                        secondvertofface.y -= (1 * 1.0f);*/


                                                        int indexofvert2 = vertices.Count + 2;
                                                        thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                                        thirdvertofface.z -= (1 * 1.0f);
                                                        thirdvertofface.y -= (1 * 1.0f);
                                                        swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;

                                                        /*thirdvertofface.z = swapy;
                                                        thirdvertofface.y = swapz;*/

                                                        int indexofvert3 = vertices.Count + 3;
                                                        fourthvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                                        fourthvertofface.z -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);


                                                        swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;

                                                        /*fourthvertofface.y = swapz;
                                                        fourthvertofface.z = swapy;*/


                                                        /*fourthvertofface.x = swapy;
                                                        fourthvertofface.y = swapx;*/
                                                        /*
                                                        int indexofvert0 = vertices.Count;
                                                        Vector3 firstvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                                        int indexofvert1 = vertices.Count + 1;
                                                        Vector3 secondvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte);

                                                        int indexofvert2 = vertices.Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                                        int indexofvert3 = vertices.Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte),datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                                        */



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
                                                    }
                                                    else if (f == 4)
                                                    {

                                                        int indexofvert0 = vertices.Count;
                                                        firstvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        //firstvertofface.x = swapy;
                                                        //firstvertofface.y = datawidthdimtop[mindex][index].thebyte;

                                                        /*firstvertofface.z -= (1 * 1.0f);
                                                        firstvertofface.y -= (1 * 1.0f);*/
                                                        //firstvertofface.z = swapy;
                                                        //firstvertofface.y = swapz;

                                                        int indexofvert1 = vertices.Count + 1;
                                                        secondvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);
                                                        swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;

                                                        /*secondvertofface.x = swapy;
                                                        secondvertofface.y = datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                                        /*secondvertofface.x -= (1 * 1.0f);
                                                        secondvertofface.y -= (1 * 1.0f);*/


                                                        int indexofvert2 = vertices.Count + 2;
                                                        thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                                        thirdvertofface.z -= (1 * 1.0f);
                                                        thirdvertofface.y -= (1 * 1.0f);
                                                        swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;

                                                        /*thirdvertofface.z = swapy;
                                                        thirdvertofface.y = swapz;*/

                                                        int indexofvert3 = vertices.Count + 3;
                                                        fourthvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                                        fourthvertofface.z -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);


                                                        swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;

                                                        /*fourthvertofface.y = swapz;
                                                        fourthvertofface.z = swapy;*/


                                                        /*fourthvertofface.x = swapy;
                                                        fourthvertofface.y = swapx;*/
                                                        /*
                                                        int indexofvert0 = vertices.Count;
                                                        Vector3 firstvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                                        int indexofvert1 = vertices.Count + 1;
                                                        Vector3 secondvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), datamapfirstvertztop[mindex][index].thebyte);

                                                        int indexofvert2 = vertices.Count + 2;
                                                        Vector3 thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                                        int indexofvert3 = vertices.Count + 3;
                                                        Vector3 fourthvertofface = new Vector3(dataheightdimtop[mindex][index].thebyte, datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte),datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                                        */



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

                                                    }
                                                    else if (f == 5)
                                                    {
                                                        int indexofvert0 = vertices.Count;



                                                        firstvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);

                                                        float swapx = firstvertofface.x;
                                                        float swapy = firstvertofface.y;
                                                        float swapz = firstvertofface.z;

                                                        firstvertofface.y -= (1 * 1.0f);

                                                        //firstvertofface.x = swapy;
                                                        //firstvertofface.y = datawidthdimtop[mindex][index].thebyte;

                                                        /*firstvertofface.z -= (1 * 1.0f);
                                                        firstvertofface.y -= (1 * 1.0f);*/
                                                        //firstvertofface.z = swapy;
                                                        //firstvertofface.y = swapz;


                                                        int indexofvert1 = vertices.Count + 1;
                                                        secondvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte);
                                                        swapx = secondvertofface.x;
                                                        swapy = secondvertofface.y;
                                                        swapz = secondvertofface.z;

                                                        secondvertofface.y -= (1 * 1.0f);
                                                        /*secondvertofface.x = swapy;
                                                        secondvertofface.y = datawidthdimtop[mindex][index].thebyte; ///swapx;*/
                                                        /*secondvertofface.x -= (1 * 1.0f);
                                                        secondvertofface.y -= (1 * 1.0f);*/

                                                        int indexofvert2 = vertices.Count + 2;
                                                        thirdvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte, dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));
                                                        //thirdvertofface.z -= (1 * 1.0f);
                                                        thirdvertofface.y -= (1 * 1.0f);
                                                        swapx = thirdvertofface.x;
                                                        swapy = thirdvertofface.y;
                                                        swapz = thirdvertofface.z;

                                                        /*thirdvertofface.z = swapy;
                                                        thirdvertofface.y = swapz;*/

                                                        int indexofvert3 = vertices.Count + 3;
                                                        fourthvertofface = new Vector3(datamapfirstvertxtop[mindex][index].thebyte + (datawidthdimtop[mindex][index].thebyte), dataheightdimtop[mindex][index].thebyte, datamapfirstvertztop[mindex][index].thebyte + (datadepthdimtop[mindex][index].thebyte));

                                                        //fourthvertofface.z -= (1 * 1.0f);
                                                        fourthvertofface.y -= (1 * 1.0f);


                                                        swapx = fourthvertofface.x;
                                                        swapy = fourthvertofface.y;
                                                        swapz = fourthvertofface.z;


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
                                                    }


                                                    //Instantiate(visualobject0, firstvertofface * 0.1f, Quaternion.identity);
                                                    //Instantiate(visualobject1, secondvertofface * 0.1f, Quaternion.identity);
                                                    //Instantiate(visualobject2, thirdvertofface * 0.1f, Quaternion.identity);
                                                    //Instantiate(visualobject3, fourthvertofface * 0.1f, Quaternion.identity);













                                                    /*
                                                    GameObject vert0 = GameObject.CreatePrimitive(UnityEngine.PrimitiveType.Cube);

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
                                                    vert3.transform.rotation = Quaternion.identity;*/

                                                }
                                            }
                                        }
                                    }
                                }



                                
                                GameObject emptyobject = parentobj;// new GameObject();
                                //var meshfilt = emptyobject.AddComponent<MeshFilter>();
                                //var meshrend = emptyobject.AddComponent<MeshRenderer>();

                                Mesh thenewmesh = new Mesh();
                                thenewmesh.vertices = vertices.ToArray();
                                thenewmesh.triangles = triangles.ToArray();

                                emptyobject.GetComponent<MeshFilter>().mesh = thenewmesh;
                                //_testChunk.GetComponent<MeshRenderer>().material = _mat;

                                emptyobject.transform.position = chunkmainpos;
                                emptyobject.transform.rotation = Quaternion.identity;

                                emptyobject.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                                emptyobject.GetComponent<MeshRenderer>().material = mat;

                                //emptyobject.transform.parent = emptyobjectparent0.transform;
                                emptyobject.gameObject.name = "faces type:" + f;


                            }

                        }

















































                        /*maps0buffer.Release();
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
                        mapdepthdimtop.Dispose();*/
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
        }
        /*maps0buffer.Release();
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
        mapdepthdimtop.Dispose();*/
        /*
        mapdata = new mapbytes[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertxtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertytop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datamapfirstvertztop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datawidthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        dataheightdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
        datadepthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];*/


        computeVertexesALLFACES = null;
        computeShaderForMap = null;
    }





    Vector3 chunkmainpos = Vector3.zero;

    // Start is called before the first frame update
    public void CreateArrays()
    {


        for (int f = 0; f < numberoffaces; f++)
        {



            mapdata = new mapbytes[levelsizex * levelsizey * levelsizez][];
            datamapfirstvertxtop = new mapofints[levelsizex * levelsizey * levelsizez][];
            datamapfirstvertytop = new mapofints[levelsizex * levelsizey * levelsizez][];
            datamapfirstvertztop = new mapofints[levelsizex * levelsizey * levelsizez][];
            datawidthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
            dataheightdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
            datadepthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];


            //GameObject emptyobjectparent0 = this.transform.gameObject;
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

                        chunkmainpos = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);


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




                    /*maps0buffer.Release();
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
                    mapdepthdimtop.Dispose();*/
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
        /*maps0buffer.Release();
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
        mapdepthdimtop.Dispose();*/

        //WorkOnShader(chunkmainpos, this.transform.gameObject);
    }




    









    // Update is called once per frame
    void Update()
    {
        
    }
}
