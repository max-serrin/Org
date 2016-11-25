Public Class Form1

    Dim strFolderName As String
    Dim intIndex As Integer = 0
    Dim cmpIndex As Integer = 0
    Dim fi As List(Of IO.FileInfo)
    Dim fii As List(Of Integer)
    Dim fil(,) As Integer

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized
        FolderBrowserDialog1.SelectedPath = "C:\"
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click
        fii.Item(intIndex) += 1
        fii.Item(cmpIndex) -= 1
        NextSet()
    End Sub

    Private Sub PictureBox2_Click(sender As Object, e As EventArgs) Handles PictureBox2.Click
        fii.Item(cmpIndex) += 1
        fii.Item(intIndex) -= 1
        NextSet()
    End Sub

    Private Sub NextSet()
        intIndex = 0
        cmpIndex = 0

        Dim c As Integer = 0
        For i0 As Integer = 0 To fi.Count
            For i1 As Integer = 0 To fi.Count
                c += fil(i0, i1)
            Next
        Next

        If c >= ((fi.Count * fi.Count - fi.Count) / 2) Then
            MsgBox("All Done!")
            PictureBox1.ImageLocation = fi.Item(fii.IndexOf(fii.Max())).FullName
            fii.Item(fii.IndexOf(fii.Max())) = 0
            PictureBox2.ImageLocation = fi.Item(fii.IndexOf(fii.Max())).FullName
        Else
            While intIndex = cmpIndex Or fil(intIndex, cmpIndex) = 1 Or fil(cmpIndex, intIndex) = 1
                intIndex = CInt(Int(fi.Count * Rnd()))
                cmpIndex = CInt(Int(fi.Count * Rnd()))
            End While
            PictureBox1.ImageLocation = fi.Item(intIndex).FullName
            PictureBox2.ImageLocation = fi.Item(cmpIndex).FullName
            fil(intIndex, cmpIndex) = 1
        End If
    End Sub

    Private Sub NewCollectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewCollectionToolStripMenuItem.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            strFolderName = FolderBrowserDialog1.SelectedPath
            Dim di As New IO.DirectoryInfo(strFolderName)
            fi = di.GetFiles("*.jpg").ToList()
            fi.AddRange(di.GetFiles("*.jpeg").ToList())
            fi.AddRange(di.GetFiles("*.png").ToList())
            fi.AddRange(di.GetFiles("*.gif").ToList())
            fi.AddRange(di.GetFiles("*.bmp").ToList())

            fii = New List(Of Integer)
            For i As Integer = 0 To fi.Count
                fii.Add(0)
            Next

            ReDim fil(fi.Count, fi.Count)
            For i0 As Integer = 0 To fi.Count
                For i1 As Integer = 0 To fi.Count
                    fil(i0, i1) = 0
                Next
            Next

            Randomize()

            intIndex = 0
            If fi.Count = 0 Then
                PictureBox1.ImageLocation = Nothing
                PictureBox2.ImageLocation = Nothing
            ElseIf fi.Count = 1 Then
                PictureBox1.ImageLocation = fi(intIndex).FullName
                PictureBox2.ImageLocation = Nothing
            Else
                While intIndex = cmpIndex
                    intIndex = CInt(Int(fi.Count * Rnd()))
                    cmpIndex = CInt(Int(fi.Count * Rnd()))
                End While
                PictureBox1.ImageLocation = fi.Item(intIndex).FullName
                PictureBox2.ImageLocation = fi.Item(cmpIndex).FullName
                fil(intIndex, cmpIndex) = 1
            End If
        End If


    End Sub
End Class
