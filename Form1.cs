using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace AppCRUD
{
    public partial class Form1 : Form
    {
        //string conexion = @"Server=.\SQLEXPRESS;Database=db_gimnasio;Trusted_Connection=True;";
        string conexion = @"Server=148.202.15.215;Database=GESTION_PERSONAL;user Id=sa;Password=SqL-Mz$r(7c1*;";
        DataTable dt = new DataTable();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            select_db();
        }

        void select_db()
        {
            string sql = "EXEC spListarEmpleados";
            SqlConnection cn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cn.Open();

            try
            {
                dataEmpleados.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            finally
            {
                cn.Close();
            }
        }

        void delete_db()
        {

        }


        void captura(int tipo, int? id)
        {
            //TIPO 1 NUEVO
            if (tipo==1)
            {
                frmEmpleados frm = new frmEmpleados(tipo, null);
                frm.ShowDialog();
            }

            //TIPO 2 EDICION
            else
            {
                frmEmpleados frm = new frmEmpleados(tipo, id);
                frm.ShowDialog();
            }


        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            captura(1,null);
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            int valorId = Convert.ToInt32(dataEmpleados.CurrentRow.Cells["id"].Value);

            captura(2, valorId);
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = $"nombre LIKE '{txtBuscar.Text}%'";
        }
    }
}
