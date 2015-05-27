/// <reference name="MicrosoftAjax.js"/>
function pageLoad() {
    var manager = Sys.WebForms.PageRequestManager.getInstance();
    manager.add_beginRequest(OnBeginRequest);
    manager.add_endRequest(OnEndRequest);

    InicializaEventos();    
}

var _setTimeOut;

function OnBeginRequest(sender, args) {
    _setTimeOut = true;
    window.setTimeout("BlockPage();", 150);
}

function BlockPage() {
    if (_setTimeOut)
        $.blockUI({ message: '<div style=\'padding: 10px;border: 5px solid #FF6500;\'><div style=\'font-weight: bold;\'>Aguarde...</div></div>', overlayCSS: { backgroundColor: '#fff' }, css: { border: 'none', left: ($(window).width() - 150) / 2 + 'px', width: '150px'} });
}

function OnEndRequest(sender, args) {
    _setTimeOut = false;

    if (!showMsg) {
        $.unblockUI();

        try {
            imagePreview();
        }
        catch (e) {
            //
        }
    }


}
