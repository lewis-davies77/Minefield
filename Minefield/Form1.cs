using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Minefield
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int atCol;
        int atRow;

        //Boolean array holds mine locations!
        bool[,] Guards = new bool[20, 20];

        private void Form1_Load(object sender, EventArgs e)
        {
            atRow = 19;
            atCol = 10;

            label11.Image = Properties.Resources.double_door_opened16px;

            showSpriteAt(atCol, atRow);
            Guard(40);
        }

        // Function to return label at (atCol, atRow)
        private Label getLabel(int atCol, int atRow)
        {
            int k = atRow * 20 + atCol + 1;
            string s = "label" + k.ToString();

            foreach (Control c in panel1.Controls)
            {
                if (c.GetType() == typeof(System.Windows.Forms.Label))
                {
                    if (c.Name == s)
                    {
                        return (Label)c;
                    }
                }
            }
            return null;
        }

        // Function to show at (atCol, atRow)
        private void showSpriteAt(int atCol, int atRow)
        {
            Label lbl = getLabel(atCol, atRow);                 //get label at (atCol, atRow)
            lbl.BackColor = Color.Yellow;                       // set backcolour to yello
            lbl.Image = Properties.Resources.alien_face16px;    // set to show image
        }

        private void hideSpriteAt(int atCol, int atRow)
        {
            Label lbl = getLabel(atCol, atRow);                 //get label at (atCol, atRow)
            lbl.Image = null;                                   // hide the alien
        }


        private void btnUp_Click(object sender, EventArgs e)
        {
            if (atRow > 0)                                      // So icon doesnt go outside of the grid
            {
                hideSpriteAt(atCol, atRow);
                atRow--;
                showSpriteAt(atCol, atRow);
                label402.Text = "Danger level" + GuardCount(atCol, atRow);
                amIDead(atCol, atRow);
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            if (atRow < 19)
            {
                hideSpriteAt(atCol, atRow);
                atRow++;
                showSpriteAt(atCol, atRow);
                label402.Text = "Danger level" + GuardCount(atCol, atRow);
                amIDead(atCol, atRow);
            }
        }

        private void btnRight_Click(object sender, EventArgs e)
        {
            if (atCol < 19)
            {
                hideSpriteAt(atCol, atRow);
                atCol++;                                                                                //problem here?
                showSpriteAt(atCol, atRow);
                label402.Text = "Danger level" + GuardCount(atCol, atRow);
                amIDead(atCol, atRow);
            }
        }

        private void btnLeft_Click(object sender, EventArgs e)
        {
            if (atCol > 0)
            {
                hideSpriteAt(atCol, atRow);
                atCol--;
                showSpriteAt(atCol, atRow);
                label402.Text = "Danger level" + GuardCount(atCol, atRow);
                amIDead(atCol, atRow);
            }
        }

        private void Guard(int target)
        {
            Random r = new Random();  // create random number generator

            //set varaibles
            int tryCol, tryRow;
            int setSoFar = 0;

            //clear all current mines
            Array.Clear(Guards, 0, Guards.Length);

            do
            {
                tryCol = r.Next(0, 20);
                tryRow = r.Next(0, 19);         //none on the bottom row!
                if (!Guards[tryCol, tryRow])
                {
                    Guards[tryCol, tryRow] = true;
                    setSoFar++;
                }
            } while (setSoFar < target);

           
        }

        private int GuardCount(int atCol, int atRow)
        {
            int sofar = 0;

            if(atCol > 0)
            {
                if (Guards[atCol - 1, atRow])
                    sofar++;
            }

            if (atCol < 18)
            {
                if (Guards[atCol + 1, atRow])
                    sofar++;
            }

            if (atRow > 0)
            {
                if (Guards[atCol, atRow - 1])
                    sofar++;
            }

            if(atRow < 18)
            {
                if (Guards[atCol, atRow + 1])
                    sofar++;
            }

            return sofar;
        }

        private void showGuards()
        {
            Label lbl;
            for (int atRow = 0; atRow < 20; atRow++)
            {
                for(int atCol = 0; atCol < 20; atCol++)
                {
                    lbl = getLabel(atCol, atRow);
                    if (Guards[atCol, atRow])
                    {
                        lbl.BackColor = Color.Red;
                        lbl.Image = Properties.Resources.soldier16px__1_;
                    }
                    else
                    {
                        lbl.BackColor = Color.LightGray;
                        lbl.Image = Properties.Resources.soldier16px__1_;
                    }
                }
            }
        }

        private void amIDead(int atCol, int atRow)
        {
            //check if won
            if (Guards[atCol, atRow])
            {
                this.BackColor = Color.Orange;
                btnDown.Enabled = false;
                btnUp.Enabled = false;
                btnRight.Enabled = false;
                btnLeft.Enabled = false;
                showGuards();
                label402.Text = "Oh no! You got caught!";
            }
            
            else if (atCol == 9 && atRow == 0)
            {
                    this.BackColor = Color.PeachPuff;
                    btnDown.Enabled = false;
                    btnUp.Enabled = false;
                    btnLeft.Enabled = false;
                    btnRight.Enabled = false;
                    showGuards();
                    label402.Text = "You did it! You escaped!";
            }

            else
            {
                label402.Text = "Danger level:";//+ Convert.ToString(GuardCount(atCol, atRow));
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnUp.Enabled = true;                       // start button
            btnDown.Enabled = true;
            btnRight.Enabled = true;
            btnLeft.Enabled = true;
            Guard(40);
        }

    }
}
