Public Class clsValidatie

    Public msg As String

    Public Function sVerplichtEmail(ByRef oCntrlToTest As String) As Boolean
        ' controleert op email format 
        Dim sReg As String = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        Dim re As New Regex(sReg)
        If re.IsMatch(oCntrlToTest) = False Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function sVerplichtText(ByRef oCntrlToTest As String) As Boolean

        If oCntrlToTest = "" Then
            Return False
        Else
            Return True
        End If
    End Function
    Public Function sVerplichtTextAndLength(ByRef oCntrlToTest As String, ByRef minLength As Integer, ByRef maxLength As Integer) As Boolean
        If oCntrlToTest = "" Then
            Return False
        Else
            Dim temp = oCntrlToTest.Length()
            If (oCntrlToTest.Trim().Length >= minLength And oCntrlToTest.Trim.Length <= maxLength) Then
                Return True
            End If
        End If
        Return False
    End Function
    Public Function isDobEqualAnAge(ByRef sDate As DateTime, ByRef Age As Integer) As Boolean

        Dim date1 As Date = sDate
        Dim date2 As Date = DateTime.Now()

        Dim yearsOld As Integer = date2.Year - date1.Year
        If date1.AddYears(yearsOld) > date2 Then yearsOld -= 1

        If yearsOld < Age Then
            Return False
        ElseIf yearsOld >= Age Then
            Return True
        End If
        Return False
    End Function
    Public Function isTextBoxValidByRegex(ByRef oCntrlToTest As TextBox, ByRef sReg As String) As Boolean
        Dim re As New Regex(sReg)

        If re.IsMatch(oCntrlToTest.Text.Trim) = False Then
            Return False
        Else
            Return True
        End If

    End Function
    Public Function VerplichtEmail(ByRef oCntrlToTest As TextBox) As Boolean
        ' controleert op email format 
        Dim sReg As String = "\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
        Dim re As New Regex(sReg)
        If re.IsMatch(oCntrlToTest.Text) = False Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function VerplichtTextBox(ByRef oCntrlToTest As TextBox) As Boolean
        If oCntrlToTest.Text.Trim = "" Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function VerplichtDropDownListValue(ByRef oCntrlToTest As DropDownList) As Boolean
        If oCntrlToTest.SelectedValue.Trim = "" Then
            Return False
        Else
            Return True
        End If
    End Function


    Public Function validateAddress2(ddlLand As DropDownList, txtZipCode As TextBox, txtHouseNr As TextBox, txtAdd As TextBox, txtAddress As TextBox, txtResidence As TextBox) As Boolean
        'LAND
        If (VerplichtDropDownListValue(ddlLand)) = False Then
            msg = Resources.Resource.ErrorLand
            Return False
        End If
        'POSTCODE
        If (isTextBoxValidByRegex(txtZipCode, "^$|^[a-zA-Z0-9 ]+$")) = False Then
            msg = Resources.Resource.ErrorZipCode
            Return False
        End If
        'Huisnummer
        If (isTextBoxValidByRegex(txtHouseNr, "^$|^[0-9]+$")) = False Then
            msg = Resources.Resource.ErrorHuisNr
            Return False
        End If
        'Toevoeging
        If (isTextBoxValidByRegex(txtAdd, "^$|^[0-9]+$")) = False Then
            msg = Resources.Resource.ErrorTov
            Return False
        End If
        'Straat
        If (isTextBoxValidByRegex(txtAddress, "^$|^[a-zA-Z0-9 .'-]+$")) = False Then
            msg = Resources.Resource.ErrorStraat
            Return False
        End If
        'Stad
        If (isTextBoxValidByRegex(txtResidence, "^$|^[a-zA-Z ]+$")) = False Then
            msg = Resources.Resource.ErrorStad
            Return False
        End If
        Return True
    End Function

    Public Function validateAddress(ddlLand As DropDownList, txtZipCode As TextBox, txtHouseNr As TextBox, txtAdd As TextBox, txtAddress As TextBox, txtResidence As TextBox) As Boolean
        'LAND
        If (VerplichtDropDownListValue(ddlLand)) = False Then
            msg = Resources.Resource.ErrorLand
            Return False
        End If
        'POSTCODE
        If (VerplichtTextBox(txtZipCode) And isTextBoxValidByRegex(txtZipCode, "^[a-zA-Z0-9 ]+$")) = False Then
            msg = Resources.Resource.ErrorZipCode

            Return False
        End If
        'Huisnummer
        If (sVerplichtTextAndLength(txtHouseNr.Text, 1, 3) And isTextBoxValidByRegex(txtHouseNr, "^[0-9]+$")) = False Then
            msg = Resources.Resource.ErrorHuisNr

            Return False
        End If
        'Toevoeging
        If (sVerplichtTextAndLength(txtAdd.Text, 1, 3) And isTextBoxValidByRegex(txtAdd, "^[0-9]+$")) = False Then
            msg = Resources.Resource.ErrorTov

            Return False
        End If
        'Straat
        If (sVerplichtTextAndLength(txtAddress.Text, 1, 100) And isTextBoxValidByRegex(txtAddress, "^$|^[a-zA-Z0-9 .'-]+$")) = False Then
            msg = Resources.Resource.ErrorStraat
            Return False
        End If
        'Stad
        If (sVerplichtTextAndLength(txtResidence.Text, 1, 100) And isTextBoxValidByRegex(txtResidence, "^[a-zA-Z ]+$")) = False Then
            msg = Resources.Resource.ErrorStad
            Return False
        End If
        Return True
    End Function

    Public Function ValidateDetails(txtFirstName As TextBox, txtSurname As TextBox, txtEmail As TextBox, txtPhone As TextBox) As Boolean
        'Naam
        If (sVerplichtTextAndLength(txtFirstName.Text, 1, 100) And isTextBoxValidByRegex(txtFirstName, "^[a-zA-Z ]+$")) = False Then
            msg = Resources.Resource.ErrorNaam
            Return False
        End If
        'Achternaam
        If (sVerplichtTextAndLength(txtSurname.Text, 1, 100) And isTextBoxValidByRegex(txtSurname, "^[a-zA-Z ]+$")) = False Then
            msg = Resources.Resource.ErrorAchternaamNaam
            Return False
        End If
        'Email
        If (VerplichtTextBox(txtEmail)) = False Then
            msg = Resources.Resource.ErrorEmail
            Return False
        End If
        'Telephone
        If (sVerplichtTextAndLength(txtPhone.Text, 3, 16) And isTextBoxValidByRegex(txtPhone, "^[+0-9]+$")) = False Then
            msg = Resources.Resource.ErrorPhone
            Return False
        End If
        Return True
    End Function

    Public Function ValidateRegister(txtFirstName As TextBox, txtSurname As TextBox, txtEmail As TextBox) As Boolean
        If (sVerplichtTextAndLength(txtFirstName.Text, 1, 100) And isTextBoxValidByRegex(txtFirstName, "^[a-zA-Z ]+$")) = False Then
            msg = Resources.Resource.ErrorNaam
            Return False
        End If
        'Achternaam
        If (sVerplichtTextAndLength(txtSurname.Text, 1, 100) And isTextBoxValidByRegex(txtSurname, "^[a-zA-Z ]+$")) = False Then
            msg = Resources.Resource.ErrorAchternaamNaam
            Return False
        End If
        'Email
        If (VerplichtTextBox(txtEmail)) = False Then
            msg = Resources.Resource.ErrorEmail
            Return False
        End If
        Return True
    End Function

    Public Function ValidatePassword(txtPassword As TextBox, txtPasswordD As TextBox) As Boolean

        Dim newPassword = txtPassword.Text
        Dim newPasswordDouble = txtPasswordD.Text

        If newPassword <> newPasswordDouble Then
            msg = Resources.Resource.ErrorWachtwordVergelijking
            Return False

        ElseIf newPassword.Length < 8 Then
            msg = Resources.Resource.ErrorWachtword8
            Return False

        ElseIf newPassword.Length > 30 Then
            msg = Resources.Resource.ErrorWachtword30
            Return False
        End If


        Dim hasUppercase As Boolean = False
        Dim hasNumber As Boolean = False
        Dim hasPunctation As Boolean = False

        For index = 0 To newPassword.Length - 1
            If newPassword(index).ToString = Char.ToUpper(newPassword(index)) Then
                hasUppercase = True
            ElseIf Char.IsNumber(newPassword(index)) Then
                hasNumber = True
            End If
        Next

        Dim pattern As String = "[.,\/#!$%\^&\*;:{}=\-_`~()]"
        Dim r As Regex = New Regex(pattern, RegexOptions.IgnoreCase)
        Dim m As Match = r.Match(newPassword)

        If m.Success = True Then
            hasPunctation = True
        End If

        Dim counter As Integer = 0

        If hasPunctation = True Then
            counter += 1
        End If
        If hasUppercase = True Then
            counter += 1
        End If
        If hasNumber = True Then
            counter += 1
        End If

        If counter >= 2 Then
            Return True
        Else
            If hasPunctation = False Then
                msg = Resources.Resource.ErrorWachtwordAlgemeen
                Return False
            ElseIf hasUppercase = False Then
                msg = Resources.Resource.ErrorWachtwordAlgemeen
                Return False
            ElseIf hasNumber = False Then
                msg = Resources.Resource.ErrorWachtwordAlgemeen
                Return False
            End If
        End If

        Return True
    End Function



End Class