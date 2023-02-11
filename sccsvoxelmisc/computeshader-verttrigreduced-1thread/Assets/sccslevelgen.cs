///////////////////////////// after learning what was a walker type level generator from the youtuber GucciDev, i decided to code my own level generator. I am the one who developed my own walls logic, my own array type of variables everywhere in this
//DEVELOPED BY STEVE CHASSÉ// script instead of using dictionaries and lists.
/////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using System.Collections;

using sccsr17;
using System.Xml.XPath;
using System.Xml.Linq;

using System.Text.RegularExpressions;
using System.Diagnostics;
using System.Globalization;

namespace sccs
{
    public class sccslevelgen
    {
        private static XElement GetElement(XDocument doc, string elementName)
        {
            foreach (XNode node in doc.DescendantNodes())
            {
                if (node is XElement)
                {
                    XElement element = (XElement)node;
                    if (element.Name.LocalName.Equals(elementName))
                        return element;
                }
            }
            return null;
        }


        private static readonly Regex sWhitespace = new Regex(@"\s+");


        //public static sccslevelgen.callbackstructdata[][][] thecallbackstructdata;
        public static int levelofdetails = 2;

        /*
        public struct callbackstructdata
        {

            public int xe;
            public int ye;
            public int ze;

            public int thevoxelindex;
            public int threadworkswtc;

            //public chunkdata[] chunkdata;

            public tutorialcubeaschunkinst tutorialcubeaschunkinst;
            public int seed;

            //public int[] chunkPos;

            public int typeofterraintile;
            public int facetype_;//= data.facetype;
                                 //public tutorialcubeaschunkinst componentparent_;// componentparent_ = data.componentparent_;
            public int levelofdetail_;// = data.levelofdetail_;
            public int minx;// = data.minx;
            public int miny;// = data.miny;
            public int minz;// = data.minz;
            public int maxx;// = data.maxx;
            public int maxy;// = data.maxy;
            public int maxz;// = data.maxz;
            public Vector3 leveldisivionoriginposition;


            //[ThreadStatic]
            public int thesomecounterout;
            //public int thesomecounterin;


            //public int rwthesomecounterin;

            //[ThreadStatic]
            public int rwthesomecounterout;

            public tutorialfacemesh[] arrayoffacemesh;
            public Dictionary<int, int> arrayofchunkvertsinst;
            public List<tutorialfacemesh> somefacemeshlisttodraw;

            public int theindexdivofdivlevel;
            public int workswtc;

        }*/



















        public int[][] arrayoflevelparts;


        public static int arraymultiplier = 1;
        public static int NUMBEROFFACES = 6;

        public static chunkdata[][][][] chunkdata;

        //public static List<chunkdata[]> chunkdata;
        //public static Dictionary<int, chunkdata> chunkdata;
        /*public chunkdata[] arraychunkdatalod0bottom;
        public chunkdata[] arraychunkdatalod0left;
        public chunkdata[] arraychunkdatalod0right;
        public chunkdata[] arraychunkdatalod0front;
        public chunkdata[] arraychunkdatalod0back;*/

        //get main index first, then get 




















        /*
        public static chunkdata getchunkinlevelgenmap(int x, int y, int z, int levelofdetail, out int indexinarray)
        {
            int orix = x;
            int oriy = y;
            int oriz = z;

            if (x < 0)
            {
                x *= -1;
                x = x + (maxx - 1);
            }

            if (y < 0)
            {
                y *= -1;
                y = y + (maxy - 1);
            }
            if (z < 0)
            {
                z *= -1;
                z = z + (maxz - 1);
            }

            int indexinsclevelgenmap = x + somewidth * (y + someheight * z);

            if (indexinsclevelgenmap >= 0 && indexinsclevelgenmap < somewidth * someheight * somedepth)
            {
                int typeofterraintile = levelmap[indexinsclevelgenmap];

                if (typeofterraintile == 0 ||
                    typeofterraintile == 1101 ||
                    typeofterraintile == 1102 ||
                    typeofterraintile == 1103 ||
                    typeofterraintile == 1104 ||
                    typeofterraintile == 1105 ||
                    typeofterraintile == 1106 ||
                    typeofterraintile == 1107 ||
                    typeofterraintile == 1108 ||
                    typeofterraintile == 1109 ||
                    typeofterraintile == 1110 ||
                    typeofterraintile == 1111 ||
                    typeofterraintile == 1112 ||
                    typeofterraintile == -99 ||
                    typeofterraintile == 1115)
                {

                    int indexofchunkinbundle = 0;
                    int indextemp = (indexinsclevelgenmap * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + indexofchunkinbundle;
                    //indexinarray = arrayofindexes[indexinsclevelgenmap * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)];
                    //indexinarray = arrayofindexes[indexinsclevelgenmap];
                    int thecounteroftileinloop = arrayofindexesalt[indextemp];

                    indexinarray = thecounteroftileinloop;




                    //indexinarray = -1;
                    //return new chunkdata();


                    //Console.WriteLine("/length:" + chunkdata[0].Length + "/index:" + indexinarray);


                    if (indexinarray >= 0 && indexinarray < chunkdata[levelofdetail][0].Length)
                    {
                        //if (levelofdetail == 1)
                        {
                            //Console.WriteLine("checking for existing map");
                            if (chunkdata[levelofdetail][0][indexinarray].map != null)
                            {
                                //Console.WriteLine("map != null");
                                return chunkdata[levelofdetail][0][indexinarray];
                            }
                            else
                            {
                                //Console.WriteLine("map == null");
                                indexinarray = -1;
                                return new chunkdata();
                            }

                            //return arraychunkdatalod0[indexinsclevelgenmap].arraychunkvertslod0;

                        }
                        /*else if (levelofdetail == 2)
                        {
                            return arraychunkdatalod1[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod1;
                        }
                        else if (levelofdetail == 3)
                        {
                            return arraychunkdatalod2[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod2;
                        }
                        else if (levelofdetail == 4)
                        {
                            return arraychunkdatalod3[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod3;
                        }
                        else if (levelofdetail == 5)
                        {
                            return arraychunkdatalod4[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod4;
                        }
                    }

                    indexinarray = -1;
                    return new chunkdata();
                }
            }
            indexinarray = -1;
            return new chunkdata();
        }*/


        /*static chunkdata themapdata = new chunkdata();
        static XmlTextReader reader;
        static int[] ia;
        static FileStream fs;
        static byte[] datafs;
        */

        /*
        public static chunkdata getmapfromfile(int x, int y, int z, int levelofdetail, out int indexinarray,string startchunkinfo, IEnumerable<string> ienumstring, sccsgraphicssec.tutorialcubeaschunkinststruct thechunk,chunkdata thecurrentchunkdata)
        {
            int orix = x;
            int oriy = y;
            int oriz = z;

            if (x < 0)
            {
                x *= -1;
                x = x + (maxx - 1);
            }

            if (y < 0)
            {
                y *= -1;
                y = y + (maxy - 1);
            }
            if (z < 0)
            {
                z *= -1;
                z = z + (maxz - 1);
            }

            int indexinsclevelgenmap = x + somewidth * (y + someheight * z);

            if (indexinsclevelgenmap >= 0 && indexinsclevelgenmap < somewidth * someheight * somedepth)
            {
                int typeofterraintile = levelmap[indexinsclevelgenmap];

                if (typeofterraintile == 0 ||
                    typeofterraintile == 1101 ||
                    typeofterraintile == 1102 ||
                    typeofterraintile == 1103 ||
                    typeofterraintile == 1104 ||
                    typeofterraintile == 1105 ||
                    typeofterraintile == 1106 ||
                    typeofterraintile == 1107 ||
                    typeofterraintile == 1108 ||
                    typeofterraintile == 1109 ||
                    typeofterraintile == 1110 ||
                    typeofterraintile == 1111 ||
                    typeofterraintile == 1112 ||
                    typeofterraintile == -99 ||
                    typeofterraintile == 1115)
                {

                    int indexofchunkinbundle = 0;
                    int indextemp = (indexinsclevelgenmap * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + indexofchunkinbundle;
                    //indexinarray = arrayofindexes[indexinsclevelgenmap * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)];
                    //indexinarray = arrayofindexes[indexinsclevelgenmap];
                    int thecounteroftileinloop = arrayofindexesalt[indextemp];

                    indexinarray = thecounteroftileinloop;



                    
                    





                    int posmainx = orix / sccsgraphicssec.currentsccsgraphicssec.incrementsfracx;
                    int posmainy = oriy / sccsgraphicssec.currentsccsgraphicssec.incrementsfracy;
                    int posmainz = oriz / sccsgraphicssec.currentsccsgraphicssec.incrementsfracz;

                    /*if (posmainx < 0)
                    {
                        posmainx *= -1;
                        posmainx = posmainx + ((sccsgraphicssec.currentsccsgraphicssec.leveldivisionx / 2) - 1);
                    }

                    if (posmainy < 0)
                    {
                        posmainy *= -1;
                        posmainy = posmainy + ((sccsgraphicssec.currentsccsgraphicssec.leveldivisiony / 2) - 1);
                    }
                    if (posmainz < 0)
                    {
                        posmainz *= -1;
                        posmainz = posmainz + ((sccsgraphicssec.currentsccsgraphicssec.leveldivisionz / 2) - 1);
                    }*/





        /*
        int lx = orix / sccsgraphicssec.currentsccsgraphicssec.incrementsfracx;
        int ly = oriy / sccsgraphicssec.currentsccsgraphicssec.incrementsfracy;
        int lz = oriz / sccsgraphicssec.currentsccsgraphicssec.incrementsfracz;

        if (lx < 0)
        {
            lx *= -1;
            lx = lx + ((sccsgraphicssec.currentsccsgraphicssec.leveldivisionx / 2) - 1);
        }

        if (ly < 0)
        {
            ly *= -1;
            ly = ly + ((sccsgraphicssec.currentsccsgraphicssec.leveldivisiony / 2) - 1);
        }
        if (lz < 0)
        {
            lz *= -1;
            lz = lz + ((sccsgraphicssec.currentsccsgraphicssec.leveldivisionz / 2) - 1);
        }
        */











        /*
        int somemainx = xe;
        int somemainy = ye;
        int somemainz = ze;

        int someindexmain = posmainx + (sccsgraphicssec.currentsccsgraphicssec.leveldivisionx) * (posmainy + (sccsgraphicssec.currentsccsgraphicssec.leveldivisiony) * posmainz);


        string pathofrelease = Directory.GetCurrentDirectory();
        ////Console.WriteLine(pathofrelease);
        string pathofchunkmap = pathofrelease + @"\chunkmaps\";




















        //var path = pathofchunkmap + @"\levelgenbytemap" + "ilod" + levelofdetail + "f" + 0 + "mx" + posmainx + "my" + posmainy + "mz" + posmainz +".xml";


        /*
        string strdata = "x" + orix + "y" + oriy + "z" + oriz;

        var nodedata = xmldoc.SelectSingleNode(strdata);



        Console.WriteLine(nodedata.Value);



        */

        //Stream output = File.OpenRead(path);

        //XmlTextReader reader = new XmlTextReader(output);



        /*
        if (reader.ReadToDescendant("x" + orix + "y" + oriy + "z" + oriz))
        {
            //Console.WriteLine("test");
            reader.Read();//this moves reader to next node which is text 
            var result = reader.Value; //this might give value than 

            //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
            ia = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

            //for (int by = 0; by < ia.Length; by++)
            //{
            //    //Console.WriteLine(ia[by]);
            //}
            themapdata = new chunkdata();
            themapdata.map = ia;
            //reader.Close();
            //reader.ResetState();

            //ia = null;
            return themapdata;
        }
        */



        /*
        if (reader.ReadToDescendant("x" + orix + "y" + oriy + "z" + oriz))
        {
            //Console.WriteLine("test");
            //reader.Read();//this moves reader to next node which is text 

            while (reader.Read())
            {
                var result = reader.Value; //this might give value than 

                //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                ia = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                //for (int by = 0; by < ia.Length; by++)
                //{
                //    //Console.WriteLine(ia[by]);
                //}
                themapdata = new chunkdata();
                themapdata.map = ia;
                //reader.Close();
                //reader.ResetState();

                //ia = null;
                //Console.WriteLine("NodeType: {0}", reader.NodeType);


                /*
                if (XmlNodeType.EndElement == reader.NodeType && "root" == reader.Name)
                {
                    reader.ResetState();

                    break;
                }



                if (XmlNodeType.EndElement == reader.NodeType)
                {

                    reader.ResetState();
                }1
                break;
            }






                //reader.ResetState();

                /*
                while (reader.Read())
                {
                    if (XmlNodeType.EndElement == reader.NodeType && "root" == reader.Name)
                    {
                        reader.ResetState();

                        break;
                    }
                }

                if (XmlNodeType.EndElement == reader.NodeType)
                {

                    reader.ResetState();
                }

        }*/

        /*
        while (reader.Read())
        {
            if (XmlNodeType.EndElement == reader.NodeType && "root" == reader.Name)
            {

                reader.ResetState();
                break;
            }



            /*if (XmlNodeType.EndElement == reader.NodeType)
            {

                reader.ResetState();
            }
        }*/

        /*
        if (themapdata != null)
        {
            return themapdata;
        }
        */


        //reader.ReadStartElement("x" + orix + "y" + oriy + "z" + oriz);











        //xdocument dont delete not working yet
        //xdocument dont delete not working yet
        //xdocument dont delete not working yet

        /*var urlList = myXmlDoc.Root.Elements("x" + orix + "y" + oriy + "z" + oriz)
                                      .Select(x => (string)x)
                                      .ToArray();





        string str = string.Join("", urlList.Where(s => !string.IsNullOrEmpty(s)).Where(s => !s.Contains("\n")));


        if (str.Length > 0)
        {
            ia = str.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            if (ia.Length > 0)
            {

                //Console.WriteLine("x" + orix + "y" + oriy + "z" + oriz);

                //for (int i = 0; i < ia.Length; i++)
                //{
                //    Console.Write(ia[i]);
                //}
                //Console.WriteLine("\r\n" + "/length:"+ia.Length);



                themapdata = new chunkdata();

                themapdata.map = ia;
                //Console.WriteLine(benchmarkwatch.ElapsedTicks);

                return themapdata;
            }
            else
            {
                indexinarray = -1;
                var emptydata = new chunkdata();
                emptydata.map = null;

                return emptydata;
            }
        }
        else
        {
            indexinarray = -1;

            var emptydata = new chunkdata();
            emptydata.map = null;

            return emptydata;
        }*/

        //xdocument dont delete not working yet
        //xdocument dont delete not working yet
        //xdocument dont delete not working yet




        /*
        using (Stream stream = File.Open(fileName, FileMode.Open))
        {
            stream.Seek(bytesPerLine * (myLine - 1), SeekOrigin.Begin);
            using (StreamReader reader = new StreamReader(stream))
            {
                string line = reader.ReadLine();
            }
        }

        int lodint = 0;
        if (levelofdetail == 0)
        {
            lodint = 512;
        }
        else if (levelofdetail == 1)
        {
            lodint = 216;
        }
        else if (levelofdetail == 2)
        {
            lodint = 64;
        }



        Stopwatch benchmarkwatch = new Stopwatch();
        benchmarkwatch.Restart();





        //Console.WriteLine("sccslevelgen");


        int skipfirstlines = 2;
        int indexofchunk = 0;



        /*
        string startchunkinf = "<" + startchunkinfo + ">";
        string endchunkinf = "</" + startchunkinfo + ">";
        //var indexofchunkstart = thestring.IndexOf(startchunkinf);
        string theline = File.ReadLines(path).Skip(skipfirstlines + indexofchunk).Take(1).First();
        //theline = theline.Substring(startchunkinf.Length, lodint);

        string firstbracket = "<";
        string firstendbracket = ">";
        var indexoffirstchunktagend = theline.IndexOf(firstendbracket);

        var thestartstring = theline.Substring(0, indexoffirstchunktagend);
        //Console.WriteLine(thestartstring);

        string thex = "x";
        string they = "y";
        string thez = "z";

        var indexofx = thestartstring.IndexOf(thex);
        var indexofy = thestartstring.IndexOf(they);
        var indexofz = thestartstring.IndexOf(thez);

        var strthevaluex = thestartstring.Substring(indexofx + 1, (indexofy - indexofx) - 1);
        var strthevaluey = thestartstring.Substring(indexofy + 1, (indexofz - indexofy) - 1);
        var strthevaluez = thestartstring.Substring(indexofz + 1, (indexoffirstchunktagend - indexofz) - 1);

        //Console.WriteLine("/x:" + thevaluex + "/y:" + thevaluey + "/z:" + thevaluez);

        int thevaluex = 0;
        int thevaluey = 0;
        int thevaluez = 0;

        if (int.TryParse(strthevaluex, out thevaluex))
        {
            //Do something to correct the problem

        }
        if (int.TryParse(strthevaluey, out thevaluey))
        {
            //Do something to correct the problem

        }
        if (int.TryParse(strthevaluez, out thevaluez))
        {
            //Do something to correct the problem

        }

        var indexofchunkx = startchunkinfo.IndexOf(thex);
        var indexofchunky = startchunkinfo.IndexOf(they);
        var indexofchunkz = startchunkinfo.IndexOf(thez);

        var strthevaluechunkx = startchunkinfo.Substring(indexofchunkx + 1, (indexofchunky - indexofchunkx) - 1);
        var strthevaluechunky = startchunkinfo.Substring(indexofchunky + 1, (indexofchunkz - indexofchunky) - 1);
        var strthevaluechunkz = startchunkinfo.Substring(indexofchunkz + 1, (startchunkinfo.Length - indexofchunkz) - 1);

        int thevaluechunkx = 0;
        int thevaluechunky = 0;
        int thevaluechunkz = 0;

        if (int.TryParse(strthevaluechunkx, out thevaluechunkx))
        {
            //Do something to correct the problem

        }
        if (int.TryParse(strthevaluechunky, out thevaluechunky))
        {
            //Do something to correct the problem

        }
        if (int.TryParse(strthevaluechunkz, out thevaluechunkz))
        {
            //Do something to correct the problem

        }

        Console.WriteLine("/thevaluechunkx:" + thevaluechunkx + "/thevaluechunky:" + thevaluechunky + "/thevaluechunkz:" + thevaluechunkz);
        */





        /*int finalvalx = 0;
        int finalvaly = 0;
        int finalvalz = 0;

        int xindex = 0;
        int yindex = 0;
        int zindex = 0;

        for (xindex = thevaluex; xindex < (int)Math.Abs(thechunk.mainmaxx - thechunk.mainminx); xindex++)
        {
            if (xindex == thevaluechunkx)
            {
                finalvalx
            }

            for (yindex = thevaluey; yindex < (int)Math.Abs(thechunk.mainmaxy - thechunk.mainminy); yindex++)
            {
                for (zindex = thevaluez; zindex < (int)Math.Abs(thechunk.mainmaxz - thechunk.mainminz); zindex++)
                {

                }
            }
        }*/



        /*
        if (xindex < 0)
        {
            xindex *= -1;
            xindex = xindex + ((int)Math.Abs(thechunk.mainmaxx - thechunk.mainminx) - 1);
        }

        if (yindex < 0)
        {
            yindex *= -1;
            yindex = yindex + ((int)Math.Abs(thechunk.mainmaxy - thechunk.mainminy) - 1);
        }
        if (zindex < 0)
        {
            zindex *= -1;
            zindex = zindex + ((int)Math.Abs(thechunk.mainmaxz - thechunk.mainminz) - 1);
        }*/

        /*
        if (thevaluechunkx < 0)
        {
            thevaluechunkx *= -1;
            thevaluechunkx = thevaluechunkx + (sccsgraphicssec.currentsccsgraphicssec.incrementsfracx );
        }

        if (thevaluechunky < 0)
        {
            thevaluechunky *= -1;
            thevaluechunky = thevaluechunky + (sccsgraphicssec.currentsccsgraphicssec.incrementsfracy );
        }
        if (thevaluechunkz < 0)
        {
            thevaluechunkz *= -1;
            thevaluechunkz = thevaluechunkz + (sccsgraphicssec.currentsccsgraphicssec.incrementsfracz );
        }*/


        /*if (thevaluechunkx < 0)
        {
            thevaluechunkx *= -1;
            thevaluechunkx = thevaluechunkx - 1;
        }

        if (thevaluechunky < 0)
        {
            thevaluechunky *= -1;
            thevaluechunky = thevaluechunky - 1;
        }
        if (thevaluechunkz < 0)
        {
            thevaluechunkz *= -1;
            thevaluechunkz = thevaluechunkz - 1;
        }*/

        /*
        if (thecurrentchunkdata.indexinfilex < 0)
        {
            thecurrentchunkdata.indexinfilex *= -1;
            thecurrentchunkdata.indexinfilex = thecurrentchunkdata.indexinfilex +(3);
        }

        if (thecurrentchunkdata.indexinfiley < 0)
        {
            thecurrentchunkdata.indexinfiley *= -1;
            thecurrentchunkdata.indexinfiley = thecurrentchunkdata.indexinfiley + (3);
        }
        if (thecurrentchunkdata.indexinfilez < 0)
        {
            thecurrentchunkdata.indexinfilez *= -1;
            thecurrentchunkdata.indexinfilez = thecurrentchunkdata.indexinfilez + (3);
        }
        */

        //Console.WriteLine("thecurrentchunkdata.indexinfilex:" + thecurrentchunkdata.indexinfilex + "/thecurrentchunkdata.indexinfiley" + thecurrentchunkdata.indexinfiley + "/thecurrentchunkdata.indexinfilez:" + thecurrentchunkdata.indexinfilez);




        //Console.WriteLine("sccsgraphicssec.currentsccsgraphicssec.incrementsfracx:" + (thechunk.mainmaxy - thechunk.mainminy));
        //int indexofchunkinfile = thevaluechunkx + sccsgraphicssec.currentsccsgraphicssec.incrementsfracx * (thevaluechunky + sccsgraphicssec.currentsccsgraphicssec.incrementsfracy * thevaluechunkz);
        // int indexofchunkinfile = thecurrentchunkdata.indexinfilez + sccsgraphicssec.currentsccsgraphicssec.incrementsfracx * (thecurrentchunkdata.indexinfiley + sccsgraphicssec.currentsccsgraphicssec.incrementsfracy * thecurrentchunkdata.indexinfilex);

        /*Console.WriteLine("maxx:" + sccsgraphicssec.currentsccsgraphicssec.incrementsfracx + "/maxy:" + sccsgraphicssec.currentsccsgraphicssec.incrementsfracy + "/maxz:" + sccsgraphicssec.currentsccsgraphicssec.incrementsfracz);
        Console.WriteLine("startchunkinfo" + startchunkinfo);
        //Console.WriteLine("indexofchunkinfile" + indexofchunkinfile);                 

        //if (indexofchunkinfile < sccsgraphicssec.currentsccsgraphicssec.incrementsfracx * sccsgraphicssec.currentsccsgraphicssec.incrementsfracy * sccsgraphicssec.currentsccsgraphicssec.incrementsfracz)
        {
            /*int originallinepadding = thecurrentchunkdata.linepaddingx + thecurrentchunkdata.linepaddingy + thecurrentchunkdata.linepaddingz;

            string thex = "x";
            string they = "y";
            string thez = "z";

            var indexofx = startchunkinfo.IndexOf(thex);
            var indexofy = startchunkinfo.IndexOf(they);
            var indexofz = startchunkinfo.IndexOf(thez);

            var strthevaluex = startchunkinfo.Substring(indexofx + 1, (indexofy - indexofx) - 1);
            var strthevaluey = startchunkinfo.Substring(indexofy + 1, (indexofz - indexofy) - 1);
            var strthevaluez = startchunkinfo.Substring(indexofz + 1, (indexofz-1) - 1);


            int thevaluex = 0;
            int thevaluey = 0;
            int thevaluez = 0;

            if (int.TryParse(strthevaluex, out thevaluex))
            {

            }
            if (int.TryParse(strthevaluey, out thevaluey))
            {

            }
            if (int.TryParse(strthevaluez, out thevaluez))
            {

            }
            //Console.WriteLine("/x:" + thevaluex + "/y:" + thevaluey + "/z:" + thevaluez);




            //benchmarkwatch.Restart();

            int linepadding = 0;// thecurrentchunkdata.linepaddingx + thecurrentchunkdata.linepaddingy + thecurrentchunkdata.linepaddingz;

            /*if (sccslevelgen.minw >= 10 && sccslevelgen.minw <= 100)
            {
                linepadding = 541;
            }
            else if (sccslevelgen.minw > 100)
            {
                linepadding = 549;
            }
            else if (sccslevelgen.minw < 10)
            {
                linepadding = 537;

            }
            linepadding = lodint;

            //Console.WriteLine("padding:" +( 512 + 23 + linepadding));


            //string startchunkinf = "<" + startchunkinfo + ">";
            //string instructions = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>  \r\n<root>";
            //string instructions = "<?xml version=" + "1.0" + " encoding=" + "UTF-8" + "?" + ">";
            //string processinginstructions = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
            //string elementroot = "<root>";
            //(processinginstructions.Length + elementroot.Length)

            //var thestring1 = thestring.Substring(((512) * indexofchunkinfile) + startchunkinf.Length, lodint); //537
            var thestring1 = thechunk.str.Substring(((linepadding) * (thecurrentchunkdata.counterofindexes)), lodint); //537

            /*if (orix == 0 && oriy == 0 && oriz == 0 && thecurrentchunkdata.posmainx == 0 && thecurrentchunkdata.posmainy == 0 && thecurrentchunkdata.posmainz == 0)
            {
                Console.WriteLine("thestring1:" + thestring1);
            }*/
        /*
        if (orix == 0 && oriy == 0 && oriz == 0 && thechunk.posmainx == 0 && thechunk.posmainy == 0 && thechunk.posmainz == 0)
        {
            Console.WriteLine("thestring1:" + thestring1);
        }
        */

        //Console.WriteLine("/thecurrentchunkdata.counterofindexes:" + thecurrentchunkdata.counterofindexes);


        //Console.WriteLine(linepadding);

        //Console.WriteLine(thecurrentchunkdata.counterofindexes + "/indexofchunkinfile:" + indexofchunkinfile);

        //Console.WriteLine("thestring1:" + thestring1.Length);


        /*
        //using (Stream stream = File.Open(path, FileMode.Open))
        {
            /*stream.Seek(bytesPerLine * ((skipfirstlines + indexofchunkinfile) - 1), SeekOrigin.Begin);
            using (StreamReader readeroffile = new StreamReader(stream))
            {
                string line = reader.ReadLine();
            }
            string astring = "";



            //stream.Seek(bytesPerLine * ((0 + indexofchunkinfile) - 1), SeekOrigin.Begin);
            //var sr = new StreamReader(thestream);
            //using (var sr = new StreamReader(thestream))
            /*{
                for (int i = 0; i < (0 +  indexofchunkinfile) - 1; i++)
                {
                    thechunk.thestreamreader.ReadLine();
                }
                //return sr.ReadLine();
                astring = thechunk.thestreamreader.ReadLine();
            }
            //Console.WriteLine(astring);
            //sr.Close();
            //sr.Dispose();

            //thechunk.thestreamreader.pos
            thestream.Seek(0, SeekOrigin.End);


            //Console.WriteLine(benchmarkwatch.ElapsedTicks);

        }*/

        //Console.WriteLine(benchmarkwatch.ElapsedTicks);




        //var thestring2 = thestring.Substring(instructions.Length + (indexofchunkinfile * (23 + lodint)), lodint + 7);

        //Console.WriteLine("thestring2" + thestring2);
        //Console.WriteLine("thestring1" + thestring1);



        //benchmarkwatch.Restart();

        //string thechunkstring = File.ReadLines(path).Skip(skipfirstlines + (indexofchunkinfile)).Take(1).First();

        //string thechunkstring = "";
        /*using (Stream stream = File.Open(path, FileMode.Open))
        {
            /*stream.Seek(bytesPerLine * ((skipfirstlines + indexofchunkinfile) - 1), SeekOrigin.Begin);
            using (StreamReader readeroffile = new StreamReader(stream))
            {
                string line = reader.ReadLine();
            }
            using (var sr = new StreamReader(path))
            {
                for (int i = 1; i < line; i++)
                    sr.ReadLine();
                return sr.ReadLine();
            }
        }*/

        /*try
        {
            var sr = new StreamReader(path);
            //{
            for (int i = 0; i < 1; i++)
            {
                sr.ReadLine();
            }
            thechunkstring = sr.ReadLine();
            sr.Close();
            sr.Dispose();
        }
        catch
        {

        }

        // }







        //Console.WriteLine("thechunkstring" + thechunkstring);


        //string startchunkinf = "<" + startchunkinfo + ">";
        //string endchunkinf = "</" + startchunkinfo + ">";
        //https://stackoverflow.com/questions/7935945/string-indexof-performance
        //var indexofchunkstart = thechunkstring.IndexOf(startchunkinf, 0, StringComparison.Ordinal);
        //var indexofchunkstart = thestring.IndexOf(startchunkinf);

        // Console.WriteLine("indexofchunkstart" + indexofchunkstart);



        //int indexofchunkstart = 0;
        //thestring = thestring.Substring(instructions.Length + indexofchunkstart + startchunkinf.Length, lodint);

        //var thestring1 = thestring0.Substring(0 + startchunkinf.Length, lodint);


        //Console.WriteLine("thestring1:" + thestring1.Length);

        ia = new int[lodint];
        int counter = 0;
        int hasbroken = 0;

        foreach (var strdata in thestring1)
        {
            int bar;
            if (int.TryParse(strdata.ToString(), out bar))
            {
                //Do something to correct the problem
                ia[counter] = bar;
            }
            counter++;

            /*else
            {
                Console.WriteLine("bar:" + bar);
                hasbroken = 1;
                break;
            }
        }

        /*if (hasbroken == 1)
        {
            ia = new int[lodint];

        }


        //Console.WriteLine(benchmarkwatch.ElapsedTicks);


        themapdata = new chunkdata();

        themapdata.map = ia;



    }
    /*else
    {

        ia = new int[lodint];
        themapdata = new chunkdata();

        themapdata.map = ia;

    }*/









        /*

        Console.WriteLine("x" + orix + "y" + oriy + "z" + oriz);

        for (int i = 0; i < ia.Length; i++)
        {
            Console.Write(ia[i]);
        }
        Console.WriteLine("\r\n" + "/length:" + ia.Length);

        */








        // Console.WriteLine("/x:" + indexofx + "/y:" + indexofy + "/z:" + indexofz);



        //int firstchunkinfilex = thechunk.realposmainx;
        //int firstchunkinfiley = thechunk.realposmainy;
        //int firstchunkinfilez = thechunk.realposmainz;




        /*
        int indexx = (int)Math.Abs(orix);
        int indexy = (int)Math.Abs(oriy);
        int indexz = (int)Math.Abs(oriz);
        */
        //int indexinsclevelgenmap = x + somewidth * (y + someheight * z);


        /*for (int xi = 0; xi < (int)Math.Abs(orix); xi++)
        {
            for (int yi = 0; yi < (int)Math.Abs(oriy); yi++)
            {
                for (int zi = 0; zi < (int)Math.Abs(oriz); zi++)
                {

                }
            }
        }
        */


















        //working 200 ticks++
        //working
        //working
        //working
        //working
        //working

        /*string startchunkinf = "<" + startchunkinfo + ">";
        string endchunkinf = "</" + startchunkinfo + ">";
        //https://stackoverflow.com/questions/7935945/string-indexof-performance
        var indexofchunkstart = thestring.IndexOf(startchunkinf,0, StringComparison.Ordinal);
        //var indexofchunkstart = thestring.IndexOf(startchunkinf);

        //Console.WriteLine("indexofchunkstart" + indexofchunkstart);

        //int indexofchunkstart = 0;
        //thestring = thestring.Substring(instructions.Length + indexofchunkstart + startchunkinf.Length, lodint);

        thestring = thestring.Substring(indexofchunkstart + startchunkinf.Length, lodint);
        //ia = thestring.Trim().Select(n => Convert.ToInt32(n)).ToArray();
        //ia = thestring.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
        //ia = thestring //.Trim()
        //     .ToCharArray()
        //     .Where(c => Char.IsNumber(c))
        //     .Select(c => Convert.ToInt32(c.ToString()))
        //     .ToArray();

        ia = new int[lodint];
        int counter = 0;
        foreach (var strdata in thestring)
        {
            int bar;
            if (int.TryParse(strdata.ToString(), out bar))
            {
                //Do something to correct the problem
                ia[counter] = bar;
                counter++;
            }                  
        }
        //working
        //working
        //working
        //working
        //working
        //working

































        /*
        string instructions = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>  \r\n<root>";

        string startchunkinf = "<" + startchunkinfo + ">";
        string endchunkinf = "</" + startchunkinfo + ">";*/
        //var indexofchunkstart = thestring.IndexOf(startchunkinf);
        //Console.WriteLine("indexofchunkstart" + indexofchunkstart);

        /*int indexofchunkstart = 0;
        var firstentry = thestring.Substring(instructions.Length + indexofchunkstart, 11); // 11 to be extended to the maxx maxy maxz and counting the negative sign.

        //Console.WriteLine(firstentry);

        int entryindex = firstentry.IndexOf(">");

        var finalfirstentry = firstentry.Substring(0, entryindex + 1);
        //Console.WriteLine(finalfirstentry);*/





















        //int firstchunkinfilex = thechunk.realposmainx;
        //int firstchunkinfiley = thechunk.realposmainy;
        //int firstchunkinfilez = thechunk.realposmainz;


        /*
        int indexx = (int)Math.Abs(orix);
        int indexy = (int)Math.Abs(oriy);
        int indexz = (int)Math.Abs(oriz);*/

        //int indexinsclevelgenmap = x + somewidth * (y + someheight * z);


        /*for (int xi = 0; xi < (int)Math.Abs(orix); xi++)
        {
            for (int yi = 0; yi < (int)Math.Abs(oriy); yi++)
            {
                for (int zi = 0; zi < (int)Math.Abs(oriz); zi++)
                {

                }
            }
        }*/







        /*thechunk.realposmainx
        thechunk.realposmainy
        thechunk.realposmainz*/



        //string instructions = "<?xml version=" + "1.0" + " encoding=" + "UTF-8" + "?" + ">";
        //string rootelement = "<root>";


        //working 200 ticks++
        //working
        //working
        //working
        //working
        //working

        /*string startchunkinf = "<" + startchunkinfo + ">";
        string endchunkinf = "</" + startchunkinfo + ">";
        //https://stackoverflow.com/questions/7935945/string-indexof-performance
        var indexofchunkstart = thestring.IndexOf(startchunkinf,0, StringComparison.Ordinal);
        //var indexofchunkstart = thestring.IndexOf(startchunkinf);

        //Console.WriteLine("indexofchunkstart" + indexofchunkstart);

        //int indexofchunkstart = 0;
        //thestring = thestring.Substring(instructions.Length + indexofchunkstart + startchunkinf.Length, lodint);

        thestring = thestring.Substring(indexofchunkstart + startchunkinf.Length, lodint);
        //ia = thestring.Trim().Select(n => Convert.ToInt32(n)).ToArray();
        //ia = thestring.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
        //ia = thestring //.Trim()
        //     .ToCharArray()
        //     .Where(c => Char.IsNumber(c))
        //     .Select(c => Convert.ToInt32(c.ToString()))
        //     .ToArray();

        ia = new int[lodint];
        int counter = 0;
        foreach (var strdata in thestring)
        {
            int bar;
            if (int.TryParse(strdata.ToString(), out bar))
            {
                //Do something to correct the problem
                ia[counter] = bar;
                counter++;
            }                  
        }
        //working
        //working
        //working
        //working
        //working
        //working



        // Console.WriteLine(thestring);

        //Console.WriteLine(benchmarkwatch.ElapsedTicks);



        //ia = new int[lodint];





        //var test = Int32.Parse(Convert.ToChar(strdata));
        //ia = line.Select(strdata => Int32.Parse(strdata)).ToArray();
        //int i = 0;
        //var a = strdata.SelectMany(s => int.TryParse(s, out i) ? new[] { i } : new int[0]).ToArray();




        //string instructions = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>  \r\n<root>";
        //working 200 ticks++
        //working
        //working
        //working
        //working
        //working
        /*string startchunkinf = "<" + startchunkinfo + ">";
        string endchunkinf = "</" + startchunkinfo + ">";
        var indexofchunkstart = thestring.IndexOf(startchunkinf);
        //Console.WriteLine("indexofchunkstart" + indexofchunkstart);
        thestring = thestring.Substring(indexofchunkstart + startchunkinf.Length, lodint);
        //ia = thestring.Trim().Select(n => Convert.ToInt32(n)).ToArray();
        //ia = thestring.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
        ia = thestring //.Trim()
             .ToCharArray()
             .Where(c => Char.IsNumber(c))
             .Select(c => Convert.ToInt32(c.ToString()))
             .ToArray();*/
        //working
        //working
        //working
        //working
        //working
        //working


















        //NOT working LAG
        //NOT working LAG
        //NOT working LAG    
        /*if (thestream != null )
        {
            //Console.WriteLine("! null");
        }*/

        /*string line = "";

        using (Stream stream = File.Open(path, FileMode.Open))
        {
            stream.Seek(skipfirstline + indexofchunk, SeekOrigin.Begin);
            using (StreamReader readers = new StreamReader(stream))
            {
                line = readers.ReadLine();
            }
        }*/
        /*string line = "";
        thestream.Seek(skipfirstline + indexofchunk, SeekOrigin.Begin);
        StreamReader readers = new StreamReader(thestream);
        line = readers.ReadLine();*/
        /*using (StreamReader readers = new StreamReader(thestream))
        {
            line = readers.ReadLine();
        }*/
        /*Console.WriteLine(line);
        readers.Close();



        ia = new int[lodint];*/
        //Console.WriteLine(thestring);
        //NOT working LAG
        //NOT working LAG
        //NOT working LAG    




        /* using ()
         {
             line = readers.ReadLine();
         }



         Console.WriteLine(line);*/



        //working 200 ticks++
        //working
        //working
        //working
        //working
        //working
        /*string startchunkinf = "<" + startchunkinfo + ">";
        string endchunkinf = "</" + startchunkinfo + ">";
        var indexofchunkstart = thestring.IndexOf(startchunkinf);
        //Console.WriteLine("indexofchunkstart" + indexofchunkstart);
        thestring = thestring.Substring(indexofchunkstart + startchunkinf.Length, lodint);
        //ia = thestring.Trim().Select(n => Convert.ToInt32(n)).ToArray();
        //ia = thestring.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
        ia = thestring //.Trim()
             .ToCharArray()
             .Where(c => Char.IsNumber(c))
             .Select(c => Convert.ToInt32(c.ToString()))
             .ToArray();*/
        //working
        //working
        //working
        //working
        //working
        //working


        //working LAG //1000 ticks++
        //working LAG
        //working LAG               
        /* string startchunkinf = "<" + startchunkinfo + ">";
         string endchunkinf = "</" + startchunkinfo + ">";
         //var indexofchunkstart = thestring.IndexOf(startchunkinf);
         string theline = File.ReadLines(path).Skip(skipfirstline + indexofchunk).Take(1).First();
         theline = theline.Substring(startchunkinf.Length, lodint);


         ia = theline //.Trim()
             .ToCharArray()
             .Where(c => Char.IsNumber(c))
             .Select(c => Convert.ToInt32(c.ToString()))
             .ToArray();*/
        //working LAG
        //working LAG
        //working LAG


        //working LAG //1000 ticks++
        //working LAG
        //working LAG 
        /*string startchunkinf = "<" + startchunkinfo + ">";
        string endchunkinf = "</" + startchunkinfo + ">";
        //var indexofchunkstart = thestring.IndexOf(startchunkinf);
        string theline = ienumstring.Skip(skipfirstline + indexofchunk).Take(1).First();
        theline = theline.Substring(startchunkinf.Length, lodint);


        ia = theline //.Trim()
            .ToCharArray()
            .Where(c => Char.IsNumber(c))
            .Select(c => Convert.ToInt32(c.ToString()))
            .ToArray();*/
        //working LAG //1000 ticks++
        //working LAG
        //working LAG 






        //ia = new int[lodint];

        /* IEnumerable<string>  test = File.ReadLines(path);

         var endvalue = test.Skip(skipfirstline + indexofchunk).Take(1).First();
        */





        //Console.WriteLine(benchmarkwatch.ElapsedTicks);



        /*thestream.Seek(skipfirstline + indexofchunk, SeekOrigin.Begin);
        StreamReader readers = new StreamReader(thestream);
        line = readers.ReadLine();

        Console.WriteLine();*/











        /*foreach (var strdata in thestring)
        {
            //var test = Int32.Parse(Convert.ToChar(strdata));
            //ia = line.Select(strdata => Int32.Parse(strdata)).ToArray();
            //int i = 0;
            //var a = strdata.SelectMany(s => int.TryParse(s, out i) ? new[] { i } : new int[0]).ToArray();


        }*/




        //Console.WriteLine("thestring:" + thestring);

        /*//https://stackoverflow.com/questions/1262965/how-do-i-read-a-specified-line-in-a-text-file
        string line = File.ReadLines(path).Skip(skipfirstline + indexofchunk).Take(1).First();


        //Console.WriteLine("line:" + line);

        string startchunkinf = "<" + startchunkinfo + ">";
        string endchunkinf = "</" + startchunkinfo + ">";


        //line.Length

        line = line.Substring(startchunkinf.Length, lodint);*/


        //ia = line.Trim().Select(n => Convert.ToInt32(n)).ToArray();

        /*Console.WriteLine("x" + orix + "y" + oriy + "z" + oriz);

        for (int i = 0; i < ia.Length; i++)
        {
            Console.Write(ia[i]);
        }
        Console.WriteLine("\r\n" + "/length:" + ia.Length);*/




        /*int i = 0;
        var a = line.SelectMany(s => int.TryParse(s, out i) ? new[] { i } : new int[0]).ToArray();
        */

        /*
        ia = line //.Trim()
               .ToCharArray()
               .Where(c => Char.IsNumber(c))
               .Select(c => Convert.ToInt32(c.ToString()))
               .ToArray();*/

        //Console.WriteLine("line:" + "/length:" + line.Length + "///" + line);




        //ia = line.Select(s => int.Parse(s)).ToArray();




        //var a = line.SelectMany(s => int.TryParse(s, out i) ? new[] { i } : new int[0]).ToArray();

        /*var iab = (byte[])line.Select(n => Convert.ToByte(n)).ToArray();

        Console.WriteLine("x" + orix + "y" + oriy + "z" + oriz);

        for (int i = 0; i < iab.Length; i++)
        {
            Console.Write(iab[i]);
        }
        Console.WriteLine("\r\n" + "/length:" + iab.Length);
        */

        //var ia = line.Select(n => Convert.ToInt32(n)).ToArray();

        /* Console.WriteLine("x" + orix + "y" + oriy + "z" + oriz);

         for (int i = 0; i < ia.Length; i++)
         {
             Console.Write(ia[i]);
         }
         Console.WriteLine("\r\n" + "/length:" + ia.Length);





        //Console.WriteLine(benchmarkwatch.ElapsedTicks);





        return themapdata;




        /*
        var urlList = myXmlDoc.Root.Elements("x" + orix + "y" + oriy + "z" + oriz);

        foreach (var strdata in urlList)
        {
            var str = strdata.Value;

            if (str.Length > 0)
            {

                //int i = 0;
                //var a = str.SelectMany(s => int.TryParse(s, out i) ? new[] { i } : new int[0]).ToArray();

                //str.Select(s => int.Parse(s)).ToArray();
                //ia = str.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                //ia = new int[str.Length];
                ia = str //.Trim()
                .ToCharArray()
                .Where(c => Char.IsNumber(c))
                .Select(c => Convert.ToInt32(c.ToString()))
                .ToArray();


                //ia = str.Split().Select(n => Convert.ToInt32(n)).ToArray();

                //var test = str.Select(n => Convert.ToInt32(n));//.ToArray()
                //int counter = 0;
                //foreach (var valint in test)
                //{
                //    Console.WriteLine();
                //    ia[counter] = valint;
                //    counter++;
                //}

                //ia = str.Trim().Select(n => Convert.ToInt32(n)).ToArray();


                if (ia.Length > 0)
                {
                    //Console.WriteLine("x" + orix + "y" + oriy + "z" + oriz);

                    //for (int i = 0; i < ia.Length; i++)
                    //{
                   //     Console.Write(ia[i]);
                    //}
                    //Console.WriteLine("\r\n" + "/length:"+ia.Length);


                    themapdata = new chunkdata();

                    themapdata.map = ia;

                    //Console.WriteLine(benchmarkwatch.ElapsedTicks);

                    return themapdata;
                }
                else
                {
                    indexinarray = -1;
                    var emptydata = new chunkdata();
                    emptydata.map = null;

                    return emptydata;
                }
            }
            else
            {
                indexinarray = -1;

                var emptydata = new chunkdata();
                emptydata.map = null;

                return emptydata;
            }

        }
        */

        /*
        indexinarray = -1;

        var emptydata = new chunkdata();
        emptydata.map = null;

        return emptydata;*/


        //ia = new int[str.Length];
        //ia = str.Trim().Select(n => Convert.ToInt32(n)).ToArray();

        //Console.WriteLine(str);


        /*
        for (int c = 0; c < str.Length; c++)
        {
            //ia[c] = Convert.ToByte(str[c]);

            //Console.WriteLine(str[c]);
            int theintval;
            //theintval =  Int32.Parse(Convert.ToChar(str[c]));
            theintval = Int32.Parse(str[c]);
            ia[c] = theintval;
        }*/

        /*
        int thecount = 0;
        foreach (var cstr in str)
        {
            int theintval;
            ia[thecount] = int.Parse(cstr);
            thecount++;
        }*/
        /*
        var provider = new CultureInfo("en-US");
        var styles = NumberStyles.Integer;
        */
        /*
        for (int c = 0; c < str.Length; c++)
        {
            //ia[c] = Convert.ToByte(str[c]);

            //Console.WriteLine(str[c]);
            int theintval;
            bool success = int.TryParse(str[c], styles, provider, out int number);
            if (success)
            {
                ia[c] = number;

            }
        }*/

        /*
        int thecount = 0;
        foreach (var cstr in str)
        {
            int theintval;
            //ia[thecount] = int.Parse(cstr);
            //Console.WriteLine(cstr);

            /*bool success = int.TryParse(str, styles, provider, out int number);
            if (success)
            {
                ia[thecount] = number;

            }
            thecount++;

        }*/



        //xdocument dont delete not working yet
        //xdocument dont delete not working yet
        //xdocument dont delete not working yet
        /*
        var urlList = myXmlDoc.Root.Elements("x" + orix + "y" + oriy + "z" + oriz)
                                      .Select(x => (string)x)
                                      .ToArray();

        string str = string.Join("", urlList.Where(s => !string.IsNullOrEmpty(s)).Where(s => !s.Contains("\n")));


        if (str.Length > 0)
        {
            ia = str.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            if (ia.Length > 0)
            {

                //Console.WriteLine("x" + orix + "y" + oriy + "z" + oriz);

                //for (int i = 0; i < ia.Length; i++)
                //{
                //    Console.Write(ia[i]);
                //}
                //Console.WriteLine("\r\n" + "/length:"+ia.Length);



                themapdata = new chunkdata();

                themapdata.map = ia;
                Console.WriteLine(benchmarkwatch.ElapsedTicks);

                return themapdata;
            }
            else
            {
                indexinarray = -1;
                var emptydata = new chunkdata();
                emptydata.map = null;

                return emptydata;
            }
        }
        else
        {
            indexinarray = -1;

            var emptydata = new chunkdata();
            emptydata.map = null;

            return emptydata;
        }*/

        //xdocument dont delete not working yet
        //xdocument dont delete not working yet
        //xdocument dont delete not working yet

        /*
    indexinarray = -1;

    var emptydata = new chunkdata();
    emptydata.map = null;

    return emptydata;
    */









































        //themapdata.map = 

        //LOADING CHUNK BACK INTO MEMORY
        //LOADING CHUNK BACK INTO MEMORY

        //writetofilecounter = 0;

        /*
        XmlDataDocument xmldoc = new XmlDataDocument();
        XmlNodeList xmlnode;
        int i = 0;
        string str = null;
        FileStream fs = new FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        xmldoc.Load(fs);
        xmlnode = xmldoc.GetElementsByTagName("x" + orix + "y" + oriy + "z" + oriz);
        //xmlnode[]
        */

        //XDocument thedoc = new XDocument(path);

        //XDocument xml = XDocument.Load(path);








        //xdocument dont delete not working yet
        //xdocument dont delete not working yet
        //xdocument dont delete not working yet

        /*var urlList = myXmlDoc.Root.Elements("x" + orix + "y" + oriy + "z" + oriz)
                                      .Select(x => (string)x)
                                      .ToArray();

        string str = string.Join("", urlList.Where(s => !string.IsNullOrEmpty(s)).Where(s => !s.Contains("\n")));


        if (str.Length > 0)
        {
            ia = str.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            if (ia.Length > 0)
            {
                ///Console.WriteLine("x" + orix + "y" + oriy + "z" + oriz);

                //for (int i = 0; i < ia.Length; i++)
                //{
                //    Console.Write(ia[i]);
                //}
                //Console.WriteLine("\r\n" + "/length:"+ia.Length);



                themapdata = new chunkdata();

                themapdata.map = ia;

                return themapdata;
            }
            else
            {
                indexinarray = -1;
                var emptydata = new chunkdata();
                emptydata.map = null;

                return emptydata;
            }
        }
        else
        {
            indexinarray = -1;

            var emptydata = new chunkdata();
            emptydata.map = null;

            return emptydata;
        }*/

        //xdocument dont delete not working yet
        //xdocument dont delete not working yet
        //xdocument dont delete not working yet







        //str = string.Join("", str.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));


        //Console.WriteLine(str);


        //string str = string.Join(" ", urlList);

        /*
        string replacement = "";
        sWhitespace.Replace(str, replacement);*/


        /*
        for (int i = 0; i < ia.Length; i++)
        {
            Console.WriteLine(ia[i]);
        }
        */
        //Console.WriteLine(str);

        //urlList.Join();

        //str = str.Replace(" ", "");

        //urlList
        //urlList.trim
        /*ia = new int[urlList.Length];

        for (int i = 0; i < urlList.Length; i++)
        {
            if (urlList[i] != Convert.ToString(' '))
            {
                //ia[i] = Convert.ToChar(urlList[i]);
            }
            //ia[i]= Convert.ToInt32(urlList[i]);
            //Console.WriteLine(urlList[i]);
        }*/

        /*
        ia = str.Select(n => Convert.ToInt32(n)).ToArray();

        themapdata.map = ia;
        return themapdata;*/

        /*
        ia = str.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
        Console.WriteLine(ia);*/







        /*
        var urlList = thedoc.Root.Elements(path)
                   .Elements("x" + orix + "y" + oriy + "z" + oriz)
                   .Select(x => (string)x)
                   .ToArray();

        Console.WriteLine(urlList);*/


        /*
        XElement root = new XElement("x" + orix + "y" + oriy + "z" + oriz, "content");
        Console.WriteLine(root);*/




        //xmltextreader dont delete not working yet
        //xmltextreader dont delete not working yet
        //xmltextreader dont delete not working yet
        //https://stackoverflow.com/questions/18891207/how-to-get-value-from-a-specific-child-element-in-xml-using-xmlreader
        //var path = @"C:\Users\steve\Desktop\#chunkmaps\" + "chunkmap" + writetofilecounter + ".xml";
        /*reader = new XmlTextReader(path);

        if (reader.ReadToDescendant("x" + orix + "y" + oriy + "z" + oriz))
        {
            //Console.WriteLine("test");
            reader.Read();//this moves reader to next node which is text 
            var result = reader.Value; //this might give value than 

            //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
            ia = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

            //for (int by = 0; by < ia.Length; by++)
            //{
            //    //Console.WriteLine(ia[by]);
            //}
            themapdata = new chunkdata();
            themapdata.map = ia;
            //reader.Close();
            //reader.ResetState();

            //ia = null;
            return themapdata;
        }*/

        //xmltextreader dont delete not working yet
        //xmltextreader dont delete not working yet
        //xmltextreader dont delete not working yet




        //filestream dont delete not working yet
        //filestream dont delete not working yet
        //filestream dont delete not working yet

        //https://www.codeproject.com/Tips/682245/NanoXML-Simple-and-fast-XML-parser - not using nanoxml.
        /* fs = new FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
         datafs = new byte[fs.Length];
         var test = fs.Read(datafs, 0, (int)fs.Length);
         //var test1 = Convert.ToInt32(datafs);
         //https://stackoverflow.com/questions/11654562/how-to-convert-byte-array-to-string
         string str = System.Text.Encoding.UTF8.GetString(datafs, 0, datafs.Length);*/


        /*
        string findstartnode = "<" + "x" + orix + "y" + oriy + "z" + oriz + ">";
        string findendnode = "</" + "x" + orix + "y" + oriy + "z" + oriz + ">";

        int pFrom = str.IndexOf(findstartnode);
        int pTo = pFrom + (512);// str.LastIndexOf(findendnode) + 1;

        //str = str.Split(findstartnode.ToCharArray()); ///, findendnode.ToCharArray()

        string result = str.Substring(pFrom + findstartnode.Length, pFrom + findstartnode.Length + 512);

        Console.WriteLine(result);


        //string str = string.Join("", urlList.Where(s => !string.IsNullOrEmpty(s)).Where(s => !s.Contains("\n")));
        /*
        if (str.Length > 0)
        {
            ia = str.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            themapdata = new chunkdata();
            themapdata.map = ia;
            return themapdata;
        }

        fs.Close();
        fs.Dispose();

        datafs = null;*/
        //filestream dont delete not working yet
        //filestream dont delete not working yet
        //filestream dont delete not working yet







        /*
        var test = myXmlDoc.Descendants().SingleOrDefault(p => p.Name.LocalName == "x" + orix + "y" + oriy + "z" + oriz);

        if (test!= null)
        {
            Console.WriteLine(test.Value);

        }*/

        //LOADING CHUNK BACK INTO MEMORY
        //LOADING CHUNK BACK INTO MEMORY

        //https://www.codeproject.com/Tips/682245/NanoXML-Simple-and-fast-XML-parser - not using nanoxml.
        /*fs = new FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
        datafs = new byte[fs.Length];
        var test = fs.Read(datafs, 0, (int)fs.Length);
        //var test1 = Convert.ToInt32(datafs);
        //https://stackoverflow.com/questions/11654562/how-to-convert-byte-array-to-string
        string s = System.Text.Encoding.UTF8.GetString(datafs, 0, datafs.Length);
        fs.Close();
        fs.Dispose();

        datafs = null;

        //Console.WriteLine(s);


        */

        //Stream output = File.OpenRead(path);
        //datafs = new byte[output.Length];

        //output.Close();
        //output.Dispose();
        //string s = System.Text.Encoding.UTF8.GetString(datafs, 0, datafs.Length);

        /*int lodint = 0;
        if (levelofdetail == 0)
        {
            lodint = 512;
        }
        else if (levelofdetail == 1)
        {
            lodint = 216;
        }
        else if (levelofdetail == 2)
        {
            lodint = 64;
        }


        if (thestring.Length > 0)
        {

            string findstartnode = "<" + "x" + orix + "y" + oriy + "z" + oriz + ">";
            string findendnode = "</" + "x" + orix + "y" + oriy + "z" + oriz + ">";

            /*var f = thestring.GroupBy(x => x)
                             .Select(g => new { Value = g.Key, Count = g.Count() })
                             .Where(s => s.Value == "FF");




            /*int pFrom = thestring.IndexOf(findstartnode);
            int pTo = pFrom  + findstartnode.Length + (lodint);// str.LastIndexOf(findendnode) + 1;

            //str = str.Split(findstartnode.ToCharArray()); ///, findendnode.ToCharArray()

            //string result = thestring.Substring(pFrom + findstartnode.Length, lodint);
            //Console.WriteLine(result);                   
        }*/






















        /*
        Stream output = File.OpenRead(path);

        var test = output.Read(datafs, 0, (int)output.Length);
        //ia = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
        output.Close();
        output.Dispose();
        string s = System.Text.Encoding.UTF8.GetString(datafs, 0, datafs.Length);
        Console.WriteLine(s);

        datafs = null;*/
        //int[] i = BitConverter.ToInt32(datafs, 0);







        /*string strData = Encoding.UTF8.GetString(data);
        ia = strData.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
        themapdata.map = ia;
        return themapdata;*/


        /*reader = new XmlTextReader(path);

        if (reader.ReadToDescendant("x" + orix + "y" + oriy + "z" + oriz))
        {
            string strData = Encoding.UTF8.GetString(data);
            ia = strData.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();
            themapdata.map = ia;
            return themapdata;
        }
        else
        {
            indexinarray = -1;
            return new chunkdata();
        }*/


















        //Console.WriteLine(strData);

        //indexinarray = -1;
        //return new chunkdata();


        //Console.WriteLine("/length:" + chunkdata[0].Length + "/index:" + indexinarray);


        /*if (indexinarray >= 0 && indexinarray < chunkdata[levelofdetail][0].Length)
        {
            //if (levelofdetail == 1)
            {
                //Console.WriteLine("checking for existing map");
                if (chunkdata[levelofdetail][0][indexinarray].map != null)
                {
                    //Console.WriteLine("map != null");
                    return chunkdata[levelofdetail][0][indexinarray];
                }
                else
                {
                    //Console.WriteLine("map == null");
                    indexinarray = -1;
                    return new chunkdata();
                }

                //return arraychunkdatalod0[indexinsclevelgenmap].arraychunkvertslod0;

            }
            /*else if (levelofdetail == 2)
            {
                return arraychunkdatalod1[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod1;
            }
            else if (levelofdetail == 3)
            {
                return arraychunkdatalod2[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod2;
            }
            else if (levelofdetail == 4)
            {
                return arraychunkdatalod3[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod3;
            }
            else if (levelofdetail == 5)
            {
                return arraychunkdatalod4[arrayofindexes[indexinsclevelgenmap]].arraychunkvertslod4;
            }
        }

        indexinarray = -1;



        return new chunkdata();
    }
}
indexinarray = -1;
return new chunkdata();
}*/

        public sccslevelgen()
        {
            currentlevelgen = this;
        }

        public static void calculateleveldimensions()
        {

            if (loadmap == 0)
            {

                /*
                int minw = 50;
                int minh = 20;
                int mind = 50;

                int maxw = 50;
                int maxh = 20;
                int maxd = 50;*/





                /*int minw = 25;
                int minh = 9;
                int mind = 25;

                int maxw = 50;
                int maxh = 12;
                int maxd = 50;*/

                /*
                int minw = 20;
                int minh = 10;
                int mind = 20;

                int maxw = 20;
                int maxh = 10;
                int maxd = 20;*/

                //int[,,] some3darray = new int[3, 3, 3];
                //int[] someflat3darray = new int[3 * 3 * 3];



                /*
                int minw = 75;
                int minh = 20;
                int mind = 75;

                int maxw = 75;
                int maxh = 20;
                int maxd = 75;*/

                /*
                int minw = 200;
                int minh = 10;
                int mind = 200;

                int maxw = 200;
                int maxh = 10;
                int maxd = 200;*/

                /*
                int minw = 10;
                int minh = 9;
                int mind = 10;

                int maxw = 15;
                int maxh = 12;
                int maxd = 15;*/

                /*int minw = 2;
                int minh = 2;
                int mind = 2;

                int maxw = 3;
                int maxh = 3;
                int maxd = 3;*/





                maxx = (int)scmaths.getSomeRandNumThousandDecimal(minw, maxw, 1, 0, -1);
                maxy = (int)scmaths.getSomeRandNumThousandDecimal(minh, maxh, 1, 0, -1);
                maxz = (int)scmaths.getSomeRandNumThousandDecimal(mind, maxd, 1, 0, -1);

                Console.WriteLine("sccslevelgen: " + maxz);



                minx = (int)scmaths.getSomeRandNumThousandDecimal(minw, maxw, 1, 0, -1);
                miny = 0;// (int)scmaths.getSomeRandNumThousandDecimal(9, 13, 1, 2, 1);
                minz = (int)scmaths.getSomeRandNumThousandDecimal(mind, maxd, 1, 0, -1);

                minx *= -1;
                minz *= -1;


                Console.WriteLine("/minx:" + (minx) + "/miny:" + (miny) + "/minz:" + (minz) + "/maxx:" + (maxx) + "/maxy:" + (maxy) + "/maxz:" + (maxz));

                /*minx = (int)scmaths.getSomeRandNumThousandDecimal(1, 2, 1, 2, 1);
                miny = 0;// (int)scmaths.getSomeRandNumThousandDecimal(9, 13, 1, 2, 1);
                minz = (int)scmaths.getSomeRandNumThousandDecimal(1, 2, 1, 2, 1);*/

                /*maxx = minx + somerw;
                maxy = miny + somerh;
                maxz = minz + somerd;*/
                /*
                maxx = somerw;
                maxy = somerh;
                maxz = somerd;*/





                //wallheightsize = 10;
                //maxy = wallheightsize;
                wallheightsize = maxy;


                //if minx == -2 and maxx = 0 , somewidth != minx + maxx.... somewidth = minx + maxx + 1... there are 3 indexes, 0 -1 -2 or 0 1 2 so somwidth = 3

                somewidth = (int)(maxx - minx);
                someheight = (int)(maxy - miny);
                somedepth = (int)(maxz - minz);

                if (somewidth < 0)
                {
                    somewidth *= -1;
                }
                if (someheight < 0)
                {
                    someheight *= -1;
                }
                if (somedepth < 0)
                {
                    somedepth *= -1;
                }

                //somewidth +=1;
                //someheight += 1;
                //somedepth += 1;

                //Console.WriteLine(minx + "/maxx:" + maxx + "/w:" + somewidth);
                //somewidth += 3;
                //someheight += 3;
                //somedepth += 3;

                maxtileamount = (somewidth * somedepth) * 1;
            }
        }





        public static sccslevelgen currentlevelgen;

        public static int[] arrayofindexes;
        public static int[] arrayofchunkinbundle;
        public static int[] arrayofindexesalt;
        public static int[] arrayofindexesmain;


        static int loadmap = 0;

        public static int neighbooraddx = 3; //3
        public static int neighbooraddz = 3; //3

        public static int somepointermax = 1;

        public struct thewalker
        {
            public int x;
            public int y;
            public int z;
        }

        thewalker[] thewalkers;// = new thewalker

        List<int> walkerdecisions = new List<int>();
        int[] walkerdecisionsarray;

        //int[] arrayofcoords;

        public int somerw;
        public int somerh;
        public int somerd;
        int istypeofl = -2;
        int istypeofr = -2;
        int istypeoft = -2;
        int istypeofb = -2;
        //int istile = -1;

        int istypeoflt = -2;
        int istypeofrt = -2;
        int istypeoflb = -2;
        int istypeofrb = -2;

        public static int somewidth;
        public static int someheight;
        public static int somedepth;

        public static int[] levelmap;
        //public int[] levelmapsortingtiles;
        //public int[] levelmapsortingtilesremains;
        //public int[] toremove;
        public int[] adjacenttiles;

        public static int maxx;
        public static int maxy;
        public static int maxz;

        public static int wallheightsize = 10;

        public static int maxtileamount;

        public static int minx;
        public static int miny;
        public static int minz;


        public static int minw = 10;
        public static int minh = 10;
        public static int mind = 10;

        public static int maxw = 10;
        public static int maxh = 10;
        public static int maxd = 10;


        /*
        public int minw = 20;
        public int minh = 20;
        public int mind = 20;

        public int maxw = 20;
        public int maxh = 20;
        public int maxd = 20;*/


        /*
        public class containerofchunkdata : ICollection<chunkdata[]>
        {

            public ICollection<chunkdata[]> myCollection = chunkdata;

            int ICollection<chunkdata[]>.Count => myCollection.Count;

            bool ICollection<chunkdata[]>.IsReadOnly => myCollection.IsReadOnly;//throw new NotImplementedException();

            public int Count => myCollection.Count;// throw new NotImplementedException();

            public bool Contains(chunkdata[] item)
            {
                return myCollection.Contains(item);//// throw new NotImplementedException();
            }

            public void CopyTo(chunkdata[][] array, int arrayIndex)
            {
                myCollection.CopyTo(array, arrayIndex);

                //throw new NotImplementedException();
            }

            System.Collections.IEnumerator IEnumerable.GetEnumerator()
            {
                return myCollection.GetEnumerator();
            }

            public bool Remove(chunkdata[] item)
            {
                return myCollection.Remove(item);//// throw new NotImplementedException();// throw new NotImplementedException();
            }

            public void Add(chunkdata[] item)
            {
                myCollection.Add(item);//// throw new NotImplementedException();// throw new NotImplementedException();
            }

            public void Clear()
            {
                myCollection.Clear();
            }

            public IEnumerator<chunkdata[]> GetEnumerator()
            {
                //throw new NotImplementedException();
                return (IEnumerator<chunkdata[]>)myCollection.GetEnumerator();
            }
        }



        containerofchunkdata[] containerofchunkdataarray;*/



        public void createlevel()
        {











            if (loadmap == 0)
            {


                levelmap = new int[somewidth * someheight * somedepth];
                //levelmapsortingtiles = new int[somewidth * someheight * somedepth];
                //levelmapsortingtilesremains = new int[somewidth * someheight * somedepth];
                //toremove = new int[somewidth * someheight * somedepth];
                adjacenttiles = new int[somewidth * someheight * somedepth];

                //somepointermax = 1;// maxtileamount / 10; //maxtileamount / 10

                /*List<int[]> listoftileloc = new List<int[]>();


                for (int x = 0; x < somepointermax; x++)
                {
                    int randx = (int)scmaths.getSomeRandNumThousandDecimal(2, somewidth - 2, 1, 0, 0);
                    int randy = (int)scmaths.getSomeRandNumThousandDecimal(2, someheight - 2, 1, 0, 0);
                    int randz = (int)scmaths.getSomeRandNumThousandDecimal(2, somedepth - 2, 1, 0, 0);

                    int posx = minx + randx;
                    int posy = miny + randy;
                    int posz = minz + randz;

                    //int[] somepos = new int[3];
                     ///somepos[0] = posx;
                     //somepos[1] = posy;
                    // somepos[2] = posz;

                     //listoftileloc.Add(somepos);


                    int[] somepos = new int[3];
                    somepos[0] = 0;
                    somepos[1] = 0;
                    somepos[2] = 0;

                    listoftileloc.Add(somepos);

                    //Console.WriteLine("rx:" + randx + "/ry:" + randy + "/rz:" + randz);
                    //Console.WriteLine("px:" + posx + "/py:" + posy + "/pz:" + posz);
                }*/

                thewalkers = new thewalker[somepointermax];

                for (int x = 0; x < thewalkers.Length; x++)
                {
                    thewalkers[x].x = 0;// somewidth / 2;
                    thewalkers[x].y = 0;//someheight / 2;
                    thewalkers[x].z = 0;// somedepth / 2;
                }






                //int sometot0 = somewidth * someheight * (-minz + maxz);
                //int sometot1 = somewidth * someheight * somedepth;
                //Console.WriteLine("tot0:" + sometot0  + "/tot1:" + sometot1);


                int outofrange = 0;

                /* for (var i = 0; i < levelmap.Length; i++)
                 {

                     levelmap[i] = -9;
                 }
                 */

                int[] leveltypes = new int[wallheightsize];
                for (int i = 0; i < leveltypes.Length; i++)
                {
                    leveltypes[i] = (i) * -1;
                }

                /*
                for (var x = minx; x < maxx; x++)
                {
                    for (var y = miny; y < maxy; y++)
                    {
                        for (var z = minz; z < maxz; z++)
                        {
                            int xx = x;
                            int yy = y;
                            int zz = z;

                            if (xx < 0)
                            {
                                xx *= -1;
                                xx = xx + (maxx - 1);
                            }

                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = yy + (maxy - 1);
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = zz + (maxz - 1);
                            }

                            int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (indexinarray < somewidth * someheight * somedepth)
                            {
                                if (y == 0)
                                {
                                    levelmap[indexinarray] = 999;
                                    //levelmapsortingtiles[indexinarray] = 999;
                                    //levelmapsortingtilesremains[indexinarray] = 999;
                                    adjacenttiles[indexinarray] = 999;
                                }
                                else
                                {
                                    levelmap[indexinarray] = y * -1;
                                    //levelmapsortingtiles[indexinarray] = y * -1;
                                    //levelmapsortingtilesremains[indexinarray] = y * -1;
                                    adjacenttiles[indexinarray] = y * -1;
                                }

                                /*for (int i = 0; i < leveltypes.Length; i++)
                                {
                                    if (leveltypes[i] * -1 == y)
                                    {
                                        if (y == 0)
                                        {
                                            levelmap[indexinarray] = 999;
                                            levelmapsortingtiles[indexinarray] = 999;
                                            levelmapsortingtilesremains[indexinarray] = 999;
                                            adjacenttiles[indexinarray] = 999;
                                        }
                                        else
                                        {
                                            levelmap[indexinarray] = leveltypes[i];
                                            levelmapsortingtiles[indexinarray] = leveltypes[i];
                                            levelmapsortingtilesremains[indexinarray] = leveltypes[i];
                                            adjacenttiles[indexinarray] = leveltypes[i];
                                        }
                                    }
                                    else
                                    {
                                        continue;
                                    }
                                }*/





                /*
                if (x > minx + 2 && x < maxx - 2 &&
                    z > minz + 2 && z < maxz - 2)
                {
                    for (int i = 0; i < leveltypes.Length; i++)
                    {
                        if (leveltypes[i] * -1 == y)
                        {
                            if (y == 0)
                            {
                                levelmap[indexinarray] = 999;
                                levelmapsortingtiles[indexinarray] = 999;
                                levelmapsortingtilesremains[indexinarray] = 999;
                                adjacenttiles[indexinarray] = 999;
                            }
                            else
                            {
                                levelmap[indexinarray] = leveltypes[i];
                                levelmapsortingtiles[indexinarray] = leveltypes[i];
                                levelmapsortingtilesremains[indexinarray] = leveltypes[i];
                                adjacenttiles[indexinarray] = leveltypes[i];
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < leveltypes.Length; i++)
                    {
                        if (leveltypes[i] * -1 == y)
                        {
                            if (y == 0)
                            {
                                levelmap[indexinarray] = 991;
                                levelmapsortingtiles[indexinarray] = 991;
                                levelmapsortingtilesremains[indexinarray] = 991;
                                adjacenttiles[indexinarray] = 991;
                            }
                            else
                            {
                                levelmap[indexinarray] = leveltypes[i];
                                levelmapsortingtiles[indexinarray] = leveltypes[i];
                                levelmapsortingtilesremains[indexinarray] = leveltypes[i];
                                adjacenttiles[indexinarray] = leveltypes[i];
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
            }
            else
            {
                Console.WriteLine("index is out of range");
            }
        }
    }
}*/













































                int countermaxtile = 0;
                for (var x = (minx); x < maxx; x++)
                {
                    for (var y = (miny); y < maxy; y++)
                    {
                        for (var z = (minz); z < maxz; z++)
                        {
                            //Console.WriteLine(y);

                            int xx = x;
                            int yy = y;
                            int zz = z;

                            if (xx < 0)
                            {
                                xx *= -1;
                                xx = xx + (maxx - 1);
                            }

                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = yy + (maxy - 1);
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = zz + (maxz - 1);
                            }

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (indexinlevelarray < somewidth * someheight * somedepth)
                            {
                                if (y == 0)
                                {
                                    levelmap[indexinlevelarray] = 999;
                                    adjacenttiles[indexinlevelarray] = 999;
                                }
                                else
                                {
                                    levelmap[indexinlevelarray] = y * -1;
                                    adjacenttiles[indexinlevelarray] = y * -1;
                                }
                            }
                        }
                    }
                }






                for (int t = 0; t < maxtileamount; t++)
                {
                    for (int p = 0; p < somepointermax; p++)
                    {
                        if (countermaxtile >= maxtileamount)
                        {
                            Console.WriteLine("reached max0");


                            break;
                        }
                        else
                        {

                        }

                        outofrange = 0;
                        for (int xi = -neighbooraddx; xi <= neighbooraddx; xi++)
                        {
                            //for (int yi = y; yi <= y; yi++)
                            {
                                for (int zi = -neighbooraddz; zi <= neighbooraddz; zi++)
                                {
                                    int neighboorx = thewalkers[p].x + xi;
                                    //int neighboory = y;
                                    int neighboorz = thewalkers[p].z + zi;

                                    int xxi = neighboorx;// (int)Math.Round(tilepos.X);
                                    int yyi = 0;// (int)Math.Round(tilepos.Y);
                                    int zzi = neighboorz;//(int)Math.Round(tilepos.Z);

                                    if (xxi < 0)
                                    {
                                        xxi *= -1;
                                        xxi = xxi + (maxx - 1);
                                    }

                                    if (yyi < 0)
                                    {
                                        yyi *= -1;
                                        yyi = yyi + (maxy - 1);
                                    }
                                    if (zzi < 0)
                                    {
                                        zzi *= -1;
                                        zzi = zzi + (maxz - 1);
                                    }

                                    //int indexinarray = xxi + somewidth * (yyi + someheight * zzi); //y is always 0 on floor tiles
                                    int indexinarray = xxi + somewidth * (yyi + someheight * zzi); //y is always 0 on floor tiles

                                    if (indexinarray < somewidth * someheight * somedepth)
                                    {
                                        //levelmap[indexinarray] = 0;

                                        if (levelmap[indexinarray] == 999)
                                        {
                                            //Console.WriteLine(indexinarray);
                                            //levelmapsortingtilesremains[indexinarray] = 0;
                                            //levelmapsortingtiles[indexinarray] = 0;
                                            //Console.WriteLine(listoftileloc[p][1]);
                                            levelmap[indexinarray] = 0;
                                            adjacenttiles[indexinarray] = 0;
                                            countermaxtile++;
                                        }
                                    }
                                    else
                                    {
                                        //int neighboorx = listoftileloc[p][0] + xi;
                                        //int neighboory = y;
                                        //int neighboorz = listoftileloc[p][2] + zi;

                                        xxi = thewalkers[p].x;// (int)Math.Round(tilepos.X);
                                        yyi = 0;// (int)Math.Round(tilepos.Y);
                                        zzi = thewalkers[p].z;//(int)Math.Round(tilepos.Z);

                                        if (xxi < 0)
                                        {
                                            xxi *= -1;
                                            xxi = xxi + (maxx - 1);
                                        }

                                        if (yyi < 0)
                                        {
                                            yyi *= -1;
                                            yyi = yyi + (maxy - 1);
                                        }
                                        if (zzi < 0)
                                        {
                                            zzi *= -1;
                                            zzi = zzi + (maxz - 1);
                                        }

                                        //int indexinarray = xxi + somewidth * (yyi + someheight * zzi); //y is always 0 on floor tiles
                                        indexinarray = xxi + somewidth * (yyi + someheight * zzi); //y is always 0 on floor tiles

                                        if (xxi >= minx && xxi <= maxx - 1)
                                        {
                                            if (xxi >= minx)
                                            {
                                                xxi += 1;
                                                thewalkers[p].x = xxi;
                                            }
                                            else if (xxi <= maxx - 1)
                                            {
                                                xxi -= 1;
                                                thewalkers[p].x = xxi;
                                            }
                                        }
                                        if (zzi >= minz && zzi <= maxz - 1)
                                        {
                                            if (zzi >= minz)
                                            {
                                                zzi += 1;
                                                thewalkers[p].z = zzi;
                                            }
                                            else if (zzi <= maxz - 1)
                                            {
                                                zzi -= 1;
                                                thewalkers[p].z = zzi;
                                            }
                                        }





                                        if (indexinarray < somewidth * someheight * somedepth)
                                        {
                                            //levelmapsortingtilesremains[indexinarray] = 999;
                                            //levelmapsortingtiles[indexinarray] = 999;
                                            //Console.WriteLine(listoftileloc[p][1]);
                                            levelmap[indexinarray] = 999;
                                            adjacenttiles[indexinarray] = 999;
                                        }
                                        else
                                        {

                                            //listoftileloc[p] = new int[3];
                                            thewalkers[p].x = 0;
                                            thewalkers[p].y = 0;
                                            thewalkers[p].z = 0;
                                        }

                                        /*levelmapsortingtilesremains[indexinlevelarray] = 0;
                                        levelmapsortingtiles[indexinlevelarray] = 0;
                                        //Console.WriteLine(listoftileloc[p][1]);
                                        levelmap[indexinlevelarray] = 0;
                                        adjacenttiles[indexinlevelarray] = 0;*/
                                    }

                                }
                            }
                        }



                        int dirlr = (int)Math.Floor(scmaths.getSomeRandNumThousandDecimal(0, 2, 1, 0, 0));
                        int dirfb = (int)Math.Floor(scmaths.getSomeRandNumThousandDecimal(0, 2, 1, 0, 0));
                        //float dirldrd = (float)scmaths.getSomeRandNumThousandDecimal(0, 2, 0.1f, 0, 0);
                        //float dirfdbd = (float)scmaths.getSomeRandNumThousandDecimal(0, 2, 0.1f, 0, 0);

                        int decidedir = (int)Math.Floor(scmaths.getSomeRandNumThousandDecimal(0, 2, 1, 0, 0));

                        //Console.WriteLine("0:" + dirlr + "/1:" + dirfb + "/2:" + decidedir);

                        if (decidedir == 0)
                        {
                            if (dirlr == 0)
                            {
                                int x = thewalkers[p].x - 1;

                                if (x >= minx + 3)
                                {
                                    //Console.WriteLine("option 0");
                                    thewalkers[p].x = x;
                                    walkerdecisions.Add(0);
                                }
                                else
                                {
                                    thewalkers[p].x = 0;
                                    thewalkers[p].y = 0;
                                    thewalkers[p].z = 0;
                                    walkerdecisions.Add(1);
                                    //listoftileloc[p][0] = somevec[0];
                                    //listoftileloc[p][1] = 0;
                                    //listoftileloc[p][2] = 0;

                                    //listoftileloc[p] = somevec;// Vector3.Zero;
                                }
                            }
                            else if (dirlr == 1)
                            {
                                int x = thewalkers[p].x + 1;

                                if (x <= maxx - 4)
                                {
                                    //Console.WriteLine("option 1");
                                    thewalkers[p].x = x;
                                    walkerdecisions.Add(2);
                                }
                                else
                                {
                                    thewalkers[p].x = 0;
                                    thewalkers[p].y = 0;
                                    thewalkers[p].z = 0;
                                    walkerdecisions.Add(3);
                                    //listoftileloc[p][0] = 0;
                                    //listoftileloc[p][1] = 0;
                                    //listoftileloc[p][2] = 0;
                                    //listoftileloc[p] = somevec;// Vector3.Zero;
                                }
                            }
                        }
                        else if (decidedir == 1)
                        {
                            if (dirfb == 0)
                            {
                                int z = thewalkers[p].z - 1;

                                if (z >= minz + 3)
                                {
                                    //Console.WriteLine("option 2");
                                    thewalkers[p].z = z;
                                    walkerdecisions.Add(4);
                                }
                                else
                                {
                                    thewalkers[p].x = 0;
                                    thewalkers[p].y = 0;
                                    thewalkers[p].z = 0;
                                    walkerdecisions.Add(5);
                                    //listoftileloc[p][0] = somevec[0];
                                    ////listoftileloc[p][1] = 0;
                                    //listoftileloc[p][2] = 0;

                                    //listoftileloc[p] = somevec;// Vector3.Zero;
                                }
                            }
                            else if (dirfb == 1)
                            {

                                int z = thewalkers[p].z + 1;

                                if (z <= maxz - 4)
                                {
                                    //Console.WriteLine("option 3");
                                    thewalkers[p].z = z;
                                    walkerdecisions.Add(6);
                                }
                                else
                                {
                                    thewalkers[p].x = 0;
                                    thewalkers[p].y = 0;
                                    thewalkers[p].z = 0;
                                    walkerdecisions.Add(7);
                                    //listoftileloc[p][0] = somevec[0];
                                    ////listoftileloc[p][1] = 0;
                                    //listoftileloc[p][2] = 0;

                                    //listoftileloc[p] = somevec;// Vector3.Zero;
                                }
                            }
                        }


















                        /*if (outofrange == 0)
                        {

                        }
                        else
                        {
                            int[] somevec = new int[3];
                            somevec[0] = 0;
                            somevec[1] = 0;
                            somevec[2] = 0;

                            listoftileloc[p] = somevec;// Vector3.Zero;
                        }*/
                    }
                }





                for (var x = minx; x < maxx; x++)
                {
                    for (var y = miny; y < maxy; y++)
                    {
                        for (var z = minz; z < maxz; z++)
                        {
                            int xx = x;
                            int yy = y;
                            int zz = z;

                            if (xx < 0)
                            {
                                xx *= -1;
                                xx = xx + (maxx - 1);
                            }

                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = yy + (maxy - 1);
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = zz + (maxz - 1);
                            }

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (levelmap[indexinlevelarray] == 0)
                            {
                                checkAllSides(x, y, z, indexinlevelarray);
                            }
                        }
                    }
                }

                createfinal();



                /*

                arrayofchunkinbundle = new int[levelmap.Length]; 
                arrayofindexes = new int[levelmap.Length];
                //arraychunkdatalod0 = new chunkdata[levelmap.Length];

                for (int i = 0; i < arrayofindexes.Length; i++)
                {
                    arrayofindexes[i] = -1;
                }




                arrayofindexesalt = new int[levelmap.Length];
                //arraychunkdatalod0 = new chunkdata[levelmap.Length];

                for (int i = 0; i < arrayofindexesalt.Length; i++)
                {
                    arrayofindexesalt[i] = -1;
                }






                
                //chunkdata = new Dictionary<int, chunkdata>();
                


                int totaltiles = 0;
                int totaltilescounter = 0;
                for (var x = minx; x < maxx; x++)
                {
                    for (var y = miny; y < maxy; y++)
                    {
                        for (var z = minz; z < maxz; z++)
                        {
                            //Console.WriteLine("/x:" + x + "/y:" + y + "/z:" + z);

                            int xx = x;
                            int yy = y;
                            int zz = z;

                            if (xx < 0)
                            {
                                xx *= -1;
                                xx = xx + (maxx - 1);
                            }

                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = yy + (maxy - 1);
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = zz + (maxz - 1);
                            }

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (indexinlevelarray < somewidth * someheight * somedepth)
                            {

                                int typeofterraintile = levelmap[indexinlevelarray];

                                if (typeofterraintile == 0 ||
                                    typeofterraintile == 1101 ||
                                    typeofterraintile == 1102 ||
                                    typeofterraintile == 1103 ||
                                    typeofterraintile == 1104 ||
                                    typeofterraintile == 1105 ||
                                    typeofterraintile == 1106 ||
                                    typeofterraintile == 1107 ||
                                    typeofterraintile == 1108 ||
                                    typeofterraintile == 1109 ||
                                    typeofterraintile == 1110 ||
                                    typeofterraintile == 1111 ||
                                    typeofterraintile == 1112 ||
                                    typeofterraintile == -99 ||
                                    typeofterraintile == 1115)
                                {

                                    arrayofindexesalt[indexinlevelarray] = totaltilescounter;
                                    totaltilescounter++;


                                    //chunkdata.Add(indexinlevelarray, new chunkdata());
                                }
                            }
                            totaltiles++;
                        }
                    }
                }*/





                //arrayofchunkinbundle = new int[levelmap.Length]; 
                arrayofindexes = new int[levelmap.Length];
                //arraychunkdatalod0 = new chunkdata[levelmap.Length];

                for (int i = 0; i < arrayofindexes.Length; i++)
                {
                    arrayofindexes[i] = -1;
                }




                arrayofindexesalt = new int[levelmap.Length * arraymultiplier];
                //arraychunkdatalod0 = new chunkdata[levelmap.Length];

                for (int i = 0; i < arrayofindexesalt.Length; i++)
                {
                    arrayofindexesalt[i] = -1;
                    //arrayofindexesalt[i] = new int[sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz];// -1;

                }



                arrayofindexesmain = new int[levelmap.Length * arraymultiplier];
                //arraychunkdatalod0 = new chunkdata[levelmap.Length];

                for (int i = 0; i < arrayofindexesmain.Length; i++)
                {
                    arrayofindexesmain[i] = -1;
                    //arrayofindexesalt[i] = new int[sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz];// -1;

                }


                //Console.WriteLine("here0");



                /*
                chunkdata = new Dictionary<int, chunkdata>();
                */

                /*
                int totaltiles = 0;
                int totaltilescounter = 0;
                for (var x = minx; x < maxx; x++)
                {
                    for (var y = miny; y < maxy; y++)
                    {
                        for (var z = minz; z < maxz; z++)
                        {
                            //Console.WriteLine("/x:" + x + "/y:" + y + "/z:" + z);

                            int xx = x;
                            int yy = y;
                            int zz = z;

                            if (xx < 0)
                            {
                                xx *= -1;
                                xx = xx + (maxx - 1);
                            }

                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = yy + (maxy - 1);
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = zz + (maxz - 1);
                            }

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                            if (indexinlevelarray < somewidth * someheight * somedepth)
                            {

                                //arrayofindexes[indexinlevelarray] = indexinlevelarray;

                                int typeofterraintile = levelmap[indexinlevelarray];

                                if (typeofterraintile == 0 ||
                                    typeofterraintile == 1101 ||
                                    typeofterraintile == 1102 ||
                                    typeofterraintile == 1103 ||
                                    typeofterraintile == 1104 ||
                                    typeofterraintile == 1105 ||
                                    typeofterraintile == 1106 ||
                                    typeofterraintile == 1107 ||
                                    typeofterraintile == 1108 ||
                                    typeofterraintile == 1109 ||
                                    typeofterraintile == 1110 ||
                                    typeofterraintile == 1111 ||
                                    typeofterraintile == 1112 ||
                                    typeofterraintile == -99 ||
                                    typeofterraintile == 1115)
                                {


                                



                                    for (int xi = 0; xi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx; xi++)
                                    {
                                        for (int yi = 0; yi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony; yi++)
                                        {
                                            for (int zi = 0; zi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz; zi++)
                                            {
                                                int indexofchunkinbundle = xi + (sccs.sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx) * (yi + (sccs.sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony) * zi); //y is always 0 on floor tiles

                                                int indexinmapplusbundlechunks = (indexinlevelarray * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + indexofchunkinbundle;

                                                //arrayofindexesalt[indexinmapplusbundlechunks] = (totaltilescounter * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + indexofchunkinbundle;

                                                int indexalt = (totaltilescounter * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + indexofchunkinbundle;


                                                arrayofindexesalt[indexinmapplusbundlechunks] = (indexalt);



                                                //arrayofindexes[indexinmapplusbundlechunks] = indexalt;//
                                                //totaltilescounter++;

                                            }
                                        }
                                    }

                                    int indexalt1 = (totaltilescounter * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + 0;

                                    arrayofindexes[indexinlevelarray] = totaltilescounter;


                                    totaltilescounter++;




                                    //chunkdata.Add(indexinlevelarray, new chunkdata());
                                }
                                else
                                {
                                    //arrayofindexes[indexinlevelarray] = -1;
                                }
                            }
                            totaltiles++;
                        }
                    }
                }*/
                /*

                int totalx = 0;
                int totaly = 0;
                int totalz = 0;

                for (int ilod = 0; ilod < 1; ilod++)
                {

                    for (int f = 0; f < 1; f++)
                    {

                        for (int x = minx, xe = 0; x < maxx; x += sccsgraphicssec.currentsccsgraphicssec.incrementsfracx, xe++)
                        {
                            for (int y = miny, ye = 0; y < maxy; y += sccsgraphicssec.currentsccsgraphicssec.incrementsfracy, ye++)
                            {
                                for (int z = minz, ze = 0; z < maxz; z += sccsgraphicssec.currentsccsgraphicssec.incrementsfracz, ze++)
                                {
                                    totalx = 0;
                                    totaly = 0;
                                    totalz = 0;
                                    //Console.WriteLine("test");
                                    for (int xi = x, secx = 0; xi < x + sccsgraphicssec.currentsccsgraphicssec.incrementsfracx; xi++, secx++)
                                    {
                                        for (int yi = y, secy = 0; yi < y + sccsgraphicssec.currentsccsgraphicssec.incrementsfracy; yi++, secy++)
                                        {
                                            for (int zi = z, secz = 0; zi < z + sccsgraphicssec.currentsccsgraphicssec.incrementsfracz; zi++, secz++)
                                            {
                                                int xx = xi;
                                                int yy = yi;
                                                int zz = zi;

                                                if (xx < 0)
                                                {
                                                    xx *= -1;
                                                    xx = xx + (maxx - 1);
                                                }

                                                if (yy < 0)
                                                {
                                                    yy *= -1;
                                                    yy = yy + (maxy - 1);
                                                }
                                                if (zz < 0)
                                                {
                                                    zz *= -1;
                                                    zz = zz + (maxz - 1);
                                                }

                                                int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                                                if (indexinlevelarray < somewidth * someheight * somedepth)
                                                {

                                                    //arrayofindexes[indexinlevelarray] = indexinlevelarray;

                                                    int typeofterraintile = levelmap[indexinlevelarray];

                                                    if (typeofterraintile == 0 ||
                                                        typeofterraintile == 1101 ||
                                                        typeofterraintile == 1102 ||
                                                        typeofterraintile == 1103 ||
                                                        typeofterraintile == 1104 ||
                                                        typeofterraintile == 1105 ||
                                                        typeofterraintile == 1106 ||
                                                        typeofterraintile == 1107 ||
                                                        typeofterraintile == 1108 ||
                                                        typeofterraintile == 1109 ||
                                                        typeofterraintile == 1110 ||
                                                        typeofterraintile == 1111 ||
                                                        typeofterraintile == 1112 ||
                                                        typeofterraintile == -99 ||
                                                        typeofterraintile == 1115)
                                                    {

                                                        for (int xxi = 0; xxi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx; xxi++)
                                                        {
                                                            for (int yyi = 0; yyi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony; yyi++)
                                                            {
                                                                for (int zzi = 0; zzi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz; zzi++)
                                                                {
                                                                    totalz++;
                                                                }
                                                                totaly++;
                                                            }
                                                            totalx++;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }


                                    break;

                                }
                            }
                        }
                    }
                }




                Console.WriteLine("/totalx:" + totalx + "/totaly:" + totaly + "/totalz:" + totalz);
                */


                //Console.WriteLine("here0");





                List<int>[] arrayofindexesforchunkdata = new List<int>[sccsgraphicssec.currentsccsgraphicssec.leveldivisionx* sccsgraphicssec.currentsccsgraphicssec.leveldivisiony * sccsgraphicssec.currentsccsgraphicssec.leveldivisionz];



                //arrayofindexesforchunkdata[]

                int totaltiles = 0;
                int totaltilescounter = 0;


                for (int ilod = 0; ilod < 1; ilod++)
                {
                    for (int f = 0; f < 1; f++)
                    {
                        for (int x = sccslevelgen.minx, xe = 0; x < sccslevelgen.maxx; x += sccsgraphicssec.currentsccsgraphicssec.incrementsfracx, xe++)
                        {
                            for (int y = sccslevelgen.miny, ye = 0; y < sccslevelgen.maxy; y += sccsgraphicssec.currentsccsgraphicssec.incrementsfracy, ye++)
                            {
                                for (int z = sccslevelgen.minz, ze = 0; z < sccslevelgen.maxz; z += sccsgraphicssec.currentsccsgraphicssec.incrementsfracz, ze++)
                                {
                                    /*int xx = x;
                                    int yy = y;
                                    int zz = z;

                                    if (xx < 0)
                                    {
                                        xx *= -1;
                                        xx = xx + (sccslevelgen.maxx - 1);
                                    }

                                    if (yy < 0)
                                    {
                                        yy *= -1;
                                        yy = yy + (sccslevelgen.maxy - 1);
                                    }
                                    if (zz < 0)
                                    {
                                        zz *= -1;
                                        zz = zz + (sccslevelgen.maxz - 1);
                                    }*/

                                    int posmainx = x / sccsgraphicssec.currentsccsgraphicssec.incrementsfracx;
                                    int posmainy = y / sccsgraphicssec.currentsccsgraphicssec.incrementsfracy;
                                    int posmainz = z / sccsgraphicssec.currentsccsgraphicssec.incrementsfracz;

                                    if (posmainx < 0)
                                    {
                                        posmainx *= -1;
                                        posmainx = posmainx + ((sccsgraphicssec.currentsccsgraphicssec.leveldivisionx / 2) - 1);
                                    }

                                    if (posmainy < 0)
                                    {
                                        posmainy *= -1;
                                        posmainy = posmainy + ((sccsgraphicssec.currentsccsgraphicssec.leveldivisiony / 2) - 1);
                                    }
                                    if (posmainz < 0)
                                    {
                                        posmainz *= -1;
                                        posmainz = posmainz + ((sccsgraphicssec.currentsccsgraphicssec.leveldivisionz / 2) - 1);
                                    }

                                    int someindexmain = posmainx + (sccsgraphicssec.currentsccsgraphicssec.leveldivisionx) * (posmainy + (sccsgraphicssec.currentsccsgraphicssec.leveldivisiony) * posmainz);

                                    arrayofindexesforchunkdata[someindexmain] = new List<int>();

                                    int counterforthelength = 0;

                                    for (int xi = x; xi < x + sccsgraphicssec.currentsccsgraphicssec.incrementsfracx; xi++)
                                    {
                                        for (int yi = y; yi < y + sccsgraphicssec.currentsccsgraphicssec.incrementsfracy; yi++)
                                        {
                                            for (int zi = z; zi < z + sccsgraphicssec.currentsccsgraphicssec.incrementsfracz; zi++)
                                            {
                                                int xx = xi;
                                                int yy = yi;
                                                int zz = zi;

                                                if (xx < 0)
                                                {
                                                    xx *= -1;
                                                    xx = xx + (maxx - 1);
                                                }

                                                if (yy < 0)
                                                {
                                                    yy *= -1;
                                                    yy = yy + (maxy - 1);
                                                }
                                                if (zz < 0)
                                                {
                                                    zz *= -1;
                                                    zz = zz + (maxz - 1);
                                                }

                                                int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                                                if (indexinlevelarray < somewidth * someheight * somedepth)
                                                {

                                                    //arrayofindexes[indexinlevelarray] = indexinlevelarray;

                                                    int typeofterraintile = levelmap[indexinlevelarray];

                                                    if (typeofterraintile == 0 ||
                                                        typeofterraintile == 1101 ||
                                                        typeofterraintile == 1102 ||
                                                        typeofterraintile == 1103 ||
                                                        typeofterraintile == 1104 ||
                                                        typeofterraintile == 1105 ||
                                                        typeofterraintile == 1106 ||
                                                        typeofterraintile == 1107 ||
                                                        typeofterraintile == 1108 ||
                                                        typeofterraintile == 1109 ||
                                                        typeofterraintile == 1110 ||
                                                        typeofterraintile == 1111 ||
                                                        typeofterraintile == 1112 ||
                                                        typeofterraintile == -99 ||
                                                        typeofterraintile == 1115)
                                                    {
                                                        for (int xxi = 0; xxi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx; xxi++)
                                                        {
                                                            for (int yyi = 0; yyi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony; yyi++)
                                                            {
                                                                for (int zzi = 0; zzi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz; zzi++)
                                                                {

                                                                    int indexofchunkinbundle = xxi + (sccs.sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx) * (yyi + (sccs.sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony) * zzi); //y is always 0 on floor tiles
                                                                    int indexinmapplusbundlechunks = (indexinlevelarray * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + indexofchunkinbundle;


                                                                    arrayofindexesalt[indexinmapplusbundlechunks] = (counterforthelength);
                                                                    arrayofindexesmain[indexinmapplusbundlechunks] = someindexmain;


                                                                    counterforthelength++;

                                                                }
                                                            }
                                                        }


                                                        totaltilescounter++;
                                                    }
                                                }

                                                totaltiles++;
                                            }
                                        }
                                    }

                                    arrayofindexesforchunkdata[someindexmain].Add(counterforthelength);
                                }
                            }
                        }

                    }
                }










                /*
                for (int ilod = 0; ilod < 1; ilod++)
                {

                    for (int f = 0; f < 1; f++)
                    {

                        for (int x = minx, xe = 0; x < maxx; x += sccsgraphicssec.currentsccsgraphicssec.incrementsfracx, xe++)
                        {
                            for (int y = miny, ye = 0; y < maxy; y += sccsgraphicssec.currentsccsgraphicssec.incrementsfracy, ye++)
                            {
                                for (int z = minz, ze = 0; z < maxz; z += sccsgraphicssec.currentsccsgraphicssec.incrementsfracz, ze++)
                                {






                                    //Console.WriteLine("test");
                                    int indexbycounter = 0;
                                    for (int xi = x, secx = 0; xi < x + sccsgraphicssec.currentsccsgraphicssec.incrementsfracx; xi++, secx++)
                                    {
                                        for (int yi = y, secy = 0; yi < y + sccsgraphicssec.currentsccsgraphicssec.incrementsfracy; yi++, secy++)
                                        {
                                            for (int zi = z, secz = 0; zi < z + sccsgraphicssec.currentsccsgraphicssec.incrementsfracz; zi++, secz++)
                                            {













                                                /*
                                                for (int xi = x, secx = -4; xi < x + sccsgraphicssec.currentsccsgraphicssec.incrementsfracx; xi++, secx++)
                                                {
                                                    for (int yi = y, secy = -2; yi < y + sccsgraphicssec.currentsccsgraphicssec.incrementsfracy; yi++, secy++)
                                                    {
                                                        for (int zi = z, secz = -4; zi < z + sccsgraphicssec.currentsccsgraphicssec.incrementsfracz; zi++, secz++)
                                                        {
                                                int xx = xi;
                                                int yy = yi;
                                                int zz = zi;

                                                if (xx < 0)
                                                {
                                                    xx *= -1;
                                                    xx = xx + (maxx - 1);
                                                }

                                                if (yy < 0)
                                                {
                                                    yy *= -1;
                                                    yy = yy + (maxy - 1);
                                                }
                                                if (zz < 0)
                                                {
                                                    zz *= -1;
                                                    zz = zz + (maxz - 1);
                                                }

                                                int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                                                if (indexinlevelarray < somewidth * someheight * somedepth)
                                                {

                                                    //arrayofindexes[indexinlevelarray] = indexinlevelarray;

                                                    int typeofterraintile = levelmap[indexinlevelarray];

                                                    if (typeofterraintile == 0 ||
                                                        typeofterraintile == 1101 ||
                                                        typeofterraintile == 1102 ||
                                                        typeofterraintile == 1103 ||
                                                        typeofterraintile == 1104 ||
                                                        typeofterraintile == 1105 ||
                                                        typeofterraintile == 1106 ||
                                                        typeofterraintile == 1107 ||
                                                        typeofterraintile == 1108 ||
                                                        typeofterraintile == 1109 ||
                                                        typeofterraintile == 1110 ||
                                                        typeofterraintile == 1111 ||
                                                        typeofterraintile == 1112 ||
                                                        typeofterraintile == -99 ||
                                                        typeofterraintile == 1115)
                                                    {

                                                        for (int xxi = 0; xxi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx; xxi++)
                                                        {
                                                            for (int yyi = 0; yyi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony; yyi++)
                                                            {
                                                                for (int zzi = 0; zzi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz; zzi++)
                                                                {
                                                                    int indexofchunkinbundle = xxi + (sccs.sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx) * (yyi + (sccs.sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony) * zzi); //y is always 0 on floor tiles
                                                                    int indexinmapplusbundlechunks = (indexinlevelarray * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + indexofchunkinbundle;


                                                                    int xxxi = secx;
                                                                    int yyyi = secy;
                                                                    int zzzi = secz;


                                                                    /*if (xxxi < 0)
                                                                    {
                                                                        xxxi *= -1;
                                                                        xxxi = xxxi + (4-1);
                                                                    }

                                                                    if (yyyi < 0)
                                                                    {
                                                                        yyyi *= -1;
                                                                        yyyi = yyyi + (2-1);
                                                                    }

                                                                    if (zzzi < 0)
                                                                    {
                                                                        zzzi *= -1;
                                                                        zzzi = zzzi + (4-1);
                                                                    }
                                                                    

                                                                    int indexincrements = xxxi + (sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsfracx) * (yyyi + (sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsfracy) * zzzi); //y is always 0 on floor tiles

                                                                    int chunkindexincrements = (indexincrements * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + indexofchunkinbundle;

                                                                    //Console.WriteLine(chunkindexincrements);
                                                                    //arrayofindexesalt[indexinmapplusbundlechunks] = (chunkindexincrements);


                                                                    /*int indexincrements = xxxi + (sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsfracx) * (yyyi + (sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsfracy) * zzzi); //y is always 0 on floor tiles




                                                                    arrayofindexesalt[indexinmapplusbundlechunks] = (chunkindexincrements);

                                                                    //arrayofindexesalt[indexinmapplusbundlechunks] = (indexbycounter);

                                                                    //Console.WriteLine("indexbycounter:" + indexbycounter);

                                                                    /*flatz++;
                                                                    if (flatz > totalz)
                                                                    {
                                                                        flaty++;
                                                                        flatz = 0;
                                                                    }
                                                                    if (flaty > totaly)
                                                                    {
                                                                        flatx++;
                                                                        flaty = 0;
                                                                    }
                                                                    if (flatx > totalx)
                                                                    {
                                                                        flatx = 0;
                                                                    }
                                                                    //secz++;
                                                                    //flatz++;

                                                                    indexbycounter++;

                                                                }
                                                                //secy++;
                                                                //flaty++;
                                                            }
                                                            //flatx++;
                                                            //secx++;
                                                        }

                                                        totaltilescounter++;

                                                    }
                                                    else
                                                    {

                                                    }
                                                }

                                                totaltiles++;
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    }
                }*/

                //Console.WriteLine("here0");



                Console.WriteLine("totaltilescounter:" + totaltilescounter + "/entireleveltiles:" + totaltiles);





                //Let's say a level of 10x10x10
                //let's say bundle chunks of 2x2x2 
                //total tiles == 1000 * 2 * 2 * 2 = 8000 tiles
                //index 







                //arraychunkdatalod0 = new chunkdata[levelmap.Length];
                /*arraychunkdatalod0top = new chunkdata[totaltilescounter * 8];
                arraychunkdatalod0bottom = new chunkdata[totaltilescounter * 8];
                arraychunkdatalod0left = new chunkdata[totaltilescounter * 8];
                arraychunkdatalod0right = new chunkdata[totaltilescounter * 8];
                arraychunkdatalod0front = new chunkdata[totaltilescounter * 8];
                arraychunkdatalod0bottom = new chunkdata[totaltilescounter * 8];*/
















                chunkdata = new chunkdata[levelofdetails][][][];
                for (int ilod = 0; ilod < 1; ilod++)
                {
                    chunkdata[ilod] = new chunkdata[NUMBEROFFACES][][];

                    for (int f = 0; f < NUMBEROFFACES; f++)
                    {
                        chunkdata[ilod][f] = new chunkdata[sccsgraphicssec.currentsccsgraphicssec.leveldivisionx * sccsgraphicssec.currentsccsgraphicssec.leveldivisiony * sccsgraphicssec.currentsccsgraphicssec.leveldivisionz][];                            
                        
                        for (int x = sccslevelgen.minx, xe = 0; x < sccslevelgen.maxx; x += sccsgraphicssec.currentsccsgraphicssec.incrementsfracx, xe++)
                        {
                            for (int y = sccslevelgen.miny, ye = 0; y < sccslevelgen.maxy; y += sccsgraphicssec.currentsccsgraphicssec.incrementsfracy, ye++)
                            {
                                for (int z = sccslevelgen.minz, ze = 0; z < sccslevelgen.maxz; z += sccsgraphicssec.currentsccsgraphicssec.incrementsfracz, ze++)
                                {
                                    /*int xx = x;
                                    int yy = y;
                                    int zz = z;

                                    if (xx < 0)
                                    {
                                        xx *= -1;
                                        xx = xx + (sccslevelgen.maxx - 1);
                                    }

                                    if (yy < 0)
                                    {
                                        yy *= -1;
                                        yy = yy + (sccslevelgen.maxy - 1);
                                    }
                                    if (zz < 0)
                                    {
                                        zz *= -1;
                                        zz = zz + (sccslevelgen.maxz - 1);
                                    }*/

                                    int posmainx = x / sccsgraphicssec.currentsccsgraphicssec.incrementsfracx;
                                    int posmainy = y / sccsgraphicssec.currentsccsgraphicssec.incrementsfracy;
                                    int posmainz = z / sccsgraphicssec.currentsccsgraphicssec.incrementsfracz;

                                    if (posmainx < 0)
                                    {
                                        posmainx *= -1;
                                        posmainx = posmainx + ((sccsgraphicssec.currentsccsgraphicssec.leveldivisionx / 2) - 1);
                                    }

                                    if (posmainy < 0)
                                    {
                                        posmainy *= -1;
                                        posmainy = posmainy + ((sccsgraphicssec.currentsccsgraphicssec.leveldivisiony / 2) - 1);
                                    }
                                    if (posmainz < 0)
                                    {
                                        posmainz *= -1;
                                        posmainz = posmainz + ((sccsgraphicssec.currentsccsgraphicssec.leveldivisionz / 2) - 1);
                                    }

                                    int someindexmain = posmainx + (sccsgraphicssec.currentsccsgraphicssec.leveldivisionx) * (posmainy + (sccsgraphicssec.currentsccsgraphicssec.leveldivisiony) * posmainz);


                                    //Console.WriteLine(arrayofindexesforchunkdata[someindexmain][0]);
                                    chunkdata[ilod][f][someindexmain] = new chunkdata[arrayofindexesforchunkdata[someindexmain][0]];


                                }
                            }
                        }
                    }
                }




                /*
                chunkdata = new chunkdata[levelofdetails][][][];


                for (int ilod = 0; ilod < levelofdetails; ilod++)
                {
                    chunkdata[ilod] = new chunkdata[NUMBEROFFACES][][];

                    for (int f = 0; f < NUMBEROFFACES; f++)
                    {
                        chunkdata[ilod][f] = new chunkdata[totaltilescounter * arraymultiplier][];
                    }
                }*/


                /*
                chunkdata = new chunkdata[levelofdetails][][][];


                for (int ilod = 0; ilod < levelofdetails; ilod++)
                {
                    chunkdata[ilod] = new chunkdata[NUMBEROFFACES][][];

                    for (int f = 0; f < NUMBEROFFACES; f++)
                    {
                        chunkdata[ilod][f] = new chunkdata[sccsgraphicssec.currentsccsgraphicssec.leveldivisionx * sccsgraphicssec.currentsccsgraphicssec.leveldivisiony * sccsgraphicssec.currentsccsgraphicssec.leveldivisionz][];

                        for (int ld = 0; ld < sccsgraphicssec.currentsccsgraphicssec.leveldivisionx * sccsgraphicssec.currentsccsgraphicssec.leveldivisiony * sccsgraphicssec.currentsccsgraphicssec.leveldivisionz; ld++)
                        {
                            //chunkdata[ilod][f][ld] = new chunkdata[sccsgraphicssec.currentsccsgraphicssec.incrementsfracx * sccsgraphicssec.currentsccsgraphicssec.incrementsfracy * sccsgraphicssec.currentsccsgraphicssec.incrementsfracz * arraymultiplier];
                            chunkdata[ilod][f][ld] = new chunkdata[sccsgraphicssec.currentsccsgraphicssec.incrementsfracx * sccsgraphicssec.currentsccsgraphicssec.incrementsfracy * sccsgraphicssec.currentsccsgraphicssec.incrementsfracz * arraymultiplier];


                        }
                    }
                }*/
                //arrayofindexesforchunkdata[someindexmain]


                //Console.WriteLine("total:" + (sccsgraphicssec.currentsccsgraphicssec.incrementsfracx * sccsgraphicssec.currentsccsgraphicssec.incrementsfracy * sccsgraphicssec.currentsccsgraphicssec.incrementsfracz * arraymultiplier));




















                /*
                containerofchunkdataarray = new containerofchunkdata[1];
                containerofchunkdataarray[0] = new containerofchunkdata();

                for (int i = 0; i < chunkdata.Count; i++)
                {
                    containerofchunkdataarray[0].Add(chunkdata[i]);
                }*/


                /*
                lock (arrayofclass.SyncRoot)
                {
                    foreach (object item in arrayofclass)
                    {
                        // Insert your code here.
                    }
                }
                */








                /*
                for (int f = 0; f < NUMBEROFFACES; f++)
                {
                    totaltiles = 0;
                    totaltilescounter = 0;
                    for (var x = minx; x < maxx; x++)
                    {
                        for (var y = miny; y < maxy; y++)
                        {
                            for (var z = minz; z < maxz; z++)
                            {
                                //Console.WriteLine("/x:" + x + "/y:" + y + "/z:" + z);

                                int xx = x;
                                int yy = y;
                                int zz = z;

                                if (xx < 0)
                                {
                                    xx *= -1;
                                    xx = xx + (maxx - 1);
                                }

                                if (yy < 0)
                                {
                                    yy *= -1;
                                    yy = yy + (maxy - 1);
                                }
                                if (zz < 0)
                                {
                                    zz *= -1;
                                    zz = zz + (maxz - 1);
                                }

                                int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                                if (indexinlevelarray < somewidth * someheight * somedepth)
                                {

                                    int typeofterraintile = levelmap[indexinlevelarray];

                                    if (typeofterraintile == 0 ||
                                        typeofterraintile == 1101 ||
                                        typeofterraintile == 1102 ||
                                        typeofterraintile == 1103 ||
                                        typeofterraintile == 1104 ||
                                        typeofterraintile == 1105 ||
                                        typeofterraintile == 1106 ||
                                        typeofterraintile == 1107 ||
                                        typeofterraintile == 1108 ||
                                        typeofterraintile == 1109 ||
                                        typeofterraintile == 1110 ||
                                        typeofterraintile == 1111 ||
                                        typeofterraintile == 1112 ||
                                        typeofterraintile == -99 ||
                                        typeofterraintile == 1115)
                                    {

                                        chunkdata[f][totaltilescounter] = new chunkdata();
                                        chunkdata[f][totaltilescounter].indexinmainarray = indexinlevelarray;


                                         //chunkdata[]

                                         totaltilescounter++;
                                        //chunkdata.Add(indexinlevelarray, new chunkdata());
                                    }
                                }
                            }
                        }
                    }
                }
                */














                /*
                chunkdata = new chunkdata[NUMBEROFFACES][];
                //chunkdata = new chunkdata[levelmap.Length];

                for (int i = 0; i < chunkdata.Length; i++)
                {
                    //chunkdata[i] = new chunkdata[totaltilescounter * arraymultiplier];
                    chunkdata[i] = new chunkdata[levelmap.Length];

                    for (int j = 0;j < chunkdata[i].Length;j++)
                    {
                        chunkdata[i][j] = new chunkdata();
                    }
                }*/
















                /*
                chunkdata = new List<chunkdata[]>();


                for (int i = 0; i < totaltilescounter; i++)
                {
                    //chunkdata.Add(new chunkdata[tutorialcubeaschunkprim.chunkdivisionx * tutorialcubeaschunkprim.chunkdivisiony * tutorialcubeaschunkprim.chunkdivisionz]);
                    chunkdata.Add(new chunkdata[sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz]);

                }*/




                /*
                arrayoflevelparts = new int[sccs.sccsgraphicssec.currentsccsgraphicssec.leveldivisionx * sccs.sccsgraphicssec.currentsccsgraphicssec.leveldivisiony * sccs.sccsgraphicssec.currentsccsgraphicssec.leveldivisionz][];

                //totaltilescounter = 0;
                for (var divx = 0; divx < sccs.sccsgraphicssec.currentsccsgraphicssec.leveldivisionx; divx++)
                {
                    for (var divy = 0; divy < sccs.sccsgraphicssec.currentsccsgraphicssec.leveldivisiony; divy++)
                    {
                        for (var divz = 0; divz < sccs.sccsgraphicssec.currentsccsgraphicssec.leveldivisionz; divz++)
                        {
                            int indexinlevelpartsarray = divz + (sccs.sccsgraphicssec.currentsccsgraphicssec.leveldivisionx) * (divy + (sccs.sccsgraphicssec.currentsccsgraphicssec.leveldivisiony) * divz); //y is always 0 on floor tiles

                            int startx = sccslevelgen.minx + (divx * sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsdivx);
                            int starty = sccslevelgen.miny + (divy * sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsdivy);
                            int startz = sccslevelgen.minz + (divz * sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsdivz);


                            // for (var x = minx; x < maxx; x++)
                            //{
                            //for (var y = miny; y < maxy; y++)
                            //{
                            //for (var z = minz; z < maxz; z++)
                            //{

                            int iteratorindex = 0;

                            for (var x = startx; x < startx + sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsdivx; x++)
                            {
                                for (var y = starty; y < starty + sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsdivy; y++)
                                {
                                    for (var z = startz; z < startz + sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsdivz; z++)
                                    {
                                        arrayoflevelparts[indexinlevelpartsarray] = new int[sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsdivx * sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsdivy * sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsdivz];

                                        //Console.WriteLine("/x:" + x + "/y:" + y + "/z:" + z);

                                        int xx = x;
                                        int yy = y;
                                        int zz = z;

                                        if (xx < 0)
                                        {
                                            xx *= -1;
                                            xx = xx + (maxx - 1);
                                        }

                                        if (yy < 0)
                                        {
                                            yy *= -1;
                                            yy = yy + (maxy - 1);
                                        }
                                        if (zz < 0)
                                        {
                                            zz *= -1;
                                            zz = zz + (maxz - 1);
                                        }

                                        int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                                        if (indexinlevelarray < somewidth * someheight * somedepth)
                                        {
                                            int typeofterraintile = levelmap[indexinlevelarray];

                                            if (typeofterraintile == 0 ||
                                               typeofterraintile == 1101 ||
                                               typeofterraintile == 1102 ||
                                               typeofterraintile == 1103 ||
                                               typeofterraintile == 1104 ||
                                               typeofterraintile == 1105 ||
                                               typeofterraintile == 1106 ||
                                               typeofterraintile == 1107 ||
                                               typeofterraintile == 1108 ||
                                               typeofterraintile == 1109 ||
                                               typeofterraintile == 1110 ||
                                               typeofterraintile == 1111 ||
                                               typeofterraintile == 1112 ||
                                               typeofterraintile == -99 ||
                                               typeofterraintile == 1115)
                                            {
                                                arrayoflevelparts[indexinlevelpartsarray][iteratorindex] = indexinlevelarray;
                                                iteratorindex++;
                                                //totaltilescounter++;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }*/
                //Console.WriteLine("totaltilescounterdiv:" + totaltilescounter + "/entireleveltilesdiv:" + totaltiles);

























                /*
                //
                //sccsgraphicssec.currentsccsgraphicssec.numberoffaces
                thecallbackstructdata = new sccslevelgen.callbackstructdata[NUMBEROFFACES][][]; //

                for (int d = 0; d < thecallbackstructdata.Length; d++)
                {
                    //thecallbackstructdata[f] = new callbackstructdata[sccsgraphicssec.currentsccsgraphicssec.leveldivisionx * sccsgraphicssec.currentsccsgraphicssec.leveldivisiony * sccsgraphicssec.currentsccsgraphicssec.leveldivisionz];
                    thecallbackstructdata[d] = new sccslevelgen.callbackstructdata[sccsgraphicssec.currentsccsgraphicssec.leveldivisionx * sccsgraphicssec.currentsccsgraphicssec.leveldivisiony * sccsgraphicssec.currentsccsgraphicssec.leveldivisionz][];

                    //chunkdivisionx * chunkdivisiony * chunkdivisionz


                    //Console.WriteLine(thecallbackstructdata[f].Length);
                    for (int f = 0; f < thecallbackstructdata[d].Length; f++)
                    {
                        //thevaluereturn[f][i] = new threaddata();
                        thecallbackstructdata[d][f] = new sccslevelgen.callbackstructdata[sccsgraphicssec.chunkdivisionx * sccsgraphicssec.chunkdivisiony * sccsgraphicssec.chunkdivisionz];
                        //arrayofthreaddata[f][i].indexofswtcdirtyarea = i;

                        //thecallbackstructdata[f][i].minx = x;//, y, z,, y + incrementsdivy, z + incrementsdivz, somemaincounter, out somemaincounter_;
                        //thecallbackstructdata[f][i].maxx = x + incrementsdivx;
                        //thecallbackstructdata[f][i].maxx = y;
                        //thecallbackstructdata[f][i].maxx = y + incrementsdivy;
                        //thecallbackstructdata[f][i].maxx = z;
                        //thecallbackstructdata[f][i].maxx = z + incrementsdivz;
                    }
                }*/












                /*
                for (var x = minx; x < maxx; x++)
                {
                    for (var y = miny; y < maxy; y++)
                    {
                        for (var z = minz; z < maxz; z++)
                        {
                            int xx = x;
                            int yy = y;
                            int zz = z;

                            if (xx < 0)
                            {
                                xx *= -1;
                                xx = xx + (maxx - 1);
                            }

                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = yy + (maxy - 1);
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = zz + (maxz - 1);
                            }

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (levelmap[indexinlevelarray] == 0)
                            {

                            }
                        }
                    }
                }*/













                //unLOADING CHUNK to XML
                //unLOADING CHUNK to XML
                string pathofrelease = Directory.GetCurrentDirectory();
                //Console.WriteLine(pathofrelease);
                string pathofchunkmap = pathofrelease + @"\chunkmaps\";

                if (!Directory.Exists(pathofchunkmap))
                {
                    //Console.WriteLine("created directory");
                    Directory.CreateDirectory(pathofchunkmap);
                }

                //int writetofilecounter = 0;

                System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                customCulture.NumberFormat.NumberDecimalSeparator = ".";
                System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;

                var path = pathofchunkmap + @"\levelfloordata" + ".xml";

                var writer = new XmlTextWriter(path, System.Text.Encoding.UTF8);

                writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
                writer.Formatting = Formatting.Indented;
                writer.Indentation = 2;

                writer.WriteStartElement("root"); // open 0

                writer.WriteStartElement("size"); //open 4 //"\r\n" + 
                writer.WriteStartElement("w");
                writer.WriteValue(somewidth);
                writer.WriteEndElement();
                writer.WriteStartElement("h");
                writer.WriteValue(someheight);
                writer.WriteEndElement();
                writer.WriteStartElement("d");
                writer.WriteValue(somedepth);
                writer.WriteEndElement();
                writer.WriteStartElement("minx");
                writer.WriteValue(minx);
                writer.WriteEndElement();
                writer.WriteStartElement("maxx");
                writer.WriteValue(maxx);
                writer.WriteEndElement();
                writer.WriteStartElement("minz");
                writer.WriteValue(minz);
                writer.WriteEndElement();
                writer.WriteStartElement("maxz");
                writer.WriteValue(maxz);
                writer.WriteEndElement();
                writer.WriteStartElement("miny");
                writer.WriteValue(miny);
                writer.WriteEndElement();
                writer.WriteStartElement("maxy");
                writer.WriteValue(maxy);
                writer.WriteEndElement();

                writer.WriteEndElement(); //open 4 //"\r\n" + 

                writer.WriteStartElement("intmap"); //open 4 //"\r\n" + 
                writer.WriteValue("\r\n");
                //for (int x = 0; x < levelmapfloor.Length; x++)
                //{
                //    writer.WriteValue(levelmapfloor[x]);
                //    writer.WriteValue("\r\n");
                //}
                writer.WriteValue(levelmap);
                writer.WriteValue("\r\n");

                writer.WriteEndElement();


                writer.WriteStartElement("walkerdecisions"); //open 4 //"\r\n" + 
                writer.WriteValue("\r\n");
                //for (int x = 0; x < levelmapfloor.Length; x++)
                //{
                //    writer.WriteValue(levelmapfloor[x]);
                //    writer.WriteValue("\r\n");
                //}
                writer.WriteValue(walkerdecisions.ToArray());
                writer.WriteValue("\r\n");
                writer.WriteEndElement();




                writer.WriteEndElement(); //close 2
                writer.Close();

                Console.WriteLine("generated new level");



                writer.Dispose();



                thewalkers = null;
                walkerdecisions.Clear();// = new List<int>();
                walkerdecisions = null;
                walkerdecisionsarray = null;
                adjacenttiles = null;



            }
            else
            {

                string pathofrelease0 = Directory.GetCurrentDirectory();
                //Console.WriteLine(pathofrelease);
                string pathofchunkmap0 = pathofrelease0 + @"\chunkmaps\";

                if (!Directory.Exists(pathofchunkmap0))
                {
                    //Console.WriteLine("created directory");
                    Directory.CreateDirectory(pathofchunkmap0);
                }

                //int writetofilecounter = 0;

                System.Globalization.CultureInfo customCulture0 = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
                customCulture0.NumberFormat.NumberDecimalSeparator = ".";
                System.Threading.Thread.CurrentThread.CurrentCulture = customCulture0;




                //LOADING CHUNK BACK INTO MEMORY
                //LOADING CHUNK BACK INTO MEMORY
                string path0 = pathofrelease0 + @"\chunkmaps\" + "levelfloordata.xml";

                //https://stackoverflow.com/questions/18891207/how-to-get-value-from-a-specific-child-element-in-xml-using-xmlreader
                //var path = @"C:\Users\steve\Desktop\#chunkmaps\" + "chunkmap" + writetofilecounter + ".xml";
                var reader = new XmlTextReader(path0);


                if (reader.ReadToDescendant("w"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    somewidth = someres[0];
                    //Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();

                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("h"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    someheight = someres[0];
                    //Console.WriteLine(someres[0]);

                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();


                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("d"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    somedepth = someres[0];
                    ///Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();


                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("minx"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    minx = someres[0];
                    //Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();

                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("maxx"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    maxx = someres[0];
                    //Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();


                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("minz"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    minz = someres[0];
                    //Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();

                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("maxz"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    maxz = someres[0];
                    //Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();

                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("miny"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    miny = someres[0];
                    //Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();

                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("maxy"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    var someres = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    maxy = someres[0];
                    //Console.WriteLine(someres[0]);
                    //for (int by = 0; by < ia.Length; by++)
                    //{
                    //    Console.WriteLine(ia[by]);
                    //}
                }
                reader.Close();






                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("intmap"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    levelmap = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    /*for (int by = 0; by < levelmap.Length; by++)
                    {
                        Console.WriteLine(levelmap[by]);
                    }*/
                }
                reader.Close();





                reader = new XmlTextReader(path0);
                if (reader.ReadToDescendant("walkerdecisions"))
                {
                    reader.Read();//this moves reader to next node which is text 
                    var result = reader.Value; //this might give value than 

                    //https://stackoverflow.com/questions/2959161/convert-string-to-int-array-using-linq
                    walkerdecisionsarray = result.Split(' ').Select(n => Convert.ToInt32(n)).ToArray();

                    /*for (int by = 0; by < levelmap.Length; by++)
                    {
                        Console.WriteLine(levelmap[by]);
                    }*/
                }
                reader.Close();






                maxtileamount = (somewidth * somedepth) * 1;

                thewalkers = new thewalker[somepointermax];

                for (int x = 0; x < thewalkers.Length; x++)
                {
                    thewalkers[x].x = 0;
                    thewalkers[x].y = 0;
                    thewalkers[x].z = 0;
                }

                Console.WriteLine("/minx:" + (minx) + "/miny:" + (miny) + "/minz:" + (minz) + "/maxx:" + (maxx) + "/maxy:" + (maxy) + "/maxz:" + (maxz));




                int regeneratinglevelbasedonxmlinfo = 0;
                if (regeneratinglevelbasedonxmlinfo == 1)
                {
                    int generatewhichmap = 0;

                    if (generatewhichmap == 0)
                    {

                        int arraylength = somewidth * someheight * somedepth;//.Length;

                        levelmap = new int[arraylength];
                        adjacenttiles = new int[arraylength];




                        /*
                        for (var x = (minx); x < maxx; x++)
                        {
                            for (var y = (miny); y < maxy; y++)
                            {
                                for (var z = (minz); z < maxz; z++)
                                {
                                    //Console.WriteLine(y);

                                    int xx = x;
                                    int yy = y;
                                    int zz = z;

                                    if (xx < 0)
                                    {
                                        xx *= -1;
                                        xx = xx + (maxx - 1);
                                    }

                                    if (yy < 0)
                                    {
                                        yy *= -1;
                                        yy = yy + (maxy - 1);
                                    }
                                    if (zz < 0)
                                    {
                                        zz *= -1;
                                        zz = zz + (maxz - 1);
                                    }

                                    int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                                    if (indexinlevelarray < somewidth * someheight * somedepth)
                                    {
                                        if (y == 0)
                                        {
                                            levelmap[indexinlevelarray] = 999;
                                            adjacenttiles[indexinlevelarray] = 999;
                                        }
                                        else
                                        {
                                            levelmap[indexinlevelarray] = y * -1;
                                            adjacenttiles[indexinlevelarray] = y * -1;
                                        }
                                    }
                                }
                            }
                        }*/





                        int countermaxtile = 0;

                        for (int t = 0; t < maxtileamount; t++)
                        {
                            for (int p = 0; p < somepointermax; p++)
                            {
                                for (int i = 0; i < walkerdecisionsarray.Length; i++)
                                {


                                    for (int xi = -neighbooraddx; xi <= neighbooraddx; xi++)
                                    {
                                        //for (int yi = y; yi <= y; yi++)
                                        {
                                            for (int zi = -neighbooraddz; zi <= neighbooraddz; zi++)
                                            {
                                                int neighboorx = thewalkers[p].x + xi;
                                                //int neighboory = y;
                                                int neighboorz = thewalkers[p].z + zi;

                                                int xxi = neighboorx;// (int)Math.Round(tilepos.X);
                                                int yyi = 0;// (int)Math.Round(tilepos.Y);
                                                int zzi = neighboorz;//(int)Math.Round(tilepos.Z);

                                                if (xxi < 0)
                                                {
                                                    xxi *= -1;
                                                    xxi = xxi + (maxx - 1);
                                                }

                                                if (yyi < 0)
                                                {
                                                    yyi *= -1;
                                                    yyi = yyi + (maxy - 1);
                                                }
                                                if (zzi < 0)
                                                {
                                                    zzi *= -1;
                                                    zzi = zzi + (maxz - 1);
                                                }


                                                if (xxi < minx)
                                                {
                                                    xxi += 1;
                                                    thewalkers[p].x = xxi;
                                                }
                                                else if (xxi > maxx - 1)
                                                {
                                                    xxi -= 1;
                                                    thewalkers[p].x = xxi;
                                                }


                                                if (zzi < minz)
                                                {
                                                    zzi += 1;
                                                    thewalkers[p].z = zzi;
                                                }
                                                else if (zzi > maxz - 1)
                                                {
                                                    zzi -= 1;
                                                    thewalkers[p].z = zzi;
                                                }

                                                /*
                                                if (xxi < minx && xxi <= maxx - 1)
                                                {

                                                }
                                                if (zzi >= minz && zzi <= maxz - 1)
                                                {

                                                }*/


                                                //int indexinarray = xxi + somewidth * (yyi + someheight * zzi); //y is always 0 on floor tiles
                                                int indexinarray = xxi + somewidth * (yyi + someheight * zzi); //y is always 0 on floor tiles

                                                if (indexinarray < somewidth * someheight * somedepth)
                                                {
                                                    //levelmap[indexinarray] = 0;

                                                    if (levelmap[indexinarray] == 999)
                                                    {
                                                        //Console.WriteLine(indexinarray);
                                                        //levelmapsortingtilesremains[indexinarray] = 0;
                                                        //levelmapsortingtiles[indexinarray] = 0;
                                                        //Console.WriteLine(listoftileloc[p][1]);
                                                        levelmap[indexinarray] = 0;
                                                        adjacenttiles[indexinarray] = 0;
                                                        countermaxtile++;
                                                    }
                                                }
                                                else
                                                {
                                                    //int neighboorx = listoftileloc[p][0] + xi;
                                                    //int neighboory = y;
                                                    //int neighboorz = listoftileloc[p][2] + zi;

                                                    /*xxi = thewalkers[p].x;// (int)Math.Round(tilepos.X);
                                                    yyi = 0;// (int)Math.Round(tilepos.Y);
                                                    zzi = thewalkers[p].z;//(int)Math.Round(tilepos.Z);

                                                    if (xxi < 0)
                                                    {
                                                        xxi *= -1;
                                                        xxi = xxi + (maxx - 1);
                                                    }

                                                    if (yyi < 0)
                                                    {
                                                        yyi *= -1;
                                                        yyi = yyi + (maxy - 1);
                                                    }
                                                    if (zzi < 0)
                                                    {
                                                        zzi *= -1;
                                                        zzi = zzi + (maxz - 1);
                                                    }

                                                    //int indexinarray = xxi + somewidth * (yyi + someheight * zzi); //y is always 0 on floor tiles
                                                    indexinarray = xxi + somewidth * (yyi + someheight * zzi); //y is always 0 on floor tiles

                                                    if (indexinarray < somewidth * someheight * somedepth)
                                                    {
                                                        //levelmapsortingtilesremains[indexinarray] = 999;
                                                        //levelmapsortingtiles[indexinarray] = 999;
                                                        //Console.WriteLine(listoftileloc[p][1]);
                                                        levelmap[indexinarray] = 999;
                                                        adjacenttiles[indexinarray] = 999;
                                                    }
                                                    else
                                                    {
                                                        //listoftileloc[p] = new int[3];
                                                        thewalkers[p].x = 0;
                                                        thewalkers[p].y = 0;
                                                        thewalkers[p].z = 0;
                                                    }*/

                                                    /*levelmapsortingtilesremains[indexinlevelarray] = 0;
                                                    levelmapsortingtiles[indexinlevelarray] = 0;
                                                    //Console.WriteLine(listoftileloc[p][1]);
                                                    levelmap[indexinlevelarray] = 0;
                                                    adjacenttiles[indexinlevelarray] = 0;*/
                                                }

                                            }
                                        }
                                    }

                                    if (walkerdecisionsarray[i] == 0)
                                    {
                                        thewalkers[p].x -= 1;
                                    }
                                    else if (walkerdecisionsarray[i] == 1)
                                    {
                                        thewalkers[p].x = 0;
                                        thewalkers[p].y = 0;
                                        thewalkers[p].z = 0;
                                    }
                                    else if (walkerdecisionsarray[i] == 2)
                                    {
                                        thewalkers[p].x += 1;
                                    }
                                    else if (walkerdecisionsarray[i] == 3)
                                    {
                                        thewalkers[p].x = 0;
                                        thewalkers[p].y = 0;
                                        thewalkers[p].z = 0;
                                    }
                                    else if (walkerdecisionsarray[i] == 4)
                                    {
                                        thewalkers[p].z -= 1;
                                    }
                                    else if (walkerdecisionsarray[i] == 5)
                                    {
                                        thewalkers[p].x = 0;
                                        thewalkers[p].y = 0;
                                        thewalkers[p].z = 0;
                                    }
                                    else if (walkerdecisionsarray[i] == 6)
                                    {
                                        thewalkers[p].z += 1;
                                    }
                                    else if (walkerdecisionsarray[i] == 7)
                                    {
                                        thewalkers[p].x = 0;
                                        thewalkers[p].y = 0;
                                        thewalkers[p].z = 0;
                                    }
                                }
                            }
                        }

                    }
                    else if (generatewhichmap == 1)
                    {
                        for (var x = minx; x < maxx; x++)
                        {
                            for (var y = miny; y < maxy; y++)
                            {
                                for (var z = minz; z < maxz; z++)
                                {
                                    int xx = x;
                                    int yy = y;
                                    int zz = z;

                                    if (xx < 0)
                                    {
                                        xx *= -1;
                                        xx = xx + (maxx - 1);
                                    }

                                    if (yy < 0)
                                    {
                                        yy *= -1;
                                        yy = yy + (maxy - 1);
                                    }
                                    if (zz < 0)
                                    {
                                        zz *= -1;
                                        zz = zz + (maxz - 1);
                                    }

                                    int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                                    if (levelmap[indexinlevelarray] == 1101 ||
                                      levelmap[indexinlevelarray] == 1102 ||
                                      levelmap[indexinlevelarray] == 1103 ||
                                      levelmap[indexinlevelarray] == 1104 ||
                                      levelmap[indexinlevelarray] == 1105 ||
                                      levelmap[indexinlevelarray] == 1106 ||
                                      levelmap[indexinlevelarray] == 1107 ||
                                      levelmap[indexinlevelarray] == 1108 ||
                                      levelmap[indexinlevelarray] == 1109 ||
                                      levelmap[indexinlevelarray] == 1110 ||
                                      levelmap[indexinlevelarray] == 1111 ||
                                      levelmap[indexinlevelarray] == 1112 ||
                                      levelmap[indexinlevelarray] == -99)
                                    {
                                        levelmap[indexinlevelarray] = 999;
                                        //adjacenttiles[indexinlevelarray] = 999;
                                        adjacenttiles[indexinlevelarray] = 999;// levelmap[indexinlevelarray];
                                    }
                                    else
                                    {
                                        adjacenttiles[indexinlevelarray] = levelmap[indexinlevelarray];
                                    }
                                }
                            }
                        }
                    }



                    for (var x = minx; x < maxx; x++)
                    {
                        for (var y = miny; y < maxy; y++)
                        {
                            for (var z = minz; z < maxz; z++)
                            {
                                int xx = x;
                                int yy = y;
                                int zz = z;

                                if (xx < 0)
                                {
                                    xx *= -1;
                                    xx = xx + (maxx - 1);
                                }

                                if (yy < 0)
                                {
                                    yy *= -1;
                                    yy = yy + (maxy - 1);
                                }
                                if (zz < 0)
                                {
                                    zz *= -1;
                                    zz = zz + (maxz - 1);
                                }

                                int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                                if (levelmap[indexinlevelarray] == 0)
                                {
                                    checkAllSides(x, y, z, indexinlevelarray);
                                }

                            }
                        }
                    }


                    createfinal();
                }
                else
                {





                    //arrayofchunkinbundle = new int[levelmap.Length]; 
                    arrayofindexes = new int[levelmap.Length];
                    //arraychunkdatalod0 = new chunkdata[levelmap.Length];

                    for (int i = 0; i < arrayofindexes.Length; i++)
                    {
                        arrayofindexes[i] = -1;
                    }




                    arrayofindexesalt = new int[levelmap.Length * arraymultiplier];
                    //arraychunkdatalod0 = new chunkdata[levelmap.Length];

                    for (int i = 0; i < arrayofindexesalt.Length; i++)
                    {
                        arrayofindexesalt[i] = -1;
                        //arrayofindexesalt[i] = new int[sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz];// -1;

                    }






                    /*
                    chunkdata = new Dictionary<int, chunkdata>();
                    */























                    int totaltiles = 0;
                    int totaltilescounter = 0;

                    for (int ilod = 0; ilod < 1; ilod++)
                    {

                        for (int f = 0; f < 1; f++)
                        {

                            for (int x = minx, xe = 0; x < maxx; x += sccsgraphicssec.currentsccsgraphicssec.incrementsfracx, xe++)
                            {
                                for (int y = miny, ye = 0; y < maxy; y += sccsgraphicssec.currentsccsgraphicssec.incrementsfracy, ye++)
                                {
                                    for (int z = minz, ze = 0; z < maxz; z += sccsgraphicssec.currentsccsgraphicssec.incrementsfracz, ze++)
                                    {
                                        //Console.WriteLine("test");
                                        for (int xi = x, secx = -2; xi < x + sccsgraphicssec.currentsccsgraphicssec.incrementsfracx; xi++, secx++)
                                        {
                                            for (int yi = y, secy = -2; yi < y + sccsgraphicssec.currentsccsgraphicssec.incrementsfracy; yi++, secy++)
                                            {
                                                for (int zi = z, secz = -2; zi < z + sccsgraphicssec.currentsccsgraphicssec.incrementsfracz; zi++, secz++)
                                                {
                                                    int xx = xi;
                                                    int yy = yi;
                                                    int zz = zi;

                                                    if (xx < 0)
                                                    {
                                                        xx *= -1;
                                                        xx = xx + (maxx - 1);
                                                    }

                                                    if (yy < 0)
                                                    {
                                                        yy *= -1;
                                                        yy = yy + (maxy - 1);
                                                    }
                                                    if (zz < 0)
                                                    {
                                                        zz *= -1;
                                                        zz = zz + (maxz - 1);
                                                    }

                                                    int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                                                    if (indexinlevelarray < somewidth * someheight * somedepth)
                                                    {

                                                        //arrayofindexes[indexinlevelarray] = indexinlevelarray;

                                                        int typeofterraintile = levelmap[indexinlevelarray];

                                                        if (typeofterraintile == 0 ||
                                                            typeofterraintile == 1101 ||
                                                            typeofterraintile == 1102 ||
                                                            typeofterraintile == 1103 ||
                                                            typeofterraintile == 1104 ||
                                                            typeofterraintile == 1105 ||
                                                            typeofterraintile == 1106 ||
                                                            typeofterraintile == 1107 ||
                                                            typeofterraintile == 1108 ||
                                                            typeofterraintile == 1109 ||
                                                            typeofterraintile == 1110 ||
                                                            typeofterraintile == 1111 ||
                                                            typeofterraintile == 1112 ||
                                                            typeofterraintile == -99 ||
                                                            typeofterraintile == 1115)
                                                        {

                                                            for (int xxi = 0; xxi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx; xxi++)
                                                            {
                                                                for (int yyi = 0; yyi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony; yyi++)
                                                                {
                                                                    for (int zzi = 0; zzi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz; zzi++)
                                                                    {
                                                                        int indexofchunkinbundle = xxi + (sccs.sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx) * (yyi + (sccs.sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony) * zzi); //y is always 0 on floor tiles
                                                                        int indexinmapplusbundlechunks = (indexinlevelarray * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + indexofchunkinbundle;

                                                                        int xxxi = secx;
                                                                        int yyyi = secy;
                                                                        int zzzi = secz;


                                                                        if (xxxi < 0)
                                                                        {
                                                                            xxxi *= -1;
                                                                            xxxi = xxxi + (2 - 1);
                                                                        }

                                                                        if (yyyi < 0)
                                                                        {
                                                                            yyyi *= -1;
                                                                            yyyi = yyyi + (2 - 1);
                                                                        }
                                                                        if (zzzi < 0)
                                                                        {
                                                                            zzzi *= -1;
                                                                            zzzi = zzzi + (2 - 1);
                                                                        }




                                                                        int indexincrements = xxxi + (sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsfracx) * (yyyi + (sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsfracy) * zzzi); //y is always 0 on floor tiles

                                                                        int chunkindexincrements = (indexincrements * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + indexofchunkinbundle;

                                                                        //Console.WriteLine(chunkindexincrements);
                                                                        arrayofindexesalt[indexinmapplusbundlechunks] = (chunkindexincrements);


                                                                    }
                                                                }
                                                            }

                                                            totaltilescounter++;

                                                        }
                                                        else
                                                        {

                                                        }
                                                    }

                                                    totaltiles++;
                                                }
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    }



                    /*
                    int totaltiles = 0;
                    int totaltilescounter = 0;
                    for (int x = minx; x < maxx; x++)
                    {
                        for (int y = miny; y < maxy; y++)
                        {
                            for (int z = minz; z < maxz; z++)
                            {
                                //Console.WriteLine("/x:" + x + "/y:" + y + "/z:" + z);

                                int xx = x;
                                int yy = y;
                                int zz = z;

                                if (xx < 0)
                                {
                                    xx *= -1;
                                    xx = xx + (maxx - 1);
                                }

                                if (yy < 0)
                                {
                                    yy *= -1;
                                    yy = yy + (maxy - 1);
                                }
                                if (zz < 0)
                                {
                                    zz *= -1;
                                    zz = zz + (maxz - 1);
                                }

                                int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles


                                if (indexinlevelarray < somewidth * someheight * somedepth)
                                {

                                    //arrayofindexes[indexinlevelarray] = indexinlevelarray;

                                    int typeofterraintile = levelmap[indexinlevelarray];

                                    if (typeofterraintile == 0 ||
                                        typeofterraintile == 1101 ||
                                        typeofterraintile == 1102 ||
                                        typeofterraintile == 1103 ||
                                        typeofterraintile == 1104 ||
                                        typeofterraintile == 1105 ||
                                        typeofterraintile == 1106 ||
                                        typeofterraintile == 1107 ||
                                        typeofterraintile == 1108 ||
                                        typeofterraintile == 1109 ||
                                        typeofterraintile == 1110 ||
                                        typeofterraintile == 1111 ||
                                        typeofterraintile == 1112 ||
                                        typeofterraintile == -99 ||
                                        typeofterraintile == 1115)
                                    {






                                        for (int xi = 0; xi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx; xi++)
                                        {
                                            for (int yi = 0; yi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony; yi++)
                                            {
                                                for (int zi = 0; zi < sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz; zi++)
                                                {
                                                    int indexofchunkinbundle = xi + (sccs.sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx) * (yi + (sccs.sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony) * zi); //y is always 0 on floor tiles

                                                    int indexinmapplusbundlechunks = (indexinlevelarray * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + indexofchunkinbundle;

                                                    //arrayofindexesalt[indexinmapplusbundlechunks] = (totaltilescounter * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + indexofchunkinbundle;

                                                    //int indexalt = (totaltilescounter * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + indexofchunkinbundle;
                                                    /*
                                                    int xxi = secx;
                                                    int yyi = secy;
                                                    int zzi = secz;*/
                    /*
                    if (xxi < 0)
                    {
                        xxi *= -1;
                        xxi = xxi + (sccsgraphicssec.currentsccsgraphicssec.incrementsfracx - 1);
                    }

                    if (yyi < 0)
                    {
                        yyi *= -1;
                        yyi = yyi + (sccsgraphicssec.currentsccsgraphicssec.incrementsfracy - 1);
                    }
                    if (zzi < 0)
                    {
                        zzi *= -1;
                        zzi = zzi + (sccsgraphicssec.currentsccsgraphicssec.incrementsfracz - 1);
                    }*/


                    //Console.WriteLine("x:" + sccsgraphicssec.currentsccsgraphicssec.incrementsfracx + "/y:" + sccsgraphicssec.currentsccsgraphicssec.incrementsfracy + "/z:" + sccsgraphicssec.currentsccsgraphicssec.incrementsfracz);
                    //Console.WriteLine("x:" + xxi + "/y:" + yyi + "/z:" + zzi);

                    /*
                    int indexincrements = xxi + (sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsfracx) * (yyi + (sccs.sccsgraphicssec.currentsccsgraphicssec.incrementsfracy) * zzi); //y is always 0 on floor tiles

                    indexincrements = (indexincrements * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + indexofchunkinbundle;
                    //Console.WriteLine(indexincrements);




                    arrayofindexesalt[indexinmapplusbundlechunks] = (indexincrements);



                    //arrayofindexes[indexinmapplusbundlechunks] = indexalt;//
                    //totaltilescounter++;

                }
            }
        }

        int indexalt1 = (totaltilescounter * (sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionx * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractiony * sccsgraphicssec.currentsccsgraphicssec.thechunkbundlefractionz)) + 0;

        arrayofindexes[indexinlevelarray] = totaltilescounter;


        totaltilescounter++;




        //chunkdata.Add(indexinlevelarray, new chunkdata());
    }
    else
    {
        //arrayofindexes[indexinlevelarray] = -1;
    }
}
totaltiles++;
}
}
}*/
                    Console.WriteLine("totaltilescounter:" + totaltilescounter + "/entireleveltiles:" + totaltiles);





                    //Let's say a level of 10x10x10
                    //let's say bundle chunks of 2x2x2 
                    //total tiles == 1000 * 2 * 2 * 2 = 8000 tiles
                    //index 







                    //arraychunkdatalod0 = new chunkdata[levelmap.Length];
                    /*arraychunkdatalod0top = new chunkdata[totaltilescounter * 8];
                    arraychunkdatalod0bottom = new chunkdata[totaltilescounter * 8];
                    arraychunkdatalod0left = new chunkdata[totaltilescounter * 8];
                    arraychunkdatalod0right = new chunkdata[totaltilescounter * 8];
                    arraychunkdatalod0front = new chunkdata[totaltilescounter * 8];
                    arraychunkdatalod0bottom = new chunkdata[totaltilescounter * 8];*/















                    /*
                    chunkdata = new chunkdata[levelofdetails][][][];


                    for (int ilod = 0; ilod < levelofdetails; ilod++)
                    {
                        chunkdata[ilod] = new chunkdata[NUMBEROFFACES][][];

                        for (int f = 0; f < NUMBEROFFACES; f++)
                        {
                            chunkdata[ilod][f] = new chunkdata[sccsgraphicssec.currentsccsgraphicssec.leveldivisionx * sccsgraphicssec.currentsccsgraphicssec.leveldivisiony * sccsgraphicssec.currentsccsgraphicssec.leveldivisionz][];

                            for (int ld = 0; ld < sccsgraphicssec.currentsccsgraphicssec.leveldivisionx * sccsgraphicssec.currentsccsgraphicssec.leveldivisiony * sccsgraphicssec.currentsccsgraphicssec.leveldivisionz; ld++)
                            {
                                chunkdata[ilod][f][ld] = new chunkdata[sccsgraphicssec.currentsccsgraphicssec.incrementsfracx * sccsgraphicssec.currentsccsgraphicssec.incrementsfracy * sccsgraphicssec.currentsccsgraphicssec.incrementsfracz * arraymultiplier];


                            }
                        }
                    }
                    */








                    /*
                    chunkdata = new chunkdata[levelofdetails][][];


                    for (int ilod = 0; ilod < levelofdetails; ilod++)
                    {
                        chunkdata[ilod] = new chunkdata[NUMBEROFFACES][];

                        for (int f = 0; f < NUMBEROFFACES; f++)
                        {
                            chunkdata[ilod][f] = new chunkdata[totaltilescounter * arraymultiplier];
                        }
                    }*/




                }




                //writetofilecounter = 0;
                /*for (int i = 0; i < arrayofchunks.Length; i++)
                {
                   
                }*/
                //LOADING CHUNK BACK INTO MEMORY
                //LOADING CHUNK BACK INTO MEMORY
            }









        }



        int checky = 0;
        void checkAllSides(int xi, int yi, int zi, int indexinlevelarray)
        {
            /*int xx = xi;
            int yy = yi;
            int zz = zi;

            if (xx < 0)
            {
                xx *= -1;
                xx = xx + (maxx - 1);
            }

            if (yy < 0)
            {
                yy *= -1;
                yy = yy + (maxy - 1);
            }

            if (zz < 0)
            {
                zz *= -1;
                zz = zz + (maxz - 1);
            }*/

            //int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

            if (indexinlevelarray < somewidth * someheight * somedepth)
            {
                if (levelmap[indexinlevelarray] == 0 && xi == minx && yi == 0 ||
                    levelmap[indexinlevelarray] == 0 && zi == minz && yi == 0 ||
                    levelmap[indexinlevelarray] == 0 && xi == maxx - 1 && yi == 0 ||
                    levelmap[indexinlevelarray] == 0 && zi == maxz - 1 && yi == 0 ||

                    levelmap[indexinlevelarray] == 0 && xi == minx && zi == minz && yi == 0 ||
                    levelmap[indexinlevelarray] == 0 && xi == minx && zi == maxz - 1 && yi == 0 ||
                    levelmap[indexinlevelarray] == 0 && xi == maxx - 1 && zi == minz && yi == 0 ||
                    levelmap[indexinlevelarray] == 0 && xi == maxx - 1 && zi == maxz - 1 && yi == 0 ||

                    levelmap[indexinlevelarray] == 999 && xi == minx && yi == 0 ||
                    levelmap[indexinlevelarray] == 999 && zi == minz && yi == 0 ||
                    levelmap[indexinlevelarray] == 999 && xi == maxx - 1 && yi == 0 ||
                    levelmap[indexinlevelarray] == 999 && zi == maxz - 1 && yi == 0 ||

                    levelmap[indexinlevelarray] == 999 && xi == minx && zi == minz && yi == 0 ||
                    levelmap[indexinlevelarray] == 999 && xi == minx && zi == maxz - 1 && yi == 0 ||
                    levelmap[indexinlevelarray] == 999 && xi == maxx - 1 && zi == minz && yi == 0 ||
                    levelmap[indexinlevelarray] == 999 && xi == maxx - 1 && zi == maxz - 1 && yi == 0)
                {
                    //Console.WriteLine("start found bordertile " + levelmap[indexinlevelarray]);
                    adjacenttiles[indexinlevelarray] = 1001;
                    levelmap[indexinlevelarray] = 999;
                    //levelmapsortingtiles[indexinlevelarray] = 998;
                }


                /*if (xi > maxx)
                {
                    Console.WriteLine("limit of map reached00");
                    adjacenttiles[indexinlevelarray] = 1001;
                    levelmap[indexinlevelarray] = 999;
                }*/

            }
            else
            {
                Console.WriteLine("generation out of range issue");
            }


            for (int x = -1; x <= 1; x++)
            {
                for (int z = -1; z <= 1; z++)
                {
                    int checkx = (((xi + x)));

                    int checkz = (((zi + z)));

                    //float checkx = ((currentTilePos.x + x));
                    //float checkz = ((currentTilePos.z + z));

                    if (x != 0 && z != 0)
                    {
                        int xiii = checkx;
                        int ziii = checkz;

                        if (xiii < 0)
                        {
                            xiii *= -1;
                            xiii = xiii + (maxx - 1);
                        }
                        if (ziii < 0)
                        {
                            ziii *= -1;
                            ziii = ziii + (maxz - 1);
                        }

                        int indexinarray0 = xiii + somewidth * (yi + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth && checkx >= minx && checkx <= maxx - 1 && checkz >= minz && checkz <= maxz - 1)
                        {
                            if (levelmap[indexinarray0] == 999) //|| levelmap[indexinarray0] == 0
                            {
                                adjacenttiles[indexinarray0] = 1001;
                            }


                            if (checkx <= minx || checkx >= maxx || checkz <= minz || checkz >= maxz)
                            {
                                if (levelmap[indexinlevelarray] == 0 || levelmap[indexinlevelarray] == 999) //|| levelmap[indexinarray0] == 0
                                {
                                    //Console.WriteLine("limit of map reached" + " " + levelmap[indexinlevelarray] +  " " + levelmap[indexinarray0]);
                                    //adjacenttiles[indexinarray0] = 1001;
                                    adjacenttiles[indexinlevelarray] = 1001;
                                }
                            }

                            /*if (checkx > maxx)
                            {
                                Console.WriteLine("limit of map reached0");
                                adjacenttiles[indexinlevelarray] = 1001;
                                levelmap[indexinlevelarray] = 999;

                            }
                            */



                            /*if (checkx <= minx || checkx >= maxx || checkz <= minz || checkz >= maxz)
                            {

                                adjacenttiles[indexinlevelarray] = 1001;
                                levelmap[indexinlevelarray] = 999;

                                adjacenttiles[indexinarray0] = 1001;
                                levelmap[indexinarray0] = 999;

                                if (levelmap[indexinlevelarray] == 0 || levelmap[indexinlevelarray] == 999) //|| levelmap[indexinarray0] == 0
                                {
                                    Console.WriteLine("limit of map reached");
                                    
                                }
                            }*/


                        }
                        else
                        {
                            /*if (x >= maxx)
                            {
                                Console.WriteLine("limit of map reached1");
                                adjacenttiles[indexinlevelarray] = 1001;
                                levelmap[indexinlevelarray] = 999;
                            }*/
                            levelmap[indexinlevelarray] = 999;
                            adjacenttiles[indexinlevelarray] = 1001;
                            //Console.WriteLine("out of range tile");
                        }
                    }




                    /*
                    if (checkx == xi && checkz == zi + (1) ||
                        checkx == xi && checkz == zi - (1) ||
                        checkx == xi + (1) && checkz == zi ||
                        checkx == xi - (1) && checkz == zi ||

                        checkx == xi + (1) && checkz == zi + (1) ||
                        checkx == xi - (1) && checkz == zi + (1) ||
                        checkx == xi + (1) && checkz == zi - (1) ||
                        checkx == xi - (1) && checkz == zi - (1))
                    {
                        int xiii = checkx;
                        int ziii = checkz;

                        if (xiii < 0)
                        {
                            xiii *= -1;
                            xiii = xiii + (maxx - 1);
                        }
                        if (ziii < 0)
                        {
                            ziii *= -1;
                            ziii = ziii + (maxz - 1);
                        }

                        int indexinarray0 = xiii + somewidth * (yi + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth)
                        {
                            if (levelmap[indexinarray0] == 999) //|| levelmap[indexinarray0] == 0
                            {

                                adjacenttiles[indexinarray0] = 1001;
                            }
                        }
                        else
                        {
                            levelmap[indexinlevelarray] = 999;
                            //levelmapsortingtiles[indexinlevelarray] = 998; //997
                            adjacenttiles[indexinlevelarray] = 1001;
                            Console.WriteLine("out of range tile");
                        }
                    }*/
                }
            }
        }














        int counter = 1;
        public void createfinal()
        {
            if (counter == 1)
            {

                //int somemaxarray0 = somewidth * someheight * somedepth;
                //int somemaxarray1 = somewidth * someheight * (-minz + maxz);

                /*int somecounter = 0;

                int[] somearray = new int[somewidth * someheight * somedepth];
                //array index test
                for (var x = minx; x < maxx; x++)
                {
                    for (var y = miny; y < maxy; y++)
                    {
                        for (var z = minz; z < maxz; z++)
                        {
                            int xx = x;
                            int yy = y;
                            int zz = z;

                            if (xx < 0)
                            {
                                xx *= -1;
                                xx = xx + (maxx - 1);
                            }

                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = yy + (maxy - 1);
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = zz + (maxz - 1);
                            }

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (somearray[indexinlevelarray] != -1)
                            {
                                somearray[indexinlevelarray] = -1;
                            }
                            else
                            {
                                Console.WriteLine("setting same value again");
                            }
                            somecounter++;
                        }
                    }
                }

                int finalcounter = 0;
                for (var x = minx; x < maxx; x++)
                {
                    for (var y = miny; y < maxy; y++)
                    {
                        for (var z = minz; z < maxz; z++)
                        {
                            int xx = x;
                            int yy = y;
                            int zz = z;

                            if (xx < 0)
                            {
                                xx *= -1;
                                xx = xx + (maxx - 1);
                            }

                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = yy + (maxy - 1);
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = zz + (maxz - 1);
                            }

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (somearray[indexinlevelarray] != -1)
                            {
                                finalcounter++;
                            }                        
                        }
                    }
                }

                Console.WriteLine("final counter:" + finalcounter + "/countertiles:" + somecounter + "/totaltiles:" + (somewidth * someheight * somedepth));
                */







                /*
                for (var x = minx; x < maxx; x++)
                {
                    for (var y = miny; y < maxy; y++)
                    {
                        for (var z = minz; z < maxz; z++)
                        {
                            int xx = x;
                            int yy = y;
                            int zz = z;

                            if (xx < 0)
                            {
                                xx *= -1;
                                xx = xx + (maxx - 1);
                            }

                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = yy + (maxy - 1);
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = zz + (maxz - 1);
                            }

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            if (levelmap[indexinlevelarray] == 0)// || levelmap[indexinlevelarray] == 0 || levelmap[indexinlevelarray] == 999 //adjacenttiles[indexinlevelarray] == 1001
                            {
                                checkforbordertiles(x, y, z);
                            }
                        }
                    }
                }*/






                ///////////////////////////////////////////
                ///////////////////////////////////////////
                for (var x = minx; x < maxx; x++)
                {
                    for (var y = miny; y < maxy; y++)
                    {
                        for (var z = minz; z < maxz; z++)
                        {
                            int xx = x;
                            int yy = y;
                            int zz = z;

                            if (xx < 0)
                            {
                                xx *= -1;
                                xx = xx + (maxx - 1);
                            }

                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = yy + (maxy - 1);
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = zz + (maxz - 1);
                            }

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            //if (levelmapsortingtiles[indexinlevelarray] == 998)
                            //{
                            if (adjacenttiles[indexinlevelarray] == 1001)// || levelmap[indexinlevelarray] == 0 || levelmap[indexinlevelarray] == 999
                            {
                                //levelmap[indexinlevelarray] = 1103;
                                buildWallsRerolled(x, y, z);
                                //buildWallsRerolledTHREE(x, y, z);
                                //buildWallsExceptOnFloor(x, y, z);
                            }
                        }
                    }
                }
                ///////////////////////////////////////////
                ///////////////////////////////////////////




                ///////////////////////////////////////////
                ///////////////////////////////////////////
                for (var x = minx; x < maxx; x++)
                {
                    for (var y = miny; y < maxy; y++)
                    {
                        for (var z = minz; z < maxz; z++)
                        {
                            int xx = x;
                            int yy = y;
                            int zz = z;

                            if (xx < 0)
                            {
                                xx *= -1;
                                xx = xx + (maxx - 1);
                            }

                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = yy + (maxy - 1);
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = zz + (maxz - 1);
                            }

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            //if (levelmapsortingtiles[indexinlevelarray] == 998)
                            //{
                            if (adjacenttiles[indexinlevelarray] == 1001 &&

                                levelmap[indexinlevelarray] != 1101 &&
                                levelmap[indexinlevelarray] != 1102 &&
                                levelmap[indexinlevelarray] != 1103 &&
                                levelmap[indexinlevelarray] != 1104 &&
                                levelmap[indexinlevelarray] != 1105 &&
                                levelmap[indexinlevelarray] != 1106 &&
                                levelmap[indexinlevelarray] != 1107 &&
                                levelmap[indexinlevelarray] != 1108 &&
                                levelmap[indexinlevelarray] != 1109 &&
                                levelmap[indexinlevelarray] != 1110 &&
                                levelmap[indexinlevelarray] != 1111 &&
                                levelmap[indexinlevelarray] != 1112 &&
                                levelmap[indexinlevelarray] != 0)
                            {
                                levelmap[indexinlevelarray] = 0; //-99
                            }
                        }
                    }
                }
                ///////////////////////////////////////////
                ///////////////////////////////////////////



                ///////////////////////////////////////////
                ///////////////////////////////////////////
                for (var x = minx; x < maxx; x++)
                {
                    for (var y = miny; y < maxy; y++)
                    {
                        for (var z = minz; z < maxz; z++)
                        {
                            int xx = x;
                            int yy = y;
                            int zz = z;

                            if (xx < 0)
                            {
                                xx *= -1;
                                xx = xx + (maxx - 1);
                            }

                            if (yy < 0)
                            {
                                yy *= -1;
                                yy = yy + (maxy - 1);
                            }
                            if (zz < 0)
                            {
                                zz *= -1;
                                zz = zz + (maxz - 1);
                            }

                            int indexinlevelarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

                            //if (levelmapsortingtiles[indexinlevelarray] == 998)
                            //{

                            if (y == 0)
                            {
                                if (levelmap[indexinlevelarray] == 1101 ||
                                levelmap[indexinlevelarray] == 1102 ||
                                levelmap[indexinlevelarray] == 1103 ||
                                levelmap[indexinlevelarray] == 1104 ||
                                levelmap[indexinlevelarray] == 1105 ||
                                levelmap[indexinlevelarray] == 1106 ||
                                levelmap[indexinlevelarray] == 1107 ||
                                levelmap[indexinlevelarray] == 1108 ||
                                levelmap[indexinlevelarray] == 1109 ||
                                levelmap[indexinlevelarray] == 1110 ||
                                levelmap[indexinlevelarray] == 1111 ||
                                levelmap[indexinlevelarray] == 1112)
                                {

                                    for (int yi = 0; yi < wallheightsize; yi++)
                                    {
                                        var indexinlevelarrayupperlayerwall = xx + somewidth * (yi + someheight * zz);

                                        if (indexinlevelarrayupperlayerwall < somewidth * someheight * somedepth)
                                        {
                                            levelmap[indexinlevelarrayupperlayerwall] = levelmap[indexinlevelarray];
                                        }
                                    }
                                }
                            }


                            if (y == wallheightsize - 1)
                            {
                                if (xx < 0)
                                {
                                    xx *= -1;
                                    xx = xx + (maxx - 1);
                                }

                                if (yy < 0)
                                {
                                    yy *= -1;
                                    yy = yy + (maxy - 1);
                                }
                                if (zz < 0)
                                {
                                    zz *= -1;
                                    zz = zz + (maxz - 1);
                                }

                                var indexinbottomfloor = xx + somewidth * (0 + someheight * zz); //y is always 0 on floor tiles


                                if (levelmap[indexinbottomfloor] == 0)
                                {
                                    var indexforceiling = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles
                                    levelmap[indexforceiling] = 1115;
                                }
                            }
                        }
                    }
                }
                ///////////////////////////////////////////
                ///////////////////////////////////////////



















                counter = 2;
            }
        }







        public void buildWallsRerolled(int x, int y, int z)
        {
            //Console.WriteLine("testing function");

            istypeofl = -2;
            istypeofr = -2;
            istypeoft = -2;
            istypeofb = -2;

            istypeoflt = -2;
            istypeofrt = -2;
            istypeoflb = -2;
            istypeofrb = -2;

            int xx = (x);
            int yy = (y);
            int zz = (z);

            if (xx < 0)
            {
                xx *= -1;
                xx = xx + (maxx - 1);
            }

            if (yy < 0)
            {
                yy *= -1;
                yy = yy + (maxy - 1);
            }
            if (zz < 0)
            {
                zz *= -1;
                zz = zz + (maxz - 1);
            }

            int indexinarray = xx + somewidth * (yy + someheight * zz); //y is always 0 on floor tiles

            /*if (adjacenttiles[indexinarray] == 1001 && x == minx ||
               adjacenttiles[indexinarray] == 1001 && z == minz ||
               adjacenttiles[indexinarray] == 1001 && x == maxx-1 ||
               adjacenttiles[indexinarray] == 1001 && z == maxz-1)
            {
                if (adjacenttiles[indexinarray] == 1001 && x == minx)
                {
                    Console.WriteLine("found bordertile minx");
                }
                else if (adjacenttiles[indexinarray] == 1001 && z == minz)
                {
                    Console.WriteLine("found bordertile minz");
                }
                else if (adjacenttiles[indexinarray] == 1001 && x == maxx - 1)
                {
                    Console.WriteLine("found bordertile maxx");
                }
                else if (adjacenttiles[indexinarray] == 1001 && z == maxz - 1)
                {
                    Console.WriteLine("found bordertile maxz");
                }
            }
            */














            for (int xxi = -1; xxi <= 1; xxi++)
            {
                //int checkx = (((xxi + x)));
                for (int zzi = -1; zzi <= 1; zzi++)
                {
                    //int checkz = (((zzi + z)));

                    int checkx = (((xxi + x)));

                    int checkz = (((zzi + z)));

                    //float checkx = ((currentTilePos.x + x));
                    //float checkz = ((currentTilePos.z + z));
                    if (checkx == x + (1) && checkz == z)
                    {
                        int xiii = checkx;
                        int ziii = checkz;

                        if (xiii < 0)
                        {
                            xiii *= -1;
                            xiii = xiii + (maxx - 1);
                        }
                        if (ziii < 0)
                        {
                            ziii *= -1;
                            ziii = ziii + (maxz - 1);
                        }

                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles


                        if (indexinarray0 < somewidth * someheight * somedepth && checkx >= minx && checkx <= maxx - 1 && checkz >= minz && checkz <= maxz - 1)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeofr = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeofr = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeofr = 1;
                            }



                            /*
                            if (x+1 == maxx-1)
                            {
                                istypeofr = -1;
                            }*/



                            if (adjacenttiles[indexinarray] == 0 && x == maxx - 1 ||
                                adjacenttiles[indexinarray] == 999 && x == maxx - 1 ||
                                adjacenttiles[indexinarray] == 1001 && x == maxx - 1)
                            {
                                //istypeofr = -1;
                            }
                        }
                        else
                        {
                            istypeofr = -1;
                        }

                        /*if (xiii < minx || xiii > maxx - 1 || ziii < minz || ziii > maxz - 1)
                        {
                            istypeofr = -1;
                        }*/
                    }
                    else if (checkx == x - (1) && checkz == z)
                    {
                        int xiii = checkx;
                        int ziii = checkz;

                        if (xiii < 0)
                        {
                            xiii *= -1;
                            xiii = xiii + (maxx - 1);
                        }
                        if (ziii < 0)
                        {
                            ziii *= -1;
                            ziii = ziii + (maxz - 1);
                        }

                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth && checkx >= minx && checkx <= maxx - 1 && checkz >= minz && checkz <= maxz - 1)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeofl = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeofl = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeofl = 1;
                            }

                            /*
                            if (x-1 == minx)
                            {
                                istypeofl = -1;
                            }
                            */

                            if (adjacenttiles[indexinarray] == 0 && x == minx ||
                               adjacenttiles[indexinarray] == 999 && x == minx ||
                               adjacenttiles[indexinarray] == 1001 && x == minx)
                            {
                                //istypeofl = -1;
                            }
                        }
                        else
                        {
                            istypeofl = -1;
                        }

                        /*if (xiii < minx || xiii > maxx - 1 || ziii < minz || ziii > maxz - 1)
                        {
                            istypeofl = -1;
                        }*/
                    }


                    else if (checkx == x && checkz == z + 1)
                    {
                        //Console.WriteLine("testzneighboorfront");
                        int xiii = checkx;
                        int ziii = checkz;

                        if (xiii < 0)
                        {
                            xiii *= -1;
                            xiii = xiii + (maxx - 1);
                        }
                        if (ziii < 0)
                        {
                            ziii *= -1;
                            ziii = ziii + (maxz - 1);
                        }

                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth && checkx >= minx && checkx <= maxx - 1 && checkz >= minz && checkz <= maxz - 1)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeoft = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeoft = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeoft = 1;
                            }

                            /*
                            if (z+1 == maxz - 1)
                            {
                                istypeoft = -1;
                            }*/


                            if (adjacenttiles[indexinarray] == 0 && z == maxz - 1 ||
                                adjacenttiles[indexinarray] == 999 && z == maxz - 1 ||
                                adjacenttiles[indexinarray] == 1001 && z == maxz - 1)
                            {
                                //istypeoft = -1;
                            }
                        }
                        else
                        {
                            istypeoft = -1;
                        }


                        /*if (xiii < minx || xiii > maxx-1 || ziii < minz || ziii > maxz -1)
                        {
                            istypeoft = -1;
                        }*/


                    }

                    else if (checkx == x && checkz == z - (1))
                    {
                        int xiii = checkx;
                        int ziii = checkz;

                        if (xiii < 0)
                        {
                            xiii *= -1;
                            xiii = xiii + (maxx - 1);
                        }
                        if (ziii < 0)
                        {
                            ziii *= -1;
                            ziii = ziii + (maxz - 1);
                        }

                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth && checkx >= minx && checkx <= maxx - 1 && checkz >= minz && checkz <= maxz - 1)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeofb = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeofb = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeofb = 1;
                            }

                            /*
                            if (z-1 == minz)
                            {
                                istypeofb = -1;
                            }
                            */


                            if (adjacenttiles[indexinarray] == 0 && z == minz ||
                               adjacenttiles[indexinarray] == 999 && z == minz ||
                               adjacenttiles[indexinarray] == 1001 && z == minz)
                            {
                                //istypeofb = -1;
                            }
                        }
                        else
                        {
                            istypeofb = -1;
                        }

                        /*if (xiii < minx || xiii > maxx - 1 || ziii < minz || ziii > maxz - 1)
                        {
                            istypeofb = -1;
                        }*/
                    }


                    else if (checkx == x + (1) && checkz == z + (1))
                    {
                        int xiii = checkx;
                        int ziii = checkz;

                        if (xiii < 0)
                        {
                            xiii *= -1;
                            xiii = xiii + (maxx - 1);
                        }
                        if (ziii < 0)
                        {
                            ziii *= -1;
                            ziii = ziii + (maxz - 1);
                        }

                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth && checkx >= minx && checkx <= maxx - 1 && checkz >= minz && checkz <= maxz - 1)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeofrt = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeofrt = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeofrt = 1;
                            }
                            /*
                            if (x+1 == maxx -1  && z+1== maxz -1)
                            {
                                istypeofrt = -1;
                            }*/

                            if (adjacenttiles[indexinarray] == 0 && x == maxx - 1 && adjacenttiles[indexinarray] == 0 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 999 && x == maxx - 1 && adjacenttiles[indexinarray] == 999 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 1001 && x == maxx - 1 && adjacenttiles[indexinarray] == 1001 && z == maxz - 1 ||

                               adjacenttiles[indexinarray] == 0 && x == maxx - 1 && adjacenttiles[indexinarray] == 999 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 999 && x == maxx - 1 && adjacenttiles[indexinarray] == 0 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 0 && x == maxx - 1 && adjacenttiles[indexinarray] == 1001 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 1001 && x == maxx - 1 && adjacenttiles[indexinarray] == 0 && z == maxz - 1 ||

                               adjacenttiles[indexinarray] == 999 && x == maxx - 1 && adjacenttiles[indexinarray] == 1001 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 1001 && x == maxx - 1 && adjacenttiles[indexinarray] == 999 && z == maxz - 1)
                            {
                                //istypeofrt = -1;
                            }
                        }
                        else
                        {
                            istypeofrt = -1;
                        }

                        /*if (xiii < minx || xiii > maxx - 1 || ziii < minz || ziii > maxz - 1)
                        {
                            istypeofrt = -1;
                        }*/
                    }





                    else if (checkx == x - (1) && checkz == z + (1))
                    {
                        int xiii = checkx;
                        int ziii = checkz;

                        if (xiii < 0)
                        {
                            xiii *= -1;
                            xiii = xiii + (maxx - 1);
                        }
                        if (ziii < 0)
                        {
                            ziii *= -1;
                            ziii = ziii + (maxz - 1);
                        }



                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth && checkx >= minx && checkx <= maxx - 1 && checkz >= minz && checkz <= maxz - 1)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeoflt = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeoflt = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeoflt = 1;
                            }


                            /*
                            if (x -1== minx  && z+1 == maxz - 1)
                            {
                                istypeoflt = -1;
                            }*/

                            if (adjacenttiles[indexinarray] == 0 && x == minx && adjacenttiles[indexinarray] == 0 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 999 && x == minx && adjacenttiles[indexinarray] == 999 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 1001 && x == minx && adjacenttiles[indexinarray] == 1001 && z == maxz - 1 ||

                               adjacenttiles[indexinarray] == 0 && x == minx && adjacenttiles[indexinarray] == 999 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 999 && x == minx && adjacenttiles[indexinarray] == 0 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 0 && x == minx && adjacenttiles[indexinarray] == 1001 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 1001 && x == minx && adjacenttiles[indexinarray] == 0 && z == maxz - 1 ||

                               adjacenttiles[indexinarray] == 999 && x == minx && adjacenttiles[indexinarray] == 1001 && z == maxz - 1 ||
                               adjacenttiles[indexinarray] == 1001 && x == minx && adjacenttiles[indexinarray] == 999 && z == maxz - 1)
                            {
                                //istypeoflt = -1;
                            }
                        }
                        else
                        {
                            istypeoflt = -1;
                        }

                        /* if (xiii < minx || xiii > maxx - 1 || ziii < minz || ziii > maxz - 1)
                         {
                             istypeoflt = -1;
                         }*/
                    }


                    else if (checkx == x + (1) && checkz == z - (1))
                    {
                        int xiii = checkx;
                        int ziii = checkz;

                        if (xiii < 0)
                        {
                            xiii *= -1;
                            xiii = xiii + (maxx - 1);
                        }
                        if (ziii < 0)
                        {
                            ziii *= -1;
                            ziii = ziii + (maxz - 1);
                        }

                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth && checkx >= minx && checkx <= maxx - 1 && checkz >= minz && checkz <= maxz - 1)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeofrb = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeofrb = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeofrb = 1;
                            }
                            /*

                            if (x+1 == maxx-1 && z-1 == minz)
                            {
                                istypeofrb = -1;
                            }*/

                            if (adjacenttiles[indexinarray] == 0 && x == maxx - 1 && adjacenttiles[indexinarray] == 0 && z == minz ||
                               adjacenttiles[indexinarray] == 999 && x == maxx - 1 && adjacenttiles[indexinarray] == 999 && z == minz ||
                               adjacenttiles[indexinarray] == 1001 && x == maxx - 1 && adjacenttiles[indexinarray] == 1001 && z == minz ||

                               adjacenttiles[indexinarray] == 0 && x == maxx - 1 && adjacenttiles[indexinarray] == 999 && z == minz ||
                               adjacenttiles[indexinarray] == 999 && x == maxx - 1 && adjacenttiles[indexinarray] == 0 && z == minz ||
                               adjacenttiles[indexinarray] == 0 && x == maxx - 1 && adjacenttiles[indexinarray] == 1001 && z == minz ||
                               adjacenttiles[indexinarray] == 1001 && x == maxx - 1 && adjacenttiles[indexinarray] == 0 && z == minz ||

                               adjacenttiles[indexinarray] == 999 && x == maxx - 1 && adjacenttiles[indexinarray] == 1001 && z == minz ||
                               adjacenttiles[indexinarray] == 1001 && x == maxx - 1 && adjacenttiles[indexinarray] == 999 && z == minz)
                            {
                                //istypeofrb = -1;
                            }
                        }
                        else
                        {
                            istypeofrb = -1;
                        }


                        /*if (xiii < minx || xiii > maxx - 1 || ziii < minz || ziii > maxz - 1)
                        {
                            istypeofrb = -1;
                        }*/
                    }


                    else if (checkx == x - (1) && checkz == z - (1))
                    {
                        int xiii = checkx;
                        int ziii = checkz;

                        if (xiii < 0)
                        {
                            xiii *= -1;
                            xiii = xiii + (maxx - 1);
                        }
                        if (ziii < 0)
                        {
                            ziii *= -1;
                            ziii = ziii + (maxz - 1);
                        }

                        int indexinarray0 = xiii + somewidth * (y + someheight * ziii); //y is always 0 on floor tiles

                        if (indexinarray0 < somewidth * someheight * somedepth && checkx >= minx && checkx <= maxx - 1 && checkz >= minz && checkz <= maxz - 1)
                        {
                            if (adjacenttiles[indexinarray0] == 0)
                            {
                                istypeoflb = 0;
                            }
                            else if (adjacenttiles[indexinarray0] == 999)
                            {
                                istypeoflb = -1;
                            }
                            else if (adjacenttiles[indexinarray0] == 1001)
                            {
                                istypeoflb = 1;
                            }
                            /*
                            if (x-1 == minx && z -1== minz)
                            {
                                istypeoflb = -1;
                            }
                            */
                            if (adjacenttiles[indexinarray] == 0 && x == minx && adjacenttiles[indexinarray] == 0 && z == minz ||
                               adjacenttiles[indexinarray] == 999 && x == minx && adjacenttiles[indexinarray] == 999 && z == minz ||
                               adjacenttiles[indexinarray] == 1001 && x == minx && adjacenttiles[indexinarray] == 1001 && z == minz ||

                               adjacenttiles[indexinarray] == 0 && x == minx && adjacenttiles[indexinarray] == 999 && z == minz ||
                               adjacenttiles[indexinarray] == 999 && x == minx && adjacenttiles[indexinarray] == 0 && z == minz ||
                               adjacenttiles[indexinarray] == 0 && x == minx && adjacenttiles[indexinarray] == 1001 && z == minz ||
                               adjacenttiles[indexinarray] == 1001 && x == minx && adjacenttiles[indexinarray] == 0 && z == minz ||

                               adjacenttiles[indexinarray] == 999 && x == minx && adjacenttiles[indexinarray] == 1001 && z == minz ||
                               adjacenttiles[indexinarray] == 1001 && x == minx && adjacenttiles[indexinarray] == 999 && z == minz)
                            {
                                //istypeoflb = -1;
                            }
                        }
                        else
                        {
                            istypeoflb = -1;
                        }


                        /*if (xiii < minx || xiii > maxx - 1 || ziii < minz || ziii > maxz - 1)
                        {
                            istypeoflb = -1;
                        }*/
                    }
                    //somecounter++;
                }
            }

            //Console.WriteLine(somecounter);




            /*
            if (x - 1 < minx && z - 1 < minz)
            {
                istypeoflb = -1;
            }

            if (x + 1 >= maxx - 1 && z - 1 < minz)
            {
                istypeofrb = -1;
            }

            if (x - 1 < minx && z + 1 >= maxz - 1)
            {
                istypeoflt = -1;
            }

            if (x + 1 >= maxx - 1 && z + 1 >= maxz - 1)
            {
                istypeofrt = -1;
            }

            if (x - 1 < minx)
            {
                istypeofl = -1;
            }

            if (x + 1 >= maxx - 1)
            {
                //Console.WriteLine("frb:" + istypeofrb + "/fr:" + istypeofr + "/frt:" + istypeofrt +  "/tile:" + adjacenttiles[indexinarray]);
                istypeofr = -1;
            }

            if (z - 1 < minz)
            {
                istypeofb = -1;
            }

            if (z + 1 >= maxz - 1)
            {
                
                istypeoft = -1;
            }
            */


            /*
            if (x > maxx-1)
            {
                istypeofr = -1;
                //Console.WriteLine("frb:" + istypeofrb + "/fr:" + istypeofr + "/frt:" + istypeofrt );
            }

            if (x < minx)
            {
                istypeofl = -1;
                //Console.WriteLine("test");
            }

            if (z > maxz - 1)
            {
                istypeoft = -1;
                //Console.WriteLine("test");
            }

            if (z < minz)
            {
                istypeofb = -1;
                //Console.WriteLine("test");
            }



            if (x > maxx - 1 && z > maxz - 1)
            {
                istypeofrt = -1;
                //Console.WriteLine("test");
            }
            if (x > maxx - 1 && z < minz)
            {
                istypeofrb = -1;
                //Console.WriteLine("test");
            }
            if (x < minx && z > maxz - 1)
            {
                istypeoflt = -1;
                //Console.WriteLine("test");
            }

            if (x < minx && z < minz)
            {
                istypeoflb = -1;
                //Console.WriteLine("test");
            }*/













            //LEFT WALL
            if (istypeofl == -1

              )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1101;
                }
            }
            //RIGHT WALL
            if (istypeofr == -1

              )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1102;
                }
            }

            //BACK WALL
            if (istypeoft == -1

              )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1104;
                }
            }
            //FRONT WALL
            if (istypeofb == -1

              )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1103;
                }
            }
















            /*
            //LEFT WALL
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 &&               istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1 ||

                istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1 ||

                istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 &&              istypeofr == 0 &&
                istypeoflb == 1 && istypeofb == 1 ||


                istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 &&              istypeofr == 0 &&
                istypeoflb == 1 && istypeofb == 1 ||




                istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == 1 ||

                istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == 1 ||

                istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 1 ||


                istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 1

              )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1101;
                }
            }
            //RIGHT WALL
            if (istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == -1 ||

                istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == -1 ||

                istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1 ||

                istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1 ||



                istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == -1 ||

                istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == -1 ||

                istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1 ||

                istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1

              )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1102;
                }
            }

            //BACK WALL
            if (istypeoflt == -1&& istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&                  istypeofr == 1 &&
                                istypeofb == 0 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&                   istypeofr == 1 &&
                                istypeofb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&                   istypeofr == 1 &&
                                istypeofb == 0||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&                   istypeofr == 1 &&
                                istypeofb == 0 ||



                                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                                istypeofb == 1 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                                istypeofb == 1 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                                istypeofb == 1 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                                istypeofb == 1

              )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1104;
                }
            }
            //FRONT WALL
            if (istypeofl == 1 && istypeoft == 0 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeofl == 1 && istypeoft == 0 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeofl == 1 && istypeoft == 0 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeofl == 1 && istypeoft == 0 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1 ||



                istypeofl == 1 && istypeoft == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeofl == 1 && istypeoft == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeofl == 1 && istypeoft == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeofl == 1 && istypeoft == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1

              )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1103;
                }
            }
            */









            /*
            //LEFT WALL
            if (istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&                 istypeofr == 0 &&
                 istypeoflb == -1&& istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||




                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||



                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 1 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 1 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||




                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 1 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 1 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1)
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1101;
                }
            }
            


        
            
            //RIGHT WALL
            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
              istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

              istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
              istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

              istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
              istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == -1 &&
              istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                 istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == -1 &&
              istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||


              istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
              istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

              istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
              istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

              istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
              istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == -1 &&
              istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                 istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == -1 &&
              istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||



              istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
              istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

              istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
              istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

              istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
              istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == -1 &&
              istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                 istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == -1 &&
              istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||



              istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
              istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

              istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
              istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

              istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
              istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == -1 &&
              istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                 istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == -1 &&
              istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1)
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1102;
                }
            }


            
            //BACK WALL
            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&                      istypeofr == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||


                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||

                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||


                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||


                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||


                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||


                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||

                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||



                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||


                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0)
            {
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1104;
                }
            }


            //FRONT WALL
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1                      && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&                   istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 &&                    istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 &&                   istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 1 &&                   istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||


                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||


                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||


                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1)
            {
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112)
                {
                    levelmap[indexinarray] = 1103;
                }
            }*/












            /*
            //LEFT WALL
            if (istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||


                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&                     istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&                      istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||



                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||


                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0


                 )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1101;
                }
            }



            //RIGHT WALL


            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||


                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&                   istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&                   istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||




                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||




                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1


                 )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1102;

                }
            }





            //BACK WALL
            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||


                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0

                )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1104;
                }
            }



            //FRONT WALL
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1

                )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1103;
                }
            }*/



















            /*
            //LEFT WALL
            if (istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||


                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 && istypeofrb == 1 ||



                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||



                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == 1 && istypeofrt == 0 &&
                 istypeofl == -1 &&  istypeofr == 0 &&
                 istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1

                 )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1101;
                }
            }



            //RIGHT WALL


            if (istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||


                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == -1 ||


                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1 ||


                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 0 && istypeofb == 1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 &&  istypeofr == -1 &&
                istypeoflb == 1 && istypeofb == 1 && istypeofrb == 1


                 )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1102;
                }
            }





            //BACK WALL
            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||


                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||


                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||


                istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1

                )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1104;
                }
            }



            //FRONT WALL
            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||


                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||


                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1||


                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1 ||

                istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                istypeofl == 1 &&  istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1

                )
            {
                if (levelmap[indexinarray] != 1101 &&
                    levelmap[indexinarray] != 1102 &&
                    levelmap[indexinarray] != 1103 &&
                    levelmap[indexinarray] != 1104 &&
                    levelmap[indexinarray] != 1105 &&
                    levelmap[indexinarray] != 1106 &&
                    levelmap[indexinarray] != 1107 &&
                    levelmap[indexinarray] != 1108 &&
                    levelmap[indexinarray] != 1109 &&
                    levelmap[indexinarray] != 1110 &&
                    levelmap[indexinarray] != 1111 &&
                    levelmap[indexinarray] != 1112

                       )
                {
                    levelmap[indexinarray] = 1103;
                }
            }*/











            //OTHER WALL TYPES


            /*/////////BUILD WALL LEFT/////////////////
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1)
            {
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1101;
                }
            }
            if (istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 1)
            {
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1101;
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
              istypeofl == -1 && istypeofr == 0 &&
              istypeoflb == 1 && istypeofb == 1)
            {
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1101;
                }
            }
            if (istypeoflt == 1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == 1 && istypeofb == 1)
            {
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1101;
                }
            }
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == -1 && istypeofr == 0 &&
                istypeoflb == -1 && istypeofb == 0)
            {
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1101;
                }
            }





            /////////BUILD WALL RIGHT/////////////////
            if (istypeoft == 1 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                                 istypeofb == 1 && istypeofrb == -1)
            {
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1102;
                }
            }

            if (istypeoft == 1 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeofb == 1 && istypeofrb == -1)
            {
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1102;
                }
            }

            if (istypeoft == 1 && istypeofrt == -1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1)
            {
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1102;
                }
            }

            if (istypeoft == 1 && istypeofrt == 1 &&
                istypeofl == 0 && istypeofr == -1 &&
                istypeofb == 1 && istypeofrb == 1)
            {
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1102;
                }
            }
            //////






            /////////BUILD WALL BACK/////////////////

            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
               istypeofl == 1 && istypeofr == 1 &&
               istypeofb == 0)
            {
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1104;
                }
            }
            if (istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
              istypeofl == 1 && istypeofr == 1 &&
              istypeofb == 0)
            {
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1104;
                }
            }

            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
              istypeofl == 1 && istypeofr == 1 &&
              istypeofb == 0)
            {
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1104;
                }
            }
            if (istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                   istypeofl == 1 && istypeofr == 1 &&
                   istypeofb == 0)
            {

                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1104;
                }
            }









            /////////BUILD WALL FRONT/////////////////

            if (istypeoft == 0 &&
               istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1)
            {

                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1103;
                }
            }


            if (istypeoft == 0 &&
              istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1)
            {

                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1103;
                }
            }


            if (istypeoft == 0 &&
              istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1)
            {

                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1103;
                }
            }


            if (istypeoft == 0 &&
              istypeofl == 1 && istypeofr == 1 &&
                istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1)
            {
                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1103;
                }
            }

            /////////////////////////////////////////
            */




            /////////BUILD WALL LEFT/////////////////

            /*if (istypeoflt == -1  && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1  && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofrb == 0 ||

                  istypeoflt == -1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofrb == 1 ||



                 istypeoflt == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofrb == 0 ||

                  istypeoflt == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofrb == 1 ||




                 istypeoflt == -1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofrb == 0 ||

                  istypeoflt == -1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofrb == 1 ||


                 istypeoflt == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeofrt == 1 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofrb == 0 ||

                  istypeoflt == 1 && istypeofrt == 0 &&
                 istypeofl == -1 && istypeofr == 0 &&
                 istypeoflb == 1 && istypeofrb == 1







            )
            {

                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1101;
                }
            }

            /////////BUILD WALL RIGHT/////////////////

            if (istypeoflt == 0 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == -1 ||

                 istypeoflt == 1 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == -1 ||

                 istypeoflt == 0 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 1 && istypeofrb == -1 ||

                 istypeoflt == 0 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == -1 ||

                 istypeoflt == 1 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == -1 ||

                 istypeoflt == 0 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 1 && istypeofrb == -1 ||

                 istypeoflt == 0 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == 1 ||

                 istypeoflt == 0 && istypeofrt == -1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 1 && istypeofrb == 1 ||


                 istypeoflt == 0 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 0 && istypeofrb == 1 ||

                 istypeoflt == 0 && istypeofrt == 1 &&
                 istypeofl == 0 && istypeofr == -1 &&
                 istypeoflb == 1 && istypeofrb == 1)
            {

                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1102;
                }
            }




            /////////BUILD WALL BACK/////////////////

            if (istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                 istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == -1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||


                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                 istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == -1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||



                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                 istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == -1 && istypeoft == -1 && istypeofrt == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1 ||


                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                 istypeoflb == 1 && istypeofb == 0 && istypeofrb == 0 ||

                 istypeoflt == 1 && istypeoft == -1 && istypeofrt == 1 &&
                 istypeoflb == 0 && istypeofb == 0 && istypeofrb == 1)
            {

                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1104;
                }
            }

            /////////BUILD WALL FRONT/////////////////

            if (istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                 istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||

                 istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == -1 ||


                 istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                 istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||

                 istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                 istypeoflb == 1 && istypeofb == -1 && istypeofrb == -1 ||


                 istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||

                 istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                 istypeoflb == -1 && istypeofb == -1 && istypeofrb == 1 ||


                 istypeoflt == 0 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1 ||

                 istypeoflt == 1 && istypeoft == 0 && istypeofrt == 0 &&
                 istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1 ||

                 istypeoflt == 0 && istypeoft == 0 && istypeofrt == 1 &&
                 istypeoflb == 1 && istypeofb == -1 && istypeofrb == 1)
            {

                if (levelmap[indexinarray] != 1101 &&
                   levelmap[indexinarray] != 1102 &&
                   levelmap[indexinarray] != 1103 &&
                   levelmap[indexinarray] != 1104 &&
                   levelmap[indexinarray] != 1105 &&
                   levelmap[indexinarray] != 1106 &&
                   levelmap[indexinarray] != 1107 &&
                   levelmap[indexinarray] != 1108 &&
                   levelmap[indexinarray] != 1109 &&
                   levelmap[indexinarray] != 1110 &&
                   levelmap[indexinarray] != 1111 &&
                   levelmap[indexinarray] != 1112

                   )
                {
                    levelmap[indexinarray] = 1103;
                }
            }*/
























            /////////BUILD WALL LEFT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 0 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == 0 ||

                                   istypeoft == -1 &&
               istypeofl == -1 && istypeofr == 0 &&
                                  istypeofb == 0)
            {

                levelmap[indexinarray] = 1105;
            }






            /////////BUILD WALL RIGHT FRONT INSIDE/////////////////
            if (istypeoft == -1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == 0 && istypeofr == -1 &&
                                  istypeofb == 1 ||

                                   istypeoft == -1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == 0 ||

                                  istypeoft == -1 &&
               istypeofl == 0 && istypeofr == -1 &&
                                  istypeofb == 0)
            {

                levelmap[indexinarray] = 1106;
            }


            /////////BUILD WALL LEFT BACK INSIDE/////////////////
            if (istypeoft == 1 &&
               istypeofl == -1 && istypeofr == 1 &&
                                  istypeofb == -1 ||

                                    istypeoft == 0 &&
                istypeofl == -1 && istypeofr == 1 &&
                                     istypeofb == -1 ||

                                    istypeoft == 1 &&
               istypeofl == -1 && istypeofr == 0 &&
                                  istypeofb == -1 ||

                                  istypeoft == 0 &&
               istypeofl == -1 && istypeofr == 0 &&
                                  istypeofb == -1)
            {
                levelmap[indexinarray] = 1107;
            }



            /////////BUILD WALL RIGHT BACK INSIDE/////////////////
            if (istypeoft == 1 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == -1 ||

                                 istypeoft == 0 &&
               istypeofl == 1 && istypeofr == -1 &&
                                  istypeofb == -1 ||

                                 istypeoft == 1 &&
               istypeofl == 0 && istypeofr == -1 &&
                                  istypeofb == -1 ||

                                  istypeoft == 0 &&
               istypeofl == 0 && istypeofr == -1 &&
                                  istypeofb == -1)
            {
                levelmap[indexinarray] = 1108;
            }










            /////////BUILD WALL LEFT FRONT OUTSIDE/////////////////
            if (istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == 1 && istypeofr == 0 &&
                                  istypeofb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                                  istypeofb == 0 ||

                 istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == 1 && istypeofr == 0 &&
                                  istypeofb == 1 ||

                istypeoflt == -1 && istypeoft == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                                  istypeofb == 1)
            {

                levelmap[indexinarray] = 1109;
            }

            /////////BUILD WALL RIGHT FRONT OUTSIDE/////////////////
            if (istypeoft == 1 && istypeofrt == -1 &&
               istypeofl == 0 && istypeofr == 1 &&
                                  istypeofb == 0 ||

                                  istypeoft == 1 && istypeofrt == -1 &&
               istypeofl == 1 && istypeofr == 1 &&
                                  istypeofb == 0 ||

                                  istypeoft == 1 && istypeofrt == -1 &&
               istypeofl == 0 && istypeofr == 1 &&
                                  istypeofb == 1 ||

                                  istypeoft == 1 && istypeofrt == -1 &&
               istypeofl == 1 && istypeofr == 1 &&
                                  istypeofb == 1)
            {
                levelmap[indexinarray] = 1110;
            }


            /////////BUILD WALL LEFT BACK OUTSIDE/////////////////
            if (istypeoft == 0 &&
                istypeofl == 1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 ||

                 istypeoft == 1 &&
                istypeofl == 1 && istypeofr == 0 &&
                 istypeoflb == -1 && istypeofb == 1 ||

                 istypeoft == 0 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == 1 ||

                  istypeoft == 1 &&
                istypeofl == 1 && istypeofr == 1 &&
                 istypeoflb == -1 && istypeofb == 1)
            {


                levelmap[indexinarray] = 1111;
            }



            /////////BUILD WALL RIGHT BACK OUTSIDE/////////////////
            if (istypeoft == 0 &&
               istypeofl == 0 && istypeofr == 1 &&
                                  istypeofb == 1 && istypeofrb == -1 ||

                                  istypeoft == 1 &&
               istypeofl == 0 && istypeofr == 1 &&
                                  istypeofb == 1 && istypeofrb == -1 ||

                                  istypeoft == 0 &&
               istypeofl == 1 && istypeofr == 1 &&
                                  istypeofb == 1 && istypeofrb == -1 ||

                                   istypeoft == 1 &&
               istypeofl == 1 && istypeofr == 1 &&
                                  istypeofb == 1 && istypeofrb == -1)
            {

                levelmap[indexinarray] = 1112;

            }


        }
    }

}
