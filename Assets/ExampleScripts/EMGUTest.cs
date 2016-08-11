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

public class EMGUTest : MonoBehaviour {

    int startX = 0;
    int startY = 0;
    int endX = 0;
    int endY = 0;

    public const byte limit = 110; 

    // Use this for initialization
    void Start()
    {
        Image<Bgr, byte> picture = new Image<Bgr, byte>("C:\\Users\\Simon\\OneDrive\\Dokumente\\Studium\\BA\\Bilder\\Photo_white_square_lights_out.jpg");//hoehe, breite
        //Image<Bgr, byte> picture = new Image<Bgr, byte>("D:\\WhiteSquare.jpg");

        //DateTime begin = System.DateTime.Now;

        //Image<Gray, byte> picture_gray = picture.Convert<Gray, byte>();

        FindFirstWhite(picture);
        //FindLastWhite(picture_gray);

        //TimeSpan diff = (System.DateTime.Now - begin);

        //Debug.Log(diff.Milliseconds);

        //Debug.Log("Startkoordinaten: (" + startX + "," + startY + ")");
        //Debug.Log("Endkoordinaten: (" + endX + "," + endY + ")");
    }

    void FindFirstWhite(Image<Bgr, byte> picture)
    {
        Image<Bgr, byte> repaint = new Image<Bgr, byte>("C:\\Users\\Simon\\OneDrive\\Dokumente\\Studium\\BA\\Bilder\\Photo_white_square_lights_out.jpg");
        Bgr red = new Bgr(0, 0, 255);

        for (int i = 0; i < picture.Height; i++)
        {
            for (int j = 0; j < picture.Width; j++)
            {
                byte gray = (byte)(Math.Floor(Math.Sqrt(0.299 * picture[i, j].Red * picture[i, j].Red + 0.587 * picture[i, j].Green * picture[i, j].Green + 0.114 * picture[i, j].Blue * picture[i, j].Blue)));
                if (gray >= limit)
                {
                    repaint[i, j] = red;
                }
            }
        }
        repaint.Save("C:\\Users\\Simon\\OneDrive\\Dokumente\\Studium\\BA\\Bilder\\Photo_red_square_lights_out.jpg");
    }

    void FindLastWhite(Image<Gray, byte> picture)
    {
        //byte gray = (byte) (white.Green * 0.2 + white.Red * 0.3 + white.Blue * 0.5);
        for (int i = picture.Height - 1; i >= 0; i--)
        {
            for (int j = picture.Width - 1; j >= 0; j--)
            {
               /* if (picture[i, j].Equals(white))
                {
                    endX = j;
                    endY = i;
                    return;
                }*/
            }
        }
    }
}
