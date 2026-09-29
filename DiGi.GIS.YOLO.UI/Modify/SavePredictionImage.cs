using DiGi.GIS.Classes;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.Versioning;

namespace DiGi.GIS.YOLO.UI
{
    public static partial class Modify
    {
        /// <summary>
        /// Decodes the orthophoto payload of an <see cref="OrtoData"/> and saves it as a JPEG file, reporting the pixel size of the saved image.
        /// <para>The one encoder both the inference export (<see cref="ExportPredictionImagesAsync"/>) and the training dataset builder (<see cref="AppendYOLOTrainingDatasetAsync"/>) save through, so the detector is trained on the same pixels it is later asked to score. The legacy builder encoded training images with WPF and normalised label boxes by the device-independent width of the image; this one reports <see cref="Image.Width"/> and <see cref="Image.Height"/>, which System.Drawing gives in pixels.</para>
        /// <para>The file is overwritten when it exists. Deciding whether to skip it is the caller&apos;s business.</para>
        /// </summary>
        /// <param name="ortoData">The orthophoto whose <see cref="OrtoData.Bytes"/> are saved.</param>
        /// <param name="path">The path of the JPEG file to write.</param>
        /// <param name="width">When this method returns true, the width of the saved image in pixels; otherwise 0.</param>
        /// <param name="height">When this method returns true, the height of the saved image in pixels; otherwise 0.</param>
        /// <returns>True when the image was decoded and saved; false when there was no payload or no path.</returns>
        [SupportedOSPlatform("windows")]
        public static bool SavePredictionImage(this OrtoData? ortoData, string? path, out int width, out int height)
        {
            width = 0;
            height = 0;

            if (ortoData?.Bytes is not byte[] bytes || bytes.Length == 0 || string.IsNullOrWhiteSpace(path))
            {
                return false;
            }

            using MemoryStream memoryStream = new(bytes);
            using Image image = Image.FromStream(memoryStream);
            image.Save(path, ImageFormat.Jpeg);

            width = image.Width;
            height = image.Height;

            return true;
        }
    }
}
