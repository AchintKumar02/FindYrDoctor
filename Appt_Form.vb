Imports System.Threading
Imports System.Timers
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Appt_Form
    Private Sub Pt_RecView_btn_Click(sender As Object, e As EventArgs) Handles Pt_RecView_btn.Click
        Pt_Records_Form.Show()
        Pt_Records_Form.Select()
    End Sub

    Private Sub Dr_RecView_btn_Click(sender As Object, e As EventArgs) Handles Dr_RecView_btn.Click
        Dr_Records_Form.Show()
        Dr_Records_Form.Select()
    End Sub

    Private Sub Home_btn_Click(sender As Object, e As EventArgs) Handles Home_btn.Click
        Me.Hide()
        Main_Form.Show()
        Main_Form.Select()
    End Sub

    Private Sub Appt_RecView_btn_Click(sender As Object, e As EventArgs) Handles Appt_RecView_btn.Click
        Appt_Records_Form.Show()
        Appt_Records_Form.Select()
    End Sub

    Private Sub Appt_Clear_btn_Click(sender As Object, e As EventArgs) Handles Appt_Clear_btn.Click
        Appt_Id_txtbox.Clear()
        Appt_Date.ResetText()
        Appt_Time.Text = ""
        Pt_Id_txtbox.Clear()
        Pt_Name_txtbox.Clear()
        Pt_Ph_No_txtbox.Clear()
        Pt_Age_txtbox.Clear()
        Dr_Id_txtbox.Clear()
        Dr_Name_txtbox.Clear()
        Dr_Category.Text = ""
    End Sub


    '-------------------------------DATABASE--------------------------------------------------------------------------------------------------------------

    Dim conn As New MySqlConnection("Data Source=localhost;database=database_fyd3;userid=root;password=''")
    Dim cmd As MySqlCommand

    Private Sub Appt_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Set random appointment id 
        Dim rand As New Random()
        Dim customerId As Integer = rand.Next(100000, 999999)
        Dim formattedId As String = customerId.ToString("D3")
        Appt_Id_txtbox.Text = formattedId


        'Load data into datagridview on form load
        Appt_Records_Form.Show()
        LoadData()
    End Sub


    Public Sub LoadData()

        Dim query As String = "SELECT * FROM appointment_table"
        Dim da As MySqlDataAdapter = New MySqlDataAdapter(query, conn)
        Dim dt As DataTable = New DataTable()
        da.Fill(dt)
        Appt_Records_Form.Appt_DataGridView1.DataSource = dt
    End Sub

    Private Sub InsertData()
        ' Insert data into table
        Dim query As String = "INSERT INTO appointment_table(Appt_Id,Appt_Date,Appt_Time,Patient_Id,Pt_Name,Pt_Mobile,Pt_Age,Doctor_Id,Dr_Name,Dr_Category) values(@Appt_Id,@Appt_Date,@Appt_Time,@Patient_Id,@Pt_Name,@Pt_Mobile,@Pt_Age,@Doctor_Id,@Dr_Name,@Dr_Category)"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("Appt_Id", Appt_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Appt_Date", Appt_Date.Text)
        cmd.Parameters.AddWithValue("Appt_Time", Appt_Time.Text)
        cmd.Parameters.AddWithValue("Patient_Id", Pt_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_Name", Pt_Name_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_Mobile", Pt_Ph_No_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_Age", Pt_Age_txtbox.Text)
        cmd.Parameters.AddWithValue("Doctor_Id", Dr_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Dr_Name", Dr_Name_txtbox.Text)
        cmd.Parameters.AddWithValue("Dr_Category", Dr_Category.Text)
        conn.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Appointment Booking Confirmed!!")
        Else
            MessageBox.Show("Appointment Booking Unsuccessful!!")
        End If
        conn.Close()
        LoadData()
    End Sub


    Private Sub UpdateData()
        ' Update data in table
        Dim query As String = "UPDATE appointment_table SET Appt_Date=@Appt_Date,Appt_Time=@Appt_Time,Patient_Id=@Patient_Id, Pt_Name=@Pt_Name, Pt_Mobile=@Pt_Mobile,Pt_Age=@Pt_Age,Doctor_Id=@Doctor_Id,Dr_Name=@Dr_Name,Dr_Category=@Dr_Category WHERE Appt_Id=@Appt_Id"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("Appt_Id", Appt_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Appt_Date", Appt_Date.Text)
        cmd.Parameters.AddWithValue("Appt_Time", Appt_Time.Text)
        cmd.Parameters.AddWithValue("Patient_Id", Pt_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_Name", Pt_Name_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_Mobile", Pt_Ph_No_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_Age", Pt_Age_txtbox.Text)
        cmd.Parameters.AddWithValue("Doctor_Id", Dr_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Dr_Name", Dr_Name_txtbox.Text)
        cmd.Parameters.AddWithValue("Dr_Category", Dr_Category.Text)
        conn.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Appointment Updated!!")
        Else
            MessageBox.Show("Appointment Not updated!!")
        End If
        conn.Close()
        LoadData()
    End Sub

    Private Sub DeleteData()
        ' Delete data from table
        Dim query As String = "DELETE FROM appointment_table WHERE Appt_Id=@Appt_Id"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@Appt_Id", Appt_Id_txtbox.Text)
        conn.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Appointmenr deleted successfully!!")
        Else
            MessageBox.Show("Appointment not deleted!!")
        End If
        conn.Close()
        LoadData()
    End Sub

    Private Sub Appt_Book_btn_Click(sender As Object, e As EventArgs) Handles Appt_Book_btn.Click

        ' Check if any field is empty
        If String.IsNullOrEmpty(Appt_Id_txtbox.Text) OrElse
            String.IsNullOrEmpty(Appt_Time.Text) OrElse
                String.IsNullOrEmpty(Appt_Date.Text) OrElse
                 String.IsNullOrEmpty(Pt_Id_txtbox.Text) OrElse
                  String.IsNullOrEmpty(Pt_Ph_No_txtbox.Text) OrElse
                   String.IsNullOrEmpty(Pt_Name_txtbox.Text) OrElse
                    String.IsNullOrEmpty(Pt_Age_txtbox.Text) OrElse
                 String.IsNullOrEmpty(Dr_Category.Text) OrElse
                  String.IsNullOrEmpty(Dr_Name_txtbox.Text) OrElse
                String.IsNullOrEmpty(Dr_Id_txtbox.Text) Then

            MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            InsertData()
        End If
    End Sub

    Private Sub Appt_Update_btn_Click(sender As Object, e As EventArgs) Handles Appt_Update_btn.Click
        ' Update button click event
        UpdateData()
    End Sub

    Private Sub Appt_Delete_btn_Click_1(sender As Object, e As EventArgs) Handles Appt_Delete_btn.Click
        ' Delete button click event
        DeleteData()
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Appt_Records_Form.Close()
    End Sub


    Private Sub Appt_Id_txtbox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Appt_Id_txtbox.KeyPress
        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = Chr(Keys.Back) Then
            e.Handled = True
        End If
    End Sub


    Private Sub Pt_Name_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pt_Name_txtbox.KeyPress

        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Input should be in Letters")
        End If
    End Sub


    Private Sub Pt_Id_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pt_Id_txtbox.KeyPress
        If Not Char.IsLetterOrDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            MessageBox.Show("Only enter alphanumeric ")
            e.Handled = True
        End If
    End Sub


    Private Sub Pt_Ph_No_txtbox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Pt_Ph_No_txtbox.Validating
        Dim mobileNumber As String = Pt_Ph_No_txtbox.Text.Trim()

        If String.IsNullOrEmpty(mobileNumber) Then
            MessageBox.Show("Please enter your mobile number.", "Mobile Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
            Return
        End If

        If mobileNumber.Length <> 10 Then
            MessageBox.Show("Please enter a valid 10-digit mobile number.", "Mobile Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
            Return
        End If

        For Each c As Char In mobileNumber
            If Not Char.IsDigit(c) Then
                MessageBox.Show("Please enter a valid 10-digit mobile number containing only digits.", "Mobile Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True
                Return
            End If
        Next
    End Sub


    Private Sub Pt_Age_txtbox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Pt_Age_txtbox.Validating
        Dim age As Integer
        If Not Integer.TryParse(Pt_Age_txtbox.Text, age) Then
            MessageBox.Show("Please enter a valid age.")
            e.Cancel = True
        End If
        If age < 0 Or age > 130 Then
            MessageBox.Show("Age should be between 0 and 130.")
            e.Cancel = True
        End If
    End Sub


    'Doctor validation

    Private Sub Dr_Id_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dr_Id_txtbox.KeyPress
        If Not Char.IsLetterOrDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            MessageBox.Show("Only enter alphanumeric ")
            e.Handled = True
        End If
    End Sub


    Private Sub Dr_Name_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Dr_Name_txtbox.KeyPress
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Input should be in Letters")
        End If
    End Sub


End Class

'-------------------------------------------------------------------------------------------------------------------------------------------------------


