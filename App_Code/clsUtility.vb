Imports System.Globalization
Imports System.Data
Imports System.IO
Imports System.Security.Cryptography

Public Class clsUtility
    Public dt As New DataTable
    Public dr As DataRow

    Public Function ReplaceVariables(str As String, drPers As DataRow, drPartij As DataRow, drFacKop As DataRow, drArtikel As DataRow, drReservering As DataRow, drReserveringItem As DataRow, dtAdres As DataTable) As String

        'Personen
        If drPers IsNot Nothing Then
            str = str.Replace("[titel]", drPers.Item("sTitel"))
            str = str.Replace("[voornaam]", drPers.Item("sVoorletters"))
            str = str.Replace("[achternaam]", drPers.Item("sNaam"))
            str = str.Replace("[telefoon_persoon]", drPers.Item("sTelefoon"))
            str = str.Replace("[email_persoon]", drPers.Item("sEmail"))
            str = str.Replace("[mobiel_persoon]", drPers.Item("sMobiel"))
        End If

        'Adres
        If dtAdres IsNot Nothing Then
            For Each drAdres As DataRow In dtAdres.Rows
                Select Case drAdres.Item("sType").toLower
                    Case "bezoek"
                        str = str.Replace("[bezoekadres_straat]", drAdres.Item("sStraat"))
                        str = str.Replace("[bezoekadres_huisnr]", drAdres.Item("sHuisNr"))
                        str = str.Replace("[bezoekadres_postcode]", drAdres.Item("sPostcode"))
                        str = str.Replace("[bezoekadres_plaats]", drAdres.Item("sPlaats"))
                        str = str.Replace("[bezoekadres_gemeente]", drAdres.Item("sGemeente"))
                        str = str.Replace("[bezoekadres_provincie]", drAdres.Item("sProvincie"))
                        str = str.Replace("[bezoekadres_land]", drAdres.Item("sLand"))
                    Case "lever"
                        str = str.Replace("[leveradres_straat]", drAdres.Item("sStraat"))
                        str = str.Replace("[leveradres_huisnr]", drAdres.Item("sHuisNr"))
                        str = str.Replace("[leveradres_postcode]", drAdres.Item("sPostcode"))
                        str = str.Replace("[leveradres_plaats]", drAdres.Item("sPlaats"))
                        str = str.Replace("[leveradres_gemeente]", drAdres.Item("sGemeente"))
                        str = str.Replace("[leveradres_provincie]", drAdres.Item("sProvincie"))
                        str = str.Replace("[leveradres_land]", drAdres.Item("sLand"))
                    Case "factuur"
                        str = str.Replace("[factuuradres_straat]", drAdres.Item("sStraat"))
                        str = str.Replace("[factuuradres_huisnr]", drAdres.Item("sHuisNr"))
                        str = str.Replace("[factuuradres_postcode]", drAdres.Item("sPostcode"))
                        str = str.Replace("[factuuradres_plaats]", drAdres.Item("sPlaats"))
                        str = str.Replace("[factuuradres_gemeente]", drAdres.Item("sGemeente"))
                        str = str.Replace("[factuuradres_provincie]", drAdres.Item("sProvincie"))
                        str = str.Replace("[factuuradres_land]", drAdres.Item("sLand"))
                End Select
            Next

        End If

        'Partijen
        If drPartij IsNot Nothing Then
            str = str.Replace("[bedrijfsnaam]", drPartij.Item("sNaam"))
            str = str.Replace("[klantnummer]", drPartij.Item("sKlantNummer"))
            str = str.Replace("[rekeningnummer]", drPartij.Item("sRekeningNummer"))
            str = str.Replace("[rekeninghouder]", drPartij.Item("sRekeningHouder"))
            str = str.Replace("[kvknummer]", drPartij.Item("sKvkNummer"))
            str = str.Replace("[btwnummer]", drPartij.Item("sBtwNummer"))
            str = str.Replace("[telefoon_bedrijf]", drPartij.Item("sTelefoon"))
            str = str.Replace("[email_bedrijf]", drPartij.Item("sEmail"))
            str = str.Replace("[mobiel_bedrijf]", drPartij.Item("sMobiel"))
            str = str.Replace("[website_bedrijf]", drPartij.Item("sWebsite"))
            str = str.Replace("[twitter]", drPartij.Item("sTwitter"))
            str = str.Replace("[pinterest]", drPartij.Item("sPinterest"))
            str = str.Replace("[facebook]", drPartij.Item("sFacebook"))
            str = str.Replace("[instagram]", drPartij.Item("sInstagram"))
            str = str.Replace("[linkedin]", drPartij.Item("sLinkedIn"))
            str = str.Replace("[googleplus]", drPartij.Item("sGoogle"))
        End If

        'Facturen
        If drFacKop IsNot Nothing Then
            str = str.Replace("[factuur]", drFacKop.Item("sFactuur"))
            str = str.Replace("[datum]", drFacKop.Item("dDatum"))
            str = str.Replace("[vervaldatum]", drFacKop.Item("dVervaldatum"))
            str = str.Replace("[partijgegevens]", drFacKop.Item("sPartijGegevens"))
            str = str.Replace("[verzendgegevens]", drFacKop.Item("sVerzendGegevens"))
            str = str.Replace("[factuurbedrag]", drFacKop.Item("iFactuurBedrag"))
            str = str.Replace("[factuurbedragexcl]", drFacKop.Item("iFactuurBedragExcl"))
            str = str.Replace("[btwbedrag]", drFacKop.Item("iBtwBedrag"))
            str = str.Replace("[status]", drFacKop.Item("sStatus"))
            str = str.Replace("[referentie]", drFacKop.Item("sReferentie"))
        End If

        'Artikelen
        If drArtikel IsNot Nothing Then
            str = str.Replace("[artikel]", drArtikel.Item("sArtikel"))
            str = str.Replace("[omschrijving]", drArtikel.Item("sOmschrijving"))
            str = str.Replace("[prijs]", drArtikel.Item("iPrijs"))
        End If

        'Reserveringen
        If drReservering IsNot Nothing Then
            str = str.Replace("[titel]", drReservering.Item("sTitle"))
            str = str.Replace("[datum]", CDate(drReservering.Item("dtStart")).ToShortDateString())
            str = str.Replace("[tijd]", CDate(drReservering.Item("dtStart")).ToShortTimeString())
            str = str.Replace("[partijgegevens]", drReservering.Item("sPartijGegevens"))
            str = str.Replace("[verzendgegevens]", drReservering.Item("sVerzendGegevens"))
        End If
        'Reserveringen
        If drReserveringItem IsNot Nothing Then
            str = str.Replace("[aantal_personen]", drReserveringItem.Item("iAantalPersonen"))
            str = str.Replace("[opmerkingen]", drReserveringItem.Item("sOpmerkingen"))
            str = str.Replace("[bijzonderheden]", drReserveringItem.Item("sBijzonderheden"))

        End If

        Return str
    End Function

    Public Sub updateCart(ByVal p As Page, ByVal sSessieID As String, Optional ByVal showDiv As Boolean = False)
        Dim BP As New BasePage
        Dim lbl As Literal = p.Master.FindControl("ltlAantal")
        Dim lbl2 As Literal = p.Master.FindControl("ltlAantal1")
        Dim FR As New clsFacRgl

        Dim iAantal As String = FR.scAantalFacRgls_Session(sSessieID, BP.iPartijIDBeheerder)
        If CInt(iAantal) > 0 Then
            'lbl.Text = CInt(FACRGL.daFacRgl.scAantalArtikelen_Session(sSessieID, BP.iPartijIDBeheerder))
        Else
            'lbl.Text = 0
        End If
    End Sub

    Public Function sURL(sDomein As String, sDomeinExtensie As String) As String
        Return URL().Replace("[domein]", sDomein).Replace("[domeinextensie]", sDomeinExtensie)
    End Function

    Public Function sLocatie(sPad As String) As String
        Return Locatie().Replace("[folder]", sPad)
    End Function

    'Public Function sName() As String
    '    Return Config("Name")
    'End Function

    Public Function sSearch(ByVal iTaalID As Long, ByVal sCode As String, ByVal sWords As String) As DataTable
        dt.Columns.Add("sTitle")
        dt.Columns.Add("sDescription")
        dt.Columns.Add("iPrijs")
        dt.Columns.Add("sLink")

        Dim AI As New clsArtikelItems
        Dim CT As New clsCategorie
        Dim ROUTE As New clsRoutes

        Dim BP As New BasePage
        AI.dt = AI.sArtikelItemsLIKEWaarde(sWords, iTaalID)

        For Each AI.dr In AI.dt.Rows

            Dim sPrijs As String = ""
            Dim sTitle As String = ""
            Dim sDescription As String = ""
            Dim sLink As String = ""
            Dim iArtikelID As Long = AI.dr.Item("iArtikelID")

            AI.dt2 = AI.sArtikelItemsByArtikelID(iArtikelID, iTaalID)
            For Each AI.dr2 In AI.dt2.Rows
                Select Case AI.dr2.Item("sType").ToString.ToLower
                    Case "naam"
                        sTitle = AI.dr2.Item("sWaarde")
                    Case "lange omschrijving"
                        sDescription = AI.dr2.Item("sWaarde")
                End Select
            Next
            ROUTE.dt = ROUTE.sRoutes("artikel", iArtikelID, iTaalID)
            Dim bAdd As Boolean = True
            If ROUTE.dt.Rows.Count > 0 Then
                ROUTE.dr = ROUTE.dt.Rows(0)
                sLink = ROUTE.dr.Item("sURL")
            Else
                bAdd = False
            End If
            If bAdd Then
                dt.Rows.Add(sTitle, sDescription, sPrijs, sLink)
            End If
        Next
        Return dt
    End Function

    'Public Sub updateCart(ByVal p As Page, ByVal sSessieID As String, Optional ByVal showDiv As Boolean = False)
    '    Dim BP As New BasePage
    '    Dim lbl As Literal = p.Master.FindControl("ltlAantal")
    '    Dim lbl2 As Literal = p.Master.FindControl("ltlAantal1")
    '    Dim FACRGL As New clsFacRgl

    '    Dim iAantal As String = FACRGL.scAantalFacRgls_Session(sSessieID, BP.iPartijIDBeheerder)
    '    If CInt(iAantal) > 0 Then
    '        'lbl.Text = CInt(FACRGL.scAantalArtikelen_Session(sSessieID, BP.iPartijIDBeheerder))
    '    Else
    '        'lbl.Text = 0
    '    End If
    'End Sub

    Public Sub FillBirthdayDropdownLists(ByVal ddlDays As DropDownList, ByVal ddlMonth As DropDownList, ByVal ddlYears As DropDownList)
        'Dim culture As CultureInfo = CultureInfo.GetCultureInfo("nl-NL")
        For day As Integer = 1 To 31
            Dim li As New ListItem()
            li.Text = day.ToString()
            li.Value = day.ToString()
            ddlDays.Items.Add(li)
        Next

        Dim dtfi As New DateTimeFormatInfo()

        For month As Integer = 1 To 12
            Dim li As New ListItem()

            'li.Text = dtfi.GetMonthName(month) + " (" + month.ToString() + ")"
            li.Text = month.ToString()
            li.Value = month.ToString()
            ddlMonth.Items.Add(li)
        Next

        Dim thisYear As Integer = Date.Now.Year
        Dim startYear As Integer = thisYear - 18
        For year As Integer = startYear To startYear - 99 Step -1
            Dim li As New ListItem()
            li.Text = year.ToString()
            li.Value = year.ToString()
            ddlYears.Items.Add(li)
        Next
    End Sub

    Public Function sHoogsteLevertijdByFacID(ByVal iFacKopID As Long) As String
        'LET OP DEZE FUNCTIE PAKT VELD iVrdFysiek als levertijd
        Dim sLevertijd As String
        Try
            Dim ART As New clsArtikelen
            ART.dt = ART.sArtikelenByFacKopID(iFacKopID)
            Dim i As Integer = 0
            Dim iLevertijden(ART.dt.Rows.Count - 1) As Integer
            Dim iLevertijd As Integer
            For Each ART.dr In ART.dt.Rows
                iLevertijd = ART.dr.Item("iLevertijd")
                iLevertijden(i) = iLevertijd
                i = i + 1
            Next
            sLevertijd = iLevertijden.Max & " werkdag(en)"
        Catch ex As Exception
            sLevertijd = "n.t.b."
        End Try
        Return sLevertijd

    End Function

    Public Function sHoogsteLevertijdBySessieID(ByVal sSessieID As String) As String
        'LET OP DEZE FUNCTIE PAKT VELD iVrdFysiek als levertijd
        Dim sLevertijd As String
        Try
            Dim ART As New clsArtikelen
            ART.dt = ART.sArtikelenBySessieID(sSessieID)
            Dim i As Integer = 0
            Dim iLevertijden(ART.dt.Rows.Count - 1) As Integer
            Dim iLevertijd As Integer
            For Each ART.dr In ART.dt.Rows
                iLevertijd = ART.dr.Item("iLevertijd")
                iLevertijden(i) = iLevertijd
                i = i + 1
            Next
            sLevertijd = iLevertijden.Max & " werkdag(en)"
        Catch ex As Exception
            sLevertijd = "n.t.b."
        End Try
        Return sLevertijd

    End Function

    Public Function LocatieUrl(ByVal sLocatie As String) As String
        Try
            Dim sPadRoot As String = Config("Bestanden.Url")
            sLocatie = sLocatie.Replace("~\", "")
            sLocatie = sLocatie.Replace("~/", "")
            sLocatie = sLocatie.Replace("\", "/")
            Return sPadRoot & sLocatie
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function Domein() As String
        Try
            Return Config("Domein")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function Locatie() As String
        Try
            Return Config("Locatie")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function URL() As String
        Try
            Return Config("URL")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function Name() As String
        Try
            Return Config("Name")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function LocatieUrlCms(ByVal sLocatie As String) As String
        Try
            Dim sPadRoot As String = Config("URLCms")
            sLocatie = sLocatie.Replace("~\", "")
            sLocatie = sLocatie.Replace("~/", "")
            sLocatie = sLocatie.Replace("\", "/")
            Return sPadRoot & sLocatie
        Catch ex As Exception
            Return ""
        End Try
    End Function


    Public Function Config(ByVal sItem As String) As String
        Try
            Return System.Configuration.ConfigurationManager.AppSettings.Item(sItem).ToString
        Catch ex As Exception
            ' TODO error afhandeling
            ' If sLoggen = True Then Logging("Fout in DtrConfig, meestal geen key in webconfig file aanwezig key: " & sItem, ex.Message)
            Return Nothing
        End Try
    End Function

    Public Function GenereerWachtWoord() As String
        Try

            Dim sWw As String = ""
            Dim sUwrLwr As String = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"
            Dim sSpec As String = "!@#$%&*?-+="
            Dim sCijfer As String = "0123456789"
            Dim i As Integer
            Dim s As String = ""
            Dim iUwrLwr, iSpec, iCijfer, iTot As Integer

            While iTot < 6
                Randomize()
                i = CInt(Int((3 * Rnd()) + 1))
                Select Case i
                    Case 1
                        If iUwrLwr < 4 Then
                            Randomize()
                            i = CInt(Int((52 * Rnd()) + 1))
                            sWw &= Mid(sUwrLwr, i, 1)
                            iUwrLwr += 1
                            iTot += 1
                        End If
                    Case 2
                        If iSpec < 1 Then
                            Randomize()
                            i = CInt(Int((8 * Rnd()) + 1))
                            sWw &= Mid(sSpec, i, 1)
                            iSpec += 1
                            iTot += 1
                        End If
                    Case 3
                        If iCijfer < 1 Then
                            Randomize()
                            i = CInt(Int((10 * Rnd()) + 1))
                            sWw &= Mid(sCijfer, i, 1)
                            iCijfer += 1
                            iTot += 1
                        End If
                End Select
            End While

            Return sWw

        Catch ex As Exception
            Return "TgD589%@" + Format(Now, "hhmmss")
        End Try
    End Function

    Public Function Encrypt(ByVal encryptString As String) As String
        Dim EncryptionKey As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim clearBytes As Byte() = Encoding.Unicode.GetBytes(encryptString)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(EncryptionKey, New Byte() {73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As MemoryStream = New MemoryStream()
                Using cs As CryptoStream = New CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write)
                    cs.Write(clearBytes, 0, clearBytes.Length)
                    cs.Close()
                End Using

                encryptString = Convert.ToBase64String(ms.ToArray())
            End Using
        End Using

        Return encryptString
    End Function

    Public Function Decrypt(ByVal cipherText As String) As String
        Dim EncryptionKey As String = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        cipherText = cipherText.Replace(" ", "+")
        Dim cipherBytes As Byte() = Convert.FromBase64String(cipherText)
        Using encryptor As Aes = Aes.Create()
            Dim pdb As Rfc2898DeriveBytes = New Rfc2898DeriveBytes(EncryptionKey, New Byte() {73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118})
            encryptor.Key = pdb.GetBytes(32)
            encryptor.IV = pdb.GetBytes(16)
            Using ms As MemoryStream = New MemoryStream()
                Using cs As CryptoStream = New CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write)
                    cs.Write(cipherBytes, 0, cipherBytes.Length)
                    cs.Close()
                End Using

                cipherText = Encoding.Unicode.GetString(ms.ToArray())
            End Using
        End Using

        Return cipherText
    End Function
End Class