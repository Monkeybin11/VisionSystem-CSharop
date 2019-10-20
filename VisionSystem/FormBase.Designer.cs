namespace VisionSystem
{
    partial class FormBase
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.文件ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新增项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.选择项目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.保存ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.另存为ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.相机设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.密码设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.项目设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.项目编辑ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.坐标转换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.相机标定ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.停止流程ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.切换用户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.技术员ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.管理员ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.管理员ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.视图ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.工具箱ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.流程栏ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.日志ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.帮助ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关于ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonStart = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonLoop = new System.Windows.Forms.ToolStripButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dockPanel1 = new WeifenLuo.WinFormsUI.Docking.DockPanel();
            this.menuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightBlue;
            this.menuStrip1.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.文件ToolStripMenuItem,
            this.设置ToolStripMenuItem,
            this.操作ToolStripMenuItem,
            this.视图ToolStripMenuItem,
            this.帮助ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1346, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 文件ToolStripMenuItem
            // 
            this.文件ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新增项目ToolStripMenuItem,
            this.选择项目ToolStripMenuItem,
            this.保存ToolStripMenuItem,
            this.另存为ToolStripMenuItem});
            this.文件ToolStripMenuItem.Name = "文件ToolStripMenuItem";
            this.文件ToolStripMenuItem.Size = new System.Drawing.Size(66, 24);
            this.文件ToolStripMenuItem.Text = "项目(&F)";
            // 
            // 新增项目ToolStripMenuItem
            // 
            this.新增项目ToolStripMenuItem.Name = "新增项目ToolStripMenuItem";
            this.新增项目ToolStripMenuItem.Size = new System.Drawing.Size(140, 24);
            this.新增项目ToolStripMenuItem.Text = "新建(&N)";
            this.新增项目ToolStripMenuItem.Click += new System.EventHandler(this.新增项目ToolStripMenuItem_Click);
            // 
            // 选择项目ToolStripMenuItem
            // 
            this.选择项目ToolStripMenuItem.Name = "选择项目ToolStripMenuItem";
            this.选择项目ToolStripMenuItem.Size = new System.Drawing.Size(140, 24);
            this.选择项目ToolStripMenuItem.Text = "打开(&O)";
            // 
            // 保存ToolStripMenuItem
            // 
            this.保存ToolStripMenuItem.Name = "保存ToolStripMenuItem";
            this.保存ToolStripMenuItem.Size = new System.Drawing.Size(140, 24);
            this.保存ToolStripMenuItem.Text = "保存(&S)";
            // 
            // 另存为ToolStripMenuItem
            // 
            this.另存为ToolStripMenuItem.Name = "另存为ToolStripMenuItem";
            this.另存为ToolStripMenuItem.Size = new System.Drawing.Size(140, 24);
            this.另存为ToolStripMenuItem.Text = "另存为(&A)";
            // 
            // 设置ToolStripMenuItem
            // 
            this.设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.相机设置ToolStripMenuItem,
            this.密码设置ToolStripMenuItem,
            this.项目设置ToolStripMenuItem,
            this.项目编辑ToolStripMenuItem});
            this.设置ToolStripMenuItem.Name = "设置ToolStripMenuItem";
            this.设置ToolStripMenuItem.Size = new System.Drawing.Size(67, 24);
            this.设置ToolStripMenuItem.Text = "设置(&S)";
            // 
            // 相机设置ToolStripMenuItem
            // 
            this.相机设置ToolStripMenuItem.Name = "相机设置ToolStripMenuItem";
            this.相机设置ToolStripMenuItem.Size = new System.Drawing.Size(154, 24);
            this.相机设置ToolStripMenuItem.Text = "系统设置(&S)";
            this.相机设置ToolStripMenuItem.Click += new System.EventHandler(this.相机设置ToolStripMenuItem_Click);
            // 
            // 密码设置ToolStripMenuItem
            // 
            this.密码设置ToolStripMenuItem.Name = "密码设置ToolStripMenuItem";
            this.密码设置ToolStripMenuItem.Size = new System.Drawing.Size(154, 24);
            this.密码设置ToolStripMenuItem.Text = "密码设置(&A)";
            this.密码设置ToolStripMenuItem.Click += new System.EventHandler(this.密码设置ToolStripMenuItem_Click);
            // 
            // 项目设置ToolStripMenuItem
            // 
            this.项目设置ToolStripMenuItem.Name = "项目设置ToolStripMenuItem";
            this.项目设置ToolStripMenuItem.Size = new System.Drawing.Size(154, 24);
            this.项目设置ToolStripMenuItem.Text = "项目设置(&P)";
            this.项目设置ToolStripMenuItem.Click += new System.EventHandler(this.项目设置ToolStripMenuItem_Click);
            // 
            // 项目编辑ToolStripMenuItem
            // 
            this.项目编辑ToolStripMenuItem.Name = "项目编辑ToolStripMenuItem";
            this.项目编辑ToolStripMenuItem.Size = new System.Drawing.Size(154, 24);
            this.项目编辑ToolStripMenuItem.Text = "界面设置(&U)";
            this.项目编辑ToolStripMenuItem.Click += new System.EventHandler(this.界面设置ToolStripMenuItem_Click);
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.坐标转换ToolStripMenuItem,
            this.相机标定ToolStripMenuItem1,
            this.停止流程ToolStripMenuItem,
            this.toolStripSeparator3,
            this.切换用户ToolStripMenuItem});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.操作ToolStripMenuItem.Text = "操作(&O)";
            // 
            // 坐标转换ToolStripMenuItem
            // 
            this.坐标转换ToolStripMenuItem.Name = "坐标转换ToolStripMenuItem";
            this.坐标转换ToolStripMenuItem.Size = new System.Drawing.Size(154, 24);
            this.坐标转换ToolStripMenuItem.Text = "执行流程(&E)";
            // 
            // 相机标定ToolStripMenuItem1
            // 
            this.相机标定ToolStripMenuItem1.Name = "相机标定ToolStripMenuItem1";
            this.相机标定ToolStripMenuItem1.Size = new System.Drawing.Size(154, 24);
            this.相机标定ToolStripMenuItem1.Text = "运行流程(&R)";
            // 
            // 停止流程ToolStripMenuItem
            // 
            this.停止流程ToolStripMenuItem.Name = "停止流程ToolStripMenuItem";
            this.停止流程ToolStripMenuItem.Size = new System.Drawing.Size(154, 24);
            this.停止流程ToolStripMenuItem.Text = "停止流程(&S)";
            this.停止流程ToolStripMenuItem.Click += new System.EventHandler(this.停止流程ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(151, 6);
            // 
            // 切换用户ToolStripMenuItem
            // 
            this.切换用户ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.技术员ToolStripMenuItem1,
            this.管理员ToolStripMenuItem1,
            this.管理员ToolStripMenuItem2});
            this.切换用户ToolStripMenuItem.Name = "切换用户ToolStripMenuItem";
            this.切换用户ToolStripMenuItem.Size = new System.Drawing.Size(154, 24);
            this.切换用户ToolStripMenuItem.Text = "切换用户(&U)";
            // 
            // 技术员ToolStripMenuItem1
            // 
            this.技术员ToolStripMenuItem1.Name = "技术员ToolStripMenuItem1";
            this.技术员ToolStripMenuItem1.Size = new System.Drawing.Size(141, 24);
            this.技术员ToolStripMenuItem1.Text = "操作员(&O)";
            // 
            // 管理员ToolStripMenuItem1
            // 
            this.管理员ToolStripMenuItem1.Name = "管理员ToolStripMenuItem1";
            this.管理员ToolStripMenuItem1.Size = new System.Drawing.Size(141, 24);
            this.管理员ToolStripMenuItem1.Text = "技术员(&T)";
            // 
            // 管理员ToolStripMenuItem2
            // 
            this.管理员ToolStripMenuItem2.Name = "管理员ToolStripMenuItem2";
            this.管理员ToolStripMenuItem2.Size = new System.Drawing.Size(141, 24);
            this.管理员ToolStripMenuItem2.Text = "管理员(&A)";
            // 
            // 视图ToolStripMenuItem
            // 
            this.视图ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.工具箱ToolStripMenuItem,
            this.流程栏ToolStripMenuItem,
            this.数据ToolStripMenuItem,
            this.日志ToolStripMenuItem});
            this.视图ToolStripMenuItem.Name = "视图ToolStripMenuItem";
            this.视图ToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.视图ToolStripMenuItem.Text = "视图(&V)";
            // 
            // 工具箱ToolStripMenuItem
            // 
            this.工具箱ToolStripMenuItem.Name = "工具箱ToolStripMenuItem";
            this.工具箱ToolStripMenuItem.Size = new System.Drawing.Size(141, 24);
            this.工具箱ToolStripMenuItem.Text = "工具箱(&T)";
            this.工具箱ToolStripMenuItem.Click += new System.EventHandler(this.工具箱ToolStripMenuItem_Click);
            // 
            // 流程栏ToolStripMenuItem
            // 
            this.流程栏ToolStripMenuItem.Name = "流程栏ToolStripMenuItem";
            this.流程栏ToolStripMenuItem.Size = new System.Drawing.Size(141, 24);
            this.流程栏ToolStripMenuItem.Text = "流程栏(&P)";
            this.流程栏ToolStripMenuItem.Click += new System.EventHandler(this.流程栏ToolStripMenuItem_Click);
            // 
            // 数据ToolStripMenuItem
            // 
            this.数据ToolStripMenuItem.Name = "数据ToolStripMenuItem";
            this.数据ToolStripMenuItem.Size = new System.Drawing.Size(141, 24);
            this.数据ToolStripMenuItem.Text = "数据栏(&D)";
            this.数据ToolStripMenuItem.Click += new System.EventHandler(this.数据ToolStripMenuItem_Click);
            // 
            // 日志ToolStripMenuItem
            // 
            this.日志ToolStripMenuItem.Name = "日志ToolStripMenuItem";
            this.日志ToolStripMenuItem.Size = new System.Drawing.Size(141, 24);
            this.日志ToolStripMenuItem.Text = "日志(&L)";
            this.日志ToolStripMenuItem.Click += new System.EventHandler(this.日志ToolStripMenuItem_Click);
            // 
            // 帮助ToolStripMenuItem
            // 
            this.帮助ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.关于ToolStripMenuItem});
            this.帮助ToolStripMenuItem.Name = "帮助ToolStripMenuItem";
            this.帮助ToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
            this.帮助ToolStripMenuItem.Text = "帮助(&H)";
            // 
            // 关于ToolStripMenuItem
            // 
            this.关于ToolStripMenuItem.Name = "关于ToolStripMenuItem";
            this.关于ToolStripMenuItem.Size = new System.Drawing.Size(126, 24);
            this.关于ToolStripMenuItem.Text = "关于(&A)";
            this.关于ToolStripMenuItem.Click += new System.EventHandler(this.关于ToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.AliceBlue;
            this.toolStrip1.Font = new System.Drawing.Font("微软雅黑", 11F);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripButton3,
            this.toolStripButton4,
            this.toolStripSeparator1,
            this.toolStripButtonStart,
            this.toolStripButtonStop,
            this.toolStripButtonLoop});
            this.toolStrip1.Location = new System.Drawing.Point(0, 28);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1346, 27);
            this.toolStrip1.TabIndex = 20;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.Image = global::VisionSystem.Properties.Resources.放大;
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(59, 24);
            this.toolStripButton2.Text = "放大";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.Image = global::VisionSystem.Properties.Resources.缩小;
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(59, 24);
            this.toolStripButton3.Text = "缩小";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.Image = global::VisionSystem.Properties.Resources.还原;
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(59, 24);
            this.toolStripButton4.Text = "还原";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonStart
            // 
            this.toolStripButtonStart.Image = global::VisionSystem.Properties.Resources.start;
            this.toolStripButtonStart.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStart.Name = "toolStripButtonStart";
            this.toolStripButtonStart.Size = new System.Drawing.Size(59, 24);
            this.toolStripButtonStart.Text = "开始";
            this.toolStripButtonStart.Click += new System.EventHandler(this.toolStripButtonStart_Click);
            // 
            // toolStripButtonStop
            // 
            this.toolStripButtonStop.Image = global::VisionSystem.Properties.Resources.stop;
            this.toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonStop.Name = "toolStripButtonStop";
            this.toolStripButtonStop.Size = new System.Drawing.Size(59, 24);
            this.toolStripButtonStop.Text = "停止";
            this.toolStripButtonStop.Click += new System.EventHandler(this.toolStripButtonStop_Click);
            // 
            // toolStripButtonLoop
            // 
            this.toolStripButtonLoop.Image = global::VisionSystem.Properties.Resources.reset;
            this.toolStripButtonLoop.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonLoop.Name = "toolStripButtonLoop";
            this.toolStripButtonLoop.Size = new System.Drawing.Size(59, 24);
            this.toolStripButtonLoop.Text = "循环";
            this.toolStripButtonLoop.Click += new System.EventHandler(this.toolStripButtonLoop_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dockPanel1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 55);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1346, 671);
            this.panel1.TabIndex = 21;
            // 
            // dockPanel1
            // 
            this.dockPanel1.ActiveAutoHideContent = null;
            this.dockPanel1.AllowDrop = true;
            this.dockPanel1.AutoSize = true;
            this.dockPanel1.BackColor = System.Drawing.Color.Black;
            this.dockPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dockPanel1.DocumentStyle = WeifenLuo.WinFormsUI.Docking.DocumentStyle.DockingSdi;
            this.dockPanel1.Location = new System.Drawing.Point(0, 0);
            this.dockPanel1.Name = "dockPanel1";
            this.dockPanel1.ShowPadIcon = false;
            this.dockPanel1.Size = new System.Drawing.Size(1346, 671);
            this.dockPanel1.TabIndex = 0;
            // 
            // FormBase
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1346, 726);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TabText = "MachineVision";
            this.Text = "MachineVision";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormBase_FormClosing);
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 文件ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 视图ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 帮助ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新增项目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 选择项目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 相机设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 坐标转换ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 相机标定ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 保存ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 另存为ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 密码设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 项目设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 项目编辑ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 停止流程ToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonStart;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripButton toolStripButtonLoop;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 切换用户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 技术员ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 管理员ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 管理员ToolStripMenuItem2;
        public System.Windows.Forms.ToolStripMenuItem 流程栏ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 工具箱ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 数据ToolStripMenuItem;
        public System.Windows.Forms.ToolStripMenuItem 日志ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 关于ToolStripMenuItem;
        public WeifenLuo.WinFormsUI.Docking.DockPanel dockPanel1;
    }
}

