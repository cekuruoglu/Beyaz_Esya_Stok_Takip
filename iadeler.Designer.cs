
namespace beyaz_esya_stok_takip
{
    partial class iadeler
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtTelefon = new System.Windows.Forms.MaskedTextBox();
            this.BarkodNotxt = new System.Windows.Forms.MaskedTextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.txtAdSoyad = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.UrunAdiTXT = new System.Windows.Forms.TextBox();
            this.txtTc = new System.Windows.Forms.MaskedTextBox();
            this.OdemeCB = new System.Windows.Forms.ComboBox();
            this.label17 = new System.Windows.Forms.Label();
            this.MarkaCB = new System.Windows.Forms.ComboBox();
            this.KategoriCB = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtTelefon);
            this.groupBox2.Controls.Add(this.BarkodNotxt);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.txtAdSoyad);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.label12);
            this.groupBox2.Controls.Add(this.label13);
            this.groupBox2.Controls.Add(this.label15);
            this.groupBox2.Controls.Add(this.label14);
            this.groupBox2.Controls.Add(this.label16);
            this.groupBox2.Controls.Add(this.UrunAdiTXT);
            this.groupBox2.Controls.Add(this.txtTc);
            this.groupBox2.Controls.Add(this.OdemeCB);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Controls.Add(this.MarkaCB);
            this.groupBox2.Controls.Add(this.KategoriCB);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.groupBox2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(84)))), ((int)(((byte)(100)))));
            this.groupBox2.Location = new System.Drawing.Point(257, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1388, 154);
            this.groupBox2.TabIndex = 14;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtrele";
            // 
            // comboBox1
            // 
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.comboBox1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(84)))), ((int)(((byte)(100)))));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "Arızalı veya Hasarlı Ürün",
            "Beklenen Performansı Karşılamama",
            "Estetik Sorunlar",
            "Uyumsuzluk",
            "Yanlış Bilgilendirme veya Beklentilerin Aşılması"});
            this.comboBox1.Location = new System.Drawing.Point(595, 113);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(331, 24);
            this.comboBox1.TabIndex = 22;
            this.comboBox1.TextChanged += new System.EventHandler(this.comboBox1_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(84)))), ((int)(((byte)(100)))));
            this.label6.Location = new System.Drawing.Point(462, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(113, 20);
            this.label6.TabIndex = 21;
            this.label6.Text = "İade Sebebi:";
            // 
            // txtTelefon
            // 
            this.txtTelefon.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTelefon.Location = new System.Drawing.Point(1166, 61);
            this.txtTelefon.Mask = "(999) 000-00-00";
            this.txtTelefon.Name = "txtTelefon";
            this.txtTelefon.Size = new System.Drawing.Size(195, 27);
            this.txtTelefon.TabIndex = 20;
            this.txtTelefon.TextChanged += new System.EventHandler(this.txtTelefon_TextChanged);
            // 
            // BarkodNotxt
            // 
            this.BarkodNotxt.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BarkodNotxt.Location = new System.Drawing.Point(486, 64);
            this.BarkodNotxt.Mask = "00000000000";
            this.BarkodNotxt.Name = "BarkodNotxt";
            this.BarkodNotxt.PromptChar = ' ';
            this.BarkodNotxt.Size = new System.Drawing.Size(177, 27);
            this.BarkodNotxt.TabIndex = 9;
            this.BarkodNotxt.ValidatingType = typeof(int);
            this.BarkodNotxt.TextChanged += new System.EventHandler(this.BarkodNotxt_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.label11.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(84)))), ((int)(((byte)(100)))));
            this.label11.Location = new System.Drawing.Point(707, 68);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 20);
            this.label11.TabIndex = 8;
            this.label11.Text = "Ürün Adı:";
            // 
            // txtAdSoyad
            // 
            this.txtAdSoyad.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtAdSoyad.Location = new System.Drawing.Point(1166, 26);
            this.txtAdSoyad.Name = "txtAdSoyad";
            this.txtAdSoyad.Size = new System.Drawing.Size(194, 27);
            this.txtAdSoyad.TabIndex = 19;
            this.txtAdSoyad.TextChanged += new System.EventHandler(this.txtTelefon_TextChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.label10.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(84)))), ((int)(((byte)(100)))));
            this.label10.Location = new System.Drawing.Point(1043, 68);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(77, 20);
            this.label10.TabIndex = 17;
            this.label10.Text = "Telefon:";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(84)))), ((int)(((byte)(100)))));
            this.label12.Location = new System.Drawing.Point(368, 69);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(103, 20);
            this.label12.TabIndex = 8;
            this.label12.Text = "Barkod No:";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.label13.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(84)))), ((int)(((byte)(100)))));
            this.label13.Location = new System.Drawing.Point(13, 70);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(118, 20);
            this.label13.TabIndex = 8;
            this.label13.Text = "Ödeme Türü:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.label15.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(84)))), ((int)(((byte)(100)))));
            this.label15.Location = new System.Drawing.Point(368, 31);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(66, 20);
            this.label15.TabIndex = 8;
            this.label15.Text = "Marka:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.label14.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(84)))), ((int)(((byte)(100)))));
            this.label14.Location = new System.Drawing.Point(1043, 28);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(94, 20);
            this.label14.TabIndex = 18;
            this.label14.Text = "Ad Soyad:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.label16.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(84)))), ((int)(((byte)(100)))));
            this.label16.Location = new System.Drawing.Point(13, 31);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(85, 20);
            this.label16.TabIndex = 8;
            this.label16.Text = "Kategori:";
            // 
            // UrunAdiTXT
            // 
            this.UrunAdiTXT.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.UrunAdiTXT.Location = new System.Drawing.Point(812, 64);
            this.UrunAdiTXT.Name = "UrunAdiTXT";
            this.UrunAdiTXT.Size = new System.Drawing.Size(187, 27);
            this.UrunAdiTXT.TabIndex = 7;
            this.UrunAdiTXT.TextChanged += new System.EventHandler(this.UrunAdiTXT_TextChanged);
            // 
            // txtTc
            // 
            this.txtTc.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txtTc.Location = new System.Drawing.Point(812, 24);
            this.txtTc.Mask = "00000000000";
            this.txtTc.Name = "txtTc";
            this.txtTc.PromptChar = ' ';
            this.txtTc.Size = new System.Drawing.Size(187, 27);
            this.txtTc.TabIndex = 16;
            this.txtTc.ValidatingType = typeof(int);
            this.txtTc.TextChanged += new System.EventHandler(this.txtTc_TextChanged);
            // 
            // OdemeCB
            // 
            this.OdemeCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.OdemeCB.FormattingEnabled = true;
            this.OdemeCB.Items.AddRange(new object[] {
            "Nakit",
            "Kredi Kartı"});
            this.OdemeCB.Location = new System.Drawing.Point(159, 65);
            this.OdemeCB.Name = "OdemeCB";
            this.OdemeCB.Size = new System.Drawing.Size(156, 28);
            this.OdemeCB.TabIndex = 6;
            this.OdemeCB.TextChanged += new System.EventHandler(this.OdemeCB_TextChanged);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Bold);
            this.label17.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(73)))), ((int)(((byte)(84)))), ((int)(((byte)(100)))));
            this.label17.Location = new System.Drawing.Point(707, 29);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(39, 20);
            this.label17.TabIndex = 15;
            this.label17.Text = "TC:";
            // 
            // MarkaCB
            // 
            this.MarkaCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.MarkaCB.FormattingEnabled = true;
            this.MarkaCB.Location = new System.Drawing.Point(486, 28);
            this.MarkaCB.Name = "MarkaCB";
            this.MarkaCB.Size = new System.Drawing.Size(177, 28);
            this.MarkaCB.TabIndex = 6;
            this.MarkaCB.TextChanged += new System.EventHandler(this.MarkaCB_TextChanged);
            // 
            // KategoriCB
            // 
            this.KategoriCB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F);
            this.KategoriCB.FormattingEnabled = true;
            this.KategoriCB.Location = new System.Drawing.Point(159, 31);
            this.KategoriCB.Name = "KategoriCB";
            this.KategoriCB.Size = new System.Drawing.Size(156, 28);
            this.KategoriCB.TabIndex = 6;
            this.KategoriCB.SelectedIndexChanged += new System.EventHandler(this.KategoriCB_SelectedIndexChanged);
            this.KategoriCB.TextChanged += new System.EventHandler(this.KategoriCB_TextChanged);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(191)))), ((int)(((byte)(202)))));
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 172);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 150;
            this.dataGridView1.RowTemplate.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.Size = new System.Drawing.Size(1905, 799);
            this.dataGridView1.TabIndex = 13;
            // 
            // iadeler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(187)))), ((int)(((byte)(191)))), ((int)(((byte)(202)))));
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.dataGridView1);
            this.Name = "iadeler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "İadeler";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FaturaRapor_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.MaskedTextBox txtTelefon;
        private System.Windows.Forms.MaskedTextBox BarkodNotxt;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox txtAdSoyad;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox UrunAdiTXT;
        private System.Windows.Forms.MaskedTextBox txtTc;
        private System.Windows.Forms.ComboBox OdemeCB;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.ComboBox MarkaCB;
        private System.Windows.Forms.ComboBox KategoriCB;
        public System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label6;
    }
}