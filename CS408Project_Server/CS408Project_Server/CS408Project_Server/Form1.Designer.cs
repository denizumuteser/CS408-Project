
namespace CS408Project_Server
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
            this.richTextBox_Console = new System.Windows.Forms.RichTextBox();
            this.textBox_Port = new System.Windows.Forms.TextBox();
            this.label_port = new System.Windows.Forms.Label();
            this.button_Listen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // richTextBox_Console
            // 
            this.richTextBox_Console.Location = new System.Drawing.Point(24, 77);
            this.richTextBox_Console.Name = "richTextBox_Console";
            this.richTextBox_Console.ReadOnly = true;
            this.richTextBox_Console.Size = new System.Drawing.Size(749, 353);
            this.richTextBox_Console.TabIndex = 0;
            this.richTextBox_Console.Text = "";
            // 
            // textBox_Port
            // 
            this.textBox_Port.Location = new System.Drawing.Point(59, 39);
            this.textBox_Port.Name = "textBox_Port";
            this.textBox_Port.Size = new System.Drawing.Size(119, 20);
            this.textBox_Port.TabIndex = 1;
            // 
            // label_port
            // 
            this.label_port.AutoSize = true;
            this.label_port.Location = new System.Drawing.Point(21, 42);
            this.label_port.Name = "label_port";
            this.label_port.Size = new System.Drawing.Size(32, 13);
            this.label_port.TabIndex = 2;
            this.label_port.Text = "Port: ";
            // 
            // button_Listen
            // 
            this.button_Listen.Location = new System.Drawing.Point(184, 37);
            this.button_Listen.Name = "button_Listen";
            this.button_Listen.Size = new System.Drawing.Size(75, 23);
            this.button_Listen.TabIndex = 3;
            this.button_Listen.Text = "Listen";
            this.button_Listen.UseVisualStyleBackColor = true;
            this.button_Listen.Click += new System.EventHandler(this.button_Listen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button_Listen);
            this.Controls.Add(this.label_port);
            this.Controls.Add(this.textBox_Port);
            this.Controls.Add(this.richTextBox_Console);
            this.Name = "Form1";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_Console;
        private System.Windows.Forms.TextBox textBox_Port;
        private System.Windows.Forms.Label label_port;
        private System.Windows.Forms.Button button_Listen;
    }
}

