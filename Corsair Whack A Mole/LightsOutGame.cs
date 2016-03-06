using CUE.NET.Brushes;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Devices.Keyboard.Enums;
using CUE.NET.Devices.Keyboard.Keys;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Corsair_Whack_A_Mole
{
    class LightsOutGame : Game
    {
        private bool WinScreen;

        public Random Random;

        ListKeyGroup OnMapKeyGroup;
        ListKeyGroup OffMapKeyGroup;
        ListKeyGroup MolesKeyGroup;

        public static CorsairKeyboardKeyId[,] GameMap = new CorsairKeyboardKeyId[4, 10] {
            { CorsairKeyboardKeyId.D1, CorsairKeyboardKeyId.D2, CorsairKeyboardKeyId.D3, CorsairKeyboardKeyId.D4, CorsairKeyboardKeyId.D5, CorsairKeyboardKeyId.D6, CorsairKeyboardKeyId.D7, CorsairKeyboardKeyId.D8, CorsairKeyboardKeyId.D9, CorsairKeyboardKeyId.D0 },
            { CorsairKeyboardKeyId.Q, CorsairKeyboardKeyId.W, CorsairKeyboardKeyId.E, CorsairKeyboardKeyId.R, CorsairKeyboardKeyId.T, CorsairKeyboardKeyId.Y, CorsairKeyboardKeyId.U, CorsairKeyboardKeyId.I, CorsairKeyboardKeyId.O, CorsairKeyboardKeyId.P },
            { CorsairKeyboardKeyId.A, CorsairKeyboardKeyId.S, CorsairKeyboardKeyId.D, CorsairKeyboardKeyId.F, CorsairKeyboardKeyId.G, CorsairKeyboardKeyId.H, CorsairKeyboardKeyId.J, CorsairKeyboardKeyId.K, CorsairKeyboardKeyId.L, CorsairKeyboardKeyId.SemicolonAndColon },
            { CorsairKeyboardKeyId.Z, CorsairKeyboardKeyId.X, CorsairKeyboardKeyId.C, CorsairKeyboardKeyId.V, CorsairKeyboardKeyId.B, CorsairKeyboardKeyId.N, CorsairKeyboardKeyId.M, CorsairKeyboardKeyId.CommaAndLessThan, CorsairKeyboardKeyId.PeriodAndBiggerThan, CorsairKeyboardKeyId.SlashAndQuestionMark }
        };

        private bool[,] BoardState;

        public LightsOutGame()
        {
            Console.WriteLine("Game setting up (lights out)");
            running = false;
            Random = new Random();
        }

        public void Initialize()
        {
            OnMapKeyGroup = new ListKeyGroup(keyboard);
            OnMapKeyGroup.Brush = new SolidColorBrush(Color.Orange);

            OffMapKeyGroup = new ListKeyGroup(keyboard);
            OffMapKeyGroup.Brush = new SolidColorBrush(Color.Blue);

            BoardState = new bool[4, 10];

            // add all of game map to key group
            for (int row = 0; row < 4; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    OnMapKeyGroup.AddKey(GameMap[row, col]);
                    BoardState[row, col] = true;
                }
            }

            ToggleKey(Random.Next(4), Random.Next(10));

            MolesKeyGroup = new ListKeyGroup(keyboard);
            MolesKeyGroup.Brush = new SolidColorBrush(Color.Yellow);

            WinScreen = false;

        }

        private void ToggleKey(int row, int col)
        {
            BoardState[row, col] = !BoardState[row, col];

            if(BoardState[row,col])
            {
                OnMapKeyGroup.AddKey(GameMap[row, col]);
                OffMapKeyGroup.RemoveKey(GameMap[row, col]);
            } else
            {
                OffMapKeyGroup.AddKey(GameMap[row, col]);
                OnMapKeyGroup.RemoveKey(GameMap[row, col]);
            }
        }

        public override void StartGame()
        {
            Console.WriteLine("lights out StartGame() called");
            //call base StartGame()
            base.StartGame();
            running = true;
            Initialize();
            GameLoop();
        }



        private void GameLoop()
        {
            while (running)
            {

                // set default color to deep sky blue
                if (!WinScreen)
                {
                    keyboard.Brush = new SolidColorBrush(Color.DeepSkyBlue);

                    //check for win state
                    bool TestValue = BoardState[0, 0];
                    bool Win = true;
                    for(int row = 0; row<4; row++)
                    {
                        for(int col=0; col<10; col++)
                        {
                            if (BoardState[row, col] != TestValue) Win = false;
                        }
                    }
                    if(Win)
                    {
                        WinScreen = true;
                    }
                }
                else
                {
                    keyboard.Brush = new SolidColorBrush(Color.Gold);

                    base.UpdateKeyboard();
                    Wait(5000);

                    Initialize();
                }

                base.UpdateKeyboard();
                Wait(100);
            }
        }

        public override void GetKeyPress(KeyEventArgs e)
        {
            base.GetKeyPress(e);
            Console.WriteLine("you hit a key" + e.KeyCode + " " + e.KeyValue);
            CorsairKeyboardKeyId KeyPressed = CorsairKeyboardKeyId.D1;

            // convery form Key to CorsairKeyboardKeyId
            // Top game row
            if (e.KeyCode.Equals(Keys.Q)) KeyPressed = CorsairKeyboardKeyId.Q;
            if (e.KeyCode.Equals(Keys.W)) KeyPressed = CorsairKeyboardKeyId.W;
            if (e.KeyCode.Equals(Keys.E)) KeyPressed = CorsairKeyboardKeyId.E;
            if (e.KeyCode.Equals(Keys.R)) KeyPressed = CorsairKeyboardKeyId.R;
            if (e.KeyCode.Equals(Keys.T)) KeyPressed = CorsairKeyboardKeyId.T;
            if (e.KeyCode.Equals(Keys.Y)) KeyPressed = CorsairKeyboardKeyId.Y;
            if (e.KeyCode.Equals(Keys.U)) KeyPressed = CorsairKeyboardKeyId.U;
            if (e.KeyCode.Equals(Keys.I)) KeyPressed = CorsairKeyboardKeyId.I;
            if (e.KeyCode.Equals(Keys.O)) KeyPressed = CorsairKeyboardKeyId.O;
            if (e.KeyCode.Equals(Keys.P)) KeyPressed = CorsairKeyboardKeyId.P;

            // Middle game row
            if (e.KeyCode.Equals(Keys.A)) KeyPressed = CorsairKeyboardKeyId.A;
            if (e.KeyCode.Equals(Keys.S)) KeyPressed = CorsairKeyboardKeyId.S;
            if (e.KeyCode.Equals(Keys.D)) KeyPressed = CorsairKeyboardKeyId.D;
            if (e.KeyCode.Equals(Keys.F)) KeyPressed = CorsairKeyboardKeyId.F;
            if (e.KeyCode.Equals(Keys.G)) KeyPressed = CorsairKeyboardKeyId.G;
            if (e.KeyCode.Equals(Keys.H)) KeyPressed = CorsairKeyboardKeyId.H;
            if (e.KeyCode.Equals(Keys.J)) KeyPressed = CorsairKeyboardKeyId.J;
            if (e.KeyCode.Equals(Keys.K)) KeyPressed = CorsairKeyboardKeyId.K;
            if (e.KeyCode.Equals(Keys.L)) KeyPressed = CorsairKeyboardKeyId.L;
            if (e.KeyCode.Equals(Keys.OemSemicolon)) KeyPressed = CorsairKeyboardKeyId.SemicolonAndColon;

            // Bottom game row
            if (e.KeyCode.Equals(Keys.Z)) KeyPressed = CorsairKeyboardKeyId.Z;
            if (e.KeyCode.Equals(Keys.X)) KeyPressed = CorsairKeyboardKeyId.X;
            if (e.KeyCode.Equals(Keys.C)) KeyPressed = CorsairKeyboardKeyId.C;
            if (e.KeyCode.Equals(Keys.V)) KeyPressed = CorsairKeyboardKeyId.V;
            if (e.KeyCode.Equals(Keys.B)) KeyPressed = CorsairKeyboardKeyId.B;
            if (e.KeyCode.Equals(Keys.N)) KeyPressed = CorsairKeyboardKeyId.N;
            if (e.KeyCode.Equals(Keys.M)) KeyPressed = CorsairKeyboardKeyId.M;
            if (e.KeyCode.Equals(Keys.Oemcomma)) KeyPressed = CorsairKeyboardKeyId.CommaAndLessThan;
            if (e.KeyCode.Equals(Keys.OemPeriod)) KeyPressed = CorsairKeyboardKeyId.PeriodAndBiggerThan;
            if (e.KeyCode.Equals(Keys.OemQuestion)) KeyPressed = CorsairKeyboardKeyId.SlashAndQuestionMark;

            int X = -1;
            int Y = -1;
            if (e.KeyCode.Equals(Keys.D1))
            {
                X = 0;
                Y = 0;
            }
            else if (e.KeyCode.Equals(Keys.D2))
            {
                X = 1;
                Y = 0;
            }
            else if (e.KeyCode.Equals(Keys.D3))
            {
                X = 2;
                Y = 0;
            }
            else if (e.KeyCode.Equals(Keys.D4))
            {
                X = 3;
                Y = 0;
            }
            else if (e.KeyCode.Equals(Keys.D5))
            {
                X = 4;
                Y = 0;
            }
            else if (e.KeyCode.Equals(Keys.D6))
            {
                X = 5;
                Y = 0;
            }
            else if (e.KeyCode.Equals(Keys.D7))
            {
                X = 6;
                Y = 0;
            }
            else if (e.KeyCode.Equals(Keys.D8))
            {
                X = 7;
                Y = 0;
            }
            else if (e.KeyCode.Equals(Keys.D9))
            {
                X = 8;
                Y = 0;
            }
            else if (e.KeyCode.Equals(Keys.D0))
            {
                X = 9;
                Y = 0;
            }
            else if (e.KeyCode.Equals(Keys.Q))
            {
                X = 0;
                Y = 1;
            }
            else if (e.KeyCode.Equals(Keys.W))
            {
                X = 1;
                Y = 1;
            }
            else if (e.KeyCode.Equals(Keys.E))
            {
                X = 2;
                Y = 1;
            }
            else if (e.KeyCode.Equals(Keys.R))
            {
                X = 3;
                Y = 1;
            }
            else if (e.KeyCode.Equals(Keys.T))
            {
                X = 4;
                Y = 1;
            }
            else if (e.KeyCode.Equals(Keys.Y))
            {
                X = 5;
                Y = 1;
            }
            else if (e.KeyCode.Equals(Keys.U))
            {
                X = 6;
                Y = 1;
            }
            else if (e.KeyCode.Equals(Keys.I))
            {
                X = 7;
                Y = 1;
            }
            else if (e.KeyCode.Equals(Keys.O))
            {
                X = 8;
                Y = 1;
            }
            else if (e.KeyCode.Equals(Keys.P))
            {
                X = 9;
                Y = 1;
            }
            else if (e.KeyCode.Equals(Keys.A))
            {
                X = 0;
                Y = 2;
            }
            else if (e.KeyCode.Equals(Keys.S))
            {
                X = 1;
                Y = 2;
            }
            else if (e.KeyCode.Equals(Keys.D))
            {
                X = 2;
                Y = 2;
            }
            else if (e.KeyCode.Equals(Keys.F))
            {
                X = 3;
                Y = 2;
            }
            else if (e.KeyCode.Equals(Keys.G))
            {
                X = 4;
                Y = 2;
            }
            else if (e.KeyCode.Equals(Keys.H))
            {
                X = 5;
                Y = 2;
            }
            else if (e.KeyCode.Equals(Keys.J))
            {
                X = 6;
                Y = 2;
            }
            else if (e.KeyCode.Equals(Keys.K))
            {
                X = 7;
                Y = 2;
            }
            else if (e.KeyCode.Equals(Keys.L))
            {
                X = 8;
                Y = 2;
            }
            else if (e.KeyCode.Equals(Keys.OemSemicolon))
            {
                X = 9;
                Y = 2;
            }
            else if (e.KeyCode.Equals(Keys.Z))
            {
                X = 0;
                Y = 3;
            }
            else if (e.KeyCode.Equals(Keys.X))
            {
                X = 1;
                Y = 3;
            }
            else if (e.KeyCode.Equals(Keys.C))
            {
                X = 2;
                Y = 3;
            }
            else if (e.KeyCode.Equals(Keys.V))
            {
                X = 3;
                Y = 3;
            }
            else if (e.KeyCode.Equals(Keys.B))
            {
                X = 4;
                Y = 3;
            }
            else if (e.KeyCode.Equals(Keys.N))
            {
                X = 5;
                Y = 3;
            }
            else if (e.KeyCode.Equals(Keys.M))
            {
                X = 6;
                Y = 3;
            }
            else if (e.KeyCode.Equals(Keys.Oemcomma))
            {
                X = 7;
                Y = 3;
            }
            else if (e.KeyCode.Equals(Keys.OemPeriod))
            {
                X = 8;
                Y = 3;
            }
            else if (e.KeyCode.Equals(Keys.OemQuestion))
            {
                X = 9;
                Y = 3;
            }
            else if(e.KeyCode.Equals(Keys.Escape))
            {
                WinScreen = true;
            }

            if (X != -1 && Y != -1)
            {
                //toggle key
                ToggleKey(Y, X);
                //up?
                if (Y != 0)
                {
                    ToggleKey(Y - 1, X);
                }
                //down?
                if (Y != 3)
                {
                    ToggleKey(Y + 1, X);
                }
                //left?
                if (X != 0)
                {
                    ToggleKey(Y, X - 1);
                }
                //right?
                if (X != 9)
                {
                    ToggleKey(Y, X + 1);
                }
            }
        }
    }
}
