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


public class Histogramm : MonoBehaviour {

    private byte[] histogramm;
    Image<Bgr, byte> picture;

    // Use this for initialization
    void Start () {
        histogramm = new byte[255];
        picture = new Image<Bgr, byte>("C:\\Users\\Simon\\OneDrive\\Dokumente\\Studium\\BA\\Bilder\\Photo_white_square.jpg");

        fillHistogramm();

        logHistogramm();
	}

    void fillHistogramm()
    {
        for (int i = 0; i < picture.Height; i++)
        {
            for (int j = picture.Width - 1; j >= 0; j--)
            {
                byte gray = (byte)(Math.Floor(Math.Sqrt(0.299 * picture[i, j].Red * picture[i, j].Red + 0.587 * picture[i, j].Green * picture[i, j].Green + 0.114 * picture[i, j].Blue * picture[i, j].Blue)));
                histogramm[gray]++;
            }
        }
    }
    
    void logHistogramm()
    {
        for (int i = 0; i < histogramm.Length; i++)
        {
            Debug.Log("[" + i + "]: " + histogramm[i]);
        }
    }
}
