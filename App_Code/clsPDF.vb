Imports System.IO
Imports Winnovative
Imports System.Drawing

Public Class clsPDF

    Public sBriefpapierBestand As String = ""
    Public bPreFooter As Boolean
    Public bPreHeader As Boolean


    'vooraf declareren
    Public sHeaderUrl As String = ""
    Public sFooterUrl As String = ""
    Public addPageNumbers As Boolean = False
    Public drawFooterLine As Boolean = False
    Public drawHeaderLine As Boolean = False

    Private Sub DrawHeader(htmlToPdfConverter As HtmlToPdfConverter)

        ' Set the header height in points
        htmlToPdfConverter.PdfHeaderOptions.HeaderHeight = 60

        ' Set header background color
        htmlToPdfConverter.PdfHeaderOptions.HeaderBackColor = Color.White

        ' Create a HTML element to be added in header
        Dim headerHtml As New HtmlToPdfElement(sHeaderUrl)

        ' Set the HTML element to fit the container height
        headerHtml.FitHeight = True

        ' Add HTML element to header
        htmlToPdfConverter.PdfHeaderOptions.AddElement(headerHtml)

        If drawHeaderLine Then
            ' Calculate the header width based on PDF page size and margins
            Dim headerWidth As Single = htmlToPdfConverter.PdfDocumentOptions.PdfPageSize.Width - htmlToPdfConverter.PdfDocumentOptions.LeftMargin - htmlToPdfConverter.PdfDocumentOptions.RightMargin

            ' Calculate header height
            Dim headerHeight As Single = htmlToPdfConverter.PdfHeaderOptions.HeaderHeight

            ' Create a line element for the bottom of the header
            Dim headerLine As New LineElement(0, headerHeight - 1, headerWidth, headerHeight - 1)

            ' Set line color
            headerLine.ForeColor = Color.Gray

            ' Add line element to the bottom of the header
            htmlToPdfConverter.PdfHeaderOptions.AddElement(headerLine)
        End If
    End Sub

    Private Sub DrawFooter(htmlToPdfConverter As HtmlToPdfConverter)
        ' Set the footer height in points
        htmlToPdfConverter.PdfFooterOptions.FooterHeight = 60

        ' Set footer background color
        'htmlToPdfConverter.PdfFooterOptions.FooterBackColor = Color.WhiteSmoke

        ' Create a HTML element to be added in footer
        Dim footerHtml As New HtmlToPdfElement(sFooterUrl)

        ' Set the HTML element to fit the container height
        footerHtml.FitHeight = True

        ' Add HTML element to footer
        htmlToPdfConverter.PdfFooterOptions.AddElement(footerHtml)

        ' Add page numbering
        If addPageNumbers Then
            ' Create a text element with page numbering place holders &p; and & P;
            Dim footerText As New TextElement(0, 30, "Pagina &p; van &P;  ", New System.Drawing.Font(New System.Drawing.FontFamily("Arial"), 10, System.Drawing.GraphicsUnit.Pixel))

            ' Align the text at the right of the footer
            footerText.TextAlign = HorizontalTextAlign.Center

            ' Set page numbering text color
            footerText.ForeColor = Color.Black

            ' Embed the text element font in PDF
            footerText.EmbedSysFont = True

            ' Add the text element to footer
            htmlToPdfConverter.PdfFooterOptions.AddElement(footerText)
        End If

        If drawFooterLine Then
            ' Calculate the footer width based on PDF page size and margins
            Dim footerWidth As Single = htmlToPdfConverter.PdfDocumentOptions.PdfPageSize.Width - htmlToPdfConverter.PdfDocumentOptions.LeftMargin - htmlToPdfConverter.PdfDocumentOptions.RightMargin

            ' Create a line element for the top of the footer
            Dim footerLine As New LineElement(0, 0, footerWidth, 0)

            ' Set line color
            footerLine.ForeColor = Color.Gray

            ' Add line element to the bottom of the footer
            htmlToPdfConverter.PdfFooterOptions.AddElement(footerLine)
        End If
    End Sub

    Private Sub htmlToPdfConverter_PrepareRenderPdfPageEvent(eventParams As PrepareRenderPdfPageParams)
        ' Set the header visibility in first, odd and even pages
        If bPreHeader Then
            If eventParams.PageNumber = 1 Then
                eventParams.Page.ShowHeader = bPreHeader
            ElseIf (eventParams.PageNumber Mod 2) = 0 AndAlso Not bPreHeader Then
                eventParams.Page.ShowHeader = False
            ElseIf (eventParams.PageNumber Mod 2) = 1 AndAlso Not bPreHeader Then
                eventParams.Page.ShowHeader = False
            End If
        End If

        ' Set the footer visibility in first, odd and even pages
        If bPreFooter Then
            If eventParams.PageNumber = 1 Then
                eventParams.Page.ShowFooter = bPreFooter
            ElseIf (eventParams.PageNumber Mod 2) = 0 AndAlso Not bPreFooter Then
                eventParams.Page.ShowFooter = False
            ElseIf (eventParams.PageNumber Mod 2) = 1 AndAlso Not bPreFooter Then
                eventParams.Page.ShowFooter = False
            End If
        End If
    End Sub

    Public Sub sPDF(p As Page, sUrl As String, sFilename As String, ByVal sBriefpapier As String, Optional ByVal bHeader As Boolean = False, Optional ByVal bFooter As Boolean = False, Optional ByVal sTonenOfOpslaan As String = "tonen")
        bPreFooter = bFooter
        bPreHeader = bHeader
        ' Execute the Display_Session_Variables.aspx page and get the HTML string 
        ' rendered by this page
        Dim outTextWriter As TextWriter = New StringWriter()
        p.Server.Execute(sUrl, outTextWriter)

        Dim htmlStringToConvert As String = outTextWriter.ToString()

        ' Create a HTML to PDF converter object with default settings
        Dim htmlToPdfConverter As New HtmlToPdfConverter()

        ' Set license key received after purchase to use the converter in licensed mode
        ' Leave it not set to use the converter in demo mode
        htmlToPdfConverter.LicenseKey = "vTMjMiMnMiEkJTIjIDwiMiEjPCMgPCsrKysyIg=="

        ' Set an adddional delay in seconds to wait for JavaScript or AJAX calls after page load completed
        ' Set this property to 0 if you don't need to wait for such asynchcronous operations to finish
        htmlToPdfConverter.ConversionDelay = 0
        htmlToPdfConverter.PdfDocumentOptions.JpegCompressionEnabled = True
        'Install a handler where you can set header and footer visibility or create a custom header and footer in each page
        If bFooter Or bHeader Then
            ' Set a handler for BeforeRenderPdfPageEvent where to set the background image in each PDF page before main content is rendered
            AddHandler htmlToPdfConverter.PrepareRenderPdfPageEvent, AddressOf htmlToPdfConverter_PrepareRenderPdfPageEvent
        End If



        htmlToPdfConverter.PdfDocumentOptions.ShowHeader = bHeader
        If (htmlToPdfConverter.PdfDocumentOptions.ShowHeader) Then DrawHeader(htmlToPdfConverter)

        htmlToPdfConverter.PdfDocumentOptions.ShowFooter = bFooter
        'If bFooter Then
        '    If sFooterUrl = "" Then
        '        sFooterUrl = U.Config("URLCms") & sUrl.Replace("factuur", "factuur_footer")
        '    End If
        'End If
        If (htmlToPdfConverter.PdfDocumentOptions.ShowFooter) Then DrawFooter(htmlToPdfConverter)



        If Trim(sBriefpapier) <> "" Then
            ' Set a handler for BeforeRenderPdfPageEvent where to set the background image in each PDF page before main content is rendered
            sBriefpapierBestand = sBriefpapier
            AddHandler htmlToPdfConverter.BeforeRenderPdfPageEvent, AddressOf htmlToPdfConverter_BeforeRenderPdfPageEvent
        End If



        ' Use the current page URL as base URL
        Dim baseUrl As String = HttpContext.Current.Request.Url.AbsoluteUri

        ' Convert the page HTML string to a PDF document in a memory buffer
        Dim outPdfBuffer() As Byte = htmlToPdfConverter.ConvertHtml(htmlStringToConvert, baseUrl)


        Try
            If sTonenOfOpslaan.ToLower = "tonen" Then

                ' send the PDF document as a response to the browser for download
                Dim Response As System.Web.HttpResponse = System.Web.HttpContext.Current.Response
                Response.Clear()
                Response.AddHeader("Content-Type", "binary/octet-stream")
                Response.AddHeader("Content-Disposition", "attachment; filename=" & sFilename & "_preview.pdf; size=" & outPdfBuffer.Length.ToString())
                Response.Flush()
                Response.BinaryWrite(outPdfBuffer)
                Response.Flush()
                Response.End()

                '' Send the PDF as response to browser
                '' Set response content type
                'p.Response.AddHeader("Content-Type", "application/pdf")

                '' Instruct the browser to open the PDF file as an attachment or inline
                'p.Response.AddHeader("Content-Disposition", String.Format("inline; filename=" & sFilename & ".pdf; size={0}", outPdfBuffer.Length.ToString()))

                '' Write the PDF document buffer to HTTP response
                'p.Response.BinaryWrite(outPdfBuffer)

                '' End the HTTP response and stop the current page processing
                'p.Response.End()
            ElseIf sTonenOfOpslaan.ToLower = "opslaan" Then

                Dim U As New clsUtility
                Dim sFolder As String = U.Locatie() & "Facturen\"

                Dim fs As New FileStream(sFolder & sFilename & ".pdf", FileMode.Create)

                fs.Write(outPdfBuffer, 0, outPdfBuffer.Length)
                fs.Flush()
                fs.Close()

            End If

        Catch ex As Exception


        End Try



    End Sub
    ''Footer van het taxatierapport2013
    'Private Sub AddFooter(ByRef pdfConverter As PdfConverter, ByVal sUrl As Integer)

    '    Dim thisPageURL As String = HttpContext.Current.Request.Url.AbsoluteUri
    '    Dim Url As String = BLG.LocatieRptUrl("report/Woonhuis2013Footer.aspx")
    '    Dim headerAndFooterHtmlUrl As String = Url & "?GegevensID=" & iGegevensID

    '    'enable footer
    '    pdfConverter.PdfDocumentOptions.ShowFooter = True
    '    ' set the footer height in points
    '    pdfConverter.PdfFooterOptions.FooterHeight = 60
    '    'write the page number
    '    pdfConverter.PdfFooterOptions.AddElement(.TextArea = New TextArea(0, 30, "&p; van &P;  ", New System.Drawing.Font(New System.Drawing.FontFamily("Verdana"), 10, System.Drawing.GraphicsUnit.Point))
    '    pdfConverter.PdfFooterOptions.TextArea.EmbedTextFont = True
    '    pdfConverter.PdfFooterOptions.TextArea.TextAlign = HorizontalTextAlign.Center

    '    '' Paraaf taxateur
    '    'pdfConverter.PdfFooterOptions.TextArea = New TextArea(0, 30, "Paraaf taxateur:", New System.Drawing.Font(New System.Drawing.FontFamily("Verdana"), 10, System.Drawing.GraphicsUnit.Point))
    '    'pdfConverter.PdfFooterOptions.TextArea.EmbedTextFont = True
    '    'pdfConverter.PdfFooterOptions.TextArea.TextAlign = HorizontalTextAlign.Left

    '    ' set the footer HTML area
    '    ' pdfConverter.PdfFooterOptions.HtmlToPdfArea = New HtmlToPdfArea(0, 0, -1, pdfConverter.PdfFooterOptions.FooterHeight, headerAndFooterHtmlUrl, 1024, -1)
    '    pdfConverter.PdfFooterOptions.HtmlToPdfArea = New HtmlToPdfArea(30, 0, -1, pdfConverter.PdfFooterOptions.FooterHeight, headerAndFooterHtmlUrl, 980, -1)
    '    pdfConverter.PdfFooterOptions.HtmlToPdfArea.FitHeight = True
    '    pdfConverter.PdfFooterOptions.HtmlToPdfArea.FitWidth = True
    '    pdfConverter.PdfFooterOptions.HtmlToPdfArea.EmbedFonts = False
    'End Sub

    'Private Sub AddFooter(pdfDocument As Document, addPageNumbers As Boolean, drawFooterLine As Boolean, sPad As String)
    '    Dim footerHtmlUrl As String = sPad

    '    ' Create the document footer template
    '    pdfDocument.AddFooterTemplate(60)

    '    ' Set footer background color
    '    Dim backColorRectangle As New RectangleElement(0, 0, pdfDocument.Footer.Width, pdfDocument.Footer.Height)
    '    backColorRectangle.BackColor = Color.WhiteSmoke
    '    pdfDocument.Footer.AddElement(backColorRectangle)

    '    ' Create a HTML element to be added in footer
    '    Dim footerHtml As New HtmlToPdfElement(footerHtmlUrl)

    '    ' Set the HTML element to fit the container height
    '    footerHtml.FitHeight = True

    '    ' Add HTML element to footer
    '    pdfDocument.Footer.AddElement(footerHtml)

    '    ' Add page numbering
    '    If addPageNumbers Then
    '        ' Create a text element with page numbering place holders &p; and & P;
    '        Dim footerText As New TextElement(0, 30, "Page &p; of &P;  ", New System.Drawing.Font(New System.Drawing.FontFamily("Times New Roman"), 10, System.Drawing.GraphicsUnit.Point))

    '        ' Align the text at the right of the footer
    '        footerText.TextAlign = HorizontalTextAlign.Right

    '        ' Set page numbering text color
    '        footerText.ForeColor = Color.Navy

    '        ' Embed the text element font in PDF
    '        footerText.EmbedSysFont = True

    '        ' Add the text element to footer
    '        pdfDocument.Footer.AddElement(footerText)
    '    End If

    '    If drawFooterLine Then
    '        Dim footerWidth As Single = pdfDocument.Footer.Width

    '        ' Create a line element for the top of the footer
    '        Dim footerLine As New LineElement(0, 0, footerWidth, 0)

    '        ' Set line color
    '        footerLine.ForeColor = Color.Gray

    '        ' Add line element to the bottom of the footer
    '        pdfDocument.Footer.AddElement(footerLine)
    '    End If
    'End Sub

    '=======================================================
    'Service provided by Telerik (www.telerik.com)
    'Conversion powered by NRefactory.
    'Twitter: @telerik
    'Facebook: facebook.com/telerik
    '=======================================================

    Private Sub htmlToPdfConverter_BeforeRenderPdfPageEvent(ByVal eventParams As BeforeRenderPdfPageParams)
        'If Not addBackgroundImageCheckBox.Checked Then
        '    Return
        'End If

        ' Get the PDF page being rendered
        Dim pdfPage As PdfPage = eventParams.Page

        ' Get the PDF page drawable area width and height
        Dim pdfPageWidth As Single = pdfPage.ClientRectangle.Width
        Dim pdfPageHeight As Single = pdfPage.ClientRectangle.Height

        ' The image to be added as background
        Dim backgroundImagePath As String = HttpContext.Current.Server.MapPath(sBriefpapierBestand)

        ' The image element to add in background
        Dim backgroundImageElement As New ImageElement(0, 0, pdfPageWidth, pdfPageHeight, backgroundImagePath)
        backgroundImageElement.KeepAspectRatio = True
        backgroundImageElement.EnlargeEnabled = True

        ' Add the background image element to PDF page before the main content is rendered
        pdfPage.AddElement(backgroundImageElement)
    End Sub
End Class
