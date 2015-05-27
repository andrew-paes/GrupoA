//Controle a modal de mensagens do sistema
var showMsg = false;

var formIsChanged = false;

// Função Popup: <a href="arquivo.ext" onclick="popup(this.href,'360','535','1'); return false;"></a>
function popup(url, w, h, s) {
    var oW = window.open(url, 'popup', 'width=' + w + ',height=' + h + ',directories=0,location=0,menubar=0,resizable=0,scrollbars=' + s + ',status=0,toolbar=0,marginleft=0,margintop=0,left=' + (((screen.availWidth - w) / 2) + -10) + ',top=' + (((screen.height - h) / 2) + -10));
}

//Função para mostrar/ocultar menu

imgDivisorAberto = new Image();
imgDivisorAberto.src = "../../img/img_divisor.gif";
imgDivisorFechado = new Image();
imgDivisorFechado.src = "../../img/img_divisor_fechado.gif";

function mostra(item) {
    if (document.getElementById(item).style.display == "block") {
        document.getElementById(item).style.display = "none";
        if (document.images) {
            document.images["seta_divisor"].src = eval("imgDivisorFechado.src");
        }
    } else {
        document.getElementById(item).style.display = "block";
        if (document.images) {
            document.images["seta_divisor"].src = eval("imgDivisorAberto.src");
        }
    }
}

//Função para mostrar/ocultar submenus
var SubMenuAnt;
var bltSubMenuAnt;
imgMenuMenos = new Image();
imgMenuMenos.src = "../img/blt_menos.gif";
imgMenuMais = new Image();
imgMenuMais.src = "../img/blt_mais.gif";

function mostrasub(item) {
    if (SubMenuAnt == "submenu_" + item) {
        if ($("#submenu_" + item).height() > 0) {
            $("#submenu_" + item).show();
            $("#submenu_" + item).attr("src", eval("imgMenuMais.src"));
        }
    } else {
        if (SubMenuAnt != undefined) {
            $("#" + SubMenuAnt).hide();
            document.images[bltSubMenuAnt].src = eval("imgMenuMais.src");
        }
        $("#submenu_" + item).show();
        document.images["bltmenu" + item].src = eval("imgMenuMenos.src");
        SubMenuAnt = "submenu_" + item;
        bltSubMenuAnt = "bltmenu" + item
    }
}


//Função para selecionar todas as checkboxes
var checkboxes = 1;

function seleciona(thisForm) {
    if (checkboxes == 1) {
        selecionacheck(thisForm);
        checkboxes = 0;
    } else {
        selecionauncheck(thisForm);
        checkboxes = 1;
    }
}

function selecionacheck(thisForm) {
    for (i = 0; i < thisForm.linha.length; i++)
        thisForm.linha[i].checked = true;
}

function selecionauncheck(thisForm) {
    for (i = 0; i < thisForm.linha.length; i++)
        thisForm.linha[i].checked = false;
}

function SelectAllCheckboxes(spanChk) {

    // Added as ASPX uses SPAN for checkbox
    var oItem = spanChk.children;
    var theBox = (spanChk.type == "checkbox") ?
    spanChk : spanChk.children.item[0];
    xState = theBox.checked;
    elm = theBox.form.elements;

    for (i = 0; i < elm.length; i++)
        if (elm[i].type == "checkbox" &&
          elm[i].id != theBox.id) {
            //elm[i].click();
            if (elm[i].checked != xState)
                elm[i].checked = !elm[i].checked;
            //elm[i].click();
            //elm[i].checked=xState;
        }
}

//variavel global
var haveItensSelected = false;
var filterBoxFx;

function abre_fecha_box(id) {
    d = document.getElementById(id);
    seta = document.getElementById("seta_" + id);
    if (d.style.display == "block") {
        d.style.display = "none";
        seta.src = "../../img/blt_seta_down.gif";
    }
    else {
        d.style.display = "block";
        seta.src = "../../img/blt_seta_up.gif";
    }
}

function showFilter(buttonFilter, show) {
    var display = "";
    var src = "";
    if (show) {
        display = "block";
        src = "../img/btn_aplicar_filtro_des.gif";
    } else {
        display = "none";
        src = "../img/btn_aplicar_filtro.gif";
    }

    document.getElementById("ctl00_holderPrincipal_IsShwoFilterBox").value = (show == true ? "S" : "N");

    buttonFilter = document.getElementById("ctl00_holderPrincipal_showFilter");

    table = document.getElementById("ctl00_holderPrincipal_tableFilter");
    table.style.display = display;
    buttonFilter.src = src;
}

function ExibeFiltro() {
    var divFiltroProduto = document.getElementById("ctl00_holderPrincipal_divFiltroProduto");
    var btnFiltroProduto = document.getElementById("btnFiltroProduto");

    if (divFiltroProduto.style.display == 'none') {
        divFiltroProduto.style.display = 'block';
        btnFiltroProduto.src = "../img/btn_aplicar_filtro_des.gif";
    }
    else {
        divFiltroProduto.style.display = 'none';
        btnFiltroProduto.src = "../img/btn_aplicar_filtro.gif";
    }
}

$(document).ready(function () {

    $("#managerLogin_UserName").focus();

    $(".barraflags").click(function () {
        $("#ctl00_hdnIdioma").val($(this).attr("rel"));

        if (formIsChanged) {
            return confirm("Você alterou os dados do formulário mas não salvou ainda!\nDeseja mudar o idioma mesmo assim?");
        }
    });

    //VERIFICA SE O FORM FOI MODIFICADO
    $("#aspnetForm *").bind("change", function () {
        formIsChanged = true;
    });

    $("#holderPrincipal1").hide();
    $("#holderPrincipal1").slideDown(500);

    //AJUSTA POSIÇAO DO MENU
    var strHeight = document.body.scrollHeight;
    $("#trBorda").prepend('<td style="height:' + strHeight + 'px;" class="borda2"><div class="borda2"><!-- //--></div></td>');

    //MONSTRA O TD DO MENU
    $(".menu").show();

    ConfiguraChecksSubForm();

    $('.btnApagarSubForm').click(function () {
        var tableGrid = $(this).prev().prev();
        var chksItemSubForm = $('.chkItemSubForm INPUT:checked', tableGrid);
        if ($(chksItemSubForm).length == 0) {
            alert('Selecione pelo menos um registro acima.');
            return false;
        }
    });


});

function ClickSubForm(Ids, objId) {
    var ctrlSubForm = $('#' + objId).parent().parent();
    var btnPostBak = $('.btnPostBak', ctrlSubForm);
    var hdnIds = $(btnPostBak).next();
    $(hdnIds).val(Ids);
    $(btnPostBak).click();
}

function ConfiguraChecksSubForm() {
    $('.chkAllSubForm INPUT').click(function () {
        var chkAll = this;
        var tableGrid = $(chkAll).parent().parent().parent().parent().parent();
        var chksItemSubForm = $('.chkItemSubForm INPUT', tableGrid);
        $(chksItemSubForm).each(function () {
            if ($(chkAll).attr('checked') == true)
                $(this).attr('checked', 'checked');
            else
                $(this).attr('checked', '');
        });
    });
}

function ValidaControlSubForm(src, args) {
    var ctrlSubForm = $(src).parent().parent();
    var blValid = ($('.chkItemSubForm INPUT', ctrlSubForm).length > 0);
    args.IsValid = blValid;
}

function RedirectEdit(url, obj) {
    $("#ctl00_hdnIdioma").val($(obj).attr("rel"));
}

function MessageBox(msg) {
    $.fancybox('<div style=\'border: 1px solid #ccc; padding: 3px;\'>' + msg + '</div>', {
        'autoDimensions': true,
        'hideOnOverlayClick': false,
        'width': 'auto',
        'height': 'auto',
        'transitionIn': 'none',
        'transitionOut': 'none'
    });
}

function UpdateUploadFile(fileName, ctrlId) {
    var lnkClicado = $('#' + ctrlId);
    var textboxFileUpload = $(lnkClicado).prev();
    $(textboxFileUpload).val(fileName);
    $.fancybox.close();
}

function InicializaEventos() {

    $('.btnNovaSecao').click(function () {

        $('#ctl00_holderPrincipal_ctl00_hdnId').val('');
        $('#ctl00_holderPrincipal_ctl00_hdnOrdem').val('');
        $('#ctl00_holderPrincipal_ctl00_txtTitulo').val('');
        $('#ctl00_holderPrincipal_ctl00_txtTituloMenu').val('');
        $('#ctl00_holderPrincipal_ctl00_cmbSecaoPai').val('');
        $('#ctl00_holderPrincipal_ctl00_cmbModelo').val('');
        $('#ctl00_holderPrincipal_ctl00_editorHtml1_txtCKEditor').val('');
        $('#ctl00_holderPrincipal_ctl00_chkExibirNoMenu').removeAttr('checked');
        $('#ctl00_holderPrincipal_ctl00_chkAtivo').removeAttr('checked');

        AminaFormConteudo();
        return false;
    });

    $('.AdicionarUploadBrowser').click(function () {

        $.fancybox({
            'type': 'iframe',
            'href': $(this).attr('href'),
            'height': 600,
            'width': 850,
            'autoScale': false,
            'scrolling': 'no',
            'centerOnScroll': true,
            'overlayOpacity': 0.5,
            'hideOnOverlayClick': false
        });

        return false;

    });

    $('.btnSubmitForm').click(function () {

        var valid = true;

        $('.reqValidators').each(function () {

            if (!this.isvalid) {
                if (valid)
                    valid = false;
            }

        });

        if (!valid) {
            showMessage('Preencha todos os campos do formulário com a sinalização vermelha.', 'Erro');
        }

    });

    //INICIO MODULO CONTEUDO
    $('.imgSinal').click(function () {

        var imgObj = $(this).attr('src');
        var imgMais = '../img/img_a.jpg';
        var imgMenos = '../img/img_b.jpg';

        if (imgObj == imgMais)
            $(this).attr('src', imgMenos);
        else
            $(this).attr('src', imgMais);

        $(this).parent().parent().nextAll().toggle();

    });

    $('.buscaHighlight').keyup(function () {
        $('.divConteudoModulo').removeHighlight();
        if ($(this).val().length >= 3) {
            $('.divConteudoModulo').highlight($(this).val());
        }
    });

    $('.divTreeView').mouseover(function () {
        $(this).removeClass('linhaEscura').addClass('linhaEscura');
    });

    $('.divTreeView').mouseout(function () {
        $(this).removeClass('linhaEscura')
    });

    $('.delItemTreeView').click(function () {
        if (confirm('Deseja realmente excluir este item?'))
            return true;
        else
            return false;
    });

    $('#btnFecharForm').click(function () {

        $("#divForm").fadeOut(300, function () {

            $("#divTree").fadeIn(300);

        });

    });

    //FIM MODULO CONTEUDO

}

function AminaFormConteudo() {
    $("#divTree").fadeOut(300, function () {

        $('#divForm').fadeIn(300);

    });
}

//FUNCAO USADA PELO CKEDITOR PARA TRANSFORMACAO DOS TEXTBOX EM CKEDITOR
function SetEditorCKEditor(idCKEditor, toolbar) {

    var ckEditor = $('#' + idCKEditor);
    ckEditor.ckeditor({

        filebrowserBrowseUrl: 'UploadBrowser.aspx',
        filebrowserUploadUrl: 'upload.aspx',
        filebrowserImageWindowWidth: '860',
        filebrowserImageWindowHeight: '670'

    });

    var editor = $('#' + idCKEditor).ckeditorGet();

    //DEIXA DISPONIVEL 2 TIPOS DE TOOLBAR A BASIC E A FULL
    editor.config.toolbar_Basic =
    [
	    ['Bold', 'Italic', '-', 'NumberedList', 'BulletedList', '-', 'Link', 'Unlink', '-', 'About']
    ];

    editor.config.toolbar_Full =
    [
	    ['Source', '-', 'Preview', '-', 'Templates'],
	    ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord', '-'],
	    ['Undo', 'Redo', '-', 'Find', 'Replace', '-', 'SelectAll', 'RemoveFormat'],
	    '/',
	    ['Bold', 'Italic', 'Underline', 'Strike', '-', 'Subscript', 'Superscript'],
	    ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', 'Blockquote', 'CreateDiv'],
	    ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'],
	    ['Link', 'Unlink', 'Anchor'],
	    ['Image', 'Flash', 'Table', 'HorizontalRule', 'SpecialChar'],
	    '/',
	    ['Styles', 'Format'],
	    ['BGColor'],
	    ['Maximize', 'ShowBlocks', '-', 'About']
    ];

    editor.config.skin = 'office2003';
    editor.config.language = 'pt-br';
    editor.config.forceEnterMode = false;
    editor.config.enterMode = CKEDITOR.ENTER_BR;
    editor.config.toolbar = toolbar;
    editor.config.resize_enabled = false;

}

//USADA PARA VALIDAR OS CKEDITOR
function validaCKEditor(src, args) {


    var chkEditor = $('#' + $('#' + src.id).attr('textBoxValidate'));
    var chkEditorValue = $(chkEditor).val();

    if (chkEditorValue == '' || chkEditorValue == '<br />\n') { //<br />\n SE FOR FIREFOX
        args.IsValid = false;
    }
    else {
        args.IsValid = true;
    }

}

function PopulaGridBrowser(objId, ids) {

    var hdn = $('#' + objId).prev();
    $(hdn).val($(hdn).val() + ids);
    $(hdn).prev().click();

}



