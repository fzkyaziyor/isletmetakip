Imports System.Data
Imports System.Data.OleDb
Public Class personelguncelle
    Dim c As OleDbConnection
    Dim adap As New OleDbDataAdapter
    Dim ds As New DataSet
    Private Sub personelguncelle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        c = New OleDbConnection(" Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb")
        c.Open()
        Dim id As Integer = menu1.lblperno.Text
        Dim adap As OleDbDataAdapter = New OleDbDataAdapter("select * from personel where perno=" & id, c)
        ds = New DataSet
        adap.Fill(ds, "personel")
        Dim kayit As DataRow = ds.Tables("personel").Rows(0)
        TextBox8.Text = kayit("isim").ToString
        TextBox6.Text = kayit("adres").ToString
        TextBox1.Text = kayit("tel").ToString
        TextBox2.Text = kayit("maas").ToString
        DateTimePicker1.Value = kayit("isebaslama").ToString
        TextBox4.Text = kayit("aciklama").ToString
        TextBox5.Text = kayit("gorev").ToString
        TextBox7.Text = kayit("resim").ToString
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim komut As OleDbCommand = New OleDbCommand("update personel set
isim=@isim,adres=@adres,tel=@tel,maas=@maas,isebaslama=@isebaslama,
aciklama=@aciklama,gorev=@gorev,resim=@resim where perno=" & menu1.lblperno.Text, c)
        komut.Parameters.AddWithValue("@isim", TextBox8.Text)
        komut.Parameters.AddWithValue("@adres", TextBox6.Text)
        komut.Parameters.AddWithValue("@tel", TextBox1.Text)
        komut.Parameters.AddWithValue("@maas", TextBox2.Text)
        komut.Parameters.AddWithValue("@isebaslama", DateTimePicker1.Value.ToShortDateString)
        komut.Parameters.AddWithValue("@aciklama", TextBox4.Text)
        komut.Parameters.AddWithValue("@gorev", TextBox5.Text)
        komut.Parameters.AddWithValue("@resim", TextBox7.Text)
        komut.ExecuteNonQuery()
        MsgBox("Personeliniz Güncellendi!")

        Dim frm2 As personeltakip = New personeltakip()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
        frm2.personelgoster(menu1.lblsatirno.Text)
        frm2.DataGridView1.FirstDisplayedScrollingRowIndex = menu1.lblustsatir .Text
        Me.Close()
    End Sub
End Class