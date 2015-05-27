/// <reference path="~/js/jquery-1.2.6-intellisense.js" />
$(document).ready(function () {

	//MOSTRA CALENDARIO 
	$(".dateField").datepicker($.extend({}, $.datepicker.regional["pt-BR"], { showStatus: true, showOn: "button", buttonImage: "../img/calendar.gif", buttonImageOnly: true }));

	//CONFIG MASCARAS
	$(':text.cpf').setMask('cpf');
	$(':text.cnpj').setMask('cnpj');
	$(':text.rg').setMask({ mask: '9999999999' });
	$(':text.cep').setMask({ mask: '99999-999' });
	$(':text.data').setMask({ mask: '39/19/9999' });
	$(':text.hora').setMask({ mask: '29:69' });
	$(':text.ddd').setMask({ mask: '99' });
	$(':text.telefone').setMask({ mask: '99999999' });
	$(':text.numero').setMask({ mask: '9', type: 'repeat' });
	$(':text.moeda').setMask({ mask: '99,999.999.999.999', type: 'reverse', defaultValue: '000' });

	$("#ctl00_holderPrincipal_showFilter").click(function () {
		
		$("#ctl00_holderPrincipal_IsShwoFilterBox").val("S");
		$("#ctl00_holderPrincipal_showFilter").attr("src", "../img/btn_aplicar_filtro_des.gif");
		$("#ctl00_holderPrincipal_tableFilter").slideDown(1000);
		$("#ctl00_closeFilter").attr("style", "cursor:pointer;");

		return false;

	});

	$("#closeFilter").click(function () {

		$("#ctl00_holderPrincipal_IsShwoFilterBox").val("N");
		$("#ctl00_holderPrincipal_showFilter").attr("src", "../img/btn_aplicar_filtro.gif");
		$("#ctl00_holderPrincipal_tableFilter").slideUp(1000);

	});
});

function showMessage(message, typeMessage) {

    $(document).ready(function() {

        //        showMsg = true;
        //        //$.blockUI({ message: '<div style=\'padding: 10px;border: 5px solid #FF6500;\'><div style=\'font-weight: bold;\'>' + msg + '</div></div>', overlayCSS: { backgroundColor: '#fff' }, css: { border: 'none', left: ($(window).width() - 380) / 2 + 'px', width: '380px'} });
        var conteudoHtml = new String();

        if (typeMessage == null)
            typeMessage = 'Sucesso';

        if (typeMessage == 'Sucesso') {
            conteudoHtml += '<div class=\"showMessageBox\">';
            conteudoHtml += '<div class=\"icon\">';
            conteudoHtml += '<img src=\"../img/iconSucesso.png\">';
            conteudoHtml += '</div>';
            conteudoHtml += '<div>';
            conteudoHtml += '<table>';
            conteudoHtml += '<tr><td>';
            conteudoHtml += message;
            conteudoHtml += '</td></tr>';
            conteudoHtml += '</table>';
            conteudoHtml += '</div>';
            conteudoHtml += '</div>';
        }
        else if (typeMessage == 'Erro') {
            conteudoHtml += '<div class=\"showMessageBox\">';
            conteudoHtml += '<div class=\"icon\">';
            conteudoHtml += '<img src=\"../img/iconErro.png\">';
            conteudoHtml += '</div>';
            conteudoHtml += '<div>';
            conteudoHtml += '<table>';
            conteudoHtml += '<tr><td>';
            conteudoHtml += message;
            conteudoHtml += '</td></tr>';
            conteudoHtml += '</table>';
            conteudoHtml += '</div>';
            conteudoHtml += '</div>';
        }
        else if (typeMessage == 'None') {
            conteudoHtml += '<div class=\"showMessageBoxNone\">';
            conteudoHtml += '<div class=\"icon\">';
            conteudoHtml += '<img src=\"../img/iconErro.png\">';
            conteudoHtml += '</div>';
            conteudoHtml += '<div>';
            conteudoHtml += '<table>';
            conteudoHtml += '<tr><td>';
            conteudoHtml += message;
            conteudoHtml += '</td></tr>';
            conteudoHtml += '</table>';
            conteudoHtml += '</div>';
            conteudoHtml += '</div>';
        }

        $.fancybox({
            'content': conteudoHtml,
            'autoScale': false,
            'hideOnOverlayClick': false
        });

        //        window.setTimeout(function() {
        //            showMsg = false;
        //            $.unblockUI();
        //        }, 3000);

    });

}


