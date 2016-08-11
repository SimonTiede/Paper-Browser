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


public class Sharpener : MonoBehaviour {

    Image<Bgr, byte> picture;
    // Use this for initialization
    void Start () {

        picture = new Image<Bgr, byte>("C:\\Users\\Simon\\OneDrive\\Dokumente\\Studium\\BA\\Bilder\\Photo_white_square_lights_out.jpg");

        sharpenImage();
    }

    void sharpenImage()
    {
        Image<Bgr, byte> copy = new Image<Bgr, byte>("C:\\Users\\Simon\\OneDrive\\Dokumente\\Studium\\BA\\Bilder\\Photo_white_square_lights_out.jpg");

        for(int i = 0; i < picture.Height; i++)
        {
            for(int j = 0; j < picture.Width; j++)
            {
                blubb(i, j);
            }
        }
    }

    void blubb(int i, int j)
    {
        int a = 0;
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if ((i + x) < picture.Height && (i + x) >= 0 && (i + y) < picture.Width && (i + y) >= 0)
                {
                    a = 1;
                    a = a + a - a;
                }
            }
        }
    }
}
