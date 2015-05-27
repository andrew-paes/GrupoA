using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GrupoA.DataAccess.ADO;
using GrupoA.DataAccess;
using Ag2.Manager.Helper;
using GrupoA.BusinessObject;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject.Enumerator;

public partial class content_module_pessoa_pessoa : SmartUserControl
{

    #region Propriedades

    #region BLL
    private UsuarioBLL _usuarioBLL;
    private UsuarioBLL usuarioBLL
    {
        get
        {
            if (_usuarioBLL == null)
                _usuarioBLL = new UsuarioBLL();
            return _usuarioBLL;
        }
    }
    #endregion

    #region TituloBLL
    private TituloBLL _tituloBLL;
    private TituloBLL tituloBLL
    {
        get
        {
            if (_tituloBLL == null)
                _tituloBLL = new TituloBLL();
            return _tituloBLL;
        }
    }        

    #endregion

    #endregion

    #region Eventos

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IdRegistro > 0)
        {
            if (!IsPostBack)
                this.LoadForm();
        }
    }

    protected void LoadForm()
    {
        if (IdRegistro > 0)
        {
            int idUser = 0;
            UsuarioBLL user = new UsuarioBLL();
            idUser = user.CarregarIdUsuarioByAvaliacao(Convert.ToInt32(IdRegistro));

            Usuario usuario = new UsuarioBLL().CarregarComDependencia(new Usuario() { UsuarioId = idUser });
            usuario.UsuarioId = idUser;

            usuario = usuarioBLL.CarregarComDependencia(usuario);

            //carregamento dos dados do Usuário
            txtNome.Text = usuario.NomeUsuario;
            txtEmail.Text = usuario.EmailUsuario;
            txtCPF.Text = usuario.CadastroPessoa;

            if (usuario.Telefones.Count > 0)
            {
                dgTelefones.DataKeyField = "telefoneId";
                dgTelefones.DataSource = usuario.Telefones;
                dgTelefones.DataBind();
            }
            else
                areaTelefones.Visible = false;

            Professor professor = new ProfessorBLL().CarregarCompletoComAvaliacoes(new Professor() { ProfessorId = Convert.ToInt32(idUser) });

            int TituloIdAvaliacao = 0;
            
            // Dados da Avaliação e Titulo / Produto
            for(int i = 0;i< professor.TituloSolicitacoes.Count;i++)
            {
                for (int j = 0; j < professor.TituloSolicitacoes[i].TituloAvaliacoes.Count; j++)
                {
                    if (professor.TituloSolicitacoes[i].TituloAvaliacoes[j].TituloAvaliacaoId == Convert.ToInt32(IdRegistro))
                    {
                        TituloIdAvaliacao = professor.TituloSolicitacoes[i].Titulo.TituloId;
                        txtAvaliacao.Text = professor.TituloSolicitacoes[i].TituloAvaliacoes[j].Avaliacao;
                        txtDataAvaliacao.Text = professor.TituloSolicitacoes[i].TituloAvaliacoes[j].DataRealizacaoAvaliacao.ToString();

                        txtFinalizada.Text = professor.TituloSolicitacoes[i].TituloAvaliacoes[j].Finalizada == true ? "Sim" : "Não";
                    }
                }
            
            }

            // Dados de Produto e Titulo
            var tituloImpresso = new TituloImpressoBLL().CarregarTituloImpressoComDependenciasPorTitulo(new Titulo() { TituloId = TituloIdAvaliacao });
            txtISBN13.Text = tituloImpresso.Isbn13;
            txtNomeDoTitulo.Text = tituloImpresso.Produto.NomeProduto;

        }
    }

    #endregion

 }
