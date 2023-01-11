using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scmaths : MonoBehaviour
{


    public static int LimitInclusiveInt(int value, int min, int max)
    {
        return Math.Min(max, Math.Max(value, min));

    }

    public static float LimitInclusiveFloat(float value, float min, float max)
    {
        return Math.Min(max, Math.Max(value, min));

    }

    //steve chassé note 2021-jan-21
    //it's still a shitty way of doing it. sebastian lague probably doesn't show us all what he can do anyway and the same goes for craig perko and holistic3d and the atomic torch studios even... My whole program, i am trying to make it work for massive instancing of objects in
    //one scene and even different jitter scenes if i can bring the simple JitterDemo Scene instancing inside of this custom C# engine. But learning from those youtuber teachers was the best experience i could've gotten and not having a sebastian lague 3d pathfinding, i had to
    //start with my own strategy. To learn how to do it in 2d in Void Expanse first as the ninekorn modder. So i incorporated what i learned from sebastian lagues tutorial in Unity3d with his old pathfinding tutorial series and i had to understand how to make a per frame 
    //pathfinding solution for javascript ecma5 of void expanse. But then, trying to make covid19 objects to proove i can do close to something that can look like that, i mean someone might like a voxel cubic artist also. Sebastian Lague has the marching cubes and marching
    //squares superioty and i think craig perko and holistic3d can also use marching cubes and marching squares but i still don't want to yet. I am not done with cubic voxels. I am barely trying.

    //and i am trying to 
    //MODIFIED 2D TO 3D VERSION OF SEBASTIEN LAGUE WITH SOME MODS SIMPLY FOR VISUALLY BEING ABLE TO MODIFY TO ELLIPSOID AND OTHER GEOMETRY FORMS - it kinda works but ive got a hard time getting a perfect sphere. im not a mathematician
    //and i am a lazy programmer.
    public static float sc_check_distance_node_3d_geometry(Vector3 nodeA, Vector3 nodeB, float minx, float miny, float minz, float maxx, float maxy, float maxz) // i was thinking about using the index instead and then was like well i need the distance man.
    {
        //var pointFrontX = (1 * Math.cos(radToDeg * Math.PI / 180));
        //var pointFrontY = (1 * Math.sin(radToDeg * Math.PI / 180));

        //SEBASTIEN LAGUE 2D BLUEPRINT FOR NODE DIAGONAL OR NOT DISTANCE.
        /*var dstX = Math.Abs((nodeA.x) - (nodeB.x));
        var dstZ = Math.Abs((nodeA.y) - (nodeB.y));

        if (dstX > dstZ)
        {
            return 14 * dstZ + 10 * (dstX - dstZ);
        }
        return 14 * dstX + 10 * (dstZ - dstX);*/

        var dstX = Math.Abs((nodeA.x) - (nodeB.x));
        var dstY = Math.Abs((nodeA.y) - (nodeB.y));
        var dstZ = Math.Abs((nodeA.z) - (nodeB.z));

        float dstX_vs_dstZ = 0;
        float dstX_vs_dstY = 0;
        float dstY_vs_dstZ = 0;

        if (dstX > dstZ)
        {
            dstX_vs_dstZ = maxx * dstZ + minx * (dstX - dstZ);
        }
        else
        {
            dstX_vs_dstZ = maxx * dstX + minx * (dstZ - dstX);
        }

        if (dstX > dstY)
        {
            dstX_vs_dstY = maxy * dstY + miny * (dstX - dstY);
        }
        else
        {
            dstX_vs_dstY = maxy * dstX + miny * (dstY - dstX);
        }

        if (dstY > dstZ)
        {
            dstY_vs_dstZ = maxz * dstZ + minz * (dstY - dstZ);
        }
        else
        {
            dstY_vs_dstZ = maxz * dstY + minz * (dstZ - dstY);
        }

        return dstX_vs_dstY + dstX_vs_dstZ + dstY_vs_dstZ;
    }



    //MODIFIED 2D TO 3D VERSION OF SEBASTIEN LAGUE WITH SOME MODS SIMPLY FOR VISUALLY BEING ABLE TO MODIFY TO ELLIPSOID AND OTHER GEOMETRY FORMS - it kinda works but ive got a hard time getting a perfect sphere. im not a mathematician
    //and i am a lazy programmer.
    public static float sc_check_distance_node_3d(Vector3 nodeA, Vector3 nodeB, float minx, float miny, float minz, float diagmaxx, float diagmaxy, float diagmaxz, float diagminx, float diagminy, float diagminz) // i was thinking about using the index instead and then was like well i need the distance man.
    {
        //var pointFrontX = (1 * cos(radToDeg * Math.PI / 180));
        //var pointFrontY = (1 * sin(radToDeg * Math.PI / 180));

        //steve chassé's notes: 2021-jan-21
        //SEBASTIEN LAGUE 2D BLUEPRINT FOR NODE DIAGONAL. It works in void expanse too. i am so jealous of those youtubers and how good their tutorials are on youtube and i am barely even to make a decent one. i am barely even able to remove what can be portrayed as 
        //despicable comments in my scripts but that is just my inner anger of not being able to have a 3d blueprint of how to do the same thing. But i think i was able to solve the problem for my planet chunk script. it's really cool but it still lags.
        //var pointFrontX = (1 * Math.cos(radToDeg * Math.PI / 180));
        //var pointFrontY = (1 * Math.sin(radToDeg * Math.PI / 180));

        //SEBASTIEN LAGUE 2D BLUEPRINT FOR NODE DIAGONAL OR NOT DISTANCE.
        /*var dstX = Math.Abs((nodeA.x) - (nodeB.x));
        var dstZ = Math.Abs((nodeA.y) - (nodeB.y));

        if (dstX > dstZ)
        {
            return 14 * dstZ + 10 * (dstX - dstZ);
        }
        return 14 * dstX + 10 * (dstZ - dstX);*/

        var dstX = Math.Abs((nodeA.x) - (nodeB.x));
        var dstY = Math.Abs((nodeA.y) - (nodeB.y));
        var dstZ = Math.Abs((nodeA.z) - (nodeB.z));

        float dstX_vs_dstZ = 0;
        float dstX_vs_dstY = 0;
        float dstY_vs_dstZ = 0;

        if (dstX > dstZ)
        {
            dstX_vs_dstZ = diagmaxx * dstZ + minx * (dstX - dstZ);
        }
        else
        {
            dstX_vs_dstZ = diagminx * dstX + minx * (dstZ - dstX);
        }

        if (dstX > dstY)
        {
            dstX_vs_dstY = diagmaxy * dstY + miny * (dstX - dstY);
        }
        else
        {
            dstX_vs_dstY = diagminy * dstX + miny * (dstY - dstX);
        }

        if (dstY > dstZ)
        {
            dstY_vs_dstZ = diagmaxz * dstZ + minz * (dstY - dstZ);
        }
        else
        {
            dstY_vs_dstZ = diagminz * dstY + minz * (dstZ - dstY);
        }

        return dstX_vs_dstY + dstX_vs_dstZ + dstY_vs_dstZ;
    }

    public static float trying_ellipsoid_with_sc_sebastian_lague_check_distanceconvertedto3dkinda(Vector3 nodeA, Vector3 nodeB)
    {
        //SEBASTIEN LAGUE 2D BLUEPRINT FOR NODE DIAGONAL OR NOT DISTANCE.
        /*var dstX = Math.Abs((nodeA.x) - (nodeB.x));
        var dstZ = Math.Abs((nodeA.y) - (nodeB.y));

        if (dstX > dstZ)
        {
            return 14 * dstZ + 10 * (dstX - dstZ);
        }
        return 14 * dstX + 10 * (dstZ - dstX);*/


        var dstX = Math.Abs((nodeA.x) - (nodeB.x));
        var dstY = Math.Abs((nodeA.y) - (nodeB.y));
        var dstZ = Math.Abs((nodeA.z) - (nodeB.z));

        float dstX_vs_dstZ = 0;
        float dstX_vs_dstY = 0;

        if (dstX > dstZ)
        {
            dstX_vs_dstZ = 14 * dstZ + 10 * (dstX - dstZ);
        }
        else
        {
            dstX_vs_dstZ = 14 * dstX + 10 * (dstZ - dstX);
        }

        if (dstX > dstY)
        {
            dstX_vs_dstY = 14 * dstY + 10 * (dstX - dstY);
        }
        else
        {
            dstX_vs_dstY = 14 * dstX + 10 * (dstY - dstX);
        }

        /*if (dstX_vs_dstY > dstX_vs_dstZ)
        {
            return dstX_vs_dstY;
        }
        else
        {
            return dstX_vs_dstZ;
        }*/

        return dstX_vs_dstY + dstX_vs_dstZ;
    }




    /*
    public float sc_check_distance_sebastian_lague_node_3d()
    {
        if (dstX > dstZ)
        {
            if (dstX > dstY)
            {
                return 14 * dstY + 14 * dstZ + 10 * (dstX - dstZ) + 10 * (dstX - dstY);
            }
            else
            {
                return 14 * dstX + 14 * dstZ + 10 * (dstX - dstZ) + 10 * (dstY - dstX);
            }
        }

        //calculating x
        if (dstX > dstY && dstX > dstZ)
        {
            var part_00 = 14 * dstY + 10 * (dstX - dstY);
            var part_01 = 14 * dstZ + 10 * (dstX - dstZ);
            return part_00 + part_01;
        }
        else if (dstX > dstY && dstX < dstZ)
        {
            var part_00 = 14 * dstY + 10 * (dstX - dstY);
            var part_01 = 14 * dstX + 10 * (dstZ - dstX);
            return part_00 + part_01;
        }
        else if (dstX < dstY && dstX < dstZ)
        {
            var part_00 = 14 * dstX + 10 * (dstY - dstX);
            var part_01 = 14 * dstX + 10 * (dstZ - dstX);
            return part_00 + part_01;
        }
        else if (dstX < dstY && dstX > dstZ)
        {
            var part_00 = 14 * dstX + 10 * (dstY - dstX);
            var part_01 = 14 * dstZ + 10 * (dstX - dstZ);
            return part_00 + part_01;
        }
        //calculating y
        else if (dstY > dstX && dstY > dstZ)
        {
            var part_00 = 14 * dstX + 10 * (dstY - dstX);
            var part_01 = 14 * dstZ + 10 * (dstY - dstZ);
            return part_00 + part_01;
        }
        else if (dstY > dstX && dstY < dstZ)
        {
            var part_00 = 14 * dstX + 10 * (dstY - dstX);
            var part_01 = 14 * dstY + 10 * (dstZ - dstY);
            return part_00 + part_01;
        }
        else if (dstY < dstX && dstY < dstZ)
        {
            var part_00 = 14 * dstY + 10 * (dstX - dstY);
            var part_01 = 14 * dstY + 10 * (dstZ - dstY);
            return part_00 + part_01;
        }
        else if (dstY < dstX && dstY > dstZ)
        {
            var part_00 = 14 * dstY + 10 * (dstX - dstY);
            var part_01 = 14 * dstZ + 10 * (dstY - dstZ);
            return part_00 + part_01;
        }

        //calculating z
        else if (dstZ > dstX && dstZ > dstY)
        {
            var part_00 = 14 * dstX + 10 * (dstZ - dstX);
            var part_01 = 14 * dstY + 10 * (dstZ - dstY);
            return part_00 + part_01;
        }
        else if (dstZ > dstX && dstZ < dstY)
        {
            var part_00 = 14 * dstX + 10 * (dstZ - dstX);
            var part_01 = 14 * dstZ + 10 * (dstY - dstZ);
            return part_00 + part_01;
        }
        else if (dstZ < dstX && dstZ < dstY)
        {
            var part_00 = 14 * dstZ + 10 * (dstX - dstZ);
            var part_01 = 14 * dstZ + 10 * (dstY - dstZ);
            return part_00 + part_01;
        }
        else if (dstZ < dstX && dstZ > dstY)
        {
            var part_00 = 14 * dstZ + 10 * (dstX - dstZ);
            var part_01 = 14 * dstY + 10 * (dstZ - dstY);
            return part_00 + part_01;
        }*/
    //calculating diagonals ? not sure that covers them all. and it doesnt work
    /*else
    {
        var part_00 = 10 * dstX; //14
        var part_01 = 10 * dstY; //14
        var part_02 = 10 * dstZ; //14
        return 10; //part_00 + part_01 + part_02
    }
}*/

    /*
    //found on the gamedevstackexchangeforums or the unity3d forums
    //static System.Random randomer = new System.Random();
    public static double getSomeRandNum(float min, float max)
    {
        randomer = new System.Random();
        var num = (Math.Floor((float)randomer.NextDouble() * max)) + 1; //999999999 // this will get a number between 1 and 99;
        num *= Math.Floor((float)randomer.NextDouble() * 2) == 1 ? 1 : -1; // this will add minus sign in 50% of cases
        if (num == 0)
        {
            return getSomeRandNum(min, max);
        }
        return num * min; // 0.000000001
    }

    //found on the gamedevstackexchangeforums or the unity3d forums
    public static float getSomeRandNumThousandDecimal(float min, float max, float negativeswtchzerofornot)
    {
        randomer = new System.Random();
        var num = Math.Floor((float)randomer.NextDouble() * max) + 1; //999
        num *= Math.Floor((float)randomer.NextDouble() * 2) == 1 ? 1 : -1; // this will add minus sign in 50% of cases
        if (negativeswtchzerofornot == 1)
        {
            if (num == 0)
            {
                return (float)getSomeRandNum(min, max);
            }
        }
        /*else
        {

        }
        return (float)(num * min); //0.001f
    }



    static System.Random randomer = new System.Random();

    public static float getSomeRandNumThousandDecimal(int minNum, int maxNum, float _decimal, int autonegative, int dontfloor)
    {
        float num = 0;
        num = (float)((randomer.NextDouble() * (maxNum - minNum)) + minNum); // this will get a number between 1 and 999;

        return num;

        /*
        if (dontfloor == -1)
        {

            if (autonegative == 1)
            {
                num *= (randomer.NextDouble() * 2) == 1 ? 1 : -1; // this will add minus sign in 50% of cases
            }
        }
        else
        {
            num = (float)(Math.Floor(randomer.NextDouble() * maxNum) + minNum); // this will get a number between 1 and 999;

            if (autonegative == 1)
            {
                num *= Math.Floor(randomer.NextDouble() * 2) == 1 ? 1 : -1; // this will add minus sign in 50% of cases
            }
            else if (autonegative == 2)
            {
                num *= -1;

            }
        }


        if (dontfloor == -1)
        {
            return (float)(num);
        }
        else
        {
            return (float)(num * _decimal);

        }
    }
    */



    //found on the gamedevstackexchangeforums or the unity3d forums
    //static System.Random randomer = new System.Random();
    public static double getSomeRandNum(float min, float max)
    {
        randomer = new System.Random();
        var num = (Math.Floor((float)randomer.NextDouble() * max)) + 1; //999999999 // this will get a number between 1 and 99;
        num *= Math.Floor((float)randomer.NextDouble() * 2) == 1 ? 1 : -1; // this will add minus sign in 50% of cases
        if (num == 0)
        {
            return getSomeRandNum(min, max);
        }
        return num * min; // 0.000000001
    }

    //found on the gamedevstackexchangeforums or the unity3d forums
    public static float getSomeRandNumThousandDecimal(float min, float max, float negativeswtchzerofornot)
    {
        randomer = new System.Random();
        var num = Math.Floor((float)randomer.NextDouble() * max) + 1; //999
        num *= Math.Floor((float)randomer.NextDouble() * 2) == 1 ? 1 : -1; // this will add minus sign in 50% of cases
        if (negativeswtchzerofornot == 1)
        {
            if (num == 0)
            {
                return (float)getSomeRandNum(min, max);
            }
        }
        /*else
        {

        }*/
        return (float)(num * min); //0.001f
    }

    static System.Random randomer = new System.Random();

    public static float getSomeRandNumThousandDecimal(int minNum, int maxNum, float _decimal, int autonegative)
    {
        var num = Math.Floor(randomer.NextDouble() * maxNum) + minNum; // this will get a number between 1 and 999;

        if (autonegative == 1)
        {
            num *= Math.Floor(randomer.NextDouble() * 2) == 1 ? 1 : -1; // this will add minus sign in 50% of cases
        }
        return (float)(num * _decimal);
    }
    public static float signedAngle(Vector2 a, Vector2 b)
    {
        return (float)(Math.Atan2(b.y - a.y, b.x - a.x) * (180 / Math.PI));
    }

}
