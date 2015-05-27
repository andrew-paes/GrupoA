using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using System.Text;


public partial class content_module_solicitacaoTituloAvalicao_solicitacaoTituloAvalicao : System.Web.UI.UserControl
{
    #region [ Properties ]

    /// <summary>
    /// 
    /// </summary>
    private ProfessorBLL _professorBLL;

    /// <summary>
    /// 
    /// </summary>
    ProfessorBLL professorBLL
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
        if (Util.GetRequestId() > 0)
        {
            hddSolicitacaoTituloId.Value = Util.GetRequestId().ToString();
            if (!IsPostBack)
            {
                //MontaChecklistPagina();
                LoadForm();
            }
        }
        else
            if (!IsPostBack)
            {
                msg.Text = "";
            }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnExecute_Click(object sender, EventArgs e)
    {
        this.SalvaSolicitacao();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptComprovanteDocencia_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        this.ComprovanteDocenciaItemDataBound(e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptProfessorInstituicao_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        this.ProfessorInstituicaoItemDataBound(e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptProfessorCurso_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        this.ProfessorCursoItemDataBound(e);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptProfessorDisciplina_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        this.ProfessorDisciplinaItemDataBound(e);
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

    #endregion

    #region [ Methods ]

    /// <summary>
    /// Carrega o formulário com a solicitação
    /// </summary>
    public void LoadForm()
    {
        this.CarregarComboStatus();

        // Busca dados
        TituloSolicitacao solicitacao = professorBLL.CarregarTituloSolicitacaoComProfessorETitulo(new TituloSolicitacao() { TituloSolicitacaoId = Convert.ToInt32(hddSolicitacaoTituloId.Value) });

        //Solicitação
        txtDataSolicitacao.Text = solicitacao.DataSolicitacao.ToString("dd/MM/yyyy");
        //txtJustificativa.Text = solicitacao.JustificativaProfessor;
        ddlStatus.SelectedValue = solicitacao.TituloSolicitacaoStatus.TituloSolicitacaoStatusId.ToString();

        if (solicitacao.TituloSolicitacaoStatus.TituloSolicitacaoStatusId == 2)
        {
            divAvaliacao.Visible = true;
            this.CarregarAvaliacao();
        }
        else if (solicitacao.TituloSolicitacaoStatus.TituloSolicitacaoStatusId == 3)
        {
            pnlBotao.Visible = false;
            divAvaliacao.Visible = false;
            pnlDestaqueAvaliacao.Visible = false;
        }
        else
        {
            divAvaliacao.Visible = false;
            pnlDestaqueAvaliacao.Visible = false;
        }

        // Professor
        hddUsuarioId.Value = solicitacao.Professor.ProfessorId.ToString();
        txtNomeProfessor.Text = solicitacao.Professor.Usuario.NomeUsuario;
        txtEmail.Text = solicitacao.Professor.Usuario.EmailUsuario;
        txtCPF.Text = solicitacao.Professor.Usuario.CadastroPessoa;

        //Título
        Produto produto = new Produto();

        if (solicitacao.Titulo.TituloImpresso.TituloImpressoId > 0)
        {
            produto.ProdutoId = solicitacao.Titulo.TituloImpresso.TituloImpressoId;
        }
        else
        {
            produto.ProdutoId = solicitacao.Titulo.TituloEletronico.TituloEletronicoId;
        }

        produto = new ProdutoBLL().Carregar(produto);

        if (produto != null)
        {
            txtTitulo.Text = produto.NomeProduto;
            txtEdicao.Text = solicitacao.Titulo.Edicao.ToString();
            if (solicitacao.Titulo.TituloImpresso.TituloImpressoId > 0)
            {
                txtISBN.Text = solicitacao.Titulo.TituloImpresso.Isbn13;
            }
            else
            {
                txtISBN.Text = solicitacao.Titulo.TituloEletronico.Isbn13;
            }

            if (solicitacao.Titulo.DataPublicacao != null)
            {
                txtDataPublicacao.Text = solicitacao.Titulo.DataPublicacao.Value.ToString("dd/MM/yyyy");
            }

            txtAutorTitulo.Text = this.CarregarAutores(solicitacao.Titulo);
        }

        if (solicitacao.TituloSolicitacaoStatus.TituloSolicitacaoStatusId == 1 || solicitacao.TituloSolicitacaoStatus.TituloSolicitacaoStatusId == 4)
        {
            pnlBotao.Visible = true;
            ddlStatus.Visible = true;
            txtStatusEditora.Visible = false;
        }
        else
        {
            //pnlBotao.Visible = false;
            ddlStatus.Visible = false;
            txtStatusEditora.Visible = true;
            txtStatusEditora.Text = solicitacao.TituloSolicitacaoStatus.StatusSolicitacao;
        }

        this.CarregaProfessor();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="titulo"></param>
    /// <returns></returns>
    private string CarregarAutores(Titulo titulo)
    {
        var autores = new TituloBLL().CarregarAutores(titulo);
        string strAutores = string.Empty;
        StringBuilder texto = new StringBuilder();

        int x = 1;

        if (autores != null && autores.Count > 0)
        {
            foreach (var item in autores)
            {
                if (!string.IsNullOrEmpty(item.NomeAutor))
                {
                    texto.Append(item.NomeAutor);

                    if (x < autores.Count)
                    {
                        texto.Append("; ");
                    }
                }
                x++;
            }
        }

        strAutores = texto.ToString();

        return strAutores;
    }

    /// <summary>
    /// 
    /// </summary>
    protected void CarregaProfessor()
    {
        Usuario usuarioBO = new Usuario();
        usuarioBO.UsuarioId = Convert.ToInt32(hddUsuarioId.Value);
        usuarioBO = new UsuarioBLL().CarregarComDependencia(usuarioBO);

        Professor professorBO = new Professor();
        professorBO = new ProfessorBLL().CarregarProfessorCompleto(usuarioBO);

        if (professorBO != null && professorBO.ProfessorId > 0)
        {
            this.chkAutorGrupoA.Checked = professorBO.AutorGrupoa;
            this.chkColaboradorGrupoA.Checked = professorBO.ColaboradorGrupoa;
            this.chkPossuiPublicacao.Checked = professorBO.PossuiPublicacao;

            if (professorBO.GraduacaoProfessor != null && professorBO.GraduacaoProfessor.GraduacaoProfessorId > 0)
            {
                this.txtGraduacao.Text = professorBO.GraduacaoProfessor.Graduacao;
            }

            this.CarregarComprovanteDocencia(professorBO);

            this.CarregarProfessorInstituicao(professorBO);
        }

        this.CarregarEnderecosUsuario(usuarioBO);
    }

    /// <summary>
    /// 
    /// </summary>
    private void SalvaSolicitacao()
    {
        if (ddlStatus.Visible)
        {
            TituloSolicitacao tituloSolicitacao = new TituloSolicitacao();
            tituloSolicitacao.TituloSolicitacaoId = Util.GetRequestId();
            tituloSolicitacao.TituloSolicitacaoStatus = new TituloSolicitacaoStatus();
            tituloSolicitacao.TituloSolicitacaoStatus.TituloSolicitacaoStatusId = Convert.ToInt32(ddlStatus.SelectedValue.ToString());

            new TituloSolicitacaoBLL().AtualizarStatus(tituloSolicitacao, System.Configuration.ConfigurationManager.AppSettings["CaminhoEmailSolicitacaoStatusAlteradoSim"].ToString(), System.Configuration.ConfigurationManager.AppSettings["CaminhoEmailSolicitacaoStatusAlteradoNao"].ToString());
        }
        else
        {
            TituloAvaliacao tituloAvaliacao = new TituloAvaliacaoBLL().Carregar(new TituloAvaliacao(Convert.ToInt32(hddAvaliacaoId.Value)));
            tituloAvaliacao.Avaliacao = txtAvaliacao.Text;
            tituloAvaliacao.NomeAvaliador = txtNomeAvaliador.Text;

            new TituloAvaliacaoBLL().Atualizar(tituloAvaliacao);
        }

        Util.ShowUpdateMessage();

        this.LoadForm();
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarComboStatus()
    {
        ddlStatus.DataTextField = "statusSolicitacao";
        ddlStatus.DataValueField = "tituloSolicitacaoStatusId";

        ddlStatus.DataSource = new TituloSolicitacaoBLL().CarregarTodosTituloSolicitacaoStatusParaLiberacao();
        ddlStatus.DataBind();
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarAvaliacao()
    {
        TituloAvaliacao tituloAvaliacao = new TituloAvaliacaoBLL().CarregarPorSolicitacao(new TituloSolicitacao() { TituloSolicitacaoId = Convert.ToInt32(hddSolicitacaoTituloId.Value) });

        if (tituloAvaliacao != null)
        {
            txtStatusProfessor.Text = "Avaliado";
            trAvaliacao.Visible = true;
            pnlDestaqueAvaliacao.Visible = true;

            hddAvaliacaoId.Value = tituloAvaliacao.TituloAvaliacaoId.ToString();
            rbRelevancia1.Checked = tituloAvaliacao.RelevanciaObra == 1;
            rbRelevancia2.Checked = tituloAvaliacao.RelevanciaObra == 2;
            rbRelevancia3.Checked = tituloAvaliacao.RelevanciaObra == 3;
            txtRelevancia.Text = tituloAvaliacao.RelevanciaObraObs;
            rbConteudo1.Checked = tituloAvaliacao.ConteudoAtualizado == 1;
            rbConteudo2.Checked = tituloAvaliacao.ConteudoAtualizado == 2;
            rbConteudo3.Checked = tituloAvaliacao.ConteudoAtualizado == 3;
            txtConteudoAtualizado.Text = tituloAvaliacao.ConteudoAtualizadoObs;
            rbQualidade1.Checked = tituloAvaliacao.QualidadeTexto == 1;
            rbQualidade2.Checked = tituloAvaliacao.QualidadeTexto == 2;
            rbQualidade3.Checked = tituloAvaliacao.QualidadeTexto == 3;
            txtQualidadeTexto.Text = tituloAvaliacao.QualidadeTextoObs;
            rbApresentacao1.Checked = tituloAvaliacao.ApresentacaoGrafica == 1;
            rbApresentacao2.Checked = tituloAvaliacao.ApresentacaoGrafica == 2;
            rbApresentacao3.Checked = tituloAvaliacao.ApresentacaoGrafica == 3;
            txtApresentacaoGrafica.Text = tituloAvaliacao.ApresentacaoGraficaObs;
            rbMaterial1.Checked = tituloAvaliacao.MaterialComplementar == 1;
            rbMaterial2.Checked = tituloAvaliacao.MaterialComplementar == 2;
            rbMaterial3.Checked = tituloAvaliacao.MaterialComplementar == 3;
            txtMaterialComplementar.Text = tituloAvaliacao.MaterialComplementarObs;
            rbAvaliacao1.Checked = tituloAvaliacao.AvaliacaoGeral == 1;
            rbAvaliacao2.Checked = tituloAvaliacao.AvaliacaoGeral == 2;
            rbAvaliacao3.Checked = tituloAvaliacao.AvaliacaoGeral == 3;
            txtAvaliacaoGeral.Text = tituloAvaliacao.AvaliacaoGeralObs;
            txtPontosFortes.Text = tituloAvaliacao.PontosFortes;
            txtPontosFracos.Text = tituloAvaliacao.PontosFracos;
            txtSugestoes.Text = tituloAvaliacao.Sugestoes;
            rbAdotadaSim.Checked = tituloAvaliacao.SeraAdotada;
            rbAdotadaNao.Checked = tituloAvaliacao.SeraAdotada == false;
            pnlAdotada.Visible = tituloAvaliacao.SeraAdotada;
            txtAdotadaQuais.Text = tituloAvaliacao.SeraAdotadaQuais;
            rbRecomendadaSim.Checked = tituloAvaliacao.SeraRecomendada;
            rbRecomendadaNao.Checked = tituloAvaliacao.SeraRecomendada == false;
            pnlRecomendada.Visible = tituloAvaliacao.SeraRecomendada;
            txtRecomendadaQuais.Text = tituloAvaliacao.SeraRecomendadaQuais;
            chkNaoSeAplica.Checked = tituloAvaliacao.NaoAplica;
            pnlPorque.Visible = tituloAvaliacao.NaoAplica;
            txtPorque.Text = tituloAvaliacao.NaoAplicaPorque;
            pnlObraAdotada.Visible = tituloAvaliacao.NaoAplica;
            txtObraAdotada.Text = tituloAvaliacao.NaoAplicaAdotada;
            pnlAutor.Visible = tituloAvaliacao.NaoAplica;
            txtAutor.Text = tituloAvaliacao.NaoAplicaAutor;
            chkRevisorTecnico.Checked = tituloAvaliacao.RevisorTecnico;
            chkIngles.Checked = tituloAvaliacao.TradutorIngles;
            chkEspanhol.Checked = tituloAvaliacao.TradutorEspanhol;
            chkFrances.Checked = tituloAvaliacao.TradutorFrances;
            chkAlemao.Checked = tituloAvaliacao.TradutorAlemao;
            txtAvaliacao.Text = tituloAvaliacao.Avaliacao;
            txtNomeAvaliador.Text = tituloAvaliacao.NomeAvaliador;
        }
        else
        {
            txtStatusProfessor.Text = "Pendente";
            pnlBotao.Visible = false;
            trAvaliacao.Visible = false;
            pnlDestaqueAvaliacao.Visible = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="usuario"></param>
    private void CarregarEnderecosUsuario(Usuario usuario)
    {
        if (usuario.Enderecos != null && usuario.Enderecos.Count > 0)
        {
            rptEndereco.DataSource = usuario.Enderecos;
            rptEndereco.DataBind();
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

    #region [ ComprovanteDocencia ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="professorBO"></param>
    private void CarregarComprovanteDocencia(Professor professorBO)
    {
        IEnumerable<ProfessorComprovanteDocencia> professorComprovanteDocenciaBOIEnum = new ProfessorComprovanteDocenciaBLL().Carregar(professorBO);

        if (professorComprovanteDocenciaBOIEnum != null && professorComprovanteDocenciaBOIEnum.Any())
        {
            this.rptComprovanteDocencia.DataSource = professorComprovanteDocenciaBOIEnum;
            this.rptComprovanteDocencia.DataBind();
        }
        else
        {
            this.divComprovanteDocencia.Visible = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    private void ComprovanteDocenciaItemDataBound(RepeaterItemEventArgs e)
    {
        ProfessorComprovanteDocencia professorComprovanteDocenciaBO = (ProfessorComprovanteDocencia)e.Item.DataItem;

        if (professorComprovanteDocenciaBO != null && professorComprovanteDocenciaBO.ProfessorComprovanteDocenciaId > 0)
        {
            Literal ltrInstituicao = (Literal)e.Item.FindControl("ltrInstituicao");
            HyperLink lnkDownloadArquivo = (HyperLink)e.Item.FindControl("lnkDownloadArquivo");

            if (professorComprovanteDocenciaBO.Instituicao != null && professorComprovanteDocenciaBO.Instituicao.InstituicaoId > 0)
            {
                Instituicao instituicaoBO = new Instituicao();
                instituicaoBO.InstituicaoId = professorComprovanteDocenciaBO.Instituicao.InstituicaoId;
                instituicaoBO = new InstituicaoBLL().Carregar(instituicaoBO);

                if (ltrInstituicao != null && instituicaoBO != null && instituicaoBO.InstituicaoId > 0)
                {
                    ltrInstituicao.Text = instituicaoBO.NomeInstituicao.ReplaceHtml();
                }
            }

            if (professorComprovanteDocenciaBO.Arquivo != null && professorComprovanteDocenciaBO.Arquivo.ArquivoId > 0)
            {
                Arquivo arquivoBO = new Arquivo();
                arquivoBO.ArquivoId = professorComprovanteDocenciaBO.Arquivo.ArquivoId;
                arquivoBO = new ArquivoBLL().CarregarArquivo(arquivoBO);

                Usuario usuario = new Usuario();
                usuario.UsuarioId = Convert.ToInt32(hddUsuarioId.Value);
                usuario = new UsuarioBLL().CarregarComDependencia(usuario);

                //string pathFile = String.Concat(GrupoA.GlobalResources.GrupoA_Resource.baseUrlUpload, GrupoA.GlobalResources.GrupoA_Resource.PastaComprovantesDocencia, usuario.CadastroPessoa, "/", arquivoBO.NomeArquivo);
                string pathFile = String.Concat(GrupoA.GlobalResources.GrupoA_Resource.baseUrlUpload, GrupoA.GlobalResources.GrupoA_Resource.PastaComprovantesDocencia, "/", arquivoBO.NomeArquivo);

                if (File.Exists(Server.MapPath(pathFile)))
                {
                    lnkDownloadArquivo.NavigateUrl = pathFile;
                }
                else
                {
                    lnkDownloadArquivo.Text = "Arquivo não existe";
                    lnkDownloadArquivo.Enabled = false;
                }
            }
        }
    }

    #endregion

    #region [ ProfessorInstituicao ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="professorBO"></param>
    private void CarregarProfessorInstituicao(Professor professorBO)
    {
        IEnumerable<ProfessorInstituicao> professorInstituicaoBOIEnum = new ProfessorInstituicaoBLL().Carregar(professorBO);

        if (professorInstituicaoBOIEnum != null && professorInstituicaoBOIEnum.Any())
        {
            this.rptProfessorInstituicao.DataSource = professorInstituicaoBOIEnum;
            this.rptProfessorInstituicao.DataBind();
        }
        else
        {
            this.divProfessorInstituicao.Visible = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    private void ProfessorInstituicaoItemDataBound(RepeaterItemEventArgs e)
    {
        ProfessorInstituicao professorInstituicaoBO = (ProfessorInstituicao)e.Item.DataItem;

        if (professorInstituicaoBO != null && professorInstituicaoBO.ProfessorInstituicaoId > 0)
        {
            Label lblNomeInstituicao = (Label)e.Item.FindControl("lblNomeInstituicao");
            Label lblNomeInstituicao2 = (Label)e.Item.FindControl("lblNomeInstituicao2");
            TextBox txtCampus = (TextBox)e.Item.FindControl("txtCampus");
            TextBox txtDepartamento = (TextBox)e.Item.FindControl("txtDepartamento");
            TextBox txtTelefoneDDD = (TextBox)e.Item.FindControl("txtTelefoneDDD");
            TextBox txtTelefone = (TextBox)e.Item.FindControl("txtTelefone");
            HtmlGenericControl divProfessorCurso = (HtmlGenericControl)e.Item.FindControl("divProfessorCurso");
            Repeater rptProfessorCurso = (Repeater)e.Item.FindControl("rptProfessorCurso");

            if (professorInstituicaoBO.Instituicao != null && professorInstituicaoBO.Instituicao.InstituicaoId > 0)
            {
                Instituicao instituicaoBO = new Instituicao();
                instituicaoBO.InstituicaoId = professorInstituicaoBO.Instituicao.InstituicaoId;
                instituicaoBO = new InstituicaoBLL().Carregar(instituicaoBO);

                if (instituicaoBO != null && instituicaoBO.InstituicaoId > 0)
                {
                    if (lblNomeInstituicao != null)
                    {
                        lblNomeInstituicao.Text = String.Concat("-- ", instituicaoBO.NomeInstituicao, " --");
                        lblNomeInstituicao2.Text = instituicaoBO.NomeInstituicao;
                    }

                    if (lblNomeInstituicao2 != null)
                    {
                        lblNomeInstituicao2.Text = instituicaoBO.NomeInstituicao;
                    }
                }
            }

            if (txtCampus != null)
            {
                txtCampus.Text = professorInstituicaoBO.Campus;
            }

            if (txtDepartamento != null)
            {
                txtDepartamento.Text = professorInstituicaoBO.Departamento;
            }

            if (txtTelefoneDDD != null && txtTelefone != null && professorInstituicaoBO.Telefone != null && professorInstituicaoBO.Telefone.TelefoneId > 0)
            {
                Telefone telefoneBO = new Telefone();
                telefoneBO.TelefoneId = professorInstituicaoBO.Telefone.TelefoneId;
                telefoneBO = new TelefoneBLL().Carregar(telefoneBO);

                if (telefoneBO != null && telefoneBO.TelefoneId > 0)
                {
                    txtTelefoneDDD.Text = telefoneBO.DddTelefone;
                    txtTelefone.Text = telefoneBO.NumeroTelefone;
                }
            }

            IEnumerable<ProfessorCurso> professorCursoBOIEnum = new List<ProfessorCurso>();
            professorCursoBOIEnum = new ProfessorCursoBLL().Carregar(professorInstituicaoBO);

            if (rptProfessorCurso != null && professorCursoBOIEnum != null && professorCursoBOIEnum.Any())
            {
                rptProfessorCurso.DataSource = professorCursoBOIEnum;
                rptProfessorCurso.DataBind();
            }
            else
            {
                if (divProfessorCurso != null)
                {
                    divProfessorCurso.Visible = false;
                }
            }
        }
    }

    #endregion

    #region [ ProfessorCurso ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    private void ProfessorCursoItemDataBound(RepeaterItemEventArgs e)
    {
        ProfessorCurso professorCursoBO = (ProfessorCurso)e.Item.DataItem;

        if (professorCursoBO != null && professorCursoBO.ProfessorCursoId > 0)
        {
            Label lblNomeCurso = (Label)e.Item.FindControl("lblNomeCurso");
            Label lblNomeCurso2 = (Label)e.Item.FindControl("lblNomeCurso2");
            CheckBox chkCoordenadorCurso = (CheckBox)e.Item.FindControl("chkCoordenadorCurso");
            TextBox txtNivel = (TextBox)e.Item.FindControl("txtNivel");
            TextBox txtCargo = (TextBox)e.Item.FindControl("txtCargo");
            HtmlGenericControl divProfessorDisciplina = (HtmlGenericControl)e.Item.FindControl("divProfessorDisciplina");
            Repeater rptProfessorDisciplina = (Repeater)e.Item.FindControl("rptProfessorDisciplina");

            if (professorCursoBO.Curso != null && professorCursoBO.Curso.CursoId > 0)
            {
                Curso cursoBO = new Curso();
                cursoBO.CursoId = professorCursoBO.Curso.CursoId;
                cursoBO = new CursoBLL().Carregar(cursoBO);

                if (cursoBO != null && cursoBO.CursoId > 0)
                {
                    if (lblNomeCurso != null)
                    {
                        lblNomeCurso.Text = String.Concat("-- ", cursoBO.Nome, " --");
                    }

                    if (lblNomeCurso != null)
                    {
                        lblNomeCurso2.Text = cursoBO.Nome;
                    }
                }
            }

            if (chkCoordenadorCurso != null)
            {
                chkCoordenadorCurso.Checked = professorCursoBO.CoordenadorCurso;
            }

            if (professorCursoBO.CursoNivel != null && professorCursoBO.CursoNivel.CursoNivelId > 0)
            {
                CursoNivel cursoNivelBO = new CursoNivel();
                cursoNivelBO.CursoNivelId = professorCursoBO.CursoNivel.CursoNivelId;
                cursoNivelBO = new CursoNivelBLL().Carregar(cursoNivelBO);

                if (txtNivel != null && cursoNivelBO != null && cursoNivelBO.CursoNivelId > 0)
                {
                    txtNivel.Text = cursoNivelBO.Nivel;
                }
            }

            if (txtCargo != null)
            {
                txtCargo.Text = professorCursoBO.Cargo;
            }

            IEnumerable<ProfessorDisciplina> professorDisciplinaBOIEnum = new List<ProfessorDisciplina>();
            professorDisciplinaBOIEnum = new ProfessorDisciplinaBLL().Carregar(professorCursoBO);

            if (rptProfessorDisciplina != null && professorDisciplinaBOIEnum != null && professorDisciplinaBOIEnum.Any())
            {
                rptProfessorDisciplina.DataSource = professorDisciplinaBOIEnum;
                rptProfessorDisciplina.DataBind();
            }
            else
            {
                if (divProfessorDisciplina != null)
                {
                    divProfessorDisciplina.Visible = false;
                }
            }
        }
    }

    #endregion

    #region [ ProfessorDisciplina ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="e"></param>
    private void ProfessorDisciplinaItemDataBound(RepeaterItemEventArgs e)
    {
        ProfessorDisciplina professorDisciplinaBO = (ProfessorDisciplina)e.Item.DataItem;

        if (professorDisciplinaBO != null && professorDisciplinaBO.ProfessorDisciplinaId > 0)
        {
            Literal ltrNomeDisciplina = (Literal)e.Item.FindControl("ltrNomeDisciplina");
            Literal ltrNroAlunos = (Literal)e.Item.FindControl("ltrNroAlunos");
            Literal ltrIndicaTitulo = (Literal)e.Item.FindControl("ltrIndicaTitulo");

            if (professorDisciplinaBO.Disciplina != null && professorDisciplinaBO.Disciplina.DisciplinaId > 0)
            {
                Disciplina disciplinaBO = new Disciplina();
                disciplinaBO.DisciplinaId = professorDisciplinaBO.Disciplina.DisciplinaId;
                disciplinaBO = new DisciplinaBLL().Carregar(disciplinaBO);

                if (ltrNomeDisciplina != null && disciplinaBO != null && disciplinaBO.DisciplinaId > 0)
                {
                    ltrNomeDisciplina.Text = disciplinaBO.Descricao;
                }
            }

            if (ltrIndicaTitulo != null)
            {
                ltrIndicaTitulo.Text = professorDisciplinaBO.IndicaTitulo ? "Sim" : "Não";
            }

            if (ltrNroAlunos != null)
            {
                ltrNroAlunos.Text = professorDisciplinaBO.NumeroAlunos.ToString();
            }
        }
    }

    #endregion

    #endregion
}
