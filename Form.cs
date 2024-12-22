using System;
using System.Collections.Generic;
using System.Linq;
using ZedGraph;

namespace KASDLab22
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
            InitializeGraph();
        }


        List<double[]> list;

        private readonly string[ ] names = { "MyHashMap", "MyTreeMap" };

        private readonly System.Drawing.Color[] colors = { System.Drawing.Color.Red, System.Drawing.Color.Purple };

        private void VisGraph(object sender, EventArgs e)
        {
            int iterations = 4;

            list = new List<double[]>();

            switch (OperationType.SelectedIndex)
            {
                case 0:
                    for (int index = 1; index <= iterations; index++)
                    {
                        int length = (int)Math.Pow(10, index);
                        double[] speeds = { Speed.SpeedOfAddingToMyHashMap(length), Speed.SpeedOfAddingToMyTreeMap(length) };
                        list.Add(speeds);
                    }
                    break;
                case 1:
                    for (int index = 1; index <= iterations; index++)
                    {
                        int length = (int)Math.Pow(10, index);
                        double[] speeds = { Speed.SpeedOfGettingFromMyHashMap(length), Speed.SpeedOfGettingFromMyTreeMap(length) };
                        list.Add(speeds);
                    }
                    break;
                case 2:
                    for (int index = 1; index <= iterations; index++)
                    {
                        int length = (int)Math.Pow(10, index);
                        double[] speeds = { Speed.SpeedOfRemovingFromMyHashMap(length), Speed.SpeedOfRemovingFromMyTreeMap(length) };
                        list.Add(speeds);
                    }
                    break;
            }

            PaintGraph(list);
        }

        private void PaintGraph(List<double[]> list)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            pane.CurveList.Clear();

            List<PointPairList> arrayOfLists = new List<PointPairList>();

            for (int index = 0; index < 2; index++)
            {
                arrayOfLists.Add(new PointPairList());
            }

            for (int i = 0; i < list.Count(); i++)
            {
                for (int index = 0; index < 2; index++)
                {
                    arrayOfLists[index].Add(Math.Pow(10, i + 1), list[i][index]);
                }
            }

            for (int index = 0; index < 2; index++)
            {
                pane.AddCurve(names[index], arrayOfLists[index], colors[index], SymbolType.None);
                ((LineItem)pane.CurveList[index]).Line.Width = 3.0f;
            }

            pane.XAxis.Scale.Max = Math.Pow(10, list.Count());

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        private void InitializeGraph()
        {
            zedGraphControl1.GraphPane.Title.Text = "ЗАВИСИМОСТЬ ВРЕМЕНИ ВЫПОЛНЕНИЯ ОТ РАЗМЕРА СТРУКТУРЫ";
            zedGraphControl1.GraphPane.XAxis.Title.Text = "РАЗМЕР СТРУКТУРЫ, шт";
            zedGraphControl1.GraphPane.YAxis.Title.Text = "ВРЕМЯ ВЫПОЛНЕНИЯ ОПЕРАЦИИ, мс";
        }
    }
}