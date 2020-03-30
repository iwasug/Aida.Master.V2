jQuery(document).ready(function () {
    $('.datepicker').datepicker({
        format: 'mm-yyyy',
        viewMode: 'months',
        minViewMode: 'months',
        autoclose: true
    });
    var oTable = $('.datatable').DataTable({
        "searching": false,
        ////'scrollX': true,
        //'dom': "<'row'<'col-sm-3 clearfix'l><'col-sm-6'<'rayon-wrapper text-center'>><'col-sm-3'f>>" +
        //    "<'row'<'col-md-12'tr>>" +
        //    "<'row'<'col-md-5'i><'col-md-7'p>>",
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
            'url': baseUrl + 'TargetSales/DatatableIndex',
            'type': 'post',
            'dataType': 'json',
            'data': function (d) {
                return {
                    Keyword: d.search.value,
                    Length: d.length,
                    Start: d.start,
                    IndexOrderCol: d.order[0].column,
                    OrderType: d.order[0].dir,
                    Periode: $('#Periode').val(),
                    RayonCode: $('select[name="RayonCode"]').val()
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
            { 'mData': 'rayon_code', 'sClass': 'text-left' },
            { 'mData': 'slm_nik', 'sClass': 'text-left' },
            { 'mData': 'slm_name', 'sClass': 'text-left' },
            { 'mData': 'fss_nik', 'sClass': 'text-left' },
            { 'mData': 'fss_name', 'sClass': 'text-left' },
            { 'mData': 'achi_group', 'sClass': 'text-center' },
            { 'mData': 'division', 'sClass': 'text-center' },
            { 'mData': 'material', 'sClass': 'text-center' },
            { 'mData': 'bulan', 'sClass': 'text-center' },
            { 'mData': 'tahun', 'sClass': 'text-center' },
            { 'mData': 'target', 'sClass': 'text-right' }
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
        },
        "footerCallback": function (row, data, start, end, display) {
            var api = this.api();

            // converting to interger to find total
            var intVal = function (i) {
                return typeof i === 'string' ?
                    i.replace(/[\$,]/g, '') * 1 :
                    typeof i === 'number' ?
                        i : 0;
            };

            var monTotal = api
                .column(11)
                .data()
                .reduce(function (a, b) {
                    return intVal(a) + intVal(b);
                }, 0);
            //alert(monTotal);
            $(api.column(11).footer()).html(monTotal.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        }
    });

    var $filterRayon = $('#datatable-filter-rayon');
    $('div.rayon-wrapper').append($filterRayon.show().clone());
    $filterRayon.remove();

    $formExportExcel = $('#form-export-excel');

    oTable.on('xhr', function () {
        var oTableData = oTable.ajax.params();

        $formExportExcel.find('input[name="Keyword"]').val(oTableData.Keyword);
        $formExportExcel.find('input[name="Length"]').val(oTableData.Length);
        $formExportExcel.find('input[name="Start"]').val(oTableData.Start);
        $formExportExcel.find('input[name="IndexOrderCol"]').val(oTableData.IndexOrderCol);
        $formExportExcel.find('input[name="OrderType"]').val(oTableData.OrderType);
        $formExportExcel.find('input[name="Periode"]').val(oTableData.Periode);
        $formExportExcel.find('input[name="RayonCode"]').val(oTableData.RayonCode);
    });

    $('input[name="FormattedValidDate"]').on('change', function () {
        loadDatatable();
    });

    $('select[name="RayonCode"]').on('change', function () {
        loadDatatable();
    });

    function loadDatatable() {
        oTable.draw();
    }

    $('#btn-export-excel').on('click', function () {

        $formExportExcel.submit();
    });
   
});