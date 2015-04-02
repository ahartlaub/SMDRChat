namespace SDMPSample
{
    partial class Form1
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
            this.Tenants = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.CampaignProperties = new System.Windows.Forms.TextBox();
            this.CampaignID = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.GetProperties = new System.Windows.Forms.Button();
            this.SetProperties = new System.Windows.Forms.Button();
            this.PropertyData = new System.Windows.Forms.TextBox();
            this.StartCampaign = new System.Windows.Forms.Button();
            this.StopCampaign = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Tenants
            // 
            this.Tenants.FormattingEnabled = true;
            this.Tenants.Location = new System.Drawing.Point(12, 32);
            this.Tenants.Name = "Tenants";
            this.Tenants.Size = new System.Drawing.Size(128, 95);
            this.Tenants.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tenants";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(146, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 47);
            this.button1.TabIndex = 2;
            this.button1.Text = "Get Campaign Stats";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CampaignProperties
            // 
            this.CampaignProperties.Location = new System.Drawing.Point(241, 32);
            this.CampaignProperties.Multiline = true;
            this.CampaignProperties.Name = "CampaignProperties";
            this.CampaignProperties.Size = new System.Drawing.Size(297, 96);
            this.CampaignProperties.TabIndex = 3;
            // 
            // CampaignID
            // 
            this.CampaignID.Location = new System.Drawing.Point(15, 166);
            this.CampaignID.Name = "CampaignID";
            this.CampaignID.Size = new System.Drawing.Size(100, 20);
            this.CampaignID.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 146);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Enter Campaign ID";
            // 
            // GetProperties
            // 
            this.GetProperties.Location = new System.Drawing.Point(15, 198);
            this.GetProperties.Name = "GetProperties";
            this.GetProperties.Size = new System.Drawing.Size(100, 23);
            this.GetProperties.TabIndex = 7;
            this.GetProperties.Text = "Get Properties";
            this.GetProperties.UseVisualStyleBackColor = true;
            this.GetProperties.Click += new System.EventHandler(this.GetProperties_Click);
            // 
            // SetProperties
            // 
            this.SetProperties.Location = new System.Drawing.Point(15, 227);
            this.SetProperties.Name = "SetProperties";
            this.SetProperties.Size = new System.Drawing.Size(100, 23);
            this.SetProperties.TabIndex = 8;
            this.SetProperties.Text = "Set Properties";
            this.SetProperties.UseVisualStyleBackColor = true;
            this.SetProperties.Click += new System.EventHandler(this.SetProperties_Click);
            // 
            // PropertyData
            // 
            this.PropertyData.Location = new System.Drawing.Point(133, 146);
            this.PropertyData.Multiline = true;
            this.PropertyData.Name = "PropertyData";
            this.PropertyData.Size = new System.Drawing.Size(405, 241);
            this.PropertyData.TabIndex = 10;
            // 
            // StartCampaign
            // 
            this.StartCampaign.Location = new System.Drawing.Point(15, 256);
            this.StartCampaign.Name = "StartCampaign";
            this.StartCampaign.Size = new System.Drawing.Size(100, 23);
            this.StartCampaign.TabIndex = 11;
            this.StartCampaign.Text = "Start Campaign";
            this.StartCampaign.UseVisualStyleBackColor = true;
            this.StartCampaign.Click += new System.EventHandler(this.StartCampaign_Click);
            // 
            // StopCampaign
            // 
            this.StopCampaign.Location = new System.Drawing.Point(15, 285);
            this.StopCampaign.Name = "StopCampaign";
            this.StopCampaign.Size = new System.Drawing.Size(100, 23);
            this.StopCampaign.TabIndex = 12;
            this.StopCampaign.Text = "Stop Campaign";
            this.StopCampaign.UseVisualStyleBackColor = true;
            this.StopCampaign.Click += new System.EventHandler(this.StopCampaign_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(549, 399);
            this.Controls.Add(this.StopCampaign);
            this.Controls.Add(this.StartCampaign);
            this.Controls.Add(this.PropertyData);
            this.Controls.Add(this.SetProperties);
            this.Controls.Add(this.GetProperties);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.CampaignID);
            this.Controls.Add(this.CampaignProperties);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Tenants);
            this.Name = "Form1";
            this.Text = "SDMP 2.0 Demonstration using RouterNet";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox Tenants;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox CampaignProperties;
        private System.Windows.Forms.TextBox CampaignID;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button GetProperties;
        private System.Windows.Forms.Button SetProperties;
        private System.Windows.Forms.TextBox PropertyData;
        private System.Windows.Forms.Button StartCampaign;
        private System.Windows.Forms.Button StopCampaign;
    }
}

