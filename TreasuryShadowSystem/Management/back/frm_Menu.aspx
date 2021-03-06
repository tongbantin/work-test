<%@ Page Title="" Language="C#" MasterPageFile="~/Content.Master" AutoEventWireup="true" CodeBehind="frm_Menu.aspx.cs" Inherits="TreasuryShadowSystem.Management.frm_Menu" %>
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
    <div id="StudentTableContainer"></div>
    <script type="text/javascript">

        $(document).ready(function() {

            //Prepare jtable plugin
            $('#StudentTableContainer').jtable({
                title: 'Menu Management',
                paging: true,
                pageSize: 10,
                sorting: true,
                toolbars: {
                    hoverAnimation: true, //Enable/disable small animation on mouse hover to a toolbar item.
                    hoverAnimationDuration: 60, //Duration of the hover animation.
                    hoverAnimationEasing: undefined //Easing of the hover animation. Uses jQuery's default animation ('swing') if set to undefined.
                },
                defaultSorting: 'Data_Type ASC',
                actions: {
                    listAction: '<%=Config.Website %>/Management/frm_Menu.aspx/MenuList',
                    createAction: '<%=Config.Website %>/Management/frm_Menu.aspx/CreateMenu',
                    updateAction: '<%=Config.Website %>/Management/frm_Menu.aspx/UpdateMenu',
                    deleteAction: '<%=Config.Website %>/Management/frm_Menu.aspx/DeleteMenu'
                },
                fields: {
                    ID: {
                        key: true,
                        create: false,
                        edit: false,
                        list: false
                    },
                    Data_Type: {
                        title: 'Type',
                        options: ['MENU', 'REPORT', 'MAINTAIN'],
                        inputClass: 'validate[required]'
                    },
                    Data_Code: {
                        title: 'Code'
                    },
                    Data_Name_Th: {
                        title: 'Name(Th)',
                        inputClass: 'inputLong'
                    },
                    Data_Name_Eng: {
                        title: 'Name(En)',
                        inputClass: 'inputLong validate[required]'
                    },
                    ParentID: {
                        title: 'Group',
                        options: '<%=Config.Website %>/Management/frm_Menu.aspx/GetParentOption',
                        inputClass: 'validate[required]'
                    },
                    Path_URL: {
                        title: 'URL',
                        inputClass: 'inputLong validate[required]'
                    },
                    Seq: {
                        title: 'Sequence',
                        visibility: 'hidden',
                        inputClass: 'validate[required]'
                    },
                    Enabled: {
                        title: 'Enabled',
                        options: {'0':'Disable', '1':'Enable'},
                        inputClass: 'validate[required]',
                        visibility: 'hidden'
                    },
                    GroupID: {
                        title: 'GroupID',
                        visibility: 'hidden',
                        options: '<%=Config.Website %>/Management/frm_Menu.aspx/GetParentOption',
                        inputClass: 'validate[required]'
                    }
                },
                //Initialize validation logic when a form is created
                formCreated: function(event, data) {
                    data.form.children(':lt(16)').wrapAll('<div class="col1"/>');
                    data.form.children(':gt(0)').wrapAll('<div class="col2"/>');
                    data.form.parents('.ui-dialog').css('width', '500px');
                    var divwidth = data.form.parents('.ui-dialog').width();
                    var left = ($(document).width() - parseFloat(divwidth)) / 2;
                    data.form.parents('.ui-dialog').css('left', left);

                    data.form.validationEngine();
                }
            });

            $('#StudentTableContainer').jtable('load');
        });

    </script>
</asp:Content>
