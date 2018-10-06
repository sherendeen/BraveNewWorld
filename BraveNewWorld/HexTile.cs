using System;
using System.Collections.Generic;
using System.Drawing;

/// <summary>
/// Authors(s):
///     Seth G. R. Herendeen
///     DEPRECATED
/// DEPRECATED
/// DEPRECATED
/// </summary>
namespace BraveNewWorld
{

    //Much tidier than the alternative
    /// <summary>
    /// DEPRECATED
    /// A struct to hold the minimum x and y values as well as the maximum x and
    /// y values. MinsAndMaxes is used as an argument for at least one method.
    /// </summary>
    public struct MinsAndMaxes
    {
        public float xMin, yMin, yMax, xMax;
    }

    // The noun project
    /// DEPRECATED
    //This class takes inspiration from http://csharphelper.com/blog/2015/10/draw-a-hexagonal-grid-in-c/
    public class HexTile
    {

        private bool isDebug = false;
        public bool IsDebug
        {
            get
            {
                return this.isDebug;
            }
            set
            {
                this.isDebug = value;
            }
        }

        public HexTile()
        {
           
        }

        //get width
        public float GetHexTileWidth(float height)
        {
            //float tmp = (float)(height / 2 / Math.Sqrt(3));
            //return (float)(4 * (tmp));
            return (float)(4 * (height / 2 / Math.Sqrt(3)));
        }

         


        public void PointToHex(float x, float y, float height, out int row, out int col)
        {
            // Find the test rectangle containing the point.
            float width = GetHexTileWidth(height);
            float tmp = width * 0.75f;
            col = (int)(x / (tmp));

            if (col % 2 == 0)
            {
                row = (int)(y / height);
            }
            else
            {
                row = (int)((y - height / 2) / height);
            }
                

            // Find the test area.
            float testX = col * width * 0.75f;
            float testY = row * height;
            if (col % 2 == 1)
            {
                testY += height / 2;
            }

            // See if the point is above or
            // below the test hexagon on the left.
            bool is_above = false, is_below = false;
            float dx = x - testX;
            if (dx < width / 4)
            {
                float dy = y - (testY + height / 2);
                if (dx < 0.001)
                {
                    // The point is on the left edge of the test rectangle.
                    if (dy < 0)
                    {
                        is_above = true;
                    }
                     if (dy > 0)
                    {
                        is_below = true;
                    }
                }
                else if (dy < 0)
                {
                    // See if the point is above the test hexagon.
                    if (-dy / dx > Math.Sqrt(3))
                    {
                        is_above = true;
                    }
                }
                else
                {
                    // See if the point is below the test hexagon.
                    if (dy / dx > Math.Sqrt(3))
                    {
                        is_below = true;
                    }
                }
            }

            // Adjust the row and column if necessary.
            if (is_above)
            {
                if (col % 2 == 0)
                {
                    row--;
                }
                col--;
            }
            else if (is_below)
            {
                if (col % 2 == 1)
                {
                    row++;
                }
                col--;
            }
        }

        //Convert hex position to pointF[] array
        public PointF[] ConvertHexToPointF(float rowPosition,
            float columnPosition, float height)
        {
            float width = GetHexTileWidth(height);
            float y = height / 2;
            float x = 0;

            y+= rowPosition * height;//FIXED

            if (columnPosition % 2 == 1)
            {
                y += height / 2;
            }

            x += columnPosition * (width * 0.75f);

            PointF[] array =
            {
                new PointF(x, y),
                new PointF(x + width * 0.25f, y - height / 2),
                new PointF(x + width * 0.75f,y-height / 2),
                new PointF(x + width, y),
                new PointF(x + width * 0.75f, y + height / 2),
                new PointF(x + width * 0.25f,y + height / 2)
            };

            return array;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hexagons"></param>
        public void DrawTilesToHexGrid(
            List<PointF> pointsZ, MinsAndMaxes minsAndMaxes, 
            float height, Graphics graphics)
        {

            try
            {

                for (int row = 0; ; row++)
                {

                    PointF[] points = ConvertHexToPointF(rowPosition: row, columnPosition: 0, height: height);

                    //break the loop if the hexagon does not fit
                    if (points[4].Y > minsAndMaxes.yMax)
                    {
                        break;
                    }

                    //Otherwise, just draw the row
                    //  int columnCount = 0;
                    for (int col = 0; ; col++)
                    {
                        //Get the points
                        points = ConvertHexToPointF(row, col, height);

                        //If we cannot fit the hexagon horizontally, next..
                        if (points[3].X > minsAndMaxes.xMax)
                        {
                            break;
                        }

                        //If the hexagon fits vertically, DRAW IT
                        if (points[4].Y <= minsAndMaxes.yMax)
                        {
                            //graphics.DrawImage(, points);
                        }


                    }


                }/////////////



                //// Create image.
                //Image newImage  = global::BraveNewWorld.Properties.Resources.settlement;

                //// Create parallelogram for drawing image.
                //PointF[] destPara = pointsZ.ToArray();



                //// Draw image to screen.
                ////for (int i = 0; i < destPara.Length; i++)
                ////{
                ////graphics.DrawImageUnscaled(newImage, (int)destPara[i].X, (int)destPara[i].Y);

                ////                    graphics.DrawImage(newImage, destPara);
                //    graphics.DrawImage(newImage, new Point((int)destPara[17].X,(int)destPara[45].Y));                  

                ////}
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(exc.Message + " "+exc.Data+ " " + exc.Source+" "+exc.StackTrace);
            }
        }

        public void DrawHexGrid(Graphics graphics, Pen pen, MinsAndMaxes minsAndMaxes, float height)
        {
            //loop until hexagons cannot fit?
            
            for(int row = 0; ;row++)
            {

                PointF[] points = ConvertHexToPointF(rowPosition: row, columnPosition: 0, height: height);

                //break the loop if the hexagon does not fit
                if (points[4].Y > minsAndMaxes.yMax)
                {
                    break;
                }

                //Otherwise, just draw the row
              //  int columnCount = 0;
                for(int col = 0; ; col++)
                {
                    //Get the points
                    points = ConvertHexToPointF(row, col, height);

                    //If we cannot fit the hexagon horizontally, next..
                    if(points[3].X > minsAndMaxes.xMax)
                    {
                        break;
                    }

                    //If the hexagon fits vertically, DRAW IT
                    if(points[4].Y <= minsAndMaxes.yMax)
                    {
                        graphics.DrawPolygon(pen, points);
                    }

                    
                }

                
            }/////////////



        } 


    }

}
