Public Class Sample
    Private Sub Dr_Register_main_btn_Click(sender As Object, e As EventArgs) Handles Dr_Register_main_btn.Click
        ' Check if any field is empty
        If String.IsNullOrEmpty(Dr_FName_txtbox.Text) OrElse
                String.IsNullOrEmpty(Dr_LName_txtbox.Text) OrElse
                String.IsNullOrEmpty(Dr_Id_txtbox.Text) OrElse
                String.IsNullOrEmpty(Dr_Category.Text) OrElse
                String.IsNullOrEmpty(Dr_Ph_No_txtbox.Text) Then
            ' Display an error message
            MessageBox.Show("Please fill in all the fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return ' Exit the sub
        End If

        ' Continue with registration process
        ' ...
    End Sub

End Class