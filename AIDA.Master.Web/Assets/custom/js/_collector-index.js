jQuery(document).ready(function () {
    var oTable = $('.datatable').DataTable({
        //'scrollX': true,
        'bProcessing': true,
        'bServerSide': true,
        'autoWidth': false,
        'order': [[3, 'asc']],
        'iDisplayLength': 10,
        'lengthMenu': [[10, 50, 100, -1], [10, 50, 100, 'All']],
        'language': {
            'processing': 'loading...'
        },
        'ajax': {
            'url': baseUrl + 'Collector/Datatable',
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
                    

                    var url = 'javascript:;';
                    var disabled = '';

                    if ($('#btn-add').length > 0) {
                        url = baseUrl + 'Collector/Edit/' + s.nik;
                    }
                    else {
                        disabled = 'disabled="disabled"';
                    }

                    return '<a href="' + url + '" class="btn btn-xs default green-stripe" ' + disabled +'>edit</a >';
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
            { 'mData': 'f_valid_to', 'sClass': 'text-center' }
        ],
        rowCallback: function (row, data, index) {
            var dtInfo = oTable.page.info();

            var arrTd = $(row).children();
            $(arrTd[0]).html(dtInfo.start + index + 1);

            return row;
        },
        drawCallback: function (settings) {
            $('.datatable').find('.checkbox').uniform();

            arrData = [];

            $('.datatable').find('input[name="arrData"]').each(function () {
                if (this.checked) {
                    arrData.push({
                        NIK: $(this).val(),
                        IsAllowed: true
                    });
                }
                else {
                    arrData.push({
                        NIK: $(this).val(),
                        IsAllowed: false
                    });
                }
            });
        }
    });
});