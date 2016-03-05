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
    class WhackAMoleGame : Game
    {
        
        
        private int Health;
        private int Score;
        private List<Mole> Moles;
        public int MoleSpawnTimer = 25;
        public int NextMole = 25;
        private bool LoseScreen;

        public Random Random;

        ListKeyGroup ActiveHealthKeyGroup;
        ListKeyGroup InactiveHealthKeyGroup;
        ListKeyGroup GameMapKeyGroup;
        ListKeyGroup MolesKeyGroup;
        ListKeyGroup ScoreKeyGroup;

        public static CorsairKeyboardKeyId[] HealthMap = new CorsairKeyboardKeyId[10] {
            CorsairKeyboardKeyId.D1,
            CorsairKeyboardKeyId.D2,
            CorsairKeyboardKeyId.D3,
            CorsairKeyboardKeyId.D4,
            CorsairKeyboardKeyId.D5,
            CorsairKeyboardKeyId.D6,
            CorsairKeyboardKeyId.D7,
            CorsairKeyboardKeyId.D8,
            CorsairKeyboardKeyId.D9,
            CorsairKeyboardKeyId.D0
        };

        public static CorsairKeyboardKeyId[,] GameMap = new CorsairKeyboardKeyId[3, 10] {
            { CorsairKeyboardKeyId.Q, CorsairKeyboardKeyId.W, CorsairKeyboardKeyId.E, CorsairKeyboardKeyId.R, CorsairKeyboardKeyId.T, CorsairKeyboardKeyId.Y, CorsairKeyboardKeyId.U, CorsairKeyboardKeyId.I, CorsairKeyboardKeyId.O, CorsairKeyboardKeyId.P },
            { CorsairKeyboardKeyId.A, CorsairKeyboardKeyId.S, CorsairKeyboardKeyId.D, CorsairKeyboardKeyId.F, CorsairKeyboardKeyId.G, CorsairKeyboardKeyId.H, CorsairKeyboardKeyId.J, CorsairKeyboardKeyId.K, CorsairKeyboardKeyId.L, CorsairKeyboardKeyId.SemicolonAndColon },
            { CorsairKeyboardKeyId.Z, CorsairKeyboardKeyId.X, CorsairKeyboardKeyId.C, CorsairKeyboardKeyId.V, CorsairKeyboardKeyId.B, CorsairKeyboardKeyId.N, CorsairKeyboardKeyId.M, CorsairKeyboardKeyId.CommaAndLessThan, CorsairKeyboardKeyId.PeriodAndBiggerThan, CorsairKeyboardKeyId.SlashAndQuestionMark }
        };

        public WhackAMoleGame()
        {
            Console.WriteLine("Game setting up (whack a mole)");
            running = false;
            Random = new Random();
        }

        public void Initialize()
        {
            ActiveHealthKeyGroup = new ListKeyGroup(keyboard);
            ActiveHealthKeyGroup.Brush = new SolidColorBrush(Color.Red);

            InactiveHealthKeyGroup = new ListKeyGroup(keyboard);
            InactiveHealthKeyGroup.Brush = new SolidColorBrush(Color.White);

            GameMapKeyGroup = new ListKeyGroup(keyboard);
            GameMapKeyGroup.Brush = new SolidColorBrush(Color.ForestGreen);

            ScoreKeyGroup = new ListKeyGroup(keyboard);
            ScoreKeyGroup.Brush = new SolidColorBrush(Color.Purple);

            // add all of game map to key group
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 10; col++)
                {
                    GameMapKeyGroup.AddKey(GameMap[row, col]);
                }
            }

            MolesKeyGroup = new ListKeyGroup(keyboard);
            MolesKeyGroup.Brush = new SolidColorBrush(Color.Yellow);

            Moles = new List<Mole>();
            SpawnMole();

            Health = 10;
            Score = 0;
            LoseScreen = false;
            MoleSpawnTimer = 25;
            NextMole = 25;

        }

        public override void StartGame()
        {
            Console.WriteLine("whack a moles StartGame() called");
            //call base StartGame()
            base.StartGame();
            running = true;
            Initialize();
            GameLoop();
        }

        

        private void GameLoop()
        {
            while(running)
            {
                
                NextMole--;
                if (NextMole < 1)
                {
                    SpawnMole();
                    NextMole = MoleSpawnTimer;
                    MoleSpawnTimer--;
                    if (MoleSpawnTimer < 3) MoleSpawnTimer = 3;
                }

                //decrement moles life
                for (int i = 0; i < Moles.Count; i++)
                {
                    Mole mole = Moles[i];
                    mole.Life--;
                    if (mole.Life == 0)
                    {
                        mole.Remove = true;
                        MolesKeyGroup.RemoveKey(mole.Key);
                        GameMapKeyGroup.AddKey(mole.Key);
                        Health--;
                        Console.WriteLine("Score: " + Score);

                        if (Health < 1)
                        {
                            //you lose
                            LoseScreen = true;
                        }
                        else
                        {
                            SpawnMole();
                        }
                    }
                }

                // Remove dead moles
                Moles.RemoveAll(item => item.Remove == true);

                // set default color to deep sky blue
                if (!LoseScreen)
                {
                    keyboard.Brush = new SolidColorBrush(Color.DeepSkyBlue);
                }
                else
                {
                    keyboard.Brush = new SolidColorBrush(Color.DarkRed);
                    
                    //remove all moles
                    for (int i = 0; i < Moles.Count; i++)
                    {
                        MolesKeyGroup.RemoveKey(Moles[i].Key);
                        GameMapKeyGroup.AddKey(Moles[i].Key);
                    }
                    Moles.RemoveAll(item => item.Remove = false);
                    DrawHealth();
                    base.UpdateKeyboard();
                    Wait(5000);

                    Initialize();
                }

                DrawHealth();
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

            //Loop over moles and look for a bit
            if (KeyPressed != CorsairKeyboardKeyId.D1)
            {
                bool HitMole = false;
                for (int i = 0; i < Moles.Count; i++)
                {
                    if (Moles[i].Key == KeyPressed)
                    {
                        Console.WriteLine("you hit a mole!");
                        HitMole = true;
                        Moles[i].Remove = true;
                        MolesKeyGroup.RemoveKey(Moles[i].Key);
                        GameMapKeyGroup.AddKey(Moles[i].Key);

                        Score++;

                        if (Score >= 10) ScoreKeyGroup.AddKey(CorsairKeyboardKeyId.F1);
                        if (Score >= 20) ScoreKeyGroup.AddKey(CorsairKeyboardKeyId.F2);
                        if (Score >= 30) ScoreKeyGroup.AddKey(CorsairKeyboardKeyId.F3);
                        if (Score >= 40) ScoreKeyGroup.AddKey(CorsairKeyboardKeyId.F4);
                        if (Score >= 50) ScoreKeyGroup.AddKey(CorsairKeyboardKeyId.F5);
                        if (Score >= 60) ScoreKeyGroup.AddKey(CorsairKeyboardKeyId.F6);
                        if (Score >= 70) ScoreKeyGroup.AddKey(CorsairKeyboardKeyId.F7);
                        if (Score >= 80) ScoreKeyGroup.AddKey(CorsairKeyboardKeyId.F8);
                        if (Score >= 90) ScoreKeyGroup.AddKey(CorsairKeyboardKeyId.F9);
                        if (Score >= 100) ScoreKeyGroup.AddKey(CorsairKeyboardKeyId.F10);
                        if (Score >= 110) ScoreKeyGroup.AddKey(CorsairKeyboardKeyId.F11);
                        if (Score >= 120) ScoreKeyGroup.AddKey(CorsairKeyboardKeyId.F12);
                    }
                }
                if (!HitMole)
                {
                    Health--;
                    if (Health < 1)
                    {
                        LoseScreen = true;
                    }
                }
            }
        }

        private void DrawHealth()
        {
            int index = 0;
            for (; index < Health; index++)
            {
                ActiveHealthKeyGroup.AddKey(HealthMap[index]);
                InactiveHealthKeyGroup.RemoveKey(HealthMap[index]);
            }
            for (; index < 10; index++)
            {
                ActiveHealthKeyGroup.RemoveKey(HealthMap[index]);
                InactiveHealthKeyGroup.AddKey(HealthMap[index]);
            }
        }

        private void SpawnMole()
        {
            Mole mole = new Mole(Random.Next(10), Random.Next(3));
            MolesKeyGroup.AddKey(mole.Key);
            GameMapKeyGroup.RemoveKey(mole.Key);

            Moles.Add(mole);
        }


        

        public class Mole
        {
            public int X;
            public int Y;
            public int Life;
            public CorsairKeyboardKeyId Key;
            public bool Remove = false;

            public Mole(int x, int y)
            {
                X = x;
                Y = y;
                Life = 30;

                Key = GameMap[y, x];
            }
        }
    }
}
