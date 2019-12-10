using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace Semestralni_rad___num
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        int n;
        decimal e = 2.7182818284590452353602874m;
        //Dodao da ima delta razlika izmedju pravog resenja i dobijenog
        //Dodao da kada n>1000, da se iskljuci opcija za singleIter
        private void Button1_Click(object sender, EventArgs er)
        {
            n = (int)numericUpDown1.Value;

            calc1();
            lMethod1.Text = Convert.ToString(method1.Items[method1.Items.Count - 1]).Split(' ')[1];//STAVIO DA SE SVE SPLITUJE PO RAZMAKU
            lMethod1Delta.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(lMethod1.Text) - e));
            calc2();
            lMethod2.Text = Convert.ToString(method2.Items[method2.Items.Count-1]).Split(' ')[1];
            lMethod2Delta.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(lMethod2.Text) - e));
            calc3();
            lMethod3.Text = Convert.ToString(method3.Items[method3.Items.Count - 1]).Split(' ')[1];
            lMethod3Delta.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(lMethod3.Text) - e));
            calc4();
            lMethod4.Text = Convert.ToString(method4.Items[method4.Items.Count - 1]).Split(' ')[1];
            lMethod4Delta.Text = Convert.ToString(Math.Abs(Convert.ToDecimal(lMethod4.Text) - e));


        }


        private void calc1()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
           
            method1.Items.Clear();
            decimal mul;
            for (int j = 0; j < n; j++)
            {
                if (cbSingleIter.Checked)
                    j = n - 1;
                mul = 1;
                for (int i = 1; i <= j; i++)
                {
                    mul *= (1 + (decimal)1 / j);
                }
                method1.Items.Add((j+1).ToString() + ".) " + mul);
            }
            watch.Stop();
            ltime1.Text = watch.ElapsedMilliseconds + " ms";

        }

        private void calc2()
        {
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();

            method2.Items.Clear();
            decimal sum = 0;
            long fakt = 1;
            int i;
            for(i=1; i<=n; i++)
            {
                try
                {
                    sum = sum + (decimal)1 / fakt;
                }catch(Exception e)
                {
                    break;
                }
                fakt *= i;
                if(cbSingleIter.Checked == false)
                    method2.Items.Add(i.ToString() + ".) " + sum);
            }
            if(cbSingleIter.Checked)
                method2.Items.Add(i.ToString() + ".) " + sum);
            watch.Stop();
            ltime2.Text = watch.ElapsedMilliseconds + " ms";

        }
        List<int> startValues3;
        System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();

        private void calc3()
        {
            watch.Start();

            method3.Items.Clear();
            startValues3 = new List<int>();
            startValues3.Add(2);
            startValues3.Add(1);
            int even = 2;
            for (int i = 0; i < n / 3 + 1; i++)
            {
                startValues3.Add(2);
                startValues3.Add(1);
                startValues3.Add(1);
                even += 2;
                startValues3.Add(even);
            }
            for(int i=0; i<n; i++)
            {
                if (cbSingleIter.Checked)
                    i = n - 1;
                calc3Single(i);
            }

        }

        private void calc3Single(int maxLen)
        {

            decimal div = 0;
            for(int i=maxLen; i>0; i--)
            {
                int j;
                j = startValues3[i];
                div = (decimal)1 / (div + j);
            }
            method3.Items.Add((maxLen+1).ToString() + ".) " + (div+2));
            watch.Stop();
            ltime3.Text = watch.ElapsedMilliseconds + " ms";

        }


        List<int> startValues4;

        private void calc4()
        {
            watch.Start();
            method4.Items.Clear();
            startValues4 = new List<int>();
            startValues4.Add(1);
            startValues4.Add(1);
            int even = 6;
            for (int i = 0; i < n ; i++)
            {
                startValues4.Add(even);
                even += 4;

            }
            for (int i = 0; i < n; i++)
            {
                if (cbSingleIter.Checked)
                    i = n - 1;
                calc3Single4(i);
            }

        }
        private void calc3Single4(int maxLen)
        {

            decimal div = 0;
            for (int i = maxLen; i > 0; i--)
            {
                int j;
                j = startValues4[i];
                div = (decimal)1 / (div + j);
            }
            method4.Items.Add((maxLen + 1).ToString() + ".) " + (2*div + 1));

            watch.Stop();
            ltime4.Text = watch.ElapsedMilliseconds + " ms";

        }
        //Dodao vrednost e
        private void Form1_Load(object sender, EventArgs et)
        {
            lE.Text = Convert.ToString(e);
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            if(numericUpDown1.Value > 1000)
            {
                cbSingleIter.Checked = true;
                cbSingleIter.Enabled = false;
            }
            else
            {
                cbSingleIter.Enabled = true;
            }
        }
    }

}
