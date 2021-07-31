using Sesak.SimulationObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace Sesak.Simulation
{
    public partial class FormPlot : DockContent
    {
        bool play = false;


        FramePlotter plotter = new FramePlotter();

        
        public bool DrawHeatMap
        {
            get { return plotter.DrawHeatMap; }
            set
            {
                plotter.DrawHeatMap = value;
                pbMain.Invalidate();
            }
        }

        float SizeMultiplier = 15;
        public static Dictionary<int, FormPlot> Instances = new Dictionary<int, FormPlot>();

        SimulationInstance simInstance = null;
        public List<OldAgent> agentSnapshot = new List<OldAgent>();
        object objLock = new object();

        List<double> frameTimes = new List<double>();
        List<FrameData> frames = new List<FrameData>();

        public FrameData OptimizerFrame = null;
        bool drawing = false;

        Stopwatch playbackTiming = new Stopwatch();

        int currentFrame = -1;
        bool optimizerMode = false;
        double seekOffset = 0;

        public void Reset()
        {
            playbackTiming.Reset();
            frameTimes.Clear();
            frames.Clear();
            agentSnapshot.Clear();
            currentFrame = -1;
            seekOffset = 0;
            SizeMultiplier = 15;
            play = false;
        }
        public FormPlot(int index = 0,bool optimizerMode = false)
        {
            InitializeComponent();
            if (Instances.ContainsKey(index))
            {
                if (Instances[index] != null && !Instances[index].IsDisposed)
                {
                    Instances[index].Close();
                }
                Instances[index] = this;
            }
            else
            {
                Instances.Add(index, this);
            }

            this.optimizerMode = optimizerMode;

            this.Text = "Plot [" + index.ToString() + "]";
            UpdateViewMode();
        }

        private void UpdateViewMode()
        {
            menuStrip1.Visible = !optimizerMode;
            panelControl.Visible = !optimizerMode;

        }
        public void SetSimulationInstance(SimulationInstance simInstance)
        {
            if (this.simInstance != null)
                simInstance.OnDrawFrame -= SimInstance_OnDrawFrame;

            this.simInstance = simInstance;
            simInstance.OnDrawFrame += SimInstance_OnDrawFrame;

            plotter.simInstance = simInstance;
            

        }


        private void SimInstance_OnDrawFrame(object sender, OnDrawFrameEventArgs e)
        {
            if (closing)
                return;
            if(optimizerMode)
            {
                if (!drawing)
                {
                    OptimizerFrame = e.Data;
                    MethodInvoker mi2 = new MethodInvoker(delegate ()
                    {
                        if (!tmrOptimizerUpdateFrame.Enabled)
                            tmrOptimizerUpdateFrame.Enabled = true;

                    });
                    this.Invoke(mi2);
                }
                return;
            }
            
            MethodInvoker mi = new MethodInvoker(delegate () 
            {
                lock (objLock)
                {
                    AddFrame(e.Data);
                    
                }
                //pbMain.Invalidate(); 
                if (currentFrame == -1)
                    btnPlay_Click(null, null);

                UpdateFrameIndex();

                
            });
            if(!this.IsDisposed)
                this.Invoke(mi);
            
        }

        private void AddFrame(FrameData data)
        {
            frames.Add(data);
            frameTimes.Add(data.Runtime);
            trkPlayback.Maximum = frames.Count - 1;

        }
        /*
        private void SimInstance_OnDrawFrame(object sender, EventArgs e)
        {
            if (closing)
                return;

            MethodInvoker mi = new MethodInvoker(delegate ()
            {
                lock (objLock)
                {
                    agentSnapshot.Clear();
                    foreach (OldAgent a in simInstance.ActiveAgent)
                    {
                        agentSnapshot.Add(a);
                    }
                }
                tickTime = simInstance.TickProcessTime;
                tmrFrame.Enabled = true;//pbMain.Invalidate();
            });
            try
            {
                if (this.IsDisposed)
                    return;

                this.Invoke(mi);
            }
            catch { }
            //throw new NotImplementedException();
        }
        */
        private void FormPlot_Load(object sender, EventArgs e)
        {
            plotter.PanOffset = new PointF(-pbMain.Width / 2 / plotter.Scale, -pbMain.Height / 2 / plotter.Scale);
            plotter.ComfortTestZone = SimulationInstance.ComfortTestZone;
            
            this.MouseWheel += FormPlot_MouseWheel;
            
        }
        public void ForceRedraw()
        {
            pbMain.Invalidate();
        }
        public void ExportImageSequence()
        {
            btnStop_Click(btnStop, EventArgs.Empty);

            try
            {
                string path;
                using (FolderBrowserDialog dlg = new FolderBrowserDialog())
                {

                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;

                    path = dlg.SelectedPath;
                }

                FormFrameExporter exporter = new FormFrameExporter(plotter, frames.ToArray(), path, pbMain.Size, false);
                exporter.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void ExportVideo()
        {
            btnStop_Click(btnStop, EventArgs.Empty);
            try
            {
                string path;
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "MP4|*.mp4|All Files|*";
                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;

                    path = dlg.FileName;
                }

                FormFrameExporter exporter = new FormFrameExporter(plotter, frames.ToArray(), path, pbMain.Size, true);
                exporter.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void FormPlot_MouseWheel(object sender, MouseEventArgs e)
        {
            float scalePrev = plotter.Scale;
            if (e.Delta > 0)
                plotter.Scale += (int)(Math.Ceiling(plotter.Scale / 10));
            else if (e.Delta < 0)
                plotter.Scale -= (int)(Math.Ceiling(plotter.Scale / 10));

            if(scalePrev != plotter.Scale)
                pbMain.Invalidate();

        }

        private void pbMain_Paint(object sender, PaintEventArgs e)
        {
            if (closing)
                return;
            drawing = true;
            Graphics g = e.Graphics;
            FrameData data;

            if (optimizerMode)
            {
                data = OptimizerFrame;
            }
            else
            {
                if (currentFrame < 0 || currentFrame >= frames.Count)
                    data = null;
                else
                {
                    lock (objLock)
                    {
                        data = frames[currentFrame];
                    }
                }
            }



            plotter.Draw(g, data, new Rectangle(new Point(), pbMain.Size));
            drawing = false;
        }

        private void tmrFrame_Tick(object sender, EventArgs e)
        {
            if (closing)
            {
                tmrFrame.Enabled = false;
                return;
            }

            UpdateFrameIndex();
        }
        private void UpdateFrameIndex()
        {
            if (frames.Count <= 0)
                return;

            int check = currentFrame;
            if (currentFrame < 0 || frameTimes[currentFrame] > playbackTiming.Elapsed.TotalSeconds + seekOffset)
                check = 0;

            int n = 0;
            for (int i = check; i < frameTimes.Count; i++)
            {
                n = i;
                if (frameTimes[i] > playbackTiming.Elapsed.TotalSeconds + seekOffset)
                    break;
            }
            if (n != currentFrame)
            {
                currentFrame = n;
                trkPlayback.Value = currentFrame;
                pbMain.Invalidate();
            }

        }


        bool closing = false;

        private void FormPlot_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(tmrDelayClose.Enabled)
            {
                return;
            }

            tmrFrame.Enabled = false;
            closing = true;

            if (this.simInstance != null)
                simInstance.OnDrawFrame -= SimInstance_OnDrawFrame;

            e.Cancel = true;
            this.Visible = false;
            tmrDelayClose.Enabled = true;
        }

        private void tmrDelayClose_Tick(object sender, EventArgs e)
        {
            this.Close();
        }

        private void trkPlayback_Scroll(object sender, EventArgs e)
        {
            if(!simInstance.IsStarted)
            {
                currentFrame = trkPlayback.Value;
                if (currentFrame >= 0 && currentFrame < frameTimes.Count)
                    seekOffset = frameTimes[currentFrame];
                else
                    seekOffset = 0;

                if (play)
                    btnPlay_Click(null, null);

                playbackTiming.Reset();


                pbMain.Invalidate();
            }
            
        }

        private void pbMain_MouseUp(object sender, MouseEventArgs e)
        {

        }

        PointF panScreenAnchor = new PointF();
        PointF panCanvasAnchor = new PointF();
        private void pbMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                panScreenAnchor = new PointF(e.X, e.Y);
                panCanvasAnchor = plotter.PanOffset;
            }
        }

        private void pbMain_Move(object sender, EventArgs e)
        {

        }

        private void pbMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right) //panning
            {
                float xDelta = e.X - panScreenAnchor.X;
                float yDelta = e.Y - panScreenAnchor.Y;

                float newPanX = xDelta / plotter.Scale + panCanvasAnchor.X;
                float newPanY = panCanvasAnchor.Y - yDelta / plotter.Scale;

                plotter.PanOffset = new PointF(newPanX, newPanY);
                pbMain.Invalidate();
            }
        }

        private void btnPlay_Click(object sender, EventArgs e)
        {
            play = !play;
            if (play)
            {
                if(!playbackTiming.IsRunning)
                    playbackTiming.Start();

                btnPlay.Image = global::Sesak.Properties.Resources.pause;
            }
            else
            {
                if (playbackTiming.IsRunning)
                    playbackTiming.Stop();
                btnPlay.Image = global::Sesak.Properties.Resources.play;
            }

            tmrFrame.Enabled = play;
            

        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            play = false;
            btnPlay.Image = global::Sesak.Properties.Resources.play;
            tmrFrame.Enabled = false;
            playbackTiming.Reset();
            trkPlayback.Value = trkPlayback.Minimum;
            currentFrame = -1;
            seekOffset = 0;
            

            pbMain.Invalidate();


        }

        private void heatMapToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void pbMain_SizeChanged(object sender, EventArgs e)
        {
            pbMain.Invalidate();
        }

        private void tmrOptimizerUpdateFrame_Tick(object sender, EventArgs e)
        {
            pbMain.Invalidate();
            tmrOptimizerUpdateFrame.Enabled = false;
        }

        private void x480ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setCanvasSize(640, 480);
        }

        private void x600ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setCanvasSize(800, 600);
            
        }
        private void setCanvasSize(int w,int h)
        {
            this.Width = w + 16;
            this.Height = h + 138;
        }

        private void x720ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setCanvasSize(1280, 720);
        }

        private void x1080ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            setCanvasSize(1920, 1080);
        }

        private void imageSequenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnStop_Click(btnStop, EventArgs.Empty);

            try
            {
                string path;
                using (FolderBrowserDialog  dlg = new FolderBrowserDialog())
                {
                    
                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;

                    path = dlg.SelectedPath;
                }

                FormFrameExporter exporter = new FormFrameExporter(plotter, frames.ToArray(), path, pbMain.Size, false);
                exporter.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void videoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            btnStop_Click(btnStop, EventArgs.Empty);
            try
            {
                string path;
                using (SaveFileDialog dlg = new SaveFileDialog())
                {
                    dlg.Filter = "MP4|*.mp4|All Files|*";
                    if (dlg.ShowDialog() != DialogResult.OK)
                        return;

                    path = dlg.FileName;
                }

                FormFrameExporter exporter = new FormFrameExporter(plotter,frames.ToArray(),path,pbMain.Size,true);
                exporter.ShowDialog();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void FormPlot_Resize(object sender, EventArgs e)
        {
            lblViewportSize.Text = pbMain.Width.ToString() + "x" + pbMain.Height.ToString();
        }
    }
}
