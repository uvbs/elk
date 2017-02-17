namespace Elk.Presentation
{
  partial class Elk_Main
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
      this.tb_DestinationUrl = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.bt_StartStop = new System.Windows.Forms.Button();
      this.bgw_Crawler = new System.ComponentModel.BackgroundWorker();
      this.l_HostChainKey = new System.Windows.Forms.Label();
      this.l_CrawlerKey = new System.Windows.Forms.Label();
      this.l_CrawlerValue = new System.Windows.Forms.Label();
      this.l_HostChainValue = new System.Windows.Forms.Label();
      this.bgw_HostChain = new System.ComponentModel.BackgroundWorker();
      this.tp_CookieValueInUrl = new System.Windows.Forms.TabPage();
      this.tp_SharedCookies = new System.Windows.Forms.TabPage();
      this.tp_PageLinksTo = new System.Windows.Forms.TabPage();
      this.dgv_PageLinksTo = new System.Windows.Forms.DataGridView();
      this.tp_Crawler = new System.Windows.Forms.TabPage();
      this.tb_CrawlerLog = new System.Windows.Forms.TextBox();
      this.tp_HostChain = new System.Windows.Forms.TabPage();
      this.tv_ResponseEntities = new System.Windows.Forms.TreeView();
      this.gb_RequestResults = new System.Windows.Forms.GroupBox();
      this.tb_IpAddress = new System.Windows.Forms.TextBox();
      this.l_IpAddress = new System.Windows.Forms.Label();
      this.cb_Https = new System.Windows.Forms.CheckBox();
      this.cb_Http = new System.Windows.Forms.CheckBox();
      this.l_OpenPorts = new System.Windows.Forms.Label();
      this.tb_Server = new System.Windows.Forms.TextBox();
      this.l_Server = new System.Windows.Forms.Label();
      this.tb_Hpkp = new System.Windows.Forms.TextBox();
      this.tb_RedirectToHttps = new System.Windows.Forms.TextBox();
      this.tb_Hsts = new System.Windows.Forms.TextBox();
      this.tb_RawHeaders = new System.Windows.Forms.TextBox();
      this.l_RawHeaders = new System.Windows.Forms.Label();
      this.tb_ResponseStatus = new System.Windows.Forms.TextBox();
      this.tb_Cookies = new System.Windows.Forms.TextBox();
      this.tb_Location = new System.Windows.Forms.TextBox();
      this.l_Location = new System.Windows.Forms.Label();
      this.l_Cookies = new System.Windows.Forms.Label();
      this.l_RedirectToHttps = new System.Windows.Forms.Label();
      this.l_Hpkp = new System.Windows.Forms.Label();
      this.l_Hsts = new System.Windows.Forms.Label();
      this.l_ResponseStatus = new System.Windows.Forms.Label();
      this.tc_AnalyzeHttpServer = new System.Windows.Forms.TabControl();
      this.tp_CrossCalls = new System.Windows.Forms.TabPage();
      this.dgv_CrossCalls = new System.Windows.Forms.DataGridView();
      this.tp_ServerVulnerabilities = new System.Windows.Forms.TabPage();
      this.dgv_Vulnerabilities = new System.Windows.Forms.DataGridView();
      this.tp_ExternalBackRedirect = new System.Windows.Forms.TabPage();
      this.cb_UserAgent = new System.Windows.Forms.ComboBox();
      this.label3 = new System.Windows.Forms.Label();
      this.tp_PageLinksTo.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_PageLinksTo)).BeginInit();
      this.tp_Crawler.SuspendLayout();
      this.tp_HostChain.SuspendLayout();
      this.gb_RequestResults.SuspendLayout();
      this.tc_AnalyzeHttpServer.SuspendLayout();
      this.tp_CrossCalls.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_CrossCalls)).BeginInit();
      this.tp_ServerVulnerabilities.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Vulnerabilities)).BeginInit();
      this.SuspendLayout();
      // 
      // tb_DestinationUrl
      // 
      this.tb_DestinationUrl.Location = new System.Drawing.Point(168, 15);
      this.tb_DestinationUrl.Name = "tb_DestinationUrl";
      this.tb_DestinationUrl.Size = new System.Drawing.Size(310, 20);
      this.tb_DestinationUrl.TabIndex = 1;
      this.tb_DestinationUrl.Text = "ruben.zhdk.ch";
      this.tb_DestinationUrl.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tb_DestinationUrl_KeyDown);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label1.Location = new System.Drawing.Point(12, 18);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(100, 13);
      this.label1.TabIndex = 1;
      this.label1.Text = "Destination URL";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label2.Location = new System.Drawing.Point(131, 18);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(38, 13);
      this.label2.TabIndex = 2;
      this.label2.Text = "http://";
      // 
      // bt_StartStop
      // 
      this.bt_StartStop.Location = new System.Drawing.Point(815, 12);
      this.bt_StartStop.Name = "bt_StartStop";
      this.bt_StartStop.Size = new System.Drawing.Size(75, 23);
      this.bt_StartStop.TabIndex = 3;
      this.bt_StartStop.Text = "Start";
      this.bt_StartStop.UseVisualStyleBackColor = true;
      this.bt_StartStop.Click += new System.EventHandler(this.bt_StartStop_Click);
      // 
      // bgw_Crawler
      // 
      this.bgw_Crawler.WorkerSupportsCancellation = true;
      this.bgw_Crawler.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_Crawler_DoWork);
      this.bgw_Crawler.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_Crawler_RunWorkerCompleted);
      // 
      // l_HostChainKey
      // 
      this.l_HostChainKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.l_HostChainKey.AutoSize = true;
      this.l_HostChainKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_HostChainKey.Location = new System.Drawing.Point(19, 610);
      this.l_HostChainKey.Name = "l_HostChainKey";
      this.l_HostChainKey.Size = new System.Drawing.Size(72, 13);
      this.l_HostChainKey.TabIndex = 16;
      this.l_HostChainKey.Text = "Host chain:";
      // 
      // l_CrawlerKey
      // 
      this.l_CrawlerKey.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.l_CrawlerKey.AutoSize = true;
      this.l_CrawlerKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_CrawlerKey.Location = new System.Drawing.Point(172, 610);
      this.l_CrawlerKey.Name = "l_CrawlerKey";
      this.l_CrawlerKey.Size = new System.Drawing.Size(53, 13);
      this.l_CrawlerKey.TabIndex = 17;
      this.l_CrawlerKey.Text = "Crawler:";
      // 
      // l_CrawlerValue
      // 
      this.l_CrawlerValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.l_CrawlerValue.AutoSize = true;
      this.l_CrawlerValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_CrawlerValue.Location = new System.Drawing.Point(225, 611);
      this.l_CrawlerValue.Name = "l_CrawlerValue";
      this.l_CrawlerValue.Size = new System.Drawing.Size(33, 13);
      this.l_CrawlerValue.TabIndex = 0;
      this.l_CrawlerValue.Text = "Done";
      // 
      // l_HostChainValue
      // 
      this.l_HostChainValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.l_HostChainValue.AutoSize = true;
      this.l_HostChainValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_HostChainValue.Location = new System.Drawing.Point(87, 610);
      this.l_HostChainValue.Name = "l_HostChainValue";
      this.l_HostChainValue.Size = new System.Drawing.Size(33, 13);
      this.l_HostChainValue.TabIndex = 18;
      this.l_HostChainValue.Text = "Done";
      // 
      // bgw_HostChain
      // 
      this.bgw_HostChain.WorkerSupportsCancellation = true;
      this.bgw_HostChain.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bgw_HostChain_DoWork);
      this.bgw_HostChain.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bgw_HostChain_RunWorkerCompleted);
      // 
      // tp_CookieValueInUrl
      // 
      this.tp_CookieValueInUrl.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tp_CookieValueInUrl.Location = new System.Drawing.Point(4, 22);
      this.tp_CookieValueInUrl.Name = "tp_CookieValueInUrl";
      this.tp_CookieValueInUrl.Padding = new System.Windows.Forms.Padding(3);
      this.tp_CookieValueInUrl.Size = new System.Drawing.Size(1170, 519);
      this.tp_CookieValueInUrl.TabIndex = 5;
      this.tp_CookieValueInUrl.Text = "Cookie value in URL";
      // 
      // tp_SharedCookies
      // 
      this.tp_SharedCookies.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tp_SharedCookies.Location = new System.Drawing.Point(4, 22);
      this.tp_SharedCookies.Name = "tp_SharedCookies";
      this.tp_SharedCookies.Padding = new System.Windows.Forms.Padding(3);
      this.tp_SharedCookies.Size = new System.Drawing.Size(1170, 519);
      this.tp_SharedCookies.TabIndex = 4;
      this.tp_SharedCookies.Text = "Shared cookies (sub) domain";
      // 
      // tp_PageLinksTo
      // 
      this.tp_PageLinksTo.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tp_PageLinksTo.Controls.Add(this.dgv_PageLinksTo);
      this.tp_PageLinksTo.Location = new System.Drawing.Point(4, 22);
      this.tp_PageLinksTo.Name = "tp_PageLinksTo";
      this.tp_PageLinksTo.Padding = new System.Windows.Forms.Padding(3);
      this.tp_PageLinksTo.Size = new System.Drawing.Size(1170, 519);
      this.tp_PageLinksTo.TabIndex = 2;
      this.tp_PageLinksTo.Text = "Page links to";
      // 
      // dgv_PageLinksTo
      // 
      this.dgv_PageLinksTo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_PageLinksTo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_PageLinksTo.Location = new System.Drawing.Point(10, 11);
      this.dgv_PageLinksTo.Name = "dgv_PageLinksTo";
      this.dgv_PageLinksTo.RowHeadersVisible = false;
      this.dgv_PageLinksTo.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_PageLinksTo.Size = new System.Drawing.Size(1147, 497);
      this.dgv_PageLinksTo.TabIndex = 0;
      this.dgv_PageLinksTo.DoubleClick += new System.EventHandler(this.DGV_HyperLinks_DoubleClick);
      // 
      // tp_Crawler
      // 
      this.tp_Crawler.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tp_Crawler.Controls.Add(this.tb_CrawlerLog);
      this.tp_Crawler.Location = new System.Drawing.Point(4, 22);
      this.tp_Crawler.Name = "tp_Crawler";
      this.tp_Crawler.Padding = new System.Windows.Forms.Padding(3);
      this.tp_Crawler.Size = new System.Drawing.Size(1170, 519);
      this.tp_Crawler.TabIndex = 1;
      this.tp_Crawler.Text = "Crawler";
      // 
      // tb_CrawlerLog
      // 
      this.tb_CrawlerLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tb_CrawlerLog.Location = new System.Drawing.Point(20, 18);
      this.tb_CrawlerLog.Multiline = true;
      this.tb_CrawlerLog.Name = "tb_CrawlerLog";
      this.tb_CrawlerLog.ReadOnly = true;
      this.tb_CrawlerLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.tb_CrawlerLog.Size = new System.Drawing.Size(1132, 481);
      this.tb_CrawlerLog.TabIndex = 7;
      // 
      // tp_HostChain
      // 
      this.tp_HostChain.BackColor = System.Drawing.Color.WhiteSmoke;
      this.tp_HostChain.Controls.Add(this.tv_ResponseEntities);
      this.tp_HostChain.Controls.Add(this.gb_RequestResults);
      this.tp_HostChain.Location = new System.Drawing.Point(4, 22);
      this.tp_HostChain.Name = "tp_HostChain";
      this.tp_HostChain.Padding = new System.Windows.Forms.Padding(3);
      this.tp_HostChain.Size = new System.Drawing.Size(1170, 519);
      this.tp_HostChain.TabIndex = 0;
      this.tp_HostChain.Text = "Host chain";
      // 
      // tv_ResponseEntities
      // 
      this.tv_ResponseEntities.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
      this.tv_ResponseEntities.Location = new System.Drawing.Point(11, 11);
      this.tv_ResponseEntities.Name = "tv_ResponseEntities";
      this.tv_ResponseEntities.Size = new System.Drawing.Size(350, 493);
      this.tv_ResponseEntities.TabIndex = 5;
      this.tv_ResponseEntities.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tv_ResponseEntities_AfterSelect);
      // 
      // gb_RequestResults
      // 
      this.gb_RequestResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.gb_RequestResults.BackColor = System.Drawing.Color.WhiteSmoke;
      this.gb_RequestResults.Controls.Add(this.tb_IpAddress);
      this.gb_RequestResults.Controls.Add(this.l_IpAddress);
      this.gb_RequestResults.Controls.Add(this.cb_Https);
      this.gb_RequestResults.Controls.Add(this.cb_Http);
      this.gb_RequestResults.Controls.Add(this.l_OpenPorts);
      this.gb_RequestResults.Controls.Add(this.tb_Server);
      this.gb_RequestResults.Controls.Add(this.l_Server);
      this.gb_RequestResults.Controls.Add(this.tb_Hpkp);
      this.gb_RequestResults.Controls.Add(this.tb_RedirectToHttps);
      this.gb_RequestResults.Controls.Add(this.tb_Hsts);
      this.gb_RequestResults.Controls.Add(this.tb_RawHeaders);
      this.gb_RequestResults.Controls.Add(this.l_RawHeaders);
      this.gb_RequestResults.Controls.Add(this.tb_ResponseStatus);
      this.gb_RequestResults.Controls.Add(this.tb_Cookies);
      this.gb_RequestResults.Controls.Add(this.tb_Location);
      this.gb_RequestResults.Controls.Add(this.l_Location);
      this.gb_RequestResults.Controls.Add(this.l_Cookies);
      this.gb_RequestResults.Controls.Add(this.l_RedirectToHttps);
      this.gb_RequestResults.Controls.Add(this.l_Hpkp);
      this.gb_RequestResults.Controls.Add(this.l_Hsts);
      this.gb_RequestResults.Controls.Add(this.l_ResponseStatus);
      this.gb_RequestResults.Location = new System.Drawing.Point(372, 5);
      this.gb_RequestResults.Name = "gb_RequestResults";
      this.gb_RequestResults.Size = new System.Drawing.Size(779, 499);
      this.gb_RequestResults.TabIndex = 4;
      this.gb_RequestResults.TabStop = false;
      // 
      // tb_IpAddress
      // 
      this.tb_IpAddress.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tb_IpAddress.Location = new System.Drawing.Point(161, 19);
      this.tb_IpAddress.Name = "tb_IpAddress";
      this.tb_IpAddress.Size = new System.Drawing.Size(601, 20);
      this.tb_IpAddress.TabIndex = 0;
      this.tb_IpAddress.TabStop = false;
      // 
      // l_IpAddress
      // 
      this.l_IpAddress.AutoSize = true;
      this.l_IpAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_IpAddress.Location = new System.Drawing.Point(22, 22);
      this.l_IpAddress.Name = "l_IpAddress";
      this.l_IpAddress.Size = new System.Drawing.Size(67, 13);
      this.l_IpAddress.TabIndex = 19;
      this.l_IpAddress.Text = "IP address";
      // 
      // cb_Https
      // 
      this.cb_Https.AutoSize = true;
      this.cb_Https.Enabled = false;
      this.cb_Https.Location = new System.Drawing.Point(257, 44);
      this.cb_Https.Name = "cb_Https";
      this.cb_Https.Size = new System.Drawing.Size(85, 17);
      this.cb_Https.TabIndex = 0;
      this.cb_Https.TabStop = false;
      this.cb_Https.Text = "HTTPS/443";
      this.cb_Https.UseVisualStyleBackColor = true;
      // 
      // cb_Http
      // 
      this.cb_Http.AutoSize = true;
      this.cb_Http.Enabled = false;
      this.cb_Http.Location = new System.Drawing.Point(161, 45);
      this.cb_Http.Name = "cb_Http";
      this.cb_Http.Size = new System.Drawing.Size(72, 17);
      this.cb_Http.TabIndex = 0;
      this.cb_Http.TabStop = false;
      this.cb_Http.Text = "HTTP/80";
      this.cb_Http.UseVisualStyleBackColor = true;
      // 
      // l_OpenPorts
      // 
      this.l_OpenPorts.AutoSize = true;
      this.l_OpenPorts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_OpenPorts.Location = new System.Drawing.Point(22, 45);
      this.l_OpenPorts.Name = "l_OpenPorts";
      this.l_OpenPorts.Size = new System.Drawing.Size(69, 13);
      this.l_OpenPorts.TabIndex = 16;
      this.l_OpenPorts.Text = "Open ports";
      // 
      // tb_Server
      // 
      this.tb_Server.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tb_Server.Location = new System.Drawing.Point(161, 70);
      this.tb_Server.Name = "tb_Server";
      this.tb_Server.Size = new System.Drawing.Size(601, 20);
      this.tb_Server.TabIndex = 0;
      this.tb_Server.TabStop = false;
      // 
      // l_Server
      // 
      this.l_Server.AutoSize = true;
      this.l_Server.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_Server.Location = new System.Drawing.Point(22, 73);
      this.l_Server.Name = "l_Server";
      this.l_Server.Size = new System.Drawing.Size(44, 13);
      this.l_Server.TabIndex = 14;
      this.l_Server.Text = "Server";
      // 
      // tb_Hpkp
      // 
      this.tb_Hpkp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tb_Hpkp.Location = new System.Drawing.Point(161, 184);
      this.tb_Hpkp.Name = "tb_Hpkp";
      this.tb_Hpkp.Size = new System.Drawing.Size(601, 20);
      this.tb_Hpkp.TabIndex = 0;
      this.tb_Hpkp.TabStop = false;
      // 
      // tb_RedirectToHttps
      // 
      this.tb_RedirectToHttps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tb_RedirectToHttps.Location = new System.Drawing.Point(161, 127);
      this.tb_RedirectToHttps.Name = "tb_RedirectToHttps";
      this.tb_RedirectToHttps.Size = new System.Drawing.Size(601, 20);
      this.tb_RedirectToHttps.TabIndex = 0;
      this.tb_RedirectToHttps.TabStop = false;
      // 
      // tb_Hsts
      // 
      this.tb_Hsts.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tb_Hsts.Location = new System.Drawing.Point(161, 155);
      this.tb_Hsts.Name = "tb_Hsts";
      this.tb_Hsts.Size = new System.Drawing.Size(601, 20);
      this.tb_Hsts.TabIndex = 0;
      this.tb_Hsts.TabStop = false;
      // 
      // tb_RawHeaders
      // 
      this.tb_RawHeaders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tb_RawHeaders.Location = new System.Drawing.Point(161, 271);
      this.tb_RawHeaders.Multiline = true;
      this.tb_RawHeaders.Name = "tb_RawHeaders";
      this.tb_RawHeaders.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.tb_RawHeaders.Size = new System.Drawing.Size(601, 211);
      this.tb_RawHeaders.TabIndex = 0;
      this.tb_RawHeaders.TabStop = false;
      // 
      // l_RawHeaders
      // 
      this.l_RawHeaders.AutoSize = true;
      this.l_RawHeaders.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_RawHeaders.Location = new System.Drawing.Point(22, 274);
      this.l_RawHeaders.Name = "l_RawHeaders";
      this.l_RawHeaders.Size = new System.Drawing.Size(81, 13);
      this.l_RawHeaders.TabIndex = 9;
      this.l_RawHeaders.Text = "Raw headers";
      // 
      // tb_ResponseStatus
      // 
      this.tb_ResponseStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tb_ResponseStatus.Location = new System.Drawing.Point(161, 99);
      this.tb_ResponseStatus.Name = "tb_ResponseStatus";
      this.tb_ResponseStatus.Size = new System.Drawing.Size(601, 20);
      this.tb_ResponseStatus.TabIndex = 0;
      this.tb_ResponseStatus.TabStop = false;
      // 
      // tb_Cookies
      // 
      this.tb_Cookies.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tb_Cookies.Location = new System.Drawing.Point(161, 213);
      this.tb_Cookies.Name = "tb_Cookies";
      this.tb_Cookies.Size = new System.Drawing.Size(601, 20);
      this.tb_Cookies.TabIndex = 0;
      this.tb_Cookies.TabStop = false;
      // 
      // tb_Location
      // 
      this.tb_Location.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tb_Location.Location = new System.Drawing.Point(161, 242);
      this.tb_Location.Name = "tb_Location";
      this.tb_Location.Size = new System.Drawing.Size(601, 20);
      this.tb_Location.TabIndex = 0;
      this.tb_Location.TabStop = false;
      // 
      // l_Location
      // 
      this.l_Location.AutoSize = true;
      this.l_Location.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_Location.Location = new System.Drawing.Point(22, 245);
      this.l_Location.Name = "l_Location";
      this.l_Location.Size = new System.Drawing.Size(56, 13);
      this.l_Location.TabIndex = 5;
      this.l_Location.Text = "Location";
      // 
      // l_Cookies
      // 
      this.l_Cookies.AutoSize = true;
      this.l_Cookies.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_Cookies.Location = new System.Drawing.Point(22, 216);
      this.l_Cookies.Name = "l_Cookies";
      this.l_Cookies.Size = new System.Drawing.Size(52, 13);
      this.l_Cookies.TabIndex = 4;
      this.l_Cookies.Text = "Cookies";
      // 
      // l_RedirectToHttps
      // 
      this.l_RedirectToHttps.AutoSize = true;
      this.l_RedirectToHttps.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_RedirectToHttps.Location = new System.Drawing.Point(22, 134);
      this.l_RedirectToHttps.Name = "l_RedirectToHttps";
      this.l_RedirectToHttps.Size = new System.Drawing.Size(115, 13);
      this.l_RedirectToHttps.TabIndex = 3;
      this.l_RedirectToHttps.Text = "Redirect to HTTPS";
      // 
      // l_Hpkp
      // 
      this.l_Hpkp.AutoSize = true;
      this.l_Hpkp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_Hpkp.Location = new System.Drawing.Point(22, 189);
      this.l_Hpkp.Name = "l_Hpkp";
      this.l_Hpkp.Size = new System.Drawing.Size(40, 13);
      this.l_Hpkp.TabIndex = 2;
      this.l_Hpkp.Text = "HPKP";
      // 
      // l_Hsts
      // 
      this.l_Hsts.AutoSize = true;
      this.l_Hsts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_Hsts.Location = new System.Drawing.Point(22, 162);
      this.l_Hsts.Name = "l_Hsts";
      this.l_Hsts.Size = new System.Drawing.Size(40, 13);
      this.l_Hsts.TabIndex = 1;
      this.l_Hsts.Text = "HSTS";
      // 
      // l_ResponseStatus
      // 
      this.l_ResponseStatus.AutoSize = true;
      this.l_ResponseStatus.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.l_ResponseStatus.Location = new System.Drawing.Point(22, 102);
      this.l_ResponseStatus.Name = "l_ResponseStatus";
      this.l_ResponseStatus.Size = new System.Drawing.Size(101, 13);
      this.l_ResponseStatus.TabIndex = 0;
      this.l_ResponseStatus.Text = "Response status";
      // 
      // tc_AnalyzeHttpServer
      // 
      this.tc_AnalyzeHttpServer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.tc_AnalyzeHttpServer.Controls.Add(this.tp_HostChain);
      this.tc_AnalyzeHttpServer.Controls.Add(this.tp_CrossCalls);
      this.tc_AnalyzeHttpServer.Controls.Add(this.tp_PageLinksTo);
      this.tc_AnalyzeHttpServer.Controls.Add(this.tp_ServerVulnerabilities);
      this.tc_AnalyzeHttpServer.Controls.Add(this.tp_Crawler);
      this.tc_AnalyzeHttpServer.Controls.Add(this.tp_ExternalBackRedirect);
      this.tc_AnalyzeHttpServer.Controls.Add(this.tp_SharedCookies);
      this.tc_AnalyzeHttpServer.Controls.Add(this.tp_CookieValueInUrl);
      this.tc_AnalyzeHttpServer.Location = new System.Drawing.Point(12, 62);
      this.tc_AnalyzeHttpServer.Name = "tc_AnalyzeHttpServer";
      this.tc_AnalyzeHttpServer.SelectedIndex = 0;
      this.tc_AnalyzeHttpServer.Size = new System.Drawing.Size(1178, 545);
      this.tc_AnalyzeHttpServer.TabIndex = 4;
      // 
      // tp_CrossCalls
      // 
      this.tp_CrossCalls.Controls.Add(this.dgv_CrossCalls);
      this.tp_CrossCalls.Location = new System.Drawing.Point(4, 22);
      this.tp_CrossCalls.Name = "tp_CrossCalls";
      this.tp_CrossCalls.Padding = new System.Windows.Forms.Padding(3);
      this.tp_CrossCalls.Size = new System.Drawing.Size(1170, 519);
      this.tp_CrossCalls.TabIndex = 8;
      this.tp_CrossCalls.Text = "Cross calls";
      this.tp_CrossCalls.UseVisualStyleBackColor = true;
      // 
      // dgv_CrossCalls
      // 
      this.dgv_CrossCalls.AllowUserToAddRows = false;
      this.dgv_CrossCalls.AllowUserToDeleteRows = false;
      this.dgv_CrossCalls.AllowUserToResizeRows = false;
      this.dgv_CrossCalls.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_CrossCalls.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_CrossCalls.Location = new System.Drawing.Point(10, 11);
      this.dgv_CrossCalls.MultiSelect = false;
      this.dgv_CrossCalls.Name = "dgv_CrossCalls";
      this.dgv_CrossCalls.RowHeadersVisible = false;
      this.dgv_CrossCalls.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.False;
      this.dgv_CrossCalls.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_CrossCalls.Size = new System.Drawing.Size(1147, 497);
      this.dgv_CrossCalls.TabIndex = 0;
      this.dgv_CrossCalls.DoubleClick += new System.EventHandler(this.DGV_CrossCalls_DoubleClick);
      // 
      // tp_ServerVulnerabilities
      // 
      this.tp_ServerVulnerabilities.Controls.Add(this.dgv_Vulnerabilities);
      this.tp_ServerVulnerabilities.Location = new System.Drawing.Point(4, 22);
      this.tp_ServerVulnerabilities.Name = "tp_ServerVulnerabilities";
      this.tp_ServerVulnerabilities.Padding = new System.Windows.Forms.Padding(3);
      this.tp_ServerVulnerabilities.Size = new System.Drawing.Size(1170, 519);
      this.tp_ServerVulnerabilities.TabIndex = 6;
      this.tp_ServerVulnerabilities.Text = "Server vulnerabilities";
      this.tp_ServerVulnerabilities.UseVisualStyleBackColor = true;
      // 
      // dgv_Vulnerabilities
      // 
      this.dgv_Vulnerabilities.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
      this.dgv_Vulnerabilities.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
      this.dgv_Vulnerabilities.Location = new System.Drawing.Point(10, 11);
      this.dgv_Vulnerabilities.MultiSelect = false;
      this.dgv_Vulnerabilities.Name = "dgv_Vulnerabilities";
      this.dgv_Vulnerabilities.RowHeadersVisible = false;
      this.dgv_Vulnerabilities.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
      this.dgv_Vulnerabilities.Size = new System.Drawing.Size(1147, 497);
      this.dgv_Vulnerabilities.TabIndex = 0;
      this.dgv_Vulnerabilities.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.dgv_Vulnerabilities_MouseDoubleClick);
      // 
      // tp_ExternalBackRedirect
      // 
      this.tp_ExternalBackRedirect.Location = new System.Drawing.Point(4, 22);
      this.tp_ExternalBackRedirect.Name = "tp_ExternalBackRedirect";
      this.tp_ExternalBackRedirect.Padding = new System.Windows.Forms.Padding(3);
      this.tp_ExternalBackRedirect.Size = new System.Drawing.Size(1170, 519);
      this.tp_ExternalBackRedirect.TabIndex = 7;
      this.tp_ExternalBackRedirect.Text = "External back redirect";
      this.tp_ExternalBackRedirect.UseVisualStyleBackColor = true;
      // 
      // cb_UserAgent
      // 
      this.cb_UserAgent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cb_UserAgent.FormattingEnabled = true;
      this.cb_UserAgent.Location = new System.Drawing.Point(596, 15);
      this.cb_UserAgent.Name = "cb_UserAgent";
      this.cb_UserAgent.Size = new System.Drawing.Size(180, 21);
      this.cb_UserAgent.TabIndex = 2;
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.label3.Location = new System.Drawing.Point(521, 18);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(69, 13);
      this.label3.TabIndex = 20;
      this.label3.Text = "User agent";
      // 
      // Elk_Main
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1202, 636);
      this.Controls.Add(this.label3);
      this.Controls.Add(this.cb_UserAgent);
      this.Controls.Add(this.l_HostChainValue);
      this.Controls.Add(this.l_CrawlerValue);
      this.Controls.Add(this.l_CrawlerKey);
      this.Controls.Add(this.l_HostChainKey);
      this.Controls.Add(this.tc_AnalyzeHttpServer);
      this.Controls.Add(this.bt_StartStop);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.label1);
      this.Controls.Add(this.tb_DestinationUrl);
      this.MinimumSize = new System.Drawing.Size(1218, 674);
      this.Name = "Elk_Main";
      this.Text = "Elk - The HTTP request server chain analyzer";
      this.tp_PageLinksTo.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_PageLinksTo)).EndInit();
      this.tp_Crawler.ResumeLayout(false);
      this.tp_Crawler.PerformLayout();
      this.tp_HostChain.ResumeLayout(false);
      this.gb_RequestResults.ResumeLayout(false);
      this.gb_RequestResults.PerformLayout();
      this.tc_AnalyzeHttpServer.ResumeLayout(false);
      this.tp_CrossCalls.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_CrossCalls)).EndInit();
      this.tp_ServerVulnerabilities.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.dgv_Vulnerabilities)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox tb_DestinationUrl;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.Button bt_StartStop;
    private System.ComponentModel.BackgroundWorker bgw_Crawler;
    private System.Windows.Forms.Label l_HostChainKey;
    private System.Windows.Forms.Label l_CrawlerKey;
    private System.Windows.Forms.Label l_CrawlerValue;
    private System.Windows.Forms.Label l_HostChainValue;
    private System.ComponentModel.BackgroundWorker bgw_HostChain;
    private System.Windows.Forms.TabPage tp_CookieValueInUrl;
    private System.Windows.Forms.TabPage tp_SharedCookies;
    private System.Windows.Forms.TabPage tp_PageLinksTo;
    private System.Windows.Forms.DataGridView dgv_PageLinksTo;
    private System.Windows.Forms.TabPage tp_Crawler;
    private System.Windows.Forms.TextBox tb_CrawlerLog;
    private System.Windows.Forms.TabPage tp_HostChain;
    private System.Windows.Forms.TreeView tv_ResponseEntities;
    private System.Windows.Forms.GroupBox gb_RequestResults;
    private System.Windows.Forms.TextBox tb_Server;
    private System.Windows.Forms.Label l_Server;
    private System.Windows.Forms.TextBox tb_Hpkp;
    private System.Windows.Forms.TextBox tb_RedirectToHttps;
    private System.Windows.Forms.TextBox tb_Hsts;
    private System.Windows.Forms.TextBox tb_RawHeaders;
    private System.Windows.Forms.Label l_RawHeaders;
    private System.Windows.Forms.TextBox tb_ResponseStatus;
    private System.Windows.Forms.TextBox tb_Cookies;
    private System.Windows.Forms.TextBox tb_Location;
    private System.Windows.Forms.Label l_Location;
    private System.Windows.Forms.Label l_Cookies;
    private System.Windows.Forms.Label l_RedirectToHttps;
    private System.Windows.Forms.Label l_Hpkp;
    private System.Windows.Forms.Label l_Hsts;
    private System.Windows.Forms.Label l_ResponseStatus;
    private System.Windows.Forms.TabControl tc_AnalyzeHttpServer;
    private System.Windows.Forms.TabPage tp_ServerVulnerabilities;
    private System.Windows.Forms.DataGridView dgv_Vulnerabilities;
    private System.Windows.Forms.CheckBox cb_Https;
    private System.Windows.Forms.CheckBox cb_Http;
    private System.Windows.Forms.Label l_OpenPorts;
    private System.Windows.Forms.TabPage tp_ExternalBackRedirect;
    private System.Windows.Forms.TextBox tb_IpAddress;
    private System.Windows.Forms.Label l_IpAddress;
    private System.Windows.Forms.ComboBox cb_UserAgent;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TabPage tp_CrossCalls;
    private System.Windows.Forms.DataGridView dgv_CrossCalls;
  }
}

