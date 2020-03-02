Imports System.Data
Imports System.Data.OleDb
Imports System.IO

Public Class menu1
    Dim adap As New OleDbDataAdapter
    Dim ds As New DataSet
    Dim c As OleDbConnection
    Dim baglanti As String
    Public zz As Integer
    Public ceksenetvar As Integer
    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Panel1.Controls.Clear()
        Dim frm2 As musteritakip = New musteritakip()
        frm2.TopLevel = False
        Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Panel1.Controls.Clear()
        Dim frm2 As veritabanı_işlemleri = New veritabanı_işlemleri()
        frm2.TopLevel = False
        Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Panel1.Controls.Clear()
        Dim frm2 As stoktakip = New stoktakip()
        frm2.TopLevel = False
        Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ceksenetvar = 1

        Panel1.Controls.Clear()
        Dim frm2 As ceksenettakip = New ceksenettakip()
        frm2.TopLevel = False
        Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
    End Sub


    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        ceksenetvar = 0
        Panel1.Controls.Clear()
        Dim frm2 As ceksenettakip = New ceksenettakip()
        frm2.TopLevel = False
        Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        End
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Panel1.Controls.Clear()
        Dim frm2 As emanettakip = New emanettakip()
        frm2.TopLevel = False
        Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Panel1.Controls.Clear()
        Dim frm2 As ihaletakip = New ihaletakip()
        frm2.TopLevel = False
        Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
    End Sub

    Private Sub menu1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim frm2 As sifre = New sifre()
        frm2.WindowState = FormWindowState.Minimized
        baglan()
        Dim sonodemetarihi, bugun As Date
        bugun = Now.Date.ToShortDateString
        sonodemetarihi = DateAdd(DateInterval.Day, 3, bugun)

        Dim komut As OleDbCommand = New OleDbCommand("select * from ceksenet where odemetarihi between @tarih1 AND @tarih and durum='ÖDENMEDİ' order by ceksenetno desc", c)
        komut.Parameters.AddWithValue("@tarih1", bugun)
        komut.Parameters.AddWithValue("@tarih2", sonodemetarihi)
        Dim adap As OleDbDataAdapter = New OleDbDataAdapter(komut)
        Dim tablo As DataTable = New DataTable
        adap.Fill(tablo)
        If tablo.Rows.Count <> 0 Then
            ceksenetvar = 1
            Button8.Visible = True
        End If
    End Sub
    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Panel1.Controls.Clear()
        Dim frm2 As personeltakip = New personeltakip()
        frm2.TopLevel = False
        Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
        lblperno.Text = frm2.DataGridView1.CurrentRow.Cells(0).Value.ToString
        lblsatirno.Text = frm2.DataGridView1.CurrentRow.Index.ToString
        lblustsatir.Text = frm2.DataGridView1.FirstDisplayedScrollingColumnIndex
    End Sub


End Class