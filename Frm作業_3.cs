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
    public partial class Frm作業_3 : Form
    {
        private List<Student> students_scores;

        public Frm作業_3()
        {
            InitializeComponent();
            students_scores = new List<Student>()
                                         {
                                            new Student{ Name = "aaa", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Male" },
                                            new Student{ Name = "bbb", Class = "CS_102", Chi = 80, Eng = 80, Math = 100, Gender = "Male" },
                                            new Student{ Name = "ccc", Class = "CS_101", Chi = 60, Eng = 50, Math = 75, Gender = "Female" },
                                            new Student{ Name = "ddd", Class = "CS_102", Chi = 80, Eng = 70, Math = 85, Gender = "Female" },
                                            new Student{ Name = "eee", Class = "CS_101", Chi = 80, Eng = 80, Math = 50, Gender = "Female" },
                                            new Student{ Name = "fff", Class = "CS_102", Chi = 80, Eng = 80, Math = 80, Gender = "Female" },
                                          };

        }
        private void button36_Click(object sender, EventArgs e)
        {
           
            #region 搜尋 班級學生成績

            // 共幾個 學員成績 ?
            textBox1.Text = students_scores.Count().ToString();

            // 找出 前面三個 的學員所有科目成績	
            var topThree = students_scores.Take(3).Select(p=> new { Name = p.Name, Chinese = p.Chi, English = p.Eng, Math = p.Math });
            this.dataGridView1.DataSource = topThree.ToList();

            // 找出 後面兩個 的學員所有科目成績					
            var lastTwo = students_scores.Skip(students_scores.Count-2).Select(p => new { Name = p.Name, Chinese = p.Chi, English = p.Eng, Math = p.Math });
            this.dataGridView2.DataSource = lastTwo.ToList();

            // 找出 Name 'aaa','bbb','ccc' 的學員國文英文科目成績						
            var stu = from p in students_scores
                      where p.Name == "aaa" || p.Name == "bbb" || p.Name == "ccc"
                      select (new { p.Name, Chinese = p.Chi, English = p.Eng, p.Math });

            this.dataGridView3.DataSource = stu.ToList();
            // 找出學員 'bbb' 的成績	                          
            var stb = students_scores.Where(p => p.Name == "bbb").Select(p => new { p.Name, Chinese = p.Chi, English = p.Eng, p.Math });
            this.dataGridView4.DataSource = stb.ToList();
            // 找出除了 'bbb' 學員的學員的所有成績 ('bbb' 退學)	
            var stelse = students_scores.Where(p => p.Name != "bbb").Select(p => new { p.Name, Chinese = p.Chi, English = p.Eng, p.Math });
            this.dataGridView5.DataSource = stelse.ToList();
            // 找出 'aaa', 'bbb' 'ccc' 學員 國文數學兩科 科目成績
            var studntScore = students_scores.Where(p => p.Name == "aaa" || p.Name == "bbb" || p.Name == "ccc").Select(p => new { p.Name, Chinese = p.Chi, p.Math });
            this.dataGridView6.DataSource = studntScore.ToList();
            // 數學不及格 ... 是誰
            var mathFail = students_scores.Where(p => p.Math < 60).Select(p => p.Name);
            foreach (var student in mathFail)
            {
                textBox2.Text = student.ToString();
            }
            #endregion
        }

        private void button37_Click(object sender, EventArgs e)
        {
            //個人 sum, min, max, avg
            // 統計 每個學生個人成績 並排序

            var student = from s in students_scores
                          select new {
                              s.Name,
                              Sum = s.Chi + s.Math + s.Eng,
                              Min = Math.Min(Math.Min(s.Chi, s.Math), s.Eng),
                              Max = Math.Max(Math.Max(s.Chi, s.Math), s.Eng),
                              Avg = (s.Chi + s.Math + s.Eng) / 3
                          };

            var orderStudent = student.OrderByDescending(p => p.Sum);
            dataGridView7.DataSource = orderStudent.ToList();
            
        }

        private void button33_Click(object sender, EventArgs e)
        {
            // split=> 分成 三群 '待加強'(60~69) '佳'(70~89) '優良'(90~100)
            var studentAvg = from s in students_scores
                             select new
                             { s.Name, Avg = (s.Chi + s.Math + s.Eng) / 3 };

            var splitScore = from s in studentAvg
                             group s by MyAvg(s.Avg) into g
                             select new { Name };

            dataGridView8.DataSource = splitScore.ToList();
            // print 每一群是哪幾個 ? (每一群 sort by 分數 descending)
        }

        private string MyAvg(int s)
        {
            if (s < 70)
            {
                return "待加強";
            }
            else if (s < 90)
            {
                return "佳";
            }
            else
            {
                return "優良";
            }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();
            var q = from f in files
                    group f by MyLength(f.Length) into g
                    select new
                    {
                        MyKey = g.Key,
                        MyGroup = g.Select(f => new
                        {
                            FileName = f.Name,
                            FileSize = f.Length,
                            Group = g.Key
                        }).OrderByDescending(f => f.FileSize)
                    };

            //this.dataGridView9.DataSource = q.ToList();
            List<dynamic> groupedFiles = new List<dynamic>();
            foreach (var group in q)
            {
                foreach (var item in group.MyGroup)
                {
                    groupedFiles.Add(item);
                }
            }

            dataGridView9.DataSource = groupedFiles.ToList();
        }

        private object MyLength(long n)
        {
            if (n > 50000)
            {
                return "大檔案";
            }
            else if (n > 10000 && n < 50000)
            {
                return "中檔案";
            }
            else return "小檔案";
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(@"c:\windows");
            System.IO.FileInfo[] files = dir.GetFiles();

            var q = from f in files
                    group f by f.CreationTime.Year into g
                    select new
                    {
                        MyKey = g.Key,
                        MyGroup = g.Select(f => new
                        {
                            FileName = f.Name,
                            FileYear = f.CreationTime.Year,
                            Group = g.Key
                        }).OrderByDescending(f => f.FileYear)
                    };
            List<dynamic> groupedFiles = new List<dynamic>();
            foreach (var group in q)
            {
                foreach (var item in group.MyGroup)
                {
                    groupedFiles.Add(item);
                }
            }

            dataGridView103*******************.DataSource = groupedFiles.ToList();
        }
    }
    public class Student
    {
        public string Name { get; set; }
        public string Class { get; set; }
        public int Chi { get; set; }
        public int Eng { get; set; }
        public int Math { get; set; }
        public string Gender { get; set; }
    }
}
