Imports System.Windows.Forms

Public Class frmDatabase

    Private Sub OK_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub Cancel_Button_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel_Button.Click
        Me.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.Close()
    End Sub
    Public Overloads Function ShowDialog() As String()
        Dim result As DialogResult = MyBase.ShowDialog()
        If result = Windows.Forms.DialogResult.OK Then
            Return {TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text}
        Else
            Return Nothing
        End If
    End Function
    Private Sub Dialog1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        TextBox1.Text = "Table1"
        TextBox2.Text = "ResidentName"
        TextBox3.Text = "ID"
        TextBox4.Text = "Expiry"
    End Sub
End Class
