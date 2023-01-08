using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class sccsikplayer : MonoBehaviour
{
    public GameObject upperarm;
    public GameObject lowerarm;

    public GameObject torsopivotemptobj;

    public GameObject upperneckpivot;


    //GameObject upperarmpivotemptobj;
    GameObject lowerarmpivotemptobj;


    GameObject targetpivotposition;
    GameObject elbowtargetpivotposition;

    public GameObject handfoot;



    /*
    public Transform shoulder;
    public Transform upperArm;
    public Transform foreArm;
    public Transform hand;
    public Transform elbowTarget;
    public Transform handTarget;

    public Transform endHand;
    public Transform endForeArm;*/


    float lengthUpperArm;
    float lengthForeArm;
    float lengthHand;
    float lengthFromUpperToHand;
    float lengthFromUpperToTarget;
    public Vector2 polarStuff;

    Vector3 offsetFromUpperToElbowTarget;
    float angle;
    public GameObject rotationPoint;
    float lengthFromUpperToEndForeArm;
    public GameObject HandParentObject;
    Vector3 previousPos;

    Vector3 dirFromUpperArmToHandTarget;
    Vector3 dirUpperArmToElbowTarget;
    Vector3 dirElbowTargetToHandTarget;
    float totalArmLength;
    float targetDistance;

    Vector3 endPoint3;
    Vector3 endPoint4;
    Vector3 crosssss;
    Vector3 crosser;
    GameObject tophierarchy;


    // Start is called before the first frame update
    void Start()
    {
        /*GameObject tophierarchy = new GameObject();
        tophierarchy.transform.position = this.transform.position;
        tophierarchy.transform.parent = this.transform;
        tophierarchy.transform.name = "ikarmtophierarchy";*/


        upperneckpivot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        upperneckpivot.transform.position = this.transform.position;// + (-Vector3.up);
        upperneckpivot.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        upperneckpivot.transform.name = "upperneckpivot";



        torsopivotemptobj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        torsopivotemptobj.transform.position = this.transform.position + (-Vector3.up);
        torsopivotemptobj.transform.localScale = new Vector3(0.5f, 1, 0.1f);
        torsopivotemptobj.transform.name = "torsopivotemptobj";





        upperarm = GameObject.CreatePrimitive(PrimitiveType.Cube);
        upperarm.transform.position = this.transform.position + (-Vector3.up * 1.5f);
        upperarm.transform.position = upperarm.transform.position + (Vector3.right * 0.25f);
        upperarm.transform.position = upperarm.transform.position + (Vector3.forward * 0.5f);

        upperarm.transform.localScale = new Vector3(0.1f,0.1f,1.0f);
        upperarm.transform.name = "upperarm";



        tophierarchy = new GameObject();////GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //upperarmpivotemptobj.transform.position = upperarm.transform.position  + (Vector3.up * 0.5f);
        tophierarchy.transform.position = upperarm.transform.position + (-Vector3.forward * 0.5f);

        //upperarmpivotemptobj.transform.position = upperarmpivotemptobj.transform.position + (Vector3.right * 0.25f);
        tophierarchy.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        tophierarchy.transform.name = "upperarmpivotemptobj";


        lowerarm = GameObject.CreatePrimitive(PrimitiveType.Cube);
        lowerarm.transform.position = upperarm.transform.position;// + (-Vector3.up * 1);
        lowerarm.transform.position = lowerarm.transform.position + (Vector3.forward * 1.0f);


        //lowerarm.transform.position = lowerarm.transform.position + (Vector3.right);
        lowerarm.transform.localScale = new Vector3(0.1f, 0.1f, 1.0f);
        lowerarm.transform.name = "lowerarm";





        lowerarmpivotemptobj = new GameObject();// GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //lowerarmpivotemptobj.transform.position = lowerarm.transform.position + (Vector3.up * 0.5f);
        //upperarmpivotemptobj.transform.position = upperarmpivotemptobj.transform.position + (Vector3.right * 0.25f);
        lowerarmpivotemptobj.transform.position = upperarm.transform.position + (Vector3.forward * 0.5f);
        lowerarmpivotemptobj.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        lowerarmpivotemptobj.transform.name = "lowerarmpivotemptobj";




        targetpivotposition = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        targetpivotposition.transform.position = lowerarm.transform.position + (-Vector3.up * 0.5f);
        //targetpivotposition.transform.position = targetpivotposition.transform.position + (Vector3.forward * 0.5f);
        //lowerarm.transform.position = lowerarm.transform.position + (Vector3.right);
        targetpivotposition.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        targetpivotposition.transform.name = "targetpivotposition";




        handfoot = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        //handfoot.transform.position = lowerarm.transform.position + (-Vector3.up * 0.5f);
        handfoot.transform.position = lowerarm.transform.position + (Vector3.forward * 0.5f);
        //lowerarm.transform.position = lowerarm.transform.position + (Vector3.right);
        handfoot.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        handfoot.transform.name = "handfoot";





        elbowtargetpivotposition = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        elbowtargetpivotposition.transform.position = lowerarmpivotemptobj.transform.position + (Vector3.up * 0.5f);
        elbowtargetpivotposition.transform.position = elbowtargetpivotposition.transform.position + (Vector3.right * 0.5f);
        elbowtargetpivotposition.transform.position = elbowtargetpivotposition.transform.position + (Vector3.forward * 0.5f);
        //lowerarm.transform.position = lowerarm.transform.position + (Vector3.right);
        elbowtargetpivotposition.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        elbowtargetpivotposition.transform.name = "elbowtargetpivotposition";




        lengthUpperArm = Vector3.Distance(upperarm.transform.position, lowerarm.transform.position);
        //lengthFromUpperToHand = Vector3.Distance(upperarm.transform.position, endHand.transform.position);
        offsetFromUpperToElbowTarget = elbowtargetpivotposition.transform.position - upperarm.transform.position;
        lengthForeArm = Vector3.Distance(handfoot.transform.position, lowerarmpivotemptobj.transform.position);
        lengthFromUpperToEndForeArm = lengthUpperArm + lengthForeArm;
        //lengthHand = Vector3.Distance(handfoot.position, endHand.position);
        //lengthForeArm += lengthHand;

        totalArmLength = lengthForeArm + lengthUpperArm;
        lengthFromUpperToTarget = Vector3.Distance(upperarm.transform.position, targetpivotposition.transform.position);



        //targetpivotposition.transform.parent = this.transform;
        //elbowtargetpivotposition.transform.parent = this.transform;


        
        tophierarchy.transform.position = tophierarchy.transform.position;

        tophierarchy.transform.parent = tophierarchy.transform;
       
        


        upperarm.transform.parent = tophierarchy.transform;

        lowerarmpivotemptobj.transform.parent = upperarm.transform;

        lowerarm.transform.parent = lowerarmpivotemptobj.transform;

        handfoot.transform.parent = lowerarm.transform;
        




    }




    // Update is called once per frame
    void Update()
    {
        dirFromUpperArmToHandTarget = targetpivotposition.transform.position - upperarm.transform.position;
        dirUpperArmToElbowTarget = elbowtargetpivotposition.transform.position - upperarm.transform.position;
        dirElbowTargetToHandTarget = targetpivotposition.transform.position - elbowtargetpivotposition.transform.position;
        var dirLowerArmToHandTarget = targetpivotposition.transform.position - lowerarm.transform.position;


        float distshoulderToHandtarget = Vector3.Distance(tophierarchy.transform.position, targetpivotposition.transform.position);
        distshoulderToHandtarget = Mathf.Min(distshoulderToHandtarget, totalArmLength - totalArmLength * 0.001f);


        //circlecircleintersect
        float circircinteradjacentX = ((distshoulderToHandtarget * distshoulderToHandtarget) - (lengthForeArm * lengthForeArm) + (lengthUpperArm * lengthUpperArm)) / (2 * distshoulderToHandtarget);

        //pythagore //c2=a2+b2
        float circircinterhalfA = Mathf.Sqrt((lengthUpperArm * lengthUpperArm) - (circircinteradjacentX * circircinteradjacentX));

        Vector3 dircircircinteradjacentX = dirFromUpperArmToHandTarget.normalized * circircinteradjacentX;//getting the vector3 direction from the shoulder to the end of length of circircinteradjacentX

        //rounding
        /*float xnewDir = Mathf.Round(newDirec.x * 100) / 100;
        float ynewDir = Mathf.Round(newDirec.y * 100) / 100;
        float znewDir = Mathf.Round(newDirec.z * 100) / 100;
        Vector3 newDirFloored = new Vector3(xnewDir, ynewDir, znewDir);
        endPoint3 = upperArm.position + (newDirec.normalized * circircinteradjacentX);
        */

        Vector3 locOfPointadjacentX = tophierarchy.transform.position + dircircircinteradjacentX;

        crosssss = Vector3.Cross(dircircircinteradjacentX, dirUpperArmToElbowTarget); ////  

        crosser = -Vector3.Cross(dircircircinteradjacentX, crosssss); ////
        Vector3 elbowposition = locOfPointadjacentX + (crosser.normalized * circircinterhalfA);

        Vector3 upperToElbow = elbowposition - tophierarchy.transform.position;
        
        
        Debug.DrawRay(locOfPointadjacentX, crosser.normalized * 0.15f, Color.red, 0.1f);

        elbowtargetpivotposition.transform.position = locOfPointadjacentX + (crosser.normalized);


        upperToElbow.Normalize();
        dirFromUpperArmToHandTarget.Normalize();


        //for 2D IK
        //upperToElbow.z = 0;
        //dirFromUpperArmToHandTarget.z = 0;

        Quaternion rotation = Quaternion.LookRotation(upperToElbow, dirFromUpperArmToHandTarget);
        //transform.rotation = rotation;

        //Quaternion q = transform.rotation;
        //q.eulerAngles = new Vector3(0, q.eulerAngles.y, q.eulerAngles.z);
        //transform.rotation = q;



        Vector3 ElbowToHand = handfoot.transform.position - lowerarm.transform.position;
        ElbowToHand.Normalize();
        dirLowerArmToHandTarget.Normalize();

        rotation = Quaternion.LookRotation(dirLowerArmToHandTarget, lowerarm.transform.right);
        lowerarm.transform.rotation = rotation;





        /*pointer0.position = elbowposition;
        pointer1.position = shoulder.position + (crosssss);
        pointer2.position = elbowtargetpivotposition.position + (crosssss);

        Debug.DrawRay(pointer1.position, crosssss.normalized * 0.15f, Color.blue, 0.1f);
        Debug.DrawRay(pointer2.position, crosssss.normalized * 0.15f, Color.blue, 0.1f);
        Debug.DrawRay(pointer3.position, crosssss.normalized * 0.15f, Color.blue, 0.1f);*/


        previousPos = transform.position;
    }
}
