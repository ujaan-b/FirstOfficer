namespace FirstOfficer
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
            this.lblAltitude = new System.Windows.Forms.Label();
            this.btnTestAI = new System.Windows.Forms.Button();
            this.txtQuestion = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.lblAIResponse = new System.Windows.Forms.Label();
            this.btnRecordVoice = new System.Windows.Forms.Button();
            this.btnStopRecord = new System.Windows.Forms.Button();
            this.btnTranscribe = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblAltitude
            // 
            this.lblAltitude.AutoSize = true;
            this.lblAltitude.Location = new System.Drawing.Point(12, 9);
            this.lblAltitude.Name = "lblAltitude";
            this.lblAltitude.Size = new System.Drawing.Size(48, 13);
            this.lblAltitude.TabIndex = 0;
            this.lblAltitude.Text = "Altitude: ";
            this.lblAltitude.Click += new System.EventHandler(this.label1_Click);
            // 
            // btnTestAI
            // 
            this.btnTestAI.BackColor = System.Drawing.Color.PowderBlue;
            this.btnTestAI.Location = new System.Drawing.Point(312, 402);
            this.btnTestAI.Name = "btnTestAI";
            this.btnTestAI.Size = new System.Drawing.Size(75, 23);
            this.btnTestAI.TabIndex = 1;
            this.btnTestAI.Text = "Ask";
            this.btnTestAI.UseVisualStyleBackColor = false;
            this.btnTestAI.Click += new System.EventHandler(this.btnTestAI_Click);
            // 
            // txtQuestion
            // 
            this.txtQuestion.Location = new System.Drawing.Point(15, 343);
            this.txtQuestion.Multiline = true;
            this.txtQuestion.Name = "txtQuestion";
            this.txtQuestion.Size = new System.Drawing.Size(756, 53);
            this.txtQuestion.TabIndex = 2;
            this.txtQuestion.TextChanged += new System.EventHandler(this.textBox1_TextChanged_1);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // lblAIResponse
            // 
            this.lblAIResponse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAIResponse.Location = new System.Drawing.Point(15, 31);
            this.lblAIResponse.Name = "lblAIResponse";
            this.lblAIResponse.Size = new System.Drawing.Size(756, 309);
            this.lblAIResponse.TabIndex = 4;
            this.lblAIResponse.Text = "label1";
            // 
            // btnRecordVoice
            // 
            this.btnRecordVoice.BackColor = System.Drawing.Color.LightGreen;
            this.btnRecordVoice.Location = new System.Drawing.Point(15, 402);
            this.btnRecordVoice.Name = "btnRecordVoice";
            this.btnRecordVoice.Size = new System.Drawing.Size(100, 23);
            this.btnRecordVoice.TabIndex = 5;
            this.btnRecordVoice.Text = "Record Voice";
            this.btnRecordVoice.UseVisualStyleBackColor = false;
            this.btnRecordVoice.Click += new System.EventHandler(this.btnRecordVoice_Click);
            // 
            // btnStopRecord
            // 
            this.btnStopRecord.BackColor = System.Drawing.Color.LightCoral;
            this.btnStopRecord.Location = new System.Drawing.Point(121, 402);
            this.btnStopRecord.Name = "btnStopRecord";
            this.btnStopRecord.Size = new System.Drawing.Size(104, 23);
            this.btnStopRecord.TabIndex = 6;
            this.btnStopRecord.Text = "Stop Recording";
            this.btnStopRecord.UseVisualStyleBackColor = false;
            this.btnStopRecord.Click += new System.EventHandler(this.btnStopRecord_Click);
            // 
            // btnTranscribe
            // 
            this.btnTranscribe.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.btnTranscribe.Location = new System.Drawing.Point(231, 402);
            this.btnTranscribe.Name = "btnTranscribe";
            this.btnTranscribe.Size = new System.Drawing.Size(75, 23);
            this.btnTranscribe.TabIndex = 7;
            this.btnTranscribe.Text = "Transcribe Voice";
            this.btnTranscribe.UseVisualStyleBackColor = false;
            this.btnTranscribe.Click += new System.EventHandler(this.btnTranscribe_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(783, 450);
            this.Controls.Add(this.btnTranscribe);
            this.Controls.Add(this.btnStopRecord);
            this.Controls.Add(this.btnRecordVoice);
            this.Controls.Add(this.lblAIResponse);
            this.Controls.Add(this.txtQuestion);
            this.Controls.Add(this.btnTestAI);
            this.Controls.Add(this.lblAltitude);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load_1);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblAltitude;
        private System.Windows.Forms.Button btnTestAI;
        private System.Windows.Forms.TextBox txtQuestion;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Label lblAIResponse;
        private System.Windows.Forms.Button btnRecordVoice;
        private System.Windows.Forms.Button btnStopRecord;
        private System.Windows.Forms.Button btnTranscribe;
    }
}

