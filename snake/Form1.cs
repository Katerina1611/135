using SnakeModel;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;


namespace Snake
{
    //TODO: Не помешают XML-комментарии (они автоматически должны простраиваться, когда ставишь три символа "/" подряд) для каждого метода и класса во всех твоих файлах. Так ты не будешь забывать, что ты писала полгода назад
    //TODO: А так же адекватные названия форм - тоже большой плюс
    public partial class Form1 : Form
    {
        private const int WIDTH = 10, SZ = 30, WALL_PER_STEP = 2;
        private int _level, _cur, _step = 1;
        private readonly Pnt[] _obj = new Pnt[1000];
        //TODO: Для таких полей лучше подходят списки (класс List). Они эффективней по методам
        private readonly Label[] _best = new Label[5];
        private int _cnt;
        private Label _lb, _lb1;
        //TODO: Не уверен, что эта переменная тут нужна, как и в конструкторах классов, в которых она используется. Почему бы не создавать ее прямо там, в конструкторе без входных переменных??
        private readonly Random _rnd;

        private readonly Label[] _score = new Label[5];

        public Form1()
        {
            InitializeComponent();
            _rnd = new Random();
        }

        private void UpdStep()
        {
            _cur = 0;

            _obj[_cur++] = new Meal(_rnd);

            for (var i = 0; i < WALL_PER_STEP * _step; ++i)
                _obj[_cur++] = new Wall(_rnd);
            _obj[_cur++] = new Head();
            for (var i = 0; i < 3; ++i)
            {
                _obj[_cur] = new Tail(_obj[_cur - 1].X, _obj[_cur - 1].Y - 1);
                _cur++;
            }
            _cnt = 3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            change_Form1(false);
            _level = 1;
            UpdStep();
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
            _lb = new Label();
            _lb1 = new Label();
            _lb.Text = "Игроки";
            _lb1.Text = "Счёт";
            _lb.Location = new Point(50, 20);
            _lb1.Location = new Point(250, 20);
            groupBox1.Controls.Add(_lb);
            groupBox1.Controls.Add(_lb1);
            _best[0] = new Label {Location = new Point(50, 50)};

            groupBox1.Controls.Add(_best[0]);
            for (var i = 1; i < 5; ++i)
            {
                _best[i] = new Label {Location = new Point(_best[i - 1].Left, _best[i - 1].Bottom + 20)};
                groupBox1.Controls.Add(_best[i]);
            }
            for (var i = 0; i < 5; ++i)
            {
                _score[i] = new Label {Location = new Point(_best[i].Right + 100, _best[i].Top)};
                groupBox1.Controls.Add(_score[i]);
            }
        }


        //TODO: Слишком много явных приведений к типу и циклов. Стена текста. Попробуй вместо массива использовать коллекцию типа списка. 
        //TODO: Тогда это все может сократиться до пары циклов foreach или LINQ-запросов
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            if (_level == 0)
            {
                change_Form1(true);
            }
            else if (_level == 1)
            {
                for (var i = 0; i < _cur; ++i)
                    _obj[i].Paint(e.Graphics, WIDTH);

                var xLast = _obj[_cur - 1].X;
                var yLast = _obj[_cur - 1].Y;
                
                //TODO: Двигаешь и рисуешь змею одновременно. Неплохо бы разнести эти обязанности
                for (var i = _cur - 1; i >= 0; --i)
                {
                    if (_obj[i].GetType() == typeof(Tail))
                    {
                        var x = _obj[i - 1].X;
                        var y = _obj[i - 1].Y;
                        ((Tail) _obj[i]).Move(x, y);
                    }
                    if (_obj[i].GetType() == typeof(Head))
                    {
                        ((Head) _obj[i]).Move();
                        for (var j = 0; j < _cur; ++j)
                            if (i != j)
                                if (_obj[i].X == _obj[j].X && _obj[i].Y == _obj[j].Y)
                                    if (_obj[j].GetType() == typeof(Meal))
                                    {
                                        _cnt++;
                                        _obj[j] = new Meal(_rnd);
                                        _obj[_cur++] = new Tail(xLast, yLast);
                                    }
                                    else
                                    {
                                        _level = 0;
                                        var t = new Form2(_step * 10 + _cnt);
                                        t.Show();
                                    }
                    }
                }
                if (_cnt == SZ)
                {
                    _step++;
                    UpdStep();
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
            var dir = -1;
            if (e.KeyCode == Keys.D)
                dir = 1;
            if (e.KeyCode == Keys.S)
                dir = 0;
            if (e.KeyCode == Keys.W)
                dir = 2;
            if (e.KeyCode == Keys.A)
                dir = 3;

            if (dir != -1)
                for (var i = 0; i < _cur; ++i)
                    if (_obj[i].GetType() == typeof(Head))
                        ((Head) _obj[i]).Change(dir);
        }

        private void buttonBack_Click(object sender, EventArgs e)
        {
            if (_level == 2)
                groupBox1.Visible = false;
            if (_level == 3)
                textBox1.Visible = false;
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
                for (var i = 0; i < 5; ++i)
                {
                    var res = sr.ReadLine().Split(' ').ToArray();
                    _best[i].Text = i + 1 + ". " + res[0];
                    _score[i].Text = res[1];
                }
            }


            Update();
        }
    }
}