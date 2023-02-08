using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;


namespace ConexionSqlServer
{
    public partial class Form1 : Form
    {
    
        //string CadenaCN = "data source =127.0.0.1\\SQLEXPRESS; database = ESCUELA; user id = eduardo; password = 2415; integrated security = true";
        string CadenaCN = "data source =(local); database = ABM; user id = eduardo; password = 2415; integrated security = true";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.MultiSelect = false;

        }

        private void ConsultarConectado()
        {
            SqlConnection CN = new SqlConnection(CadenaCN);
            string sSql = "Select * from Empleados";

            SqlCommand comm = new SqlCommand(sSql, CN);
            //se abre la coneccion
            CN.Open();
            SqlDataReader dr = comm.ExecuteReader();
            if (dr.HasRows)
            {
                DataTable dt = new DataTable();
                dt.Load(dr);
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("error al recuperar datos");
            }
            dr.Close();
            CN.Close();

        }

        private void ActualizarConectado()
        {
            string sSql = "UPDATE Personas SET Nombres = 'Juan' WHERE Id_Persona = 1";
            SqlConnection CN = new SqlConnection(CadenaCN);
            CN.Open();
            SqlCommand cmd = new SqlCommand(sSql, CN);
            int resultado = cmd.ExecuteNonQuery();
            CN.Close();
            if (resultado > 0) { MessageBox.Show("Se actualizaron " + resultado.ToString() + " registros"); }
        }

        private void ConsultarDesconectado()
        {
            SqlConnection CN = new SqlConnection(CadenaCN);
            CN.Open();
            string sSql = "Select * from Personas";
            SqlDataAdapter da = new SqlDataAdapter(sSql, CN);
            DataTable dt = new DataTable();
            da.Fill(dt);
            CN.Close();
            dataGridView1.DataSource = dt;
        }

        private void ActualizarDesconectado()
        {
            SqlConnection CN = new SqlConnection(CadenaCN);
            string sSql = "Select * from Personas";
            SqlDataAdapter da = new SqlDataAdapter(sSql, CN);
            SqlCommandBuilder cmb = new SqlCommandBuilder(da);
            DataSet ds = new DataSet();
            da.Fill(ds);
            ds.Tables[0].Rows[dataGridView1.SelectedRows[0].Index]["Apellido"] = textBox1.Text;
            ds.Tables[0].Rows[dataGridView1.SelectedRows[0].Index]["Nombres"] = textBox2.Text;
            ds.Tables[0].Rows[dataGridView1.SelectedRows[0].Index]["NroDoc"] = textBox3.Text;
            da.Update(ds);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ActualizarConectado();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConsultarDesconectado();
            dataGridView1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConsultarConectado();
            dataGridView1.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ActualizarDesconectado();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                label5.Text= dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells["Id_Persona"].Value.ToString();
                textBox1.Text = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells["Apellido"].Value.ToString();
                textBox2.Text = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells["Nombres"].Value.ToString();
                textBox3.Text = dataGridView1.Rows[dataGridView1.SelectedRows[0].Index].Cells["NroDoc"].Value.ToString();
            }
        }

        private void Identificador(object sender, EventArgs e)
        {

        }
    }
}
