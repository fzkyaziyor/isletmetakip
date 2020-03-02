Imports System.Data
Imports System.Data.OleDb
Public Class ihalekaydet
    Dim adap, adap1 As New OleDbDataAdapter
    Dim ds, ds1 As New DataSet
    Dim c As OleDbConnection
    Dim baglanti, sorgu As String

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text = "" Then
            MsgBox("Yeni Kurum Adını Girmediniz!")
        Else
            baglan()
            Dim komut As OleDbCommand = New OleDbCommand("INSERT INTO kurumlar (kurumadi) values (@kurumadi)", c)
            komut.Parameters.AddWithValue("@kurumadi", TextBox1.Text)
            komut.ExecuteNonQuery()
            MsgBox("Yeni Kurum Eklendi!")
            Dim eklenen As String = TextBox1.Text
            kurumgoster()
            TextBox2.Text = eklenen
            TextBox1.Text = ""
        End If

    End Sub


    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox10.Text = "" Then
            MsgBox("Yeni Ürün Adını Girmediniz!")
        Else
            baglan()
            Dim komut As OleDbCommand = New OleDbCommand("INSERT INTO ihalemallari (mal) values (@mal)", c)
            komut.Parameters.AddWithValue("@mal", TextBox10.Text)
            komut.ExecuteNonQuery()
            MsgBox("Yeni Ürün Eklendi!")
            Dim eklenen As String = TextBox10.Text
            urunlistele()
            TextBox7.Text = eklenen
            TextBox10.Text = ""
        End If

    End Sub



    Private Sub ihalekaydet_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kurumgoster()
        urunlistele()
        birim()
        DataGridView1.Font = New Font("arial", 10, FontStyle.Bold)
        DataGridView2.Font = New Font("arial", 10, FontStyle.Bold)

    End Sub



    Public Sub kurumgoster()
        baglan()
        'COMBOBOXTA FİRMA İSİMLERİ LİSTELENSİN SEÇİLEN FİRMANIN İD SİNİ ALALIM
        Dim komut As OleDbCommand = New OleDbCommand("select * from kurumlar order by kurumadi asc", c)
        Dim dt As DataTable = New DataTable
        dt.Load(komut.ExecuteReader)
        DataGridView1.DataSource = dt

    End Sub
    Public Sub urunlistele()
        baglan()
        'COMBOBOXTA FİRMA İSİMLERİ LİSTELENSİN SEÇİLEN FİRMANIN İD SİNİ ALALIM
        Dim komut As OleDbCommand = New OleDbCommand("select * from ihalemallari order by mal asc", c)
        Dim dt As DataTable = New DataTable
        dt.Load(komut.ExecuteReader)
        DataGridView2.DataSource = dt

    End Sub
    Public Sub ara(ByVal sorgu As String)
        baglan()
        adap = New OleDbDataAdapter(sorgu, c)
        ds = New DataSet
        adap.Fill(ds, "hjgjjk")
        If ds.Tables(0).Rows.Count <> 0 Then
            DataGridView1.DataSource = ds.Tables(0)

        Else
            MsgBox("Aradığınız Kriterde Kayıt Yok!")
        End If

    End Sub
    Private Sub ara1(ByVal sorgu As String)
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
        ara("select * from kurumlar where kurumadi like '" & TextBox11.Text & "%' order by kurumadi asc")
    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged
        ara1("select * from ihalemallari where mal like '" & TextBox12.Text & "%' order by kurumadi asc")
    End Sub

    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        TextBox5.Text = Val(TextBox6.Text) * Val(TextBox4.Text)
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        baglan()
        Dim silid As Integer = (DataGridView1.CurrentRow.Cells(0).Value)

        Dim cevap As DialogResult = MessageBox.Show("Seçili Kurum Kaydını Sileyim mi?", "SİL", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("delete from kurumlar where kurumid=" & silid, c)
            komut.ExecuteNonQuery()
            MsgBox("Silindi!")
        End If
        kurumgoster()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        baglan()
        Dim silid As Integer = (DataGridView2.CurrentRow.Cells(0).Value)

        Dim cevap As DialogResult = MessageBox.Show("Seçili Ürün Kaydı Silinsin mi?", "SİL", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("delete from ihalemallari where malid=" & silid, c)
            komut.ExecuteNonQuery()
            MsgBox("Silindi!")
        End If
        urunlistele()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        If TextBox2.Text = "" Or TextBox7.Text = "" Then
            MsgBox("Bir Kurum Adı ve/veya Ürün Adi Seçinizi Lütfen!")
        ElseIf TextBox6.Text = "" Or TextBox4.Text = "" Or TextBox5.Text = "" Then
            MsgBox("Boş Alan Bırakmayınız!")

        ElseIf ComboBox1.Text = "Seçiniz…" Then
            MsgBox("Birim Seçiniz")
        Else
            baglan()
            Dim komut As OleDbCommand = New OleDbCommand("INSERT INTO ihale (ihaleadi,kurumadi,mal,birim,miktar,birimfiyat,toplam,ihaletarih,sontarih,aciklama) values (@ihaleadi,@kurumadi,@mal,@birim,@miktar,@birimfiyat,@toplam,@ihaletarih,@sontarih,@aciklama)", c)
            komut.Parameters.AddWithValue("@ihaleadi", TextBox2.Text + " " + TextBox6.Text + " " + TextBox7.Text)
            komut.Parameters.AddWithValue("@kurumadi", TextBox2.Text)
            komut.Parameters.AddWithValue("@mal", TextBox7.Text)
            komut.Parameters.AddWithValue("@birim", ComboBox1.Text)
            komut.Parameters.AddWithValue("@miktar", TextBox6.Text)
            komut.Parameters.AddWithValue("@birimfiyat", TextBox4.Text)
            komut.Parameters.AddWithValue("@toplam", TextBox5.Text)
            komut.Parameters.AddWithValue("@ihaletarih", DateTimePicker1.Value.ToShortDateString)
            komut.Parameters.AddWithValue("@sontarih", DateTimePicker2.Value.ToShortDateString)
            komut.Parameters.AddWithValue("@aciklama", TextBox9.Text)
            komut.ExecuteNonQuery()
            MsgBox("Yeni İhaleniz Eklendi!")

            Dim f2 As ihaletakip = Application.OpenForms("ihaletakip")
            f2.ihalegoster(0)

            menu1.Panel1.Controls.Clear()
            Dim frm2 As ihaletakip = New ihaletakip()
            frm2.TopLevel = False
            menu1.Panel1.Controls.Add(frm2)
            frm2.Show()
            frm2.Dock = DockStyle.Fill
            frm2.BringToFront()
            Me.Close()

        End If




    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
    End Sub



    Public Sub birim()
        baglan()
        'COMBOBOXTA FİRMA İSİMLERİ LİSTELENSİN SEÇİLEN FİRMANIN İD SİNİ ALALIM
        Dim komut As OleDbCommand = New OleDbCommand("select * from birimler", c)
        Dim dt As DataTable = New DataTable
        dt.Load(komut.ExecuteReader)
        ComboBox1.DataSource = dt
        ComboBox1.DisplayMember = "birim"
        ComboBox1.ValueMember = "birimid"
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBox2.Text = DataGridView1.CurrentRow.Cells(1).Value
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        TextBox7.Text = DataGridView2.CurrentRow.Cells(1).Value
    End Sub
End Class