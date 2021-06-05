using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PROBA2
{
    public partial class Form1 : Form
    {
        bool turn = true; // true = X turn, false = Y turn
        bool against_computer = false;
        int turn_count = 0;


        public Form1()
        {
            InitializeComponent();
        }

        //about dugme
        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Napravio Mihailo Dašić", "O Iks-Oksu");
        }

        //exit dugme
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //sortiramo poteze
        private void button_click(object sender, EventArgs e)
        {  //zapocinjanje nacini
            if ((p1.Text == "Player1") || (p2.Text == "Player2"))
            {
                MessageBox.Show("Morate navesti imena igrača pre početka!\nUkucajte -COMPUTER- na mestu imena drugog igrača za igru protiv AI");
            }
            else
            {
                Button b = (Button)sender;
                if (turn)
                    b.Text = "X";
                
                else
                    b.Text = "O";
                

                turn = !turn;
                b.Enabled = false;
                turn_count++;

                label2.Focus();
                check_for_winner();
            }
            if((!turn) && (against_computer))

            {
                computer_make_move();
            }
        }
        //pojednostavljena verzija sa odredjenim ciljevima
        private void computer_make_move()
        {
            //prioritet 1:  pobediti
            //prioritet 2:  blokirati
            //prioritet 3:  ici na ivicu
            //prioritet 4:  otvoren prostor

            Button move = null;

            //trazimo mogucnosti
            move = look_for_win_or_block("O"); //trazimo pobedu
            if (move == null)
            {
                move = look_for_win_or_block("X"); //trazimo blok
                if (move == null)
                {
                    move = look_for_corner();
                    if (move == null)
                    {
                        move = look_for_open_space();
                    }//zavrsi if
                }//zavrsi if
            }//zavrsi if

            move.PerformClick();

        }

        private Button look_for_open_space()
        {
            Console.WriteLine("Looking for open space");
            Button b = null;
            foreach (Control c in Controls)
            {
                b = c as Button;
                if (b != null)
                {
                    if (b.Text == "")
                        return b;
                }//end if
            }//end if

            return null;
        }

        private Button look_for_corner()
        {
            Console.WriteLine("Looking for corner");
            if (A1.Text == "O")
            {
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (A3.Text == "O")
            {
                if (A1.Text == "")
                    return A1;
                if (C3.Text == "")
                    return C3;
                if (C1.Text == "")
                    return C1;
            }

            if (C3.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C1.Text == "")
                    return C1;
            }

            if (C1.Text == "O")
            {
                if (A1.Text == "")
                    return A3;
                if (A3.Text == "")
                    return A3;
                if (C3.Text == "")
                    return C3;
            }

            if (A1.Text == "")
                return A1;
            if (A3.Text == "")
                return A3;
            if (C1.Text == "")
                return C1;
            if (C3.Text == "")
                return C3;

            return null;
        }

        private Button look_for_win_or_block(string mark)
        { Console.WriteLine("Looking for win or block:  " + mark);
            //horizontalni test
            if ((A1.Text == mark) && (A2.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A2.Text == mark) && (A3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (A3.Text == mark) && (A2.Text == ""))
                return A2;

            if ((B1.Text == mark) && (B2.Text == mark) && (B3.Text == ""))
                return B3;
            if ((B2.Text == mark) && (B3.Text == mark) && (B1.Text == ""))
                return B1;
            if ((B1.Text == mark) && (B3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((C1.Text == mark) && (C2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((C2.Text == mark) && (C3.Text == mark) && (C1.Text == ""))
                return C1;
            if ((C1.Text == mark) && (C3.Text == mark) && (C2.Text == ""))
                return C2;

            //vertikalni test
            if ((A1.Text == mark) && (B1.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B1.Text == mark) && (C1.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C1.Text == mark) && (B1.Text == ""))
                return B1;

            if ((A2.Text == mark) && (B2.Text == mark) && (C2.Text == ""))
                return C2;
            if ((B2.Text == mark) && (C2.Text == mark) && (A2.Text == ""))
                return A2;
            if ((A2.Text == mark) && (C2.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B3.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B3.Text == mark) && (C3.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C3.Text == mark) && (B3.Text == ""))
                return B3;

            //dijagonalni test
            if ((A1.Text == mark) && (B2.Text == mark) && (C3.Text == ""))
                return C3;
            if ((B2.Text == mark) && (C3.Text == mark) && (A1.Text == ""))
                return A1;
            if ((A1.Text == mark) && (C3.Text == mark) && (B2.Text == ""))
                return B2;

            if ((A3.Text == mark) && (B2.Text == mark) && (C1.Text == ""))
                return C1;
            if ((B2.Text == mark) && (C1.Text == mark) && (A3.Text == ""))
                return A3;
            if ((A3.Text == mark) && (C1.Text == mark) && (B2.Text == ""))
                return B2;

            return null;
        }
        //tražimo ko je pobedio
        private void check_for_winner()
        {
            bool there_is_a_winner = false;
            //horizontalne potvrde
            if ((A1.Text == A2.Text) && (A2.Text == A3.Text) && (!A1.Enabled))
             there_is_a_winner = true;
            else if ((B1.Text == B2.Text) && (B2.Text == B3.Text) && (!B1.Enabled))
                there_is_a_winner = true;
            else if ((C1.Text == C2.Text) && (C2.Text == C3.Text) && (!C1.Enabled))
                there_is_a_winner = true;

            //vertikalne potvrde
            if ((A1.Text == B1.Text) && (B1.Text == C1.Text) && (!A1.Enabled))
                there_is_a_winner = true;
            else if ((A2.Text == B2.Text) && (B2.Text == C2.Text) && (!A2.Enabled))
                there_is_a_winner = true;
            else if ((A3.Text == B3.Text) && (B3.Text == C3.Text) && (!A3.Enabled))
                there_is_a_winner = true;

            //dijagonalne potvrde
            if ((A1.Text == B2.Text) && (B2.Text == C3.Text) && (!A1.Enabled))
                there_is_a_winner = true;
            else if ((A3.Text == B2.Text) && (B2.Text == C1.Text) && (!C1.Enabled))
                there_is_a_winner = true;

            //deklaracija runde/igre
            if (there_is_a_winner)
            {
                
                disableButtons();
                String winner = "";
                if (turn)
                {
                    winner = p2.Text;
                    o_win_count.Text = (Int32.Parse(o_win_count.Text) + 1).ToString();
                }
                else
                {
                    winner = p1.Text;
                    x_win_count.Text = (Int32.Parse(x_win_count.Text) + 1).ToString();
                }
                MessageBox.Show(winner + " pobeđuje!", "Jej!");
                newGameToolStripMenuItem.PerformClick();
                label2.Focus();
            }// end if 
            else
            {
                if(turn_count == 9)
                {
                    draw_count.Text = (Int32.Parse(draw_count.Text) + 1).ToString();
                    MessageBox.Show("Nema pobednika!", "Nerešeno!");
                    newGameToolStripMenuItem.PerformClick();
                    label2.Focus();
                }
                
            }
        }// end check_for_winner
        private void disableButtons()
        {

            
                foreach (Control c in Controls)
                {
                    try //replaced
                    {
                        Button b = (Button)c;
                        b.Enabled = false;
                    }//end try //replaced
                    catch { } //replaced
                }
            

        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            turn = true;
            turn_count = 0;
            

                foreach (Control c in Controls)
                {
                    try
                    {
                        Button b = (Button)c;
                    b.Enabled = true;
                    b.Text = "";
                    }//završi try
                catch
                {
                }
            }// završi foreach
            
            
        }

        // prelaženje preko dugmeta, da se prikaže čiji je red
        private void button_enter(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            
            if(b.Enabled)
            {
                if (turn)
                b.Text = "X";
            else
                b.Text = "O";

            }//end if
        }

        private void button_leave(object sender, EventArgs e)
        {
            Button b = (Button)sender;
            if (b.Enabled)
            {
                    b.Text = "";
               

            }//end if
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        //resetovanje poena
        private void resetCountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            o_win_count.Text = "0";
            x_win_count.Text = "0";
            draw_count.Text = "0";

        }

        private void o_win_count_Click(object sender, EventArgs e)
        {

        }

        private void p2_TextChanged(object sender, EventArgs e)
        {
            if (p2.Text.ToUpper() == "COMPUTER")
                against_computer = true;
            else
                against_computer = false;
        }

       

        private void aIToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            p1.Text = "Igrač";
            p2.Text = "COMPUTER";
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            aIToolStripMenuItem.PerformClick();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
