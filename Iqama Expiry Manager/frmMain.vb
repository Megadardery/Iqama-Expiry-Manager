Imports System.Windows.Forms.ListView

Public Class frmMain
    'Even though this is neat and dandy, the Add20 function will need to be updated accordingly, namely the MagicNumber.
    Public Const strFormat As String = "dd / MM / yyyy"
    Public Const strTrimmedFormat As String = "d/M/yyyy"
    Public Const strSimplfiedFormat As String = "d/M/yy"
    Public Const MagicNumber As Integer = 3

    'Checks the full list and sets the forecolor accordingly. Fixes broken date formats (for whatever reason), and saves the list.
    Sub RefreshList()
        'The stringbuilder for the file save.
        Dim Save As New System.Text.StringBuilder()

        For Each subject As ListViewItem In lstData.Items

            Dim Name As ListViewItem.ListViewSubItem = subject.SubItems(0)
            Dim Number As ListViewItem.ListViewSubItem = subject.SubItems(1)
            Dim Expiry As ListViewItem.ListViewSubItem = subject.SubItems(2)

            Dim Diff As Long
            Try
                Dim dat As Date = DateTime.ParseExact(Expiry.Text, strFormat, Nothing, Globalization.DateTimeStyles.AllowWhiteSpaces)
                Expiry.Text = Format(dat, strFormat)
                Diff = DateDiff(DateInterval.Day, Today, dat)
            Catch ex As FormatException
                Dim newDate As Date

                If DateTime.TryParseExact(Expiry.Text, strTrimmedFormat, Nothing, Globalization.DateTimeStyles.AllowWhiteSpaces, newDate) = False Then
                    MessageBox.Show("لم يستطع التعرف علي أحد التواريخ", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.RightAlign)
                    Diff = 0
                    newDate = Now
                Else
                    Diff = DateDiff(DateInterval.Day, Today, newDate)
                End If
                Expiry.Text = Format(newDate, strFormat)
            End Try

            Select Case Diff
                Case Is < 1
                    subject.StateImageIndex = 3
                Case Is <= 60
                    subject.StateImageIndex = 1
                Case Is <= 90
                    subject.StateImageIndex = 2
                Case Else
                    subject.StateImageIndex = 0
            End Select
            If Save.Length <> 0 Then Save.Append("|")
            Save.Append(subject.Text)
            Save.Append("#")
            Save.Append(Number.Text)
            Save.Append("#")
            Save.Append(Expiry.Text)
        Next
        If Me.lstData.Items.Count = 0 Then frmExport.Avilable = False Else frmExport.Avilable = True
        Try
            Dim writer As New IO.StreamWriter(Database)
            writer.Write(Save.ToString())
            writer.Flush()
            writer.Close()
            writer.Dispose()
        Catch ex As Exception
            NoAccess(ex)
        End Try
        Save = Nothing
        If Lastcolumn <> -1 Then lstData_ColumnClick(Nothing, New ColumnClickEventArgs(Lastcolumn))
    End Sub
    Sub Edit()
        If lstData.SelectedItems.Count = 0 Then Exit Sub
        With lstData.SelectedItems
            Dim Result As String() = frmAddEdit.ShowDialog(.Item(0).Text, .Item(0).SubItems(1).Text, .Item(0).SubItems(2).Text)
            If Result IsNot Nothing Then

                .Item(0).Text = Result(0)
                .Item(0).SubItems(1).Text = Result(1)
                .Item(0).SubItems(2).Text = Result(2)
                RefreshList()
            End If
        End With
    End Sub

    'ADDING A NEW ITEM!
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles btnNew.Click
        Dim Result As String() = frmAddEdit.ShowDialog()
        If Result IsNot Nothing Then
            
            CreateListViewItem(Result)
            RefreshList()
        End If
    End Sub

    Sub NoAccess(ex As Exception)
        Process.Start(Application.LocalUserAppDataPath)
        MessageBox.Show("لا يستطيع البرنامج الوصول إلي مكان الحفظ. سيقوم البرنامج بالإغلاق." & Environment.NewLine & ex.GetType.Name & ": " & ex.Message, "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Application.Exit()
    End Sub

    Private Sub frmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        RefreshList()
    End Sub

    Private Sub frmMain_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.Control = True And e.KeyCode = Keys.N Then
            btnNew.PerformClick()
        End If
    End Sub
    ReadOnly Database As String = IO.Path.Combine(Application.LocalUserAppDataPath, "data.dat")
    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFastEdit.AutoSize = False
        txtFastEdit.Visible = False

        PictureBox1.Image = ImageList1.Images(3)
        PictureBox2.Image = ImageList1.Images(2)
        PictureBox3.Image = ImageList1.Images(1)
        PictureBox4.Image = ImageList1.Images(0)

        lstData.Items.Clear()
        Dim RetrivedData As String
        Try
            If IO.File.Exists(Database) Then
                Dim reader As New IO.StreamReader(Database)
                RetrivedData = reader.ReadToEnd
                reader.Close()
                reader.Dispose()
            Else
                Exit Sub
            End If


        Catch ex As Exception
            NoAccess(ex)
            If Me.lstData.Items.Count = 0 Then frmExport.Avilable = False
            Exit Sub
        End Try

        Try
            If RetrivedData = "" Then Exit Sub
            Dim SplitupData As String() = Split(RetrivedData, "|")
            For Each Current As String In SplitupData
                CreateListViewItem(Split(Current, "#"))
            Next
            RefreshList()
        Catch
            DeleteKey()
            lstData.Items.Clear()
            frmExport.Avilable = False
            Exit Sub
        End Try

    End Sub

    Sub DeleteKey()
        Try
            Process.Start(Application.LocalUserAppDataPath)
            If MessageBox.Show("لم يستطع البرنامج أن يفتح قاعدة البيانات." & vbCrLf & "سيحاول البرنامج حذفها، متابعة؟", "ملف تالف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.Yes Then
                IO.File.Delete(Database)
            End If

        Catch ex As Exception
            NoAccess(ex)
        End Try
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        frmExport.ShowDialog()
    End Sub


    Private Sub lstData_KeyDown(sender As Object, e As KeyEventArgs) Handles lstData.KeyDown
        If lstData.SelectedItems.Count <> 0 Then
            If e.Control = True And e.KeyCode = Keys.E Then
                Edit()
            ElseIf e.Control = True And e.KeyCode = Keys.F Then
                If btnFind.Enabled = True Then
                    btnFind.PerformClick()
                End If
            ElseIf e.Modifiers = Keys.None AndAlso e.KeyCode = Keys.Delete Then
                DeleteToolStripMenuItem.PerformClick()
            End If

        End If

    End Sub

    Private Sub lstData_MouseUp(sender As Object, e As MouseEventArgs) Handles lstData.MouseUp
        If e.Button = Windows.Forms.MouseButtons.Right Then
            If Me.lstData.SelectedItems.Count <> 0 AndAlso Me.lstData.FocusedItem.Bounds.Contains(e.Location) Then
                ContextMenuStrip1.Show(lstData, e.Location)
            ElseIf Me.lstData.Bounds.Contains(e.Location) Then
                ContextMenuStrip2.Show(lstData, e.Location)
            End If
        End If
    End Sub

    Private Sub ToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles ToolStripMenuItem2.Click
        btnNew.PerformClick()
    End Sub

    Private Sub mnuDeleteAll_Click(sender As Object, e As EventArgs) Handles mnuDeleteAll1.Click, mnuDeleteAll2.Click
        If Me.lstData.Items.Count <> 0 AndAlso MessageBox.Show("هل أنت متأكد من حذف جميع البيانات؟", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.Yes Then
            Me.lstData.Items.Clear()
            RefreshList()
        End If
    End Sub

    Private Sub EditToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles EditToolStripMenuItem.Click
        Edit()
    End Sub

    Private Sub DeleteToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles DeleteToolStripMenuItem.Click
        If lstData.SelectedItems.Count <> 0 AndAlso MessageBox.Show("هل تريد خذف بيانات " & Me.lstData.SelectedItems(0).Text & "?", "تأكيد الحذف", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2, MessageBoxOptions.RightAlign) = Windows.Forms.DialogResult.Yes Then
            lstData.SelectedItems(0).Remove()
            RefreshList()
            If lstData.FocusedItem IsNot Nothing Then lstData.FocusedItem.Selected = True
        End If
    End Sub


    Dim Lastcolumn As Integer = -1
    Private Sub lstData_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles lstData.ColumnClick


        Static Descending As Boolean = False

        If sender IsNot Nothing AndAlso e.Column = Lastcolumn Then Descending = Not Descending
        If e.Column <> Lastcolumn Then Descending = False
        Lastcolumn = e.Column

        If e.Column <= 1 Then

            Me.lstData.ListViewItemSorter = New ListViewItemComparer(e.Column, Descending)

        ElseIf e.Column = 2 Then
            Me.lstData.ListViewItemSorter = New ListViewItemComparer(e.Column, Descending, frmMain.strFormat)
        End If
        lstData.Sort()
        Me.lstData.ListViewItemSorter = Nothing
    End Sub

    Private Sub btnFind_Click(sender As Object, e As EventArgs) Handles btnFind.Click
        frmSearch.StartPosition = FormStartPosition.CenterParent
        If frmSearch.Visible = False Then frmSearch.Show(Me)

    End Sub

    Private Sub FindToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles FindToolStripMenuItem.Click
        btnFind.PerformClick()
    End Sub

















    Dim curRow As Integer
    Dim curCol As Integer

    Private Sub lstData_DoubleClick(sender As Object, e As EventArgs) Handles lstData.DoubleClick
        If lstData.SelectedItems.Count = 0 Then Exit Sub
        Dim position As Point = lstData.PointToClient(Windows.Forms.Cursor.Position)
        Dim index As Integer = 0
        With lstData.SelectedItems(0)
            txtFastEdit.MaxLength = 255
            If .SubItems(1).Bounds.Contains(position) Then
                index = 1
                txtFastEdit.MaxLength = 10
            ElseIf .SubItems(2).Bounds.Contains(position) Then
                index = 2
            End If
            curRow = .Index
            curCol = index

            txtFastEdit.Bounds = getBounds(curRow, curCol)
            txtFastEdit.Text = .SubItems(curCol).Text
            txtFastEdit.Visible = True
            txtFastEdit.Focus()
            txtFastEdit.SelectionStart = txtFastEdit.TextLength
            txtFastEdit.SelectionLength = 0
        End With

    End Sub

    Function Valid() As Boolean
        Dim txt As String = txtFastEdit.Text
        Select Case curCol
            Case 0
                If txt = "" Then Return False
            Case 1
                If txt.Length <> 10 Then Return False
            Case 2
                Dim newdate As Date
                If DateTime.TryParseExact(txt, strTrimmedFormat, Nothing, Globalization.DateTimeStyles.AllowWhiteSpaces, newdate) = False Then
                    Return False
                Else
                    txtFastEdit.Text = Format(newdate, strFormat)
                End If
        End Select
        Return True
    End Function

    Private Sub txtFastEdit_KeyDown(sender As Object, e As KeyEventArgs) Handles txtFastEdit.KeyDown
        If e.KeyCode = Keys.Enter Then
            If Valid() Then
                lstData.Items(curRow).SubItems(curCol).Text = txtFastEdit.Text
                RefreshList()
            End If
            txtFastEdit.Visible = False
            lstData.Focus()
        ElseIf e.KeyCode = Keys.Escape Then
            txtFastEdit.Visible = False
            lstData.Focus()
        End If
    End Sub


    Private Sub lstData_MouseClick(sender As Object, e As MouseEventArgs) Handles lstData.MouseClick
        If txtFastEdit.Visible = True Then
            If Valid() Then
                lstData.Items(curRow).SubItems(curCol).Text = txtFastEdit.Text
                RefreshList()
            End If
           
            txtFastEdit.Visible = False
            lstData.Focus()
        End If
    End Sub

    Private Sub txtFastEdit_TextChanged(sender As Object, e As EventArgs) Handles txtFastEdit.TextChanged
        If txtFastEdit.Text.Contains("#") Then
            Beep()
            txtFastEdit.Text = txtFastEdit.Text.Remove(InStr(txtFastEdit.Text, "#") - 1, 1)
        End If
        If txtFastEdit.Text.Contains("|") Then
            Beep()
            txtFastEdit.Text = txtFastEdit.Text.Remove(InStr(txtFastEdit.Text, "|") - 1, 1)
        End If
    End Sub
End Class