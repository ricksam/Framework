using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;
using lib.Visual.WinAPI;

namespace lib.Visual
{
  public static class Functions
  {
    #region public static string GetDirAppCondig()
    /// <summary>
    /// Retorna o diretório personalizado de configuração da aplicação
    /// </summary>
    /// <returns></returns>
    public static string GetDirAppCondig()
    {
      string Dir = 
        Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\" +
        Application.ProductName;

      if (!Directory.Exists(Dir))
      { Directory.CreateDirectory(Dir); }

      return Dir;
    }
    #endregion

    #region public static Bitmap GetImageFromHandle(IntPtr ptr, int width, int height)
    public static Bitmap GetImageFromHandle(IntPtr ptr, int width, int height)
    {
      //Variable to keep the handle to bitmap.
      IntPtr hBitmap;

      //Here we get the handle to the control device context.
      IntPtr hDC = USER32.GetDC(ptr);

      //Here we make a compatible device context in memory for screen device context.
      IntPtr hMemDC = GDI32.CreateCompatibleDC(hDC);      

      //We create a compatible bitmap of screen size using screen device context.
      hBitmap = GDI32.CreateCompatibleBitmap(hDC, width, height);

      //As hBitmap is IntPtr we can not check it against null. For this purspose IntPtr.Zero is used.
      if (hBitmap != IntPtr.Zero)
      {
        //Here we select the compatible bitmap in memeory device context and keeps the refrence to Old bitmap.
        IntPtr hOld = (IntPtr)GDI32.SelectObject(hMemDC, hBitmap);
        //We copy the Bitmap to the memory device context.
        GDI32.BitBlt(hMemDC, 0, 0, width, height, hDC, 0, 0, IMAGECAPTURE.SRCCOPY);
        //We select the old bitmap back to the memory device context.
        GDI32.SelectObject(hMemDC, hOld);
        //We delete the memory device context.
        GDI32.DeleteDC(hMemDC);
        //We release the screen device context.
        USER32.ReleaseDC(USER32.GetDesktopWindow(), hDC);
        //Image is created by Image bitmap handle and stored in local variable.
        Bitmap bmp = System.Drawing.Image.FromHbitmap(hBitmap);
        //Release the memory for compatible bitmap.
        GDI32.DeleteObject(hBitmap);
        //This statement runs the garbage collector manually.
        GC.Collect();
        //Return the bitmap 
        return bmp;
      }

      //If hBitmap is null retunrn null.
      return null;
    }
    #endregion

    #region public static Bitmap GetDesktopImage()
    public static Bitmap GetDesktopImage()
    {
      return GetImageFromHandle(
        //Here we get the handle to the desktop device context.
        USER32.GetDesktopWindow(),
        //We pass SM_CXSCREEN constant to GetSystemMetrics to get the X coordinates of screen.
        USER32.GetSystemMetrics(SCREENCAPTURE.SM_CXSCREEN),
        //We pass SM_CYSCREEN constant to GetSystemMetrics to get the Y coordinates of screen.
        USER32.GetSystemMetrics(SCREENCAPTURE.SM_CYSCREEN)
      );
    }
    #endregion

    #region public static void Sobre()
    public static void Sobre() 
    {
      Msg.Information(
        "Desenvolvedor: Ricardo Sampaio\n" +
        "Empresa: RCK Software\n" +
        "Contato: jricksam@gmail.com" +
        " \n",
        Application.ProductName
        );
    }
    #endregion

    public static void SetPropertiesGrid(System.Windows.Forms.DataGridView Grid) 
    {
      try
      {
        Grid.AutoGenerateColumns = false;
        Grid.EnableHeadersVisualStyles = true;
        Grid.BorderStyle = BorderStyle.FixedSingle;
        Grid.ReadOnly = true;
        Grid.MultiSelect = false;

        Grid.AllowUserToResizeColumns = true;
        Grid.AllowUserToResizeRows = false;
        Grid.AllowUserToAddRows = false;
        Grid.AllowUserToDeleteRows = false;
        Grid.AllowUserToOrderColumns = false;

        Grid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
        Grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

        Grid.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
        Grid.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
        Grid.ColumnHeadersDefaultCellStyle.BackColor = SystemColors.Control;
        Grid.ColumnHeadersDefaultCellStyle.ForeColor = SystemColors.WindowText;

        //this.ColumnHeadersDefaultCellStyle.Font = new Font("Microsoft Sans Serif", 8.25F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
        Grid.ColumnHeadersDefaultCellStyle.SelectionBackColor = SystemColors.Highlight;
        Grid.ColumnHeadersDefaultCellStyle.SelectionForeColor = SystemColors.HighlightText;
        Grid.ColumnHeadersHeight = 20;

        Grid.DefaultCellStyle.SelectionBackColor = Color.FromName("ActiveCaption");
        Grid.DefaultCellStyle.SelectionForeColor = Color.White;

        Grid.RowHeadersVisible = false;
        //this.RowHeadersWidth = 50;
        Grid.RowTemplate.Height = 20;
        Grid.RowHeadersWidthSizeMode = DataGridViewRowHeadersWidthSizeMode.DisableResizing;
        Grid.RowHeadersDefaultCellStyle.SelectionBackColor = Color.FromName("ActiveCaption");
        Grid.RowsDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 240);

        Grid.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromName("ActiveCaption");
        Grid.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.White;
        Grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(255, 255, 255);

        Grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
      }
      catch { return; }
    }
  }
}
