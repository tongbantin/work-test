﻿var _parentUrl = getAbsolutePath();

$(function() {
    var prmSearch = { multipleSearch: true, overlay: false };
    var grid = $("#jqgrid_ctrs");
    var lastsel;

    $("#jqgrid_ctrs").jqGrid({
        prmNames: {
            search: "isSearch",
            nd: null,
            rows: "numRows",
            page: "page",
            sort: "sortField",
            order: "sortOrder"
        },
        // add by default to avoid webmethod parameter conflicts
        postData: { searchString: '', searchField: '', searchOper: '' },
        // setup ajax call to webmethod
        datatype: function(pdata) { getData(pdata); },
        height: 250,
        // this is what jqGrid is looking for in json callback
        jsonReader: {
            root: "rows",
            page: "page",
            total: "totalpages",
            records: "totalrecords",
            cell: "cell",
            id: "UserGroupID", //index of the column with the PK in it 
            userdata: "userdata",
            repeatitems: true
        },
        colNames: ['ID', 'NAME', 'BU', 'DEPARTMENT'],
        colModel: [
                    { name: 'UserGroupID', index: 'UserGroupID', width: 50, align: 'right', searchoptions: { sopt: ['eq', 'ne'] }, editable: true, editoptions: { readonly: 'true'} },
                    { name: 'UserGroupName', index: 'UserGroupName', width: 400, searchoptions: { sopt: ['eq', 'ne'] }, editable: true, edittype: 'text', editrules: { required: true} },
                    { name: 'BU', index: 'BU', width: 100, searchoptions: { sopt: ['eq'] }, editable: true, edittype: 'text' },
                    { name: 'DepCode', index: 'DepCode', width: 400, searchoptions: { sopt: ['eq', 'cn'] }, editable: true, edittype: 'text' }
                ],
        rowNum: 15,
        rowList: [15, 30, 45],
        //loadonce: true,
        gridview: true,
        //height: 'auto',
        pager: '#jqgrid_ctrs_pager',
        rownumbers: false,
        viewrecords: true,
        sortname: 'UserGroupID',
        sortorder: "asc",
        //editurl: '<%= ResolveClientUrl("~/adminws.asmx/PerformCRUDAction") %>',
        editurl: _parentUrl + '/adminws.asmx/PerformCRUDGroupAction',
        caption: "UserGroup Master",
        onCellSelect: function(rowid, iCol, aData) {

            //            if (rowid && rowid !== lastsel) {

            //                if (lastsel)
            //                    $("#jqgrid_ctrs").jqGrid('restoreRow', lastsel);
            //                $("#jqgrid_ctrs").jqGrid('editRow', rowid, true);
            //                lastsel = rowid;
            //            }
        },
        success: function(data, textStatus) {
            if (textStatus == "success")
                ReceivedClientData(JSON.parse(getMain(data)).rows);
        },
        error: function(data, textStatus) {
            alert('An error has occured retrieving data!');
        }
    }).navGrid('#jqgrid_ctrs_pager', { view: false, del: false, add: true, edit: true, refresh: true },
               {closeOnEscape: true, reloadAfterSubmit: true, closeAfterEdit: true, width: 400 }, // default settings for edit
               {closeOnEscape: true, reloadAfterSubmit: true, closeAfterEdit: true, width: 400 }, // default settings for add
               {}, // delete instead that del:false we need this
               {closeOnEscape: true, multipleSearch: false, closeAfterSearch: true }, // search options
               {} /* view parameters*/
             );


});

function getData(pData) {

    $.ajax({
        type: 'POST',
        contentType: "application/json; charset=utf-8",
        //url: '<%= ResolveClientUrl("~/adminws.asmx/GetMenuData") %>',
        url: _parentUrl + '/adminws.asmx/GetUSGroupData',
        //data: '{}',
        data: JSON.stringify(pData),
        dataType: "json",
        success: function(data, textStatus) {
            if (textStatus == "success") {
                //ReceivedClientData(JSON.parse(getMain(data)).rows);
                var grid = $("#jqgrid_ctrs")[0];
                grid.addJSONData(JSON.parse(data.d));
            }
        },
        error: function(data, textStatus) {
            alert('An error has occured retrieving data!');
        }
    });
}
function ReceivedClientData(data) {
    var thegrid = $("#jqgrid_ctrs");
    thegrid.clearGridData();
    for (var i = 0; i < data.length; i++)
        thegrid.addRowData(i + 1, data[i]);
}
function getMain(dObj) {
    if (dObj.hasOwnProperty('d'))
        return dObj.d;
    else
        return dObj;
}

function getAbsolutePath() {
    var loc = window.location;
    var pathName = loc.pathname.substring(0, loc.pathname.lastIndexOf('/') + 1);
    var newUrl = loc.href.substring(0, loc.href.length - (loc.pathname + loc.search + loc.hash).length);
    if (pathName != "/") {
        var p = pathName.split('/');
        var pn = '/' + p[1] + '/';
        if (pn.indexOf("Web") != -1)
            newUrl = loc.href.substring(0, loc.href.length - ((loc.pathname + loc.search + loc.hash).length - pn.length));
    }
    return newUrl;
}