using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Office.Utils;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraRichEdit.API.Native;

namespace DragDropExample
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        public Form1()
        {
            InitializeComponent();           
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = CreateDataSource();
            SetUpGrid(gridControl1);
        }
        private DataTable CreateDataSource()
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("Id", typeof(int));
            dataTable.Columns.Add("CustomerName", typeof(string));
            dataTable.Columns.Add("CustomerAddress", typeof(string));
            dataTable.Columns.Add("Date", typeof(DateTime));

            for (int i = 0; i < 10; i++)
            {
                dataTable.Rows.Add(i, "Name" + i, "Address" + i, DateTime.Today.AddDays(i));
            }
            return dataTable;
        }
        public void SetUpGrid(GridControl gridControl)
        {
            GridView view = gridControl1.MainView as GridView;

            view.OptionsBehavior.Editable = false;
            view.MouseDown += view_MouseDown;
            view.MouseMove += view_MouseMove;

            gridControl.AllowDrop = true;
            gridControl.DragOver += gridControl_DragOver;
        }

        void gridControl_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;
        }

        GridHitInfo downHitInfo = null;
        void view_MouseMove(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.Button == MouseButtons.Left && downHitInfo != null)
            {
                Size dragSize = SystemInformation.DragSize;
                Rectangle dragRect = new Rectangle(new Point(downHitInfo.HitPoint.X - dragSize.Width / 2,
                    downHitInfo.HitPoint.Y - dragSize.Height / 2), dragSize);

                if (!dragRect.Contains(new Point(e.X, e.Y)))
                {
                    string cellTextValue = view.GetDataRow(downHitInfo.RowHandle)[downHitInfo.Column.FieldName].ToString();

                    view.GridControl.DoDragDrop(cellTextValue, DragDropEffects.Move);
                    downHitInfo = null;
                    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = true;
                }
            }
        }
        void view_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            downHitInfo = null;
            GridHitInfo hitInfo = view.CalcHitInfo(new Point(e.X, e.Y));
            if (Control.ModifierKeys != Keys.None) return;
            if (e.Button == MouseButtons.Left && hitInfo.RowHandle >= 0)
                downHitInfo = hitInfo;
        }

        private void richEditControl1_DragDrop(object sender, DragEventArgs e)
        {
            string value = (string)e.Data.GetData(typeof(string));
            richEditControl1.Document.BeginUpdate();
            richEditControl1.Document.InsertText(richEditControl1.Document.CaretPosition, value);
            richEditControl1.Document.EndUpdate();
            richEditControl1.Options.Behavior.Drop = DevExpress.XtraRichEdit.DocumentCapability.Enabled;
            richEditControl1.Focus();
        }
        private Graphics richEditGraphics = null;
        DocumentPosition oldPosition = null;
        private void richEditControl1_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(typeof(string)))
                e.Effect = DragDropEffects.Move;
            else
                e.Effect = DragDropEffects.None;

            Point docPoint = Units.PixelsToDocuments(richEditControl1.PointToClient(Form.MousePosition),
                richEditControl1.DpiX, richEditControl1.DpiY);

            DocumentPosition pos = richEditControl1.GetPositionFromPoint(docPoint);

            Rectangle rect = Units.DocumentsToPixels(richEditControl1.GetBoundsFromPosition(pos),
                richEditControl1.DpiX, richEditControl1.DpiY);

            richEditControl1.Document.CaretPosition = pos;

            if (richEditGraphics == null)
                richEditGraphics = richEditControl1.CreateGraphics();
            if (oldPosition != pos)
            {
                rect.Width = 2;
                richEditControl1.Refresh();
                richEditGraphics.FillRectangle(Brushes.Black, rect);
                richEditControl1.ScrollToCaret();
                oldPosition = pos;
            }                
        }
    }
}
