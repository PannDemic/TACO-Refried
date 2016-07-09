using OpenTK.Graphics;

namespace Taco
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.SearchSystem = new System.Windows.Forms.TextBox();
            this.Ticker = new System.Windows.Forms.Timer(this.components);
            this.MonitorBranch = new System.Windows.Forms.CheckBox();
            this.MonitorDeklein = new System.Windows.Forms.CheckBox();
            this.MonitorTenal = new System.Windows.Forms.CheckBox();
            this.MonitorVenal = new System.Windows.Forms.CheckBox();
            this.FullscreenToggle = new System.Windows.Forms.Button();
            this.QuitTaco = new System.Windows.Forms.Button();
            this.TestFlood = new System.Windows.Forms.Button();
            this.UIContainer = new System.Windows.Forms.SplitContainer();
            this.BufferingIndicator = new System.Windows.Forms.Label();
            this.FindSystem = new System.Windows.Forms.Button();
            this.LogWatchToggle = new System.Windows.Forms.Button();
            this.UITabControl = new System.Windows.Forms.TabControl();
            this.CombinedPage = new System.Windows.Forms.TabPage();
            this.GOTGPage = new System.Windows.Forms.TabPage();
            this.GOTGIntel = new System.Windows.Forms.TextBox();
            this.BranchPage = new System.Windows.Forms.TabPage();
            this.BranchIntel = new System.Windows.Forms.TextBox();
            this.DekleinPage = new System.Windows.Forms.TabPage();
            this.DekleinIntel = new System.Windows.Forms.TextBox();
            this.TenalPage = new System.Windows.Forms.TabPage();
            this.TenalIntel = new System.Windows.Forms.TextBox();
            this.VenalPage = new System.Windows.Forms.TabPage();
            this.VenalIntel = new System.Windows.Forms.TextBox();
            this.FadePage = new System.Windows.Forms.TabPage();
            this.FadeIntel = new System.Windows.Forms.TextBox();
            this.PureBlindPage = new System.Windows.Forms.TabPage();
            this.PureBlindIntel = new System.Windows.Forms.TextBox();
            this.TributePage = new System.Windows.Forms.TabPage();
            this.TributeIntel = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ValePage = new System.Windows.Forms.TabPage();
            this.ValeIntel = new System.Windows.Forms.TextBox();
            this.ProvidencePage = new System.Windows.Forms.TabPage();
            this.ProvidenceIntel = new System.Windows.Forms.TextBox();
            this.DelvePage = new System.Windows.Forms.TabPage();
            this.DelveIntel = new System.Windows.Forms.TextBox();
            this.ConfigPage = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.ChannelsPage = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.MonitorGOTG = new System.Windows.Forms.CheckBox();
            this.MonitorDelve = new System.Windows.Forms.CheckBox();
            this.MonitorProvidence = new System.Windows.Forms.CheckBox();
            this.MonitorVale = new System.Windows.Forms.CheckBox();
            this.MonitorTribute = new System.Windows.Forms.CheckBox();
            this.MonitorPureBlind = new System.Windows.Forms.CheckBox();
            this.MonitorFade = new System.Windows.Forms.CheckBox();
            this.MonitorGameLog = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.AlertGOTG = new System.Windows.Forms.CheckBox();
            this.AlertDelve = new System.Windows.Forms.CheckBox();
            this.AlertProvidence = new System.Windows.Forms.CheckBox();
            this.AlertVale = new System.Windows.Forms.CheckBox();
            this.AlertTribute = new System.Windows.Forms.CheckBox();
            this.AlertPureBlind = new System.Windows.Forms.CheckBox();
            this.AlertFade = new System.Windows.Forms.CheckBox();
            this.AlertVenal = new System.Windows.Forms.CheckBox();
            this.AlertTenal = new System.Windows.Forms.CheckBox();
            this.AlertDeklein = new System.Windows.Forms.CheckBox();
            this.AlertBranch = new System.Windows.Forms.CheckBox();
            this.AlertManagementPage = new System.Windows.Forms.TabPage();
            this.AddCustomAlertGroup = new System.Windows.Forms.GroupBox();
            this.SaveCustomAlert = new System.Windows.Forms.Button();
            this.PlayCustomTextAlertSound = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.CustomTextAlertSound = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.CustomAlertRepeatInterval = new System.Windows.Forms.NumericUpDown();
            this.NewCustomAlertText = new System.Windows.Forms.TextBox();
            this.AddNewCustomAlert = new System.Windows.Forms.Button();
            this.AddRangeAlertGroup = new System.Windows.Forms.GroupBox();
            this.SaveRangeAlert = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.NewRangeAlertType = new System.Windows.Forms.ComboBox();
            this.PlayRangeAlertSound = new System.Windows.Forms.LinkLabel();
            this.AddNewRangeAlert = new System.Windows.Forms.Button();
            this.RangeAlertSound = new System.Windows.Forms.ComboBox();
            this.LowerAlertRange = new System.Windows.Forms.NumericUpDown();
            this.LowerLimitOperator = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.UpperLimitOperator = new System.Windows.Forms.ComboBox();
            this.UpperAlertRange = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.RangeAlertSystem = new System.Windows.Forms.ComboBox();
            this.RangeAlertCharacter = new System.Windows.Forms.ComboBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.CancelEditSelectedItem = new System.Windows.Forms.LinkLabel();
            this.EditSelectedItem = new System.Windows.Forms.LinkLabel();
            this.MoveAlertDown = new System.Windows.Forms.Button();
            this.MoveAlertUp = new System.Windows.Forms.Button();
            this.PlayAlertSound = new System.Windows.Forms.LinkLabel();
            this.RemoveSelectedItem = new System.Windows.Forms.LinkLabel();
            this.AlertList = new System.Windows.Forms.CheckedListBox();
            this.ListManagementPage = new System.Windows.Forms.TabPage();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.NewLinkedCharacter = new System.Windows.Forms.TextBox();
            this.RemoveLinkedCharacter = new System.Windows.Forms.Button();
            this.CharacterList = new System.Windows.Forms.ListBox();
            this.AddLinkedCharacter = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.IgnoreSystemList = new System.Windows.Forms.ListBox();
            this.RemoveIgnoreSystem = new System.Windows.Forms.Button();
            this.NewIgnoreSystem = new System.Windows.Forms.ComboBox();
            this.AddIgnoreSystem = new System.Windows.Forms.Button();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.IgnoreTextList = new System.Windows.Forms.ListBox();
            this.RemoveIgnoreText = new System.Windows.Forms.Button();
            this.NewIgnoreText = new System.Windows.Forms.TextBox();
            this.AddIgnoreText = new System.Windows.Forms.Button();
            this.MiscSettingsPage = new System.Windows.Forms.TabPage();
            this.CrashException = new System.Windows.Forms.Button();
            this.CrashRecursive = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.PlayAnomalyWatcherSoundPreview = new System.Windows.Forms.LinkLabel();
            this.AnomalyWatcherSound = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.DisplayOpenFileAlerts = new System.Windows.Forms.CheckBox();
            this.DisplayNewFileAlerts = new System.Windows.Forms.CheckBox();
            this.ClearSelectedSystems = new System.Windows.Forms.Button();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label29 = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.MaxAlerts = new System.Windows.Forms.NumericUpDown();
            this.MaxAlertAge = new System.Windows.Forms.NumericUpDown();
            this.label26 = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.ShowAlertAgeSecs = new System.Windows.Forms.CheckBox();
            this.ShowReportCount = new System.Windows.Forms.CheckBox();
            this.ShowAlertAge = new System.Windows.Forms.CheckBox();
            this.ShowCharacterLocations = new System.Windows.Forms.CheckBox();
            this.MapRangeFrom = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.DisplayCharacterNames = new System.Windows.Forms.CheckBox();
            this.CentreOnCharacter = new System.Windows.Forms.ComboBox();
            this.CameraFollowCharacter = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.PreserveSelectedSystems = new System.Windows.Forms.CheckBox();
            this.PreserveFullScreenStatus = new System.Windows.Forms.CheckBox();
            this.PreserveHomeSystem = new System.Windows.Forms.CheckBox();
            this.PreserveWindowPosition = new System.Windows.Forms.CheckBox();
            this.PreserveWindowSize = new System.Windows.Forms.CheckBox();
            this.PreserveCameraDistance = new System.Windows.Forms.CheckBox();
            this.PreserveLookAt = new System.Windows.Forms.CheckBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.ChooseLogPath = new System.Windows.Forms.Button();
            this.OverrideLogPath = new System.Windows.Forms.CheckBox();
            this.LogPath = new System.Windows.Forms.TextBox();
            this.InfoPage = new System.Windows.Forms.TabPage();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.label22 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.label32 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.LogBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.PathFindingTicker = new System.Windows.Forms.Timer(this.components);
            this.IntelUpdateTicker = new System.Windows.Forms.Timer(this.components);
            this.CustomSoundPicker = new System.Windows.Forms.OpenFileDialog();
            this.MenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.followMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noneToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mapRangeFromMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.homeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.anomalyMonitorMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.muteSoundMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.clearSelectedSystemsMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quitMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HintToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.glOut = new OpenTK.GLControl();
            this.CombinedIntel = new Taco.Classes.RichTextBoxEx();
            ((System.ComponentModel.ISupportInitialize)(this.UIContainer)).BeginInit();
            this.UIContainer.Panel1.SuspendLayout();
            this.UIContainer.Panel2.SuspendLayout();
            this.UIContainer.SuspendLayout();
            this.UITabControl.SuspendLayout();
            this.CombinedPage.SuspendLayout();
            this.GOTGPage.SuspendLayout();
            this.BranchPage.SuspendLayout();
            this.DekleinPage.SuspendLayout();
            this.TenalPage.SuspendLayout();
            this.VenalPage.SuspendLayout();
            this.FadePage.SuspendLayout();
            this.PureBlindPage.SuspendLayout();
            this.TributePage.SuspendLayout();
            this.ValePage.SuspendLayout();
            this.ProvidencePage.SuspendLayout();
            this.DelvePage.SuspendLayout();
            this.ConfigPage.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.ChannelsPage.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.AlertManagementPage.SuspendLayout();
            this.AddCustomAlertGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomAlertRepeatInterval)).BeginInit();
            this.AddRangeAlertGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LowerAlertRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpperAlertRange)).BeginInit();
            this.groupBox9.SuspendLayout();
            this.ListManagementPage.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.MiscSettingsPage.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox8.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxAlerts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxAlertAge)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.InfoPage.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox14.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox13.SuspendLayout();
            this.MenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // SearchSystem
            // 
            this.SearchSystem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchSystem.Location = new System.Drawing.Point(3, 6);
            this.SearchSystem.Name = "SearchSystem";
            this.SearchSystem.Size = new System.Drawing.Size(202, 21);
            this.SearchSystem.TabIndex = 1;
            // 
            // Ticker
            // 
            this.Ticker.Enabled = true;
            this.Ticker.Interval = 10;
            this.Ticker.Tick += new System.EventHandler(this.Ticker_Tick);
            // 
            // MonitorBranch
            // 
            this.MonitorBranch.AutoSize = true;
            this.MonitorBranch.Checked = true;
            this.MonitorBranch.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MonitorBranch.Location = new System.Drawing.Point(6, 18);
            this.MonitorBranch.Name = "MonitorBranch";
            this.MonitorBranch.Size = new System.Drawing.Size(60, 16);
            this.MonitorBranch.TabIndex = 5;
            this.MonitorBranch.Text = "Branch";
            this.MonitorBranch.UseVisualStyleBackColor = true;
            this.MonitorBranch.CheckedChanged += new System.EventHandler(this.MonitorBranch_CheckedChanged);
            // 
            // MonitorDeklein
            // 
            this.MonitorDeklein.AutoSize = true;
            this.MonitorDeklein.Checked = true;
            this.MonitorDeklein.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MonitorDeklein.Location = new System.Drawing.Point(6, 39);
            this.MonitorDeklein.Name = "MonitorDeklein";
            this.MonitorDeklein.Size = new System.Drawing.Size(66, 16);
            this.MonitorDeklein.TabIndex = 6;
            this.MonitorDeklein.Text = "Deklein";
            this.MonitorDeklein.UseVisualStyleBackColor = true;
            this.MonitorDeklein.CheckedChanged += new System.EventHandler(this.MonitorDeklein_CheckedChanged);
            // 
            // MonitorTenal
            // 
            this.MonitorTenal.AutoSize = true;
            this.MonitorTenal.Checked = true;
            this.MonitorTenal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MonitorTenal.Location = new System.Drawing.Point(6, 60);
            this.MonitorTenal.Name = "MonitorTenal";
            this.MonitorTenal.Size = new System.Drawing.Size(54, 16);
            this.MonitorTenal.TabIndex = 7;
            this.MonitorTenal.Text = "Tenal";
            this.MonitorTenal.UseVisualStyleBackColor = true;
            this.MonitorTenal.CheckedChanged += new System.EventHandler(this.MonitorTenal_CheckedChanged);
            // 
            // MonitorVenal
            // 
            this.MonitorVenal.AutoSize = true;
            this.MonitorVenal.Checked = true;
            this.MonitorVenal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MonitorVenal.Location = new System.Drawing.Point(6, 81);
            this.MonitorVenal.Name = "MonitorVenal";
            this.MonitorVenal.Size = new System.Drawing.Size(54, 16);
            this.MonitorVenal.TabIndex = 8;
            this.MonitorVenal.Text = "Venal";
            this.MonitorVenal.UseVisualStyleBackColor = true;
            this.MonitorVenal.CheckedChanged += new System.EventHandler(this.MonitorVenal_CheckedChanged);
            // 
            // FullscreenToggle
            // 
            this.FullscreenToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FullscreenToggle.Location = new System.Drawing.Point(298, 5);
            this.FullscreenToggle.Name = "FullscreenToggle";
            this.FullscreenToggle.Size = new System.Drawing.Size(148, 21);
            this.FullscreenToggle.TabIndex = 3;
            this.FullscreenToggle.Text = "Fullscreen";
            this.FullscreenToggle.UseVisualStyleBackColor = true;
            this.FullscreenToggle.Click += new System.EventHandler(this.FullscreenToggle_Click);
            // 
            // QuitTaco
            // 
            this.QuitTaco.Location = new System.Drawing.Point(6, 434);
            this.QuitTaco.Name = "QuitTaco";
            this.QuitTaco.Size = new System.Drawing.Size(410, 21);
            this.QuitTaco.TabIndex = 11;
            this.QuitTaco.Text = "Quit";
            this.QuitTaco.UseVisualStyleBackColor = true;
            this.QuitTaco.Click += new System.EventHandler(this.QuitTaco_Click);
            // 
            // TestFlood
            // 
            this.TestFlood.Enabled = false;
            this.TestFlood.Location = new System.Drawing.Point(6, 461);
            this.TestFlood.Name = "TestFlood";
            this.TestFlood.Size = new System.Drawing.Size(130, 21);
            this.TestFlood.TabIndex = 10;
            this.TestFlood.TabStop = false;
            this.TestFlood.Text = "Test Flood";
            this.TestFlood.UseVisualStyleBackColor = true;
            this.TestFlood.Visible = false;
            this.TestFlood.Click += new System.EventHandler(this.TestFlood_Click);
            // 
            // UIContainer
            // 
            this.UIContainer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UIContainer.IsSplitterFixed = true;
            this.UIContainer.Location = new System.Drawing.Point(775, 11);
            this.UIContainer.Name = "UIContainer";
            this.UIContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // UIContainer.Panel1
            // 
            this.UIContainer.Panel1.Controls.Add(this.BufferingIndicator);
            this.UIContainer.Panel1.Controls.Add(this.FindSystem);
            this.UIContainer.Panel1.Controls.Add(this.LogWatchToggle);
            this.UIContainer.Panel1.Controls.Add(this.FullscreenToggle);
            this.UIContainer.Panel1.Controls.Add(this.SearchSystem);
            this.UIContainer.Panel1.Resize += new System.EventHandler(this.UIContainer_Panel1_Resize);
            this.UIContainer.Panel1MinSize = 65;
            // 
            // UIContainer.Panel2
            // 
            this.UIContainer.Panel2.Controls.Add(this.UITabControl);
            this.UIContainer.Size = new System.Drawing.Size(450, 692);
            this.UIContainer.SplitterDistance = 65;
            this.UIContainer.TabIndex = 18;
            // 
            // BufferingIndicator
            // 
            this.BufferingIndicator.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BufferingIndicator.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BufferingIndicator.Location = new System.Drawing.Point(3, 6);
            this.BufferingIndicator.Name = "BufferingIndicator";
            this.BufferingIndicator.Size = new System.Drawing.Size(202, 18);
            this.BufferingIndicator.TabIndex = 10;
            this.BufferingIndicator.Text = "Buffering Intel";
            this.BufferingIndicator.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.BufferingIndicator.Visible = false;
            // 
            // FindSystem
            // 
            this.FindSystem.Location = new System.Drawing.Point(211, 5);
            this.FindSystem.Name = "FindSystem";
            this.FindSystem.Size = new System.Drawing.Size(81, 21);
            this.FindSystem.TabIndex = 2;
            this.FindSystem.Text = "Find";
            this.FindSystem.UseVisualStyleBackColor = true;
            this.FindSystem.Click += new System.EventHandler(this.FindSystem_Click);
            // 
            // LogWatchToggle
            // 
            this.LogWatchToggle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LogWatchToggle.Location = new System.Drawing.Point(3, 31);
            this.LogWatchToggle.Name = "LogWatchToggle";
            this.LogWatchToggle.Size = new System.Drawing.Size(443, 21);
            this.LogWatchToggle.TabIndex = 9;
            this.LogWatchToggle.Text = "Start Watching Logs";
            this.LogWatchToggle.UseVisualStyleBackColor = true;
            this.LogWatchToggle.Click += new System.EventHandler(this.LogWatchToggle_Click);
            // 
            // UITabControl
            // 
            this.UITabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UITabControl.Controls.Add(this.CombinedPage);
            this.UITabControl.Controls.Add(this.GOTGPage);
            this.UITabControl.Controls.Add(this.BranchPage);
            this.UITabControl.Controls.Add(this.DekleinPage);
            this.UITabControl.Controls.Add(this.TenalPage);
            this.UITabControl.Controls.Add(this.VenalPage);
            this.UITabControl.Controls.Add(this.FadePage);
            this.UITabControl.Controls.Add(this.PureBlindPage);
            this.UITabControl.Controls.Add(this.TributePage);
            this.UITabControl.Controls.Add(this.ValePage);
            this.UITabControl.Controls.Add(this.ProvidencePage);
            this.UITabControl.Controls.Add(this.DelvePage);
            this.UITabControl.Controls.Add(this.ConfigPage);
            this.UITabControl.ItemSize = new System.Drawing.Size(37, 18);
            this.UITabControl.Location = new System.Drawing.Point(0, 0);
            this.UITabControl.Margin = new System.Windows.Forms.Padding(0);
            this.UITabControl.Name = "UITabControl";
            this.UITabControl.Padding = new System.Drawing.Point(0, 0);
            this.UITabControl.SelectedIndex = 0;
            this.UITabControl.Size = new System.Drawing.Size(450, 623);
            this.UITabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.UITabControl.TabIndex = 19;
            // 
            // CombinedPage
            // 
            this.CombinedPage.Controls.Add(this.CombinedIntel);
            this.CombinedPage.Location = new System.Drawing.Point(4, 22);
            this.CombinedPage.Margin = new System.Windows.Forms.Padding(0);
            this.CombinedPage.Name = "CombinedPage";
            this.CombinedPage.Size = new System.Drawing.Size(442, 597);
            this.CombinedPage.TabIndex = 0;
            this.CombinedPage.Text = "All";
            this.CombinedPage.UseVisualStyleBackColor = true;
            // 
            // GOTGPage
            // 
            this.GOTGPage.Controls.Add(this.GOTGIntel);
            this.GOTGPage.Location = new System.Drawing.Point(4, 22);
            this.GOTGPage.Name = "GOTGPage";
            this.GOTGPage.Padding = new System.Windows.Forms.Padding(3);
            this.GOTGPage.Size = new System.Drawing.Size(442, 597);
            this.GOTGPage.TabIndex = 14;
            this.GOTGPage.Text = "GOTG";
            this.GOTGPage.UseVisualStyleBackColor = true;
            // 
            // GOTGIntel
            // 
            this.GOTGIntel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GOTGIntel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GOTGIntel.Location = new System.Drawing.Point(-4, 0);
            this.GOTGIntel.Multiline = true;
            this.GOTGIntel.Name = "GOTGIntel";
            this.GOTGIntel.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.GOTGIntel.Size = new System.Drawing.Size(447, 599);
            this.GOTGIntel.TabIndex = 0;
            this.GOTGIntel.WordWrap = false;
            // 
            // BranchPage
            // 
            this.BranchPage.Controls.Add(this.BranchIntel);
            this.BranchPage.Location = new System.Drawing.Point(4, 22);
            this.BranchPage.Name = "BranchPage";
            this.BranchPage.Size = new System.Drawing.Size(442, 597);
            this.BranchPage.TabIndex = 2;
            this.BranchPage.Text = "Brn";
            this.BranchPage.UseVisualStyleBackColor = true;
            // 
            // BranchIntel
            // 
            this.BranchIntel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BranchIntel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BranchIntel.Location = new System.Drawing.Point(-4, 0);
            this.BranchIntel.Multiline = true;
            this.BranchIntel.Name = "BranchIntel";
            this.BranchIntel.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.BranchIntel.Size = new System.Drawing.Size(447, 599);
            this.BranchIntel.TabIndex = 13;
            this.BranchIntel.WordWrap = false;
            // 
            // DekleinPage
            // 
            this.DekleinPage.Controls.Add(this.DekleinIntel);
            this.DekleinPage.Location = new System.Drawing.Point(4, 22);
            this.DekleinPage.Name = "DekleinPage";
            this.DekleinPage.Size = new System.Drawing.Size(442, 597);
            this.DekleinPage.TabIndex = 3;
            this.DekleinPage.Text = "Dek";
            this.DekleinPage.UseVisualStyleBackColor = true;
            // 
            // DekleinIntel
            // 
            this.DekleinIntel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DekleinIntel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DekleinIntel.Location = new System.Drawing.Point(-4, 0);
            this.DekleinIntel.Multiline = true;
            this.DekleinIntel.Name = "DekleinIntel";
            this.DekleinIntel.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DekleinIntel.Size = new System.Drawing.Size(447, 599);
            this.DekleinIntel.TabIndex = 0;
            this.DekleinIntel.WordWrap = false;
            // 
            // TenalPage
            // 
            this.TenalPage.Controls.Add(this.TenalIntel);
            this.TenalPage.Location = new System.Drawing.Point(4, 22);
            this.TenalPage.Name = "TenalPage";
            this.TenalPage.Size = new System.Drawing.Size(442, 597);
            this.TenalPage.TabIndex = 4;
            this.TenalPage.Text = "Tnl";
            this.TenalPage.UseVisualStyleBackColor = true;
            // 
            // TenalIntel
            // 
            this.TenalIntel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TenalIntel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TenalIntel.Location = new System.Drawing.Point(-4, 0);
            this.TenalIntel.Multiline = true;
            this.TenalIntel.Name = "TenalIntel";
            this.TenalIntel.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TenalIntel.Size = new System.Drawing.Size(447, 599);
            this.TenalIntel.TabIndex = 0;
            this.TenalIntel.WordWrap = false;
            // 
            // VenalPage
            // 
            this.VenalPage.Controls.Add(this.VenalIntel);
            this.VenalPage.Location = new System.Drawing.Point(4, 22);
            this.VenalPage.Name = "VenalPage";
            this.VenalPage.Size = new System.Drawing.Size(442, 597);
            this.VenalPage.TabIndex = 5;
            this.VenalPage.Text = "Vnl";
            this.VenalPage.UseVisualStyleBackColor = true;
            // 
            // VenalIntel
            // 
            this.VenalIntel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.VenalIntel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VenalIntel.Location = new System.Drawing.Point(-4, 0);
            this.VenalIntel.Multiline = true;
            this.VenalIntel.Name = "VenalIntel";
            this.VenalIntel.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.VenalIntel.Size = new System.Drawing.Size(447, 599);
            this.VenalIntel.TabIndex = 0;
            this.VenalIntel.WordWrap = false;
            // 
            // FadePage
            // 
            this.FadePage.Controls.Add(this.FadeIntel);
            this.FadePage.Location = new System.Drawing.Point(4, 22);
            this.FadePage.Name = "FadePage";
            this.FadePage.Size = new System.Drawing.Size(442, 597);
            this.FadePage.TabIndex = 8;
            this.FadePage.Text = "Fade";
            this.FadePage.UseVisualStyleBackColor = true;
            // 
            // FadeIntel
            // 
            this.FadeIntel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FadeIntel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FadeIntel.Location = new System.Drawing.Point(-4, 0);
            this.FadeIntel.Multiline = true;
            this.FadeIntel.Name = "FadeIntel";
            this.FadeIntel.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.FadeIntel.Size = new System.Drawing.Size(447, 599);
            this.FadeIntel.TabIndex = 0;
            this.FadeIntel.WordWrap = false;
            // 
            // PureBlindPage
            // 
            this.PureBlindPage.Controls.Add(this.PureBlindIntel);
            this.PureBlindPage.Location = new System.Drawing.Point(4, 22);
            this.PureBlindPage.Name = "PureBlindPage";
            this.PureBlindPage.Size = new System.Drawing.Size(442, 597);
            this.PureBlindPage.TabIndex = 9;
            this.PureBlindPage.Text = "PB";
            this.PureBlindPage.UseVisualStyleBackColor = true;
            // 
            // PureBlindIntel
            // 
            this.PureBlindIntel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PureBlindIntel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PureBlindIntel.Location = new System.Drawing.Point(-4, 0);
            this.PureBlindIntel.Multiline = true;
            this.PureBlindIntel.Name = "PureBlindIntel";
            this.PureBlindIntel.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.PureBlindIntel.Size = new System.Drawing.Size(447, 599);
            this.PureBlindIntel.TabIndex = 0;
            this.PureBlindIntel.WordWrap = false;
            // 
            // TributePage
            // 
            this.TributePage.Controls.Add(this.TributeIntel);
            this.TributePage.Controls.Add(this.textBox1);
            this.TributePage.Location = new System.Drawing.Point(4, 22);
            this.TributePage.Name = "TributePage";
            this.TributePage.Size = new System.Drawing.Size(442, 597);
            this.TributePage.TabIndex = 10;
            this.TributePage.Text = "Tri";
            this.TributePage.UseVisualStyleBackColor = true;
            // 
            // TributeIntel
            // 
            this.TributeIntel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TributeIntel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TributeIntel.Location = new System.Drawing.Point(-4, 0);
            this.TributeIntel.Multiline = true;
            this.TributeIntel.Name = "TributeIntel";
            this.TributeIntel.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TributeIntel.Size = new System.Drawing.Size(447, 599);
            this.TributeIntel.TabIndex = 19;
            this.TributeIntel.WordWrap = false;
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(-4, 0);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(447, 599);
            this.textBox1.TabIndex = 0;
            // 
            // ValePage
            // 
            this.ValePage.Controls.Add(this.ValeIntel);
            this.ValePage.Location = new System.Drawing.Point(4, 22);
            this.ValePage.Name = "ValePage";
            this.ValePage.Size = new System.Drawing.Size(442, 597);
            this.ValePage.TabIndex = 11;
            this.ValePage.Text = "Vale";
            this.ValePage.UseVisualStyleBackColor = true;
            // 
            // ValeIntel
            // 
            this.ValeIntel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ValeIntel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ValeIntel.Location = new System.Drawing.Point(-4, 0);
            this.ValeIntel.Multiline = true;
            this.ValeIntel.Name = "ValeIntel";
            this.ValeIntel.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ValeIntel.Size = new System.Drawing.Size(447, 599);
            this.ValeIntel.TabIndex = 0;
            this.ValeIntel.WordWrap = false;
            // 
            // ProvidencePage
            // 
            this.ProvidencePage.Controls.Add(this.ProvidenceIntel);
            this.ProvidencePage.Location = new System.Drawing.Point(4, 22);
            this.ProvidencePage.Name = "ProvidencePage";
            this.ProvidencePage.Size = new System.Drawing.Size(442, 597);
            this.ProvidencePage.TabIndex = 12;
            this.ProvidencePage.Text = "Provi";
            this.ProvidencePage.UseVisualStyleBackColor = true;
            // 
            // ProvidenceIntel
            // 
            this.ProvidenceIntel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ProvidenceIntel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProvidenceIntel.Location = new System.Drawing.Point(-4, 0);
            this.ProvidenceIntel.Multiline = true;
            this.ProvidenceIntel.Name = "ProvidenceIntel";
            this.ProvidenceIntel.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.ProvidenceIntel.Size = new System.Drawing.Size(447, 599);
            this.ProvidenceIntel.TabIndex = 0;
            this.ProvidenceIntel.WordWrap = false;
            // 
            // DelvePage
            // 
            this.DelvePage.Controls.Add(this.DelveIntel);
            this.DelvePage.Location = new System.Drawing.Point(4, 22);
            this.DelvePage.Name = "DelvePage";
            this.DelvePage.Size = new System.Drawing.Size(442, 597);
            this.DelvePage.TabIndex = 13;
            this.DelvePage.Text = "Delve";
            this.DelvePage.UseVisualStyleBackColor = true;
            // 
            // DelveIntel
            // 
            this.DelveIntel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DelveIntel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DelveIntel.Location = new System.Drawing.Point(-4, 0);
            this.DelveIntel.Multiline = true;
            this.DelveIntel.Name = "DelveIntel";
            this.DelveIntel.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.DelveIntel.Size = new System.Drawing.Size(447, 599);
            this.DelveIntel.TabIndex = 0;
            this.DelveIntel.WordWrap = false;
            // 
            // ConfigPage
            // 
            this.ConfigPage.Controls.Add(this.tabControl1);
            this.ConfigPage.Location = new System.Drawing.Point(4, 22);
            this.ConfigPage.Name = "ConfigPage";
            this.ConfigPage.Padding = new System.Windows.Forms.Padding(3);
            this.ConfigPage.Size = new System.Drawing.Size(442, 597);
            this.ConfigPage.TabIndex = 1;
            this.ConfigPage.Text = "Config";
            this.ConfigPage.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.ChannelsPage);
            this.tabControl1.Controls.Add(this.AlertManagementPage);
            this.tabControl1.Controls.Add(this.ListManagementPage);
            this.tabControl1.Controls.Add(this.MiscSettingsPage);
            this.tabControl1.Controls.Add(this.InfoPage);
            this.tabControl1.ItemSize = new System.Drawing.Size(85, 18);
            this.tabControl1.Location = new System.Drawing.Point(6, 6);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(430, 587);
            this.tabControl1.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl1.TabIndex = 18;
            // 
            // ChannelsPage
            // 
            this.ChannelsPage.Controls.Add(this.groupBox1);
            this.ChannelsPage.Controls.Add(this.groupBox3);
            this.ChannelsPage.Location = new System.Drawing.Point(4, 22);
            this.ChannelsPage.Name = "ChannelsPage";
            this.ChannelsPage.Padding = new System.Windows.Forms.Padding(3);
            this.ChannelsPage.Size = new System.Drawing.Size(422, 561);
            this.ChannelsPage.TabIndex = 0;
            this.ChannelsPage.Text = "Channels";
            this.ChannelsPage.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.MonitorGOTG);
            this.groupBox1.Controls.Add(this.MonitorDelve);
            this.groupBox1.Controls.Add(this.MonitorProvidence);
            this.groupBox1.Controls.Add(this.MonitorVale);
            this.groupBox1.Controls.Add(this.MonitorTribute);
            this.groupBox1.Controls.Add(this.MonitorPureBlind);
            this.groupBox1.Controls.Add(this.MonitorFade);
            this.groupBox1.Controls.Add(this.MonitorGameLog);
            this.groupBox1.Controls.Add(this.MonitorBranch);
            this.groupBox1.Controls.Add(this.MonitorDeklein);
            this.groupBox1.Controls.Add(this.MonitorTenal);
            this.groupBox1.Controls.Add(this.MonitorVenal);
            this.groupBox1.Location = new System.Drawing.Point(6, 6);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(202, 273);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Channels to Monitor";
            // 
            // MonitorGOTG
            // 
            this.MonitorGOTG.AutoSize = true;
            this.MonitorGOTG.Checked = true;
            this.MonitorGOTG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MonitorGOTG.Location = new System.Drawing.Point(7, 231);
            this.MonitorGOTG.Name = "MonitorGOTG";
            this.MonitorGOTG.Size = new System.Drawing.Size(48, 16);
            this.MonitorGOTG.TabIndex = 16;
            this.MonitorGOTG.Text = "GOTG";
            this.MonitorGOTG.UseVisualStyleBackColor = true;
            this.MonitorGOTG.CheckedChanged += new System.EventHandler(this.MonitorGOTG_CheckedChanged);
            // 
            // MonitorDelve
            // 
            this.MonitorDelve.AutoSize = true;
            this.MonitorDelve.Checked = true;
            this.MonitorDelve.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MonitorDelve.Location = new System.Drawing.Point(6, 208);
            this.MonitorDelve.Name = "MonitorDelve";
            this.MonitorDelve.Size = new System.Drawing.Size(54, 16);
            this.MonitorDelve.TabIndex = 15;
            this.MonitorDelve.Text = "Delve";
            this.MonitorDelve.UseVisualStyleBackColor = true;
            this.MonitorDelve.CheckedChanged += new System.EventHandler(this.MonitorDelve_CheckedChanged);
            // 
            // MonitorProvidence
            // 
            this.MonitorProvidence.AutoSize = true;
            this.MonitorProvidence.Checked = true;
            this.MonitorProvidence.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MonitorProvidence.Location = new System.Drawing.Point(6, 186);
            this.MonitorProvidence.Name = "MonitorProvidence";
            this.MonitorProvidence.Size = new System.Drawing.Size(84, 16);
            this.MonitorProvidence.TabIndex = 14;
            this.MonitorProvidence.Text = "Providence";
            this.MonitorProvidence.UseVisualStyleBackColor = true;
            this.MonitorProvidence.CheckedChanged += new System.EventHandler(this.MonitorProvidence_CheckedChanged);
            // 
            // MonitorVale
            // 
            this.MonitorVale.AutoSize = true;
            this.MonitorVale.Checked = true;
            this.MonitorVale.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MonitorVale.Location = new System.Drawing.Point(6, 165);
            this.MonitorVale.Name = "MonitorVale";
            this.MonitorVale.Size = new System.Drawing.Size(132, 16);
            this.MonitorVale.TabIndex = 13;
            this.MonitorVale.Text = "Vale of the Silent";
            this.MonitorVale.UseVisualStyleBackColor = true;
            this.MonitorVale.CheckedChanged += new System.EventHandler(this.MonitorVale_CheckedChanged);
            // 
            // MonitorTribute
            // 
            this.MonitorTribute.AutoSize = true;
            this.MonitorTribute.Checked = true;
            this.MonitorTribute.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MonitorTribute.Location = new System.Drawing.Point(6, 144);
            this.MonitorTribute.Name = "MonitorTribute";
            this.MonitorTribute.Size = new System.Drawing.Size(66, 16);
            this.MonitorTribute.TabIndex = 12;
            this.MonitorTribute.Text = "Tribute";
            this.MonitorTribute.UseVisualStyleBackColor = true;
            this.MonitorTribute.CheckedChanged += new System.EventHandler(this.MonitorTribute_CheckedChanged);
            // 
            // MonitorPureBlind
            // 
            this.MonitorPureBlind.AutoSize = true;
            this.MonitorPureBlind.Checked = true;
            this.MonitorPureBlind.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MonitorPureBlind.Location = new System.Drawing.Point(6, 123);
            this.MonitorPureBlind.Name = "MonitorPureBlind";
            this.MonitorPureBlind.Size = new System.Drawing.Size(84, 16);
            this.MonitorPureBlind.TabIndex = 11;
            this.MonitorPureBlind.Text = "Pure Blind";
            this.MonitorPureBlind.UseVisualStyleBackColor = true;
            this.MonitorPureBlind.CheckedChanged += new System.EventHandler(this.MonitorPureBlind_CheckedChanged);
            // 
            // MonitorFade
            // 
            this.MonitorFade.AutoSize = true;
            this.MonitorFade.Checked = true;
            this.MonitorFade.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MonitorFade.Location = new System.Drawing.Point(6, 102);
            this.MonitorFade.Name = "MonitorFade";
            this.MonitorFade.Size = new System.Drawing.Size(48, 16);
            this.MonitorFade.TabIndex = 10;
            this.MonitorFade.Text = "Fade";
            this.MonitorFade.UseVisualStyleBackColor = true;
            this.MonitorFade.CheckedChanged += new System.EventHandler(this.MonitorFade_CheckedChanged);
            // 
            // MonitorGameLog
            // 
            this.MonitorGameLog.AutoSize = true;
            this.MonitorGameLog.Checked = true;
            this.MonitorGameLog.CheckState = System.Windows.Forms.CheckState.Checked;
            this.MonitorGameLog.Location = new System.Drawing.Point(6, 253);
            this.MonitorGameLog.Name = "MonitorGameLog";
            this.MonitorGameLog.Size = new System.Drawing.Size(72, 16);
            this.MonitorGameLog.TabIndex = 9;
            this.MonitorGameLog.Text = "Game Log";
            this.MonitorGameLog.UseVisualStyleBackColor = true;
            this.MonitorGameLog.CheckedChanged += new System.EventHandler(this.MonitorGameLog_CheckedChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.AlertGOTG);
            this.groupBox3.Controls.Add(this.AlertDelve);
            this.groupBox3.Controls.Add(this.AlertProvidence);
            this.groupBox3.Controls.Add(this.AlertVale);
            this.groupBox3.Controls.Add(this.AlertTribute);
            this.groupBox3.Controls.Add(this.AlertPureBlind);
            this.groupBox3.Controls.Add(this.AlertFade);
            this.groupBox3.Controls.Add(this.AlertVenal);
            this.groupBox3.Controls.Add(this.AlertTenal);
            this.groupBox3.Controls.Add(this.AlertDeklein);
            this.groupBox3.Controls.Add(this.AlertBranch);
            this.groupBox3.Location = new System.Drawing.Point(214, 6);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(202, 285);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Alert Auto-focus Channels";
            // 
            // AlertGOTG
            // 
            this.AlertGOTG.AutoSize = true;
            this.AlertGOTG.Checked = true;
            this.AlertGOTG.CheckState = System.Windows.Forms.CheckState.Checked;
            this.AlertGOTG.Location = new System.Drawing.Point(6, 230);
            this.AlertGOTG.Name = "AlertGOTG";
            this.AlertGOTG.Size = new System.Drawing.Size(48, 16);
            this.AlertGOTG.TabIndex = 10;
            this.AlertGOTG.Text = "GOTG";
            this.AlertGOTG.UseVisualStyleBackColor = true;
            this.AlertGOTG.CheckedChanged += new System.EventHandler(this.AlertGOTG_CheckedChanged);
            // 
            // AlertDelve
            // 
            this.AlertDelve.AutoSize = true;
            this.AlertDelve.Location = new System.Drawing.Point(6, 208);
            this.AlertDelve.Name = "AlertDelve";
            this.AlertDelve.Size = new System.Drawing.Size(54, 16);
            this.AlertDelve.TabIndex = 9;
            this.AlertDelve.Text = "Delve";
            this.AlertDelve.UseVisualStyleBackColor = true;
            this.AlertDelve.CheckedChanged += new System.EventHandler(this.AlertDelve_CheckedChanged);
            // 
            // AlertProvidence
            // 
            this.AlertProvidence.AutoSize = true;
            this.AlertProvidence.Location = new System.Drawing.Point(6, 186);
            this.AlertProvidence.Name = "AlertProvidence";
            this.AlertProvidence.Size = new System.Drawing.Size(84, 16);
            this.AlertProvidence.TabIndex = 8;
            this.AlertProvidence.Text = "Providence";
            this.AlertProvidence.UseVisualStyleBackColor = true;
            this.AlertProvidence.CheckedChanged += new System.EventHandler(this.AlertProvidence_CheckedChanged);
            // 
            // AlertVale
            // 
            this.AlertVale.AutoSize = true;
            this.AlertVale.Location = new System.Drawing.Point(6, 165);
            this.AlertVale.Name = "AlertVale";
            this.AlertVale.Size = new System.Drawing.Size(132, 16);
            this.AlertVale.TabIndex = 7;
            this.AlertVale.Text = "Vale of the Silent";
            this.AlertVale.UseVisualStyleBackColor = true;
            this.AlertVale.CheckedChanged += new System.EventHandler(this.AlertVale_CheckedChanged);
            // 
            // AlertTribute
            // 
            this.AlertTribute.AutoSize = true;
            this.AlertTribute.Location = new System.Drawing.Point(6, 144);
            this.AlertTribute.Name = "AlertTribute";
            this.AlertTribute.Size = new System.Drawing.Size(66, 16);
            this.AlertTribute.TabIndex = 6;
            this.AlertTribute.Text = "Tribute";
            this.AlertTribute.UseVisualStyleBackColor = true;
            this.AlertTribute.CheckedChanged += new System.EventHandler(this.AlertTribute_CheckedChanged);
            // 
            // AlertPureBlind
            // 
            this.AlertPureBlind.AutoSize = true;
            this.AlertPureBlind.Location = new System.Drawing.Point(6, 123);
            this.AlertPureBlind.Name = "AlertPureBlind";
            this.AlertPureBlind.Size = new System.Drawing.Size(84, 16);
            this.AlertPureBlind.TabIndex = 5;
            this.AlertPureBlind.Text = "Pure Blind";
            this.AlertPureBlind.UseVisualStyleBackColor = true;
            this.AlertPureBlind.CheckedChanged += new System.EventHandler(this.AlertPureBlind_CheckedChanged);
            // 
            // AlertFade
            // 
            this.AlertFade.AutoSize = true;
            this.AlertFade.Location = new System.Drawing.Point(6, 102);
            this.AlertFade.Name = "AlertFade";
            this.AlertFade.Size = new System.Drawing.Size(48, 16);
            this.AlertFade.TabIndex = 4;
            this.AlertFade.Text = "Fade";
            this.AlertFade.UseVisualStyleBackColor = true;
            this.AlertFade.CheckedChanged += new System.EventHandler(this.AlertFade_CheckedChanged);
            // 
            // AlertVenal
            // 
            this.AlertVenal.AutoSize = true;
            this.AlertVenal.Location = new System.Drawing.Point(6, 81);
            this.AlertVenal.Name = "AlertVenal";
            this.AlertVenal.Size = new System.Drawing.Size(54, 16);
            this.AlertVenal.TabIndex = 3;
            this.AlertVenal.Text = "Venal";
            this.AlertVenal.UseVisualStyleBackColor = true;
            this.AlertVenal.CheckedChanged += new System.EventHandler(this.AlertVenal_CheckedChanged);
            // 
            // AlertTenal
            // 
            this.AlertTenal.AutoSize = true;
            this.AlertTenal.Location = new System.Drawing.Point(6, 60);
            this.AlertTenal.Name = "AlertTenal";
            this.AlertTenal.Size = new System.Drawing.Size(54, 16);
            this.AlertTenal.TabIndex = 2;
            this.AlertTenal.Text = "Tenal";
            this.AlertTenal.UseVisualStyleBackColor = true;
            this.AlertTenal.CheckedChanged += new System.EventHandler(this.AlertTenal_CheckedChanged);
            // 
            // AlertDeklein
            // 
            this.AlertDeklein.AutoSize = true;
            this.AlertDeklein.Location = new System.Drawing.Point(6, 39);
            this.AlertDeklein.Name = "AlertDeklein";
            this.AlertDeklein.Size = new System.Drawing.Size(66, 16);
            this.AlertDeklein.TabIndex = 1;
            this.AlertDeklein.Text = "Deklein";
            this.AlertDeklein.UseVisualStyleBackColor = true;
            this.AlertDeklein.CheckedChanged += new System.EventHandler(this.AlertDeklein_CheckedChanged);
            // 
            // AlertBranch
            // 
            this.AlertBranch.AutoSize = true;
            this.AlertBranch.Location = new System.Drawing.Point(6, 18);
            this.AlertBranch.Name = "AlertBranch";
            this.AlertBranch.Size = new System.Drawing.Size(60, 16);
            this.AlertBranch.TabIndex = 0;
            this.AlertBranch.Text = "Branch";
            this.AlertBranch.UseVisualStyleBackColor = true;
            this.AlertBranch.CheckedChanged += new System.EventHandler(this.AlertBranch_CheckedChanged);
            // 
            // AlertManagementPage
            // 
            this.AlertManagementPage.Controls.Add(this.AddCustomAlertGroup);
            this.AlertManagementPage.Controls.Add(this.AddRangeAlertGroup);
            this.AlertManagementPage.Controls.Add(this.groupBox9);
            this.AlertManagementPage.Location = new System.Drawing.Point(4, 22);
            this.AlertManagementPage.Name = "AlertManagementPage";
            this.AlertManagementPage.Padding = new System.Windows.Forms.Padding(3);
            this.AlertManagementPage.Size = new System.Drawing.Size(422, 561);
            this.AlertManagementPage.TabIndex = 1;
            this.AlertManagementPage.Text = "Alerts";
            this.AlertManagementPage.UseVisualStyleBackColor = true;
            // 
            // AddCustomAlertGroup
            // 
            this.AddCustomAlertGroup.Controls.Add(this.SaveCustomAlert);
            this.AddCustomAlertGroup.Controls.Add(this.PlayCustomTextAlertSound);
            this.AddCustomAlertGroup.Controls.Add(this.label1);
            this.AddCustomAlertGroup.Controls.Add(this.CustomTextAlertSound);
            this.AddCustomAlertGroup.Controls.Add(this.label7);
            this.AddCustomAlertGroup.Controls.Add(this.label5);
            this.AddCustomAlertGroup.Controls.Add(this.CustomAlertRepeatInterval);
            this.AddCustomAlertGroup.Controls.Add(this.NewCustomAlertText);
            this.AddCustomAlertGroup.Controls.Add(this.AddNewCustomAlert);
            this.AddCustomAlertGroup.Location = new System.Drawing.Point(6, 393);
            this.AddCustomAlertGroup.Name = "AddCustomAlertGroup";
            this.AddCustomAlertGroup.Size = new System.Drawing.Size(410, 72);
            this.AddCustomAlertGroup.TabIndex = 9;
            this.AddCustomAlertGroup.TabStop = false;
            this.AddCustomAlertGroup.Text = "Add Custom Text Alert";
            // 
            // SaveCustomAlert
            // 
            this.SaveCustomAlert.Location = new System.Drawing.Point(356, 42);
            this.SaveCustomAlert.Name = "SaveCustomAlert";
            this.SaveCustomAlert.Size = new System.Drawing.Size(48, 21);
            this.SaveCustomAlert.TabIndex = 18;
            this.SaveCustomAlert.Text = "Save";
            this.SaveCustomAlert.UseVisualStyleBackColor = true;
            this.SaveCustomAlert.Visible = false;
            this.SaveCustomAlert.Click += new System.EventHandler(this.SaveCustomAlert_Click);
            // 
            // PlayCustomTextAlertSound
            // 
            this.PlayCustomTextAlertSound.AutoSize = true;
            this.PlayCustomTextAlertSound.Location = new System.Drawing.Point(6, 47);
            this.PlayCustomTextAlertSound.Name = "PlayCustomTextAlertSound";
            this.PlayCustomTextAlertSound.Size = new System.Drawing.Size(29, 12);
            this.PlayCustomTextAlertSound.TabIndex = 8;
            this.PlayCustomTextAlertSound.TabStop = true;
            this.PlayCustomTextAlertSound.Text = "Play";
            this.PlayCustomTextAlertSound.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PlayCustomTextAlertSound_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "Trigger:";
            // 
            // CustomTextAlertSound
            // 
            this.CustomTextAlertSound.FormattingEnabled = true;
            this.CustomTextAlertSound.Location = new System.Drawing.Point(36, 44);
            this.CustomTextAlertSound.Name = "CustomTextAlertSound";
            this.CustomTextAlertSound.Size = new System.Drawing.Size(314, 20);
            this.CustomTextAlertSound.TabIndex = 6;
            this.CustomTextAlertSound.SelectedIndexChanged += new System.EventHandler(this.CustomTextAlertSound_SelectedIndexChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(374, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(29, 12);
            this.label7.TabIndex = 5;
            this.label7.Text = "secs";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(254, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "Trigger every";
            // 
            // CustomAlertRepeatInterval
            // 
            this.CustomAlertRepeatInterval.Location = new System.Drawing.Point(329, 18);
            this.CustomAlertRepeatInterval.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.CustomAlertRepeatInterval.Name = "CustomAlertRepeatInterval";
            this.CustomAlertRepeatInterval.Size = new System.Drawing.Size(39, 21);
            this.CustomAlertRepeatInterval.TabIndex = 3;
            // 
            // NewCustomAlertText
            // 
            this.NewCustomAlertText.Location = new System.Drawing.Point(49, 18);
            this.NewCustomAlertText.Name = "NewCustomAlertText";
            this.NewCustomAlertText.Size = new System.Drawing.Size(199, 21);
            this.NewCustomAlertText.TabIndex = 2;
            // 
            // AddNewCustomAlert
            // 
            this.AddNewCustomAlert.Location = new System.Drawing.Point(356, 42);
            this.AddNewCustomAlert.Name = "AddNewCustomAlert";
            this.AddNewCustomAlert.Size = new System.Drawing.Size(48, 21);
            this.AddNewCustomAlert.TabIndex = 1;
            this.AddNewCustomAlert.Text = "Add";
            this.AddNewCustomAlert.UseVisualStyleBackColor = true;
            this.AddNewCustomAlert.Click += new System.EventHandler(this.AddNewCustomAlert_Click);
            // 
            // AddRangeAlertGroup
            // 
            this.AddRangeAlertGroup.Controls.Add(this.SaveRangeAlert);
            this.AddRangeAlertGroup.Controls.Add(this.label8);
            this.AddRangeAlertGroup.Controls.Add(this.NewRangeAlertType);
            this.AddRangeAlertGroup.Controls.Add(this.PlayRangeAlertSound);
            this.AddRangeAlertGroup.Controls.Add(this.AddNewRangeAlert);
            this.AddRangeAlertGroup.Controls.Add(this.RangeAlertSound);
            this.AddRangeAlertGroup.Controls.Add(this.LowerAlertRange);
            this.AddRangeAlertGroup.Controls.Add(this.LowerLimitOperator);
            this.AddRangeAlertGroup.Controls.Add(this.label2);
            this.AddRangeAlertGroup.Controls.Add(this.UpperLimitOperator);
            this.AddRangeAlertGroup.Controls.Add(this.UpperAlertRange);
            this.AddRangeAlertGroup.Controls.Add(this.label6);
            this.AddRangeAlertGroup.Controls.Add(this.RangeAlertSystem);
            this.AddRangeAlertGroup.Controls.Add(this.RangeAlertCharacter);
            this.AddRangeAlertGroup.Controls.Add(this.comboBox1);
            this.AddRangeAlertGroup.Location = new System.Drawing.Point(6, 293);
            this.AddRangeAlertGroup.Name = "AddRangeAlertGroup";
            this.AddRangeAlertGroup.Size = new System.Drawing.Size(410, 95);
            this.AddRangeAlertGroup.TabIndex = 8;
            this.AddRangeAlertGroup.TabStop = false;
            this.AddRangeAlertGroup.Text = "Add Range Based Alert";
            // 
            // SaveRangeAlert
            // 
            this.SaveRangeAlert.Location = new System.Drawing.Point(355, 66);
            this.SaveRangeAlert.Name = "SaveRangeAlert";
            this.SaveRangeAlert.Size = new System.Drawing.Size(48, 21);
            this.SaveRangeAlert.TabIndex = 18;
            this.SaveRangeAlert.Text = "Save";
            this.SaveRangeAlert.UseVisualStyleBackColor = true;
            this.SaveRangeAlert.Visible = false;
            this.SaveRangeAlert.Click += new System.EventHandler(this.SaveRangeAlert_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 45);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(47, 12);
            this.label8.TabIndex = 21;
            this.label8.Text = "Select:";
            // 
            // NewRangeAlertType
            // 
            this.NewRangeAlertType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.NewRangeAlertType.FormattingEnabled = true;
            this.NewRangeAlertType.Items.AddRange(new object[] {
            "Home System",
            "Any Character",
            "Single System",
            "Single Character"});
            this.NewRangeAlertType.Location = new System.Drawing.Point(298, 18);
            this.NewRangeAlertType.Name = "NewRangeAlertType";
            this.NewRangeAlertType.Size = new System.Drawing.Size(105, 20);
            this.NewRangeAlertType.TabIndex = 20;
            this.NewRangeAlertType.TextChanged += new System.EventHandler(this.NewRangeAlertType_TextChanged);
            // 
            // PlayRangeAlertSound
            // 
            this.PlayRangeAlertSound.AutoSize = true;
            this.PlayRangeAlertSound.Location = new System.Drawing.Point(6, 70);
            this.PlayRangeAlertSound.Name = "PlayRangeAlertSound";
            this.PlayRangeAlertSound.Size = new System.Drawing.Size(29, 12);
            this.PlayRangeAlertSound.TabIndex = 9;
            this.PlayRangeAlertSound.TabStop = true;
            this.PlayRangeAlertSound.Text = "Play";
            this.PlayRangeAlertSound.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PlayRangeAlertSound_LinkClicked);
            // 
            // AddNewRangeAlert
            // 
            this.AddNewRangeAlert.Location = new System.Drawing.Point(355, 66);
            this.AddNewRangeAlert.Name = "AddNewRangeAlert";
            this.AddNewRangeAlert.Size = new System.Drawing.Size(48, 21);
            this.AddNewRangeAlert.TabIndex = 0;
            this.AddNewRangeAlert.Text = "Add";
            this.AddNewRangeAlert.UseVisualStyleBackColor = true;
            this.AddNewRangeAlert.Click += new System.EventHandler(this.AddNewRangeAlert_Click);
            // 
            // RangeAlertSound
            // 
            this.RangeAlertSound.FormattingEnabled = true;
            this.RangeAlertSound.Location = new System.Drawing.Point(36, 67);
            this.RangeAlertSound.Name = "RangeAlertSound";
            this.RangeAlertSound.Size = new System.Drawing.Size(314, 20);
            this.RangeAlertSound.TabIndex = 10;
            this.RangeAlertSound.SelectedIndexChanged += new System.EventHandler(this.RangeAlertSound_SelectedIndexChanged);
            // 
            // LowerAlertRange
            // 
            this.LowerAlertRange.Enabled = false;
            this.LowerAlertRange.Location = new System.Drawing.Point(191, 18);
            this.LowerAlertRange.Name = "LowerAlertRange";
            this.LowerAlertRange.Size = new System.Drawing.Size(35, 21);
            this.LowerAlertRange.TabIndex = 11;
            // 
            // LowerLimitOperator
            // 
            this.LowerLimitOperator.Enabled = false;
            this.LowerLimitOperator.FormattingEnabled = true;
            this.LowerLimitOperator.Items.AddRange(new object[] {
            ">",
            ">="});
            this.LowerLimitOperator.Location = new System.Drawing.Point(151, 18);
            this.LowerLimitOperator.Name = "LowerLimitOperator";
            this.LowerLimitOperator.Size = new System.Drawing.Size(34, 20);
            this.LowerLimitOperator.TabIndex = 10;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(90, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "jumps and";
            // 
            // UpperLimitOperator
            // 
            this.UpperLimitOperator.FormattingEnabled = true;
            this.UpperLimitOperator.Items.AddRange(new object[] {
            "=",
            "<="});
            this.UpperLimitOperator.Location = new System.Drawing.Point(9, 18);
            this.UpperLimitOperator.Name = "UpperLimitOperator";
            this.UpperLimitOperator.Size = new System.Drawing.Size(34, 20);
            this.UpperLimitOperator.TabIndex = 8;
            this.UpperLimitOperator.SelectedIndexChanged += new System.EventHandler(this.UpperLimitOperator_SelectedIndexChanged);
            // 
            // UpperAlertRange
            // 
            this.UpperAlertRange.Location = new System.Drawing.Point(49, 18);
            this.UpperAlertRange.Name = "UpperAlertRange";
            this.UpperAlertRange.Size = new System.Drawing.Size(35, 21);
            this.UpperAlertRange.TabIndex = 4;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(232, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 12);
            this.label6.TabIndex = 5;
            this.label6.Text = "jumps from:";
            // 
            // RangeAlertSystem
            // 
            this.RangeAlertSystem.Enabled = false;
            this.RangeAlertSystem.FormattingEnabled = true;
            this.RangeAlertSystem.Location = new System.Drawing.Point(46, 42);
            this.RangeAlertSystem.Name = "RangeAlertSystem";
            this.RangeAlertSystem.Size = new System.Drawing.Size(357, 20);
            this.RangeAlertSystem.TabIndex = 6;
            this.RangeAlertSystem.Visible = false;
            this.RangeAlertSystem.Leave += new System.EventHandler(this.RangeAlertSystem_Leave);
            // 
            // RangeAlertCharacter
            // 
            this.RangeAlertCharacter.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.RangeAlertCharacter.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.RangeAlertCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.RangeAlertCharacter.Enabled = false;
            this.RangeAlertCharacter.FormattingEnabled = true;
            this.RangeAlertCharacter.Location = new System.Drawing.Point(46, 42);
            this.RangeAlertCharacter.Name = "RangeAlertCharacter";
            this.RangeAlertCharacter.Size = new System.Drawing.Size(357, 20);
            this.RangeAlertCharacter.TabIndex = 18;
            this.RangeAlertCharacter.Visible = false;
            // 
            // comboBox1
            // 
            this.comboBox1.Enabled = false;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(46, 42);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(357, 20);
            this.comboBox1.TabIndex = 22;
            this.comboBox1.Text = "N/A";
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.CancelEditSelectedItem);
            this.groupBox9.Controls.Add(this.EditSelectedItem);
            this.groupBox9.Controls.Add(this.MoveAlertDown);
            this.groupBox9.Controls.Add(this.MoveAlertUp);
            this.groupBox9.Controls.Add(this.PlayAlertSound);
            this.groupBox9.Controls.Add(this.RemoveSelectedItem);
            this.groupBox9.Controls.Add(this.AlertList);
            this.groupBox9.Location = new System.Drawing.Point(6, 6);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(410, 282);
            this.groupBox9.TabIndex = 17;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "Alert List";
            // 
            // CancelEditSelectedItem
            // 
            this.CancelEditSelectedItem.AutoSize = true;
            this.CancelEditSelectedItem.Location = new System.Drawing.Point(148, 261);
            this.CancelEditSelectedItem.Name = "CancelEditSelectedItem";
            this.CancelEditSelectedItem.Size = new System.Drawing.Size(71, 12);
            this.CancelEditSelectedItem.TabIndex = 24;
            this.CancelEditSelectedItem.TabStop = true;
            this.CancelEditSelectedItem.Text = "Cancel Edit";
            this.CancelEditSelectedItem.Visible = false;
            this.CancelEditSelectedItem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.CancelEditSelectedItem_LinkClicked);
            // 
            // EditSelectedItem
            // 
            this.EditSelectedItem.AutoSize = true;
            this.EditSelectedItem.Location = new System.Drawing.Point(144, 261);
            this.EditSelectedItem.Name = "EditSelectedItem";
            this.EditSelectedItem.Size = new System.Drawing.Size(83, 12);
            this.EditSelectedItem.TabIndex = 23;
            this.EditSelectedItem.TabStop = true;
            this.EditSelectedItem.Text = "Edit Selected";
            this.EditSelectedItem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.EditSelectedItem_LinkClicked);
            // 
            // MoveAlertDown
            // 
            this.MoveAlertDown.Font = new System.Drawing.Font("Wingdings 3", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.MoveAlertDown.Location = new System.Drawing.Point(381, 143);
            this.MoveAlertDown.Name = "MoveAlertDown";
            this.MoveAlertDown.Size = new System.Drawing.Size(23, 21);
            this.MoveAlertDown.TabIndex = 22;
            this.MoveAlertDown.Text = "q";
            this.MoveAlertDown.UseVisualStyleBackColor = true;
            this.MoveAlertDown.Click += new System.EventHandler(this.MoveAlertDown_Click);
            // 
            // MoveAlertUp
            // 
            this.MoveAlertUp.Font = new System.Drawing.Font("Wingdings 3", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(2)));
            this.MoveAlertUp.Location = new System.Drawing.Point(381, 116);
            this.MoveAlertUp.Name = "MoveAlertUp";
            this.MoveAlertUp.Size = new System.Drawing.Size(23, 21);
            this.MoveAlertUp.TabIndex = 21;
            this.MoveAlertUp.Text = "p";
            this.MoveAlertUp.UseVisualStyleBackColor = true;
            this.MoveAlertUp.Click += new System.EventHandler(this.MoveAlertUp_Click);
            // 
            // PlayAlertSound
            // 
            this.PlayAlertSound.AutoSize = true;
            this.PlayAlertSound.Enabled = false;
            this.PlayAlertSound.Location = new System.Drawing.Point(3, 261);
            this.PlayAlertSound.Name = "PlayAlertSound";
            this.PlayAlertSound.Size = new System.Drawing.Size(83, 12);
            this.PlayAlertSound.TabIndex = 20;
            this.PlayAlertSound.TabStop = true;
            this.PlayAlertSound.Text = "Play Selected";
            this.PlayAlertSound.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PlayAlertSound_LinkClicked);
            // 
            // RemoveSelectedItem
            // 
            this.RemoveSelectedItem.AutoSize = true;
            this.RemoveSelectedItem.Enabled = false;
            this.RemoveSelectedItem.Location = new System.Drawing.Point(283, 261);
            this.RemoveSelectedItem.Name = "RemoveSelectedItem";
            this.RemoveSelectedItem.Size = new System.Drawing.Size(95, 12);
            this.RemoveSelectedItem.TabIndex = 19;
            this.RemoveSelectedItem.TabStop = true;
            this.RemoveSelectedItem.Text = "Remove Selected";
            this.RemoveSelectedItem.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.RemoveSelectedItem_LinkClicked);
            // 
            // AlertList
            // 
            this.AlertList.FormattingEnabled = true;
            this.AlertList.Location = new System.Drawing.Point(6, 18);
            this.AlertList.Name = "AlertList";
            this.AlertList.Size = new System.Drawing.Size(369, 228);
            this.AlertList.TabIndex = 18;
            this.AlertList.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.AlertList_ItemCheck);
            this.AlertList.SelectedIndexChanged += new System.EventHandler(this.AlertList_SelectedIndexChanged);
            this.AlertList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AlertList_KeyDown);
            // 
            // ListManagementPage
            // 
            this.ListManagementPage.Controls.Add(this.groupBox17);
            this.ListManagementPage.Controls.Add(this.groupBox7);
            this.ListManagementPage.Controls.Add(this.groupBox6);
            this.ListManagementPage.Location = new System.Drawing.Point(4, 22);
            this.ListManagementPage.Name = "ListManagementPage";
            this.ListManagementPage.Size = new System.Drawing.Size(422, 561);
            this.ListManagementPage.TabIndex = 2;
            this.ListManagementPage.Text = "Lists";
            this.ListManagementPage.UseVisualStyleBackColor = true;
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.NewLinkedCharacter);
            this.groupBox17.Controls.Add(this.RemoveLinkedCharacter);
            this.groupBox17.Controls.Add(this.CharacterList);
            this.groupBox17.Controls.Add(this.AddLinkedCharacter);
            this.groupBox17.Location = new System.Drawing.Point(6, 236);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(410, 225);
            this.groupBox17.TabIndex = 20;
            this.groupBox17.TabStop = false;
            this.groupBox17.Text = "Linked Characters";
            // 
            // NewLinkedCharacter
            // 
            this.NewLinkedCharacter.Location = new System.Drawing.Point(7, 198);
            this.NewLinkedCharacter.Name = "NewLinkedCharacter";
            this.NewLinkedCharacter.Size = new System.Drawing.Size(134, 21);
            this.NewLinkedCharacter.TabIndex = 3;
            // 
            // RemoveLinkedCharacter
            // 
            this.RemoveLinkedCharacter.Location = new System.Drawing.Point(176, 196);
            this.RemoveLinkedCharacter.Name = "RemoveLinkedCharacter";
            this.RemoveLinkedCharacter.Size = new System.Drawing.Size(23, 21);
            this.RemoveLinkedCharacter.TabIndex = 2;
            this.RemoveLinkedCharacter.Text = "-";
            this.RemoveLinkedCharacter.UseVisualStyleBackColor = true;
            this.RemoveLinkedCharacter.Click += new System.EventHandler(this.RemoveLinkedCharacter_Click);
            // 
            // CharacterList
            // 
            this.CharacterList.FormattingEnabled = true;
            this.CharacterList.ItemHeight = 12;
            this.CharacterList.Location = new System.Drawing.Point(7, 18);
            this.CharacterList.Name = "CharacterList";
            this.CharacterList.Size = new System.Drawing.Size(192, 172);
            this.CharacterList.Sorted = true;
            this.CharacterList.TabIndex = 0;
            // 
            // AddLinkedCharacter
            // 
            this.AddLinkedCharacter.Location = new System.Drawing.Point(147, 196);
            this.AddLinkedCharacter.Name = "AddLinkedCharacter";
            this.AddLinkedCharacter.Size = new System.Drawing.Size(23, 21);
            this.AddLinkedCharacter.TabIndex = 1;
            this.AddLinkedCharacter.Text = "+";
            this.AddLinkedCharacter.UseVisualStyleBackColor = true;
            this.AddLinkedCharacter.Click += new System.EventHandler(this.AddLinkedCharacter_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.IgnoreSystemList);
            this.groupBox7.Controls.Add(this.RemoveIgnoreSystem);
            this.groupBox7.Controls.Add(this.NewIgnoreSystem);
            this.groupBox7.Controls.Add(this.AddIgnoreSystem);
            this.groupBox7.Location = new System.Drawing.Point(211, 6);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(205, 225);
            this.groupBox7.TabIndex = 19;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Ignore Systems";
            // 
            // IgnoreSystemList
            // 
            this.IgnoreSystemList.FormattingEnabled = true;
            this.IgnoreSystemList.ItemHeight = 12;
            this.IgnoreSystemList.Location = new System.Drawing.Point(7, 18);
            this.IgnoreSystemList.Name = "IgnoreSystemList";
            this.IgnoreSystemList.Size = new System.Drawing.Size(192, 172);
            this.IgnoreSystemList.TabIndex = 25;
            // 
            // RemoveIgnoreSystem
            // 
            this.RemoveIgnoreSystem.Location = new System.Drawing.Point(176, 196);
            this.RemoveIgnoreSystem.Name = "RemoveIgnoreSystem";
            this.RemoveIgnoreSystem.Size = new System.Drawing.Size(23, 21);
            this.RemoveIgnoreSystem.TabIndex = 23;
            this.RemoveIgnoreSystem.Text = "-";
            this.RemoveIgnoreSystem.UseVisualStyleBackColor = true;
            this.RemoveIgnoreSystem.Click += new System.EventHandler(this.RemoveIgnoreSystem_Click);
            // 
            // NewIgnoreSystem
            // 
            this.NewIgnoreSystem.FormattingEnabled = true;
            this.NewIgnoreSystem.Location = new System.Drawing.Point(7, 198);
            this.NewIgnoreSystem.Name = "NewIgnoreSystem";
            this.NewIgnoreSystem.Size = new System.Drawing.Size(134, 20);
            this.NewIgnoreSystem.TabIndex = 24;
            this.NewIgnoreSystem.Leave += new System.EventHandler(this.NewIgnoreSystem_Leave);
            // 
            // AddIgnoreSystem
            // 
            this.AddIgnoreSystem.Location = new System.Drawing.Point(147, 196);
            this.AddIgnoreSystem.Name = "AddIgnoreSystem";
            this.AddIgnoreSystem.Size = new System.Drawing.Size(23, 21);
            this.AddIgnoreSystem.TabIndex = 22;
            this.AddIgnoreSystem.Text = "+";
            this.AddIgnoreSystem.UseVisualStyleBackColor = true;
            this.AddIgnoreSystem.Click += new System.EventHandler(this.AddIgnoreSystem_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.IgnoreTextList);
            this.groupBox6.Controls.Add(this.RemoveIgnoreText);
            this.groupBox6.Controls.Add(this.NewIgnoreText);
            this.groupBox6.Controls.Add(this.AddIgnoreText);
            this.groupBox6.Location = new System.Drawing.Point(6, 6);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(205, 225);
            this.groupBox6.TabIndex = 18;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Ignore Text";
            // 
            // IgnoreTextList
            // 
            this.IgnoreTextList.FormattingEnabled = true;
            this.IgnoreTextList.ItemHeight = 12;
            this.IgnoreTextList.Location = new System.Drawing.Point(7, 18);
            this.IgnoreTextList.Name = "IgnoreTextList";
            this.IgnoreTextList.Size = new System.Drawing.Size(192, 172);
            this.IgnoreTextList.TabIndex = 0;
            // 
            // RemoveIgnoreText
            // 
            this.RemoveIgnoreText.Location = new System.Drawing.Point(176, 196);
            this.RemoveIgnoreText.Name = "RemoveIgnoreText";
            this.RemoveIgnoreText.Size = new System.Drawing.Size(23, 21);
            this.RemoveIgnoreText.TabIndex = 21;
            this.RemoveIgnoreText.Text = "-";
            this.RemoveIgnoreText.UseVisualStyleBackColor = true;
            this.RemoveIgnoreText.Click += new System.EventHandler(this.RemoveIgnoreText_Click);
            // 
            // NewIgnoreText
            // 
            this.NewIgnoreText.Location = new System.Drawing.Point(7, 198);
            this.NewIgnoreText.Name = "NewIgnoreText";
            this.NewIgnoreText.Size = new System.Drawing.Size(134, 21);
            this.NewIgnoreText.TabIndex = 25;
            // 
            // AddIgnoreText
            // 
            this.AddIgnoreText.Location = new System.Drawing.Point(147, 196);
            this.AddIgnoreText.Name = "AddIgnoreText";
            this.AddIgnoreText.Size = new System.Drawing.Size(23, 21);
            this.AddIgnoreText.TabIndex = 20;
            this.AddIgnoreText.Text = "+";
            this.AddIgnoreText.UseVisualStyleBackColor = true;
            this.AddIgnoreText.Click += new System.EventHandler(this.AddIgnoreText_Click);
            // 
            // MiscSettingsPage
            // 
            this.MiscSettingsPage.Controls.Add(this.CrashException);
            this.MiscSettingsPage.Controls.Add(this.CrashRecursive);
            this.MiscSettingsPage.Controls.Add(this.groupBox12);
            this.MiscSettingsPage.Controls.Add(this.label9);
            this.MiscSettingsPage.Controls.Add(this.QuitTaco);
            this.MiscSettingsPage.Controls.Add(this.groupBox8);
            this.MiscSettingsPage.Controls.Add(this.ClearSelectedSystems);
            this.MiscSettingsPage.Controls.Add(this.TestFlood);
            this.MiscSettingsPage.Controls.Add(this.groupBox5);
            this.MiscSettingsPage.Controls.Add(this.groupBox2);
            this.MiscSettingsPage.Controls.Add(this.groupBox4);
            this.MiscSettingsPage.Location = new System.Drawing.Point(4, 22);
            this.MiscSettingsPage.Name = "MiscSettingsPage";
            this.MiscSettingsPage.Size = new System.Drawing.Size(422, 561);
            this.MiscSettingsPage.TabIndex = 3;
            this.MiscSettingsPage.Text = "Misc Settings";
            this.MiscSettingsPage.UseVisualStyleBackColor = true;
            // 
            // CrashException
            // 
            this.CrashException.Enabled = false;
            this.CrashException.Location = new System.Drawing.Point(146, 461);
            this.CrashException.Name = "CrashException";
            this.CrashException.Size = new System.Drawing.Size(130, 21);
            this.CrashException.TabIndex = 19;
            this.CrashException.Text = "Crash Exception";
            this.CrashException.UseVisualStyleBackColor = true;
            this.CrashException.Visible = false;
            this.CrashException.Click += new System.EventHandler(this.CrashException_Click);
            // 
            // CrashRecursive
            // 
            this.CrashRecursive.Enabled = false;
            this.CrashRecursive.Location = new System.Drawing.Point(286, 461);
            this.CrashRecursive.Name = "CrashRecursive";
            this.CrashRecursive.Size = new System.Drawing.Size(130, 21);
            this.CrashRecursive.TabIndex = 2;
            this.CrashRecursive.Text = "Crash Recursive";
            this.CrashRecursive.UseVisualStyleBackColor = true;
            this.CrashRecursive.Visible = false;
            this.CrashRecursive.Click += new System.EventHandler(this.CrashRecursive_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.PlayAnomalyWatcherSoundPreview);
            this.groupBox12.Controls.Add(this.AnomalyWatcherSound);
            this.groupBox12.Location = new System.Drawing.Point(6, 354);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(410, 48);
            this.groupBox12.TabIndex = 18;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "Anomaly Monitor Alert Sound";
            // 
            // PlayAnomalyWatcherSoundPreview
            // 
            this.PlayAnomalyWatcherSoundPreview.AutoSize = true;
            this.PlayAnomalyWatcherSoundPreview.Location = new System.Drawing.Point(6, 20);
            this.PlayAnomalyWatcherSoundPreview.Name = "PlayAnomalyWatcherSoundPreview";
            this.PlayAnomalyWatcherSoundPreview.Size = new System.Drawing.Size(29, 12);
            this.PlayAnomalyWatcherSoundPreview.TabIndex = 1;
            this.PlayAnomalyWatcherSoundPreview.TabStop = true;
            this.PlayAnomalyWatcherSoundPreview.Text = "Play";
            this.PlayAnomalyWatcherSoundPreview.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.PlayAnomalyWatcherSoundPreview_LinkClicked);
            // 
            // AnomalyWatcherSound
            // 
            this.AnomalyWatcherSound.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AnomalyWatcherSound.FormattingEnabled = true;
            this.AnomalyWatcherSound.Location = new System.Drawing.Point(39, 18);
            this.AnomalyWatcherSound.Name = "AnomalyWatcherSound";
            this.AnomalyWatcherSound.Size = new System.Drawing.Size(364, 20);
            this.AnomalyWatcherSound.TabIndex = 0;
            this.AnomalyWatcherSound.SelectedIndexChanged += new System.EventHandler(this.AnomalyWatcherSound_SelectedIndexChanged);
            this.AnomalyWatcherSound.TextChanged += new System.EventHandler(this.AnomalyWatcherSound_TextChanged);
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(17, 505);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(389, 52);
            this.label9.TabIndex = 18;
            this.label9.Text = "T.A.C.O. is 100% free, and always will be.  However, if you use T.A.C.O. on a reg" +
    "ular basis, please consider a donation of ISK to McNubblet. Any donations will b" +
    "e greatly appreciated :)";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.DisplayOpenFileAlerts);
            this.groupBox8.Controls.Add(this.DisplayNewFileAlerts);
            this.groupBox8.Location = new System.Drawing.Point(6, 230);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(410, 43);
            this.groupBox8.TabIndex = 17;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "Miscellaneous Settings";
            // 
            // DisplayOpenFileAlerts
            // 
            this.DisplayOpenFileAlerts.AutoSize = true;
            this.DisplayOpenFileAlerts.Location = new System.Drawing.Point(216, 18);
            this.DisplayOpenFileAlerts.Name = "DisplayOpenFileAlerts";
            this.DisplayOpenFileAlerts.Size = new System.Drawing.Size(180, 16);
            this.DisplayOpenFileAlerts.TabIndex = 1;
            this.DisplayOpenFileAlerts.Text = "Display \"Open File\" alerts";
            this.DisplayOpenFileAlerts.UseVisualStyleBackColor = true;
            this.DisplayOpenFileAlerts.CheckedChanged += new System.EventHandler(this.DisplayOpenFileAlerts_CheckedChanged);
            // 
            // DisplayNewFileAlerts
            // 
            this.DisplayNewFileAlerts.AutoSize = true;
            this.DisplayNewFileAlerts.Location = new System.Drawing.Point(11, 18);
            this.DisplayNewFileAlerts.Name = "DisplayNewFileAlerts";
            this.DisplayNewFileAlerts.Size = new System.Drawing.Size(174, 16);
            this.DisplayNewFileAlerts.TabIndex = 0;
            this.DisplayNewFileAlerts.Text = "Display \"New File\" alerts";
            this.DisplayNewFileAlerts.UseVisualStyleBackColor = true;
            this.DisplayNewFileAlerts.CheckedChanged += new System.EventHandler(this.DisplayNewFileAlerts_CheckedChanged);
            // 
            // ClearSelectedSystems
            // 
            this.ClearSelectedSystems.Location = new System.Drawing.Point(6, 407);
            this.ClearSelectedSystems.Name = "ClearSelectedSystems";
            this.ClearSelectedSystems.Size = new System.Drawing.Size(410, 21);
            this.ClearSelectedSystems.TabIndex = 16;
            this.ClearSelectedSystems.Text = "Clear Selected Systems";
            this.ClearSelectedSystems.UseVisualStyleBackColor = true;
            this.ClearSelectedSystems.Click += new System.EventHandler(this.ClearSelectedSystems_Click);
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label29);
            this.groupBox5.Controls.Add(this.label28);
            this.groupBox5.Controls.Add(this.label27);
            this.groupBox5.Controls.Add(this.MaxAlerts);
            this.groupBox5.Controls.Add(this.MaxAlertAge);
            this.groupBox5.Controls.Add(this.label26);
            this.groupBox5.Controls.Add(this.label25);
            this.groupBox5.Controls.Add(this.ShowAlertAgeSecs);
            this.groupBox5.Controls.Add(this.ShowReportCount);
            this.groupBox5.Controls.Add(this.ShowAlertAge);
            this.groupBox5.Controls.Add(this.ShowCharacterLocations);
            this.groupBox5.Controls.Add(this.MapRangeFrom);
            this.groupBox5.Controls.Add(this.label10);
            this.groupBox5.Controls.Add(this.DisplayCharacterNames);
            this.groupBox5.Controls.Add(this.CentreOnCharacter);
            this.groupBox5.Controls.Add(this.CameraFollowCharacter);
            this.groupBox5.Location = new System.Drawing.Point(6, 92);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(410, 132);
            this.groupBox5.TabIndex = 12;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Map Settings";
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(233, 62);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(137, 12);
            this.label29.TabIndex = 17;
            this.label29.Text = "Max alerts to display:";
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(142, 62);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(29, 12);
            this.label28.TabIndex = 16;
            this.label28.Text = "mins";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(24, 62);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(89, 12);
            this.label27.TabIndex = 15;
            this.label27.Text = "Max Alert Age:";
            // 
            // MaxAlerts
            // 
            this.MaxAlerts.Location = new System.Drawing.Point(338, 60);
            this.MaxAlerts.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.MaxAlerts.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxAlerts.Name = "MaxAlerts";
            this.MaxAlerts.Size = new System.Drawing.Size(35, 21);
            this.MaxAlerts.TabIndex = 14;
            this.MaxAlerts.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.MaxAlerts.ValueChanged += new System.EventHandler(this.MaxAlerts_ValueChanged);
            // 
            // MaxAlertAge
            // 
            this.MaxAlertAge.Location = new System.Drawing.Point(101, 60);
            this.MaxAlertAge.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.MaxAlertAge.Name = "MaxAlertAge";
            this.MaxAlertAge.Size = new System.Drawing.Size(35, 21);
            this.MaxAlertAge.TabIndex = 13;
            this.HintToolTip.SetToolTip(this.MaxAlertAge, "Set to 0 for no limit");
            this.MaxAlertAge.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.MaxAlertAge.ValueChanged += new System.EventHandler(this.MaxAlertAge_ValueChanged);
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(160, 40);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(11, 12);
            this.label26.TabIndex = 12;
            this.label26.Text = ")";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(106, 40);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(11, 12);
            this.label25.TabIndex = 11;
            this.label25.Text = "(";
            // 
            // ShowAlertAgeSecs
            // 
            this.ShowAlertAgeSecs.AutoSize = true;
            this.ShowAlertAgeSecs.Checked = true;
            this.ShowAlertAgeSecs.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowAlertAgeSecs.Location = new System.Drawing.Point(119, 39);
            this.ShowAlertAgeSecs.Name = "ShowAlertAgeSecs";
            this.ShowAlertAgeSecs.Size = new System.Drawing.Size(48, 16);
            this.ShowAlertAgeSecs.TabIndex = 10;
            this.ShowAlertAgeSecs.Text = "secs";
            this.ShowAlertAgeSecs.UseVisualStyleBackColor = true;
            this.ShowAlertAgeSecs.CheckedChanged += new System.EventHandler(this.ShowAlertAgeSecs_CheckedChanged);
            // 
            // ShowReportCount
            // 
            this.ShowReportCount.AutoSize = true;
            this.ShowReportCount.Location = new System.Drawing.Point(216, 39);
            this.ShowReportCount.Name = "ShowReportCount";
            this.ShowReportCount.Size = new System.Drawing.Size(168, 16);
            this.ShowReportCount.TabIndex = 9;
            this.ShowReportCount.Text = "Show System Report Count";
            this.HintToolTip.SetToolTip(this.ShowReportCount, "Show number of times a system has\r\nbeen reporting since application startup");
            this.ShowReportCount.UseVisualStyleBackColor = true;
            this.ShowReportCount.CheckedChanged += new System.EventHandler(this.ShowReportCount_CheckedChanged);
            // 
            // ShowAlertAge
            // 
            this.ShowAlertAge.AutoSize = true;
            this.ShowAlertAge.Checked = true;
            this.ShowAlertAge.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ShowAlertAge.Location = new System.Drawing.Point(8, 39);
            this.ShowAlertAge.Name = "ShowAlertAge";
            this.ShowAlertAge.Size = new System.Drawing.Size(108, 16);
            this.ShowAlertAge.TabIndex = 8;
            this.ShowAlertAge.Text = "Show Alert Age";
            this.ShowAlertAge.UseVisualStyleBackColor = true;
            this.ShowAlertAge.CheckedChanged += new System.EventHandler(this.ShowAlertAge_CheckedChanged);
            // 
            // ShowCharacterLocations
            // 
            this.ShowCharacterLocations.AutoSize = true;
            this.ShowCharacterLocations.Location = new System.Drawing.Point(8, 18);
            this.ShowCharacterLocations.Name = "ShowCharacterLocations";
            this.ShowCharacterLocations.Size = new System.Drawing.Size(168, 16);
            this.ShowCharacterLocations.TabIndex = 7;
            this.ShowCharacterLocations.Text = "Show Character Locations";
            this.ShowCharacterLocations.UseVisualStyleBackColor = true;
            this.ShowCharacterLocations.CheckedChanged += new System.EventHandler(this.ShowCharacterLocations_CheckedChanged);
            // 
            // MapRangeFrom
            // 
            this.MapRangeFrom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.MapRangeFrom.FormattingEnabled = true;
            this.MapRangeFrom.Items.AddRange(new object[] {
            "Home System"});
            this.MapRangeFrom.Location = new System.Drawing.Point(216, 103);
            this.MapRangeFrom.Name = "MapRangeFrom";
            this.MapRangeFrom.Size = new System.Drawing.Size(179, 20);
            this.MapRangeFrom.TabIndex = 6;
            this.MapRangeFrom.SelectedIndexChanged += new System.EventHandler(this.MapRangeFrom_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(213, 85);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(185, 12);
            this.label10.TabIndex = 5;
            this.label10.Text = "Jump range on map relative to:";
            // 
            // DisplayCharacterNames
            // 
            this.DisplayCharacterNames.AutoSize = true;
            this.DisplayCharacterNames.Enabled = false;
            this.DisplayCharacterNames.Location = new System.Drawing.Point(216, 18);
            this.DisplayCharacterNames.Name = "DisplayCharacterNames";
            this.DisplayCharacterNames.Size = new System.Drawing.Size(162, 16);
            this.DisplayCharacterNames.TabIndex = 4;
            this.DisplayCharacterNames.Text = "Display Character Names";
            this.DisplayCharacterNames.UseVisualStyleBackColor = true;
            this.DisplayCharacterNames.CheckedChanged += new System.EventHandler(this.DisplayCharacterNames_CheckedChanged);
            // 
            // CentreOnCharacter
            // 
            this.CentreOnCharacter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CentreOnCharacter.Enabled = false;
            this.CentreOnCharacter.FormattingEnabled = true;
            this.CentreOnCharacter.Location = new System.Drawing.Point(25, 103);
            this.CentreOnCharacter.Name = "CentreOnCharacter";
            this.CentreOnCharacter.Size = new System.Drawing.Size(174, 20);
            this.CentreOnCharacter.TabIndex = 3;
            this.CentreOnCharacter.SelectedIndexChanged += new System.EventHandler(this.CentreOnCharacter_SelectedIndexChanged);
            // 
            // CameraFollowCharacter
            // 
            this.CameraFollowCharacter.AutoSize = true;
            this.CameraFollowCharacter.Location = new System.Drawing.Point(8, 84);
            this.CameraFollowCharacter.Name = "CameraFollowCharacter";
            this.CameraFollowCharacter.Size = new System.Drawing.Size(222, 16);
            this.CameraFollowCharacter.TabIndex = 2;
            this.CameraFollowCharacter.Text = "Keep camera centred on character:";
            this.CameraFollowCharacter.UseVisualStyleBackColor = true;
            this.CameraFollowCharacter.CheckedChanged += new System.EventHandler(this.CameraFollowCharacter_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.PreserveSelectedSystems);
            this.groupBox2.Controls.Add(this.PreserveFullScreenStatus);
            this.groupBox2.Controls.Add(this.PreserveHomeSystem);
            this.groupBox2.Controls.Add(this.PreserveWindowPosition);
            this.groupBox2.Controls.Add(this.PreserveWindowSize);
            this.groupBox2.Controls.Add(this.PreserveCameraDistance);
            this.groupBox2.Controls.Add(this.PreserveLookAt);
            this.groupBox2.Location = new System.Drawing.Point(6, 6);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(410, 81);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Window Settings (Preserve between sessions)";
            // 
            // PreserveSelectedSystems
            // 
            this.PreserveSelectedSystems.AutoSize = true;
            this.PreserveSelectedSystems.Location = new System.Drawing.Point(288, 39);
            this.PreserveSelectedSystems.Name = "PreserveSelectedSystems";
            this.PreserveSelectedSystems.Size = new System.Drawing.Size(120, 16);
            this.PreserveSelectedSystems.TabIndex = 4;
            this.PreserveSelectedSystems.Text = "Selected Systems";
            this.PreserveSelectedSystems.UseVisualStyleBackColor = true;
            this.PreserveSelectedSystems.CheckedChanged += new System.EventHandler(this.PreserveSelectedSystems_CheckedChanged);
            // 
            // PreserveFullScreenStatus
            // 
            this.PreserveFullScreenStatus.AutoSize = true;
            this.PreserveFullScreenStatus.Location = new System.Drawing.Point(288, 18);
            this.PreserveFullScreenStatus.Name = "PreserveFullScreenStatus";
            this.PreserveFullScreenStatus.Size = new System.Drawing.Size(132, 16);
            this.PreserveFullScreenStatus.TabIndex = 3;
            this.PreserveFullScreenStatus.Text = "Full Screen Status";
            this.PreserveFullScreenStatus.UseVisualStyleBackColor = true;
            this.PreserveFullScreenStatus.CheckedChanged += new System.EventHandler(this.PreserveFullScreenStatus_CheckedChanged);
            // 
            // PreserveHomeSystem
            // 
            this.PreserveHomeSystem.AutoSize = true;
            this.PreserveHomeSystem.Location = new System.Drawing.Point(11, 60);
            this.PreserveHomeSystem.Name = "PreserveHomeSystem";
            this.PreserveHomeSystem.Size = new System.Drawing.Size(90, 16);
            this.PreserveHomeSystem.TabIndex = 2;
            this.PreserveHomeSystem.Text = "Home System";
            this.PreserveHomeSystem.UseVisualStyleBackColor = true;
            this.PreserveHomeSystem.CheckedChanged += new System.EventHandler(this.PreserveHomeSystem_CheckedChanged);
            // 
            // PreserveWindowPosition
            // 
            this.PreserveWindowPosition.AutoSize = true;
            this.PreserveWindowPosition.Location = new System.Drawing.Point(152, 18);
            this.PreserveWindowPosition.Name = "PreserveWindowPosition";
            this.PreserveWindowPosition.Size = new System.Drawing.Size(114, 16);
            this.PreserveWindowPosition.TabIndex = 1;
            this.PreserveWindowPosition.Text = "Window Position";
            this.PreserveWindowPosition.UseVisualStyleBackColor = true;
            this.PreserveWindowPosition.CheckedChanged += new System.EventHandler(this.PreserveWindowPosition_CheckedChanged);
            // 
            // PreserveWindowSize
            // 
            this.PreserveWindowSize.AutoSize = true;
            this.PreserveWindowSize.Location = new System.Drawing.Point(11, 18);
            this.PreserveWindowSize.Name = "PreserveWindowSize";
            this.PreserveWindowSize.Size = new System.Drawing.Size(90, 16);
            this.PreserveWindowSize.TabIndex = 0;
            this.PreserveWindowSize.Text = "Window Size";
            this.PreserveWindowSize.UseVisualStyleBackColor = true;
            this.PreserveWindowSize.CheckedChanged += new System.EventHandler(this.PreserveWindowSize_CheckedChanged);
            // 
            // PreserveCameraDistance
            // 
            this.PreserveCameraDistance.AutoSize = true;
            this.PreserveCameraDistance.Location = new System.Drawing.Point(152, 39);
            this.PreserveCameraDistance.Name = "PreserveCameraDistance";
            this.PreserveCameraDistance.Size = new System.Drawing.Size(144, 16);
            this.PreserveCameraDistance.TabIndex = 1;
            this.PreserveCameraDistance.Text = "Save camera distance";
            this.PreserveCameraDistance.UseVisualStyleBackColor = true;
            this.PreserveCameraDistance.CheckedChanged += new System.EventHandler(this.PreserveCameraDistance_CheckedChanged);
            // 
            // PreserveLookAt
            // 
            this.PreserveLookAt.AutoSize = true;
            this.PreserveLookAt.Location = new System.Drawing.Point(11, 39);
            this.PreserveLookAt.Name = "PreserveLookAt";
            this.PreserveLookAt.Size = new System.Drawing.Size(144, 16);
            this.PreserveLookAt.TabIndex = 0;
            this.PreserveLookAt.Text = "Save camera position";
            this.PreserveLookAt.UseVisualStyleBackColor = true;
            this.PreserveLookAt.CheckedChanged += new System.EventHandler(this.PreserveLookAt_CheckedChanged);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.ChooseLogPath);
            this.groupBox4.Controls.Add(this.OverrideLogPath);
            this.groupBox4.Controls.Add(this.LogPath);
            this.groupBox4.Location = new System.Drawing.Point(6, 279);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(410, 69);
            this.groupBox4.TabIndex = 3;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Log Directory";
            // 
            // ChooseLogPath
            // 
            this.ChooseLogPath.Enabled = false;
            this.ChooseLogPath.Location = new System.Drawing.Point(329, 39);
            this.ChooseLogPath.Name = "ChooseLogPath";
            this.ChooseLogPath.Size = new System.Drawing.Size(75, 21);
            this.ChooseLogPath.TabIndex = 2;
            this.ChooseLogPath.Text = "Change";
            this.ChooseLogPath.UseVisualStyleBackColor = true;
            this.ChooseLogPath.Click += new System.EventHandler(this.ChooseLogPath_Click);
            // 
            // OverrideLogPath
            // 
            this.OverrideLogPath.AutoSize = true;
            this.OverrideLogPath.Location = new System.Drawing.Point(11, 42);
            this.OverrideLogPath.Name = "OverrideLogPath";
            this.OverrideLogPath.Size = new System.Drawing.Size(198, 16);
            this.OverrideLogPath.TabIndex = 1;
            this.OverrideLogPath.Text = "Manually select log directory";
            this.OverrideLogPath.UseVisualStyleBackColor = true;
            this.OverrideLogPath.CheckedChanged += new System.EventHandler(this.OverrideLogPath_CheckedChanged);
            // 
            // LogPath
            // 
            this.LogPath.Enabled = false;
            this.LogPath.Location = new System.Drawing.Point(11, 18);
            this.LogPath.Name = "LogPath";
            this.LogPath.ReadOnly = true;
            this.LogPath.Size = new System.Drawing.Size(393, 21);
            this.LogPath.TabIndex = 0;
            // 
            // InfoPage
            // 
            this.InfoPage.Controls.Add(this.groupBox16);
            this.InfoPage.Controls.Add(this.groupBox15);
            this.InfoPage.Controls.Add(this.groupBox14);
            this.InfoPage.Controls.Add(this.pictureBox1);
            this.InfoPage.Controls.Add(this.label21);
            this.InfoPage.Controls.Add(this.label20);
            this.InfoPage.Controls.Add(this.label19);
            this.InfoPage.Controls.Add(this.groupBox13);
            this.InfoPage.Location = new System.Drawing.Point(4, 22);
            this.InfoPage.Name = "InfoPage";
            this.InfoPage.Size = new System.Drawing.Size(422, 561);
            this.InfoPage.TabIndex = 4;
            this.InfoPage.Text = "About";
            this.InfoPage.UseVisualStyleBackColor = true;
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.label24);
            this.groupBox16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox16.Location = new System.Drawing.Point(5, 273);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(410, 56);
            this.groupBox16.TabIndex = 7;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "Anomaly Monitor";
            // 
            // label24
            // 
            this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label24.Location = new System.Drawing.Point(6, 15);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(398, 41);
            this.label24.TabIndex = 0;
            this.label24.Text = resources.GetString("label24.Text");
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.label23);
            this.groupBox15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox15.Location = new System.Drawing.Point(5, 494);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(410, 72);
            this.groupBox15.TabIndex = 6;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "Acknowledgements";
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label23.Location = new System.Drawing.Point(2, 15);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(402, 54);
            this.label23.TabIndex = 0;
            this.label23.Text = "Thanks to everyone\'s who has helped out in any way.\r\n\r\nExtra thanks to the 10 peo" +
    "ple who have thrown some ISK my way, it\'s much appreciated.";
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.label22);
            this.groupBox14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox14.Location = new System.Drawing.Point(5, 335);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(410, 153);
            this.groupBox14.TabIndex = 5;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "Beta Software";
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label22.Location = new System.Drawing.Point(6, 15);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(398, 136);
            this.label22.TabIndex = 0;
            this.label22.Text = resources.GetString("label22.Text");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Taco.Properties.Resources.AngryTaco;
            this.pictureBox1.Location = new System.Drawing.Point(9, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(96, 75);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(154, 73);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(125, 12);
            this.label21.TabIndex = 3;
            this.label21.Text = "v0.7.0b by McNubblet";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(114, 57);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(227, 12);
            this.label20.TabIndex = 2;
            this.label20.Text = "Tactical Alerting Combatant Overwatch";
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Segoe UI", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label19.Location = new System.Drawing.Point(135, 10);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(151, 47);
            this.label19.TabIndex = 1;
            this.label19.Text = "T.A.C.O.";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label32);
            this.groupBox13.Controls.Add(this.label31);
            this.groupBox13.Controls.Add(this.label30);
            this.groupBox13.Controls.Add(this.label18);
            this.groupBox13.Controls.Add(this.label17);
            this.groupBox13.Controls.Add(this.label16);
            this.groupBox13.Controls.Add(this.label15);
            this.groupBox13.Controls.Add(this.label14);
            this.groupBox13.Controls.Add(this.label13);
            this.groupBox13.Controls.Add(this.label12);
            this.groupBox13.Controls.Add(this.label11);
            this.groupBox13.Controls.Add(this.label4);
            this.groupBox13.Controls.Add(this.label3);
            this.groupBox13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox13.Location = new System.Drawing.Point(5, 93);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(410, 174);
            this.groupBox13.TabIndex = 0;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "Controls and Hotkeys";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.Location = new System.Drawing.Point(6, 142);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(400, 26);
            this.label32.TabIndex = 12;
            this.label32.Text = "NOTE: When the combined intel pane is focused, intel will buffer until you leave " +
    "the\r\ncombined intel pane.";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.Location = new System.Drawing.Point(82, 94);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(305, 26);
            this.label31.TabIndex = 11;
            this.label31.Text = "When highlighting a name in the combined intel pane, open the\r\nplayers zKillboard" +
    " and save the name";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(6, 94);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(64, 13);
            this.label30.TabIndex = 10;
            this.label30.Text = "Z or Enter";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label18.Location = new System.Drawing.Point(82, 122);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(70, 13);
            this.label18.TabIndex = 9;
            this.label18.Text = "Quit T.A.C.O.";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label17.Location = new System.Drawing.Point(6, 122);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(16, 13);
            this.label17.TabIndex = 8;
            this.label17.Text = "Q";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label16.Location = new System.Drawing.Point(82, 78);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(179, 13);
            this.label16.TabIndex = 7;
            this.label16.Text = "Fullscreen Only: Exit fullscreen mode";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.Location = new System.Drawing.Point(6, 78);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(31, 13);
            this.label15.TabIndex = 6;
            this.label15.Text = "ESC";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(82, 63);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(308, 13);
            this.label14.TabIndex = 5;
            this.label14.Text = "Fullscreen Only: Toggle intel panel - map occupies entire screen";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label13.Location = new System.Drawing.Point(82, 47);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(188, 13);
            this.label13.TabIndex = 4;
            this.label13.Text = "System: Toggle persistant system label";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(82, 19);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(195, 26);
            this.label12.TabIndex = 3;
            this.label12.Text = "System: Set/Unset home system\r\nEmpty space: Open extra controls menu\r\n";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(6, 63);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(16, 13);
            this.label11.TabIndex = 2;
            this.label11.Text = "H";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 47);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(60, 13);
            this.label4.TabIndex = 1;
            this.label4.Text = "Left-click";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(68, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Right-click";
            // 
            // LogBrowser
            // 
            this.LogBrowser.Description = "Select your preferred log location:";
            this.LogBrowser.ShowNewFolderButton = false;
            // 
            // PathFindingTicker
            // 
            this.PathFindingTicker.Enabled = true;
            this.PathFindingTicker.Interval = 10;
            this.PathFindingTicker.Tick += new System.EventHandler(this.PathFindingTicker_Tick);
            // 
            // IntelUpdateTicker
            // 
            this.IntelUpdateTicker.Enabled = true;
            this.IntelUpdateTicker.Interval = 500;
            this.IntelUpdateTicker.Tick += new System.EventHandler(this.IntelUpdateTicker_Tick);
            // 
            // CustomSoundPicker
            // 
            this.CustomSoundPicker.FileName = "*.wav";
            this.CustomSoundPicker.Filter = "WAV files|*.wav";
            // 
            // MenuStrip
            // 
            this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.followMenuItem,
            this.mapRangeFromMenuItem,
            this.toolStripSeparator1,
            this.anomalyMonitorMenuItem,
            this.toolStripSeparator2,
            this.muteSoundMenuItem,
            this.toolStripSeparator3,
            this.clearSelectedSystemsMenuItem,
            this.quitMenuItem});
            this.MenuStrip.Name = "MenuStrip";
            this.MenuStrip.Size = new System.Drawing.Size(211, 154);
            // 
            // followMenuItem
            // 
            this.followMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.noneToolStripMenuItem});
            this.followMenuItem.Name = "followMenuItem";
            this.followMenuItem.Size = new System.Drawing.Size(210, 22);
            this.followMenuItem.Text = "Follow Character";
            this.followMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.followMenuItem_DropDownItemClicked);
            // 
            // noneToolStripMenuItem
            // 
            this.noneToolStripMenuItem.Checked = true;
            this.noneToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.noneToolStripMenuItem.Name = "noneToolStripMenuItem";
            this.noneToolStripMenuItem.Size = new System.Drawing.Size(108, 22);
            this.noneToolStripMenuItem.Text = "None";
            // 
            // mapRangeFromMenuItem
            // 
            this.mapRangeFromMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.homeToolStripMenuItem});
            this.mapRangeFromMenuItem.Name = "mapRangeFromMenuItem";
            this.mapRangeFromMenuItem.Size = new System.Drawing.Size(210, 22);
            this.mapRangeFromMenuItem.Text = "Map Range From";
            this.mapRangeFromMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.mapRangeFromMenuItem_DropDownItemClicked);
            // 
            // homeToolStripMenuItem
            // 
            this.homeToolStripMenuItem.Checked = true;
            this.homeToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.homeToolStripMenuItem.Name = "homeToolStripMenuItem";
            this.homeToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.homeToolStripMenuItem.Text = "Home System";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(207, 6);
            // 
            // anomalyMonitorMenuItem
            // 
            this.anomalyMonitorMenuItem.Name = "anomalyMonitorMenuItem";
            this.anomalyMonitorMenuItem.Size = new System.Drawing.Size(210, 22);
            this.anomalyMonitorMenuItem.Text = "Anomaly Monitor";
            this.anomalyMonitorMenuItem.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.anomalyMonitorMenuItem_DropDownItemClicked);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(207, 6);
            // 
            // muteSoundMenuItem
            // 
            this.muteSoundMenuItem.CheckOnClick = true;
            this.muteSoundMenuItem.Name = "muteSoundMenuItem";
            this.muteSoundMenuItem.Size = new System.Drawing.Size(210, 22);
            this.muteSoundMenuItem.Text = "Mute Sound";
            this.muteSoundMenuItem.Click += new System.EventHandler(this.muteSoundMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(207, 6);
            // 
            // clearSelectedSystemsMenuItem
            // 
            this.clearSelectedSystemsMenuItem.Name = "clearSelectedSystemsMenuItem";
            this.clearSelectedSystemsMenuItem.Size = new System.Drawing.Size(210, 22);
            this.clearSelectedSystemsMenuItem.Text = "Clear Selected Systems";
            this.clearSelectedSystemsMenuItem.Click += new System.EventHandler(this.clearSelectedSystemsMenuItem_Click);
            // 
            // quitMenuItem
            // 
            this.quitMenuItem.Name = "quitMenuItem";
            this.quitMenuItem.Size = new System.Drawing.Size(210, 22);
            this.quitMenuItem.Text = "Quit";
            this.quitMenuItem.Click += new System.EventHandler(this.quitToolStripMenuItem_Click);
            // 
            // glOut
            // 
            this.glOut.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.glOut.BackColor = System.Drawing.Color.Black;
            this.glOut.Location = new System.Drawing.Point(12, 11);
            this.glOut.Name = "glOut";
            this.glOut.Size = new System.Drawing.Size(750, 692);
            this.glOut.TabIndex = 13;
            this.glOut.TabStop = false;
            this.glOut.VSync = false;
            this.glOut.Load += new System.EventHandler(this.glOut_Load);
            this.glOut.Paint += new System.Windows.Forms.PaintEventHandler(this.glOut_Paint);
            this.glOut.MouseDown += new System.Windows.Forms.MouseEventHandler(this.glOut_MouseDown);
            this.glOut.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glOut_MouseMove);
            this.glOut.MouseUp += new System.Windows.Forms.MouseEventHandler(this.glOut_MouseUp);
            this.glOut.Resize += new System.EventHandler(this.glOut_Resize);
            // 
            // CombinedIntel
            // 
            this.CombinedIntel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CombinedIntel.BackColor = System.Drawing.Color.White;
            this.CombinedIntel.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CombinedIntel.Font = new System.Drawing.Font("Consolas", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CombinedIntel.Location = new System.Drawing.Point(-1, 0);
            this.CombinedIntel.Name = "CombinedIntel";
            this.CombinedIntel.ReadOnly = true;
            this.CombinedIntel.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedBoth;
            this.CombinedIntel.Size = new System.Drawing.Size(444, 599);
            this.CombinedIntel.TabIndex = 12;
            this.CombinedIntel.TabStop = false;
            this.CombinedIntel.Text = "";
            this.CombinedIntel.WordWrap = false;
            this.CombinedIntel.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(this.CombinedIntel_LinkClicked);
            this.CombinedIntel.Enter += new System.EventHandler(this.CombinedIntel_Enter);
            this.CombinedIntel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CombinedIntel_KeyDown);
            this.CombinedIntel.Leave += new System.EventHandler(this.CombinedIntel_Leave);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 717);
            this.Controls.Add(this.glOut);
            this.Controls.Add(this.UIContainer);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.Text = "T.A.C.O. - Tactical Alerting Combatant Overwatch v0.7.0b";
            this.Deactivate += new System.EventHandler(this.MainForm_Deactivate);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.UIContainer.Panel1.ResumeLayout(false);
            this.UIContainer.Panel1.PerformLayout();
            this.UIContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.UIContainer)).EndInit();
            this.UIContainer.ResumeLayout(false);
            this.UITabControl.ResumeLayout(false);
            this.CombinedPage.ResumeLayout(false);
            this.GOTGPage.ResumeLayout(false);
            this.GOTGPage.PerformLayout();
            this.BranchPage.ResumeLayout(false);
            this.BranchPage.PerformLayout();
            this.DekleinPage.ResumeLayout(false);
            this.DekleinPage.PerformLayout();
            this.TenalPage.ResumeLayout(false);
            this.TenalPage.PerformLayout();
            this.VenalPage.ResumeLayout(false);
            this.VenalPage.PerformLayout();
            this.FadePage.ResumeLayout(false);
            this.FadePage.PerformLayout();
            this.PureBlindPage.ResumeLayout(false);
            this.PureBlindPage.PerformLayout();
            this.TributePage.ResumeLayout(false);
            this.TributePage.PerformLayout();
            this.ValePage.ResumeLayout(false);
            this.ValePage.PerformLayout();
            this.ProvidencePage.ResumeLayout(false);
            this.ProvidencePage.PerformLayout();
            this.DelvePage.ResumeLayout(false);
            this.DelvePage.PerformLayout();
            this.ConfigPage.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.ChannelsPage.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.AlertManagementPage.ResumeLayout(false);
            this.AddCustomAlertGroup.ResumeLayout(false);
            this.AddCustomAlertGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CustomAlertRepeatInterval)).EndInit();
            this.AddRangeAlertGroup.ResumeLayout(false);
            this.AddRangeAlertGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LowerAlertRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.UpperAlertRange)).EndInit();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.ListManagementPage.ResumeLayout(false);
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.MiscSettingsPage.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MaxAlerts)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxAlertAge)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.InfoPage.ResumeLayout(false);
            this.InfoPage.PerformLayout();
            this.groupBox16.ResumeLayout(false);
            this.groupBox15.ResumeLayout(false);
            this.groupBox14.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.MenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private OpenTK.GLControl glOut;
        private System.Windows.Forms.TextBox SearchSystem;
        private System.Windows.Forms.Timer Ticker;
        private System.Windows.Forms.CheckBox MonitorBranch;
        private System.Windows.Forms.CheckBox MonitorDeklein;
        private System.Windows.Forms.CheckBox MonitorTenal;
        private System.Windows.Forms.CheckBox MonitorVenal;
        private System.Windows.Forms.Button FullscreenToggle;
        private System.Windows.Forms.Button QuitTaco;
        private System.Windows.Forms.Button TestFlood;
        private System.Windows.Forms.SplitContainer UIContainer;
        //private System.Windows.Forms.TextBox CombinedIntel;
        private Taco.Classes.RichTextBoxEx CombinedIntel;
        private System.Windows.Forms.Button LogWatchToggle;
        private System.Windows.Forms.Button FindSystem;
        private System.Windows.Forms.TabControl UITabControl;
        private System.Windows.Forms.TabPage CombinedPage;
        private System.Windows.Forms.TabPage ConfigPage;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.CheckBox PreserveFullScreenStatus;
        private System.Windows.Forms.CheckBox PreserveHomeSystem;
        private System.Windows.Forms.CheckBox PreserveWindowPosition;
        private System.Windows.Forms.CheckBox PreserveWindowSize;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox AlertVenal;
        private System.Windows.Forms.CheckBox AlertTenal;
        private System.Windows.Forms.CheckBox AlertDeklein;
        private System.Windows.Forms.CheckBox AlertBranch;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button ChooseLogPath;
        private System.Windows.Forms.CheckBox OverrideLogPath;
        private System.Windows.Forms.TextBox LogPath;
        private System.Windows.Forms.TabPage BranchPage;
        private System.Windows.Forms.TabPage DekleinPage;
        private System.Windows.Forms.TabPage TenalPage;
        private System.Windows.Forms.TabPage VenalPage;
        private System.Windows.Forms.TextBox BranchIntel;
        private System.Windows.Forms.TextBox DekleinIntel;
        private System.Windows.Forms.TextBox TenalIntel;
        private System.Windows.Forms.TextBox VenalIntel;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.CheckBox PreserveCameraDistance;
        private System.Windows.Forms.CheckBox PreserveLookAt;
        private System.Windows.Forms.FolderBrowserDialog LogBrowser;
        private System.Windows.Forms.Timer PathFindingTicker;
        private System.Windows.Forms.Timer IntelUpdateTicker;
        private System.Windows.Forms.CheckBox PreserveSelectedSystems;
        private System.Windows.Forms.Button ClearSelectedSystems;
        private System.Windows.Forms.TextBox NewCustomAlertText;
        private System.Windows.Forms.Button AddNewCustomAlert;
        private System.Windows.Forms.GroupBox groupBox9;
        private System.Windows.Forms.Button AddNewRangeAlert;
        private System.Windows.Forms.NumericUpDown UpperAlertRange;
        private System.Windows.Forms.GroupBox AddCustomAlertGroup;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown CustomAlertRepeatInterval;
        private System.Windows.Forms.GroupBox AddRangeAlertGroup;
        private System.Windows.Forms.ComboBox UpperLimitOperator;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox RangeAlertSystem;
        private System.Windows.Forms.CheckedListBox AlertList;
        private System.Windows.Forms.LinkLabel RemoveSelectedItem;
        private System.Windows.Forms.LinkLabel PlayAlertSound;
        private System.Windows.Forms.LinkLabel PlayCustomTextAlertSound;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox CustomTextAlertSound;
        private System.Windows.Forms.ComboBox RangeAlertSound;
        private System.Windows.Forms.LinkLabel PlayRangeAlertSound;
        private System.Windows.Forms.OpenFileDialog CustomSoundPicker;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.ListBox IgnoreSystemList;
        private System.Windows.Forms.Button RemoveIgnoreSystem;
        private System.Windows.Forms.ComboBox NewIgnoreSystem;
        private System.Windows.Forms.Button AddIgnoreSystem;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Button RemoveIgnoreText;
        private System.Windows.Forms.TextBox NewIgnoreText;
        private System.Windows.Forms.ListBox IgnoreTextList;
        private System.Windows.Forms.Button AddIgnoreText;
        private System.Windows.Forms.CheckBox MonitorGameLog;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.CheckBox DisplayOpenFileAlerts;
        private System.Windows.Forms.CheckBox DisplayNewFileAlerts;
        private System.Windows.Forms.Button MoveAlertDown;
        private System.Windows.Forms.Button MoveAlertUp;
        private System.Windows.Forms.NumericUpDown LowerAlertRange;
        private System.Windows.Forms.ComboBox LowerLimitOperator;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TabPage FadePage;
        private System.Windows.Forms.TabPage PureBlindPage;
        private System.Windows.Forms.TabPage TributePage;
        private System.Windows.Forms.TabPage ValePage;
        private System.Windows.Forms.TabPage ProvidencePage;
        private System.Windows.Forms.TabPage DelvePage;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage ChannelsPage;
        private System.Windows.Forms.TabPage AlertManagementPage;
        private System.Windows.Forms.TabPage ListManagementPage;
        private System.Windows.Forms.CheckBox MonitorDelve;
        private System.Windows.Forms.CheckBox MonitorProvidence;
        private System.Windows.Forms.CheckBox MonitorVale;
        private System.Windows.Forms.CheckBox MonitorTribute;
        private System.Windows.Forms.CheckBox MonitorPureBlind;
        private System.Windows.Forms.CheckBox MonitorFade;
        private System.Windows.Forms.CheckBox AlertPureBlind;
        private System.Windows.Forms.CheckBox AlertFade;
        private System.Windows.Forms.TabPage MiscSettingsPage;
        private System.Windows.Forms.CheckBox AlertDelve;
        private System.Windows.Forms.CheckBox AlertProvidence;
        private System.Windows.Forms.CheckBox AlertVale;
        private System.Windows.Forms.CheckBox AlertTribute;
        private System.Windows.Forms.TextBox FadeIntel;
        private System.Windows.Forms.TextBox PureBlindIntel;
        private System.Windows.Forms.TextBox TributeIntel;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox ValeIntel;
        private System.Windows.Forms.TextBox ProvidenceIntel;
        private System.Windows.Forms.TextBox DelveIntel;
        private System.Windows.Forms.ComboBox RangeAlertCharacter;
        private System.Windows.Forms.ComboBox CentreOnCharacter;
        private System.Windows.Forms.CheckBox CameraFollowCharacter;
        private System.Windows.Forms.CheckBox DisplayCharacterNames;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox MapRangeFrom;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.CheckBox ShowCharacterLocations;
        private System.Windows.Forms.ComboBox NewRangeAlertType;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TabPage InfoPage;
        private System.Windows.Forms.ContextMenuStrip MenuStrip;
        private System.Windows.Forms.ToolStripMenuItem followMenuItem;
        private System.Windows.Forms.ToolStripMenuItem mapRangeFromMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem quitMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noneToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem homeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSelectedSystemsMenuItem;
        private System.Windows.Forms.ToolStripMenuItem anomalyMonitorMenuItem;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.LinkLabel PlayAnomalyWatcherSoundPreview;
        private System.Windows.Forms.ComboBox AnomalyWatcherSound;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.GroupBox groupBox15;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.GroupBox groupBox14;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.Button CrashException;
        private System.Windows.Forms.Button CrashRecursive;
        private System.Windows.Forms.CheckBox ShowReportCount;
        private System.Windows.Forms.CheckBox ShowAlertAge;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.CheckBox ShowAlertAgeSecs;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.NumericUpDown MaxAlerts;
        private System.Windows.Forms.NumericUpDown MaxAlertAge;
        private System.Windows.Forms.ToolTip HintToolTip;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.ToolStripMenuItem muteSoundMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.ListBox CharacterList;
        private System.Windows.Forms.Label BufferingIndicator;
        private System.Windows.Forms.LinkLabel EditSelectedItem;
        private System.Windows.Forms.LinkLabel CancelEditSelectedItem;
        private System.Windows.Forms.Button SaveRangeAlert;
        private System.Windows.Forms.Button SaveCustomAlert;
        private System.Windows.Forms.TextBox NewLinkedCharacter;
        private System.Windows.Forms.Button RemoveLinkedCharacter;
        private System.Windows.Forms.Button AddLinkedCharacter;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.TabPage GOTGPage;
        private System.Windows.Forms.TextBox GOTGIntel;
        private System.Windows.Forms.CheckBox MonitorGOTG;
        private System.Windows.Forms.CheckBox AlertGOTG;
    }
}

