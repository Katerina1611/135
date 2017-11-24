using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace snake
{
    public partial class Form2 : Form
    {
        int cnt;
        public Form2(int sz)
        {
            cnt = sz;
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            textBox2.Text = cnt.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> name = new List<string>(); 
            List<int> res = new List<int>();
            
            using (var sr = new StreamReader("Highscore.txt"))
            {
                for (int i = 0; i < 5; ++i)
                {
                    string[] temp = sr.ReadLine().Split(' ').ToArray();
                    name.Insert(i, temp[0]);
                    res.Insert(i, Convert.ToInt32(temp[1]));
                }
            }
            for (int i = 0; i < 5; ++i)
            {
                if (cnt > res[i])
                {
                    res.Insert(i, cnt);
                    name.Insert(i, textBox1.Text);
                    break;
                }
            }
            using (var sw = new StreamWriter("Highscore.txt"))
            {
                for (int i = 0; i < 5; ++i)
                {
                    sw.WriteLine(name[i] + " " + res[i].ToString());
                }
            }
            this.Close();
        }
    }
}
