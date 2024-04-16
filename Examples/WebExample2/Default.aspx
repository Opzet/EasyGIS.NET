<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="EGIS.Web.Controls" Namespace="EGIS.Web.Controls" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Easy GIS .NET Web Example 1</title>
</head>
<body>
    <form id="form1" runat="server">
    
    <div>
    
    <p>Easy GIS .NET Web Example2</p>
    <p>This example shows how to set CustomRenderSettings on layers in an SFMap Web Control
    <br />    
    </p>   
        Select Custom Render Settings<br />
        <asp:DropDownList ID="DropDownList1" runat="server" Width="175px">
            <asp:ListItem>Please Select..</asp:ListItem>
            <asp:ListItem>Population Density</asp:ListItem>
            <asp:ListItem>Average House Sale</asp:ListItem>
            <asp:ListItem>Divorced</asp:ListItem>
            <asp:ListItem>Median Rent</asp:ListItem>            
        </asp:DropDownList>
        <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Generate Map"
            Width="103px" /><br />
        <br />       
           <cc1:SFMap ID="SFMap1" runat="server" Height="364px" Width="561px" ProjectName="~/us_demo.egp" style="border-right: gray thin dashed; border-top: gray thin dashed; border-left: gray thin dashed; border-bottom: gray thin dashed" MaxZoomLevel="1000" MinZoomLevel="2"  CacheOnClient="false"/><br />
            <%--Note that currently the MapPanControl must be added after the SFMap control is added to the page--%>            
            <cc1:MapPanControl ID="MapPanControl1" runat="server" style="z-index: 100; text-align: center; position:absolute;left:20px;top:200px" />                          
    </div>
    </form>
</body>
</html>
