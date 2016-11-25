<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <asp:Image ID="Image1" runat="server" BorderStyle="None" Height="100%" ImageAlign="Middle" />
        <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick">
        </asp:Timer>
    </div>

</asp:Content>
