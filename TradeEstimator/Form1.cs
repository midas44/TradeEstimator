using TradeEstimator.Charts;
using TradeEstimator.Conf;
using TradeEstimator.Log;
using TradeEstimator.Main;
using OpenTK.Audio.OpenAL;
using ScottPlot;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using Color = System.Drawing.Color;
using System;

namespace TradeEstimator
{
    public partial class Form1 : Form
    {
        public static Form1 form1 = null;

        public delegate void EnableDelegate(bool enable);


        public Config config;
        public Logger logger;

        public RunnerBase runnerBase;
        public Runner runner;

        int active_timerange_index;

        string active_timerange;

        public Chart1 chart1;

        double track_x;

        double track_y;

        int chosen_timepoint;

        string current_instrument;

        public bool noRun;

        public int chart_limit_x_scale;

        public Color backgroundColor1 = Color.FromArgb(24, 33, 49); //GreyNavy
        public Color backgroundColor2 = Color.FromArgb(13, 27, 62); //Navy

        public Color foregroundColor1 = Color.FromArgb(199, 214, 255); //Lilac
        public Color foregroundColor2 = Color.FromArgb(235, 228, 232); //Smoke
        public Color foregroundColor3 = Color.FromArgb(121, 184, 255); //Blue
        public Color foregroundColor4 = Color.FromArgb(204, 226, 255); //Sky
        public Color foregroundColor5 = Color.FromArgb(106, 115, 125); //Grey
        public Color foregroundColor6 = Color.FromArgb(169, 179, 214); //Silver

        public Color highlightColor1 = Color.FromArgb(113, 221, 244); //Aqua
        public Color highlightColor2 = Color.FromArgb(249, 117, 131); //Red
        public Color highlightColor3 = Color.FromArgb(179, 146, 240); //Violet
        public Color highlightColor4 = Color.FromArgb(246, 220, 189); //Sand

        public ScottPlot.Color backgroundColor1Hex; //GreyNavy
        public ScottPlot.Color backgroundColor2Hex; //Navy

        public ScottPlot.Color foregroundColor1Hex; //Lilac
        public ScottPlot.Color foregroundColor2Hex; //Smoke
        public ScottPlot.Color foregroundColor3Hex; //Blue
        public ScottPlot.Color foregroundColor4Hex; //Sky
        public ScottPlot.Color foregroundColor5Hex; //Grey
        public ScottPlot.Color foregroundColor6Hex; //Silver

        public ScottPlot.Color highlightColor1Hex; //Aqua
        public ScottPlot.Color highlightColor2Hex; //Red
        public ScottPlot.Color highlightColor3Hex; //Violet

        double indScale_ui;
        double zonesScale_ui;

        int tsetIndex;

        public int global_run_counter;

        public double chart1YRange;

        int activeIndicator;

        string activeIndicatorName;


        public Form1()
        {
            InitializeComponent();

            form1 = this;
            form1.StartPosition = FormStartPosition.Manual;
            form1.Location = new Point(0, 0);


            noRun = true;

            backgroundColor1Hex = getScottPlotColor(backgroundColor1);
            backgroundColor2Hex = getScottPlotColor(backgroundColor2);

            foregroundColor1Hex = getScottPlotColor(foregroundColor1);
            foregroundColor2Hex = getScottPlotColor(foregroundColor2);
            foregroundColor3Hex = getScottPlotColor(foregroundColor3);
            foregroundColor4Hex = getScottPlotColor(foregroundColor4);
            foregroundColor5Hex = getScottPlotColor(foregroundColor5);
            foregroundColor6Hex = getScottPlotColor(foregroundColor6);

            highlightColor1Hex = getScottPlotColor(highlightColor1);
            highlightColor2Hex = getScottPlotColor(highlightColor2);
            highlightColor3Hex = getScottPlotColor(highlightColor3);

            form1.BackColor = backgroundColor1;
            form1.ForeColor = foregroundColor1;

            textBox_log.BackColor = backgroundColor1;
            textBox_log.ForeColor = foregroundColor1;

            dateTimePicker1.CustomFormat = "yyyy.MM.dd (ddd)";
            dateTimePicker2.CustomFormat = "yyyy.MM.dd (ddd)";

            dateTimePicker1.CalendarMonthBackground = backgroundColor1;
            dateTimePicker1.CalendarForeColor = Color.FromArgb(199, 201, 228);

            dateTimePicker2.CalendarMonthBackground = backgroundColor1;
            dateTimePicker2.CalendarForeColor = Color.FromArgb(199, 201, 228);

            comboBox_instrument.BackColor = backgroundColor1;
            comboBox_instrument.ForeColor = foregroundColor1;


            comboBox_tr_model.BackColor = backgroundColor1;
            comboBox_tr_model.ForeColor = foregroundColor1;


            button_mode0.BackColor = backgroundColor1;
            button_mode0.ForeColor = foregroundColor1;

            button_mode1.BackColor = backgroundColor1;
            button_mode1.ForeColor = foregroundColor1;

            button_next.BackColor = backgroundColor1;
            button_next.ForeColor = foregroundColor1;

            button_prev.BackColor = backgroundColor1;
            button_prev.ForeColor = foregroundColor1;

            button_rnd.BackColor = backgroundColor1;
            button_rnd.ForeColor = foregroundColor1;

            button_open_dir.BackColor = backgroundColor1;
            button_open_dir.ForeColor = foregroundColor1;

            button_reset1.BackColor = backgroundColor1;
            button_reset1.ForeColor = foregroundColor1;
            button_reset2.BackColor = backgroundColor1;
            button_reset2.ForeColor = foregroundColor1;

            label1.ForeColor = foregroundColor4;
            label2.ForeColor = foregroundColor3;
            label3.ForeColor = highlightColor2;
            label4.ForeColor = highlightColor3;
            label5.ForeColor = highlightColor1;
            label6.ForeColor = highlightColor1;
            label7.ForeColor = highlightColor4;

            form1.Text = "TradeEstimator";

            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
            label8.Text = "";

            checkedListBox_period.BackColor = backgroundColor1;
            checkedListBox_period.ForeColor = foregroundColor1;


            progressBar_instr.Visible = false;
            progressBar_timepoint.Visible = false;

            track_x = 0;
            track_y = 0;

            chosen_timepoint = -1;

            panel_chart1.Visible = false;

            panel_chart1.Dock = DockStyle.Fill;

            plot1.Dock = DockStyle.Fill;

            chart_limit_x_scale = -1;

            tsetIndex = -1;

            global_run_counter = 0;

            chart1YRange = 3;

            Application.DoEvents();

            noRun = false;

            activeIndicator = 0;

            //only here! only once!
            //::::::::::::::::::::::::::::::::::
            globalStart();
            //::::::::::::::::::::::::::::::::::

            start();
        }


        private void globalStart()
        {
            config = new();
            logger = new(config);
            runnerBase = new(config, logger);
            Application.DoEvents();
        }


        private void start()
        {
            //isFinished = false;
            runner = new(runnerBase);
            Application.DoEvents();

            //runTrade();    //on button Up        
        }


        private ScottPlot.Color getScottPlotColor(Color clr)
        {
            return ScottPlot.Color.FromHex(clr.R.ToString("X2") + clr.G.ToString("X2") + clr.B.ToString("X2"));
        }


        public string get_path()
        {
            return textBox_path.Text;
        }


        public void set_path(string path)
        {
            textBox_path.Text = path;
        }


        public void setConfig(Config config)
        {
            this.config = config;
        }


        private void arrangePanels()
        {
            switch (config.ui_mode)
            {
                case 0:
                    arrangePanels0();
                    break;

                case 1:
                    arrangePanels1();
                    break;
            }
        }


        private void arrangePanels0()
        {
            panel0.Visible = false;
            panel1.Visible = false;
            panel3.Visible = false;
            textBox_log.Visible = false;

            panel_modes.Visible = true;
            panel_models.Visible = true;
            panel_run.Visible = true;


            panel_instr_time.Visible = false;
            panel_labels.Visible = false;
            panel_tset.Visible = false;
            panel_tr.Visible = false;


            int x1 = 0;
            int x2 = x1 + panel_modes.Width;
            int x3 = x2 + panel_models.Width;
            int x4 = x3 + panel_run.Width;

            int y1 = 0;
            int y2 = panel_modes.Height;

            panel3.Location = new Point(0, 0);
            panel3.Height = y2;

            panel_modes.Location = new Point(x1, y1);
            panel_models.Location = new Point(x2, y1);
            panel_run.Location = new Point(x3, y1);
            textBox_log.Location = new Point(x4, y1);

            panel3.Dock = DockStyle.Fill;

            panel1.Dock = DockStyle.Fill;
            textBox_log.Dock = DockStyle.Right;

            form1.WindowState = FormWindowState.Normal;
            form1.CenterToScreen();

            panel0.Visible = false;
            panel1.Visible = true;
            panel3.Visible = true;
            textBox_log.Visible = true;
        }


        private void arrangePanels1()
        {
            panel0.Visible = false;
            panel1.Visible = false;
            panel3.Visible = false;
            textBox_log.Visible = false;


            //panel1
            panel_modes.Visible = true;
            panel_models.Visible = true;
            panel_run.Visible = false;

            panel_instr_time.Visible = true;
            panel_labels.Visible = true;
            panel_tset.Visible = true;
            panel_tr.Visible = true;


            int left_pad = 60;


            //on panel1 subpanels
            int y1 = 0;
            int y2 = panel_modes.Height;
            int x1 = left_pad;
            int x2 = x1 + panel_modes.Width;
            int x3 = x2 + panel_models.Width;
            int x4 = x3 + panel_instr_time.Width;
            int x5 = x4 + panel_tset.Width;
            int x6 = x5 + panel_labels.Width;
            int x7 = x6 + panel_tr.Width;

            panel_modes.Location = new Point(x1, y1);
            panel_models.Location = new Point(x2, y1);
            panel_instr_time.Location = new Point(x3, y1);
            panel_tset.Location = new Point(x4, y1);
            panel_labels.Location = new Point(x5, y1);
            panel_tr.Location = new Point(x6, y1);

            //on panel2 subpanels

            int x8 = x1;

            //main panels
            int bottom_pad = 210;
            panel1.Height = y2;
            panel3.Height = y2 + bottom_pad;


            panel1.Dock = DockStyle.Fill;

            panel0.Location = new Point(0, 0);

            panel0.Dock = DockStyle.Top;
            panel3.Location = new Point(0, 0);
            panel3.Dock = DockStyle.Bottom;
            panel0.Dock = DockStyle.Fill;

            form1.CenterToScreen();
            form1.WindowState = FormWindowState.Maximized;

            //main
            panel0.Visible = true;
            panel1.Visible = true;
            panel3.Visible = true;
            textBox_log.Visible = false;
        }


        public void setupMode_0()
        {
            logger.log("setupMode_0", 1);

            form1.Location = new Point(config.form0_x, config.form0_y);
            form1.Width = config.form0_w;
            form1.Height = config.form0_h;
            //form1.WindowState = FormWindowState.Normal;

            arrangePanels();

            highlight_mode_button(config.ui_mode);

            setupModelControls(config);

            set_progressBar_timepoint_value(0);
            set_progressBar_instr_value(0);

            Application.DoEvents();
        }


        public void setupMode_1()
        {
            logger.log("setupMode_1", 1);

            form1.Location = new Point(config.form1_x, config.form1_y);
            form1.Width = config.form1_w;
            form1.Height = config.form1_h;
            //form1.WindowState = FormWindowState.Normal;
            //form1.WindowState = FormWindowState.Maximized;

            arrangePanels();

            highlight_mode_button(config.ui_mode);

            reset_labels();

            setupModelControls(config);

            if (runner != null)
            {
                setupTrControls(runner.trModel);
            }

            setupInstrTimeControls(config);

            setActiveTimerange();

            Application.DoEvents();
        }



        private void setActiveTimerange()
        {
            active_timerange_index = config.timerange_index;
            checkedListBox_period.SetItemChecked(active_timerange_index, true);
            active_timerange = checkedListBox_period.Items[active_timerange_index].ToString();
        }


        public void clear_terminal()
        {
            textBox_log.Clear();
        }


        public void output_terminal(string content)
        {
            textBox_log.AppendText(content);
        }


        public DateTime get_date1()
        {
            return dateTimePicker1.Value.Date;
        }


        public DateTime get_date2()
        {
            return dateTimePicker2.Value.Date;
        }


        public void set_date1(DateTime date1)
        {
            dateTimePicker1.Value = date1.Date;
        }


        public void set_date2(DateTime date2)
        {
            dateTimePicker2.Value = date2.Date;
        }


        public string get_combobox_instrument_text()
        {
            return comboBox_instrument.Text;
        }


        public void set_instrument(string instr)
        {
            comboBox_instrument.SelectedItem = instr;
        }


        public string get_combobox_tr_model_text()
        {
            return comboBox_tr_model.Text;
        }


        public string getActiveTimerange()
        {
            for (int i = 0; i <= (checkedListBox_period.Items.Count - 1); i++)
            {
                if (checkedListBox_period.GetItemChecked(i))
                {
                    active_timerange_index = i;
                }
            }
            config.timerange_index = active_timerange_index;
            active_timerange = checkedListBox_period.Items[active_timerange_index].ToString().Trim();

            label12.Text = " " + active_timerange;

            return active_timerange;
        }


        public void setupModelControls(Config config)
        {
            noRun = true;

            comboBox_instrument.Items.Clear();
            foreach (string instr in config.instruments)
            {
                comboBox_instrument.Items.Add(instr);
            }


            comboBox_tr_model.Items.Clear();
            foreach (string tr_model in config.trade_models)
            {
                comboBox_tr_model.Items.Add(tr_model);
            }

            comboBox_tr_model.SelectedItem = config.chosen_tr_model;


            noRun = false;

            Application.DoEvents();
        }



        public void setupInstrTimeControls(Config config)
        {
            noRun = true;

            dateTimePicker1.MinDate = config.date1;
            dateTimePicker1.MaxDate = config.date2;

            dateTimePicker2.MinDate = config.date1;
            dateTimePicker2.MaxDate = config.date2;

            //checkedListBox_period.SetItemChecked(config.timerange_index, true);
            setActiveTimerange(); //new

            set_instrument(config.chosen_instrument);
            set_date1(config.date1);
            set_date2(config.chosen_date);

            noRun = false;

            Application.DoEvents();
        }



        public void setupTrControls(TradeModel trModel)
        {

            if (trModel == null) { return; }

            noRun = true;

            //TO DO

            label8.Text = "trade_id = " + trModel.tradeId;

            noRun = false;

            Application.DoEvents();
        }


        public void setupAnControls()
        {


        }



        public void setupTsetUI(List<string> astroNumPoints)
        {
            if (runner != null)
            {
                noRun = true;

                comboBox_tset.Items.Clear();

                int i = -1;

                foreach (string s in astroNumPoints)
                {
                    i++;
                    comboBox_tset.Items.Add(s);
                }

                if (tsetIndex < 0 && i >= 0)
                {
                    tsetIndex = i;
                }
                comboBox_tset.SelectedIndex = tsetIndex;

                noRun = false;

                Application.DoEvents();
            }
        }


        private void checkedListBox_period_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < checkedListBox_period.Items.Count; ix++)
            {
                if (ix != e.Index)
                {
                    checkedListBox_period.SetItemChecked(ix, false);
                }
            }
        }


        private void button_goPrevPeriod_Click(object sender, EventArgs e)
        {

            if (runner != null)
            {
                if (noRun) { return; }

                runner.goPrevPeriod();
                runner.setTimingUI();
                runner.run();
            }
        }


        private void button_goNextPeriod_Click(object sender, EventArgs e)
        {

            if (runner != null)
            {
                if (noRun) { return; }

                runner.goNextPeriod();
                runner.setTimingUI();
                runner.run();
            }
        }


        private void button_goRandomPeriod_Click(object sender, EventArgs e)
        {

            if (runner != null)
            {
                if (noRun) { return; }

                noRun = true;
                runner.goRandomPeriod();
                runner.setTimingUI();
                runner.goRandomInstr();
                noRun = false;

                runner.run();

                //quick fix
                /*
            if (chart1 != null)
            {
                chart1.set_chart_limits();

                chart1.finalize();
            }
            */

            }
        }


        private void button_goFirstPeriod_Click(object sender, EventArgs e)
        {

            if (runner != null)
            {
                if (noRun) { return; }

                runner.goFirstPeriod();
                runner.setTimingUI();
                runner.run();
            }
        }

        private void button_goLastPeriod_Click(object sender, EventArgs e)
        {

            if (runner != null)
            {
                if (noRun) { return; }

                runner.goLastPeriod();
                runner.setTimingUI();
                runner.run();
            }
        }


        public void show_panelB()
        {
            panel_chart1.Visible = true;
        }


        public void set_chart1(Chart1 chart1)
        {
            this.chart1 = chart1;
        }


        public Chart1 get_chart1()
        {
            return this.chart1;
        }



        private void plot1_MouseMove(object sender, MouseEventArgs e)
        {
            if (noRun) { return; }

            if (chart1 != null)
            {
                chart1.set_chart_limits_y();
            }

            Pixel p = new(e.Location.X, e.Location.Y);
            Coordinates c = Form1.form1.plot1.Plot.GetCoordinates(p);

            double x = c.X;
            double y = c.Y;

            int xx = (int)Math.Ceiling(x);

            if (chart1 != null)
            {
                if (xx >= 0 && xx < chart1.timeline.Length)
                {
                    string t = chart1.timeline[xx].ToString("MMM dd yyyy (ddd)  HH:mm");
                    string info = current_instrument + " " + t; //TODO: get instr format                   
                    label1.Text = info;

                    label3.Text = y.ToString(chart1.instr_price_format);
                    //chart1.track(x, y);
                    label7.Text = "O: " + chart1.Open[xx].ToString(chart1.instr_price_format) +
                                  "  H: " + chart1.High[xx].ToString(chart1.instr_price_format) +
                                  "  L: " + chart1.Low[xx].ToString(chart1.instr_price_format) +
                                  "  C: " + chart1.Close[xx].ToString(chart1.instr_price_format);
                    // " V: " + ((int)Math.Round(chart1.Volume[xx])).ToString();
                }
            }

            track_x = x;
            track_y = y;
        }



        private void plot1_DoubleClick(object sender, EventArgs e)
        {
            if (noRun) { return; }

            chart1.set_chart_limits();

            chart1.finalize();
        }



        private void plot1_MouseUp(object sender, MouseEventArgs e) //new
        {
            if (noRun) { return; }

            double x = 0;
            double y = 0;

            x = track_x;
            y = track_y;

            int xx = (int)Math.Ceiling(x);

            if (chart1 != null)
            {
                int n = chart1.timeline.Length;

                if (xx >= 0 && xx < n)
                {
                    chosen_timepoint = xx;

                    string t = chart1.timeline[xx].ToString("MMM dd yyyy (ddd)  HH:mm");
                    string info = current_instrument + " " + t;

                    label2.Text = info;

                    label4.Text = "ADR: " + ((int)Math.Round(chart1.ADR[xx] / chart1.instr_tick)).ToString() + " (" + chart1.ADR[xx].ToString(chart1.instr_price_format) + ")";

                    label5.Text = "O: " + chart1.Open[xx].ToString(chart1.instr_price_format) +
                                  "  H: " + chart1.High[xx].ToString(chart1.instr_price_format) +
                                  "  L: " + chart1.Low[xx].ToString(chart1.instr_price_format) +
                                  "  C: " + chart1.Close[xx].ToString(chart1.instr_price_format);
                    //" V: " + ((int)Math.Round(chart.Volume[xx])).ToString();

                    chart1.setMark(x, y);
                }
            }

            runner.tradesRun();
        }



        public double getChart1MarkX()
        {
            double x = -1;

            if (chart1 != null)
            {
                if (chosen_timepoint > 0)
                {
                    x = chosen_timepoint;
                }
            }
            return x;
        }


        public void reset_labels()
        {
            label1.Text = "";
            label2.Text = "";
            label3.Text = "";
            label4.Text = "";
            label5.Text = "";
            label6.Text = "";
            label7.Text = "";
        }



        public int get_chosen_timepoint()
        {
            if (chosen_timepoint < 0) { return 1; }

            return chosen_timepoint;
        }


        public void reset_chosen_timepoint()
        {

            chosen_timepoint = -1;

            track_x = 0;
            track_y = 0;

            reset_labels();
        }


        public void highlight_mode_button(int mode_number)
        {

            if (mode_number == 1)
            {

                button_mode1.BackColor = backgroundColor1;
                button_mode1.ForeColor = highlightColor2;

                button_mode0.BackColor = backgroundColor1;
                button_mode0.ForeColor = highlightColor1;
            }

            if (mode_number == 0)
            {
                button_mode0.BackColor = backgroundColor1;
                button_mode0.ForeColor = highlightColor2;

                button_mode1.BackColor = backgroundColor1;
                button_mode1.ForeColor = highlightColor1;
            }

            Application.DoEvents();
        }



        public void set_chart1_scale_x()
        {
            if (chart1 != null)
            {
                chart1.set_chart_limits_x();
            }
        }

        public void set_chart1_scale_y()
        {
            if (chart1 != null)
            {
                chart1.set_chart_limits_y();
            }
        }

        public void set_chart1_scale_xy()
        {
            if (chart1 != null)
            {
                chart1.set_chart_limits_x();
                chart1.set_chart_limits_y();
            }
        }



        private void button_open_dir_Click(object sender, EventArgs e)
        {
            if (Directory.Exists(textBox_path.Text))
            {
                Process.Start("explorer.exe", @textBox_path.Text.Replace("/", "\\"));
            }

        }


        public void set_progressBar_timepoint_maximum(int value)
        {
            progressBar_timepoint.Visible = true;
            progressBar_timepoint.Maximum = value;
            Application.DoEvents();
        }


        public void set_progressBar_timepoint_value(int value)
        {
            progressBar_timepoint.Value = value;
            Application.DoEvents();
        }


        public void set_progressBar_instr_maximum(int value)
        {
            progressBar_instr.Visible = true;
            progressBar_instr.Maximum = value;
            Application.DoEvents();
        }


        public void set_progressBar_instr_value(int value)
        {
            progressBar_instr.Value = value;
            Application.DoEvents();
        }



        private void button_tsetSave_Click(object sender, EventArgs e)
        {
            //runner.tset.clear();  //TO DO: make option to clear

            int index = comboBox_tset.SelectedIndex;

            if (index == -1) { return; }

            string s = "";
            s += comboBox_instrument.Text.Trim() + ",";
            s += dateTimePicker1.Value.ToString(config.date_format) + ",";
            s += dateTimePicker2.Value.ToString(config.date_format) + ",";


            runner.tset.saveRecord(index, s);
            tsetIndex = index;
            runner.setupTset();
        }


        private void button_tsetAdd_Click(object sender, EventArgs e)
        {
            int index = runner.tset.lastIndex + 1;

            string s = "";
            s += comboBox_instrument.Text.Trim() + ",";
            s += dateTimePicker1.Value.ToString(config.date_format) + ",";
            s += dateTimePicker2.Value.ToString(config.date_format) + ",";


            runner.tset.addRecord(s);
            tsetIndex = runner.tset.lastIndex;
            runner.setupTset();
        }


        private void button_tsetDel_Click(object sender, EventArgs e)
        {
            noRun = true;

            int index = comboBox_tset.SelectedIndex;

            if (index == -1) { return; }

            comboBox_tset.Items.RemoveAt(index);

            tsetIndex = comboBox_tset.SelectedIndex;

            List<string> items = new List<string>();

            foreach (string s in comboBox_tset.Items)
            {
                items.Add(s);
            }

            runner.tset.saveAll(items);
            runner.setupTset();


            noRun = false;
            Application.DoEvents();
        }


        private void loadTset()
        {

            tsetIndex = comboBox_tset.SelectedIndex;

            string s = comboBox_tset.Text.Trim();

            if (s.Length > 10)
            {
                noRun = true;

                string[] sss = s.Split(',');

                int i = 0;
                foreach (string part in sss)
                {
                    if (i == 0)
                    {
                        comboBox_instrument.SelectedItem = part;
                    }

                    if (i == 1)
                    {
                        dateTimePicker1.Value = DateTime.ParseExact(part.Trim(), config.date_format, System.Globalization.CultureInfo.InvariantCulture);
                    }

                    if (i == 2)
                    {
                        dateTimePicker2.Value = DateTime.ParseExact(part.Trim(), config.date_format, System.Globalization.CultureInfo.InvariantCulture);
                    }


                    i++;
                }

                noRun = false;

                Application.DoEvents();

                if (runner != null)
                {
                    runner.run();
                }
            }
        }


        private void comboBox_tset_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (noRun) { return; }
            //if (no_tset_load) { return; }

            tsetIndex = comboBox_tset.SelectedIndex;

            loadTset(); //Activate?

            //tsetIndex = comboBox_tset.SelectedIndex;
        }



        private void button_tsetPrev_Click(object sender, EventArgs e)
        {
            tsetIndex = comboBox_tset.SelectedIndex;

            if (tsetIndex == 0)
            {
                tsetIndex = comboBox_tset.Items.Count - 1;
            }
            else
            {
                tsetIndex--;
            }

            comboBox_tset.SelectedIndex = tsetIndex;
            loadTset();
        }


        private void button_tsetNext_Click(object sender, EventArgs e)
        {
            tsetIndex = comboBox_tset.SelectedIndex;

            if (tsetIndex == comboBox_tset.Items.Count - 1)
            {
                tsetIndex = 0;
            }
            else
            {
                tsetIndex++;
            }

            comboBox_tset.SelectedIndex = tsetIndex;
            loadTset();
        }


        private void button_loadTset_Click(object sender, EventArgs e)
        {
            loadTset();
        }


        public void showMark2()
        {
            double x = track_x;
            double y = track_y;
            int xx = (int)Math.Ceiling(x);

            if (chart1 != null)
            {
                int n = chart1.timeline.Length;

                if (xx >= 0 && xx < n)
                {
                    chosen_timepoint = xx;

                    string t = chart1.timeline[xx].ToString("MMM dd yyyy (ddd)  HH:mm");
                    string info = current_instrument + " " + t;
                    label2.Text = info;


                    label4.Text = "ADR: " + ((int)Math.Round(chart1.ADR[xx] / chart1.instr_tick)).ToString() + " (" + chart1.ADR[xx].ToString(chart1.instr_price_format) + ")";



                }
            }
        }


        private void dateTimePicker2_CloseUp(object sender, EventArgs e)
        {
            if (noRun) { return; }

            runner.setTimingUI();
            runner.run();
        }


        private void button_mode1_Click(object sender, EventArgs e)
        {
            config.ui_mode = 1;
            start();
        }


        private void button_mode0_Click(object sender, EventArgs e)
        {
            config.ui_mode = 0;
            start();
        }


        private void comboBox_tr_model_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (runner == null) { return; }
            if (noRun) { return; }
            config.chosen_tr_model = comboBox_tr_model.Text;
            setupTrControls(runner.createTrModel());
            runner.tradesRun();
        }


        private void comboBox_instrument_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (runner == null) { return; }
            if (noRun) { return; }
            config.chosen_instrument = comboBox_instrument.Text;
            runner.run();
        }


        private void checkedListBox_period_MouseUp(object sender, MouseEventArgs e)
        {
            if (runner != null)
            {
                if (noRun) { return; }

                runner.setTimingUI();
                runner.run();
            }
        }


        private void chart1_autorescale_y()
        {
            if (chart1 != null)
            {
                chart1.set_chart_limits_y();
            }
        }


        private void plot1_Move(object sender, EventArgs e)
        {
            chart1_autorescale_y();
        }


        private void runTrade()
        {
            if (runner == null) { return; }
            if (noRun) { return; }
            runner.tradesRun();
        }

        private void button_rescale_time_Click(object sender, EventArgs e)
        {

            if (noRun) { return; }


            if (chart1 != null)
            {
                chart1.autoscale_x();

                chart1.finalize();
            }
        }

        private void button_rescale_price_Click(object sender, EventArgs e)
        {
            if (noRun) { return; }


            if (chart1 != null)
            {
                //chart1.set_chart_limits_y();

                chart1.autoscale_y();

                chart1.finalize();
            }
        }

        private void button_rescale_all_Click(object sender, EventArgs e)
        {
            if (noRun) { return; }


            if (chart1 != null)
            {
                chart1.set_chart_limits();

                chart1.finalize();
            }
        }

        private void buttonTrade_Click(object sender, EventArgs e)
        {
            runTrade();
        }
    }

}