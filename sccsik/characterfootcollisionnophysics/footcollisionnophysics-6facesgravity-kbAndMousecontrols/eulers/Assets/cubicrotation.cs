//made by me steve chassé. aka ninekorn aka nine aka 9

//cant make it work when i put my code in a different class?


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubicrotation : MonoBehaviour
{
    public static new cubicrotation instance;

    void Awake()
    {
        instance = this;
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }
    */


    public void setrotation(int indexofmaxvalueofperfacegravity, int indexofmaxvalueofperfacegravitylast , GameObject viewer)
    {

        //if (Mathf.Abs(Mathf.Abs(indexofmaxvalueofperfacegravitydotinvert) - Mathf.Abs(indexofmaxvalueofperfacegravitynextdotinvert)) > 0.5f)


        Vector3 faceposcubic = Vector3.zero;

        if (indexofmaxvalueofperfacegravity == 0 && indexofmaxvalueofperfacegravitylast != indexofmaxvalueofperfacegravity) //t
        {

            Vector3 eulerangles = new Vector3(viewer.transform.localEulerAngles.x, viewer.transform.localEulerAngles.y, viewer.transform.localEulerAngles.z);

            Quaternion q = viewer.transform.rotation;

            float x = q.x;
            float y = q.y;
            float z = q.z;
            float w = q.w;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


            float pitchdeg = scmaths.RadianToDegree(pitchcurrent);
            float yawdeg = scmaths.RadianToDegree(yawcurrent);
            float rolldeg = scmaths.RadianToDegree(rollcurrent);

            ////Debug.Log("pitch:" + pitchdeg);
            //Debug.Log("yawdeg start:" + yawdeg);
            //Debug.Log("rolldeg:" + rolldeg);

            float altyaw = 0;

            if (indexofmaxvalueofperfacegravitylast == 1)
            {
                altyaw = yawdeg;
            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                altyaw = yawdeg;
            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                altyaw = yawdeg;
            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {
                altyaw = yawdeg;
            }




            if (pitchdeg >= 0)
            {
                if (yawdeg >= 0 & yawdeg < 90)
                {
                    altyaw = yawdeg;
                }

                if (yawdeg < 0 & yawdeg >= -90)
                {
                    altyaw = yawdeg;

                    altyaw *= -1;

                    altyaw = 360 - altyaw;
                }
            }

            if (pitchdeg < 0)
            {
                if (yawdeg >= 0 & yawdeg < 90)
                {
                    altyaw = yawdeg;

                    altyaw = 180 - altyaw;
                }

                if (yawdeg < 0 & yawdeg >= -90)
                {
                    altyaw = yawdeg;

                    altyaw *= -1;

                    altyaw = 180 + altyaw;
                }
            }


            if (altyaw >= 0 && altyaw < 90 ||
                altyaw >= 90 && altyaw < 180)
            {
                eulerangles.x = 0;
                eulerangles.z = 0;
            }
            else
            {
                eulerangles.x = 0;
                eulerangles.z = 0;
            }





            if (indexofmaxvalueofperfacegravitylast == 1)
            {
                viewer.transform.rotation = Quaternion.Euler(0, altyaw - 90, 0);
            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                viewer.transform.rotation = Quaternion.Euler(0, altyaw + 90, 0);
            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                viewer.transform.rotation = Quaternion.Euler(0, altyaw, 0);
            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {
                viewer.transform.rotation = Quaternion.Euler(0, altyaw + 180, 0);
            }



            faceposcubic = Vector3.up;
        }
        else if (indexofmaxvalueofperfacegravity == 1 && indexofmaxvalueofperfacegravity != indexofmaxvalueofperfacegravitylast
        )
        //l
        {

            Vector3 eulerangles = new Vector3(viewer.transform.localEulerAngles.x, viewer.transform.localEulerAngles.y, viewer.transform.localEulerAngles.z);


            Quaternion q = viewer.transform.rotation;

            float x = q.x;
            float y = q.y;
            float z = q.z;
            float w = q.w;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


            float pitchdeg = scmaths.RadianToDegree(pitchcurrent);
            float yawdeg = scmaths.RadianToDegree(yawcurrent);
            float rolldeg = scmaths.RadianToDegree(rollcurrent);

            //Debug.Log("pitchdeg:" + pitchdeg);
            //Debug.Log("yawdeg:" + yawdeg);
            //Debug.Log("rolldeg:" + rolldeg);

            float altyaw = 0;



            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                altyaw = rolldeg;

                if (pitchdeg >= 0)
                {



                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }


                }


            }

            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                altyaw = yawdeg;//

                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }



            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {

                altyaw = yawdeg;//

                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }



            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {
                altyaw = rolldeg;//



                if (pitchdeg >= 0)
                {

                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                }

                if (pitchdeg < 0)
                {
                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }


                }

            }



            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                if (altyaw >= 0 && altyaw < 90 ||
                 altyaw >= 90 && altyaw < 180)
                {
                    //Debug.Log("here00");


                    if (altyaw >= 0 && altyaw < 90)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = 0;
                        //Debug.Log("here0");
                        altyaw = 90 - altyaw;
                        altyaw += 90;
                        altyaw += 180;
                    }
                    else if (altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = 0;
                        //Debug.Log("here1");
                        altyaw = 180 - altyaw;
                        altyaw += 180;
                    }
                    else
                    {
                        //Debug.Log("here111");


                    }
                }
                else
                {

                    eulerangles.z = 90;
                    eulerangles.y = 0;

                    if (altyaw >= 180 && altyaw < 270)
                    {
                        //Debug.Log("here2");
                        altyaw = 270 - altyaw;
                        altyaw += 90;
                    }
                    else if (altyaw >= 270 && altyaw < 360)
                    {
                        //Debug.Log("here3");
                        altyaw = 360 - altyaw;


                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {

                if (pitchdeg >= 0)
                {



                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;

                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                            altyaw -= 90;

                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw += 180;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw += 90;


                        }
                    }
                }
                else
                {

                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {


                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;

                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                            altyaw -= 90;

                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;

                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw += 180;

                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;

                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw += 90;


                        }
                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {








                if (pitchdeg >= 0)
                {



                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = 0;
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;

                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                            altyaw -= 90;

                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw += 180;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw += 90;
                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = 0;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;

                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                            altyaw -= 90;

                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;

                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw += 180;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;

                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw += 90;

                        }
                    }
                }


            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {


                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                 altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = 0;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw += 180;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");

                            altyaw = 180 - altyaw;
                            altyaw -= 90;
                            altyaw = 90 - altyaw;
                            altyaw += 180;

                        }
                    }
                    else
                    {

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("reached2");

                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                            altyaw += 180;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //eulerangles.x = 90;
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("reached3");

                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw -= 90;
                            altyaw += 180;

                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {
                        //eulerangles.x = 90;
                        eulerangles.z = 90;
                        eulerangles.y = 0;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw += 180;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");

                            altyaw = 180 - altyaw;
                            altyaw -= 90;
                            altyaw = 90 - altyaw;

                            altyaw += 180;
                        }
                    }
                    else
                    {

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            eulerangles.z = 90;
                            eulerangles.y = 0;

                            altyaw = 270 - altyaw;
                            altyaw = 90 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;

                            //Debug.Log("reached3");

                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 90;

                        }
                    }
                }

            }






            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);

            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);

            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);

            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);
            }

            faceposcubic = -Vector3.right;
        }
        else if (indexofmaxvalueofperfacegravity == 2 && indexofmaxvalueofperfacegravitylast != indexofmaxvalueofperfacegravity) //r
        {

            Vector3 eulerangles = new Vector3(viewer.transform.localEulerAngles.x, viewer.transform.localEulerAngles.y, viewer.transform.localEulerAngles.z);


            Quaternion q = viewer.transform.rotation;

            float x = q.x;
            float y = q.y;
            float z = q.z;
            float w = q.w;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


            float pitchdeg = scmaths.RadianToDegree(pitchcurrent);
            float yawdeg = scmaths.RadianToDegree(yawcurrent);
            float rolldeg = scmaths.RadianToDegree(rollcurrent);

            //Debug.Log("pitchdeg:" + pitchdeg);
            //Debug.Log("yawdeg:" + yawdeg);
            //Debug.Log("rolldeg:" + rolldeg);

            float altyaw = 0;



            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                altyaw = rolldeg;//

                if (pitchdeg >= 0)
                {



                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (rolldeg >= 0 & rolldeg < 90)
                    {
                        altyaw = rolldeg;
                        altyaw = 180 - altyaw;
                    }

                    if (rolldeg < 0 & rolldeg >= -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }


                }



            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {

                altyaw = yawdeg;//

                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }



            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {


                altyaw = yawdeg;//

                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {
                altyaw = rolldeg;//


                if (pitchdeg >= 0)
                {



                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }


                }
            }




            //Debug.Log("altyaw0:" + altyaw);




            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                if (altyaw >= 0 && altyaw < 90 ||
                 altyaw >= 90 && altyaw < 180)
                {
                    eulerangles.z = -90;
                    eulerangles.y = 0;

                    if (altyaw >= 0 && altyaw < 90)
                    {
                        //Debug.Log("here0");

                    }
                    else if (altyaw >= 90 && altyaw < 180)
                    {
                        //Debug.Log("here1");
                        altyaw = 180 - altyaw;
                        altyaw -= 90;
                        altyaw = 90 - altyaw;


                    }
                }
                else
                {

                    if (altyaw >= 180 && altyaw < 270)
                    {
                        eulerangles.z = -90;
                        eulerangles.y = 0;
                        //Debug.Log("here2");
                        altyaw = 270 - altyaw;

                        altyaw = 90 - altyaw;
                        altyaw += 180;
                    }
                    else if (altyaw >= 270 && altyaw < 360)
                    {
                        eulerangles.z = -90;
                        eulerangles.y = 0;

                        //Debug.Log("here3");
                        altyaw = 360 - altyaw;

                        altyaw = 90 - altyaw;
                        altyaw += 180;
                        altyaw += 90;

                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                if (pitchdeg >= 0)
                {

                    if (altyaw >= 0 && altyaw < 90 ||
                altyaw >= 90 && altyaw < 180)
                    {

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;

                        }
                    }
                    else
                    {

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                            altyaw += 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;

                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {


                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                            altyaw = 90 - altyaw;

                            altyaw += 180;

                        }
                    }
                    else
                    {


                        if (altyaw >= 180 && altyaw < 270)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                            altyaw += 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;

                        }
                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {

                if (pitchdeg >= 0)
                {

                    if (altyaw >= 0 && altyaw < 90 ||
                altyaw >= 90 && altyaw < 180)
                    {

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;

                        }
                    }
                    else
                    {
                        eulerangles.z = -90;
                        eulerangles.y = 0;
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                            altyaw += 90;

                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;
                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {


                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 0;
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                            altyaw = 90 - altyaw;

                            altyaw += 180;

                        }
                    }
                    else
                    {
                        eulerangles.z = -90;
                        eulerangles.y = 0;

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                            altyaw += 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;
                        }
                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {




                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                 altyaw >= 90 && altyaw < 180)
                    {

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("reached0");

                            altyaw = 90 - altyaw;
                            altyaw += 90;

                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("reached1");

                            altyaw = 180 - altyaw;

                        }
                    }
                    else
                    {
                        eulerangles.z = -90;
                        eulerangles.y = 0;


                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");

                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");

                            altyaw = 360 - altyaw;
                            altyaw += 180;

                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("reached0");

                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            eulerangles.z = -90;
                            eulerangles.y = 0;
                            //Debug.Log("reached1");

                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        eulerangles.z = -90;
                        eulerangles.y = 0;


                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");

                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");

                            altyaw = 360 - altyaw;
                            altyaw += 180;

                        }
                    }
                }

            }


            //Debug.Log("altyaw1:" + altyaw);

            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);

            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);

            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);

            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {
                viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);
            }




            faceposcubic = Vector3.right;
        }
        else if (indexofmaxvalueofperfacegravity == 3 && indexofmaxvalueofperfacegravitylast != indexofmaxvalueofperfacegravity) //fr
        {
            Vector3 eulerangles = new Vector3(viewer.transform.rotation.eulerAngles.x, viewer.transform.rotation.eulerAngles.y, viewer.transform.rotation.eulerAngles.z);

            Quaternion q = viewer.transform.rotation;

            float x = q.x;
            float y = q.y;
            float z = q.z;
            float w = q.w;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


            float pitchdeg = scmaths.RadianToDegree(pitchcurrent);
            float yawdeg = scmaths.RadianToDegree(yawcurrent);
            float rolldeg = scmaths.RadianToDegree(rollcurrent);
            //yawdeg = rolldeg;


            //Debug.Log("pitch:" + pitchdeg);
            //Debug.Log("yawdeg:" + yawdeg);
            //Debug.Log("rolldeg:" + rolldeg);

            //float altyaw = (360 - yawdeg) - 90;
            float altyaw = 0;// yawdeg;

            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                altyaw = rolldeg;//
                if (pitchdeg >= 0)
                {

                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                }

                if (pitchdeg < 0)
                {
                    if (rolldeg < 0 & rolldeg > -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (rolldeg <= -90 & rolldeg > -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 1)
            {
                altyaw = yawdeg;//
                if (pitchdeg >= 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                    }


                    if (yawdeg < -90 & yawdeg >= -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

                if (pitchdeg < 0)
                {


                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                    }


                    if (yawdeg < -90 & yawdeg >= -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                altyaw = yawdeg;//


                if (pitchdeg >= 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                    }


                    if (yawdeg < -90 & yawdeg >= -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

                if (pitchdeg < 0)
                {


                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                    }


                    if (yawdeg < -90 & yawdeg >= -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {
                altyaw = rolldeg;//

                if (pitchdeg >= 0)
                {
                    if (rolldeg >= 0 & rolldeg < 90)
                    {
                        altyaw = rolldeg;
                    }


                    if (rolldeg < -90 & rolldeg >= -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (rolldeg < 0 & rolldeg >= -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

                if (pitchdeg < 0)
                {


                    if (rolldeg >= 0 & rolldeg < 90)
                    {
                        altyaw = rolldeg;
                    }


                    if (rolldeg < -90 & rolldeg >= -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (rolldeg < 0 & rolldeg >= -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }
            }



            //Debug.Log("altyaw:" + altyaw);


            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                {
                    eulerangles.z = 90;
                    eulerangles.y = 90;

                    if (altyaw >= 0 && altyaw < 90)
                    {
                        //Debug.Log("here0");
                        altyaw = 90 - altyaw;
                    }
                    else if (altyaw >= 90 && altyaw < 180)
                    {
                        //Debug.Log("here1");
                        altyaw = 180 - altyaw;
                        altyaw += 270;
                    }
                }
                else
                {


                    eulerangles.z = -90;
                    eulerangles.y = -90;


                    if (altyaw >= 180 && altyaw < 270)
                    {
                        //Debug.Log("here2");
                        altyaw = 270 - altyaw;
                        altyaw = 90 - altyaw;
                        altyaw += 270;

                    }
                    else if (altyaw >= 270 && altyaw < 360)
                    {
                        //Debug.Log("here3");
                        altyaw = 360 - altyaw;

                        altyaw = 90 - altyaw;

                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 1)
            {
                if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                {

                    if (altyaw >= 0 && altyaw < 90)
                    {
                        if (rolldeg >= 0)
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 90;
                            //Debug.Log("here00");
                            altyaw -= 90;
                        }
                        else
                        {
                            eulerangles.z = 90;
                            eulerangles.y = 90;
                            //Debug.Log("here01");

                            altyaw = 90 - altyaw;
                        }

                    }
                    else if (altyaw >= 90 && altyaw < 180)
                    {
                        //Debug.Log("here1");
                        altyaw = 180 - altyaw;
                        altyaw -= 90;

                    }
                }
                else
                {


                    eulerangles.z = -90;
                    eulerangles.y = -90;


                    if (altyaw >= 180 && altyaw < 270)
                    {

                        altyaw = 270 - altyaw;

                        if (rolldeg >= 0)
                        {
                            //Debug.Log("here21");
                            altyaw = 90 - altyaw;
                            altyaw += 270;


                        }
                        else
                        {
                            //Debug.Log("here22");
                        }

                    }
                    else if (altyaw >= 270 && altyaw < 360)
                    {
                        //Debug.Log("here3");
                        altyaw = 360 - altyaw;

                        altyaw = 90 - altyaw;

                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {

                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = 90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("here0");
                            altyaw = 90 - altyaw;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {


                        eulerangles.z = -90;
                        eulerangles.y = -90;

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw -= 270;
                            altyaw = 90 - altyaw;
                            altyaw += 90;

                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = 90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("here0");
                            altyaw -= 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("here1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        eulerangles.z = -90;
                        eulerangles.y = -90;

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("here2");
                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                            altyaw = 270 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("here3");
                            altyaw = 360 - altyaw;
                            altyaw -= 270;
                            altyaw = 90 - altyaw;
                            altyaw += 90;

                        }
                    }
                }

            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {

                if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                {
                    eulerangles.z = 90;
                    eulerangles.y = 90;

                    if (altyaw >= 0 && altyaw < 90)
                    {
                        //Debug.Log("reached0");
                        altyaw += 90;
                    }
                    else if (altyaw >= 90 && altyaw < 180)
                    {
                        if (rolldeg >= 0)
                        {
                            //Debug.Log("reached12");
                            altyaw = 180 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 180;

                        }
                        else
                        {
                            //Debug.Log("reached13");
                            altyaw = 180 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                            altyaw += 90;
                        }

                    }
                }
                else
                {


                    eulerangles.z = -90;
                    eulerangles.y = -90;

                    if (altyaw >= 180 && altyaw < 270)
                    {


                        //Debug.Log("reached2");
                        altyaw = 270 - altyaw;
                        altyaw = 90 - altyaw;
                        if (rolldeg >= 0)
                        {
                            //Debug.Log("reached21");
                        }
                        else
                        {
                            altyaw += 90;

                            //Debug.Log("reached22");
                        }

                    }
                    else if (altyaw >= 270 && altyaw < 360)
                    {
                        //Debug.Log("reached3");
                        altyaw = 360 - altyaw;

                        altyaw = 90 - altyaw;

                    }
                }
            }







            viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z);


            faceposcubic = Vector3.forward;
        }
        else if (indexofmaxvalueofperfacegravity == 4 && indexofmaxvalueofperfacegravitylast != indexofmaxvalueofperfacegravity)
        //ba
        {
            Vector3 eulerangles = new Vector3(viewer.transform.rotation.eulerAngles.x, viewer.transform.rotation.eulerAngles.y, viewer.transform.rotation.eulerAngles.z);

            Quaternion q = viewer.transform.rotation;

            float x = q.x;
            float y = q.y;
            float z = q.z;
            float w = q.w;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


            float pitchdeg = scmaths.RadianToDegree(pitchcurrent);
            float yawdeg = scmaths.RadianToDegree(yawcurrent);
            float rolldeg = scmaths.RadianToDegree(rollcurrent);
            //yawdeg = rolldeg;

            //Debug.Log("pitch:" + pitchdeg);
            //Debug.Log("yawdeg:" + yawdeg);
            //Debug.Log("rolldeg:" + rolldeg);

            //float altyaw = (360 - yawdeg) - 90;
            float altyaw = 0;// yawdeg;



            if (indexofmaxvalueofperfacegravitylast == 0)
            {
                altyaw = rolldeg;//
                yawdeg = rolldeg;


                if (pitchdeg >= 0)
                {
                    if (rolldeg >= 0 & rolldeg < 90)
                    {
                        altyaw = rolldeg;
                    }

                    if (rolldeg < 0 & rolldeg >= -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 360 - altyaw;

                    }

                    if (rolldeg < -90 & rolldeg >= -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 360 - altyaw;
                    }


                }

                if (pitchdeg < 0)
                {
                    if (rolldeg >= 0 & rolldeg < 90)
                    {
                        altyaw = rolldeg;
                        altyaw = 180 - altyaw;
                    }

                    if (rolldeg < 0 & rolldeg >= -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

            }
            else if (indexofmaxvalueofperfacegravitylast == 1)
            {
                altyaw = yawdeg;//


                if (pitchdeg >= 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 360 - altyaw;

                    }
                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }
                }


            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                altyaw = yawdeg;//


                if (pitchdeg >= 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 360 - altyaw;

                    }
                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }
                }

            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {
                altyaw = rolldeg;//

                if (pitchdeg >= 0)
                {
                    if (rolldeg >= 0 & rolldeg < 90)
                    {
                        altyaw = rolldeg;
                    }


                    if (rolldeg < -90 & rolldeg >= -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (rolldeg < 0 & rolldeg >= -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

                if (pitchdeg < 0)
                {


                    if (rolldeg >= 0 & rolldeg < 90)
                    {
                        altyaw = rolldeg;
                    }


                    if (rolldeg < -90 & rolldeg >= -180)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 270 - altyaw;

                    }

                    if (rolldeg < 0 & rolldeg >= -90)
                    {
                        altyaw = rolldeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

            }









            //Debug.Log("altyaw0:" + altyaw);

            if (indexofmaxvalueofperfacegravitylast == 0)
            {


                if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                {
                    //Debug.Log("here00");
                    eulerangles.z = 90;
                    eulerangles.y = -90;

                    if (altyaw >= 0 && altyaw < 90)
                    {
                        //Debug.Log("here0");
                        altyaw = 90 - altyaw;
                        altyaw += 180;//

                    }
                    else if (altyaw >= 90 && altyaw < 180)
                    {
                        //Debug.Log("here2");
                        altyaw = 180 - altyaw;
                        altyaw += 90;//

                    }
                }
                else
                {
                    //Debug.Log("here11");

                    eulerangles.z = 90;
                    eulerangles.y = -90;


                    if (altyaw >= 180 && altyaw < 270)
                    {
                        //Debug.Log("here2");//
                        altyaw = 270 - altyaw;

                    }
                    else if (altyaw >= 270 && altyaw < 360)
                    {
                        //Debug.Log("here3");
                        altyaw = 360 - altyaw;
                        altyaw += 270;//
                    }
                }

            }
            else if (indexofmaxvalueofperfacegravitylast == 1)
            {
                //Debug.Log("altyaw0:" + altyaw);

                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        ////Debug.Log("reached0");

                        eulerangles.z = 90;
                        eulerangles.y = -90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached00");
                            altyaw = 90 - altyaw;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached01");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        ////Debug.Log("reached1");


                        eulerangles.z = 90;
                        eulerangles.y = -90;


                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached02");

                            altyaw = 180 - altyaw;
                            altyaw += 180;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached03");

                            altyaw = 360 - altyaw;
                            altyaw += 90;
                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        ////Debug.Log("reached2");

                        eulerangles.z = 90;
                        eulerangles.y = -90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached21");

                            altyaw = 90 - altyaw;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached22");
                            altyaw = 180 - altyaw;
                            altyaw += 180;
                        }

                        altyaw += 90;
                    }
                    else
                    {

                        ////Debug.Log("reached3");

                        eulerangles.z = 90;
                        eulerangles.y = -90;

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached31");

                            altyaw = 270 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached32");
                            altyaw = 360 - altyaw;
                        }
                        altyaw += 180;



                    }
                }




            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                ////Debug.Log("altyaw0:" + altyaw);

                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        ////Debug.Log("reached0");
                        eulerangles.z = -90;
                        eulerangles.y = 90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached00");
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached01");
                            altyaw = 180 - altyaw;
                        }

                    }
                    else
                    {
                        ////Debug.Log("reached1");

                        eulerangles.z = -90;
                        eulerangles.y = 90;

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached02");

                            altyaw = 180 - altyaw;
                            altyaw += 180;

                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached03");

                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;

                        }



                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        ////Debug.Log("reached2");

                        eulerangles.z = -90;
                        eulerangles.y = 90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached21");

                            altyaw = 90 - altyaw;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached22");

                            altyaw = 180 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 180;
                        }

                    }
                    else
                    {

                        ////Debug.Log("reached3");

                        eulerangles.z = -90;
                        eulerangles.y = 90;

                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached31");

                            altyaw = 270 - altyaw;
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached32");
                            altyaw = 360 - altyaw;

                        }
                        altyaw += 180;



                    }
                }


            }
            else if (indexofmaxvalueofperfacegravitylast == 5)
            {


                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = -90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw -= 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            if (rolldeg >= 0)
                            {
                                //Debug.Log("reached1");
                                altyaw = 180 - altyaw;
                                altyaw = 90 - altyaw;
                            }
                            else
                            {
                                //Debug.Log("reached1");
                                altyaw = 180 - altyaw;
                                altyaw = 90 - altyaw;
                                altyaw += 90;
                            }

                        }
                    }
                    else
                    {


                        eulerangles.z = 90;
                        eulerangles.y = -90;


                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                            altyaw += 180;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;
                        }
                    }
                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                    altyaw >= 90 && altyaw < 180)
                    {
                        eulerangles.z = 90;
                        eulerangles.y = -90;

                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw -= 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");

                            if (rolldeg >= 0)
                            {
                                //Debug.Log("reached11");
                                altyaw -= 90;

                            }
                            else
                            {
                                //Debug.Log("reached12");
                                altyaw = 180 - altyaw;
                                altyaw = 90 - altyaw;
                                altyaw += 90;
                            }
                        }
                    }
                    else
                    {


                        eulerangles.z = 90;
                        eulerangles.y = -90;


                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                            altyaw += 180;

                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw = 90 - altyaw;
                        }
                    }
                }

            }



            ////Debug.Log("altyaw:" + altyaw);




            viewer.transform.rotation = Quaternion.Euler(altyaw, eulerangles.y, eulerangles.z); //Quaternion.Euler(eulerangles);



            faceposcubic = -Vector3.forward;
        }
        else if (indexofmaxvalueofperfacegravity == 5 && indexofmaxvalueofperfacegravitylast != indexofmaxvalueofperfacegravity) //bo
        {
            Vector3 eulerangles = new Vector3(viewer.transform.localEulerAngles.x, viewer.transform.localEulerAngles.y, viewer.transform.localEulerAngles.z);

            Quaternion q = viewer.transform.rotation;

            float x = q.x;
            float y = q.y;
            float z = q.z;
            float w = q.w;

            //https://answers.unity.com/questions/416169/finding-pitchrollyaw-from-quaternions.html - also found elsewhere
            float rollcurrent = Mathf.Atan2(2 * y * w - 2 * x * z, 1 - 2 * y * y - 2 * z * z);
            float pitchcurrent = Mathf.Atan2(2 * x * w - 2 * y * z, 1 - 2 * x * x - 2 * z * z);
            float yawcurrent = Mathf.Asin(2 * x * y + 2 * z * w);


            float pitchdeg = scmaths.RadianToDegree(pitchcurrent);
            float yawdeg = scmaths.RadianToDegree(yawcurrent);
            float rolldeg = scmaths.RadianToDegree(rollcurrent);

            //Debug.Log("pitch:" + pitchdeg);
            //Debug.Log("yawdeg start:" + yawdeg);
            //Debug.Log("rolldeg:" + rolldeg);

            float altyaw = 0;

            if (indexofmaxvalueofperfacegravitylast == 1)
            {
                altyaw = yawdeg;

                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }


                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                altyaw = yawdeg;

                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                altyaw = yawdeg;


                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }


                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {
                altyaw = yawdeg;

                altyaw = yawdeg;


                if (pitchdeg >= 0)
                {



                    if (yawdeg < 0 & yawdeg > -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }
                    if (yawdeg <= -90 & yawdeg > -180)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;
                        altyaw = 360 - altyaw;
                    }

                }

                if (pitchdeg < 0)
                {
                    if (yawdeg >= 0 & yawdeg < 90)
                    {
                        altyaw = yawdeg;
                        altyaw = 180 - altyaw;
                    }

                    if (yawdeg < 0 & yawdeg >= -90)
                    {
                        altyaw = yawdeg;

                        altyaw *= -1;

                        altyaw = 180 + altyaw;
                    }

                }

            }


















            //Debug.Log("altyaw0:" + altyaw);


            if (altyaw >= 0 && altyaw < 90 ||
                altyaw >= 90 && altyaw < 180)
            {
                eulerangles.x = 0;
                eulerangles.z = 180;
            }
            else
            {
                eulerangles.x = 0;
                eulerangles.z = 180;
            }



            if (indexofmaxvalueofperfacegravitylast == 1)
            {
                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;
                        }
                    }


                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;

                        }
                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;
                        }
                    }


                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;
                        }
                    }

                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;
                        }
                    }


                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;

                        }
                    }
                }
            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {
                if (pitchdeg >= 0)
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;

                        }
                    }


                }
                else
                {
                    if (altyaw >= 0 && altyaw < 90 ||
                        altyaw >= 90 && altyaw < 180)
                    {
                        if (altyaw >= 0 && altyaw < 90)
                        {
                            //Debug.Log("reached0");
                            altyaw = 90 - altyaw;
                            altyaw += 90;
                        }
                        else if (altyaw >= 90 && altyaw < 180)
                        {
                            //Debug.Log("reached1");
                            altyaw = 180 - altyaw;
                        }
                    }
                    else
                    {
                        if (altyaw >= 180 && altyaw < 270)
                        {
                            //Debug.Log("reached2");
                            altyaw = 270 - altyaw;
                            altyaw -= 90;
                        }
                        else if (altyaw >= 270 && altyaw < 360)
                        {
                            //Debug.Log("reached3");
                            altyaw = 360 - altyaw;
                            altyaw += 180;

                        }
                    }
                }
            }

            if (indexofmaxvalueofperfacegravitylast == 1)
            {
                viewer.transform.rotation = Quaternion.Euler(eulerangles.x, altyaw - 90, eulerangles.z);
            }
            else if (indexofmaxvalueofperfacegravitylast == 2)
            {
                viewer.transform.rotation = Quaternion.Euler(eulerangles.x, altyaw + 90, eulerangles.z);
            }
            else if (indexofmaxvalueofperfacegravitylast == 3)
            {
                viewer.transform.rotation = Quaternion.Euler(eulerangles.x, altyaw, eulerangles.z);
            }
            else if (indexofmaxvalueofperfacegravitylast == 4)
            {
                viewer.transform.rotation = Quaternion.Euler(eulerangles.x, altyaw + 180, eulerangles.z);
            }
            faceposcubic = -Vector3.up;
        }


    }
}
