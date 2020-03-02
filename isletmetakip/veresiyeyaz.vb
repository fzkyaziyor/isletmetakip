Imports System.Data
Imports System.Data.OleDb
Public Class veresiyeyaz
    Dim c As OleDbConnection
    Dim baglanti, sorgu As String
    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        baglan()
        Dim komut As OleDbCommand = New OleDbCommand("INSERT INTO musteriborc (musterino,toplamtutar,tarih,nott) values (@musterino,@toplamtutar,@tarih,@nott)", c)

        komut.Parameters.AddWithValue("@musterino", Label2.Text)
        komut.Parameters.AddWithValue("@toplamtutar", Val(TextBox6.Text))
        komut.Parameters.AddWithValue("@tarih", DateTimePicker1.Value.ToShortDateString)
        komut.Parameters.AddWithValue("@nott", TextBox5.Text)
        komut.ExecuteNonQuery()
        'Dim f2 As musteritakip = Application.OpenForms("musteritakip")
        'musteritakip.odemegoster()
        'musteritakip.borcgoster()
        'yapılan müsteri ödemesini aynı zamanda kasagelir tablosuna DA kaydedelim!

        MsgBox("Müşterinin Veresiye Kaydı Tamamlandı!")


        musteritakip.musteriyenile(0)

        menu1.Panel1.Controls.Clear()
        Dim frm2 As musteritakip = New musteritakip()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
        Me.Close()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        menu1.Panel1.Controls.Clear()
        Dim frm2 As musteritakip = New musteritakip()
        frm2.TopLevel = False
        menu1.Panel1.Controls.Add(frm2)
        frm2.Show()
        frm2.Dock = DockStyle.Fill
        frm2.BringToFront()
        Me.Close()
    End Sub



    Private Sub veresiyeyaz_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Dim f1 As musteritakip = Application.OpenForms("musteritakip")
        'Label1.Text = f1.TextBox2.Text + "  " + f1.TextBox7.Text
        'Label2.Text = f1.Label9.Text 'müşteri no sunu çektik
        TextBox6.Focus()

    End Sub

    Private Sub TextBox6_Leave(sender As Object, e As EventArgs) Handles TextBox6.Leave
        TextBox6.Text = Val(TextBox6.Text).ToString("C")
    End Sub
End Class