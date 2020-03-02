Imports System.Data
Imports System.Data.OleDb
Public Class ihalegüncelle
    Dim adap As New OleDbDataAdapter
    Dim ds, ds1 As New DataSet
    Dim c As OleDbConnection
    Dim baglanti As String
    Dim komut As OleDbCommand
    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As ihaletakip = New ihaletakip()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()

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
    Public Sub birim()

    End Sub
    Public Sub birimlistele()
        baglan()
        'COMBOBOXTA FİRMA İSİMLERİ LİSTELENSİN SEÇİLEN FİRMANIN İD SİNİ ALALIM
        Dim komut As OleDbCommand = New OleDbCommand("select * from birimler", c)
        Dim dt As DataTable = New DataTable
        dt.Load(komut.ExecuteReader)
        ComboBox1.DataSource = dt
        ComboBox1.DisplayMember = "birim"
        ComboBox1.ValueMember = "birimid"
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
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

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If TextBox10.Text = "" Then
            MsgBox("Yeni Kurum Adını Girmediniz!")
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

    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        ara("select * from kurumlar where kurumadi like '" & TextBox11.Text & "%'")
    End Sub

    Private Sub TextBox12_TextChanged(sender As Object, e As EventArgs) Handles TextBox12.TextChanged
        ara1("select * from ihalemallari where mal like '" & TextBox12.Text & "%'")
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

    Private Sub ara1(ByVal sorgu As String)
        baglan()
        adap = New OleDbDataAdapter(sorgu, c)
        ds = New DataSet
        adap.Fill(ds, "hjgjjk")
        If ds.Tables(0).Rows.Count <> 0 Then
            DataGridView2.DataSource = ds.Tables(0)

        Else
            MsgBox("aradığınız kriterde çek/senet kaydı yok")
        End If

    End Sub
    Private Sub TextBox4_TextChanged(sender As Object, e As EventArgs) Handles TextBox4.TextChanged
        TextBox5.Text = Val(TextBox6.Text) * Val(TextBox4.Text)
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        TextBox5.Text = Val(TextBox6.Text) * Val(TextBox4.Text)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        baglan()
        Dim id As Integer = Label1.Text
        Dim komut As OleDbCommand = New OleDbCommand("update ihale set ihaleadi=@ihaleadi,kurumadi=@kurumadi,mal=@mal,birim=@birim,miktar=@miktar,birimfiyat=@birimfiyat,toplam=@toplam,ihaletarih=@ihaletarih,sontarih=sontarih,aciklama=@aciklama where ihaleid=" & id, c)
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
        MsgBox("ihaleniz Güncellendi!")

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
    End Sub

    Private Sub ihalegüncelle_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        kurumgoster()
        urunlistele()
        birimlistele()


        baglan()
        Dim id As Integer = Val(menu1.lblperno.Text)
        adap = New OleDbDataAdapter("select * from ihale where ihaleid=" & id & " order by ihaleid desc", c)
        ds = New DataSet
        adap.Fill(ds, "çeksenet")
        Dim kayit As DataRow = ds.Tables(0).Rows(0)
        TextBox2.Text = kayit("kurumadi").ToString
        TextBox7.Text = kayit("mal").ToString
        TextBox6.Text = kayit("miktar").ToString

        Dim birim As Double = kayit("birimfiyat").ToString
        TextBox4.Text = (birim.ToString("C"))
        Dim toplam As Double = kayit("toplam").ToString
        TextBox5.Text = (toplam.ToString("C"))

        DateTimePicker1.Value = kayit("ihaletarih").ToString
        DateTimePicker2.Value = kayit("sontarih").ToString
        TextBox9.Text = kayit("aciklama").ToString
        ComboBox1.Text = kayit("birim").ToString
        DataGridView1.Font = New Font("arial", 10, FontStyle.Bold)
        DataGridView2.Font = New Font("arial", 10, FontStyle.Bold)



    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBox2.Text = DataGridView1.CurrentRow.Cells(1).Value
    End Sub

    Private Sub DataGridView2_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView2.CellClick
        TextBox7.Text = DataGridView2.CurrentRow.Cells(1).Value
    End Sub
End Class