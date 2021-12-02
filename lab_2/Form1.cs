using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace lab_2
{
    public partial class Form1 : Form
    {
        Specter specter = new Specter();
        public Form1()
        {
            InitializeComponent();

            Signal signal = new Signal();
            PolyHarmonicSignal signal1 = new PolyHarmonicSignal();
            
            GraphDrawer graphDrawer = new GraphDrawer();
            PointPairList pointPairs = signal.generateSignal(256);
            
            graphDrawer.drawSignal(pointPairs, zedGraphControl1, "original", Color.Black);
            specter.dpf(pointPairs);
            graphDrawer.drawSpecter(specter.specterA,"specterA",zedGraphControl2);
            graphDrawer.drawSpecter(specter.specterF, "specterF", zedGraphControl3);
            graphDrawer.drawSignal(specter.undpf(), zedGraphControl1, "undpf", Color.Blue);
            //graphDrawer.drawSignal(specter.undpfNF(), zedGraphControl1, "undpfNF", Color.Red);

            pointPairs = signal1.generateSignal(256);
            graphDrawer.drawSignal(pointPairs, zedGraphControl4, "original", Color.Black);
            specter.dpf(pointPairs);
            graphDrawer.drawSpecter(specter.specterA, "specterA", zedGraphControl5);
            graphDrawer.drawSpecter(specter.specterF, "specterF", zedGraphControl6);
            
            graphDrawer.drawSignal(specter.undpf(), zedGraphControl4, "undpf1", Color.Blue);
            //graphDrawer.drawSignal(specter.undpfNF(), zedGraphControl4, "undpf2", Color.Red);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            Filter filter = new Filter();
            GraphDrawer graphDrawer = new GraphDrawer(true);
            filter.filterMethod(Convert.ToInt32(textBoxMin.Text), Convert.ToInt32(textBoxMax.Text), specter.specterA, specter.specterF);
            graphDrawer.drawSpecter(filter.specterA, "specterA", zedGraphControl5);
            graphDrawer.drawSpecter(filter.specterF, "specterF", zedGraphControl6);
            graphDrawer.drawSignal(specter.undpfParam(filter.specterA,filter.specterF), zedGraphControl4, "undpf1", Color.Blue);
        }
    }
    public class GraphDrawer 
    {
        bool clean = false;
        public GraphDrawer(bool clean) 
        {
            this.clean = clean;
        }
        public GraphDrawer()
        {
        }
        public void drawSignal(PointPairList points, ZedGraphControl zedGraphControl, string label, Color color) 
        {
            GraphPane pane = zedGraphControl.GraphPane;
            if (clean)
            {
                pane.CurveList.Clear();
            }
            LineItem curve = pane.AddCurve(label, points, color, SymbolType.None);
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }
        public void drawSpecter(List<double> values, string label, ZedGraphControl zedGraphControl)
        {
            double[] valuseForDiagram = values.ToArray();
            GraphPane pane = zedGraphControl.GraphPane;
            if (clean)
            {
                pane.CurveList.Clear();
            }
            BarItem bar = pane.AddBar("specter", null, valuseForDiagram, Color.Red);
            pane.BarSettings.MinClusterGap = 0.0f;
            zedGraphControl.AxisChange();
            zedGraphControl.Invalidate();
        }
    }

}
