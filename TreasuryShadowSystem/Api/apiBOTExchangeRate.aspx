﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="apiBOTExchangeRate.aspx.cs" Inherits="TreasuryShadowSystem.API.apiBOTExchangeRate" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function() {
            var url = "https://apigw1.bot.or.th/bot/public/Stat-ExchangeRate/v2/DAILY_AVG_EXG_RATE/?start_period=" + $("#<%= FromDateID.ClientID %>").val() + "&end_period=" + $("#<%= ToDateID.ClientID %>").val();
            var xhttp = new XMLHttpRequest();
            xhttp.open("GET", url, false); //true for async
            xhttp.setRequestHeader('x-ibm-client-id', 'ade23632-534c-459e-a052-113b7d891e0d');
            xhttp.send();
            if (xhttp.readyState == 4 && xhttp.status == 200) {
                var js = JSON.parse(xhttp.responseText);
                $.ajax({
                    type: "POST",
                    url: "<%=Config.Website %>/API/apiBOTExchangeRate.aspx/ExchangeRate",
                    data: "{result:" + JSON.stringify(js["result"]["data"]["data_detail"]) + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function(response) {

                    },
                    failure: function(response) {

                    }
                });
            } else {
                alert(xhttp.status);
            }
            return false;
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <asp:HiddenField ID="FromDateID" runat="server" />
    <asp:HiddenField ID="ToDateID" runat="server" />
</asp:Content>
