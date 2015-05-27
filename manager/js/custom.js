// JScript File


   // máscara de campos
    // uso: onkeydown="FormataCampo(this,event,'##/##/####')"
    function FormataCampo(Campo,teclapres,mascara, sai) {

        if (sai == "S" && consistente == "N") {
            if (obrigatorio == "N" && Campo.value.length > 0) {
                obrig_fixo = "S";
                Consist(Campo.maxLength, Campo);
                obrig_fixo = "N";
            }
            if (obrigatorio == "S" || Campo.value.length > 0) {
                if (sai == "S") {
                    if (Campo.value.length != mascara.length) {
                        alert('O campo precisa estar neste formato:\n\n       '+ mascara);
                        Campo.value = "";
                    }
                    erro = "S";
                    return false;
                }
            }
        }
        if (sai == "S" && obrigatorio == "N" && Campo.value.length > 0) {
            obrig_fixo = "S";
            Consist(Campo.maxLength, Campo);
            obrig_fixo = "N";
            if (consistente == "N") {
                alert('O campo precisa estar neste formato:\n\n       '+ mascara);
            }
        }

        strtext = Campo.value;
        tamtext = strtext.length;
        tammask = mascara.length;
        arrmask = new Array(tammask);
        for (var i = 0 ; i < tammask; i++) {
            arrmask[i] = mascara.slice(i,i+1)
        } 

		//alert(teclapres.keyCode );
        if (((((arrmask[tamtext] == "#") || (arrmask[tamtext] == "9"))) || (((arrmask[tamtext+1] != "#") || (arrmask[tamtext+1] != "9"))))) {
            if ((teclapres.keyCode >= 35 && teclapres.keyCode <= 40)||(teclapres.keyCode >= 48 && teclapres.keyCode <= 57)||(teclapres.keyCode >= 96 && teclapres.keyCode <= 105)||(teclapres.keyCode == 8)||(teclapres.keyCode == 9) ||(teclapres.keyCode == 46) ||(teclapres.keyCode == 13)||(teclapres.keyCode == 16)){
                Organiza_Casa(Campo,arrmask[tamtext],teclapres.keyCode,strtext)		
            } else {
                Detona_Event(Campo,strtext)
            }
        } else {
            if ((arrmask[tamtext] == "A")) {
                charupper = event.valueOf()
                Detona_Event(Campo,strtext)
                masktext = strtext + charupper 
                Campo.value = masktext
            }
        }
    }

    function check_date(field){
        var checkstr = "0123456789";
        var DateField = field;
        var Datevalue = "";
        var DateTemp = "";
        var seperator = "/";
        var day;
        var month;
        var year;
        var leap = 0;
        var err = 0;
        var i;
		var strDataMsg = "";

        err = 0;
        DateValue = DateField.value;
       /* Delete all chars except 0..9 */
       for (i = 0; i < DateValue.length; i++) {
          if (checkstr.indexOf(DateValue.substr(i,1)) >= 0) {
             DateTemp = DateTemp + DateValue.substr(i,1);
          }
       }
       DateValue = DateTemp;
       /* Always change date to 8 digits - string*/
       /* if year is entered as 2-digit / always assume 20xx */
       if (DateValue.length == 6) {
          DateValue = DateValue.substr(0,4) + '20' + DateValue.substr(4,2); }
       if (DateValue.length != 8) {
          err = 19;
		  strDataMsg = "A data informada é inválida.";		  
		}

       /* year is wrong if year = 0000 */
       year = DateValue.substr(4,4);
       if (year == 0) {
          err = 20;
		  strDataMsg = "O ano informado é inválido.";
       }
       /* Validation of month*/
       month = DateValue.substr(2,2);
       if ((month < 1) || (month > 12)) {
          err = 21;
		  strDataMsg = "O mês informado é inválido.";
       }

       /* Validation of day*/
       day = DateValue.substr(0,2);
       if (day < 1) {
         err = 22;
		 strDataMsg = "o dia informado é inválido.";
       }

       /* Validation leap-year / february / day */
       if ((year % 4 == 0) || (year % 100 == 0) || (year % 400 == 0)) {
          leap = 1;
       }
       if ((month == 2) && (leap == 1) && (day > 29)) {
          err = 23;
		  strDataMsg = "O mês informado possui no máximo 29 dias.";
       }
       if ((month == 2) && (leap != 1) && (day > 28)) {
          err = 24;
		  strDataMsg = "O mês informado possui no máximo 28 dias.";
       }
       /* Validation of other months */
       if ((day > 31) && ((month == "01") || (month == "03") || (month == "05") || (month == "07") || (month == "08") || (month == "10") || (month == "12"))) {
          err = 25;
		  strDataMsg = "O mês informado possui no máximo 31 dias.";
       }
       if ((day > 30) && ((month == "04") || (month == "06") || (month == "09") || (month == "11"))) {
          err = 26;
		  strDataMsg = "O mês informado possui no máximo 30 dias.";
       }
       /* if 00 ist entered, no error, deleting the entry */
       if ((day == 0) && (month == 0) && (year == 00)) {
          err = 0; day = ""; month = ""; year = ""; seperator = "";
       }
       /* if no error, write the completed date to Input-Field (e.g. 13.12.2001) */
       if (err == 0) {
          //DateField.value = day + seperator + month + seperator + year;
          return true;
       }
       else {
          alert(strDataMsg);
          DateField.select();
          DateField.focus();
          return false;
       }
    }

	function Organiza_Casa(Campo,arrpos,teclapres_key,strtext){
        if (((arrpos == "/") || (arrpos == ".") || (arrpos == ",") || (arrpos == ":") || (arrpos == " ") || (arrpos == "-")) && !(teclapres_key == 8)){
            separador = arrpos
            masktext = strtext + separador
            Campo.value = masktext
        }
    }


// Limita os caractéres do text area
function limitatext(campo,maxtamanho,e){

     var key;

     if (window.event) {
        key = window.event.keyCode;
     } else if (e) {    
        key = e.which;
     } else {
        return true;
     }   

	if (campo.value.length >= maxtamanho) {	
		campo.value = campo.value.substring(0,maxtamanho);
		//return false;
	}
	
	//alert(campo.value.length);

	if (campo.value.length>=maxtamanho && key !=8 && key !=46) { 
		return false;
	}

}


function soMoney(tammax){
	event.srcElement.maxLength=tammax;
	if (event.srcElement.value.length+1>tammax) { 
		event.KeyCode=0;
	}
	if ( (event.keyCode >= 48) && (event.keyCode <= 57 ) || (event.keyCode == 44)) {
		if (event.keyCode == 44){
			if (event.srcElement.value.indexOf(",") > -1) {			
				event.keyCode = 0
				return false
			}
		}
		return true
	} else {
		if (event.keyCode != 8){
			 event.keyCode = 0
			 return false
		 }
	}
}

function soNumeros(e) {
    var keychar;
    var key = window.event ? e.keyCode : e.which;

    keychar = String.fromCharCode(key);

    // teclas de controle
    if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27)) {
        return true;
    } else if ((("0123456789").indexOf(keychar) > -1)) {
        return true;
    } else {
        return false;
    }
}        

function soMoneyIEFF(e){
     var key;
     var keychar;

     if (window.event) {
        key = window.event.keyCode;
     } else if (e) {
        key = e.which;
     } else {
        return true;
     }

     keychar = String.fromCharCode(key);

     // teclas de controle
     if ((key==null) || (key==0) || (key==8) ||
          (key==9) || (key==13) || (key==27) || (key==44) ) {
        return true;
     } else if ((("0123456789,").indexOf(keychar) > -1)) {
        return true;    
     } else {
        return false;
     }
}

function formataValor(campo,tammax,teclapres,f) {
    var tecla = teclapres.keyCode;
    vr = f[campo].value;
    vr = vr.replace( "/", "" );
    vr = vr.replace( "/", "" );
    vr = vr.replace( ",", "" );
    vr = vr.replace( ".", "" );
    vr = vr.replace( ".", "" );
    vr = vr.replace( ".", "" );
    vr = vr.replace( ".", "" );
    tam = vr.length;

    //alert(tam +'='+tammax)/

    if (tam < tammax && tecla != 8){ tam = vr.length + 1 ; }

    if (tecla == 8 ){    tam = tam - 1 ; }
       
    if ( tecla == 8 || tecla >= 48 && tecla <= 57 || tecla >= 96 && tecla <= 105 ){
        if ( tam <= 2 ){
             f[campo].value = vr ; }
         if ( (tam > 2) && (tam <= 5) ){
             f[campo].value = vr.substr( 0, tam - 2 ) + ',' + vr.substr( tam - 2, tam ) ; }
         if ( (tam >= 6) && (tam <= 8) ){
             f[campo].value = vr.substr( 0, tam - 5 ) + '.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ; }
         if ( (tam >= 9) && (tam <= 11) ){
             f[campo].value = vr.substr( 0, tam - 8 ) + '.' + vr.substr( tam - 8, 3 ) + '.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ; }
         if ( (tam >= 12) && (tam <= 14) ){
             f[campo].value = vr.substr( 0, tam - 11 ) + '.' + vr.substr( tam - 11, 3 ) + '.' + vr.substr( tam - 8, 3 ) + '.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ; }
         if ( (tam >= 15) && (tam <= 17) ){
             f[campo].value = vr.substr( 0, tam - 14 ) + '.' + vr.substr( tam - 14, 3 ) + '.' + vr.substr( tam - 11, 3 ) + '.' + vr.substr( tam - 8, 3 ) + '.' + vr.substr( tam - 5, 3 ) + ',' + vr.substr( tam - 2, tam ) ;}
    }       
   
}