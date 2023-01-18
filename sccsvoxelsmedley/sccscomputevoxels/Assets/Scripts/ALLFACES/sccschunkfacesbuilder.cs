using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sccschunkfacesbuilder : MonoBehaviour
{



    // Start is called before the first frame update
    void Start()
    {




        for (int f = 0;f < 6;f++)
        {
            GameObject emptyobject = planetdivobjectpool.current.GetPooledObject();// this.transform.gameObject.GetComponent<planetdivobjectpool>().GetPooledObject();


            emptyobject.SetActive(true);

            emptyobject.GetComponent<sccscomputevoxelALLFACES>().CreateTheFaces(f);



        }



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
