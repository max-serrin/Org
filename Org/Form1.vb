Imports System.IO

Public Class Form1

    Dim strFolderName As String
    Dim intIndex As Integer = 0
    Dim fi As List(Of IO.FileInfo)
    Dim getf As String = "*.jpg"
    Dim ud As Stack(Of String)

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.WindowState = FormWindowState.Maximized

        If Directory.Exists(My.Application.CommandLineArgs.First()) Then
            FolderBrowserDialog1.SelectedPath = My.Application.CommandLineArgs.First()
        Else
            FolderBrowserDialog1.SelectedPath = "C:\"
        End If

        AddHandler btnNext.KeyPress, AddressOf Form1_KeyPress
    End Sub

    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub NewCollectionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NewCollectionToolStripMenuItem.Click
        ud = New Stack(Of String)

        intIndex = 0
        Label1.Text = "0"
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            openFolder()
        End If
    End Sub

    Private Sub openFolder()
        strFolderName = FolderBrowserDialog1.SelectedPath
        Dim di As New IO.DirectoryInfo(strFolderName)
        getf = "*.jpg"
        fi = di.GetFiles("*.jpg").ToList()
        fi.AddRange(di.GetFiles("*.jpeg").ToList())
        fi.AddRange(di.GetFiles("*.png").ToList())
        fi.AddRange(di.GetFiles("*.gif").ToList())
        fi.AddRange(di.GetFiles("*.bmp").ToList())
        If fi.Count = 0 Then
            getf = Nothing
            btnNext.Enabled = False
            btnBack.Enabled = False
            btnPlus.Enabled = False
            btnMinus.Enabled = False
            btnMove.Enabled = False
            btnDelete.Enabled = False
        Else
            intIndex = 0
            If getf Is Nothing Then
                PictureBox1.ImageLocation = Nothing
            Else
                PictureBox1.ImageLocation = fi(intIndex).FullName
            End If
            btnNext.Enabled = True
            btnBack.Enabled = True
            btnPlus.Enabled = True
            btnMinus.Enabled = True
            btnMove.Enabled = True
            btnDelete.Enabled = True
            Label1.Text = fi.Count.ToString()
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        intIndex += 1
        If intIndex >= fi.Count Then
            intIndex = 0
        End If
        PictureBox1.ImageLocation = fi(intIndex).FullName
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If intIndex <= 0 Then
            intIndex = fi.Count
        End If
        intIndex -= 1
        PictureBox1.ImageLocation = fi(intIndex).FullName
    End Sub

    Private Sub btnPlus_Click(sender As Object, e As EventArgs) Handles btnPlus.Click
        moveCurrentImageToFolder("+")
    End Sub

    Private Sub btnMinus_Click(sender As Object, e As EventArgs) Handles btnMinus.Click
        moveCurrentImageToFolder("-")
    End Sub

    Private Sub Form1_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles Me.KeyPress
        Select Case e.KeyChar
            Case "="
                btnPlus.PerformClick()
            Case "-"
                btnMinus.PerformClick()
            Case "\"
                btnMove.PerformClick()
            Case "`"
                btnDelete.PerformClick()
            Case "z"
                UndoToolStripMenuItem.PerformClick()
            Case "."
                btnNext.PerformClick()
            Case ","
                btnBack.PerformClick()
        End Select
    End Sub

    Private Sub btnMove_Click(sender As Object, e As EventArgs) Handles btnMove.Click
        moveCurrentImageToFolder("Move")
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        moveCurrentImageToFolder("Delete")
    End Sub

    Private Sub moveCurrentImageToFolder(folder As String)
        If My.Computer.FileSystem.DirectoryExists(strFolderName + "\" + folder) Then

        Else
            My.Computer.FileSystem.CreateDirectory(strFolderName + "\" + folder)
        End If

        Dim dest As String
        dest = fi(intIndex).DirectoryName() + "\" + folder + "\" + fi(intIndex).Name

        My.Computer.FileSystem.MoveFile(fi(intIndex).FullName, dest)
        ud.Push(dest)

        Dim di As New IO.DirectoryInfo(strFolderName)
        fi.RemoveAt(intIndex)
        If fi.Count = 0 Then
            getf = Nothing
            btnNext.Enabled = False
            btnBack.Enabled = False
            btnPlus.Enabled = False
            btnMinus.Enabled = False
            btnMove.Enabled = False
            btnDelete.Enabled = False
            PictureBox1.ImageLocation = Nothing
        Else
            If intIndex >= fi.Count Then
                intIndex = fi.Count - 1
            End If
            PictureBox1.ImageLocation = fi(intIndex).FullName
            Label1.Text = fi.Count.ToString()
        End If
    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As System.Windows.Forms.MouseEventArgs) Handles PictureBox1.MouseClick
        If fi Is Nothing Then
            openFolder()
        ElseIf fi.Count > 0 Then
            Select Case e.Button
                Case Windows.Forms.MouseButtons.Left
                    moveCurrentImageToFolder("+")
                Case Windows.Forms.MouseButtons.Right
                    moveCurrentImageToFolder("-")
                Case Windows.Forms.MouseButtons.XButton1
                    btnBack.PerformClick()
                Case Windows.Forms.MouseButtons.XButton2
                    btnNext.PerformClick()
            End Select
        End If
    End Sub

    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        If ud.Count > 0 Then
            Dim dest As System.IO.FileInfo

            dest = New IO.FileInfo(ud.Pop())

            My.Computer.FileSystem.MoveFile(dest.FullName, strFolderName + "\" + dest.Name)

            fi.Insert(intIndex, New IO.FileInfo(strFolderName + "\" + dest.Name))
            PictureBox1.ImageLocation = fi(intIndex).FullName
        End If
    End Sub
End Class
