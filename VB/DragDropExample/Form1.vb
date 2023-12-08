Imports System
Imports System.Data
Imports System.Drawing
Imports System.Windows.Forms
Imports DevExpress.Office.Utils
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraRichEdit.API.Native

Namespace DragDropExample

    Public Partial Class Form1
        Inherits DevExpress.XtraEditors.XtraForm

        Public Sub New()
            InitializeComponent()
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs)
            gridControl1.DataSource = CreateDataSource()
            SetUpGrid(gridControl1)
        End Sub

        Private Function CreateDataSource() As DataTable
            Dim dataTable As DataTable = New DataTable()
            dataTable.Columns.Add("Id", GetType(Integer))
            dataTable.Columns.Add("CustomerName", GetType(String))
            dataTable.Columns.Add("CustomerAddress", GetType(String))
            dataTable.Columns.Add("Date", GetType(Date))
            For i As Integer = 0 To 10 - 1
                dataTable.Rows.Add(i, "Name" & i, "Address" & i, Date.Today.AddDays(i))
            Next

            Return dataTable
        End Function

        Public Sub SetUpGrid(ByVal gridControl As GridControl)
            Dim view As GridView = TryCast(gridControl1.MainView, GridView)
            view.OptionsBehavior.Editable = False
            AddHandler view.MouseDown, AddressOf view_MouseDown
            AddHandler view.MouseMove, AddressOf view_MouseMove
            gridControl.AllowDrop = True
            AddHandler gridControl.DragOver, AddressOf gridControl_DragOver
        End Sub

        Private Sub gridControl_DragOver(ByVal sender As Object, ByVal e As DragEventArgs)
            If e.Data.GetDataPresent(GetType(GridCellData)) Then
                e.Effect = DragDropEffects.Move
            Else
                e.Effect = DragDropEffects.None
            End If
        End Sub

        Private downHitInfo As GridHitInfo = Nothing

        Private Sub view_MouseMove(ByVal sender As Object, ByVal e As MouseEventArgs)
            Dim view As GridView = TryCast(sender, GridView)
            If e.Button = MouseButtons.Left AndAlso downHitInfo IsNot Nothing Then
                Dim dragSize As Size = SystemInformation.DragSize
                Dim dragRect As Rectangle = New Rectangle(New Point(downHitInfo.HitPoint.X - dragSize.Width \ 2, downHitInfo.HitPoint.Y - dragSize.Height \ 2), dragSize)
                If Not dragRect.Contains(New Point(e.X, e.Y)) Then
                    Dim cellTextValue As String = view.GetDataRow(downHitInfo.RowHandle)(downHitInfo.Column.FieldName).ToString()
                    Dim clipboardData As GridCellData = New GridCellData(cellTextValue)
                    view.GridControl.DoDragDrop(clipboardData, DragDropEffects.Move)
                    downHitInfo = Nothing
                    DevExpress.Utils.DXMouseEventArgs.GetMouseArgs(e).Handled = True
                End If
            End If
        End Sub

        Private Sub view_MouseDown(ByVal sender As Object, ByVal e As MouseEventArgs)
            Dim view As GridView = TryCast(sender, GridView)
            downHitInfo = Nothing
            Dim hitInfo As GridHitInfo = view.CalcHitInfo(New Point(e.X, e.Y))
            If ModifierKeys <> Keys.None Then Return
            If e.Button = MouseButtons.Left AndAlso hitInfo.RowHandle >= 0 Then downHitInfo = hitInfo
        End Sub

        Private Sub richEditControl1_DragDrop(ByVal sender As Object, ByVal e As DragEventArgs)
            Dim value As GridCellData = CType(e.Data.GetData(GetType(GridCellData)), GridCellData)
            If value Is Nothing Then Return
            richEditControl1.Document.BeginUpdate()
            richEditControl1.Document.InsertText(richEditControl1.Document.CaretPosition, value.Content)
            richEditControl1.Document.EndUpdate()
            richEditControl1.Options.Behavior.Drop = DevExpress.XtraRichEdit.DocumentCapability.Enabled
            richEditControl1.Focus()
        End Sub

        Private richEditGraphics As Graphics = Nothing

        Private oldPosition As DocumentPosition = Nothing

        Private Sub richEditControl1_DragOver(ByVal sender As Object, ByVal e As DragEventArgs)
            If Not e.Data.GetDataPresent(GetType(GridCellData)) Then Return
            e.Effect = DragDropEffects.Move
            Dim docPoint As Point = Units.PixelsToDocuments(richEditControl1.PointToClient(New Point(e.X, e.Y)), richEditControl1.DpiX, richEditControl1.DpiY)
            Dim pos As DocumentPosition = richEditControl1.GetPositionFromPoint(docPoint)
            If pos Is Nothing Then Return
            Dim rect As Rectangle = Units.DocumentsToPixels(richEditControl1.GetBoundsFromPosition(pos), richEditControl1.DpiX, richEditControl1.DpiY)
            richEditControl1.Document.CaretPosition = pos
            If richEditGraphics Is Nothing Then richEditGraphics = richEditControl1.CreateGraphics()
            If oldPosition IsNot pos Then
                rect.Width = 2
                richEditControl1.Refresh()
                richEditGraphics.FillRectangle(Brushes.Black, rect)
                richEditControl1.ScrollToCaret()
                oldPosition = pos
            End If
        End Sub
    End Class
End Namespace
