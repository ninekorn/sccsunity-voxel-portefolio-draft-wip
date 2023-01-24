using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.PlayerSettings;

public class sccsikplayer : MonoBehaviour
{

    public sccsplayer themovementplayerscript;

    public GameObject[] arrayofikarms = new GameObject[4];


    public static sccsikplayer currentsccsikplayer;

    public Vector3 initeulerangles = new Vector3(0, 0, 0);

    float lengthofarm = 0.0f;
    // Start is called before the first frame update
    void Start()
    {

        currentsccsikplayer = this;


        //this.transform.position = 





        var pelvisemptygameobject = new GameObject();
        pelvisemptygameobject.transform.name = "pelvisemptygameobject";
        pelvisemptygameobject.transform.position = this.transform.position + new Vector3(0, -0.35f, 0.0f);// new Vector3(0, -0.35f, 0.0f);// this.transform.position;// shoulderoriginpositionoffset;
        //pelvisemptygameobject.transform.parent = this.transform;

        var pelvisrenderer = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        pelvisrenderer.localScale = new Vector3(0.35f, 0.15f, 0.15f);
        pelvisrenderer.transform.position = pelvisemptygameobject.transform.position;// new Vector3(0, 0, 0.0f);
        //pelvisrenderer.transform.parent = pelvisemptygameobject.transform;
        pelvisrenderer.transform.name = "pelvisrenderer";
        pelvisrenderer.GetComponent<Renderer>().material.color = Color.black;



        var torsorenderer = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        torsorenderer.localScale = new Vector3(0.35f, 0.45f, 0.15f);
     
        var torsoemptygameobject = new GameObject();
        torsoemptygameobject.transform.name = "torsoemptygameobject";
        torsoemptygameobject.transform.position = pelvisemptygameobject.transform.position + new Vector3(0, pelvisrenderer.transform.localScale.y * 0.5f, 0);// this.transform.position;// new Vector3(0, -0.35f, 0.0f);// this.transform.position;// shoulderoriginpositionoffset;
        //torsoemptygameobject.transform.parent = this.transform;

        torsorenderer.transform.position = torsoemptygameobject.transform.position+ new Vector3(0, pelvisrenderer.transform.localScale.y * 0.5f + torsorenderer.transform.localScale.y * 0.5f);// new Vector3(0, 0, 0.0f);
        //torsorenderer.transform.parent = torsoemptygameobject.transform;
        torsorenderer.transform.name = "torsorenderer";











        var headrenderer = GameObject.CreatePrimitive(PrimitiveType.Cube).transform;
        headrenderer.localScale = new Vector3(0.15f, 0.15f, 0.15f);
        var heademptygameobject = new GameObject();
        heademptygameobject.transform.name = "heademptygameobject";
        heademptygameobject.transform.position = torsorenderer.transform.position + new Vector3(0, torsorenderer.transform.localScale.y * 0.5f, 0);// new Vector3(0, -0.35f, 0.0f);// this.transform.position;// shoulderoriginpositionoffset;                                                                                                                                                                                      //heademptygameobject.transform.parent = torsoemptygameobject.transform;

        headrenderer.GetComponent<Renderer>().material.color = Color.black;

        headrenderer.transform.position = torsorenderer.transform.position + new Vector3(0, torsorenderer.transform.localScale.y * 0.5f + (headrenderer.localScale.y * 0.5f), 0); //heademptygameobject.transform.position;
        //headrenderer.transform.parent = heademptygameobject.transform;





        this.transform.gameObject.AddComponent<sccsplayer>().enabled = false;


        themovementplayerscript = this.transform.gameObject.GetComponent<sccsplayer>();

        themovementplayerscript.camera = GameObject.FindGameObjectWithTag("MainCamera");
        themovementplayerscript.originalcamerapivot = new GameObject();

        themovementplayerscript.originalcamerapivot.transform.parent = this.transform;

        themovementplayerscript.originalcamerapivot.transform.position = themovementplayerscript.camera.transform.position;
        themovementplayerscript.upperbodypivot = torsoemptygameobject.transform.gameObject;//torsorenderer.transform.gameObject;
        themovementplayerscript.headpivotpoint = heademptygameobject.transform.gameObject;


        themovementplayerscript.isgroundedpivotpoint = new GameObject();

        //themovementplayerscript.isgroundedpivotpoint.transform.position = new Vector3(0,0,0);// pelvisemptygameobject.transform.position;// + new Vector3(0, -pelvisrenderer.transform.localScale.y * 0.5f, 0);
        themovementplayerscript.isgroundedpivotpoint.transform.position = pelvisemptygameobject.transform.position;// pelvisemptygameobject.transform.position;// + new Vector3(0, -pelvisrenderer.transform.localScale.y * 0.5f, 0);

        themovementplayerscript.isgroundedpivotpoint.transform.rotation = Quaternion.Euler(new Vector3(90, 0, 0));
        themovementplayerscript.isgroundedpivotpoint.name = "isgroundedpivotray";



        var layerMaskforscript = 1 << 8;
        themovementplayerscript.layerMask = layerMaskforscript;

        themovementplayerscript.viewer = GameObject.FindGameObjectWithTag("Player");

        //themovementplayerscript.transform.parent















        for (int iarm = 0; iarm < 4; iarm++)
        {
            if (iarm == 0) // LEFT ARM
            {
                arrayofikarms[iarm] = new GameObject();
                arrayofikarms[iarm].transform.parent = torsorenderer.transform;
                arrayofikarms[iarm].transform.name = "arrayofikarms[iarm]";
                arrayofikarms[iarm].SetActive(false);
                var ikarmscript0 = arrayofikarms[iarm].AddComponent<sccsikarm>();

                ikarmscript0.typeofArm = 0;
                ikarmscript0.shoulderoriginpositionoffset = torsorenderer.transform.position + new Vector3(-torsorenderer.transform.localScale.x * 0.5f, 0, 0) + new Vector3(0, torsorenderer.transform.localScale.y * 0.5f, 0) ;
                ikarmscript0.StartScript(torsorenderer.transform, arrayofikarms[iarm]);


                Vector3 currentshoulderpos = ikarmscript0.shoulderrenderer.transform.position;

                ikarmscript0.prefabgameobjectempty.transform.position = currentshoulderpos + new Vector3(-ikarmscript0.shoulderrenderer.localScale.x * 0.5f,0,0);


                //arrayofikarms[iarm].transform.position = ikarmscript0.shoulderoriginpositionoffset + new Vector3(-ikarmscript0.shoulderrenderer.localScale.x * 0.5f, -ikarmscript0.shoulderrenderer.localScale.y * 0.5f, 0);
                



                arrayofikarms[iarm].SetActive(true);



            }
            else if (iarm == 1) //RIGHT ARM
            {
                arrayofikarms[iarm] = new GameObject();
                arrayofikarms[iarm].transform.parent = torsorenderer.transform;

                arrayofikarms[iarm].SetActive(false);
                var ikarmscript1 = arrayofikarms[iarm].AddComponent<sccsikarm>();

                ikarmscript1.typeofArm = 1;
                ikarmscript1.shoulderoriginpositionoffset = torsorenderer.transform.position + new Vector3(torsorenderer.transform.localScale.x*0.5f, 0, 0) + new Vector3(0, torsorenderer.transform.localScale.y * 0.5f, 0);
                ikarmscript1.StartScript(torsorenderer.transform, arrayofikarms[iarm]);

                //arrayofikarms[iarm].transform.position = ikarmscript1.shoulderoriginpositionoffset;
                Vector3 currentshoulderpos = ikarmscript1.shoulderrenderer.transform.position;

                ikarmscript1.prefabgameobjectempty.transform.position = currentshoulderpos + new Vector3(ikarmscript1.shoulderrenderer.localScale.x * 0.5f, 0, 0);

                arrayofikarms[iarm].SetActive(true);


            }
          else  if (iarm == 2) //LEFT LEG
            {
                arrayofikarms[iarm] = new GameObject();
                arrayofikarms[iarm].transform.parent = pelvisrenderer.transform;

                arrayofikarms[iarm].SetActive(false);
                var ikarmscript0 = arrayofikarms[iarm].AddComponent<sccsikarm>();

                ikarmscript0.typeofArm = 2;
                ikarmscript0.shoulderoriginpositionoffset = pelvisrenderer.transform.position + new Vector3(-torsorenderer.transform.localScale.x * 0.5f, 0, 0);
                ikarmscript0.StartScript(pelvisrenderer.transform, arrayofikarms[iarm]);
                //arrayofikarms[iarm].transform.position = ikarmscript0.shoulderoriginpositionoffset;

                Vector3 currentshoulderpos = ikarmscript0.shoulderrenderer.transform.position;

                ikarmscript0.prefabgameobjectempty.transform.position = currentshoulderpos + new Vector3(-ikarmscript0.shoulderrenderer.localScale.x * 0.5f, (-pelvisrenderer.localScale.y * 0.5f) + (-ikarmscript0.shoulderrenderer.localScale.y * 0.5f), 0)+
                new Vector3(ikarmscript0.shoulderrenderer.transform.localScale.x * 0.5f, 0, 0);
                ;


                playerInteractionrev11 playerinteractionrev11 = ikarmscript0.shoulder.gameObject.AddComponent<playerInteractionrev11>();

         
                var planetmanager = GameObject.FindGameObjectWithTag("terrain");

                playerinteractionrev11.swtcForTypeOfInteract = 2;
                playerinteractionrev11.whichsiderayselect = 0;

                playerinteractionrev11.planetmanager = planetmanager.transform;
                playerinteractionrev11.activeBlockType = 0;
                playerinteractionrev11.retAdd = null;
                playerinteractionrev11.retAdd = null;
                playerinteractionrev11.sphere = null;



                var layerMask = 1 << 8;
                playerinteractionrev11.layerMask = layerMask;

                playerinteractionrev11.upperleg = ikarmscript0.renderupperarm;
                playerinteractionrev11.lowerleg = ikarmscript0.foreArmrenderer;
                playerinteractionrev11.foot = ikarmscript0.handrenderer;
                playerinteractionrev11.foottarget = ikarmscript0.handTarget.transform;
                playerinteractionrev11.footTarget = ikarmscript0.handTarget.transform;

                playerinteractionrev11.legstaticpivot = ikarmscript0.shoulder;





                arrayofikarms[iarm].SetActive(true);
                //ikarm1.transform.parent = this.transform;



            }
            else if (iarm == 3) ////RIGHT LEG
            {
                arrayofikarms[iarm] = new GameObject();
                arrayofikarms[iarm].transform.parent = pelvisrenderer.transform;

                arrayofikarms[iarm].SetActive(false);
                var ikarmscript1 = arrayofikarms[iarm].AddComponent<sccsikarm>();

                ikarmscript1.typeofArm = 3;
                ikarmscript1.shoulderoriginpositionoffset = pelvisrenderer.transform.position + new Vector3(torsorenderer.transform.localScale.x * 0.5f, 0, 0);
                ikarmscript1.StartScript(pelvisrenderer.transform, arrayofikarms[iarm]);
                //arrayofikarms[iarm].transform.position = ikarmscript1.shoulderoriginpositionoffset;

                Vector3 currentshoulderpos = ikarmscript1.shoulderrenderer.transform.position;

                ikarmscript1.prefabgameobjectempty.transform.position = currentshoulderpos + new Vector3(ikarmscript1.shoulderrenderer.localScale.x * 0.5f, (-pelvisrenderer.localScale.y * 0.5f) + (-ikarmscript1.shoulderrenderer.localScale.y * 0.5f), 0) -
                   new Vector3(ikarmscript1.shoulderrenderer.transform.localScale.x * 0.5f, 0, 0);



                playerInteractionrev11 playerinteractionrev11 = ikarmscript1.shoulder.gameObject.AddComponent<playerInteractionrev11>();


                var planetmanager = GameObject.FindGameObjectWithTag("terrain");

                playerinteractionrev11.swtcForTypeOfInteract = 2;
                playerinteractionrev11.whichsiderayselect = 1;

                playerinteractionrev11.planetmanager = planetmanager.transform;
                playerinteractionrev11.activeBlockType = 0;
                playerinteractionrev11.retAdd = null;
                playerinteractionrev11.retAdd = null;
                playerinteractionrev11.sphere = null;

                var layerMask = 1 << 8;

                playerinteractionrev11.layerMask = layerMask;

                playerinteractionrev11.upperleg = ikarmscript1.renderupperarm;
                playerinteractionrev11.lowerleg = ikarmscript1.foreArmrenderer;
                playerinteractionrev11.foot = ikarmscript1.handrenderer;
                playerinteractionrev11.foottarget = ikarmscript1.handTarget.transform;
                playerinteractionrev11.footTarget = ikarmscript1.handTarget.transform;

                playerinteractionrev11.legstaticpivot = ikarmscript1.shoulder;


                arrayofikarms[iarm].SetActive(true);
                //arrayofikarms[iarm].transform.parent = this.transform;

                lengthofarm = arrayofikarms[iarm].gameObject.GetComponent<sccsikarm>().totalArmLength;
            }













            /*else if (iarm == 2)
            {
                GameObject ikarm1 = new GameObject();
                ikarm1.SetActive(false);
                var ikarmscript1 = ikarm1.AddComponent<sccsikarm>();

                ikarmscript1.typeofArm = 2;
                ikarmscript1.shoulderoriginpositionoffset = new Vector3(-0.25f, -0.25f, 0);

                ikarmscript1.originroteulerangles = new Vector3(90,0,0);

                ikarm1.SetActive(true);
                ikarm1.transform.parent = this.transform;
            }
            else if (iarm == 3)
            {
                GameObject ikarm1 = new GameObject();
                ikarm1.SetActive(false);
                var ikarmscript1 = ikarm1.AddComponent<sccsikarm>();

                ikarmscript1.typeofArm = 3;
                ikarmscript1.shoulderoriginpositionoffset = new Vector3(0.25f, -0.25f, 0);

                ikarmscript1.originroteulerangles = new Vector3(90, 0, 0);


                ikarm1.SetActive(true);
                ikarm1.transform.parent = this.transform;
            }*/
        }


        themovementplayerscript.pointertarget = new GameObject();
        themovementplayerscript.pointertarget.transform.position = pelvisemptygameobject.transform.position + new Vector3(0, -lengthofarm, 0);
        themovementplayerscript.transform.parent = this.transform;
        themovementplayerscript.pointertarget.name = "pointertarget";

        //themovementplayerscript.viewer = themovementplayerscript.pointertarget;

        themovementplayerscript.enabled = true;





        this.transform.position = themovementplayerscript.pointertarget.transform.position;

        themovementplayerscript.camera.transform.position = heademptygameobject.transform.position + new Vector3(0,headrenderer.transform.localScale.y * 2.95f,0);
        //themovementplayerscript.camera.transform.position += new Vector3(0, 0, -headrenderer.transform.localScale.z * 2.75f);


        themovementplayerscript.isgroundedpivotpoint.transform.parent = this.transform;


        var copyrotscript = heademptygameobject.transform.gameObject.AddComponent<copyRotation>();

        copyrotscript.parent = themovementplayerscript.camera;



        pelvisemptygameobject.transform.parent = this.transform;
        pelvisrenderer.transform.parent = pelvisemptygameobject.transform;
        torsoemptygameobject.transform.parent = this.transform;
        torsorenderer.transform.parent = torsoemptygameobject.transform;

        themovementplayerscript.camera.transform.parent = torsoemptygameobject.transform;


        heademptygameobject.transform.parent = torsoemptygameobject.transform;
        //headrenderer.transform.parent = torsoemptygameobject.transform;
        headrenderer.transform.parent = heademptygameobject.transform;



        this.transform.rotation = Quaternion.Euler(initeulerangles);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
