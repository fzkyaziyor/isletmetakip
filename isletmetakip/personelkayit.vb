Imports System.Data
Imports System.Data.OleDb
Public Class personelkayit
    Dim c As OleDbConnection
    Dim adap As New OleDbDataAdapter
    Dim ds As New DataSet
    Public id As Integer
    Private Sub personelkayit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        c = New OleDbConnection(" Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb")
        c.Open()
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim komut As OleDbCommand = New OleDbCommand("insert into personel
(isim,adres,tel,maas,isebaslama,aciklama,gorev,resim) values
(@isim,@adres,@tel,@maas,@isebaslama,@aciklama,@gorev,@resim)", c)
        komut.Parameters.AddWithValue("@isim", TextBox8.Text)
        komut.Parameters.AddWithValue("@adres", TextBox6.Text)
        komut.Parameters.AddWithValue("@tel", TextBox1.Text)
        komut.Parameters.AddWithValue("@maas", TextBox2.Text)
        komut.Parameters.AddWithValue("@isebaslama", DateTimePicker1.Value.ToShortDateString)
        komut.Parameters.AddWithValue("@aciklama", TextBox4.Text)
        komut.Parameters.AddWithValue("@gorev", TextBox5.Text)
        komut.Parameters.AddWithValue("@resim", TextBox7.Text)
        komut.ExecuteNonQuery()
        MsgBox("Yeni personel Kaydınız Yapıldı...")
        Me.Close()  'bu formu kapat
        menu1.Panel1.Controls.Clear()
        Dim frm2 As personeltakip = New personeltakip()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
        menu1.lblperno.Text = frm2.DataGridView1.Rows(0).Cells(0).Value
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim ofd As New OpenFileDialog
        ofd.Filter = "Resim Seç(Bitmap Files(*.bmp) | *.bmp| JPG Files (*.jpg) |*.jpg| GIF Files (*.gif) |*.gif"
        If ofd.ShowDialog = Windows.Forms.DialogResult.OK Then
            PictureBox1.Image = Image.FromFile(ofd.FileName)
        End If
        TextBox7.Text = ofd.FileName
    End Sub
End Class