Imports System.Data
Imports System.Data.OleDb
Imports System.Collections
Imports System.Drawing.Printing

Public Class ceksenettakip
    Dim adap, adap1 As New OleDbDataAdapter
    Dim ds As New DataSet
    Dim c As OleDbConnection
    Dim baglanti, sorgu As String

    Dim strFormat As StringFormat
    Dim arrColumnLefts As ArrayList = New ArrayList()
    Dim arrColumnWidths As ArrayList = New ArrayList()
    Dim iCellHeight As Integer = 0
    Dim iTotalWidth As Integer = 0
    Dim iRow As Integer = 0
    Dim bFirstPage As Boolean = False
    Dim bNewPage As Boolean = False
    Dim iHeaderHeight As Integer = 0
    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub
    Public Sub ceksenetgoster()
        baglan()
        Dim adap As OleDbDataAdapter = New OleDbDataAdapter("select * from ceksenet order by ceksenetno desc", c)
        ds = New DataSet
        adap.Fill(ds, "ceksenettt")
        DataGridView1.DataSource = ds.Tables(0)





        'toplam tutarı yazdıralım
        Dim adet As Integer = ds.Tables(0).Rows.Count
        Dim sonuc As Double
        Dim kayit As DataRow
        For i = 0 To adet - 1
            kayit = ds.Tables(0).Rows(i)
            sonuc = sonuc + kayit("tutar")
        Next
        TextBox7.Text = (sonuc.ToString("C"))




        'For i As Integer = 0 To DataGridView1.Rows.Count - 1 - 1
        '    Application.DoEvents()
        '    Dim renk As DataGridViewCellStyle = New DataGridViewCellStyle()

        '    If Convert.ToInt32(DataGridView1.Rows(i).Cells(0).Value) = 16 Then

        '        renk.BackColor = Color.YellowGreen
        '    Else
        '        renk.BackColor = Color.Red
        '        renk.ForeColor = Color.White
        '    End If

        '    DataGridView1.Rows(i).DefaultCellStyle = renk
        'Next






    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ara("select * from ceksenet where durum='ÖDENDİ' order by ceksenetno desc")
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        ara("select * from ceksenet where durum='ÖDENMEDİ' order by ceksenetno desc")
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        ara("select * from ceksenet order by ceksenetno desc")
    End Sub


    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged
        ara("select * from ceksenet where firma like '" & TextBox6.Text & "%' order by ceksenetno desc")
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click

        'ara("select * from ceksenet where (cdate([odemetarihi])) between cdate('" & DateTimePicker1.Value.ToShortDateString & "') and cdate('" & DateTimePicker2.Value.ToShortDateString & "')")

        'baglan()
        'Dim ilk, son As Date
        'ilk = DateTimePicker1.Value.ToShortDateString
        'son = DateTimePicker2.Value.ToShortDateString
        'MsgBox(ilk)
        'MsgBox(son)
        'Dim komut As OleDbCommand = New OleDbCommand("select * from ceksenet where odemetarihi between @tarih1 AND @tarih", c)
        'komut.Parameters.AddWithValue("@tarih1", ilk)
        'komut.Parameters.AddWithValue("@tarih2", son)
        'Dim adap As OleDbDataAdapter = New OleDbDataAdapter(komut)
        'Dim tablo As DataTable = New DataTable
        'adap.Fill(tablo)
        'DataGridView1.DataSource = tablo

        Dim tarih1, tarih2 As String

        tarih1 = DateTimePicker1.Value.ToShortDateString

        tarih2 = DateTimePicker2.Value.ToShortDateString


        bul("select * from ceksenet where CDATE([odemetarihi]) BETWEEN CDATE('" & tarih1 & "') AND CDATE('" & tarih2 & "') order by ceksenetno desc")




    End Sub
    Public Sub bul(ByVal sorgum As String)
        baglan()
        sorgu = sorgum
        adap = New OleDbDataAdapter(sorgu, c)
        ds = New DataSet
        adap.Fill(ds, "çeksenet")

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
                miktar = kayit("tutar")
                sonuc = sonuc + miktar
            Next
            TextBox7.Text = (sonuc.ToString("###,###,###. TL"))
            '<<<<<<<<<<<<<<<<<<<<<>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
        Else
            MsgBox("aradığın kriterde kayıt yok")
        End If
    End Sub
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        baglan()
        'Dim kayit As DataRow
        'Dim numara As CurrencyManager
        'numara = Me.BindingContext(ds.Tables(0))
        'kayit = ds.Tables(0).Rows.Item(numara.Position)
        Dim silid As Integer = (DataGridView1.CurrentRow.Cells(0).Value)

        Dim cevap As DialogResult = MessageBox.Show("Seçili Kayıt Silinsin mi?", "ÇEK SENET SİL", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("delete from ceksenet where ceksenetno=" & silid, c)
            komut.ExecuteNonQuery()
            MsgBox("Silindi!")
        End If
        ceksenetgoster()
    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click
        baglan()
        'Dim kayit As DataRow
        'Dim numara As CurrencyManager
        'numara = Me.BindingContext(ds.Tables(0))
        'kayit = ds.Tables(0).Rows.Item(numara.Position)
        Dim odeid As Integer = (DataGridView1.CurrentRow.Cells(0).Value)




        Dim cevap As DialogResult = MessageBox.Show("Seçili Kayıt ÖDENDİ olarak Güncellensin mi?", "ÇEK SENET ÖDE", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("update ceksenet set durum='ödendi' where ceksenetno=" & odeid, c)
            komut.ExecuteNonQuery()
            MsgBox("Ödendi!")
        End If
        ceksenetgoster()

        Dim a As Integer = Label1.Text
        DataGridView1.FirstDisplayedScrollingRowIndex = a
        DataGridView1.Rows(Val(Label2.Text)).Selected = True


    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim firma, tur, durum As String
        firma = Label3.Text
        durum = "ödendi"

        If RadioButton1.Checked Then
            tur = "çek"
            ara("select * from ceksenet where firma='" & firma & "' and durum='" & durum & "' and tur='" & tur & "'order by ceksenetno desc")
        End If
        If RadioButton2.Checked Then
            tur = "senet"
            ara("select * from ceksenet where firma='" & firma & "' and durum='" & durum & "' and tur='" & tur & "'order by ceksenetno desc")
        End If

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim firma, tur, durum As String
        firma = Label3.Text
        durum = "ödenmedi"

        If RadioButton1.Checked Then
            tur = "çek"
            ara("select * from ceksenet where firma='" & firma & "' and durum='" & durum & "' and tur='" & tur & "' order by ceksenetno desc")
        End If
        If RadioButton2.Checked Then
            tur = "senet"
            ara("select * from ceksenet where firma='" & firma & "' and durum='" & durum & "' and tur='" & tur & "' order by ceksenetno desc")
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As ceksenetkayit = New ceksenetkayit()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
    End Sub

    Private Sub ceksenettakip_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        DataGridView1.Font = New Font("arial", 9, FontStyle.Bold)
        baglan()
        If menu1.ceksenetvar = 1 Then
            ceksenetgoster()
        Else
            Dim sonodemetarihi, bugun As Date
            bugun = Now.Date.ToShortDateString
            sonodemetarihi = DateAdd(DateInterval.Day, 3, bugun)

            Dim komut As OleDbCommand = New OleDbCommand("select * from ceksenet where odemetarihi between @tarih1 AND @tarih and durum='ödenmedi' order by ceksenetno desc", c)
            komut.Parameters.AddWithValue("@tarih1", bugun)
            komut.Parameters.AddWithValue("@tarih2", sonodemetarihi)
            Dim adap As OleDbDataAdapter = New OleDbDataAdapter(komut)
            Dim tablo As DataTable = New DataTable
            adap.Fill(tablo)
            DataGridView1.DataSource = tablo

            Dim sonsatir As Integer = DataGridView1.FirstDisplayedScrollingRowIndex
            Label1.Text = sonsatir
            Label2.Text = DataGridView1.CurrentRow.Index


        End If



        If DataGridView1.Rows.Count <> 0 Then
            Label3.Text = (DataGridView1.CurrentRow.Cells(1).Value)

        End If

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        baglan()
        Dim odeid As Integer = (DataGridView1.CurrentRow.Cells(0).Value)
        Dim cevap As DialogResult = MessageBox.Show("Seçili Kaydınız Tekrar ÖDENMEDİ olarak değişecek!", "ÇEK SENET ÖDE", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("update ceksenet set durum='ödenmedi' where ceksenetno=" & odeid, c)
            komut.ExecuteNonQuery()
            MsgBox("Değiştirildi!")
        End If
        ceksenetgoster()

        Dim a As Integer = Label1.Text
        DataGridView1.FirstDisplayedScrollingRowIndex = a
        DataGridView1.Rows(Val(Label2.Text)).Selected = True

    End Sub



    Private Sub ara(ByVal sorgu As String)
        baglan()
        adap = New OleDbDataAdapter(sorgu, c)
        ds = New DataSet
        adap.Fill(ds, "hjgjjk")
        If ds.Tables(0).Rows.Count <> 0 Then
            DataGridView1.DataSource = ds.Tables(0)
            'datagridview1 de goster ve tutarı topla
            Dim adet As Integer = ds.Tables(0).Rows.Count
            Dim sonuc As Double
            Dim kayit As DataRow
            For i = 0 To adet - 1
                kayit = ds.Tables(0).Rows(i)
                sonuc = sonuc + kayit("tutar")
            Next
            TextBox7.Text = (sonuc.ToString("C"))
        Else
            MsgBox("aradığınız kriterde çek/senet kaydı yok")
        End If

    End Sub



    Private Sub DataGridView1_Click(sender As Object, e As EventArgs) Handles DataGridView1.Click
        Dim sonsatir As Integer = DataGridView1.FirstDisplayedScrollingRowIndex
        Label1.Text = sonsatir
        Label2.Text = DataGridView1.CurrentRow.Index
    End Sub



    Private Sub DataGridView1_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellDoubleClick
        Dim firma As String = (DataGridView1.CurrentRow.Cells(1).Value)
        Label3.Text = firma
        Dim sorgu As String = "select * from ceksenet where firma='" + firma + "' order by ceksenetno desc"
        adap = New OleDbDataAdapter(sorgu, c)
        Dim ds As New DataSet
        adap.Fill(ds, "lklşk")
        DataGridView1.DataSource = ds.Tables(0)

        Dim adet As Integer = ds.Tables(0).Rows.Count
        Dim sonuc As Double
        Dim kayit As DataRow

        For i = 0 To adet - 1
            kayit = ds.Tables(0).Rows(i)
            sonuc = sonuc + kayit("tutar")
        Next

        TextBox7.Text = (sonuc.ToString("C"))

    End Sub
    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Dim firma As String = (DataGridView1.CurrentRow.Cells(1).Value)
        Label3.Text = firma
    End Sub

    Private Sub DataGridView1_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles DataGridView1.RowPrePaint
        For i As Integer = 0 To DataGridView1.Rows.Count - 1

            Dim durum As String = DataGridView1.Rows(i).Cells(7).Value

            If durum = "ödendi" Then
                DataGridView1.Rows(i).DefaultCellStyle.BackColor = Color.Lavender
            Else
                DataGridView1.Rows(i).DefaultCellStyle.ForeColor = Color.Crimson
            End If
        Next

        Dim cell_style1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        'cell_style1.BackColor = Color.LightGreen
        cell_style1.Alignment = DataGridViewContentAlignment.MiddleRight
        cell_style1.Format = "C"
        DataGridView1.Columns(6).DefaultCellStyle = cell_style1

    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        Dim onizleme As PrintPreviewDialog = New PrintPreviewDialog()
        onizleme.Document = PrintDocument1
        onizleme.ShowDialog()
    End Sub



    Private Sub PrintDocument1_PrintPage(sender As Object, e As PrintPageEventArgs) Handles PrintDocument1.PrintPage
        Try
            Dim iLeftMargin As Integer = e.MarginBounds.Left
            Dim iTopMargin As Integer = e.MarginBounds.Top
            Dim bMorePagesToPrint As Boolean = False
            Dim iTmpWidth As Integer = 0
            bFirstPage = True

            If bFirstPage Then

                For Each GridCol As DataGridViewColumn In DataGridView1.Columns
                    iTmpWidth = CInt((Math.Floor(CDbl((CDbl(GridCol.Width) / CDbl(iTotalWidth) * CDbl(iTotalWidth) * (CDbl(e.MarginBounds.Width) / CDbl(iTotalWidth)))))))
                    iHeaderHeight = CInt((e.Graphics.MeasureString(GridCol.HeaderText, GridCol.InheritedStyle.Font, iTmpWidth).Height)) + 11
                    arrColumnLefts.Add(iLeftMargin)
                    arrColumnWidths.Add(iTmpWidth)
                    iLeftMargin += iTmpWidth
                Next
            End If

            While iRow <= DataGridView1.Rows.Count - 1
                Dim GridRow As DataGridViewRow = DataGridView1.Rows(iRow)
                iCellHeight = GridRow.Height + 5
                Dim iCount As Integer = 0

                If iTopMargin + iCellHeight >= e.MarginBounds.Height + e.MarginBounds.Top Then
                    bNewPage = True
                    bFirstPage = False
                    bMorePagesToPrint = True
                    Exit While
                Else

                    If bNewPage Then
                        e.Graphics.DrawString("Çek/Senet", New Font(DataGridView1.Font, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top - e.Graphics.MeasureString("Çek/Senet", New Font(DataGridView1.Font, FontStyle.Bold), e.MarginBounds.Width).Height - 13)
                        Dim strDate As String = DateTime.Now.ToLongDateString() & " " + DateTime.Now.ToShortTimeString()
                        e.Graphics.DrawString(strDate, New Font(DataGridView1.Font, FontStyle.Bold), Brushes.Black, e.MarginBounds.Left + (e.MarginBounds.Width - e.Graphics.MeasureString(strDate, New Font(DataGridView1.Font, FontStyle.Bold), e.MarginBounds.Width).Width), e.MarginBounds.Top - e.Graphics.MeasureString("Çek/Senet", New Font(New Font(DataGridView1.Font, FontStyle.Bold), FontStyle.Bold), e.MarginBounds.Width).Height - 13)
                        iTopMargin = e.MarginBounds.Top

                        For Each GridCol As DataGridViewColumn In DataGridView1.Columns
                            e.Graphics.FillRectangle(New SolidBrush(Color.LightGray), New Rectangle(CInt(arrColumnLefts(iCount)), iTopMargin, CInt(arrColumnWidths(iCount)), iHeaderHeight))
                            e.Graphics.DrawRectangle(Pens.Black, New Rectangle(CInt(arrColumnLefts(iCount)), iTopMargin, CInt(arrColumnWidths(iCount)), iHeaderHeight))
                            e.Graphics.DrawString(GridCol.HeaderText, GridCol.InheritedStyle.Font, New SolidBrush(GridCol.InheritedStyle.ForeColor), New RectangleF(CInt(arrColumnLefts(iCount)), iTopMargin, CInt(arrColumnWidths(iCount)), iHeaderHeight), strFormat)
                            iCount += 1
                        Next

                        bNewPage = False
                        iTopMargin += iHeaderHeight
                    End If

                    iCount = 0

                    For Each Cel As DataGridViewCell In GridRow.Cells

                        If Cel.Value IsNot Nothing Then
                            e.Graphics.DrawString(Cel.Value.ToString(), Cel.InheritedStyle.Font, New SolidBrush(Cel.InheritedStyle.ForeColor), New RectangleF(CInt(arrColumnLefts(iCount)), CSng(iTopMargin), CInt(arrColumnWidths(iCount)), CSng(iCellHeight)), strFormat)
                        End If

                        e.Graphics.DrawRectangle(Pens.Black, New Rectangle(CInt(arrColumnLefts(iCount)), iTopMargin, CInt(arrColumnWidths(iCount)), iCellHeight))
                        iCount += 1
                    Next
                End If

                iRow += 1
                iTopMargin += iCellHeight
            End While

            If bMorePagesToPrint Then
                e.HasMorePages = True
            Else
                e.HasMorePages = False
            End If

        Catch exc As Exception
            MessageBox.Show(exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub

    Private Sub PrintDocument1_BeginPrint(sender As Object, e As PrintEventArgs) Handles PrintDocument1.BeginPrint
        Try
            strFormat = New StringFormat()
            strFormat.Alignment = StringAlignment.Near
            strFormat.LineAlignment = StringAlignment.Center
            strFormat.Trimming = StringTrimming.EllipsisCharacter
            arrColumnLefts.Clear()
            arrColumnWidths.Clear()
            iCellHeight = 0
            iRow = 0
            bFirstPage = True
            bNewPage = True
            iTotalWidth = 0

            For Each dgvGridCol As DataGridViewColumn In DataGridView1.Columns
                iTotalWidth += dgvGridCol.Width
            Next

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.[Error])
        End Try
    End Sub
End Class