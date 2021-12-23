<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_AssetGroup.aspx.cs" Inherits="TreasuryShadowSystem.Asset.frm_AssetGroup" %>
<%@ Import namespace="TreasuryShadowSystem.Configuration" %>
<asp:Content ID="Content1" ContentPlaceHolderID="headContent" runat="server">
    <link href="../Scripts/jtable/themes/metro/blue/jtable.css" rel="stylesheet"  type="text/css" />
    <script type="text/javascript" src="../Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/jquery.jtable.js"></script>
    <script type="text/javascript" src="../Scripts/jtable/extensions/jquery.jtable.aspnetpagemethods.js"></script>
    <style type="text/css">
        input.inputLong { width: 400px !important; }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContent" runat="server">
    <div id="AssetGroupTableContainer" style="height:500px; overflow:auto"></div>
    <script type="text/javascript">

        $(document).ready(function() {

            //Prepare jtable plugin
        $('#AssetGroupTableContainer').jtable({
                title: 'Asset Group',
                paging: false,
                sorting: false,
                toolbars: {
                    hoverAnimation: true, //Enable/disable small animation on mouse hover to a toolbar item.
                    hoverAnimationDuration: 60, //Duration of the hover animation.
                    hoverAnimationEasing: undefined //Easing of the hover animation. Uses jQuery's default animation ('swing') if set to undefined.
                },
                defaultSorting: 'NO ASC',
                actions: {
                    listAction: '<%=Config.Website %>/Asset/frm_AssetGroup.aspx/AssetGroupList',
                    createAction: '<%=Config.Website %>/Asset/frm_AssetGroup.aspx/CreateAssetGroup',
                    updateAction: '<%=Config.Website %>/Asset/frm_AssetGroup.aspx/UpdateAssetGroup',
                    deleteAction: '<%=Config.Website %>/Asset/frm_AssetGroup.aspx/DeleteAssetGroup'
                },
                fields: {
                    ASSETGROUPID: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    NO: {
                        title: 'No.'
                    },
                    DESCRIPTION: {
                        title: 'Description',
                        width: '90%'
                    },
                    SUBREPORTFIX: {
                        title: 'SUB-REPORT TEMPLATE',
                        visibility: 'hidden'
                    },
                    PARENT: {
                        title: 'Under Group',
                        options: '<%=Config.Website %>/Asset/frm_AssetGroup.aspx/GetAssetGroupOption',
                        visibility: 'hidden'
                    },
                    RANGEYEAR: {
                        title: 'For Maturity Date',
                        type: 'checkbox',
                        values: { 'n': 'No', 'y': 'Yes' },
                        defaultValue: 'n',
                        visibility: 'hidden'
                    },
                    SCRIPLESS: {
                        title: 'Scripless',
                        options: { '' : '', 'n': 'No', 'y': 'Yes' },
                        defaultValue: ''
                    },
                    SUBGROUP: {
                        title: 'SubGroup',
                        visibility: 'hidden'
                    },  
                    Enabled: {
                        title: 'Enabled',
                        options: { '0': 'Disable', '1': 'Enable' },
                        defaultValue: '1',
                        inputClass: 'validate[required]',
                        visibility: 'hidden'
                    }
                },
                //Initialize validation logic when a form is created
                formCreated: function(event, data) {
                    data.form.validationEngine();
                }
            });

            $('#AssetGroupTableContainer').jtable('load');
        });

    </script>
</asp:Content>
