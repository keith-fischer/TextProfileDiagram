using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Msagl.Drawing;
namespace StringCompare
{
    public partial class Form1 : Form
    {
        Dictionary<string, int> WordCountDict = new Dictionary<string, int>();
        bool Render = false;
        public Form1()
        {
            InitializeComponent();
            this.textBox1.MaxLength = 99999999;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //stringcompare sc = new stringcompare();
            //int n=sc.Compare(this.textBox1.Text, this.textBox2.Text);
            //this.Text = n.ToString();
            if (!Render)
            {
                //IList<string> list = new List<string>();
                //IList<string> list2 = new List<string>();

                //char[] a = this.textBox1.Text.ToCharArray();
                //char[] b = this.textBox2.Text.ToCharArray();
                //string[] sb = this.textBox2.Text.Split(new char[] { ' ' });

                //foreach (char c in a)
                //{
                //    foreach (char d in b)
                //    {
                //        Node A = graph.FindNode(c.ToString());
                //        Node B = graph.FindNode(d.ToString());
                //        if (A != null)
                //        {
                //            A.Attr.LineWidth++;
                //        }
                //        else
                //        {
                //            if (!list.Contains(c.ToString() + d.ToString()))
                //            {
                //                list.Add(c.ToString() + d.ToString());
                //                Edge E = (Edge)graph.AddEdge(c.ToString(), d.ToString());
                //                E.Attr.LineWidth++;
                //            }
                //        }
                //        if (B != null)
                //        {
                //            B.Attr.LineWidth++;
                //        }
                //        else
                //        {
                //            if (!list.Contains(c.ToString() + d.ToString()))
                //            {
                //                list.Add(c.ToString() + d.ToString());
                //                Edge E = (Edge)graph.AddEdge(c.ToString(), d.ToString());
                //                E.Attr.LineWidth++;
                //            }
                //        }

                //    }

                //}
                this.Text = "Parsing...";
                string[] sa = this.textBox1.Text.Split(new char[] { ' ' });// space char 32 delimiter
                this.progressBar1.Value = 0;
                this.progressBar1.Minimum = 0;
                this.progressBar1.Maximum = sa.Length + 1;
                this.progressBar1.Refresh();
                this.Text = "Analyzing Text...";

                Application.DoEvents();
                //count word pairs into hash
                for (int i = 0; i < sa.Length; i++)/// (string c in sa)
                {
                    string c2 = "";
                    string c = sa[i];
                    if (i + 1 < sa.Length)
                        c2 = sa[i + 1];
                    if (c.Length > 0 && c2.Length > 0)
                    {
                        if (!WordCountDict.ContainsKey(c + "||" + c2))
                        {
                            //graph.AddEdge(c , c2);
                            WordCountDict.Add(c + "||" + c2, 1);
                        }
                        else
                            WordCountDict[c + "||" + c2]++;

                    }
                    this.progressBar1.Increment(1);
                    this.progressBar1.Refresh();
                    Application.DoEvents();
                }
            }

            this.progressBar1.Refresh();
            Application.DoEvents();
            Graph graph = new Graph("graph");
            //Edge edge = (Edge)graph.AddEdge("S24", "27");

            graph.Attr.LayerDirection = LayerDirection.LR;

            this.progressBar1.Value = 0;            
            int v=(int)this.numericUpDown1.Value;
            string[] words=new string[WordCountDict.Count];
            WordCountDict.Keys.CopyTo(words , 0);
            
            this.progressBar1.Minimum = 0;
            this.progressBar1.Maximum = words.Length + 1;
            this.progressBar1.Refresh();
            this.Text = "Inserting Nodes...";

            Application.DoEvents();
            //insert edges into graph with threshhold count display
            foreach (string word in words)
            {
                int n=WordCountDict[word];
                if (n<v)
                    continue;
                string[] nodes=word.Split(new string[]{"||"},StringSplitOptions.None);
                if (nodes[0].Length==0)
                    nodes[0]="BLANK";
                if (nodes[1].Length==0)
                    nodes[1]="BLANK";

                Edge E = (Edge)graph.AddEdge(nodes[0] , nodes[1]);
                E.LabelText = n.ToString() + ":" + nodes[0] + ":" + nodes[1];
                n = EvalLineWidth(n, v);
                E.Attr.LineWidth=n;
                
                if (n>16)
                    E.Attr.Color=Microsoft.Msagl.Drawing.Color.LightGray;
                
                this.progressBar1.Increment(1);
                this.progressBar1.Refresh();
                Application.DoEvents();
            }
            this.progressBar1.Value = 0;
            this.progressBar1.Refresh();
            this.Text = "Rendering Graph...";

            Application.DoEvents();
            System.Drawing.Color tempColor = this.progressBar1.BackColor;


            this.progressBar1.BackColor = System.Drawing.Color.Red;
            this.progressBar1.Refresh();
            Application.DoEvents();
            //Render graph
            gViewer1.Graph = graph;
            this.progressBar1.BackColor = tempColor;
            this.Text = "Text Profiler";

                //foreach (string d in sb)
                //{
                //    if (c == d)
                //    {
                //        Edge E = new Edge("A" + c, "A" + c + "B" + d, "B" + d);
                //        if (!list.Contains("A" + c + "B" + d))
                //        {

                //            E = (Edge)graph.AddEdge("A" + c, "B" + d);

                //            list.Add("A" + c + "B" + d);
                //        }
                //        else
                //        {
                //            Edge A=graph.EdgeById("A" + c);
                //            if(A!=null)
                //            A.Attr.LineWidth++;
                //            Edge B = graph.EdgeById("B" + d);
                //            if(B!=null)
                //            B.Attr.LineWidth++;

                //        }
                //    }
                //}

            //foreach (string d in sb)
            //{
            //    foreach (string c in sa)
            //    {
            //        if (c == d)
            //        {
            //            Edge E = new Edge("B" + d, "B" + d + "A" + c, "A" + c);
            //            if (!list.Contains("B" + d + "A" + c))
            //            {
            //                list.Add("B" + d + "A" + c);
            //                graph.AddEdge("B" + d, "A" + c);
            //            }
            //            else
            //            {
            //                Edge A = graph.EdgeById("A" + c);
            //                if (A != null)
            //                    A.Attr.LineWidth++;
            //                Edge B = graph.EdgeById("B" + d);
            //                if (B != null)
            //                    B.Attr.LineWidth++;

            //            }

            //        }
            //    }

            //}
            //this.listBox1.Items.Clear();
            //foreach (string s in list)
            //    this.listBox1.Items.Add(s);


            //edge.LabelText = "Edge Label Test";

            //graph.AddEdge("S24", "25");
            //edge = graph.AddEdge("S1", "10") as Edge;

            //edge.LabelText = "Init";
            //edge.Attr.ArrowheadAtTarget = ArrowStyle.Tee;
            ////  edge.Attr.Weight = 10;
            //edge = graph.AddEdge("S1", "2") as Edge;
            //// edge.Attr.Weight = 10;
            //graph.AddEdge("S35", "36");
            //graph.AddEdge("S35", "43");
            //graph.AddEdge("S30", "31");
            //graph.AddEdge("S30", "33");
            //graph.AddEdge("9", "42");
            //graph.AddEdge("9", "T1");
            //graph.AddEdge("25", "T1");
            //graph.AddEdge("25", "26");
            //graph.AddEdge("27", "T24");
            //graph.AddEdge("2", "3");
            //graph.AddEdge("2", "16");
            //graph.AddEdge("2", "17");
            //graph.AddEdge("2", "T1");
            //graph.AddEdge("2", "18");
            //graph.AddEdge("10", "11");
            //graph.AddEdge("10", "14");
            //graph.AddEdge("10", "T1");
            //graph.AddEdge("10", "13");
            //graph.AddEdge("10", "12");
            //graph.AddEdge("31", "T1");
            //edge = (Edge)graph.AddEdge("31", "32");
            //edge.Attr.ArrowheadAtTarget = ArrowStyle.Tee;
            //edge.Attr.LineWidth = 10;

            //edge = (Edge)graph.AddEdge("33", "T30");
            //edge.Attr.LineWidth = 15;
            //edge.Attr.AddStyle(Microsoft.Msagl.Drawing.Style.Dashed);
            //graph.AddEdge("33", "34");
            //graph.AddEdge("42", "4");
            //graph.AddEdge("26", "4");
            //graph.AddEdge("3", "4");
            //graph.AddEdge("16", "15");
            //graph.AddEdge("17", "19");
            //graph.AddEdge("18", "29");
            //graph.AddEdge("11", "4");
            //graph.AddEdge("14", "15");
            //graph.AddEdge("37", "39");
            //graph.AddEdge("37", "41");
            //graph.AddEdge("37", "38");
            //graph.AddEdge("37", "40");
            //graph.AddEdge("13", "19");
            //graph.AddEdge("12", "29");
            //graph.AddEdge("43", "38");
            //graph.AddEdge("43", "40");
            //graph.AddEdge("36", "19");
            //graph.AddEdge("32", "23");
            //graph.AddEdge("34", "29");
            //graph.AddEdge("39", "15");
            //graph.AddEdge("41", "29");
            //graph.AddEdge("38", "4");
            //graph.AddEdge("40", "19");
            //graph.AddEdge("4", "5");
            //graph.AddEdge("19", "21");
            //graph.AddEdge("19", "20");
            //graph.AddEdge("19", "28");
            //graph.AddEdge("5", "6");
            //graph.AddEdge("5", "T35");
            //graph.AddEdge("5", "23");
            //graph.AddEdge("21", "22");
            //graph.AddEdge("20", "15");
            //graph.AddEdge("28", "29");
            //graph.AddEdge("6", "7");
            //graph.AddEdge("15", "T1");
            //graph.AddEdge("22", "23");
            //graph.AddEdge("22", "T35");
            //graph.AddEdge("29", "T30");
            //graph.AddEdge("7", "T8");
            //graph.AddEdge("23", "T24");
            //graph.AddEdge("23", "T1");

            ////node.LabelText = "Label Test";
            //CreateSourceNode(graph.FindNode("S1") as Node);
            //CreateSourceNode(graph.FindNode("S24") as Node);
            //CreateSourceNode(graph.FindNode("S35") as Node);


            //CreateTargetNode(graph.FindNode("T24") as Node);
            //CreateTargetNode(graph.FindNode("T1") as Node);
            //CreateTargetNode(graph.FindNode("T30") as Node);
            //CreateTargetNode(graph.FindNode("T8") as Node);

            //layout the graph and draw it
            //System.Drawing.Color tempColor = this.progressBar1.BackColor;
            

            //this.progressBar1.BackColor = System.Drawing.Color.Red;
            //this.progressBar1.Refresh();
            //Application.DoEvents();
            //gViewer1.Graph = graph;
            //this.progressBar1.BackColor = tempColor;
            //this.Text = "Text Profiler";

            //this.propertyGrid1.SelectedObject = graph;
        }

        private int EvalLineWidth(int Count, int thresh)
        {
            int rc = 1;

            rc = Count / thresh;
            if (rc > 30)
                rc = 30;
            return rc;
        }
        private static void CreateSourceNode(Node a)
        {
            a.Attr.Shape = Microsoft.Msagl.Drawing.Shape.Box;
            a.Attr.XRadius = 3;
            a.Attr.YRadius = 3;
            a.Attr.FillColor = Microsoft.Msagl.Drawing.Color.Green;
            a.Attr.LineWidth = 10;
        }

        private void CreateTargetNode(Node a)
        {
            a.Attr.Shape = Microsoft.Msagl.Drawing.Shape.DoubleCircle;
            a.Attr.FillColor = Microsoft.Msagl.Drawing.Color.LightGray;

            a.Attr.LabelMargin = -4;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Render = !Render;
            if (Render)
                this.button2.BackColor = System.Drawing.Color.Green;
            else
                this.button2.BackColor = System.Drawing.Color.LightGray;
        }
    }
}
