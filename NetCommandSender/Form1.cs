using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworkLibrary.NetworkPackage.Commands;
using NetworkLibrary;
namespace NetCommandSender
{
    public partial class Form1 : Form
    {
        Writer writer;
        public Form1()
        {
            InitializeComponent();
        }

        private void rb_buttons_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_buttons.Checked)
            {
                lb_commands.Items.Clear();
                foreach(var com in CommandInformations.ButtonParameterCount)
                {
                    lb_commands.Items.Add(com.Key);
                }
            }
        }

        private void rb_center_CheckedChanged(object sender, EventArgs e)
        {
            if (rb_center.Checked)
            {
                lb_commands.Items.Clear();
            }
        }

        private void lb_commands_SelectedIndexChanged(object sender, EventArgs e)
        {
            object key = lb_commands.SelectedItem;
            int parametercount = 0;
            flp_boxes.Controls.Clear();
            if (key.GetType() == typeof(Commands_Button))
            {
                parametercount = CommandInformations.ButtonParameterCount[(Commands_Button)key].Length;
            }
            
            for(int i = 0; i < parametercount; i++)
            {
                TextBox box = new TextBox();
                box.Width = 180;
                box.Text = CommandInformations.ButtonParameterCount[(Commands_Button)key][i];
                flp_boxes.Controls.Add(box);
            }
        }

        private void btn_connect_Click(object sender, EventArgs e)
        {
            try
            {
                writer = new Writer("127.0.0.1");
                btn_send.Enabled = true;
            }
            catch(Exception ex)
            {
                btn_send.Enabled = false;
                Console.WriteLine("Failed to connect: {0}", ex.Message);
            }
        }

        private void btn_send_Click(object sender, EventArgs e)
        {
            if (rb_buttons.Checked)
            {
                Commands_Button command = (Commands_Button)lb_commands.SelectedItem;
                List<string> parameters = new List<string>();
                foreach(var item in flp_boxes.Controls)
                {
                    parameters.Add(((TextBox)item).Text);
                }
                byte[] p = Package.Create(Command_Types.BUTTONS, command,parameters.ToArray());
                writer.write(p);
            }
            else
            {

            }
        }
    }
}
