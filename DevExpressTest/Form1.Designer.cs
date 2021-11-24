
namespace DevExpressTest
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.imageCollection1 = new DevExpress.Utils.ImageCollection(this.components);
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.imageComboBoxEdit = new DevExpress.XtraEditors.ImageComboBoxEdit();
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // imageCollection1
            // 
            this.imageCollection1.ImageStream = ((DevExpress.Utils.ImageCollectionStreamer)(resources.GetObject("imageCollection1.ImageStream")));
            this.imageCollection1.Images.SetKeyName(0, "female.png");
            this.imageCollection1.Images.SetKeyName(1, "male.png");
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(64, 105);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(39, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Gender:";
            // 
            // imageComboBoxEdit
            // 
            this.imageComboBoxEdit.Location = new System.Drawing.Point(133, 102);
            this.imageComboBoxEdit.Name = "imageComboBoxEdit";
            this.imageComboBoxEdit.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.imageComboBoxEdit.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Male", ((short)(1)), 1),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("Female", ((short)(0)), 0)});
            this.imageComboBoxEdit.Properties.SmallImages = this.imageCollection1;
            this.imageComboBoxEdit.Size = new System.Drawing.Size(190, 20);
            this.imageComboBoxEdit.TabIndex = 1;
            this.imageComboBoxEdit.SelectedIndexChanged += new System.EventHandler(this.imageComboBoxEdit_SelectedIndexChanged);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 257);
            this.Controls.Add(this.imageComboBoxEdit);
            this.Controls.Add(this.labelControl1);
            this.Name = "Form1";
            this.Text = "Image comboBox Edit";
            ((System.ComponentModel.ISupportInitialize)(this.imageCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.imageComboBoxEdit.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.Utils.ImageCollection imageCollection1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.ImageComboBoxEdit imageComboBoxEdit;
    }
}