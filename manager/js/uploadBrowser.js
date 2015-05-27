$(document).ready(function() {

    $("#FileUpload1").filestyle({
        image: "../img/btn_Selecionar.png",
        imageheight: 22,
        imagewidth: 96,
        width: 298
    });

    $('.btnUpload').change(function() {

        window.setTimeout(function() {

            $.blockUI({ message: '<div style=\'padding: 10px;border: 5px solid #FF6500;\'><div style=\'font-weight: bold;\'>Aguarde...</div></div>', overlayCSS: { backgroundColor: '#fff' }, css: { border: 'none', left: ($(window).width() - 150) / 2 + 'px', width: '150px'} });

            //__doPostBack('btnUpload', '');

        }, 300);

        return false;

    });

    $('.divFiles').mouseover(function() {

        $(this).removeClass('divFilesEscuro');
        $(this).addClass('divFilesEscuro');

    });

    $('.divFiles').mouseout(function() {

        $(this).removeClass('divFilesEscuro');

    });

    $('.divFiles').click(function(event) {

        var chk = $('input:checkbox', this);

        if (event.target.nodeName != 'INPUT') {

            if ($(chk).parent().hasClass('ckEditorClass')) {
                $('.ckEditorClass INPUT').removeAttr('checked');
                $(chk).attr('checked', 'checked');
            }
            else {
                if ($(chk).attr('checked') == true) {
                    $(chk).removeAttr('checked')
                }
                else {
                    $(chk).attr('checked', 'checked')
                }
            }
        }

        if ($('input:checkbox:checked').length > 0) {

            $('.divFecharSalvar').show();
            $('.divFecharSalvar').animate(
            {
                'height': 22,
                'padding-bottom': 3,
                'padding-top': 3
            }
            , 300);

        }
        else {

            $('.divFecharSalvar').animate(
            {
                'height': 0,
                'padding-bottom': 0,
                'padding-top': 0
            }, 300, function() {

                $(this).hide();

            });

        }

    });

    $('.divFilesLista').click(function(event) {

        var chk = $('input:checkbox', this);

        if (event.target.nodeName != 'INPUT') {

            if ($(chk).parent().hasClass('ckEditorClass')) {
                $('.ckEditorClass INPUT').removeAttr('checked');
                $(chk).attr('checked', 'checked');
            }
            else {
                if ($(chk).attr('checked') == true) {
                    $(chk).removeAttr('checked')
                }
                else {
                    $(chk).attr('checked', 'checked')
                }
            }
        }

        if ($('input:checkbox:checked').length > 0) {

            $('.divFecharSalvar').show();
            $('.divFecharSalvar').animate(
            {
                'height': 22,
                'padding-bottom': 3,
                'padding-top': 3
            }
            , 300);

        }
        else {

            $('.divFecharSalvar').animate(
            {
                'height': 0,
                'padding-bottom': 0,
                'padding-top': 0
            }, 300, function() {

                $(this).hide();

            });

        }

    });

    $('.divFilesLista').mouseover(function() {

        $(this).removeClass('divFilesListaEscuro');
        $(this).addClass('divFilesListaEscuro');

    });

    $('.divFilesLista').mouseout(function() {

        $(this).removeClass('divFilesListaEscuro');

    });

    // Show menu when #myDiv is clicked
    $("DIV.divFiles, DIV.divFilesLista").contextMenu({
        menu: 'myMenu'
    },
        function(action, el, pos) {

            var hdnFileName = $(el).prev().val();
            var hdnFilePath = $(el).prev().prev().val();
            var filePath = hdnFilePath + hdnFileName;

            if (action == 'delete') {

                $.blockUI({ message: '<div style=\'padding: 10px;border: 5px solid #FF6500;\'><div style=\'font-weight: bold;\'>Aguarde...</div></div>', overlayCSS: { backgroundColor: '#fff' }, css: { border: 'none', left: ($(window).width() - 150) / 2 + 'px', width: '150px'} });

                $.ajax({
                    type: "POST",
                    url: 'UploadBrowser.aspx/DeleteFile',
                    data: "{'filePath':'" + filePath + "'}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    error: function(err) {

                        $.unblockUI();
                        alert('Ocorreu um erro na requisição ao servidor');

                    },
                    success: function(retorno) {

                        $.unblockUI();

                        if (retorno == undefined || retorno.d == '' || retorno.d == 'true')
                            $(el).hide(300);
                        else {
                            alert(retorno.d);
                        }

                    }
                });

            }
            else if (action == 'download') {
                $('#ifrDownLoad').attr('src', 'Download.aspx?f=' + filePath);
            }
            else if (action == 'renomear') {

                var fileName = jQuery.url.setUrl(filePath).attr("file");

                var divRename = '<div style="padding: 5px;border: 3px solid #ccc;">';
                divRename += '<div style="font-weight: bold;">';
                divRename += '<input type="text" class="txt" value="' + fileName + '" id="txtRename" style="width: 280px;" name="txtRename">';
                divRename += '<div style="margin-top: 5px;">';
                divRename += '<a style="color: orange; font-weight: bold;" id="lnkRename" href="javascript:;">Enviar</a>';
                divRename += '&nbsp;&nbsp;&nbsp;&nbsp;';
                divRename += '<a style="color: orange; font-weight: bold;" id="lnkCancelarRename" href="javascript:;">Cancelar</a>';
                divRename += '</div>';
                divRename += '</div>';
                divRename += '</div>';

                $.blockUI({ message: divRename, overlayCSS: { backgroundColor: '#fff' }, css: { border: 'none', left: ($(window).width() - 300) / 2 + 'px', width: '300px'} });

                $('#lnkCancelarRename').click(function() {

                    $.unblockUI();

                });

                $('#lnkRename').click(function() {

                    $('#txtTitulo').removeClass('txtError');
                    if ($('#txtRename').val() == '') {
                        $('#txtRename').addClass('txtError');
                    }
                    else {

                        var arq1 = jQuery.url.setUrl(filePath).attr("file");
                        var arq2 = $('#txtRename').val();

                        if (arq1 == arq2) {
                            $.unblockUI();
                            return;
                        }

                        $.ajax({
                            type: "POST",
                            url: 'UploadBrowser.aspx/RenameFile',
                            data: "{'filePathOriginal':'" + filePath + "', 'filePathNovo':'" + $('#txtRename').val() + "'}",
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            async: true,
                            error: function(err) {

                                $.unblockUI();
                                alert('Ocorreu um erro na requisição ao servidor');

                            },
                            success: function(retorno) {

                                if (retorno == undefined || retorno.d == '' || retorno.d == 'true') {
                                    $('.divNomeArquivo', el).html($('#txtRename').val());
                                    $(el).prev().val($('#txtRename').val());
                                    $.unblockUI();
                                }
                                else {
                                    alert(retorno.d);
                                }

                            }
                        });
                    }

                });

            }

        });

    $('.itemAccordion').click(function() {

        var div = $(this).next();

        $('#hdnControleAccord').val($(div).attr('id'))
        $('.itemAccordionConteudo').slideUp(300);
        $(div).slideDown(300);

    });

    var hdnControleAccord = $('#hdnControleAccord').val();
    $('#' + hdnControleAccord).show();

});

function validaTitulo(src, args) {

    args.IsValid = true;
    $('#txtTitulo').removeClass('txtError');
    if ($('#txtTitulo').val() == '') {
        $('#txtTitulo').addClass('txtError');
        args.IsValid = false;
    }

    return args.IsValid;
}

function validaPalavraChave(src, args) {

    args.IsValid = true;
    $('#txtPalavraChave').removeClass('txtError');
    if ($('#txtPalavraChave').val() == '') {
        $('#txtPalavraChave').addClass('txtError');
        args.IsValid = false;
    }

    return args.IsValid;
}

function validaArquivo(src, args) {

    args.IsValid = true;
    $('#txt_FileUpload1').removeClass('txtError');
    if ($('#txt_FileUpload1').val() == '') {
        $('#txt_FileUpload1').addClass('txtError');
        args.IsValid = false;
    }

    return args.IsValid;
}

function validaBusca(src, args) {

    args.IsValid = true;
    $('#txtPalavraChaveBusca').removeClass('txtError');
    if ($('#txtPalavraChaveBusca').val() == '') {
        $('#txtPalavraChaveBusca').addClass('txtError');
        args.IsValid = false;
    }

    return args.IsValid;
}
