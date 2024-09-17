using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace MyHomeWork
{
    public partial class Frm作業_2 : Form
    {
        public Frm作業_2()
        {
            InitializeComponent();
            this.productPhotoTableAdapter1.Fill(this.adwDataSet1.ProductPhoto);
            var pyear = from o in this.adwDataSet1.ProductPhoto
                        select o.ModifiedDate.Year;
            this.comboBox3.DataSource = pyear.Distinct().ToList();
        }
        private void Frm作業_2_Load(object sender, EventArgs e)
        {
            //for (int i = 1990; i <= 2024; i++)
            //{
            //    comboBox3.Items.Add(i.ToString());
            //}
        }

        private void button11_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = this.adwDataSet1.ProductPhoto;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 確認是否選擇了某一行
            if (dataGridView1.CurrentRow != null)
            {
                // 取得選擇行的 PhotoID (或其他唯一標識符)
                int selectedPhotoID = (int)dataGridView1.CurrentRow.Cells["ProductPhotoID"].Value;

                // 使用 LINQ 查詢該行對應的圖片
                var selectedPhoto = (from lp in adwDataSet1.ProductPhoto
                                     where lp.ProductPhotoID == selectedPhotoID
                                     select lp.LargePhoto).FirstOrDefault();

                if (selectedPhoto != null && selectedPhoto.Length > 0)
                {
                    // 將圖片資料轉換成 MemoryStream，並顯示於 PictureBox
                    using (var ms = new System.IO.MemoryStream(selectedPhoto))
                    {
                        pictureBox1.Image = Image.FromStream(ms);
                    }
                }
                else
                {
                    // 若無圖片則清空 PictureBox
                    pictureBox1.Image = null;
                }
            }
        }

        

        private void button3_Click(object sender, EventArgs e)
        {

            var whichDate = from p in this.adwDataSet1.ProductPhoto
                            where p.ModifiedDate > dateTimePicker1.Value && p.ModifiedDate < dateTimePicker2.Value
                            select p;
            dataGridView1.DataSource = whichDate.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            var whichYear = from p in this.adwDataSet1.ProductPhoto
                            where p.ModifiedDate.Year.ToString() == comboBox3.Text
                            select p;

            dataGridView1 .DataSource = whichYear.ToList();
        }

        private void button10_Click(object sender, EventArgs e)
        {

            var whichSeason = from o in this.adwDataSet1.ProductPhoto
                              where MySeason(o.ModifiedDate.Month) == comboBox2.Text
                              select o;

            dataGridView1.DataSource = whichSeason.ToList();
            
        }

        private string MySeason(int n)
        {
            if (n == 1 || n == 2 || n == 3)
            {
                return "第一季";
            }
            else if (n == 3 || n == 4 || n == 5)
            {
                return "第二季";
            }
            else if (n == 6 || n == 7 || n == 8)
            {
                return "第三季";
            }
            else return "第四季";
        }
    }
}
