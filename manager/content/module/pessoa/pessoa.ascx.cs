using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessLogicalLayer.Helper;
using GrupoA.BusinessObject;
using GrupoA.GlobalResources;
using Seguranca;

public partial class content_module_pessoa_pessoa : SmartUserControl
{
    #region [  Properties ]

    /// <summary>
    /// 
    /// </summary>
    private UsuarioBLL _usuarioBLL;

    /// <summary>
    /// 
    /// </summary>
    private UsuarioBLL usuarioBLL
    {
        get
        {
            if (_usuarioBLL == null)
                _usuarioBLL = new UsuarioBLL();
            return _usuarioBLL;
        }
    }

    private ProfessorBLL _professorBLL;
    private ProfessorBLL professorBLL
    {
        get
        {
            if (_professorBLL == null)
                _professorBLL = new ProfessorBLL();
            return _professorBLL;
        }
    }


    #endregion

    #region [ Page Events ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        this.LoadPage();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEnviarSenha_Click(object sender, EventArgs e)
    {
        this.EnviarSenha();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptEndereco_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        this.EnderecoItemDataBound(e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptTelefone_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        this.TelefoneItemDataBound(e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        this.ExecuteClick();
    }

    #region Endereço

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbEditar_Click(object sender, EventArgs e)
    {
        this.CarregarEndereco(sender);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbExcluir_Click(object sender, EventArgs e)
    {
        this.ExcluirEnderenco(sender);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvarEndereco_Click(object sender, EventArgs e)
    {
        this.SalvarEnderenco();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelarEndereco_Click(object sender, EventArgs e)
    {
        this.CancelarEdicaoEndereco();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlEstado_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.CarregaCidades();
    }

    #endregion

    #region Telefone

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbEditarTel_Click(object sender, EventArgs e)
    {
        this.CarregarTelefone(sender);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void lbExcluirTel_Click(object sender, EventArgs e)
    {
        this.ExcluirTelefone(sender);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSalvarTel_Click(object sender, EventArgs e)
    {
        this.SalvarTelefone();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelarTel_Click(object sender, EventArgs e)
    {
        this.CancelarEdicaoTelefone();
    }

    #endregion

    #endregion

    #region [ Methods ]

    /// <summary>
    /// 
    /// </summary>
    private void LoadPage()
    {
        if (IdRegistro > 0)
        {
            hddUsuarioId.Value = IdRegistro.ToString();

            if (!IsPostBack)
            {
                this.LoadForm();
            }
        }
    }

    /// <summary>
    /// Carrega o formulário com o Usuário conforme identificador UsuarioId
    /// </summary>
    protected void LoadForm()
    {
        this.CarregarDadosUsuario();
    }

    /// <summary>
    /// Atribui aos campos da tela as informações básicas do Usuário
    /// </summary>
    /// <param name="usuario">Objeto Usuário que contém o código identificador para busca dos dados.</param>
    private void CarregarDadosUsuario()
    {
        Usuario usuario = new Usuario();
        usuario.UsuarioId = Convert.ToInt32(hddUsuarioId.Value);
        usuario = usuarioBLL.CarregarComDependencia(usuario);

        this.txtNome.Text = usuario.NomeUsuario;
        this.txtEmail.Text = usuario.EmailUsuario;

        if (usuario.TipoPessoa != 1)
        {
            txtTipoPessoa.Text = "PJ";
            txtCPF.Width = new Unit("110px");
            lblCPF.Text = "CNPJ";
            lblDtNascimento.Text = "Data Fundação";

            pnlSexo.Visible = false;
            pnlOcupacao.Visible = false;
        }
        else
        {
            txtTipoPessoa.Text = "PF";
            txtCPF.Width = new Unit("80px");

            pnlSexo.Visible = true;
            pnlOcupacao.Visible = true;

            this.CarregaOcupacoes();
            if (usuario.ProfissionalOcupacao != null && usuario.ProfissionalOcupacao.ProfissionalOcupacaoId > 0)
            {
                ddlOcupacao.SelectedValue = usuario.ProfissionalOcupacao.ProfissionalOcupacaoId.ToString();
            }

            if (!String.IsNullOrEmpty(usuario.Sexo))
            {
                if (usuario.Sexo.Equals("M"))
                {
                    this.rdMasculino.Checked = true;
                }
                else if (usuario.Sexo.Equals("F"))
                {
                    this.rdFeminino.Checked = true;
                }
            }
        }

        if (usuario.DataNascimento.HasValue)
        {
            this.txtDtNascimentoD.Text = usuario.DataNascimento.Value.ToString("dd");
            this.txtDtNascimentoM.Text = usuario.DataNascimento.Value.ToString("MM");
            this.txtDtNascimentoA.Text = usuario.DataNascimento.Value.ToString("yyyy");
        }
        this.txtCPF.Text = usuario.CadastroPessoa;
        //this.txtLogin.Text = usuario.Login;

        hddCadastroPessoa.Value = usuario.CadastroPessoa;
        hddEmail.Value = usuario.EmailUsuario;

        this.txtDtCadastro.Text = usuario.DataHoraCadastro.ToString("dd/MM/yyyy");

        this.chkAtivo.Checked = usuario.Ativo;
        this.chkNews.Checked = usuario.OptinNewsletter;
        this.chkSMS.Checked = usuario.OptinSMS;

        this.CarregarEnderecosUsuario(usuario);
        this.CarregarTelefonesUsuario(usuario);

        hddUrl.Value = String.Concat(System.Configuration.ConfigurationManager.AppSettings["CaminhoSite"].ToString(), "autenticacao/simularAcesso.aspx");
        hddChave.Value = MD5Util.CreateHash(String.Concat(DateTime.Now.ToString("yyyyMMdd"), hddUsuarioId.Value, System.Configuration.ConfigurationManager.AppSettings["ChaveSistema"].ToString()));
    }

    /// <summary>
    /// 
    /// </summary>
    protected void CarregaOcupacoes()
    {
        UsuarioBLL usuarioBLL = new UsuarioBLL();
        List<ProfissionalOcupacao> profissionalOcupacao = null;
        profissionalOcupacao = usuarioBLL.CarregarProfissionalOcupacao();

        // Carrega as Ocupações
        ddlOcupacao.DataSource = profissionalOcupacao;
        ddlOcupacao.DataTextField = "ocupacao";
        ddlOcupacao.DataValueField = "profissionalOcupacaoId";
        ddlOcupacao.DataBind();
        ddlOcupacao.Items.Insert(0, "");
        ddlOcupacao.SelectedValue = "0";
    }

    /// <summary>
    /// 
    /// </summary>
    private void EnviarSenha()
    {
        Dictionary<string, string> dicionarioDados = null;
        string emailOrigem = string.Empty;
        string emailDestino = string.Empty;
        string assuntoEmail = "Esqueci minha senha";
        string caminhotemplate = string.Empty;
        Usuario usuarioBO = null;

        usuarioBO = new Usuario();
        usuarioBO.CadastroPessoa = hddCadastroPessoa.Value;
        usuarioBO.EmailUsuario = hddEmail.Value;

        usuarioBO = new UsuarioBLL().CarregarPorCadastroPessoaEmail(usuarioBO);

        if (usuarioBO != null && usuarioBO.UsuarioId > 0)
        {
            usuarioBO = new UsuarioBLL().CarregarUsuarioEsqueciMinhaSenha(usuarioBO, System.Configuration.ConfigurationManager.AppSettings["ChaveSistema"].ToString());

            emailDestino = usuarioBO.EmailUsuario;

            dicionarioDados = this.PopulaDicionario(usuarioBO);

            // Carrega o email de destino
            Configuracao configuracao = new Configuracao();
            configuracao.Chave = "emailRemetenteDefault";
            emailOrigem = new ConfiguracaoBLL().CarregarCompleto(configuracao).ConfiguracaoValor.Valor;

            // Carrega o caminho do template
            caminhotemplate = System.Configuration.ConfigurationManager.AppSettings["CaminhoEmailEsqueciMinhaSenha"].ToString();

            StringBuilder templateEmail = new EmailHelper().PopulaTemplateEmail(dicionarioDados, caminhotemplate);

            new EmailHelper().EnviarEmail(emailOrigem, emailDestino, assuntoEmail, templateEmail);

            Util.ShowMessage("Em breve o leitor receberá sua senha por e-mail.", Ag2.Manager.Enumerator.typeMessage.Sucesso);
        }
        else
        {
            Util.ShowMessage("Não foi encontrado encontrado um cadastro combinando o CPF/CNPJ com o e-mail informado.", Ag2.Manager.Enumerator.typeMessage.Erro);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="usuario"></param>
    /// <returns></returns>
    private Dictionary<string, string> PopulaDicionario(Usuario usuario)
    {
        Dictionary<string, string> dicionarioDados = new Dictionary<string, string>();

        dicionarioDados.Add("Cpf_Cnpj", usuario.CadastroPessoa);
        dicionarioDados.Add("Senha", usuario.Senha);
        dicionarioDados.Add("CaminhoSite", System.Configuration.ConfigurationManager.AppSettings["CaminhoImagem"].ToString());

        return dicionarioDados;
    }

    #region [ Endereco ]

    /// <summary>
    /// Carrega os endereços do usuário através do código identificador usuarioId
    /// </summary>
    /// <param name="usuario">Objeto Usuário que contém o código identificador para busca dos dados.</param>
    private void CarregarEnderecosUsuario(Usuario usuario)
    {
        if (usuario.Enderecos != null && usuario.Enderecos.Count > 0)
        {
            this.rptEndereco.DataSource = usuario.Enderecos;
            this.rptEndereco.DataBind();
            rptEndereco.Visible = true;
        }
        else
        {
            this.divEnderecoLista.Visible = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    private void EnderecoItemDataBound(RepeaterItemEventArgs e)
    {
        Endereco enderecoBO = (Endereco)e.Item.DataItem;

        if (enderecoBO != null && enderecoBO.EnderecoId > 0)
        {
            Label lblTipoEndereco = (Label)e.Item.FindControl("lblTipoEndereco");
            Label lblPreferencialEntrega = (Label)e.Item.FindControl("lblPreferencialEntrega");
            TextBox txtNomeEntrega = (TextBox)e.Item.FindControl("txtNomeEntrega");
            TextBox txtLogradouro = (TextBox)e.Item.FindControl("txtLogradouro");
            TextBox txtNumero = (TextBox)e.Item.FindControl("txtNumero");
            TextBox txtComplemento = (TextBox)e.Item.FindControl("txtComplemento");
            TextBox txtBairro = (TextBox)e.Item.FindControl("txtBairro");
            TextBox txtCEP = (TextBox)e.Item.FindControl("txtCEP");
            TextBox txtMunicipio = (TextBox)e.Item.FindControl("txtMunicipio");
            TextBox txtEstado = (TextBox)e.Item.FindControl("txtEstado");
            LinkButton lbEditar = (LinkButton)e.Item.FindControl("lbEditar");
            LinkButton lbExcluir = (LinkButton)e.Item.FindControl("lbExcluir");

            lbEditar.CommandArgument = enderecoBO.EnderecoId.ToString();
            lbExcluir.CommandArgument = enderecoBO.EnderecoId.ToString();

            if (enderecoBO.EnderecoTipo != null && enderecoBO.EnderecoTipo.EnderecoTipoId > 0)
            {
                lblTipoEndereco.Text = String.Concat("-- ", enderecoBO.EnderecoTipo.Tipo, " -- ");
            }

            if (enderecoBO.PreferencialParaEntrega)
            {
                lblPreferencialEntrega.Visible = true;
            }
            else
            {
                lblPreferencialEntrega.Visible = false;
            }

            txtNomeEntrega.Text = enderecoBO.NomeEndereco;
            txtLogradouro.Text = enderecoBO.Logradouro;
            txtNumero.Text = enderecoBO.Numero;
            txtComplemento.Text = enderecoBO.Complemento;
            txtBairro.Text = enderecoBO.Bairro;
            txtCEP.Text = String.Format("{0:00000-000}", Convert.ToInt32(enderecoBO.Cep));

            if (enderecoBO.Municipio != null && enderecoBO.Municipio.MunicipioId > 0)
            {
                txtMunicipio.Text = enderecoBO.Municipio.NomeMunicipio;

                if (enderecoBO.Municipio.Regiao != null && enderecoBO.Municipio.Regiao.RegiaoId > 0)
                {
                    txtEstado.Text = enderecoBO.Municipio.Regiao.NomeRegiao;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    private void CarregarEndereco(object sender)
    {
        Int32 enderecoId = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString());
        hddEnderecoId.Value = enderecoId.ToString();

        Endereco endereco = new EnderecoBLL().CarregarEnderecoComDependencias(new Endereco(enderecoId));

        txtNomeEntrega.Text = endereco.NomeEndereco;
        txtLogradouro.Text = endereco.Logradouro;
        txtNumeroEdicao.Text = endereco.Numero;
        txtComplemento.Text = endereco.Complemento;
        txtBairro.Text = endereco.Bairro;
        txtCEPEdicao.Text = endereco.Cep;

        this.CarregaRegioes();
        ddlEstado.SelectedValue = endereco.Municipio.Regiao.RegiaoId.ToString();

        this.CarregaCidades(endereco.Municipio.Regiao.RegiaoId);
        ddlMunicipio.SelectedValue = endereco.Municipio.MunicipioId.ToString();

        pnlEndereco.Visible = true;
        rptEndereco.Visible = false;
    }

    /// <summary>
    /// 
    /// </summary>
    private void CancelarEdicaoEndereco()
    {
        pnlEndereco.Visible = false;
        rptEndereco.Visible = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    private void ExcluirEnderenco(object sender)
    {
        Int32 enderecoId = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString());
        new EnderecoBLL().Excluir(new Endereco(enderecoId));
        Util.ShowMessage("Endereço excluído com sucesso.", Ag2.Manager.Enumerator.typeMessage.Sucesso);

        Usuario usuario = new Usuario();
        usuario.UsuarioId = Convert.ToInt32(hddUsuarioId.Value);
        usuario = usuarioBLL.CarregarComDependencia(usuario);

        this.CarregarEnderecosUsuario(usuario);
    }

    /// <summary>
    /// 
    /// </summary>
    protected void CarregaRegioes()
    {
        MunicipioBLL municipioBLL = new MunicipioBLL();
        List<Regiao> regiao = municipioBLL.CarregarTodasRegioes();
        regiao.Insert(0, new Regiao() { RegiaoId = 0, NomeRegiao = "Selecione" });

        // Carrega os Estados		
        ddlEstado.DataSource = regiao;
        ddlEstado.DataTextField = "nomeRegiao";
        ddlEstado.DataValueField = "regiaoId";
        ddlEstado.DataBind();
        ddlEstado.SelectedValue = "0";
    }

    /// <summary>
    /// 
    /// </summary>
    protected void CarregaCidades()
    {
        if (ddlEstado.SelectedValue != null && ddlEstado.SelectedValue != String.Empty && Convert.ToInt32(ddlEstado.SelectedValue) > 0)
        {
            this.CarregaCidades(Convert.ToInt32(ddlEstado.SelectedValue));
        }
        else
        {
            ddlMunicipio.Items.Clear();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="regiaoId"></param>
    protected void CarregaCidades(int regiaoId)
    {
        MunicipioBLL municipioBLL = new MunicipioBLL();
        List<Municipio> municipio = null;
        municipio = municipioBLL.CarregarTodasCidades(regiaoId);

        // Carrega as Cidades
        ddlMunicipio.DataSource = municipio;
        ddlMunicipio.DataTextField = "nomeMunicipio";
        ddlMunicipio.DataValueField = "municipioId";
        ddlMunicipio.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    private void SalvarEnderenco()
    {
        if (this.ValidaCep(txtCEPEdicao.Text))
        {
            Endereco endereco = new EnderecoBLL().Carregar(new Endereco(Convert.ToInt32(hddEnderecoId.Value)));

            endereco.NomeEndereco = txtNomeEntrega.Text;
            endereco.Logradouro = txtLogradouro.Text;
            endereco.Numero = txtNumeroEdicao.Text;
            endereco.Complemento = txtComplemento.Text;
            endereco.Bairro = txtBairro.Text;
            endereco.Cep = txtCEPEdicao.Text.Replace("-", "");
            endereco.Municipio.MunicipioId = Convert.ToInt32(ddlMunicipio.SelectedValue);

            new EnderecoBLL().Atualizar(endereco);
            Util.ShowMessage("Endereço alterado com sucesso.", Ag2.Manager.Enumerator.typeMessage.Sucesso);

            pnlEndereco.Visible = false;

            Usuario usuario = new Usuario();
            usuario.UsuarioId = Convert.ToInt32(hddUsuarioId.Value);
            usuario = usuarioBLL.CarregarComDependencia(usuario);

            this.CarregarEnderecosUsuario(usuario);
        }
        else
        {
            Util.ShowMessage("CEP inválido.", Ag2.Manager.Enumerator.typeMessage.Erro);
        }
    }

    #endregion

    #region [ Telefone ]

    /// <summary>
    /// Carrega os telefones do Usuário
    /// </summary>
    /// <param name="usuario">Objeto Usuário que contém o código identificador para busca dos dados.</param>
    private void CarregarTelefonesUsuario(Usuario usuario)
    {
        if (usuario.Telefones != null && usuario.Telefones.Count > 0)
        {
            List<Telefone> telefones = usuario.Telefones.Where(c => c.TelefoneTipo.TelefoneTipoId == 1 || c.TelefoneTipo.TelefoneTipoId == 3).ToList();

            if (telefones != null && telefones.Count > 0)
            {
                rptTelefone.Visible = true;
                this.rptTelefone.DataSource = usuario.Telefones;
                this.rptTelefone.DataBind();
            }
            else
            {
                this.divTelefoneLista.Visible = false;
            }
        }
        else
        {
            this.divTelefoneLista.Visible = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    private void TelefoneItemDataBound(RepeaterItemEventArgs e)
    {
        Telefone telefoneBO = (Telefone)e.Item.DataItem;

        if (telefoneBO != null && telefoneBO.TelefoneId > 0)
        {
            Label lblTipoTelefone = (Label)e.Item.FindControl("lblTipoTelefone");
            TextBox txtTelefoneDDD = (TextBox)e.Item.FindControl("txtTelefoneDDD");
            TextBox txtTelefone = (TextBox)e.Item.FindControl("txtTelefone");
            TextBox txtRamal = (TextBox)e.Item.FindControl("txtRamal");
            LinkButton lbEditarTel = (LinkButton)e.Item.FindControl("lbEditarTel");
            LinkButton lbExcluirTel = (LinkButton)e.Item.FindControl("lbExcluirTel");

            lbEditarTel.CommandArgument = telefoneBO.TelefoneId.ToString();
            lbExcluirTel.CommandArgument = telefoneBO.TelefoneId.ToString();

            if (telefoneBO.TelefoneTipo != null && telefoneBO.TelefoneTipo.TelefoneTipoId > 0)
            {
                lblTipoTelefone.Text = String.Concat("-- ", telefoneBO.TelefoneTipo.TipoTelefone, " -- ");
            }

            txtTelefoneDDD.Text = telefoneBO.DddTelefone;
            txtTelefone.Text = telefoneBO.NumeroTelefone;
            txtRamal.Text = telefoneBO.Ramal;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    private void CarregarTelefone(object sender)
    {
        Int32 telefoneId = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString());
        hddTelefoneId.Value = telefoneId.ToString();

        Telefone telefone = new TelefoneBLL().Carregar(new Telefone(telefoneId));

        txtTelefoneDDDEdicao.Text = telefone.DddTelefone;
        txtTelefoneEdicao.Text = telefone.NumeroTelefone;
        txtRamalEdicao.Text = telefone.Ramal;

        pnlTelefone.Visible = true;
        rptTelefone.Visible = false;
    }

    /// <summary>
    /// 
    /// </summary>
    private void CancelarEdicaoTelefone()
    {
        pnlTelefone.Visible = false;
        rptTelefone.Visible = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    private void ExcluirTelefone(object sender)
    {
        Int32 telefoneId = Convert.ToInt32(((LinkButton)sender).CommandArgument.ToString());
        new TelefoneBLL().Excluir(new Telefone(telefoneId));
        Util.ShowMessage("Telefone excluído com sucesso.", Ag2.Manager.Enumerator.typeMessage.Sucesso);

        Usuario usuario = new Usuario();
        usuario.UsuarioId = Convert.ToInt32(hddUsuarioId.Value);
        usuario = usuarioBLL.CarregarComDependencia(usuario);

        this.CarregarTelefonesUsuario(usuario);
    }

    /// <summary>
    /// 
    /// </summary>
    private void SalvarTelefone()
    {
        Telefone telefone = new TelefoneBLL().Carregar(new Telefone(Convert.ToInt32(hddTelefoneId.Value)));

        telefone.DddTelefone = txtTelefoneDDDEdicao.Text;
        telefone.NumeroTelefone = txtTelefoneEdicao.Text;
        telefone.Ramal = txtRamalEdicao.Text;

        new TelefoneBLL().Atualizar(telefone);
        Util.ShowMessage("Telefone alterado com sucesso.", Ag2.Manager.Enumerator.typeMessage.Sucesso);

        pnlTelefone.Visible = false;

        Usuario usuario = new Usuario();
        usuario.UsuarioId = Convert.ToInt32(hddUsuarioId.Value);
        usuario = usuarioBLL.CarregarComDependencia(usuario);

        this.CarregarTelefonesUsuario(usuario);
    }

    #endregion

    #region [ Execute ]

    /// <summary>
    /// 
    /// </summary>
    private void ExecuteClick()
    {
        if (this.ValidaDadosDoUsuario())
        {
            Page.MaintainScrollPositionOnPostBack = false;
            UpdateUsuario();
        }
    }

    /// <summary>
    /// Atualiza o status do Usuário para ativo ou não
    /// </summary>
    private void UpdateUsuario()
    {
        Usuario usuario = new Usuario();
        usuario.UsuarioId = Convert.ToInt32(hddUsuarioId.Value);
        usuario = usuarioBLL.CarregarComDependencia(usuario);

        usuario.NomeUsuario = txtNome.Text;
        usuario.EmailUsuario = txtEmail.Text;
        usuario.Login = null;

        if (rdMasculino.Checked)
        {
            usuario.Sexo = "M";
        }
        else if (rdFeminino.Checked)
        {
            usuario.Sexo = "F";
        }
        else
        {
            usuario.Sexo = null;
        }

        usuario.DataNascimento = new DateTime(Convert.ToInt32(txtDtNascimentoA.Text), Convert.ToInt32(txtDtNascimentoM.Text), Convert.ToInt32(txtDtNascimentoD.Text));

        if (ddlOcupacao.SelectedValue != null && ddlOcupacao.SelectedValue != String.Empty && Convert.ToInt32(ddlOcupacao.SelectedValue) > 0)
        {
            usuario.ProfissionalOcupacao = new ProfissionalOcupacao();
            usuario.ProfissionalOcupacao.ProfissionalOcupacaoId = Convert.ToInt32(ddlOcupacao.SelectedValue);
        }
        else
        {
            usuario.ProfissionalOcupacao = null;
        }

        usuario.OptinSMS = chkSMS.Checked;
        usuario.OptinNewsletter = chkNews.Checked;
        usuario.Ativo = chkAtivo.Checked;

        if (!String.IsNullOrEmpty(txtSenha.Text))
        {
            usuario.Senha = txtSenha.Text;
        }

        usuarioBLL.AtualizarUsuario(usuario, System.Configuration.ConfigurationManager.AppSettings["ChaveSistema"].ToString());

        hddCadastroPessoa.Value = usuario.CadastroPessoa;
        hddEmail.Value = usuario.EmailUsuario;

        Util.ShowUpdateMessage();
    }

    #endregion

    #region Validações

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cadastroBasico"></param>
    /// <returns></returns>
    protected bool ValidaDadosDoUsuario()
    {
        Usuario usuario = new Usuario();
        usuario.UsuarioId = Convert.ToInt32(hddUsuarioId.Value);
        bool retorno = true;

        // Valida E-mail e Senha
        if (!ValidaEmail(txtEmail.Text))
        {
            Util.ShowMessage(GrupoA_MensagensErro.Cadastro_Email_Invalido, Ag2.Manager.Enumerator.typeMessage.Erro);
            retorno = false;
        }

        if (!String.IsNullOrEmpty(txtSenha.Text) || !String.IsNullOrEmpty(txtConfirmacaoSenha.Text))
        {
            if (!txtSenha.Text.Equals(txtConfirmacaoSenha.Text))
            {
                Util.ShowMessage(GrupoA_MensagensErro.Cadastro_Senha_Confirmacao_Invalida, Ag2.Manager.Enumerator.typeMessage.Erro);
                retorno = false;
            }

            if (txtSenha.Text.Length < 6)
            {
                Util.ShowMessage(GrupoA_MensagensErro.Cadastro_Senha_Invalida, Ag2.Manager.Enumerator.typeMessage.Erro);
                retorno = false;
            }
        }

        #region [ Valida Pessoa ]

        // Validação de Pessoa Física
        if (txtTipoPessoa.Text.ToUpper().Trim() == "PF")
        {
            usuario.CadastroPessoa = string.Concat(txtCPF.Text);

            // Verifica preenchimento de CPF
            bool cpfDuplicado = !usuarioBLL.ValidarCadastroPessoaUnicoUsuario(usuario);
            if ((!ValidarCPF(usuario.CadastroPessoa)) || (cpfDuplicado))
            {
                if (cpfDuplicado)
                {
                    Util.ShowMessage(GrupoA_MensagensErro.Cadastro_CPF_Duplicado, Ag2.Manager.Enumerator.typeMessage.Erro);
                }
                else
                {
                    Util.ShowMessage(GrupoA_MensagensErro.Cadastro_CPF_Invalido, Ag2.Manager.Enumerator.typeMessage.Erro);
                }
                retorno = false;
            }

            if (String.IsNullOrEmpty(txtDtNascimentoD.Text) || String.IsNullOrEmpty(txtDtNascimentoM.Text) || String.IsNullOrEmpty(txtDtNascimentoA.Text))
            {
                Util.ShowMessage(GrupoA_MensagensErro.Cadastro_Nascimento_Data_Valida, Ag2.Manager.Enumerator.typeMessage.Erro);
                retorno = false;
                retorno = false;
            }
            else
            {
                //Verifica Nascimento
                DateTime datData;
                if ((Int32.Parse(txtDtNascimentoA.Text) < 1900 || Int32.Parse(txtDtNascimentoA.Text) > (DateTime.Now.Year - 5)) ||
                    (!DateTime.TryParse(string.Concat(txtDtNascimentoD.Text, "/", txtDtNascimentoM.Text, "/", txtDtNascimentoA.Text), out datData)))
                {
                    Util.ShowMessage(GrupoA_MensagensErro.Cadastro_Nascimento_Data_Valida, Ag2.Manager.Enumerator.typeMessage.Erro);
                    retorno = false;
                }
            }
        }
        else
        {
            usuario.CadastroPessoa = string.Concat(txtCPF.Text.Trim());

            // Verifica preenchimento de CNPJ
            bool cnpjDuplicado = !usuarioBLL.ValidarCadastroPessoaUnicoUsuario(usuario);
            if (usuario.CadastroPessoa.Length == 0 ||
                (!IsValidCNPJ(usuario.CadastroPessoa)) || (cnpjDuplicado))
            {
                if (cnpjDuplicado)
                {
                    Util.ShowMessage(GrupoA_MensagensErro.Cadastro_CNPJ_Duplicado, Ag2.Manager.Enumerator.typeMessage.Erro);
                }
                else
                {
                    Util.ShowMessage(GrupoA_MensagensErro.Cadastro_CNPJ_Invalido, Ag2.Manager.Enumerator.typeMessage.Erro);
                }
                retorno = false;
            }


        }

        #endregion

        // Verifica se o usuário já existe
        usuario.EmailUsuario = txtEmail.Text;
        usuario.UsuarioId = Convert.ToInt32(hddUsuarioId.Value);
        if (!usuarioBLL.ValidarEmailUnicoUsuario(usuario))
        {
            Util.ShowMessage(GrupoA_MensagensErro.Cadastro_Email_Duplicado, Ag2.Manager.Enumerator.typeMessage.Erro);
            retorno = false;
        }

        return retorno;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="strCPF"></param>
    /// <returns></returns>
    public static bool ValidarCPF(string strCPF)
    {
        Int64 intVal = 0;
        if (strCPF.Length != 11)
        {
            return false;
        }
        try
        {
            //strCPF = strCPF.Replace(".", "").Replace("/", "").Replace("-", "").Replace(" ", "");
            //strCPF = strCPF.Trim();
            intVal = Int64.Parse(strCPF);

            char[] lCPFBase = strCPF.Substring(0, 9).ToCharArray();
            char[] lCPFDigiVeri = strCPF.Substring(9, 2).ToCharArray();
            Int64 d1 = 0;
            for (int i = 0; i < 9; i++)
            {
                d1 += Int64.Parse(lCPFBase[i].ToString()) * (10 - i);
            }
            if (d1 == 0) return false;

            d1 = 11 - (d1 % 11);
            if (d1 > 9) d1 = 0;

            if (Int64.Parse(lCPFDigiVeri[0].ToString()) != d1)
                return false;

            d1 *= 2;
            for (int i = 0; i < 9; i++)
            {
                d1 += Int64.Parse(lCPFBase[i].ToString()) * (11 - i);
            }
            d1 = 11 - (d1 % 11);
            if (d1 > 9) d1 = 0;

            return Int64.Parse(lCPFDigiVeri[1].ToString()) == d1;

        }
        catch (Exception)
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="strCNPJ"></param>
    /// <returns></returns>
    public static bool IsValidCNPJ(string strCNPJ)
    {
        if (!strCNPJ.Substring(0, 1).Equals(""))
        {
            try
            {
                strCNPJ = strCNPJ.Replace(".", "").Replace("/", "").Replace("-", "").Replace(" ", "");
                strCNPJ = strCNPJ.Trim();
                int soma = 0, dig;
                string cnpj_calc = strCNPJ.Substring(0, 12);

                if (strCNPJ.Length != 14)
                    return false;
                char[] chr_cnpj = strCNPJ.ToCharArray();
                /* Primeira parte */
                for (int i = 0; i < 4; i++)
                    if (chr_cnpj[i] - 48 >= 0 && chr_cnpj[i] - 48 <= 9)
                        soma += (chr_cnpj[i] - 48) * (6 - (i + 1));
                for (int i = 0; i < 8; i++)
                    if (chr_cnpj[i + 4] - 48 >= 0 && chr_cnpj[i + 4] - 48 <= 9)
                        soma += (chr_cnpj[i + 4] - 48) * (10 - (i + 1));
                dig = 11 - (soma % 11);
                cnpj_calc += (dig == 10 || dig == 11) ? "0" : dig.ToString();
                /* Segunda parte */
                soma = 0;
                for (int i = 0; i < 5; i++)
                    if (chr_cnpj[i] - 48 >= 0 && chr_cnpj[i] - 48 <= 9)
                        soma += (chr_cnpj[i] - 48) * (7 - (i + 1));
                for (int i = 0; i < 8; i++)
                    if (chr_cnpj[i + 5] - 48 >= 0 && chr_cnpj[i + 5] - 48 <= 9)
                        soma += (chr_cnpj[i + 5] - 48) * (10 - (i + 1));
                dig = 11 - (soma % 11);
                cnpj_calc += (dig == 10 || dig == 11) ?
                    "0" : dig.ToString();
                return strCNPJ.Equals(cnpj_calc);
            }
            catch
            {
                //(Exception e){   
                //System.err.println("Erro !"+e);   
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="cep"></param>
    /// <returns></returns>
    private bool ValidaCep(string cep)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(cep, ("[0-9]{5}-[0-9]{3}"));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    private bool ValidaEmail(string email)
    {
        return System.Text.RegularExpressions.Regex.IsMatch(email, ("(?<user>[^@]+)@(?<host>.+)"));
    }

    #endregion

    #endregion
}