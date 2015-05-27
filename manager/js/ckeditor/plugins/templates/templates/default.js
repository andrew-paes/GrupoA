/*
Copyright (c) 2003-2011, CKSource - Frederico Knabben. All rights reserved.
For licensing, see LICENSE.html or http://ckeditor.com/license
*/

CKEDITOR.addTemplates('default', { imagesPath: CKEDITOR.getUrl(CKEDITOR.plugins.getPath('templates') + 'templates/images/'), templates: [
{
    title: 'Revista - 2 Colunas',
    image: 'template2.gif',
    description: 'Modelo de página com duas colunas.',
    html: '<p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec laoreet mauris euismod leo posuere quis aliquet libero pretium. Cras facilisis hendrerit laoreet. In cursus placerat odio a facilisis. Integer leo nunc, porttitor eu tincidunt vestibulum, gravida in nunc. Fusce dictum nulla tellus. Nunc accumsan risus sit amet tellus malesuada nec cursus dolor accumsan. Nulla ullamcorper sapien vel massa tincidunt vitae ultrices elit malesuada. Maecenas sit amet cursus tellus.</p>' +
          '<table cellspacing="0" cellpadding="0" width="680" border="0"><tr><td width="332" valign="top">Inserir texto coluna esquerda</td><td width="16"><!-- --></td><td width="332" valign="top">Inserir texto coluna direita</td></tr></table>'
},
{
    title: 'Imagem com legenda',
    image: 'template6.gif',
    description: 'Modelo de imagem com legenda',
    html: '<div class="imgLeg">Imagem<br /><span>Texto da legenda<span></div>'
},
{
    title: 'Tabela Rosa - BMJ',
    image: 'template4.gif',
    description: 'Modelo de tabela rosa',
    html: '<table cellspacing="0" width="100%" cellpadding="0" border="0" class="tableRosa"><tr><td valign="top">Texto da tabela</td></tr></table>'
},
{
    title: 'Tabela Azul - BMJ',
    image: 'template5.gif',
    description: 'Modelo de tabela azul',
    html: '<table cellspacing="0" width="100%" cellpadding="0" border="0" class="tableAzul"><tr><td valign="top">Texto da tabela</td></tr></table>'
},
{
    title: 'Tabela Roxo Fraco - Pátio',
    image: 'template7.gif',
    description: 'Modelo de tabela azul',
    html: '<table cellspacing="0" width="100%" cellpadding="0" border="0" class="tableRoxoFraco"><tr><td valign="top">Texto da tabela</td></tr></table>'
},
{
    title: 'Tabela Roxo Forte - Pátio',
    image: 'template8.gif',
    description: 'Modelo de tabela azul',
    html: '<table cellspacing="0" width="100%" cellpadding="0" border="0" class="tableRoxoForte"><tr><td valign="top">Texto da tabela</td></tr></table>'
},
{
    title: 'Tabela Verde Fraco - Pátio',
    image: 'template9.gif',
    description: 'Modelo de tabela azul',
    html: '<table cellspacing="0" width="100%" cellpadding="0" border="0" class="tableVerdeFraco"><tr><td valign="top">Texto da tabela</td></tr></table>'
},
{
    title: 'Tabela Verde Forte - Pátio',
    image: 'template10.gif',
    description: 'Modelo de tabela azul',
    html: '<table cellspacing="0" width="100%" cellpadding="0" border="0" class="tableVerdeForte"><tr><td valign="top">Texto da tabela</td></tr></table>'
},
{
    title: 'Tabela Laranja Fraco - Pátio',
    image: 'template11.gif',
    description: 'Modelo de tabela azul',
    html: '<table cellspacing="0" width="100%" cellpadding="0" border="0" class="tableLaranjaFraco"><tr><td valign="top">Texto da tabela</td></tr></table>'
},
{
    title: 'Tabela Laranja Forte - Pátio',
    image: 'template12.gif',
    description: 'Modelo de tabela azul',
    html: '<table cellspacing="0" width="100%" cellpadding="0" border="0" class="tableLaranjaForte"><tr><td valign="top">Texto da tabela</td></tr></table>'
},
{
    title: 'Image and Title',
    image: 'template1.gif',
    description: 'One main image with a title and text that surround the image.',
    html: '<h3><img style="margin-right: 10px" height="100" width="100" align="left"/>Type the title here</h3><p>Type the text here</p>'
},
{
    title: 'Strange Template',
    image: 'template2.gif',
    description: 'A template that defines two colums, each one with a title, and some text.',
    html: '<table cellspacing="0" cellpadding="0" style="width:100%" border="0"><tr><td style="width:50%"><h3>Title 1</h3></td><td></td><td style="width:50%"><h3>Title 2</h3></td></tr><tr><td>Text 1</td><td></td><td>Text 2</td></tr></table><p>More text goes here.</p>'
},
{
    title: 'Text and Table',
    image: 'template3.gif',
    description: 'A title with some text and a table.',
    html: '<div style="width: 80%"><h3>Title goes here</h3><table style="width:150px;float: right" cellspacing="0" cellpadding="0" border="1"><caption style="border:solid 1px black"><strong>Table title</strong></caption></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr><tr><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr></table><p>Type the text here</p></div>'
}
]});
