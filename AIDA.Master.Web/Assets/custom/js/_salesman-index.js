jQuery(document).ready(function () {
    var oTable = $('.datatable').DataTable({
        //'scrollX': true,
        //'dom': "<'row'<'col-sm-3 clearfix'l><'col-sm-6'<'date-wrapper text-center'>><'col-sm-3'f>>" +
        //    "<'row'<'col-md-12'tr>>" +
        //    "<'row'<'col-md-5'i><'col-md-7'p>>",
        'bProcessing': true,
        'bServerSide': true,
        'autoWidth': false,
        'order': [[2, 'asc']],
        'iDisplayLength': 10,
        'lengthMenu': [[10, 50, 100, -1], [10, 50, 100, 'All']],
        'language': {
            'processing': 'loading...'
        },
        'ajax': {
            'url': baseUrl + 'Salesman/Datatable',
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
                    return '<a href="'+baseUrl+'Salesman/Edit/'+s.nik+'" class="btn btn-xs default green-stripe">edit</a >';
                }, 'bSortable': false, 'sClass': 'text-left'
            },
            { 'mData': 'nik', 'sClass': 'text-center' },
            { 'mData': 'fullname', 'sClass': 'text-left' },
            {
                'mData': function (s) {
                    if (s.is_role) {
                        return '<i class="fa fa-check font-green"></i>';
                    }
                    else {
                        return '<i class="fa fa-times font-red"></i>';
                    }
                }, 'sClass': 'text-center'
            },
            { 'mData': 'f_valid_from', 'sClass': 'text-center' },
            { 'mData': 'f_valid_to', 'sClass': 'text-center' },
        ],
        rowCallback: function (row, data, index) {
            var dtInfo = oTable.page.info();

            var arrTd = $(row).children();
            $(arrTd[0]).html(dtInfo.start + index + 1);

            return row;
        },
        drawCallback: function (settings) {
            //$('[data-toggle="tooltip"]').tooltip();

            //$('#btn-export-excel').removeAttr('disabled');
            //$('#btn-refresh').removeAttr('disabled');
        }
    });

    var $filterDate = $('#datatable-filter-date');
    $('div.date-wrapper').append($filterDate.show().clone());
    $filterDate.remove();

    $('.datepicker').datepicker({
        format: 'mm-yyyy',
        viewMode: 'months',
        minViewMode: 'months',
        autoclose: true
    });

    $('#btn-date').on('click', function () {
        $('#main-form').submit();
    });

    //$('#datatable-filter-date').find('input[name="p"]').on('change', function () {
    //    var curVal = $('#main-form').find('input[name="p"]').val();

    //    if (curVal == $(this).val()) return false;

    //    $('#main-form').find('input[name="p"]').val($(this).val());
    //});
});