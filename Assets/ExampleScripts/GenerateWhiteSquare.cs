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

public class GenerateWhiteSquare : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Image<Bgr, byte> picture = new Image<Bgr, byte>("C:\\Download.jpg");

        Bgr white = new Bgr(255, 255, 255);
        Bgr black = new Bgr(0, 0, 0);

        for (int i = 0; i < picture.Height; i++)
        {
            for (int j = 0; j < picture.Width; j++)
            {
                picture[i, j] = black;
            }
        }

        for(int i = 20; i < 40; i++)
        {
            for(int j = 40; j < 80; j++)
            {
                picture[i, j] = white;
            }
        }
        picture.Save("D:\\WhiteSquare.jpg");
    }
}
