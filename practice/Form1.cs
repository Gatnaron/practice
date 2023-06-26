using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace practice
{
    public partial class Form1 : Form
    {
        public SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["practice.Properties.Settings.factoryDBConnectionString"].ConnectionString);
        SqlCommand command = new SqlCommand();
        private SqlDataAdapter adapter = null;
        private DataTable dt = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "factoryDBDataSet.Employees". При необходимости она может быть перемещена или удалена.
            //this.employeesTableAdapter.Fill(this.factoryDBDataSet.Employees);
            FillE();
        }

        private void FillE()
        {
            conn.Open();
            adapter = new SqlDataAdapter("SELECT Employees.ID_Employees AS '№', Employees.Surname AS 'Фамилия', Employees.Name AS 'Имя', Employees.Patronymic AS 'Отчество', Department.Name AS 'Отдел' FROM Employees JOIN Department ON Department.ID_Department = Employees.ID_Department", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView2.DataSource = dt;
            conn.Close();
        }

        private void Fill1()
        {
            conn.Open();
            adapter = new SqlDataAdapter("SELECT ComSys.Name AS 'Название', Employees.Surname AS 'Фамилия', Employees.Name AS 'Имя', Employees.Patronymic AS 'Отчество' FROM ComSys JOIN Employees ON Employees.ID_Employees = ComSys.ID_Employees", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void Fill2()
        {
            conn.Open();
            adapter = new SqlDataAdapter("SELECT Products.Name AS 'Название', Employees.Surname AS 'Фамилия', Employees.Name AS 'Имя', Employees.Patronymic AS 'Отчество' FROM Products JOIN Employees ON Employees.ID_Employees = Products.ID_Employees JOIN ComSys ON Products.ID_ComSys = ComSys.ID_ComSys", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void Fill3()
        {
            conn.Open();
            adapter = new SqlDataAdapter("SELECT Details.Name AS 'Название', Employees.Surname AS 'Фамилия', Employees.Name AS 'Имя', Employees.Patronymic AS 'Отчество' FROM Details JOIN Employees ON Employees.ID_Employees = Details.ID_Employees JOIN  ComSys ON Details.ID_ComSys = ComSys.ID_ComSys JOIN Products ON Products.ID_Products = Details.ID_Products", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Fill1();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            Fill2();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            Fill3();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            conn.Open();
            adapter = new SqlDataAdapter($"SELECT Products.Name AS 'Название', Employees.Surname AS 'Фамилия', Employees.Name AS 'Имя', Employees.Patronymic AS 'Отчество' FROM Products JOIN Employees ON Employees.ID_Employees = Products.ID_Employees JOIN ComSys ON Products.ID_ComSys = ComSys.ID_ComSys WHERE Products.ID_ComSys = {textBox9.Text}", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            conn.Open();
            adapter = new SqlDataAdapter($"SELECT Details.Name AS 'Название', Employees.Surname AS 'Фамилия', Employees.Name AS 'Имя', Employees.Patronymic AS 'Отчество' FROM Details JOIN Employees ON Employees.ID_Employees = Details.ID_Employees JOIN  ComSys ON Details.ID_ComSys = ComSys.ID_ComSys JOIN Products ON Products.ID_Products = Details.ID_Products WHERE Details.ID_ComSys = {textBox10.Text} AND Details.ID_Products = {textBox12.Text}", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            conn.Open();
            adapter = new SqlDataAdapter($"SELECT ComSys.Name AS 'Название', Employees.Surname AS 'Фамилия', Employees.Name AS 'Имя', Employees.Patronymic AS 'Отчество' FROM ComSys JOIN Employees ON Employees.ID_Employees = ComSys.ID_Employees WHERE Employees.Surname = '{textBox13.Text}' AND Employees.Name = '{textBox11.Text}' AND Employees.Patronymic = '{textBox8.Text}'", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
            textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            conn.Open();
            command = new SqlCommand($"INSERT INTO ComSys(Name, ID_Employees) VALUES ('{textBox5.Text}', {textBox14.Text})", conn);
            command.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Система связи создана");
            Fill1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            conn.Open();
            command = new SqlCommand($"INSERT INTO Products(Name, ID_Employees, ID_ComSys) VALUES ('{textBox5.Text}.{textBox6.Text}', {textBox14.Text}, {textBox5.Text})", conn);
            command.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Изделие добавлено");
            Fill2();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            conn.Open();
            command = new SqlCommand($"INSERT INTO Details(Name, ID_Employees, ID_ComSys, ID_Products) VALUES ('{textBox5.Text}.{textBox6.Text}.{textBox7.Text}', {textBox14.Text}, {textBox5.Text}, {textBox6.Text})", conn);
            command.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Деталь добавлена");
            Fill3();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            conn.Open();
            adapter = new SqlDataAdapter($"SELECT Products.Name AS 'Название', Employees.Surname AS 'Фамилия', Employees.Name AS 'Имя', Employees.Patronymic AS 'Отчество' FROM Products JOIN Employees ON Employees.ID_Employees = Products.ID_Employees WHERE Employees.Surname = '{textBox13.Text}' AND Employees.Name = '{textBox11.Text}' AND Employees.Patronymic = '{textBox8.Text}'", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            conn.Open();
            adapter = new SqlDataAdapter($"SELECT Details.Name AS 'Название', Employees.Surname AS 'Фамилия', Employees.Name AS 'Имя', Employees.Patronymic AS 'Отчество' FROM Details JOIN Employees ON Employees.ID_Employees = Details.ID_Employees WHERE Employees.Surname = '{textBox13.Text}' AND Employees.Name = '{textBox11.Text}' AND Employees.Patronymic = '{textBox8.Text}'", conn);
            dt = new DataTable();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
            conn.Close();
        }
    }
}
