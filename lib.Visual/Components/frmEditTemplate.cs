using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using lib.Class;

namespace lib.Visual.Components
{
  public partial class frmEditTemplate : lib.Visual.Models.frmDialog
  {
    #region Constructor
    public frmEditTemplate()
    {
      InitializeComponent();
    }
    #endregion

    #region Methods
    private void CarregaDados()
    {
      CarregaCombos();
      cbLabelTransparente.Checked = Resources.Skin.Labels.Transparent;
      cbAplicarSkin.Checked = Resources.Skin.Enabled;
      cmbLabelBStyle.Text = Resources.Skin.Labels.BorderStyle.ToString();
    }

    private void CarregaCombos()
    {      
      cmbLabelBStyle.Items.Clear();      
      cmbLabelBStyle.Items.AddRange(getEnumList(typeof(BorderStyle)));      
    }

    private object[] getEnumList(Type EnumType)
    {
      object[] lo = new object[] { };
      Array arr = Enum.GetValues(EnumType);
      lo = new object[arr.Length];

      for (int i = 0; i < lo.Length; i++)
      { lo[i] = arr.GetValue(i); }
      return lo;
    }
    private void Atualiza()
    {
      lblExLabel.Font = Resources.Skin.Labels.Font;
      lblExLabel.BackColor = Resources.Skin.Labels.BackColor;
      lblExLabel.ForeColor = Resources.Skin.Labels.ForeColor;
      lblExLabel.BorderStyle = Resources.Skin.Labels.BorderStyle;

      txtExControl.Font = Resources.Skin.Controls.Font;
      txtExControl.BackColor = Resources.Skin.Controls.BackColor;
      txtExControl.ForeColor = Resources.Skin.Controls.ForeColor;
      txtExControl.BorderStyle = BorderStyle.FixedSingle;
    }
    private Font TrocaFonte(Font fnt)
    {
      dlgFont.Font = fnt;
      if (dlgFont.ShowDialog() == DialogResult.OK)
      { fnt = dlgFont.Font; }
      return fnt;
    }
    private Color TrocaCor(Color clr)
    {
      dlgColor.Color = clr;
      if (dlgColor.ShowDialog() == DialogResult.OK)
      { clr = dlgColor.Color; }      
      return clr;
    }
    protected override void OnConfirm()
    {
      Resources.Skin.Enabled = cbAplicarSkin.Checked;
      Resources.Skin.Labels.Transparent = cbLabelTransparente.Checked;
      Resources.Skin.Labels.BorderStyle = ((BorderStyle)cmbLabelBStyle.SelectedIndex);      
      base.OnConfirm();
    }
    #endregion

    #region Events
    private void frmEditTemplate_Load(object sender, EventArgs e)
    {      
      CarregaDados();
    }

    private void btnFonteLabel_Click(object sender, EventArgs e)
    {
      Resources.Skin.Labels.Font = TrocaFonte(Resources.Skin.Labels.Font);
      Atualiza();
    }

    private void btnFontControl_Click(object sender, EventArgs e)
    {
      Resources.Skin.Controls.Font = TrocaFonte(Resources.Skin.Controls.Font);
      Atualiza();
    }

    private void btnControlColorBack_Click(object sender, EventArgs e)
    {
      Resources.Skin.Controls.BackColor = TrocaCor(Resources.Skin.Controls.BackColor);
      Atualiza();
    }

    private void btnControlColorFore_Click(object sender, EventArgs e)
    {
      Resources.Skin.Controls.ForeColor = TrocaCor(Resources.Skin.Controls.ForeColor);
      Atualiza();
    }

    private void btnControlColorBorder_Click(object sender, EventArgs e)
    {
      Resources.Skin.Controls.BorderColor = TrocaCor(Resources.Skin.Controls.BorderColor);
      Atualiza();
    }

    private void btnLabelFonte_Click(object sender, EventArgs e)
    {
      Resources.Skin.Labels.Font = TrocaFonte(Resources.Skin.Labels.Font);
      Atualiza();
    }

    private void btnLabelColorBack_Click(object sender, EventArgs e)
    {
      Resources.Skin.Labels.BackColor = TrocaCor(Resources.Skin.Labels.BackColor);
      Atualiza();
    }

    private void btnLabelColorFore_Click(object sender, EventArgs e)
    {
      Resources.Skin.Labels.ForeColor = TrocaCor(Resources.Skin.Labels.ForeColor);
      Atualiza();
    }

    private void btnLabelColorBorder_Click(object sender, EventArgs e)
    {
      Resources.Skin.Labels.BorderColor = TrocaCor(Resources.Skin.Labels.BorderColor);
      Atualiza();
    }

    private void btnContainerBackImage_Click(object sender, EventArgs e)
    {
      if (dlgOpen.ShowDialog() == DialogResult.OK)
      {
        Resources.Skin.Containers.ImageBack = ProcessImage.ImageToString(dlgOpen.FileName);
        Atualiza();
      }
    }

    private void btnButtonBack_Click(object sender, EventArgs e)
    {
      if (dlgOpen.ShowDialog() == DialogResult.OK)
      {
        Resources.Skin.Buttons.ImageBack = ProcessImage.ImageToString(dlgOpen.FileName);
        Atualiza();
      }
    }

    private void btnButtonHover_Click(object sender, EventArgs e)
    {
      if (dlgOpen.ShowDialog() == DialogResult.OK)
      {
        Resources.Skin.Buttons.ImageHover = ProcessImage.ImageToString(dlgOpen.FileName);
        Atualiza();
      }
    }

    private void sknButton1_Click(object sender, EventArgs e)
    {
      if (dlgOpen.ShowDialog() == DialogResult.OK)
      {
        Resources.Skin.Buttons.ImageDown = ProcessImage.ImageToString(dlgOpen.FileName);
        Atualiza();
      }
    }

    private void btnButtonFore_Click(object sender, EventArgs e)
    {
      Resources.Skin.Buttons.ForeColor = TrocaCor(Resources.Skin.Buttons.ForeColor);
      Atualiza();
    }

    private void btnTabBackColor_Click(object sender, EventArgs e)
    {
      if (dlgOpen.ShowDialog() == DialogResult.OK)
      {
        Resources.Skin.Containers.TabImageBack = ProcessImage.ImageToString(dlgOpen.FileName);
        Atualiza();
      }
    }

    private void sknButton1_Click_1(object sender, EventArgs e)
    {
      Resources.Skin.Buttons.Font = TrocaFonte(Resources.Skin.Buttons.Font);
      Atualiza();
    }

    private void sknButton2_Click(object sender, EventArgs e)
    {
      Resources.Skin.Buttons.BorderColor = TrocaCor(Resources.Skin.Buttons.BorderColor);
      Atualiza();
    }

    private void btnGridBackColor_Click(object sender, EventArgs e)
    {
      Resources.Skin.Controls.GridBackColor = TrocaCor(Resources.Skin.Controls.GridBackColor);
      Atualiza();
    }

    private void sknButton3_Click(object sender, EventArgs e)
    {
      Resources.Skin.Controls.ColumnBackColor = TrocaCor(Resources.Skin.Controls.ColumnBackColor);
      Atualiza();
    }

    private void btnImgGroupBox_Click(object sender, EventArgs e)
    {
      if (dlgOpen.ShowDialog() == DialogResult.OK)
      {
        Resources.Skin.Controls.ImageGroupBok = ProcessImage.ImageToString(dlgOpen.FileName);
        Atualiza();
      }
    }
    #endregion

    private void sknButton4_Click(object sender, EventArgs e)
    {
      if (dlgOpen.ShowDialog() == DialogResult.OK)
      {
        Resources.Skin.Controls.ImageProgressBar = ProcessImage.ImageToString(dlgOpen.FileName);
        Atualiza();
      }
    }

    private void btnControlInactive_Click(object sender, EventArgs e)
    {
      Resources.Skin.Controls.BackColorInactive = TrocaCor(Resources.Skin.Controls.BackColorInactive);
      Atualiza();
    }

    private void sknButton5_Click(object sender, EventArgs e)
    {
      Resources.Skin.Containers.BackColor = TrocaCor(Resources.Skin.Containers.BackColor);
    }

    private void sknButton6_Click(object sender, EventArgs e)
    {
      Resources.Skin.Containers.ButtonAreaBackColor = TrocaCor(Resources.Skin.Containers.ButtonAreaBackColor);
    }
  }
}
