using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;

namespace snake
{
    public partial class Form1 : Form
    {
        int _level = 0, _cur = 0, _step = 1;
        Label[] best = new Label[5];
        Label lb, lb1;
        
        Label[] score = new Label[5];
        Random rnd;
        const int WIDTH = 10, SZ = 30, WALL_PER_STEP = 2;
        int cnt;
        Pnt[] _obj = new Pnt[1000];
        public Form1()
        {
            InitializeComponent();
            rnd = new Random();
        }
        private void updStep()
        {
            _cur = 0;
            
            _obj[_cur++] = new Meal(rnd);
            
            for (int i = 0; i < WALL_PER_STEP * _step; ++i)
            {
                _obj[_cur++] = new Wall(rnd);
            }
            _obj[_cur++] = new head();
            for (int i = 0; i < 3; ++i)
            {
                _obj[_cur] = new Tail(_obj[_cur - 1].X, _obj[_cur - 1].Y - 1);
                _cur++;
            }
            cnt = 3;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            change_Form1(false);
            _level = 1;
            updStep();
            Invalidate();
        }
        private void change_Form1(bool q)
        {
            buttonBack.Visible = !q;
            buttonBack.Enabled = !q;
            buttonPlay.Visible = q;
            buttonPlay.Enabled = q;
            buttonHighscore.Visible = q;
            buttonHighscore.Enabled = q;
            buttonRule.Visible = q;
            buttonRule.Enabled = q;
            labelMain.Visible = q;
        }
        private void Form1_Load(object sender, EventArgs e)
        {

            groupBox1.Visible = false;
            change_Form1(true);
            groupBox1.Enabled = false;
            lb = new Label();
            lb1 = new Label();
            lb.Text = "Игроки";
            lb1.Text = "Счёт";
            lb.Location = new Point(50, 20);
            lb1.Location = new Point(250, 20);
            groupBox1.Controls.Add(lb);
            groupBox1.Controls.Add(lb1);
            best[0] = new Label();
            best[0].Location = new Point(50, 50);
            
            groupBox1.Controls.Add(best[0]);
            for (int i = 1; i < 5; ++i)
            {
                best[i] = new Label();
                best[i].Location = new Point(best[i - 1].Left, best[i - 1].Bottom + 20);
                groupBox1.Controls.Add(best[i]);
            }
            for (int i = 0; i < 5; ++i)
            {
                score[i] = new Label();
                score[i].Location = new Point(best[i].Right + 100, best[i].Top);
                groupBox1.Controls.Add(score[i]);
            }
        }
       
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (_level == 0)
            {
                change_Form1(true);
            }
            else if (_level == 1)
            {
                for (int i = 0; i < _cur; ++i)
                {
                    _obj[i].Paint(e.Graphics, WIDTH);
                }

                int xLast = _obj[_cur - 1].X;
                int yLast = _obj[_cur - 1].Y;

                for (int i = _cur - 1; i >= 0; --i)
                {
                    if (_obj[i].GetType() == typeof(Tail))
                    {
                        int x = _obj[i - 1].X;
                        int y = _obj[i - 1].Y;
                        ((Tail)_obj[i]).Move(x, y);
                    } 
                    if (_obj[i].GetType() == typeof(head))
                    {
                        ((head)_obj[i]).Move();
                        for (int j = 0; j < _cur; ++j)
                        {
                            if (i != j)
                            {
                                if (_obj[i].X == _obj[j].X && _obj[i].Y == _obj[j].Y)
                                {
                                    if (_obj[j].GetType() == typeof(Meal))
                                    {
                                        cnt++;
                                        _obj[j] = new Meal(rnd);
                                        _obj[_cur++] = new Tail(xLast, yLast);
                                    }
                                    else 
                                    {
                                        _level = 0;
                                        Form2 t = new Form2(_step * 10 + cnt);
                                        t.Show();
                                    }
                                }
                            }
                        }
                    }
                }
                if(cnt == SZ)
                {
                    _step++;
                    updStep();
                }
                Invalidate();
                Thread.Sleep(100 - _step * 5);
            }
            else if (_level == 2)
            {
                change_Form1(false);
                
            }
            else
            {
                change_Form1(false);
            }
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            int dir = -1;
            if (e.KeyCode == Keys.D)
            {
                dir = 1;
            }
            if (e.KeyCode == Keys.S)
            {
                dir = 0;
            }
            if (e.KeyCode == Keys.W)
            {
                dir = 2;
            }
            if (e.KeyCode == Keys.A)
            {
                dir = 3;
            }

            if (dir != -1)
            for (int i = 0; i < _cur; ++i)
            {
                if (_obj[i].GetType() == typeof(head))
                {
                    ((head)_obj[i]).change(dir);
                }
            }
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (_level == 2)
            {
                groupBox1.Visible = false;
            }
            if (_level == 3)
            {
                textBox1.Visible = false;
            }
            _level = 0;
            
            change_Form1(true);

        }

        private void buttonRule_Click(object sender, EventArgs e)
        {
            _level = 3;
            textBox1.Location = new Point(50, 50);
            textBox1.Size = new Size(400, 100);
            textBox1.Visible = true;
            Update();
        }

        private void buttonHighscore_Click(object sender, EventArgs e)
        {
            _level = 2;
            change_Form1(false);
            
            groupBox1.Visible = true;
            using (var sr = new StreamReader("Highscore.txt"))
            {
                for (int i = 0; i < 5; ++i)
                {
                    string[] res = sr.ReadLine().Split(' ').ToArray();
                    best[i].Text = (i + 1).ToString() + ". " + res[0];
                    score[i].Text = res[1];
                }
            }
            
            
            
            Update();
        }
    }
}
