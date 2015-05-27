// cria o obejto ag2 caso o mesmo não exista
if (typeof ag2 == "undefined") var ag2 = new Object();

/**
* Construtor.
*/
ag2.UploadApi = function() {
    // identificador do componente, gerado automaticamente
    this.id = "";
    // arquivo responsável pelo upload
    this.action = "../upload.aspx";
    // caminho onde deverá ser salvo/carregado os arquivos
    this.path = "";
    //Caminho base para o diretorio de upload
    this.baseUrlUpload = "";
    // nome do arquivo, para quando já existe um elemento carregado
    this.fileName = "";
    // tamanho máximo do arquiv em Kb
    this.maxSize = null;
    // largura maxima do arquivo em pixels, quando imagem
    this.maxWidth = null;
    // altura máxima do arquivo, quando imagem
    this.maxHeight = null;
    // filtros para caixa de dialogo
    this.filters = new Array();
    // armazena a instancia para futura consulta	
    ag2.UploadApi.instances.push(this);
}


// Armazena todas as instâncias de objetos
// Utilizado para implementar a busca de objetos a partir do flash sem a necessidade do uso de callBack
// pois para isso seriam necessárias personalizar implementações de segurança para cada domínio onde fosse utilizado
ag2.UploadApi.instances = new Array();



/**
* Gera de forma automática um identificador para o componente
*/
ag2.UploadApi.generateComponenteId = function() {
    return Math.round(Math.random() * (new Date()).getTime());
}


/**
* Recupera a instância JS de um dado componente
* @param componentId Identificador do componente
*/
ag2.UploadApi.getInstance = function(componentId) {
    var instance;
    for (var i = 0; i < ag2.UploadApi.instances.length; i++) {
        if (ag2.UploadApi.instances[i].id == componentId) {
            instance = ag2.UploadApi.instances[i];
            break;
        }
    }
    return instance;
}

ag2.UploadApi.prototype =
{
    /**
    * Seta filtros de arquivos para a caixa de dialogo de seleção
    * @param labbel String que vai aparecer na lista de arquivos disponíveis. Ex: "Imagens (*.jpg)"
    * @param extensions String com as extensões de arquivos permitidas, se mais de uma, devem ser divididas com (;). Ex: *.jpg; *.gif
    */
    addFilter: function(label, extensions) {
        var element = label + " | " + extensions;
        this.filters.push(element);
    },
    /**
    * Método chamado pelo flash quando o usuário clica no botão de excluir arquivo.
    */
    deleteFile: function() {

        var arq = this.path + "/" + document.getElementById(this.id + "_fileName").value;

        $.ajax({
            type: "POST",
            url: "../File.aspx/DeleteFile",
            data: "{'path':'" + arq + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(msg) {
                //
            }
        });

    },
    /**
    * Método chamado pelo flash quando o úsuario clica no botão para procurar o arquivo
    */
    onBrowse: function() {
        this.objFlash = (navigator.appName.indexOf("Microsoft") != -1) ? $(this.id) : document[this.id];
        this.objFlash.initFlashAPI(ag2.UploadApiUtil.getInstance(this.id));

        //alert("onBrowse: " + this.id);

    },
    /**
    * Método acionado quando o arquivo é selecionado e a caixa de diálogo é fechada
    * @param fileName Nome do arquivo
    */
    onSelect: function(fileName) {
        // vai fazer upload
        // apagar anterior?
        this.fileName = fileName;
        //alert("Arquivo selecionado: " + fileName + "\nComponente: " + this.id);
    },
    /**
    * Método acionado quando o upload é iniciado
    */
    onStart: function() {
        //alert("Iniciou carregamento: " + this.fileName + "\nComponente: " + this.id);
    },
    /**
    * Método acionado pelo flash durante o progresso do carregamento
    * @param totalBytes Tamanho do arquivo em bytes
    * @param totalLoaded Total de bytes carregados
    */
    onProgress: function(totalBytes, totalLoaded) {
        //alert("Progresso: " + this.fileName + "\nComponente: " + this.id + "\nTamanho: " + totalBytes + "\nCarregado: " + totalLoaded);
    },
    /**
    * Método acionado quando existe algum tipo de falha no upload informado pelo flash, 
    * sendo que os erros previstos são:	* -100 | Security Error,  -200 | Io Error,  -300 | Size Error.
    * @param errorId Código do error
    */
    onError: function(errorId) {

        if (errorId == "-300") {
            alert("Tamanho do arquivo excedeu o limite.");
        }
        else {
            alert("\nComponente: " + this.id + "\nErro: " + errorId);
        }
    },
    /**
    * Método acionado pelo flash quando o carregamento é finalizado
    * @param componentId Identificador do componente
    * @param fileName Nome do arquivo
    */
    onComplete: function() {
        var _temp = this;

        $.ajax({
            type: "POST",
            url: "../File.aspx/ImageInfo",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(r) {
                var obj;
                if (r.d != undefined)
                    obj = r.d;
                else
                    obj = r;

                if (obj.Error != "") {
                    alert(obj.Error);
                }

                document.getElementById(_temp.id + "_fileName").value = obj.Name;
            }
        });

    },
    /**
    * Método acionado pelo flash quando o carregamento é finalizado e os dados estão prontos para uso
    * @param componentId Identificador do componente
    * @param fileName Nome do arquivo
    */
    onCompleteData: function() {
        //alert("Arquivo ccompleto: " + this.fileName + "\nComponente: " + this.componentId);
    },
    /**
    * Método utilizado para escrever o flash no html. Utiliza a biblioteca SWF Object como auxiliar
    * @param div Id da div onde o objeto será escrito
    */
    write: function(div) {
        var ag2Upload = new SWFObject("../swf/ag2_uploadapi.swf", this.id, "550", "200", "9");
        ag2Upload.addParam("wmode", "transparent");
        ag2Upload.addParam("quality", "high");
        // tamanho máximo do arquivo
        ag2Upload.addVariable("componentId", this.id);
        // action - path da pagina responsavel pelo upload
        ag2Upload.addVariable("action", this.action);
        // tamanho máximo do arquivo
        ag2Upload.addVariable("fileName", this.fileName);
        // diretório onde o arquivo será/foi salvo
        ag2Upload.addVariable("path", this.path);
        //Caminho base para o diretorio de upload
        ag2Upload.addVariable("baseUrlUpload", this.baseUrlUpload);
        // tamanho máximo do arquivo em kb
        ag2Upload.addVariable("maxSize", this.maxSize);
        // largura máxima
        ag2Upload.addVariable("maxWidth", this.maxWidth);
        // altura máxima
        ag2Upload.addVariable("maxHeight", this.maxHeight);
        // a descrição deve ser separada das extenções por pipe ( | )
        // as extenções possiveis devem ser separadas por ;
        for (var i = 0; i < this.filters.length; i++)
            ag2Upload.addVariable("filter" + i, this.filters[i]);
        // escreve o obejto flash
        ag2Upload.write(div);
    }
}