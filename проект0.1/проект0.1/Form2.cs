using System.Windows.Forms;
using System.Collections.Generic;

namespace проект0._1
{
    public partial class Form2 : Form
    {
        public database db = new database();
        List<sale> Sales = new List<sale>();

        public Form2()
        {
            InitializeComponent();
            comboBox1.Items.Add("Мебель");
            comboBox1.Items.Add("Продукты");
            comboBox1.Items.Add("Электроника");
            ViewListBox();
        }

        private void ViewListBox()
        {
            listBox1.Items.Clear();
            Sales = db.ExecuteReader<sale>("select * from [sales];");
            foreach(var sale in Sales)
            {
                listBox1.Items.Add(sale.Product_type + " - " + sale.Quantity + " шт. - " + sale.Operation_type);
            }
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (comboBox1.Text == "")
                return;
            string OpType = "";
            if (radioButton1.Checked)
                OpType = "Прибытие";
            if (radioButton2.Checked)
                OpType = "Убытие";
            if (OpType == "")
                return;
            db.ExecuteNonQuery($"insert into [sales]([Product_type],[Quantity],[CostPerOne],[Mass],[Client],[Operation_type]) values (\"{comboBox1.Text}\", {textBox2.Text}, {textBox3.Text}, {textBox4.Text}, \"{textBox5.Text}\", \"{OpType}\");");
            ViewListBox();
        }
    }
}
