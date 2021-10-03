using System.Collections.Generic;
using UnityEngine;

namespace DTerrain
{
    public class ComplexShape
    {
        public List<Column> Columns;
        public Texture2D Texture;
        public float AlphaTreshold { get; set; } = 0.01f;

        public ComplexShape(Texture2D texture)
        {
            Texture = texture;
            PrepareColumns();
        }
        
        protected void PrepareColumns()
        {
            Columns?.Clear();
            Columns = new List<Column>();

            //Iterate texture
            for (int x = 0; x < Texture.width; x++)
            {
                Column c = new Column(x);
                for (int y = 0; y < Texture.height; y++)
                {
                    int potentialMin = y;
                    int potentialMax = y - 1;
                    while (y < Texture.height && Texture.GetPixel(x, y).a > AlphaTreshold)
                    {
                        y++;
                        potentialMax++;
                    }
                    if (potentialMin <= potentialMax)
                    {
                        c.AddRange(potentialMin, potentialMax); //Add range to a column...
                    }
                }
                Columns.Add(c); //And add the column!
            }
        }
    }
}
