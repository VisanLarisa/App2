using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace App2
{
    class dGV: DataGridView
    {
        protected override void PaintBackground(Graphics graphics, Rectangle clipBounds, Rectangle gridBounds)
        {
            base.PaintBackground(graphics, clipBounds, gridBounds);
            Rectangle rectSource = new Rectangle(this.Location.X,this.Location.Y, this.Width, this.Height);
            Rectangle rectDest = new Rectangle(0, 0, rectSource.Width, rectSource.Height);
            Bitmap b = new Bitmap(Parent.ClientRectangle.Width, Parent.ClientRectangle.Height);
            Graphics.FromImage(b).DrawImage(this.Parent.BackgroundImage, Parent.ClientRectangle);
            graphics.DrawImage(b, rectDest, rectSource, GraphicsUnit.Pixel);
            SetCellsTransparent();
        }

        private void SetCellsTransparent()
        {
            this.EnableHeadersVisualStyles = false;
            this.ColumnHeadersDefaultCellStyle.BackColor = Color.Transparent;
            this.RowHeadersDefaultCellStyle.BackColor = Color.Transparent;
            this.DefaultCellStyle.BackColor = Color.Transparent;
            this.DefaultCellStyle.SelectionBackColor = Color.Transparent;

            //bonus
            this.BorderStyle = BorderStyle.None;
            this.CellBorderStyle = DataGridViewCellBorderStyle.None;
            this.ColumnHeadersBorderStyle= DataGridViewHeaderBorderStyle.None;
            this.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(62, 120, 138);
            this.DefaultCellStyle.ForeColor = Color.Yellow;
            this.DefaultCellStyle.SelectionForeColor = Color.FromArgb(62, 120, 138);
            this.RowHeadersDefaultCellStyle.ForeColor = Color.FromArgb(62, 120, 138);
            this.RowHeadersBorderStyle= DataGridViewHeaderBorderStyle.None;
            this.RowHeadersDefaultCellStyle.SelectionBackColor = Color.Transparent;

            this.ColumnHeadersDefaultCellStyle.Font= new Font("Monotype Corsiva", 20);
            this.DefaultCellStyle.Font= new Font("Century Gothic", 12);

            /*     
                        Util.Find<HScrollBar>(this).BackColor = Color.Red;*/
        }
        static public class Util
        {
            static public T Find<T>(Control container) where T : Control
            {
                foreach (Control child in container.Controls)
                    return (child is T ? (T)child : Find<T>(child));
                // Not found.
                return null;
            }
        }
    }
}
