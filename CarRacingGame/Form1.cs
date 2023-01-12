using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CarRacingGame
{
    public partial class Form1 : Form
    {
        int roadSpeed;
        int trafficSpeed;
        int playerSpeed = 12;
        int score;
        int carImage;

        Random rand = new Random();
        Random carPosition = new Random();

        bool goleft, goright;

        public Form1()
        {
            InitializeComponent();
            ResetGame();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void keyisdown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = true;
            }
        }

        private void keyisup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                goleft = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                goright = false;
            }
        }

        private void gameTimerEvent(object sender, EventArgs e)
        {
            txtScore.Text = "Score: " + score;
            score++;

            if (goleft == true && player.Left > 5)
            {
                player.Left -= playerSpeed;
            }
            if (goright == true && player.Left < 310)
            {
                player.Left += playerSpeed;
            }

            roadtrack1.Top += roadSpeed;
            roadtrack2.Top += roadSpeed;

            if (roadtrack2.Top > 503)
            {
                roadtrack2.Top = -503;
            }
            if (roadtrack1.Top > 503)
            {
                roadtrack1.Top = -503;
            }

            AI1.Top += trafficSpeed;
            AI2.Top += trafficSpeed;

            if (AI1.Top > 520)
            {
                changeAICars(AI1);
            }

            if (AI2.Top > 520)
            {
                changeAICars(AI2);
            }

            if (player.Bounds.IntersectsWith(AI1.Bounds) || player.Bounds.IntersectsWith(AI2.Bounds))
            {
                gameOver();
            }

            if (score > 40 && score < 500)
            {
                trophy.Image = Properties.Resources.bronze;
            }

            if (score > 500 && score < 800)
            {
                trophy.Image = Properties.Resources.silver;
                roadSpeed = 17;
                trafficSpeed = 18;
            }

            if (score >800 && score < 1000)
            {
                trophy.Image = Properties.Resources.silver;
                roadSpeed = 20;
                trafficSpeed = 21;
            }

            if (score >1000 && score < 1500)
            {
                trophy.Image = Properties.Resources.silver;
                roadSpeed = 22;
                trafficSpeed = 23;
            }

            if (score >1500)
            {
                trophy.Image = Properties.Resources.gold;
                trafficSpeed = 27;
                roadSpeed = 25;
            }
        }

        private void changeAICars(PictureBox tempCar)
        {
            carImage = rand.Next(1, 9);

            switch (carImage)
            {
                case 1:
                    tempCar.Image = Properties.Resources.ambulance;
                    break;
                case 2:
                    tempCar.Image = Properties.Resources.carGreen;
                    break;
                case 3:
                    tempCar.Image = Properties.Resources.carGrey;
                    break;
                case 4:
                    tempCar.Image = Properties.Resources.carPink;
                    break;
                case 5:
                    tempCar.Image = Properties.Resources.carOrange;
                    break;
                case 6:
                    tempCar.Image = Properties.Resources.CarRed;
                    break;
                case 7:
                    tempCar.Image = Properties.Resources.carYellow;
                    break;
                case 8:
                    tempCar.Image = Properties.Resources.TruckBlue;
                    break;
                case 9:
                    tempCar.Image = Properties.Resources.TruckWhite;
                    break;
            }

            tempCar.Top = carPosition.Next(100, 400) * -1;

            if ((string)tempCar.Tag == "carLeft")
            {
                tempCar.Left = carPosition.Next(5, 183);
            }
            if ((string)tempCar.Tag == "carRight")
            {
                tempCar.Left = carPosition.Next(239, 390);
            }

        }

        private void gameOver()
        {
            playSound();
            gameTimer.Stop();
            explosion.Visible = true;
            player.Controls.Add(explosion);
            explosion.Location = new Point(-8, 5);
            explosion.BackColor = Color.Transparent;

            trophy.Visible = true;
            trophy.BringToFront();

            btnStart.Enabled = true;

        }

        private void ResetGame()
        {
            btnStart.Enabled = false;
            explosion.Visible = false;
            trophy.Visible = false;
            goleft = false;
            goright= false;
            score = 0;
            trophy.Image = Properties.Resources.bronze;

            roadSpeed = 12;
            trafficSpeed = 15;

            AI1.Top = carPosition.Next(200, 500) * -1;
            AI1.Left = carPosition.Next(5, 183);

            AI2.Top = carPosition.Next(200, 500) * -1;
            AI2.Left = carPosition.Next(239, 390);

            gameTimer.Start();
        }

        private void restartGame(object sender, EventArgs e)
        {
            ResetGame();
        }

        private void playSound()
        {
            System.Media.SoundPlayer playerCrash = new System.Media.SoundPlayer(Properties.Resources.hit);
            playerCrash.Play();

        }

    }
}
