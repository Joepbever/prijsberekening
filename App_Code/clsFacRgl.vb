Imports System.Data
Imports System.Data.SqlClient

Public Class clsFacRgl
    Public dt As New DataTable
    Public dr As DataRow

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand

    Public sBedrag As String
    Public sVerzendkosten As String
    Public sBedragMinusKorting As String
    Public sBedragMinusKortingZonderVerzendkosten As String
    Public sDiscount As String
    Public bDiscount As Boolean = False
    Public sAantalArtikelen As String
    Public iAantalRegels As Integer = 0
    Public iBtw As Decimal

    Public Function scAantalArtikelen_Session(sSession As String, iPartijID As Long, sType As String) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT SUM(iAantal) AS iAantalCount FROM FacRgl WHERE (sSession = @sSession) AND (iPartijID = @iPartijID) AND (sType = @sType)"
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.Scalar(cmd)
    End Function

    Public Function sFacRglsAndVerzendkosten(sSession As String, iPartijID As Long, sType As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT FacRgl.*, ArtikelVarianten.iPrijsVerzendkosten FROM FacRgl INNER JOIN ArtikelVarianten ON FacRgl.iArtikelVariantID = ArtikelVarianten.iArtikelVariantID WHERE (FacRgl.sSession = @sSession) And (FacRgl.iPartijID = @iPartijID) And (FacRgl.sType = @sType)"
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.sDatatable(cmd)
    End Function

    Public Function uFacRglBySession(iFacKopID As Long, sFactuur As String, sSessionNew As String, sSession As String) As Boolean
        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE FacRgl SET iFacKopID = @iFacKopID, sFactuur = @sFactuur, sSession = @sSessionNew WHERE sSession = @sSession "
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        cmd.Parameters.AddWithValue("@sFactuur", sFactuur)
        cmd.Parameters.AddWithValue("@sSessionNew", sSessionNew)
        cmd.Parameters.AddWithValue("@sSession", sSession)
        Return CON.Update(cmd)
    End Function

    Public Function dFacRglByFacRglID(iFacRglID As Long) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "DELETE FROM FacRgl WHERE (iFacRglID = @iFacRglID)"
        cmd.Parameters.AddWithValue("@iFacRglID", iFacRglID)
        Return CON.Update(cmd)
    End Function

    Public Function sFacRglsByTypeAndSessionAndData(sType As String, sSession As String, iPartijID As Long, sData As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = " SELECT * FROM FacRgl WHERE (FacRgl.sSession = @sSession) AND (iPartijID = @iPartijID) AND (sType = @sType) AND (sData = @sData)"
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sData", sData)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sFacRglsByTypeAndSession(sType As String, sSession As String, iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = " SELECT * FROM FacRgl WHERE (FacRgl.sSession = @sSession) AND (iPartijID = @iPartijID) AND (sType = @sType)"
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sFacRgls(sSession As String, iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT FacRgl.* FROM FacRgl INNER JOIN Artikelen ON FacRgl.iArtikelID = Artikelen.iArtikelID WHERE (FacRgl.sSession = @sSession) AND (FacRgl.iPartijID = @iPartijID) AND (FacRgl.sType = 'artikel')"
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sFacRglByFacRglID(iFacRglID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = " SELECT * FROM FacRgl WHERE (iFacRglID = @iFacRglID)"
        cmd.Parameters.AddWithValue("@iFacRglID", iFacRglID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sFacRglsByFacKopID(iFacKopID As Long, sType As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = " SELECT * FROM FacRgl WHERE (iFacKopID = @iFacKopID) AND (sType = @sType)"
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.sDatatable(cmd)
    End Function
    Public Function sFacRgls_SessionNoArt(sSession As String, iPartijID As Long, sType As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = " SELECT * FROM FacRgl WHERE (FacRgl.sSession = @sSession) AND (FacRgl.iPartijID = @iPartijID) AND (FacRgl.sType = @sType)"
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.sDatatable(cmd)
    End Function
    Public Function sFacRgls_SessionAndImg(sSession As String, iPartijID As Long, sType As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT facrgl.ifacrglid, " &
                            "       facrgl.ifackopid, " &
                            "       facrgl.sfactuur, " &
                            "       facrgl.iaantal, " &
                            "       facrgl.somschrijving, " &
                            "       artikelen.somschrijving   AS sArtikelOmschrijving, " &
                            "       artikelen.sartikelcode, " &
                            "       artikelen.ivrdbeschikbaar AS iVrdBeschikbaarArtikel, " &
                            "       facrgl.ibedrag, " &
                            "       facrgl.ibtwbedrag, " &
                            "       facrgl.istuksprijs, " &
                            "       facrgl.ibtwperc, " &
                            "       facrgl.sartikel, " &
                            "       facrgl.sopmerking, " &
                            "       facrgl.sdata, " &
                            "       facrgl.iitemsid, " &
                            "       facrgl.iartikelid, " &
                            "       facrgl.ssession, " &
                            "       facrgl.ipartijid, " &
                            "       facrgl.iartikelvariantid, " &
                            "       artikelen.sartikelgroep, " &
                            "       artikelvarianten.swaarde, " &
                            "       artikelvarianten.ivrdbeschikbaar, " &
                            "	   images.*" &
                            "FROM   facrgl " &
                            "       INNER JOIN artikelen " &
                            "               ON facrgl.iartikelid = artikelen.iartikelid " &
                            "       LEFT JOIN artikelvarianten " &
                            "               ON facrgl.iartikelvariantid = artikelvarianten.iartikelvariantid " &
                            "       LEFT JOIN tussimages " &
                            "              ON tussimages.itussid = (SELECT TOP 1 itussid " &
                            "                                       FROM   tussimages " &
                            "                                       WHERE  tussimages.iid = " &
                            "                                              artikelen.iartikelid " &
                            "                                              AND stusstype = @sType) " &
                            "       LEFT JOIN images " &
                            "              ON images.iimageid = tussimages.iimageid " &
                            "WHERE  ( facrgl.ssession = @sSession ) " &
                            "       AND ( facrgl.ipartijid = @iPartijID ) " &
                            "       AND ( facrgl.stype = @sType )"
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sFacRgls_Session(sSession As String, iPartijID As Long, sType As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT FacRgl.iFacRglID, FacRgl.iFacKopID, FacRgl.sFactuur, FacRgl.iAantal, FacRgl.sOmschrijving, Artikelen.iVrdBeschikbaar AS iVrdBeschikbaarArtikel, Artikelen.sOmschrijving AS sArtikelOmschrijving, Artikelen.sArtikelCode, FacRgl.iBedrag, FacRgl.iBtwBedrag, FacRgl.iStuksPrijs, FacRgl.iBtwPerc, FacRgl.sArtikel, FacRgl.sOpmerking, FacRgl.sData, FacRgl.iItemsID, FacRgl.iArtikelID, FacRgl.sSession, FacRgl.iPartijID, FacRgl.iArtikelVariantID, Artikelen.sArtikelGroep, ArtikelVarianten.sWaarde, ArtikelVarianten.iVrdBeschikbaar FROM FacRgl INNER JOIN Artikelen ON FacRgl.iArtikelID = Artikelen.iArtikelID LEFT JOIN ArtikelVarianten ON FacRgl.iArtikelVariantID = ArtikelVarianten.iArtikelVariantID WHERE (FacRgl.sSession = @sSession) AND (FacRgl.iPartijID = @iPartijID) AND (FacRgl.sType = @sType)"
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sFacRgls_Session_Related(sSession As String, iPartijID As Long, sType As String) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Artikelen.sKorting, Artikelen.bIngelogd, Artikelen.bUitgelogd, FacRgl.iFacRglID, FacRgl.iFacKopID, FacRgl.sFactuur, FacRgl.iAantal, FacRgl.sOmschrijving, FacRgl.iBedrag, FacRgl.iBtwBedrag, FacRgl.iStuksPrijs, FacRgl.iBtwPerc, Artikelen.iArtikelID, FacRgl.sSession, FacRgl.iPartijID, Artikelen.sArtikel, Artikelen.sSpecificatieGroep, Artikelen.sOmschrijving AS Expr1, Artikelen.iPrijs, Artikelen.iPrijsInkoop, Routes.sURL FROM FacRgl INNER JOIN tussArtikelen ON FacRgl.iArtikelID = tussArtikelen.iArtikelID_1 INNER JOIN Artikelen ON tussArtikelen.iArtikelID_2 = Artikelen.iArtikelID INNER JOIN Routes ON Artikelen.iArtikelID = Routes.iID WHERE (FacRgl.sSession = @sSession) AND (FacRgl.iPartijID = @iPartijID) AND (FacRgl.sType = @sType) AND (Routes.sType = @sType)"
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sFacRgls_Session_Related_Routes_Items(sSession As String, iPartijID As Long, sType As String, iTaalID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Artikelen.sKorting, Artikelen.bIngelogd, Artikelen.bUitgelogd, Artikelen.iArtikelID, Artikelen.iPrijs, Routes.sURL, ArtikelItems.sWaarde FROM            FacRgl INNER JOIN                         tussArtikelen ON FacRgl.iArtikelID = tussArtikelen.iArtikelID_1 INNER JOIN Artikelen ON tussArtikelen.iArtikelID_2 = Artikelen.iArtikelID INNER JOIN                         Routes ON Artikelen.iArtikelID = Routes.iID INNER JOIN                         ArtikelItems ON Routes.iID = ArtikelItems.iArtikelID WHERE        (Routes.sType = @sType) AND (FacRgl.sSession = @sSession) AND (FacRgl.iPartijID = @iPartijID) AND (FacRgl.sType = @sType) AND (ArtikelItems.sType = 'Naam') AND (ArtikelItems.iTaalID = @iTaalID)"
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iTaalID", iTaalID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function scAantalFacRgls_Session(sSession As String, iPartijID As Long) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT COUNT(*) FROM FacRgl WHERE (sSession = @sSession) AND (iPartijID = @iPartijID)"
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.Scalar(cmd)
    End Function

    Public Function scTotaalBedrag_Session(sSession As String, iPartijID As Long, sType As String) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT SUM(iBedrag) As iTotaalBedrag FROM FacRgl WHERE (sSession = @sSession) AND (iPartijID = @iPartijID) AND (sType = @sType)"
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.Scalar(cmd)
    End Function

    Public Function uFacRglVanuitWw(iBedrag As Decimal, iBtwBedrag As Decimal, iStuksPrijs As Decimal, iAantal As Long, iFacRglID As Long) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE FacRgl SET iBedrag = @iBedrag, iBtwBedrag = @iBtwBedrag, iStuksPrijs = @iStuksPrijs, iAantal = @iAantal WHERE (iFacRglID = @iFacRglID)"
        cmd.Parameters.AddWithValue("@iBedrag", iBedrag)
        cmd.Parameters.AddWithValue("@iBtwBedrag", iBtwBedrag)
        cmd.Parameters.AddWithValue("@iStuksPrijs", iStuksPrijs)
        cmd.Parameters.AddWithValue("@iAantal", iAantal)
        cmd.Parameters.AddWithValue("@iFacRglID", iFacRglID)
        Return CON.Update(cmd)
    End Function

    Public Function sArtikel_ArtikelID_Session(iArtikelID As Long, sSession As String, iArtikelVariantID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT iFacRglID, iFacKopID, sFactuur, iAantal, sOmschrijving, iBedrag, iBtwBedrag, iStuksPrijs, iBtwPerc, sArtikel, sOpmerking, sData, iItemsID, iArtikelID, sSession FROM FacRgl WHERE (iArtikelID = @iArtikelID) AND (sSession = @sSession) AND (iArtikelVariantID = @iArtikelVariantID)"
        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iArtikelVariantID", iArtikelVariantID)
        Return CON.sDatatable(cmd)
    End Function

    Public Function iscFacRgl(iFacKopID As Long, sFactuur As String, iAantal As Long, sOmschrijving As String, iBedrag As Decimal, iBtwBedrag As Decimal, iStuksPrijs As Decimal, iBtwPerc As Decimal, sArtikel As String, sOpmerking As String, sData As String, iItemsID As Long, iArtikelID As Long, sSession As String, iPartijID As Long, iArtikelVariantID As Long, sType As String) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO FacRgl (iFacKopID, sFactuur, iAantal, sOmschrijving, iBedrag, iBtwBedrag, iStuksPrijs, iBtwPerc, sArtikel, sOpmerking, sData, iItemsID, iArtikelID, sSession, iPartijID, iArtikelVariantID, sType) VALUES (@iFacKopID,@sFactuur,@iAantal,@sOmschrijving,@iBedrag,@iBtwBedrag,@iStuksPrijs,@iBtwPerc,@sArtikel,@sOpmerking,@sData,@iItemsID,@iArtikelID,@sSession,@iPartijID,@iArtikelVariantID, @sType); SELECT SCOPE_IDENTITY()"
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        cmd.Parameters.AddWithValue("@sFactuur", sFactuur)
        cmd.Parameters.AddWithValue("@iAantal", iAantal)
        cmd.Parameters.AddWithValue("@sOmschrijving", sOmschrijving)
        cmd.Parameters.AddWithValue("@iBedrag", iBedrag)
        cmd.Parameters.AddWithValue("@iBtwBedrag", iBtwBedrag)
        cmd.Parameters.AddWithValue("@iStuksPrijs", iStuksPrijs)
        cmd.Parameters.AddWithValue("@iBtwPerc", iBtwPerc)
        cmd.Parameters.AddWithValue("@sArtikel", sArtikel)
        cmd.Parameters.AddWithValue("@sOpmerking", sOpmerking)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@iItemsID", iItemsID)
        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iArtikelVariantID", iArtikelVariantID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.Scalar(cmd)
    End Function

    Public Function uFacRgl(iFacKopID As Long, sFactuur As String, iAantal As Long, sOmschrijving As String, iBedrag As Decimal, iBtwBedrag As Decimal, iStuksPrijs As Decimal, iBtwPerc As Decimal, sArtikel As String, sOpmerking As String, sData As String, iItemsID As Long, iArtikelID As Long, sSession As String, iPartijID As Long, iArtikelVariantID As Long, sType As String, iFacRglID As Long) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE dbo.FacRgl SET iFacKopID = @iFacKopID, sFactuur = @sFactuur, iAantal = @iAantal, sOmschrijving = @sOmschrijving, iBedrag = @iBedrag, iBtwBedrag = @iBtwBedrag, iStuksPrijs = @iStuksPrijs, iBtwPerc = @iBtwPerc, sArtikel = @sArtikel, sOpmerking = @sOpmerking, sData = @sData, iItemsID = @iItemsID, iArtikelID = @iArtikelID, sSession = @sSession, iPartijID = @iPartijID, iArtikelVariantID = @iArtikelVariantID, sType = @sType WHERE (iFacRglID = @iFacRglID)"
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        cmd.Parameters.AddWithValue("@sFactuur", sFactuur)
        cmd.Parameters.AddWithValue("@iAantal", iAantal)
        cmd.Parameters.AddWithValue("@sOmschrijving", sOmschrijving)
        cmd.Parameters.AddWithValue("@iBedrag", iBedrag)
        cmd.Parameters.AddWithValue("@iBtwBedrag", iBtwBedrag)
        cmd.Parameters.AddWithValue("@iStuksPrijs", iStuksPrijs)
        cmd.Parameters.AddWithValue("@iBtwPerc", iBtwPerc)
        cmd.Parameters.AddWithValue("@sArtikel", sArtikel)
        cmd.Parameters.AddWithValue("@sOpmerking", sOpmerking)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@iItemsID", iItemsID)
        cmd.Parameters.AddWithValue("@iArtikelID", iArtikelID)
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@iArtikelVariantID", iArtikelVariantID)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@iFacRglID", iFacRglID)
        Return CON.Update(cmd)
    End Function

    Public Function sInfo(sSession As String, iPartijID As Long) As Boolean
        Dim iBedrag, iAantalArtikelen, iKorting, iVerzendKosten As Decimal


        dt = sFacRgls(sSession, iPartijID)
        For Each Me.dr In dt.Rows
            iAantalRegels = iAantalRegels + 1
            iAantalArtikelen += dr.Item("iAantal")
            iBtw += dr.Item("iBtwBedrag")
            iBedrag += dr.Item("iBedrag")
        Next
        dt = sFacRgls_SessionNoArt(sSession, iPartijID, "korting")
        For Each Me.dr In dt.Rows
            iKorting += dr.Item("iBedrag")
            iBtw += dr.Item("iBtwBedrag")
            bDiscount = True
        Next
        dt = sFacRgls_SessionNoArt(sSession, iPartijID, "verzendkosten")
        For Each Me.dr In dt.Rows
            iVerzendKosten = dr.Item("iBedrag")
            iBtw += dr.Item("iBtwBedrag")
            ' iBedrag += dr.Item("iBedrag")
        Next

        Dim BP As New BasePage
        Dim ST As New clsSettings

        sVerzendkosten = iVerzendKosten
        sAantalArtikelen = CInt(iAantalArtikelen)
        sDiscount = iKorting
        sBedrag = iBedrag
        sBedragMinusKortingZonderVerzendkosten = iBedrag + iKorting
        sBedragMinusKorting = iBedrag + iKorting + iVerzendKosten
        Return True
    End Function


    Public Function sFacRglsAndArtikelByTypeAndSession(sType As String, sSession As String, iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT Artikelen.dVerzendkosten, Artikelen.sBtw, Artikelen.iArtikelID, Artikelen.sAantalPerEenheid, FacRgl.iAantal FROM FacRgl INNER JOIN Artikelen ON FacRgl.iArtikelID = Artikelen.iArtikelID WHERE (FacRgl.sSession = @sSession) AND (FacRgl.iPartijID = @iPartijID) AND (FacRgl.sType = @sType)"
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sFacRglsByFacKopIDArtikel(iFacKopID As Long, iPartijID As Long) As DataTable
        cmd.Parameters.Clear()
        cmd.CommandText = "SELECT FacRgl.*, Artikelen.bIngelogd, Artikelen.bUitgelogd FROM FacRgl INNER JOIN Artikelen ON FacRgl.iArtikelID = Artikelen.iArtikelID WHERE (FacRgl.iFacKopID = @iFacKopID) AND (FacRgl.iPartijID = @iPartijID) AND (FacRgl.sType = 'artikel')"
        cmd.Parameters.AddWithValue("@iFacKopID", iFacKopID)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        Return CON.sDatatable(cmd)
    End Function
    Public Function dFacRglByType(iPartijID As Long, sSession As String, sType As String) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "DELETE FROM FacRgl WHERE (iPartijID = @iPartijID) AND (sSession = @sSession) AND (sType = @sType)"
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sSession", sSession)
        cmd.Parameters.AddWithValue("@sType", sType)
        Return CON.Update(cmd)
    End Function

    Dim BTW As New clsBtwTarieven

    Public Function RefreshVerzendkosten(iTaalID As Long, sSessieID As String, iPartijIDBeheerder As Long, iLandID As Long, iArtikelVerzendID As Long, sBetaalMethode As String) As Decimal
        Dim dVerzendkosten As Decimal = 0
        Dim BtwTarieven As New List(Of Integer)

        Dim FACRGL As New clsFacRgl
        Dim LAND As New clsLanden
        Dim ST As New clsSettings

        ST.InitWebshopSettings(iPartijIDBeheerder)


        Dim sNaam As String = ""
        Dim sOmschrijving As String = ""
        If ST.bVerzendkostenGratis Then
            dVerzendkosten = 0
        Else
            'Verzendkosten artikel
            dt = sFacRglsAndArtikelByTypeAndSession("artikel", sSessieID, iPartijIDBeheerder)
            For Each Me.dr In dt.Rows
                If Not IsDBNull(dr.Item("dVerzendKosten")) And dr.Item("dVerzendKosten") > dVerzendkosten Then 'Hoogste verzendkosten aanhouden
                    dVerzendkosten = dVerzendkosten + (dr.Item("dVerzendKosten") * dr.Item("iAantal"))
                End If
                ' BTW bepalen
                If Not IsDBNull(dr.Item("sBtw")) Then
                    BtwTarieven.Add(BTW.scBtwTarief(dr.Item("sBtw")))
                End If
            Next

            'Verzendkosten op basis van land
            Dim dVerzendkostenLand As Decimal = 0

            dVerzendkostenLand = LAND.BepaalVerzendkosten(iLandID)

            If dVerzendkostenLand > dVerzendkosten Then 'Als verzendkosten per artikel bij elkaar lager is dan laagste verzendkosten hanteren
                If LAND.bGebruikVerzendKostenLand Then
                    dVerzendkosten = dVerzendkostenLand
                Else
                    dVerzendkosten = dVerzendkosten
                End If
            Else
                dVerzendkosten = dVerzendkostenLand
            End If


            If LAND.bGratisVerzenden = False Then
                Dim A As New clsArtikelen
                'Verzendkosten op basis van methode
                A.dr = A.sArtikelByID(iArtikelVerzendID)
                sNaam = A.dr.Item("sArtikel")
                sOmschrijving = A.dr.Item("sOmschrijving")
                Dim dVerzendkostenMethode As Decimal = A.dr.Item("iPrijs")
                dVerzendkosten = dVerzendkosten + dVerzendkostenMethode
            End If


        End If


        If dt.Rows.Count > 0 Then
            Dim iBtwTotaal As Decimal
            Dim dMaxBtwPercentage As Decimal = BtwTarieven.Max()
            iBtwTotaal = dVerzendkosten / (dMaxBtwPercentage + 100) * dMaxBtwPercentage
            dt = sFacRglsByTypeAndSession("verzendkosten", sSessieID, iPartijIDBeheerder)
            If dt.Rows.Count > 0 Then
                dr = dt.Rows(0)
                dFacRglByFacRglID(dr.Item("iFacRglID"))
                iscFacRgl(0, "", 1, sOmschrijving, Math.Round(dVerzendkosten, 2), Math.Round(iBtwTotaal, 2), Math.Round(dVerzendkosten, 2), dMaxBtwPercentage, sNaam, "", "", 0, iArtikelVerzendID, sSessieID, iPartijIDBeheerder, 0, "verzendkosten")
            Else
                iscFacRgl(0, "", 1, sOmschrijving, Math.Round(dVerzendkosten, 2), Math.Round(iBtwTotaal, 2), Math.Round(dVerzendkosten, 2), dMaxBtwPercentage, sNaam, "", "", 0, iArtikelVerzendID, sSessieID, iPartijIDBeheerder, 0, "verzendkosten")
            End If


            ''tcc korting op bepaalde methodes
            'sInfo(sSessieID, iPartijIDBeheerder)
            'Dim dTotaal As Decimal = 0
            'Select Case sBetaalMethode.ToLower
            '    Case "ideal2"
            '        dTotaal = sBedragMinusKorting / 100 * 2
            '        sNaam = "iDEAL"
            '        sOmschrijving = "2% korting"
            '    Case "paypal2"
            '        dTotaal = sBedragMinusKorting / 100 * 1
            '        sNaam = "PayPal"
            '        sOmschrijving = "1% korting"
            '    Case Else
            '        dTotaal = sBedragMinusKorting
            'End Select
            'sDiscount = dTotaal
            'iBtwTotaal = dTotaal / (dMaxBtwPercentage + 100) * dMaxBtwPercentage
            'dt = sFacRglsByTypeAndSession("betaalmethodekorting", sSessieID, iPartijIDBeheerder)
            'If dt.Rows.Count > 0 Then
            '    dr = dt.Rows(0)
            '    dFacRglByFacRglID(dr.Item("iFacRglID"))
            '    iscFacRgl(0, "", 1, sOmschrijving, Math.Round(-dTotaal, 2), Math.Round(-iBtwTotaal, 2), Math.Round(-dTotaal, 2), dMaxBtwPercentage, sNaam, "", "", 0, 0, sSessieID, iPartijIDBeheerder, 0, "betaalmethodekorting")
            'Else
            '    iscFacRgl(0, "", 1, sOmschrijving, Math.Round(-dTotaal, 2), Math.Round(-iBtwTotaal, 2), Math.Round(-dTotaal, 2), dMaxBtwPercentage, sNaam, "", "", 0, 0, sSessieID, iPartijIDBeheerder, 0, "betaalmethodekorting")
            'End If
        Else
            'Verzendkosten verwijderen als er geen artikelen zijn
            dFacRglByType(iPartijIDBeheerder, sSessieID, "verzendkosten")
        End If
        Return dVerzendkosten
    End Function


    Public Function iArtikelInFacRgl(ByVal iArtikelID As Long, iArtikelVariantID As Long, ByVal sSession As String, ByVal iAantalArtikelen As Long, iPartijID As Long, sOmschrijving As String, iTaalID As Long) As Boolean
        Dim iBtwTarief, iBedrag, iAantal, iBtwTotaal, iArtikelPrijs As Decimal
        Dim ART As New clsArtikelen
        ART.dr = ART.sArtikelAndItemsByID(iArtikelID, iTaalID, "naam")
        iArtikelPrijs = ART.dr.Item("iPrijs")
        Try
            If IsNumeric(ART.dr.Item("sKorting")) Then
                Dim dKorting As Decimal = ART.dr.Item("sKorting").ToString().Replace(".", ",")
                If dKorting > 0 Then
                    iArtikelPrijs = iArtikelPrijs - dKorting
                End If
            End If
        Catch ex As Exception

        End Try

        dt = sArtikel_ArtikelID_Session(iArtikelID, sSession, iArtikelVariantID)
        Dim BTW As New clsBtwTarieven
        iBtwTarief = BTW.scBtwTarief(ART.dr.Item("sBtw"))

        If sOmschrijving = "" Then
            sOmschrijving = ART.dr.Item("sOmschrijving")
        End If
        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)
            iAantal = dr.Item("iAantal")
            iAantal = iAantal + CDec(iAantalArtikelen)
            iBedrag = iAantal * iArtikelPrijs
            iBtwTotaal = iBedrag / (iBtwTarief + 100) * iBtwTarief
            uFacRgl(0, "", iAantal, sOmschrijving, Math.Round(iBedrag, 2), Math.Round(iBtwTotaal, 2), Math.Round(iArtikelPrijs, 2), iBtwTarief, ART.dr.Item("sWaarde"), "", "", 0, iArtikelID, sSession, iPartijID, iArtikelVariantID, "artikel", dr.Item("iFacRglID"))
        Else
            iAantal = CDec(iAantalArtikelen)
            iBedrag = iAantal * iArtikelPrijs
            iBtwTotaal = iBedrag / (iBtwTarief + 100) * iBtwTarief
            iscFacRgl(0, "", iAantal, sOmschrijving, Math.Round(iBedrag, 2), Math.Round(iBtwTotaal, 2), Math.Round(iArtikelPrijs, 2), iBtwTarief, ART.dr.Item("sWaarde"), "", "", 0, iArtikelID, sSession, iPartijID, iArtikelVariantID, "artikel")
        End If


        'controle of er kortingspercentages zijn
        dt = sFacRglsByTypeAndSessionAndData("korting", sSession, iPartijID, "percentage")
        If dt.Rows.Count > 0 Then
            Dim KT As New clsKortingCodes
            For Each dr As DataRow In dt.Rows
                dFacRglByFacRglID(dr.Item("iFacRglID"))
                Dim iKortingsbedrag As Decimal = KT.KortingsBedragViaSession(iPartijID, dr.Item("sArtikel"), sSession)
                If KT.bOk = True Then 'controle of de korting nog geldig is.
                    iBtwTotaal = iKortingsbedrag / (iBtwTarief + 100) * iBtwTarief '(iBtwTarief / 100) * iKortingsbedrag
                    iscFacRgl(0, "", 0, KT.sOmschrijving, Math.Round(-iKortingsbedrag, 2), Math.Round(-iBtwTotaal, 2), Math.Round(-iKortingsbedrag, 2), iBtwTarief, KT.sKortingsCode, "", KT.sSoort, 0, 0, sSession, iPartijID, 0, "korting")
                End If
            Next
        End If


        Return True
    End Function



    Public Function sKortingByFacKopID(iFacKopID As Long) As Boolean
        Dim iKorting As Decimal
        Dim dt As New DataTable
        dt = sFacRglsByFacKopID(iFacKopID, "korting")
        If dt.Rows.Count > 0 Then
            For Each dr As DataRow In dt.Rows
                iKorting += dr.Item("iBedrag")
            Next
            sDiscount = iKorting.ToString()
        Else
            sDiscount = ""
        End If
        Return True
    End Function


    'Public daFacRgl As New dsFacRglTableAdapters.taFacRgl
    'Public dtFacRgl As New dsFacRgl.dtFacRglDataTable
    'Public drFacRgl As dsFacRgl.dtFacRglRow


    'Dim STF As New clsArtikelStaffel

    'Public Function InsertArtikel(ByVal iArtikelID As Long, ByVal sSession As String, ByVal iAantalArtikelen As String) As Boolean

    '    Try
    '        Dim iBtwTarief, iBedrag, iAantal, iBtwTotaal, iArtikelPrijs As Decimal

    '        Dim ART As New clsArtikelen
    '        ART.drART = ART.daART.sArtikel(iArtikelID).Rows(0)
    '        If Trim(ART.drART.sKorting) <> "" Then
    '            iArtikelPrijs = ART.drART.sKorting
    '        Else
    '            iArtikelPrijs = ART.drART.iPrijs
    '        End If

    '        Dim FACRGL As New clsFacRgl
    '        FACRGL.dtFacRgl = FACRGL.daFacRgl.sArtikel_ArtikelID_Session(iArtikelID, sSession)


    '        Dim BTW As New clsBtwTarieven
    '        iBtwTarief = BTW.daBTW.scBtwTarief(ART.drART.sBtw)

    '        If FACRGL.dtFacRgl.Rows.Count > 0 Then
    '            FACRGL.drFacRgl = FACRGL.dtFacRgl.Rows(0)
    '            iAantal = FACRGL.drFacRgl.iAantal
    '            iAantal = iAantal + CDec(iAantalArtikelen)
    '            STF.dtStaffel = STF.daStaffel.sArtikelStaffelByArtikelIDAndStaffel(iArtikelID, iAantal)
    '            If STF.dtStaffel.Rows.Count > 0 Then
    '                STF.drStaffel = STF.dtStaffel.Rows(0)
    '                iArtikelPrijs = STF.drStaffel.iPrijs
    '            End If
    '            iBedrag = iAantal * iArtikelPrijs
    '            iBtwTotaal = (iBtwTarief / 100) * iBedrag
    '            FACRGL.daFacRgl.Update(0, "", iAantal, ART.drART.sArtikelCode, iBedrag, iBtwTotaal, iArtikelPrijs, iBtwTarief, ART.drART.sArtikel, "", "", 0, iArtikelID, sSession, FACRGL.drFacRgl.iFacRglID)
    '        Else
    '            iAantal = CDec(iAantalArtikelen)
    '            STF.dtStaffel = STF.daStaffel.sArtikelStaffelByArtikelIDAndStaffel(iArtikelID, iAantal)
    '            If STF.dtStaffel.Rows.Count > 0 Then
    '                STF.drStaffel = STF.dtStaffel.Rows(0)
    '                iArtikelPrijs = STF.drStaffel.iPrijs
    '            End If
    '            iBedrag = iAantal * iArtikelPrijs
    '            'iBtwTotaal = iBedrag / 100 * iBtwTarief
    '            iBtwTotaal = (iBtwTarief / 100) * iBedrag
    '            FACRGL.daFacRgl.Insert(0, "", iAantal, ART.drART.sArtikelCode, iBedrag, iBtwTotaal, iArtikelPrijs, iBtwTarief, ART.drART.sArtikel, "", "", 0, iArtikelID, sSession)
    '        End If

    '    Catch ex As Exception
    '        Dim s As String = ex.Message
    '    End Try

    '    Return True

    'End Function

    Public iBedragRegel As Decimal
    Public Function uFacRglAantal(ByVal iAantal As Long, ByVal iFacRglID As Long, sSession As String) As Boolean
        Dim iBtwTarief, iBedrag, iBtwTotaal, iArtikelPrijs As Decimal
        dr = sFacRglByFacRglID(iFacRglID).Rows(0)

        Dim ART As New clsArtikelen
        ART.dr = ART.sArtikelByID(dr.Item("iArtikelID"))
        iArtikelPrijs = dr.Item("iStuksPrijs")

        Dim BTW As New clsBtwTarieven
        iBtwTarief = BTW.scBtwTarief(ART.dr.Item("sBtw"))

        iBedrag = iAantal * iArtikelPrijs
        iBedragRegel = iBedrag
        iBtwTotaal = iBedrag / (iBtwTarief + 100) * iBtwTarief

        uFacRglVanuitWw(Math.Round(iBedrag, 2), Math.Round(iBtwTotaal, 2), Math.Round(iArtikelPrijs, 2), iAantal, iFacRglID)

        Dim BP As New BasePage
        Dim dt As New DataTable
        dt = sFacRglsByTypeAndSessionAndData("korting", sSession, BP.iPartijIDBeheerder, "percentage")
        If dt.Rows.Count > 0 Then
            Dim KT As New clsKortingCodes
            For Each Me.dr In dt.Rows
                dFacRglByFacRglID(dr.Item("iFacRglID"))
                Dim iKortingsbedrag As Decimal = KT.KortingsBedragViaSession(BP.iPartijIDBeheerder, dr.Item("sArtikel"), sSession)
                If KT.bOk = True Then 'controle of de korting nog geldig is.
                    iBtwTotaal = iKortingsbedrag / (iBtwTarief + 100) * iBtwTarief
                    iscFacRgl(0, "", 0, KT.sOmschrijving, Math.Round(-iKortingsbedrag, 2), Math.Round(-iBtwTotaal, 2), Math.Round(-iKortingsbedrag, 2), iBtwTarief, KT.sKortingsCode, "", KT.sSoort, 0, 0, sSession, BP.iPartijIDBeheerder, 0, "korting")
                End If
            Next
        End If

        Return True
    End Function



End Class
