Imports System.Drawing
Imports System.Math
Imports System.Threading.Timer

Public Class Form1
    Public Structure spritedata
        Public sprite As Bitmap
        Public mask As Bitmap
        Public center As Point
        Public frame As Integer
    End Structure

    Dim stand, flyingObj, reppuken, start(3), intro(9), MoveR(3), MoveL(3),
        PunchR(6), PunchL(6), KickR(4), KickL(4), JumpR(6), JumpL(6), JumpFwdR(6), JumpFwdL(6), CrouchR(1), CrouchL(1),
        JumpAtkR(6), JumpAtkL(6), CrouchAtkR(2), CrouchAtkL(2),
        ReppukenR(0), ReppukenL(0), RepStanceR(1), RepStanceL(1), StaggerR(2), StaggerL(2), CrouchStgR(2), CrouchStgL(2), Explode(2) As spritedata
    Dim graph As Graphics
    Dim bmp As Bitmap
    Dim currFrame As Integer = 0
    Dim posx, posy, state, facing, shootFacing, vx, vy, xobj, yobj, xRep, yRep, score As Integer
    Dim indexIntro, indexIdle, indexMR, indexML, indexJR, indexJL,
        indexCR, indexCL, indexPL, indexPR, indexKL, indexKR, indexJAR, indexJAL, indexCAR, indexCAL, indexJFR, indexJFL,
        indexShoot, indexStanceR, indexStanceL, indexStaggerR, indexStaggerL, indexCSR, indexCSL, indexExplode, RandomNumber As Integer
    Dim attack, shoot, isCollision1, isCollision2, lose As Boolean
    Dim box1, box2, box3 As Rectangle
    Dim data As New spritedata
    Dim choose As New DialogResult

    Sub SetIntro()
        intro(0).sprite = My.Resources.fights_03
        intro(0).mask = MaskOf(intro(0).sprite)
        intro(1).sprite = My.Resources.fights_02
        intro(1).mask = MaskOf(intro(1).sprite)
        intro(2).sprite = My.Resources.fights_03
        intro(2).mask = MaskOf(intro(2).sprite)
        intro(3).sprite = My.Resources.intro_01R
        intro(3).mask = MaskOf(intro(3).sprite)
        intro(4).sprite = My.Resources.intro_02R
        intro(4).mask = MaskOf(intro(4).sprite)
        intro(5).sprite = My.Resources.intro_03R
        intro(5).mask = MaskOf(intro(5).sprite)
        intro(6).sprite = My.Resources.intro_04R
        intro(6).mask = MaskOf(intro(6).sprite)
        intro(7).sprite = My.Resources.intro_05R
        intro(7).mask = MaskOf(intro(7).sprite)
        intro(8).sprite = My.Resources.intro_06R
        intro(8).mask = MaskOf(intro(8).sprite)
        intro(9).sprite = My.Resources.idleR
        intro(9).mask = MaskOf(intro(9).sprite)
    End Sub
    Sub SetMoveRight()
        MoveR(0).sprite = My.Resources.walking_01R
        MoveR(0).mask = MaskOf(MoveR(0).sprite)
        'MoveR(0).frame = 1
        MoveR(1).sprite = My.Resources.walking_02R
        MoveR(1).mask = MaskOf(MoveR(1).sprite)
        'MoveR(1).frame = 1
        MoveR(2).sprite = My.Resources.walking_01R
        MoveR(2).mask = MaskOf(MoveR(2).sprite)
        'MoveR(2).frame = 1
        MoveR(3).sprite = My.Resources.idleR
        MoveR(3).mask = MaskOf(MoveR(3).sprite)
    End Sub
    Sub SetMoveLeft()
        MoveL(0).sprite = My.Resources.walking_01L
        MoveL(0).mask = MaskOf(MoveL(0).sprite)
        'MoveL(0).frame = 1
        MoveL(1).sprite = My.Resources.walking_02L
        MoveL(1).mask = MaskOf(MoveL(1).sprite)
        'MoveL(1).frame = 1
        MoveL(2).sprite = My.Resources.walking_01L
        MoveL(2).mask = MaskOf(MoveL(2).sprite)
        'MoveL(2).frame = 1
        MoveL(3).sprite = My.Resources.idleL
        MoveL(3).mask = MaskOf(MoveL(3).sprite)
    End Sub
    Sub SetJumpR()
        JumpR(0).sprite = My.Resources.jumping_01R
        JumpR(0).mask = MaskOf(JumpR(0).sprite)
        JumpR(1).sprite = My.Resources.jumping_02R
        JumpR(1).mask = MaskOf(JumpR(1).sprite)
        JumpR(1).frame = 6
        JumpR(2).sprite = My.Resources.jumping_03R
        JumpR(2).mask = MaskOf(JumpR(2).sprite)
        JumpR(2).frame = 6
        JumpR(3).sprite = My.Resources.jumping_04R
        JumpR(3).mask = MaskOf(JumpR(3).sprite)
        JumpR(4).sprite = My.Resources.idleR
        JumpR(4).mask = MaskOf(JumpR(4).sprite)
    End Sub
    Sub SetJumpL()
        JumpL(0).sprite = My.Resources.jumping_01L
        JumpL(0).mask = MaskOf(JumpL(0).sprite)
        JumpL(1).sprite = My.Resources.jumping_02L
        JumpL(1).mask = MaskOf(JumpL(1).sprite)
        JumpL(1).frame = 3
        JumpL(2).sprite = My.Resources.jumping_03L
        JumpL(2).mask = MaskOf(JumpL(2).sprite)
        JumpL(2).frame = 3
        JumpL(3).sprite = My.Resources.jumping_04L
        JumpL(3).mask = MaskOf(JumpL(3).sprite)
        JumpL(4).sprite = My.Resources.idleL
        JumpL(4).mask = MaskOf(JumpL(4).sprite)
    End Sub
    Sub SetJumpFwdR()
        JumpFwdR(0).sprite = My.Resources.jumping_01R
        JumpFwdR(0).mask = MaskOf(JumpFwdR(0).sprite)
        JumpFwdR(1).sprite = My.Resources.jumping_02R
        JumpFwdR(1).mask = MaskOf(JumpFwdR(1).sprite)
        JumpFwdR(1).frame = 2
        JumpFwdR(2).sprite = My.Resources.jumping_03R
        JumpFwdR(2).mask = MaskOf(JumpFwdR(2).sprite)
        JumpFwdR(2).frame = 2
        JumpFwdR(3).sprite = My.Resources.jumping_04R
        JumpFwdR(3).mask = MaskOf(JumpFwdR(3).sprite)
        JumpFwdR(4).sprite = My.Resources.idleR
        JumpFwdR(4).mask = MaskOf(JumpFwdR(4).sprite)
    End Sub
    Sub SetJumpFwdL()
        JumpFwdL(0).sprite = My.Resources.jumping_01L
        JumpFwdL(0).mask = MaskOf(JumpFwdL(0).sprite)
        JumpFwdL(1).sprite = My.Resources.jumping_02L
        JumpFwdL(1).mask = MaskOf(JumpFwdL(1).sprite)
        JumpFwdL(1).frame = 2
        JumpFwdL(2).sprite = My.Resources.jumping_03L
        JumpFwdL(2).mask = MaskOf(JumpFwdL(2).sprite)
        JumpFwdL(2).frame = 2
        JumpFwdL(3).sprite = My.Resources.jumping_04L
        JumpFwdL(3).mask = MaskOf(JumpFwdL(3).sprite)
        JumpFwdL(4).sprite = My.Resources.idleL
        JumpFwdL(4).mask = MaskOf(JumpFwdL(4).sprite)
    End Sub
    Sub SetJumpAtkR()
        JumpAtkR(0).sprite = My.Resources.jumping_01R
        JumpAtkR(0).mask = MaskOf(JumpAtkR(0).sprite)
        JumpAtkR(1).sprite = My.Resources.jumping_02R
        JumpAtkR(1).mask = MaskOf(JumpAtkR(1).sprite)
        JumpAtkR(1).frame = 2
        JumpAtkR(2).sprite = My.Resources.jumpatk_01R
        JumpAtkR(2).mask = MaskOf(JumpAtkR(2).sprite)
        JumpAtkR(3).sprite = My.Resources.jumpatk_02R
        JumpAtkR(3).mask = MaskOf(JumpAtkR(3).sprite)
        JumpAtkR(4).sprite = My.Resources.jumping_03R
        JumpAtkR(4).mask = MaskOf(JumpAtkR(4).sprite)
        JumpAtkR(4).frame = 2
        JumpAtkR(5).sprite = My.Resources.jumping_04R
        JumpAtkR(5).mask = MaskOf(JumpAtkR(5).sprite)
        JumpAtkR(6).sprite = My.Resources.idleR
        JumpAtkR(6).mask = MaskOf(JumpAtkR(6).sprite)
    End Sub
    Sub SetJumpAtkL()
        JumpAtkL(0).sprite = My.Resources.jumping_01L
        JumpAtkL(0).mask = MaskOf(JumpAtkL(0).sprite)
        JumpAtkL(1).sprite = My.Resources.jumping_02L
        JumpAtkL(1).mask = MaskOf(JumpAtkL(1).sprite)
        JumpAtkL(1).frame = 2
        JumpAtkL(2).sprite = My.Resources.jumpatk_01L
        JumpAtkL(2).mask = MaskOf(JumpAtkL(2).sprite)
        JumpAtkL(3).sprite = My.Resources.jumpatk_02L
        JumpAtkL(3).mask = MaskOf(JumpAtkL(3).sprite)
        JumpAtkL(4).sprite = My.Resources.jumping_03L
        JumpAtkL(4).mask = MaskOf(JumpAtkL(4).sprite)
        JumpAtkL(4).frame = 2
        JumpAtkL(5).sprite = My.Resources.jumping_04L
        JumpAtkL(5).mask = MaskOf(JumpAtkL(5).sprite)
        JumpAtkL(6).sprite = My.Resources.idleL
        JumpAtkL(6).mask = MaskOf(JumpAtkL(6).sprite)
    End Sub
    Sub SetCrouchR()
        CrouchR(0).sprite = My.Resources.crouchR
        CrouchR(0).center.X = posx - 20 + CrouchR(0).sprite.Width
        CrouchR(0).center.Y = posy - 20 + CrouchR(0).sprite.Height
        CrouchR(0).mask = MaskOf(CrouchR(0).sprite)
        CrouchR(1).sprite = My.Resources.idleR
        CrouchR(1).mask = MaskOf(CrouchR(1).sprite)
    End Sub
    Sub SetCrouchL()
        CrouchL(0).sprite = My.Resources.crouchL
        CrouchL(0).center.X = posx - 20 + CrouchL(0).sprite.Width
        CrouchL(0).center.Y = posy - 20 + CrouchL(0).sprite.Height
        CrouchL(0).mask = MaskOf(CrouchL(0).sprite)
        CrouchL(1).sprite = My.Resources.idleL
        CrouchL(1).mask = MaskOf(CrouchL(1).sprite)
    End Sub
    Sub SetCrouchAtkR()
        CrouchAtkR(0).sprite = My.Resources.crouchatk_01R
        CrouchAtkR(0).center.X = posx - 20 + CrouchAtkR(0).sprite.Width
        CrouchAtkR(0).center.Y = posy - 20 + CrouchAtkR(0).sprite.Height
        CrouchAtkR(0).mask = MaskOf(CrouchAtkR(0).sprite)
        CrouchAtkR(1).sprite = My.Resources.crouchatk_02R
        CrouchAtkR(1).center.X = posx - 20 + CrouchAtkR(1).sprite.Width
        CrouchAtkR(1).center.Y = posy - 20 + CrouchAtkR(1).sprite.Height
        CrouchAtkR(1).mask = MaskOf(CrouchAtkR(1).sprite)
        CrouchAtkR(2).sprite = My.Resources.idleR
        CrouchAtkR(2).mask = MaskOf(CrouchAtkR(2).sprite)
    End Sub
    Sub SetCrouchAtkL()
        CrouchAtkL(0).sprite = My.Resources.crouchatk_01L
        CrouchAtkL(0).center.X = posx - 20 + CrouchAtkL(0).sprite.Width
        CrouchAtkL(0).center.Y = posy - 20 + CrouchAtkL(0).sprite.Height
        CrouchAtkL(0).mask = MaskOf(CrouchAtkL(0).sprite)
        CrouchAtkL(1).sprite = My.Resources.crouchatk_02L
        CrouchAtkL(1).center.X = posx - 50 + CrouchAtkL(1).sprite.Width
        CrouchAtkL(1).center.Y = posy - 20 + CrouchAtkL(1).sprite.Height
        CrouchAtkL(1).mask = MaskOf(CrouchAtkL(1).sprite)
        CrouchAtkL(2).sprite = My.Resources.idleL
        CrouchAtkL(2).mask = MaskOf(CrouchAtkL(2).sprite)
    End Sub
    Sub SetPunchR()
        PunchR(0).sprite = My.Resources.punch_01R
        PunchR(0).mask = MaskOf(PunchR(0).sprite)
        PunchR(1).sprite = My.Resources.punch_02R
        PunchR(1).mask = MaskOf(PunchR(1).sprite)
        PunchR(2).sprite = My.Resources.punch_04R
        PunchR(2).mask = MaskOf(PunchR(2).sprite)
        PunchR(3).sprite = My.Resources.punch_05R
        PunchR(3).mask = MaskOf(PunchR(3).sprite)
        PunchR(4).sprite = My.Resources.punch_06R
        PunchR(4).mask = MaskOf(PunchR(4).sprite)
        PunchR(5).sprite = My.Resources.punch_07R
        PunchR(5).mask = MaskOf(PunchR(5).sprite)
        PunchR(6).sprite = My.Resources.idleR
        PunchR(6).mask = MaskOf(PunchR(6).sprite)
    End Sub
    Sub SetPunchL()
        PunchL(0).sprite = My.Resources.punch_01L
        PunchL(0).mask = MaskOf(PunchL(0).sprite)
        PunchL(1).sprite = My.Resources.punch_02L
        PunchL(1).mask = MaskOf(PunchL(1).sprite)
        PunchL(2).sprite = My.Resources.punch_04L
        PunchL(2).center.X = posx - 50 + PunchL(2).sprite.Width
        PunchL(2).center.Y = posy - 50 + PunchL(2).sprite.Height
        PunchL(2).mask = MaskOf(PunchL(2).sprite)
        PunchL(3).sprite = My.Resources.punch_05L
        PunchL(3).mask = MaskOf(PunchL(3).sprite)
        PunchL(4).sprite = My.Resources.punch_06L
        PunchL(4).center.X = posx - 50 + PunchL(4).sprite.Width
        PunchL(4).center.Y = posy - 50 + PunchL(4).sprite.Height
        PunchL(4).mask = MaskOf(PunchL(4).sprite)
        PunchL(5).sprite = My.Resources.punch_07L
        PunchL(5).mask = MaskOf(PunchL(5).sprite)
        PunchL(6).sprite = My.Resources.idleL
        PunchL(6).mask = MaskOf(PunchL(6).sprite)
    End Sub
    Sub SetKickR()
        KickR(0).sprite = My.Resources.kick_01R
        KickR(0).mask = MaskOf(KickR(0).sprite)
        KickR(1).sprite = My.Resources.kick_02R
        KickR(1).mask = MaskOf(KickR(1).sprite)
        KickR(2).sprite = My.Resources.kick_03R
        KickR(2).mask = MaskOf(KickR(2).sprite)
        KickR(3).sprite = My.Resources.kick_04R
        KickR(3).mask = MaskOf(KickR(3).sprite)
        KickR(4).sprite = My.Resources.idleR
        KickR(4).mask = MaskOf(KickR(4).sprite)
    End Sub
    Sub SetKickL()
        KickL(0).sprite = My.Resources.kick_01L
        KickL(0).mask = MaskOf(KickL(0).sprite)
        KickL(1).sprite = My.Resources.kick_02L
        KickL(1).mask = MaskOf(KickL(0).sprite)
        KickL(1).center.X = posx - 50 + KickL(1).sprite.Width
        KickL(1).center.Y = posy - 50 + KickL(1).sprite.Height
        KickL(2).sprite = My.Resources.kick_03L
        KickL(2).mask = MaskOf(KickL(2).sprite)
        KickL(3).sprite = My.Resources.kick_04L
        KickL(3).mask = MaskOf(KickL(3).sprite)
        KickL(4).sprite = My.Resources.idleL
        KickL(4).mask = MaskOf(KickL(4).sprite)
    End Sub
    Sub SetStanceR()
        RepStanceR(0).sprite = My.Resources.punch_04R
        RepStanceR(0).mask = MaskOf(RepStanceR(0).sprite)
        RepStanceR(1).sprite = My.Resources.punch_06R
        RepStanceR(1).mask = MaskOf(RepStanceR(1).sprite)
    End Sub
    Sub SetStanceL()
        RepStanceL(0).sprite = My.Resources.punch_04L
        RepStanceL(0).mask = MaskOf(RepStanceL(0).sprite)
        RepStanceL(1).sprite = My.Resources.punch_06L
        RepStanceL(1).mask = MaskOf(RepStanceL(1).sprite)
    End Sub
    Sub SetReppukenR()
        ReppukenR(0).sprite = My.Resources.reppuken_01R
        ReppukenR(0).mask = MaskOf(ReppukenR(0).sprite)
    End Sub
    Sub SetReppukenL()
        ReppukenL(0).sprite = My.Resources.reppuken_01L
        ReppukenL(0).mask = MaskOf(ReppukenL(0).sprite)
    End Sub
    Sub SetStaggerR()
        StaggerR(0).sprite = My.Resources.dmg_01R
        StaggerR(0).mask = MaskOf(StaggerR(0).sprite)
        StaggerR(1).sprite = My.Resources.dmg_02R
        StaggerR(1).mask = MaskOf(StaggerR(1).sprite)
        StaggerR(2).sprite = My.Resources.dmg_03R
        StaggerR(2).mask = MaskOf(StaggerR(2).sprite)
    End Sub
    Sub SetStaggerL()
        StaggerL(0).sprite = My.Resources.dmg_01L
        StaggerL(0).mask = MaskOf(StaggerL(0).sprite)
        StaggerL(1).sprite = My.Resources.dmg_02L
        StaggerL(1).mask = MaskOf(StaggerL(1).sprite)
        StaggerL(2).sprite = My.Resources.dmg_03L
        StaggerL(2).mask = MaskOf(StaggerL(2).sprite)
    End Sub
    Sub SetCrouchStgR()
        CrouchStgR(0).sprite = My.Resources.crouchdmg_01R
        CrouchStgR(0).center.X = posx - 20 + CrouchStgR(0).sprite.Width
        CrouchStgR(0).center.Y = posy - 20 + CrouchStgR(0).sprite.Height
        CrouchStgR(0).mask = MaskOf(CrouchStgR(0).sprite)
        CrouchStgR(1).sprite = My.Resources.crouchdmg_02R
        CrouchStgR(1).center.X = posx - 20 + CrouchStgR(1).sprite.Width
        CrouchStgR(1).center.Y = posy - 20 + CrouchStgR(1).sprite.Height
        CrouchStgR(1).mask = MaskOf(CrouchStgR(1).sprite)
        CrouchStgR(2).sprite = My.Resources.crouchdmg_03R
        CrouchStgR(2).center.X = posx - 20 + CrouchStgR(2).sprite.Width
        CrouchStgR(2).center.Y = posy - 20 + CrouchStgR(2).sprite.Height
        CrouchStgR(2).mask = MaskOf(CrouchStgR(2).sprite)
    End Sub
    Sub SetCrouchStgL()
        CrouchStgL(0).sprite = My.Resources.crouchdmg_01L
        CrouchStgL(0).mask = MaskOf(CrouchStgL(0).sprite)
        CrouchStgL(1).sprite = My.Resources.crouchdmg_02L
        CrouchStgL(1).mask = MaskOf(CrouchStgL(1).sprite)
        CrouchStgL(2).sprite = My.Resources.crouchdmg_03L
        CrouchStgL(2).mask = MaskOf(CrouchStgL(2).sprite)
    End Sub
    Sub SetExplode()
        Explode(0).sprite = My.Resources.explode1
        Explode(0).mask = MaskOf(Explode(0).sprite)
        Explode(1).sprite = My.Resources.explode2
        Explode(1).mask = MaskOf(Explode(1).sprite)
        Explode(2).sprite = My.Resources.explode2
        Explode(2).mask = MaskOf(Explode(2).sprite)
    End Sub

    Function MaskOf(ByVal b As Bitmap) As Bitmap
        Dim a As Bitmap
        Dim c As Color
        Dim i, j As Integer

        a = b.Clone
        c = a.GetPixel(0, 0)
        For i = 0 To b.Width - 1
            For j = 0 To b.Height - 1
                If a.GetPixel(i, j) = c Then
                    a.SetPixel(i, j, Color.White)
                Else
                    a.SetPixel(i, j, Color.Black)
                End If
            Next
        Next

        Return a
    End Function

    Function SpriteOf(ByVal b As Bitmap) As Bitmap
        Dim a As Bitmap
        Dim c As Color
        Dim i, j As Integer

        a = b.Clone
        c = a.GetPixel(0, 0)
        For i = 0 To b.Width - 1
            For j = 0 To b.Height - 1
                If a.GetPixel(i, j) = c Then
                    a.SetPixel(i, j, Color.Black)
                End If
            Next
        Next

        Return a
    End Function

    Sub spriteand(ByVal c As Bitmap, ByVal d As Bitmap, ByVal x As Integer, ByVal y As Integer)
        Dim i, j, a, r, g, b As Integer

        For i = 0 To d.Width - 1
            For j = 0 To d.Height - 1
                a = c.GetPixel(i + x, j + y).A And d.GetPixel(i, j).A
                r = c.GetPixel(i + x, j + y).R And d.GetPixel(i, j).R
                g = c.GetPixel(i + x, j + y).G And d.GetPixel(i, j).G
                b = c.GetPixel(i + x, j + y).B And d.GetPixel(i, j).B
                c.SetPixel(i + x, j + y, Color.FromArgb(a, r, g, b))
            Next
        Next
    End Sub

    Sub spriteor(ByVal c As Bitmap, ByVal d As Bitmap, ByVal x As Integer, ByVal y As Integer)
        Dim i, j, a, r, g, b As Integer

        For i = 0 To d.Width - 1
            For j = 0 To d.Height - 1
                a = c.GetPixel(i + x, j + y).A Or d.GetPixel(i, j).A
                r = c.GetPixel(i + x, j + y).R Or d.GetPixel(i, j).R
                g = c.GetPixel(i + x, j + y).G Or d.GetPixel(i, j).G
                b = c.GetPixel(i + x, j + y).B Or d.GetPixel(i, j).B
                c.SetPixel(i + x, j + y, Color.FromArgb(a, r, g, b))
            Next
        Next
    End Sub

    Sub PutSprite(ByVal c As Bitmap, ByVal d As Bitmap, ByVal x As Integer, ByVal y As Integer)
        Dim sprite, mask As Bitmap
        mask = MaskOf(d)
        sprite = SpriteOf(d)
        spriteand(bmp, mask, x, y)
        spriteor(bmp, sprite, x, y)
    End Sub

    Private Sub Form1_KeyDown(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyDown
        bmp = New Bitmap(My.Resources.bkg3)
        graph = Graphics.FromImage(bmp)

        If e.KeyCode = Keys.Left Then 'walk left
            If indexIntro >= 9 Then
                facing = -1
                indexML = 0
                state = 1
                Timer1.Start()
            End If

        ElseIf e.KeyCode = Keys.Right Then 'walk right
            If indexIntro >= 9 Then
                facing = 1
                indexMR = 0
                state = 1
                Timer1.Start()
            End If

        ElseIf e.KeyCode = Keys.Up Then 'jump
            If indexIntro >= 9 Then
                indexJR = 0
                indexJL = 0
                state = 2
                Timer1.Start()
            End If

        ElseIf e.KeyCode = Keys.Down Then 'crouch
            If indexIntro >= 9 Then
                indexCR = 0
                indexCL = 0
                state = 3
                Timer1.Start()
            End If

        ElseIf e.KeyCode = Keys.X Then 'kick
            If indexIntro >= 9 Then
                indexKR = 0
                indexKL = 0
                state = 4
                Timer1.Start()
            End If

        ElseIf e.KeyCode = Keys.Z Then 'punch
            If indexIntro >= 9 Then
                indexPR = 0
                indexPL = 0
                state = 5
                Timer1.Start()
            End If

        ElseIf e.KeyCode = Keys.Q Then 'jump attack
            If indexIntro >= 9 Then
                indexJAR = 0
                indexJAL = 0
                state = 6
                Timer1.Start()
            End If
        ElseIf e.KeyCode = Keys.C Then 'crouch attack
            If indexIntro >= 9 Then
                indexCAR = 0
                indexCAL = 0
                state = 7
                Timer1.Start()
            End If
        ElseIf e.KeyCode = Keys.D Then 'jump forward
            If indexIntro >= 9 Then
                indexJFR = 0
                indexJFL = 0
                state = 8
                Timer1.Start()
            End If
        ElseIf e.KeyCode = Keys.Space Then 'reppuken
            If indexIntro >= 9 Then
                indexShoot = 0
                indexStanceR = 0
                indexStanceL = 0
                state = 9
                Timer1.Start()
            End If
        End If
    End Sub

    Private Sub Form1_KeyUp(ByVal sender As Object, ByVal e As KeyEventArgs) Handles Me.KeyUp
        If e.KeyCode = Keys.Z Then
            If indexIntro >= 9 Then
                indexPR = 0
                indexPL = 0
                state = 5
                Timer1.Start()
            End If
        End If
        If e.KeyCode = Keys.X Then
            If indexIntro >= 9 Then
                indexKR = 0
                indexKL = 0
                state = 4
                Timer1.Start()
            End If
        End If
        If e.KeyCode = Keys.Down Then
            If indexIntro >= 9 Then
                indexCR = 1
                indexCL = 1
                state = 3
                Timer1.Start()
            End If
        End If
        If e.KeyCode = Keys.Q Then
            If indexIntro >= 9 Then
                indexJAR = 0
                indexJAR = 0
                state = 6
                Timer1.Start()
            End If
        End If
        If e.KeyCode = Keys.C Then
            If indexIntro >= 9 Then
                indexCAR = 0
                indexCAL = 0
                state = 7
                Timer1.Start()
            End If
        End If
    End Sub

    Sub random_move()
        Randomize()
        Dim myRandom As New Random
        RandomNumber = myRandom.Next(1, 3)

        If RandomNumber = 1 Then
            xobj = 390
            yobj = Rnd()
            If yobj < 0 Then
                yobj = Rnd() * 20 + 165
            Else
                yobj = Rnd() * 30 + 120
            End If

        ElseIf RandomNumber = 2 Then
            xobj = 10
            yobj = Rnd()
            If yobj < 0 Then
                yobj = Rnd() * 20 + 165
            Else
                yobj = Rnd() * 30 + 120
            End If

        End If
    End Sub

    Sub hitbox()
        Dim myPen1 As Pen
        Dim myPen2 As Pen
        myPen1 = New Pen(Drawing.Color.FromArgb(255, 0, 0, 255), 2)
        myPen2 = New Pen(Drawing.Color.FromArgb(255, 255, 0, 0), 2)

        box1.X = posx
        box1.Y = posy
        box1.Width = stand.sprite.Width
        box1.Height = stand.sprite.Height

        box2.X = xobj
        box2.Y = yobj
        box2.Width = flyingObj.sprite.Width
        box2.Height = flyingObj.sprite.Height

        graph.DrawRectangle(myPen1, box1)
        graph.DrawRectangle(myPen2, box2)

    End Sub

    Sub reppukenBox()
        Dim myPen As Pen
        myPen = New Pen(Drawing.Color.FromArgb(0, 255, 0, 0), 2)
        box3.X = xRep
        box3.Y = yRep
        box3.Width = reppuken.sprite.Width
        box3.Height = reppuken.sprite.Height
        graph.DrawRectangle(myPen, box3)
    End Sub

    Function Collision() As Boolean
        isCollision1 = False

        If box1.Left < box2.Right And box2.Left < box1.Right And box2.Top < box1.Bottom And box1.Top < box2.Bottom Then
            isCollision1 = True
        End If

        Return isCollision1
    End Function

    Function Collision2() As Boolean
        isCollision2 = False

        If box3.Left < box2.Right And box2.Left < box3.Right And box2.Top < box3.Bottom And box3.Top < box2.Bottom Then
            isCollision2 = True
        End If

        Return isCollision2
    End Function

    Private Sub Form1_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        bmp = New Bitmap(My.Resources.bkg3)
        graph = Graphics.FromImage(bmp)
        stand.sprite = My.Resources.idleR
        flyingObj.sprite = My.Resources.ball
        reppuken.sprite = My.Resources.reppuken_01R
        reppuken.sprite = My.Resources.reppuken_01L

        facing = 1
        posx = 200
        posy = 120
        vx = 10
        vy = 0
        
        attack = False
        shoot = False
        lose = False
        state = 0
        score = 0
        indexIntro = 0

        SetIntro()
        SetMoveRight()
        SetMoveLeft()
        SetJumpR()
        SetJumpL()
        SetCrouchR()
        SetCrouchL()
        SetPunchR()
        SetPunchL()
        SetKickR()
        SetKickL()
        SetJumpAtkR()
        SetJumpAtkL()
        SetCrouchAtkR()
        SetCrouchAtkL()
        SetJumpFwdR()
        SetJumpFwdL()
        SetReppukenR()
        SetReppukenL()
        SetStanceR()
        SetStanceL()
        SetStaggerR()
        SetStaggerL()
        SetCrouchStgR()
        SetCrouchStgL()
        SetExplode()
        random_move()
        Timer1.Interval = 100
        Timer1.Start()
        PictureBox1.Image = bmp
    End Sub

    Sub DrawAgain()
        bmp = New Bitmap(My.Resources.bkg3)
        graph = Graphics.FromImage(bmp)
        PutSprite(bmp, stand.sprite, posx, posy)
        hitbox()
        reppukenBox()
        If Timer2.Enabled Then
            PutSprite(bmp, flyingObj.sprite, xobj, yobj)
        End If
        If shoot = True Then
            PutSprite(bmp, reppuken.sprite, xRep, yRep)
        End If
    End Sub
    
    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer1.Tick
        Timer1.Interval = 1
        If state = 0 Then 'intro
            If indexIntro < 10 Then
                stand = intro(indexIntro)
                If indexIntro >= 0 And indexIntro <= 2 Then
                    posx = 160
                    posy = 100
                    Timer1.Interval = 400
                ElseIf indexIntro > 2 And indexIntro <= 8 Then
                    posx = 200
                    posy = 120
                    Timer1.Interval = 10
                ElseIf indexIntro = 9 Then
                    Timer2.Start()
                End If
                indexIntro = indexIntro + 1
            End If

        ElseIf state = 1 Then 'move
            If facing = 1 Then
                stand = MoveR(indexMR)
                If indexMR = 0 Then
                    posx = posx + 15
                ElseIf indexMR = 1 Then
                    posx = posx + 5
                ElseIf indexMR = 2 Then
                    posx = posx + 15
                End If
            Else
                stand = MoveL(indexML)
                If indexML = 0 Then
                    posx = posx - 15
                ElseIf indexML = 1 Then
                    posx = posx - 5
                ElseIf indexML = 2 Then
                    posx = posx - 15
                End If
            End If
            If indexMR = 3 Or indexML = 3 Then
                Timer1.Stop()
            End If

            indexMR += 1
            indexML += 1

        ElseIf state = 2 Then 'jump
        If facing = 1 Then
            stand = JumpR(indexJR)
            If indexJR = 0 Then
                posy = 120
            ElseIf indexJR = 1 Then
                posy = posy - 20
            ElseIf indexJR = 2 Then
                posy = posy + 20
            ElseIf indexJR = 4 Then
                posy = 120
                Timer1.Stop()
            End If
        Else
            stand = JumpL(indexJL)
            If indexJL = 0 Then
                posy = 120
            ElseIf indexJL = 1 Then
                posy = posy - 20
            ElseIf indexJL = 2 Then
                posy = posy + 20
            ElseIf indexJL = 4 Then
                posy = 120
                Timer1.Stop()
            End If
        End If
        currFrame += 1
        If currFrame >= JumpR(indexJR).frame Then
            indexJL = indexJL + 1
            indexJR = indexJR + 1
            currFrame = 0
        End If

        ElseIf state = 3 Then 'crouch
        If facing = 1 Then
            stand = CrouchR(indexCR)
            If indexCR = 0 Then
                    posy = CrouchR(0).center.Y
                Timer1.Stop()
            ElseIf indexCR = 1 Then
                posy = 120
                Timer1.Stop()
            End If
        Else
            stand = CrouchL(indexCL)
            If indexCL = 0 Then
                posy = 180
                Timer1.Stop()
            ElseIf indexCL = 1 Then
                posy = 120
                Timer1.Stop()
            End If
        End If
        indexCR = indexCR + 1
        indexCL = indexCL + 1

        ElseIf state = 4 Then 'kick
        If facing = 1 Then
            attack = True
            stand = KickR(indexKR)
            If indexKR = 1 Then
                attack = True
                    posx = posx + 10
            End If
            If indexKR = 4 Then
                posx = posx - 10
                attack = False
                Timer1.Stop()
            End If
        Else
            attack = True
            stand = KickL(indexKL)
            If indexKL = 1 Then
                attack = True
                    posx = KickL(1).center.X - 100
            End If
            If indexKL = 4 Then
                posx = posx + 10
                attack = False
                Timer1.Stop()
            End If
        End If
        indexKR = indexKR + 1
        indexKL = indexKL + 1

        ElseIf state = 5 Then 'punch
        If facing = 1 Then
            attack = True
            stand = PunchR(indexPR)
            If indexPR = 4 Or indexPR = 5 Then
                attack = True
                posx = posx + 10
            End If
                If indexPR = 6 Then
                    posx = posx - 10
                    attack = False
                    Timer1.Stop()
                End If
        Else
            attack = True
            stand = PunchL(indexPL)
            If indexPL = 4 Or indexPL = 5 Then
                attack = True
                posx = PunchL(4).center.X - 100
            End If
            If indexPL = 6 Then
                posx = posx + 10
                attack = False
                Timer1.Stop()
            End If
        End If
        indexPR = indexPR + 1
        indexPL = indexPL + 1

        ElseIf state = 6 Then 'jump attack
        If facing = 1 Then
            stand = JumpAtkR(indexJAR)
            If indexJAR = 0 Then
                posy = 120
            ElseIf indexJAR = 1 Then
                posy = posy - 20
            ElseIf indexJAR = 3 Then
                attack = True
            ElseIf indexJAR = 4 Then
                posy = posy + 20
            ElseIf indexJAR = 6 Then
                posy = 120
                Timer1.Stop()
            End If
        Else
            stand = JumpAtkL(indexJAL)
            If posy = 120 Then
                posy = posy - 70
            End If
            If indexJAL = 0 Then
                posy = 120
            ElseIf indexJAL = 3 Then
                attack = True
            ElseIf indexJAL = 5 Then
                posy = posy + 70
            ElseIf indexJAL = 6 Then
                posy = 120
                Timer1.Stop()
            End If
            End If
            currFrame += 1
            If currFrame >= JumpAtkR(indexJAR).frame Then
                indexJAR = indexJAR + 1
                indexJAL = indexJAL + 1
                currFrame = 0
            End If

            ElseIf state = 7 Then 'crouch attack
                If facing = 1 Then
                    stand = CrouchAtkR(indexCAR)

                    If indexCAR = 0 Then
                        posy = CrouchAtkR(0).center.Y
                    ElseIf indexCAR = 1 Then
                        posy = CrouchAtkR(1).center.Y
                        attack = True
                    ElseIf indexCAR = 2 Then
                        posy = 120
                        Timer1.Stop()
                    End If
                Else
                    stand = CrouchAtkL(indexCAL)
                    If indexCAL = 0 Then
                        posy = CrouchAtkL(0).center.Y
                    ElseIf indexCAL = 1 Then
                        posy = CrouchAtkL(1).center.Y
                    posx = CrouchAtkL(1).center.X - 120
                        attack = True
                    ElseIf indexCAL = 2 Then
                        posy = 120
                        Timer1.Stop()
                    End If
                End If
                indexCAR = indexCAR + 1
                indexCAL = indexCAL + 1

            ElseIf state = 8 Then 'jump forward
                If facing = 1 Then
                    stand = JumpFwdR(indexJFR)
                    If indexJFR = 0 Then
                        posy = 120
                    ElseIf indexJFR = 1 Then
                        posx = posx + Cos(60 * PI / 400) * 50
                        posy = posy - Sin(30 * PI / 400) * 150
                    ElseIf indexJFR = 2 Then
                        posx = posx + Cos(60 * PI / 180) * 50
                        posy = posy + Sin(30 * PI / 400) * 150
                    ElseIf indexJFR = 4 Then
                        posy = 120
                        Timer1.Stop()
                    End If
                Else
                    stand = JumpFwdL(indexJFL)
                    If indexJFL = 0 Then
                        posy = 120
                    ElseIf indexJFL = 1 Then
                        posx = posx - Cos(60 * PI / 400) * 50
                        posy = posy - Sin(30 * PI / 400) * 150
                    ElseIf indexJFL = 2 Then
                        posx = posx - Cos(60 * PI / 180) * 50
                        posy = posy + Sin(30 * PI / 400) * 150
                    ElseIf indexJFL = 4 Then
                        posy = 120
                        Timer1.Stop()
                    End If
                End If
                currFrame += 1
                If currFrame >= JumpFwdR(indexJFR).frame Then
                    indexJFR = indexJFR + 1
                    indexJFL = indexJFL + 1
                    currFrame = 0
                End If

            ElseIf state = 9 Then 'reppuken <- NEMBAK
                If facing = 1 Then
                    shootFacing = 1
                    If indexShoot = 1 Then
                        stand = RepStanceR(indexStanceR)
                        attack = False
                        If indexStanceR = 1 Then
                            Timer1.Stop()
                            attack = False
                        End If
                        reppuken.sprite = ReppukenR(0).sprite
                        xRep = posx + 70
                        yRep = posy - 10
                        shoot = True
                        Timer2.Start()
                    Else
                        attack = True
                        stand = RepStanceR(indexStanceR)
                    End If
                Else
                    shootFacing = -1
                    If indexShoot = 1 Then
                        stand = RepStanceL(indexStanceL)
                        attack = True
                        If indexStanceL = 1 Then
                            Timer1.Stop()
                            attack = False
                        End If
                        reppuken.sprite = ReppukenL(0).sprite
                        xRep = posx - 50
                        yRep = posy - 10
                        shoot = True
                        Timer2.Start()
                    Else
                        attack = True
                        stand = RepStanceL(indexStanceL)
                    End If
                End If
                indexStanceR += 1
                indexStanceL += 1
                indexShoot += 1
            End If
            DrawAgain()
            PictureBox1.Image = bmp

    End Sub

    Private Sub Timer2_Tick(ByVal sender As Object, ByVal e As EventArgs) Handles Timer2.Tick
        Timer2.Interval = 1
        DrawAgain()
        PictureBox1.Image = bmp

        If RandomNumber = 1 Then
            xobj -= 20
        ElseIf RandomNumber = 2 Then
            xobj += 20
        End If


        If xobj <= -vx Or xobj >= bmp.Width - flyingObj.sprite.Width - vx Then
            random_move()
        End If

        If Collision() Then
            Timer2.Stop()
            If attack Then
                score += 1
                lblScore.Text = score
                If score = 10 Then
                    Timer1.Stop()
                    Timer2.Stop()
                    Timer3.Stop()
                    choose = MessageBox.Show("YOU WIN" + Environment.NewLine + "Do you want to play again?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If choose = DialogResult.No Then
                        Close()
                    ElseIf choose = DialogResult.Yes Then
                        Application.Restart()
                    End If
                Else
                    Timer3.Start()
                    Timer2.Stop()
                    'Timer1.Stop()
                End If
                lose = False
                indexStaggerR = 0
                indexStaggerL = 0
                indexExplode = 0
                PictureBox1.Image = bmp
                DrawAgain()
                'random_move()
                Timer3.Start()
            Else 'lose condition
                Timer3.Start()
                'Timer2.Stop()
                'Timer1.Stop()
                indexStaggerR = 0
                indexStaggerL = 0
                indexExplode = 0
                lose = True
                'attack = False
            End If
            attack = False
        End If

        If shootFacing = 1 Then
            xRep = xRep + 25 '<- NEMBAK KANAN
            If xRep >= bmp.Width - box3.Width + 5 Then
                shoot = False
            End If
            If Collision2() Then
                lose = False
                shoot = False
                score += 1
                lblScore.Text = score
                indexExplode = 0
                If score = 10 Then
                    Timer1.Stop()
                    Timer2.Stop()
                    Timer3.Stop()
                    choose = MessageBox.Show("YOU WIN" + Environment.NewLine + "Do you want to play again?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If choose = DialogResult.No Then
                        Close()
                    ElseIf choose = DialogResult.Yes Then
                        Application.Restart()
                    End If
                Else
                    Timer3.Start()
                    Timer2.Stop()
                End If
                lose = False
                PictureBox1.Image = bmp
                DrawAgain()
                Timer2.Start()
            End If

        ElseIf shootFacing = -1 Then
            xRep = xRep - 25 'NEMBAK KIRI
            If xRep <= -xRep - 10 Then
                shoot = False
            End If
            If Collision2() Then
                lose = False
                shoot = False
                score += 1
                lblScore.Text = score
                indexExplode = 0
                If score = 10 Then
                    Timer1.Stop()
                    Timer2.Stop()
                    Timer3.Stop()
                    choose = MessageBox.Show("YOU WIN" + Environment.NewLine + "Do you want to play again?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If choose = DialogResult.No Then
                        Close()
                    ElseIf choose = DialogResult.Yes Then
                        Application.Restart()
                    End If
                Else
                    Timer3.Start()
                    Timer2.Stop()
                End If
                lose = False
                PictureBox1.Image = bmp
                DrawAgain()
                Timer2.Start()
            End If
        End If

    End Sub

    Private Sub Timer3_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        If lose Then
            'If facing = 1 And indexCR = 0 Then
            '    stand = CrouchStgR(indexCSR)
            '    flyingObj = Explode(indexExplode)
            '    PutSprite(bmp, stand.sprite, posx, posy)
            '    PutSprite(bmp, flyingObj.sprite, xobj, yobj)
            '    PictureBox1.Image = bmp
            '    If indexCSR = 0 Then
            '        posy = 100
            '    ElseIf indexCSR = 1 Then
            '        posy = 100
            '    ElseIf indexCSR = 2 And indexExplode = 2 Then
            '        posy = 100
            '        DrawAgain()
            '        PictureBox1.Image = bmp
            '        Timer3.Stop()
            '        Timer1.Stop()
            '        choose = MessageBox.Show("YOU LOSE" + Environment.NewLine + "Do you want to play again?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            '        If choose = DialogResult.No Then
            '            Close()
            '        ElseIf choose = DialogResult.Yes Then
            '            Application.Restart()
            '        End If
            '    End If
            '    indexCSR += 1
            '    indexExplode += 1

            'ElseIf facing = -1 And indexCL = 0 Then
            '    stand = CrouchStgL(indexCSL)
            '    flyingObj = Explode(indexExplode)
            '    PutSprite(bmp, stand.sprite, posx, posy)
            '    PutSprite(bmp, flyingObj.sprite, xobj, yobj)
            '    PictureBox1.Image = bmp
            '    If indexCSL = 2 And indexExplode = 2 Then
            '        DrawAgain()
            '        PictureBox1.Image = bmp
            '        Timer3.Stop()
            '        Timer1.Stop()
            '        choose = MessageBox.Show("YOU LOSE" + Environment.NewLine + "Do you want to play again?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            '        If choose = DialogResult.No Then
            '            Close()
            '        ElseIf choose = DialogResult.Yes Then
            '            Application.Restart()
            '        End If
            '    End If
            '    indexCSL += 1
            '    indexExplode += 1
            'End If

            If facing = 1 Then
                stand = StaggerR(indexStaggerR)
                flyingObj = Explode(indexExplode)
                PutSprite(bmp, stand.sprite, posx, posy)
                PutSprite(bmp, flyingObj.sprite, xobj, yobj)
                PictureBox1.Image = bmp
                If indexStaggerR = 2 And indexExplode = 2 Then
                    DrawAgain()
                    PictureBox1.Image = bmp
                    Timer3.Stop()
                    Timer1.Stop()
                    choose = MessageBox.Show("YOU LOSE" + Environment.NewLine + "Do you want to play again?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If choose = DialogResult.No Then
                        Close()
                    ElseIf choose = DialogResult.Yes Then
                        Application.Restart()
                    End If
                End If
                indexStaggerR += 1
                indexExplode += 1
            Else
                stand = StaggerL(indexStaggerL)
                flyingObj = Explode(indexExplode)
                PutSprite(bmp, stand.sprite, posx, posy)
                PutSprite(bmp, flyingObj.sprite, xobj, yobj)
                PictureBox1.Image = bmp
                If indexStaggerL = 2 And indexExplode = 2 Then
                    DrawAgain()
                    PictureBox1.Image = bmp
                    Timer3.Stop()
                    Timer1.Stop()
                    choose = MessageBox.Show("YOU LOSE" + Environment.NewLine + "Do you want to play again?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                    If choose = DialogResult.No Then
                        Close()
                    ElseIf choose = DialogResult.Yes Then
                        Application.Restart()
                    End If
                End If
                indexStaggerL += 1
                indexExplode += 1
            End If
        Else
            flyingObj = Explode(indexExplode)
            If xobj < 0 Then
                xobj = 0
            End If

            If xobj + flyingObj.sprite.Width >= bmp.Width Then
                xobj = bmp.Width - flyingObj.sprite.Width - 1
            End If

            'If yobj < 0 Then
            '    yobj = 0
            'End If

            'If yobj + flyingObj.sprite.Height >= bmp.Height Then
            '    yobj = bmp.Height - flyingObj.sprite.Height - 1
            'End If

            PutSprite(bmp, flyingObj.sprite, xobj, yobj)
            PictureBox1.Image = bmp
            indexExplode += 1
            If indexExplode = 2 Then
                Timer3.Stop()
                flyingObj.sprite = My.Resources.ball
                random_move()
            End If
            xobj = 0 '-> NOTED
            Timer2.Start()
        End If
    End Sub
    
End Class

'RESPAWN OBJECT BERMASALAH