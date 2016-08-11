using UnityEngine;
using System.Collections;
using Emgu.CV;
using Emgu.CV.Util;
using Emgu.CV.UI;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Runtime.InteropServices;
using System;
using System.Drawing;
using System.Collections.Generic;

public class EMGU : MonoBehaviour {

    Image<Bgr, byte> picture;//hoehe, breite

    Image<Gray, byte> grayScaled;

    Image<Bgr, byte> copy;

    Image<Bgr, byte> blurred;

    Image<Gray, byte> edged;

    public void Start()
    {
        picture = new Image<Bgr, byte>("C:\\Users\\Simon\\OneDrive\\Dokumente\\Studium\\BA\\Bilder\\Photo_white_square_lights_out.jpg");
        copy = picture.Copy();
        grayScaled = picture.Convert<Gray, Byte>();
        blurred = picture.SmoothGaussian(1, 11, Int32.MaxValue, Int32.MaxValue);

        edged = blurred.Canny(0, 30);

        VectorOfVectorOfPoint contoursDetected = new VectorOfVectorOfPoint();

        CvInvoke.FindContours(edged, contoursDetected, null, Emgu.CV.CvEnum.RetrType.List, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple, new System.Drawing.Point());
        List<VectorOfPoint> contoursArray = new List<VectorOfPoint>();
        int count = contoursDetected.Size;
        for (int i = 0; i < count; i++)
        {
            using (VectorOfPoint currContour = contoursDetected[i])
            {
                contoursArray.Add(currContour);
            }
        }

        Debug.Log(count);

        VectorOfPoint[] reducedContourArray = new VectorOfPoint[2000];

        int i = 0;

        foreach (VectorOfPoint c in contoursArray)
        {
            Double p = CvInvoke.ArcLength(c, true);
            CvInvoke.ApproxPolyDP(c, c, p, true);

            reducedContourArray[i]  = c;
            i++;
        }

        CvInvoke.DrawContours(copy, reducedContourArray, -1, (0, 255, 0));

        grayScaled.Save("D:\\gray.jpg");
        blurred.Save("D:\\blurred.jpg");
        edged.Save("D:\\edged.jpg");

        copy.Save("D:\\processed.jpg");

    }
}
