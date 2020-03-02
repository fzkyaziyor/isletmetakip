Imports System.Data
Imports System.Data.OleDb
Public Class stokamalal
    Dim adap, adap1 As New OleDbDataAdapter
    Dim ds, ds1 As New DataSet
    Dim c As OleDbConnection
    Dim baglanti, sorgu As String
    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub



    Private Sub stokamalal_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        firmagoster()
        DataGridView1.Font = New Font("arial", 9, FontStyle.Bold)

    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As stoktakip = New stoktakip()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
        Me.Close()
    End Sub

    Public Sub firmagoster()
        baglan()
        Dim komut As OleDbCommand = New OleDbCommand("select * from firma order by firmaadi asc", c)
        Dim dt As DataTable = New DataTable
        dt.Load(komut.ExecuteReader)
        c.Close()
        DataGridView1.DataSource = dt
    End Sub
    Private Sub ara(ByVal sorgu As String)
        baglan()
        adap = New OleDbDataAdapter(sorgu, c)
        ds = New DataSet
        adap.Fill(ds, "hjgjjk")
        If ds.Tables(0).Rows.Count <> 0 Then
            DataGridView1.DataSource = ds.Tables(0)

        Else
            MsgBox("aradığınız kriterde firma kaydı yok")
        End If

    End Sub
    Private Sub TextBox11_TextChanged(sender As Object, e As EventArgs) Handles TextBox11.TextChanged
        ara("select * from firma where firmaadi like '" & TextBox11.Text & "%' order by firmaid asc")
    End Sub

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs)

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As firmasec = New firmasec()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim silid As Integer = DataGridView1.CurrentRow.Cells(0).Value

        Dim cevap As DialogResult = MessageBox.Show(DataGridView1.CurrentRow.Cells(1).Value & " Kaydı Silinsin mi?", "FİRMA SİL", MessageBoxButtons.YesNo)
        If cevap = DialogResult.Yes Then
            Dim komut As OleDbCommand = New OleDbCommand("delete from firma where firmaid=" & silid, c)
            komut.ExecuteNonQuery()
            MsgBox("Firma Kaydınız Silindi!")
        End If
        firmagoster()
    End Sub











    'Private Sub TextBox3_TextChanged(sender As Object, e As EventArgs) Handles TextBox3.TextChanged
    'TextBox4.Text = TextBox1.Text * TextBox3.Text
    'End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click



        baglan()

        Dim komut As OleDbCommand = New OleDbCommand("insert into aldigimmal (toplamtutar,aldigimtarih,firma,miktar) values (@toplamtutar,@aldigimtarih,@firma,@miktar)", c)

        komut.Parameters.AddWithValue("@toplamtutar", TextBox4.Text)
        komut.Parameters.AddWithValue("@aldigimtarih", DateTimePicker1.Value.ToShortDateString)
        komut.Parameters.AddWithValue("@firma", TextBox1.Text)
        komut.Parameters.AddWithValue("@miktar", TextBox2.Text)
        komut.ExecuteNonQuery()

        MsgBox("aldığınız ürün  kaydedildi.")
        Dim f2 As stoktakip = Application.OpenForms("stoktakip")
        f2.urungoster()

        menu1.Panel1.Controls.Clear()
        Dim frm2 As stoktakip = New stoktakip()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()


        Me.Close()

    End Sub

    Private Sub TextBox4_Leave(sender As Object, e As EventArgs) Handles TextBox4.Leave
        TextBox4.Text = Val(TextBox4.Text).ToString("C")
    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        TextBox1.Text = DataGridView1.CurrentRow.Cells(1).Value
    End Sub
End Class