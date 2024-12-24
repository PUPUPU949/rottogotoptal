using System.Windows.Forms;
using System.Linq;

namespace проект0._1
{
    public partial class Form3 : Form
    {
        public database db = new database();

        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (db.ExecuteReader<user>("select * from [users];").Where(x => x.Login == textBox1.Text && x.Password == textBox2.Text).Any())
            {
                if (db.ExecuteReader<user>("select * from [users];").Where(x => x.Login == textBox1.Text && x.Password == textBox2.Text).First().Role == 1)
                {
                    MessageBox.Show("Вы успешно вошли как управляющий!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    new Form1().Show();
                }
                else if (db.ExecuteReader<user>("select * from [users];").Where(x => x.Login == textBox1.Text && x.Password == textBox2.Text).First().Role == 2)
                {
                    MessageBox.Show("Вы успешно вошли как сотрудник!", "Успех!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    new Form2().Show();
                }
                return;
            }
            MessageBox.Show("Аккаунт не найден!", "Ошибка!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
