Imports System.Data.Odbc
Imports System.Data
Module Codes
    Public Function GetExceptionInfo(ex As Exception) As String
        Dim Result As String
        Dim hr As Integer = Runtime.InteropServices.Marshal.GetHRForException(ex)
        Result = ex.GetType.ToString & "(0x" & hr.ToString("X8") & "): " & ex.Message & Environment.NewLine & Environment.NewLine
        Dim st As StackTrace = New StackTrace(ex, True)
        For Each sf As StackFrame In st.GetFrames
            If sf.GetFileLineNumber() > 0 Then

                Result &= "Filename: " & IO.Path.GetFileName(sf.GetFileName) & ", Line: " & sf.GetFileLineNumber() & ", in method: " & sf.GetMethod.Name & Environment.NewLine

            End If
        Next
        Return Result
    End Function

    Function getBounds(row As Integer, column As Integer) As Rectangle
        Dim x, y, width, height As Integer
        Dim rect As Rectangle = frmMain.lstData.Items(row).SubItems(column).Bounds
        x = rect.X + frmMain.lstData.Location.X + 2
        y = rect.Y + frmMain.lstData.Location.Y + 2
        width = frmMain.lstData.Columns(column).Width
        height = rect.Height
        If column = 0 Then
            x = x + 23
            width = width - 23
        End If

        Return New Rectangle(x, y, width, height)
    End Function
        ''' <summary>
        ''' Returns a string consisting of the input with specifid characters un-duplicated, or all characters if SpecificChar is nothing.
        ''' </summary>
        ''' <param name="Str">Required. String expression to remove duplicated characters.</param>
        ''' <param name="SpecificChar">Optional. The character to remove its duplicates. If opted out defaults to all characters.</param>
        ''' <returns></returns>
        ''' <remarks></remarks>
    Function StrUnDup(ByVal Str As String, Optional ByVal SpecificChar As String = "") As String
        Dim sb As New System.Text.StringBuilder(Str.Length)
        Dim lastChar As Char

        For Each c As Char In Str
            If lastChar = c Then
                If SpecificChar = "" Then
                    Continue For
                End If
                If c = SpecificChar Then Continue For
            End If
            sb.Append(c)
            lastChar = c
        Next

        Return sb.ToString
    End Function

    Function MassSave(Mass As String) As List(Of ListViewItem)
        Dim RtExlmn As Integer = MsgBoxStyle.Exclamation + MsgBoxStyle.MsgBoxRight
        Dim Delimiter As String = vbTab
        Dim FullText As String() = Split(Mass, Environment.NewLine)
        Dim strCurrent As String()
        Dim dteCurrent As Date
        Dim items As New List(Of ListViewItem)
        For x As Integer = 0 To FullText.Length - 1
            FullText(x) = StrUnDup(FullText(x), vbTab)
            strCurrent = Split(FullText(x), Delimiter)
            If strCurrent(0).Trim = "" Then Continue For
            If strCurrent(0).Trim.StartsWith("[") = True Then Continue For

            If strCurrent.Length <> 3 Then
                MsgBox(String.Format("لم يتمكن البرنامج من قراءة البيانات في السطر {1}. من فضلك تأكد ثم حاول مجدداً.", x + 1), RtExlmn)
                Return Nothing
                Exit Function
            End If

            If strCurrent(0).Trim.Length > 255 Or strCurrent(0).Contains("#") Or strCurrent(0).Contains("|") Then
                MsgBox(String.Format("لم يتمكن البرنامج من قراءة اسم صحيح في السطر {1}. من فضلك تأكد ثم حاول مجدداً.", x + 1), RtExlmn)
                Return Nothing
                Exit Function
            End If


            If DateTime.TryParseExact(strCurrent(2), frmMain.strTrimmedFormat, Nothing, Globalization.DateTimeStyles.AllowWhiteSpaces, dteCurrent) = False Then
                MsgBox(String.Format("لم يتمكن البرنامج من قراءة تاريخ صحيح في السطر {1}. من فضلك تأكد ثم حاول مجدداً.", x + 1), RtExlmn)
                Return Nothing
                Exit Function
            End If
            If dteCurrent < frmAddEdit.txtExpiry.MinDate OrElse dteCurrent > frmAddEdit.txtExpiry.MaxDate Then
                MsgBox(String.Format("التاريخ الموجود في السطر {1} قديم جداً أو حديث جداً. من فضلك تأكد ثم حاول مجدداً.", x + 1), RtExlmn)
                Return Nothing
                Exit Function
            End If
            If strCurrent(1).Trim.Length <> 10 Then
                MsgBox(String.Format("لم يتمكن البرنامج من قراءة رقم إقامة صحيح في السطر {1}. من فضلك تأكد ثم حاول مجدداً.", x + 1), RtExlmn)
                Return Nothing
                Exit Function
            End If
            Dim itmCurrent As New ListViewItem(strCurrent(0).Trim)
            itmCurrent.SubItems.Add(strCurrent(1).Trim)
            itmCurrent.SubItems.Add(Format(dteCurrent, frmMain.strFormat))
            items.Add(itmCurrent)
        Next
        Return items
    End Function

    Sub CreateListViewItem(array As String())
        Dim item As New ListViewItem(array(0))
        Dim ByPass As Boolean = True
        For Each Obj As String In array
            If ByPass Then
                ByPass = False
                Continue For
            End If
            item.SubItems.Add(Obj)
        Next
        frmMain.lstData.Items.Add(item)
    End Sub

#Region "All Import Sub Procedures"
    Sub ImportTextfile(FileName As String)

        Dim Thingy As New IO.StreamReader(FileName)
        Dim strFile As String = Thingy.ReadToEnd
        Dim result As List(Of ListViewItem) = MassSave(strFile)
        Thingy.Close()
        Thingy.Dispose()
        If result Is Nothing Then
            Exit Sub
        End If
        If (frmMain.lstData.Items.Count <> 0 And frmExport.chkClear.Checked = True) AndAlso MsgBox("هذا الإجراء سيقوم بحذف كافة البيانات الحالية. المتابعة؟", vbDefaultButton2 + vbExclamation + vbYesNo + vbMsgBoxRight) = MsgBoxResult.Yes Then
            frmMain.lstData.Items.Clear()
        End If
        frmMain.lstData.Items.AddRange(result.ToArray)
        frmMain.RefreshList()
        MsgBox("تم تحميل البيانات بنجاح.", MsgBoxStyle.Information)

    End Sub

    Sub ImportDatabase(FileName As String)
        Dim Retrived As String() = frmDatabase.ShowDialog()
        If Retrived Is Nothing Then Exit Sub

        Dim Connection As New OdbcConnection("Driver={Microsoft Access Driver (*.mdb)};Dbq=")
        Dim DataAdapter As New OdbcDataAdapter("SELECT * FROM " & Retrived(0), Connection)
        Dim CommandBuilder As New OdbcCommandBuilder(DataAdapter)
        Dim DataTable As New DataTable
        Try

            Connection.ConnectionString &= FileName

            Try
                Connection.Open()
            Catch ex As OdbcException
                MsgBox("لم يستطع البرنامج فتح قاعدة البيانات. إن إستمر الخطأ راسلني مع ذكر نوع الويندوز." & Environment.NewLine & Environment.NewLine & GetExceptionInfo(ex), MsgBoxStyle.Exclamation + MsgBoxStyle.MsgBoxRight)
                Exit Sub
            End Try
            DataAdapter.Fill(DataTable)
            Dim DataView As DataView = DataTable.DefaultView

            DataTable = DataView.ToTable()
            Dim items As New List(Of ListViewItem)
            For Each subject As DataRow In DataTable.Rows
                Dim X As New ListViewItem(subject(Retrived(1)).ToString)
                X.SubItems.Add(subject(Retrived(2)).ToString)
                X.SubItems.Add(Format(DateTime.ParseExact(subject(Retrived(3)), frmMain.strTrimmedFormat, Nothing, Globalization.DateTimeStyles.AllowWhiteSpaces), frmMain.strFormat))
                items.Add(X)
            Next
            If (frmMain.lstData.Items.Count <> 0 And frmExport.chkClear.Checked = True) AndAlso MsgBox("هذا الإجراء سيقوم بحذف كافة البيانات الحالية. المتابعة؟", vbDefaultButton2 + vbExclamation + vbYesNo + vbMsgBoxRight) = MsgBoxResult.Yes Then
                frmMain.lstData.Items.Clear()
            End If
            frmMain.lstData.Items.AddRange(items.ToArray)
            frmMain.RefreshList()
            MsgBox("تم تحميل البيانات بنجاح.", MsgBoxStyle.Information)

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation)
        Finally
            Connection.Close()
            Connection.Dispose()
            DataAdapter.Dispose()
            CommandBuilder.Dispose()
            DataTable.Dispose()
        End Try
    End Sub

#End Region
#Region "All Export Sub Procedures"

#Region "Helpful Function"

    Function TextfileString() As String
        Dim Save As New System.Text.StringBuilder()

        Save.Append("[اسم المقيم]" & vbTab & vbTab & "[رقم الإقامة]" & vbTab & vbTab & "[تاريخ الإنتهاء]")
        For Each subject As ListViewItem In frmMain.lstData.Items
            Save.Append(Environment.NewLine)
            Save.Append(subject.Text)
            Save.Append(vbTab)
            Save.Append(vbTab)
            Save.Append(vbTab)
            Save.Append(subject.SubItems(1).Text)
            Save.Append(vbTab)
            Save.Append(vbTab)
            Save.Append(vbTab)
            Save.Append(subject.SubItems(2).Text)
        Next
        Return Save.ToString
    End Function
#End Region

    Sub ExportTextfile(FileName As String)

        Dim Thingy As New IO.StreamWriter(FileName)
        Thingy.WriteLine(TextfileString)
        Thingy.Flush()
        Thingy.Close()
        Thingy.Dispose()
        MsgBox("تم نسخ الملف بنجاح.", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight)

    End Sub

    Sub ExportDatabase(FileName As String)
        Dim Thingy As New IO.BinaryWriter(New IO.FileStream(FileName, IO.FileMode.Create))
        Thingy.Write(My.Resources.Database)
        Thingy.Flush()
        Thingy.Close()
        Thingy.Dispose()
        Dim Connection As New OdbcConnection("Driver={Microsoft Access Driver (*.mdb)};Dbq=")
        Dim DataAdapter As New OdbcDataAdapter("SELECT * FROM Table1", Connection)
        Dim CommandBuilder As New OdbcCommandBuilder(DataAdapter)
        Dim DataTable As New DataTable
        Try
            Connection.ConnectionString &= FileName

            Try
                Connection.Open()
            Catch ex As OdbcException
                MsgBox("لم يستطع البرنامج فتح قاعدة البيانات. إن إستمر الخطأ راسلني مع ذكر نوع الويندوز." & Environment.NewLine & Environment.NewLine & GetExceptionInfo(ex), MsgBoxStyle.Exclamation + MsgBoxStyle.MsgBoxRight)
                Exit Sub
            End Try
            DataAdapter.Fill(DataTable)
            Dim DataView As DataView = DataTable.DefaultView

            DataTable = DataView.ToTable()
            For Each subject As ListViewItem In frmMain.lstData.Items
                DataTable.Rows.Add({subject.Text, subject.SubItems(1).Text, subject.SubItems(2).Text})
            Next
            DataAdapter.Update(DataTable)
            MsgBox("تم نسخ الملف بنجاح.", MsgBoxStyle.Information + MsgBoxStyle.MsgBoxRight)
        Catch ex As Exception
            MessageBox.Show("Unfortunately an error has occured while saving the database. The database may be empty. Please report to Megadardery@yahoo.com with the following message:" _
               & Environment.NewLine & Environment.NewLine & GetExceptionInfo(ex), "Unhandled Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            Connection.Close()
            Connection.Dispose()
            DataAdapter.Dispose()
            DataTable.Dispose()
        End Try
    End Sub

#End Region

    Class ListViewItemComparer
        Implements IComparer

        Private col As Integer
        Private dat As String
        Private des As Boolean
        Public Sub New()
            col = 0
        End Sub

        Public Sub New(ByVal column As Integer)
            col = column
        End Sub

        Public Sub New(ByVal column As Integer, Descending As Boolean)
            col = column
            des = Descending
        End Sub

        Public Sub New(ByVal column As Integer, Dateformat As String)
            col = column
            dat = Dateformat
        End Sub

        Public Sub New(ByVal column As Integer, Descending As Boolean, Dateformat As String)
            col = column
            dat = Dateformat
            des = Descending
        End Sub

        Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer _
           Implements IComparer.Compare
            Dim result As Integer
            If dat <> "" Then
                Dim Date1 As Date = DateTime.ParseExact(CType(x, ListViewItem).SubItems(col).Text, dat, Nothing, Globalization.DateTimeStyles.AllowWhiteSpaces)
                Dim Date2 As Date = DateTime.ParseExact(CType(y, ListViewItem).SubItems(col).Text, dat, Nothing, Globalization.DateTimeStyles.AllowWhiteSpaces)
                result = [Date].Compare(Date1, Date2)
            Else
                result = [String].Compare(CType(x, ListViewItem).SubItems(col).Text, CType(y, ListViewItem).SubItems(col).Text)
            End If

            If des = True Then result = -result
            Return result
        End Function
    End Class
End Module

