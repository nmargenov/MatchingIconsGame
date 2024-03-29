using Matching_Game;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGameButtons
{
    public partial class MainForm : Form
    {

        Button firstClicked, secondClicked;

        int clickedMatches;
        int seconds;
        int wrongGuesses;

        bool gamesFinish;
        bool allowToPlay;

        List<Button> buttons = new List<Button>();
        List<Image> images = new List<Image>();

        Image defaultImage = Properties.Resources._default;

        Random random = new Random();

        public MainForm()
        {
            InitializeComponent();
            ButtonsLoader();
            ImagesLoader();
            getFreeButton();
            setImages();
            Hider();
        }
        void ButtonsLoader()
        {
            buttons.Add(card1,
                card2,
                card3,
                card4,
                card5,
                card6,
                card7,
                card8,
                card9,
                card10,
                card11,
                card12,
                card13,
                card14,
                card15,
                card16);

        }
        void ImagesLoader()
        {
            images.Add(Properties.Resources.image1,
            Properties.Resources.image2,
            Properties.Resources.image3,
            Properties.Resources.image4,
            Properties.Resources.image5,
            Properties.Resources.image6,
            Properties.Resources.image7,
            Properties.Resources.image8);

        }
        private void timerCountSeconds_Tick(object sender, EventArgs e)
        {
            if (gamesFinish == true)
            {
                timerCountSeconds.Stop();
            }
            else
            {
                seconds++;
                timerLabel.Text = "Time: " + seconds.ToString() + "s";
            }
        }

        private Button getFreeButton()
        {
            int num = random.Next(0, buttons.Count());
            while (buttons[num].Tag != null)
            {
                num = random.Next(0, buttons.Count());
            }
            return buttons[num];
        }

        private void setImages()
        {
            foreach (Image image in images)
            {
                getFreeButton().Tag = image;
                getFreeButton().Tag = image;
            }
        }

        private void Hider()
        {
            foreach (Button button in buttons)
            {
                button.BackgroundImage = defaultImage;
            }

        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();


            firstClicked.BackgroundImage = defaultImage;
            secondClicked.BackgroundImage = defaultImage;

            firstClicked = null;
            secondClicked = null;
        }

        private void playButton_Click(object sender, EventArgs e)
        {
            allowToPlay = true;
            timerCountSeconds.Start();
        }

        private void howToPlayButton_Click(object sender, EventArgs e)
        {
            if (allowToPlay == false)
            {
                HowToPlay HowToPlay = new HowToPlay();
                HowToPlay.ShowDialog();
            }
        }

        private void buttonClick(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;

            if (allowToPlay == true)
            {
                if (clickedButton == null)
                {
                    return;
                }

                if (firstClicked != null && secondClicked != null)
                {
                    return;
                }
                if (firstClicked == clickedButton)
                {
                    return;
                }

                if (firstClicked == null)
                {
                    firstClicked = clickedButton;
                    firstClicked.BackgroundImage = (Image)clickedButton.Tag;
                    return;
                }
                if (firstClicked != null)
                {
                    secondClicked = clickedButton;
                    secondClicked.BackgroundImage = (Image)clickedButton.Tag;
                }
                if (firstClicked.BackgroundImage == secondClicked.BackgroundImage)
                {
                    firstClicked.Enabled = false;
                    secondClicked.Enabled = false;
                    System.Threading.Thread.Sleep(750);
                    firstClicked.Visible = false;
                    secondClicked.Visible = false;
                    firstClicked = null;
                    secondClicked = null;
                    clickedMatches += 1;
                    if (clickedMatches == 8)
                    {
                        gamesFinish = true;
                        MessageBox.Show($"Congratulations! It took you {seconds} seconds and {wrongGuesses} wrong guesses to beat the game!", "End of the game", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Close();
                    }
                }
                else
                {
                    wrongGuessesLabel.Text = "Wrong Guesses: " + ++wrongGuesses;
                    timer1.Start();
                }
            }
        }
    }
}
