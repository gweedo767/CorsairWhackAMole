using CUE.NET.Devices.Keyboard;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Corsair_Whack_A_Mole
{
    class Game
    {
        public CorsairKeyboard keyboard;
        public int running; // is the game running now? // 0 = no, 1 == yes, 2 == paused

        public virtual void StartGame()
        {
            Console.WriteLine("Game StartGame() called");
        }

        public virtual void GetKeyPress(KeyEventArgs e)
        {
            Console.WriteLine("Game GetKeyPress() called");
        }

        public virtual void RestoreState()
        {
            Console.WriteLine("Game RestoreState() called");
        }

        public void SetKeyboard(CorsairKeyboard keyboard)
        {
            this.keyboard = keyboard;
        }

        protected void UpdateKeyboard()
        {
            keyboard.Update();
        }

        public static void Wait(int milliseconds)
        {
            Thread.Sleep(milliseconds);
        }

        public void StopGame()
        {
            running = 0; // stopped
        }

        public void PauseGame()
        {
            running = 2; //paused
        }

        public void UnPauseGame()
        {
            running = 1;
        }

        public int GetRunningState()
        {
            return running;
        }
    }
}
