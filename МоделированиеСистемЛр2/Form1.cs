using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace МоделированиеСистемЛр2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs ea)
        {
            try
            {
                float d = float.Parse(textBox1.Text);
                float e = float.Parse(textBox2.Text);
                float y = float.Parse(textBox3.Text);
                f1 f1 = new f1(d, e, y);
                double s = f1.S();
                double m1 = f1.Mf();
                textBox4.Text = s.ToString();
                textBox5.Text = m1.ToString();

                //float b = float.Parse(textBox6.Text);
                //float c = float.Parse(textBox7.Text);
                //y = float.Parse(textBox8.Text);
                //f2 f2 = new f2(b, c, y);
                //s = f2.S();
                //double m2 = f2.Mf();
                //textBox9.Text = s.ToString();
                //textBox10.Text = m2.ToString();

                double[] xF1 = new double[] { 1, 1.2f, 1.5f, 2, 2.5f, 3.5f, 4, 4.5f, 4.8f, 5 };
                //double[] xF2 = new double[] { 120, 122, 125, 130, 150, 155 };
                textBox11.Text = f1.GetIntegralPoints(xF1);
                //textBox12.Text = f2.GetIntegralPoints(xF2);
                textBox13.Text = f1.GetIntegralPoints(1, 5, 20);
                //textBox14.Text = f2.GetIntegralPoints(100, 200, 50);
            }
            catch { }
        }
    }
    public class f
    {
        protected double[] x, y;
        protected double epsilon = 0.0001f;
        public double Value(double x)
        {
            int len = this.x.Length;
            if (x < this.x[0] || x > this.x[len - 1])
                return 0;
            if (x == this.x[len - 1])
                return this.y[len - 1];
            int i = 0;
            while (x >= this.x[i + 1])
                i++;
            double res = this.y[i] + (this.y[i + 1] - this.y[i]) /
                (this.x[i + 1] - this.x[i]) * (x - this.x[i]);
            return res;
        }
        public double IntegralValue(double x)
        {
            double z = this.x[0];
            double sum = 0;
            while (z < x)
            {
                sum += (Value(z) + Value(z + epsilon)) / 2;
                z += epsilon;
            }
            return sum * epsilon;
        }
        public double S()
        {
            double z = x[0];
            double sum = 0;
            while (z < x[x.Length - 1])
            {
                sum += (Value(z) + Value(z + epsilon)) / 2;
                z += epsilon;
            }
            return sum * epsilon;
        }
        public double Mf()
        {
            double z = x[0];
            double sum = 0;
            while (z < x[x.Length - 1])
            {
                sum += (z * Value(z) + (z + epsilon) * Value(z + epsilon)) / 2;
                z += epsilon;
            }
            return sum * epsilon;
        }
        public string GetIntegralPoints(double[] x)
        {
            string s = "";
            for (int i = 0; i < x.Length; i++)
                s += string.Format("{0:0.000000}, {1}/", IntegralValue(x[i]), x[i]);
            return s;

        }
        public string GetIntegralPoints(float start, float end, int count)
        {
            string s = "";
            double step = (end - start) / count, x = start;
            for (int i = 0; i < count; i++)
            {
                s += string.Format("{0}\t{1}\n", IntegralValue(x), x);
                x += step;
            }
            return s;
        }
    }
    public class f1 : f
    {
        public f1(float d, float e, float y)
        {
            this.x = new double[]
            {
                d,
                (4 * d + e) / 5,
                (3 * d + 2 * e) / 5,
                (2 * d + 3 * e) / 5,
                (d + 4 * e) / 5,
                e
            };
            this.y = new double[]
            {
                0,
                y,
                0,
                0,
                y,
                0
            };
        }        
    }
    public class f2 : f
    {
        public f2(float b, float c, float y)
        {
            this.x = new double[]
            {
                b,
                (3 * b + c) / 4,
                (b + 3 * c) / 4,
                c
            };
            this.y = new double[]
            {
                2 * y,
                y,
                y,
                0
            };
        }
    }
}