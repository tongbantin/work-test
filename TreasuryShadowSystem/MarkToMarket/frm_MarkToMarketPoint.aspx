<%@ Page Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_MarkToMarketPoint.aspx.cs" Inherits="TreasuryShadowSystem.MarkToMarket.frm_MarkToMarketPoint" Title="Untitled Page" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link href="../Scripts/jtable/themes/metro/blue/jtable.css" rel="stylesheet"  type="text/css" />
    <script type="text/javascript" src="../Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="PointTableContainer" style="width: 500px;"></div>
    <script type="text/javascript">

        $(document).ready(function() {

            //Prepare jtable plugin
            $('#PointTableContainer').jtable({
                title: 'Mark To Market Point',
                paging: true,
                pageSize: 10,
                sorting: true,
                defaultSorting: 'FUND ASC',
                actions: {
                    listAction: '<%=Config.Website %>/MarkToMarket/frm_MarkToMarketPoint.aspx/PointList',
                    createAction: '<%=Config.Website %>/MarkToMarket/frm_MarkToMarketPoint.aspx/CreatePoint',
                    updateAction: '<%=Config.Website %>/MarkToMarket/frm_MarkToMarketPoint.aspx/UpdatePoint',
                    deleteAction: '<%=Config.Website %>/MarkToMarket/frm_MarkToMarketPoint.aspx/DeletePoint'
                },
                fields: {
                    CONFIGPOINTID: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    FUND: {
                        title: 'Fund' 
                    },
                    CCY: {
                        title: 'CCY' 
                    },
                    CTRCCY: {
                        title: 'CTRCCY' 
                    },
                    SWAP_MID_POINT: {
                        title: 'SWAP POINT' 
                    },
                    SPOT_MID_POINT: {
                        title: 'SPOT' 
                    },
                    OUTRIGHT_MID_POINT: {
                        title: 'REVAL' 
                    }
                }
            });

            $('#PointTableContainer').jtable('load');
        });

    </script>
</asp:Content>
