Imports System.Data
Imports System.Data.SqlClient

Public Class clsImages
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public Function sImageByIDAndSoort(iID As Long, sSoort As String, sTussType As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Images INNER JOIN tussImages ON Images.iImageID = tussImages.iImageID WHERE (tussImages.iID = @iID) AND (Images.sSoort = @sSoort) AND (tussImages.sTussType = @sTussType) ORDER BY Images.iVolgorde"
        cmd.Parameters.AddWithValue("@iID", iID)
        cmd.Parameters.AddWithValue("@sSoort", sSoort)
        cmd.Parameters.AddWithValue("@sTussType", sTussType)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sImageByIDAndInfo(iID As Long, sInfo As String, sTussType As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Images INNER JOIN tussImages ON Images.iImageID = tussImages.iImageID WHERE (tussImages.iID = @iID) AND (Images.sInfo = @sInfo) AND (tussImages.sTussType = @sTussType) ORDER BY Images.iVolgorde"
        cmd.Parameters.AddWithValue("@iID", iID)
        cmd.Parameters.AddWithValue("@sInfo", sInfo)
        cmd.Parameters.AddWithValue("@sTussType", sTussType)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sImageByTussTypeAndID(sTussType As String, iID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT * FROM Images INNER JOIN tussImages ON Images.iImageID = tussImages.iImageID WHERE (tussImages.sTussType = @sTussType) AND (tussImages.iID = @iID) ORDER BY Images.iVolgorde"
        cmd.Parameters.AddWithValue("@sTussType", sTussType)
        cmd.Parameters.AddWithValue("@iID", iID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sImageByPageAndParentAndTussType(sQs As String, sTussType As String, iTaalID As Long, sParentPage As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Images.iImageID AS ImageID, tussImages.iID, Images.sType AS ImagesType, Images.sOriginal, Images.sBig, Images.sSmall, Images.sData, Images.sInfo, Images.sSoort, Images.iVolgorde, tussTaalPages.sQueryString, tussTaalPages.iTaalID, Pages.sURL FROM tussTaalPages INNER JOIN Images INNER JOIN tussImages ON Images.iImageID = tussImages.iImageID ON tussTaalPages.iPageID = tussImages.iID INNER JOIN Pages ON tussTaalPages.iPageID = Pages.iPageID INNER JOIN tussTaalPages AS tussTaalPages2 ON pages.iParentID = tussTaalPages2.iPageID AND tussTaalPages2.sQueryString = @sParentPage WHERE (tussImages.sTussType = @TussType) AND (tussTaalPages.iTaalID = @iTaalID) AND (tussTaalPages.sQueryString = @sQs) ORDER BY Images.iVolgorde"
        cmd.Parameters.AddWithValue("@sQs", sQs)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@TussType", sTussType)
        cmd.Parameters.AddWithValue("@sParentPage", sParentPage)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sImageByPageAndTussType(sQs As String, sTussType As String, iTaalID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Images.iImageID AS ImageID, tussImages.iID, Images.sType AS ImagesType, Images.sOriginal, Images.sBig, Images.sSmall, Images.sData, Images.sInfo, Images.sSoort, Images.iVolgorde, tussTaalPages.sQueryString, tussTaalPages.iTaalID FROM tussTaalPages INNER JOIN Images INNER JOIN tussImages ON Images.iImageID = tussImages.iImageID ON tussTaalPages.iPageID = tussImages.iID WHERE (tussImages.sTussType = @TussType) AND (tussTaalPages.iTaalID = @iTaalID) AND (tussTaalPages.sQueryString = @sQs) ORDER BY Images.iVolgorde"
        cmd.Parameters.AddWithValue("@sQs", sQs)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@TussType", sTussType)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sImageByPageAndTussTypeAndsSoort(sQs As String, sTussType As String, iTaalID As Long, sSoort As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Images.iImageID AS ImageID, tussImages.iID, Images.sType AS ImagesType, Images.sOriginal, Images.sBig, Images.sSmall, Images.sData, Images.sInfo, Images.sSoort, Images.iVolgorde, tussTaalPages.sQueryString, tussTaalPages.iTaalID FROM tussTaalPages INNER JOIN Images INNER JOIN tussImages ON Images.iImageID = tussImages.iImageID ON tussTaalPages.iPageID = tussImages.iID WHERE (tussImages.sTussType = @TussType) AND (tussTaalPages.iTaalID = @iTaalID) AND (tussTaalPages.sQueryString = @sQs) AND (Images.sSoort = @sSoort) ORDER BY Images.iVolgorde"
        cmd.Parameters.AddWithValue("@sQs", sQs)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        cmd.Parameters.AddWithValue("@TussType", sTussType)
        cmd.Parameters.AddWithValue("@sSoort", sSoort)
        Return CON.sDatatable(cmd)
    End Function

    'Public Function iscImage(ByVal upl As HttpPostedFile, ByVal sType As String, ByVal sData As String, ByVal sInfo As String) As Long
    '    Dim iBigImageWidth, iBigImageHeight, iSmallImageWidth, iSmallImageHeight As Integer
    '    Dim iPartijID As Long = HttpContext.Current.Profile.Item("iPartijID")


    '    iSmallImageWidth = CLng(SETTING.daSettings.scSettingByTypeAndGroup("Images", sType & "_Small_Width", iPartijID))
    '    iSmallImageHeight = CLng(SETTING.daSettings.scSettingByTypeAndGroup("Images", sType & "_Small_Width", iPartijID))

    '    iBigImageWidth = CLng(SETTING.daSettings.scSettingByTypeAndGroup("Images", sType & "_Big_Width", iPartijID))
    '    iBigImageHeight = CLng(SETTING.daSettings.scSettingByTypeAndGroup("Images", sType & "_Big_Height", iPartijID))


    '    Dim sMelding As String = ""

    '    'Guid Aanmaken
    '    Dim guid As Guid = Guid.NewGuid()
    '    'Volledige foto locatie (origineel)
    '    Dim U As New clsUtility
    '    Dim sFolder As String = U.Locatie().Replace("[folder]", HttpContext.Current.Profile.Item("sPad"))

    '    Dim OriginalLoc As String = sFolder & "Uploads\Images\Original\" & guid.ToString() & upl.FileName
    '    Dim BigLoc As String = sFolder & "Uploads\Images\Big\" & guid.ToString() & upl.FileName
    '    Dim SmallLoc As String = sFolder & "\Uploads\Images\Small\" & guid.ToString() & upl.FileName

    '    Dim OriginalLoc1 As String = "~/Uploads/Images/Original/" & guid.ToString() & upl.FileName
    '    Dim BigLoc1 As String = "~/Uploads/Images/Big/" & guid.ToString() & upl.FileName
    '    Dim SmallLoc1 As String = "~/Uploads/Images/Small/" & guid.ToString() & upl.FileName
    '    'Extensie controle
    '    Dim Extension As String = Path.GetExtension(upl.FileName).ToLower()
    '    Dim allowedExtensions As String() = {".png", ".jpeg", ".jpg", ".gif", ".PNG", ".JPEG", ".JPG", ".GIF"}

    '    Dim FileOK As Boolean = False
    '    For i As Integer = 0 To allowedExtensions.Length - 1
    '        If Extension = allowedExtensions(i) Then
    '            FileOK = True
    '        End If
    '    Next

    '    If FileOK = False Then
    '        Return 0
    '    Else
    '        upl.SaveAs(OriginalLoc)
    '        If Compress(OriginalLoc, BigLoc, iBigImageWidth, iBigImageHeight, True) = True Then
    '            If Compress(OriginalLoc, SmallLoc, iSmallImageWidth, iSmallImageHeight, False) = True Then
    '                Return daImg.iscImage(sType, OriginalLoc1, BigLoc1, SmallLoc1, sData, sInfo, 0)
    '            End If
    '            Return 0
    '        Else
    '            Return 0
    '        End If
    '    End If
    'End Function

    'Public Function iscNoImage(ByVal sType As String, ByVal sData As String, ByVal sInfo As String) As Long

    '    Dim guid As Guid = Guid.NewGuid()

    '    Dim FileName As String = "no-image.png"

    '    Dim U As New clsUtility
    '    Dim sFolder As String = U.Locatie().Replace("[folder]", HttpContext.Current.Profile.Item("sPad"))

    '    File.Copy(Server.MapPath(sFolder & "Uploads/Editor/no-image.png"), Server.MapPath(sFolder & "Uploads/Images/Big/" & guid.ToString() & FileName))
    '    File.Copy(Server.MapPath(sFolder & "Uploads/Editor/no-image.png"), Server.MapPath(sFolder & "Uploads/Images/Original/" & guid.ToString() & FileName))
    '    File.Copy(Server.MapPath(sFolder & "Uploads/Editor/no-image.png"), Server.MapPath(sFolder & "Uploads/Images/Small/" & guid.ToString() & FileName))

    '    Dim OriginalLoc As String = sFolder & "Uploads/Images/Original/" & guid.ToString() & FileName
    '    Dim BigLoc As String = sFolder & "Uploads/Images/Big/" & guid.ToString() & FileName

    '    Return daImg.iscImage(sType, OriginalLoc, BigLoc, BigLoc, sData, sInfo, 0)

    'End Function

    'Public Sub dImage(ByVal iImageID As Long)
    '    Try
    '        dtImg = daImg.sImage(iImageID)

    '        Dim U As New clsUtility
    '        Dim sFolder As String = U.Locatie().Replace("[folder]", HttpContext.Current.Profile.Item("sPad"))

    '        If dtImg.Rows.Count > 0 Then
    '            drImg = dtImg.Rows(0)
    '            File.Delete(sFolder & drImg.sSmall.Replace("~/", "").Replace("/", "\"))
    '            File.Delete(sFolder & drImg.sBig.Replace("~/", "").Replace("/", "\"))
    '            File.Delete(sFolder & drImg.sOriginal.Replace("~/", "").Replace("/", "\"))
    '        End If
    '        daImg.Delete(iImageID)
    '    Catch ex As Exception
    '        'LOG.iLog("clsImages", "dImage", "Fout bij het verwijderen van images Of image record.")
    '    End Try
    'End Sub


    'Public Function Compress(ByVal sBestandTmp As String, ByVal sBestand As String, ByVal iBreedte As Integer, ByVal iHoogte As Integer, ByVal FitDimensions As Boolean) As Boolean
    '    Try
    '        ' zet originele foto in bitmap object
    '        Dim bmpOrigineel As New Bitmap(sBestandTmp)

    '        Dim lSchaal As Single
    '        Dim lTmp As Single
    '        ' bepaal de schaal om te comprimeren

    '        If FitDimensions = True Then
    '            If bmpOrigineel.Width > bmpOrigineel.Height Then
    '                ' liggend
    '                lSchaal = iBreedte / bmpOrigineel.Width
    '                iBreedte = iBreedte
    '                lTmp = bmpOrigineel.Height * lSchaal
    '                iHoogte = lTmp
    '                If iHoogte > iHoogte Then
    '                    iBreedte = iBreedte * (iHoogte / iHoogte)
    '                    iHoogte = iHoogte * (iHoogte / iHoogte)
    '                End If
    '            Else
    '                ' staand
    '                lSchaal = iHoogte / bmpOrigineel.Height
    '                iHoogte = iHoogte
    '                lTmp = bmpOrigineel.Width * lSchaal
    '                iBreedte = lTmp
    '                If iBreedte > iBreedte Then
    '                    iBreedte = iBreedte * (iBreedte / iBreedte)
    '                    iHoogte = iHoogte * (iBreedte / iBreedte)
    '                End If
    '            End If
    '        Else
    '            '' Schaal bepalen
    '            If bmpOrigineel.Width < bmpOrigineel.Height Then
    '                ' liggend
    '                lSchaal = iBreedte / bmpOrigineel.Width
    '                iBreedte = iBreedte
    '                lTmp = bmpOrigineel.Height * lSchaal
    '                iHoogte = lTmp
    '                If iHoogte > iHoogte Then
    '                    iBreedte = iBreedte * (iHoogte / iHoogte)
    '                    iHoogte = iHoogte * (iHoogte / iHoogte)
    '                End If
    '            Else
    '                ' staand
    '                lSchaal = iHoogte / bmpOrigineel.Height
    '                iHoogte = iHoogte
    '                lTmp = bmpOrigineel.Width * lSchaal
    '                iBreedte = lTmp
    '                If iBreedte > iBreedte Then
    '                    iBreedte = iBreedte * (iBreedte / iBreedte)
    '                    iHoogte = iHoogte * (iBreedte / iBreedte)
    '                End If
    '            End If
    '        End If


    '        ' Maak een bitmap voor het resultaat van het comprimeren
    '        Dim bmpCopy As New Bitmap(iBreedte, iHoogte, PixelFormat.Format24bppRgb)

    '        ' 1 maak een Graphics object voor het comprimeren van de bitmap
    '        Dim grCopy As Graphics = Graphics.FromImage(bmpCopy)
    '        grCopy.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
    '        grCopy.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic


    '        ' Comprimeer de foto in het Graphics object
    '        grCopy.DrawImage(bmpOrigineel, -1, -1, bmpCopy.Width + 1, bmpCopy.Height + 1)

    '        ' Zet de bitmap in een image
    '        Dim imgCopy As Image
    '        imgCopy = bmpCopy
    '        imgCopy.Save(sBestand)

    '        bmpCopy.Dispose()
    '        bmpOrigineel.Dispose()
    '        imgCopy.Dispose()
    '        grCopy.Dispose()

    '        Return True
    '    Catch ex As Exception
    '        ' TODO error afhandeling
    '        'If bLoggen = True Then Logging(Reflection.MethodInfo.GetCurrentMethod.ToString, ex.Message)
    '        Return False
    '    End Try
    'End Function
End Class