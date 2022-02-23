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

namespace AppCRUD
{
    public partial class frmEmpleados : Form
    {
        //HOST 5
        string conexion = @"Server=148.202.15.215;Database=GESTION_PERSONAL;user Id=sa;Password=SqL-Mz$r(7c1*;";
        private int Tipo;
        private int? Id;
        public frmEmpleados(int tipo, int? id)
        {
            InitializeComponent();
            this.Tipo = tipo;
            this.Id = id;
        }
        private void frmEmpleados_Load(object sender, EventArgs e)
        {
            listar_catalogos();

            if (Tipo==1)
            {
                txtTitulo.Text = "Nuevo Registro";
            }
            else
            {
                txtTitulo.Text = "Editar Registro " + Id.ToString();
                txtID.Text = Id.ToString();
                mostrar_edicion(Id);
            }

        }


        void insert_db()
        {
            string sql = "EXEC sp_InsertEmpleado @Nombre,@APaterno,@AMaterno,@Edad ,@Fecha_Nacimiento,@Domicilio,@Telefono,@Correo,@Puesto,@Salario ,@Horario, " + 
                       "@Departamento, @Sucursal, @Estatus, @NSS, @EstadoCivil, @Idioma, @Pais";

            SqlConnection cn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text);
            cmd.Parameters.AddWithValue("@APaterno", txtAPaterno.Text);
            cmd.Parameters.AddWithValue("@AMaterno", txtAMaterno.Text);
            cmd.Parameters.AddWithValue("@Edad", Convert.ToInt32(txtEdad.Text));
            cmd.Parameters.AddWithValue("@Fecha_Nacimiento", Convert.ToDateTime(txtFechaNacimiento.Text));
            cmd.Parameters.AddWithValue("@Domicilio", txtDomicilio.Text);
            cmd.Parameters.AddWithValue("@Telefono", txtTelefono.Text);
            cmd.Parameters.AddWithValue("@Correo", txtCorreo.Text);
            cmd.Parameters.AddWithValue("@Puesto", txtPuesto.Text);
            cmd.Parameters.AddWithValue("@Salario", txtSalario.Text);
            cmd.Parameters.AddWithValue("@Horario", txtHorario.Text);
            cmd.Parameters.AddWithValue("@Departamento", txtDepartamento.Text);
            cmd.Parameters.AddWithValue("@Sucursal", txtSucursal.Text);
            cmd.Parameters.AddWithValue("@Estatus", txtEstatus.Text);
            cmd.Parameters.AddWithValue("@NSS", txtNSS.Text);
            cmd.Parameters.AddWithValue("@EstadoCivil", txtEstadoCivil.Text);
            cmd.Parameters.AddWithValue("@Idioma", txtIdioma.Text);
            cmd.Parameters.AddWithValue("@Pais", txtPais.Text);

            cmd.CommandType = CommandType.Text;
            cn.Open();
            try
            {
                int resultado = cmd.ExecuteNonQuery();
                if (resultado > 0)
                    MessageBox.Show("Registro Ingresado correctamente!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            finally
            {
                cn.Close();
            }
        }

        void update_db()
        {

        }
        void mostrar_edicion(int? Id)
        {
            string sql= "spListarEmpleados @id";
            SqlConnection cn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand(sql, cn);
            cmd.Parameters.AddWithValue("id",Id);
            cmd.CommandType = CommandType.Text;
            cn.Open();
            SqlDataReader reader;
            try
            {
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    txtID.Text = reader[0].ToString();
                    txtNombre.Text = reader[1].ToString();
                    txtAPaterno.Text = reader[2].ToString();
                    txtAMaterno.Text = reader[3].ToString();
                    txtEdad.Text = reader[4].ToString();
                    txtFechaNacimiento.Text = reader[5].ToString();
                    txtDomicilio.Text = reader[6].ToString();
                    txtTelefono.Text = reader[7].ToString();
                    txtCorreo.Text = reader[8].ToString();
                    txtPuesto.Text = reader[15].ToString();
                    txtSalario.Text = reader[16].ToString();
                    txtHorario.Text = reader[17].ToString();
                    txtDepartamento.Text = reader[18].ToString();
                    txtSucursal.Text = reader[19].ToString();
                    txtEstatus.Text = reader[14].ToString();
                    txtNSS.Text = reader[9].ToString();
                    txtEstadoCivil.Text = reader[10].ToString();
                    txtIdioma.Text = reader[11].ToString();
                    txtPais.Text = reader[12].ToString();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.ToString());
            }
            finally
            {
                cn.Close();
            }

        }

        void listar_catalogos()
        {
            txtPuesto.DataSource = get_catalogos("spListarCatPuestosEmpleados");
            txtEstatus.DataSource = get_catalogos("spListarCatEstatusEmpleados");
            txtDepartamento.DataSource = get_catalogos("spListarCatDepartamentos");
            txtEstadoCivil.DataSource = get_catalogos("spListarCatEstadoCivilEmpleados");
            txtHorario.DataSource = get_catalogos("spListarCatHorariosEmpleados");
            txtIdioma.DataSource = get_catalogos("spListarCatIdiomasempleados");
            txtPais.DataSource = get_catalogos("spListarCatPaisesEmpleados");
            txtSalario.DataSource = get_catalogos("spListarCatSalariosEmpleados");
            txtSucursal.DataSource = get_catalogos("spListarCatSucursales");

            txtPuesto.ValueMember = "id";
            txtEstatus.ValueMember = "estatus";
            txtDepartamento.ValueMember = "departamento";
            txtEstadoCivil.ValueMember = "estado_civil";
            txtHorario.ValueMember = "horario";
            txtIdioma.ValueMember = "idioma";
            txtPais.ValueMember = "pais";
            txtSalario.ValueMember = "salario";
            txtSucursal.ValueMember = "sucursal";
        }

        DataTable get_catalogos(string sp)
        {
            string sql = "EXEC " + sp;
            DataTable dt = new DataTable();
            SqlConnection cn = new SqlConnection(conexion);
            SqlCommand cmd = new SqlCommand(sql, cn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            cn.Open();
            try
            {
                da.Fill(dt);
            }
            catch
            {

            }
            finally
            {
                cn.Close();
            }
            return dt;
        }



        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if(Tipo==1)
            {
                insert_db();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
