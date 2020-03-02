Imports System.Data
Imports System.Data.OleDb

Public Class ceksenetkayit
    Dim c As OleDbConnection
    Dim baglanti, sorgu1 As String
    Dim adap As New OleDbDataAdapter
    Dim ds As New DataSet

    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub

    Private Sub ceksenetkayit_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        firmaekle()
        DataGridView1.Font = New Font("arial", 9, FontStyle.Bold)

    End Sub

    Public Sub firmaekle()
        baglan()

        Dim adap1 As New OleDbDataAdapter
        Dim ds1 As New DataSet
        sorgu1 = "select * from firma order by firmaadi asc"
        adap1 = New OleDbDataAdapter(sorgu1, c)
        ds1 = New DataSet
        adap1.Fill(ds1, "firmaaaaaaa")
        DataGridView1.DataSource = ds1.Tables(0)




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





    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox2.Text = "" Then
            MsgBox("firma seçmediniz ki!")
        Else
            baglan()
            Dim komut As OleDbCommand = New OleDbCommand("insert 
into ceksenet (firma,tur,aciklama,yazmatarihi,odemetarihi,
tutar,durum) values (@firma,@tur,@aciklama,@yazmatarihi,@odemetarihi,
@tutar,@durum)", c)
            komut.Parameters.AddWithValue("@firma", TextBox2.Text)
            Dim tur As String
            If RadioButton1.Checked Then
                tur = "çek"
            Else
                tur = "senet"
            End If
            komut.Parameters.AddWithValue("@tur", tur)
            komut.Parameters.AddWithValue("@aciklama", tbaciklama.Text)
            komut.Parameters.AddWithValue("@yazmatarihi", DateTimePicker1.Value.ToShortDateString)
            komut.Parameters.AddWithValue("@odemetarihi", DateTimePicker2.Value.ToShortDateString)
            komut.Parameters.AddWithValue("@tutar", tbodenen.Text)
            komut.Parameters.AddWithValue("@durum", "ödenmedi")
            komut.ExecuteNonQuery()
            MsgBox("Yeni Çek/Senet Kaydınız Yapıldı...")


            Dim f2 As ceksenettakip = Application.OpenForms("ceksenettakip")
            f2.ceksenetgoster()

            menu1.Panel1.Controls.Clear()
            Dim frm2 As ceksenettakip = New ceksenettakip()
            frm2.TopLevel = False
            menu1.Panel1.Controls.Add(frm2)
            frm2.Show()
            frm2.Dock = DockStyle.Fill
            frm2.BringToFront()
            Me.Close()

        End If


    End Sub


    Private Sub tbodenen_Leave(sender As Object, e As EventArgs) Handles tbodenen.Leave
        tbodenen.Text = Val(tbodenen.Text).ToString("C")
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBox2.Text = DataGridView1.CurrentRow.Cells(1).Value
    End Sub

    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        ara("select * from firma where firmaadi like '" & TextBox11.Text & "%' order by firmaid asc")
    End Sub

    Private Sub tbodenen_GotFocus(sender As Object, e As EventArgs) Handles tbodenen.GotFocus
        tbodenen.SelectAll()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As ceksenettakip = New ceksenettakip()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
        Me.Close()
    End Sub

    Private Sub ara(ByVal sorgu As String)
        baglan()
        adap = New OleDbDataAdapter(sorgu, c)
        ds = New DataSet
        adap.Fill(ds, "hjgjjk")
        If ds.Tables(0).Rows.Count <> 0 Then
            DataGridView1.DataSource = ds.Tables(0)

        Else
            MsgBox("aradığınız kriterde çek/senet kaydı yok")
        End If

    End Sub
End Class