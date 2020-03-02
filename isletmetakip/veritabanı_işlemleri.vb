Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Public Class veritabanı_işlemleri
    Private Sub veritabanı_işlemleri_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        Try
            Dim saveFD1 As SaveFileDialog = New SaveFileDialog()
            Dim filename = ""
            saveFD1.Title = "Yedek Alma "
            saveFD1.DefaultExt = "accdb"
            saveFD1.Filter = "Ms-Access Files (*.accdb) |*.accdb|All Files|*.*"
            saveFD1.FilterIndex = 1
            saveFD1.RestoreDirectory = True
            If saveFD1.ShowDialog() = DialogResult.OK Then
                filename = saveFD1.FileName
                backup(filename)
                MessageBox.Show("Yedekleme işlemi Başarılı !", "YEDEKLEME DURUMU", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Yedekleme yapılırken hata oluştu.", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Protected Sub backup(ByVal path As String)
        Dim src As String = Application.StartupPath & "\isletme.accdb"
        Dim dst As String = path
        System.IO.File.Copy(src, dst, True)
    End Sub

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub


    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Try
            Dim saveFD12 As OpenFileDialog = New OpenFileDialog()
            Dim filename As String = ""
            saveFD12.InitialDirectory = "D:"
            saveFD12.Title = "Geri Yüklemek İstediğiniz Veritabanını Seçiniz"
            saveFD12.DefaultExt = "accdb"
            saveFD12.Filter = "Ms-Access Files (*.accdb) |*.accdb|All Files|*.*"
            saveFD12.FilterIndex = 1
            saveFD12.RestoreDirectory = True
            If saveFD12.ShowDialog() = DialogResult.OK Then
                filename = saveFD12.FileName
                Dim src As String = filename
                Dim dst As String = Application.StartupPath & "\isletme.accdb"
                System.IO.File.Copy(src, dst, True)
                MessageBox.Show("Geri Yükleme İşleminiz Başarılı", "GERİ YÜKLEME", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Geri Yüklemede Hata Oluştu", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub
End Class