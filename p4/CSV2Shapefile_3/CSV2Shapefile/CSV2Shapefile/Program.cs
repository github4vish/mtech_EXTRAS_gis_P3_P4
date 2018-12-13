using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MapWinGIS; 

namespace CSV2Shapefile
{
    class Program
    {
        public static void Main(string[] args)
        {
            //Create new shapefile
            Shapefile myShapefile = new Shapefile();
            //Define the path of the new shapefile and geometry type
            myShapefile.CreateNew(@"D:\GISLesson04\road.shp", ShpfileType.SHP_POINT);
            //Create new field
            MapWinGIS.Field myField = new Field();
            //Set the field properties
            myField.Name = "ID";
            myField.Type = FieldType.INTEGER_FIELD;
            myField.Width = 10;
            //Add the filed for the shapefile table
            int intFieldIndex = 0;
            myShapefile.EditInsertField(myField, ref intFieldIndex, null);
            
            int myCounter = 0;
            int myShapeIndex = 0;
            string myLine;

            // Read the file and display it line by line.
            System.IO.StreamReader myFile =
               new System.IO.StreamReader(@"D:\GISLesson04\road.csv");
            // Using while loop to read csv file line by line
            while ((myLine = myFile.ReadLine()) != null)
            {
                if (myCounter > 0)
                {
                    MapWinGIS.Shape myShape = new Shape();
                    myShape.Create(ShpfileType.SHP_POINT);
                    MapWinGIS.Point myPoint = new Point();
                    myPoint.x = GetX(myLine);
                    myPoint.y = GetY(myLine);
                    int myPointIndex = 0;
                    myShape.InsertPoint(myPoint, ref myPointIndex);
                    myShapefile.EditInsertShape(myShape, ref myShapeIndex);
                    myShapeIndex++;
                }
                
                myCounter++;
            }
            myShapefile.StopEditingShapes(true, true, null);
            myFile.Close();


            Console.ReadKey();
        }

        private static double GetY(string sentence)
        {
            string strY = sentence.Substring(sentence.LastIndexOf(";") + 1);
            double result = Convert.ToDouble(strY);
            return result;
        }

        private static double GetX(string sentence)
        {
            string strNewSentense = sentence.Substring(sentence.IndexOf(";")+1);
            string strX = strNewSentense.Substring(0, strNewSentense.IndexOf(";"));
            double result = Convert.ToDouble(strX);
            return result;
        }

        private static Int16 GetIndex(string sentence)
        {
            string strId = sentence.Substring(0, sentence.IndexOf(";"));
            Int16 result = Convert.ToInt16(strId);
            return result;
        }
        
    }
}
