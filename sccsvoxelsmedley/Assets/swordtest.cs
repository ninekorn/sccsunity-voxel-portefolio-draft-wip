//https://forum.unity.com/threads/need-help-on-sword-attack-concept.455461/#post-2984933

using UnityEngine;
using System.Collections;
using UnityEngine.XR;

public class swordtest : MonoBehaviour
{
    public GameObject sword;
    public GameObject target;
    public GameObject hand;

    public float speed = 10;
    public float rotate_speed = 10;

    public float max_distance = 1f;

    void Start()
    {
        sword = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sword.transform.localScale = new Vector3(0.1f, 0.5f, 2);
        sword.transform.name = "sword";


        GameObject sharpside = GameObject.CreatePrimitive(PrimitiveType.Cube);
        sharpside.transform.localScale = new Vector3(0.1f, 0.1f, 2);
        sharpside.transform.position = new Vector3(0, -0.25f, 0) + this.transform.position ;
        sharpside.GetComponent<MeshRenderer>().material.color = Color.red;
        sharpside.transform.parent = sword.transform;
        //sharpside.transform.parent = this.transform;
        sharpside.transform.name = "sharpside";


        target = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        target.transform.localScale = new Vector3(0.2f, 1, 0.2f);
        target.GetComponent<MeshRenderer>().material.color = Color.yellow;
        //target.transform.parent = this.transform;
        target.transform.name = "target";

        hand = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        hand.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        hand.transform.position = new Vector3(0, 0, -1f) + this.transform.position;
        hand.GetComponent<MeshRenderer>().material.color = Color.green;
        //hand.transform.parent = this.transform;
        hand.transform.name = "hand";
        //sword.transform.parent = hand.transform;
    }

    void Update()
    {

        Vector3 mov = new Vector3(Input.GetAxis("Mouse X") * Time.deltaTime * speed, Input.GetAxis("Mouse Y") * Time.deltaTime * speed, 0);

        hand.transform.position += mov;

        Vector3 clamped = hand.transform.position;
        clamped.x = Mathf.Clamp(clamped.x, target.transform.position.x - max_distance, target.transform.position.x + max_distance);
        clamped.y = Mathf.Clamp(clamped.y, target.transform.position.y - max_distance, target.transform.position.y + max_distance);

        hand.transform.position = clamped;

        Quaternion rot = new Quaternion();
        rot.SetLookRotation(sword.transform.forward, -(target.transform.position - hand.transform.position).normalized);
        sword.transform.rotation = Quaternion.Lerp(sword.transform.rotation, rot, rotate_speed * Time.deltaTime);

    }
}

