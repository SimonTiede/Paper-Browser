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


public class Line {
    Point start;
    Point end;

    public Line(int x1, int y1, int x2, int y2)
    {
        start = new Point(x1, y1);
        end = new Point(x2, y2);
    }

    public Line(Point p1, Point p2)
    {
        start = p1;
        end = p2;
    }

    public Point StartPoint()
    {
        return start;
    }

    public Point EndPoint()
    {
        return end;
    }
}

public class Point {
    int x;
    int y;

    public Point (int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public int getX()
    {
        return x;
    }

    public int getY()
    {
        return y;
    }
}

public class FourPointSquare : MonoBehaviour {

    private Line topLine;
    private Line bottomLine;
    private Line leftLine;
    private Line rightLine;

    private Point leftCorner;
    private Point rightCorner;
    private Point topCorner;
    private Point bottomCorner;

    private const int minCornerLength = 40;
    private const int minLineLength = 80;

    private const byte limit = 110;
    private Bgr red = new Bgr(0, 0, 255);

    Image<Bgr, byte> repaint = new Image<Bgr, byte>("C:\\Users\\Simon\\OneDrive\\Dokumente\\Studium\\BA\\Bilder\\Photo_white_turned.jpg");
    Image<Bgr, byte> picture = new Image<Bgr, byte>("C:\\Users\\Simon\\OneDrive\\Dokumente\\Studium\\BA\\Bilder\\Photo_white_turned.jpg");

    // Use this for initialization
    void Start () {
        Debug.Log("W:" + picture.Width + ", H:" + picture.Height);

        topLine = FindTopLine();
        bottomLine = FindBottomLine();
        leftLine = FindLeftRow();
        rightLine = FindRightRow();

        leftCorner = FindHorizontalCorner(leftLine, -1);
        rightCorner = FindHorizontalCorner(rightLine, 1);
        topCorner = FindVerticalCorner(topLine, -1);
        bottomCorner = FindVerticalCorner(bottomLine, 1);

        DrawLine(topLine);
        DrawLine(bottomLine);
        DrawLine(leftLine);
        DrawLine(rightLine);

        DrawLine(new Line(leftCorner.getX(), leftCorner.getY(), leftLine.EndPoint().getX(), leftCorner.getY()));
        DrawLine(new Line(rightLine.EndPoint().getX(), rightCorner.getY(), rightCorner.getX(), rightCorner.getY()));
        DrawLine(new Line(topCorner.getX(), topCorner.getY(), topCorner.getX(), topLine.EndPoint().getY()));
        DrawLine(new Line(bottomCorner.getX(), bottomLine.EndPoint().getY(), bottomCorner.getX(), bottomCorner.getY()));

        DrawLine(new Line(topCorner, leftCorner));
        DrawLine(new Line(leftCorner, bottomCorner));
        DrawLine(new Line(bottomCorner, rightCorner));
        DrawLine(new Line(rightCorner, topCorner));

        repaint.Save("C:\\Users\\Simon\\OneDrive\\Dokumente\\Studium\\BA\\Bilder\\Photo_red_square_turned_4.jpg");

    }

    Point FindVerticalCorner(Line line, int direction)
    {
        int startX = line.StartPoint().getX();
        int endX = line.EndPoint().getX();

        int y = line.EndPoint().getY();
        int brightness = Brightness(y, (startX + endX) / 2);    
        while ((startX < endX) && (y > 0) && (y < picture.Height-1))
        {
            y += direction;
            while (startX < endX && Brightness(y, startX) < brightness-20)
            {
                startX++;
            }

            while (startX < endX && Brightness(y, endX) < brightness-20)
            {
                endX--;
            }
        }

        return new Point(endX, y);
    }

    Point FindHorizontalCorner(Line line, int direction)
    {
        int startY = line.StartPoint().getY();
        int endY = line.EndPoint().getY();

        int x = line.EndPoint().getX();

        int brightness = Brightness((startY + endY) / 2, x-5*direction);
        while ((startY < endY) && x > 0 && x < picture.Width-1)
        {
            x += direction;
            while(startY  < endY && Brightness(startY, x) < brightness - 20)
            {
                startY++;
            }

            while (startY < endY && Brightness(endY, x) < brightness - 20)
            {
                endY--;
            }
        }

        return new Point(x, endY);
    }

    float CalculateSteigung(Point p1, Point p2)//Methodenname
    {
        return (float)(p2.getY() - p1.getY()) / (p2.getX() - p1.getX());
    }
    
    void DrawLine(Line line)
    {
        if (Math.Abs(line.EndPoint().getX() - line.StartPoint().getX()) > Math.Abs(line.EndPoint().getY() - line.StartPoint().getY()))
        {
            float steigung = (float)(line.EndPoint().getY() - line.StartPoint().getY()) / (line.EndPoint().getX() - line.StartPoint().getX());
            int y = line.StartPoint().getY();

            if (line.StartPoint().getX() < line.EndPoint().getX())
            {
                for (int x = line.StartPoint().getX(); x < line.EndPoint().getX(); x++)
                {
                    repaint[y + (int)((x - line.StartPoint().getX()) * steigung), x] = red;
                }
            }

            else
            {
                for (int x = line.StartPoint().getX(); x > line.EndPoint().getX(); x--)
                {
                    repaint[y + (int)((x - line.StartPoint().getX()) * steigung), x] = red;
                }
            }
        }
        else
        {
            float steigung = (float)(line.EndPoint().getX() - line.StartPoint().getX()) / (line.EndPoint().getY() - line.StartPoint().getY());
            int x = line.StartPoint().getX();
            if (line.StartPoint().getY() < line.EndPoint().getY())
            {
                for (int y = line.StartPoint().getY(); y < line.EndPoint().getY(); y++)
                {
                    repaint[y, x + (int)((y - line.StartPoint().getY()) * steigung)] = red;
                }
            }
            else
            {
                for (int y = line.StartPoint().getY(); y > line.EndPoint().getY(); y--)
                {
                    repaint[y, x + (int)((y - line.StartPoint().getY()) * steigung)] = red;
                    
                }
            }
        }
    }

    byte Brightness(int y, int x)
    {
       return (byte)(Math.Floor(Math.Sqrt(0.299 * picture[y, x].Red * picture[y, x].Red + 0.587 * picture[y, x].Green * picture[y, x].Green + 0.114 * picture[y, x].Blue * picture[y, x].Blue)));

    }
    bool IsLight(int y, int x)
    {
        //Debug.Log("x: " + x);
        //Debug.Log("y: " + y);

        if (Math.Max(Math.Abs(picture[y, x].Green - picture[y, x].Red), Math.Abs(picture[y, x].Green - picture[y, x].Blue)) < 40)
        {
            return Brightness(y, x) > limit;
        }
        else
            return false;
        
    }

    Line FindLightLineInColumn(int x)
    {
        int currentLength = 0;
        for (int y = 0; y < picture.Height; y++)
        {
            if (IsLight(y, x))
            {
                currentLength++;
            }
            else
            {
                if (currentLength >= minLineLength)
                {
                    return new Line(x, y - currentLength, x, y);
                }
                currentLength = 0;
            }
        }
        return null;
    }

    Line FindLightLineInRow(int y)
    {
        int currentLength = 0;
        for (int x = 0; x < picture.Width; x++)
        {
            if (IsLight(y, x))
            {
                currentLength++;
            }
            else //TODO: Fehler, wenn das Blatt bis zum Rand des Bildes reicht.
            {
                if (currentLength >= minLineLength)
                {
                    Debug.Log(currentLength+":"+x+":"+y);
                    return new Line(x - currentLength, y, x ,y);
                }
                currentLength = 0;
            }
        }
        return null;
    }

    Line FindLeftRow()
    {
        Line result;
        for (int x = 0; x < picture.Width; x++)
        {
            result = FindLightLineInColumn(x);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }

    Line FindRightRow()
    {
        Line result;
        for (int x = picture.Width - 1; x >= 0; x--)
        {
            result = FindLightLineInColumn(x);
            if (result != null)
            {
                return result;
            }
        }
        return null;
    }

    Line FindTopLine()
    {
        Line result;
        for(int y = 5; y < picture.Height; y++)
        {
            result = FindLightLineInRow(y);
            if(result != null)
            {
                return result;
            }
        }

        return null;
    }


    Line FindBottomLine()
    {
        Line result;
        for (int y = picture.Height - 1; y >= 0; y--)
        {
            result = FindLightLineInRow(y);
            if (result != null)
            {
                return result;
            }
        }

        return null;
    }

    void MarkAPoint(int x, int y)
    {
        for (int i = -3; i < 4; i++)
        {
            for (int j = -3; j < 4; j++)
            {
                if ((i + x) >= 0 && (i + x) < picture.Width && (j + y) >= 0 && (j + y) < picture.Height)
                {
                    repaint[j+y, x+i] = red;
                }
            }
        }
    }

}
