using LinqLabs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyHomeWork
{
    public partial class Frm作業_1 : Form
    {
        private static int _currentPage = 0;
        private static int _pageSize = 0;
        public Frm作業_1()
        {
            InitializeComponent();
            //NOTE Distinct()
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            this.ordersTableAdapter1.Fill(this.nwDataSet1.Orders);
            this.order_DetailsTableAdapter1.Fill(this.nwDataSet1.Order_Details);
            this.productsTableAdapter1.Fill(this.nwDataSet1.Products);

            var pyear = from o in this.nwDataSet1.Orders
                     select o.OrderDate.Year;
            this.comboBox1.DataSource = pyear.Distinct().ToList();
        }

        private void bindingSource1_CurrentChanged(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    where f.Extension == ".log"
                    select f;

            //files[0].CreationTime
            this.dataGridView1.DataSource = q.ToList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var p = from c in files
                    where c.CreationTime.Year == 2024
                    select c;
            this.dataGridView1.DataSource = p.ToList();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var p = from l in files
                    where l.Length > 100000
                    select l;

            this.dataGridView1.DataSource = p.ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.dataGridView1.DataSource = nwDataSet1.Orders.ToList();
            //this.dataGridView2.DataSource = this.nwDataSet1.Order_Details;

            var od = from d in nwDataSet1.Order_Details
                     join p in nwDataSet1.Products
                     on d.ProductID equals p.ProductID
                     select d;

            this.dataGridView2.DataSource = od.ToList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            var d = from p in nwDataSet1.Orders
                    where p.OrderDate.Year.ToString() == comboBox1.Text
                    select p;

            this.dataGridView1.DataSource= d.ToList();

            var od = from op in nwDataSet1.Order_Details
                     join p in nwDataSet1.Orders
                     on op.OrderID equals p.OrderID
                     where p.OrderDate.Year.ToString() == comboBox1.Text
                     select op;

            this.dataGridView2.DataSource= od.ToList();

        }

        private void Frm作業_1_Load(object sender, EventArgs e)
        {
            //for (int i = 1990; i <= 2024; i++)
            //{
            //    comboBox1.Items.Add(i.ToString());
            //}

            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            this.dataGridView2.DataSource = this.nwDataSet1.Products.ToList();
            lblDetails.Text = "產品列表";
        }
        private void button12_Click(object sender, EventArgs e)
        {
            int _pageSize = Convert.ToInt32(textBox1.Text);

            if (_currentPage > 0)
            {
                // 減少頁數，回到上一頁
                _currentPage--;

                // 計算需要跳過的記錄數
                int skip = (_currentPage) * _pageSize;

                // 取得資料，並跳過已顯示的記錄
                var products = this.nwDataSet1.Products.Skip(skip).Take(_pageSize);

                // 將結果綁定到 DataGridView
                this.dataGridView2.DataSource = products.ToList();
            }

        }
        private void button13_Click(object sender, EventArgs e)
        {
        
            int _pageSize = Convert.ToInt32(textBox1.Text);
            int skip = (_currentPage) * _pageSize;

            // 取得資料，並跳過已顯示的記錄
            var products = this.nwDataSet1.Products.Skip(skip).Take(_pageSize);

            // 將結果綁定到 DataGridView
            this.dataGridView2.DataSource = products.ToList();

            // 增加頁數，準備下一頁
            _currentPage++;


            //Distinct()
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int selectId = (int)dataGridView1.CurrentRow.Cells["OrderID"].Value;

            var selectedOrder = from n in nwDataSet1.Order_Details
                                where n.OrderID == selectId
                                select n;

            dataGridView2.DataSource = selectedOrder.ToList();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            var eachYOrder = from n in nwDataSet1.Orders
                             group n by n.OrderDate.Year into y
                             select new { Year = y.Key, Amount = y.Count() };
            dataGridView2.DataSource = eachYOrder.ToList();
        }
    }        
}
