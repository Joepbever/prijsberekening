Imports System.Data
Imports System.Data.SqlClient

Public Class clsConnection
    Dim U As New clsUtility

    Public Function sDatatable(cmd As SqlCommand) As DataTable
        Dim con As New SqlConnection
        con.ConnectionString = U.Config("conn")
        con.Open()
        cmd.Connection = con

        Dim sdr As SqlDataReader = cmd.ExecuteReader()
        Dim dt As New DataTable()
        dt.Load(sdr)

        cmd.Dispose()
        con.Close()
        Return dt
    End Function

    Public Function sRow(cmd As SqlCommand) As DataRow
        Dim con As New SqlConnection
        con.ConnectionString = U.Config("conn")
        con.Open()
        cmd.Connection = con

        Dim sdr As SqlDataReader = cmd.ExecuteReader()
        Dim dt As New DataTable()
        dt.Load(sdr)
        Dim dr As DataRow
        If dt.Rows.Count > 0 Then
            dr = dt.Rows(0)
        Else
            dr = Nothing
        End If
        cmd.Dispose()
        con.Close()
        Return dr
    End Function

    Public Function Scalar(cmd As SqlCommand) As String
        Dim con As New SqlConnection
        con.ConnectionString = U.Config("conn")
        con.Open()
        cmd.Connection = con
        Dim sOutput As String = cmd.ExecuteScalar()
        cmd.Dispose()
        con.Close()
        Return sOutput
    End Function

    Public Function Update(cmd As SqlCommand) As Boolean
        Dim con As New SqlConnection
        con.ConnectionString = U.Config("conn")
        con.Open()
        cmd.Connection = con
        cmd.ExecuteNonQuery()
        cmd.Dispose()
        con.Close()
        Return True
    End Function

    Public Function sDatatableWebservices(cmd As SqlCommand) As DataTable
        Dim con As New SqlConnection
        con.ConnectionString = U.Config("webservice")
        con.Open()
        cmd.Connection = con

        Dim sdr As SqlDataReader = cmd.ExecuteReader()
        Dim dt As New DataTable()
        dt.Load(sdr)

        cmd.Dispose()
        con.Close()
        Return dt
    End Function
End Class