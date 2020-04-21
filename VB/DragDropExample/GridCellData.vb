Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace DragDropExample
	Public Class GridCellData
'INSTANT VB NOTE: The field content was renamed since Visual Basic does not allow fields to have the same name as other class members:
		Private content_Conflict As String
		Public Sub New(ByVal text As String)
			content_Conflict = text
		End Sub
		Public Property Content() As String
			Get
				Return content_Conflict
			End Get
			Set(ByVal value As String)
				content_Conflict = value
			End Set
		End Property
	End Class
End Namespace
