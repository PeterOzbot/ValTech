using System.Drawing;
using System.Windows.Forms;

namespace CustomScrollbarTableLayoutPanel
{
    partial class ItemLineControl
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
            this.LineLabel = new System.Windows.Forms.Label();
            this.QuantityLabel = new System.Windows.Forms.Label();
            this.NameLabel = new System.Windows.Forms.Label();
            this.AmountLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // LineLabel
            // 
            this.LineLabel.BackColor = System.Drawing.Color.Gainsboro;
            this.LineLabel.Location = new System.Drawing.Point(0, 34);
            this.LineLabel.Name = "LineLabel";
            this.LineLabel.Size = new System.Drawing.Size(462, 1);
            this.LineLabel.TabIndex = 0;
            // 
            // QuantityLabel
            // 
            this.QuantityLabel.Location = new System.Drawing.Point(0, 0);
            this.QuantityLabel.Margin = new System.Windows.Forms.Padding(0);
            this.QuantityLabel.Name = "QuantityLabel";
            this.QuantityLabel.Size = new System.Drawing.Size(95, 35);
            this.QuantityLabel.TabIndex = 1;
            this.QuantityLabel.Text = "1,000";
            this.QuantityLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // NameLabel
            // 
            this.NameLabel.BackColor = System.Drawing.Color.White;
            this.NameLabel.Location = new System.Drawing.Point(95, 0);
            this.NameLabel.Margin = new System.Windows.Forms.Padding(0);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(230, 35);
            this.NameLabel.TabIndex = 2;
            this.NameLabel.Text = "SOLATA KRHNOLISNATA GENTILE";
            this.NameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AmountLabel
            // 
            this.AmountLabel.Location = new System.Drawing.Point(325, 0);
            this.AmountLabel.Margin = new System.Windows.Forms.Padding(0);
            this.AmountLabel.Name = "AmountLabel";
            this.AmountLabel.Size = new System.Drawing.Size(137, 35);
            this.AmountLabel.TabIndex = 3;
            this.AmountLabel.Text = "10.000,35 €";
            this.AmountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ItemLineControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.LineLabel);
            this.Controls.Add(this.AmountLabel);
            this.Controls.Add(this.NameLabel);
            this.Controls.Add(this.QuantityLabel);
            this.Name = "ItemLineControl";
            this.Size = new System.Drawing.Size(462, 35);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label LineLabel;
        private Label QuantityLabel;
        private Label NameLabel;
        private Label AmountLabel;
    }
}
