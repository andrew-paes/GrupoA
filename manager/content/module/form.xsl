<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt"
    xmlns:user="http://mycompany.com/mynamespace"
    xmlns:asp="remove" xmlns:ag2="remove">
  <xsl:output method="xml" indent="yes" encoding="utf-8" omit-xml-declaration="yes"/>


  <msxsl:script language="JavaScript" implements-prefix="user">
    <![CDATA[
    function getExpression(regExpName){
      var expression = '';
      
      expression = regExpName;
      
      if (regExpName.substring(0,1) == "!") {
        regExpName = regExpName.substring(1,(regExpName.length)).ToLower();
        
        switch (regExpName) {
        
          case 'email':
            expression = '^[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\\w\\.-]*[a-zA-Z0-9]\\.[a-zA-Z][a-zA-Z\\.]*[a-zA-Z]$';
            break; 
            
          case 'numeric':
            expression = '^\\d*$';
            break;
          
        }
      }
      
      return expression;
    }
  ]]>
  </msxsl:script>



  <xsl:template match="/" >
    <xsl:apply-templates select="//fields" />
  </xsl:template>


  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE TEXTBOX-->
  <xsl:template match="TextBox">
    <xsl:if test="name(parent::node())='fields'">
      <xsl:apply-templates mode="defaultConfigs" select="." />
      <br />
    </xsl:if>
    <asp:TextBox id="{@name}" runat="server" MaxLength="{@maxlength}" CssClass="frmborder" Width="{@width}">
      <xsl:apply-templates mode="attributes" select="*" />
    </asp:TextBox>
    <xsl:if test="name(parent::node())='fields'">
      <br />
      <br />
    </xsl:if>
  </xsl:template>
  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE TEXTBOX-->

  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE IMAGEPREVIEW-->
  <xsl:template match="ImagePreview">
    <xsl:if test="name(parent::node())='fields'">
      <xsl:apply-templates mode="defaultConfigs" select="." />
      <br />
    </xsl:if>
    <asp:Image id="{@name}" runat="server" Width="{@width}" Height="{@height}" ImageUrl="{@caminho}" >
      <xsl:apply-templates mode="attributes" select="*" />
    </asp:Image>
    <xsl:if test="name(parent::node())='fields'">
      <br />
      <br />
    </xsl:if>
  </xsl:template>
  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE IMAGEPREVIEW-->

  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE HTMLTEXTBOX-->
  <xsl:template match="HtmlTextBox">
    <xsl:if test="name(parent::node())='fields'">
      <xsl:apply-templates mode="defaultConfigs" select="." />
      <br />
    </xsl:if>
    <ag2:HtmlTextBox id="{@name}"  runat="server">
      <xsl:attribute name="MaxLength" >
        <xsl:choose>
          <xsl:when test="string(@maxlength)=''">
            <xsl:value-of select="2" />
          </xsl:when>

          <xsl:otherwise>
            <xsl:value-of select="@maxlength" />
          </xsl:otherwise>
        </xsl:choose>
      </xsl:attribute>

      <xsl:apply-templates mode="attributes" select="*" />
    </ag2:HtmlTextBox>
    <xsl:if test="name(parent::node())='fields'">
      <br />
    </xsl:if>
  </xsl:template>
  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE HTMLTEXTBOX-->

  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE HIDDEN-->
  <xsl:template match="Hidden">
    <asp:HiddenField id="{@name}" runat="server" Value="{@value}" />
  </xsl:template>
  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE HIDDEN-->

  <!--***************************************-->
  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE PASSWORD-->
  <xsl:template match="Password">
    <table border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td nowrap="nowrap" style="padding-right: 40px">
          <xsl:apply-templates mode="defaultConfigs" select="." />
          <br />
          <asp:TextBox id="{@name}" runat="server" MaxLength="{@maxlength}" CssClass="frmborder" Width="90px" TextMode="Password">
            <xsl:apply-templates mode="attributes" select="*" />
          </asp:TextBox>
        </td>
        <td nowrap="nowrap">
          Confirme sua senha:
          <asp:RequiredFieldValidator ErrorMessage="Campo Obrigatório" runat="server" ControlToValidate="{./@name}Retype" SetFocusOnError="true" />
          <asp:CompareValidator runat="server" ControlToValidate="{./@name}" ControlToCompare = "{./@name}Retype" Type="String" ErrorMessage="As senhas informadas devem ser iguais" SetFocusOnErros="true" Style="margin-left:10px" />
          <br />
          <asp:TextBox id="{@name}Retype" runat="server" MaxLength="{@maxlength}" CssClass="frmborder" Width="90px" TextMode="Password" >
            <xsl:apply-templates mode="attributes" select="*" />
          </asp:TextBox>
        </td>
      </tr>
    </table>
    <br />
  </xsl:template>
  <!--***************************************-->

  <!--***************************************-->
  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE COMBOS-->
  <xsl:template match="ListBox">
    <xsl:apply-templates mode="defaultConfigs" select="." />
    <!-- asp:ListBox -->
    <br />
    <xsl:choose>
      <xsl:when test="not(@multiSelectType) or @multiSelectType='None'">
        <asp:ListBox Rows="1" id="{@name}" runat="server" CssClass="frmborder" DataTextField="{@dataTextField}" DataValueField="{@dataValueField}" >
          <xsl:if test="number(@rows)>0">
            <xsl:attribute name="SelectionMode">Multiple</xsl:attribute>
            <xsl:attribute name="Rows">
              <xsl:value-of select="@rows"/>
            </xsl:attribute>
          </xsl:if>
          <xsl:apply-templates mode="listItem" select="." />
        </asp:ListBox>
      </xsl:when>

      <xsl:when test="@multiSelectType='Single'">
        <asp:ListBox Rows="10" id="{@name}" runat="server" CssClass="frmborder" DataTextField="{@dataTextField}" DataValueField="{@dataValueField}" SelectionMode="Multiple">
          <xsl:apply-templates mode="attributes" select="*" />
          <xsl:apply-templates mode="listItem" select="." />
        </asp:ListBox>
      </xsl:when>

      <xsl:when test="@multiSelectType='CheckBox'">
        <asp:CheckBoxList runat="server" id="{@name}"  DataTextField="{@dataTextField}" DataValueField="{@dataValueField}" RepeatLayout="Table" >
          <xsl:apply-templates mode="attributes" select="*" />
          <xsl:apply-templates mode="listItem" select="." />
        </asp:CheckBoxList>
      </xsl:when>

      <xsl:when test="@multiSelectType='Radio'">
        <asp:RadioButtonList runat="server" id="{@name}"  DataTextField="{@dataTextField}" DataValueField="{@dataValueField}" RepeatLayout="Table" >
          <xsl:apply-templates mode="attributes" select="*" />
          <xsl:apply-templates mode="listItem" select="./*" />
        </asp:RadioButtonList>
      </xsl:when>
    </xsl:choose>
    <br />
    <br />
  </xsl:template>
  <!--***************************************-->

  <!--***************************************-->
  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE EDITABLELISTBOX-->
  <xsl:template match="EditableListBox">
    <strong>
      <xsl:value-of select="@label" />:
    </strong>
    <xsl:if test="./@required='true'">
      <asp:RequiredFieldValidator ErrorMessage="Campo Obrigatório" runat="server" ControlToValidate="{@name}:ddl_{@name}" SetFocusOnError="true" />
    </xsl:if>
    <br />
    <ag2:ManageableDropDownList ID="{@name}" CssClass="frmborder" DataValueField="{@dataValueField}" DataTextField="{@dataTextField}" TableName="{@tableName}" runat="server" />
    <br />
    <br />
  </xsl:template>
  <!--***************************************-->

  <!--***************************************-->
  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE CHECKBOX-->
  <xsl:template match="CheckBox">
    <xsl:apply-templates mode="defaultConfigs" select="." />
    <br />
    <asp:CheckBox id="{@name}" runat="server">
      <xsl:apply-templates mode="attributes" select="*" />
    </asp:CheckBox>
    <br />
    <br />
  </xsl:template>
  <!--***************************************-->

  <!--***************************************-->
  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE UPLOAD-->
  <xsl:template match="UploadImage">
    <xsl:apply-templates mode="defaultConfigs" select="." />
    <!-- asp:ListBox -->
    <br />
    <ag2:UploadField id="{@name}" runat="server" MaxFileLength="{@maxFileLength}" FindButtonUrl="~/img/btn_enviar_imagem.gif" ChangeButtonUrl="~/img/btn_enviar_imagem.gif" DeleteButtonUrl="~/img/btn_excluir_imagem.gif" AllowedExtensions="{@allowedExtensions}" TargetFolder="{@targetFolder}" Width="580px">
      <xsl:apply-templates mode="attributes" select="*" />
    </ag2:UploadField>
    <br />
    <br />
  </xsl:template>
  <!--***************************************-->

  <!--***************************************-->
  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE UPLOAD-->
  <xsl:template match="UploadFile">
    <xsl:apply-templates mode="defaultConfigs" select="." />
    <!-- asp:ListBox -->
    <br />
    <ag2:UploadField id="{@name}" runat="server" MaxFileLength="50000" FindButtonUrl="~/img/btn_enviar_arquivo.gif" ChangeButtonUrl="~/img/btn_enviar_arquivo.gif" DeleteButtonUrl="~/img/btn_excluir_arquivo.gif" AllowedExtensions="{@allowedExtensions}" TargetFolder="{@targetFolder}" Width="580px">
      <xsl:apply-templates mode="attributes" select="*" />
    </ag2:UploadField>
    <br />
    <br />
  </xsl:template>
  <!--***************************************-->

  <!--***************************************-->
  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE TEXTAREA-->
  <xsl:template match="TextArea">
    <xsl:apply-templates mode="defaultConfigs" select="." />
    <br />
    <asp:TextBox id="{@name}" runat="server" CssClass="frmborder" Width="70%" rows="10" textMode="multiline">
      <xsl:apply-templates mode="attributes" select="*" />
    </asp:TextBox>
    <br />
    <br />
  </xsl:template>
  <!--***************************************-->

  <!--***************************************-->
  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE DATA-->
  <xsl:template match="Date">
    <xsl:apply-templates mode="defaultConfigs" select="." />
    <xsl:if test="name(parent::node())='fields'">
      <br />
    </xsl:if>
    <ag2:DateField id="{@name}" runat="server" CssClass="dateField">
      <xsl:apply-templates mode="attributes" select="*" />
    </ag2:DateField>
    <xsl:if test="name(parent::node())='fields'">
      <br />
      <br />
    </xsl:if>
  </xsl:template>
  <!-- // TEMPLATE: PARA IMPLEMENTAÇÃO DE DATA-->
  <!--***************************************-->

  <!--***************************************-->
  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE LABEL-->
  <xsl:template match="Label">
    <xsl:if test="name(parent::node())='fields'">
      <br />
    </xsl:if>
    <asp:Label id="{@name}" Text="{node()}" runat="server">

    </asp:Label>
    <xsl:if test="name(parent::node())='fields'">
      <br />
      <br />
    </xsl:if>

  </xsl:template>
  <!-- // TEMPLATE: PARA IMPLEMENTAÇÃO DE LABEL-->
  <!--***************************************-->

  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE ABAS-->
  <xsl:template match="TabPanel">

    <asp:TabPanel runat="server" HeaderText="{@text}" ID="{@name}">
      <ContentTemplate>
        <xsl:apply-templates select="./*" />
      </ContentTemplate>
    </asp:TabPanel>

  </xsl:template>
  <!--***************************************-->

  <!-- TEMPLATE: PARA IMPLEMENTAÇÃO DE SUBFORMS-->
  <xsl:template match="SubForm">

    <ag2:SubForm runat="server" moduleName="{@moduleName}" label="{@label}" Required="{@required}" selectType="{@selectType}" ID="{@name}">
    </ag2:SubForm>

    <br />

  </xsl:template>
  <!--***************************************-->

  <xsl:template match="group">
    <strong>
      <xsl:value-of select="@label" />:
    </strong>
    <xsl:for-each select="./*">
      <xsl:if test="./@required='true'">
        <asp:RequiredFieldValidator ErrorMessage="Campo Obrigatório" runat="server" ControlToValidate="{./@name}" SetFocusOnError="true" />
      </xsl:if>

      <xsl:if test="@ValidationExpression">
        <asp:RegularExpressionValidator runat="server" ControlToValidate="{./@name}"
         ValidationExpression="{user:getExpression(string(./@ValidationExpression))}" Display="Static">Conteúdo inválido</asp:RegularExpressionValidator>
      </xsl:if>
    </xsl:for-each>
    <br />
    <xsl:apply-templates select="./*" />
    <br />
    <br />
  </xsl:template>

  <xsl:template match="TabContainer">

    <asp:TabContainer ID="{./@name}" runat="server" ActiveTabIndex="0">

      <xsl:apply-templates select="./*" />

    </asp:TabContainer>
    <br />
  </xsl:template>

  <!-- TEMPLATE DE CONFIGURAÇÕES COMUNS A TODOS OS CAMPOS -->
  <xsl:template mode="defaultConfigs" match="*">
    <strong>
      <xsl:value-of select="./@label" />
    </strong>
    <xsl:if test="name()='Date'">
      <xsl:if test="./@required='true'">
        <asp:RequiredFieldValidator ErrorMessage="Campo Obrigatório" runat="server" ControlToValidate="{./@name}:{./@name}" SetFocusOnError="true" />
      </xsl:if>
    </xsl:if>
    <xsl:if test="./@required='true'">
      <asp:RequiredFieldValidator ErrorMessage="Campo Obrigatório" runat="server" ControlToValidate="{./@name}" SetFocusOnError="true" />
    </xsl:if>
    <xsl:if test="./@requiredFile='true'">
      <asp:RequiredFieldValidator ErrorMessage="Campo Obrigatório" runat="server" ControlToValidate="{./@name}:fileName" SetFocusOnError="true" />
    </xsl:if>
    <xsl:if test="@ValidationExpression">
      <asp:RegularExpressionValidator runat="server" ControlToValidate="{./@name}"
       ValidationExpression="{user:getExpression(string(./@ValidationExpression))}" Display="Static">Conteúdo inválido</asp:RegularExpressionValidator>
    </xsl:if>
  </xsl:template>

  <!-- TEMPLATE DE CONFIGURAÇÕES DOS ATRIBUTOS DE UM CONTROLE -->
  <xsl:template mode="attributes" match="*">
    <xsl:for-each select="attribute">
      <xsl:attribute name="{@name}">
        <xsl:value-of select="@value" />
      </xsl:attribute>
    </xsl:for-each>
  </xsl:template>

  <!-- ITENS DO COMBOBOX -->
  <xsl:template mode="listItem" match="*">
    <xsl:for-each select="listItem">
      <asp:ListItem Text="{@text}" Value="{@value}">
        <xsl:attribute name="Selected">
          <xsl:choose>
            <xsl:when test="string(@selected)=''">
              <xsl:value-of select="'false'" />
            </xsl:when>

            <xsl:otherwise>
              <xsl:value-of select="@selected" />
            </xsl:otherwise>
          </xsl:choose>

        </xsl:attribute>


        <xsl:attribute name="Enabled">
          <xsl:choose>
            <xsl:when test="string(@enabled)=''">
              <xsl:value-of select="true" />
            </xsl:when>

            <xsl:otherwise>
              <xsl:value-of select="@enabled" />
            </xsl:otherwise>
          </xsl:choose>
        </xsl:attribute>
      </asp:ListItem>
    </xsl:for-each>
  </xsl:template>

</xsl:stylesheet>

