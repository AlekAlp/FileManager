using System;
using System.Collections.Generic;
using System.Text;

namespace FileManager
{
    class Interface
    {
        public static void Print(int setSizePage)
        {
            for (int horiz = 0; horiz < 102; horiz++)
            {
                for (int vert = 0; vert < 17 + setSizePage; vert++)
                {
                    if (horiz == 0)
                    {
                        if (vert == 0)
                            LeftUpAngle(horiz, vert);
                        else if (vert != 0)
                        {
                            if (vert == 2)
                                LeftAngle(horiz, vert);
                            else if (vert == 4 + setSizePage)
                                LeftAngle(horiz, vert);
                            else if (vert == 6 + setSizePage)
                                LeftAngle(horiz, vert);
                            else if (vert == 8 + setSizePage)
                                LeftAngle(horiz, vert);
                            else
                                Vertical(horiz, vert);
                        }
                        if (vert == 16 + setSizePage)
                            LeftDownAngle(horiz, vert);
                    }
                    else if (horiz == 101)
                    {
                        if (vert == 0)
                            RightUpAngle(horiz, vert);
                        else if (vert != 0)
                        {
                            if (vert == 2)
                                RightAngle(horiz, vert);
                            else if (vert == 4 + setSizePage)
                                RightAngle(horiz, vert);
                            else if (vert == 6 + setSizePage)
                                RightAngle(horiz, vert);
                            else if (vert == 8 + setSizePage)
                                RightAngle(horiz, vert);
                            else
                                Vertical(horiz, vert);
                        }
                        if (vert == 16 + setSizePage)
                            RightDownAngle(horiz, vert);
                    }
                    if ((horiz > 0) && (horiz < 101))
                    {
                        if (vert == 0)
                            Horizontal(horiz, vert);
                        else if (vert == 2)
                            Horizontal(horiz, vert);
                        else if (vert == 4 + setSizePage)
                            Horizontal(horiz, vert);
                        else if (vert == 6 + setSizePage)
                            Horizontal(horiz, vert);
                        else if (vert == 8 + setSizePage)
                            Horizontal(horiz, vert);
                        else if (vert == 16 + setSizePage)
                            Horizontal(horiz, vert);
                        if (horiz == 20)
                        {
                            if (vert == 4 + setSizePage)
                                UpAngle(horiz, vert);
                            else if(vert == 5 + setSizePage)
                                Vertical(horiz, vert);
                            else if(vert == 6 + setSizePage)
                                DownAngle(horiz, vert);
                        }
                    }
                }                
            }
            Console.SetCursorPosition(21, 5 +setSizePage);
        }
        static void Horizontal(int i, int j)
        {
            Console.SetCursorPosition(i, j);
            Console.Write("═");
        }
        static void Vertical(int i, int j)
        {
            Console.SetCursorPosition(i, j);
            Console.Write("║");
        }
        static void LeftUpAngle(int i, int j)
        {
            Console.SetCursorPosition(i, j);
            Console.Write("╔");
        }
        static void RightUpAngle(int i, int j)
        {
            Console.SetCursorPosition(i, j);
            Console.Write("╗");
        }
        static void LeftAngle(int i, int j)
        {
            Console.SetCursorPosition(i, j);
            Console.Write("╠");
        }
        static void RightAngle(int i, int j)
        {
            Console.SetCursorPosition(i, j);
            Console.Write("╣");
        }
        static void UpAngle(int i, int j)
        {
            Console.SetCursorPosition(i, j);
            Console.Write("╦");
        }
        static void DownAngle(int i, int j)
        {
            Console.SetCursorPosition(i, j);
            Console.Write("╩");
        }
        static void LeftDownAngle(int i, int j)
        {
            Console.SetCursorPosition(i, j);
            Console.Write("╚");
        }
        static void RightDownAngle(int i, int j)
        {
            Console.SetCursorPosition(i, j);
            Console.Write("╝");
        }
    }
}
