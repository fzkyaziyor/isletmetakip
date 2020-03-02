Imports System.Data
Imports System.Data.OleDb
Public Class musteriodemesi
    Dim c As OleDbConnection
    Dim baglanti, sorgu As String
    Public Sub baglan()
        baglanti = " Provider =Microsoft.ACE.OLEDB.12.0; Data Source=isletme.accdb"
        c = New OleDbConnection(baglanti)
        c.Open()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        'musteriodeme tablosuna kayıt yapıyoruz..
        baglan()
        Dim komut As OleDbCommand = New OleDbCommand("INSERT INTO musteriodeme (musterino,odemetutari,odemetarihi,nott) values (@musterino,@odemetutari,@odemetarihi,@nott)", c)

        komut.Parameters.AddWithValue("@musterino", Label2.Text)
        komut.Parameters.AddWithValue("@odemetutari", Val(TextBox6.Text))
        komut.Parameters.AddWithValue("@odemetarihi", DateTimePicker1.Value.ToShortDateString)
        komut.Parameters.AddWithValue("@nott", TextBox5.Text)
        komut.ExecuteNonQuery()
        'Dim f2 As musteritakip = Application.OpenForms("musteritakip")
        'musteritakip.odemegoster()
        'musteritakip.borcgoster()
        'yapılan müsteri ödemesini aynı zamanda kasagelir tablosuna DA kaydedelim!

        MsgBox("Müşterinin Ödeme Kaydı Tamamlandı!")

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

    Private Sub TextBox6_TextChanged(sender As Object, e As EventArgs) Handles TextBox6.TextChanged

    End Sub

    Public Sub musteriodemesi_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        TextBox6.Focus()


    End Sub

    Private Sub TextBox6_LostFocus(sender As Object, e As EventArgs) Handles TextBox6.LostFocus
        TextBox6.Text = Val((TextBox6.Text)).ToString("C")
    End Sub

    Private Sub TextBox6_Leave(sender As Object, e As EventArgs) Handles TextBox6.Leave
        TextBox6.Text = Val(TextBox6.Text).ToString("C")
    End Sub
End Class