namespace TradeEstimator
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel0 = new Panel();
            panel_chart1 = new Panel();
            plot1 = new ScottPlot.WinForms.FormsPlot();
            panel3 = new Panel();
            panel1 = new Panel();
            panel_tr = new Panel();
            button_rescale = new Button();
            textBox_log = new TextBox();
            panel_tset = new Panel();
            button_tsetDel = new Button();
            button_tsetAdd = new Button();
            button_tsetSave = new Button();
            button_loadTset = new Button();
            button_tsetNext = new Button();
            button_tsetPrev = new Button();
            comboBox_tset = new ComboBox();
            panel_labels = new Panel();
            label7 = new Label();
            label5 = new Label();
            label12 = new Label();
            label6 = new Label();
            label4 = new Label();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            panel_models = new Panel();
            comboBox_tr_model = new ComboBox();
            panel_run = new Panel();
            buttonRun = new Button();
            progressBar_instr = new ProgressBar();
            progressBar_timepoint = new ProgressBar();
            button_open_dir = new Button();
            textBox_path = new TextBox();
            panel_modes = new Panel();
            button_mode1 = new Button();
            button_mode0 = new Button();
            panel_instr_time = new Panel();
            button_reset2 = new Button();
            button_reset1 = new Button();
            button_rnd = new Button();
            button_next = new Button();
            button_prev = new Button();
            comboBox_instrument = new ComboBox();
            dateTimePicker2 = new DateTimePicker();
            dateTimePicker1 = new DateTimePicker();
            checkedListBox_period = new CheckedListBox();
            panel0.SuspendLayout();
            panel_chart1.SuspendLayout();
            panel3.SuspendLayout();
            panel1.SuspendLayout();
            panel_tr.SuspendLayout();
            panel_tset.SuspendLayout();
            panel_labels.SuspendLayout();
            panel_models.SuspendLayout();
            panel_run.SuspendLayout();
            panel_modes.SuspendLayout();
            panel_instr_time.SuspendLayout();
            SuspendLayout();
            // 
            // panel0
            // 
            panel0.Controls.Add(panel_chart1);
            panel0.Location = new Point(23, 27);
            panel0.Margin = new Padding(4);
            panel0.Name = "panel0";
            panel0.Size = new Size(2133, 208);
            panel0.TabIndex = 7;
            // 
            // panel_chart1
            // 
            panel_chart1.Controls.Add(plot1);
            panel_chart1.Location = new Point(27, 20);
            panel_chart1.Name = "panel_chart1";
            panel_chart1.Size = new Size(513, 160);
            panel_chart1.TabIndex = 12;
            // 
            // plot1
            // 
            plot1.DisplayScale = 1F;
            plot1.Font = new Font("Segoe UI", 12F);
            plot1.Location = new Point(17, 15);
            plot1.Margin = new Padding(4);
            plot1.Name = "plot1";
            plot1.Size = new Size(424, 117);
            plot1.TabIndex = 10;
            plot1.Visible = false;
            plot1.DoubleClick += plot1_DoubleClick;
            plot1.MouseMove += plot1_MouseMove;
            plot1.MouseUp += plot1_MouseUp;
            plot1.Move += plot1_Move;
            // 
            // panel3
            // 
            panel3.Controls.Add(panel1);
            panel3.Location = new Point(23, 264);
            panel3.Name = "panel3";
            panel3.Size = new Size(2133, 1020);
            panel3.TabIndex = 30;
            // 
            // panel1
            // 
            panel1.Controls.Add(panel_tr);
            panel1.Controls.Add(textBox_log);
            panel1.Controls.Add(panel_tset);
            panel1.Controls.Add(panel_labels);
            panel1.Controls.Add(panel_models);
            panel1.Controls.Add(panel_run);
            panel1.Controls.Add(panel_modes);
            panel1.Controls.Add(panel_instr_time);
            panel1.Location = new Point(27, 24);
            panel1.Margin = new Padding(4);
            panel1.Name = "panel1";
            panel1.Size = new Size(2080, 957);
            panel1.TabIndex = 31;
            // 
            // panel_tr
            // 
            panel_tr.BackColor = Color.FromArgb(24, 33, 49);
            panel_tr.Controls.Add(button_rescale);
            panel_tr.ForeColor = Color.FromArgb(199, 214, 255);
            panel_tr.Location = new Point(1182, 587);
            panel_tr.Name = "panel_tr";
            panel_tr.Size = new Size(881, 350);
            panel_tr.TabIndex = 40;
            // 
            // button_rescale
            // 
            button_rescale.BackColor = Color.FromArgb(24, 33, 49);
            button_rescale.FlatStyle = FlatStyle.Flat;
            button_rescale.ForeColor = Color.FromArgb(199, 214, 255);
            button_rescale.Location = new Point(33, 56);
            button_rescale.Margin = new Padding(4);
            button_rescale.Name = "button_rescale";
            button_rescale.Size = new Size(187, 40);
            button_rescale.TabIndex = 25;
            button_rescale.Text = "Rescale";
            button_rescale.UseVisualStyleBackColor = false;
            button_rescale.Click += button_rescale_Click;
            // 
            // textBox_log
            // 
            textBox_log.BackColor = Color.FromArgb(24, 33, 49);
            textBox_log.BorderStyle = BorderStyle.FixedSingle;
            textBox_log.ForeColor = Color.FromArgb(199, 214, 255);
            textBox_log.Location = new Point(1243, 29);
            textBox_log.Multiline = true;
            textBox_log.Name = "textBox_log";
            textBox_log.ScrollBars = ScrollBars.Vertical;
            textBox_log.Size = new Size(643, 160);
            textBox_log.TabIndex = 39;
            // 
            // panel_tset
            // 
            panel_tset.Controls.Add(button_tsetDel);
            panel_tset.Controls.Add(button_tsetAdd);
            panel_tset.Controls.Add(button_tsetSave);
            panel_tset.Controls.Add(button_loadTset);
            panel_tset.Controls.Add(button_tsetNext);
            panel_tset.Controls.Add(button_tsetPrev);
            panel_tset.Controls.Add(comboBox_tset);
            panel_tset.Location = new Point(818, 219);
            panel_tset.Name = "panel_tset";
            panel_tset.Size = new Size(654, 350);
            panel_tset.TabIndex = 27;
            // 
            // button_tsetDel
            // 
            button_tsetDel.BackColor = Color.FromArgb(24, 33, 49);
            button_tsetDel.FlatStyle = FlatStyle.Flat;
            button_tsetDel.ForeColor = Color.FromArgb(249, 117, 131);
            button_tsetDel.Location = new Point(24, 68);
            button_tsetDel.Margin = new Padding(4);
            button_tsetDel.Name = "button_tsetDel";
            button_tsetDel.Size = new Size(44, 31);
            button_tsetDel.TabIndex = 54;
            button_tsetDel.Text = "del";
            button_tsetDel.UseVisualStyleBackColor = false;
            button_tsetDel.Click += button_tsetDel_Click;
            // 
            // button_tsetAdd
            // 
            button_tsetAdd.BackColor = Color.FromArgb(24, 33, 49);
            button_tsetAdd.FlatStyle = FlatStyle.Flat;
            button_tsetAdd.ForeColor = Color.FromArgb(199, 214, 255);
            button_tsetAdd.Location = new Point(154, 67);
            button_tsetAdd.Margin = new Padding(4);
            button_tsetAdd.Name = "button_tsetAdd";
            button_tsetAdd.Size = new Size(54, 31);
            button_tsetAdd.TabIndex = 53;
            button_tsetAdd.Text = "add";
            button_tsetAdd.UseVisualStyleBackColor = false;
            button_tsetAdd.Click += button_tsetAdd_Click;
            // 
            // button_tsetSave
            // 
            button_tsetSave.BackColor = Color.FromArgb(24, 33, 49);
            button_tsetSave.FlatStyle = FlatStyle.Flat;
            button_tsetSave.ForeColor = Color.FromArgb(199, 214, 255);
            button_tsetSave.Location = new Point(87, 67);
            button_tsetSave.Margin = new Padding(4);
            button_tsetSave.Name = "button_tsetSave";
            button_tsetSave.Size = new Size(59, 31);
            button_tsetSave.TabIndex = 52;
            button_tsetSave.Text = "save";
            button_tsetSave.UseVisualStyleBackColor = false;
            button_tsetSave.Click += button_tsetSave_Click;
            // 
            // button_loadTset
            // 
            button_loadTset.BackColor = Color.FromArgb(24, 33, 49);
            button_loadTset.FlatStyle = FlatStyle.Flat;
            button_loadTset.ForeColor = Color.FromArgb(199, 214, 255);
            button_loadTset.Location = new Point(556, 28);
            button_loadTset.Margin = new Padding(4);
            button_loadTset.Name = "button_loadTset";
            button_loadTset.Size = new Size(66, 31);
            button_loadTset.TabIndex = 51;
            button_loadTset.Text = "load";
            button_loadTset.UseVisualStyleBackColor = false;
            button_loadTset.Click += button_loadTset_Click;
            // 
            // button_tsetNext
            // 
            button_tsetNext.BackColor = Color.FromArgb(24, 33, 49);
            button_tsetNext.FlatStyle = FlatStyle.Flat;
            button_tsetNext.ForeColor = Color.FromArgb(199, 214, 255);
            button_tsetNext.Location = new Point(476, 67);
            button_tsetNext.Margin = new Padding(4);
            button_tsetNext.Name = "button_tsetNext";
            button_tsetNext.Size = new Size(66, 31);
            button_tsetNext.TabIndex = 50;
            button_tsetNext.Text = ">";
            button_tsetNext.UseVisualStyleBackColor = false;
            button_tsetNext.Click += button_tsetNext_Click;
            // 
            // button_tsetPrev
            // 
            button_tsetPrev.BackColor = Color.FromArgb(24, 33, 49);
            button_tsetPrev.FlatStyle = FlatStyle.Flat;
            button_tsetPrev.ForeColor = Color.FromArgb(199, 214, 255);
            button_tsetPrev.Location = new Point(402, 67);
            button_tsetPrev.Margin = new Padding(4);
            button_tsetPrev.Name = "button_tsetPrev";
            button_tsetPrev.Size = new Size(66, 31);
            button_tsetPrev.TabIndex = 49;
            button_tsetPrev.Text = "<";
            button_tsetPrev.UseVisualStyleBackColor = false;
            button_tsetPrev.Click += button_tsetPrev_Click;
            // 
            // comboBox_tset
            // 
            comboBox_tset.BackColor = Color.FromArgb(24, 33, 49);
            comboBox_tset.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_tset.FlatStyle = FlatStyle.Flat;
            comboBox_tset.ForeColor = Color.FromArgb(199, 214, 255);
            comboBox_tset.FormattingEnabled = true;
            comboBox_tset.Location = new Point(24, 29);
            comboBox_tset.Margin = new Padding(4);
            comboBox_tset.MaxDropDownItems = 12;
            comboBox_tset.Name = "comboBox_tset";
            comboBox_tset.Size = new Size(524, 31);
            comboBox_tset.TabIndex = 48;
            comboBox_tset.TabStop = false;
            comboBox_tset.SelectedIndexChanged += comboBox_tset_SelectedIndexChanged;
            // 
            // panel_labels
            // 
            panel_labels.Controls.Add(label7);
            panel_labels.Controls.Add(label5);
            panel_labels.Controls.Add(label12);
            panel_labels.Controls.Add(label6);
            panel_labels.Controls.Add(label4);
            panel_labels.Controls.Add(label3);
            panel_labels.Controls.Add(label2);
            panel_labels.Controls.Add(label1);
            panel_labels.Location = new Point(28, 587);
            panel_labels.Name = "panel_labels";
            panel_labels.Size = new Size(1132, 350);
            panel_labels.TabIndex = 25;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.FlatStyle = FlatStyle.Flat;
            label7.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label7.ForeColor = Color.FromArgb(246, 220, 189);
            label7.Location = new Point(326, 21);
            label7.Name = "label7";
            label7.Size = new Size(76, 31);
            label7.TabIndex = 69;
            label7.Text = "label7";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.FlatStyle = FlatStyle.Flat;
            label5.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label5.ForeColor = Color.FromArgb(113, 221, 244);
            label5.Location = new Point(326, 65);
            label5.Name = "label5";
            label5.Size = new Size(76, 31);
            label5.TabIndex = 68;
            label5.Text = "label5";
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.FlatStyle = FlatStyle.Flat;
            label12.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label12.ForeColor = Color.LimeGreen;
            label12.Location = new Point(21, 109);
            label12.Name = "label12";
            label12.Size = new Size(88, 31);
            label12.TabIndex = 67;
            label12.Text = "label12";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.FlatStyle = FlatStyle.Flat;
            label6.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label6.ForeColor = Color.FromArgb(113, 221, 244);
            label6.Location = new Point(782, 109);
            label6.Name = "label6";
            label6.Size = new Size(76, 31);
            label6.TabIndex = 66;
            label6.Text = "label6";
            label6.Visible = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.FlatStyle = FlatStyle.Flat;
            label4.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label4.ForeColor = Color.FromArgb(179, 146, 240);
            label4.Location = new Point(782, 65);
            label4.Name = "label4";
            label4.Size = new Size(76, 31);
            label4.TabIndex = 65;
            label4.Text = "label4";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.FlatStyle = FlatStyle.Flat;
            label3.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label3.ForeColor = Color.FromArgb(249, 117, 131);
            label3.Location = new Point(782, 21);
            label3.Name = "label3";
            label3.Size = new Size(76, 31);
            label3.TabIndex = 64;
            label3.Text = "label3";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.FlatStyle = FlatStyle.Flat;
            label2.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label2.ForeColor = Color.FromArgb(121, 184, 255);
            label2.Location = new Point(21, 62);
            label2.Name = "label2";
            label2.Size = new Size(76, 31);
            label2.TabIndex = 63;
            label2.Text = "label2";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.FlatStyle = FlatStyle.Flat;
            label1.Font = new Font("Segoe UI", 13.8F, FontStyle.Regular, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.FromArgb(204, 226, 255);
            label1.Location = new Point(21, 21);
            label1.Name = "label1";
            label1.Size = new Size(76, 31);
            label1.TabIndex = 62;
            label1.Text = "label1";
            // 
            // panel_models
            // 
            panel_models.Controls.Add(comboBox_tr_model);
            panel_models.Location = new Point(214, 29);
            panel_models.Name = "panel_models";
            panel_models.Size = new Size(216, 160);
            panel_models.TabIndex = 24;
            // 
            // comboBox_tr_model
            // 
            comboBox_tr_model.BackColor = Color.FromArgb(24, 33, 49);
            comboBox_tr_model.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_tr_model.FlatStyle = FlatStyle.Flat;
            comboBox_tr_model.ForeColor = Color.FromArgb(199, 214, 255);
            comboBox_tr_model.FormattingEnabled = true;
            comboBox_tr_model.Location = new Point(21, 17);
            comboBox_tr_model.Margin = new Padding(4);
            comboBox_tr_model.MaxDropDownItems = 12;
            comboBox_tr_model.Name = "comboBox_tr_model";
            comboBox_tr_model.Size = new Size(170, 31);
            comboBox_tr_model.TabIndex = 37;
            comboBox_tr_model.TabStop = false;
            comboBox_tr_model.SelectedIndexChanged += comboBox_tr_model_SelectedIndexChanged_1;
            // 
            // panel_run
            // 
            panel_run.Controls.Add(buttonRun);
            panel_run.Controls.Add(progressBar_instr);
            panel_run.Controls.Add(progressBar_timepoint);
            panel_run.Controls.Add(button_open_dir);
            panel_run.Controls.Add(textBox_path);
            panel_run.Location = new Point(456, 29);
            panel_run.Name = "panel_run";
            panel_run.Size = new Size(760, 160);
            panel_run.TabIndex = 23;
            // 
            // buttonRun
            // 
            buttonRun.BackColor = Color.FromArgb(24, 33, 49);
            buttonRun.FlatStyle = FlatStyle.Flat;
            buttonRun.ForeColor = Color.FromArgb(199, 214, 255);
            buttonRun.Location = new Point(19, 19);
            buttonRun.Margin = new Padding(4);
            buttonRun.Name = "buttonRun";
            buttonRun.Size = new Size(121, 40);
            buttonRun.TabIndex = 37;
            buttonRun.Text = "Run";
            buttonRun.UseVisualStyleBackColor = false;
            // 
            // progressBar_instr
            // 
            progressBar_instr.ForeColor = Color.Cyan;
            progressBar_instr.Location = new Point(172, 72);
            progressBar_instr.Maximum = 1000;
            progressBar_instr.Name = "progressBar_instr";
            progressBar_instr.Size = new Size(452, 5);
            progressBar_instr.Step = 1;
            progressBar_instr.Style = ProgressBarStyle.Continuous;
            progressBar_instr.TabIndex = 36;
            progressBar_instr.Value = 444;
            // 
            // progressBar_timepoint
            // 
            progressBar_timepoint.ForeColor = Color.Cyan;
            progressBar_timepoint.Location = new Point(172, 96);
            progressBar_timepoint.Maximum = 1000;
            progressBar_timepoint.Name = "progressBar_timepoint";
            progressBar_timepoint.Size = new Size(452, 5);
            progressBar_timepoint.Step = 1;
            progressBar_timepoint.Style = ProgressBarStyle.Continuous;
            progressBar_timepoint.TabIndex = 35;
            progressBar_timepoint.Value = 444;
            // 
            // button_open_dir
            // 
            button_open_dir.BackColor = Color.FromArgb(24, 33, 49);
            button_open_dir.FlatStyle = FlatStyle.Flat;
            button_open_dir.ForeColor = Color.FromArgb(199, 214, 255);
            button_open_dir.Location = new Point(644, 17);
            button_open_dir.Margin = new Padding(4);
            button_open_dir.Name = "button_open_dir";
            button_open_dir.Size = new Size(86, 32);
            button_open_dir.TabIndex = 34;
            button_open_dir.Text = "Open";
            button_open_dir.UseVisualStyleBackColor = false;
            // 
            // textBox_path
            // 
            textBox_path.BackColor = Color.FromArgb(24, 33, 49);
            textBox_path.ForeColor = Color.White;
            textBox_path.Location = new Point(172, 19);
            textBox_path.Name = "textBox_path";
            textBox_path.Size = new Size(452, 30);
            textBox_path.TabIndex = 33;
            // 
            // panel_modes
            // 
            panel_modes.Controls.Add(button_mode1);
            panel_modes.Controls.Add(button_mode0);
            panel_modes.Location = new Point(28, 29);
            panel_modes.Name = "panel_modes";
            panel_modes.Size = new Size(160, 160);
            panel_modes.TabIndex = 21;
            // 
            // button_mode1
            // 
            button_mode1.BackColor = Color.FromArgb(24, 33, 49);
            button_mode1.FlatStyle = FlatStyle.Flat;
            button_mode1.ForeColor = Color.FromArgb(113, 221, 244);
            button_mode1.Location = new Point(16, 86);
            button_mode1.Margin = new Padding(4);
            button_mode1.Name = "button_mode1";
            button_mode1.Size = new Size(121, 40);
            button_mode1.TabIndex = 25;
            button_mode1.Text = "Mode 1";
            button_mode1.UseVisualStyleBackColor = false;
            button_mode1.Click += button_mode1_Click;
            // 
            // button_mode0
            // 
            button_mode0.BackColor = Color.FromArgb(24, 33, 49);
            button_mode0.FlatStyle = FlatStyle.Flat;
            button_mode0.ForeColor = Color.FromArgb(113, 221, 244);
            button_mode0.Location = new Point(16, 13);
            button_mode0.Margin = new Padding(4);
            button_mode0.Name = "button_mode0";
            button_mode0.Size = new Size(121, 40);
            button_mode0.TabIndex = 17;
            button_mode0.Text = "Mode 0";
            button_mode0.UseVisualStyleBackColor = false;
            button_mode0.Click += button_mode0_Click;
            // 
            // panel_instr_time
            // 
            panel_instr_time.Controls.Add(button_reset2);
            panel_instr_time.Controls.Add(button_reset1);
            panel_instr_time.Controls.Add(button_rnd);
            panel_instr_time.Controls.Add(button_next);
            panel_instr_time.Controls.Add(button_prev);
            panel_instr_time.Controls.Add(comboBox_instrument);
            panel_instr_time.Controls.Add(dateTimePicker2);
            panel_instr_time.Controls.Add(dateTimePicker1);
            panel_instr_time.Controls.Add(checkedListBox_period);
            panel_instr_time.Location = new Point(28, 219);
            panel_instr_time.Name = "panel_instr_time";
            panel_instr_time.Size = new Size(762, 350);
            panel_instr_time.TabIndex = 18;
            // 
            // button_reset2
            // 
            button_reset2.BackColor = Color.FromArgb(24, 33, 49);
            button_reset2.FlatStyle = FlatStyle.Flat;
            button_reset2.ForeColor = Color.FromArgb(199, 214, 255);
            button_reset2.Location = new Point(500, 62);
            button_reset2.Margin = new Padding(4);
            button_reset2.Name = "button_reset2";
            button_reset2.Size = new Size(61, 40);
            button_reset2.TabIndex = 26;
            button_reset2.Text = "R>";
            button_reset2.UseVisualStyleBackColor = false;
            button_reset2.Click += button_goLastPeriod_Click;
            // 
            // button_reset1
            // 
            button_reset1.BackColor = Color.FromArgb(24, 33, 49);
            button_reset1.FlatStyle = FlatStyle.Flat;
            button_reset1.ForeColor = Color.FromArgb(199, 214, 255);
            button_reset1.Location = new Point(133, 62);
            button_reset1.Margin = new Padding(4);
            button_reset1.Name = "button_reset1";
            button_reset1.Size = new Size(61, 40);
            button_reset1.TabIndex = 25;
            button_reset1.Text = "<R";
            button_reset1.UseVisualStyleBackColor = false;
            button_reset1.Click += button_goFirstPeriod_Click;
            // 
            // button_rnd
            // 
            button_rnd.BackColor = Color.FromArgb(24, 33, 49);
            button_rnd.FlatStyle = FlatStyle.Flat;
            button_rnd.ForeColor = Color.FromArgb(199, 214, 255);
            button_rnd.Location = new Point(581, 62);
            button_rnd.Margin = new Padding(4);
            button_rnd.Name = "button_rnd";
            button_rnd.Size = new Size(121, 40);
            button_rnd.TabIndex = 24;
            button_rnd.Text = "Rnd";
            button_rnd.UseVisualStyleBackColor = false;
            button_rnd.Click += button_goRandomPeriod_Click;
            // 
            // button_next
            // 
            button_next.BackColor = Color.FromArgb(24, 33, 49);
            button_next.FlatStyle = FlatStyle.Flat;
            button_next.ForeColor = Color.FromArgb(199, 214, 255);
            button_next.Location = new Point(351, 62);
            button_next.Margin = new Padding(4);
            button_next.Name = "button_next";
            button_next.Size = new Size(121, 40);
            button_next.TabIndex = 21;
            button_next.Text = "Next";
            button_next.UseVisualStyleBackColor = false;
            button_next.Click += button_goNextPeriod_Click;
            // 
            // button_prev
            // 
            button_prev.BackColor = Color.FromArgb(24, 33, 49);
            button_prev.FlatStyle = FlatStyle.Flat;
            button_prev.ForeColor = Color.FromArgb(199, 214, 255);
            button_prev.Location = new Point(222, 62);
            button_prev.Margin = new Padding(4);
            button_prev.Name = "button_prev";
            button_prev.Size = new Size(121, 40);
            button_prev.TabIndex = 20;
            button_prev.Text = "Prev";
            button_prev.UseVisualStyleBackColor = false;
            button_prev.Click += button_goPrevPeriod_Click;
            // 
            // comboBox_instrument
            // 
            comboBox_instrument.BackColor = Color.FromArgb(24, 33, 49);
            comboBox_instrument.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox_instrument.FlatStyle = FlatStyle.Flat;
            comboBox_instrument.ForeColor = Color.FromArgb(199, 214, 255);
            comboBox_instrument.FormattingEnabled = true;
            comboBox_instrument.Location = new Point(579, 15);
            comboBox_instrument.Margin = new Padding(4);
            comboBox_instrument.MaxDropDownItems = 12;
            comboBox_instrument.Name = "comboBox_instrument";
            comboBox_instrument.Size = new Size(170, 31);
            comboBox_instrument.TabIndex = 19;
            comboBox_instrument.TabStop = false;
            comboBox_instrument.SelectedIndexChanged += comboBox_instrument_SelectedIndexChanged_1;
            // 
            // dateTimePicker2
            // 
            dateTimePicker2.CalendarForeColor = Color.Magenta;
            dateTimePicker2.CalendarMonthBackground = Color.Cyan;
            dateTimePicker2.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dateTimePicker2.Format = DateTimePickerFormat.Custom;
            dateTimePicker2.Location = new Point(351, 13);
            dateTimePicker2.Margin = new Padding(4);
            dateTimePicker2.Name = "dateTimePicker2";
            dateTimePicker2.Size = new Size(210, 32);
            dateTimePicker2.TabIndex = 18;
            dateTimePicker2.CloseUp += dateTimePicker2_CloseUp;
            // 
            // dateTimePicker1
            // 
            dateTimePicker1.CalendarForeColor = Color.Chartreuse;
            dateTimePicker1.CalendarMonthBackground = Color.DarkSeaGreen;
            dateTimePicker1.CalendarTitleBackColor = Color.Tomato;
            dateTimePicker1.CalendarTitleForeColor = Color.NavajoWhite;
            dateTimePicker1.CalendarTrailingForeColor = Color.Magenta;
            dateTimePicker1.Enabled = false;
            dateTimePicker1.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dateTimePicker1.Format = DateTimePickerFormat.Custom;
            dateTimePicker1.Location = new Point(133, 13);
            dateTimePicker1.Margin = new Padding(4);
            dateTimePicker1.Name = "dateTimePicker1";
            dateTimePicker1.Size = new Size(210, 32);
            dateTimePicker1.TabIndex = 17;
            // 
            // checkedListBox_period
            // 
            checkedListBox_period.BackColor = Color.FromArgb(24, 33, 49);
            checkedListBox_period.BorderStyle = BorderStyle.None;
            checkedListBox_period.CheckOnClick = true;
            checkedListBox_period.ForeColor = Color.FromArgb(199, 214, 255);
            checkedListBox_period.FormattingEnabled = true;
            checkedListBox_period.Items.AddRange(new object[] { "day", "2 days", "3 days", "week", "2 weeks", "month", "year" });
            checkedListBox_period.Location = new Point(16, 16);
            checkedListBox_period.Name = "checkedListBox_period";
            checkedListBox_period.Size = new Size(110, 225);
            checkedListBox_period.TabIndex = 7;
            checkedListBox_period.ItemCheck += checkedListBox_period_ItemCheck;
            checkedListBox_period.MouseUp += checkedListBox_period_MouseUp;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(24, 33, 49);
            ClientSize = new Size(2186, 1334);
            Controls.Add(panel0);
            Controls.Add(panel3);
            Font = new Font("Segoe UI", 10F);
            ForeColor = Color.FromArgb(199, 214, 255);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4);
            Name = "Form1";
            Text = "TradeEstimator";
            panel0.ResumeLayout(false);
            panel_chart1.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            panel_tr.ResumeLayout(false);
            panel_tset.ResumeLayout(false);
            panel_labels.ResumeLayout(false);
            panel_labels.PerformLayout();
            panel_models.ResumeLayout(false);
            panel_run.ResumeLayout(false);
            panel_run.PerformLayout();
            panel_modes.ResumeLayout(false);
            panel_instr_time.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion
        private Panel panel0;
        private Panel panel_chart1;
        private ScottPlot.WinForms.FormsPlot plot1;
        private Button button_scalex3;
        private Button button_scalex1;
        private Panel panel3;
        private Panel panel1;
        private TextBox textBox_log;
        private Panel panel_tset;
        private Button button_tsetDel;
        private Button button_tsetAdd;
        private Button button_tsetSave;
        private Button button_loadTset;
        private Button button_tsetNext;
        private Button button_tsetPrev;
        private ComboBox comboBox_tset;
        private Panel panel_labels;
        private Label label6;
        private Label label4;
        private Label label3;
        private Label label2;
        private Label label1;
        private Panel panel_models;
        private ComboBox comboBox_tr_model;
        private Panel panel_run;
        private Button buttonRun;
        private ProgressBar progressBar_instr;
        private ProgressBar progressBar_timepoint;
        private Button button_open_dir;
        private TextBox textBox_path;
        private Panel panel_modes;
        private Button button_mode1;
        private Button button_mode0;
        private Panel panel_instr_time;
        private Button button_reset2;
        private Button button_reset1;
        private Button button_rnd;
        private Button button_next;
        private Button button_prev;
        private ComboBox comboBox_instrument;
        private DateTimePicker dateTimePicker2;
        private DateTimePicker dateTimePicker1;
        private CheckedListBox checkedListBox_period;
        private Panel panel_tr;
        private Label label12;
        private Label label5;
        private Label label7;
        private Button button_rescale;
    }
}