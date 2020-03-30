jQuery(document).ready(function () {
    var oTable = $('.datatable').DataTable({
        //'scrollX': true,
        'dom': "<'row'<'col-sm-3 clearfix'l><'col-sm-6'<'rayon-wrapper text-center'>><'col-sm-3'f>>" +
            "<'row'<'col-md-12'tr>>" +
            "<'row'<'col-md-5'i><'col-md-7'p>>",
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
            'url': baseUrl + 'MasterListTagih/Datatable',
            'type': 'post',
            'dataType': 'json',
            'data': function (d) {
                return {
                    Keyword: d.search.value,
                    Length: d.length,
                    Start: d.start,
                    IndexOrderCol: d.order[0].column,
                    OrderType: d.order[0].dir,
                    RayonCode: $('#datatable-filter-rayon').find('select[name="RayonCode"]').val()
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
            { 'mData': 'customer_code', 'sClass': 'text-left' },
            { 'mData': 'customer_name', 'sClass': 'text-left' },
            { 'mData': 'rayon_code', 'sClass': 'text-left' },
            { 'mData': 'slm_nik', 'sClass': 'text-left' },
            { 'mData': 'slm_fullname', 'sClass': 'text-left' },
            { 'mData': 'fss_nik', 'sClass': 'text-left' },
            { 'mData': 'fss_fullname', 'sClass': 'text-left' },
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

    var $filterRayon = $('#datatable-filter-rayon');
    $('div.rayon-wrapper').append($filterRayon.show().clone());
    $filterRayon.remove();

    $('select[name="RayonCode"]').select2();

    $('select[name="RayonCode"]').on('change', function () {
        loadDatatable();
    });

    function loadDatatable() {
        oTable.draw();
    }

    $formExportExcel = $('#form-export-excel');

    oTable.on('xhr', function () {
        var oTableData = oTable.ajax.params();

        $formExportExcel.find('input[name="Keyword"]').val(oTableData.Keyword);
        $formExportExcel.find('input[name="Length"]').val(oTableData.Length);
        $formExportExcel.find('input[name="Start"]').val(oTableData.Start);
        $formExportExcel.find('input[name="IndexOrderCol"]').val(oTableData.IndexOrderCol);
        $formExportExcel.find('input[name="OrderType"]').val(oTableData.OrderType);
        $formExportExcel.find('input[name="RayonCode"]').val(oTableData.RayonCode);
    });

    $('#btn-export-excel').on('click', function () {
        $formExportExcel.submit();
    });
});