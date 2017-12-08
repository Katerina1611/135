using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Snake
{
    public partial class Form2 : Form
    {
        private readonly int _cnt;

        public Form2(int sz)
        {
            _cnt = sz;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox2.Text = _cnt.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name = new List<string>();
            var res = new List<int>();

            using (var sr = new StreamReader("Highscore.txt"))
            {
                for (var i = 0; i < 5; ++i)
                {
                    var temp = sr.ReadLine().Split(' ').ToArray();
                    name.Insert(i, temp[0]);
                    res.Insert(i, Convert.ToInt32(temp[1]));
                }
            }
            for (var i = 0; i < 5; ++i)
                if (_cnt > res[i])
                {
                    res.Insert(i, _cnt);
                    name.Insert(i, textBox1.Text);
                    break;
                }
            using (var sw = new StreamWriter("Highscore.txt"))
            {
                for (var i = 0; i < 5; ++i)
                    sw.WriteLine(name[i] + " " + res[i]);
            }
            Close();
        }
    }
}