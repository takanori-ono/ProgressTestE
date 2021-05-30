using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using System.Reflection;

namespace ProgressTestExe
{
    public partial class Form1 : Form
    {
        public string Key { get; set; }

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Key = "12341234";

                var db = new webtestEntities();
                var new_rec = new progress_test() { no =Key, count = 0 };
                db.progress_test.Add(new_rec);
                db.SaveChanges();

                for (int cnt=10; cnt <= 100; cnt += 10)
                {
                    Thread.Sleep(1000);
                    new_rec.count = cnt;
                    db.SaveChanges();
                }
            }
            catch { }


        }

        private void count_up()
        {
            try
            {
                var db = new webtestEntities();
                var new_rec = new progress_test() { no = Key, count = 0 };
                db.progress_test.Add(new_rec);
                db.SaveChanges();

                for (int cnt = 10; cnt <= 100; cnt += 10)
                {
                    Thread.Sleep(1000);
                    new_rec.count = cnt;
                    db.SaveChanges();
                }
            }
            catch { }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string[] CommandParams = System.Environment.GetCommandLineArgs();
            bool param_ari = parse_param(CommandParams.ToList());

            count_up();
            create_file();

            if (param_ari) Close();
        }
        bool parse_param(List<string> parms)
        {
            if (parms.Count >= 2)
            {
                // 1番目は自分
                Key = parms[1];
                return true;
            }
            else
            {
                Key = "12341234";
            }
            return false;
        }
        void create_file()
        {
            string path = Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location) + $"\\{Key}.txt";
            var wr = new StreamWriter(path, false, System.Text.Encoding.GetEncoding("shift_jis"));
            wr.WriteLine(Key + " desu");
            wr.Close();
        }

    }
}
