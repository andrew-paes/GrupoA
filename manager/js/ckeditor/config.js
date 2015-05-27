/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.editorConfig = function (config) {
    // Define changes to default configuration here. For example:
    // config.language = 'fr';
    // config.uiColor = '#AADC6E';

    config.toolbar_Full =
    [
	    { name: 'document', items : [ 'Source','-','DocProps','Preview','Print','-','Templates' ] },
	    { name: 'clipboard', items : [ 'Cut','Copy','Paste','PasteText','PasteFromWord' ] },
	    { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', 'RemoveFormat', '-', 'Maximize', 'ShowBlocks', '-', 'SpellChecker', 'Scayt'] },
	    //{ name: 'forms', items : [ 'Form', 'Checkbox', 'Radio', 'TextField', 'Textarea', 'Select', 'Button', 'ImageButton', 'HiddenField' ] },
	    '/',
	    { name: 'basicstyles', items : [ ,'Undo','Redo','-','Bold','Italic','Underline','Strike','Subscript','Superscript'] },
	    { name: 'paragraph', items : [ 'NumberedList','BulletedList','-','Outdent','Indent','-','Blockquote','CreateDiv','-','JustifyLeft','JustifyCenter','JustifyRight','JustifyBlock'] },
	    { name: 'links', items : [ 'Link','Unlink','Anchor' ] },
	    '/',
	    { name: 'styles', items : [ 'Styles','Format','Font','FontSize' ] },
	    { name: 'colors', items: ['TextColor', 'BGColor'] },
        '/',
        { name: 'insert', items : [ 'Image','Flash','Table','HorizontalRule','Smiley','SpecialChar','PageBreak','Iframe' ] }
    ];
 
    config.toolbar_Basic =
    [
	    { name: 'document', items: ['Source', '-', 'DocProps', 'Preview', 'Print', '-', 'Templates'] },
	    { name: 'clipboard', items: ['Cut', 'Copy', 'Paste', 'PasteText', 'PasteFromWord'] },
	    { name: 'editing', items: ['Find', 'Replace', '-', 'SelectAll', 'RemoveFormat', '-', 'Maximize', 'ShowBlocks', '-', 'SpellChecker', 'Scayt'] },
	    '/',
	    { name: 'basicstyles', items: [, 'Undo', 'Redo', '-', 'Bold', 'Italic', 'Underline', 'Strike', 'Subscript', 'Superscript'] },
	    { name: 'paragraph', items: ['NumberedList', 'BulletedList', '-', 'Outdent', 'Indent', '-', 'Blockquote', 'CreateDiv', '-', 'JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock'] },
	    { name: 'links', items: ['Link', 'Unlink', 'Anchor'] }
    ];
}
