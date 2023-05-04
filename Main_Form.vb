Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports MySql.Data.MySqlClient

Public Class Main_Form
    Private Sub Dr_menu_btn_Click(sender As Object, e As EventArgs) Handles Dr_menu_btn.Click
        Dr_Reg_Form.Show()
        Dr_Reg_Form.Select()
    End Sub

    Private Sub Pt_menu_btn_Click(sender As Object, e As EventArgs) Handles Pt_menu_btn.Click
        Pt_Reg_Form.Show()
        Pt_Reg_Form.Select()
    End Sub

    Private Sub Appt_menu_btn_Click(sender As Object, e As EventArgs) Handles Appt_menu_btn.Click
        Appt_Form.Show()
        Appt_Form.Select()
    End Sub

    Private Sub Report_menu_btn_Click(sender As Object, e As EventArgs) Handles Report_menu_btn.Click
        Report_Form.Show()
        Report_Form.Select()
    End Sub

    Private Sub Payment_menu_btn_Click(sender As Object, e As EventArgs) Handles Payment_menu_btn.Click
        Payment_Form.Show()
        Payment_Form.Select()
    End Sub

    Private Sub Home_menu_btn_Click(sender As Object, e As EventArgs) Handles Home_menu_btn.Click
        Me.Select()
    End Sub


    '---------------------------------------DATABASE------------------------------------------------------------------------------------------------------

    Dim connectionString As String = "Data Source=localhost;database=database_fyd3;userid=root;password=''"
    Dim connection As MySqlConnection = New MySqlConnection(connectionString)

    Private Sub Main_Form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim query As String = "SELECT * FROM doctor_table"
        Dim dataAdapter As MySqlDataAdapter = New MySqlDataAdapter(query, connection)
        Dim dataTable As DataTable = New DataTable()
        dataAdapter.Fill(dataTable)
        Home_DataGridView1.DataSource = dataTable
    End Sub

    Private Sub Dr_Category_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Dr_Category.SelectedIndexChanged
        DrName_Searchbox.Text = ""
        Dim searchTerm2 As String = Dr_Category.Text.Trim()
        Dim query As String = $"SELECT * FROM doctor_table WHERE Dr_Category LIKE '%{searchTerm2}%'"
        Dim dataAdapter As MySqlDataAdapter = New MySqlDataAdapter(query, connection)
        Dim dataTable As DataTable = New DataTable()
        dataAdapter.Fill(dataTable)
        Home_DataGridView1.DataSource = dataTable
    End Sub

    Private Sub DrName_Searchbox_TextChanged(sender As Object, e As EventArgs) Handles DrName_Searchbox.TextChanged
        Dr_Category.Text = ""
        Dim searchTerm As String = DrName_Searchbox.Text.Trim()
        Dim query As String = $"SELECT * FROM doctor_table WHERE First_Name LIKE '%{searchTerm}%'"
        Dim dataAdapter As MySqlDataAdapter = New MySqlDataAdapter(query, connection)
        Dim dataTable As DataTable = New DataTable()
        dataAdapter.Fill(dataTable)
        Home_DataGridView1.DataSource = dataTable
    End Sub

    Private Sub MainF_Search_btn_Click_1(sender As Object, e As EventArgs) Handles MainF_Search_btn.Click
        Dr_Category.Text = ""
        Dim searchTerm As String = DrName_Searchbox.Text.Trim()
        Dim searchTerm2 As String = Dr_Category.Text.Trim()
        Dim query As String = $"SELECT * FROM doctor_table WHERE Dr_Category LIKE '%{searchTerm2}%' AND First_Name LIKE '%{searchTerm}%'"
        Dim dataAdapter As MySqlDataAdapter = New MySqlDataAdapter(query, connection)
        Dim dataTable As DataTable = New DataTable()
        dataAdapter.Fill(dataTable)
        Home_DataGridView1.DataSource = dataTable
    End Sub

End Class