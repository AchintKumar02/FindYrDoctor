Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Report_Form
    Private Sub Home_btn_Click(sender As Object, e As EventArgs) Handles Home_btn.Click
        Me.Hide()
        Main_Form.Show()
        Main_Form.Select()
    End Sub

    Private Sub Report_RecView_btn_Click(sender As Object, e As EventArgs) Handles Report_RecView_btn.Click
        Report_Records_Form.Show()
        Report_Records_Form.Select()
    End Sub

    Private Sub Report_clear_btn_Click(sender As Object, e As EventArgs) Handles Report_clear_btn.Click
        Report_Id_txtbox.Clear()
        Pay_Id_txtbox.Clear()
        Pt_Id_txtbox.Clear()
        Pt_Name_txtbox.Clear()
        Dr_Id_txtbox.Clear()
        Dr_Name_txtbox.Clear()
    End Sub

    '-------------------------------DATABASE-------------------------------------------------------------------------------------------------------------

    Dim conn As New MySqlConnection("Data Source=localhost;database=database_fyd3;userid=root;password=''")
    Dim cmd As MySqlCommand

    Private Sub Report_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim rand As New Random()
        Dim customerId As Integer = rand.Next(100000, 999999)
        Dim formattedId As String = customerId.ToString("D3")


        Report_Id_txtbox.Text = formattedId

        Dim currentDate As Date = Date.Now
        Report_Date.Text = currentDate.ToString("dd-MMM-yy")
        Report_Records_Form.Show()
        LoadData()
    End Sub

    Public Sub LoadData()
        ' Select data from table and load into DataGridView
        Dim query As String = "Select * From report_table"
        Dim da As MySqlDataAdapter = New MySqlDataAdapter(query, conn)
        Dim dt As DataTable = New DataTable()
        da.Fill(dt)
        Report_Records_Form.Report_DataGridView1.DataSource = dt
    End Sub

    Private Sub InsertData()
        ' Insert data into table
        Dim query As String = "INSERT INTO report_table(Report_Id,Payment_Id,Patient_Id,Pt_Name,Doctor_Id,Dr_Name,Report,Date) values(@Report_Id,@Payment_Id,@Patient_Id,@Pt_Name,@Doctor_Id,@Dr_Name,@Report,@Date)"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("Report_Id", Report_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Payment_Id", Pay_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Patient_Id", Pt_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_Name", Pt_Name_txtbox.Text)
        cmd.Parameters.AddWithValue("Doctor_Id", Dr_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Dr_Name", Dr_Name_txtbox.Text)
        cmd.Parameters.AddWithValue("Report", Report_RichTextBox.Text)
        cmd.Parameters.AddWithValue("Date", Report_Date.Text)
        conn.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Data inserted successfully.")
        Else
            MessageBox.Show("Data not inserted.")
        End If
        conn.Close()
        LoadData()
    End Sub

    Private Sub UpdateData()
        ' Update data in table
        Dim query As String = "UPDATE report_table SET Payment_Id=@Payment_Id,Patient_Id=@Patient_Id,Pt_Name=@Pt_Name, Doctor_Id=@Doctor_Id, Dr_Name=@Dr_Name,Report=@Report,Date=@Date WHERE Report_Id=@Report_Id"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("Report_Id", Report_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Payment_Id", Pay_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Patient_Id", Pt_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_Name", Pt_Name_txtbox.Text)
        cmd.Parameters.AddWithValue("Doctor_Id", Dr_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Dr_Name", Dr_Name_txtbox.Text)
        cmd.Parameters.AddWithValue("Report", Report_RichTextBox.Text)
        cmd.Parameters.AddWithValue("Date", Report_Date.Text)
        conn.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Report Updated!!")
        Else
            MessageBox.Show("Report Not updated!!")
        End If
        conn.Close()
        LoadData()
    End Sub

    Private Sub Report_main_btn_Click(sender As Object, e As EventArgs) Handles Report_main_btn.Click
        ' Check if any field is empty
        If String.IsNullOrEmpty(Report_Id_txtbox.Text) OrElse
            String.IsNullOrEmpty(Pay_Id_txtbox.Text) OrElse
                String.IsNullOrEmpty(Dr_Id_txtbox.Text) OrElse
                 String.IsNullOrEmpty(Pt_Id_txtbox.Text) OrElse
                  String.IsNullOrEmpty(Pt_Name_txtbox.Text) OrElse
                  String.IsNullOrEmpty(Dr_Name_txtbox.Text) OrElse
                   String.IsNullOrEmpty(Report_Date.Text) Then


            MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            InsertData()
        End If
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Report_Records_Form.Close()
    End Sub

    Private Sub Report_Update_btn_Click(sender As Object, e As EventArgs) Handles Report_Update_btn.Click
        ' Update button click event
        UpdateData()
    End Sub

    Private Sub Report_Id_txtbox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Report_Id_txtbox.KeyPress

        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = Chr(Keys.Back) Then
            MessageBox.Show("Numeric digits only")
            e.Handled = True
        End If
    End Sub

    Private Sub Pay_Id_txtbox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Pay_Id_txtbox.KeyPress

        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = Chr(Keys.Back) Then
            MessageBox.Show("Numeric digits only")
            e.Handled = True
        End If
    End Sub

    Private Sub Pt_Id_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pt_Id_txtbox.KeyPress
        If Not Char.IsLetterOrDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            MessageBox.Show("Only enter alphanumeric ")
            e.Handled = True
        End If
    End Sub

    Private Sub Pt_Name_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pt_Name_txtbox.KeyPress

        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Input should be in Letters")
        End If
    End Sub


    Private Sub Dr_Id_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dr_Id_txtbox.KeyPress
        If Not Char.IsLetterOrDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            MessageBox.Show("Only enter alphanumeric ")
            e.Handled = True
        End If
    End Sub


    Private Sub Dr_Name_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dr_Name_txtbox.KeyPress
        ' Check if the pressed key is a letter
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Input should be in Letters")
        End If
    End Sub


End Class

'---------------------------------------------------------------------------------------------------------------------------------------------------
