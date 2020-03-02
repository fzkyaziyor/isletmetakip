Imports System.Data
Imports System.Data.OleDb
Imports System.Windows.Forms


Public Class personeltakip
    Dim c As OleDbConnection
    Dim adap As New OleDbDataAdapter
    Dim ds As New DataSet
    'Public id As Integer
    Private Sub personeltakip_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        c = New OleDbConnection(" Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb")
        c.Open()
        personelgoster(0)
        'Dim dosyaYolu As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "UygulamaAdı", "DosyaAdı.txt")
    End Sub

    Public Sub odemegoster()
        Dim perno As Integer = TextBox9.Text
        Dim adap As OleDbDataAdapter = New OleDbDataAdapter("select * from persodeme where perno=" & perno, c)
        ds = New DataSet
        adap.Fill(ds, "odeme")
        DataGridView2.DataSource = ds.Tables("odeme")

        Dim adet As Integer = ds.Tables("odeme").Rows.Count
        Dim sonuc As Double
        For i = 0 To adet - 1
            sonuc = sonuc + DataGridView2.Rows(i).Cells("odenen").Value
        Next
        TextBox10.Text = (sonuc.ToString("C"))
    End Sub
    Public Sub personelgoster(ByVal s As Integer)
        Dim adap As OleDbDataAdapter = New OleDbDataAdapter("select * from personel order by perno desc", c)
        ds = New DataSet
        adap.Fill(ds, "personel")
        DataGridView1.DataSource = ds.Tables(0)

        If ds.Tables(0).Rows.Count <> 0 Then
            TextBox8.Text = DataGridView1.Rows(s).Cells(1).Value.ToString
            TextBox6.Text = DataGridView1.Rows(s).Cells(2).Value.ToString
            TextBox1.Text = DataGridView1.Rows(s).Cells(3).Value.ToString
            TextBox2.Text = DataGridView1.Rows(s).Cells(4).Value.ToString
            TextBox3.Text = DataGridView1.Rows(s).Cells(5).Value.ToString
            TextBox4.Text = DataGridView1.Rows(s).Cells(6).Value.ToString
            TextBox5.Text = DataGridView1.Rows(s).Cells(7).Value.ToString
            TextBox7.Text = DataGridView1.Rows(s).Cells(8).Value.ToString
            TextBox9.Text = DataGridView1.Rows(s).Cells(0).Value.ToString

            Dim yol As String = DataGridView1.Rows(s).Cells(8).Value.ToString
            Try
                PictureBox1.Image = Image.FromFile(yol)
            Catch ex As Exception
                PictureBox1.Image = Nothing
            End Try



            'If yol = "-" Then

            'Else

            'End If

            menu1.lblperno.Text = DataGridView1.Rows(s).Cells(0).Value.ToString
            DataGridView1.Rows(s).Selected = True
        End If
        odemegoster()
    End Sub
    Private Sub DataGridView1_CellMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DataGridView1.CellMouseClick
        Dim aktifsatir As Integer = DataGridView1.CurrentRow.Index.ToString
        Dim ustsatirno As Integer = DataGridView1.FirstDisplayedScrollingRowIndex
        menu1.lblsatirno.Text = aktifsatir
        menu1.lblustsatir.Text = ustsatirno
        personelgoster(menu1.lblsatirno.Text)
        DataGridView1.FirstDisplayedScrollingRowIndex = ustsatirno
        DataGridView1.Rows(aktifsatir).Selected = True
        menu1.lblperno.Text = TextBox9.Text
        odemegoster()
    End Sub

    Private Sub LinkLabel3_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel3.LinkClicked
        Dim frm2 As personelguncelle = New personelguncelle()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Label7.Text = TextBox9.Text
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel2.LinkClicked
        Dim silid As Integer = TextBox9.Text
        'tıkladığımız odemenin id sini çekiyoruz.......
        Dim cevap As DialogResult = MessageBox.Show("bu personnel kaydını silmek istediğinizden emin misiniz?", "PERSONEL SİLME", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("delete from personel where perno=" & silid, c)
            komut.ExecuteNonQuery()
            MsgBox("personel kaydınız silindi")

            Dim cevap1 As DialogResult = MessageBox.Show("bu personele ait ödeme kayıtları da silinsin mi?", "MÜŞTERİ SİLME", MessageBoxButtons.YesNo)
            If cevap1 = DialogResult.Yes Then
                Dim komut1 As OleDbCommand = New OleDbCommand("delete from persodeme where perno=" & silid, c)
                komut1.ExecuteNonQuery()
                MsgBox("Tüm ödeme kayıtları silindi")

            Else
                MsgBox("işleminiz iptal edildi")
            End If
            personelgoster(0)
            DataGridView1.FirstDisplayedScrollingRowIndex = menu1.lblustsatir.Text
            'textboxları boşaltabiliriz
        End If

    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        Dim frm2 As personelkayit = New personelkayit()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
    End Sub

    Private Sub LinkLabel4_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel4.LinkClicked
        Dim frm2 As personeleodemeyap = New personeleodemeyap()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()



    End Sub
End Class