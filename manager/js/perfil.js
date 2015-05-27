$(document).ready(function () {

    $('.chkFull').click(function () {

        var chkFull = $('INPUT', this);
        var tr = $(chkFull).parent().parent().parent("TR");
        var chkGeral = $('SPAN.chkGeral INPUT', tr);

        if ($(chkFull).attr('checked') == true) {
            $(chkGeral).removeAttr('checked');
        }

    });

    $('.chkGeral').click(function () {

        var chkGeral = $('INPUT', this);
        var tr = $(chkGeral).parent().parent().parent("TR");
        var chkFull = $('SPAN.chkFull INPUT', tr);
        var count = $('SPAN.chkGeral INPUT:checked', tr).length;

        if (count == 4) {
            $('SPAN.chkGeral INPUT:checked', tr).removeAttr('checked');
            $(chkFull).attr('checked', 'checked');
            return;
        }

        if ($(chkGeral).attr('checked') == true) {
            $(chkFull).removeAttr('checked');
        }

    });

});