Imports System.Data
Imports System.Data.OleDb
Public Class stoktakip
    Dim adap, adap1 As New OleDbDataAdapter
    Dim ds, ds1 As New DataSet
    Dim c As OleDbConnection
    Dim baglanti, sorgu As String

    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub




    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As stokamalal = New stokamalal()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
        frm2.TextBox1.Text = Label4.Text
    End Sub

    Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Public Sub bul(ByVal sorgum As String)
        baglan()
        adap = New OleDbDataAdapter(sorgum, c)
        ds = New DataSet
        adap.Fill(ds, "stok")

        If ds.Tables(0).Rows.Count <> 0 Then
            DataGridView1.DataSource = ds.Tables(0)
            '<<<<<<<<<tabloyu toplayacak>>>>>>>>>>>
            Dim miktar As Double
            Dim adet As Integer
            Dim sonuc As Double
            Dim kayit As DataRow

            adet = ds.Tables(0).Rows.Count   'kayıt sayısı
            For i = 0 To adet - 1
                kayit = ds.Tables(0).Rows(i)
                miktar = kayit("toplamtutar")
                sonuc = sonuc + miktar
            Next
            TextBox2.Text = (sonuc.ToString("C"))

            Dim toplamborc As Double = TextBox2.Text
            Dim toplamodeme As Double = TextBox3.Text
            TextBox4.Text = (toplamborc - toplamodeme).ToString("C")

            '<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        Else
            MsgBox("aradığın tarihte kayıt yok")

        End If
    End Sub
    Public Sub bul2(ByVal sorgum As String)
        baglan()
        adap = New OleDbDataAdapter(sorgum, c)
        ds = New DataSet
        adap.Fill(ds, "stok")

        If ds.Tables(0).Rows.Count <> 0 Then
            DataGridView2.DataSource = ds.Tables(0)
            '<<<<<<<<<tabloyu toplayacak>>>>>>>>>>>
            Dim miktar As Double
            Dim adet As Integer
            Dim sonuc As Double
            Dim kayit As DataRow

            adet = ds.Tables(0).Rows.Count   'kayıt sayısı
            For i = 0 To adet - 1
                kayit = ds.Tables(0).Rows(i)
                miktar = kayit("odenentutar")
                sonuc = sonuc + miktar
            Next
            TextBox2.Text = (sonuc.ToString("C"))
            '<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            Dim toplamborc As Double = TextBox2.Text
            Dim toplamodeme As Double = TextBox3.Text
            TextBox4.Text = (toplamborc - toplamodeme).ToString("C")

        Else
            MsgBox("aradığın tarihte kayıt yok")

        End If
    End Sub

    Private Sub stoktakip_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        urungoster()
        Dim cell_style As DataGridViewCellStyle = New DataGridViewCellStyle()
        cell_style.BackColor = Color.LightGreen
        cell_style.Alignment = DataGridViewContentAlignment.MiddleRight
        cell_style.Format = "C"
        DataGridView2.Columns(3).DefaultCellStyle = cell_style

        Dim cell_style1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        cell_style1.BackColor = Color.LightGreen
        cell_style1.Alignment = DataGridViewContentAlignment.MiddleRight
        cell_style1.Format = "C"
        DataGridView1.Columns(1).DefaultCellStyle = cell_style1
        Label6.Text = DataGridView1.CurrentRow.Cells(0).Value

        Dim sonsatir As Integer = DataGridView1.FirstDisplayedScrollingRowIndex
        Dim sonsatir1 As Integer = DataGridView2.FirstDisplayedScrollingRowIndex
        Label1.Text = sonsatir
        Label2.Text = DataGridView1.CurrentRow.Index
        Label6.Text = DataGridView1.CurrentRow.Cells(0).Value

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        baglan()
        Dim adap As OleDbDataAdapter = New OleDbDataAdapter("select * from aldigimmal where firma like '" & TextBox1.Text & "%' order by islemid desc", c)

        Dim ds As New DataSet
        adap.Fill(ds, "pers")

        If ds.Tables(0).Rows.Count <> 0 Then

            DataGridView1.DataSource = ds.Tables(0)
            Dim toplamborc As Double = TextBox2.Text
            Dim toplamodeme As Double = TextBox3.Text
            TextBox4.Text = (toplamborc - toplamodeme).ToString("C")

        Else
            MsgBox("ARADIĞINIZ KRİTERDE BİR FİRMA KAYDI YOK")
        End If
    End Sub

    Public Sub urungoster()
        baglan()
        Dim adap As OleDbDataAdapter = New OleDbDataAdapter("select * from aldigimmal order by islemid desc", c)
        ds = New DataSet
        adap.Fill(ds, "stok")
        DataGridView1.DataSource = ds.Tables(0)
        Label4.Text = (DataGridView1.CurrentRow.Cells(3).Value)


        Dim adet As Integer = ds.Tables(0).Rows.Count
        Dim sonuc As Double
        Dim kayit As DataRow
        For i = 0 To adet - 1
            kayit = ds.Tables(0).Rows(i)
            sonuc = sonuc + kayit("toplamtutar")
        Next
        TextBox2.Text = (sonuc.ToString("C"))


        Dim adap1 As OleDbDataAdapter = New OleDbDataAdapter("select * from odeme where firma='" + Label4.Text + "' order by sirano desc", c)
        ds1 = New DataSet
        adap1.Fill(ds1, "stok")
        DataGridView2.DataSource = ds1.Tables(0)

        Dim adet1 As Integer = ds1.Tables(0).Rows.Count
        Dim sonuc1 As Double
        Dim kayit1 As DataRow
        For i = 0 To adet1 - 1
            kayit1 = ds1.Tables(0).Rows(i)
            sonuc1 = sonuc1 + kayit1("odenentutar")
        Next
        TextBox3.Text = (sonuc1.ToString("C"))

        Dim toplamborc As Double = TextBox2.Text
        Dim toplamodeme As Double = TextBox3.Text
        TextBox4.Text = (toplamborc - toplamodeme).ToString("C")



    End Sub



    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub










    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As firmayaodemeyap = New firmayaodemeyap()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
        frm2.TextBox1.Text = Label4.Text
    End Sub

    Private Sub TextBox1_TextChanged_1(sender As Object, e As EventArgs) Handles TextBox1.TextChanged
        bul("select * from aldigimmal where firma like '" & TextBox1.Text & "%'")
    End Sub




    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim tarih1, tarih2 As String

        tarih1 = DateTimePicker3.Value.ToShortDateString


        tarih2 = DateTimePicker4.Value.ToShortDateString


        bul2("select * from odeme where firma='" + Label4.Text + "' and CDATE([odemetarihi]) BETWEEN CDATE('" & tarih1 & "') AND CDATE('" & tarih2 & "') order by sirano desc")


    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim tarih1, tarih2 As String

        tarih1 = DateTimePicker3.Value.ToShortDateString


        tarih2 = DateTimePicker4.Value.ToShortDateString


        bul2("select * from odeme where CDATE([odemetarihi]) BETWEEN CDATE('" & tarih1 & "') AND CDATE('" & tarih2 & "')")

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim tarih1, tarih2 As String

        tarih1 = DateTimePicker1.Value.ToShortDateString


        tarih2 = DateTimePicker2.Value.ToShortDateString


        bul("select * from aldigimmal where CDATE([aldigimtarih]) BETWEEN CDATE('" & tarih1 & "') AND CDATE('" & tarih2 & "')")
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        urungoster()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim adap1 As OleDbDataAdapter = New OleDbDataAdapter("select * from odeme order by sirano desc", c)
        ds1 = New DataSet
        adap1.Fill(ds1, "stok")
        DataGridView2.DataSource = ds1.Tables(0)

        Dim adet1 As Integer = ds1.Tables(0).Rows.Count
        Dim sonuc1 As Double
        Dim kayit1 As DataRow
        For i = 0 To adet1 - 1
            kayit1 = ds1.Tables(0).Rows(i)
            sonuc1 = sonuc1 + kayit1("odenentutar")
        Next
        TextBox3.Text = (sonuc1.ToString("C"))

        Dim toplamborc As Double = TextBox2.Text
        Dim toplamodeme As Double = TextBox3.Text
        TextBox4.Text = (toplamborc - toplamodeme).ToString("C")

    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        baglan()
        Dim silid As Integer = (DataGridView1.CurrentRow.Cells(0).Value)


        Dim cevap As DialogResult = MessageBox.Show("Seçili Ürün Alma Kaydı Silinsin mi?", "SİL", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("delete from aldigimmal where islemid=" & silid, c)
            komut.ExecuteNonQuery()
            MsgBox("Silindi!")
        End If
        urungoster()

        DataGridView1.Rows(Label2.Text).Selected = True
        DataGridView1.FirstDisplayedScrollingRowIndex = Label1.Text

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim firma As String = Label4.Text
        baglan()
        Dim silid As Integer = (DataGridView2.CurrentRow.Cells(0).Value)

        Dim cevap As DialogResult = MessageBox.Show("Seçili Ödeme Kaydınız Silinsin mi?", "SİL", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("delete from odeme where sirano=" & silid, c)
            komut.ExecuteNonQuery()
            MsgBox("Silindi!")
        End If
        urungoster()
        Dim sorgu1 As String = "select * from odeme where firma='" + firma + "' order by sirano desc"
        adap1 = New OleDbDataAdapter(sorgu1, c)
        Dim ds1 As New DataSet
        adap1.Fill(ds1, "lklşk")
        DataGridView2.DataSource = ds1.Tables(0)

        Dim adet As Integer = ds1.Tables(0).Rows.Count
        Dim sonuc As Double
        Dim kayit1 As DataRow

        For i = 0 To adet - 1
            kayit1 = ds1.Tables(0).Rows(i)
            sonuc = sonuc + kayit1("odenentutar")
        Next

        TextBox3.Text = (sonuc.ToString("C"))
    End Sub



    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        Dim tarih1, tarih2 As String

        tarih1 = DateTimePicker1.Value.ToShortDateString


        tarih2 = DateTimePicker2.Value.ToShortDateString


        bul("select * from aldigimmal where firma='" + Label4.Text + "' and CDATE([aldigimtarih]) BETWEEN CDATE('" & tarih1 & "') AND CDATE('" & tarih2 & "')")
    End Sub





    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        Dim sonsatir As Integer = DataGridView1.FirstDisplayedScrollingRowIndex
        Dim sonsatir1 As Integer = DataGridView2.FirstDisplayedScrollingRowIndex
        Label1.Text = sonsatir
        Label2.Text = DataGridView1.CurrentRow.Index
        Label6.Text = DataGridView1.CurrentRow.Cells(0).Value


        Label4.Text = (DataGridView1.CurrentRow.Cells(3).Value)
        Dim firma As String = (DataGridView1.CurrentRow.Cells(3).Value)
        Dim sorgu1 As String = "select * from odeme where firma='" + firma + "' order by sirano desc"
        adap1 = New OleDbDataAdapter(sorgu1, c)
        Dim ds1 As New DataSet
        adap1.Fill(ds1, "lklşk")
        DataGridView2.DataSource = ds1.Tables(0)

        Dim adet As Integer = ds1.Tables(0).Rows.Count
        Dim sonuc As Double
        Dim kayit1 As DataRow

        For i = 0 To adet - 1
            kayit1 = ds1.Tables(0).Rows(i)
            sonuc = sonuc + kayit1("odenentutar")
        Next

        TextBox3.Text = (sonuc.ToString("C"))
        Dim toplamborc As Double = TextBox2.Text
        Dim toplamodeme As Double = TextBox3.Text
        TextBox4.Text = (toplamborc - toplamodeme).ToString("C")






    End Sub

    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim firma As String = (DataGridView1.CurrentRow.Cells(3).Value)

        Dim sorgu As String = "select * from aldigimmal where firma='" + firma + "' order by islemid desc"
        adap = New OleDbDataAdapter(sorgu, c)
        Dim ds As New DataSet
        adap.Fill(ds, "lklşk")
        DataGridView1.DataSource = ds.Tables(0)

        Dim adet As Integer = ds.Tables(0).Rows.Count
        Dim sonuc As Double
        Dim kayit As DataRow

        For i = 0 To adet - 1
            kayit = ds.Tables(0).Rows(i)
            sonuc = sonuc + kayit("toplamtutar")
        Next

        TextBox2.Text = (sonuc.ToString("C"))
        Dim toplamborc As Double = TextBox2.Text
        Dim toplamodeme As Double = TextBox3.Text
        TextBox4.Text = (toplamborc - toplamodeme).ToString("C")

    End Sub
End Class