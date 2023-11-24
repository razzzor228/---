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
    public partial class НачатьРаботу : Form
    {
        public НачатьРаботу()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form form = new ВыборПользователя();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }
    }
}
