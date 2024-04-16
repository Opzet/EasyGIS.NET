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
    
    <p>Easy GIS .NET Web Example1</p>
    <p>This example shows how to load an Easy GIS .NET shapefile project in the SFMap Web Control
    <br />    
    </p>            
    <cc1:SFMap ID="SFMap1" runat="server" Height="364px" Width="561px" ProjectName="~/world.egp" style="border-right: gray thin dashed; border-top: gray thin dashed; border-left: gray thin dashed; border-bottom: gray thin dashed" MaxZoomLevel="250" MinZoomLevel="0.5"  />    
    <cc1:MapPanControl ID="MapPanControl1" runat="server"  MapReferenceId="SFMap1" style="z-index: 100; left: 29px; position: absolute; top: 98px; text-align: center" />        
    </div>
    <div>
    
     <p>    
            NOTE: In order for the control to render maps an entry in the httpHandlers section of the web.config file must be made.
            If you are using the SFMap Control in your own projects and the control is added to a page using the design view in Visual Studio an entry is added to the web.config file automatically; however
            if the Control is manually added to a page it will be neccessary to add the following section to the web.config file.
            </p>
    <div style=" background-color:#efefef; border:solid 2px #cecece; font-weight:bold; color:#3030df">
    <pre>    &lt;httpHandlers&gt;
      &lt;add path="egismap.axd" verb="*" type="EGIS.Web.Controls.SFMapImageProvider, EGIS.Web.Controls, Version=3.4.0.0,
       Culture=neutral, PublicKeyToken=05b98c869b5ffe6a" validate="true" /&gt;
    &lt;/httpHandlers&gt;
    </pre>
    </div>         
    </div>
    </form>
</body>
</html>
