<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TestViewStateMac.aspx.cs" Inherits="WebCommonTest.TestViewStateMac" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <asp:TextBox ID="TextBox1" TextMode="MultiLine" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
    <div>
        <asp:GridView ID="GridView1" DataMember="key,value" runat="server"></asp:GridView>
    </div>
    </form>
</body>
</html>
