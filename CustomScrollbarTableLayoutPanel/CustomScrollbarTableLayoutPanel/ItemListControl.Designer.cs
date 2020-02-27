namespace CustomScrollbarTableLayoutPanel
{
    partial class ItemListControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ItemTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.ScrollbarPanel = new CustomScrollbarTableLayoutPanel.ItemListScrollbar();
            this.SuspendLayout();
            // 
            // ItemTableLayout
            // 
            this.ItemTableLayout.AutoScroll = true;
            this.ItemTableLayout.AutoScrollMinSize = new System.Drawing.Size(0, 510);
            this.ItemTableLayout.ColumnCount = 2;
            this.ItemTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ItemTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ItemTableLayout.Location = new System.Drawing.Point(0, 0);
            this.ItemTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.ItemTableLayout.MaximumSize = new System.Drawing.Size(482, 500);
            this.ItemTableLayout.Name = "ItemTableLayout";
            this.ItemTableLayout.RowCount = 2;
            this.ItemTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ItemTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.ItemTableLayout.Size = new System.Drawing.Size(482, 500);
            this.ItemTableLayout.TabIndex = 0;
            // 
            // ScrollbarPanel
            // 
            this.ScrollbarPanel.BackColor = System.Drawing.Color.White;
            this.ScrollbarPanel.Location = new System.Drawing.Point(462, 0);
            this.ScrollbarPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ScrollbarPanel.Name = "ScrollbarPanel";
            this.ScrollbarPanel.Size = new System.Drawing.Size(20, 500);
            this.ScrollbarPanel.TabIndex = 1;
            this.ScrollbarPanel.TotalSize = 0;
            this.ScrollbarPanel.Value = 0;
            this.ScrollbarPanel.VisibleSize = 0;
            // 
            // ItemListControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.ScrollbarPanel);
            this.Controls.Add(this.ItemTableLayout);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ItemListControl";
            this.Size = new System.Drawing.Size(482, 500);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel ItemTableLayout;
        private ItemListScrollbar ScrollbarPanel;
    }
}
