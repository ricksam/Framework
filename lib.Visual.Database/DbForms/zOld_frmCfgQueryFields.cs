using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using lib.Database.Query;
using lib.Database.Drivers;
using lib.Visual.Components;

namespace lib.Visual.Forms
{
  public partial class frmCfgQueryFields : lib.Visual.Models.frmDialog
  {
    protected frmCfgQueryFields()
    {
      InitializeComponent();
    }

    public frmCfgQueryFields(DataTable Table):this()
    {      
      bs = new FieldColumn();
      cmbName.Items.Clear();
      for (int i = 0; i < Table.Columns.Count; i++)
      { cmbName.Items.Add(Table.Columns[i].ColumnName.ToUpper()); }
    }

    public FieldColumn bs { get; set; }

    private void CarregaDados() 
    {
      cmbTipo.Items.AddRange(Enum.GetNames(typeof(enmFieldType)));

      txtTexto.Text = bs.Text;
      cmbName.Text = bs.Name;
      cbSensitive.Checked = bs.Sensitive;
      cmbTipo.SelectedIndex = ((int)bs.Type);
      txtTamanho.AsInt = bs.Size;
    }

    protected override void OnConfirm()
    {
      bs.Text = txtTexto.Text;
      bs.Name = cmbName.Text;
      bs.Sensitive = cbSensitive.Checked;
      bs.Type = ((enmFieldType)cmbTipo.SelectedIndex);
      bs.Size = txtTamanho.AsInt;
      base.OnConfirm();
    }

    private void frmCfgQueryFields_Load(object sender, EventArgs e)
    {
      CarregaDados();
    }

    private void txtTexto_Leave(object sender, EventArgs e)
    {
      if (cmbName.Text == "")
      { cmbName.Text = txtTexto.Text; }
    }
  }
}
