﻿jQuery(document).ready(function () {
    $('.datepicker').datepicker({
        format: 'mm-yyyy',
        viewMode: 'months',
        minViewMode: 'months',
        autoclose: true
    });
    var oTable = $('.datatable').DataTable({
        //'scrollX': true,
        'bProcessing': true,
        'bServerSide': true,
        'autoWidth': false,
        'order': [[0, 'desc']],
        'iDisplayLength': 10,
        'lengthMenu': [[10, 50, 100, -1], [10, 50, 100, 'All']],
        'language': {
            'processing': 'loading...'
        },
        'ajax': {
            'url': baseUrl + 'HierSales/DatatableIndex',
            'type': 'post',
            'dataType': 'json',
            'data': function (d) {
                return {
                    Keyword: d.search.value,
                    Length: d.length,
                    Start: d.start,
                    IndexOrderCol: d.order[0].column,
                    OrderType: d.order[0].dir
                };
            },
            'error': function (e) {
                alert('terjadi kesalaha sistem!');
            }
        },
        aoColumns: [
            {
                'mData': function (s) {
                    return '';
                }, 'bSortable': false, 'sClass': 'text-right'
            },
            {
                'mData': function (s) {
                    return '<a href="' + baseUrl + 'MasterListSales?rayon=' + s.rayon_code + '" class="">' + s.rayon_code + '</a>';
                }, 'sClass': 'text-left'
            },
            //{ 'mData': 'rayon_code', 'sClass': 'text-left' },
            { 'mData': 'rayon_type', 'sClass': 'text-left' },
            { 'mData': 'plant_code', 'sClass': 'text-left' },
            { 'mData': 'slm_nik', 'sClass': 'text-left' },
            { 'mData': 'slm_fullname', 'sClass': 'text-left' },
            { 'mData': 'fss_nik', 'sClass': 'text-left' },
            { 'mData': 'fss_fullname', 'sClass': 'text-left' },
            { 'mData': 'asm_nik', 'sClass': 'text-left' },
            { 'mData': 'asm_fullname', 'sClass': 'text-left' },
            { 'mData': 'nsm_nik', 'sClass': 'text-left' },
            { 'mData': 'nsm_fullname', 'sClass': 'text-left' },
            { 'mData': 'bum_nik', 'sClass': 'text-left' },
            { 'mData': 'bum_fullname', 'sClass': 'text-left' },
            { 'mData': 'f_valid_from', 'sClass': 'text-center' },
            { 'mData': 'f_valid_to', 'sClass': 'text-center' }
        ],
        rowCallback: function (row, data, index) {
            var dtInfo = oTable.page.info();

            var arrTd = $(row).children();
            $(arrTd[0]).html(dtInfo.start + index + 1);

            return row;
        },
        drawCallback: function (settings) {
            //$('[data-toggle="tooltip"]').tooltip();

            $('#btn-export-excel').removeAttr('disabled');
            //$('#btn-refresh').removeAttr('disabled');
        }
    });

    $formExportExcel = $('#form-export-excel');

    oTable.on('xhr', function () {
        var oTableData = oTable.ajax.params();

        $formExportExcel.find('input[name="Keyword"]').val(oTableData.Keyword);
        $formExportExcel.find('input[name="Length"]').val(oTableData.Length);
        $formExportExcel.find('input[name="Start"]').val(oTableData.Start);
        $formExportExcel.find('input[name="IndexOrderCol"]').val(oTableData.IndexOrderCol);
        $formExportExcel.find('input[name="OrderType"]').val(oTableData.OrderType);
    });

    $('#btn-export-excel').on('click', function () {
        $formExportExcel.submit();
    });
    $('#btn-modal-submit').on('click', function () {
        $formExportExcel.submit();
    });
});