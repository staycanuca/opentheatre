﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.IO;

namespace WebPlex.CControls
{
    public partial class ctrlMovieDetails : UserControl
    {
        public ctrlMovieDetails()
        {
            InitializeComponent();

            panelFiles.Size = new Size(panelDetails.Size.Width, panelFiles.Size.Height);
            panelTorrents.Size = new Size(panelDetails.Size.Width, panelTorrents.Size.Height);
        }

        public string infoImdbId = "";
        public string infoImagePoster = "";
        public string infoFanartUrl = "";
        public string infoTrailerUrl = "";

        private void ctrlMovieDetails_Load(object sender, EventArgs e)
        {
            if (panelTorrents.Controls.Count == 0) { panelTorrents.Visible = false; panelTitleTorrents.Visible = false; }
            if (infoTrailerUrl == "") { btnWatchTrailer.Visible = false; }
            if (infoImagePoster == "") { imgPoster.Image = Utilities.ChangeOpacity(Properties.Resources.poster_default, 1); }
            if (infoFanartUrl == "") { BackgroundImage = Utilities.ChangeOpacity(Properties.Resources.background_original, 0.5F); }
            panelTitleFiles.Size = new Size(panelDetails.Size.Width, panelTitleFiles.Size.Height);
            panelTitleTorrents.Size = new Size(panelDetails.Size.Width, panelTitleTorrents.Size.Height);
            panelFiles.Size = new Size(panelDetails.Size.Width, panelFiles.Size.Height);

            foreach (Control ctrl in panelFiles.Controls)
            {
                ctrl.Size = new Size(panelDetails.Size.Width - 3, ctrl.Size.Height);
            }

            panelTorrents.Size = new Size(panelDetails.Size.Width, panelTorrents.Size.Height);

            foreach (Control ctrl in panelTorrents.Controls)
            {
                ctrl.Size = new Size(panelDetails.Size.Width - 3, ctrl.Size.Height);
            }

        }

        private void appClose_Click(object sender, EventArgs e)
        {
            frmWebPlex.form.tab.SelectedTab = frmWebPlex.form.currentTab;
            Parent.Controls.Clear();
        }

        private void imgIMDb_Click(object sender, EventArgs e)
        {
            Process.Start("www.imdb.com/title/" + infoImdbId);
        }

        private void ctrlDetails_SizeChanged(object sender, EventArgs e)
        {
            panelFiles.Size = new Size(panelDetails.Size.Width, panelFiles.Size.Height);

            foreach (Control ctrl in panelFiles.Controls)
            {
                ctrl.Size = new Size(panelFiles.Size.Width - 5, ctrl.Size.Height);
            }
        }

        private void btnWatchTrailer_ClickButtonArea(object Sender, MouseEventArgs e)
        {
            Process.Start(infoTrailerUrl);
        }

        private void imgSearchForMore_Click(object sender, EventArgs e)
        {
            string noSymbolsTitle = Regex.Replace(infoTitle.Text, "[^A-Za-z0-9 _]", " ");
            string ifYearExists = ""; if (infoYear.Text != "Year") { ifYearExists = " " + infoYear.Text; }
            frmWebPlex.form.txtSearchFiles.Text = noSymbolsTitle + ifYearExists;
            frmWebPlex.form.showFiles(frmWebPlex.selectedFiles);
            frmWebPlex.form.tab.SelectedTab = frmWebPlex.form.tabFiles;
            Parent.Controls.Clear();
        }

        public void AddStream(string URL, bool isLocal, bool isTorrent, Panel toPanel, string torrentName = "", string quality = "")
        {
            try
            {
                ctrlStreamInfo ctrlInfo = new ctrlStreamInfo
                {
                    infoFileURL = URL
                };

                if (isLocal == false && isTorrent == false)
                {
                    ctrlInfo.infoHost.Text = new Uri(URL).Host.Replace("www.", "");
                    ctrlInfo.infoName.Text = Path.GetFileName(new Uri(URL).LocalPath);
                    toPanel.Controls.Add(ctrlInfo);
                }
                else if (isLocal == true && isTorrent == false)
                {
                    ctrlInfo.infoHost.Text = new Uri(URL).Host.Replace("www.", "");
                    ctrlInfo.infoName.Text = Path.GetFileName(new Uri(URL).LocalPath);
                    ctrlInfo.isLocal = isLocal;
                    toPanel.Controls.Add(ctrlInfo);
                }
                else if (isLocal == false && isTorrent == true)
                {
                    if (torrentName == "YIFY")
                    {

                        //  Trackers : Public trackers for Magnets
                        string trackers = "&tr=" + "udp://open.demonii.com:1337/announce" + " &tr=" + "udp://tracker.openbittorrent.com:80" + "&tr=" + "udp://tracker.coppersurfer.tk:6969" + "&tr=" + "udp://glotorrents.pw:6969/announce" + "&tr=" + "udp://tracker.opentrackr.org:1337/announce" + "&tr=" + "udp://torrent.gresille.org:80/announce" + "&tr=" + "udp://p4p.arenabg.com:1337" + "&tr=" + "udp://tracker.leechers-paradise.org:6969";

                        //  Magnet : magnet:?xt=urn:btih:TORRENT_HASH&dn=Url+Encoded+Movie+Name&tr=http://track.one:1234/announce&tr=udp://track.two:80
                        ctrlInfo.infoMagnet = "magnet:?xt=urn:btih:" + Path.GetFileName(URL) + "&dn=" + infoTitle.Text.Replace(" ", "+") + "%28" + infoYear.Text + "%29+%5B" + "720p" + "%5D+%5B" + "YTS.AG" + "%5D" + trackers;

                        ctrlInfo.isTorrent = true;
                        ctrlInfo.infoHost.Text = "YIFY";
                        ctrlInfo.infoName.Text = infoTitle.Text + " (" + infoYear.Text + ") [" + quality + "] [" + "YIFY" + "].torrent";
                        toPanel.Controls.Add(ctrlInfo);
                    }
                    else if (torrentName == "POPCORN")
                    {
                        ctrlInfo.imgDownload.Visible = false;
                        ctrlInfo.infoMagnet = URL;
                        ctrlInfo.isTorrent = true;
                        ctrlInfo.infoHost.Text = "Popcorn Time";
                        ctrlInfo.infoName.Text = infoTitle.Text + " (" + infoYear.Text + ") [" + quality + "] [" + "POPCORN TIME" + "].torrent";
                        toPanel.Controls.Add(ctrlInfo);
                    }
                }
            }
            catch { }
        }
    }
}
