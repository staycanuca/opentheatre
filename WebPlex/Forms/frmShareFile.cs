﻿using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace WebPlex
{
    public partial class frmShareFile : Form
    {
        public frmShareFile()
        {
            InitializeComponent();
        }

        public string infoFileURL;

        private void imgFacebook_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.facebook.com/sharer/sharer.php?u=" + infoFileURL);
            Close();
        }

        private void imgTwitter_Click(object sender, EventArgs e)
        {
            Process.Start("https://twitter.com/home?status=Check%20out%20this%20file%20I%20found%20on%20%40WebPlex%20" + infoFileURL);
            Close();
        }

        private void imgGooglePlus_Click(object sender, EventArgs e)
        {
            Process.Start("https://plus.google.com/share?url=" + infoFileURL);
            Close();
        }

        private void imgReddit_Click(object sender, EventArgs e)
        {
            Process.Start("http://reddit.com/submit?url=" + infoFileURL + "&title=" + Path.GetFileNameWithoutExtension(new Uri(infoFileURL).LocalPath) + "%20%5BWebPlex%5D");
            Close();
        }

        private void imgEmail_Click(object sender, EventArgs e)
        {
            Process.Start("mailto:?&body=Check%20out%20this%20awesome%20file%20I%20found%20on%20WebPlex%20-%20" + infoFileURL);
            Close();
        }

        private void imgClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmShareFile_Load(object sender, EventArgs e)
        {

        }
    }
}
