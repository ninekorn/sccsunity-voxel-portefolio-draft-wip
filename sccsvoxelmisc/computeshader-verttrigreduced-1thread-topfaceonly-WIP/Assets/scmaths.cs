using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scmaths : MonoBehaviour
{

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

        }*/
    }


}
