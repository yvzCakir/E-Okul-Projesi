using System;
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
    public partial class FrmOgrenci : Form
    {
        public FrmOgrenci()
        {
            InitializeComponent();
        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-P9F8OIM\SQLEXPRESS;Initial Catalog=BonusOkul;Integrated Security=True");

        DataSet1TableAdapters.DataTable1TableAdapter ds = new DataSet1TableAdapters.DataTable1TableAdapter();
        private void FrmOgrenci_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * From TBLKULUPLER", baglanti);
            SqlDataAdapter da = new SqlDataAdapter(komut);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CmbKulüp.DisplayMember = "KULUPAD";
            CmbKulüp.ValueMember = "KULUPID";
            CmbKulüp.DataSource = dt;
            baglanti.Close();
        }
        string c = " ";
        private void BtnEkle_Click(object sender, EventArgs e)
        {
            ds.OgrenciEkle(TxtAd.Text, TxtSoyad.Text, byte.Parse(CmbKulüp.SelectedValue.ToString()), c);
            MessageBox.Show("Öğrenci Ekleme Yapıldı");

        }

        private void BtnListele_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciListesi();
        }

        private void CmbKulüp_SelectedIndexChanged(object sender, EventArgs e)
        {
            // TxtID.Text = CmbKulüp.SelectedValue.ToString();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            ds.OgrenciSil(int.Parse(TxtID.Text));
            MessageBox.Show("Silme İşlemi Gerçekleştirildi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            TxtID.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            TxtAd.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            TxtSoyad.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            CmbKulüp.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
            c = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
            if (c == "Kız")
            {
                radioButton1.Checked = true;

            }
            else
            {
                radioButton2.Checked = true;


            }


        }

        private void BtnAra_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ds.OgrenciGetir(TxtAra.Text);    
        }
    }
}


      /* private void BtnGüncelle_Click(object sender, EventArgs e)
        {
            ds.OgrenciGuncelle(TxtAd.Text, TxtSoyad.Text, byte.Parse(CmbKulüp.SelectedValue.ToString()), c, int.Parse(TxtID.Text));

        }
   


 private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

         if (radioButton1.Checked == true)
           {
              c = " Kız";
            }
      }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton2.Checked == true)
            {
                c = " Erkek";
            }
        }
    }
}
*/