
namespace NetCommandSender
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
            this.lb_commands = new System.Windows.Forms.ListBox();
            this.rb_buttons = new System.Windows.Forms.RadioButton();
            this.rb_center = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_connect = new System.Windows.Forms.Button();
            this.btn_send = new System.Windows.Forms.Button();
            this.flp_boxes = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lb_commands
            // 
            this.lb_commands.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_commands.FormattingEnabled = true;
            this.lb_commands.ItemHeight = 16;
            this.lb_commands.Location = new System.Drawing.Point(12, 76);
            this.lb_commands.Name = "lb_commands";
            this.lb_commands.Size = new System.Drawing.Size(239, 356);
            this.lb_commands.TabIndex = 0;
            this.lb_commands.SelectedIndexChanged += new System.EventHandler(this.lb_commands_SelectedIndexChanged);
            // 
            // rb_buttons
            // 
            this.rb_buttons.AutoSize = true;
            this.rb_buttons.Location = new System.Drawing.Point(6, 21);
            this.rb_buttons.Name = "rb_buttons";
            this.rb_buttons.Size = new System.Drawing.Size(77, 21);
            this.rb_buttons.TabIndex = 1;
            this.rb_buttons.TabStop = true;
            this.rb_buttons.Text = "Buttons";
            this.rb_buttons.UseVisualStyleBackColor = true;
            this.rb_buttons.CheckedChanged += new System.EventHandler(this.rb_buttons_CheckedChanged);
            // 
            // rb_center
            // 
            this.rb_center.AutoSize = true;
            this.rb_center.Location = new System.Drawing.Point(122, 21);
            this.rb_center.Name = "rb_center";
            this.rb_center.Size = new System.Drawing.Size(71, 21);
            this.rb_center.TabIndex = 2;
            this.rb_center.TabStop = true;
            this.rb_center.Text = "Center";
            this.rb_center.UseVisualStyleBackColor = true;
            this.rb_center.CheckedChanged += new System.EventHandler(this.rb_center_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rb_center);
            this.groupBox1.Controls.Add(this.rb_buttons);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 58);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Command Types";
            // 
            // btn_connect
            // 
            this.btn_connect.Location = new System.Drawing.Point(257, 24);
            this.btn_connect.Name = "btn_connect";
            this.btn_connect.Size = new System.Drawing.Size(86, 38);
            this.btn_connect.TabIndex = 4;
            this.btn_connect.Text = "Connect";
            this.btn_connect.UseVisualStyleBackColor = true;
            this.btn_connect.Click += new System.EventHandler(this.btn_connect_Click);
            // 
            // btn_send
            // 
            this.btn_send.Enabled = false;
            this.btn_send.Location = new System.Drawing.Point(523, 24);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(86, 38);
            this.btn_send.TabIndex = 5;
            this.btn_send.Text = "Send ";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // flp_boxes
            // 
            this.flp_boxes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.flp_boxes.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flp_boxes.Location = new System.Drawing.Point(257, 76);
            this.flp_boxes.Name = "flp_boxes";
            this.flp_boxes.Size = new System.Drawing.Size(352, 355);
            this.flp_boxes.TabIndex = 7;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(633, 450);
            this.Controls.Add(this.flp_boxes);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.btn_connect);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lb_commands);
            this.Name = "Form1";
            this.Text = "NetCommandSender";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lb_commands;
        private System.Windows.Forms.RadioButton rb_buttons;
        private System.Windows.Forms.RadioButton rb_center;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_connect;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.FlowLayoutPanel flp_boxes;
    }
}

