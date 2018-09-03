Imports System.Data.SqlClient
Imports MailChimp.Helper
Imports MailChimp
Imports System.IO

Public Class clsMail

    Dim BP As New BasePage
    Dim U As New clsUtility
    Dim LI As New clsLijstItems

    Dim CON As New clsConnection
    Dim cmd As New SqlCommand


    Public Sub RegistratieMailVersturen(iPartijIDBeheerder As Long, iTaalID As Long, sEmail As String, sVoornaam As String, sAchternaam As String)
        Dim LI As New clsLijstItems
        Dim U As New clsUtility
        LI.dt = LI.sLijstItemByTitleAndType("Registratie bevestiging", iTaalID, "Mailing")
        If LI.dt.Rows.Count > 0 Then
            LI.dr = LI.dt.Rows(0)
            Dim sBericht As String = LI.dr.Item("sDescription")
            Dim email As String = sEmail

            sBericht = sBericht.Replace("[name]", sVoornaam & " " & sAchternaam)
            sBericht = sBericht.Replace("[email]", sEmail)
            ' sBericht = sBericht.Replace("[subject]", txtTel.Text)
            ' sBericht = sBericht.Replace("[message]", txtMessage.Text.Replace(Environment.NewLine, "<br />"))

            Dim sType As String = LI.dr.Item("sTitle")
            Dim sVan As String = LI.dr.Item("sItem1").replace("[email]", email)
            Dim sNaar As String = LI.dr.Item("sColor").replace("[email]", email)
            Dim sCC As String = LI.dr.Item("sItem4").replace("[email]", email)
            Dim sBCC As String = LI.dr.Item("sItem5").replace("[email]", email)
            Dim sOnderwerp As String = LI.dr.Item("sSubTitle").replace("[email]", email)
            Dim sBijlagen As String = ""
            Dim sInfo As String = U.Config("Name")

            Dim sTemplate As String = sHtml("~/EmailTemplates/Mail.aspx")
            sTemplate = sTemplate.Replace("[subject]", sOnderwerp)
            sTemplate = sTemplate.Replace("[body]", sBericht)

            Dim iMailID As Long = iscMail(0, iPartijIDBeheerder, "versturen", sType, "", sVan, sNaar, sCC, sBCC, sOnderwerp, sTemplate, sBijlagen, "", sInfo, Now)

        End If
    End Sub


    Public Function uStatus(iID As Long, sStatus As String) As Long
        Dim cmd As New SqlCommand
        cmd.CommandText = "UPDATE Mail SET sStatus = @sStatus WHERE (iID = @iID)"
        cmd.Parameters.AddWithValue("@sStatus", sStatus)
        cmd.Parameters.AddWithValue("@iID", iID)
        Return CON.Update(cmd)
    End Function

    Public Function iscMail(iBcorePartijID As Long, iPartijID As Long, sStatus As String, sType As String, sItem As String, sVan As String,
                                              sNaar As String, sCC As String, sBCC As String, sOnderwerp As String, sBericht As String, sBijlagen As String,
                                              sData As String, sInfo As String, dtVerstuurd As Date) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "INSERT INTO Mail (iBcorePartijID, iPartijID, sStatus, sType, sItem, sVan, sNaar, sCC, sBCC, sOnderwerp, sBericht, sBijlagen, sData, sInfo, dtVerstuurd)VALUES (@iBcorePartijID, @iPartijID, @sStatus, @sType, @sItem, @sVan, @sNaar, @sCC, @sBCC, @sOnderwerp, @sBericht, @sBijlagen, @sData, @sInfo, @dtVerstuurd); SELECT SCOPE_IDENTITY()"
        cmd.Parameters.AddWithValue("@iBcorePartijID", iBcorePartijID)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sStatus", sStatus)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sItem", sItem)
        cmd.Parameters.AddWithValue("@sVan", sVan)
        cmd.Parameters.AddWithValue("@sNaar", sNaar)
        cmd.Parameters.AddWithValue("@sCC", sCC)
        cmd.Parameters.AddWithValue("@sBCC", sBCC)
        cmd.Parameters.AddWithValue("@sOnderwerp", sOnderwerp)
        cmd.Parameters.AddWithValue("@sBericht", sBericht)
        cmd.Parameters.AddWithValue("@sBijlagen", sBijlagen)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@sInfo", sInfo)
        cmd.Parameters.AddWithValue("@dtVerstuurd", dtVerstuurd)
        Return CON.Scalar(cmd)
    End Function

    Public Function uMail(iBcorePartijID As Long, iPartijID As Long, sStatus As String, sType As String, sItem As String, sVan As String,
                                              sNaar As String, sCC As String, sBCC As String, sOnderwerp As String, sBericht As String, sBijlagen As String,
                                              sData As String, sInfo As String, iMailID As Long) As Long
        cmd.Parameters.Clear()
        cmd.CommandText = "UPDATE Mail SET iBcorePartijID = @iBcorePartijID, iPartijID = @iPartijID, sStatus = @sStatus, sType = @sType, sItem = @sItem, sVan = @sVan, sNaar = @sNaar, sCC = @sCC, sBCC = @sBCC, sOnderwerp = @sOnderwerp, sBericht = @sBericht, sBijlagen = @sBijlagen, sData = @sData, sInfo = @sInfo WHERE (iID = @iMailID)"
        cmd.Parameters.AddWithValue("@iBcorePartijID", iBcorePartijID)
        cmd.Parameters.AddWithValue("@iPartijID", iPartijID)
        cmd.Parameters.AddWithValue("@sStatus", sStatus)
        cmd.Parameters.AddWithValue("@sType", sType)
        cmd.Parameters.AddWithValue("@sItem", sItem)
        cmd.Parameters.AddWithValue("@sVan", sVan)
        cmd.Parameters.AddWithValue("@sNaar", sNaar)
        cmd.Parameters.AddWithValue("@sCC", sCC)
        cmd.Parameters.AddWithValue("@sBCC", sBCC)
        cmd.Parameters.AddWithValue("@sOnderwerp", sOnderwerp)
        cmd.Parameters.AddWithValue("@sBericht", sBericht)
        cmd.Parameters.AddWithValue("@sBijlagen", sBijlagen)
        cmd.Parameters.AddWithValue("@sData", sData)
        cmd.Parameters.AddWithValue("@sInfo", sInfo)
        cmd.Parameters.AddWithValue("@iMailID", iMailID)
        Return CON.Update(cmd)
    End Function

    Public Function SubScribeMailchimp(ByVal sEmail As String) As Boolean
        Try
            Dim apikey As String = "1211de38ceae7d38129359aaae0427ea-us8"
            Dim list_id As String = "72002f39a4"
            ' Dim resp As String = ""

            'Dim request As WebRequest = WebRequest.Create("https://us11.api.mailchimp.com/2.0/?method=listSubscribe&output=xml&apikey=" & apikey & "&id=" & list_id & "&email_address=" & HttpContext.Current.Server.UrlEncode(sEmail))
            'request.Method = "POST"
            'Dim response As WebResponse = request.GetResponse()


            Dim mc As New MailChimpManager(apikey)

            '  Create the email parameter
            Dim email As New EmailParameter() With {.Email = sEmail}

            Dim results As EmailParameter = mc.Subscribe(list_id, email)

            'Dim xmlhttp = HttpContext.Current.Server.CreateObject("MSXML2.ServerXMLHTTP")
            'xmlhttp.Open("GET", "https://us11.api.mailchimp.com/2.0/?method=listSubscribe&output=xml&apikey=" & apikey & "&id=" & list_id & "&email_address=" & HttpContext.Current.Server.UrlEncode(sEmail), False) '& "&merge_vars="
            'xmlhttp.send()
            'resp = xmlhttp.statusText
            'xmlhttp = Nothing

            'If resp = "OK" Then
            '    Return True
            'Else
            '    Return False
            'End If
            ' Dim s As String = ""
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function sHtml(ByVal sPath As String) As String
        Dim sw As New StringWriter()
        Dim htw As New HtmlTextWriter(sw)
        HttpContext.Current.Server.Execute(sPath, htw)
        Return sw.ToString()
    End Function

    Public Function MailPassword(MP As MasterPage, sUsername As String, sLanguage As String) As Boolean
        Dim V As New clsValidatie
        Dim bOk As Boolean = True
        Dim username As String = sUsername

        If Trim(sUsername) = "" Then bOk = False
        If bOk = False Then
            'V.MessageModal(MP, "Verplicht: ", "Gebruikersnaam / e-mailadres")
            Return False
        End If
        Dim Users As MembershipUserCollection = Membership.FindUsersByName(username)
        If Users.Count < 1 Then
            'V.MessageModal(MP, "", "Account met gebruikersnaam: " & HttpContext.Current.Server.HtmlEncode(username) & " niet bekend. Controleer a.u.b. het ingevoerde gebruikersnaam.")
            Return False
        End If
        Dim sGegevens As String = ""
        Dim sEmail As String = ""
        Dim sUserID As String = ""
        For Each u As MembershipUser In Users
            '    sGegevens += "Gebruikersnaam: " + User.UserName + "<br />"
            sGegevens += u.GetPassword()
            sUserID = u.ProviderUserKey.ToString()
        Next

        Dim P As New clsPersonen
        P.dt = P.sPersoonByUserID(sUserID)

        If P.dt.Rows.Count > 0 Then
            P.dr = P.dt.Rows(0)
            sEmail = P.dr.Item("sEmail")
        End If

        LI.dt = LI.sLijstItemByTitleAndType("Wachtwoord vergeten", BP.sTaalID(sLanguage), "Mailing")
        If LI.dt.Rows.Count > 0 Then
            LI.dr = LI.dt.Rows(0)
            Dim sMsg As String = LI.dr.Item("sDescription")
            Dim sSubject As String = LI.dr.Item("sSubTitle")
            Dim sMailTemplate As String = sHtml("~/EmailTemplates/Mail.aspx")
            sMailTemplate = sMailTemplate.Replace("[subject]", sSubject)
            sMailTemplate = sMailTemplate.Replace("[body]", sGegevens)

            iscMail(0, BP.iPartijIDBeheerder, "versturen", LI.dr.Item("sTitle"), "", LI.dr.Item("sItem1"), sEmail, "", "", LI.dr.Item("sSubTitle"), sMailTemplate, "", "", U.Config("Name"), Now)
        End If

        Return True
    End Function

    Public Sub MailOrder(ByVal iFacKopID As String, ByVal sLanguage As String, bFactuur As Boolean)
        Dim FACKOP As New clsFacKop
        Dim PERS As New clsPersonen

        Dim sEmail As String = "nigel@websentiment.nl"
        Dim sFactuur As String = ""
        FACKOP.dt = FACKOP.sFacKopByFacKopID(iFacKopID)
        If FACKOP.dt.Rows.Count > 0 Then
            FACKOP.dr = FACKOP.dt.Rows(0)
            sFactuur = FACKOP.dr.Item("sFactuur")
            PERS.dt = PERS.sPersoonByPersID(FACKOP.dr.Item("iPersID"))
            If PERS.dt.Rows.Count > 0 Then
                PERS.dr = PERS.dt.Rows(0)
                sEmail = PERS.dr.Item("sEmail")
            End If
        End If

        Dim sFileName As String = iFacKopID.ToString() & "_" & sFactuur.ToString()
        Dim iTaalID As Long = BP.sTaalID(sLanguage)
        LI.dt = LI.sLijstItemByTitleAndType("Orderbevestiging", iTaalID, "Mailing")


        Dim sLocatieEnBestand As String = ""
        If bFactuur Then
            sLocatieEnBestand = BP.sLocatie & "facturen\" & sFileName & ".pdf"
        End If
        Dim sName As String = U.Config("Name")
        Dim sMsg, sSubject, sMailTemplate, sMailVan, sMailNaar As String
        Dim sTemplate As String = sHtml("~/EmailTemplates/Mail.aspx?iFacKopID=" & iFacKopID & "&iTaalID=" & iTaalID)
        If LI.dt.Rows.Count > 0 Then
            LI.dr = LI.dt.Rows(0)
            sMsg = LI.dr.Item("sDescription")
            sSubject = LI.dr.Item("sSubTitle").Replace("[factuur]", sFactuur)
            sMailVan = LI.dr.Item("sColor")
            sMailNaar = sEmail
            sMailTemplate = sTemplate.Replace("[subject]", sSubject)
            sMailTemplate = sMailTemplate.Replace("[body]", sMsg)

            Dim sVan As String = LI.dr.Item("sItem1").replace("[email]", sEmail)
            Dim sNaar As String = LI.dr.Item("sColor").replace("[email]", sEmail)
            Dim sCC As String = LI.dr.Item("sItem4").replace("[email]", sEmail)
            Dim sBCC As String = LI.dr.Item("sItem5").replace("[email]", sEmail)

            iscMail(0, BP.iPartijIDBeheerder, "versturen", LI.dr.Item("sTitle"), "", sVan, sNaar, sCC, sBCC, sSubject, sMailTemplate, sLocatieEnBestand, "", sName, Now)
        End If
    End Sub


    Public Sub MailRegistratie(sLanguage As String, sEmail As String, sBedrijfsnaam As String, sNaam As String, sAdres As String, sTel As String,
                               sVat As String, sKvk As String)
        LI.dt = LI.sLijstItemByTitleAndType("Accountaanvraag naar aanvrager", BP.sTaalID(sLanguage), "Mailing")
        Dim sName As String = U.Config("Name")
        Dim sMsg, sSubject, sMailTemplate, sMailVan, sMailNaar As String
        Dim sTemplate As String = sHtml("~/EmailTemplates/Mail.aspx")
        If LI.dt.Rows.Count > 0 Then
            LI.dr = LI.dt.Rows(0)
            sMsg = LI.dr.Item("sDescription")
            sSubject = LI.dr.Item("sSubTitle")
            sMailVan = LI.dr.Item("sColor")
            sMailNaar = sEmail
            sMailTemplate = sTemplate.Replace("[subject]", sSubject)
            sMailTemplate = sMailTemplate.Replace("[body]", sMsg)
            iscMail(0, BP.iPartijIDBeheerder, "versturen", LI.dr.Item("sTitle"), LI.dr.Item("sItem4"), LI.dr.Item("sItem5"), sEmail, "", "", sSubject, sMailTemplate, "", "", sName, Now)
        End If

        LI.dt = LI.sLijstItemByTitleAndType("Accountaanvraag naar webshop", BP.sTaalID(sLanguage), "Mailing")
        If LI.dt.Rows.Count > 0 Then
            LI.dr = LI.dt.Rows(0)
            sMsg = LI.dr.Item("sDescription")
            sMsg = sMsg.Replace("[company]", sBedrijfsnaam).Replace("[contactpersoon]", sNaam).Replace("[adres]", sAdres).Replace("[vat]", sVat).Replace("[kvk]", sKvk).Replace("[tel]", sTel).Replace("[emai]", sEmail)
            sSubject = LI.dr.Item("sSubTitle")
            sMailVan = LI.dr.Item("sItem5")

            Dim sVan As String = LI.dr.Item("sItem1").replace("[email]", sEmail)
            Dim sNaar As String = LI.dr.Item("sColor").replace("[email]", sEmail)
            Dim sCC As String = LI.dr.Item("sItem4").replace("[email]", sEmail)
            Dim sBCC As String = LI.dr.Item("sItem5").replace("[email]", sEmail)

            sMailTemplate = sTemplate.Replace("[subject]", sSubject)
            sMailTemplate = sMailTemplate.Replace("[body]", sMsg)
            iscMail(0, BP.iPartijIDBeheerder, "versturen", LI.dr.Item("sTitle"), "", sVan, sNaar, sCC, sBCC, sSubject, sMailTemplate, "", "", sName, Now)
        End If
    End Sub
End Class