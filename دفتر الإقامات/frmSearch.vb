Imports System.Windows.Forms

Public Class frmSearch

    Private Sub chkDates_CheckedChanged(sender As Object, e As EventArgs) Handles chkDates.CheckedChanged
        Me.chkRange.Enabled = chkDates.Checked
        Me.lblExpiry.Enabled = chkDates.Checked
        Me.lblTo.Enabled = chkDates.Checked
        Me.txtExpiryS.Enabled = chkDates.Checked
        Me.txtExpiryE.Enabled = chkDates.Checked
    End Sub
    Dim Month As Integer
    Private Sub txtExpiryS_ValueChanged(sender As Object, e As EventArgs) Handles txtExpiryS.ValueChanged, txtExpiryE.ValueChanged
        Dim DTP As DateTimePicker = CType(sender, DateTimePicker)
        If Month = 12 And DTP.Value.Month = 1 And DTP.Value.Year < DTP.MaxDate.Year Then
            Month = DTP.Value.Month
            DTP.Value = DateAdd(DateInterval.Year, 1, DTP.Value)
        ElseIf Month = 1 And DTP.Value.Month = 12 And DTP.Value.Year > DTP.MinDate.Year Then
            Month = DTP.Value.Month
            DTP.Value = DateAdd(DateInterval.Year, -1, DTP.Value)
        End If
        Month = DTP.Value.Month
    End Sub

    Private Sub chkSingleDate_CheckedChanged(sender As Object, e As EventArgs) Handles chkRange.CheckedChanged
        If chkRange.Checked = False Then
            Me.lblExpiry.Text = "تاريخ:"
            Me.txtExpiryE.Visible = False
            Me.lblTo.Visible = False
        Else
            Me.lblExpiry.Text = "من:"
            Me.txtExpiryE.Visible = True
            Me.lblTo.Visible = True
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If frmMain.lstData.Items.Count = 0 Then
            MessageBox.Show("لا يوجد بيانات للبحث", "بحث", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
            Exit Sub
        End If


        Dim Index As Integer = 0
        If frmMain.lstData.SelectedItems.Count <> 0 AndAlso frmMain.lstData.SelectedIndices(0) <> frmMain.lstData.Items.Count - 1 Then
            Index = frmMain.lstData.SelectedIndices(0) + 1
        End If

        Dim strName As String = txtName.Text.ToLower
        Dim strNumber As String = txtNumber.Text
        Dim Repeated As Boolean = False
        For counter As Integer = Index To frmMain.lstData.Items.Count
            If counter = frmMain.lstData.Items.Count Then
                If Repeated = True Then
                    MessageBox.Show("لم يتم إيجاد البيان المطلوب.", "بحث", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
                    If frmMain.lstData.SelectedItems.Count <> 0 Then frmMain.lstData.SelectedItems(0).Selected = False
                    Exit For
                Else
                    counter = 0
                    Repeated = True
                End If

            End If

            Dim _number As String = frmMain.lstData.Items(counter).Text.ToLower
            Dim _name As String = frmMain.lstData.Items(counter).SubItems(1).Text
            Dim _expiry As String = frmMain.lstData.Items(counter).SubItems(2).Text

            If txtName.Text = "" Then strName = _name 'Assign all items to the empty search name field.
            If chkExact.Checked AndAlso _name <> strName Then
                Continue For
            ElseIf chkExact.Checked = False AndAlso _name.Contains(strName) = False Then
                Continue For
            End If

            If txtNumber.Text = "" Then strNumber = _number
            If _number.StartsWith(strNumber) = False Then Continue For

            If chkDates.Checked = True Then

                If chkRange.Checked = False Then
                    If _expiry <> txtExpiryS.Text Then Continue For
                Else
                    Dim dteComparison As Date = DateTime.ParseExact(_expiry, frmMain.strFormat, Nothing)
                    Dim dteStart As Date = DateTime.ParseExact(Me.txtExpiryS.Text, frmMain.strFormat, Nothing)
                    Dim dteEnd As Date = DateTime.ParseExact(Me.txtExpiryE.Text, frmMain.strFormat, Nothing)
                    If dteComparison < dteStart OrElse dteComparison > dteEnd Then Continue For
                End If
            End If
            frmMain.lstData.Items(counter).Selected = True
            frmMain.lstData.EnsureVisible(counter)
            Exit For
        Next

    End Sub

    Private Sub frmSearch_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        frmMain.lstData.Focus()
    End Sub

    Private Sub frmSearch_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.CenterToParent()
        txtExpiryS.CustomFormat = frmMain.strFormat
        txtExpiryE.CustomFormat = frmMain.strFormat
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class
