using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System;
using UnityEditor;
using sccs;
using UnityEngine.UIElements;

public class singleChunkFAIL2023jan06 : MonoBehaviour
{
    /*public struct DVertex
    {
        public Vector3 position;
    }*/



    public int levelofdetail = 1;
    public int levelofdetailmul = 1;

    public int widthflat;
    public int heightflat;
    public int depthflat;


    int _combineMax = 10;

    public int width = 30;
    public int height = 30;
    public int depth = 30;

    //int _mapWidth = 20;
    //int _mapHeight = 10;
    //int _mapDepth = 20;

    //int _indexOfChunk = 0;

    public int[] _chunkArray;
    public int[] _tempChunkArray;

    //int[] _perlinChunkArray;

    public int[] _tempChunkArrayRightFace;
    public int[] _tempChunkArrayLeftFace;

    public int[] _tempChunkArrayFrontFace;
    public int[] _tempChunkArrayBackFace;
    public int[] _tempChunkArrayBottomFace;


    int[] _chunkVertexArray;
    int[] _testVertexArray;

    int _seed = 3425;//3425 //3420 //3441
    int _block;

    public int vertexlistWidth = 0;
    public int vertexlistHeight = 0;
    public int vertexlistDepth = 0;

    Vector3[] _vertices;
    //Vector3[] _normals;
    //Vector2[] _uvs;
    int[] _triangles;

    List<int> triangles;

    int _totalVertex = 0;

    int vertexlisttopCounter = 0;

    int _detailScale = 10;
    int _heightScale = 3;

    //Stopwatch _stopWatch = new Stopwatch();

    float planeSize = 0.1f;
    public GameObject _testChunk;
    Vector3 _chunkPos;

    List<Vector3> vertexlist = new List<Vector3>();
    //List<Vector3> _normals = new List<Vector3>();
    ///List<Vector2> _uvs = new List<Vector2>();

    //List<Vector4> _toShaderArray = new List<Vector4>();

    Mesh _mesh;

    public GameObject _sphereVisual;

    //public Shader _shader;
    public Material _mat;
    MeshRenderer _meshRend;

    int _index0 = 0;
    int _index1 = 0;
    int _index2 = 0;
    int _index3 = 0;

    int _newVertzCounter = 0;
    //MeshCombineUtility _meshCombineUtility;
    chunkdata[] _arrayOfChunks;

    public int[] map;

    public void startthegen(Vector3 chunkPos)
    {
        //_arrayOfChunks = SC_Terrain._arrayOfChunks;// new chunk[_width * _mapWidth * _width * _mapHeight * _width * _mapDepth];
        //_indexOfChunk = SC_Terrain._indexInArrayOfChunks;
        //_meshCombineUtility = new MeshCombineUtility();

        //_shader = _mat.shader;
        /*vertexlistWidth = _width + 1;
        vertexlistHeight = _height + 1;
        vertexlistDepth = _depth + 1;
        _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
        */
        //_vertices = new Vector3[1];

        //meshFilters = new MeshFilter[_combineMax];
        //_testChunk = this.gameObject;

        //StartCoroutine(buildTerrain(_chunkPos));
        //_chunkPos = this.transform.position;
        //buildTerrain(_chunkPos);

        int total = width * height * depth;

        for (int t = 0; t < vertexlistWidth * vertexlistHeight * vertexlistDepth; t++) //total
        {
            if (t < total)
            {
                if (map[t] == 1 || map[t] == 3)
                {
                    _chunkArray[t] = 1;

                    _tempChunkArray[t] = map[t]; //map[t]
                    _tempChunkArrayRightFace[t] = 1;
                    _tempChunkArrayLeftFace[t] = 1;

                    _tempChunkArrayBottomFace[t] = 1;
                    _tempChunkArrayBackFace[t] = 1;
                    _tempChunkArrayFrontFace[t] = 1;
                }
                else
                {
                    _chunkArray[t] = 0;

                    _tempChunkArray[t] = 0;
                    _tempChunkArrayRightFace[t] = 0;
                    _tempChunkArrayLeftFace[t] = 0;

                    _tempChunkArrayBottomFace[t] = 0;
                    _tempChunkArrayBackFace[t] = 0;
                    _tempChunkArrayFrontFace[t] = 0;

                }
            }

            if (t < vertexlistWidth * vertexlistHeight * vertexlistDepth)
            {
                _chunkVertexArray[t] = 0;
                /*_chunkVertexArray1[t] = 0;
                _chunkVertexArray2[t] = 0;
                _chunkVertexArray3[t] = 0;
                _chunkVertexArray4[t] = 0;
                _chunkVertexArray5[t] = 0;*/

                _testVertexArray[t] = 0;
                /*_testVertexArray1[t] = 0;
                _testVertexArray2[t] = 0;
                _testVertexArray3[t] = 0;
                _testVertexArray4[t] = 0;
                _testVertexArray5[t] = 0;*/
            }
        }


        _chunkPos = chunkPos;
        _chunkData = buildingTerrain(_chunkPos);

    }



    chunkdata[] _arrayOfChunkDataCHUNK;
    int[] _arrayOfChunkCHUNK;

    public void buildTerrain(Vector3 _chunkPos)
    {
        //_arrayOfChunks = SC_Terrain._arrayOfChunks;// new chunk[_width * _mapWidth * _width * _mapHeight * _width * _mapDepth];
        //_indexOfChunk = SC_Terrain._indexInArrayOfChunks;

        //_width = SC_Terrain._width;
        //_width = SC_Terrain._height;
        //_width = SC_Terrain._depth;

        //_arrayOfChunkDataCHUNK = SC_Terrain._arrayOfChunkData;
        //_arrayOfChunkCHUNK = SC_Terrain._arrayOfChunk;

        /* vertexlistWidth = _width + 1;
         vertexlistHeight = _height + 1;
         vertexlistDepth = _depth + 1;
         _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];*/

        //_vertices = new Vector3[1];

        //meshFilters = new MeshFilter[_combineMax];

        //_chunkData = buildingTerrain(_chunkPos);
        //_chunkData = new chunkData();
        //UnityEngine.Debug.Log("test");
    }





    int _indexOfInstance = 0;

    //MeshInstance[] combines;
    MeshFilter[] meshFilters;

    MeshRenderer _meshRenderer;

    bool _stop = true;

    GameObject _newChunk;

    bool _dontAdd = true;



    bool resultsOne = false;
    bool resultsTwo = false;
    bool resultsThree = false;

    public GameObject _vertVisual;
    public GameObject _sphereVisualOtherColor;
    public GameObject _sphereVisualOtherColorRed;
    public GameObject _sphereVisualOtherColorBlue;
    public GameObject _sphereVisualOtherColorBlack;



    float size0 = 0;
    float temporaryY = 0;
    chunkdata _chunkData;


    public void createvars()
    {
        //Vector3 _chunkPos = this.gameObject.transform.position;
        _index0 = 0;
        _index1 = 0;
        _index2 = 0;
        _index3 = 0;

        vertexlist = new List<Vector3>();
        triangles = new List<int>();
        //_normals = new List<Vector3>();
        //_uvs = new List<Vector2>();

        _totalVertex = 0;
        _newVertzCounter = 0;
        //vertexlisttopCounter = 0;

        /*_stopWatch.Stop();
        _stopWatch.Reset();
        _stopWatch.Start();*/

        /*vertexlistWidth = width + 1;
        vertexlistHeight = height + 1;
        vertexlistDepth = depth + 1;*/

        _tempChunkArrayBottomFace = new int[width * height * depth];
        _tempChunkArrayBackFace = new int[width * height * depth];
        _tempChunkArrayFrontFace = new int[width * height * depth];
        _tempChunkArrayLeftFace = new int[width * height * depth];
        _tempChunkArrayRightFace = new int[width * height * depth];
        _tempChunkArray = new int[width * height * depth];
        _chunkArray = new int[width * height * depth];

        _chunkVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
        _testVertexArray = new int[vertexlistWidth * vertexlistHeight * vertexlistDepth];
        //_perlinChunkArray = new int[width * height * depth];
    }



    chunkdata buildingTerrain(Vector3 _chunkPos) //unsafe
    {
        _chunkData = new chunkdata();


        //fixed (byte* _array = _chunkArray)
        {
            /*for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    for (int z = 0; z < depth; z++)
                    {
                        temporaryY = 10f;
                        //float temporaryZ = 10f;
                        //float temporaryX = 10f;

                        temporaryY *= Mathf.PerlinNoise((x * planeSize + _chunkPos.x + _seed) / _detailScale, (z * planeSize + _chunkPos.z + _seed) / _detailScale) * _heightScale;
                        //temporaryX *= Mathf.PerlinNoise((y * planeSize + _chunkPos.y + _seed) / _detailScale, (z * planeSize + _chunkPos.z + _seed) / _detailScale) * _heightScale;
                        //temporaryZ *= Mathf.PerlinNoise((x * planeSize + _chunkPos.x + _seed) / _detailScale, (y * planeSize + _chunkPos.y + _seed) / _detailScale) * _heightScale;

                        size0 = (1 / planeSize) * _chunkPos.y;
                        temporaryY -= size0;

                        //_chunkArray[x + _width * (y + _height * z)] = 1;

                        if ((int)Math.Round(temporaryY) >= y)
                        {
                            _chunkArray[x + width * (y + height * z)] = 1;
                            _tempChunkArray[x + width * (y + height * z)] = 1;
                            _tempChunkArrayRightFace[x + width * (y + height * z)] = 1;
                            _tempChunkArrayLeftFace[x + width * (y + height * z)] = 1;

                            _tempChunkArrayBottomFace[x + width * (y + height * z)] = 1;
                            _tempChunkArrayBackFace[x + width * (y + height * z)] = 1;
                            _tempChunkArrayFrontFace[x + width * (y + height * z)] = 1;
                        }
                        else
                        {
                            _chunkArray[x + width * (y + height * z)] = 0;
                            _tempChunkArray[x + width * (y + height * z)] = 0;
                            _tempChunkArrayRightFace[x + width * (y + height * z)] = 0;
                            _tempChunkArrayLeftFace[x + width * (y + height * z)] = 0;


                            _tempChunkArrayBottomFace[x + width * (y + height * z)] = 0;
                            _tempChunkArrayBackFace[x + width * (y + height * z)] = 0;
                            _tempChunkArrayFrontFace[x + width * (y + height * z)] = 0;

                        }

                        /*if (Math.Floor(temporaryY) >= y + 1)
                        {
                            _perlinChunkArray[x + _width * (y + _height * z)] = 2;
                        }

                        else if (Math.Floor(temporaryY) < y - 1)
                        {
                            _perlinChunkArray[x + _width * (y + _height * z)] = 1;
                        }
                        else
                        {
                            _perlinChunkArray[x + _width * (y + _height * z)] = 0;
                        }*/

            /*float size1 = (1 / planeSize) * _chunkPos.x;
            temporaryX -= size1;

            float size2 = (1 / planeSize) * _chunkPos.z;
            temporaryZ -= size2;
            */
            /*if ((int)Math.Round(temporaryY) >= y && (int)Math.Round(temporaryX) >= x && (int)Math.Round(temporaryZ) >= z)
            {
                map[x, y, z] = 1;
            }*/

            /*if (currentPosition.y < 1)
            {
                map[x, 0, z] = 1;
            }*/

            //_array[x + _width * (y + _height * z)] = 1;

            /*if (y ==0)
            {
                _chunkArray[x + _width * (y + _height * z)] = 1;
            }
            else
            {
                if ((int)Math.Round(temporaryY) >= y)
                {
                    _chunkArray[x + _width * (y + _height * z)] = 1;
                }
            }
            //_chunkArray[x + _width * (y + _height * z)] = 1;
        }
    }
}*/
        }

        //resultsOne = Array.TrueForAll(_perlinChunkArray, s => s == 0);
        //resultsTwo = Array.TrueForAll(_perlinChunkArray, s => s == 1);
        //resultsThree = Array.TrueForAll(_perlinChunkArray, s => s == 2);

        /*for (int x = 0; x < _width; x++)
        {
            for (int y = 0; y < _height; y++)
            {
                for (int z = 0; z < _depth; z++)
                {
                    _block = _chunkArray[x + _width * (y + _height * z)];
                    if (_block == 0) continue;
                    {
                        buildVertex(x, y, z);
                        //buildVertexArray(x, y, z);
                    }
                }
            }
        }*/






        /*for (int y = _height - 1; y >= 0; y--)
        {
            for (int x = 0; x < _width; x++)
            {
                for (int z = 0; z < _depth; z++)
                {
                    _maxWidth = _width;
                    _maxDepth = _depth;

                    //_lastRowX = 0;
                    //_lastRowZ = 0;

                    foundVertOne = false;
                    foundVertTwo = false;
                    foundVertThree = false;
                    foundVertFour = false;

                    _block = _tempChunkArray[x + _width * (y + _height * z)];

                    if (_block == 0 || _block == 2) continue; //|| _block == 2
                    {
                        //buildTopFace(x, y, z, _chunkPos);
                        //buildTopLeft(x, y, z, _chunkPos);
                        //yield return new WaitForSeconds(_iterateSpeed);
                        //////Instantiate(_sphereVisual, new Vector3(_x + 0.5f, y, _z + 0.5f), Quaternion.identity);                       
                    }
                }
            }
        }*/





        //_testChunk = new GameObject();
        _testChunk.transform.position = _chunkPos;
        _mesh = new Mesh();
        _testChunk.AddComponent<MeshFilter>();
        _testChunk.AddComponent<MeshRenderer>();

        //StartCoroutine(_buildFaces(_chunkPos));
        _buildFaces(_chunkPos);




        //_stopWatch.Stop();
        //_milli = _stopWatch.Elapsed.Milliseconds;
        //UnityEngine.Debug.Log(_milli);


        //_testChunk.AddComponent<MeshRenderer>();

        _mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;
        _testChunk.GetComponent<MeshRenderer>().material = _mat;
        //_meshRend.material = _mat;





        _chunkData._chunkBlockArray = _chunkArray;
        _chunkData._chunkVerticesArray = _chunkVertexArray;

        _chunkData._chunkVertices = vertexlist;
        _chunkData._chunkTriangles = triangles;

        _chunkData._chunkPosition = _chunkPos;


        //_chunkData._trueForAll = false;

        //_chunkData.resultsOne = resultsOne;
        //_chunkData.resultsTwo = resultsTwo;
        //_chunkData.resultsThree = resultsThree;



        return _chunkData;
    }

    int _milli = 0;
    int _maxHeight = 0;

    void _buildFaces(Vector3 _chunkPos)
    {
        //for (int y = height - 1; y >= 0; y--)
        for (int _x = 0; _x < width; _x++)
        {
            for (int y = 0; y < height; y++)
            {
                for (int _z = 0; _z < depth; _z++)
                {
                    _block = map[_x + width * (y + height * _z)];
                    //if (_block == 0) continue; //|| _block == 2
                    {

                        //StartCoroutine(buildFaces(_x, y, _z));                  
                        buildTopFace(_x, y, _z);
                        buildTopRight(_x, y, _z);
                        buildTopLeft(_x, y, _z);
                        buildFrontFace(_x, y, _z);
                        buildBackFace(_x, y, _z);
                        buildBottomFace(_x, y, _z);
                        //yield return new WaitForSeconds(_iterateSpeed);
                        ////////////Instantiate(_sphereVisual, new Vector3(_x + 0.5f, y, _z + 0.5f), Quaternion.identity);                      
                    }









                    /*_block = _tempChunkArrayRightFace[_x + _width * (y + _height * _z)];
                    if (_block == 0 || _block == 2) continue; //|| _block == 2
                    {
                        buildTopRight(_x, y, _z, _chunkPos);
                    }*/
                }
            }
        }
    }

    public float _iterateSpeed = 0.5f;

    int oneVertIndexX = 0;
    int oneVertIndexY = 0;
    int oneVertIndexZ = 0;

    int twoVertIndexX = 0;
    int twoVertIndexY = 0;
    int twoVertIndexZ = 0;

    int threeVertIndexX = 0;
    int threeVertIndexY = 0;
    int threeVertIndexZ = 0;

    int fourVertIndexX = 0;
    int fourVertIndexY = 0;
    int fourVertIndexZ = 0;

    int _maxWidth = 0;
    int _maxDepth = 0;
    int rowIterateX = 0;
    int rowIterateY = 0;
    int rowIterateZ = 0;


    bool foundVertOne = false;
    bool foundVertTwo = false;
    bool foundVertThree = false;
    bool foundVertFour = false;







    int firstvertx = 0;
    int firstverty = 0;
    int firstvertz = 0;


    int secondvertx = 0;
    int secondverty = 0;
    int secondvertz = 0;



    int thirdvertx = 0;
    int thirdverty = 0;
    int thirdvertz = 0;



    int fourthvertx = 0;
    int fourthverty = 0;
    int fourthvertz = 0;



    int someixtop = 0;
    int someiytop = 0;
    int someiztop = 0;
    int someindextop = 0;


    int someixleft = 0;
    int someiyleft = 0;
    int someizleft = 0;
    int someindexleft = 0;


    int someixright = 0;
    int someiyright = 0;
    int someizright = 0;
    int someindexright = 0;


    int someixfront = 0;
    int someiyfront = 0;
    int someizfront = 0;
    int someindexfront = 0;


    int someixback = 0;
    int someiyback = 0;
    int someizback = 0;
    int someindexback = 0;


    int someixbottom = 0;
    int someiybottom = 0;
    int someizbottom = 0;
    int someindexbottom = 0;








    //UnityEngine.Debug.Log("_xx: " + _xx + " _zz: " + _zz + " _maxWidth: " + _maxWidth + " _maxDepth: " + _maxDepth + " rowIterateX: " + rowIterateX + " rowIterateZ: " + rowIterateZ);
    void buildTopFace(int xi, int yi, int zi) //int _x, int _y, int _z, Vector3 chunkPos
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArray[xi + width * (yi + height * zi)];
        if (_block == 1) //|| _block == 2
        {

            if (IsTransparent(xi, yi + 1, zi))
            {
                for (int _xx = 0; _xx < _maxWidth; _xx++)
                {
                    rowIterateX = xi + _xx;
                    for (int _zz = 0; _zz < _maxDepth; _zz++)
                    {
                        rowIterateZ = zi + _zz;

                        if (rowIterateX < width && rowIterateZ < depth)
                        {
                            //if (someswtc == 1)
                            {
                                if (_xx == 0 && _zz == 0)
                                {
                                    oneVertIndexX = rowIterateX;
                                    oneVertIndexY = yi + 1;
                                    oneVertIndexZ = rowIterateZ;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                    foundVertOne = true;

                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi + 1;
                                            threeVertIndexZ = rowIterateZ;
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi + 1;
                                                    threeVertIndexZ = rowIterateZ;
                                                    _maxWidth = _xx;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArray[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                        if (_block == 0)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi + 1;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (_block == 1 || _block == 2) //_block == 1||
                                        {
                                            if (_block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX, yi + 1, rowIterateZ + 1))
                                                {
                                                    _block = _tempChunkArray[(rowIterateX) + width * ((yi + 1) + height * (rowIterateZ + 1))];

                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX;
                                                        twoVertIndexY = yi + 1;
                                                        twoVertIndexZ = rowIterateZ + 1;
                                                        _maxDepth = _zz + 1;
                                                        foundVertTwo = true;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi + 1;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = rowIterateX;
                                                twoVertIndexY = yi + 1;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = yi + 1;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_xx == 0 && _zz > 0)
                                {
                                    if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArray[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                        if (_block == 0)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi + 1;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }


                                        }
                                        else if (_block == 1 || _block == 2) //_block == 1||
                                        {
                                            if (_block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX, yi + 1, rowIterateZ + 1))
                                                {
                                                    _block = _tempChunkArray[(rowIterateX) + width * ((yi + 1) + height * (rowIterateZ + 1))];
                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX;
                                                        twoVertIndexY = yi + 1;
                                                        twoVertIndexZ = rowIterateZ + 1;
                                                        _maxDepth = _zz + 1;
                                                        foundVertTwo = true;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                        {
                                                            fourVertIndexX = threeVertIndexX;
                                                            fourVertIndexY = yi + 1;
                                                            fourVertIndexZ = twoVertIndexZ;
                                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                else //continue??
                                                {

                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = rowIterateX;
                                                twoVertIndexY = yi + 1;
                                                twoVertIndexZ = rowIterateZ + 1;
                                                _maxDepth = _zz + 1;
                                                foundVertTwo = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = yi + 1;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            //********************************************************
                                            if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi + 1;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxWidth = _xx;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                                else if (_xx > 0 && _zz == 0)
                                {
                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi + 1;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxWidth = _xx;
                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        _block = _tempChunkArray[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }

                                        if (blockExistsInArray(rowIterateX, yi + 1, rowIterateZ + 1))
                                        {
                                            //*****************************************************************************
                                            _block = _tempChunkArray[(rowIterateX) + width * ((yi + 1) + height * (rowIterateZ + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi + 1;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                            //*****************************************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }

                                else if (_xx > 0 && _zz > 0)
                                {
                                    if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                    {
                                        _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                        if (_block == 0)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX + 1;
                                            threeVertIndexY = yi + 1;
                                            threeVertIndexZ = rowIterateZ - _zz;
                                            _maxWidth = _xx;
                                            foundVertThree = true;
                                            ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi + 1;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }

                                            //***********************************************************
                                            if (blockExistsInArray(rowIterateX + 1, yi + 1, rowIterateZ))
                                            {
                                                _block = _tempChunkArray[(rowIterateX + 1) + width * ((yi + 1) + height * (rowIterateZ))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX + 1;
                                                    threeVertIndexY = yi + 1;
                                                    threeVertIndexZ = rowIterateZ - _zz;
                                                    _maxWidth = _xx;

                                                    foundVertThree = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi + 1;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //*******************************************************
                                        }
                                    }
                                    else
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (!blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi + 1;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, yi, rowIterateZ))
                        {
                            _tempChunkArray[(rowIterateX) + width * (yi + height * (rowIterateZ))] = 2;
                            ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + chunkPos, Quaternion.identity);
                        }
                    }
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                    _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                    _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                    _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                    triangles.Add(_index0);
                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                    triangles.Add(_index2);
                    triangles.Add(_index1);
                }
            }
        }
        /*//_mesh = new Mesh();
        _mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

        _meshRend = _testChunk.GetComponent<MeshRenderer>();
        _meshRend.material = _mat;*/
    }







    void buildTopLeft(int xi, int yi, int zi) //int _x, int _y, int _z, Vector3 chunkPos
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArrayLeftFace[xi + width * (yi + height * zi)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(xi - 1, yi, zi))
            {
                for (int _yy = 0; _yy < _maxHeight; _yy++)
                {
                    rowIterateY = yi + _yy;
                    for (int _zz = 0; _zz < _maxDepth; _zz++)
                    {
                        rowIterateZ = zi + _zz;

                        if (rowIterateY < height && rowIterateZ < depth)
                        {
                            if (_yy == 0 && _zz == 0)
                            {
                                oneVertIndexX = xi;
                                oneVertIndexY = rowIterateY;
                                oneVertIndexZ = rowIterateZ;
                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = xi;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = xi;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = xi;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(xi - 1, rowIterateY, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = xi;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = xi;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = xi;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _zz > 0)
                            {
                                if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = xi;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(xi - 1, rowIterateY, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = xi;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            else //continue??
                                            {

                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = xi;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = xi;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = xi;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = xi;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = xi;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ - _zz;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(xi - 1, rowIterateY, rowIterateZ + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayLeftFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = xi;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(xi - 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayLeftFace[(xi - 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;

                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(xi, rowIterateY, rowIterateZ))
                        {
                            _tempChunkArrayLeftFace[(xi) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                            ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                    _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                    _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                    _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                    triangles.Add(_index0);
                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                    triangles.Add(_index2);
                    triangles.Add(_index1);
                }
            }
        }
        /*//_mesh = new Mesh();
        _mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = _trigz.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

        _meshRend = _testChunk.GetComponent<MeshRenderer>();
        _meshRend.material = _mat;*/

        /*_mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;*/
        //_testChunk.GetComponent<MeshRenderer>().material = _mat;
    }

    void buildTopRight(int xi, int yi, int zi) //int xi, int _y, int _z, Vector3 chunkPos
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArrayRightFace[xi + width * (yi + height * zi)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(xi + 1, yi, zi))
            {
                for (int _yy = 0; _yy < _maxHeight; _yy++)
                {
                    rowIterateY = yi + _yy;
                    for (int _zz = 0; _zz < _maxDepth; _zz++)
                    {
                        rowIterateZ = zi + _zz;

                        if (rowIterateY < height && rowIterateZ < depth)
                        {
                            if (_yy == 0 && _zz == 0)
                            {
                                oneVertIndexX = xi + 1;
                                oneVertIndexY = rowIterateY;
                                oneVertIndexZ = rowIterateZ;
                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = xi + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = xi + 1;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = xi + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(xi + 1, rowIterateY, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = xi + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = xi + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = xi + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _zz > 0)
                            {
                                if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = xi + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(xi + 1, rowIterateY, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = xi + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = xi + 1;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            else //continue??
                                            {

                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = xi + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = xi + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = xi + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);
                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = xi + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = xi + 1;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = rowIterateZ - _zz;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY) + height * (rowIterateZ + 1))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(xi + 1, rowIterateY, rowIterateZ + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY) + height * (rowIterateZ + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = xi + 1;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(xi, rowIterateY + 1, rowIterateZ))
                                {
                                    _block = _tempChunkArrayRightFace[(xi) + width * ((rowIterateY + 1) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = xi + 1;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        ////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = xi + 1;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(xi + 1, rowIterateY + 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayRightFace[(xi + 1) + width * ((rowIterateY + 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = xi + 1;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxHeight = _yy;

                                                foundVertThree = true;
                                                ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = xi + 1;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(xi, rowIterateY, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = xi + 1;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(xi, rowIterateY, rowIterateZ))
                        {
                            _tempChunkArrayRightFace[(xi) + width * (rowIterateY + height * (rowIterateZ))] = 2;
                            ////////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                    _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                    _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                    _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                    triangles.Add(_index2);
                    triangles.Add(_index1);
                    triangles.Add(_index0);
                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                }
            }
        }
        /*//_mesh = new Mesh();
        _mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

        _meshRend = _testChunk.GetComponent<MeshRenderer>();
        _meshRend.material = _mat;*/
    }




    void buildFrontFace(int xi, int yi, int zi) // int _x, int _y, int _z, Vector3 chunkPos
    {

        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        oneVertIndexX = 0;
        oneVertIndexY = 0;
        oneVertIndexZ = 0;

        twoVertIndexX = 0;
        twoVertIndexY = 0;
        twoVertIndexZ = 0;

        threeVertIndexX = 0;
        threeVertIndexY = 0;
        threeVertIndexZ = 0;

        fourVertIndexX = 0;
        fourVertIndexY = 0;
        fourVertIndexZ = 0;

        _block = _tempChunkArrayFrontFace[xi + width * (yi + height * zi)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(xi, yi, zi + 1))
            {






                for (int _yy = 0; _yy < _maxHeight; _yy++)
                {
                    rowIterateY = yi + _yy;
                    for (int _xx = 0; _xx < _maxWidth; _xx++)
                    {
                        rowIterateX = xi + _xx;

                        if (rowIterateY < height && rowIterateX < width)
                        {
                            if (_yy == 0 && _xx == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = rowIterateY;
                                oneVertIndexZ = zi + 1;
                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi + 1;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = zi + 1;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi + 1;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi + 1))
                                            {
                                                _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = zi + 1;
                                                    _maxWidth = _xx + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi + 1;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = zi + 1;
                                    _maxWidth = _xx + 1;
                                    foundVertTwo = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _xx > 0)
                            {

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];
                                    
                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi + 1;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }


                                    }

                                }
















                                    /*if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {
                                        _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi + 1;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }


                                        }
                                        else if (_block == 1 || _block == 2) //_block == 1||
                                        {
                                            if (_block == 1)
                                            {
                                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi + 1))
                                                {
                                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi + 1))];
                                                    if (_block == 1 || _block == 2)
                                                    {
                                                        twoVertIndexX = rowIterateX + 1;
                                                        twoVertIndexY = rowIterateY;
                                                        twoVertIndexZ = zi + 1;
                                                        _maxWidth = _xx + 1;
                                                        foundVertTwo = true;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                        {
                                                            fourVertIndexX = twoVertIndexX;
                                                            fourVertIndexY = threeVertIndexY;
                                                            fourVertIndexZ = zi + 1;
                                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                        }
                                                    }
                                                }
                                                else //continue??
                                                {

                                                }
                                            }
                                            else if (_block == 2)
                                            {
                                                twoVertIndexX = rowIterateX + 1;
                                                twoVertIndexY = rowIterateY;
                                                twoVertIndexZ = zi + 1;
                                                _maxWidth = _xx + 1;
                                                foundVertTwo = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi + 1;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                    }

                                    if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                    {
                                        _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                        if (_block == 0)
                                        {
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY + 1;
                                            threeVertIndexZ = zi + 1;
                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        else if (_block == 1 || _block == 2)
                                        {
                                            //********************************************************
                                            if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                            {
                                                _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    threeVertIndexX = rowIterateX - _xx;
                                                    threeVertIndexY = rowIterateY + 1;
                                                    threeVertIndexZ = zi + 1;
                                                    _maxHeight = _yy;
                                                    foundVertThree = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi + 1;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            //************************************************************
                                        }
                                    }
                                    else
                                    {

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }*/
                                }
                                else if (_yy > 0 && _xx == 0)
                            {








                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {

                                    /*//UnityEngine.Debug.Log("test");
                                    threeVertIndexX = rowIterateX - _xx;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = zi + 1;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                    if (foundVertTwo)
                                    {
                                        if (foundVertThree)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }*/



                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];



                                    /*if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                    {

                                    }*/


                                    /*if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else
                                    {

                                        //if (_block == 2)
                                        {
                                            //UnityEngine.Debug.Log("test");
                                            threeVertIndexX = rowIterateX - _xx;
                                            threeVertIndexY = rowIterateY;
                                            threeVertIndexZ = zi + 1;

                                            _maxHeight = _yy;
                                            foundVertThree = true;
                                            //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                            if (foundVertTwo)
                                            {
                                                if (foundVertThree)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        
                                    }*/

                                }
                                else
                                {
                                    

                                    /*_block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }*/




                                    //if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                                    {

                                        


                                        /*//UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY;
                                        threeVertIndexZ = zi + 1;

                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }*/
                                    }

                                    /*threeVertIndexX = rowIterateX - _xx;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = zi + 1;
                                    _maxHeight = _yy - 1;
                                    foundVertThree = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }*/














                                    /*_block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }*/




                                }









                                /*if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi + 1;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX - _xx;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = zi + 1;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi - 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi+1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi+1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }*/










                                /*if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi + 1;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX - _xx;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = zi + 1;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayFrontFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi + 1;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }*/
                            }

                            else if (_yy > 0 && _xx > 0)
                            {
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                /*if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi + 1;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi + 1;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi + 1))
                                        {
                                            _block = _tempChunkArrayFrontFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi + 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi + 1;
                                                _maxHeight = _yy;

                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi + 1;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi + 1;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }*/
                            }
                        }

                        if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                        {
                            if (_tempChunkArrayFrontFace[(rowIterateX) + width * (rowIterateY + height * (zi))]  == 0)
                            {
                                _tempChunkArrayFrontFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 0;
                            }
                            if (_tempChunkArrayFrontFace[(rowIterateX) + width * (rowIterateY + height * (zi))] == 1)
                            {
                                _tempChunkArrayFrontFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
                            }




                            //_tempChunkArrayFrontFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;

                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0.0625f, 0.875f));
                    vertexlist.Add(new Vector3(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                    _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                    _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                    _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                    /*if (map[x, y, z] == leftExtremity[x, y, z]
                     || map[x, y, z] == backExtremity[x, y, z]
                     || map[x, y, z] == rightExtremity[x, y, z]
                     || map[x, y, z] == frontExtremity[x, y, z]
                     || map[x, y, z] == leftInsideCornerExtremity[x, y, z]
                     || map[x, y, z] == rightInsideCornerExtremity[x, y, z]
                    || map[x, y, z] == backInsideCornerExtremity[x, y, z]
                    || map[x, y, z] == frontInsideCornerExtremity[x, y, z]
                    || map[x, y, z] == leftOutsideCornerExtremity[x, y, z]
                    || map[x, y, z] == rightOutsideCornerExtremity[x, y, z]
                    || map[x, y, z] == backOutsideCornerExtremity[x, y, z]
                    || map[x, y, z] == frontOutsideCornerExtremity[x, y, z])
                    {
                        uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                        uv.Add(new Vector2(0.0625f, 0.9375f));
                        uv.Add(new Vector2(0, 0.875f));
                        uv.Add(new Vector2(0.0625f, 0.875f));
                    }
                    else
                    {
                        uv.Add(new Vector2(0, 1)); //// dis is weed
                        uv.Add(new Vector2(0.0625f, 1));
                        uv.Add(new Vector2(0, 0.9375f));
                        uv.Add(new Vector2(0.0625f, 0.9375f));
                    }*/


                    /*uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    uv.Add(new Vector2(0, 0.875f));
                    uv.Add(new Vector2(0.0625f, 0.875f));
                    */

                    triangles.Add(_index0);
                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                    triangles.Add(_index2);
                    triangles.Add(_index1);
                }
            }
        }
        /*//_mesh = new Mesh();
        _mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = _trigz.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

        _meshRend = _testChunk.GetComponent<MeshRenderer>();
        _meshRend.material = _mat;*/
    }


    void buildBackFace(int xi, int yi, int zi) //int _x, int _y, int zi, Vector3 chunkPos
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArrayBackFace[xi + width * (yi + height * zi)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(xi, yi, zi - 1))
            {
                for (int _yy = 0; _yy < _maxHeight; _yy++)
                {
                    rowIterateY = yi + _yy;
                    for (int _xx = 0; _xx < _maxWidth; _xx++)
                    {
                        rowIterateX = xi + _xx;

                        if (rowIterateY < height && rowIterateX < width)
                        {
                            if (_yy == 0 && _xx == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = rowIterateY;
                                oneVertIndexZ = zi;
                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y+1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = zi;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi - 1))
                                            {
                                                _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi - 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = zi;
                                                    _maxWidth = _xx + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = zi;
                                    _maxWidth = _xx + 1;
                                    foundVertTwo = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy == 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX + 1;
                                        twoVertIndexY = rowIterateY;
                                        twoVertIndexZ = zi;
                                        _maxWidth = _xx + 1;
                                        foundVertTwo = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi - 1))
                                            {
                                                _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi - 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX + 1;
                                                    twoVertIndexY = rowIterateY;
                                                    twoVertIndexZ = zi;
                                                    _maxWidth = _xx + 1;
                                                    foundVertTwo = true;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                    {
                                                        fourVertIndexX = twoVertIndexX;
                                                        fourVertIndexY = threeVertIndexY;
                                                        fourVertIndexZ = zi;
                                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            else //continue??
                                            {

                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX + 1;
                                            twoVertIndexY = rowIterateY;
                                            twoVertIndexZ = zi;
                                            _maxWidth = _xx + 1;
                                            foundVertTwo = true;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);


                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX + 1;
                                    twoVertIndexY = rowIterateY;
                                    twoVertIndexZ = zi;
                                    _maxWidth = _xx + 1;
                                    foundVertTwo = true;


                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, y + 1, rowIterateZ + 1) * planeSize + _chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);


                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_yy > 0 && _xx == 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi;
                                                _maxHeight = _yy;
                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX - _xx;
                                    threeVertIndexY = rowIterateY + 1;
                                    threeVertIndexZ = zi;
                                    _maxHeight = _yy;
                                    foundVertThree = true;
                                    //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX + 1, rowIterateY, zi - 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayBackFace[(rowIterateX + 1) + width * ((rowIterateY) + height * (zi - 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                            {
                                                fourVertIndexX = twoVertIndexX;
                                                fourVertIndexY = threeVertIndexY;
                                                fourVertIndexZ = zi;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_yy > 0 && _xx > 0)
                            {
                                if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi))
                                {
                                    _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX - _xx;
                                        threeVertIndexY = rowIterateY + 1;
                                        threeVertIndexZ = zi;
                                        _maxHeight = _yy;
                                        foundVertThree = true;
                                        //////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                        {
                                            fourVertIndexX = twoVertIndexX;
                                            fourVertIndexY = threeVertIndexY;
                                            fourVertIndexZ = zi;
                                            //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX, rowIterateY + 1, zi - 1))
                                        {
                                            _block = _tempChunkArrayBackFace[(rowIterateX) + width * ((rowIterateY + 1) + height * (zi - 1))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX - _xx;
                                                threeVertIndexY = rowIterateY + 1;
                                                threeVertIndexZ = zi;
                                                _maxHeight = _yy;

                                                foundVertThree = true;
                                                //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, rowIterateZ - ziz) * planeSize + _chunkPos, Quaternion.identity);

                                                if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                                {
                                                    fourVertIndexX = twoVertIndexX;
                                                    fourVertIndexY = threeVertIndexY;
                                                    fourVertIndexZ = zi;
                                                    //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX + 1, rowIterateY, zi))
                                {
                                    if (rowIterateX + 1 == twoVertIndexX && rowIterateY + 1 == threeVertIndexY)
                                    {
                                        fourVertIndexX = twoVertIndexX;
                                        fourVertIndexY = threeVertIndexY;
                                        fourVertIndexZ = zi;
                                        //////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, y + 1, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, rowIterateY, zi))
                        {
                            _tempChunkArrayBackFace[(rowIterateX) + width * (rowIterateY + height * (zi))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + _chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0.0625f, 0.9375f));
                    vertexlist.Add(new Vector3(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0, 0.875f));
                    vertexlist.Add(new Vector3(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    //uv.Add(new Vector2(0.0625f, 0.875f));
                    vertexlist.Add(new Vector3(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize));
                    //////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + _chunkPos, Quaternion.identity);
                    _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                    _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                    _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                    _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                    /*if (map[x, y, z] == leftExtremity[x, y, z]
                     || map[x, y, z] == backExtremity[x, y, z]
                     || map[x, y, z] == rightExtremity[x, y, z]
                     || map[x, y, z] == frontExtremity[x, y, z]
                     || map[x, y, z] == leftInsideCornerExtremity[x, y, z]
                     || map[x, y, z] == rightInsideCornerExtremity[x, y, z]
                    || map[x, y, z] == backInsideCornerExtremity[x, y, z]
                    || map[x, y, z] == frontInsideCornerExtremity[x, y, z]
                    || map[x, y, z] == leftOutsideCornerExtremity[x, y, z]
                    || map[x, y, z] == rightOutsideCornerExtremity[x, y, z]
                    || map[x, y, z] == backOutsideCornerExtremity[x, y, z]
                    || map[x, y, z] == frontOutsideCornerExtremity[x, y, z])
                    {
                        uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                        uv.Add(new Vector2(0.0625f, 0.9375f));
                        uv.Add(new Vector2(0, 0.875f));
                        uv.Add(new Vector2(0.0625f, 0.875f));
                    }
                    else
                    {
                        uv.Add(new Vector2(0, 1)); //// dis is weed
                        uv.Add(new Vector2(0.0625f, 1));
                        uv.Add(new Vector2(0, 0.9375f));
                        uv.Add(new Vector2(0.0625f, 0.9375f));
                    }*/


                    /*uv.Add(new Vector2(0, 0.9375f)); /// dis is rocks
                    uv.Add(new Vector2(0.0625f, 0.9375f));
                    uv.Add(new Vector2(0, 0.875f));
                    uv.Add(new Vector2(0.0625f, 0.875f));*/


                    triangles.Add(_index2);
                    triangles.Add(_index1);
                    triangles.Add(_index0);
                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                }
            }
        }
    }




    void buildBottomFace(int xi, int yi, int zi) //int _x, int _y, int _z, Vector3 chunkPos
    {
        _maxWidth = width;
        _maxDepth = depth;
        _maxHeight = height;
        foundVertOne = false;
        foundVertTwo = false;
        foundVertThree = false;
        foundVertFour = false;
        //TOPFACE

        _block = _tempChunkArrayBottomFace[xi + width * (yi + height * zi)];
        if (_block == 1) //|| _block == 2
        {
            if (IsTransparent(xi, yi - 1, zi))
            {
                for (int _xx = 0; _xx < _maxWidth; _xx++)
                {
                    rowIterateX = xi + _xx;
                    for (int _zz = 0; _zz < _maxDepth; _zz++)
                    {
                        rowIterateZ = zi + _zz;

                        if (rowIterateX < width && rowIterateZ < depth)
                        {
                            if (_xx == 0 && _zz == 0)
                            {
                                oneVertIndexX = rowIterateX;
                                oneVertIndexY = yi;
                                oneVertIndexZ = rowIterateZ;
                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                foundVertOne = true;

                                if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi;
                                        threeVertIndexZ = rowIterateZ;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];

                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi;
                                                threeVertIndexZ = rowIterateZ;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = yi;
                                    threeVertIndexZ = rowIterateZ;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = yi;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, yi - 1, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi - 1) + height * (rowIterateZ + 1))];

                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = yi;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = yi;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_xx == 0 && _zz > 0)
                            {
                                if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                    if (_block == 0)
                                    {
                                        twoVertIndexX = rowIterateX;
                                        twoVertIndexY = yi;
                                        twoVertIndexZ = rowIterateZ + 1;
                                        _maxDepth = _zz + 1;
                                        foundVertTwo = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }


                                    }
                                    else if (_block == 1 || _block == 2) //_block == 1||
                                    {
                                        if (_block == 1)
                                        {
                                            if (blockExistsInArray(rowIterateX, yi - 1, rowIterateZ + 1))
                                            {
                                                _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi - 1) + height * (rowIterateZ + 1))];
                                                if (_block == 1 || _block == 2)
                                                {
                                                    twoVertIndexX = rowIterateX;
                                                    twoVertIndexY = yi;
                                                    twoVertIndexZ = rowIterateZ + 1;
                                                    _maxDepth = _zz + 1;
                                                    foundVertTwo = true;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                    {
                                                        fourVertIndexX = threeVertIndexX;
                                                        fourVertIndexY = yi;
                                                        fourVertIndexZ = twoVertIndexZ;
                                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                    }
                                                }
                                            }
                                            else //continue??
                                            {

                                            }
                                        }
                                        else if (_block == 2)
                                        {
                                            twoVertIndexX = rowIterateX;
                                            twoVertIndexY = yi;
                                            twoVertIndexZ = rowIterateZ + 1;
                                            _maxDepth = _zz + 1;
                                            foundVertTwo = true;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);

                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    twoVertIndexX = rowIterateX;
                                    twoVertIndexY = yi;
                                    twoVertIndexZ = rowIterateZ + 1;
                                    _maxDepth = _zz + 1;
                                    foundVertTwo = true;

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX, yi + 1, rowIterateZ + 1) * planeSize + chunkPos, Quaternion.identity);
                                }

                                if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        //Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        //********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        //Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                            else if (_xx > 0 && _zz == 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        if (foundVertTwo)
                                        {
                                            if (foundVertThree)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;
                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    threeVertIndexX = rowIterateX + 1;
                                    threeVertIndexY = yi;
                                    threeVertIndexZ = rowIterateZ - _zz;
                                    _maxWidth = _xx;
                                    foundVertThree = true;
                                    ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi) + height * (rowIterateZ + 1))];

                                    if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }
                                    }

                                    if (blockExistsInArray(rowIterateX, yi - 1, rowIterateZ + 1))
                                    {
                                        //*****************************************************************************
                                        _block = _tempChunkArrayBottomFace[(rowIterateX) + width * ((yi - 1) + height * (rowIterateZ + 1))];
                                        if (_block == 1 || _block == 2)
                                        {
                                            if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                            {
                                                fourVertIndexX = threeVertIndexX;
                                                fourVertIndexY = yi;
                                                fourVertIndexZ = twoVertIndexZ;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                            }
                                        }
                                        //*****************************************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }

                            else if (_xx > 0 && _zz > 0)
                            {
                                if (blockExistsInArray(rowIterateX + 1, yi, rowIterateZ))
                                {
                                    _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi) + height * (rowIterateZ))];

                                    if (_block == 0)
                                    {
                                        //UnityEngine.Debug.Log("test");
                                        threeVertIndexX = rowIterateX + 1;
                                        threeVertIndexY = yi;
                                        threeVertIndexZ = rowIterateZ - _zz;
                                        _maxWidth = _xx;
                                        foundVertThree = true;
                                        ////Instantiate(_sphereVisualOtherColorBlack, new Vector3(rowIterateX+1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                    else if (_block == 1 || _block == 2)
                                    {
                                        if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                        {
                                            fourVertIndexX = threeVertIndexX;
                                            fourVertIndexY = yi;
                                            fourVertIndexZ = twoVertIndexZ;
                                            ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                        }

                                        //***********************************************************
                                        if (blockExistsInArray(rowIterateX + 1, yi - 1, rowIterateZ))
                                        {
                                            _block = _tempChunkArrayBottomFace[(rowIterateX + 1) + width * ((yi - 1) + height * (rowIterateZ))];
                                            if (_block == 1 || _block == 2)
                                            {
                                                threeVertIndexX = rowIterateX + 1;
                                                threeVertIndexY = yi;
                                                threeVertIndexZ = rowIterateZ - _zz;
                                                _maxWidth = _xx;

                                                foundVertThree = true;
                                                ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, rowIterateZ - _zz) * planeSize + chunkPos, Quaternion.identity);

                                                if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                                {
                                                    fourVertIndexX = threeVertIndexX;
                                                    fourVertIndexY = yi;
                                                    fourVertIndexZ = twoVertIndexZ;
                                                    ////Instantiate(_sphereVisualOtherColorOrange, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                                }
                                            }
                                        }
                                        //*******************************************************
                                    }
                                }
                                else
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }

                                if (!blockExistsInArray(rowIterateX, yi, rowIterateZ + 1))
                                {
                                    if (rowIterateZ + 1 == twoVertIndexZ && rowIterateX + 1 == threeVertIndexX)
                                    {
                                        fourVertIndexX = threeVertIndexX;
                                        fourVertIndexY = yi;
                                        fourVertIndexZ = twoVertIndexZ;
                                        ////Instantiate(_sphereVisualOtherColor, new Vector3(rowIterateX + 1, yi + 1, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                                    }
                                }
                            }
                        }

                        if (blockExistsInArray(rowIterateX, yi, rowIterateZ))
                        {
                            _tempChunkArrayBottomFace[(rowIterateX) + width * (yi + height * (rowIterateZ))] = 2;
                            //////Instantiate(_blockZero, new Vector3(rowIterateX + 0.5f, y, rowIterateZ + 0.5f) * planeSize + chunkPos, Quaternion.identity);
                        }
                    }
                }










                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(oneVertIndexX * planeSize, oneVertIndexY * planeSize, oneVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(oneVertIndexX, oneVertIndexY, oneVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = 1;
                    _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(twoVertIndexX * planeSize, twoVertIndexY * planeSize, twoVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(twoVertIndexX, twoVertIndexY, twoVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = 1;
                    _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }
                if (getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(threeVertIndexX * planeSize, threeVertIndexY * planeSize, threeVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(threeVertIndexX, threeVertIndexY, threeVertIndexZ)*planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = 1;
                    _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;

                }
                if (getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 0)
                {
                    vertexlist.Add(new Vector3(fourVertIndexX * planeSize, fourVertIndexY * planeSize, fourVertIndexZ * planeSize));
                    ////////////Instantiate(_sphereVisualOtherColorBlack, new Vector3(fourVertIndexX, fourVertIndexY, fourVertIndexZ) * planeSize + chunkPos, Quaternion.identity);
                    _chunkVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = 1;
                    _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)] = _newVertzCounter;
                    _newVertzCounter++;
                }

                if (getChunkVertexByte(oneVertIndexX, oneVertIndexY, oneVertIndexZ) == 1 && getChunkVertexByte(twoVertIndexX, twoVertIndexY, twoVertIndexZ) == 1 && getChunkVertexByte(threeVertIndexX, threeVertIndexY, threeVertIndexZ) == 1 && getChunkVertexByte(fourVertIndexX, fourVertIndexY, fourVertIndexZ) == 1)
                {
                    _index0 = _testVertexArray[oneVertIndexX + vertexlistWidth * ((oneVertIndexY) + vertexlistHeight * oneVertIndexZ)];
                    _index1 = _testVertexArray[twoVertIndexX + vertexlistWidth * ((twoVertIndexY) + vertexlistHeight * twoVertIndexZ)];
                    _index2 = _testVertexArray[threeVertIndexX + vertexlistWidth * ((threeVertIndexY) + vertexlistHeight * threeVertIndexZ)];
                    _index3 = _testVertexArray[fourVertIndexX + vertexlistWidth * ((fourVertIndexY) + vertexlistHeight * fourVertIndexZ)];

                    triangles.Add(_index2);
                    triangles.Add(_index1);
                    triangles.Add(_index0);
                    triangles.Add(_index1);
                    triangles.Add(_index2);
                    triangles.Add(_index3);
                }
            }
        }
        /*//_mesh = new Mesh();
        _mesh.vertices = vertexlist.ToArray();
        _mesh.triangles = triangles.ToArray();

        _testChunk.GetComponent<MeshFilter>().mesh = _mesh;

        _meshRend = _testChunk.GetComponent<MeshRenderer>();
        _meshRend.material = _mat;*/
    }








    int getChunklod0Vertexint0(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
        {
            return _chunkVertexArray[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
        }
        return 0;
    }

    public bool blockExistsInArray(int _x, int _y, int _z)
    {
        if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= width) || (_y >= height) || (_z >= depth))
        {
            return false;
        }
        /*else if (_chunkArray[_x + width * (_y + height * _z)]==0)
        {
            return false;
        }*/
        else
        {
            return true;
        }
        //return _chunkArray[_x + width * (_y + height * _z)] == 0;
    }

    public bool IsTransparent(int _x, int _y, int _z)
    {
        if ((_x < 0) || (_y < 0) || (_z < 0) || (_x >= width) || (_y >= height) || (_z >= depth)) return true;
        return _chunkArray[_x + width * (_y + height * _z)] == 0;
    }

    int getChunkByte(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < width && _y < height && _z < depth)
        {
            return _chunkArray[_x + width * (_y + height * _z)];
        }
        return 0;
    }


    int getTempArrayByte(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < width && _y < height && _z < depth)
        {
            return _tempChunkArray[_x + width * (_y + height * _z)];
        }
        return 0;
    }



    int getChunkVertexByte(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
        {
            return _chunkVertexArray[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
        }
        return 0;
    }


    /*byte ChunkVertexByteExists(int _x, int _y, int _z)
    {
        if (_x >= 0 && _y >= 0 && _z >= 0 && _x < vertexlistWidth && _y < vertexlistHeight && _z < vertexlistDepth)
        {
            return _chunkVertexArray[_x + vertexlistWidth * (_y + vertexlistHeight * _z)];
        }
        return 0;
    }*/

    /*bool byteExists(int _x, int _y, int _z)
    {
        if (_x < 0 || _y < 0 || _z < 0 || _x > _width || _y > _height || _z > _depth) return false;
        {
            return true;
        }
    }*/

    /* private void OnDrawGizmos()
     {
         if (_vertices == null)
         {
             return;
         }
         else
         {
             for (int i = 0; i < _vertices.Length; i++)
             {
                 Gizmos.color = Color.black;
                 Gizmos.DrawSphere(_vertices[i],0.1f);
             }
         }
     }*/

    /*private void OnDrawGizmos()
    {
        if (vertexlisttop == null)
        {
            return;
        }
        for (int i = 0; i < vertexlisttop.Count; i++)
        {
            //Gizmos.color = Color.black;
            //Gizmos.DrawSphere(vertexlisttop[i], 0.1f);
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(vertexlisttop[i], _normals[i]);
        }
    }*/
}




/*int _currentMaxIndex = _lastCorner3;
//UnityEngine.Debug.Log(1 ^ _currentMaxIndex);

int y = 0;
int total = 0;
int _threshold = _currentMaxIndex;

for (int x = 0; x<_currentMaxIndex; x++)
{
if (y > (_threshold - 1))
{
y = 0; //reset
total += x;
}
y += 1;
}
UnityEngine.Debug.Log(y);
UnityEngine.Debug.Log(_currentMaxIndex + "__currentMaxIndex ");
*/


/*var _numberOfVerticesThatAreSharedVertices = _faceCount * 2; // because 2 is the minimum for new faces. whatever = 18 + 4 + 4 so 
float _vertsPerFace = 4;
int x = 0;
int _numberOfFacesThatUseFourNewVertices = 0;
for (; x < _currentMaxIndex; x++)
{
_numberOfFacesThatUseFourNewVertices++;
if (_numberOfFacesThatUseFourNewVertices * _vertsPerFace + _numberOfVerticesThatAreSharedVertices - _currentMaxIndex == 0)
{
break;
}
else
{
_numberOfVerticesThatAreSharedVertices -= 2;
}
}
UnityEngine.Debug.Log(_numberOfFacesThatUseFourNewVertices);
UnityEngine.Debug.Log(_numberOfVerticesThatAreSharedVertices);
*/
