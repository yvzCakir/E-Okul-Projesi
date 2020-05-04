﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace BonusProje1
{
    public partial class FrmSınavNotlar : Form
    {
        public FrmSınavNotlar()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-P9F8OIM\SQLEXPRESS;Initial Catalog=BonusOkul;Integrated Security=True");
        DataSet1TableAdapters.TBLNOTLARTableAdapter ds = new DataSet1TableAdapters.TBLNOTLARTableAdapter();

        private void BtnAra_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.NotListesi(int.Parse(TxtID.Text));
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrmSınavNotlar_Load(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLDERSLER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            comboBox1.DisplayMember="DERSAD";
            comboBox1.ValueMember = "DERSID";
            comboBox1.DataSource = dt;
            baglanti.Close();

        }
        int notid;
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            notid =int.Parse (dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            TxtSinav1.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            TxtSinav2.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            TxtSinav3.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
            TxtProje.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
            TxtOrtalama.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
            TxtDurum.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();


        }
        int sinav1, sinav2, sinav3, proje;

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            FrmOgretmen fr = new FrmOgretmen();
            fr.Show();
            this.Hide();

        }

        

        double ortalama;
        private void BtnHesapla_Click(object sender, EventArgs e)
        {
           
           // string durum;
            sinav1 = Convert.ToInt16(TxtSinav1.Text);
            sinav2 = Convert.ToInt16(TxtSinav2.Text);
            sinav3 = Convert.ToInt16(TxtSinav3.Text);
            proje = Convert.ToInt16(TxtProje.Text);
            ortalama = (sinav1 + sinav2 + sinav3 + proje) / 4;
            TxtOrtalama.Text = ortalama.ToString();
            if (ortalama >= 50) 
            {
                TxtDurum.Text = "True";
            }
            else
            {
                TxtDurum.Text = "False";
            }
        }

        private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            ds.NotGuncelle(byte.Parse(comboBox1.SelectedValue.ToString()), int.Parse(TxtID.Text), byte.Parse(TxtSinav1.Text), byte.Parse(TxtSinav2.Text), byte.Parse(TxtSinav3.Text), byte.Parse(TxtProje.Text), decimal.Parse(TxtOrtalama.Text), bool.Parse(TxtDurum.Text),notid);
        }
    }
}
