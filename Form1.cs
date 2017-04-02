using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace match_game
{
    public partial class Form1 : Form
    {
        Label firstClicked = null;


        Label secondClicked = null;
        int time = 70;
        int level = 0;
        

        private void reset()
        {
            time = 70;
            if (level == 5)
            {
                MessageBox.Show("You have completed all the levels.");
                button1.Enabled = true;
                return;
            }
            level++;
            button1_Click(this, null);
            
        }


        private void CheckForWinner()
        {

            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }
            timer2.Stop();
            MessageBox.Show("You matched all the icons!", "Congratulations");
            reset();
            //button1_Click(null, null);
        }



        private void AssignIconsToSquares()
        {
            Random random = new Random();

            List<string> icons = new List<string>() 
             { 
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
                 };
            
            foreach (Control control in tableLayoutPanel1.Controls)
            {
               
                Label iconLabel = control as Label;
                
                if (iconLabel != null)
                {
                 
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }         
            }
            
        }

        public Form1()
        {
            
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true) return;
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {

                if (clickedLabel.ForeColor == Color.Black)
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;
                CheckForWinner();
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }
                timer1.Start();
            }
        }

        
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            firstClicked = null;
            secondClicked = null;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (time > 0)
            {
                time--;
                timerlabel.Text = "Time left: "+time + "seconds";
            }
            else
            {
                timer2.Stop();
                MessageBox.Show("Game over");
                tableLayoutPanel1.Visible = false;
                button1.Enabled = true;
                time = 70;
                level = 0;/*to start from the beginning*/
                
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label.Text="Level: "+Convert.ToString(level);

            time = time - (level * 5);
            AssignIconsToSquares();
           
            button1.Enabled = false;
            tableLayoutPanel1.Visible = true;
            timerlabel.Text ="time left: "+ time + "seconds";

            timer2.Start();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //timer1.Stop();
            timer2.Stop();
            DialogResult w= MessageBox.Show("Do you want to exit","confirmation", MessageBoxButtons.YesNo);
            if (w == DialogResult.Yes)
            {
                
                Close();
            }
            if (w == DialogResult.No)
            {
                timer2.Start();
                return;
            }
         }
        }

       

    }


    

