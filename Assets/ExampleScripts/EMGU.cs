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

public class EMGU : MonoBehaviour {

    Image<Bgr, byte> picture;//hoehe, breite

    Image<Gray, byte> grayScaled;

    Image<Bgr, byte> copy;

    Image<Bgr, byte> blurred;

    Image<Gray, byte> edged;

    public void Start ()
    {
        picture = new Image<Bgr, byte>("C:\\Users\\Simon\\OneDrive\\Dokumente\\Studium\\BA\\Bilder\\Photo_white_square_lights_out.jpg");
        copy = picture.Copy();
        grayScaled = picture.Convert<Gray, Byte>();
        blurred = picture.SmoothGaussian(5, 5, 0, 0);

        edged = blurred.Canny(0, 50);
    }
}
