using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RegistroEjemplo1.Entidades;
using RegistroEjemplo1.BLL;

namespace RegistroEjemplo1.UI.Registro
{
    public partial class RegistroPersona : Form
    {
        public RegistroPersona()
        {
            InitializeComponent();
        }

        private void Limpiar()
        {
            IdnumericUpDown.Value = 0;
            NombretextBox.Text = string.Empty;
            TelefonotextBox.Text = string.Empty;
            CedulatextBox.Text = string.Empty;
            DirecciontextBox.Text = string.Empty;
            FechaNacimientodateTimePicker.Value = DateTime.Now;
            MyErrorProvider.Clear();

        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        private Personas LlenarClase()
        {
            Personas persona = new Personas();
            persona.PersonaId = Convert.ToInt32(IdnumericUpDown.Value);
            persona.Nombre = NombretextBox.Text;
            persona.Telefono = TelefonotextBox.Text;
            persona.Cedula = CedulatextBox.Text;
            persona.Direccion = DirecciontextBox.Text;
            persona.FechaNacimiento = FechaNacimientodateTimePicker.Value;
            return persona;

        }

        private void LlenaCampo(Personas persona)
        {
            IdnumericUpDown.Value = persona.PersonaId;
            NombretextBox.Text = persona.Nombre;
            TelefonotextBox.Text = persona.Telefono;
            CedulatextBox.Text = persona.Cedula;
            DirecciontextBox.Text = persona.Direccion;
            FechaNacimientodateTimePicker.Value = persona.FechaNacimiento;    
        }


   


        private bool Validar()
        {
            bool paso = false;

            MyErrorProvider.Clear();

            if(string.IsNullOrWhiteSpace(NombretextBox.Text))
            {
                MyErrorProvider.SetError(NombretextBox,"El campo nombre no puede estar vacio");
                NombretextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(TelefonotextBox.Text))
            {
                MyErrorProvider.SetError(TelefonotextBox, "El campo telefono no puede estar vacio");
                TelefonotextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(CedulatextBox.Text))
            {
                MyErrorProvider.SetError(CedulatextBox, "El campo nombre no puede estar vacio");
                CedulatextBox.Focus();
                paso = false;
            }

            if (string.IsNullOrWhiteSpace(DirecciontextBox.Text))
            {
                MyErrorProvider.SetError(DirecciontextBox, "El campo nombre no puede estar vacio");
                DirecciontextBox.Focus();
                paso = false;
            }

            return paso;
        }

        private bool ExisteEnLaBaseDeDatos()
        {
            Personas personas = PersonasBll.Buscar((int)IdnumericUpDown.Value);

            return (personas != null);
        }

        private void Buscarbutton_Click(object sender, EventArgs e)
        {
            int id;

            Personas persona = new Personas();
            int.TryParse(IdnumericUpDown.Text, out id);

            Limpiar();

            persona = PersonasBll.Buscar(id);

            if(persona != null)
            {
                MessageBox.Show("Persona Encontrada");
                LlenaCampo(persona);
            }
            else
            {
                MessageBox.Show("Persona no Encotrada");
            }
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            MyErrorProvider.Clear();

            int id;
            int.TryParse(IdnumericUpDown.Text, out id);

            Limpiar();

            if (PersonasBll.Eliminar(id))
                MessageBox.Show("Eliminado", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MyErrorProvider.SetError(IdnumericUpDown, "No se puede eliminar una persona que no existe");

        }

        private void Guardarbutton_Click_1(object sender, EventArgs e)
        {
            Personas personas;
            bool paso = false;

            // if (!Validar())
            // return;
            personas = LlenarClase();
            Limpiar();

            if (IdnumericUpDown.Value == 0)
                paso = PersonasBll.Guardar(personas);
            else
            {
                if (!ExisteEnLaBaseDeDatos())
                {
                    MessageBox.Show("No se puedce modificar una persona que no existe", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                paso = PersonasBll.Modificar(personas);
            }

            if (paso)
                MessageBox.Show("Guardado!!", "Exito", MessageBoxButtons.OK, MessageBoxIcon.Information);
            else
                MessageBox.Show("Error al Guardar!!", "Fallo", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
