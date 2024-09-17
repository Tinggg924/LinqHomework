using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace Starter
{

    //Notes: LINQ 主要參考 
    //組件 System.Core.dll,
    //namespace {}  System.Linq
    //public static class Enumerable
   
    //public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate);

    //1. 泛型 (泛用方法)                                          (ex.  void SwapAnyType<T>(ref T a, ref T b)
    //2. 委派參數 Lambda Expression (匿名方法簡潔版)               (ex.  MyWhere(nums, n => n %2==0);
    //3. Iterator                                                (ex.  MyIterator)
    //4. 擴充方法          
    public partial class FrmLangForLINQ : Form
    {
        public FrmLangForLINQ()
        {
            InitializeComponent();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //int[] nums = { 1, 2, 3 };
            //nums.Max()

            int n1, n2;
            n1 = 100;
            n2 = 200;

            MessageBox.Show(n1 + "," + n2);

          
            Swap(ref n1, ref n2);  //FrmLangForLINQ.Swap(ref n1, ref n2);
            //MessageBox.Show(SystemInformation.ComputerName);
            //Application.StartupPath

            MessageBox.Show(n1 + "," + n2);

            //==============================

            string s1 = "aaa", s2 = "bbb";
            MessageBox.Show(s1 + "," + s2);

            Swap(ref s1, ref s2);
            MessageBox.Show(s1 + "," + s2);

        }

        private static void Swap(ref int n1, ref int n2)
        {
            int temp = n2;
            n2 = n1;
            n1 = temp;
        }

        private static void Swap(ref string n1, ref string n2)
        {
            string temp = n2;
            n2 = n1;
            n1 = temp;
        }

        //..........
        private static void Swapxxx(ref object n1, ref object n2)
        {
            object temp = n2;
            n2 = n1;
            n1 = temp;
        }

        private static void SwapAnyType<T>(ref T n1, ref T n2)
        {
            T temp = n2;
            n2 = n1;
            n1 = temp;
        }
        private void button7_Click(object sender, EventArgs e)
        {
            int n1, n2;
            n1 = 100;
            n2 = 200;
            MessageBox.Show(n1 + "," + n2);

            SwapAnyType<int>(ref n1, ref n2);
            MessageBox.Show(n1 + "," + n2);
            //==============================
            string s1 = "aaa", s2 = "bbb";
            MessageBox.Show(s1 + "," + s2);

            // SwapAnyType<string>(ref s1, ref s2);
            SwapAnyType(ref s1, ref s2); //推斷型別
            MessageBox.Show(s1 + "," + s2);

        }

        private void button2_Click(object sender, EventArgs e)
        {
            //具名方法
            //this.buttonX.Click += new EventHandler( ButtonX_Click);
            this.buttonX.Click += ButtonX_Click;

            // NOTE:           嚴重性 程式碼 說明 專案  檔案 行   隱藏項目狀態
            //錯誤(作用中)    CS0123  'aaa' 沒有任何多載符合委派 'EventHandler' LinqLabs C:\Shared\LINQ\LinqLabs(Solution)\LinqLabs\2.FrmLangForLINQ.cs    102

            this.buttonX.Click += aaa;

            //================================
            //C# 2.0 匿名方法
            this.buttonX.Click += delegate (object sender1, EventArgs e1)
                                          {
                                              MessageBox.Show("匿名方法");
                                          };

            //===============================
            //匿名方法 C# 3.0 lambda => goes to
            this.buttonX.Click += (object sender1, EventArgs e1) =>
                                    {
                                        MessageBox.Show("匿名方法 簡潔版");
                                    };

        }



        private void ButtonX_Click(object sender, EventArgs e)
        {
            MessageBox.Show("ButtonX click");
        }

        private void aaa(object sender, EventArgs e)
        {
            MessageBox.Show("aaa");
        }

         bool Test (int n)
        {
            return n > 5;
        }

        bool IsEven(int n)
        {
            return n %2==0;
        }
        //Step 1: create delegate class
        //Step 2: create delegate object
        //Step 3: Invoke method / call method

        delegate bool MyDelegate(int n);

        private void button9_Click(object sender, EventArgs e)
        {
            bool result;
            result = Test(3);

          
            //============================
            MyDelegate delegateObj  =  new MyDelegate(Test);
            result =  delegateObj(7);

            //===========================

            delegateObj = IsEven;

            result = delegateObj(7);


            MessageBox.Show("result = " + result);

        }
    }
}
