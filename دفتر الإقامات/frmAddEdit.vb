Imports System.Windows.Forms

Public Class frmAddEdit
 
    Private Function Dialog() As String()
        Dim result As DialogResult = MyBase.ShowDialog()

        If result = Windows.Forms.DialogResult.OK Then
            Return {txtNumber.Text, txtName.Text, txtExpiry.Text}
        Else
            Return Nothing
        End If
    End Function



    Public Overloads Function ShowDialog() As String()
        Me.Text = "إضافة بيان جديد"
        btnSave.Text = "إضافة"
        Me.txtName.Text = ""
        Me.txtExpiry.Value = Now
        Me.txtNumber.Text = ""
        btnSave.Enabled = False
        Return Dialog()
    End Function

    Public Overloads Function ShowDialog(ByVal Number As String, ByVal Name As String, ByVal Expiry As String) As String()
        Dim dtExpiry As Date = Date.ParseExact(Expiry, frmMain.strFormat, Nothing)

        Me.Text = "تعديل بيان حالي"
        btnSave.Text = "حفظ"
        Me.txtName.Text = Name
        Me.txtNumber.Text = Number
        If dtExpiry > txtExpiry.MaxDate OrElse dtExpiry < txtExpiry.MinDate Then
            Me.txtExpiry.Value = Now
        Else
            Me.txtExpiry.Value = dtExpiry
        End If
        btnSave.Enabled = True
        Return Dialog()
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub txt_TextChanged(sender As Object, e As EventArgs) Handles txtName.TextChanged, txtNumber.TextChanged
        Dim Textbox As TextBox = CType(sender, Windows.Forms.TextBox)
        If Textbox.Text.Contains("#") Then
            Beep()
            Textbox.Text = Textbox.Text.Remove(InStr(Textbox.Text, "#") - 1, 1)
        End If
        If Textbox.Text.Contains("|") Then
            Beep()
            Textbox.Text = Textbox.Text.Remove(InStr(Textbox.Text, "|") - 1, 1)
        End If
        If txtName.Text.Trim = "" OrElse txtNumber.Text.Trim.Length <> 10 OrElse Not IsNumeric(txtNumber.Text.Trim) Then Me.btnSave.Enabled = False Else Me.btnSave.Enabled = True
    End Sub

    Private Sub frmEdit_Shown(sender As Object, e As EventArgs) Handles Me.Shown
        txtNumber.Focus()
        txtNumber.SelectionStart = txtName.TextLength
        txtNumber.SelectionLength = 0
    End Sub

    Dim Month As Integer

    Private Sub txtExpiry_ValueChanged(sender As Object, e As EventArgs) Handles txtExpiry.ValueChanged
        If Month = 12 And txtExpiry.Value.Month = 1 And txtExpiry.Value.Year < txtExpiry.MaxDate.Year Then
            Month = txtExpiry.Value.Month
            txtExpiry.Value = DateAdd(DateInterval.Year, 1, txtExpiry.Value)
        ElseIf Month = 1 And txtExpiry.Value.Month = 12 And txtExpiry.Value.Year > txtExpiry.MinDate.Year Then
            Month = txtExpiry.Value.Month
            txtExpiry.Value = DateAdd(DateInterval.Year, -1, txtExpiry.Value)
        End If
        Month = txtExpiry.Value.Month
    End Sub

    Private Sub frmEdit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtExpiry.CustomFormat = frmMain.strFormat
    End Sub
End Class
