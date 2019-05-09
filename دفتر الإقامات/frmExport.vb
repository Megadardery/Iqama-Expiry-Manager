Imports System.Windows.Forms
Public Class frmExport
    Public Avilable As Boolean

    Private Sub frmExport_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Avilable = False Then
            btnExport.Enabled = False
        Else
            btnExport.Enabled = True
        End If
    End Sub
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnImport_Click(sender As Object, e As EventArgs) Handles btnImport.Click
        Dim OpenDialog As New OpenFileDialog
        OpenDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        OpenDialog.Filter = "Supported file (*.mdb;*.txt)|*.mdb;*.txt|Access Database (*.mdb)|*.mdb|Textfile (*.txt)|*.txt"
        OpenDialog.Title = "Please select a file to load"
        If OpenDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Select Case IO.Path.GetExtension(OpenDialog.FileName)
                Case ".txt"
                    ImportTextfile(OpenDialog.FileName)
                Case ".mdb"
                    ImportDatabase(OpenDialog.FileName)
                Case Else
                    MsgBox("The file type is not supported.", MsgBoxStyle.Exclamation)
                    Exit Sub
            End Select
            If frmMain.lstData.Items.Count > 0 Then
                Me.Avilable = True
                frmExport_Load(Nothing, Nothing)
            End If
        End If
        OpenDialog.Dispose()
    End Sub

    Private Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim SaveDialog As New SaveFileDialog
        SaveDialog.DefaultExt = "mdb"
        SaveDialog.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
        SaveDialog.Filter = "Access Database (*.mdb)|*.mdb|Textfile (*.txt)|*.txt"
        SaveDialog.Title = "Please select a location to export to"
        If SaveDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Select Case IO.Path.GetExtension(SaveDialog.FileName)
                Case ".txt"
                    ExportTextfile(SaveDialog.FileName)
                Case ".mdb"
                    ExportDatabase(SaveDialog.FileName)
                Case Else
                    MsgBox("The file type is not supported.", MsgBoxStyle.Exclamation)
                    Exit Sub
            End Select
        End If
        SaveDialog.Dispose()
    End Sub

End Class
