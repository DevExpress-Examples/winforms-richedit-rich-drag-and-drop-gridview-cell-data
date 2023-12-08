Namespace DragDropExample

    Public Class GridCellData

        Private contentField As String

        Public Sub New(ByVal text As String)
            contentField = text
        End Sub

        Public Property Content As String
            Get
                Return contentField
            End Get

            Set(ByVal value As String)
                contentField = value
            End Set
        End Property
    End Class
End Namespace
