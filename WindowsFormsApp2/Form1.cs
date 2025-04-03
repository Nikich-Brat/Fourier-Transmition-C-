#define math
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        const double Pi = 3.14159;
        int len = 0;
        double[] signal;
        struct Arr
        {
            float[] signal;
        };
        struct TSpectr
        {
            Arr realpart;
            Arr impart;
            Arr amlitude;
            Arr freq;
            Arr phase;
        };

        

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            double a1 = Convert.ToDouble(this.textBox1.Text);
            double a2 = Convert.ToDouble(this.textBox2.Text);
            double f1 = Convert.ToDouble(this.textBox3.Text);
            double f2 = Convert.ToDouble(this.textBox4.Text);
            double timeinterval = Convert.ToDouble(this.textBox5.Text);
            double freqdisc = Convert.ToDouble(this.textBox6.Text);
            len = (int)(timeinterval * freqdisc); // static_cast
            double[] signal = new double[len];
            chart1.Series[0].Points.Clear(); 
            for (int i = 0; i <= len - 1; i++)
            {
                signal[i] = a1 * Math.Sin(2 * Pi * f1 * i * (1 / freqdisc)) + a2 * Math.Sin(2 * Pi * f2 * i * (1 / freqdisc));
                this.chart1.Series[0].Points.AddXY((i / (double)len) * timeinterval, signal[i]);
            }
        }


        private void chart1_Click_1(object sender, EventArgs e)
        {
            TSpectr spectr;





        }

        private void button2_Click(object sender, EventArgs e)
        {
            double[] tspectra = new double[len];
            chart2.Series[0].Points.Clear();
            SetLength(spectr.ampl, length(signal));
            SetLength(spectr.realpart, length(signal));
            SetLength(spectr.impart, length(signal));
            SetLength(spectr.freq, length(signal));
            SetLength(spectr.phase, length(signal));

        }
    }
}
