<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>trafik trap</title>

	<meta name="mobile-web-app-capable" content="yes">
	<meta name="viewport" content="width=device-width, initial-scale=1">
	<meta name="theme-color" content="#616161">
	
	<link href='https://fonts.googleapis.com/css?family=Roboto' rel='stylesheet' type='text/css'>
	<link rel="stylesheet" type="text/css" href="styles.css"/>

	<link rel="import" href="/bower_components/google-map/google-map.html"/>
</head>
<body>
    <form id="form1" runat="server">
        <img id="sound" src="/svg/volume-up.svg">

        <div id="status" class="hide">
            <img />
        </div>
        <div id="queue">
        </div>

        <google-map disabledefaultui zoom="15">
		    <google-map-marker id="self" icon="/svg/room.svg"></google-map-marker>
	    </google-map>

        <div id="warning" class="hide">!</div>
        <div id="add">+</div>
        <div id="done" class="hide">
            <img width="24" src="/svg/done.svg" /></div>

        <script src="script.js" async></script>
    </form>
</body>
</html>
