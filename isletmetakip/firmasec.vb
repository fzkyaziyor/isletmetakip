Imports System.Data
Imports System.Data.OleDb
Imports System.Xml
Public Class firmasec
    Dim c As OleDbConnection
    Dim baglanti, sorgu As String
    Dim ds, ds1 As New DataSet
    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub

    Private Sub firmasec_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        firmagoster()
        DataGridView1.Font = New Font("arial", 9, FontStyle.Bold)
    End Sub



    Private Sub Button1_Click(sender As Object, e As EventArgs)


    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs)

        Dim silid As Integer = DataGridView1.CurrentRow.Cells(0).Value

        Dim cevap As DialogResult = MessageBox.Show(DataGridView1.CurrentRow.Cells(1).Value & " Kaydı Silinsin mi?", "FİRMA SİL", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("delete from firma where firmaid=" & silid, c)
            komut.ExecuteNonQuery()
            MsgBox("Firma Kaydınız Silindi!")
        End If
        firmagoster()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As emanettakip = New emanettakip()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
        Me.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        baglan()
        Dim komut As OleDbCommand = New OleDbCommand("insert into firma(firmaadi,aciklama) values (@firmaadi,@aciklama)", c)
        komut.Parameters.AddWithValue("@firmaadi", TextBox1.Text)
        komut.Parameters.AddWithValue("@aciklama", TextBox2.Text)

        komut.ExecuteNonQuery()
        MsgBox("Firma Kaydınız Tamamlandı.")
        firmagoster()
        stokamalal.firmagoster()
        firmayaodemeyap.firmagoster()
        emanettakip.emanetgoster()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim silid As Integer = DataGridView1.CurrentRow.Cells(0).Value

        Dim cevap As DialogResult = MessageBox.Show(DataGridView1.CurrentRow.Cells(1).Value & " Kaydı Silinsin mi?", "FİRMA SİL", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("delete from firma where firmaid=" & silid, c)
            komut.ExecuteNonQuery()
            MsgBox("Firma Kaydınız Silindi!")
        End If
        firmagoster()
    End Sub

    Public Sub firmagoster()
        baglan()
        Dim adap As OleDbDataAdapter = New OleDbDataAdapter("select * from firma order by firmaadi asc", c)
        ds = New DataSet
        adap.Fill(ds, "firma")
        DataGridView1.DataSource = ds.Tables(0)


    End Sub

    Private Sub DataGridView1_CurrentCellChanged(sender As Object, e As EventArgs) Handles DataGridView1.CurrentCellChanged
        baglan()
        Dim kayit As DataRow
        Dim numara As CurrencyManager
        numara = Me.BindingContext(ds.Tables(0))
        kayit = ds.Tables(0).Rows.Item(numara.Position)
        Label7.Text = kayit("firmaid").ToString
        Label8.Text = kayit("firmaadi").ToString

    End Sub


End Class