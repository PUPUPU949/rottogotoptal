using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

namespace проект0._1
{
    public partial class Form1 : Form
    {
        public database db = new database();
        int Product_type = 0;
        List<product> Products;
        string[] Types = new string[3] { "Электроника", "Мебель", "Продукты"};
        string[] Columns = new string[6] { "Id", "Type", "Quantity", "CostPerOne", "Cost", "Mass" };
        List<string> Changes = new List<string>();

        public Form1()
        {
            InitializeComponent();
            ShowTable();
        }

        private void ShowTable()
        {
            Products = db.ExecuteReader<product>("select * from [products];");
            dataGridView1.DataSource = Products;
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            if (((Button)sender).BackColor == Color.DodgerBlue)
            {
                Product_type = 0;
                ((Button)sender).BackColor = Color.Silver;
                return;
            }
            Product_type = 1;
            button4.BackColor = Color.Silver;
            button5.BackColor = Color.Silver;
            ((Button)sender).BackColor = Color.DodgerBlue;
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            if (((Button)sender).BackColor == Color.DodgerBlue)
            {
                Product_type = 0;
                ((Button)sender).BackColor = Color.Silver;
                return;
            }
            Product_type = 2;
            button3.BackColor = Color.Silver;
            button5.BackColor = Color.Silver;
            ((Button)sender).BackColor = Color.DodgerBlue;
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            if (((Button)sender).BackColor == Color.DodgerBlue)
            {
                Product_type = 0;
                ((Button)sender).BackColor = Color.Silver;
                return;
            }
            Product_type = 3;
            button4.BackColor = Color.Silver;
            button3.BackColor = Color.Silver;
            ((Button)sender).BackColor = Color.DodgerBlue;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (Product_type == 0)
            {
                MessageBox.Show("Выберите тип продукта!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            db.ExecuteNonQuery($"insert into [products]([Type],[Quantity],[CostPerOne],[Cost],[Mass]) values (\"{Types[Product_type - 1]}\",0,0,0,0);");
            ShowTable();
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            foreach (string change in Changes)
            {
                if (Columns[int.Parse(change.Split(',')[1])] == "Type")
                    db.ExecuteNonQuery($"update [products] set [{Columns[int.Parse(change.Split(',')[1])]}] = \"{change.Split(',')[2]}\" where [Id] = {int.Parse(change.Split(',')[0])};");
                else
                    db.ExecuteNonQuery($"update [products] set [{Columns[int.Parse(change.Split(',')[1])]}] = {change.Split(',')[2]} where [Id] = {int.Parse(change.Split(',')[0])};");
            }
            Changes.Clear();
            ShowTable();
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (Columns[e.ColumnIndex] == "Id")
                return;
            Changes.Add(dataGridView1[0, e.RowIndex].Value + "," + e.ColumnIndex + "," + dataGridView1[e.ColumnIndex, e.RowIndex].Value);
            Changes.Add(dataGridView1[0, e.RowIndex].Value + "," + 4 + "," + dataGridView1[4, e.RowIndex].Value);
            int sum = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                sum += int.Parse(row.Cells[4].Value.ToString());
            }
            label1.Text = "Общая стоимость груза на складе: " + sum;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
