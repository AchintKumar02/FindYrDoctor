Imports System.ComponentModel.DataAnnotations
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Pt_Reg_Form
    Private Sub Guna2TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pt_Id_txtbox.KeyPress
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then

            Else
                MessageBox.Show("Invalid Input! Please Enter Numbers Only.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub Pt_Ph_No_TextChanged(sender As Object, e As KeyPressEventArgs) Handles Pt_Ph_No_txtbox.KeyPress
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then

            Else
                MessageBox.Show("Invalid Input! Please Enter Numbers Only.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                e.Handled = True
            End If
        End If
    End Sub

    Private Sub Pt_Aadhar_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pt_Aadhar_txtbox.KeyPress
        If e.KeyChar <> ChrW(Keys.Back) Then
            If Char.IsNumber(e.KeyChar) Then

            Else
                MessageBox.Show("Invalid Input! Please Enter Numbers Only.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                e.Handled = True
            End If
        End If

    End Sub

    Private Sub Pt_Register_Menu_btn_Click(sender As Object, e As EventArgs) Handles Pt_Register_Menu_btn.Click
        Pt_RegPanel.Visible = True
        Pt_UpdatePanel.Visible = False
    End Sub

    Private Sub Pt_Update_Menu_btn_Click(sender As Object, e As EventArgs) Handles Pt_Update_Menu_btn.Click
        Pt_RegPanel.Visible = False
        Pt_UpdatePanel.Visible = True
    End Sub

    Private Sub Pt_RecordsView_btn_Click(sender As Object, e As EventArgs) Handles Pt_RecordsView_btn.Click
        Pt_Records_Form.Show()
        Pt_Records_Form.Select()
    End Sub

    Private Sub Home_btn_Click(sender As Object, e As EventArgs) Handles Home_btn.Click
        Me.Hide()
        Main_Form.Show()
        Main_Form.Select()
    End Sub

    Private Sub PtReg_Clear_btn_Click(sender As Object, e As EventArgs) Handles PtReg_Clear_btn.Click
        Pt_FName_txtbox.Clear()
        Pt_LName_txtbox.Clear()
        Pt_Id_txtbox.Clear()
        Pt_Gender.Text = ""
        Pt_Ph_No_txtbox.Clear()
        Pt_Aadhar_txtbox.Clear()
        Pt_DOB.ResetText()
        Pt_Age_txtbox.Clear()
    End Sub

    Private Sub PtUpd_Clear_btn_Click(sender As Object, e As EventArgs) Handles PtUpd_Clear_btn.Click
        Pt_FName_txtbox.Clear()
        Pt_LName_txtbox.Clear()
        Pt_Id_txtbox.Clear()
        Pt_Gender.Text = ""
        Pt_Ph_No_txtbox.Clear()
        Pt_Aadhar_txtbox.Clear()
        Pt_DOB.ResetText()
        Pt_Age_txtbox.Clear()
    End Sub

    '-------------------------------DATABASE------------------------------------------------------------------------------------------------------------------------------------

    Dim conn As New MySqlConnection("Data Source=localhost;database=database_fyd3;userid=root;password=''")
    Dim cmd As MySqlCommand


    Private Sub Pt_Reg_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim rnd As New Random()
        Dim patientId As Integer = rnd.Next(1000, 9999)
        Pt_Id_txtbox.Text = "PT" & patientId.ToString()

        Dim currentDate As Date = Date.Now
        Pt_Reg_date.Text = currentDate.ToString("dd-MMM-yy")

        Pt_Records_Form.Show()
        LoadData()
    End Sub

    Public Sub LoadData()
        Dim query As String = "SELECT * FROM patient_table"
        Dim da As MySqlDataAdapter = New MySqlDataAdapter(query, conn)
        Dim dt As DataTable = New DataTable()
        da.Fill(dt)
        Pt_Records_Form.Pt_DataGridView1.DataSource = dt
    End Sub

    Private Sub InsertData()
        ' Insert data into table
        Dim query As String = "INSERT INTO patient_table(First_Name,Last_Name,Patient_Id,Gender,Pt_Mobile,Pt_Aadhar,Pt_DOB,Pt_Reg_date,Pt_Age) values(@First_Name,@Last_Name,@Patient_Id,@Gender,@Pt_Mobile,@Pt_Aadhar,@Pt_DOB,@Pt_Reg_date,@Pt_Age)"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("First_Name", Pt_FName_txtbox.Text)
        cmd.Parameters.AddWithValue("Last_Name", Pt_LName_txtbox.Text)
        cmd.Parameters.AddWithValue("Patient_Id", Pt_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Gender", Pt_Gender.Text)
        cmd.Parameters.AddWithValue("Pt_Mobile", Pt_Ph_No_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_Aadhar", Pt_Aadhar_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_DOB", Pt_DOB.Text)
        cmd.Parameters.AddWithValue("Pt_Reg_date", Pt_Reg_date.Text)
        cmd.Parameters.AddWithValue("Pt_Age", Pt_Age_txtbox.Text)
        conn.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Patient Registered Successfully!!")
        Else
            MessageBox.Show("Patient Registration Unsuccessful!!")
        End If
        conn.Close()
        LoadData()
    End Sub

    Private Sub UpdateData()
        ' Update data in table
        Dim query As String = "UPDATE patient_table SET First_Name=@First_Name,Last_Name=@Last_Name,Gender=@Gender, Pt_Mobile=@Pt_Mobile, Pt_Aadhar=@Pt_Aadhar,Pt_DOB=@Pt_DOB,Pt_Age=@Pt_Age WHERE Patient_Id=@Patient_Id"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("First_Name", Pt_FName_Upd_txtbox.Text)
        cmd.Parameters.AddWithValue("Last_Name", Pt_LName_Upd_txtbox.Text)
        cmd.Parameters.AddWithValue("Patient_Id", Pt_Id_Upd_txtbox.Text)
        cmd.Parameters.AddWithValue("Gender", Pt_Gender_Upd.Text)
        cmd.Parameters.AddWithValue("Pt_Mobile", Pt_Ph_No_Upd_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_Aadhar", Pt_Aadhar_Upd_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_DOB", Pt_DOB_Upd.Text)
        cmd.Parameters.AddWithValue("Pt_Age", Pt_Age_Upd_txtbox.Text)
        conn.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Data updated successfully!!")
        Else
            MessageBox.Show("Data not updated!!")
        End If
        conn.Close()
        LoadData()
    End Sub

    Private Sub DeleteData()
        ' Delete data from table
        Dim query As String = "DELETE FROM patient_table WHERE Patient_Id=@Patient_Id"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("@Patient_Id", Pt_Id_Upd_txtbox.Text)
        conn.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Data deleted successfully!!")
        Else
            MessageBox.Show("Data not deleted!!")
        End If
        conn.Close()
        LoadData()
    End Sub


    Private Sub Pt_Register_main_btn_Click(sender As Object, e As EventArgs) Handles Pt_Register_main_btn.Click
        If String.IsNullOrEmpty(Pt_FName_txtbox.Text) OrElse
            String.IsNullOrEmpty(Pt_LName_txtbox.Text) OrElse
                String.IsNullOrEmpty(Pt_Id_txtbox.Text) OrElse
                  String.IsNullOrEmpty(Pt_Reg_date.Text) OrElse
                 String.IsNullOrEmpty(Pt_Aadhar_txtbox.Text) OrElse
                  String.IsNullOrEmpty(Pt_DOB.Text) OrElse
                   String.IsNullOrEmpty(Pt_Age_txtbox.Text) OrElse
                    String.IsNullOrEmpty(Pt_Gender.Text) OrElse
                   String.IsNullOrEmpty(Pt_Reg_date.Text) OrElse
                String.IsNullOrEmpty(Pt_Ph_No_txtbox.Text) Then
            MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            InsertData()
        End If
    End Sub

    Private Sub Pt_Update_main_btn_Click(sender As Object, e As EventArgs) Handles Pt_Update_main_btn.Click
        If String.IsNullOrEmpty(Pt_FName_txtbox.Text) OrElse
            String.IsNullOrEmpty(Pt_LName_txtbox.Text) OrElse
                String.IsNullOrEmpty(Pt_Id_txtbox.Text) OrElse
                 String.IsNullOrEmpty(Pt_Aadhar_Upd_txtbox.Text) OrElse
                  String.IsNullOrEmpty(Pt_DOB_Upd.Text) OrElse
                   String.IsNullOrEmpty(Pt_Age_Upd_txtbox.Text) OrElse
                    String.IsNullOrEmpty(Pt_Gender_Upd.Text) OrElse
                String.IsNullOrEmpty(Pt_Ph_No_Upd_txtbox.Text) Then

            MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            UpdateData()
        End If

    End Sub

    Private Sub Pt_Delete_btn_Click(sender As Object, e As EventArgs) Handles Pt_Delete_btn.Click
        ' Delete button click event
        DeleteData()
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Pt_Records_Form.Close()
    End Sub


    'patient register form validation




    Private Sub Pt_FName_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pt_FName_txtbox.KeyPress
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Input should be in Letters")
        End If
    End Sub
    Private Sub Pt_LName_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pt_LName_txtbox.KeyPress
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            MessageBox.Show("Input should be in Letters")
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

        ' Mobile number is valid
    End Sub
    Private Sub Pt_Aadhar_txtbox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Pt_Aadhar_txtbox.Validating
        Dim mobileNumber As String = Pt_Aadhar_txtbox.Text.Trim()

        ' Check if mobile number is empty or null
        If String.IsNullOrEmpty(mobileNumber) Then
            MessageBox.Show("Please enter your mobile number.", "Aadhar Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
            Return
        End If

        ' Check if mobile number has exactly 12 digits
        If mobileNumber.Length <> 12 Then
            MessageBox.Show("Please enter a valid 12-digit Aadhar number.", "Aadhar Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
            Return
        End If

        ' Check if mobile number contains only digits
        For Each c As Char In mobileNumber
            If Not Char.IsDigit(c) Then
                MessageBox.Show("Please enter a valid 12-digit Aadhar number containing only digits.", "Aadhar Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True
                Return
            End If
        Next

        ' Mobile number is valid
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
    Private Sub Pt_DOB_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Pt_DOB.Validating
        If Pt_DOB.Value > DateTime.Now Then
            MessageBox.Show("Date of Birth cannot be greater than current date.")
            Pt_DOB.ResetText()
            e.Cancel = True
        End If
    End Sub




    'UPDATE PAGE VALIDATION



    Private Sub Pt_FName_Upd_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pt_FName_Upd_txtbox.KeyPress
        ' Check if the pressed key is a letter
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then

            e.Handled = True
            MessageBox.Show("Input should be in Letters")
        End If
    End Sub
    Private Sub Pt_FName_Upd_txtbox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Pt_FName_Upd_txtbox.Validating
        If String.IsNullOrEmpty(Pt_FName_Upd_txtbox.Text) Then
            MessageBox.Show("Please enter a value.")
            e.Cancel = True
        End If
    End Sub

    Private Sub Pt_LName_Upd_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pt_LName_Upd_txtbox.KeyPress
        ' Check if the pressed key is a letter
        If Not Char.IsLetter(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then

            e.Handled = True
            MessageBox.Show("Input should be in Letters")
        End If
    End Sub
    Private Sub Pt_Ph_No_upd_txtbox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Pt_Ph_No_Upd_txtbox.Validating
        Dim mobileNumber As String = Pt_Ph_No_Upd_txtbox.Text.Trim()

        ' Check if mobile number is empty or null
        If String.IsNullOrEmpty(mobileNumber) Then
            MessageBox.Show("Please enter your mobile number.", "Mobile Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
            Return
        End If

        ' Check if mobile number has exactly 10 digits
        If mobileNumber.Length <> 10 Then
            MessageBox.Show("Please enter a valid 10-digit mobile number.", "Mobile Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
            Return
        End If

        ' Check if mobile number contains only digits
        For Each c As Char In mobileNumber
            If Not Char.IsDigit(c) Then
                MessageBox.Show("Please enter a valid 10-digit mobile number containing only digits.", "Mobile Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True
                Return
            End If
        Next

        ' Mobile number is valid
    End Sub
    Private Sub Pt_Aadhar_upd_txtbox_Validating(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles Pt_Aadhar_Upd_txtbox.Validating
        Dim mobileNumber As String = Pt_Aadhar_txtbox.Text.Trim()

        ' Check if mobile number is empty or null
        If String.IsNullOrEmpty(mobileNumber) Then
            MessageBox.Show("Please enter your Aadhar number.", "Aadhar Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
            Return
        End If

        ' Check if mobile number has exactly 12 digits
        If mobileNumber.Length <> 12 Then
            MessageBox.Show("Please enter a valid 12-digit Aadhar number.", "Aadhar Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            e.Cancel = True
            Return
        End If

        ' Check if mobile number contains only digits
        For Each c As Char In mobileNumber
            If Not Char.IsDigit(c) Then
                MessageBox.Show("Please enter a valid 12-digit Aadhar number containing only digits.", "Aadhar Number Validation", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                e.Cancel = True
                Return
            End If
        Next

        ' Mobile number is valid
    End Sub
    Private Sub Pt_Age_Upd_txtbox_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Pt_Age_txtbox.Validating
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
    Private Sub Pt_DOB_upd_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles Pt_DOB.Validating
        If Pt_DOB.Value > DateTime.Now Then
            MessageBox.Show("Date of Birth cannot be greater than current date.")
            Pt_DOB.ResetText()
            e.Cancel = True
        End If
    End Sub


End Class

'--------------------------------------------------------------------------------------------------------------------------------------------------------------------------
