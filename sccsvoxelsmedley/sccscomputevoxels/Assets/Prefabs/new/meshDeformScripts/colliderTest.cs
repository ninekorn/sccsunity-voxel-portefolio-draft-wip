using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class colliderTest : MonoBehaviour
{
    Rigidbody myRigidbody;
    Vector3 previousPos;

    Vector3 currentPos;
    Vector3 movementThisStep;

    public LayerMask layerMask = -1;

    Vector3[] vertices;

    public GameObject collPoint;
    Vector3 velocity;

    bool getVelocity = false;
    Vector3 worldPt;

    bool checkDistanceToCollision = false;
    float currentSpeed = 0;

    Vector3[] rayhits;
    Vector3[] rayhitsNorms;
    Vector3[] listOfVertices;
    float[] vertDistToCol;
    Transform[] objectsCollidingWith;
    int[] triangleIndexHit;
    int[] crapIndex;

    int pos = 0;

    private float minimumExtent;
    private float partialExtent;
    private float sqrMinimumExtent;
    private Collider myCollider;
    public float skinWidth = 0.1f; //probably doesn't need to be changed 


    int getData = 0;

    public GameObject shadowObject;

    RaycastHit rayHit;
    Rigidbody shadowRigid;

    public Vector3 LastKnownVelocity;
    public Vector3 LastKnownAngularVelocity;

    Vector3 startingVelocity;

    Vector3 offsetter1;

    Vector3 startPosition;
    Vector3 endPosition;

    bool getVelocityData = false;
    public collisionatorManager collisionatorManager;
    bool hasReachedTarget = false;
    bool applyingCollisionForceToObjectCollidedWith = false;

    bool updateStuff = false;


    private void Start()
    {
        myRigidbody = transform.GetComponent<Rigidbody>();
        shadowRigid = shadowObject.GetComponent<Rigidbody>();
        previousPos = shadowObject.transform.position;
        vertices = transform.GetComponent<MeshFilter>().mesh.vertices;

        rayhits = new Vector3[vertices.Length];
        rayhitsNorms = new Vector3[vertices.Length];
        listOfVertices = new Vector3[vertices.Length];
        vertDistToCol = new float[vertices.Length];
        objectsCollidingWith = new Transform[vertices.Length];
        triangleIndexHit = new int[vertices.Length*3];
        crapIndex = new int[vertices.Length];

        myCollider = GetComponent<Collider>();
        minimumExtent = Mathf.Min(Mathf.Min(myCollider.bounds.extents.x, myCollider.bounds.extents.y), myCollider.bounds.extents.z);
        partialExtent = minimumExtent * (1.0f - skinWidth);
        sqrMinimumExtent = minimumExtent * minimumExtent;

        startPosition = transform.position;
    }

    int[] SortAndIndex<T>(T[] rg)
    {
        int i, c = rg.Length;
        var keys = new int[c];
        if (c > 1)
        {
            for (i = 0; i < c; i++)
                keys[i] = i;

            System.Array.Sort(rg, keys /*, ... */);
        }
        return keys;
    }


    int counter = 0;
    void Update()
    {

        startPosition = transform.position;
        currentPos = shadowObject.transform.position;
        movementThisStep = shadowObject.transform.position - previousPos;
        velocity = shadowObject.transform.GetComponent<Rigidbody>().velocity;
        float shadowSpeed = velocity.magnitude;
        float movementSqrMagnitude = movementThisStep.sqrMagnitude;

        startingVelocity = shadowObject.transform.GetComponent<Rigidbody>().velocity;
        Vector3 angularVelocityShadow = shadowObject.transform.GetComponent<Rigidbody>().angularVelocity;

        Vector3 realPos = shadowObject.transform.position;


        if (checkDistanceToCollision == false)
        {
            //if (movementSqrMagnitude > sqrMinimumExtent)
            //{
            for (int i = 0; i < vertices.Length; i++)
            {
                worldPt = transform.TransformPoint(vertices[i]);
                if (Physics.Raycast(worldPt, velocity, out rayHit, Mathf.Infinity, layerMask))
                {
                    rayhits[i] = rayHit.point;
                    rayhitsNorms[i] = rayHit.normal;
                    listOfVertices[i] = vertices[i];
                    vertDistToCol[i] = Vector3.Distance(rayHit.point, worldPt);
                    objectsCollidingWith[i] = rayHit.transform;
                    triangleIndexHit[i] = rayHit.triangleIndex;
                    getVelocity = true;
                    checkDistanceToCollision = true;
                }

                /*else
                {
                    transform.position = Vector3.MoveTowards(Vector3.Lerp(transform.position, shadowObject.transform.position, shadowSpeed * Time.deltaTime * 0.5f), shadowObject.transform.position, shadowSpeed * Time.deltaTime * 0.5f);
                    transform.rotation = Quaternion.Lerp(transform.rotation, shadowObject.transform.rotation, angularVelocityShadow.magnitude * 10f * Time.deltaTime);
                }*/
            }
        }

        if (getVelocity == true)
        {
            int[] idx = SortAndIndex(vertDistToCol);

            for (int i = rayhits.Length-1; i >= 0; i--)
            {
                //worldPt = transform.TransformPoint(vertices[i]);
                //float minValue = vertDistToCol.Min();
                //pos = Array.IndexOf(vertDistToCol, minValue);

                if (vertDistToCol[idx[i]] > 0 && rayhits[idx[i]] != Vector3.zero)
                {
                    pos = idx[i];
                }
            }

            /*if (vertDistToCol[idx[i]] > 0 && rayhits[idx[i]] != Vector3.zero)
            {
                pos = idx[i];
            }*/

            //Instantiate(collPoint, worldPt, Quaternion.identity);


            //Debug.DrawLine(transform.position,rayhits[pos],Color.green,1000f);
            //float minValue = vertDistToCol.Min();
            //pos = Array.IndexOf(vertDistToCol, minValue);

            worldPt = transform.TransformPoint(listOfVertices[pos]);
            offsetter1 = rayhits[pos];
            Vector3 objectCenter = transform.position;
            Vector3 offset00 = worldPt - objectCenter;
            offsetter1 -= offset00;
            getVelocity = false;
            hasReachedTarget = false;
            getData = 1;
        }


        if (getData == 1)
        {
            if (applyingCollisionForceToObjectCollidedWith)
            {
                //Instantiate(collPoint, rayhits[pos], Quaternion.identity);
                shadowObject.transform.GetComponent<Rigidbody>().velocity *= 0.85f;
                shadowObject.transform.GetComponent<Rigidbody>().angularVelocity *= 0.75f;
             
                velocity = shadowObject.transform.GetComponent<Rigidbody>().velocity;
                shadowSpeed = velocity.magnitude;
                applyingCollisionForceToObjectCollidedWith = false;
                hasReachedTarget = false;
            }

            if (hasReachedTarget == false)
            {
                if (Vector3.Distance(transform.position, offsetter1) < Vector3.Distance(transform.position, shadowObject.transform.position))
                {
                    transform.position = Vector3.MoveTowards(transform.position, offsetter1, shadowSpeed * Time.deltaTime * 0.97f);
                }
                else
                {
                    transform.position = Vector3.MoveTowards(Vector3.Slerp(transform.position, shadowObject.transform.position, shadowSpeed * Time.deltaTime * 0.97f), offsetter1, shadowSpeed * Time.deltaTime * 0.97f);
                }
                transform.rotation = Quaternion.Lerp(transform.rotation, shadowObject.transform.rotation, angularVelocityShadow.magnitude * 10f * Time.deltaTime);

                //Debug.DrawLine(transform.position,offsetter1,Color.green,10f);
                worldPt = transform.TransformPoint(listOfVertices[pos]);
                Debug.DrawLine(worldPt, rayhits[pos], Color.red, 0.1f);


                if (transform.position == offsetter1)
                {
                    //Debug.Log("reached target");
                    LastKnownVelocity = shadowObject.transform.GetComponent<Rigidbody>().velocity;
                    LastKnownAngularVelocity = shadowObject.transform.GetComponent<Rigidbody>().angularVelocity;
                    collisionatorManager.collisionForceToApply(objectsCollidingWith[pos], rayhits[pos], LastKnownVelocity, movementThisStep,listOfVertices[pos],triangleIndexHit[pos],pos,transform);

                    applyingCollisionForceToObjectCollidedWith = true;
                    hasReachedTarget = true;
                    checkDistanceToCollision = false;
                    updateStuff = true;
                    //shadowObject.transform.position = transform.position;
                    getData = 3;


                    /*if (getVelocityData == false)
                    {
                        Instantiate(collPoint, rayhits[pos], Quaternion.identity);
                      
                        //currentSpeed = LastKnownVelocity.magnitude;
                        //shadowObject.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        //shadowObject.transform.position = transform.position;
                        applyingCollisionForceToObjectCollidedWith = true;

                        //shadowRigid.isKinematic = true;
                        //myRigidbody.isKinematic = true;

                        //getData = 4;
                        /*if (LastKnownVelocity.magnitude > 10)
                        {
                            getData = 2;
                        }
                        else
                        {
                            myRigidbody.isKinematic = false;
                            myRigidbody.velocity = LastKnownVelocity;
                            myRigidbody.angularVelocity = LastKnownAngularVelocity;

                            shadowObject.transform.GetComponent<Rigidbody>().velocity = Vector3.zero;
                            shadowObject.transform.position = transform.position;
                            shadowRigid.isKinematic = true;
                            getData = 3;
                        }
                        hasReachedTarget = true;
                        checkDistanceToCollision = false;
                        //getVelocityData = true;
                        getData = 3;
                    }*/

                }
                else
                {
                    hasReachedTarget = true;
                    checkDistanceToCollision = false;
                    //getData = 3;
                }
            }
        }
        /*if (updateStuff)
        {
            collisionatorManager.collisionForceToApply(objectsCollidingWith[pos], rayhits[pos], LastKnownVelocity, movementThisStep, listOfVertices[pos], triangleIndexHit[pos], pos, transform);
        }
        */

        if (getData == 2)
        {

            //Vector3 reflectedPos = Vector3.Reflect(rayhits[pos], rayhitsNorms[pos]);
            Vector3 reflectedDirection = Vector3.Reflect(LastKnownVelocity, rayhitsNorms[pos]).normalized;

            Debug.DrawLine(rayhits[pos], reflectedDirection+transform.position, Color.red, 10f);

            float mass0 = shadowRigid.mass;
            float d0 = Vector3.Dot(LastKnownVelocity, rayhitsNorms[pos]);
            float j0 = Mathf.Max(-(1 + 0.5f) * d0, 0);

            shadowRigid.velocity += j0 * reflectedDirection * 0.25f;
            shadowRigid.angularVelocity += j0 * reflectedDirection * UnityEngine.Random.Range(1f, 10f);
            
            /*float mass0 = shadowRigid.mass;
            float d0 = Vector3.Dot(LastKnownVelocity, rayhitsNorms[pos]);
            float j0 = Mathf.Max(-(1 + 0.5f) * d0, 0);

            shadowRigid.velocity += j0 * rayhitsNorms[pos] * 0.15f;
            shadowRigid.angularVelocity += j0 * rayhitsNorms[pos] * UnityEngine.Random.Range(1f, 100f);
            */
            checkDistanceToCollision = false;
            getVelocityData = false;
            getData = 3;
        }

        endPosition = transform.position;
        previousPos = shadowObject.transform.position;
    }
}