using sccs;
using sccsr17;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;







/*
public class chunkdata
{

    public int isfirstchunkinbundle;
    public int _maxWidth;
    public int _maxHeight;
    public int _maxDepth;

    public int vertexlistWidth;
    public int vertexlistHeight;
    public int vertexlistDepth;

    public int widthflat;
    public int heightflat;
    public int depthflat;

    public int typeofterraintile;
    /*
    public int[][] somearrayofcoordsfloor;
    public int[][] somearrayofcoords;
    public chunkdata[] listofchunksadjacent;
    public chunkdata[] listofchunksadjacentfloor;

    public int memoryvertexcounter;
    //public tutorialchunkcubemap tutorialchunkcubemap;
    //public List<tutorialcubeaschunkinst.DVertex> vertexlisttop;


    public int rowIterateX;// = 0;
    public int rowIterateY;
    public int rowIterateZ;// = 0;

    public bool foundVertOne;// = false;
    public bool foundVertTwo;// = false;
    public bool foundVertThree;// = false;
    public bool foundVertFour;// = false;

    public int firstvertx;
    public int firstverty;
    public int firstvertz;

    public int secondvertx;
    public int secondverty;
    public int secondvertz;

    public int thirdvertx;
    public int thirdverty;
    public int thirdvertz;

    public int fourthvertx;
    public int fourthverty;
    public int fourthvertz;


    public int oneVertIndexX;// = 0;
    public int oneVertIndexY;// = 0;
    public int oneVertIndexZ;// = 0;

    public int twoVertIndexX;// = 0;
    public int twoVertIndexY;// = 0;
    public int twoVertIndexZ;// = 0;

    public int threeVertIndexX;// = 0;
    public int threeVertIndexY;//= 0;
    public int threeVertIndexZ;// = 0;

    public int fourVertIndexX;// = 0;
    public int fourVertIndexY;// = 0;
    public int fourVertIndexZ;// = 0;


    //public Vector4 chunkoriginpos;

    public int[] map;
    /*public int[] mapvertindexfordims;
    public int[] widthdimtop;
    public int[] heightdimtop;
    public int[] depthdimtop;
    public int[] mapfirstvertxtop;
    public int[] mapfirstvertytop;
    public int[] mapfirstvertztop;

    public int[] _tempChunkArray;
    public int[] _chunkVertexArray0;
    public int[] _testVertexArray0;


    public int someixtop;//= 0;
    public int someiytop;
    public int someiztop;
    public int someindextop;

    public int _newVertzCounter;
    public int someixleft;
    public int someiyleft;
    public int someizleft;
    public int someindexleft;


    public int someixright;
    public int someiyright;
    public int someizright;
    public int someindexright;


    public int someixfront;
    public int someiyfront;
    public int someizfront;
    public int someindexfront;


    public int someixback;
    public int someiyback;
    public int someizback;
    public int someindexback;


    public int someixbottom;
    public int someiybottom;
    public int someizbottom;
    public int someindexbottom;


    public int swtcdirtyarea;

    public float[] positioninbundle;

    public float distanceculling;
    public bool frustrumculldraw;
    public float[] realpos;
    public int[] chunkPos;
    //public int indexinlevelgenmap;
    public int indexintypeoftiles;
    public int typeofterraintiles;

    public int x;
    public int y;
    public int z;

    public int width;
    public int height;
    public int depth;
}*/

struct mapofints
{
    public int thebyte;
};

struct vertstruct
{
    Vector3 position;
};

struct trigstruct
{
    int trigindex;
};



public struct MapStruct
{
    public int cx;
    public int cy;
    public int cz;
    public int ix;
    public int iy;
    public int iz;
    public int thebyte;
    public Vector3 position;
}

namespace sccs
{
    //[RequireComponent(typeof(MeshFilter))]
    //[RequireComponent(typeof(MeshRenderer))]
    public class sccsgraphicssec : MonoBehaviour
    {
        public GameObject visualobject0;
        public GameObject visualobject1;
        public GameObject visualobject2;
        public GameObject visualobject3;

        public Material mat;

        private MapStruct[][] data;

        private mapofints[][] datachunkarray;
        private mapofints[][] datachunkvertexarray;
        private mapofints[][] datatestarray;
        private mapofints[][] datatemparray;



        private mapofints[][] datamapfirstvertxtop;
        private mapofints[][] datamapfirstvertytop;
        private mapofints[][] datamapfirstvertztop;
        private mapofints[][] datawidthdimtop;
        private mapofints[][] dataheightdimtop;
        private mapofints[][] datadepthdimtop;
        /*
        RWStructuredBuffer<mapofints> mapfirstvertxtop;
        RWStructuredBuffer<mapofints> mapfirstvertytop;
        RWStructuredBuffer<mapofints> mapfirstvertztop;

        RWStructuredBuffer<mapofints> widthdimtop;
        RWStructuredBuffer<mapofints> heightdimtop;
        RWStructuredBuffer<mapofints> depthdimtop;*/








        public ComputeShader computeShader;

        public ComputeShader computeShaderVertexMaps;

        public static tutorialcubeaschunkinststruct[][][] mainchunktopstruct;

        public static double paddingformaps = 0.0;

        public static sccsgraphicssec currentsccsgraphicssec;
        
        public int leveldivisionx = 1;
        public int leveldivisiony = 1;
        public int leveldivisionz = 1;

        public int thechunkbundlefractionx = 2;
        public int thechunkbundlefractiony = 2;
        public int thechunkbundlefractionz = 2;

        public int incrementsfracx = 1;
        public int incrementsfracy = 1;
        public int incrementsfracz = 1;


        int mapx = 10;
        int mapy = 10;
        int mapz = 10;


        int levelsizex = 1;
        int levelsizey = 1;
        int levelsizez = 1;

        public void Awake()
        {
            currentsccsgraphicssec = this;

            data = new MapStruct[levelsizex* levelsizey * levelsizez][];
            datachunkarray = new mapofints[levelsizex * levelsizey * levelsizez][];
            datachunkvertexarray = new mapofints[levelsizex * levelsizey * levelsizez][];
            datatestarray = new mapofints[levelsizex * levelsizey * levelsizez][];
            datatemparray = new mapofints[levelsizex * levelsizey * levelsizez][];


            datamapfirstvertxtop = new mapofints[levelsizex * levelsizey * levelsizez][];
            datamapfirstvertytop = new mapofints[levelsizex * levelsizey * levelsizez][];
            datamapfirstvertztop = new mapofints[levelsizex * levelsizey * levelsizez][];
            datawidthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
            dataheightdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];
            datadepthdimtop = new mapofints[levelsizex * levelsizey * levelsizez][];

  



            int vertexlistWidth = mapx + 1;
            int vertexlistHeight = mapy + 1;
            int vertexlistDepth = mapz + 1;

            for (int mx = 0; mx < levelsizex; mx++)
            {
                for (int my = 0; my < levelsizey; my++)
                {
                    for (int mz = 0; mz < levelsizez; mz++)
                    {
                        int mindex = mx + levelsizex * (my + levelsizey * mz);

                        //int totalSize = mapx * mapy * mapz;
                        data[mindex] = new MapStruct[mapx * mapy * mapz];

                        datachunkarray[mindex] = new mapofints[mapx * mapy * mapz];
                        datatemparray[mindex] = new mapofints[mapx * mapy * mapz];
                        datachunkvertexarray[mindex] = new mapofints[vertexlistWidth * vertexlistHeight * vertexlistDepth];
                        datatestarray[mindex] = new mapofints[vertexlistWidth * vertexlistHeight * vertexlistDepth];


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



                                    datachunkarray[mindex][index] = new mapofints();
                                    datachunkarray[mindex][index].thebyte = 0;

                                    datatemparray[mindex][index] = new mapofints();
                                    datatemparray[mindex][index].thebyte = 0;


                                    data[mindex][index] = new MapStruct();
                                    data[mindex][index].thebyte = 0;
                                    data[mindex][index].position = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);
                                    data[mindex][index].ix = x;
                                    data[mindex][index].iy = y;
                                    data[mindex][index].iz = z;
                                }
                            }
                        }



                        for (int i = 0; i < datachunkvertexarray[mindex].Length; i++)
                        {
                            datachunkvertexarray[mindex][i] = new mapofints();
                            datachunkvertexarray[mindex][i].thebyte = 0;

                            datatestarray[mindex][i] = new mapofints();
                            datatestarray[mindex][i].thebyte = 0;

                        }



                    }
                }
            }





          

        }


        // Start is called before the first frame update
        void Start()
        {
            int thebytesize = sizeof(int) * 7;
            int vector3Size = sizeof(float) * 3;
            int totalSize = thebytesize  + vector3Size;

   

            /*for (int x = 0; x < mapx; x++)
            {
                for (int y = 0; y < mapy; y++)
                {
                    for (int z = 0; z < mapz; z++)
                    {
                        int index = x + mapx * (y + mapy * z);

                        /*if (data[index].thebyte == 0)
                        {
                            Debug.Log("index:" + index);
                        }

                        Debug.Log("map:" + data[index].thebyte);
                    }
                }
            }*/



            for (int mx = 0; mx < levelsizex; mx++)
            {
                for (int my = 0; my < levelsizey; my++)
                {
                    for (int mz = 0; mz < levelsizez; mz++)
                    {


                        int mindex = mx + levelsizex * (my + levelsizey * mz);

                        Vector3 chunkmainpos = new Vector3(mx * mapx * 0.1f, my * mapy * 0.1f, mz * mapz * 0.1f);


                        //int totalSize = mapx * mapy * mapz;
                        //data[mindex] = new MapStruct[mapx * mapy * mapz];

                        ComputeBuffer mapsbuffer = new ComputeBuffer(data[mindex].Length, totalSize);

                        mapsbuffer.SetData(data[mindex]);

                        computeShader.SetBuffer(0, "themap", mapsbuffer);
                        //computeShader.SetFloat("resolution", data.Length);
                        //computeShader.SetFloat("repetitions", repetitions);
                        computeShader.Dispatch(0, data[mindex].Length / 10, 1, 1);


                        mapsbuffer.GetData(data[mindex]);




                        int[] mapint = new int[mapx * mapy * mapz];
                        for (int x = 0; x < mapx; x++)
                        {
                            for (int y = 0; y < mapy; y++)
                            {
                                for (int z = 0; z < mapz; z++)
                                {
                                    int index = x + mapx * (y + mapy * z);

                                    mapint[index] = data[mindex][index].thebyte;

                                    //Debug.Log("map:" + data[index].thebyte);
                                }
                            }
                        }

                        int vertexlistWidth = mapx + 1;
                        int vertexlistHeight = mapy + 1;
                        int vertexlistDepth = mapz + 1;


                        /*
                        int[] _chunkArray = new int[mapx * mapy * mapz];
                        int[] _tempChunkArray = new int[mapx * mapy * mapz];
                        int[] _chunkVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
                        int[] _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
                        */

                        int total = mapx * mapy * mapz;

                        for (int t = 0; t < vertexlistWidth * vertexlistHeight * vertexlistDepth; t++) //total
                        {
                            if (t < total)
                            {
                                if (mapint[t] == 1 || mapint[t] == 3)
                                {
                                    datachunkarray[mindex][t].thebyte = 1;
                                    datatemparray[mindex][t].thebyte = mapint[t]; //map[t]
                                }
                                else
                                {
                                    datachunkarray[mindex][t].thebyte = 0;
                                    datatemparray[mindex][t].thebyte = 0;

                                }
                            }

                            if (t < vertexlistWidth * vertexlistHeight * vertexlistDepth)
                            {
                                datachunkvertexarray[mindex][t].thebyte = 0;
                                /*_chunkVertexArray1[t] = 0;
                                _chunkVertexArray2[t] = 0;
                                _chunkVertexArray3[t] = 0;
                                _chunkVertexArray4[t] = 0;
                                _chunkVertexArray5[t] = 0;*/

                                datatestarray[mindex][t].thebyte = 0;
                                /*_testVertexArray1[t] = 0;
                                _testVertexArray2[t] = 0;
                                _testVertexArray3[t] = 0;
                                _testVertexArray4[t] = 0;
                                _testVertexArray5[t] = 0;*/
                            }
                        }

                        /*
                        int[] _chunkArray = new int[mapx * mapy * mapz];
                        int[] _tempChunkArray = new int[mapx * mapy * mapz];
                        int[] _chunkVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
                        int[] _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/


                        ComputeBuffer maps0buffer = new ComputeBuffer(data[mindex].Length, totalSize);
                        maps0buffer.SetData(data[mindex]);

                        
                        ComputeBuffer _chunkVertexArraybuffer = new ComputeBuffer(datachunkvertexarray[mindex].Length, 4);
                        _chunkVertexArraybuffer.SetData(datachunkvertexarray[mindex]);



                        ComputeBuffer _tempChunkArraybuffer = new ComputeBuffer(datatemparray[mindex].Length, 4);
                        _tempChunkArraybuffer.SetData(datatemparray[mindex]);


                        /*ComputeBuffer _chunkArraybuffer = new ComputeBuffer(datachunkarray[mindex].Length, 4);
                        _chunkArraybuffer.SetData(datachunkarray[mindex]);

                       
                        ComputeBuffer _testVertexArraybuffer = new ComputeBuffer(datatestarray[mindex].Length, 4);
                        _testVertexArraybuffer.SetData(datatestarray[mindex]);*/




                        /*computeShaderVertexMaps.SetBuffer(0, "_chunkArray", _chunkArraybuffer);
                
                        computeShaderVertexMaps.SetBuffer(0, "_testVertexArray", _testVertexArraybuffer);*/





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

                        computeShaderVertexMaps.SetBuffer(0, "mapfirstvertxtop", mapvertlocbufferx);
                        //computeShaderVertexMaps.SetBuffer(0, "mapfirstvertytop", mapvertlocbuffery);
                        computeShaderVertexMaps.SetBuffer(0, "mapfirstvertztop", mapvertlocbufferz);

                        computeShaderVertexMaps.SetBuffer(0, "widthdimtop", mapwidthdimtop);
                        computeShaderVertexMaps.SetBuffer(0, "heightdimtop", mapheightdimtop);
                        computeShaderVertexMaps.SetBuffer(0, "depthdimtop", mapdepthdimtop);

                        computeShaderVertexMaps.SetBuffer(0, "themap", maps0buffer);
                        computeShaderVertexMaps.SetBuffer(0, "_chunkVertexArray", _chunkVertexArraybuffer);
                        computeShaderVertexMaps.SetBuffer(0, "_tempChunkArray", _tempChunkArraybuffer);


                        /*ComputeBuffer triCountBuffer = new ComputeBuffer(1, sizeof(int), ComputeBufferType.Raw);

                        int maxTriangleCount = (mapx * mapy * mapz) * 6 * 3 * 2;
                        ComputeBuffer triangleBuffer;
                        triangleBuffer = new ComputeBuffer(maxTriangleCount, sizeof(int), ComputeBufferType.Append);
                        triangleBuffer.SetCounterValue(0);
                        computeShaderVertexMaps.SetBuffer(0, "triangles", triangleBuffer);

                        int maxVerticesCount = (mapx * mapy * mapz) * 6 * 4;
                        ComputeBuffer verticesBuffer;
                        verticesBuffer = new ComputeBuffer(maxVerticesCount, sizeof(float)* 3, ComputeBufferType.Append);
                        verticesBuffer.SetCounterValue(0);
                        computeShaderVertexMaps.SetBuffer(0, "vertexlist", verticesBuffer);
                        */





                        /*private mapofints[][] mapfirstvertxtop;
                        private mapofints[][] mapfirstvertytop;
                        private mapofints[][] mapfirstvertztop;
                        private mapofints[][] widthdimtop;
                        private mapofints[][] heightdimtop;
                        private mapofints[][] depthdimtop;*/

















                        //computeShader.SetFloat("resolution", data.Length);
                        //computeShader.SetFloat("repetitions", repetitions);
                        computeShaderVertexMaps.Dispatch(0, data[mindex].Length / 10, 1, 1); //data[mindex].Length / 10
                        //datachunkvertexarray[mindex].Length / 10
                        //vertmapsbuffer.GetData(data[mindex]);

                        //ComputeBuffer triangleBuffer;
                        //triangleBuffer.SetCounterValue(0);

                        mapvertlocbufferx.GetData(datamapfirstvertxtop[mindex]);
                        mapvertlocbufferz.GetData(datamapfirstvertztop[mindex]);

                        mapwidthdimtop.GetData(datawidthdimtop[mindex]);
                        mapheightdimtop.GetData(dataheightdimtop[mindex]);
                        mapdepthdimtop.GetData(datadepthdimtop[mindex]);
                        /*
                        for (int i = 0; i < datamapfirstvertxtop[mindex].Length; i++)
                        {
                            Debug.Log(datamapfirstvertxtop[mindex][i].thebyte);
                        }*/




                        for (int x = 0; x < mapx; x++)
                        {
                            for (int y = 0; y < mapy; y++)
                            {
                                for (int z = 0; z < mapz; z++)
                                {
                                    int index = x + mapx * (y + mapy * z);

                                    //mapint[index] = data[mindex][index].thebyte;

                                    //Debug.Log("map:" + data[index].thebyte);




                                    //if (mapint[index] == 1)
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
































                        /* mapdepthdimtop.GetData(datadepthdimtop[mindex]);
                         for (int i = 0; i < datadepthdimtop[mindex].Length; i++)
                         {
                             Debug.Log(datadepthdimtop[mindex][i].thebyte);
                         }*/

                        /*_testVertexArraybuffer.GetData(datatestarray[mindex]);
                        for (int i = 0;i < datatestarray[mindex].Length;i++)
                        {
                            Debug.Log(datatestarray[mindex][i].thebyte);
                        }*/







                        /*
                        
                        ComputeBuffer.CopyCount(triangleBuffer, triCountBuffer, 0);
                        int[] triCountArray = { 0 };
                        triCountBuffer.GetData(triCountArray);
                        int numTris = triCountArray[0];

                        // Get triangle data from shader
                        trigstruct[] tris = new trigstruct[numTris];
                        triangleBuffer.GetData(tris, 0, 0, numTris);


                        Debug.Log("/tris.Length:" + tris.Length + "/numTris:" + numTris);

                        */





















                        mapsbuffer.Dispose();
                        maps0buffer.Dispose();
                        _tempChunkArraybuffer.Dispose();
                        _chunkVertexArraybuffer.Dispose();


                        mapvertlocbufferx.Dispose();
                        //mapvertlocbuffery.Dispose();
                        mapvertlocbufferz.Dispose();
                        mapwidthdimtop.Dispose();
                        mapheightdimtop.Dispose();
                        mapdepthdimtop.Dispose();
                   


                        /*_chunkArraybuffer.Dispose();
                         _tempChunkArraybuffer.Dispose();
                         _testVertexArraybuffer.Dispose();*/
                        /*triangleBuffer.Dispose();
                        verticesBuffer.Dispose();*/





                        
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
        }






        // Update is called once per frame
        void Update()
        {

        }






    }

}
