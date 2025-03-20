using CmlLib.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CmlLib.Core.Auth;
using CmlLib.Core.ProcessBuilder;
using System.Reflection.Emit;
using System.Web.UI.WebControls;

namespace EzLauncher__MC_
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void btnlaunch_Click_1(object sender, EventArgs e)
        {
            guna2Panel2.Visible = true;
            guna2Panel2.Location = new Point(95, 74);

            var path = new MinecraftPath("MC");
            var launcher = new MinecraftLauncher(path);

            // Update Label3 with the file progress
            launcher.FileProgressChanged += (send, args) =>
            {
                string progressText = $"Name: {args.Name}\n" +
                                      $"Type: {args.EventType}\n" +
                                      $"Total: {args.TotalTasks}\n" +
                                      $"Progressed: {args.ProgressedTasks}";
                label2.Text = progressText; // Update Label3 with file progress
            };

            // Update Label3 with the byte progress
            launcher.ByteProgressChanged += (send, args) =>
            {
                string byteProgressText = $"{args.ProgressedBytes} bytes / {args.TotalBytes} bytes";
                label3.Text = byteProgressText; // Update Label3 with byte progress
            };

            await launcher.InstallAsync(versionstextbox.Text);

            var process = await launcher.BuildProcessAsync(versionstextbox.Text, new MLaunchOption
            {
                Session = MSession.CreateOfflineSession(usernametextbox.Text),
                MaximumRamMb = 4096
            });

            process.Start();
            guna2Panel2.Visible = false;
            guna2Panel2.Location = new Point(999, 999);
        }

    }
    }

