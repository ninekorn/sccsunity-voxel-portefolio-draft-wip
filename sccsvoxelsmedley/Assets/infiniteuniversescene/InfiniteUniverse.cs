using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

public class smallChunk
{
    public Vector3 worldPosition;
    public GameObject chunkz;

    public smallChunk(Vector3 worldPos, GameObject chunk)
    {
        worldPosition = worldPos;
        chunkz = chunk;
    }
}

public class bigChunk
{
    public Vector3 worldAreaPosition;
    public GameObject areaChunkz;
    public smallChunk[,,] chunkerList;

    public bigChunk(Vector3 worldPos, GameObject chunk, smallChunk[,,] chunkList)
    {
        worldAreaPosition = worldPos;
        areaChunkz = chunk;
        chunkerList = chunkList;
    }
}

public class InfiniteUniverse : MonoBehaviour
{

    public GameObject objecter;
    public GameObject chunkCube;
    public GameObject chunkArea;

    GameObject chunkParentCube;

    public InfiniteUniverse currentInfiniteUniverse;

    public float speed;

    int chunkWidthXZ = 4;
    int chunkWidth = 12;
    float planeSize = 0.125f;
    int chunkWidthSquared = 144;

    int chunkTinyWidth = 3;
    int chunkTinyHeight = 144;
    int chunkTinyDepth = 3;

    float xPosition;
    float yPosition;
    float zPosition;

    float xPose;
    float yPose;
    float zPose;

    float areaPosX;
    float areaPosY;
    float areaPosZ;

    int XAreaPosfloor;
    int YAreaPosfloor;
    int ZAreaPosfloor;
    smallChunk[,,] chunkSmall;
    bigChunk[,,] chunkArear;

    bool starter = false;
    bool isMoving = false;

    public Vector3 fakePosition;
    public Vector3 currentRealPos;
    public ScreenBounds infini;
    Vector3 IAmMoving;

    void Start()
    {
        currentInfiniteUniverse = this;
        //chunkSmall = new smallChunk[3, chunkWidth, 3];
        chunkArear = new bigChunk[chunkWidth + chunkWidth, chunkWidth, chunkWidth + chunkWidth];
    }

    void Update()
    {
        StartCoroutine(CheckMoving());

        IAmMoving = ScreenBounds.currentScreenBounds.amIMoving;
        currentRealPos = ScreenBounds.currentScreenBounds.realPosition;
        fakePosition = ScreenBounds.currentScreenBounds.fakePosition;

        //xPosition = transform.position.x;
        //yPosition = transform.position.y;
        //zPosition = transform.position.z;
        xPosition = fakePosition.x;
        yPosition = fakePosition.y;
        zPosition = fakePosition.z;

        xPose = Mathf.Floor(xPosition / chunkWidthXZ) * chunkWidthXZ;
        yPose = Mathf.Floor(yPosition / planeSize) * planeSize;
        zPose = Mathf.Floor(zPosition / chunkWidthXZ) * chunkWidthXZ;

        areaPosX = xPosition / chunkWidth;
        areaPosY = yPosition / chunkWidth;
        areaPosZ = zPosition / chunkWidth;

        float areaPosXX = xPosition;
        float areaPosYY = yPosition;
        float areaPosZZ = zPosition;

        XAreaPosfloor = (int)Mathf.Floor(areaPosX);
        YAreaPosfloor = (int)Mathf.Floor(areaPosY);
        ZAreaPosfloor = (int)Mathf.Floor(areaPosZ);

        Vector3 currentPos = new Vector3(xPose, yPose, zPose);
        Vector3 realPosition = new Vector3(xPosition, yPosition, zPosition);
        Vector3 newAreaChunkPos = new Vector3(XAreaPosfloor, YAreaPosfloor, ZAreaPosfloor) * chunkWidth;

        if (isMoving)
        {
            StartCoroutine(buildAreaChunks(newAreaChunkPos));
        }
    }

    /*IEnumerator buildSmallChunk(bigChunk areachunker)
    {
        //chunkParentCube = new GameObject();
        for (float xiii = 0; xiii < 4; xiii += chunkWidthXZ)
        {
            for (float yiii = 0; yiii < planeSize; yiii += planeSize)
            {
                for (float ziii = 0; ziii < 4; ziii += chunkWidthXZ)
                {
                    Vector3 colissdecriss3 = new Vector3(xiii, yiii, ziii);
                    Vector3 realPos3 = colissdecriss3 + new Vector3(xPose, yPose, zPose);

                    //Debug.Log(realPos3);
                    int xPoser;
                    int yPoser;
                    int zPoser;

                    getTheBytes(realPos3, areachunker, out xPoser, out yPoser, out zPoser);

                    if (xPoser >= 0 && yPoser >= 0 && zPoser >= 0)
                    {
                        if (areachunker.chunkerList[xPoser, yPoser, zPoser] == null)
                        {
                            GameObject newChunk = (GameObject)Instantiate(chunkCube, realPos3, Quaternion.identity);
                            areachunker.chunkerList[xPoser, yPoser, zPoser] = new smallChunk(realPos3, newChunk);
                            newChunk.transform.parent = areachunker.areaChunkz.transform;
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
    }*/

    IEnumerator buildAreaChunks(Vector3 areaPos)
    {
        for (float xiii = -chunkWidth; xiii < chunkWidth+ chunkWidth; xiii += chunkWidth)
        {
            for (float yiii = 0; yiii < chunkWidth; yiii += chunkWidth)
            {
                for (float ziii = -chunkWidth; ziii < chunkWidth + chunkWidth; ziii += chunkWidth)
                {
                    Vector3 colissdecriss3 = new Vector3(xiii, yiii, ziii);
                    Vector3 realPos3 = colissdecriss3 + new Vector3(areaPos.x, areaPos.y, areaPos.z);

                    int xPoser;
                    int yPoser;
                    int zPoser;

                    //getTheAreaBytes(realPos3, out xPoser, out yPoser, out zPoser);
                    getTheAreaArrayPos(realPos3, out xPoser, out yPoser, out zPoser);

                    if (xPoser >= 0 && yPoser >= 0 && zPoser >= 0)
                    {
                        if (chunkArear[xPoser, yPoser, zPoser] == null)
                        {
                            smallChunk[,,] fuckinchunkeranus = new smallChunk[3, chunkTinyHeight, 3];
                            GameObject newChunk = (GameObject)Instantiate(chunkArea, realPos3, Quaternion.identity);
                            chunkArear[xPoser, yPoser, zPoser] = new bigChunk(realPos3, newChunk, fuckinchunkeranus);
                        }
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
    }



    /*IEnumerator buildAreaChunks(Vector3 areaPos)
    {
        for (float xiii = 0; xiii < chunkWidth+chunkWidth; xiii += chunkWidth)
        {
            for (float yiii = 0; yiii < chunkWidth + chunkWidth; yiii += chunkWidth)
            {
                for (float ziii = 0; ziii < chunkWidth + chunkWidth; ziii += chunkWidth)
                {
                    Vector3 colissdecriss3 = new Vector3(xiii, yiii, ziii);
                    Vector3 realPos3 = colissdecriss3 + new Vector3(areaPos.x, areaPos.y, areaPos.z);

                    int xPoser;
                    int yPoser;
                    int zPoser;

                    getTheAreaBytes(realPos3, out xPoser, out yPoser, out zPoser);

                    if (chunkArear[xPoser, yPoser, zPoser] == null)
                    {
                        smallChunk[,,] fuckinchunkeranus = new smallChunk[3, chunkTinyHeight, 3];
                        GameObject newChunk = (GameObject)Instantiate(chunkArea, realPos3, Quaternion.identity);
                        chunkArear[xPoser, yPoser, zPoser] = new bigChunk(realPos3, newChunk, fuckinchunkeranus);
                        //StartCoroutine(buildSmallChunk(chunkArear[xPoser, yPoser, zPoser]));
                    }

                    else
                    {
                        //StartCoroutine(buildSmallChunk(chunkArear[xPoser, yPoser, zPoser]));
                    }
                }
            }
        }
        yield return new WaitForSeconds(0.1f);
    }*/



    public bigChunk getDaAreaChunk(bigChunk[,,] chuk, int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (x >= chunkWidth) || (y >= chunkWidth) || (z >= chunkWidth))
        {
            Debug.Log("outside range");
            return null;
        }
        return chuk[x, y, z];
    }

    public smallChunk getDaChunk(smallChunk[,,] chuk, int x, int y, int z)
    {
        if ((x < 0) || (y < 0) || (z < 0) || (x >= chunkTinyWidth) || (y >= chunkTinyHeight) || (z >= chunkTinyDepth))
        {
            Debug.Log("outside range");
            return null;
        }
        return chuk[x, y, z];
    }

    public void getTheAreaBytes(Vector3 chunkPos, out int xPose, out int yPose, out int zPose)
    {
        int xPos = -1;
        int yPos = -1;
        int zPos = -1;

        xPose = xPos;
        yPose = yPos;
        zPose = zPos;

        int x = (int)chunkPos.x;
        int y = (int)chunkPos.y;
        int z = (int)chunkPos.z;

        if (x >= 0 && x < chunkWidthSquared)
        {
            int yo = (int)Mathf.Floor(x / chunkWidth);
            xPos = yo;
        }
        if (x >= chunkWidthSquared)
        {
            int yo = (int)Mathf.Floor(x / chunkWidthSquared);
            int yo1 = yo * chunkWidthSquared;
            xPos = (int)Mathf.Floor((x - (yo1)) / chunkWidth);
        }

        if (x < 0 && x > -chunkWidthSquared)
        {
            int yo = (int)Mathf.Floor(-x / chunkWidth);
            xPos = yo + chunkWidth;
        }
        if (x <= -chunkWidthSquared)
        {
            int yo = (int)Mathf.Floor(-x / chunkWidthSquared);
            int yo1 = yo * chunkWidthSquared;
            xPos = (int)Mathf.Floor((-x - (yo1)) / chunkWidth) + chunkWidth;
        }

        if (y >= 0 && y < chunkWidthSquared)
        {
            int yo = (int)Mathf.Floor(y / chunkWidth);
            yPos = yo;
        }
        if (y >= chunkWidthSquared)
        {
            int yo = (int)Mathf.Floor(y / chunkWidthSquared);
            int yo1 = yo * chunkWidthSquared;
            yPos = (int)Mathf.Floor((y - (yo1)) / chunkWidth);
        }


        if (z >= 0 && z < chunkWidthSquared)
        {
            int yo = (int)Mathf.Floor(z / chunkWidth);
            zPos = yo;
        }
        if (z >= chunkWidthSquared)
        {
            int yo = (int)Mathf.Floor(z / chunkWidthSquared);
            int yo1 = yo * chunkWidthSquared;
            zPos = (int)Mathf.Floor((z - (yo1)) / chunkWidth);
        }


        if (z < 0 && z > -chunkWidthSquared)
        {
            int yo = (int)Mathf.Floor(-z / chunkWidth);
            zPos = yo + chunkWidth;
        }
        if (z <= -chunkWidthSquared)
        {
            int yo = (int)Mathf.Floor(-z / chunkWidthSquared);
            int yo1 = yo * chunkWidthSquared;
            zPos = (int)Mathf.Floor((-z - (yo1)) / chunkWidth) + chunkWidth;
        }

        xPose = xPos;
        yPose = yPos;
        zPose = zPos;

    }


    int multiplicatorX = 0;
    int othermultiplicatorX = 0;
    bool cancelerX = false;

    int multiplicatorY = 0;
    int othermultiplicatorY = 0;
    bool cancelerY = false;

    int multiplicatorZ = 0;
    int othermultiplicatorZ = 0;
    bool cancelerZ = false;






    public void getTheAreaArrayPos(Vector3 chunkPos, out int xPose, out int yPose, out int zPose)
    {
        multiplicatorX = 0;
        othermultiplicatorX = 0;
        cancelerX = false;

        multiplicatorY = 0;
        othermultiplicatorY = 0;
        cancelerY = false;

        multiplicatorZ = 0;
        othermultiplicatorZ = 0;
        cancelerZ = false;

        int xPos = -1;
        int yPos = -1;
        int zPos = -1;
        xPose = xPos;
        yPose = yPos;
        zPose = zPos;

        float x = chunkPos.x;
        float y = chunkPos.y;
        float z = chunkPos.z;


        while (cancelerX == false)
        {
            if (x == 0)
            {
                xPos = 0;
                cancelerX = true;
                break;
            }
            if (x != 0)
            {
                if (multiplicatorX * chunkWidth == x)
                {
                    if (x < chunkWidthSquared)
                    {
                        xPos = multiplicatorX;
                        cancelerX = true;
                        break;
                    }
                    if (x >= chunkWidthSquared)
                    {

                        if (x % chunkWidthSquared == 0)
                        {
                            xPos = 0;
                            cancelerX = true;
                            break;
                        }
                        else
                        {
                            while (true)
                            {
                                if (othermultiplicatorX * chunkWidth % chunkWidthSquared == 0)
                                {
                                    float ajeaisj = othermultiplicatorX * chunkWidth;
                                    xPos = (int)((x - ajeaisj) / chunkWidth);
                                    cancelerX = true;
                                    break;
                                }
                                else
                                {
                                    othermultiplicatorX--;
                                }
                            }
                        }
                    }
                    break;
                }
                if (multiplicatorX * chunkWidth == -x)
                {
                    if (-x < chunkWidthSquared)
                    {
                        xPos = multiplicatorX + chunkWidth;
                        cancelerX = true;
                        break;
                    }
                    if (-x >= chunkWidthSquared)
                    {

                        if (-x % chunkWidthSquared == 0)
                        {
                            xPos = 0;
                            cancelerX = true;
                            break;
                        }
                        else
                        {
                            while (true)
                            {
                                if (othermultiplicatorX * chunkWidth % chunkWidthSquared == 0)
                                {
                                    float ajeaisj = othermultiplicatorX * chunkWidth;
                                    xPos = (int)((-x - ajeaisj) / chunkWidth) + chunkWidth;
                                    cancelerX = true;
                                    break;
                                }
                                else
                                {
                                    othermultiplicatorX--;
                                }
                            }
                        }
                    }
                    break;
                }
                else
                {
                    multiplicatorX++;
                    othermultiplicatorX = multiplicatorX;
                }
            }
        }


        while (cancelerY == false)
        {
            if (y == 0)
            {
                yPos = 0;
                cancelerY = true;
                break;
            }
            if (y != 0)
            {
                if (multiplicatorY * chunkWidth == y)
                {
                    if (y < chunkWidthSquared)
                    {
                        yPos = multiplicatorY;
                        cancelerY = true;
                        break;
                    }
                    if (y >= chunkWidthSquared)
                    {

                        if (y % chunkWidthSquared == 0)
                        {
                            yPos = 0;
                            cancelerY = true;
                            break;
                        }
                        else
                        {
                            while (true)
                            {
                                if (othermultiplicatorY * chunkWidth % chunkWidthSquared == 0)
                                {
                                    float ajeaisj = othermultiplicatorY * chunkWidth;
                                    yPos = (int)((y - ajeaisj) / chunkWidth);
                                    cancelerY = true;
                                    break;
                                }
                                else
                                {
                                    othermultiplicatorY--;
                                }
                            }
                        }
                    }
                    break;
                }
                if (multiplicatorY * chunkWidth == -y)
                {
                    if (-y < chunkWidthSquared)
                    {
                        yPos = multiplicatorY + chunkWidth;
                        cancelerY = true;
                        break;
                    }
                    if (-y >= chunkWidthSquared)
                    {

                        if (-y % chunkWidthSquared == 0)
                        {
                            yPos = 0;
                            cancelerY = true;
                            break;
                        }
                        else
                        {
                            while (true)
                            {
                                if (othermultiplicatorY * chunkWidth % chunkWidthSquared == 0)
                                {
                                    float ajeaisj = othermultiplicatorY * chunkWidth;
                                    yPos = (int)((-y - ajeaisj) / chunkWidth) + chunkWidth;
                                    cancelerY = true;
                                    break;
                                }
                                else
                                {
                                    othermultiplicatorY--;
                                }
                            }
                        }
                    }
                    break;
                }
                else
                {
                    multiplicatorY++;
                    othermultiplicatorY = multiplicatorY;
                }
            }
        }


        while (cancelerZ == false)
        {
            if (z == 0)
            {
                zPos = 0;
                cancelerZ = true;
                break;
            }
            if (z != 0)
            {
                if (multiplicatorZ * chunkWidth == z)
                {
                    if (z < chunkWidthSquared)
                    {
                        zPos = multiplicatorZ;
                        cancelerZ = true;
                        break;
                    }
                    if (z >= chunkWidthSquared)
                    {

                        if (z % chunkWidthSquared == 0)
                        {
                            zPos = 0;
                            cancelerZ = true;
                            break;
                        }
                        else
                        {
                            while (true)
                            {
                                if (othermultiplicatorZ * chunkWidth % chunkWidthSquared == 0)
                                {
                                    float ajeaisj = othermultiplicatorZ * chunkWidth;
                                    zPos = (int)((z - ajeaisj) / chunkWidth);
                                    cancelerZ = true;
                                    break;
                                }
                                else
                                {
                                    othermultiplicatorZ--;
                                }
                            }
                        }
                    }
                    break;
                }
                if (multiplicatorZ * chunkWidth == -z)
                {
                    if (-z < chunkWidthSquared)
                    {
                        zPos = multiplicatorZ + chunkWidth;
                        cancelerZ = true;
                        break;
                    }
                    if (-z >= chunkWidthSquared)
                    {

                        if (-z % chunkWidthSquared == 0)
                        {
                            zPos = 0;
                            cancelerZ = true;
                            break;
                        }
                        else
                        {
                            while (true)
                            {
                                if (othermultiplicatorZ * chunkWidth % chunkWidthSquared == 0)
                                {
                                    float ajeaisj = othermultiplicatorZ * chunkWidth;
                                    zPos = (int)((-z - ajeaisj) / chunkWidth) + chunkWidth;
                                    cancelerZ = true;
                                    break;
                                }
                                else
                                {
                                    othermultiplicatorZ--;
                                }
                            }
                        }
                    }
                    break;
                }
                else
                {
                    multiplicatorZ++;
                    othermultiplicatorZ = multiplicatorZ;
                }
            }
        }
        xPose = xPos;
        yPose = yPos;
        zPose = zPos;
    }






















    ///////////////////////////////////////////////ITS GONNA LAG WHEN PLAYER REACHES HIGH POSITIONS BECAUSE THE WHILE LOOPS WILL HAVE TO ITERATE A LONG TIME... i guess
    public void getTheBytes(Vector3 chunkPos, bigChunk areachunker, out int xPose, out int yPose, out int zPose)
    {
        multiplicatorX = 0;
        othermultiplicatorX = 0;
        cancelerX = false;

        multiplicatorY = 0;
        othermultiplicatorY = 0;
        cancelerY = false;

        multiplicatorZ = 0;
        othermultiplicatorZ = 0;
        cancelerZ = false;

        int xPos = -1;
        int yPos = -1;
        int zPos = -1;
        xPose = xPos;
        yPose = yPos;
        zPose = zPos;
        Vector3 areaPos = areachunker.worldAreaPosition;

        float x = chunkPos.x;
        float y = chunkPos.y;
        float z = chunkPos.z;

        int currentXAreaPos = (int)Mathf.Floor(x / chunkWidth);
        int currentXAreaPosC = currentXAreaPos * chunkWidth;

        int currentYAreaPos = (int)Mathf.Floor(y / chunkWidth);
        int currentYAreaPosC = currentYAreaPos * chunkWidth;

        int currentZAreaPos = (int)Mathf.Floor(z / chunkWidth);
        int currentZAreaPosC = currentZAreaPos * chunkWidth;

        if (areaPos == new Vector3(currentXAreaPosC, currentYAreaPosC, currentZAreaPosC))
        {

            while (cancelerX == false)
            {
                if (x == 0)
                {
                    xPos = 0;
                    cancelerX = true;
                    break;
                }
                if (x != 0)
                {
                    if (multiplicatorX * chunkWidthXZ == x)
                    {
                        if (x < chunkWidth)
                        {
                            xPos = multiplicatorX;
                            cancelerX = true;
                            break;
                        }
                        if (x >= chunkWidth)
                        {

                            if (x % chunkWidth == 0)
                            {
                                xPos = 0;
                                cancelerX = true;
                                break;
                            }
                            else
                            {
                                while (true)
                                {
                                    if (othermultiplicatorX * chunkWidthXZ % chunkWidth == 0)
                                    {
                                        float ajeaisj = othermultiplicatorX * chunkWidthXZ;
                                        xPos = (int)((x - ajeaisj) / chunkWidthXZ);
                                        cancelerX = true;
                                        break;
                                    }
                                    else
                                    {
                                        othermultiplicatorX--;
                                    }
                                }
                            }
                        }
                        break;
                    }
                    if (multiplicatorX * chunkWidthXZ == -x)
                    {
                        if (-x < chunkWidth)
                        {
                            xPos = multiplicatorX + chunkWidthXZ;
                            cancelerX = true;
                            break;
                        }
                        if (-x >= chunkWidth)
                        {

                            if (-x % chunkWidth == 0)
                            {
                                xPos = 0;
                                cancelerX = true;
                                break;
                            }
                            else
                            {
                                while (true)
                                {
                                    if (othermultiplicatorX * chunkWidthXZ % chunkWidth == 0)
                                    {
                                        float ajeaisj = othermultiplicatorX * chunkWidthXZ;
                                        xPos = (int)((-x - ajeaisj) / chunkWidthXZ) + chunkWidthXZ;
                                        cancelerX = true;
                                        break;
                                    }
                                    else
                                    {
                                        othermultiplicatorX--;
                                    }
                                }
                            }
                        }
                        break;
                    }
                    else
                    {
                        multiplicatorX++;
                        othermultiplicatorX = multiplicatorX;
                    }
                }
            }


            while (cancelerY == false)
            {
                if (y == 0)
                {
                    yPos = 0;
                    cancelerY = true;
                    break;
                }
                if (y != 0)
                {
                    if (multiplicatorY * chunkWidthXZ == y)
                    {
                        if (y < chunkWidth)
                        {
                            yPos = multiplicatorY;
                            cancelerY = true;
                            break;
                        }
                        if (y >= chunkWidth)
                        {

                            if (y % chunkWidth == 0)
                            {
                                yPos = 0;
                                cancelerY = true;
                                break;
                            }
                            else
                            {
                                while (true)
                                {
                                    if (othermultiplicatorY * chunkWidthXZ % chunkWidth == 0)
                                    {
                                        float ajeaisj = othermultiplicatorY * chunkWidthXZ;
                                        yPos = (int)((y - ajeaisj) / chunkWidthXZ);
                                        cancelerY = true;
                                        break;
                                    }
                                    else
                                    {
                                        othermultiplicatorY--;
                                    }
                                }
                            }
                        }
                        break;
                    }
                    if (multiplicatorY * chunkWidthXZ == -y)
                    {
                        if (-y < chunkWidth)
                        {
                            yPos = multiplicatorY + chunkWidthXZ;
                            cancelerY = true;
                            break;
                        }
                        if (-y >= chunkWidth)
                        {

                            if (-y % chunkWidth == 0)
                            {
                                yPos = 0;
                                cancelerY = true;
                                break;
                            }
                            else
                            {
                                while (true)
                                {
                                    if (othermultiplicatorY * chunkWidthXZ % chunkWidth == 0)
                                    {
                                        float ajeaisj = othermultiplicatorY * chunkWidthXZ;
                                        yPos = (int)((-y - ajeaisj) / chunkWidthXZ) + chunkWidthXZ;
                                        cancelerY = true;
                                        break;
                                    }
                                    else
                                    {
                                        othermultiplicatorY--;
                                    }
                                }
                            }
                        }
                        break;
                    }
                    else
                    {
                        multiplicatorY++;
                        othermultiplicatorY = multiplicatorY;
                    }
                }
            }


            while (cancelerZ == false)
            {
                if (z == 0)
                {
                    zPos = 0;
                    cancelerZ = true;
                    break;
                }
                if (z != 0)
                {
                    if (multiplicatorZ * chunkWidthXZ == z)
                    {
                        if (z < chunkWidth)
                        {
                            zPos = multiplicatorZ;
                            cancelerZ = true;
                            break;
                        }
                        if (z >= chunkWidth)
                        {

                            if (z % chunkWidth == 0)
                            {
                                zPos = 0;
                                cancelerZ = true;
                                break;
                            }
                            else
                            {
                                while (true)
                                {
                                    if (othermultiplicatorZ * chunkWidthXZ % chunkWidth == 0)
                                    {
                                        float ajeaisj = othermultiplicatorZ * chunkWidthXZ;
                                        zPos = (int)((z - ajeaisj) / chunkWidthXZ);
                                        cancelerZ = true;
                                        break;
                                    }
                                    else
                                    {
                                        othermultiplicatorZ--;
                                    }
                                }
                            }
                        }
                        break;
                    }
                    if (multiplicatorZ * chunkWidthXZ == -z)
                    {
                        if (-z < chunkWidth)
                        {
                            zPos = multiplicatorZ + chunkWidthXZ;
                            cancelerZ = true;
                            break;
                        }
                        if (-z >= chunkWidth)
                        {

                            if (-z % chunkWidth == 0)
                            {
                                zPos = 0;
                                cancelerZ = true;
                                break;
                            }
                            else
                            {
                                while (true)
                                {
                                    if (othermultiplicatorZ * chunkWidthXZ % chunkWidth == 0)
                                    {
                                        float ajeaisj = othermultiplicatorZ * chunkWidthXZ;
                                        zPos = (int)((-z - ajeaisj) / chunkWidthXZ) + chunkWidthXZ;
                                        cancelerZ = true;
                                        break;
                                    }
                                    else
                                    {
                                        othermultiplicatorZ--;
                                    }
                                }
                            }
                        }
                        break;
                    }
                    else
                    {
                        multiplicatorZ++;
                        othermultiplicatorZ = multiplicatorZ;
                    }
                }
            }
            xPose = xPos;
            yPose = yPos;
            zPose = zPos;
        }
    }

    IEnumerator CheckMoving()
    {
        Vector3 startPos = currentRealPos;
        yield return new WaitForSeconds(0.01f);
        Vector3 finalPos = currentRealPos;
        if (startPos.x != finalPos.x || startPos.y != finalPos.y
            || startPos.z != finalPos.z)
        {
            isMoving = true;
        }

        else if (startPos.x == finalPos.x && startPos.y == finalPos.y
             && startPos.z == finalPos.z)
        {
            isMoving = false;
        }
    }
}
