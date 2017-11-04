using System;
using System.Collections.Generic;

namespace L4
{
    public class Vector
    {
        public int x, y, z;
        
        public Vector(int x,int y,int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        /// <summary>
        /// расчитать расстояние до вершины,
        /// описанной параметром радиус-вектором
        /// </summary>
        /// <param name="v">радиус вектор</param>
        public double DistanceTo(Vector v)
        {
            //катет в плоскости ху
            double xy = Math.Sqrt((x - v.x) * (x - v.x) + (y - v.y) * (y - v.y));
            return Math.Sqrt(xy * xy + (z - v.z) * (z - v.z));
        }

        /// <summary>
        /// посчитать длину ломаной
        /// </summary>
        /// <param name="lv">массив радиус векторов</param>
        public static double Length(List<Vector> lv)
        {
            double len = 0;
            for (int i = 1; i < lv.Count; i++)
                len += lv[i - 1].DistanceTo(lv[i]);
            return len;
        }

        /// <summary>
        /// преобразовать к строке
        /// </summary>
        public override string ToString()
        {
            return "(" + x + "," + y + "," + z + ")";
        }
    }
}
