Imports System.Data
Imports System.Data.OleDb
Public Class ihaletakip
    Dim adap As New OleDbDataAdapter
    Dim ds, ds1 As New DataSet
    Dim c As OleDbConnection
    Dim baglanti As String
    Public a As Integer
    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub
    Private Sub ihaletakip_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ihalegoster(0)
        If DataGridView1.Rows.Count <> 0 Then
            Label9.Text = (DataGridView1.CurrentRow.Cells(0).Value)
            hareketgoster(CInt(Label9.Text))
            menu1.lblperno.Text = DataGridView1.CurrentRow.Cells(0).Value
        End If

    End Sub
    Public Sub ihalegoster(ByVal sayi As Integer)
        baglan()
        Dim adap As OleDbDataAdapter = New OleDbDataAdapter("select * from ihale order by ihaleid desc", c)
        ds = New DataSet
        adap.Fill(ds, "musteri")
        DataGridView1.DataSource = ds.Tables(0)



        If ds.Tables(0).Rows.Count <> 0 Then
            Label9.Text = (DataGridView1.CurrentRow.Cells(0).Value)
            Dim kayit As DataRow = ds.Tables(0).Rows(sayi)
            TextBox2.Text = kayit("kurumadi").ToString
            TextBox7.Text = kayit("mal").ToString
            TextBox6.Text = kayit("miktar").ToString
            TextBox1.Text = kayit("birim").ToString
            Dim birim As Double = kayit("birimfiyat").ToString
            TextBox4.Text = (birim.ToString("C"))
            Dim toplam As Double = kayit("toplam").ToString
            TextBox5.Text = (toplam.ToString("C"))

            TextBox3.Text = kayit("ihaletarih").ToString
            TextBox8.Text = kayit("sontarih").ToString
            TextBox9.Text = kayit("aciklama").ToString
        End If

    End Sub
    Public Sub hareketgoster(ByVal sayi As Integer)
        baglan()
        Dim adap1 As OleDbDataAdapter = New OleDbDataAdapter("select * from ihalehareketleri where ihaleid=" & Label9.Text & " order by hareketid desc", c)
        ds1 = New DataSet
        adap1.Fill(ds1, "musteri")
        DataGridView2.DataSource = ds1.Tables(0)
        'toplamverilen bulalım
        Dim adet1 As Integer = ds1.Tables(0).Rows.Count
        Dim sonuc1 As Double
        Dim kayit1 As DataRow
        For i = 0 To adet1 - 1
            kayit1 = ds1.Tables(0).Rows(i)
            sonuc1 = sonuc1 + kayit1("verilen")
        Next
        TextBox11.Text = (sonuc1.ToString)
        TextBox10.Text = TextBox6.Text - TextBox11.Text






    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        baglan()




        Dim komut As OleDbCommand = New OleDbCommand("INSERT INTO ihalehareketleri (verilen,ihaleid,aciklama) values (@verilen,@ihaleid,@aciklama)", c)
        komut.Parameters.AddWithValue("@verilen", NumericUpDown1.Value)
        komut.Parameters.AddWithValue("@ihaleid", Label9.Text)
        komut.Parameters.AddWithValue("@aciklama", TextBox12.Text)


        komut.ExecuteNonQuery()
        MsgBox("Aktif İhalede " & NumericUpDown1.Value & " adet ürün verildiği Güncellendi!")

        hareketgoster(CInt(Label9.Text))


    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        baglan()
        Dim silid As Integer = (DataGridView2.CurrentRow.Cells(0).Value)

        Dim cevap As DialogResult = MessageBox.Show("Seçili Kayıt Silinsin mi?", "SİL", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("delete from ihalehareketleri where hareketid=" & silid, c)
            komut.ExecuteNonQuery()
            MsgBox("Silindi...")
        End If
        hareketgoster(CInt(Label9.Text))

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As ihalekaydet = New ihalekaydet()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        a = Label9.Text
        'menu1.Panel1.Controls.Clear()
        Dim frm2 As ihalegüncelle = New ihalegüncelle()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Label1.Text = a
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Label9.Text = (DataGridView1.CurrentRow.Cells(0).Value)
        menu1.lblperno.Text = DataGridView1.CurrentRow.Cells(0).Value
        baglan()
        adap = New OleDbDataAdapter("select * from ihale where ihaleid=" & Label9.Text & " order by ihaleid desc", c)
        ds = New DataSet
        adap.Fill(ds, "stok")
        Dim kayit As DataRow = ds.Tables(0).Rows(0)

        TextBox2.Text = kayit("kurumadi").ToString
        TextBox7.Text = kayit("mal").ToString
        TextBox6.Text = kayit("miktar").ToString
        TextBox1.Text = kayit("birim").ToString
        Dim birim As Double = kayit("birimfiyat").ToString
        TextBox4.Text = (birim.ToString("C"))
        Dim toplam As Double = kayit("toplam").ToString
        TextBox5.Text = (toplam.ToString("C"))

        TextBox3.Text = kayit("ihaletarih").ToString
        TextBox8.Text = kayit("sontarih").ToString
        TextBox9.Text = kayit("aciklama").ToString


        'hareketleri görüntüleyelim...

        hareketgoster(CInt(Label9.Text))





    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        baglan()
        Dim silid As Integer = (DataGridView1.CurrentRow.Cells(0).Value)

        Dim cevap As DialogResult = MessageBox.Show("Seçili İhaleyi ve Bu İhaleye Ait Diğer Kayıtlar Silinsin mi?", "SİL", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("delete from ihale where ihaleid=" & silid, c)
            komut.ExecuteNonQuery()

            Dim komut1 As OleDbCommand = New OleDbCommand("delete from ihalehareketleri where ihaleid=" & silid, c)
            komut1.ExecuteNonQuery()



            MsgBox("Silindi...")
        End If
        ihalegoster(0)

    End Sub

    Private Sub TextBox9_GotFocus(sender As Object, e As EventArgs) Handles TextBox9.GotFocus
        TextBox9.SelectAll()
    End Sub
End Class