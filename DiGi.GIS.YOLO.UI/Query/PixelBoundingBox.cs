using DiGi.Geometry.Planar.Classes;
using DiGi.GIS.Classes;
using System;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Query
    {
        /// <summary>
        /// Projects a building&apos;s world bounding box onto an orthophoto and returns it as a pixel rectangle clamped to the image.
        /// <para>The box is cloned before it is grown by <paramref name="offset"/>, so the same footprint box can be projected onto every year of a building without the offset accumulating. The top-left corner in world coordinates (minimum X, maximum Y) maps to the top-left corner of the image, because <see cref="OrtoData.ToOrto(Point2D?)"/> flips the Y axis.</para>
        /// <para>The rectangle is clamped to <c>[0, width] x [0, height]</c>. Ultralytics discards the whole image when one of its label boxes leaves the 0..1 range, so an unclamped box on a building at the edge of a crop would silently cost the image rather than just the part of the box outside it. A box that is wholly outside the image, or has no area left once clamped, is dropped.</para>
        /// </summary>
        /// <param name="ortoData">The orthophoto the box is projected onto.</param>
        /// <param name="boundingBox2D">The building&apos;s bounding box in world coordinates. It is not modified.</param>
        /// <param name="offset">The distance, in world units, the box is grown by on every side before it is projected.</param>
        /// <param name="width">The width of the saved image in pixels.</param>
        /// <param name="height">The height of the saved image in pixels.</param>
        /// <param name="clamped">When this method returns, true if the projected rectangle crossed an image edge and was clamped.</param>
        /// <returns>The pixel rectangle, with <see cref="BoundingBox2D.Min"/> at its top-left corner, or null when there is no image, no box, or no area left inside the image.</returns>
        public static BoundingBox2D? PixelBoundingBox(this OrtoData? ortoData, BoundingBox2D? boundingBox2D, double offset, int width, int height, out bool clamped)
        {
            clamped = false;

            if (ortoData is null || boundingBox2D is null || width <= 0 || height <= 0)
            {
                return null;
            }

            BoundingBox2D boundingBox2D_Temp = new(boundingBox2D);
            if (!double.IsNaN(offset) && offset != 0)
            {
                boundingBox2D_Temp.Offset(offset);
            }

            Point2D? point2D_TopLeft = ortoData.ToOrto(boundingBox2D_Temp.TopLeft);
            Point2D? point2D_BottomRight = ortoData.ToOrto(boundingBox2D_Temp.BottomRight);
            if (point2D_TopLeft is null || point2D_BottomRight is null)
            {
                return null;
            }

            double x_Min = Math.Min(point2D_TopLeft.X, point2D_BottomRight.X);
            double x_Max = Math.Max(point2D_TopLeft.X, point2D_BottomRight.X);
            double y_Min = Math.Min(point2D_TopLeft.Y, point2D_BottomRight.Y);
            double y_Max = Math.Max(point2D_TopLeft.Y, point2D_BottomRight.Y);

            if (double.IsNaN(x_Min) || double.IsNaN(x_Max) || double.IsNaN(y_Min) || double.IsNaN(y_Max))
            {
                return null;
            }

            double x_Min_Clamped = Math.Clamp(x_Min, 0, width);
            double x_Max_Clamped = Math.Clamp(x_Max, 0, width);
            double y_Min_Clamped = Math.Clamp(y_Min, 0, height);
            double y_Max_Clamped = Math.Clamp(y_Max, 0, height);

            if (x_Max_Clamped - x_Min_Clamped <= 0 || y_Max_Clamped - y_Min_Clamped <= 0)
            {
                return null;
            }

            clamped = x_Min_Clamped != x_Min || x_Max_Clamped != x_Max || y_Min_Clamped != y_Min || y_Max_Clamped != y_Max;

            return new BoundingBox2D(new Point2D(x_Min_Clamped, y_Min_Clamped), new Point2D(x_Max_Clamped, y_Max_Clamped));
        }
    }
}
