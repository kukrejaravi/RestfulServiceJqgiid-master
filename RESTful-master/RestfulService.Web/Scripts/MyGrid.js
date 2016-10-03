$(function () {
    $("#grid").jqGrid({  
        url: "/Home/GetGridData",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['Id', 'Title', 'Url'],
        colModel: [
            { key: true, hidden: true, name: 'id', index: 'id', editable: true},
            { key: false, name: 'title', index: 'title', editable: true, editrules: { required: true } },
            { key: false, name: 'url', index: 'url', editable: true, editrules: { required: true } }],
        pager: $('#pager'),
        rowNum: 2,
        rowList: [2, 4, 6, 8],
        height: '100%',
        viewrecords: true,
        caption: 'JQgridSample',
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            Id: "0"
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: true, add: true, del: true, search: false, refresh: true },
        {
            // edit options
         
            zIndex: 100,
            url: "/Home/Edit",
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // add options
            zIndex: 100,
            url: "/Home/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // delete options
            zIndex: 100,
            url: "/Home/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this task?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        });//Grid ends

});//Function ends