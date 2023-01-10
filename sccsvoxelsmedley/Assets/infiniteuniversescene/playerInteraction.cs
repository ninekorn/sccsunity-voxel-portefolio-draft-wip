using UnityEngine;
using System.Collections;


public class playerInteraction : MonoBehaviour
{

    public byte activeBlockType = 1;
    public Transform retAdd, retDel;

    Mesh mesh;

    //public terrain terrain;

    float planeSize = 0.125f;

    float quarter = 1;

    public Transform sphere;
    Vector3 yoMan;
    Vector3 yoMan1;

    int multiplicator = 3;

    int tileSize = 10;

    float diameter;
    float fraction;
    float radius;
    float whatever;

    int roundedX;
    int roundedY;
    int roundedZ;

    //public World world;
    //public WorldPos pos;

    void Start()
    {
        //terrain = GetComponent<terrain>();
        //retAdd.localScale = retAdd.localScale * planeSize;
        //retDel.localScale = retDel.localScale * planeSize;
        fraction = 1 / planeSize;
        radius = planeSize / 2;
        diameter = 0.1f;
        whatever = 1 / (diameter * 2);

    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f, 0));

        if (Physics.Raycast(ray, out hit, 100f))
        {
            //Debug.DrawRay(ray.origin,ray.direction,Color.green);

            Vector3 p = hit.point - hit.normal / (tileSize*2);
            
            float offset = planeSize / whatever;
            float offset2 = planeSize / whatever;
            //Vector3 pp = hit.transform.worldToLocalMatrix.MultiplyPoint3x4(hit.point);
            //Vector3 hiter = hit.point;
            Vector3 pp = hit.transform.worldToLocalMatrix.MultiplyPoint3x4(p);
            Transform t = hit.transform;
            /////////////////////TOPFACE/////////////////////////

            if (hit.normal.x == 0 && hit.normal.y == 1 && hit.normal.z == 0)
            {
                retDel.position = new Vector3((Mathf.Floor(p.x * fraction) / fraction)+ radius, (Mathf.Floor(p.y * fraction) / fraction) + radius, (Mathf.Floor(p.z * fraction) / fraction) + radius);
                //yoMan = new Vector3((Mathf.Floor(pp.x * fraction) / fraction), (Mathf.Floor(pp.y * fraction) / fraction) , (Mathf.Floor(pp.z * fraction) / fraction));

                //retDel.position = new Vector3((Mathf.Floor(xxx * fraction) / fraction)+planeSize/2, (Mathf.Floor(yyy * fraction) / fraction)-planeSize/2, (Mathf.Floor(zzz * fraction) / fraction) + planeSize / 2);
                //Instantiate(sphere,retDel.position,Quaternion.identity);

                //retDel.position = new Vector3(p.x,p.y,p.z);
                //yoMan = new Vector3((Mathf.Floor(pp.x * tileSize) / tileSize) + offset * multiplicator, (Mathf.Floor(pp.y * tileSize) / tileSize) + offset, (Mathf.Ceil(pp.z * tileSize) / tileSize) + offset);

                roundedX = (int)((Mathf.FloorToInt(p.x * fraction) / fraction) / planeSize);
                roundedY = (int)((Mathf.FloorToInt(p.y * fraction) / fraction) / planeSize);
                roundedZ = (int)((Mathf.FloorToInt(p.z * fraction) / fraction) / planeSize);



                /*//////////////////////FRONT FACE///////////////////////////////
                if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == -1)
                {
                    //Debug.Log("yo1");

                    retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
                    yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * -2);

                    retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 5);
                    yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * 3);
                }


                /////////////////////RIGHT FACE////////////////////////////////
                if (hit.normal.x == 1 && hit.normal.y == 0 && hit.normal.z == 0)
                {
                    //Debug.Log("yo2");
                    retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset * multiplicator, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                    yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset * multiplicator, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

                    retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset + offset + offset * multiplicator, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                    yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset + offset + offset * multiplicator, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
                }

                //////////////////////LEFT FACE////////////////////////////////
                if (hit.normal.x == -1 && hit.normal.y == 0 && hit.normal.z == 0)
                {
                    //Debug.Log("yo3");
                    retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset * -multiplicator, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                    yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset * -multiplicator, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

                    retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                    yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) - offset * 5, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);
                }

                ////////////////////BACK FACE////////////////////////////////
                if (hit.normal.x == 0 && hit.normal.y == 0 && hit.normal.z == 1)
                {
                    //Debug.Log("yo4");
                    retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset * -multiplicator);
                    yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * 5);

                    retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset * multiplicator);
                    yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) + offset, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset + offset * multiplicator);
                }*/

                /*/////////////////////BOTTOM FACE/////////////////////////////////
                if (hit.normal.x == 0 && hit.normal.y == -1 && hit.normal.z == 0)
                {
                    //Debug.Log("yo5");
                    retDel.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 1, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                    yoMan = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 1, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

                    retAdd.position = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 1, (Mathf.Ceil(p.z * tileSize) / tileSize) - offset);
                    yoMan1 = new Vector3((Mathf.Floor(p.x * tileSize) / tileSize) + offset, (Mathf.Floor(p.y * tileSize) / tileSize) - offset * 1, (Mathf.Ceil(p.z * tileSize) / tileSize) + offset);

                }*/
                if (Input.GetMouseButton(1))
                {

                    //float xx = hit.transform.position.x;
                    //float yy = hit.transform.position.y;
                    //float zz = hit.transform.position.z;

                    //int xu;
                    //int yu;
                    //int zu;

                    //Debug.Log(roundedX);
                    //if (hit.transform != null)
                    //{
                    Transform objectHit = hit.transform;

                    if (objectHit.GetComponent<chunko>() != null)
                    {
                        objectHit.GetComponent<chunko>().SetBrick(roundedX, roundedY, roundedZ, 0);
                    }
                    //}


                    //world.(pos.x + x, pos.y + y, pos.z + z);


                    /*if (terrain.getChunk(t.position.x, t.position.y, t.position.z) != null)
                    {
                        if (hit.transform == t)
                        {
                            //Debug.Log(terrain.getChunk(xx, yy, zz).chunker.transform.position);
                            //chuka.chunker.GetComponent<chunk>().SetBrick(xxx, yyy, zzz, 0);
                            chunky chuka = terrain.getChunk(xx, yy, zz);
                            chuka.chunker.GetComponent<chunk>().SetBrick(roundedX, roundedY, roundedZ, 0);
                        }
                    }*/
                }
            }
            ///////ADD BLOCK/////////////
            if (Input.GetMouseButtonDown(0))
            {
                //float x = (yoMan1.x);
                //float y = (yoMan1.y);
                //float z = (yoMan1.z);            
            }


            ///////DELETE BLOCK///////////
            /*if (Input.GetMouseButtonDown(1))
            {

                float xx = hit.transform.position.x;
                float yy = hit.transform.position.y;
                float zz = hit.transform.position.z;



                //Debug.Log(roundedX + " " + roundedY + " " + roundedZ);
                //Debug.Log(pp);

                //Debug.Log(roundedX + " " + roundedY + " " + roundedZ);


                //int xxx = (int)Mathf.Floor((yoMan.x) / diameter);
                //int yyy = (int)Mathf.Floor((yoMan.y) / diameter);
                //int zzz = (int)Mathf.Floor((yoMan.z) / diameter);

                int xu;
                int yu;
                int zu;

                Transform t = hit.transform;

                if (terrain.getChunk(t.position.x, t.position.y, t.position.z) != null)
                {
                    if (hit.transform == t)
                    {
                        //Debug.Log(terrain.getChunk(xx, yy, zz).chunker.transform.position);
                        //chuka.chunker.GetComponent<chunk>().SetBrick(xxx, yyy, zzz, 0);
                        chunky chuka = terrain.getChunk(xx, yy, zz);
                        chuka.chunker.GetComponent<chunk>().SetBrick(roundedX, roundedY, roundedZ, 0);
                    }               
                }


                /*if (terrain.getChunkPos(xx, yy, zz) != null)
                {
                    Debug.Log(terrain.getChunkPos(xx, yy, zz).transform.position);
                    //terrain.getChunkPos(xx, yy, zz).SetBrick(xxx, yyy, zzz, 0);
                }

            }*/
        }
        /*else
        {
            retAdd.position = new Vector3(0, -100, 0);
            retDel.position = new Vector3(0, -100, 0);
        }*/
    }
}
