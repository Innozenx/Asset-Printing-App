Option Strict Off
Option Explicit On
Imports System.Management
Imports System.Data.SqlClient
Imports System.Reflection

Friend Class Form1
    Inherits System.Windows.Forms.Form
    'Declaration of Private Subroutine
    Private Declare Sub about Lib "tsclib.dll" ()
    Private Declare Sub openport Lib "tsclib.dll" (ByVal PrinterName As String)
    Private Declare Sub closeport Lib "tsclib.dll" ()
    Private Declare Sub sendcommand Lib "tsclib.dll" (ByVal command_Renamed As String)
    Private Declare Sub setup Lib "tsclib.dll" (ByVal LabelWidth As String, ByVal LabelHeight As String, ByVal Speed As String, ByVal Density As String, ByVal Sensor As String, ByVal Vertical As String, ByVal Offset As String)
    Private Declare Sub downloadpcx Lib "tsclib.dll" (ByVal Filename As String, ByVal ImageName As String)
    Private Declare Sub barcode Lib "tsclib.dll" (ByVal X As String, ByVal Y As String, ByVal CodeType As String, ByVal Height_Renamed As String, ByVal Readable As String, ByVal rotation As String, ByVal Narrow As String, ByVal Wide As String, ByVal Code As String)
    Private Declare Sub printerfont Lib "tsclib.dll" (ByVal X As String, ByVal Y As String, ByVal FontName As String, ByVal rotation As String, ByVal Xmul As String, ByVal Ymul As String, ByVal Content As String)
    Private Declare Sub clearbuffer Lib "tsclib.dll" ()
    Private Declare Sub printlabel Lib "tsclib.dll" (ByVal NumberOfSet As String, ByVal NumberOfCopy As String)
    Private Declare Sub formfeed Lib "tsclib.dll" ()
    Private Declare Sub nobackfeed Lib "tsclib.dll" ()
    Private Declare Sub windowsfont Lib "tsclib.dll" (ByVal X As Short, ByVal Y As Short, ByVal fontheight_Renamed As Short, ByVal rotation As Short, ByVal fontstyle As Short, ByVal fontunderline As Short, ByVal FaceName As String, ByVal TextContent As String)
    Private Declare Sub windowsfontUnicode Lib "tsclib.dll" (ByVal X As Short, ByVal Y As Short, ByVal fontheight_Renamed As Short, ByVal rotation As Short, ByVal fontstyle As Short, ByVal fontunderline As Short, ByVal FaceName As String, ByVal TextContent As Byte())
    Private Declare Sub sendBinaryData Lib "tsclib.dll" (ByVal TextContent As Byte(), ByVal length As Integer)
    Private Declare Function usbportqueryprinter Lib "tsclib.dll" () As Byte
    Private connection As SqlConnection
    Private command, commandPropertyHistory, commandBase As SqlCommand
    Private commandUpdate As SqlCommand
    Private reader, readerPropertyHistory, baseReader As SqlDataReader
    Private results As String
    Private resultForPrinting As Boolean
    Private resultID As Integer
    Private s As Object
    Private e As EventArgs
    Private myPrinterStatus As String
    Private WithEvents timer As New System.Windows.Forms.Timer

    Private Sub Command1_Click(ByVal eventSender As System.Object, ByVal eventArgs As System.EventArgs) Handles printButton.Click

        Dim B1 As String = "20080101"
        Dim WT1 As String = "TSC Printers"
        Dim status As Byte = 0
        Dim result_unicode() As Byte = System.Text.Encoding.GetEncoding(1200).GetBytes("TEST UNICODE")
        Dim result_utf8() As Byte = System.Text.Encoding.UTF8.GetBytes("TEXT 40,620,""ARIAL.TTF"",0,12,12,""utf8 test Wörter auf Deutsch""")

        Dim assetCode, assetId, brand, category, uom, dateAcquired, location, assignedTo, serialNo As String
        Dim masterId, qty, propertyHistoryId, cntr As Integer

        'DB DATA VARIABLE DECLARATION


        'Call about()
        'status = usbportqueryprinter() '0 = idle, 1 = head open, 16 = pause, following <ESC>!? command of TSPL manual
        Call openport("TSC TTP-244CE")
        'Call sendcommand("SIZE 2.5, 1.5")
        'Call sendcommand("SPEED 4")
        'Call sendcommand("DENSITY 12")
        'Call sendcommand("DIRECTION 1")
        ''Call sendcommand("SET TEAR ON")
        'Call sendcommand("CODEPAGE UTF-8")
        ''Call downloadpcx(Application.StartupPath & "\UL.PCX", "UL.PCX")
        ''Call downloadpcx(Application.StartupPath & "\MY_SAMPLE.PCX", "MY_SAMPLE.PCX")
        'Call windowsfont(40, 490, 48, 0, 0, 0, "Arial", "Windows Font Test")
        'Call windowsfontUnicode(40, 550, 48, 0, 0, 0, "Arial", result_unicode)
        'Call sendcommand("PUTPCX 40,40,""MY_SAMPLE.PCX""")
        'Call sendBinaryData(result_utf8, result_utf8.Length)
        'Call barcode("40", "300", "128", "80", "1", "0", "2", "2", B1)
        'Call printerfont("40", "440", "0", "0", "15", "15", WT1)
        'Call printlabel("1", "1")
        'Call closeport()

        'augmented content setup

        'Call windowsfont(10, 10, 80, 0, 2, 0, "arial", "Arial font 80 pt")
        'Call sendcommand("PUTPCX 150, 150, ""my_sampl.PCX""")
        'Call sendcommand("QRCODE 150, 150, M, 5, M, 0, M1, S2, ""N123456""")
        'Call sendcommand("TEXT 10,10, ""3"", 0,1,1, ""@YEAR""")
        'Call barcode("40", "40", "128", "80", "1", "0", "2", "2", "TESTPRINT")
        'Call printlabel("1", "1")

        commandUpdate = connection.CreateCommand
        command = connection.CreateCommand
        command.CommandText = "SELECT * From AS_MasterList WHERE forPrinting = 'true' AND Removed_Status IS NULL"

        commandPropertyHistory = connection.CreateCommand
        commandPropertyHistory.CommandText = "WITH CTE (Asset_Code, DateAcquired, Category, Brand, Uom, Qty, forPrinting, PropertyHistoryId, Location, MasterId, Name, SerialNo, rn)
                                                AS(
                                                    SELECT AS_MasterList.Asset_Code, AS_MasterList.DateAcquired, AS_MasterList.Category, AS_MasterList.Brand, AS_MasterList.Uom, AS_MasterList.Qty, AS_MasterList.forPrinting, AS_PropertyHistory.Id, AS_PropertyHistory.Location, AS_PropertyHistory.MasterId, AS_AlphaList.Name, AS_Masterlist.SerialNo,
                                                    ROW_NUMBER() OVER (PARTITION BY AS_PropertyHistory.MasterId ORDER BY AS_PropertyHistory.Id DESC) AS rn
                                                    FROM AS_MasterList    
                                                    JOIN AS_PropertyHistory ON AS_MasterList.Id=AS_PropertyHistory.MasterId AND AS_MasterList.forPrinting='true' AND (AS_MasterList.Removed_Status IS NULL OR AS_MasterList.Removed_Status = 'false')
                                                    JOIN AS_AlphaList ON AS_PropertyHistory.Assisned_To=AS_AlphaList.id_number
                                                    )
                                              SELECT * FROM CTE
                                              WHERE CTE.rn = 1
                                              ORDER BY PropertyHistoryId DESC
                                              "

        reader = command.ExecuteReader()
        readerPropertyHistory = commandPropertyHistory.ExecuteReader()

        If Not readerPropertyHistory.HasRows Then
            MsgBox("empty")

        Else
            Do While readerPropertyHistory.Read()

                assetCode = readerPropertyHistory.GetString(0)
                'dateAcquired = readerPropertyHistory.GetDateTime(1).ToShortDateString()
                dateAcquired = If(IsDBNull(readerPropertyHistory(1)), "N/A", readerPropertyHistory.GetDateTime(1).ToShortDateString())
                category = If(IsDBNull(readerPropertyHistory(2)), "N/A", readerPropertyHistory.GetString(2))
                brand = If(IsDBNull(readerPropertyHistory(3)), "N/A", readerPropertyHistory.GetString(3))
                uom = If(IsDBNull(readerPropertyHistory(4)), "N/A", readerPropertyHistory.GetString(4))
                qty = If(IsDBNull(readerPropertyHistory(5)), 0, readerPropertyHistory.GetInt32(5))
                propertyHistoryId = If(IsDBNull(readerPropertyHistory(7)), 0, readerPropertyHistory.GetInt32(7))
                location = If(IsDBNull(readerPropertyHistory(8)), "N/A", readerPropertyHistory.GetString(8))
                assignedTo = If(IsDBNull(readerPropertyHistory(10)), "N/A", readerPropertyHistory.GetString(10))
                serialNo = If(IsDBNull(readerPropertyHistory(11)), "N/A", readerPropertyHistory.GetString(11))

                commandUpdate.CommandText = "UPDATE AS_MasterList SET forPrinting = 'false' WHERE Asset_Code = '" & assetCode & "'"
                commandUpdate.ExecuteNonQuery()

                Call clearbuffer()

                If category = "Laptop" Then
                    For cntr = 1 To qty
                        Call clearbuffer()

                        Call windowsfont(30, 10, 38, 0, 2, 0, "Arial", "ENCHANTED KINGDOM INC.")
                        Call printerfont("20", "65", "2", "0", "1", "1", "Assignee: ")
                        Call printerfont("20", "85", "2", "0", "1", "1", "" + assignedTo)
                        Call printerfont("20", "115", "2", "0", "1", "1", "Date Acquired: " + dateAcquired)
                        Call printerfont("20", "145", "2", "0", "1", "1", "Location: " + location)
                        Call printerfont("20", "175", "2", "0", "1", "1", "Asset Code: " + assetCode)
                        Call printerfont("20", "205", "2", "0", "1", "1", "SN:" + serialNo)
                        Call printerfont("20", "255", "2", "0", "1", "1", "" + brand + category + qty.ToString + uom)
                        Call sendcommand("QRCODE 370, 160, L, 6, M, 0, """ + assetCode + """")

                        printlabel(1, 1)

                        Call clearbuffer()

                        Call windowsfont(30, 10, 38, 0, 2, 0, "Arial", "ENCHANTED KINGDOM INC.")
                        Call printerfont("20", "65", "2", "0", "1", "1", "Assignee: ")
                        Call printerfont("20", "85", "2", "0", "1", "1", "" + assignedTo)
                        Call printerfont("20", "115", "2", "0", "1", "1", "Date Acquired: " + dateAcquired)
                        Call printerfont("20", "145", "2", "0", "1", "1", "Location: " + location)
                        Call printerfont("20", "175", "2", "0", "1", "1", "Asset Code: " + assetCode)
                        Call printerfont("20", "205", "2", "0", "1", "1", "SN:" + serialNo)
                        Call printerfont("20", "255", "2", "0", "1", "1", "" + brand + category + qty.ToString + uom)
                        Call sendcommand("QRCODE 370, 160, L, 6, M, 0, """ + assetCode + """")

                        printlabel(1, 1)

                    Next


                Else
                    For cntr = 1 To qty
                        Call clearbuffer()

                        Call windowsfont(30, 10, 38, 0, 2, 0, "Arial", "ENCHANTED KINGDOM INC.")
                        Call printerfont("20", "65", "2", "0", "1", "1", "Assignee: ")
                        Call printerfont("20", "85", "2", "0", "1", "1", "" + assignedTo)
                        Call printerfont("20", "115", "2", "0", "1", "1", "Date Acquired: " + dateAcquired)
                        Call printerfont("20", "145", "2", "0", "1", "1", "Location: " + location)
                        Call printerfont("20", "175", "2", "0", "1", "1", "Asset Code: " + assetCode)
                        Call printerfont("20", "205", "2", "0", "1", "1", "SN:" + serialNo)
                        Call printerfont("20", "255", "2", "0", "1", "1", "" + brand + category + qty.ToString + uom)
                        Call sendcommand("QRCODE 370, 160, L, 6, M, 0, """ + assetCode + """")

                        printlabel(1, 1)
                    Next


                End If

            Loop

            'Do While reader.Read()
            '    MsgBox(reader.GetString(5))
            '    resultID = reader.GetInt32(0)

            '    assetCode = reader.GetString(1)
            '    assetId = reader.GetString(5)
            '    brand = reader.GetString(11)
            '    category = reader.GetString(9)
            '    uom = reader.GetString(12)

            '    'commandUpdate.CommandText = "UPDATE AS_MasterList SET forPrinting = 'false' WHERE id = '" & resultID & "'"
            '    'commandUpdate.ExecuteNonQuery()

            '    Call clearbuffer()

            '    Call printerfont("20", "35", "3", "0", "1", "1", "EKI ASSET INVENTORY TAG")
            '    Call printerfont("20", "65", "3", "0", "1", "1", "Asset Code: " + assetCode)
            '    Call printerfont("20", "185", "3", "0", "1", "1", "Date Acquired: " + uom)
            '    Call printerfont("20", "125", "3", "0", "1", "1", "Brand: " + brand)
            '    Call printerfont("20", "155", "3", "0", "1", "1", "Category: " + category)
            '    Call printerfont("20", "185", "3", "0", "1", "1", "uom: " + uom)
            '    Call sendcommand("QRCODE 325, 150, L, 7, M, 0, """ + assetCode + """")

            '    printlabel(1, 1)

            'Loop

            reader.Close()
            readerPropertyHistory.Close()
            connection.Close()

            reloadForm()

        End If



    End Sub

    Private Sub reloadForm()

        'timer.Stop()
        Application.Restart()
        Form1_Load(s, e)
        InitializeComponent()


    End Sub
    Private Enum PrinterStatus As UShort
        PrinterOther = 1
        PrinterUnknown = 2
        PrinterIdle = 3
        PrinterPrinting = 4
        PrinterWarmingUp = 5
    End Enum

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles Me.Load

        Me.CenterToScreen()
        Dim versionNumber As Version

        versionNumber = Assembly.GetExecutingAssembly().GetName().Version
        buildVersion.Text = versionNumber.ToString

        'timer.Interval = 60000 / 4

        'AddHandler timer.Tick, (AddressOf elapsedEvent)
        'timer.Enabled = True
        'timer.Start()

        Try

            'connection = New SqlConnection("Server=192.168.10.220;Database=AssetManagementDB;MultipleActiveResultSets=True;User=sa;Pwd=p@$$w0rd")
            connection = New SqlConnection("Server=192.168.10.206;Database=EK_ASSETMANAGEMENT;MultipleActiveResultSets=True;User=ekportaluser;Pwd=ekportalpass")
            connection.Open()

        Catch ex As Exception
            Debug.WriteLine(ex)

        End Try

        '---------------------------PRINTER STATUS----------------------------------------

        Try
            Dim searcher As ManagementObjectSearcher
            'get all printers
            searcher = New ManagementObjectSearcher("root\CIMV2",
                                                    "SELECT * FROM Win32_Printer")

            'show their status
            Dim sb As New System.Text.StringBuilder
            For Each printObj As ManagementObject In searcher.Get()
                If (printObj("Name") = "TSC TTP-244CE") Then
                    sb.AppendFormat("Name: {0}  Status: {1}", printObj("Name"), DirectCast(printObj("PrinterStatus"), PrinterStatus))
                    sb.AppendLine()

                    If Not (printObj("PrinterStatus") = 1) And Not (printObj("PrinterStatus") = 2) Then
                        Try
                            Call openport("TSC TTP-244CE")
                            Call closeport
                            isPrinterConnected.Text = "CONNECTED"
                            isPrinterConnected.Enabled = False
                            myPrinterStatus = "CONNECTED"

                        Catch ex As Exception

                        End Try

                    Else
                        myPrinterStatus = "NOT CONNECTED"
                    End If
                End If
            Next

            ''https://docs.microsoft.com/en-us/windows/desktop/CIMWin32Prov/win32-printer
            'sb.AppendLine()
            'sb.AppendLine("Properties")
            'For Each printObj As ManagementObject In searcher.Get()
            '    sb.AppendFormat("Name: {0}", printObj("Name"))
            '    sb.AppendLine()
            '    For Each prop As PropertyData In printObj.Properties
            '        sb.AppendFormat("printObj({0}): {1}", prop.Name, prop.Value)
            '        sb.AppendLine()
            '    Next
            '    sb.AppendLine()
            'Next
            'RichTextBox1.Text = sb.ToString


        Catch err As ManagementException
            MessageBox.Show("An error occurred while querying for WMI data: " & err.Message)
        End Try

        '----------------------------------END PRINTER STATUS----------------------------------------

        If (connection.State = ConnectionState.Open) And (myPrinterStatus = "CONNECTED") Then
            isConnected.Text = "CONNECTED"
            isConnected.Enabled = False
            isConnected.ForeColor = Color.Green
            isPrinterConnected.ForeColor = Color.Green

            printoutLabel1.Text = GetPending(command, connection, reader)
            printoutLabel1.ForeColor = Color.Red

            printoutLabel3.Text = GetPendingInventoriable(command, connection, reader)
            printoutLabel3.ForeColor = Color.Red

            priority.Text = GetPendingPriority(command, connection, reader)
            priority.ForeColor = Color.Red

        ElseIf (connection.State = ConnectionState.Open) And Not (myPrinterStatus = "CONNECTED") Then
            isConnected.Text = "CONNECTED"
            isConnected.Enabled = False
            isPrinterConnected.Text = "NOT CONNECTED"
            isPrinterConnected.ForeColor = Color.Red
            isPrinterConnected.Enabled = False

            printoutLabel1.Text = GetPending(command, connection, reader)
            printoutLabel1.ForeColor = Color.Red

            printoutLabel3.Text = GetPendingInventoriable(command, connection, reader)
            printoutLabel3.ForeColor = Color.Red

            priority.Text = GetPendingPriority(command, connection, reader)
            priority.ForeColor = Color.Red

            printButton.Enabled = True
            printInventoriable.Enabled = True

        Else
            isConnected.Text = "NOT CONNECTED"
            isConnected.ForeColor = Color.Red
            printButton.Enabled = False
            isConnected.Enabled = False

            printoutLabel1.Text = "NO"
            printoutLabel1.ForeColor = Color.Green

        End If

        timeStamp.Text = DateTime.Now

        'connection.Close()
    End Sub

    Private Sub elapsedEvent()
        'timer.Stop()
        Application.Restart()
        'Me.Dispose()
        'InitializeComponent()
        'Form1_Load(s, e)
    End Sub

    Private Sub Form1_Closed(sender As Object, e As EventArgs) Handles Me.Closed
        connection.Close()
        timer.Stop()
    End Sub

    Private Sub refreshButton_Click(sender As Object, e As EventArgs) Handles refreshButton.Click
        timer.Stop()
        reloadForm()
    End Sub

    Private Sub selective_print_Click(sender As Object, e As EventArgs) Handles selective_print.Click
        Dim B1 As String = "20080101"
        Dim WT1 As String = "TSC Printers"
        Dim status As Byte = 0
        Dim result_unicode() As Byte = System.Text.Encoding.GetEncoding(1200).GetBytes("TEST UNICODE")
        Dim result_utf8() As Byte = System.Text.Encoding.UTF8.GetBytes("TEXT 40,620,""ARIAL.TTF"",0,12,12,""utf8 test Wörter auf Deutsch""")

        Dim assetCode, assetId, brand, category, uom, dateAcquired, location, assignedTo, serialNo As String
        Dim masterId, qty, propertyHistoryId As Integer

        'DB DATA VARIABLE DECLARATION


        'Call about()
        'status = usbportqueryprinter() '0 = idle, 1 = head open, 16 = pause, following <ESC>!? command of TSPL manual
        Call openport("TSC TTP-244CE")
        'Call sendcommand("SIZE 2.5, 1.5")
        'Call sendcommand("SPEED 4")
        'Call sendcommand("DENSITY 12")
        'Call sendcommand("DIRECTION 1")
        ''Call sendcommand("SET TEAR ON")
        'Call sendcommand("CODEPAGE UTF-8")
        ''Call downloadpcx(Application.StartupPath & "\UL.PCX", "UL.PCX")
        ''Call downloadpcx(Application.StartupPath & "\MY_SAMPLE.PCX", "MY_SAMPLE.PCX")
        'Call windowsfont(40, 490, 48, 0, 0, 0, "Arial", "Windows Font Test")
        'Call windowsfontUnicode(40, 550, 48, 0, 0, 0, "Arial", result_unicode)
        'Call sendcommand("PUTPCX 40,40,""MY_SAMPLE.PCX""")
        'Call sendBinaryData(result_utf8, result_utf8.Length)
        'Call barcode("40", "300", "128", "80", "1", "0", "2", "2", B1)
        'Call printerfont("40", "440", "0", "0", "15", "15", WT1)
        'Call printlabel("1", "1")
        'Call closeport()

        'augmented content setup

        'Call windowsfont(10, 10, 80, 0, 2, 0, "arial", "Arial font 80 pt")
        'Call sendcommand("PUTPCX 150, 150, ""my_sampl.PCX""")
        'Call sendcommand("QRCODE 150, 150, M, 5, M, 0, M1, S2, ""N123456""")
        'Call sendcommand("TEXT 10,10, ""3"", 0,1,1, ""@YEAR""")
        'Call barcode("40", "40", "128", "80", "1", "0", "2", "2", "TESTPRINT")
        'Call printlabel("1", "1")

        commandUpdate = connection.CreateCommand
        command = connection.CreateCommand
        command.CommandText = "SELECT * From AS_MasterList WHERE print_special = 'TRUE'"

        commandPropertyHistory = connection.CreateCommand
        commandPropertyHistory.CommandText = "WITH CTE (Asset_Code, DateAcquired, Category, Brand, Uom, Qty, forPrinting, PropertyHistoryId, Location, MasterId, Name, SerialNo, rn)
                                                AS(
                                                    SELECT AS_MasterList.Asset_Code, AS_MasterList.DateAcquired, AS_MasterList.Category, AS_MasterList.Brand, AS_MasterList.Uom, AS_MasterList.Qty, AS_MasterList.forPrinting, AS_PropertyHistory.Id, AS_PropertyHistory.Location, AS_PropertyHistory.MasterId, AS_AlphaList.Name, AS_Masterlist.SerialNo,
                                                    ROW_NUMBER() OVER (PARTITION BY AS_PropertyHistory.MasterId ORDER BY AS_PropertyHistory.Id DESC) AS rn
                                                    FROM AS_MasterList
                                                    JOIN AS_PropertyHistory ON AS_MasterList.Id=AS_PropertyHistory.MasterId AND AS_MasterList.print_special = 'true'
                                                    JOIN AS_AlphaList ON AS_PropertyHistory.Assisned_To=AS_AlphaList.id_number
                                                    )
                                              SELECT * FROM CTE
                                              WHERE CTE.rn = 1
                                              ORDER BY PropertyHistoryId DESC
                                              "

        reader = command.ExecuteReader()
        readerPropertyHistory = commandPropertyHistory.ExecuteReader()

        If Not readerPropertyHistory.HasRows Then
            MsgBox("empty")

        Else
            Do While readerPropertyHistory.Read()

                assetCode = readerPropertyHistory.GetString(0)
                'dateAcquired = readerPropertyHistory.GetDateTime(1).ToShortDateString()
                dateAcquired = If(IsDBNull(readerPropertyHistory(1)), "N/A", readerPropertyHistory.GetDateTime(1).ToShortDateString())
                category = If(IsDBNull(readerPropertyHistory(2)), "N/A", readerPropertyHistory.GetString(2))
                brand = If(IsDBNull(readerPropertyHistory(3)), "N/A", readerPropertyHistory.GetString(3))
                uom = If(IsDBNull(readerPropertyHistory(4)), "N/A", readerPropertyHistory.GetString(4))
                qty = If(IsDBNull(readerPropertyHistory(5)), 0, readerPropertyHistory.GetInt32(5))
                propertyHistoryId = If(IsDBNull(readerPropertyHistory(7)), 0, readerPropertyHistory.GetInt32(7))
                location = If(IsDBNull(readerPropertyHistory(8)), "N/A", readerPropertyHistory.GetString(8))
                assignedTo = If(IsDBNull(readerPropertyHistory(10)), "N/A", readerPropertyHistory.GetString(10))
                serialNo = If(IsDBNull(readerPropertyHistory(11)), "N/A", readerPropertyHistory.GetString(11))

                commandUpdate.CommandText = "UPDATE AS_MasterList SET print_special = 'false' WHERE Asset_Code = '" & assetCode & "'"
                commandUpdate.ExecuteNonQuery()

                Call clearbuffer()

                If category = "Laptop" Then
                    Call windowsfont(30, 10, 38, 0, 2, 0, "Arial", "ENCHANTED KINGDOM INC.")
                    Call printerfont("20", "65", "2", "0", "1", "1", "Assignee: ")
                    Call printerfont("20", "85", "2", "0", "1", "1", "" + assignedTo)
                    Call printerfont("20", "115", "2", "0", "1", "1", "Date Acquired: " + dateAcquired)
                    Call printerfont("20", "145", "2", "0", "1", "1", "Location: " + location)
                    Call printerfont("20", "175", "2", "0", "1", "1", "Asset Code: " + assetCode)
                    Call printerfont("20", "205", "2", "0", "1", "1", "SN:" + serialNo)
                    Call printerfont("20", "255", "2", "0", "1", "1", "" + brand + category + qty.ToString + uom)
                    Call sendcommand("QRCODE 370, 160, L, 6, M, 0, """ + assetCode + """")

                    printlabel(1, 1)

                    Call clearbuffer()

                    Call windowsfont(30, 10, 38, 0, 2, 0, "Arial", "ENCHANTED KINGDOM INC.")
                    Call printerfont("20", "65", "2", "0", "1", "1", "Assignee: ")
                    Call printerfont("20", "85", "2", "0", "1", "1", "" + assignedTo)
                    Call printerfont("20", "115", "2", "0", "1", "1", "Date Acquired: " + dateAcquired)
                    Call printerfont("20", "145", "2", "0", "1", "1", "Location: " + location)
                    Call printerfont("20", "175", "2", "0", "1", "1", "Asset Code: " + assetCode)
                    Call printerfont("20", "205", "2", "0", "1", "1", "SN:" + serialNo)
                    Call printerfont("20", "255", "2", "0", "1", "1", "" + brand + category + qty.ToString + uom)
                    Call sendcommand("QRCODE 370, 160, L, 6, M, 0, """ + assetCode + """")

                    printlabel(1, 1)

                Else

                    Call windowsfont(30, 10, 38, 0, 2, 0, "Arial", "ENCHANTED KINGDOM INC.")
                    Call printerfont("20", "65", "2", "0", "1", "1", "Assignee: ")
                    Call printerfont("20", "85", "2", "0", "1", "1", "" + assignedTo)
                    Call printerfont("20", "115", "2", "0", "1", "1", "Date Acquired: " + dateAcquired)
                    Call printerfont("20", "145", "2", "0", "1", "1", "Location: " + location)
                    Call printerfont("20", "175", "2", "0", "1", "1", "Asset Code: " + assetCode)
                    Call printerfont("20", "205", "2", "0", "1", "1", "SN:" + serialNo)
                    Call printerfont("20", "255", "2", "0", "1", "1", "" + brand + category + qty.ToString + uom)
                    Call sendcommand("QRCODE 370, 160, L, 6, M, 0, """ + assetCode + """")

                    printlabel(1, 1)

                End If

            Loop

            'Do While reader.Read()
            '    MsgBox(reader.GetString(5))
            '    resultID = reader.GetInt32(0)

            '    assetCode = reader.GetString(1)
            '    assetId = reader.GetString(5)
            '    brand = reader.GetString(11)
            '    category = reader.GetString(9)
            '    uom = reader.GetString(12)

            '    'commandUpdate.CommandText = "UPDATE AS_MasterList SET forPrinting = 'false' WHERE id = '" & resultID & "'"
            '    'commandUpdate.ExecuteNonQuery()

            '    Call clearbuffer()

            '    Call printerfont("20", "35", "3", "0", "1", "1", "EKI ASSET INVENTORY TAG")
            '    Call printerfont("20", "65", "3", "0", "1", "1", "Asset Code: " + assetCode)
            '    Call printerfont("20", "185", "3", "0", "1", "1", "Date Acquired: " + uom)
            '    Call printerfont("20", "125", "3", "0", "1", "1", "Brand: " + brand)
            '    Call printerfont("20", "155", "3", "0", "1", "1", "Category: " + category)
            '    Call printerfont("20", "185", "3", "0", "1", "1", "uom: " + uom)
            '    Call sendcommand("QRCODE 325, 150, L, 7, M, 0, """ + assetCode + """")

            '    printlabel(1, 1)

            'Loop

            reader.Close()
            readerPropertyHistory.Close()
            connection.Close()

            reloadForm()

        End If
    End Sub

    Private Sub initButton_Click(sender As Object, e As EventArgs) Handles initButton.Click
        Dim rerun As Integer

        rerun = 0

        initButton.BackColor = Color.LightGreen

        Do While rerun <= 1
            Call openport("TSC TTP-244CE")
            Call clearbuffer()

            'Call printerfont("20", "35", "3", "0", "1", "1", "ENCHANTED KINGDOM INC.")
            Call windowsfont(30, 35, 38, 0, 2, 0, "Arial", "ENCHANTED KINGDOM INC.")
            Call printerfont("20", "85", "2", "0", "1", "1", "INITIALIZING PRINTER...")
            Call printerfont("20", "115", "2", "0", "1", "1", "DISREGARD THIS STICKER.")
            Call printerfont("20", "145", "2", "0", "1", "1", "FOR ANY CONCERNS,")
            Call printerfont("20", "165", "2", "0", "1", "1", "PLEASE CONTACT @ITS - LOCAL: 132")

            printlabel(1, 1)
            rerun = rerun + 1
        Loop

    End Sub

    Private Sub printInventoriable_Click(sender As Object, e As EventArgs) Handles printInventoriable.Click
        Dim B1 As String = "20080101"
        Dim WT1 As String = "TSC Printers"
        Dim status As Byte = 0
        Dim result_unicode() As Byte = System.Text.Encoding.GetEncoding(1200).GetBytes("TEST UNICODE")
        Dim result_utf8() As Byte = System.Text.Encoding.UTF8.GetBytes("TEXT 40,620,""ARIAL.TTF"",0,12,12,""utf8 test Wörter auf Deutsch""")

        Dim assetCode, brand, category, uom, dateAcquired, assignee As String
        Dim qty, cntr, last_print As Integer

        'DB DATA VARIABLE DECLARATION

        Call openport("TSC TTP-244CE")

        commandUpdate = connection.CreateCommand
        command = connection.CreateCommand
        commandBase = connection.CreateCommand
        'command.CommandText = "SELECT * From AS_Inventoriables WHERE To_Print = 'TRUE' AND Not Removed_Status = 'True' OR To_Print = 'TRUE' AND Removed_Status IS NULL"
        command.CommandText = "WITH CTE (Asset_Code, DateAcquired, Category, Brand, Uom, Qty, ToPrint, PrintFrom, PrintTo, LastPrint, Location, Name, HistoryId, rn)
                                                AS(
                                                    SELECT AS_Inventoriables.Asset_Code, AS_Inventoriables.Date_Acquired, AS_Inventoriables.Category, AS_Inventoriables.Brand, AS_Inventoriables.Uom, AS_Inventoriables.Qty, AS_Inventoriables.To_Print, AS_Inventoriables.Print_From, AS_Inventoriables.Print_To, AS_Inventoriables.Last_Print, AS_InventoriablesHistory.Location, AS_InventoriablesHistory.Assignee, AS_InventoriablesHistory.Id,
                                                    ROW_NUMBER() OVER (PARTITION BY AS_InventoriablesHistory.Asset_Code ORDER BY AS_InventoriablesHistory.Id DESC) AS rn
                                                    FROM AS_Inventoriables
                                                    JOIN AS_InventoriablesHistory ON AS_Inventoriables.Asset_Code=AS_InventoriablesHistory.Asset_Code AND AS_Inventoriables.To_Print='true' AND (AS_Inventoriables.Removed_Status IS NULL OR AS_Inventoriables.Removed_Status = 'false')
                                                    )
                                              SELECT * FROM CTE
                                              WHERE CTE.rn = 1
                                              ORDER BY HistoryId DESC
                                              "
        reader = command.ExecuteReader()

        Do While reader.Read()

            'assetCode = reader.GetString(1)
            'dateAcquired = If(IsDBNull(reader(17)), "N/A", reader.GetDateTime(17).ToShortDateString())
            'category = If(IsDBNull(reader(6)), "N/A", reader.GetString(6))
            'brand = If(IsDBNull(reader(7)), "N/A", reader.GetString(7))
            'uom = If(IsDBNull(reader(10)), "N/A", reader.GetString(10))
            'qty = If(IsDBNull(reader(8)), "N/A", reader.GetInt32(8))
            assetCode = reader.GetString(0)
            dateAcquired = If(IsDBNull(reader(1)), "N/A", reader.GetDateTime(1).ToShortDateString())
            category = If(IsDBNull(reader(2)), "N/A", reader.GetString(2))
            brand = If(IsDBNull(reader(3)), "N/A", reader.GetString(3))
            uom = If(IsDBNull(reader(4)), "N/A", reader.GetString(4))
            qty = If(IsDBNull(reader(5)), "N/A", reader.GetInt32(5))
            assignee = If(IsDBNull(reader(11)), "N/A", reader.GetString(11))
            last_print = If(IsDBNull(reader(9)), 0, reader.GetInt32(9))

            commandBase.CommandText = "SELECT Print_From, Print_To FROM AS_Inventoriables WHERE Asset_Code = '" & assetCode & "'"
            baseReader = commandBase.ExecuteReader()
            baseReader.Read()

            For cntr = baseReader.GetInt32(0) To baseReader.GetInt32(1)
                'status = usbportqueryprinter()

                'If (status <> 0 And (status <> 20 Or status <> 10)) Then
                '    commandUpdate.CommandText = "UPDATE AS_Inventoriables SET Interrupted = 'true', Last_Print = '" & cntr - 1 & "' WHERE Asset_Code = '" & assetCode & "'"
                '    commandUpdate.ExecuteNonQuery()

                '    Exit Do
                'End If

                Call clearbuffer()

                'Call printerfont("20", "35", "3", "0", "1", "1", "ENCHANTED KINGDOM INC.")
                Call windowsfont(30, 35, 38, 0, 2, 0, "Arial", "ENCHANTED KINGDOM INC.")
                Call printerfont("20", "85", "2", "0", "1", "1", "Date Acquired: " + dateAcquired)
                Call printerfont("20", "115", "2", "0", "1", "1", "Asset Code: " + assetCode)
                Call printerfont("20", "135", "2", "0", "1", "1", "#" + cntr.ToString)
                Call printerfont("20", "165", "2", "0", "1", "1", "" + brand + category + uom)
                Call sendcommand("QRCODE 370, 175, L, 6, M, 0, """ + assetCode + """")

                printlabel(1, 1)

                commandUpdate.CommandText = "UPDATE AS_Inventoriables SET Last_Print = '" & cntr & "' WHERE Asset_Code = '" & assetCode & "'"
                commandUpdate.ExecuteNonQuery()

            Next

            commandUpdate.CommandText = "UPDATE AS_Inventoriables SET To_Print = 'false', Print_From = '1', Print_To = '" & qty & "', Interrupted = 'false' WHERE Asset_Code = '" & assetCode & "'"
            commandUpdate.ExecuteNonQuery()
        Loop

        reader.Close()
        connection.Close()

        reloadForm()
    End Sub
End Class

Module myFunctions
    Public Function GetPending(command, connection, reader) As String
        Dim cnt As Integer

        cnt = 0
        command = connection.CreateCommand
        command.CommandText = "SELECT id FROM AS_MasterList WHERE forPrinting = 'TRUE' AND Not Removed_Status = 'TRUE' OR forPrinting = 'TRUE' AND Removed_Status IS NULL"
        reader = command.ExecuteReader()

        While (reader.Read())
            cnt = cnt + 1
        End While

        reader.Close()
        Return cnt

    End Function

    Public Function GetPendingInventoriable(command, connection, reader) As String
        Dim cnt As Integer

        cnt = 0
        command = connection.CreateCommand
        command.CommandText = "SELECT id FROM AS_Inventoriables WHERE To_Print = 'TRUE' AND Not Removed_Status = 'True' OR To_Print = 'TRUE' AND Removed_Status IS NULL"
        reader = command.ExecuteReader()

        While (reader.Read())
            cnt = cnt + 1
        End While

        reader.Close()
        Return cnt

    End Function

    Public Function GetPendingPriority(command, connection, reader) As String
        Dim cnt As Integer

        cnt = 0
        command = connection.CreateCommand
        command.CommandText = "SELECT id FROM AS_MasterList WHERE print_special = 'TRUE' AND Not Removed_Status = 'TRUE' OR print_special = 'TRUE' AND Removed_Status IS NULL"
        reader = command.ExecuteReader()

        While (reader.Read())
            cnt = cnt + 1
        End While

        reader.Close()
        Return cnt

    End Function

End Module