﻿// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using ToolsGenGkode.Properties;

namespace ToolsGenGkode.filters
{
    public partial class FrmFilter3 : Form
    {
        public FrmFilter3()
        {
            InitializeComponent();
        }

        private void FrmFilter3_Load(object sender, EventArgs e)
        {
            numericUpDown1.Value = Settings.Default.filter3_minValue;

            if (Settings.Default.filter3_mapS == null)
            {
                textBox1.Text = "";
            }
            else
            {
                string newStr = "";
                foreach (string VARIABLE in Settings.Default.filter3_mapS)
                {
                    newStr += VARIABLE + (char)13;
                }
                textBox1.Text = newStr;
            }

            if (Settings.Default.filter3_mapF == null)
            {
                textBox2.Text = "";
            }
            else
            {
                string newStr = "";
                foreach (string VARIABLE in Settings.Default.filter3_mapF)
                {
                    newStr += VARIABLE + (char)13;
                }
                textBox2.Text = newStr;
            }


            ParseDataString();

            //chart1.ChartAreas[0].AxisX.ScaleView.Zoom(0,255);

            chart1.ChartAreas[0].AxisX.Maximum = 255;
            chart1.ChartAreas[0].AxisX.Minimum = 0;
            chart1.ChartAreas[0].AxisX.Title = @"Bright";
            chart1.ChartAreas[0].AxisY.Title = @"S - power";
            chart1.Legends[0].Enabled = false;

            chart1.Series.Clear();

            chart1.Series.Add(new Series("Sseries"));
            chart1.Series.Add(new Series("Fseries"));



            chart1.Series["Sseries"].ChartType = SeriesChartType.Line;
            chart1.Series["Sseries"].BorderWidth = 2;
            chart1.Series["Sseries"].MarkerStyle = MarkerStyle.Circle;
            chart1.Series["Sseries"].BorderColor = Color.Blue;

            chart1.Series["Fseries"].ChartType = SeriesChartType.Line;
            chart1.Series["Fseries"].BorderWidth = 2;
            chart1.Series["Fseries"].MarkerStyle = MarkerStyle.Circle;
            chart1.Series["Fseries"].BorderColor = Color.Crimson;

            chart1.ChartAreas[0].AxisX.ArrowStyle = AxisArrowStyle.Lines;
            chart1.ChartAreas[0].AxisY.ArrowStyle = AxisArrowStyle.Lines;



            //chart1.ChartAreas[0].CursorX.IsUserEnabled = true;
            //chart1.ChartAreas[0].CursorY.IsUserEnabled = true;


            chart1.ChartAreas[0].CursorX.IsUserSelectionEnabled = false;
            chart1.ChartAreas[0].CursorY.IsUserSelectionEnabled = false;
            //chart1.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            //chart1.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;
            //chart1.Series[0].Label = @"Bright->S";

            chartRefresh();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            Settings.Default.filter3_minValue = (int) numericUpDown1.Value;

            string[] ss = textBox1.Text.Split((char)13);

            Settings.Default.filter3_mapS = new StringCollection();

            foreach (string VARIABLE in ss)
            {
                Settings.Default.filter3_mapS.Add(VARIABLE);
            }


            string[] ss2 = textBox2.Text.Split((char)13);

            Settings.Default.filter3_mapF = new StringCollection();

            foreach (string VARIABLE in ss2)
            {
                Settings.Default.filter3_mapF.Add(VARIABLE);
            }



            Settings.Default.Save();
        }

        List<myPoint> PointsS = new List<myPoint>();
        List<myPoint> PointsF = new List<myPoint>();

        //парсинг строки
        public void ParseDataString()
        {
            PointsS = new List<myPoint>();

            string[] ss = textBox1.Text.Split((char)13);


            foreach (string VARIABLE in ss)
            {
                string[] newSS = VARIABLE.Split(';');
                if (newSS.Length != 2) continue;

                int p1 = 0;
                int p2 = 0;

                int.TryParse(newSS[0], out p1);
                int.TryParse(newSS[1], out p2);

                PointsS.Add(new myPoint(p1, p2));
            }


            PointsF = new List<myPoint>();

            string[] ss2 = textBox2.Text.Split((char)13);


            foreach (string VARIABLE in ss2)
            {
                string[] newSS = VARIABLE.Split(';');
                if (newSS.Length != 2) continue;

                int p1 = 0;
                int p2 = 0;

                int.TryParse(newSS[0], out p1);
                int.TryParse(newSS[1], out p2);

                PointsF.Add(new myPoint(p1, p2));
            }






        }


        public void chartRefresh()
        {
            chart1.Series["Sseries"].Points.Clear();
            chart1.Series["Fseries"].Points.Clear();


            foreach (myPoint VARIABLE in PointsS)
            {
                //    if (VARIABLE.Cells[0].Value == null || VARIABLE.Cells[1].Value == null) continue;

                //    int x = 0;
                //    int y = 0;

                //    int.TryParse(VARIABLE.Cells[0].Value.ToString(), out x);
                //    int.TryParse(VARIABLE.Cells[1].Value.ToString(), out y);

                int pos = chart1.Series["Sseries"].Points.AddXY(VARIABLE.X, VARIABLE.Y);
                chart1.Series["Sseries"].Points[pos].Color = Color.Blue;

            }


            foreach (myPoint VARIABLE in PointsF)
            {
                //    if (VARIABLE.Cells[0].Value == null || VARIABLE.Cells[1].Value == null) continue;

                //    int x = 0;
                //    int y = 0;

                //    int.TryParse(VARIABLE.Cells[0].Value.ToString(), out x);
                //    int.TryParse(VARIABLE.Cells[1].Value.ToString(), out y);

                int pos = chart1.Series["Fseries"].Points.AddXY(VARIABLE.X, VARIABLE.Y);
                chart1.Series["Fseries"].Points[pos].Color = Color.Crimson;

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            ParseDataString();
            chartRefresh();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0;0 \n 255;1000";
            ParseDataString();
            chartRefresh();
        }
    }

    public class myPoint
    {
        public int X;
        public int Y;
        public bool selected;

        public myPoint()
        {
            X = 0;
            Y = 0;
            selected = false;

        }

        public myPoint(int _x, int _y, bool _select = false)
        {
            X = _x;
            Y = _y;
            selected = _select;
        }

    }
}
