using System;
using System.Collections.Generic;
using System.Text;

using Unity;
using UnityEngine;

namespace sccs
{

    public class tutorialcubeaschunkinststruct
    {
        
        /*public tutorialfacemesh[] arrayoffacemesh;
        public List<tutorialfacemesh> somefacemeshlisttodraw;// = new List<tutorialfacemesh>();
        */



        public int originalposmainx;
        public int originalposmainy;
        public int originalposmainz;

        public int processsteps;

        public int canbuildinstances;
        public int counterofindexes;


        public chunkdataindexes indexesofdata;

        public int hasfiles;

        public int someindexmain;
        public int incrementsdivx;
        public int incrementsdivy;
        public int incrementsdivz;
        public int incrementsfracx;
        public int incrementsfracy;
        public int incrementsfracz;

        public int realposmainx;
        public int realposmainy;
        public int realposmainz;

        public string str;


        public Dictionary<int, int> arrayofchunkvertsinst;

        public int canloop;
        public int hasinit;

        public float distanceculling;
        public int frustrumculling;
        public float[] chunkpos;

        //public sccslevelgen sccslevelgen;
        //public sclevelgenglobals somelevelgenprimglobals;

        public int lastposmainx;
        public int lastposmainy;
        public int lastposmainz;

        public int totaliterations;
        public int ite;
        public int x;
        public int y;
        public int z;

        public int xe;
        public int ye;
        public int ze;

        public int posmainx;
        public int posmainy;
        public int posmainz;


        public int mainminx;
        public int mainminy;
        public int mainminz;
        public int mainmaxx;
        public int mainmaxy;
        public int mainmaxz;

        public int facetype;
        public int levelofdetail;

        public int sx;
        public int sy;
        public int sz;
    }




    public struct instancetype
    {
        public Vector4 instancePos;
    };
    public struct scinstanceintmaps
    {
        public Matrix4x4 instanceintmap;
    };


    public struct scinstancevertdimensions
    {
        public Matrix4x4 instanceMatrix4x4;
        public Matrix4x4 instanceMatrix4x4b;
        public Matrix4x4 instanceMatrix4x4c;
        public Matrix4x4 instanceMatrix4x4d;
    };

    public class chunkdataindexes
    {
        public int[] counteroflines;
    }

    public class chunkdata
    {

        public int originalposmainx;
        public int originalposmainy;
        public int originalposmainz;

        public int isextremitytypex;
        public int isextremitytypey;
        public int isextremitytypez;


        public int[][] somearrayofcoords;// = new int[6][];
        public int[][] somearrayofcoordsfloor;// = new int[6][];

        public chunkdata[] listofchunksadjacent;//= new chunkdata[6];
        public chunkdata[] listofchunksadjacentfloor;// = new chunkdata[6];

        public int linepaddingx;
        public int linepaddingy;
        public int linepaddingz;

        public int counterofindexes;

        public int wherethechunkisinthefileindex;

        public int indexinfilex;
        public int indexinfiley;
        public int indexinfilez;


        public int posmainx;
        public int posmainy;
        public int posmainz;


        public int isfirstchunkinbundle;
        public int _maxWidth;
        public int _maxHeight;
        public int _maxDepth;

        public int vertexlistWidth;
        public int vertexlistHeight;
        public int vertexlistDepth;

        public int widthflat;
        public int heightflat;
        public int depthflat;


        public int typeofterraintile;
        /*
        public int[][] somearrayofcoordsfloor;
        public int[][] somearrayofcoords;
        public chunkdata[] listofchunksadjacent;
        public chunkdata[] listofchunksadjacentfloor;*/

        public int blockexistsinarray;
        public int cornerswtcvertex0;
        public int cornerswtcvertex1;
        public int cornerswtcvertex2;
        public int cornerswtcvertex3;

        public int memoryvertexcounter;

        public int rowIterateX;// = 0;
        public int rowIterateY;
        public int rowIterateZ;// = 0;

        public bool foundVertOne;// = false;
        public bool foundVertTwo;// = false;
        public bool foundVertThree;// = false;
        public bool foundVertFour;// = false;

        public int firstvertx;
        public int firstverty;
        public int firstvertz;

        public int secondvertx;
        public int secondverty;
        public int secondvertz;

        public int thirdvertx;
        public int thirdverty;
        public int thirdvertz;

        public int fourthvertx;
        public int fourthverty;
        public int fourthvertz;


        public int oneVertIndexX;// = 0;
        public int oneVertIndexY;// = 0;
        public int oneVertIndexZ;// = 0;

        public int twoVertIndexX;// = 0;
        public int twoVertIndexY;// = 0;
        public int twoVertIndexZ;// = 0;

        public int threeVertIndexX;// = 0;
        public int threeVertIndexY;//= 0;
        public int threeVertIndexZ;// = 0;

        public int fourVertIndexX;// = 0;
        public int fourVertIndexY;// = 0;
        public int fourVertIndexZ;// = 0;


        public Vector4 chunkoriginpos;

        public int[] map;
        public int[] mapvertindexfordims;
        public int[] widthdimtop;
        public int[] heightdimtop;
        public int[] depthdimtop;
        public int[] mapfirstvertxtop;
        public int[] mapfirstvertytop;
        public int[] mapfirstvertztop;

        public int[] _tempChunkArray;
        public int[] _chunkVertexArray0;
        public int[] _testVertexArray0;


        public int someixtop;//= 0;
        public int someiytop;
        public int someiztop;
        public int someindextop;

        public int _newVertzCounter;
        public int someixleft;
        public int someiyleft;
        public int someizleft;
        public int someindexleft;


        public int someixright;
        public int someiyright;
        public int someizright;
        public int someindexright;


        public int someixfront;
        public int someiyfront;
        public int someizfront;
        public int someindexfront;


        public int someixback;
        public int someiyback;
        public int someizback;
        public int someindexback;


        public int someixbottom;
        public int someiybottom;
        public int someizbottom;
        public int someindexbottom;


        public int swtcdirtyarea;

        public float[] positioninbundle;


        //public tutorialchunkcubemap arraychunkvertslod0;

  
        public float distanceculling;
        public bool frustrumculldraw;
        public float[] realpos;
        public int[] chunkPos;
        //public int indexinlevelgenmap;
        public int indexintypeoftiles;
        public int typeofterraintiles;

        public int x;
        public int y;
        public int z;

        public int width;
        public int height;
        public int depth;


        public scinstancevertdimensions instanceintmapfirstvertexa;
        public scinstancevertdimensions instanceintmapfirstvertexb;

        public scinstanceintmaps instanceintmap;

        public scinstancevertdimensions instancesMatrix4x4a;

        public scinstancevertdimensions instancesMatrix4x4b;

        public int indexinmainarray;


        public int isextremitytype;
        public int indexofthefirstchunktile;
        public float nodedistance;
        public float[] coordsfloat;


        public Vector3 _chunkPosition;
        public List<Vector3> _chunkVertices;
        public List<int> _chunkTriangles;
        public int[] _chunkVerticesArray;
        public int[] _chunkBlockArray;



        
    }


}
