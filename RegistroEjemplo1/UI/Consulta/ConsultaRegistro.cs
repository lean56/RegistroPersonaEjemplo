using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RegistroEjemplo1.BLL;
using RegistroEjemplo1.Entidades;

namespace RegistroEjemplo1.UI.Consulta
{
    public partial class ConsultaRegistro : Form
    {
        public ConsultaRegistro()
        {
            InitializeComponent();
        }

        private void Consultarbutton_Click(object sender, EventArgs e)
        {
            var listado = new List<Personas>();

            if (CriteriotextBox.Text.Trim().Length > 0)
            {
                switch (FiltrarcomboBox.SelectedIndex)
                {
                    case 0://Todo
                        listado = PersonasBll.GetList(p => true);
                        break;

                    case 1://ID
                        int id = Convert.ToInt32(CriteriotextBox.Text);
                        listado = PersonasBll.GetList(p => p.PersonaId == id);
                        break;

                    case 2://Nombre
                        listado = PersonasBll.GetList(p => p.Nombre.Contains(CriteriotextBox.Text));
                        break;

                    case 3://Cedula
                        listado = PersonasBll.GetList(p => p.Cedula.Contains(CriteriotextBox.Text));
                        break;

                    case 4://Direccion
                        listado = PersonasBll.GetList(p => p.Direccion.Contains(CriteriotextBox.Text));
                        break;
                }

                listado = listado.Where(c => c.FechaNacimiento.Date >= DesdedateTimePicker.Value.Date && c.FechaNacimiento.Date <= HastadateTimePicker.Value.Date).ToList();
            }
            else
            {
                listado = PersonasBll.GetList(p => true);
            }

            ConsultadataGridView.DataSource = null;
            ConsultadataGridView.DataSource = listado;
        }
    
    }
}
