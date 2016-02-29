using System;
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
        WhackAMoleGame game;
        Task GameTask;

        public MainForm()
        {
            InitializeComponent();

            game = new WhackAMoleGame();

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
                    game.SetKeyboard(keyboard);
                    this.lblKeyboardType.Text = "Keyboard Type: " + keyboard.DeviceInfo.Model;
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
        }

        private void toggleButton_Click(object sender, EventArgs e)
        {
            if (!game.GetRunningState())
            {
                this.toggleButton.Text = "Disable";

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
                CueSDK.Reinitialize();

                game.StopGame();
            }

            Console.WriteLine("Playing set to: {0}", game.GetRunningState());
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (game.running)
            {
                game.GetKeyPress(e);
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

    }
}
