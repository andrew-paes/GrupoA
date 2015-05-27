function ajaxManager(strUrl, jSonData, errorFunction, successFunction) {

    $.ajax({
        type: "POST",
        url: strUrl,
        data: jSonData,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        async: true,
        error: errorFunction,
        success: successFunction
    });

}

function GetGridContent(IDs, ctrlId) {

    //            var reg = new RegExp("[|]+", "g");
    //            var splitIDs = IDs.split(reg);

    //            for (var i = 0; i < splitIDs.length; i++) {
    //                alert(splitIDs[i]);
    //            }

    ajaxManager(
        "edit.aspx/CarregaConteudoGrids",
        "{'controlId':'" + ctrlId + "','strIDs':'" + IDs + "'}",
        function(e) { alert("Ocorreu um erro na requisição ajax. \r(CarregaConteudoGrids)"); },
        function(retorno) {
            $('#' + ctrlId).html(retorno);
            ConfiguraChecksSubForm();
        }
    );

}