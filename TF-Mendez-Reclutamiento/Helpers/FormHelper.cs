using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TF_Mendez_Reclutamiento.Helpers
{
    public static class FormHelper
    {
        public static void LimpiarTextBoxes(this Form Form, Panel Panel = null)
        {
            if (Panel != null)
            {
                foreach (var item in Panel.Controls.OfType<TextBox>())
                {
                    item.Clear();
                }
            }
            else
            {
                foreach (var panel in Form.Controls.OfType<Panel>())
                {
                    foreach (var item in panel.Controls.OfType<TextBox>())
                    {
                        item.Clear();
                    }
                }
                    
            }
        }        

        public static void HabilitarTextBoxes(this Form Form, bool Habilitado, Panel Panel = null)
        {
            if (Panel != null)
            {
                foreach (var item in Panel.Controls.OfType<TextBox>())
                {
                    item.Enabled = Habilitado;
                }
            }
            else
            {
                foreach (var panel in Form.Controls.OfType<Panel>())
                {
                    foreach (var item in panel.Controls.OfType<TextBox>())
                    {
                        item.Enabled = Habilitado;
                    }
                }
                    
            }
        }

        public static void HabilitarComboBoxes(this Form Form, bool Habilitado, Panel Panel = null)
        {
            if (Panel != null)
            {
                foreach (var item in Panel.Controls.OfType<ComboBox>())
                {
                    item.Enabled = Habilitado;
                }
            }
            else
            {
                foreach (var panel in Form.Controls.OfType<Panel>())
                {
                    foreach (var item in panel.Controls.OfType<ComboBox>())
                    {
                        item.Enabled = Habilitado;
                    }
                }

            }
        }

        public static void PrepararDataGridViews(this Form Form, Panel Panel = null)
        {
            if (Panel != null)
            {
                foreach (var item in Panel.Controls.OfType<DataGridView>())
                {
                    item.AllowUserToAddRows = false;
                    item.AllowUserToDeleteRows = false;
                    item.EditMode = DataGridViewEditMode.EditProgrammatically;
                }
            }
            else
            {
                foreach (var panel in Form.Controls.OfType<Panel>())
                {
                    foreach (var item in panel.Controls.OfType<DataGridView>())
                    {
                        item.AllowUserToAddRows = false;
                        item.AllowUserToDeleteRows = false;
                        item.EditMode = DataGridViewEditMode.EditProgrammatically;
                    }                    
                }
            }
        }

        public static void PrepararComboBoxes(this Form Form, Panel Panel = null)
        {
            if (Panel != null)
            {
                foreach (var item in Panel.Controls.OfType<ComboBox>())
                {
                    item.DropDownStyle = ComboBoxStyle.DropDownList;
                }
            }
            else
            {
                foreach (var panel in Form.Controls.OfType<Panel>())
                {
                    foreach (var item in panel.Controls.OfType<ComboBox>())
                    {
                        item.DropDownStyle = ComboBoxStyle.DropDownList;
                    }
                }
            }
        }
    }
}
