Imports System.Data
Imports System.Data.OleDb
Public Class musteriekle
    Dim c As OleDbConnection
    Dim baglanti, sorgu1 As String

    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        baglan()
        Dim komut As OleDbCommand = New OleDbCommand("insert into musteriler (soyadi,adi,notlar,adres,tel) values (@soyadi,@adi,@notlar,@adres,@tel)", c)
        komut.Parameters.AddWithValue("@soyadi", TextBox3.Text)
        komut.Parameters.AddWithValue("@adi", TextBox1.Text)
        komut.Parameters.AddWithValue("@notlar", TextBox2.Text)
        komut.Parameters.AddWithValue("@adres", TextBox4.Text)
        komut.Parameters.AddWithValue("@tel", TextBox5.Text)


        komut.ExecuteNonQuery()
        MsgBox("Yeni Müşteri Kaydınız Yapıldı...")
        Me.Close()  'bu formu kapat

        Dim f2 As musteritakip = Application.OpenForms("musteritakip")
        f2.musteriyenile(0)


        menu1.Panel1.Controls.Clear()
        Dim frm2 As musteritakip = New musteritakip()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()



    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As musteritakip = New musteritakip()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
        Me.Close()
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        TextBox1.Text = TextBox1.Text.ToUpper()
        TextBox1.SelectionStart = TextBox1.Text.Length
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
        TextBox3.Text = TextBox3.Text.ToUpper()
        TextBox3.SelectionStart = TextBox3.Text.Length
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        TextBox4.Text = TextBox4.Text.ToUpper()
        TextBox4.SelectionStart = TextBox4.Text.Length
    End Sub

    Private Sub musteriekle_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub TextBox2_GotFocus(sender As Object, e As EventArgs) Handles TextBox2.GotFocus
        TextBox2.SelectAll()
    End Sub
End Class