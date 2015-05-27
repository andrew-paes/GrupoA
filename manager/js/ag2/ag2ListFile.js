var globalArquivoId = 0;

$(document).ready(function() {

    $('.lnkInserirArquivo').click(function() {

        var urlPath = '';
        var tipoArquivo = $(this).prev();
        var registroId = $(this).prev().prev();
        var hdnTargetFolder = $(this).prev().prev().prev();
        var hdnMaxFileLength = $(this).prev().prev().prev().prev();
        var hdnModuleName = $(this).prev().prev().prev().prev().prev();
        var hdnScriptModal = $(this).prev().prev().prev().prev().prev().prev();

        var lnkInserirArquivoHidden = $(this).next();

        if ($(hdnScriptModal).val() != '')
            urlPath = 'module/' + $(hdnModuleName).val() + '/' + $(hdnScriptModal).val();
        else
            urlPath = 'module/modalArquivo/modalArquivo.aspx';

        $.fancybox({
            'type': 'iframe',
            'href': urlPath + '?idObjPostback=' +
                                $(lnkInserirArquivoHidden).attr('id') +
                                '&tipo=' + $(tipoArquivo).val() +
                                '&arquivoId=' + globalArquivoId +
                                '&id=' + $(registroId).val() +
                                '&tf=' + $(hdnTargetFolder).val() +
                                '&md=' + $(hdnModuleName).val() +
                                '&mfl=' + $(hdnMaxFileLength).val(),
            'width': 400,
            'height': 250,
            'autoScale': false,
            'scrolling': 'no',
            'centerOnScroll': true,
            'overlayOpacity': 0.5,
            'hideOnOverlayClick': false,
            'showCloseButton': false
        });

    });

    $('.btnCancelarModal').click(function() {

        window.parent.globalArquivoId = 0;
        window.parent.$.fancybox.close();
    });

});

function EditarRegistro(arquivoId, btnInserirArquivoId) {

    globalArquivoId = arquivoId;

    $('#' + btnInserirArquivoId).click();

}

function ExecuteDelegateEvent(idObjPostback, idArquivoInserido) {

    $('#' + idObjPostback + '').next().val('' + idArquivoInserido + '');
    $('#' + idObjPostback + '').click();

}