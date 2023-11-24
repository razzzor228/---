using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace КП___Автопарковка
{
    public partial class ВыборПользователя : Form
    {
        public ВыборПользователя()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form form2_1 = new Сотрудник();
            this.Hide();
            form2_1.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form2_2 = new Клиент();
            this.Hide();
            form2_2.ShowDialog();
            this.Show();
        }
    }
}
