Imports System.Data
Imports System.Data.OleDb
Public Class musteritakip
    Dim adap1, adap2, adap3 As New OleDbDataAdapter
    Dim ds1, ds2, ds3 As New DataSet
    Public id As Integer
    Public ad, soyad As String
    Public Sub musteritakip_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        musteriyenile(0)
        Dim cell_style1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        cell_style1.BackColor = Color.LightGreen
        cell_style1.Alignment = DataGridViewContentAlignment.MiddleRight
        cell_style1.Format = "C"
        DataGridView2.Columns(2).DefaultCellStyle = cell_style1

        Dim cell_style As DataGridViewCellStyle = New DataGridViewCellStyle()
        cell_style.BackColor = Color.LightGreen
        cell_style.Alignment = DataGridViewContentAlignment.MiddleRight
        cell_style.Format = "C"
        DataGridView3.Columns(2).DefaultCellStyle = cell_style
    End Sub

    Dim c As OleDbConnection
    Dim baglanti, sorgu As String

    Public Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        id = Label9.Text
        ad = Label3.Text
        soyad = Label2.Text

        menu1.Panel1.Controls.Clear()
        Dim frm2 As musteriodemesi = New musteriodemesi()


        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Label2.Text = id
        frm2.Label1.Text = ad + "  " + soyad
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()



    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        baglan()
        Dim adap1 As OleDbDataAdapter = New OleDbDataAdapter("select * from musteriler where (adi) like '%" & TextBox1.Text & "%' or (soyadi) like '%" & TextBox1.Text & "%'", c)
        Dim ds1 As New DataSet
        adap1.Fill(ds1, "pers")
        If ds1.Tables(0).Rows.Count <> 0 Then
            DataGridView1.DataSource = ds1.Tables(0)
        Else
            MsgBox("ARADIĞINIZ KRİTERDE BİR MÜŞTERİ KAYDI YOK")
        End If

    End Sub

    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub



    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        id = Label9.Text
        ad = Label3.Text
        soyad = Label2.Text

        menu1.Panel1.Controls.Clear()
        Dim frm2 As veresiyeyaz = New veresiyeyaz()


        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Label2.Text = id
        frm2.Label1.Text = ad + "  " + soyad
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()



    End Sub
    Public Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As musteriekle = New musteriekle()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        baglan()
        Dim silid As Integer = DataGridView1.CurrentRow.Cells(0).Value

        'tıkladığımız odemenin id sini çekiyoruz.......
        Dim cevap As DialogResult = MessageBox.Show("bu müşteri kaydını silmek istediğinizden emin misiniz?", "MÜŞTERİ SİLME", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("delete from musteriler where musterino=" & silid, c)
            komut.ExecuteNonQuery()
            MsgBox("müşteri kaydınız silindi")

            Dim cevap1 As DialogResult = MessageBox.Show("bu müşteriye ait borç ve veresiye kayıtlarıda silinsin mi?", "MÜŞTERİ SİLME", MessageBoxButtons.YesNo)
            If cevap1 = DialogResult.Yes Then
                Dim komut1 As OleDbCommand = New OleDbCommand("delete from musteriodeme where musterino=" & silid, c)
                komut1.ExecuteNonQuery()

                Dim komut2 As OleDbCommand = New OleDbCommand("delete from musteriborc where musterino=" & silid, c)
                komut2.ExecuteNonQuery()

                MsgBox("Tüm kayıtları silindi")

                musteriyenile(0)
            End If
        Else
            MsgBox("işleminiz iptal edildi")


        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        baglan()

        Dim komut1 As OleDbCommand = New OleDbCommand("delete from musteriodeme where odemeid=" & DataGridView2.CurrentRow.Cells(0).Value, c)
        komut1.ExecuteNonQuery()
        MsgBox("Ödeme kaydınız silindi...")
        musteriyenile(0)


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        baglan()
        Dim komut1 As OleDbCommand = New OleDbCommand("delete from musteriborc where veresiyeid=" & DataGridView3.CurrentRow.Cells(0).Value, c)
        komut1.ExecuteNonQuery()
        MsgBox("Borç kaydınız silindi...")
        musteriyenile(0)
    End Sub

    Public Sub musteriyenile(ByVal sayi As Integer)
        baglan()
        'müşterileri datagridview1 e çekelim
        Dim adap1 As OleDbDataAdapter = New OleDbDataAdapter("select * from musteriler order by soyadi asc", c)
        ds1 = New DataSet
        adap1.Fill(ds1, "musteri")
        DataGridView1.DataSource = ds1.Tables(0)

        If ds1.Tables(0).Rows.Count <> 0 Then
            Dim kayit As DataRow = ds1.Tables(0).Rows(sayi)
            Label3.Text = kayit("adi").ToString
            Label2.Text = kayit("soyadi").ToString
            TextBox6.Text = kayit("adres").ToString
            TextBox5.Text = kayit("notlar").ToString
            Label9.Text = kayit("musterino").ToString
            TextBox8.Text = kayit("tel").ToString
        End If
        odemegoster()
        borcgoster()
    End Sub
    Function tiklanankayit(ByVal ds As DataSet) As DataRow
        'tıklanan satır kayıt değişkenine atılacak
        Dim kayit As DataRow
        Dim numara As CurrencyManager
        numara = Me.BindingContext(ds.Tables(0))
        kayit = ds.Tables(0).Rows.Item(numara.Position)
        Return kayit
    End Function
    Private Sub DataGridView1_CurrentCellChanged(sender As Object, e As EventArgs) Handles DataGridView1.CurrentCellChanged
        Dim kayit As DataRow = tiklanankayit(ds1)
        Label3.Text = kayit("adi").ToString
        Label2.Text = kayit("soyadi").ToString
        TextBox6.Text = kayit("adres").ToString
        TextBox5.Text = kayit("notlar").ToString
        Label9.Text = kayit("musterino").ToString
        TextBox8.Text = kayit("tel").ToString
        odemegoster()
        borcgoster()
    End Sub

    Public Sub odemegoster()

        Dim musterino As Integer = Val(Label9.Text)
        Dim ds2 As New DataSet
        Dim adap2 As OleDbDataAdapter = New OleDbDataAdapter("select * from musteriodeme where musterino=" & musterino & " order by odemeid desc", c)
        adap2.Fill(ds2, "odeme")
        DataGridView2.DataSource = ds2.Tables(0)
        'toplamı bulalım
        Dim adet As Integer = ds2.Tables(0).Rows.Count
        Dim sonuc As Double
        Dim kayit1 As DataRow
        For i = 0 To adet - 1
            kayit1 = ds2.Tables(0).Rows(i)
            sonuc = sonuc + kayit1("odemetutari")
        Next
        TextBox10.Text = (sonuc.ToString("C"))

      
    End Sub
    Public Sub borcgoster()
        Dim musterino As Integer = Label9.Text
        Dim ds3 As New DataSet
        Dim adap3 As OleDbDataAdapter = New OleDbDataAdapter("select * from musteriborc where musterino=" & musterino & " order by veresiyeid desc", c)
        adap3.Fill(ds3, "odeme")
        DataGridView3.DataSource = ds3.Tables(0)

        Dim adet As Integer = ds3.Tables(0).Rows.Count
        Dim sonuc As Double
        Dim kayit1 As DataRow

        For i = 0 To adet - 1
            kayit1 = ds3.Tables(0).Rows(i)
            sonuc = sonuc + kayit1("toplamtutar")
        Next
        TextBox3.Text = (sonuc.ToString("C"))
        'kalan borcu yazdıralım
        Dim toplamborc As Double = TextBox3.Text
        Dim toplamodeme As Double = TextBox10.Text
        TextBox4.Text = (toplamborc - toplamodeme).ToString("C")


    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim sn As Integer = DataGridView1.FirstDisplayedScrollingRowIndex
        Dim musno As String = DataGridView1.CurrentRow.Index
        musteriyenile(musno)
        DataGridView1.Rows(musno).Selected = True
        DataGridView1.FirstDisplayedScrollingRowIndex = sn

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        Dim kayit As DataRow = tiklanankayit(ds1)
        Label3.Text = kayit("adi").ToString
        Label2.Text = kayit("soyadi").ToString
        TextBox6.Text = kayit("adres").ToString
        TextBox5.Text = kayit("notlar").ToString
        Label9.Text = kayit("musterino").ToString
        TextBox8.Text = kayit("tel").ToString
        odemegoster()
        borcgoster()
    End Sub
End Class