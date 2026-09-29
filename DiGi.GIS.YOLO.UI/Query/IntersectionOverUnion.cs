using DiGi.Geometry.Planar.Classes;
using System;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Computes the intersection over union of two axis-aligned boxes: the area they share divided by the area they cover together.
        /// <para>1 for identical boxes, 0 for boxes that do not overlap or only touch. Both boxes must be in the same coordinate space - the label check compares a label box and a detection, both in pixels of the same image.</para>
        /// </summary>
        /// <param name="boundingBox2D_1">The first box.</param>
        /// <param name="boundingBox2D_2">The second box.</param>
        /// <returns>The intersection over union in [0, 1], or <see cref="double.NaN"/> when either box is missing or neither has any area.</returns>
        public static double IntersectionOverUnion(BoundingBox2D? boundingBox2D_1, BoundingBox2D? boundingBox2D_2)
        {
            if (boundingBox2D_1 is null || boundingBox2D_2 is null)
            {
                return double.NaN;
            }

            Point2D point2D_Min_1 = boundingBox2D_1.Min;
            Point2D point2D_Max_1 = boundingBox2D_1.Max;
            Point2D point2D_Min_2 = boundingBox2D_2.Min;
            Point2D point2D_Max_2 = boundingBox2D_2.Max;

            double area_1 = (point2D_Max_1.X - point2D_Min_1.X) * (point2D_Max_1.Y - point2D_Min_1.Y);
            double area_2 = (point2D_Max_2.X - point2D_Min_2.X) * (point2D_Max_2.Y - point2D_Min_2.Y);

            double width_Intersection = Math.Min(point2D_Max_1.X, point2D_Max_2.X) - Math.Max(point2D_Min_1.X, point2D_Min_2.X);
            double height_Intersection = Math.Min(point2D_Max_1.Y, point2D_Max_2.Y) - Math.Max(point2D_Min_1.Y, point2D_Min_2.Y);

            double area_Intersection = width_Intersection <= 0 || height_Intersection <= 0 ? 0 : width_Intersection * height_Intersection;

            double area_Union = area_1 + area_2 - area_Intersection;
            if (double.IsNaN(area_Union) || area_Union <= 0)
            {
                return double.NaN;
            }

            return area_Intersection / area_Union;
        }
    }
}
