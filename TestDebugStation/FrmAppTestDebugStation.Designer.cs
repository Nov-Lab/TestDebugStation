
namespace TestDebugStation
{
    partial class FrmAppTestDebugStation
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MnuDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.MnuDebug_LaunchDebugStation = new System.Windows.Forms.ToolStripMenuItem();
            this.LstTestMenu = new System.Windows.Forms.ListBox();
            this.TtlTestMenu = new System.Windows.Forms.Label();
            this.BtnExecuteTest = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuDebug});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(319, 24);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MnuDebug
            // 
            this.MnuDebug.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MnuDebug_LaunchDebugStation});
            this.MnuDebug.Name = "MnuDebug";
            this.MnuDebug.Size = new System.Drawing.Size(71, 20);
            this.MnuDebug.Text = "デバッグ(&D)";
            // 
            // MnuDebug_LaunchDebugStation
            // 
            this.MnuDebug_LaunchDebugStation.Name = "MnuDebug_LaunchDebugStation";
            this.MnuDebug_LaunchDebugStation.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.MnuDebug_LaunchDebugStation.Size = new System.Drawing.Size(228, 22);
            this.MnuDebug_LaunchDebugStation.Text = "デバッグステーションを起動(&L)";
            this.MnuDebug_LaunchDebugStation.Click += new System.EventHandler(this.MnuDebug_LaunchDebugStation_Click);
            // 
            // LstTestMenu
            // 
            this.LstTestMenu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.LstTestMenu.Font = new System.Drawing.Font("ＭＳ ゴシック", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.LstTestMenu.FormattingEnabled = true;
            this.LstTestMenu.ItemHeight = 12;
            this.LstTestMenu.Location = new System.Drawing.Point(8, 48);
            this.LstTestMenu.Name = "LstTestMenu";
            this.LstTestMenu.Size = new System.Drawing.Size(304, 280);
            this.LstTestMenu.TabIndex = 1;
            this.LstTestMenu.DoubleClick += new System.EventHandler(this.LstTestMenu_DoubleClick);
            this.LstTestMenu.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LstTestMenu_KeyPress);
            // 
            // TtlTestMenu
            // 
            this.TtlTestMenu.AutoSize = true;
            this.TtlTestMenu.Location = new System.Drawing.Point(8, 32);
            this.TtlTestMenu.Name = "TtlTestMenu";
            this.TtlTestMenu.Size = new System.Drawing.Size(57, 12);
            this.TtlTestMenu.TabIndex = 0;
            this.TtlTestMenu.Text = "テスト項目:";
            // 
            // BtnExecuteTest
            // 
            this.BtnExecuteTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnExecuteTest.Location = new System.Drawing.Point(216, 336);
            this.BtnExecuteTest.Name = "BtnExecuteTest";
            this.BtnExecuteTest.Size = new System.Drawing.Size(96, 24);
            this.BtnExecuteTest.TabIndex = 2;
            this.BtnExecuteTest.Text = "テスト実行";
            this.BtnExecuteTest.UseVisualStyleBackColor = true;
            this.BtnExecuteTest.Click += new System.EventHandler(this.BtnExecuteTest_Click);
            // 
            // FrmAppTestDebugStation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 370);
            this.Controls.Add(this.BtnExecuteTest);
            this.Controls.Add(this.TtlTestMenu);
            this.Controls.Add(this.LstTestMenu);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmAppTestDebugStation";
            this.Text = "TestDebugStation";
            this.Load += new System.EventHandler(this.FrmAppTestDebugStation_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MnuDebug;
        private System.Windows.Forms.ToolStripMenuItem MnuDebug_LaunchDebugStation;
        private System.Windows.Forms.ListBox LstTestMenu;
        private System.Windows.Forms.Label TtlTestMenu;
        private System.Windows.Forms.Button BtnExecuteTest;
    }
}

