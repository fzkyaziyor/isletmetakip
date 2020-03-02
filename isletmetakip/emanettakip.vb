Imports System.Data
Imports System.Data.OleDb
Public Class emanettakip
    Dim adap, adap1 As New OleDbDataAdapter
    Dim ds As New DataSet
    Dim c As OleDbConnection
    Dim baglanti, sorgu As String
    Public id As Integer
    Public ney As String
    Public ss, ind As Integer
    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As firmasec = New firmasec()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        baglan()
        If TextBox1.Text = "" Then
            MsgBox("Firma Adını Belirtilmeli")
        Else
            Dim komut As OleDbCommand = New OleDbCommand("insert into emanet (firma,aciklama) values (@firma,@aciklama)", c)

            komut.Parameters.AddWithValue("@firma", TextBox1.Text)
            komut.Parameters.AddWithValue("@aciklama", TextBox2.Text)
            komut.ExecuteNonQuery()

            MsgBox("Emanet Kaydınız Tamamlandı.")
            Dim f2 As emanettakip = Application.OpenForms("emanettakip")
            f2.emanetgoster()
        End If
    End Sub

    Public Sub emanetgoster()
        baglan()
        Dim adap As OleDbDataAdapter = New OleDbDataAdapter("select * from emanet order by id desc", c)
        ds = New DataSet
        adap.Fill(ds, "ceksenettt")
        DataGridView1.DataSource = ds.Tables(0)


    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        baglan()
        Dim silid As Integer = DataGridView1.CurrentRow.Cells(0).Value
        Dim cevap As DialogResult = MessageBox.Show("Seçili Firma Kaydını Sileyim mi?", "FİRMA SİL", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("delete from emanet where id=" & silid, c)
            komut.ExecuteNonQuery()
            MsgBox("Emanet Kaydınız Silindi!")
        End If
        emanetgoster()
    End Sub

    Private Sub emanettakip_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        DataGridView1.Font = New Font("arial", 10, FontStyle.Bold)
        emanetgoster()
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
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        id = DataGridView1.CurrentRow.Cells(0).Value
        ney = DataGridView1.CurrentRow.Cells(3).Value
        Dim f As String = DataGridView1.CurrentRow.Cells(1).Value
        ss = DataGridView1.FirstDisplayedScrollingRowIndex



        menu1.Panel1.Controls.Clear()
        Dim frm2 As emanetguncelle = New emanetguncelle()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Label12.Text = id
        frm2.TextBox2.Text = ney
        frm2.TextBox1.Text = f
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()

    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        emanetgoster()
    End Sub

    Private Sub TextBox2_GotFocus(sender As Object, e As EventArgs) Handles TextBox2.GotFocus
        TextBox2.SelectAll()
    End Sub
    Private Sub ara(ByVal sorgu As String)
        baglan()
        adap = New OleDbDataAdapter(sorgu, c)
        ds = New DataSet
        adap.Fill(ds, "hjgjjk")
        If ds.Tables(0).Rows.Count <> 0 Then
            DataGridView2.DataSource = ds.Tables(0)

        Else
            MsgBox("Aradığınız Kriterde Kayıt Yok!")
        End If

    End Sub
    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        ara("select * from firma where firmaadi like '" & TextBox11.Text & "%' order by firmaid asc")
    End Sub



    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        Dim sonsatir As Integer = DataGridView1.FirstDisplayedScrollingRowIndex
        Label12.Text = sonsatir
        Label2.Text = DataGridView1.FirstDisplayedScrollingRowIndex
    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick

        Dim firma As String = (DataGridView1.CurrentRow.Cells(1).Value)

        Dim sorgu As String = "select * from emanet where firma='" + firma + "' order by id desc"
        adap = New OleDbDataAdapter(sorgu, c)
        Dim ds As New DataSet
        adap.Fill(ds, "lklşk")
        DataGridView1.DataSource = ds.Tables(0)



    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        TextBox1.Text = DataGridView2.CurrentRow.Cells(1).Value
    End Sub
End Class