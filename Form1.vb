Imports Guna.UI2.WinForms
Public Class Login_Form
    Private Sub Login_btn_Click(sender As Object, e As EventArgs) Handles Login_btn.Click
        If Username_Txtbox.Text = "Akash" And Password_Txtbox.Text = "akash12" Or Username_Txtbox.Text = "Achint" And Password_Txtbox.Text = "achint12" Then
            MessageBox.Show("Login Successfull!!")
            Me.Hide()
            Main_Form.Show()
        Else
            MessageDialog.Show("Invalid Credentials!!")
        End If
    End Sub

    Private Sub Clear_btn_Click(sender As Object, e As EventArgs) Handles Clear_btn.Click
        Username_Txtbox.Clear()
        Password_Txtbox.Clear()
    End Sub


End Class
