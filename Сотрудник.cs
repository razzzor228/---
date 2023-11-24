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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace КП___Автопарковка
{
    public partial class Сотрудник : Form
    {
        public Сотрудник()
        {
            InitializeComponent();
        }

        private void Сотрудник_Load(object sender, EventArgs e)
        {
            sqlConnection1.Open();
            var temp = new DataTable();
            temp.Load(sqlCommand1.ExecuteReader());
            клиентDataGridView.DataSource = temp;
            //////////////
//            SELECT ПарковочноеМесто.Номер, ПарковочноеМесто.Тип, ПарковочноеМесто.Статус, Заезд.ГосНомер
//FROM     ПарковочноеМесто LEFT OUTER JOIN
//                  Заезд ON ПарковочноеМесто.Номер = Заезд.НомерМеста LEFT OUTER JOIN
//                  Выезд ON Заезд.IDЗаезда = Выезд.IDЗаезда
//WHERE(Заезд.ДатаЗаезда IS NOT NULL) AND(Выезд.ДатаВыезда IS NULL) AND(Выезд.ВремяВыезда IS NULL) OR
//                  (ПарковочноеМесто.Статус = 'Свободно')

            //string query = @"SELECT Клиент.ФИО, Автомобиль.ГосНомер, Автомобиль.Модель, Автомобиль.ТипТС, Автомобиль.ЦветТС
            //                FROM Клиент
            //                INNER JOIN Автомобиль ON Клиент.IDКлиента = Автомобиль.IDКлиента
            //                WHERE Клиент.ФИО = @ClientName AND Клиент.Телефон = @NT";

            //        SqlCommand command = new SqlCommand(query, sqlConnection1);
            var temp1 = new DataTable();
            temp1.Load(sqlCommand3.ExecuteReader());
            парковочноеМестоDataGridView.DataSource = temp1;
            //////////////
            var temp2 = new DataTable();
            temp2.Load(sqlCommand5.ExecuteReader());
            тарификацияDataGridView.DataSource = temp2;
            //////////////
            var temp3 = new DataTable();
            temp3.Load(sqlCommand6.ExecuteReader());
            транспортноеСредствоDataGridView.DataSource = temp3;
            //////////////
            var temp4 = new DataTable();
            temp4.Load(sqlCommand9.ExecuteReader());
            заездDataGridView.DataSource = temp4;
            //////////////
            var temp5 = new DataTable();
            temp5.Load(sqlCommand11.ExecuteReader());
            выездDataGridView.DataSource = temp5;
            //////////////
            var temp6 = new DataTable();
            temp6.Load(sqlCommand7.ExecuteReader());
            dataGridView1.DataSource = temp6;
            //////////////
            var temp7 = new DataTable();
            temp7.Load(sqlCommand12.ExecuteReader());
            оплатаDataGridView.DataSource = temp7;
            sqlConnection1.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.DataGridViewColumn Col = null;
            Col = клиентDataGridView.Columns["ФИО"]; 
            if (Col != null) 
            {
                if (radioButton1.Checked)
                    клиентDataGridView.Sort(Col, System.ComponentModel.ListSortDirection.Ascending);
                else
                    клиентDataGridView.Sort(Col, System.ComponentModel.ListSortDirection.Descending);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (maskedText_FIO_FULL.Text != "" || masked_NT.Text != "")
            {
                textBox1.Text = "ВВЕДИТЕ ВСЕ ДАННЫЕ!";
                sqlCommand2.Parameters["@part_of_the_word"].Value = maskedText_FIO_FULL.Text;
                sqlCommand2.Parameters["@NT"].Value = masked_NT.Text;
                sqlConnection1.Open();
                sqlCommand2.ExecuteNonQuery();
                bool proverka = (bool)sqlCommand2.Parameters["@proverka"].Value;
                string MESSAGE_res = (string)sqlCommand2.Parameters["@MESSAGE"].Value;
                if (proverka)
                {
                    int ID_RES = (int)sqlCommand2.Parameters["@ID"].Value;
                    string PD_res = (string)sqlCommand2.Parameters["@PD"].Value;
                    ID.Text = Convert.ToString(ID_RES);
                    PD.Text = PD_res;
                    textBox1.Text = MESSAGE_res;
                    string query = @"SELECT Клиент.ФИО, Автомобиль.ГосНомер, Автомобиль.Модель, Автомобиль.ТипТС, Автомобиль.ЦветТС
                            FROM Клиент
                            INNER JOIN Автомобиль ON Клиент.IDКлиента = Автомобиль.IDКлиента
                            WHERE Клиент.ФИО = @ClientName AND Клиент.Телефон = @NT";

                    SqlCommand command = new SqlCommand(query, sqlConnection1);
                    command.Parameters.AddWithValue("@ClientName", maskedText_FIO_FULL.Text);
                    command.Parameters.AddWithValue("@NT", masked_NT.Text);

                    var temp = new DataTable();
                    temp.Load(command.ExecuteReader());

                    dataGridView2.DataSource = temp;
                    sqlConnection1.Close();
                }
                else
                {
                    ID.Text = "";
                    PD.Text = "";
                    textBox1.Text = "";
                    var temp1 = new DataTable();
                    dataGridView2.DataSource = temp1;
                    textBox1.Text = MESSAGE_res;
                }
            }
            else
            {
                textBox1.Text = "ВВЕДИТЕ ВСЕ ДАННЫЕ!";
            }
            sqlConnection1.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            maskedText_FIO_FULL.Clear();
            ID.Clear();
            PD.Clear();
            MESSAGE.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (типComboBox.Text != "" || new_stoimost.Text != "")
            {
                sqlConnection1.Open();
                sqlCommand4.Parameters["@name_tarif"].Value = типComboBox.Text;
                sqlCommand4.Parameters["@new_tarif"].Value = Convert.ToInt32(new_stoimost.Text);
                sqlCommand4.ExecuteNonQuery();
                string result = (string)sqlCommand4.Parameters["@message"].Value;
                message_tarif.Text = result;
                var temp = new DataTable();
                temp.Load(sqlCommand5.ExecuteReader());
                тарификацияDataGridView.DataSource = temp;
                sqlConnection1.Close();
            }
            else
            {
                string result = "ВВЕДИТЕ ДАННЫЕ!";
                message_tarif.Text = result;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sqlConnection1.Open();
            var temp = new DataTable();
            temp.Load(sqlCommand7.ExecuteReader());
            dataGridView1.DataSource = temp;
            sqlConnection1.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sqlConnection1.Open();
            sqlCommand8.Parameters["@GN"].Value = masked_GN_in.Text;
            sqlCommand8.Parameters["@number_m"].Value = номерComboBox_in.Text;
            sqlCommand8.Parameters["@type_t"].Value = типComboBox1_in.Text;
            sqlCommand8.ExecuteNonQuery();
            string message = (string)sqlCommand8.Parameters["@message_in"].Value;
            message_in.Text = message;
            //////////////
            var temp1 = new DataTable();
            temp1.Load(sqlCommand3.ExecuteReader());
            парковочноеМестоDataGridView.DataSource = temp1;
            //////////////
            var temp2 = new DataTable();
            temp2.Load(sqlCommand5.ExecuteReader());
            тарификацияDataGridView.DataSource = temp2;
            //////////////
            var temp3 = new DataTable();
            temp3.Load(sqlCommand6.ExecuteReader());
            транспортноеСредствоDataGridView.DataSource = temp3;
            //////////////
            var temp4 = new DataTable();
            temp4.Load(sqlCommand9.ExecuteReader());
            заездDataGridView.DataSource = temp4;
            //////////////
            var temp5 = new DataTable();
            temp5.Load(sqlCommand11.ExecuteReader());
            выездDataGridView.DataSource = temp5;
            //////////////
            var temp6 = new DataTable();
            temp6.Load(sqlCommand7.ExecuteReader());
            dataGridView1.DataSource = temp6;
            //////////////
            var temp7 = new DataTable();
            temp7.Load(sqlCommand12.ExecuteReader());
            оплатаDataGridView.DataSource = temp7;
            sqlConnection1.Close();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            masked_GN_in.Clear();
            message_in.Clear();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sqlConnection1.Open();
            sqlCommand10.Parameters["@GN_out"].Value = masked_GN_out.Text;
            sqlCommand10.ExecuteNonQuery();
            string message = (string)sqlCommand10.Parameters["@MESSAGE"].Value;
            message_out.Text = message;
            var temp = new DataTable();
            temp.Load(sqlCommand1.ExecuteReader());
            клиентDataGridView.DataSource = temp;
            //////////////
            var temp1 = new DataTable();
            temp1.Load(sqlCommand3.ExecuteReader());
            парковочноеМестоDataGridView.DataSource = temp1;
            //////////////
            var temp2 = new DataTable();
            temp2.Load(sqlCommand5.ExecuteReader());
            тарификацияDataGridView.DataSource = temp2;
            //////////////
            var temp3 = new DataTable();
            temp3.Load(sqlCommand6.ExecuteReader());
            транспортноеСредствоDataGridView.DataSource = temp3;
            //////////////
            var temp4 = new DataTable();
            temp4.Load(sqlCommand9.ExecuteReader());
            заездDataGridView.DataSource = temp4;
            //////////////
            var temp5 = new DataTable();
            temp5.Load(sqlCommand11.ExecuteReader());
            выездDataGridView.DataSource = temp5;
            //////////////
            var temp6 = new DataTable();
            temp6.Load(sqlCommand7.ExecuteReader());
            dataGridView1.DataSource = temp6;
            //////////////
            var temp7 = new DataTable();
            temp7.Load(sqlCommand12.ExecuteReader());
            оплатаDataGridView.DataSource = temp7;
            sqlConnection1.Close();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            masked_GN_out.Clear();
            message_out.Clear();
        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            sqlConnection1.Open();
            sqlCommand10.Parameters["@GN_out"].Value = masked_GN_out.Text;
            sqlCommand10.ExecuteNonQuery();
            string message = (string)sqlCommand10.Parameters["@MESSAGE"].Value;
            message_out.Text = message;
            var temp = new DataTable();
            temp.Load(sqlCommand1.ExecuteReader());
            клиентDataGridView.DataSource = temp;
            //////////////
            var temp1 = new DataTable();
            temp1.Load(sqlCommand3.ExecuteReader());
            парковочноеМестоDataGridView.DataSource = temp1;
            //////////////
            var temp2 = new DataTable();
            temp2.Load(sqlCommand5.ExecuteReader());
            тарификацияDataGridView.DataSource = temp2;
            //////////////
            var temp3 = new DataTable();
            temp3.Load(sqlCommand6.ExecuteReader());
            транспортноеСредствоDataGridView.DataSource = temp3;
            //////////////
            var temp4 = new DataTable();
            temp4.Load(sqlCommand9.ExecuteReader());
            заездDataGridView.DataSource = temp4;
            //////////////
            var temp5 = new DataTable();
            temp5.Load(sqlCommand11.ExecuteReader());
            выездDataGridView.DataSource = temp5;
            //////////////
            var temp6 = new DataTable();
            temp6.Load(sqlCommand7.ExecuteReader());
            dataGridView1.DataSource = temp6;
            //////////////
            var temp7 = new DataTable();
            temp7.Load(sqlCommand12.ExecuteReader());
            оплатаDataGridView.DataSource = temp7;
            sqlConnection1.Close();
            sqlConnection1.Close();
        }

        private void button6_Click_1(object sender, EventArgs e)
        {
            textBox_fio.Text = "";
            textBox3_pd.Text = "";
            textBox2_tel.Text = "";
            if (masked_GN_auto.Text != "")
            {
                sqlConnection1.Open();
                sqlCommand13.Parameters["@GN"].Value = masked_GN_auto.Text;
                sqlCommand13.ExecuteNonQuery();
                string MESSAGE_res = (string)sqlCommand13.Parameters["@MESSAGE"].Value;     
                if (MESSAGE_res != "ДАННЫЙ ГОС.НОМЕР НЕ ЗАРЕГИСТРИРОВАН НА АВТОПАРКОВКЕ!")
                {
                    int Num_res;
                    string ID_FIO = (string)sqlCommand13.Parameters["@FIO"].Value;
                    string TEL_res = (string)sqlCommand13.Parameters["@TEL"].Value;
                    string PD_res = (string)sqlCommand13.Parameters["@PD"].Value;
                    Num_res = Convert.ToInt32(sqlCommand13.Parameters["@Num"].Value);
                    if (Num_res != 100)
                        textBox2.Text = Convert.ToString(Num_res);
                    else textBox2.Text = "ДАННЫЙ АВТОМОБИЛЬ НЕ СТОИТ НА АВТОПАРКОВКЕ";
                    textBox_fio.Text = ID_FIO;
                    textBox3_pd.Text = PD_res;
                    textBox2_tel.Text = TEL_res;
                    message_poisk.Text = "ПОИСК ВЫПОЛНЕН УСПЕШНО!";
                }
                else
                {
                    message_poisk.Text = "ДАННЫЙ ГОС.НОМЕР НЕ ЗАРЕГИСТРИРОВАН НА АВТОПАРКОВКЕ!";
                }
                sqlConnection1.Close();
            }
            else
            {
                message_poisk.Text = "ВВЕДИТЕ ВСЕ ДАННЫЕ!";
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            textBox_fio.Text = "";
            textBox3_pd.Text = "";
            textBox2_tel.Text = "";
            message_poisk.Text = "";
            masked_GN_auto.Text = "";
        }

        private void button11_Click(object sender, EventArgs e)
        {
            ID.Text = "";
            PD.Text = "";
            masked_NT.Text = "";
            textBox1.Text = "";
            maskedText_FIO_FULL.Text = "";
            var temp = new DataTable();
            dataGridView2.DataSource = temp;
        }
    }
}
