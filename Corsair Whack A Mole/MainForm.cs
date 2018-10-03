﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CUE.NET;
using CUE.NET.Brushes;
using CUE.NET.Devices.Generic.Enums;
using CUE.NET.Devices.Keyboard;
using CUE.NET.Devices.Keyboard.Enums;
using CUE.NET.Devices.Keyboard.Extensions;
using CUE.NET.Devices.Keyboard.Keys;
using CUE.NET.Exceptions;
using CUE.NET.Gradients;
using CUE.NET.Profiles;

namespace Corsair_Whack_A_Mole
{
    public partial class MainForm : Form
    {
        CorsairKeyboard keyboard;
        Game game;
        Task GameTask;

        public MainForm()
        {
            InitializeComponent();

            try
            {
                // setup CUE SDK
                CueSDK.Initialize();
                Console.WriteLine("Initialized with " + CueSDK.LoadedArchitecture + "-SDK");
                keyboard = CueSDK.KeyboardSDK;
                this.toggleButton.Enabled = false;
                if (keyboard != null)
                {
                    //found a keyboard, let them play!
                    this.toggleButton.Enabled = true;
                    Console.WriteLine("Keyboard found: {0}", keyboard.DeviceInfo.Model);
                    this.lblKeyboardType.Text = "Keyboard Type: " + keyboard.DeviceInfo.Model;
                    game.SetKeyboard(keyboard);
                }
            }
            catch (CUEException ex)
            {
                Console.WriteLine("CUE Exception! ErrorCode: " + Enum.GetName(typeof(CorsairError), ex.Error));
            }
            catch (WrapperException ex)
            {
                Console.WriteLine("Wrapper Exception! Message:" + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception! Message:" + ex.Message);
            }

            game = new WhackAMoleGame();
            game.SetKeyboard(keyboard);
        }

        private void toggleButton_Click(object sender, EventArgs e)
        {
            if (game.GetRunningState() == 0)
            {
                this.toggleButton.Text = "Disable";
                this.pauseButton.Text = "Pause";
                this.pauseButton.Enabled = true;

                //don't let me people change games while running
                radioLightsOut.Enabled = false;
                radioWhackAMole.Enabled = false;
                radioSimonSays.Enabled = false;

                CueSDK.Reinitialize();
                keyboard.Brush = new SolidColorBrush(Color.DeepSkyBlue);
                keyboard.Update();

                GameTask = Task.Factory.StartNew(
                () =>
                {
                    game.StartGame();
                });
            }
            else
            {
                // display the game and set the keyboard to previous state
                this.toggleButton.Text = "Enable";
                this.pauseButton.Enabled = false;
                CueSDK.Reinitialize();

                radioLightsOut.Enabled = true;
                radioWhackAMole.Enabled = true;
                radioSimonSays.Enabled = true;

                game.StopGame();
            }

            Console.WriteLine("Playing set to: {0}", game.GetRunningState());
        }

        public void gameSelect_Changed(object sender, EventArgs e)
        {
            Console.WriteLine("game changed!");

            if(this.radioWhackAMole.Checked)
            {
                game = new WhackAMoleGame();
                game.SetKeyboard(keyboard);
            }
            else if(this.radioLightsOut.Checked)
            {
                game = new LightsOutGame();
                game.SetKeyboard(keyboard);
            }
            else if(this.radioSimonSays.Checked)
            {
                game = new SimonSaysGame();
                game.SetKeyboard(keyboard);
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (game.running == 1)
            {
                game.GetKeyPress(e);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void formFocused(object sender, EventArgs e)
        {
            if(game.GetRunningState() == 2)
            {
                Console.WriteLine("Was paused, restoring!");

                CueSDK.Reinitialize();
                keyboard.Brush = new SolidColorBrush(Color.DeepSkyBlue);
                keyboard.Update();

                this.pauseButton.Text = "Pause";
                this.pauseButton.Enabled = true;
                
                GameTask = Task.Factory.StartNew(
                () =>
                {
                    game.RestoreState();
                });
            }
        }

        private void formDefocused(object sender, EventArgs e)
        {
            if (game.GetRunningState() == 1)
            {
                Console.WriteLine("pausing game");
                game.PauseGame();
                this.pauseButton.Text = "Unpause";
                this.pauseButton.Enabled = true;
                CueSDK.Reinitialize();

                /*
                // display the game and set the keyboard to previous state
                this.toggleButton.Text = "Enable";
                CueSDK.Reinitialize();

                radioLightsOut.Enabled = true;
                radioWhackAMole.Enabled = true;

                game.StopGame();
                */
            }
        }

        private void pauseButton_Click(object sender, EventArgs e)
        {
            if(game.GetRunningState() == 1)
            {
                //pause game just like form lost focus
                formDefocused(null, null);
                //game.PauseGame();
                this.pauseButton.Text = "Unpause";
            }
            else if(game.GetRunningState() == 2)
            {
                //restore game just like focus restore
                formFocused(null, null);
                this.pauseButton.Text = "Pause";
            }
            
        }
    }
}
