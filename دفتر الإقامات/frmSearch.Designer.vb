<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSearch
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtExpiryS = New System.Windows.Forms.DateTimePicker()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtName = New System.Windows.Forms.TextBox()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.lblExpiry = New System.Windows.Forms.Label()
        Me.grbName = New System.Windows.Forms.GroupBox()
        Me.chkExact = New System.Windows.Forms.CheckBox()
        Me.grbDate = New System.Windows.Forms.GroupBox()
        Me.chkRange = New System.Windows.Forms.CheckBox()
        Me.chkDates = New System.Windows.Forms.CheckBox()
        Me.txtExpiryE = New System.Windows.Forms.DateTimePicker()
        Me.lblTo = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.txtNumber = New System.Windows.Forms.TextBox()
        Me.grbName.SuspendLayout()
        Me.grbDate.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtExpiryS
        '
        Me.txtExpiryS.CustomFormat = "MM / yyyy"
        Me.txtExpiryS.Enabled = False
        Me.txtExpiryS.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpiryS.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtExpiryS.Location = New System.Drawing.Point(209, 24)
        Me.txtExpiryS.MaxDate = New Date(3999, 12, 31, 0, 0, 0, 0)
        Me.txtExpiryS.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.txtExpiryS.Name = "txtExpiryS"
        Me.txtExpiryS.Size = New System.Drawing.Size(144, 29)
        Me.txtExpiryS.TabIndex = 1
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancel.Font = New System.Drawing.Font("Times New Roman", 9.75!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancel.Location = New System.Drawing.Point(136, 184)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(68, 22)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "إلغاء"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtName
        '
        Me.txtName.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtName.Location = New System.Drawing.Point(6, 28)
        Me.txtName.MaxLength = 255
        Me.txtName.Name = "txtName"
        Me.txtName.Size = New System.Drawing.Size(269, 29)
        Me.txtName.TabIndex = 0
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Font = New System.Drawing.Font("Times New Roman", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnNext.Location = New System.Drawing.Point(12, 170)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(118, 36)
        Me.btnNext.TabIndex = 3
        Me.btnNext.Text = "إوجد التالي"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'lblExpiry
        '
        Me.lblExpiry.AutoSize = True
        Me.lblExpiry.Enabled = False
        Me.lblExpiry.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblExpiry.Location = New System.Drawing.Point(359, 30)
        Me.lblExpiry.Name = "lblExpiry"
        Me.lblExpiry.Size = New System.Drawing.Size(47, 21)
        Me.lblExpiry.TabIndex = 0
        Me.lblExpiry.Text = "تاريخ:"
        '
        'grbName
        '
        Me.grbName.Controls.Add(Me.chkExact)
        Me.grbName.Controls.Add(Me.txtName)
        Me.grbName.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grbName.Location = New System.Drawing.Point(143, 12)
        Me.grbName.Name = "grbName"
        Me.grbName.Size = New System.Drawing.Size(281, 78)
        Me.grbName.TabIndex = 0
        Me.grbName.TabStop = False
        Me.grbName.Text = "اسم المقيم"
        '
        'chkExact
        '
        Me.chkExact.AutoSize = True
        Me.chkExact.BackColor = System.Drawing.Color.Transparent
        Me.chkExact.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkExact.Location = New System.Drawing.Point(159, 58)
        Me.chkExact.Name = "chkExact"
        Me.chkExact.Size = New System.Drawing.Size(115, 21)
        Me.chkExact.TabIndex = 1
        Me.chkExact.Text = "نفس الاسم بالضبط"
        Me.chkExact.UseVisualStyleBackColor = False
        '
        'grbDate
        '
        Me.grbDate.Controls.Add(Me.chkRange)
        Me.grbDate.Controls.Add(Me.chkDates)
        Me.grbDate.Controls.Add(Me.txtExpiryE)
        Me.grbDate.Controls.Add(Me.txtExpiryS)
        Me.grbDate.Controls.Add(Me.lblTo)
        Me.grbDate.Controls.Add(Me.lblExpiry)
        Me.grbDate.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grbDate.Location = New System.Drawing.Point(12, 97)
        Me.grbDate.Name = "grbDate"
        Me.grbDate.Size = New System.Drawing.Size(412, 66)
        Me.grbDate.TabIndex = 1
        Me.grbDate.TabStop = False
        Me.grbDate.Text = "   تاريخ انتهاء الإقامة"
        '
        'chkRange
        '
        Me.chkRange.AutoSize = True
        Me.chkRange.Enabled = False
        Me.chkRange.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRange.Location = New System.Drawing.Point(0, 2)
        Me.chkRange.Name = "chkRange"
        Me.chkRange.Size = New System.Drawing.Size(48, 21)
        Me.chkRange.TabIndex = 2
        Me.chkRange.Text = "مدى"
        Me.chkRange.UseVisualStyleBackColor = True
        '
        'chkDates
        '
        Me.chkDates.AutoSize = True
        Me.chkDates.Font = New System.Drawing.Font("Times New Roman", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDates.Location = New System.Drawing.Point(389, 4)
        Me.chkDates.Name = "chkDates"
        Me.chkDates.Size = New System.Drawing.Size(15, 14)
        Me.chkDates.TabIndex = 2
        Me.chkDates.UseVisualStyleBackColor = True
        '
        'txtExpiryE
        '
        Me.txtExpiryE.CustomFormat = "MM / yyyy"
        Me.txtExpiryE.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtExpiryE.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtExpiryE.Location = New System.Drawing.Point(24, 24)
        Me.txtExpiryE.MaxDate = New Date(3999, 12, 31, 0, 0, 0, 0)
        Me.txtExpiryE.MinDate = New Date(2000, 1, 1, 0, 0, 0, 0)
        Me.txtExpiryE.Name = "txtExpiryE"
        Me.txtExpiryE.Size = New System.Drawing.Size(144, 29)
        Me.txtExpiryE.TabIndex = 4
        Me.txtExpiryE.Visible = False
        '
        'lblTo
        '
        Me.lblTo.AutoSize = True
        Me.lblTo.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTo.Location = New System.Drawing.Point(174, 30)
        Me.lblTo.Name = "lblTo"
        Me.lblTo.Size = New System.Drawing.Size(29, 21)
        Me.lblTo.TabIndex = 3
        Me.lblTo.Text = "إلي"
        Me.lblTo.Visible = False
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtNumber)
        Me.GroupBox1.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(125, 78)
        Me.GroupBox1.TabIndex = 2
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "رقم الإقامة"
        '
        'txtNumber
        '
        Me.txtNumber.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtNumber.Font = New System.Drawing.Font("Times New Roman", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNumber.Location = New System.Drawing.Point(6, 28)
        Me.txtNumber.MaxLength = 10
        Me.txtNumber.Name = "txtNumber"
        Me.txtNumber.Size = New System.Drawing.Size(113, 29)
        Me.txtNumber.TabIndex = 0
        '
        'frmSearch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(435, 212)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.grbDate)
        Me.Controls.Add(Me.grbName)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.btnCancel)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSearch"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.Yes
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "بحث"
        Me.TopMost = True
        Me.grbName.ResumeLayout(False)
        Me.grbName.PerformLayout()
        Me.grbDate.ResumeLayout(False)
        Me.grbDate.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtExpiryS As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtName As System.Windows.Forms.TextBox
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents lblExpiry As System.Windows.Forms.Label
    Friend WithEvents grbName As System.Windows.Forms.GroupBox
    Friend WithEvents chkExact As System.Windows.Forms.CheckBox
    Friend WithEvents grbDate As System.Windows.Forms.GroupBox
    Friend WithEvents chkDates As System.Windows.Forms.CheckBox
    Friend WithEvents chkRange As System.Windows.Forms.CheckBox
    Friend WithEvents txtExpiryE As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblTo As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtNumber As System.Windows.Forms.TextBox

End Class
