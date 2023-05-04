Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient
Imports Mysqlx.Datatypes.Scalar.Types

Public Class Payment_Form

    Private Sub Home_btn_Click(sender As Object, e As EventArgs) Handles Home_btn.Click
        Me.Hide()
        Main_Form.Show()
        Main_Form.Select()
    End Sub

    Private Sub Pay_RecView_btn_Click(sender As Object, e As EventArgs) Handles Pay_RecView_btn.Click
        Pay_Records_Form2.Show()
        Pay_Records_Form2.Select()
    End Sub

    Private Sub Pay_clear_btn_Click(sender As Object, e As EventArgs) Handles Pay_clear_btn.Click
        Pay_Id_txtbox.Clear()
        Pt_Id_txtbox.Clear()
        Pt_Name_txtbox.Clear()
        Pay_Amt_txtbox.Clear()
        Pay_Mode.SelectedItem = "UPI"
    End Sub


    '-------------------------------DATABASE-------------------------------------------------------------------------------------------------------------

    Dim conn As New MySqlConnection("Data Source=localhost;database=database_fyd3;userid=root;password=''")
    Dim cmd As MySqlCommand

    Private Sub Payment_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim rand As New Random()
        Dim customerId As Integer = rand.Next(100000, 999999)
        Dim formattedId As String = customerId.ToString("D3")
        Pay_Id_txtbox.Text = formattedId


        ' Retrieve the current system date
        Dim currentDate As Date = Date.Now
        Pay_Date.Text = currentDate.ToString("dd-MMM-yy")

        Pay_Mode.SelectedItem = "UPI"
        Pay_Records_Form2.Show()
        LoadData()
    End Sub

    Public Sub LoadData()
        Dim query As String = "SELECT * FROM payment_table"
        Dim da As MySqlDataAdapter = New MySqlDataAdapter(query, conn)
        Dim dt As DataTable = New DataTable()
        da.Fill(dt)
        Pay_Records_Form2.Pay_DataGridView1.DataSource = dt
    End Sub

    Private Sub InsertData()
        ' Insert data into table
        Dim query As String = "INSERT INTO payment_table(Payment_Id,Patient_Id,Pt_Name,Pay_Amt,Pay_Mode,Date) values(@Payment_Id,@Patient_Id,@Pt_Name,@Pay_Amt,@Pay_Mode,@Date)"
        cmd = New MySqlCommand(query, conn)
        cmd.Parameters.AddWithValue("Payment_Id", Pay_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Patient_Id", Pt_Id_txtbox.Text)
        cmd.Parameters.AddWithValue("Pt_Name", Pt_Name_txtbox.Text)
        cmd.Parameters.AddWithValue("Pay_Amt", Pay_Amt_txtbox.Text)
        cmd.Parameters.AddWithValue("Pay_Mode", Pay_Mode.Text)
        cmd.Parameters.AddWithValue("Date", Pay_Date.Text)
        conn.Open()
        If cmd.ExecuteNonQuery() = 1 Then
            MessageBox.Show("Payment Success.")
        Else
            MessageBox.Show("Payment Failure.")
        End If
        conn.Close()
        LoadData()
    End Sub

    Private Sub Pay_main_btn_Click(sender As Object, e As EventArgs) Handles Pay_main_btn.Click
        If String.IsNullOrEmpty(Pt_Name_txtbox.Text) Then

            MessageBox.Show("Please enter a value for the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

            Pt_Name_txtbox.Focus()

            Return

            If String.IsNullOrEmpty(Pt_Id_txtbox.Text) Then

                MessageBox.Show("Please enter a value for the textbox.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)

                Pt_Id_txtbox.Focus()

                Return
            End If
        End If

        If String.IsNullOrEmpty(Pay_Id_txtbox.Text) OrElse
            String.IsNullOrEmpty(Pt_Id_txtbox.Text) OrElse
                String.IsNullOrEmpty(Pt_Name_txtbox.Text) OrElse
                   String.IsNullOrEmpty(Pay_Amt_txtbox.Text) OrElse
                 String.IsNullOrEmpty(Pay_Mode.Text) OrElse
                  String.IsNullOrEmpty(Pay_Date.Text) Then

            MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        Else
            InsertData()
        End If
    End Sub

    Private Sub Guna2ControlBox1_Click(sender As Object, e As EventArgs) Handles Guna2ControlBox1.Click
        Pay_Records_Form2.Close()
    End Sub


    'validating 
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

    Private Sub Pay_Id_txtbox_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Pay_Id_txtbox.KeyPress
        If Not Char.IsLetterOrDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            MessageBox.Show("Only enter alphanumeric ")
            e.Handled = True
        End If
    End Sub
    Private Sub Pay_Amt_TxtBox_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Pay_Amt_txtbox.KeyPress

        If Not Char.IsDigit(e.KeyChar) AndAlso Not e.KeyChar = Chr(Keys.Back) Then
            MessageBox.Show("Only enter numeric values")
            e.Handled = True
        End If
    End Sub


End Class


'---------------------------------------------------------------------------------------------------------------------------------------------------




