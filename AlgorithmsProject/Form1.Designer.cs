namespace AlgorithmsProject
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.buttonOpen = new System.Windows.Forms.Button();
            this.openFileDialogFileSelect = new System.Windows.Forms.OpenFileDialog();
            this.zedGraphControlCompressionRatios = new ZedGraph.ZedGraphControl();
            this.zedGraphControlRunningTimes = new ZedGraph.ZedGraphControl();
            this.labelFile = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // buttonOpen
            // 
            this.buttonOpen.Location = new System.Drawing.Point(12, 12);
            this.buttonOpen.Name = "buttonOpen";
            this.buttonOpen.Size = new System.Drawing.Size(75, 23);
            this.buttonOpen.TabIndex = 0;
            this.buttonOpen.Text = "Open...";
            this.buttonOpen.UseVisualStyleBackColor = true;
            this.buttonOpen.Click += new System.EventHandler(this.buttonOpen_Click);
            // 
            // openFileDialogFileSelect
            // 
            this.openFileDialogFileSelect.DefaultExt = "txt";
            this.openFileDialogFileSelect.Filter = "Text files (*.txt)|*.txt";
            this.openFileDialogFileSelect.Title = "Open a file";
            // 
            // zedGraphControlCompressionRatios
            // 
            this.zedGraphControlCompressionRatios.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.zedGraphControlCompressionRatios.Location = new System.Drawing.Point(12, 41);
            this.zedGraphControlCompressionRatios.Name = "zedGraphControlCompressionRatios";
            this.zedGraphControlCompressionRatios.ScrollGrace = 0D;
            this.zedGraphControlCompressionRatios.ScrollMaxX = 0D;
            this.zedGraphControlCompressionRatios.ScrollMaxY = 0D;
            this.zedGraphControlCompressionRatios.ScrollMaxY2 = 0D;
            this.zedGraphControlCompressionRatios.ScrollMinX = 0D;
            this.zedGraphControlCompressionRatios.ScrollMinY = 0D;
            this.zedGraphControlCompressionRatios.ScrollMinY2 = 0D;
            this.zedGraphControlCompressionRatios.Size = new System.Drawing.Size(570, 570);
            this.zedGraphControlCompressionRatios.TabIndex = 0;
            // 
            // zedGraphControlRunningTimes
            // 
            this.zedGraphControlRunningTimes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.zedGraphControlRunningTimes.Location = new System.Drawing.Point(588, 41);
            this.zedGraphControlRunningTimes.Name = "zedGraphControlRunningTimes";
            this.zedGraphControlRunningTimes.ScrollGrace = 0D;
            this.zedGraphControlRunningTimes.ScrollMaxX = 0D;
            this.zedGraphControlRunningTimes.ScrollMaxY = 0D;
            this.zedGraphControlRunningTimes.ScrollMaxY2 = 0D;
            this.zedGraphControlRunningTimes.ScrollMinX = 0D;
            this.zedGraphControlRunningTimes.ScrollMinY = 0D;
            this.zedGraphControlRunningTimes.ScrollMinY2 = 0D;
            this.zedGraphControlRunningTimes.Size = new System.Drawing.Size(570, 570);
            this.zedGraphControlRunningTimes.TabIndex = 0;
            // 
            // labelFile
            // 
            this.labelFile.AutoSize = true;
            this.labelFile.Location = new System.Drawing.Point(93, 17);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(38, 13);
            this.labelFile.TabIndex = 1;
            this.labelFile.Text = "Ready";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1170, 623);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.zedGraphControlRunningTimes);
            this.Controls.Add(this.zedGraphControlCompressionRatios);
            this.Controls.Add(this.buttonOpen);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "Form1";
            this.Text = "Static & Dynamic Huffman Coding";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialogFileSelect;
        private ZedGraph.ZedGraphControl zedGraphControlCompressionRatios;
        private ZedGraph.ZedGraphControl zedGraphControlRunningTimes;
        private System.Windows.Forms.Label labelFile;
    }
}

