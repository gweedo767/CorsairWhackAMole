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
    class SimonSaysGame : Game
    {
        private List<char> Pattern = new List<char>();
        private List<char> RemainingPattern = new List<char>();
        private int Level;
        private int Time = 500;
        private bool DisplayingPattern = false;

        public Random Random;


        ListKeyGroup RedKeyGroup;
        ListKeyGroup BlueKeyGroup;
        ListKeyGroup YellowKeyGroup;
        ListKeyGroup GreenKeyGroup;

        public static CorsairKeyboardKeyId[] RedKeys = new CorsairKeyboardKeyId[10] {
            CorsairKeyboardKeyId.D1,
            CorsairKeyboardKeyId.D2,
            CorsairKeyboardKeyId.D3,
            CorsairKeyboardKeyId.D4,
            CorsairKeyboardKeyId.D5,
            CorsairKeyboardKeyId.Q,
            CorsairKeyboardKeyId.W,
            CorsairKeyboardKeyId.E,
            CorsairKeyboardKeyId.R,
            CorsairKeyboardKeyId.T,
        };

        public static CorsairKeyboardKeyId[] GreenKeys = new CorsairKeyboardKeyId[10] {
            CorsairKeyboardKeyId.D6,
            CorsairKeyboardKeyId.D7,
            CorsairKeyboardKeyId.D8,
            CorsairKeyboardKeyId.D9,
            CorsairKeyboardKeyId.D0,
            CorsairKeyboardKeyId.Y,
            CorsairKeyboardKeyId.U,
            CorsairKeyboardKeyId.I,
            CorsairKeyboardKeyId.O,
            CorsairKeyboardKeyId.P
        };

        public static CorsairKeyboardKeyId[] BlueKeys = new CorsairKeyboardKeyId[10] {
            CorsairKeyboardKeyId.A,
            CorsairKeyboardKeyId.S,
            CorsairKeyboardKeyId.D,
            CorsairKeyboardKeyId.F,
            CorsairKeyboardKeyId.G,
            CorsairKeyboardKeyId.Z,
            CorsairKeyboardKeyId.X,
            CorsairKeyboardKeyId.C,
            CorsairKeyboardKeyId.V,
            CorsairKeyboardKeyId.B,
        };

        public static CorsairKeyboardKeyId[] YellowKeys = new CorsairKeyboardKeyId[10] {
            CorsairKeyboardKeyId.H,
            CorsairKeyboardKeyId.J,
            CorsairKeyboardKeyId.K,
            CorsairKeyboardKeyId.L,
            CorsairKeyboardKeyId.SemicolonAndColon,
            CorsairKeyboardKeyId.N,
            CorsairKeyboardKeyId.M,
            CorsairKeyboardKeyId.CommaAndLessThan,
            CorsairKeyboardKeyId.PeriodAndBiggerThan,
            CorsairKeyboardKeyId.SlashAndQuestionMark
        };

        public SimonSaysGame()
        {
            Console.WriteLine("Game setting up (Simon Says)");
            running = 0;
            Random = new Random();
        }

        public void Initialize()
        {

            //clear the board
            keyboard.Brush = new SolidColorBrush(Color.Purple);
            keyboard.Update();

            RedKeyGroup = new ListKeyGroup(keyboard);
            RedKeyGroup.Brush = new SolidColorBrush(Color.Red);

            BlueKeyGroup = new ListKeyGroup(keyboard);
            BlueKeyGroup.Brush = new SolidColorBrush(Color.Blue);

            YellowKeyGroup = new ListKeyGroup(keyboard);
            YellowKeyGroup.Brush = new SolidColorBrush(Color.Yellow);

            GreenKeyGroup = new ListKeyGroup(keyboard);
            GreenKeyGroup.Brush = new SolidColorBrush(Color.Green);

            for (int num = 0; num < 10; num++)
            {
                RedKeyGroup.AddKey(RedKeys[num]);
                GreenKeyGroup.AddKey(GreenKeys[num]);
                BlueKeyGroup.AddKey(BlueKeys[num]);
                YellowKeyGroup.AddKey(YellowKeys[num]);
            }

            keyboard.Update();
            Level = 0;

        }

        public override void StartGame()
        {
            Console.WriteLine("SimonSays StartGame() called");
            //call base StartGame()
            base.StartGame();
            Pattern.Clear();
            running = 1;
            Level = 1;
            Initialize();
            GameLoop();
        }

        private void GameLoop()
        {
            if (running == 1)
            {
                DisplayPattern();
            }
        }

        private void DisplayPattern()
        {
            running = 3;
            int num = Random.Next(1, 20);
            if (num == 1 && (Pattern.Count != 0))
            {
                Pattern.Add(Pattern[Pattern.Count - 1]);
            }
            else
            {
                int Colour = Random.Next(1, 5);
                switch (Colour)
                {
                    case 1:
                        Pattern.Add('r');
                        break;
                    case 2:
                        Pattern.Add('g');
                        break;
                    case 3:
                        Pattern.Add('b');
                        break;
                    case 4:
                        Pattern.Add('y');
                        break;
                }
            }
            foreach (char colour in Pattern)
            {
                //MessageBox.Show(Convert.ToString(colour));
                DisplayColour(colour);
            }
            RemainingPattern.Clear();
            RemainingPattern = Pattern.ToList();
            Level++;
            switch (Level)
            {
                case 5:
                    Time = 400;
                    break;
                case 10:
                    Time = 300;
                    break;
                case 15:
                    Time = 200;
                    break;
                case 20:
                    Time = 100;
                    break;    
            }
            running = 1;
        }

        private void DisplayColour(char x)
        {
            // MessageBox.Show(Convert.ToString(x));
            Wait(Time);
            switch (x)
            {
                case 'r':
                    RedKeyGroup.Brush = new SolidColorBrush(Color.White);
                    keyboard.Update();
                    Wait(Time);
                    RedKeyGroup.Brush = new SolidColorBrush(Color.Red);
                    keyboard.Update();
                    break;
                case 'g':
                    GreenKeyGroup.Brush = new SolidColorBrush(Color.White);
                    keyboard.Update();
                    Wait(Time);
                    GreenKeyGroup.Brush = new SolidColorBrush(Color.Green);
                    keyboard.Update();
                    break;
                case 'b':
                    BlueKeyGroup.Brush = new SolidColorBrush(Color.White);
                    keyboard.Update();
                    Wait(Time);
                    BlueKeyGroup.Brush = new SolidColorBrush(Color.Blue);
                    keyboard.Update();
                    break;
                case 'y':
                    YellowKeyGroup.Brush = new SolidColorBrush(Color.White);
                    keyboard.Update();
                    Wait(Time);
                    YellowKeyGroup.Brush = new SolidColorBrush(Color.Yellow);
                    keyboard.Update();
                    break;

            }
        }

        private void CheckKeyPattern(char color)
        {
            if (color == RemainingPattern[0])
            {
                RemainingPattern.RemoveAt(0);
                if (RemainingPattern.Count == 0)
                {
                    keyboard.Brush = new SolidColorBrush(Color.Green);
                    keyboard.Update();
                    Wait(1000);
                    keyboard.Brush = new SolidColorBrush(Color.Purple);
                    keyboard.Update();
                    DisplayPattern();
                }
            }
            else
            {
                keyboard.Brush = new SolidColorBrush(Color.Red);
                keyboard.Update();
                Pattern.Clear();
                Wait(2000);
                Initialize();
                StartGame();

            }
        }
        public override void GetKeyPress(KeyEventArgs e)
        {
            base.GetKeyPress(e);
            Console.WriteLine("you hit a key" + e.KeyCode + " " + e.KeyValue);
            CorsairKeyboardKeyId KeyPressed = CorsairKeyboardKeyId.Keypad0;

            
                // convery form Key to CorsairKeyboardKeyId
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

            if (Array.IndexOf(RedKeys, KeyPressed) != -1) CheckKeyPattern('r');
            if (Array.IndexOf(GreenKeys, KeyPressed) != -1) CheckKeyPattern('g');
            if (Array.IndexOf(BlueKeys, KeyPressed) != -1) CheckKeyPattern('b');
            if (Array.IndexOf(YellowKeys, KeyPressed) != -1) CheckKeyPattern('y');

        }
    }
}
