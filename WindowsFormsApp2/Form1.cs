#define math
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Base;



namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        struct Spectr
        {
            public double[] realpart;
            public double[] impart;
            public double[] phase;
            public double[] amplitude;
            public double[] freq;
        };

        const double Pi = 3.14159;
        int len = 0;
        double[] signal;
        double freqdisc;

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
            freqdisc = Convert.ToDouble(this.textBox6.Text);
            len = (int)(timeinterval * freqdisc); // static_cast
            signal = new double[len];
            chart1.Series[0].Points.Clear(); 
            for (int i = 0; i <= len - 1; i++)
            {
                signal[i] = a1 * Math.Sin(2 * Pi * f1 * i * (1 / freqdisc)) + a2 * Math.Sin(2 * Pi * f2 * i * (1 / freqdisc));
                this.chart1.Series[0].Points.AddXY((i / (double)len) * timeinterval, signal[i]);
            }
        }
            
        private void button2_Click(object sender, EventArgs e)
        {
            double[] tspectra = new double[len];
            chart2.Series[0].Points.Clear();
            Spectr spectr = new Spectr();
            spectr.amplitude = new double[len];
            spectr.freq = new double[len];
            spectr.impart = new double[len];
            spectr.realpart = new double[len];
            spectr.phase = new double[len];
            double[,] resultmassive = new double[5,len];
            resultmassive = (new DFT()).DFTDirect(signal, (int)freqdisc);
            spectr.amplitude[0] = 0;
            for (int i = 0; i < len; i++){
                spectr.amplitude[i] = resultmassive[0,i];
                spectr.freq[i] = resultmassive[1,i];
                spectr.realpart[i] = resultmassive[3,i];
                spectr.impart[i] = resultmassive[4,i];
                chart2.Series[0].Points.AddXY(spectr.freq[i], spectr.amplitude[i]);
            }

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }
    }

}