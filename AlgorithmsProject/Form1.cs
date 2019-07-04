//StaticHuffman.cs, HuffmanNode.cs and Extensions.cs are modified from their source for the program's needs. Source: http://www.eliasdigital.com/Articles/Article/2016/Apr/6/ACloserLookAtHuffmanEncodingUsingCSharp/95

using System;
using System.Drawing;
using System.Windows.Forms;
using ZedGraph;

namespace AlgorithmsProject
{
    public partial class Form1 : Form
    {
        private double _static_huffman_running_time = 0;
        private double _static_huffman_compression_ratio = 0;       
        private double _dynamic_huffman_running_time = 0;
        private double _dynamic_huffman_compression_ratio = 0;

        public Form1()
        {
            InitializeComponent();
        }

        public void Perform_Static_Huffman(string path_to_file)
        {
            StaticHuffman static_huffman = new StaticHuffman();
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            stopwatch.Start();
            _static_huffman_compression_ratio = static_huffman.Compress(path_to_file);
            stopwatch.Stop();

            _static_huffman_running_time = stopwatch.Elapsed.TotalSeconds;
        }

        public void Perform_Dynamic_Huffman(string path_to_file)
        {
            System.IO.StreamReader file = new System.IO.StreamReader(path_to_file);

            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            stopwatch.Start();
            _dynamic_huffman_compression_ratio = DynamicHuffman.Encode(path_to_file);
            stopwatch.Stop();

            _dynamic_huffman_running_time = stopwatch.Elapsed.TotalSeconds;
        }

        public void Draw_Compression_Ratios(Color static_color, Color dynamic_color)
        {
            zedGraphControlCompressionRatios.GraphPane = new GraphPane();

            PointPairList static_points = new PointPairList();
            static_points.Add(1, _static_huffman_compression_ratio * 100);

            PointPairList dynamic_points = new PointPairList();
            dynamic_points.Add(2, _dynamic_huffman_compression_ratio * 100);

            zedGraphControlCompressionRatios.GraphPane.Title.Text = "Compression Ratios";
            zedGraphControlCompressionRatios.GraphPane.XAxis.Title.Text = "Algorithms";
            zedGraphControlCompressionRatios.GraphPane.YAxis.Title.Text = "Ratio (%)";

            zedGraphControlCompressionRatios.GraphPane.AddBar($"Static ({_static_huffman_compression_ratio * 100}%)", static_points, static_color);
            zedGraphControlCompressionRatios.GraphPane.AddBar($"Dynamic ({_dynamic_huffman_compression_ratio * 100}%)", dynamic_points, dynamic_color);            
        }

        public void Draw_Running_Times(Color static_color, Color dynamic_color)
        {
            zedGraphControlRunningTimes.GraphPane = new GraphPane();

            PointPairList static_points = new PointPairList();
            static_points.Add(1, _static_huffman_running_time);

            PointPairList dynamic_points = new PointPairList();
            dynamic_points.Add(2, _dynamic_huffman_running_time);

            zedGraphControlRunningTimes.GraphPane.Title.Text = "Running times";
            zedGraphControlRunningTimes.GraphPane.XAxis.Title.Text = "Algorithms";
            zedGraphControlRunningTimes.GraphPane.YAxis.Title.Text = "Time (seconds)";

            zedGraphControlRunningTimes.GraphPane.AddBar($"Static ({_static_huffman_running_time})", static_points, static_color);
            zedGraphControlRunningTimes.GraphPane.AddBar($"Dynamic ({_dynamic_huffman_running_time})", dynamic_points, dynamic_color);
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            if(openFileDialogFileSelect.ShowDialog() == DialogResult.OK)
            {
                labelFile.Text = $"Loading {openFileDialogFileSelect.FileName}...";
                Cursor = Cursors.WaitCursor;
                Application.DoEvents();

                Perform_Static_Huffman(openFileDialogFileSelect.FileName);
                Perform_Dynamic_Huffman(openFileDialogFileSelect.FileName);
                Draw_Compression_Ratios(Color.Green, Color.Blue);
                Draw_Running_Times(Color.Green, Color.Blue);
                
                zedGraphControlCompressionRatios.AxisChange();
                zedGraphControlRunningTimes.AxisChange();

                Size = new Size(Size.Width, Size.Height + 1);
                Size = new Size(Size.Width, Size.Height - 1);

                labelFile.Text = $"Loaded {openFileDialogFileSelect.FileName}.";
                Cursor = Cursors.Default;
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (System.IO.File.Exists($@"{Application.StartupPath}\output.dynhuf"))
                System.IO.File.Delete($@"{Application.StartupPath}\output.dynhuf");
        }
    }
}
