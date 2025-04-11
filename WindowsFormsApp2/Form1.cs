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
using System.CodeDom.Compiler;
using System.IO;
using SpectrNS;
using SignalNS;
using FFT;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static System.Net.WebRequestMethods;


namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        int len = 0;
        Spectr spectr = new Spectr();
        Signal signal = new Signal();

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
            signal.timeinterval = Convert.ToDouble(this.textBox5.Text);
            signal.freqdisc = Convert.ToDouble(this.textBox6.Text);
            len = (int)(signal.timeinterval * signal.freqdisc); // static_cast
            signal.amplitude = new double[len];
            signal.time = new double[len];
            chart1.Series[0].Points.Clear();
            for (int i = 0; i <= len - 1; i++)
            {
                signal.amplitude[i] = a1 * Math.Sin(2 * Math.PI * f1 * i * (1 / signal.freqdisc)) + a2 * Math.Sin(2 * Math.PI * f2 * i * (1 / signal.freqdisc));
                signal.time[i] = (double)((i / (double)len) * signal.timeinterval);
                this.chart1.Series[0].Points.AddXY(signal.time[i], signal.amplitude[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            chart2.Series[0].Points.Clear();
            len = (int)(signal.timeinterval * signal.freqdisc); // static_cast
            spectr.amplitude = new double[len];
            spectr.freq = new double[len];
            spectr.impart = new double[len];
            spectr.realpart = new double[len];
            spectr.phase = new double[len];
            double[,] resultmassive = new double[5, len];
            signal.freqdisc = Convert.ToDouble(this.textBox6.Text);
            resultmassive = (new DFT()).DFTDirect(signal.amplitude, (int)signal.freqdisc);
            for (int i = 0; i < len; i++)
            {
                spectr.amplitude[i] = resultmassive[0, i];
                spectr.freq[i] = resultmassive[1, i];
                spectr.realpart[i] = resultmassive[3, i];
                spectr.impart[i] = resultmassive[4, i];
                chart2.Series[0].Points.AddXY(spectr.freq[i], spectr.amplitude[i]);
            }

        }

        private async void signalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;// чтение части файла
            openFileDialog1.AddExtension = true;
            openFileDialog1.Filter = "(*.dat)|*.dat";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string path = openFileDialog1.FileName;
            StreamReader f = new StreamReader(path);
            signal.amplitude = new double[0];
            signal.time = new double[0];
            chart1.Series[0].Points.Clear();
            signal.timeinterval = Convert.ToDouble(f.ReadLine());
            signal.freqdisc = Convert.ToDouble(f.ReadLine());
            this.textBox5.Text = Convert.ToString(signal.timeinterval);
            this.textBox6.Text = Convert.ToString(signal.freqdisc);
            while (!f.EndOfStream)
            {
                string s = f.ReadLine();
                int IndexOfChar = s.IndexOf(" ");
                Array.Resize(ref signal.amplitude, i + 1);
                Array.Resize(ref signal.time, i + 1);
                signal.amplitude[i] = Convert.ToDouble(s.Substring(IndexOfChar));
                signal.time[i] = Convert.ToDouble(s.Substring(0, IndexOfChar));
                chart1.Series[0].Points.AddXY(signal.time[i], signal.amplitude[i]);
                i++;
            }
            f.Close();

        }

        private async void spectrToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;// чтение части файла
            chart2.Series[0].Points.Clear();
            openFileDialog1.AddExtension = true;
            openFileDialog1.Filter = "(*.dat)|*.dat";
            if (openFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string path = openFileDialog1.FileName;
            StreamReader f = new StreamReader(path);
            while (!f.EndOfStream)
            {
                string s = f.ReadLine();
                int IndexOfChar = s.IndexOf(" ");
                spectr.amplitude = new double[i + 1];
                spectr.freq = new double[i + 1];
                spectr.impart = new double[i + 1];
                spectr.realpart = new double[i + 1];
                spectr.phase = new double[i + 1];
                spectr.amplitude[i] = Convert.ToDouble(s.Substring(IndexOfChar));
                spectr.freq[i] = Convert.ToDouble(s.Substring(0, IndexOfChar));
                chart2.Series[0].Points.AddXY(spectr.freq[i], spectr.amplitude[i]);
                i++;
            }
            f.Close();
        }

        private async void signalToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string text = null;
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = "dat";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string path = saveFileDialog1.FileName;

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                await writer.WriteLineAsync(Convert.ToString(signal.timeinterval));
                await writer.WriteLineAsync(Convert.ToString(signal.freqdisc));
                // преобразуем строку в байты
                for (int i = 0; i < len; i++)
                {
                    text = Convert.ToString(signal.time[i]) + " " + Convert.ToString(signal.amplitude[i]);
                    await writer.WriteLineAsync(text);
                }
                Console.WriteLine("Текст записан в файл");
            }

        }

        private async void spectrToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            string text = null;
            saveFileDialog1.AddExtension = true;
            saveFileDialog1.DefaultExt = "dat";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string path = saveFileDialog1.FileName;
            using (StreamWriter writer = new StreamWriter(path, false))
            {
                // преобразуем строку в байты
                for (int i = 0; i < len; i++)
                {
                    text = Convert.ToString(spectr.freq[i]) + " " + Convert.ToString(spectr.amplitude[i]);
                    await writer.WriteLineAsync(text);
                }
                Console.WriteLine("Текст записан в файл");
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Spectr spectr = new Spectr();
            Signal signal = new Signal();
        }
    }
    
}