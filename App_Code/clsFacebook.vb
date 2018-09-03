Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.VisualBasic

Public Class clsFacebook
    Public dt As New DataTable
    Public dr As DataRow

    Public Function sFacebookByPartijIDBeheerder(iPartijIDBeheerder As Long, iCount As Long) As DataTable
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "SELECT TOP(@iCount) sFacebookPostImageUrl, sFacebookPostMessage, sFacebookPostStory, sFacebookPostDate FROM FacebookPosts WHERE iPartijIDBeheerder = @iPartijIDBeheerder ORDER BY sFacebookPostDate DESC"
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@iCount", iCount)
        Return CON.sDatatable(cmd)
    End Function

    Public Function sFacebookInstagramByPartijIDBeheerder(iPartijIDBeheerder As Long, iCount As Long) As DataTable
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "SELECT TOP(@iCount) iInstagramID AS iFacebookID, sInstagramImageID AS PostID, sInstagramImageUrl AS ImageUrl, sInstagramPostDate AS PostDate, sInstagramPostMessage AS PostMessage, iInstagramPostLikes AS PostLikes, sType FROM            Instagram WHERE (iPartijIDBeheerder = @iPartijIDBeheerder) UNION SELECT TOP(@iCount) iFacebookID, sFacebookPostID AS PostID, sFacebookPostImageUrl AS ImageUrl, sFacebookPostDate AS PostDate, sFacebookPostMessage AS PostMessage, iFacebookPostLikes AS PostLikes, sType FROM FacebookPosts WHERE (iPartijIDBeheerder = @iPartijIDBeheerder) ORDER BY PostDate DESC"
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        cmd.Parameters.AddWithValue("@iCount", iCount)
        Return CON.sDatatable(cmd)
    End Function

    Public Sub iFacebook(iPartijIDBeheerder As Long, sFacebookPostID As String, sFacebookPostImageUrl As String, sFacebookPostMessage As String, sFacebookPostStory As String, sFacebookPostDate As String)
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "INSERT INTO FacebookPosts (sFacebookPostID, sFacebookPostImageUrl, sFacebookPostDate, sFacebookPostMessage, sFacebookPostStory, iPartijIDBeheerder) VALUES (@sFacebookPostID, @sFacebookPostImageUrl, @sFacebookPostDate, @sFacebookPostMessage, @sFacebookPostStory, @iPartijIDBeheerder)"
        cmd.Parameters.AddWithValue("@sFacebookPostID", sFacebookPostID)
        cmd.Parameters.AddWithValue("@sFacebookPostImageUrl", sFacebookPostImageUrl)
        cmd.Parameters.AddWithValue("@sFacebookPostDate", sFacebookPostDate)
        cmd.Parameters.AddWithValue("@sFacebookPostMessage", sFacebookPostMessage)
        cmd.Parameters.AddWithValue("@sFacebookPostStory", sFacebookPostStory)
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        CON.Scalar(cmd)
    End Sub

    Public Sub dFacebookByPartijIDBeheerder(iPartijIDBeheerder As Long)
        Dim CON As New clsConnection
        Dim cmd As New SqlCommand
        cmd.CommandText = "DELETE FROM FacebookPosts WHERE iPartijIDBeheerder = @iPartijIDBeheerder"
        cmd.Parameters.AddWithValue("@iPartijIDBeheerder", iPartijIDBeheerder)
        CON.Scalar(cmd)
    End Sub

End Class
