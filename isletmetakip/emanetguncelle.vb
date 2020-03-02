Imports System.Data
Imports System.Data.OleDb
Public Class emanetguncelle
    Dim adap, adap1 As New OleDbDataAdapter
    Dim ds As New DataSet
    Dim c As OleDbConnection
    Dim baglanti, sorgu As String
    Public id As Integer
    Public ney As String

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click

        baglan()
        Dim komut As OleDbCommand = New OleDbCommand("update emanet set aciklama='" + TextBox2.Text + "',firma='" + TextBox1.Text + "'  where id=" & Label12.Text, c)
        komut.ExecuteNonQuery()
        MsgBox("Emanet Kaydınız Güncellendi")
        menu1.Panel1.Controls.Clear()
        Dim frm2 As emanettakip = New emanettakip()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()





        Me.Close()
    End Sub


    Private Sub emanetguncelle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        firmagoster()
        DataGridView2.Font = New Font("arial", 9, FontStyle.Bold)
    End Sub
    Public Sub firmagoster()
        baglan()
        Dim komut As OleDbCommand = New OleDbCommand("select * from firma order by firmaadi asc", c)
        Dim dt As DataTable = New DataTable
        dt.Load(komut.ExecuteReader)
        c.Close()
        DataGridView2.DataSource = dt
    End Sub
    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As emanettakip = New emanettakip()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
        Me.Close()
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        TextBox1.Text = DataGridView2.CurrentRow.Cells(1).Value
    End Sub
End Class