/// <reference path="~/js/jquery-1.2.6-intellisense.js" />

if (net == undefined) var net = new Object();
//
net.Upload = function(id) {
    this.objUpload = null;
    this.maxSize = 1;
    this.target = "./";
    this.ext = null;
    this.action = "upload.aspx";
    //
    var info = net.Upload.getRandObj();
    this.src = info.target;
    this.id = id == undefined ? info.id : id;
    //
    this.objUpload = (navigator.appName.indexOf("Microsoft") != -1) ? $("#" + this.id)[0] : document[this.id];
    //
    //if(isOpera()) alert("This browser don't suport Upload feature. Use IE or FF. Thanks");
}
net.Upload.getRandObj = function() {
    var n = Math.round(Math.random() * (new Date()).getTime());
    return { target: "?r=" + n, id: "objUpload_" + n };
}
net.Upload.createObject = function(src, id, a) {
    var info = net.Upload.getRandObj();
    var a = (a == undefined || a < 1) ? 1 : a;
    this.id = (id == undefined) ? info.id : id;
    this.src = (src == undefined) ? ("uploadapi.swf" + info.target) : (src + info.target);
    this.toString = function() {
        var flashTag;

        if (navigator.appName.indexOf("Microsoft") != -1) {
            flashTag = '<div style="position: relative; left: 0; top:0; width: ' + a + '; height: ' + a + ';"><object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase=""http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" width="10%" height="10%" id="' + this.id + '">';
        }
        else {
            flashTag = '<div style="position: relative; left: 0; top:0; width: ' + a + 'px; height: ' + a + 'px;"><object classid="clsid:d27cdb6e-ae6d-11cf-96b8-444553540000" codebase=""http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=8,0,0,0" width="10%" height="10%" id="' + this.id + '">';
        }

        flashTag += '  <param name="movie" value="' + this.src + '" />' +
		'  <param name="quality" value="high" />' +
		'  <param name="allowScriptAccess" VALUE="sameDomain">' +
		'  <embed src="' + this.src + '" quality="high" allowScriptAccess="sameDomain" swLiveConnect="true" pluginspage="http://www.macromedia.com/go/getflashplayer" type="application/x-shockwave-flash" width="100%" height="100%" name="' + this.id + '"></embed>' +
		'</object></div>';
        return flashTag;
    };
    document.write(this.toString());
    eval("window." + this.id + "= document.forms[0]." + this.id);

    return this.id;
}

net.Upload.prototype = {

    addListener: function(obj) {
        this.objJS = obj;
    },

    browse: function() {
        this.init();
        //
        this.objUpload.browse();
    },

    upload: function() {
        this.objUpload.upload();
    },

    init: function() {
        this.objUpload.config(this.objJS, this.ext, this.target, this.action, this.maxSize);
    }

}



function UploadField(id, objUpload) {
    this.id = id;
    this.objUpload = objUpload;
    this.deleteButtonUrl;
    this.changeButtonUrl;
    this.findButtonUrl;
    this.gerenateUniqueName;
    this.imageInfo;
    this.progressBar = $("#" + this.id + "_progress")[0];
}

UploadField.prototype = {

    onSelect: function(fileName, fileSize, fileType) {
        if ($("#" + this.id + "_fileName").val() == "") {
            this.withoutFile();
        }
        this.objUpload.upload();
    },

    onStart: function() {
        //$("#" + this.id + "_progress").show();
        //$("#" + this.id + "_progress").width("0%");
        $.blockUI({ message: '<div style=\'padding: 10px;\'><div style=\'font-weight: bold;\'>Aguarde...<span id=\'percentSpan\'></span></div><div style=\'width: 100%;text-align: left;border: 1px solid #cccccc;\'><div id=\'_progress\' style=\'background-image: url(../img/bgr_gray.jpg);width: 100%;display: block;\'>&nbsp;</div></div></div>' });
        $("#_progress").show();
        $("#_progress").width("0%");
        $("#percentSpan").text("0%");
    },

    onProgress: function(f, bl, bt) {
        var p = Math.ceil((bl / bt) * 100);
        //alert(p);
        //$("#" + this.id + "_progress").width(p + "%");
        $("#_progress").width(p + "%");
        $("#percentSpan").text(p + "%");
        //alert($("#_progress"));
        //$("#" + this.id + "_progress").html(p + "%");
    },

    onError: function(f, e) {
        if (e == 1500) {
            alert("O tamanho do arquivo selecionado excede o limite de " + this.objUpload.maxSize + "Kb");
        }
        //alert("Errror="+e);
    },

    onComplete: function(f, e) {

        if ($("#" + this.id + "_fileName").val() != '') {
            this.deleteFile();
        }

        var _temp = this;

        $.ajax({
            type: "POST",
            url: "../File.aspx/ImageInfo",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function(e) {
                alert(e.toString());
            },
            success: function(r) {
                _temp.imageInfo = r;
                _temp.withFile();
                $("#" + _temp.id + "_fileName").val(_temp.imageInfo.Name);
                $("#" + _temp.id + "_progress").slideUp(1500);
                $().ajaxStop($.unblockUI);
            }
        });
    },

    withoutFile: function() {
        $("#" + this.id + "_fileName").val("");
        container = this.id + "_container";
        table = $("#" + this.id + "_table")[0];

        //muda class da DIV
        $("#" + container).addClass("boxcinzasem");
        table.tBodies[0].deleteRow(0);
        table.tBodies[0].insertRow(0);

        //coluna 1
        cell1 = table.tBodies[0].rows[0].insertCell(0);
        cell1.innerHTML = "<a href=\"javascript:void(0);\" onclick=\"upLoad" + this.id + ".browse();\"><img src=\"" + this.findButtonUrl + "\" /></a>";

        //coluna 2
        //cell2 = table.tBodies[0].rows[0].insertCell(1);
        //cell2.width = "80%";
        //cell2.innerHTML = "<div id=\"" + this.id + "_progress\" style=\"width: 0%; display: none; height: 30%; background-color: #FF6500;text-align:center\"></div>";
    },

    withFile: function() {
        container = this.id + "_container";
        table = $("#" + this.id + "_table")[0];
        //muda class da DIV
        $("#" + container).addClass("boxcinzacom");
        table.tBodies[0].deleteRow(0);
        table.tBodies[0].insertRow(0);

        //insert column for image preview     
        if (this.isImage()) {
            imgPreview = this.imagePreview();
            $(imgPreview).hide();
            $("#" + imgPreview).addClass("mr12");
            $(imgPreview).fadeIn(1500);
            table.tBodies[0].rows[0].insertCell(0).appendChild(imgPreview);
        } else {
            table.tBodies[0].rows[0].insertCell(0).innerHTML = "&nbsp;";
        }

        cell1 = table.tBodies[0].rows[0].insertCell(1);
        cell1.innerHTML = '<a href="javascript:void(0);"  onclick="upLoad' + this.id + '.browse();"><img src="' + this.changeButtonUrl + '" border="0" alt="Substituir Imagem" id="' + this.id + '_changeButton" /></a><br /><br /><a href="javascript:upField' + this.id + '.deleteFile();"><img src="' + this.deleteButtonUrl + '"  border="0" alt="Excluir Imagem" "' + this.id + '_deleteButton" /></a><br />';


        cell2 = table.tBodies[0].rows[0].insertCell(2);
        cell3 = table.tBodies[0].rows[0].insertCell(3);
        cell4 = table.tBodies[0].rows[0].insertCell(4);
        cellImageInfo = table.tBodies[0].rows[0].insertCell(5);

        cell2.setAttribute("width", "20");
        cell2.innerHTML = "<div style=\"width:20px;\"><!--  --></div>";

        cell3.setAttribute("width", "1");
        cell3.style.backgroundColor = "#E2E2E2";
        cell3.innerHTML = "<div style=\"width:1px;\"><!--  --></div>";

        cell4.setAttribute("width", "20");
        cell4.innerHTML = "<div style=\"width:20px;\"><!--  --></div>";

        cellImageInfo.width = "100%";
        strFileInfo = "Nome do arquivo: <b>" + this.imageInfo.Name + "</b><br />";
        strFileInfo += "Formato do arquivo: <b>" + this.imageInfo.Format + "</b>";
        cellImageInfo.innerHTML = strFileInfo;

    },

    deleteFile: function() {

        var _temp = this;

        $.ajax({
            type: "POST",
            url: "../File.aspx/DeleteFile",
            data: "{'path':'" + _temp.imageInfo.Url + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            error: function(e) {
                alert(e.toString());
            },
            success: function(msg) {
                _temp.withoutFile();
            }
        });
    },

    imagePreview: function() {
        img = document.createElement("img");
        img.setAttribute("src", this.imageInfo.Url);
        img.setAttribute("width", "90");
        img.setAttribute("height", "70");
        img.style.borderStyle = "solid";
        img.style.borderColor = "#D8D8D8";
        img.style.borderWidth = 1;

        return img;
    },

    isImage: function() {
        var imageExtension = new Array("jpg", "gif", "png");
        var isImg = false;

        for (i = 0; i < imageExtension.length; i++) {
            if (this.imageInfo.Name.indexOf(imageExtension[i]) != -1) {
                isImg = true;
                break;
            }
        }

        //retorno da imagem
        return isImg;
    }
}
