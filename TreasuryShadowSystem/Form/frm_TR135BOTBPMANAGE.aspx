<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_TR135BOTBPMANAGE.aspx.cs" Inherits="TreasuryShadowSystem.Form.frm_TR135BOTBPMANAGE" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link href="../Scripts/jtable/themes/metro/blue/jtable.css" rel="stylesheet"  type="text/css" />
    <script type="text/javascript" src="../Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="StudentTableContainer" style="width: 1000px;"></div>
    <script type="text/javascript">

        $(document).ready(function() {

            //Prepare jtable plugin
            $('#StudentTableContainer').jtable({
                title: 'TBP Management',
                paging: false,
                sorting: false,
                actions: {
                    listAction: '<%=Config.Website %>/Form/frm_TR135BOTBPMANAGE.aspx/TBPList',
                    createAction: '<%=Config.Website %>/Form/frm_TR135BOTBPMANAGE.aspx/CreateTBP',
                    updateAction: '<%=Config.Website %>/Form/frm_TR135BOTBPMANAGE.aspx/UpdateTBP'
                },
                fields: {
                    TBPID: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    MonthYear: {
                        title: 'MonthYear'
                    },
                    Assets: {
                        title: 'Assets'
                    },
                    Liabilities: {
                        title: 'Liabilities'
                    }/*,
                    C970003: {
                        title: 'C970003'
                    },
                    C970004: {
                        title: 'C970004'
                    },
                    C970005: {
                        title: 'C970005'
                    },
                    C970006: {
                        title: 'C970006'
                    },
                    C970007: {
                        title: 'C970007'
                    },
                    C970008: {
                        title: 'C970008'
                    },
                    C970009: {
                        title: 'C970009'
                    },
                    C970010: {
                        title: 'C970010'
                    },
                    C970011: {
                        title: 'C970011'
                    },
                    C970012: {
                        title: 'C970012'
                    }*/
                }
            });

            $('#StudentTableContainer').jtable('load');
        });

    </script>
</asp:Content>
