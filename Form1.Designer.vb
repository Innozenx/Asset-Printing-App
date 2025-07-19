<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class Form1
#Region "Windows Form 設計工具產生的程式碼 "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'此為 Windows Form 設計工具所需的呼叫。
		InitializeComponent()
	End Sub
	'Form 覆寫 Dispose 以清除元件清單。
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'為 Windows Form 設計工具的必要項
	Private components As System.ComponentModel.IContainer
	Public ToolTip1 As System.Windows.Forms.ToolTip
	Public WithEvents printButton As System.Windows.Forms.Button
	'注意: 以下為 Windows Form 設計工具所需的程序
	'可以使用 Windows Form 設計工具進行修改。
	'請不要使用程式碼編輯器進行修改。
	<System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.printButton = New System.Windows.Forms.Button()
        Me.statusLabel = New System.Windows.Forms.Label()
        Me.printoutLabel2 = New System.Windows.Forms.Label()
        Me.printoutLabel1 = New System.Windows.Forms.Label()
        Me.asOf = New System.Windows.Forms.Label()
        Me.timeStamp = New System.Windows.Forms.Label()
        Me.isConnected = New System.Windows.Forms.Label()
        Me.printerStatusLabel = New System.Windows.Forms.Label()
        Me.isPrinterConnected = New System.Windows.Forms.Label()
        Me.refreshButton = New System.Windows.Forms.Button()
        Me.initButton = New System.Windows.Forms.Button()
        Me.printInventoriable = New System.Windows.Forms.Button()
        Me.printoutLabel3 = New System.Windows.Forms.Label()
        Me.printoutLabel4 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.buildVersion = New System.Windows.Forms.Label()
        Me.selective_print = New System.Windows.Forms.Button()
        Me.priority = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'printButton
        '
        Me.printButton.BackColor = System.Drawing.SystemColors.Control
        Me.printButton.Cursor = System.Windows.Forms.Cursors.Default
        Me.printButton.ForeColor = System.Drawing.SystemColors.ControlText
        Me.printButton.Location = New System.Drawing.Point(145, 139)
        Me.printButton.Name = "printButton"
        Me.printButton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.printButton.Size = New System.Drawing.Size(169, 42)
        Me.printButton.TabIndex = 0
        Me.printButton.Text = "Print Masterlist Tags"
        Me.printButton.UseVisualStyleBackColor = False
        '
        'statusLabel
        '
        Me.statusLabel.AutoSize = True
        Me.statusLabel.Location = New System.Drawing.Point(238, 36)
        Me.statusLabel.Name = "statusLabel"
        Me.statusLabel.Size = New System.Drawing.Size(115, 13)
        Me.statusLabel.TabIndex = 1
        Me.statusLabel.Text = "DB Connection Status:"
        '
        'printoutLabel2
        '
        Me.printoutLabel2.AutoSize = True
        Me.printoutLabel2.Location = New System.Drawing.Point(187, 70)
        Me.printoutLabel2.Name = "printoutLabel2"
        Me.printoutLabel2.Size = New System.Drawing.Size(127, 13)
        Me.printoutLabel2.TabIndex = 3
        Me.printoutLabel2.Text = "PENDING PRINTOUT/S"
        '
        'printoutLabel1
        '
        Me.printoutLabel1.AutoSize = True
        Me.printoutLabel1.Location = New System.Drawing.Point(171, 70)
        Me.printoutLabel1.Name = "printoutLabel1"
        Me.printoutLabel1.Size = New System.Drawing.Size(10, 13)
        Me.printoutLabel1.TabIndex = 4
        Me.printoutLabel1.Text = "-"
        '
        'asOf
        '
        Me.asOf.AutoSize = True
        Me.asOf.Location = New System.Drawing.Point(270, 114)
        Me.asOf.Name = "asOf"
        Me.asOf.Size = New System.Drawing.Size(36, 13)
        Me.asOf.TabIndex = 5
        Me.asOf.Text = "As Of:"
        '
        'timeStamp
        '
        Me.timeStamp.AutoSize = True
        Me.timeStamp.Location = New System.Drawing.Point(312, 114)
        Me.timeStamp.Name = "timeStamp"
        Me.timeStamp.Size = New System.Drawing.Size(31, 13)
        Me.timeStamp.TabIndex = 6
        Me.timeStamp.Text = "--:--:--"
        '
        'isConnected
        '
        Me.isConnected.AutoSize = True
        Me.isConnected.Location = New System.Drawing.Point(359, 36)
        Me.isConnected.Name = "isConnected"
        Me.isConnected.Size = New System.Drawing.Size(43, 13)
        Me.isConnected.TabIndex = 7
        Me.isConnected.Text = "------------"
        '
        'printerStatusLabel
        '
        Me.printerStatusLabel.AutoSize = True
        Me.printerStatusLabel.Location = New System.Drawing.Point(223, 14)
        Me.printerStatusLabel.Name = "printerStatusLabel"
        Me.printerStatusLabel.Size = New System.Drawing.Size(130, 13)
        Me.printerStatusLabel.TabIndex = 8
        Me.printerStatusLabel.Text = "Printer Connection Status:"
        '
        'isPrinterConnected
        '
        Me.isPrinterConnected.AutoSize = True
        Me.isPrinterConnected.Location = New System.Drawing.Point(359, 14)
        Me.isPrinterConnected.Name = "isPrinterConnected"
        Me.isPrinterConnected.Size = New System.Drawing.Size(43, 13)
        Me.isPrinterConnected.TabIndex = 9
        Me.isPrinterConnected.Text = "------------"
        '
        'refreshButton
        '
        Me.refreshButton.BackColor = System.Drawing.SystemColors.ActiveBorder
        Me.refreshButton.BackgroundImage = CType(resources.GetObject("refreshButton.BackgroundImage"), System.Drawing.Image)
        Me.refreshButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.refreshButton.Cursor = System.Windows.Forms.Cursors.Hand
        Me.refreshButton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.refreshButton.Location = New System.Drawing.Point(12, 168)
        Me.refreshButton.Name = "refreshButton"
        Me.refreshButton.Size = New System.Drawing.Size(33, 26)
        Me.refreshButton.TabIndex = 10
        Me.refreshButton.UseVisualStyleBackColor = False
        '
        'initButton
        '
        Me.initButton.BackColor = System.Drawing.Color.LightCoral
        Me.initButton.Location = New System.Drawing.Point(51, 168)
        Me.initButton.Name = "initButton"
        Me.initButton.Size = New System.Drawing.Size(56, 26)
        Me.initButton.TabIndex = 11
        Me.initButton.Text = "Initialize"
        Me.initButton.UseVisualStyleBackColor = False
        '
        'printInventoriable
        '
        Me.printInventoriable.BackColor = System.Drawing.SystemColors.Control
        Me.printInventoriable.Cursor = System.Windows.Forms.Cursors.Default
        Me.printInventoriable.ForeColor = System.Drawing.SystemColors.ControlText
        Me.printInventoriable.Location = New System.Drawing.Point(362, 139)
        Me.printInventoriable.Name = "printInventoriable"
        Me.printInventoriable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.printInventoriable.Size = New System.Drawing.Size(169, 42)
        Me.printInventoriable.TabIndex = 12
        Me.printInventoriable.Text = "Print Inventoriable Tags"
        Me.printInventoriable.UseVisualStyleBackColor = False
        '
        'printoutLabel3
        '
        Me.printoutLabel3.AutoSize = True
        Me.printoutLabel3.Location = New System.Drawing.Point(388, 70)
        Me.printoutLabel3.Name = "printoutLabel3"
        Me.printoutLabel3.Size = New System.Drawing.Size(10, 13)
        Me.printoutLabel3.TabIndex = 14
        Me.printoutLabel3.Text = "-"
        '
        'printoutLabel4
        '
        Me.printoutLabel4.AutoSize = True
        Me.printoutLabel4.Location = New System.Drawing.Point(404, 70)
        Me.printoutLabel4.Name = "printoutLabel4"
        Me.printoutLabel4.Size = New System.Drawing.Size(127, 13)
        Me.printoutLabel4.TabIndex = 13
        Me.printoutLabel4.Text = "PENDING PRINTOUT/S"
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(414, 92)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 13)
        Me.Label1.TabIndex = 18
        Me.Label1.Text = "For Inventoriables"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(204, 92)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(69, 13)
        Me.Label2.TabIndex = 19
        Me.Label2.Text = "For Masterlist"
        '
        'buildVersion
        '
        Me.buildVersion.AutoSize = True
        Me.buildVersion.Location = New System.Drawing.Point(12, 9)
        Me.buildVersion.Name = "buildVersion"
        Me.buildVersion.Size = New System.Drawing.Size(68, 13)
        Me.buildVersion.TabIndex = 20
        Me.buildVersion.Text = "Build Version"
        '
        'selective_print
        '
        Me.selective_print.Location = New System.Drawing.Point(12, 139)
        Me.selective_print.Name = "selective_print"
        Me.selective_print.Size = New System.Drawing.Size(95, 23)
        Me.selective_print.TabIndex = 21
        Me.selective_print.Text = "Print Selected"
        Me.selective_print.UseVisualStyleBackColor = True
        '
        'priority
        '
        Me.priority.AutoSize = True
        Me.priority.Location = New System.Drawing.Point(12, 114)
        Me.priority.Name = "priority"
        Me.priority.Size = New System.Drawing.Size(10, 13)
        Me.priority.TabIndex = 22
        Me.priority.Text = "-"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(28, 114)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(71, 13)
        Me.Label3.TabIndex = 23
        Me.Label3.Text = "Priority Item/s"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(610, 203)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.priority)
        Me.Controls.Add(Me.selective_print)
        Me.Controls.Add(Me.buildVersion)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.printoutLabel3)
        Me.Controls.Add(Me.printoutLabel4)
        Me.Controls.Add(Me.printInventoriable)
        Me.Controls.Add(Me.initButton)
        Me.Controls.Add(Me.refreshButton)
        Me.Controls.Add(Me.isPrinterConnected)
        Me.Controls.Add(Me.printerStatusLabel)
        Me.Controls.Add(Me.isConnected)
        Me.Controls.Add(Me.timeStamp)
        Me.Controls.Add(Me.asOf)
        Me.Controls.Add(Me.printoutLabel1)
        Me.Controls.Add(Me.printoutLabel2)
        Me.Controls.Add(Me.statusLabel)
        Me.Controls.Add(Me.printButton)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Location = New System.Drawing.Point(960, 540)
        Me.MaximizeBox = False
        Me.Name = "Form1"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Text = "Asset Tag Print"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents statusLabel As Label
    Friend WithEvents printoutLabel2 As Label
    Friend WithEvents printoutLabel1 As Label
    Friend WithEvents asOf As Label
    Friend WithEvents timeStamp As Label
    Friend WithEvents isConnected As Label
    Friend WithEvents printerStatusLabel As Label
    Friend WithEvents isPrinterConnected As Label
    Friend WithEvents refreshButton As Button
    Friend WithEvents initButton As Button
    Public WithEvents printInventoriable As Button
    Friend WithEvents printoutLabel3 As Label
    Friend WithEvents printoutLabel4 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents buildVersion As Label
    Friend WithEvents selective_print As Button
    Friend WithEvents priority As Label
    Friend WithEvents Label3 As Label
#End Region
End Class