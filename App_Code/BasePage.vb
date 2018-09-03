Imports System.Xml
Imports System.Threading
Imports System.Globalization

Public Class BasePage
    Inherits Page

    Dim U As New clsUtility
    Public iPartijIDBeheerder As Long = U.Config("iPartijIDBeheerder")
    Public sLocatie As String = U.Config("Locatie") '"E:\internet\MAP\"
    Public sDefaultLanguage As String = "NL"
    Public iPartijID As Long = 0
    Public iPersID As Long = 0
    Public bMultilanguage As Boolean = False
    Protected Overrides Sub InitializeCulture()
        Dim sLanguage As String
        Dim TAL As New clsTalen
        TAL.dt = TAL.sTalenByPartijID(iPartijIDBeheerder)
        If TAL.dt.Rows.Count > 2 Then
            'meertalig
            bMultilanguage = True
            Try
                sLanguage = RouteData.Values("language")
            Catch ex As Exception
                sLanguage = sDefaultLanguage
            End Try

            TAL.dt = TAL.sTaalByLanguage(sLanguage.ToUpper(), iPartijIDBeheerder)
            TAL.dr = TAL.dt.Rows(0)

            sLanguage = TAL.dr.Item("sCulture")
        Else
            TAL.dr = TAL.dt.Rows(0)
            sLanguage = TAL.dr.Item("sCulture")
        End If
        Page.UICulture = sLanguage
        Page.Culture = sLanguage
        Thread.CurrentThread.CurrentUICulture = New CultureInfo(sLanguage)
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(sLanguage)
        MyBase.InitializeCulture()
    End Sub

    Public Function IsOnline() As Boolean
        Return User.Identity.IsAuthenticated
    End Function

    Public Function InitPage() As Long
        If IsOnline() = False Then Response.Redirect("/")
        Return sPartijID()
    End Function


    Public Function sPartijID() As Long
        Dim ACC As New clsAccounts
        Dim muUser As MembershipUser = GetCurrentUser()
        Dim sUserID As String = ""
        If (Not muUser Is Nothing) Then
            sUserID = muUser.ProviderUserKey.ToString()
            ACC.dt = ACC.sAccountByUserID(sUserID, iPartijIDBeheerder)
            If ACC.dt.Rows.Count > 0 Then
                ACC.dr = ACC.dt.Rows(0)
                iPartijID = ACC.dr.Item("iPartijID")
                iPersID = ACC.dr.Item("iPersID")
                Return iPartijID
            Else
                Return 0
            End If
        Else
            Return 0
        End If
    End Function
    Private Function GetCurrentUser() As MembershipUser
        Dim httpContext As HttpContext = HttpContext.Current
        If httpContext IsNot Nothing AndAlso httpContext.User IsNot Nothing AndAlso httpContext.User.Identity.IsAuthenticated Then
            Return Membership.GetUser()
        End If
        Return Nothing
    End Function

    Public Function sLogoutPage() As String
        FormsAuthentication.SignOut()
        Return "/uitloggen"
    End Function

    Public Function sTaalID(sLanguage As String) As Long
        Dim TAL As New clsTalen
        TAL.dt = TAL.sTalenByPartijID(iPartijIDBeheerder)
        If TAL.dt.Rows.Count > 1 Then
            bMultilanguage = True
        Else
            bMultilanguage = False
        End If
        For Each TAL.dr In TAL.dt.Rows
            If TAL.dr.Item("sCode").ToUpper() = sLanguage.ToUpper() Then
                Return TAL.dr.Item("iTaalID")
            End If
        Next
        Return 0
    End Function

    Public Sub Message(MP As MasterPage, sMsg As String, Optional sSoort As String = "warning")

        MP.FindControl("divGlobalMessage").Visible = True
        Dim ltl As Literal = MP.FindControl("ltlGlobalMessage")
        ltl.Text = sMsg
        Dim divAlert As HtmlGenericControl = MP.FindControl("divAlert")
        divAlert.Attributes.Add("class", "alert alert-" & sSoort)
    End Sub


    Public Sub HideMessage()
        Me.Master.FindControl("divGlobalMessage").Visible = False
    End Sub



    Public Function FriendlyUrl(sUrl As String) As String
        sUrl = sUrl.ToLower()
        ' remove entities
        sUrl = Regex.Replace(sUrl, "w+;", "")
        sUrl = Regex.Replace(sUrl, "&", "en")
        ' remove anything that is not letters, numbers, dash, or space
        sUrl = Regex.Replace(sUrl, "[^a-zA-ZàáâäãåąčćęèéêëėįìíîïłńòóôöõøùúûüųūÿýżźñçčšžÀÁÂÄÃÅĄĆČĖĘÈÉÊËÌÍÎÏĮŁŃÒÓÔÖÕØÙÚÛÜŲŪŸÝŻŹÑßÇŒÆČŠŽ∂ð0-9\-\s]", "")
        ' replace spaces
        sUrl = sUrl.Replace(" "c, "-"c)
        ' collapse dashes
        sUrl = Regex.Replace(sUrl, "-{2,}", "-")
        ' trim excessive dashes at the beginning
        'sUrl = sUrl.TrimStart(New () {"-"C})
        ' if it's too long, clip it
        'If sUrl.Length > 80 Then
        '    sUrl = sUrl.Substring(0, 79)
        'End If
        ' remove trailing dashes
        'sUrl = sUrl.TrimEnd(New () {"-"C})
        Return sUrl
    End Function

    Public Function sURL() As String
        Dim U As New clsUtility
        Return U.URL()
    End Function

    Public Sub StaticTextLanguage(pPage As Page, sPage As String, sCode As String)
        'Ophalen van XMLDocument
        Dim xDoc As New XmlDocument
        xDoc.Load(Server.MapPath("~/Xml/Strings.xml"))
        Dim xNodes As XmlNodeList = xDoc.SelectNodes("/controls/" + sPage + "/" + sCode.ToUpper() + "/control")

        'Teksten verwerken in controls
        Dim name, text, type As String
        For Each xNode As XmlNode In xNodes
            Try
                name = xNode.Attributes("name").Value
                text = xNode.Attributes("text").Value
                type = xNode.Attributes("type").Value

                Select Case type.ToLower()
                    Case "cb"
                        Dim cb As CheckBox = pPage.Form.FindControl(name)
                        cb.Text = text
                    Case "ltl"
                        Dim ltl As Literal
                        ltl = pPage.Form.FindControl(name)
                        ltl.Text = text
                    Case "lbl"
                        Dim lbl As Label = pPage.Form.FindControl(name)
                        lbl.Text = text
                    Case "btn"
                        Dim btn As Button = pPage.Form.FindControl(name)
                        btn.Text = text
                    Case "htmlbtn"
                        Dim btn As HtmlButton = pPage.Form.FindControl(name)
                        btn.InnerText = text
                End Select
            Catch ex As Exception
                Dim ss As String = ex.Message
            End Try
        Next
    End Sub


    Public Sub sBreadcrumbs(ltl As Literal)

        Dim sLanguage As String = (If((Page.RouteData.Values("language") IsNot Nothing), Page.RouteData.Values("language").ToString(), Nothing))
        Dim sParent As String = (If((Page.RouteData.Values("parentpage") IsNot Nothing), Page.RouteData.Values("parentpage").ToString(), Nothing))
        Dim sQs As String = (If((Page.RouteData.Values("page") IsNot Nothing), Page.RouteData.Values("page").ToString(), Nothing))
        Dim sAfbeelding As String = (If((Page.RouteData.Values("safbeelding") IsNot Nothing), Page.RouteData.Values("safbeelding").ToString(), Nothing))

        Dim sNiveau1 As String = (If((Page.RouteData.Values("niveau1") IsNot Nothing), Page.RouteData.Values("niveau1").ToString(), Nothing))
        Dim sNiveau2 As String = (If((Page.RouteData.Values("niveau2") IsNot Nothing), Page.RouteData.Values("niveau2").ToString(), Nothing))
        Dim sNiveau3 As String = (If((Page.RouteData.Values("niveau3") IsNot Nothing), Page.RouteData.Values("niveau3").ToString(), Nothing))
        Dim sNiveau4 As String = (If((Page.RouteData.Values("niveau4") IsNot Nothing), Page.RouteData.Values("niveau4").ToString(), Nothing))

        Dim sArtikel As String = (If((Page.RouteData.Values("artikel") IsNot Nothing), Page.RouteData.Values("artikel").ToString(), Nothing))


        'Dim sUrlCategorie As String = (If((Page.RouteData.Values("subcategorie") IsNot Nothing), Page.RouteData.Values("subcategorie").ToString(), ""))
        'Dim sUrlArtikelgroep As String = (If((Page.RouteData.Values("artikelgroep") IsNot Nothing), Page.RouteData.Values("artikelgroep").ToString(), ""))
        'Dim sArtikel As String = (If((Page.RouteData.Values("artikel") IsNot Nothing), Page.RouteData.Values("artikel").ToString(), ""))
        'Dim sArtikelID As String = (If((Page.RouteData.Values("artikelid") IsNot Nothing), Page.RouteData.Values("artikelid").ToString(), ""))
        'Dim sCatid As String = (If((Page.RouteData.Values("catid") IsNot Nothing), Page.RouteData.Values("catid").ToString(), ""))
        'iArtikelID = (If((Page.RouteData.Values("artikelgroep") IsNot Nothing), Page.RouteData.Values("artikelgroep").ToString(), ""))

        Dim s As New StringBuilder
        'Home altijd tonen als je op een andere pagina bent.
        Dim sLink As String = ""
        Dim sUrlLink As String = sURL()

        If sLanguage IsNot Nothing Then
            If bMultilanguage Then
                sLink = sLink & sLanguage
                sUrlLink = sUrlLink & sLanguage
            End If
        End If

        If sLanguage = sDefaultLanguage.ToLower Then
            s.Append("<li itemscope='' itemtype='/'><a itemprop='url' href='/'>" & "Home" & "</a></li>")
        Else
            s.Append("<li itemscope='' itemtype='" & sUrlLink & "'><a itemprop='url' href='" & sLink & "/home'>" & "Home" & "</a></li>")
        End If

        If sParent IsNot Nothing And sQs <> "" Then
            sLink = sLink & "/" & sParent
            sUrlLink = sUrlLink & "/" & sParent
            s.Append("<li itemscope='' itemtype='" & sUrlLink & "'><a itemprop='url' href='" & sLink & "'>" & sParent.Replace("-", " ") & "</a></li>")
        End If

        If sQs IsNot Nothing And sQs <> "" Then
            If sQs <> "home" Then
                sLink = sLink & "/" & sQs
                sUrlLink = sUrlLink & "/" & sQs
                s.Append("<li itemscope='' itemtype='" & sUrlLink & "'><a itemprop='url' href='" & sLink & "'>" & Char.ToUpper(Page.RouteData.DataTokens("sText")(0)) + Page.RouteData.DataTokens("sText").Substring(1).Replace("-", " ") & "</a></li>")
            End If
        End If

        If sAfbeelding IsNot Nothing Then
            sLink = sLink & "/" & sAfbeelding
            sUrlLink = sUrlLink & "/" & sAfbeelding
            s.Append("<li itemscope='' itemtype='" & sUrlLink & "'><a itemprop='url' href='" & sLink & "'>" & Page.RouteData.DataTokens("sTitle") & "</a></li>")
        End If

        If sNiveau1 IsNot Nothing Then
            sLink = sLink & "/" & sNiveau1
            sUrlLink = sUrlLink & "/" & sNiveau1
            s.Append("<li itemscope='' itemtype='" & sUrlLink & "'><a itemprop='url' href='" & sLink & "'>" & Char.ToUpper(sNiveau1(0)) + sNiveau1.Substring(1).Replace("-", " ") & "</a></li>")
        End If

        If sNiveau2 IsNot Nothing And sNiveau2 <> "" Then
            sLink = sLink & "/" & sNiveau2
            sUrlLink = sUrlLink & "/" & sNiveau2
            s.Append("<li itemscope='' itemtype='" & sUrlLink & "'><a itemprop='url' href='" & sLink & "'>" & Char.ToUpper(sNiveau2(0)) + sNiveau2.Substring(1).Replace("-", " ") & "</a></li>")
        End If

        If sNiveau3 IsNot Nothing And sNiveau3 <> "" Then
            sLink = sLink & "/" & sNiveau3
            sUrlLink = sUrlLink & "/" & sNiveau3
            s.Append("<li itemscope='' itemtype='" & sUrlLink & "'><a itemprop='url' href='" & sLink & "'>" & Char.ToUpper(sNiveau3(0)) + sNiveau3.Substring(1).Replace("-", " ") & "</a></li>")
        End If

        If sNiveau4 IsNot Nothing And sNiveau4 <> "" Then
            sLink = sLink & "/" & sNiveau4
            sUrlLink = sUrlLink & "/" & sNiveau4
            s.Append("<li itemscope='' itemtype='" & sUrlLink & "'><a itemprop='url' href='" & sLink & "'>" & Char.ToUpper(sNiveau4(0)) + sNiveau4.Substring(1).Replace("-", " ") & "</a></li>")
        End If

        If sArtikel IsNot Nothing And sArtikel <> "" Then
            sLink = sLink & "/" & sArtikel
            sUrlLink = sUrlLink & "/" & sArtikel
            s.Append("<li itemscope='' itemtype='" & sUrlLink & "'><a itemprop='url' href='" & sLink & "'>" & Char.ToUpper(sArtikel(0)) + sArtikel.Substring(1).Replace("-", " ") & "</a></li>")
        End If

        ltl.Text = s.ToString()
    End Sub

    Public Function onlineCheck() As Long
        Dim sLanguage As String
        If RouteData.Values("language") IsNot Nothing Then
            sLanguage = RouteData.Values("language").ToString().ToUpper
        Else
            sLanguage = sDefaultLanguage.ToUpper
        End If

        Dim sDestination = HttpContext.Current.Request.Url.AbsoluteUri

        If IsOnline() = False Then
            Dim P As New clsPage
            Response.Redirect(P.sPageUrlByGuid(sLanguage, "1b46b0e6-da14-42e6-b1c3-97d3fbfd9b6f") & "?w=2&d=" & sDestination)
        End If
        Return sPartijID()
    End Function


End Class