Public Class Form1

    Dim strFolderName As String
    Dim intIndex As Integer = 0
    Dim total As Integer = 0
    Dim fi As List(Of IO.FileInfo)
    Dim fii As List(Of Integer)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        FolderBrowserDialog1.SelectedPath = "C:\"
    End Sub


    Private Sub LoadCollectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LoadCollectionToolStripMenuItem.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            strFolderName = FolderBrowserDialog1.SelectedPath
            Dim di As New IO.DirectoryInfo(strFolderName)
            fi = di.GetFiles("*.wav").ToList()
            If fi.Count = 0 Then
                Return
            End If

            fii = New List(Of Integer)
            For i As Integer = 0 To fi.Count
                fii.Add(0)
            Next

            Randomize()
            Do
                intIndex = CInt(Int(fi.Count * Rnd()))
            Loop While fii(intIndex) = 1
            My.Computer.Audio.Play(fi.Item(intIndex).FullName)
            'PictureBox1.ImageLocation = fi.Item(intIndex).FullName
            fii(intIndex) = 1
            total = 1
        End If

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        total += 1
        If total = fi.Count Then
            For i As Integer = 0 To fi.Count
                fii(i) = 0
            Next
        End If

        Do
            intIndex = CInt(Int(fi.Count * Rnd()))
        Loop While fii(intIndex) = 1
        My.Computer.Audio.Play(fi.Item(intIndex).FullName)
        'PictureBox1.ImageLocation = fi.Item(intIndex).FullName
    End Sub

    Private Sub PlayAgainToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlayAgainToolStripMenuItem.Click
        My.Computer.Audio.Play(fi.Item(intIndex).FullName)
    End Sub

    Private Sub PlayNextToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PlayNextToolStripMenuItem.Click
        PictureBox1_Click(sender, e)
    End Sub
End Class
