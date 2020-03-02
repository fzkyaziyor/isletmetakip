Public Class sifre
    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim sifre As String = "kdhamit"
        Dim iste As String = TextBox3.Text
        If iste = sifre Then
            menu1.Show()
            Me.WindowState = FormWindowState.Minimized
        Else
            MsgBox("Şifrenizi hatalı girdiniz")
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        End
    End Sub
End Class