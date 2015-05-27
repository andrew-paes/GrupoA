function tab(elementID)
{
    try
    {
		if (((event.srcElement.type == "text" && event.srcElement.readOnly == false && event.srcElement.style.display != 'none') || ((event.srcElement.type == "select-one" || event.srcElement.type == "select-multiple") && event.srcElement.disabled == false && event.srcElement.style.display != 'none') || event.srcElement.type == "textarea" || event.srcElement.type == "file" || event.srcElement.type == "password" || event.srcElement.type == "checkbox") && (event.srcElement.id.indexOf('txt') > -1 || event.srcElement.id.indexOf('ddl') > -1 || event.srcElement.id.indexOf('lb') > -1 || event.srcElement.id.indexOf('chk') > -1)) 
		{		
			var keyboardKey = window.event.which;
			if(keyboardKey == null) keyboardKey = window.event.keyCode;		
			if(keyboardKey == 13)			
			{		
				event.returnValue = false;
				event.keyCode = 0;
		    
				var firstField = null;
				var currentField = null;
				var nextField = null;
				
				for (i = 0 ; i <= document.forms[0].elements.length - 1; i++) 
				{					
					if (((document.forms[0].elements[i].type == "text" && document.forms[0].elements[i].readOnly == false && document.forms[0].elements[i].style.display != 'none') || ((document.forms[0].elements[i].type == "select-one" || document.forms[0].elements[i].type == "select-multiple") && document.forms[0].elements[i].disabled == false && document.forms[0].elements[i].style.display != 'none') || document.forms[0].elements[i].type == "submit" || document.forms[0].elements[i].type == "textarea" || document.forms[0].elements[i].type == "file" || document.forms[0].elements[i].type == "password" || document.forms[0].elements[i].type == "checkbox") && (document.forms[0].elements[i].id.indexOf('btn') > -1 || document.forms[0].elements[i].id.indexOf('txt') > -1 || document.forms[0].elements[i].id.indexOf('ddl') > -1 || document.forms[0].elements[i].id.indexOf('lb') > -1 || document.forms[0].elements[i].id.indexOf('chk') > -1)) 
					{
						if (currentField != null) 
						{
							nextField = document.forms[0].elements[i];
							break;
						}
						
						if (event.srcElement.name == document.forms[0].elements[i].name) 
						{
							currentField = document.forms[0].elements[i];
						}
					}
				}
				        
				if (nextField != null) 
				{					
					nextField.focus();
					nextField.select();								
				}				
				else
				{									
					document.getElementById(elementID).focus();					
				}				
			}
		}
	}
	catch(e){}	
}

function filter(value,filter,type,complement)
{										
	try
	{
		var characters = '';			
		var returnValue = '';

		switch(filter)
		{				
			case 0:
				characters = complement + '0123456789';
				break;
			case 1:
				characters = complement + 'ABCDEFGHIJKLMNOPQRSTUVWXYZ';
				break;
			case 2:
				characters = complement + '"' + "'";
				break;										
			default:
				characters = complement + '0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ';
				break;					
		}
		
		if (type == 0)
		{
			for (var i = 0; i < value.length; i++) 
			{ 																								
				if (characters.indexOf(value.toUpperCase().substring(i,i+1)) > -1)
				{
					returnValue = returnValue + value.substring(i,i+1);
				}												
			}
		}
		else
		{
			for (var i = 0; i < value.length; i++) 
			{ 																								
				if (characters.indexOf(value.toUpperCase().substring(i,i+1)) == -1)
				{
					returnValue = returnValue + value.substring(i,i+1);
				}												
			}
		}

		return returnValue;
	}
	catch(e){}
}

function endereco()
{
	try
	{
		var field = event.srcElement;

		if (window.event.type == 'keypress')
		{
			
			if (field.value.indexOf(',') == -1)
			{
			   var keyboardKey = window.event.which;
			   if(keyboardKey == null) keyboardKey = window.event.keyCode;
			   if((keyboardKey != 34) && (keyboardKey != 39) && (keyboardKey != 46) && (keyboardKey != 58) && (keyboardKey != 59) && (keyboardKey != 43) && (keyboardKey != 45) && (keyboardKey != 47) && (keyboardKey != 92) && (keyboardKey != 60) && (keyboardKey != 61) && (keyboardKey != 62) && (keyboardKey != 63) && (keyboardKey != 64) && (keyboardKey != 33) && (keyboardKey != 34) && (keyboardKey != 35) && (keyboardKey != 36) && (keyboardKey != 37) && (keyboardKey != 38) && (keyboardKey != 40) && (keyboardKey != 41) && (keyboardKey != 42) && (keyboardKey != 247))  
			   {  
			     event.returnValue = true;
			   }
			   else 
			   {
			     event.returnValue = false;
			   } 
			}
			else
			{
			   var keyboardKey = window.event.which;
			   if(keyboardKey == null) keyboardKey = window.event.keyCode;
			   if((keyboardKey > 47 && keyboardKey < 58) || (keyboardKey == 13) || (keyboardKey == 32)) 
			   {
			      event.returnValue = true;
			   }  		
			   else 
			   {
			      event.returnValue = false;
			   }
			}
		}
		else
		{
			  if (field.value.indexOf(',') == -1)
			  {
                 field.value = filter(field.value,2,1,'/.:\"!@#$%¨&*()-=+_[]<>;?*|');			     
			  }
			  else
			  {
			     field.value = filter(field.value,0,0,'ABCDEFGHIJKLMNOPQRSTUVWXYZÇÁÀÃÂÄÉÈÊËÍÌÎÏÓÒÕÔÖÚÙÛÜ, ');
			  }		
		}
	}
	catch(e){}
}

function aspa()
{
	try
	{
		var field = event.srcElement;

		if (window.event.type == 'keypress')
		{
			var keyboardKey = window.event.which;
			if(keyboardKey == null) keyboardKey = window.event.keyCode;
			if(keyboardKey != 34 && keyboardKey != 39) event.returnValue = true;
			else event.returnValue = false;
		}
		else
		{
			field.value = filter(field.value,2,1,'');
		}
	}
	catch(e){}
}

function numerico()
{
	try
	{	
		var field = event.srcElement;
		
		if (window.event.type == 'keypress')
		{
			var keyboardKey = window.event.which;
			if(keyboardKey == null) keyboardKey = window.event.keyCode;
			if((keyboardKey > 47 && keyboardKey < 58) || (keyboardKey == 13)) event.returnValue = true;		
			else event.returnValue = false;
		}
		else
		{
			field.value = filter(field.value,0,0,'');						
		}				
	}
	catch(e){}									
}

function percentual()
{
	try
	{	
		var field = event.srcElement;
		
		if (window.event.type == 'keypress')
		{
			var keyboardKey = window.event.which;
			if(keyboardKey == null) keyboardKey = window.event.keyCode;
			if((keyboardKey > 47 && keyboardKey < 58) || (keyboardKey == 44 && field.value.indexOf(',') == -1) || (keyboardKey == 13)) event.returnValue = true;				
			else event.returnValue = false;
		}
		else
		{						
			if (field.value.length > 0)
			{
				var complement = '';
				
				if (field.value.indexOf(',') == -1)
				{
					complement = ',00';
				}
				else 
				{	
					if (field.value.length-1 == field.value.indexOf(','))
					{
						complement = '00';
					}
					else if ((field.value.length - field.value.indexOf(',')) == 1 || (field.value.length - field.value.indexOf(',')) == 2)
					{
						complement = '0';	
					}							
				}
				
				var value = filter(field.value + complement,0,0,','); 
				var valueLength = value.length;			
				var returnValue = '';
						
				var valueDec = value.substring(value.indexOf(',')+1,value.indexOf(',')+3);				
				value = parseFloat(value.substring(0,value.indexOf(','))).toString();	
				valueLength = value.length;
				
				field.value = value + ',' + valueDec;				
			}														
		}				
	}
	catch(e){}										
}

function dolar(digitsAfterDecimal)
{
	try
	{	
		var field = event.srcElement;
		var returnValue = '';
		
		if (digitsAfterDecimal == null)
		{
			digitsAfterDecimal = 2;
		}
		
		if (window.event.type == 'keypress')
		{
			var keyboardKey = window.event.which;
			if(keyboardKey == null) keyboardKey = window.event.keyCode;
			if((keyboardKey > 47 && keyboardKey < 58) || (keyboardKey == 44 && field.value.indexOf('.') == -1) || (keyboardKey == 46) || (keyboardKey == 13)) event.returnValue = true;				
			else event.returnValue = false;
		}
		else
		{						
			if (field.value.length > 0)
			{
				if (field.value.indexOf('.') == 0)
				{
					field.value = '0' + field.value;
				}
				else if (field.value.indexOf('.') == -1)
				{
					field.value += '.';
				}
				
				for (var i = 0; i < digitsAfterDecimal; i++) 
				{ 																								
					field.value += '0';												
				}
								
				var value = filter(field.value,0,0,'.');
				var valueLength = value.length;													
				var valueDec = value.substring(value.indexOf('.') + 1,value.indexOf('.') + 1 + digitsAfterDecimal);				
				value = parseFloat(value.substring(0,value.indexOf('.'))).toString();	
				valueLength = value.length;
				
				if (valueLength > 2)
				{
					for (var i = 0;i <= valueLength;i++)
					{																
						if (((i != 0 ) && (i == valueLength % 3)) || ((i != valueLength) && (i > valueLength % 3) && (i % 3 == valueLength % 3)))
						{							
							returnValue += ',' + value.substring(i,i+1);							
						}												
						else
						{
							returnValue += value.substring(i,i+1);	
						}
					}
										
					if (returnValue == 'NaN')
					{
						field.value = '';
					}
					else
					{
						field.value = returnValue + '.' + valueDec;
					}
				}
				else
				{
					field.value = value + '.' + valueDec;
				}
			}														
		}				
	}
	catch(e){}									
}

function real(digitsAfterDecimal)
{
	try
	{	
		var field = event.srcElement;
		var returnValue = '';
		
		if (digitsAfterDecimal == null)
		{
			digitsAfterDecimal = 2;
		}
		
		if (window.event.type == 'keypress')
		{
			var keyboardKey = window.event.which;
			if(keyboardKey == null) keyboardKey = window.event.keyCode;
			if((keyboardKey > 47 && keyboardKey < 58) || (keyboardKey == 44 && field.value.indexOf(',') == -1) || (keyboardKey == 46) || (keyboardKey == 13)) event.returnValue = true;				
			else event.returnValue = false;
		}
		else
		{						
			if (field.value.length > 0)
			{
				if (field.value.indexOf(',') == 0)
				{
					field.value = '0' + field.value;
				}
				else if (field.value.indexOf(',') == -1)
				{
					field.value += ',';
				}
				
				for (var i = 0; i < digitsAfterDecimal; i++) 
				{ 																								
					field.value += '0';												
				}
								
				var value = filter(field.value,0,0,',');
				var valueLength = value.length;													
				var valueDec = value.substring(value.indexOf(',') + 1,value.indexOf(',') + 1 + digitsAfterDecimal);				
				value = parseFloat(value.substring(0,value.indexOf(','))).toString();	
				valueLength = value.length;
				
				if (valueLength > 2)
				{
					for (var i = 0;i <= valueLength;i++)
					{																
						if (((i != 0 ) && (i == valueLength % 3)) || ((i != valueLength) && (i > valueLength % 3) && (i % 3 == valueLength % 3)))
						{							
							returnValue += '.' + value.substring(i,i+1);							
						}												
						else
						{
							returnValue += value.substring(i,i+1);	
						}
					}
					
					if (returnValue == 'NaN')
					{
						field.value = '';
					}
					else
					{
						field.value = returnValue + ',' + valueDec;
					}
				}
				else
				{
					field.value = value + ',' + valueDec;
				}
			}														
		}				
	}
	catch(e){}									
}

function hora()
{
	try
	{	
		var field = event.srcElement;
		
		if (window.event.type == 'keypress')
		{
			var keyboardKey = window.event.which;
			if(keyboardKey == null) keyboardKey = window.event.keyCode;
			if((keyboardKey > 47 && keyboardKey < 58) || (keyboardKey == 13)) event.returnValue = true;		
			else event.returnValue = false;
			
			if (keyboardKey != 8)
			{
				if (field.value.length == 2) field.value += ':';
				if (field.value.length >= 5) checkHora(field);				
			}
		}
		else
		{
			var value = filter(field.value,0,0,'');
			var returnValue = '';
			
			for (var i = 0; i < value.length; i++) 
			{								
				if (i == 2)
				{
					returnValue += ':' + value.substring(i,i+1);
				}
				else
				{
					returnValue += value.substring(i,i+1);	
				}														
			}
			
			field.value = returnValue;	
			checkHora(field);		
		}				
	}
	catch(e){}									
}

function checkHora(field) 
{		
	try
	{		
		var value = filter(field.value,0,0,'');
		
		if (value.length > 0)
		{		
			if (value.length != 4 || (value.length == 4 && parseFloat(field.value.substring(0,2)) >= 24) || (value.length == 4 && parseFloat(field.value.substring(0,2)) == 24 && parseFloat(field.value.substring(3,5)) > 0) || (value.length == 4 && parseFloat(field.value.substring(3,5)) > 59))
			{			
				field.value = '';							
				alert('Horário inválido.');
				field.focus();				
			}		
		}
	}
	catch(e){}
}

function data(culture)
{
	try
	{
		var field = event.srcElement;
		
		if (window.event.type == 'keypress')
		{
			var keyboardKey = window.event.which;
			
			if(keyboardKey == null) keyboardKey = window.event.keyCode;
			if((keyboardKey > 47 && keyboardKey < 58) || (keyboardKey == 13)) event.returnValue = true;
			else event.returnValue = false;
			
			if (keyboardKey != 8)
			{
				if (field.value.length == 2) field.value += '/';
				if (field.value.length == 5) field.value += '/';				
				if (field.value.length >= 10) checkData(field);
			}
		}
		else
		{			
			var value = filter(field.value,0,0,'');
			var returnValue = '';
			
			for (var i = 0; i < value.length; i++) 
			{				
				if (value.length == 6 && i == 4)
				{
					returnValue = returnValue + '/20' + value.substring(i,i+1);	
				}
				else
				{
					switch(i)
					{
						case 2:
							returnValue += '/' + value.substring(i,i+1);				
							break;
						case 4:
							returnValue += '/' + value.substring(i,i+1);				
							break;		
						default:
							returnValue += value.substring(i,i+1);				
							break;					
					}
				}								
			}
			
			field.value = returnValue;						
			checkData(field,culture);			
		}					
	}
	catch(e){}				
}

function checkData(field,culture) 
{		
	try
	{
		var date = field.value;
		var day = '';
		var month = '';
		var year = '';
		var sep1 = '';
		var sep2 = '';
		var valid = 1;

		if (date.length == 10) 
		{
			if (culture == 'en-US')
			{
				day = date.substring(3, 5);
				month = date.substring(0, 2);	
			}
			else
			{
				day = date.substring(0, 2);
				month = date.substring(3, 5);
			}
								
			year = date.substring(6, 10);
			sep1 = date.substring(2, 3);
			sep2 = date.substring(5, 6);
			
			if (isNaN(parseFloat(year)) == true || parseFloat(year) < 1)
			{
				valid = 0;	
			}
			else
			{
				if (sep1 != '/' || sep2 != '/') 
				{
					valid = 0;
				}
				else
				{
					if (isNaN(parseFloat(month)) == true || parseFloat(month) < 1 || parseFloat(month) > 12)
					{
						valid = 0;
					}
					else
					{
						if (parseFloat(month) == 2) 
						{
							if ((parseFloat(year)) / 4 == parseInt(parseFloat(year) / 4)) 
							{
								if (parseFloat(day) < 1 || parseFloat(day) > 29) 
								{
									valid = 0;
								}
							}
							else
							{
								if (parseFloat(day) < 1 || parseFloat(day) > 28) 
								{
									valid = 0;
								}
							}
						}
						else
						{
							if (parseFloat(month) == 4 || parseFloat(month) == 6 || parseFloat(month) == 9 || parseFloat(month) == 11) 
							{
								if (parseFloat(day) < 1 || parseFloat(day) > 30) 
								{
									valid = 0;
								}
							}
							else
							{
								if (parseFloat(month) == 1 || parseFloat(month) == 3 || parseFloat(month) == 5 || parseFloat(month) == 7 || parseFloat(month) == 8 || parseFloat(month) == 10 || parseFloat(month) == 12) 
								{
									if (parseFloat(day) < 1 || parseFloat(day) > 31) 
									{
										valid = 0;
									}
								}
							}
						}
					}
				}
			}
		}
		else
		{
			if (date.length > 0)
			{
				valid = 0;
			}
		}
		
		if (valid == 0)
		{											
			field.value = '';							
			alert('Data inválida.');
			field.focus();	
		}
	}
	catch(e){}
}

function agencia()
{
	try
	{	
		var field = event.srcElement;
		
		if (window.event.type == 'keypress')
		{
			var keyboardKey = window.event.which;
			if(keyboardKey == null) keyboardKey = window.event.keyCode;
			if((keyboardKey > 47 && keyboardKey < 58) || (keyboardKey == 13)) event.returnValue = true;		
			else event.returnValue = false;
			
			if (keyboardKey != 8)
			{				
				if (field.value.length == 5) field.value += '/';								
				//if (field.value.length == 8) checkAgencia(field);								
			}
		}
		else
		{						
			var value = filter(field.value,0,0,'');
			var returnValue = '';
									
			for (var i = 0; i < value.length; i++) 
			{								
				if (i == 5)
				{
					returnValue += '/' + value.substring(i,i+1);				
				}
				else
				{
					returnValue += value.substring(i,i+1);					
				}																	
			}
			
			field.value = returnValue;	
			//checkAgencia(field);							
		}				
	}
	catch(e){}									
}

function bancoAgencia()
{
	try
	{	
		var field = event.srcElement;
		
		if (window.event.type == 'keypress')
		{
			var keyboardKey = window.event.which;
			if(keyboardKey == null) keyboardKey = window.event.keyCode;
			if((keyboardKey > 47 && keyboardKey < 58) || (keyboardKey == 13)) event.returnValue = true;		
			else event.returnValue = false;
			
			if (keyboardKey != 8)
			{				
				if (field.value.length == 3) field.value += '-';								
				if (field.value.length == 9) field.value += '/';								
				//if (field.value.length == 8) checkAgencia(field);								
			}
		}
		else
		{						
			var value = filter(field.value,0,0,'');
			var returnValue = '';
									
			for (var i = 0; i < value.length; i++) 
			{								
				if (i == 3)
				{
					returnValue += '-' + value.substring(i,i+1);				
				}
				else if (i == 8)
				{
					returnValue += '/' + value.substring(i,i+1);				
				}
				else
				{
					returnValue += value.substring(i,i+1);					
				}																	
			}
			
			field.value = returnValue;	
			//checkAgencia(field);							
		}				
	}
	catch(e){}									
}

function checkAgencia(field) 
{		
	try
	{		
		var value = filter(field.value,0,0,'');
		
		if (value.length > 0 && value.length != 7)
		{
			field.value = '';							
			alert('Agência inválida.');
			field.focus();			
		}
	}
	catch(e){}
}

function magnus()
{
	try
	{
		var field = event.srcElement;
		
		var keyboardKey = window.event.which;
		if(keyboardKey == null) keyboardKey = window.event.keyCode;
		if((keyboardKey > 47 && keyboardKey < 58) || (keyboardKey == 13)) event.returnValue = true;		
		else event.returnValue = false;
		
		if (keyboardKey != 0 && keyboardKey != 8 && keyboardKey != 13)
		{
			var	fieldValue = filter(field.value,0,0,''); 
			var fieldLength = fieldValue.length;
			var returnValue = '';
			
			for (var i = 0; i < fieldLength; i++) 
			{ 																								
				switch(i)
				{
					case 2:
					case 4:						
						returnValue += fieldValue.substring(i,i+1) + '.';						
						break;											
					case 7:						
						returnValue += fieldValue.substring(i,i+1) + '-';						
						break;
					default:
						returnValue += fieldValue.substring(i,i+1);													
						break;
				}												
			}
			
			field.value = returnValue;
		}
	}
	catch(e){}
}

function checkFilter(fieldTitle)
{
	try
	{
		if (window.event.type == 'click')
		{
			if (event.srcElement.value == fieldTitle)
			{
				event.srcElement.value = '';
			}
		}
		else if (window.event.type == 'blur')
		{
			if (event.srcElement.value == '')
			{
				event.srcElement.value = fieldTitle;
			}
		}
	}
	catch(e){}
}

function showDiv(ID)
{
	if (document.getElementById(ID).style.display == 'none')
	{
		document.getElementById(ID).style.display = 'inline';
	}
	else
	{
		document.getElementById(ID).style.display = 'none';
	}
}

function formatTruncate(textValue,length)
{
	if (textValue.length > length)
	{
		return textValue.substring(0,length-3) + '...';
	}
	else
	{
		return textValue;
	}
}

function changeFocus()
{
	if (event.srcElement.className != 'fieldRO' && event.srcElement.className != 'fieldRON')
	{
		if (window.event.type == 'focus')
		{
			event.srcElement.style.background = '#FFCC99';
		}
		else
		{
			event.srcElement.style.background = '#FFFFFF';
		}
	}
}

function focus(fielName)
{
	try
	{
		document.getElementById(fielName).focus();
	}
	catch(e){}
}

function blockEnter()
{
	var keyboardKey = window.event.which;
	
	if(keyboardKey == null) keyboardKey = window.event.keyCode;		
	
	if(keyboardKey == 13)			
	{		
		event.returnValue = false;		
	}	
}

function loadDDL_CallBack(response)
{			
	try
	{		
		eval(response.value);		
		response = null;		
	}
	catch(e){}	
}


function changeField(fieldIndex)	
{			
	var field;			
	var fieldID;	
	var dataGridID;
	var keyboardKey;
	
	try
	{
		field = event.srcElement;			
		fieldID = field.name.substring(field.name.lastIndexOf(':') + 1,field.name.length);	
		dataGridID = event.srcElement.parentElement.parentElement.parentElement.parentElement.id;
		keyboardKey = window.event.which;
		
		if(keyboardKey == null) keyboardKey = window.event.keyCode;		
					
		switch(keyboardKey)
		{								
			case 13:
			case 43:				
				event.returnValue = false;
				event.keyCode = 0;			
				try
				{						
					document.getElementById(dataGridID + ':_ctl' + (fieldIndex + 4) + ':' + fieldID).focus();
				}
				catch(e)
				{						
					document.getElementById(dataGridID + ':_ctl3:' + fieldID).focus();
				}
				break;
			case 45:
				event.returnValue = false;
				event.keyCode = 0;			
				try
				{
					document.getElementById(dataGridID + ':_ctl' + (fieldIndex + 2) + ':' + fieldID).focus();
				}
				catch(e)
				{
					document.getElementById(dataGridID + ':_ctl3:' + fieldID).focus();
				}
				break;			
		}	
	}
	catch(e){}			
}

function checkAll()
{
	var field;			
	var fieldID;
	var dataGridID;
	
	try
	{											
		field = event.srcElement;			
		fieldID = field.name.substring(field.name.lastIndexOf(':') + 1,field.name.length).replace('All','');	
		dataGridID = event.srcElement.parentElement.parentElement.parentElement.parentElement.parentElement.id;
		
		for (i = 3 ; i <= parseInt(document.getElementById('txtRowCount').value) + 2; i++) 
		{					
			if (field.checked == true)
			{						
				document.getElementById(dataGridID + '__ctl' + i + '_' + fieldID).checked = true;																														
			}
			else
			{						
				document.getElementById(dataGridID + '__ctl' + i + '_' + fieldID).checked = false;												
			}					
		}						        													
	}
	catch(e){}	
}	

function copyAll()
{
	var field;			
	var fieldID;
	var dataGridID;
	
	try
	{											
		if (window.event.altKey == true && window.event.keyCode == 78)
		{	
			field = event.srcElement;			
			fieldID = field.name.substring(field.name.lastIndexOf(':') + 1,field.name.length);	
			dataGridID = event.srcElement.parentElement.parentElement.parentElement.parentElement.id;
			
			for (i = 3 ; i <= parseInt(document.getElementById('txtRowCount').value) + 2; i++) 
			{					
				document.getElementById(dataGridID + '__ctl' + i + '_' + fieldID).value = field.value;								
			}
		}						        													
	}
	catch(e){}		
}		

function setWaitingPosition(e)
{			
	try
	{
		document.getElementById('divAguarde').style.top = event.clientY + document.body.scrollTop - 60;
		document.getElementById('divAguarde').style.left = event.clientX + document.body.scrollLeft - 200;	
	}
	catch(e){}			
}

function waiting(displayStatus)
{					
	document.onmousemove = setWaitingPosition
	
	try
	{				
		strMessage = document.getElementById('lblMsgAguarde').outerText.toString();			
	}
	catch(e)
	{
		strMessage = 'Aguarde...';
	}
		
	if (displayStatus == true)
	{
		if (document.getElementById('divAguarde') == null)
		{				
			var oDiv = document.createElement('<div class = "waiting" align="center" id="divAguarde" style="display:inline; position:absolute; width:200px; height:60px; z-index:1"><img id="imgSetaDir" src="../image/barra_progresso.gif" border="0" align="center" valign="top"/></div>');				
			oDiv.innerHTML = strMessage + '<BR><img id="imgSetaDir" src="../image/barra_progresso.gif" border="0" align="center" valign="top"/>';					
			document.body.insertBefore(oDiv);								
		}
		else
		{												
			document.getElementById('divAguarde').style.display = 'inline';				
		}
	}
	else if (document.getElementById('divAguarde') != null)
	{	
		document.getElementById('divAguarde').style.display = 'none';		
	}			
}

function translateValue(strValue,strDirection,strComplement,NumChar)
{
	strReturnValue = strValue;	
												
	for (i = strValue.toString().length; i < NumChar; i++)
	{
		switch (strDirection)
		{
			case 'left':
				strReturnValue = strComplement + strReturnValue;
				break;
			case 'right':
				strReturnValue = strReturnValue + strComplement;	
				break;
		}			
	}
	
	return strReturnValue;
	
}

function trim(strValue)
{
	var strTemp = '';
			
	for (i=0; i < strValue.length; i++)
	{
		if (strValue.substr(i,1) != ' ')
		{
			strTemp = strValue.substr(i,strValue.length - 1);
			break;
		}	
	}
	
	strValue = strTemp;
	strTemp = '';

	for (i=0; i < strValue.length; i++)
	{
		if (strValue.substr(strValue.length - i - 1,1) != ' ')
		{
			strTemp = strValue.substr(0,strValue.length - i);
			break;
		}	
	}
	
	strValue = strTemp;
		
	return strValue;
}

function compareTextCaseSensitive (opt1, opt2) 
{ 	
	return opt1.text < opt2.text ? -1 : 
	opt1.text > opt2.text ? 1 : 0; 
} 


function insertColumn(pIDFrom,pIDTo)
{
	try
	{
		var arrOptions = new Array();							
		var lbFrom = document.getElementById(pIDFrom);
		var lbTo = document.getElementById(pIDTo);			
		var txtFrom = document.getElementById(pIDFrom.replace('lb','txtCod'));
		var txtTo = document.getElementById(pIDTo.replace('lb','txtCod'));							 			
						
		for (i = 0; i <= lbFrom.length - 1; i++) 
		{													
			if (lbFrom.options[i].selected == true)
			{
				try
				{
					lbTo.length = lbTo.length + 1;
					lbTo.options[lbTo.length - 1].value = lbFrom.options[i].value;
					lbTo.options[lbTo.length - 1].text = lbFrom.options[i].text;														
					
					txtFrom.value = txtFrom.value.replace('|' + lbFrom.options[i].value + '|','');
					txtTo.value += '|' + lbFrom.options[i].value + '|';					
				}
				catch(e){}
				
				lbFrom.remove(i);
				i -= 1;
			}								
		}
		
		for(i = 0; i < lbTo.length; i++)  
		{
			arrOptions[i] = new Option(lbTo.options[i].text,lbTo.options[i].value,lbTo.options[i].defaultSelected,lbTo.options[i].selected); 
		}

		arrOptions.sort(compareTextCaseSensitive);
						
		lbTo.options.length = 0; 
		
		for (i = 0; i < arrOptions.length; i++)
		{ 
			lbTo.options[i] = arrOptions[i]; 
		}
		
		arrOptions = null;
		lbFrom = null;
		lbTo = null;
		txtFrom = null;
		txtTo = null;
	}
	catch(e){}	
}

function insertAllColumn(pIDFrom,pIDTo)
{
	try
	{
		var arrOptions = new Array();
		var lbFrom = document.getElementById(pIDFrom);
		var lbTo = document.getElementById(pIDTo);		
		var txtFrom = document.getElementById(pIDFrom.replace('lb','txtCod'));
		var txtTo = document.getElementById(pIDTo.replace('lb','txtCod'));		
												 	
		while (lbFrom.length > 0) 
		{																	
			try
			{
				lbTo.length = lbTo.length + 1;
				lbTo.options[lbTo.length - 1].value = lbFrom.options[0].value;
				lbTo.options[lbTo.length - 1].text = lbFrom.options[0].text;	
				
				txtFrom.value = txtFrom.value.replace('|' + lbFrom.options[0].value + '|','');
				txtTo.value += '|' + lbFrom.options[0].value + '|';													
			}
			catch(e){alert(e)}
								
			lbFrom.remove(0);											
		}
		
		for(i = 0; i < lbTo.length; i++)
		{
			arrOptions[i] = new Option(lbTo.options[i].text,lbTo.options[i].value,lbTo.options[i].defaultSelected,lbTo.options[i].selected); 
		}

		arrOptions.sort(compareTextCaseSensitive);
						
		lbTo.options.length = 0; 
		
		for (i = 0; i < arrOptions.length; i++)
		{ 
			lbTo.options[i] = arrOptions[i]; 
		}	
		
		arrOptions = null;
		lbFrom = null;
		lbTo = null;
		txtFrom = null;
		txtTo = null;
	}
	catch(e){alert(e)}
}	

function openExportacao(pageName)
{
	var strValue = '';
	var strParameters = '';
	var strMultipleParameters = '';
	
	for (i = 0 ; i <= document.forms[0].elements.length - 1; i++) 
	{
		if (document.forms[0].elements[i].type == "text" || document.forms[0].elements[i].type == "textarea" || document.forms[0].elements[i].type == "select-one" || document.forms[0].elements[i].type == "select-multiple") 
		{
			if (document.forms[0].elements[i].type == "select-multiple")
			{
				strMultipleParameters = '';
				
				for (e = 0 ; e <= document.forms[0].elements[i].options.length - 1; e++) 
				{
					if (document.forms[0].elements[i].options[e].selected == true)
					{
						if (strMultipleParameters == '')
						{
							strMultipleParameters = document.forms[0].elements[i].options[e].value;					
						}
						else
						{
							strMultipleParameters += ',' + document.forms[0].elements[i].options[e].value;
						}
					}	
				}
				
				strValue = strMultipleParameters;								
			}
			else
			{
				strValue = document.forms[0].elements[i].value;
			}
			
			if (strValue != '')
			{
				if (strParameters == '')
				{
					strParameters = '?' + document.forms[0].elements[i].id + '=' + strValue;					
				}
				else
				{
					strParameters += '&' + document.forms[0].elements[i].id + '=' + strValue;
				}
			}						
		}
	}

	window.open(pageName + strParameters,pageName.replace('.aspx',''),'toolbar=yes,menubar=yes,scrollbars=yes,resizable=yes,width=600,height=400,left=' + (window.screen.width / 2 - 300) + ',top=' + (window.screen.height / 2 - 200));
}

function validateEmail(email)
{
	var regExpEmail = /^[\w-]+(\.[\w-]+)*@(([A-Za-z\d][A-Za-z\d-]{0,61}[A-Za-z\d]\.)+[A-Za-z]{2,6}|\[\d{1,3}(\.\d{1,3}){3}\])(.*)$/;
	
	if (regExpEmail.test(email) && email != '' && email != null)
		return true;
	else 
		return false;
}

function convertMinuteToHourMinute(pMinute)
{
	dHour = 60;            
	iHour = 0;            
	dMinute = 0;
	strHour = '';
	strMinute = '';

	pMinute = pMinute.toString();

	if (pMinute != '')
	{		
		iHour = parseInt(parseFloat(pMinute) / dHour);

		dMinute = ((parseFloat(pMinute) % dHour) / dHour) * dHour;
						
		if (iHour < 10) { strHour = '0' + iHour; }
		else { strHour = iHour.toString(); }

		if (dMinute < 10) { strMinute = '0' + dMinute.toString(); }
		else { strMinute = dMinute.toString();}

		return (strHour + ':' + strMinute);
	}
	else
		return '';
}