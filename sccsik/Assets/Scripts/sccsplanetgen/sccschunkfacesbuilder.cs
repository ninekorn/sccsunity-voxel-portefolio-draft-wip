using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccschunkfacesbuilder : MonoBehaviour
{

    public struct chunkdata
    {
        /*public List<List<Vector3>> vertices;
        public List<List<int>> triangles;*/

        public List<Vector3>[] vertices;
        public List<int>[] triangles;

        /*
        public Vector3[][] vertices;
        public int[][] triangles;*/
    }



    sccscomputevoxelALLFACES[] arrayofchunkdivs;



    chunkdata[] listofchunkdata;

    // Start is called before the first frame update
    void Start()
    {


        arrayofchunkdivs = new sccscomputevoxelALLFACES[6];

        listofchunkdata = new chunkdata[6];

        for (int f = 0; f < 6; f++)
        {
            GameObject emptyobject = planetdivobjectpool.current.GetPooledObject();// this.transform.gameObject.GetComponent<planetdivobjectpool>().GetPooledObject();
            emptyobject.SetActive(true);

            /*listofchunkdata[0].vertices = new Vector3[sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizex * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizey * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizez][];
            listofchunkdata[0].triangles = new int[sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizex * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizey * sccscomputevoxelALLFACES.currentsccscomputevoxelALLFACES.levelsizez][];
            */
         
            arrayofchunkdivs[f] = emptyobject.GetComponent<sccscomputevoxelALLFACES>();

            /*int sizeofarray = arrayofchunkdivs[f].levelsizex * arrayofchunkdivs[f].levelsizey * arrayofchunkdivs[f].levelsizez;
            listofchunkdata[f].vertices = new List<Vector3>[sizeofarray];
            listofchunkdata[f].triangles = new List<int>[sizeofarray];*/

            arrayofchunkdivs[f].CreateTheShaders(f);
            arrayofchunkdivs[f].CreateTheArrays(f);

            arrayofchunkdivs[f].CreateTheMaps(f);



            arrayofchunkdivs[f].ComputeTheVertexes();
            arrayofchunkdivs[f].CreateTheVerticesAndTriangles(f, out listofchunkdata[f].vertices, out listofchunkdata[f].triangles);
            arrayofchunkdivs[f].CreateTheMesh(f, listofchunkdata[f].vertices, listofchunkdata[f].triangles);

            var script = arrayofchunkdivs[f] ;

            if (f == 0)
            {
                this.transform.position = new Vector3(script.levelsizex * script.mapx * script.planesize * 0.5f, script.levelsizey * script.mapy * script.planesize * 0.5f, script.levelsizez * script.mapz * script.planesize * 0.5f);
            }
            emptyobject.transform.parent = this.transform;



            //emptyobject.transform.position = planetcoreposition;//planetcoreposition;//
        }
    }






    // Update is called once per frame
    void Update()
    {
        
    }
}
