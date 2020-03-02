Imports System.Data
Imports System.Data.OleDb
Imports System.Xml
Public Class firmayaodemeyap
    Dim adap As New OleDbDataAdapter
    Dim ds As New DataSet
    Dim c As OleDbConnection
    Dim baglanti As String
    Public zz As Integer


    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub
    Private Sub yeniurunekle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        firmagoster()
        DataGridView1.Font = New Font("arial", 9, FontStyle.Bold)
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        baglan()
        Dim komut As OleDbCommand = New OleDbCommand("insert 
into odeme (firma,odenentutar,nott) values (@firma,@odenentutar,@nott)", c)
        komut.Parameters.AddWithValue("@firma", TextBox1.Text)
        komut.Parameters.AddWithValue("@odenentutar", TextBox4.Text)
        komut.Parameters.AddWithValue("@nott", TextBox2.Text)
        komut.ExecuteNonQuery()
        MsgBox("Yeni Ödeme Kaydınız Yapıldı")

        Dim f2 As stoktakip = Application.OpenForms("stoktakip")
        f2.urungoster()

        menu1.Panel1.Controls.Clear()
        Dim frm2 As stoktakip = New stoktakip()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()

        Me.Close()

    End Sub



    Private Sub Button7_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button7_Click_1(sender As Object, e As EventArgs) Handles Button7.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As stoktakip = New stoktakip()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
        Me.Close()
    End Sub



    Private Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles TextBox4.Leave
        TextBox4.Text = Val(TextBox4.Text).ToString("C")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As firmasec = New firmasec()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
    End Sub
    Private Sub ara(ByVal sorgu As String)
        baglan()
        adap = New OleDbDataAdapter(sorgu, c)
        ds = New DataSet
        adap.Fill(ds, "hjgjjk")
        If ds.Tables(0).Rows.Count <> 0 Then
            DataGridView1.DataSource = ds.Tables(0)

        Else
            MsgBox("aradığınız kriterde firma kaydı yok")
        End If

    End Sub
    Public Sub firmagoster()
        baglan()
        Dim komut As OleDbCommand = New OleDbCommand("select * from firma order by firmaadi asc", c)
        Dim dt As DataTable = New DataTable
        dt.Load(komut.ExecuteReader)
        c.Close()
        DataGridView1.DataSource = dt
    End Sub
    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        ara("select * from firma where firmaadi like '" & TextBox11.Text & "%' order by firmaid asc")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        baglan()
        Dim silid As Integer = (DataGridView1.CurrentRow.Cells(0).Value)
        Dim cevap As DialogResult = MessageBox.Show(DataGridView1.CurrentRow.Cells(1).Value & " Kaydı Silinsin mi?", "FİRMA SİL", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("delete from firma where firmaid=" & silid, c)
            komut.ExecuteNonQuery()
            MsgBox("Firma Kaydı Silindi!")
        End If
        firmagoster()
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBox1.Text = DataGridView1.CurrentRow.Cells(1).Value
    End Sub

    Private Sub TextBox2_GotFocus(sender As Object, e As EventArgs) Handles TextBox2.GotFocus
        TextBox2.SelectAll()
    End Sub
End Class