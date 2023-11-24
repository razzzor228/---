using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace КП___Автопарковка
{
    public partial class Клиент : Form
    {
        public Клиент()
        {
            InitializeComponent();
        }

        private void Клиент_Load(object sender, EventArgs e)
        {
            sqlConnection1.Open();
            var temp = new DataTable();
            temp.Load(sqlCommand1.ExecuteReader());
            тарифDataGridView.DataSource = temp;
            var temp1 = new DataTable();
            temp1.Load(sqlCommand2.ExecuteReader());
            парковочноеМестоDataGridView.DataSource = temp1;
            sqlConnection1.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (masked_FIO.MaskCompleted && masked_PD.MaskCompleted
                && masked_NT.MaskCompleted && masked_GN.MaskCompleted
                && masked_MTS.MaskCompleted && masked_COLOR.MaskCompleted)
            {
                // присвоить значения входным параметрам процедуры с преобразованием типов
                sqlCommand3.Parameters["@new_fio"].Value = masked_FIO.Text;
                sqlCommand3.Parameters["@new_pd"].Value = masked_PD.Text;
                sqlCommand3.Parameters["@new_tel"].Value = masked_NT.Text;
                sqlCommand3.Parameters["@new_number"].Value = masked_GN.Text;
                sqlCommand3.Parameters["@new_model"].Value = masked_MTS.Text;
                sqlCommand3.Parameters["@new_tip"].Value = TTS.Text;
                sqlCommand3.Parameters["@new_color"].Value = masked_COLOR.Text;
                // открыть соединение с БД
                sqlConnection1.Open();
                // выполнить sql-выражение (хранимую процедуру) и вернуть количество измененных записей
                sqlCommand3.ExecuteNonQuery();
                // закрыть соединение с БД
                sqlConnection1.Close();
                // вывести значение выходного параметра
                string message = (string)sqlCommand3.Parameters["@message"].Value;
                message_add.Text = message; // на форму
                                            // вывод результата через окно сообщения
                MessageBox.Show(message, "Информация!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("ВВЕДИТЕ ВСЕ ДАННЫЕ!", "Информация!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            masked_FIO.Clear();
            masked_PD.Clear();
            masked_NT.Clear();
            masked_GN.Clear();
            masked_MTS.Clear();
            masked_COLOR.Clear();
            message_add.Clear();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (maskedTextBox4.MaskCompleted && maskedTextBox3.MaskCompleted
                && comboBox1.Text != "" && maskedTextBox1.MaskCompleted)
            {
                // присвоить значения входным параметрам процедуры с преобразованием типов
                sqlCommand4.Parameters["@NT"].Value = maskedTextBox5.Text; 
                sqlCommand4.Parameters["@FIO"].Value = maskedTextBox4.Text;
                sqlCommand4.Parameters["@GN"].Value = maskedTextBox3.Text;
                sqlCommand4.Parameters["@Model"].Value = maskedTextBox2.Text;
                sqlCommand4.Parameters["@Type"].Value = comboBox1.Text;
                sqlCommand4.Parameters["@Color"].Value = maskedTextBox1.Text;
                // открыть соединение с БД
                sqlConnection1.Open();
                // выполнить sql-выражение (хранимую процедуру) и вернуть количество измененных записей
                sqlCommand4.ExecuteNonQuery();
                // закрыть соединение с БД
                sqlConnection1.Close();
                // вывести значение выходного параметра
                string message = (string)sqlCommand4.Parameters["@Message"].Value;
                textBox1.Text = message; // на форму
                                            // вывод результата через окно сообщения
                MessageBox.Show(message, "Информация!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("ВВЕДИТЕ ВСЕ ДАННЫЕ!", "Информация!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
