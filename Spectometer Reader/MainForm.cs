using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Collections.Generic;

namespace Spectometer_Reader
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();

            Chart.MouseWheel += new MouseEventHandler(chData_MouseWheel);

            ChartInit();

        }

        //CHART INIT
        private void ChartInit()
        {
            Chart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            Chart.ChartAreas[0].AxisX.IsStartedFromZero = false;
            Chart.ChartAreas[0].AxisY.Interval = 5;
            Chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            Chart.ChartAreas[0].AxisX2.ScaleView.Zoomable = true;
            Chart.ChartAreas[0].AxisY.MajorGrid.LineColor = Color.Gray;
            Chart.ChartAreas[0].AxisY.MinorGrid.LineColor = Color.Gray;
            Chart.ChartAreas[0].AxisX.MinorGrid.LineColor = Color.Gray;
            Chart.ChartAreas[0].AxisX.MajorGrid.LineColor = Color.Gray;
            Chart.ChartAreas[0].AxisX2.MajorGrid.LineColor = Color.Transparent;
            Chart.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.Transparent;
            Chart.ChartAreas[0].AxisY2.MinorGrid.LineColor = Color.Transparent;
            Chart.ChartAreas[0].AxisY2.MinorGrid.LineColor = Color.Transparent;
            Chart.ChartAreas[0].AxisX2.LabelStyle.Enabled = false;
            Chart.ChartAreas[0].AxisY2.LabelStyle.Enabled = false;
            Chart.ChartAreas[0].AxisX.IsMarginVisible = false;


            Chart.Series["DataG"].ToolTip = "Hz=#VALX, dB(µV)=#VALY";

        }
        //Draw Above limit //TODO

        //Chart focus
        private void Chart_MouseLeave(object sender, EventArgs e)
        {
            Chart.Parent.Focus();
        }

        private void Chart_MouseEnter(object sender, EventArgs e)
        {
            Chart.Focus();
        }
        //Track enter chart
        private void chartTracking_MouseEnter(object sender, EventArgs e)
        {
            this.Chart.Focus();
        }

        private void chartTracking_MouseLeave(object sender, EventArgs e)
        {
            this.Chart.Parent.Focus();
        }

        //zoom chart
        private void chData_MouseWheel(object sender, MouseEventArgs e)
        {

            try
            {
                if (e.Delta < 0)
                {
                    Chart.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    Chart.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                    Chart.ChartAreas[0].AxisX2.ScaleView.ZoomReset();
                    Chart.ChartAreas[0].AxisY2.ScaleView.ZoomReset();

                }

                if (e.Delta > 0)
                {
                    double xMin = Chart.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    double xMax = Chart.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    double yMin = Chart.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    double yMax = Chart.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                    double posXStart = Chart.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 2;
                    double posXFinish = Chart.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 2;
                    double posYStart = Chart.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 2;
                    double posYFinish = Chart.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 2;

                    Chart.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    Chart.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);

                    double x2Min = Chart.ChartAreas[0].AxisX2.ScaleView.ViewMinimum;
                    double x2Max = Chart.ChartAreas[0].AxisX2.ScaleView.ViewMaximum;
                    double y2Min = Chart.ChartAreas[0].AxisY2.ScaleView.ViewMinimum;
                    double y2Max = Chart.ChartAreas[0].AxisY2.ScaleView.ViewMaximum;

                    double posX2Start = Chart.ChartAreas[0].AxisX2.PixelPositionToValue(e.Location.X) - (x2Max - x2Min) / 2;
                    double posX2Finish = Chart.ChartAreas[0].AxisX2.PixelPositionToValue(e.Location.X) + (x2Max - x2Min) / 2;
                    double posY2Start = Chart.ChartAreas[0].AxisY2.PixelPositionToValue(e.Location.Y) - (y2Max - y2Min) / 2;
                    double posY2Finish = Chart.ChartAreas[0].AxisY2.PixelPositionToValue(e.Location.Y) + (y2Max - y2Min) / 2;

                    Chart.ChartAreas[0].AxisX2.ScaleView.Zoom(posXStart, posXFinish);
                    Chart.ChartAreas[0].AxisY2.ScaleView.Zoom(posYStart, posYFinish);

                }
            }
            catch { }
        }


        private void MainForm_Load(object sender, EventArgs e)
        {


        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {


        }
        //Play Real time data
        private void Play_Click_1(object sender, EventArgs e)
        {


        }
        //Record RealTime data

        //pause Real time data
        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
        // Export to excel
        private void toolStripButton4_Click(object sender, EventArgs e)
        {

        }

        private void Data_Click(object sender, EventArgs e)
        {

        }


        //Update 
        private void button1_Click(object sender, EventArgs e)
        {

        }
        // Cursor Data
        private void OnChartMouseMove(object sender, MouseEventArgs e)
        {
            var sourceChart = sender as Chart;
            HitTestResult result = Chart.HitTest(e.X, e.Y);
            ChartArea chartAreas = Chart.ChartAreas[0];

            if (result.ChartElementType == ChartElementType.DataPoint)
            {
                chartAreas.CursorX.Position = chartAreas.AxisX.PixelPositionToValue(e.X);
                chartAreas.CursorY.Position = chartAreas.AxisY.PixelPositionToValue(e.Y);
            }
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ScaleBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //linear
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            Chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            Chart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            Chart.ChartAreas[0].AxisX.IsStartedFromZero = true;
            Chart.ChartAreas[0].AxisX.IsLogarithmic = false;
            Chart.ChartAreas[0].AxisX.LogarithmBase = 10;
            Chart.ChartAreas[0].AxisX.Interval = 1;
            Chart.ChartAreas[0].AxisX.MajorTickMark.Interval = 1;
            Chart.ChartAreas[0].AxisX.MajorGrid.Interval = 0.25;

        }
        //log10
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (Chart.Series["DataG"].Points.Count > 0)
            {

                Chart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
                Chart.ChartAreas[0].AxisX.IsStartedFromZero = false;
                Chart.ChartAreas[0].AxisX.MinorGrid.Enabled = true;
                Chart.ChartAreas[0].AxisX.MinorGrid.Interval = 1;
                Chart.ChartAreas[0].AxisX.MajorGrid.Enabled = true;
                Chart.ChartAreas[0].AxisX.IsLogarithmic = true;

                Chart.ChartAreas[0].AxisX.LogarithmBase = 20;
                Chart.ChartAreas[0].AxisX.Interval = 1;
                Chart.ChartAreas[0].AxisX.MajorTickMark.Interval = 1;
                Chart.ChartAreas[0].RecalculateAxesScale();

            }
        }


        //LOAD DATA
        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {

            OpenFileDialog openFileDialog = new OpenFileDialog();
           
            openFileDialog.Filter = "|*.csv";
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                int i = 0;
                
                StreamReader reader = new StreamReader(openFileDialog.OpenFile());
                while(i < 16){
                    string header = reader.ReadLine();
                    i++;
                }
                List<string> StringX = new List<string>();
                List<string> StringY = new List<string>();


                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!String.IsNullOrWhiteSpace(line))
                    {
                        string[] values = line.Split(',');
                        if (values.Length >= 1)
                        {
                            StringX.Add(values[0]);
                            StringY.Add(values[1]);
                        }
                    }
                }

                List<double> DoubleX = StringX.Select(x => double.Parse(x)).ToList();
                List<double> DoubleY = StringY.Select(x => double.Parse(x)).ToList();
                Chart.Series["DataG"].Points.DataBindXY(DoubleX, DoubleY);


            }

        }

        private void checkedListBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        // Real LIMITS
        public void toolStripButton2_Click_1(object sender, EventArgs e)
        {
            //READ LIMIT FILE
            
                
            }



        private void button2_Click(object sender, EventArgs e)
        {
            //read
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "|*.csv";
            DialogResult result = openFileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {

                StreamReader reader = new StreamReader(openFileDialog.OpenFile());
                string header = reader.ReadLine();
                List<string> LStringX = new List<string>();
                List<string> cls1p = new List<string>();
                List<string> cls2p = new List<string>();
                List<string> cls3p = new List<string>();
                List<string> cls4p = new List<string>();
                List<string> cls5p = new List<string>();

                List<string> cls1pq = new List<string>();
                List<string> cls2pq = new List<string>();
                List<string> cls3pq = new List<string>();
                List<string> cls4pq = new List<string>();
                List<string> cls5pq = new List<string>();


                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (!string.IsNullOrWhiteSpace(line))
                    {
                        string[] values = line.Split(',');
                        if (values.Length >= 1)
                        {
                            LStringX.Add(values[0]);
                            //P
                            cls1p.Add(values[1]);
                            cls2p.Add(values[2]);
                            cls3p.Add(values[3]);
                            cls4p.Add(values[4]);
                            cls5p.Add(values[5]);
                           //PQ
                            cls1pq.Add(values[7]);
                            cls2pq.Add(values[8]);
                            cls3pq.Add(values[9]);
                            cls4pq.Add(values[10]);
                            cls5pq.Add(values[11]);
                        }

                    }
                }
                List<double> LDoubleX = LStringX.Select(x => double.Parse(x)).ToList();
                List<double> Dcls1p = cls1p.Select(x => double.Parse(x)).ToList();
                List<double> Dcls2p = cls2p.Select(x => double.Parse(x)).ToList();
                List<double> Dcls3p = cls3p.Select(x => double.Parse(x)).ToList();
                List<double> Dcls4p = cls4p.Select(x => double.Parse(x)).ToList();
                List<double> Dcls5p = cls5p.Select(x => double.Parse(x)).ToList();

                List<double> Dcls1pq = cls1pq.Select(x => double.Parse(x)).ToList();
                List<double> Dcls2pq = cls2pq.Select(x => double.Parse(x)).ToList();
                List<double> Dcls3pq = cls3pq.Select(x => double.Parse(x)).ToList();
                List<double> Dcls4pq = cls4pq.Select(x => double.Parse(x)).ToList();
                List<double> Dcls5pq = cls5pq.Select(x => double.Parse(x)).ToList();




                //clear points
                Chart.Series["Class1P"].Points.Clear();
                Chart.Series["Class2P"].Points.Clear();
                Chart.Series["Class3P"].Points.Clear();
                Chart.Series["Class4P"].Points.Clear();
                Chart.Series["Class5P"].Points.Clear();
                Chart.Series["Class1PQ"].Points.Clear();
                Chart.Series["Class2PQ"].Points.Clear();
                Chart.Series["Class3PQ"].Points.Clear();
                Chart.Series["Class4PQ"].Points.Clear();
                Chart.Series["Class5PQ"].Points.Clear();
                // PEAK
                //CLASS 1
                if (checkedListBox2.CheckedItems.Contains("Peak") == true && checkedListBox1.CheckedItems.Contains("Class 1") == true)
                {

                    Chart.Series["Class1P"].Points.AddXY(LDoubleX[0], Dcls1p[0]);
                    Chart.Series["Class1P"].Points.AddXY(LDoubleX[1], Dcls1p[1]);
                    int index = Chart.Series["Class1P"].Points.AddXY(LDoubleX[2], Dcls1p[2]);
                    Chart.Series["Class1P"].Points[index].Color = Color.Transparent;
                    Chart.Series["Class1P"].Points.AddXY(LDoubleX[3], Dcls1p[3]);
                    int index2 = Chart.Series["Class1P"].Points.AddXY(LDoubleX[4], Dcls1p[4]);
                    Chart.Series["Class1P"].Points[index2].Color = Color.Transparent;
                    Chart.Series["Class1P"].Points.AddXY(LDoubleX[5], Dcls1p[5]);
                    int index3 = Chart.Series["Class1P"].Points.AddXY(LDoubleX[6], Dcls1p[6]);
                    Chart.Series["Class1P"].Points[index3].Color = Color.Transparent;
                    Chart.Series["Class1P"].Points.AddXY(LDoubleX[7], Dcls1p[7]);
                }

                else if (checkedListBox2.CheckedItems.Contains("Peak") == false || checkedListBox1.CheckedItems.Contains("Class 1") == false)
                {
                    Chart.Series["Class1P"].Points.Clear();
                }

                //CLASS 2
                if (checkedListBox2.CheckedItems.Contains("Peak") == true && checkedListBox1.CheckedItems.Contains("Class 2") == true)
                {

                    Chart.Series["Class2P"].Points.AddXY(LDoubleX[0], Dcls2p[0]);
                    Chart.Series["Class2P"].Points.AddXY(LDoubleX[1], Dcls2p[1]);
                    int index = Chart.Series["Class2P"].Points.AddXY(LDoubleX[2], Dcls2p[2]);
                    Chart.Series["Class2P"].Points[index].Color = Color.Transparent;
                    Chart.Series["Class2P"].Points.AddXY(LDoubleX[3], Dcls2p[3]);
                    int index2 = Chart.Series["Class2P"].Points.AddXY(LDoubleX[4], Dcls2p[4]);
                    Chart.Series["Class2P"].Points[index2].Color = Color.Transparent;
                    Chart.Series["Class2P"].Points.AddXY(LDoubleX[5], Dcls2p[5]);
                    int index3 = Chart.Series["Class2P"].Points.AddXY(LDoubleX[6], Dcls2p[6]);
                    Chart.Series["Class2P"].Points[index3].Color = Color.Transparent;
                    Chart.Series["Class2P"].Points.AddXY(LDoubleX[7], Dcls2p[7]);
                }
                else if (checkedListBox2.CheckedItems.Contains("Peak") == false || checkedListBox1.CheckedItems.Contains("Class 2") == false)
                {
                    Chart.Series["Class2P"].Points.Clear();
                }
                //CLASS 3
                if (checkedListBox2.CheckedItems.Contains("Peak") == true && checkedListBox1.CheckedItems.Contains("Class 3") == true)
                {

                    Chart.Series["Class3P"].Points.AddXY(LDoubleX[0], Dcls3p[0]);
                    Chart.Series["Class3P"].Points.AddXY(LDoubleX[1], Dcls3p[1]);
                    int index = Chart.Series["Class3P"].Points.AddXY(LDoubleX[2], Dcls3p[2]);
                    Chart.Series["Class3P"].Points[index].Color = Color.Transparent;
                    Chart.Series["Class3P"].Points.AddXY(LDoubleX[3], Dcls3p[3]);
                    int index2 = Chart.Series["Class3P"].Points.AddXY(LDoubleX[4], Dcls3p[4]);
                    Chart.Series["Class3P"].Points[index2].Color = Color.Transparent;
                    Chart.Series["Class3P"].Points.AddXY(LDoubleX[5], Dcls3p[5]);
                    int index3 = Chart.Series["Class3P"].Points.AddXY(LDoubleX[6], Dcls3p[6]);
                    Chart.Series["Class3P"].Points[index3].Color = Color.Transparent;
                    Chart.Series["Class3P"].Points.AddXY(LDoubleX[7], Dcls3p[7]);
                }
                else if (checkedListBox2.CheckedItems.Contains("Peak") == false || checkedListBox1.CheckedItems.Contains("Class 3") == false)
                {
                    Chart.Series["Class3P"].Points.Clear();
                }
                //CLASS 4
                if (checkedListBox2.CheckedItems.Contains("Peak") == true && checkedListBox1.CheckedItems.Contains("Class 4") == true)
                {

                    Chart.Series["Class4P"].Points.AddXY(LDoubleX[0], Dcls4p[0]);
                    Chart.Series["Class4P"].Points.AddXY(LDoubleX[1], Dcls4p[1]);
                    int index = Chart.Series["Class4P"].Points.AddXY(LDoubleX[2], Dcls4p[2]);
                    Chart.Series["Class4P"].Points[index].Color = Color.Transparent;
                    Chart.Series["Class4P"].Points.AddXY(LDoubleX[3], Dcls4p[3]);
                    int index2 = Chart.Series["Class4P"].Points.AddXY(LDoubleX[4], Dcls4p[4]);
                    Chart.Series["Class4P"].Points[index2].Color = Color.Transparent;
                    Chart.Series["Class4P"].Points.AddXY(LDoubleX[5], Dcls4p[5]);
                    int index3 = Chart.Series["Class4P"].Points.AddXY(LDoubleX[6], Dcls4p[6]);
                    Chart.Series["Class4P"].Points[index3].Color = Color.Transparent;
                    Chart.Series["Class4P"].Points.AddXY(LDoubleX[7], Dcls4p[7]);

                }
                else if (checkedListBox2.CheckedItems.Contains("Peak") == false || checkedListBox1.CheckedItems.Contains("Class 4") == false)
                {
                    Chart.Series["Class4P"].Points.Clear();
                }
                //CLASS 5
                if (checkedListBox2.CheckedItems.Contains("Peak") == true && checkedListBox1.CheckedItems.Contains("Class 5") == true)
                {

                    Chart.Series["Class5P"].Points.AddXY(LDoubleX[0], Dcls5p[0]);
                    Chart.Series["Class5P"].Points.AddXY(LDoubleX[1], Dcls5p[1]);
                    int index = Chart.Series["Class5P"].Points.AddXY(LDoubleX[2], Dcls5p[2]);
                    Chart.Series["Class5P"].Points[index].Color = Color.Transparent;
                    Chart.Series["Class5P"].Points.AddXY(LDoubleX[3], Dcls5p[3]);
                    int index2 = Chart.Series["Class5P"].Points.AddXY(LDoubleX[4], Dcls5p[4]);
                    Chart.Series["Class5P"].Points[index2].Color = Color.Transparent;
                    Chart.Series["Class5P"].Points.AddXY(LDoubleX[5], Dcls5p[5]);
                    int index3 = Chart.Series["Class5P"].Points.AddXY(LDoubleX[6], Dcls5p[6]);
                    Chart.Series["Class5P"].Points[index3].Color = Color.Transparent;
                    Chart.Series["Class5P"].Points.AddXY(LDoubleX[7], Dcls5p[7]);

                }
                else if (checkedListBox2.CheckedItems.Contains("Peak") == false || checkedListBox1.CheckedItems.Contains("Class 5") == false)
                {
                    Chart.Series["Class5P"].Points.Clear();
                }
                //PEAK Q
                //CLASS 1
                if (checkedListBox4.CheckedItems.Contains("Q-Peak") == true && checkedListBox3.CheckedItems.Contains("Class 1") == true)
                {

                    Chart.Series["Class1PQ"].Points.AddXY(LDoubleX[0], Dcls1pq[0]);
                    Chart.Series["Class1PQ"].Points.AddXY(LDoubleX[1], Dcls1pq[1]);
                    int index = Chart.Series["Class1PQ"].Points.AddXY(LDoubleX[2], Dcls1pq[2]);
                    Chart.Series["Class1PQ"].Points[index].Color = Color.Transparent;
                    Chart.Series["Class1PQ"].Points.AddXY(LDoubleX[3], Dcls1pq[3]);
                    int index2 = Chart.Series["Class1PQ"].Points.AddXY(LDoubleX[4], Dcls1pq[4]);
                    Chart.Series["Class1PQ"].Points[index2].Color = Color.Transparent;
                    Chart.Series["Class1PQ"].Points.AddXY(LDoubleX[5], Dcls1pq[5]);
                    int index3 = Chart.Series["Class1PQ"].Points.AddXY(LDoubleX[6], Dcls1pq[6]);
                    Chart.Series["Class1PQ"].Points[index3].Color = Color.Transparent;
                    Chart.Series["Class1PQ"].Points.AddXY(LDoubleX[7], Dcls1pq[7]);
                }

                else if (checkedListBox4.CheckedItems.Contains("Q-Peak") == false || checkedListBox3.CheckedItems.Contains("Class 1") == false)
                {
                    Chart.Series["Class1PQ"].Points.Clear();
                }

                //CLASS 2
                if (checkedListBox4.CheckedItems.Contains("Q-Peak") == true && checkedListBox3.CheckedItems.Contains("Class 2") == true)
                {

                    Chart.Series["Class2PQ"].Points.AddXY(LDoubleX[0], Dcls2pq[0]);
                    Chart.Series["Class2PQ"].Points.AddXY(LDoubleX[1], Dcls2pq[1]);
                    int index = Chart.Series["Class2PQ"].Points.AddXY(LDoubleX[2], Dcls2pq[2]);
                    Chart.Series["Class2PQ"].Points[index].Color = Color.Transparent;
                    Chart.Series["Class2PQ"].Points.AddXY(LDoubleX[3], Dcls2pq[3]);
                    int index2 = Chart.Series["Class2PQ"].Points.AddXY(LDoubleX[4], Dcls2pq[4]);
                    Chart.Series["Class2PQ"].Points[index2].Color = Color.Transparent;
                    Chart.Series["Class2PQ"].Points.AddXY(LDoubleX[5], Dcls2pq[5]);
                    int index3 = Chart.Series["Class2PQ"].Points.AddXY(LDoubleX[6], Dcls2pq[6]);
                    Chart.Series["Class2PQ"].Points[index3].Color = Color.Transparent;
                    Chart.Series["Class2PQ"].Points.AddXY(LDoubleX[7], Dcls2pq[7]);
                }
                else if (checkedListBox4.CheckedItems.Contains("Q-Peak") == false || checkedListBox3.CheckedItems.Contains("Class 2") == false)
                {
                    Chart.Series["Class2PQ"].Points.Clear();
                }
                //CLASS 3
                if (checkedListBox4.CheckedItems.Contains("Q-Peak") == true && checkedListBox3.CheckedItems.Contains("Class 3") == true)
                {

                    Chart.Series["Class3PQ"].Points.AddXY(LDoubleX[0], Dcls3pq[0]);
                    Chart.Series["Class3PQ"].Points.AddXY(LDoubleX[1], Dcls3pq[1]);
                    int index = Chart.Series["Class3PQ"].Points.AddXY(LDoubleX[2], Dcls3pq[2]);
                    Chart.Series["Class3PQ"].Points[index].Color = Color.Transparent;
                    Chart.Series["Class3PQ"].Points.AddXY(LDoubleX[3], Dcls3pq[3]);
                    int index2 = Chart.Series["Class3PQ"].Points.AddXY(LDoubleX[4], Dcls3pq[4]);
                    Chart.Series["Class3PQ"].Points[index2].Color = Color.Transparent;
                    Chart.Series["Class3PQ"].Points.AddXY(LDoubleX[5], Dcls3pq[5]);
                    int index3 = Chart.Series["Class3PQ"].Points.AddXY(LDoubleX[6], Dcls3pq[6]);
                    Chart.Series["Class3PQ"].Points[index3].Color = Color.Transparent;
                    Chart.Series["Class3PQ"].Points.AddXY(LDoubleX[7], Dcls3pq[7]);
                }
                else if (checkedListBox4.CheckedItems.Contains("Q-Peak") == false || checkedListBox3.CheckedItems.Contains("Class 3") == false)
                {
                    Chart.Series["Class3PQ"].Points.Clear();
                }
                //CLASS 4
                if (checkedListBox4.CheckedItems.Contains("Q-Peak") == true && checkedListBox3.CheckedItems.Contains("Class 4") == true)
                {

                    Chart.Series["Class4PQ"].Points.AddXY(LDoubleX[0], Dcls4pq[0]);
                    Chart.Series["Class4PQ"].Points.AddXY(LDoubleX[1], Dcls4pq[1]);
                    int index = Chart.Series["Class4PQ"].Points.AddXY(LDoubleX[2], Dcls4pq[2]);
                    Chart.Series["Class4PQ"].Points[index].Color = Color.Transparent;
                    Chart.Series["Class4PQ"].Points.AddXY(LDoubleX[3], Dcls4pq[3]);
                    int index2 = Chart.Series["Class4PQ"].Points.AddXY(LDoubleX[4], Dcls4pq[4]);
                    Chart.Series["Class4PQ"].Points[index2].Color = Color.Transparent;
                    Chart.Series["Class4PQ"].Points.AddXY(LDoubleX[5], Dcls4pq[5]);
                    int index3 = Chart.Series["Class4PQ"].Points.AddXY(LDoubleX[6], Dcls4pq[6]);
                    Chart.Series["Class4PQ"].Points[index3].Color = Color.Transparent;
                    Chart.Series["Class4PQ"].Points.AddXY(LDoubleX[7], Dcls4pq[7]);

                }
                else if (checkedListBox4.CheckedItems.Contains("Q-Peak") == false || checkedListBox3.CheckedItems.Contains("Class 4") == false)
                {
                    Chart.Series["Class4PQ"].Points.Clear();
                }
                //CLASS 5
                if (checkedListBox4.CheckedItems.Contains("Q-Peak") == true && checkedListBox3.CheckedItems.Contains("Class 5") == true)
                {

                    Chart.Series["Class5PQ"].Points.AddXY(LDoubleX[0], Dcls5pq[0]);
                    Chart.Series["Class5PQ"].Points.AddXY(LDoubleX[1], Dcls5pq[1]);
                    int index = Chart.Series["Class5PQ"].Points.AddXY(LDoubleX[2], Dcls5pq[2]);
                    Chart.Series["Class5PQ"].Points[index].Color = Color.Transparent;
                    Chart.Series["Class5PQ"].Points.AddXY(LDoubleX[3], Dcls5pq[3]);
                    int index2 = Chart.Series["Class5PQ"].Points.AddXY(LDoubleX[4], Dcls5pq[4]);
                    Chart.Series["Class5PQ"].Points[index2].Color = Color.Transparent;
                    Chart.Series["Class5PQ"].Points.AddXY(LDoubleX[5], Dcls5pq[5]);
                    int index3 = Chart.Series["Class5PQ"].Points.AddXY(LDoubleX[6], Dcls5pq[6]);
                    Chart.Series["Class5PQ"].Points[index3].Color = Color.Transparent;
                    Chart.Series["Class5PQ"].Points.AddXY(LDoubleX[7], Dcls5pq[7]);
                    Chart.ChartAreas[0].RecalculateAxesScale();
                }
                else if (checkedListBox4.CheckedItems.Contains("Q-Peak") == false || checkedListBox3.CheckedItems.Contains("Class 5") == false)
                {
                    Chart.Series["Class5PQ"].Points.Clear();
                }


            }
        }
        //SET RANGE
        private void button1_Click_1(object sender, EventArgs e)
        {
            //MINIMUM
            //Hz
            if (comboBox2.SelectedIndex == 0)
            {
                int min;
                if (int.TryParse(textBox2.Text, out min))
                {
                    if (min > 0)
                    {
                        Chart.ChartAreas[0].AxisX.Minimum = min;
                        label5.Visible = true;
                        label5.Text = (min + " Hz");
                    }
                }
            }

            //KHz
            if (comboBox2.SelectedIndex == 1)
            {
                double min;
                if (double.TryParse(textBox2.Text, out min))
                {
                    if (min > 0)
                    {
                        Chart.ChartAreas[0].AxisX.Minimum = min * 1000;
                        label5.Visible = true;
                        label5.Text = (min + " KHz");
                    }
                }
            }
            //MHz
            if (comboBox2.SelectedIndex == 2)
            {
                double min;
                if (double.TryParse(textBox2.Text, out min))
                {
                    if (min > 0)
                    {
                        Chart.ChartAreas[0].AxisX.Minimum = min * 1000000;
                        label5.Visible = true;
                        label5.Text = (min + " MHz");

                    }
                }
            }
            //MAXIMUM
            //Hz
            if (comboBox3.SelectedIndex == 0)
            {
                int max;
                if (int.TryParse(textBox3.Text, out max))
                {
                    if (max > 0)
                    {
                        Chart.ChartAreas[0].AxisX.Maximum = max;
                        label4.Visible = true;
                        label4.Text = (max + " Hz");
                    }
                }
            }
            //KHz
            if (comboBox3.SelectedIndex == 1)
            {
                double max;
                if (double.TryParse(textBox3.Text, out max))
                {
                    if (max > 0)
                    {
                        Chart.ChartAreas[0].AxisX.Maximum = max * 1000;
                        label4.Visible = true;
                        label4.Text = (max + " KHz");
                    }
                }
            }
            //MHz
            if (comboBox3.SelectedIndex == 2)
            {
                double max;
                if (double.TryParse(textBox3.Text, out max))
                {
                    if (max > 0)
                    {
                        Chart.ChartAreas[0].AxisX.Maximum = max * 1000000;
                        label4.Visible = true;
                        label4.Text = (max + " MHz");
                    }
                }
            }

        }

     

        private void label2_Click(object sender, EventArgs e)
        {

        }


    }
}


    
    

   

