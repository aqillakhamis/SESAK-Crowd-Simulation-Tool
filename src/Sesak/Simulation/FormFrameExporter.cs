using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sesak.Simulation
{
    public partial class FormFrameExporter : Form
    {
        FramePlotter plotter;
        FrameData[] frames = null;
        Size targetSize;
        string targetPath;
        string targetVideo;

        bool exportVideo;
        bool cancelFlag = false;

        int currentFrame = 0;
        string ffmpegPath = "";

        public FormFrameExporter(FramePlotter plotter, FrameData[] frames,string targetPath,Size targetSize,bool exportVideo)
        {
            InitializeComponent();
            this.plotter = plotter; //should use own plotter
            this.frames = frames;
            this.exportVideo = exportVideo;

            if (exportVideo)
            {
                this.targetVideo = targetPath;
                this.targetPath = createTempPath();
                
            }
            else
            {
                this.targetPath = targetPath;
            }
            this.targetSize = targetSize;
        }

        private bool LoadFfmpegPath()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string j = System.IO.Path.Combine(s, "Sesak", "cfg.json");
            string jcfg = "";
            Dictionary<string, string> cfg = null;
            try
            {
                if (File.Exists(j))
                {
                    jcfg = File.ReadAllText(j);
                }
                cfg = JsonConvert.DeserializeObject<Dictionary<string, string>>(jcfg);
                if (cfg != null)
                {
                    if (cfg.ContainsKey("ffmpeg_path"))
                    {
                        ffmpegPath = cfg["ffmpeg_path"];
                    }

                    if (!File.Exists(ffmpegPath))
                    {
                        ffmpegPath = "";
                    }
                }
                else
                {
                    ffmpegPath = "";
                }
            }
            catch { }

            if(ffmpegPath.Length <= 0)
            {
                using(OpenFileDialog dlg = new  OpenFileDialog())
                {
                    dlg.Title = "Locate ffmpeg.exe";
                    dlg.Filter = "ffmpeg.exe|ffmpeg.exe|All Files|*";
                    if (dlg.ShowDialog() != DialogResult.OK)
                        return false;

                    if (cfg == null)
                        cfg = new Dictionary<string, string>();

                    if (cfg.ContainsKey("ffmpeg_path"))
                        cfg["ffmpeg_path"] = dlg.FileName;
                    else
                        cfg.Add("ffmpeg_path", dlg.FileName);

                    jcfg = JsonConvert.SerializeObject(cfg);
                    try
                    {
                        File.WriteAllText(j, jcfg);
                    }
                    catch { }

                    ffmpegPath = dlg.FileName;
                }
            }

            return true;
            
        }
        
        private string createTempPath()
        {
            string s = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            string result = System.IO.Path.Combine(s, "Sesak", "tmp");
            try
            {
                System.IO.Directory.Delete(result, true);
            }
            catch
            {

            }
            System.IO.Directory.CreateDirectory(result);
            
            return result;
        }

        private void FormFrameExporter_Load(object sender, EventArgs e)
        {
            if (frames == null)
                return;

            lblProgress.Text = "0/" + frames.Length.ToString();
            exportFrame();
        }

        private void exportFrame()
        {
            cancelFlag = false;

            if(exportVideo)
            {
                if (!LoadFfmpegPath())
                {
                    this.Close();
                    return;
                }

            }

            bgWorker.RunWorkerAsync();

            

        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i < frames.Length; i++)
            {
                currentFrame = i;

                Bitmap bmp = new Bitmap(targetSize.Width, targetSize.Height);

                Graphics g = Graphics.FromImage(bmp);
                FrameData data;

                data = frames[i];


                plotter.Draw(g, data, new Rectangle(new Point(), bmp.Size), true);
                string imgfn = i.ToString("000000") + ".png"; //should use dynamic leading zero length
                string fullPath = System.IO.Path.Combine(targetPath, imgfn);

                bmp.Save(fullPath, ImageFormat.Png);
                bmp.Dispose();


                bgWorker.ReportProgress(i * 100 / frames.Length);
                if (cancelFlag)
                    return;
            }

            


            if (!cancelFlag && exportVideo)
            {
                try
                {
                    ProcessStartInfo psi = new ProcessStartInfo();
                    string arg = " -r 30 -f image2 -i \"" + targetPath + "\\%6d.png" + "\" -vcodec libx264 -crf 20 -pix_fmt yuv420p \"" + targetVideo + "\"";
                    psi.FileName = ffmpegPath;
                    psi.Arguments =  arg;
                    psi.WindowStyle = ProcessWindowStyle.Normal;
                    psi.UseShellExecute = true;
                    Process.Start(psi);
                }
                catch(Exception ex)
                {

                }
            }
            
        }

        private void bgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progBar.Value = e.ProgressPercentage;
            lblProgress.Text = currentFrame.ToString() + "/" + frames.Length.ToString();
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            progBar.Value = 100;
            lblProgress.Text = frames.Length.ToString() + "/" + frames.Length.ToString();
            if(!cancelFlag)
                MessageBox.Show("Export Completed!", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            cancelFlag = true;
        }
    }
}
