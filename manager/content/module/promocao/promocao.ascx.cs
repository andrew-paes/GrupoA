using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ag2.Manager.Helper;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;

public partial class content_module_promocao_promocao : System.Web.UI.UserControl
{
    #region Propriedades

    private PromocaoBLL _promocaoBll;
    PromocaoBLL PromocaoBll
    {
        get { return _promocaoBll ?? (_promocaoBll = new PromocaoBLL()); }
    }

    private CategoriaBLL _categoriaBll;
    CategoriaBLL CategoriaBll
    {
        get { return _categoriaBll ?? (_categoriaBll = new CategoriaBLL()); }
    }

    int IdPromocao
    {
        get { return Convert.ToInt32(hddPromocaoId.Value); }
    }

    #endregion

    #region Eventos

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void Page_Load(object sender, EventArgs e)
    {
        if (Util.GetRequestId() > 0)
        {
            hddPromocaoId.Value = Util.GetRequestId().ToString();
            if (!IsPostBack)
            {
                montaListaTipoPromocao();
                loadForm();
            }
        }
        else
            if (!IsPostBack)
                montaListaTipoPromocao();
        msg.Text = "";
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void btnExecute_Click(object sender, ImageClickEventArgs e)
    {
        saveOrUpdate();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptCatNivel1_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            Categoria categoria = (Categoria)e.Item.DataItem;
            Repeater rptCatNivel2 = (Repeater)e.Item.FindControl("rptCatNivel2");
            CheckBox cbCatNivel1 = (CheckBox)e.Item.FindControl("cbCatNivel1");

            cbCatNivel1.Text = categoria.NomeCategoria;
            cbCatNivel1.InputAttributes.Add("value", categoria.CategoriaId.ToString());

            if (categoria.Categorias != null && categoria.Categorias.Count > 0)
            {
                rptCatNivel2.DataSource = categoria.Categorias;
                rptCatNivel2.DataBind();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptCatNivel2_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            Categoria categoria = (Categoria)e.Item.DataItem;
            Repeater rptCatNivel3 = (Repeater)e.Item.FindControl("rptCatNivel3");
            CheckBox cbCatNivel2 = (CheckBox)e.Item.FindControl("cbCatNivel2");

            cbCatNivel2.Text = categoria.NomeCategoria;
            cbCatNivel2.InputAttributes.Add("value", categoria.CategoriaId.ToString());

            if (categoria.Categorias != null && categoria.Categorias.Count > 0)
            {
                rptCatNivel3.DataSource = categoria.Categorias;
                rptCatNivel3.DataBind();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptCatNivel3_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.DataItem != null)
        {
            Categoria categoria = (Categoria)e.Item.DataItem;
            CheckBox cbCatNivel3 = (CheckBox)e.Item.FindControl("cbCatNivel3");

            cbCatNivel3.Text = categoria.NomeCategoria;
            cbCatNivel3.InputAttributes.Add("value", categoria.CategoriaId.ToString());
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void gvCupom_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            PromocaoCupomPedido promocaoCupomPedido = (PromocaoCupomPedido)e.Row.DataItem;

            Label lblCupom = (Label)e.Row.FindControl("lblCupom");
            Label lblAmigavel = (Label)e.Row.FindControl("lblAmigavel");
            Label lblPedido = (Label)e.Row.FindControl("lblPedido");
            Label lblNome = (Label)e.Row.FindControl("lblNome");
            Label lblUsuario = (Label)e.Row.FindControl("lblUsuario");
            ImageButton btnEditar = (ImageButton)e.Row.FindControl("btnEditar");

            lblCupom.Text = promocaoCupomPedido.PromocaoCupom.CodigoCupom.ToString();

            if (promocaoCupomPedido.Pedido != null)
            {
                lblPedido.Text = promocaoCupomPedido.Pedido.PedidoId.ToString();
                lblNome.Text = promocaoCupomPedido.Pedido.Usuario.NomeUsuario;
                lblUsuario.Text = promocaoCupomPedido.Pedido.Usuario.UsuarioId.ToString();
            }
            else
            {
                lblPedido.Text = "-";
                lblNome.Text = "-";
                lblUsuario.Text = "-";
            }

            if (!String.IsNullOrEmpty(promocaoCupomPedido.PromocaoCupom.CodigoAmigavel))
            {
                btnEditar.CommandArgument = String.Concat(promocaoCupomPedido.PromocaoCupom.PromocaoCupomId, "|", promocaoCupomPedido.PromocaoCupom.CodigoAmigavel);
                lblAmigavel.Text = promocaoCupomPedido.PromocaoCupom.CodigoAmigavel;
            }
            else
            {
                btnEditar.CommandArgument = String.Concat(promocaoCupomPedido.PromocaoCupom.PromocaoCupomId, "|");
                lblAmigavel.Text = "-";
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEditar_Click(object sender, ImageClickEventArgs e)
    {
        String[] valores = ((ImageButton)sender).CommandArgument.ToString().Split('|');

        hddPromocaoCupomId.Value = valores[0];
        txtCogidoAmigavel.Text = valores[1];

        pnlCodigoAmigavel.Visible = true;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEditarPromocaoCupom_Click(object sender, ImageClickEventArgs e)
    {
        PromocaoCupom promocaoCupom = new PromocaoBLL().CarregarPromocaoCupom(new PromocaoCupom(Convert.ToInt32(hddPromocaoCupomId.Value)));

        if (!String.IsNullOrEmpty(txtCogidoAmigavel.Text))
        {
            promocaoCupom.CodigoAmigavel = txtCogidoAmigavel.Text;
        }
        else
        {
            promocaoCupom.CodigoAmigavel = null;
        }

        if ((new PromocaoBLL().CarregarPorCodigoAmigavel(promocaoCupom.PromocaoCupomId, promocaoCupom.CodigoAmigavel)) == null)
        {
            new PromocaoBLL().AtualizarPromocaoCupom(promocaoCupom);

            Util.ShowMessage("Cupom alterado com sucesso.", Ag2.Manager.Enumerator.typeMessage.Sucesso);

            hddPromocaoCupomId.Value = String.Empty;
            txtCogidoAmigavel.Text = String.Empty;
            pnlCodigoAmigavel.Visible = false;

            this.CarregarCupons();
        }
        else
        {
            Util.ShowMessage("Código amigável já está sendo utilizado por outro cupom.", Ag2.Manager.Enumerator.typeMessage.Erro);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rblPromocaoTipo_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rblPromocaoTipo.SelectedValue.Equals("1")) //
        {
            pnlValorDesconto.Visible = false;
            pnlPercentualDesconto.Visible = true;
        }
        else
        {
            pnlPercentualDesconto.Visible = false;
            pnlValorDesconto.Visible = true;
        }
    }

    /// <summary>
    /// Método para controlar a troca entre os níveis de restrição. Para auxiliar, é utilizado um campo
    /// oculto (hddNivelRestricaoAnterior) para controle da exibição dos painéis.
    /// </summary>
    /// <param name="sender">Default</param>
    /// <param name="e">Default</param>
    protected void rblNivelRestricao_SelectedIndexChanged(object sender, EventArgs e)
    {
        string opcao = rblNivelRestricao.SelectedValue.ToString();
        // Analisa qual a opção anterior
        string opcaoAnterior = hddNivelRestricaoAnterior.Value;
        // Efetua as operações de exclusão conforme necessário e esconde o painel
        if (opcaoAnterior.Equals("USUARIO"))
        {
            pnlRestricaoNivelUsuario.Visible = false;
            PromocaoBll.ExcluirTodasRestricoesPorUsuario(new Promocao() { PromocaoId = IdPromocao });
        }
        else if (opcaoAnterior.Equals("PERFIL"))
        {
            pnlRestricaoNivelPerfil.Visible = false;
            PromocaoBll.ExcluirTodasRestricoesPorPerfil(new Promocao() { PromocaoId = IdPromocao });
        }
        else if (opcaoAnterior.Equals("REVISTA"))
        {
            pnlRestricaoNivelRevista.Visible = false;
            PromocaoBll.ExcluirRevistasPorPromocao(new Promocao() { PromocaoId = IdPromocao });
        }
        // Mostra o novo painel
        if (opcao.Equals("USUARIO"))
        {
            pnlRestricaoNivelUsuario.Visible = true;
            this.carregarGridUsuariosAdicionados();
        }
        else if (opcao.Equals("PERFIL"))
        {
            pnlRestricaoNivelPerfil.Visible = true;
            this.carregarGridPerfisAdicionados();
            this.carregarGridPerfisAindaNaoAdicionados();
        }
        else if (opcao.Equals("REVISTA"))
        {
            pnlRestricaoNivelRevista.Visible = true;
            this.carregarGridRevistasAdicionadas();
            this.carregarGridRevistasAindaNaoAdicionados();
        }
        // Atualiza o campo escondido
        hddNivelRestricaoAnterior.Value = opcao;
    }

    #region Usuários

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPesquisarUsuario_Click(object sender, EventArgs e)
    {
        this.carregarUsuariosPorCPF();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgUsuariosAdicionados_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "DELETE")
        {
            Promocao promocao = new Promocao() { PromocaoId = IdPromocao };
            Usuario usuario = new Usuario() { UsuarioId = Convert.ToInt32(e.CommandArgument) };
            PromocaoBll.ExcluirRestricaoPorUsuario(promocao, usuario);
            this.carregarGridUsuariosAdicionados();
            if (pnlUsuariosEncontrados.Visible)
                this.carregarUsuariosPorCPF();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgUsuariosEncontrados_EditCommand(object source, DataGridCommandEventArgs e)
    {
        // Inserção de uma restrição entre a promoção e um usuário
        Promocao promocao = new Promocao() { PromocaoId = IdPromocao };
        Usuario usuario = new Usuario() { UsuarioId = Convert.ToInt32(e.CommandArgument) };
        PromocaoBll.IncluirRestricaoPorUsuario(promocao, usuario);
        this.carregarGridUsuariosAdicionados();
        this.carregarUsuariosPorCPF();
    }

    #endregion

    #region Perfis

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgPerfisAdicionados_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "DELETE")
        {
            Promocao promocao = new Promocao() { PromocaoId = IdPromocao };
            Perfil perfil = new Perfil() { PerfilId = Convert.ToInt32(e.CommandArgument) };
            PromocaoBll.ExcluirRestricaoPorPerfil(promocao, perfil);
            this.carregarGridPerfisAdicionados();
            this.carregarGridPerfisAindaNaoAdicionados();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgPerfisNaoAdicionados_EditCommand(object source, DataGridCommandEventArgs e)
    {
        // Inserção de uma restrição entre a promoção e um usuário
        Promocao promocao = new Promocao() { PromocaoId = IdPromocao };
        Perfil perfil = new Perfil() { PerfilId = Convert.ToInt32(e.CommandArgument) };
        PromocaoBll.IncluirRestricaoPorPerfil(promocao, perfil);
        this.carregarGridPerfisAdicionados();
        this.carregarGridPerfisAindaNaoAdicionados();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Sender"></param>
    /// <param name="e"></param>
    protected void grids_ItemCreated(Object Sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            ImageButton _myButton = (ImageButton)e.Item.FindControl("btnInserir");
            _myButton.Attributes.Add("onclick", "return confirm('Deseja realmente excluir o item selecionado?');");
        }
    }

    #endregion

    #region Revistas

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgRevistasAdicionadas_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "DELETE")
        {
            Promocao promocao = new Promocao() { PromocaoId = IdPromocao };
            Revista revista = new Revista() { RevistaId = Convert.ToInt32(e.CommandArgument) };
            PromocaoBll.ExcluirPromocaoRevista(promocao, revista);
            this.carregarGridRevistasAdicionadas();
            this.carregarGridRevistasAindaNaoAdicionados();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgRevistasNaoAdicionadas_EditCommand(object source, DataGridCommandEventArgs e)
    {
        // Inserção de uma restrição entre a promoção e um usuário
        Promocao promocao = new Promocao() { PromocaoId = IdPromocao };
        Revista revista = new Revista() { RevistaId = Convert.ToInt32(e.CommandArgument) };
        PromocaoBll.InserirPromocaoRevista(promocao, revista);
        this.carregarGridRevistasAdicionadas();
        this.carregarGridRevistasAindaNaoAdicionados();
    }

    #endregion

    #region Produtos

    /// <summary>
    /// Carrega todos os produtos conforme o EAN13.
    /// IMPORTANTE: Só serão carregados os produtos que ainda não contiverem ligação com as restrições
    ///             anteriores da promoção.
    /// </summary>
    protected void carregarProdutosPorISBN13()
    {
        List<Produto> produtos = new List<Produto>();

        if (dgProdutosAdicionados.DataKeys.Count > 0)
        {
            foreach (int codigo in dgProdutosAdicionados.DataKeys)
            {
                produtos.Add(new Produto() { ProdutoId = codigo });
            }
        }

        produtos = PromocaoBll.CarregarProdutosPorIsbn13ExcetoProdutos(txtISBN13.Text, produtos);

        dgProdutosEncontrados.DataSource = produtos;
        dgProdutosEncontrados.DataKeyField = "produtoId";
        dgProdutosEncontrados.DataBind();

        pnlProdutosEncontrados.Visible = true;

        if (produtos.Count > 0)
        {
            lblTextoPesquisaProdutos.Visible = false;
            dgProdutosEncontrados.Visible = true;
        }
        else
        {
            lblTextoPesquisaProdutos.Visible = true;
            dgProdutosEncontrados.Visible = false;
        }
    }

    /// <summary>
    /// Carrega uma listagem dos produtos que devem ter restrição adicionado a Promoção
    /// </summary>
    protected void carregarGridProdutosAdicionados()
    {
        Promocao promocao = new Promocao() { PromocaoId = IdPromocao };
        List<Produto> produtos = PromocaoBll.CarregarProdutosPorPromocao(promocao);
        dgProdutosAdicionados.DataSource = produtos;
        dgProdutosAdicionados.DataKeyField = "produtoId";
        dgProdutosAdicionados.DataBind();
        if (produtos.Count > 0)
        {
            dgProdutosAdicionados.Visible = true;
            lblTextoProdutosAdicionados.Visible = false;
        }
        else
        {
            dgProdutosAdicionados.Visible = false;
            lblTextoProdutosAdicionados.Visible = true;
        }
    }

    #endregion

    #region ProdutoTipos

    /// <summary>
    /// Carrega todos os produtoTipos
    /// </summary>
    protected void carregarProdutoTipos()
    {
        List<ProdutoTipo> produtoTipos = new List<ProdutoTipo>();

        // Busca os usuários já inseridos no grid de usuários já adicionados
        if (dgProdutoTiposAdicionados.DataKeys.Count > 0)
            foreach (int codigo in dgProdutoTiposAdicionados.DataKeys)
                produtoTipos.Add(new ProdutoTipo() { ProdutoTipoId = codigo });
        // Lista todos os usuários que ainda não tiverem ligação com a Promoção
        produtoTipos = PromocaoBll.CarregarTodosProdutoTiposExcetoProdutoTipos(produtoTipos);

        dgProdutoTiposEncontrados.DataSource = produtoTipos;
        dgProdutoTiposEncontrados.DataKeyField = "produtoTipoId";
        dgProdutoTiposEncontrados.DataBind();

        pnlProdutoTiposEncontrados.Visible = true;

        if (produtoTipos.Count > 0)
        {
            lblTextoPesquisaProdutoTipos.Visible = false;
            dgProdutoTiposEncontrados.Visible = true;
        }
        else
        {
            lblTextoPesquisaProdutoTipos.Visible = true;
            dgProdutoTiposEncontrados.Visible = false;
        }
    }

    /// <summary>
    /// Carrega uma listagem dos produtoTipos que devem ter restrição adicionado a Promoção
    /// </summary>
    protected void carregarGridProdutoTiposAdicionados()
    {
        Promocao promocao = new Promocao() { PromocaoId = IdPromocao };
        List<ProdutoTipo> produtoTipos = PromocaoBll.CarregarTiposDeProdutoPorPromocao(promocao);
        dgProdutoTiposAdicionados.DataSource = produtoTipos;
        dgProdutoTiposAdicionados.DataKeyField = "produtoTipoId";
        dgProdutoTiposAdicionados.DataBind();
        if (produtoTipos.Count > 0)
        {
            dgProdutoTiposAdicionados.Visible = true;
            lblTextoProdutoTiposAdicionados.Visible = false;
        }
        else
        {
            dgProdutoTiposAdicionados.Visible = false;
            lblTextoProdutoTiposAdicionados.Visible = true;
        }
    }

    #endregion

    #region Produto

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnPesquisarProduto_Click(object sender, EventArgs e)
    {
        this.carregarProdutosPorISBN13();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgProdutosAdicionados_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "DELETE")
        {
            Promocao promocao = new Promocao() { PromocaoId = IdPromocao };
            Produto produto = new Produto() { ProdutoId = Convert.ToInt32(e.CommandArgument) };
            PromocaoBll.ExcluirRestricaoPorProduto(promocao, produto);
            this.carregarGridProdutosAdicionados();
            if (pnlProdutosEncontrados.Visible)
                this.carregarProdutosPorISBN13();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgProdutosEncontrados_EditCommand(object source, DataGridCommandEventArgs e)
    {
        // Inserção de uma restrição entre a promoção e um usuário
        Promocao promocao = new Promocao() { PromocaoId = IdPromocao };
        Produto produto = new Produto() { ProdutoId = Convert.ToInt32(e.CommandArgument) };
        PromocaoBll.IncluirRestricaoPorProduto(promocao, produto);
        this.carregarGridProdutosAdicionados();
        this.carregarProdutosPorISBN13();
    }

    #endregion

    #region ProdutoTipo

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgProdutoTiposAdicionados_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "DELETE")
        {
            Promocao promocao = new Promocao() { PromocaoId = IdPromocao };
            ProdutoTipo produtoTipo = new ProdutoTipo() { ProdutoTipoId = Convert.ToInt32(e.CommandArgument) };
            PromocaoBll.ExcluirRestricaoPorTipoDeProduto(promocao, produtoTipo);
            this.carregarGridProdutoTiposAdicionados();
            if (pnlProdutoTiposEncontrados.Visible)
                this.carregarProdutoTipos();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgProdutoTiposEncontrados_EditCommand(object source, DataGridCommandEventArgs e)
    {
        // Inserção de uma restrição entre a promoção e um usuário
        Promocao promocao = new Promocao() { PromocaoId = IdPromocao };
        ProdutoTipo produtoTipo = new ProdutoTipo() { ProdutoTipoId = Convert.ToInt32(e.CommandArgument) };
        PromocaoBll.IncluirRestricaoPorTipoDeProduto(promocao, produtoTipo);
        this.carregarGridProdutoTiposAdicionados();
        this.carregarProdutoTipos();
    }

    #endregion

    #region Categoria
    /// <summary>
    /// Método para controlar a troca entre os tipos de restrição. Para auxiliar, é utilizado um campo
    /// oculto (hddNivelRestricaoTipoAnterior) para controle da exibição dos painéis.
    /// </summary>
    /// <param name="sender">Default</param>
    /// <param name="e">Default</param>
    protected void rblTipoRestricao_SelectedIndexChanged(object sender, EventArgs e)
    {
        string opcao = rblTipoRestricao.SelectedValue.ToString();
        // Analisa qual a opção anterior
        string opcaoAnterior = hddNivelRestricaoTipoAnterior.Value;
        // Efetua as operações de exclusão conforme necessário e esconde o painel
        if (opcaoAnterior.Equals("PRODUTO"))
        {
            pnlRestricaoProduto.Visible = false;
            PromocaoBll.ExcluirTodasRestricoesPorProduto(new Promocao() { PromocaoId = IdPromocao });
            txtISBN13.Text = string.Empty;
            pnlProdutosEncontrados.Visible = false;
        }
        else if (opcaoAnterior.Equals("TIPOPRODUTO"))
        {
            pnlRestricaoProdutoTipo.Visible = false;
            PromocaoBll.ExcluirRestricoesPorTipoDeProduto(new Promocao() { PromocaoId = IdPromocao });
        }
        else if (opcaoAnterior.Equals("CATEGORIA"))
        {
            pnlRestricaoCategoria.Visible = false;
            PromocaoBll.ExcluirTodasRestricoesPorCategoria(new Promocao() { PromocaoId = IdPromocao });
        }
        // Mostra o novo painel
        if (opcao.Equals("PRODUTO"))
        {
            pnlRestricaoProduto.Visible = true;
            this.carregarGridProdutosAdicionados();
        }
        else if (opcao.Equals("TIPOPRODUTO"))
        {
            pnlRestricaoProdutoTipo.Visible = true;
            this.carregarGridProdutoTiposAdicionados();
            this.carregarProdutoTipos();
        }
        else if (opcao.Equals("CATEGORIA"))
        {
            pnlRestricaoCategoria.Visible = true;
            this.CarregarCategorias();
        }
        // Atualiza o campo escondido
        hddNivelRestricaoTipoAnterior.Value = opcao;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAtualizarCategorias_Click(object sender, ImageClickEventArgs e)
    {
        SalvarCategorias();
    }

    #endregion

    #region Faixas de Valor

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInserirFaixa_Click(object sender, ImageClickEventArgs e)
    {
        if (Page.IsValid)
        {
            this.InserirPromocaoFaixa();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="Sender"></param>
    /// <param name="e"></param>
    protected void dgFaixasPromocao_ItemDataBound(Object Sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            PromocaoFaixa promocaoFaixa = (PromocaoFaixa)e.Item.DataItem;
            Label labelFreteGratisStr = (Label)e.Item.FindControl("lblFreteGratisStr");
            labelFreteGratisStr.Text = (promocaoFaixa.FreteGratis ? "Sim" : "Não");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="e"></param>
    protected void dgFaixasPromocao_DeleteCommand(object source, DataGridCommandEventArgs e)
    {
        if (e.CommandName.ToUpper() == "DELETE")
        {
            PromocaoFaixa promocaoFaixa = new PromocaoFaixa()
            {
                PromocaoFaixaId = Convert.ToInt32(e.
                    CommandArgument)
            };
            PromocaoBll.ExcluirFaixaDaPromocao(promocaoFaixa);
            this.CarregarFaixasPromocao();
        }
    }

    #endregion

    #endregion

    #region Métodos

    /// <summary>
    /// Popula a lista de radios conforme os tipos de promoção
    /// </summary>
    protected void montaListaTipoPromocao()
    {
        rblPromocaoTipo.DataSource = PromocaoBll.CarregarTiposDePromocoes();
        rblPromocaoTipo.DataTextField = "tipoPromocao";
        rblPromocaoTipo.DataValueField = "promocaoTipoId";
        rblPromocaoTipo.DataBind();
    }

    /// <summary>
    /// Salva ou atualiza as informações após validar a página atual.
    /// A informação entre salvar ou atualizar será feita com base no 
    /// campo oculto "hddPromocaoId".
    /// </summary>
    public void saveOrUpdate()
    {
        if (Page.IsValid)
        {
            Promocao promocao = new Promocao();
            promocao.OrigemSistema = false;
            promocao.PromocaoId = Convert.ToInt32(hddPromocaoId.Value);
            promocao.NomePromocao = txtNome.Text;
            promocao.DataHoraInicio = Convert.ToDateTime(txtDataPromocaoInicio.Text);
            promocao.DataHoraFim = Convert.ToDateTime(txtDataPromocaoFim.Text);
            promocao.DescricaoPromocao = txtDescricao.Text;
            promocao.Reutilizavel = chkReutilizavel.Checked;
            promocao.Ativa = chkAtivo.Checked;
            promocao.PromocaoTipo = new PromocaoTipo() { PromocaoTipoId = Convert.ToInt32(rblPromocaoTipo.SelectedValue) };
            if (hddPromocaoId.Value == "0")
            {
                if (!promocao.Reutilizavel)
                {
                    promocao.NumeroMaximoCupom = Convert.ToInt32(txtNumeroMaximoCupom.Text);
                }
                else
                {
                    promocao.NumeroMaximoCupom = 1;
                    promocao.AplicaAutomaticamente = chkAutomatica.Checked;
                }

                promocao.NumeroMaximoCupomDif = promocao.NumeroMaximoCupom;

                PromocaoBll.InserirPromocao(promocao);

                Ag2.Security.SecureQueryString sqGrid = new Ag2.Security.SecureQueryString();
                sqGrid["md"] = Util.GetQueryString("md");
                sqGrid["id"] = promocao.PromocaoId.ToString();
                sqGrid["origem"] = "insert";

                Response.Redirect(string.Format("~/content/edit.aspx?q={0}", sqGrid.ToString()));
            }
            else
            {
                if (!promocao.Reutilizavel)
                {
                    if (hddNumeroMaximoCupom.Value != "")
                    {
                        promocao.NumeroMaximoCupomDif = Convert.ToInt32(txtNumeroMaximoCupom.Text) - Convert.ToInt32(hddNumeroMaximoCupom.Value);
                        promocao.NumeroMaximoCupom = Convert.ToInt32(txtNumeroMaximoCupom.Text);
                    }
                    else
                    {
                        promocao.NumeroMaximoCupom = Convert.ToInt32(txtNumeroMaximoCupom.Text);
                        promocao.NumeroMaximoCupomDif = promocao.NumeroMaximoCupom;
                    }
                }
                else
                {
                    promocao.NumeroMaximoCupom = 1;
                    promocao.NumeroMaximoCupomDif = 0;
                    promocao.AplicaAutomaticamente = chkAutomatica.Checked;
                }

                PromocaoBll.AtualizarPromocao(promocao);
                // Envia atualização à todos os usuários que contém alerta desse promocao
                hddNumeroMaximoCupom.Value = promocao.NumeroMaximoCupom.ToString();

                Util.ShowUpdateMessage();
            }
        }
    }

    /// <summary>
    /// Carrega o formulário conforme o código contido no campo
    /// oculto "hddPromocaoId"
    /// </summary>
    public void loadForm()
    {
        //// Busca dados
        Promocao promocao = new Promocao();
        promocao.PromocaoId = int.Parse(hddPromocaoId.Value);
        promocao = PromocaoBll.CarregarPromocaoComTipo(promocao);
        txtNome.Text = promocao.NomePromocao;
        txtDataPromocaoInicio.Text = promocao.DataHoraInicio.ToString("dd/MM/yyyy");
        txtDataPromocaoFim.Text = promocao.DataHoraFim.ToString("dd/MM/yyyy");
        txtDescricao.Text = promocao.DescricaoPromocao;

        if (!promocao.Reutilizavel)
        {
            txtNumeroMaximoCupom.Text = promocao.NumeroMaximoCupom.ToString();
            hddNumeroMaximoCupom.Value = promocao.NumeroMaximoCupom.ToString();
            divAutomatica.Visible = false;
        }
        else
        {
            divAutomatica.Style.Clear();
            divQuantidadeCupons.Visible = false;
            chkAutomatica.Checked = promocao.AplicaAutomaticamente;
        }

        chkReutilizavel.Checked = promocao.Reutilizavel;
        chkReutilizavel.Enabled = false;
        chkAtivo.Checked = promocao.Ativa;
        rblPromocaoTipo.SelectedValue = promocao.PromocaoTipo.PromocaoTipoId.ToString();
        this.rblPromocaoTipo_SelectedIndexChanged(null, null);
        pnlNivelRestricao.Visible = true;
        pnlTipoRestricao.Visible = true;
        CarregarFaixasPromocao();

        //hlExibirCupom.NavigateUrl = "~/content/module/promocao/listaCupom.aspx?promocaoId=" + promocao.PromocaoId.ToString();
        //hlExibirCupom.Visible = true;

        promocao.Usuarios = PromocaoBll.CarregarUsuariosPorPromocao(promocao);
        if (promocao.Usuarios != null && promocao.Usuarios.Count > 0)
        {
            rblNivelRestricao.SelectedValue = "USUARIO";
            rblNivelRestricao_SelectedIndexChanged(null, null);
        }
        else
        {
            promocao.Perfis = PromocaoBll.CarregarPerfisPorPromocao(promocao);
            if (promocao.Perfis != null && promocao.Perfis.Count > 0)
            {
                rblNivelRestricao.SelectedValue = "PERFIL";
                rblNivelRestricao_SelectedIndexChanged(null, null);
            }
            else
            {
                promocao.Revistas = PromocaoBll.CarregarRevistasPorPromocao(promocao);
                if (promocao.Revistas != null && promocao.Revistas.Count > 0)
                {
                    rblNivelRestricao.SelectedValue = "REVISTA";
                    rblNivelRestricao_SelectedIndexChanged(null, null);
                }
            }
        }

        promocao.Categorias = PromocaoBll.CarregarCategoriasPorPromocao(promocao);
        if (promocao.Categorias != null && promocao.Categorias.Count > 0)
        {
            rblTipoRestricao.SelectedValue = "CATEGORIA";
            rblTipoRestricao_SelectedIndexChanged(null, null);
        }

        else
        {
            promocao.Produtos = PromocaoBll.CarregarProdutosPorPromocao(promocao);
            if (promocao.Produtos != null && promocao.Produtos.Count > 0)
            {
                rblTipoRestricao.SelectedValue = "PRODUTO";
                rblTipoRestricao_SelectedIndexChanged(null, null);
            }
            else
            {
                promocao.ProdutoTipos = PromocaoBll.CarregarTiposDeProdutoPorPromocao(promocao);
                if (promocao.ProdutoTipos != null && promocao.ProdutoTipos.Count > 0)
                {
                    rblTipoRestricao.SelectedValue = "TIPOPRODUTO";
                    rblTipoRestricao_SelectedIndexChanged(null, null);
                }

            }
        }

        this.CarregarCupons();

        if (Util.GetQueryString("origem") == "insert")
        {
            Util.ShowInsertMessage();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarCupons()
    {
        divCupom.Visible = true;

        gvCupom.DataSource = new PromocaoBLL().CarregarPromocaoCupomPedidoPorPromocao(new Promocao(Util.GetRequestId()));
        gvCupom.DataBind();
    }

    /// <summary>
    /// Carrega todas as categorias para montar a treeview. Neste método só serão carregadas as categorias que
    /// são áreas de conhecimento (pai = null).
    /// </summary>
    protected void CarregarCategorias()
    {
        this.lblCategorias.Visible = true;

        List<Categoria> categorias = new CategoriaBLL().CarregarCategoriasComFilhos();

        rptCatNivel1.DataSource = categorias;
        rptCatNivel1.DataBind();

        hddCategorias.Value = new PromocaoBLL().CarregarCategoriasSelecionadasPorPromocao(IdPromocao).ToString();
    }

    /// <summary>
    /// Carrega os nodos filho da treview de Categorias. Deve ser passado o nodo pai para que os filhos sejam inseridos,
    /// a categoria para pesquisar quais são seus filhos e as categorias que devem ser selecionadas.
    /// </summary>
    /// <param name="nodoPai"></param>
    /// <param name="categoria"></param>
    /// <param name="categoriasSelecionadas"></param>
    protected void CarregarCategoriasFilhas(TreeNode nodoPai, Categoria categoria, List<int> categoriasSelecionadas)
    {
        // Busca os filhos da categoria
        List<Categoria> categoriasFilhas = CategoriaBll.CarregarCategoriasFilhas(categoria);

        if (categoriasFilhas.Count > 0)
        {
            foreach (Categoria _categoria in categoriasFilhas)
            {
                // Cria um novo nodo
                TreeNode nodo = new TreeNode();
                nodo.Value = _categoria.CategoriaId.ToString();
                nodo.Text = _categoria.NomeCategoria;
                nodo.Expanded = false;

                // Seleciona se necessário
                if (categoriasSelecionadas.Contains(_categoria.CategoriaId))
                    nodo.Checked = true;

                // Popular os filhos recursivamente
                CarregarCategoriasFilhas(nodo, _categoria, categoriasSelecionadas);

                // Adiciona o nodo
                nodoPai.ChildNodes.Add(nodo);
            }
        }
    }

    /// <summary>
    /// Salva todas as categorias selecionadas na treeview. Essa função salva 
    /// somente as categorias que estão na raiz da árvore.
    /// </summary>
    protected void SalvarCategorias()
    {
        List<Categoria> categorias = new List<Categoria>();

        // Deve excluir todas as categorias
        PromocaoBll.ExcluirTodasRestricoesPorCategoria(new Promocao() { PromocaoId = IdPromocao });

        for (int countNivel1 = 0; countNivel1 < rptCatNivel1.Items.Count; countNivel1++)
        {
            CheckBox cbCatNivel1 = (CheckBox)rptCatNivel1.Items[countNivel1].FindControl("cbCatNivel1");
            if (cbCatNivel1.Checked)
            {
                Categoria categoria = new Categoria();
                categoria.CategoriaId = Convert.ToInt32(cbCatNivel1.InputAttributes["value"].ToString());
                categorias.Add(categoria);
            }

            Repeater rptCatNivel2 = (Repeater)rptCatNivel1.Items[countNivel1].FindControl("rptCatNivel2");

            for (int countNivel2 = 0; countNivel2 < rptCatNivel2.Items.Count; countNivel2++)
            {
                CheckBox cbCatNivel2 = (CheckBox)rptCatNivel2.Items[countNivel2].FindControl("cbCatNivel2");
                if (cbCatNivel2.Checked)
                {
                    Categoria categoria = new Categoria();
                    categoria.CategoriaId = Convert.ToInt32(cbCatNivel2.InputAttributes["value"].ToString());
                    categorias.Add(categoria);
                }

                Repeater rptCatNivel3 = (Repeater)rptCatNivel2.Items[countNivel2].FindControl("rptCatNivel3");

                for (int countNivel3 = 0; countNivel3 < rptCatNivel3.Items.Count; countNivel3++)
                {
                    CheckBox cbCatNivel3 = (CheckBox)rptCatNivel3.Items[countNivel3].FindControl("cbCatNivel3");
                    if (cbCatNivel3.Checked)
                    {
                        Categoria categoria = new Categoria();
                        categoria.CategoriaId = Convert.ToInt32(cbCatNivel3.InputAttributes["value"].ToString());
                        categorias.Add(categoria);
                    }
                }
            }
        }

        // Para cada nodo deve salvar se estiver marcado
        foreach (Categoria categoria in categorias)
        {
            //if (nodo.Checked)
            PromocaoBll.IncluirRestricaoPorCategoria(
                new Promocao() { PromocaoId = IdPromocao },
                new Categoria() { CategoriaId = categoria.CategoriaId });
            // Se tiver nodos filhos deve salvar recursivamente
            //if (nodo.ChildNodes.Count > 0)
            //    this.SalvarCategoriasFilhas(nodo);
        }
    }

    #region Usuários

    /// <summary>
    /// Carrega todos os ususários conforme o CPF/CNPJ preenchido no campo de cadastro de pessoa.
    /// IMPORTANTE: Só serão carregados os usuários que ainda não contiverem ligação com as restrições
    ///             anteriores da promoção.
    /// </summary>
    protected void carregarUsuariosPorCPF()
    {
        List<Usuario> usuarios = new List<Usuario>();

        // Busca os usuários já inseridos no grid de usuários já adicionados
        if (dgUsuariosAdicionados.DataKeys.Count > 0)
            foreach (int codigo in dgUsuariosAdicionados.DataKeys)
                usuarios.Add(new Usuario() { UsuarioId = codigo });
        // Lista todos os usuários que ainda não tiverem ligação com a Promoção
        usuarios = PromocaoBll.CarregarTodosUsuariosPorCadastroPessoaExcetoUsuarios(new Usuario() { CadastroPessoa = txtCadastroPessoa.Text }, usuarios);

        dgUsuariosEncontrados.DataSource = usuarios;
        dgUsuariosEncontrados.DataKeyField = "usuarioId";
        dgUsuariosEncontrados.DataBind();

        pnlUsuariosEncontrados.Visible = true;

        if (usuarios.Count > 0)
        {
            lblTextoPesquisaUsuarios.Visible = false;
            dgUsuariosEncontrados.Visible = true;
        }
        else
        {
            lblTextoPesquisaUsuarios.Visible = true;
            dgUsuariosEncontrados.Visible = false;
        }
    }

    /// <summary>
    /// Carrega uma listagem dos usuários que devem ter restrição adicionado a Promoção
    /// </summary>
    protected void carregarGridUsuariosAdicionados()
    {
        Promocao promocao = new Promocao() { PromocaoId = IdPromocao };
        List<Usuario> usuarios = PromocaoBll.CarregarUsuariosPorPromocao(promocao);
        dgUsuariosAdicionados.DataSource = usuarios;
        dgUsuariosAdicionados.DataKeyField = "usuarioId";
        dgUsuariosAdicionados.DataBind();
        if (usuarios.Count > 0)
        {
            dgUsuariosAdicionados.Visible = true;
            lblTextoUsuariosAdicionados.Visible = false;
        }
        else
        {
            lblTextoUsuariosAdicionados.Visible = true;
            dgUsuariosAdicionados.Visible = false;
        }
    }

    #endregion

    #region Perfis

    /// <summary>
    /// Carrega todos os perfis que ainda não foram ligados à uma Promoção.
    /// </summary>
    protected void carregarGridPerfisAindaNaoAdicionados()
    {
        List<Perfil> perfis = new List<Perfil>();

        // Busca os usuários já inseridos no grid de usuários já adicionados
        if (dgPerfisAdicionados.DataKeys.Count > 0)
            foreach (int codigo in dgPerfisAdicionados.DataKeys)
                perfis.Add(new Perfil() { PerfilId = codigo });
        // Lista todos os usuários que ainda não tiverem ligação com a Promoção
        perfis = PromocaoBll.CarregarTodosPerfisExcetoPerfis(perfis);
        dgPerfisNaoAdicionados.DataSource = perfis;
        dgPerfisNaoAdicionados.DataKeyField = "PerfilId";
        dgPerfisNaoAdicionados.DataBind();
        if (perfis.Count > 0)
        {
            //lblUsuarioNaoEncontrado.Visible = false;
            dgPerfisNaoAdicionados.Visible = true;
            lblTextoPerfis.Text = "Selecione os perfis a serem restritos a Promoção.";
        }
        else
        {
            dgPerfisNaoAdicionados.Visible = false;
            lblTextoPerfis.Text = "Todos os perfis possíveis já foram adicionados.";
        }
    }

    /// <summary>
    /// Carrega uma listagem dos usuários que devem ter restrição adicionado a Promoção
    /// </summary>
    protected void carregarGridPerfisAdicionados()
    {
        Promocao promocao = new Promocao() { PromocaoId = IdPromocao };
        List<Perfil> perfis = PromocaoBll.CarregarPerfisPorPromocao(promocao);
        dgPerfisAdicionados.DataSource = perfis;
        dgPerfisAdicionados.DataKeyField = "PerfilId";
        dgPerfisAdicionados.DataBind();
        if (perfis.Count > 0)
        {
            dgPerfisAdicionados.Visible = true;
            lblTextoPerfisAdicionados.Text = "Nenhum perfil adicionado.";
            lblTextoPerfisAdicionados.Visible = false;
        }
        else
        {
            lblTextoPerfisAdicionados.Text = "Nenhum perfil adicionado.";
            lblTextoPerfisAdicionados.Visible = true;
            dgPerfisAdicionados.Visible = false;
        }
    }

    #endregion

    #region Revistas

    /// <summary>
    /// Carrega todos os perfis que ainda não foram ligados à uma Promoção.
    /// </summary>
    protected void carregarGridRevistasAindaNaoAdicionados()
    {
        List<Revista> revistas = new List<Revista>();

        // Busca os usuários já inseridos no grid de usuários já adicionados
        if (dgRevistasAdicionadas.DataKeys.Count > 0)
            foreach (int codigo in dgRevistasAdicionadas.DataKeys)
                revistas.Add(new Revista() { RevistaId = codigo });
        // Lista todos os usuários que ainda não tiverem ligação com a Promoção
        revistas = PromocaoBll.CarregarTodasRevistasExcetoRevistas(revistas);
        dgRevistasNaoAdicionadas.DataSource = revistas;
        dgRevistasNaoAdicionadas.DataKeyField = "revistaId";
        dgRevistasNaoAdicionadas.DataBind();
        if (revistas.Count > 0)
        {
            dgRevistasNaoAdicionadas.Visible = true;
            lblTextoRevistas.Text = "Selecione as revistas a serem restritas a Promoção.";
        }
        else
        {
            dgRevistasNaoAdicionadas.Visible = false;
            lblTextoRevistas.Text = "Todas as revistas possíveis já foram adicionadas.";
        }
    }

    /// <summary>
    /// Carrega uma listagem dos usuários que devem ter restrição adicionado a Promoção
    /// </summary>
    protected void carregarGridRevistasAdicionadas()
    {
        Promocao promocao = new Promocao() { PromocaoId = IdPromocao };
        List<Revista> revistas = PromocaoBll.CarregarRevistasPorPromocao(promocao);
        dgRevistasAdicionadas.DataSource = revistas;
        dgRevistasAdicionadas.DataKeyField = "revistaId";
        dgRevistasAdicionadas.DataBind();
        if (revistas.Count > 0)
        {
            dgRevistasAdicionadas.Visible = true;
            lblTextoRevistasAdicionadas.Text = "Nenhuma revista adicionada.";
            lblTextoRevistasAdicionadas.Visible = false;
        }
        else
        {
            lblTextoRevistasAdicionadas.Text = "Nenhuma revista adicionada.";
            lblTextoRevistasAdicionadas.Visible = true;
            dgRevistasAdicionadas.Visible = false;
        }
    }

    #endregion

    #region Faixas de Valor

    /// <summary>
    /// Insere uma nova faixa de Promoção
    /// </summary>
    public void InserirPromocaoFaixa()
    {
        // Carrega os campos
        PromocaoFaixa promocaoFaixa = new PromocaoFaixa();
        promocaoFaixa.ValorMinimo = decimal.Parse(txtValorMinimo.Text);
        if (!string.IsNullOrEmpty(txtValorDesconto.Text))
        {
            promocaoFaixa.ValorDesconto = decimal.Parse(txtValorDesconto.Text);
        }
        if (!string.IsNullOrEmpty(txtPercentualDesconto.Text))
        {
            promocaoFaixa.PercentualDesconto = decimal.Parse(txtPercentualDesconto.Text);
        }

        promocaoFaixa.FreteGratis = chkFreteGratis.Checked;
        promocaoFaixa.Promocao = new Promocao() { PromocaoId = IdPromocao };
        // Insere na BLL
        PromocaoBll.InserirFaixaDaPromocao(promocaoFaixa);
        // Limpa os campos de inserção
        txtValorMinimo.Text = string.Empty;
        txtValorDesconto.Text = string.Empty;
        txtPercentualDesconto.Text = string.Empty;
        chkFreteGratis.Checked = false;
        // Mensagem
        Util.ShowInsertMessage();
        // Carrega novamente o grid de faixas de promoção
        CarregarFaixasPromocao();
    }

    /// <summary>
    /// Carrega as faixas da promoção.
    /// </summary>
    public void CarregarFaixasPromocao()
    {
        pnlFaixasPromocao.Visible = true;
        List<PromocaoFaixa> faixas = PromocaoBll.CarregarFaixasDaPromocao(new Promocao() { PromocaoId = IdPromocao });
        if (faixas.Count > 0)
        {
            dgFaixasPromocao.Visible = true;
            dgFaixasPromocao.DataSource = faixas;
            dgFaixasPromocao.DataKeyField = "promocaoFaixaId";
            dgFaixasPromocao.DataBind();
        }
        else
        {
            dgFaixasPromocao.Visible = false;
            lblNenhumaFaixa.Visible = true;
        }
    }

    #endregion

    #endregion

    #region Validações

    /// <summary>
    /// Validação do intervalo de Datas da Promocao
    /// </summary>
    /// <param name="source">Padrão</param>
    /// <param name="args">Padrão</param>
    protected void cvValidarDatasPromocao_ServerValidate(object source, ServerValidateEventArgs args)
    {
        try
        {
            if ((txtDataPromocaoInicio.Text.Length == 0) && (txtDataPromocaoInicio.Text.Length == 0))
            {
                args.IsValid = true;
                return;
            }
            DateTime dtIni = DateTime.Parse(txtDataPromocaoInicio.Text);
            DateTime dtFim = DateTime.Parse(txtDataPromocaoFim.Text);

            if (DateTime.Compare(dtFim, dtIni) >= 0)
            {
                args.IsValid = true;
            }
            else
            {
                cvValidarDatasPromocao.ErrorMessage = "Data final deve ser maior que data inicial.";
                args.IsValid = false;
            }
        }
        catch
        {
            cvValidarDatasPromocao.ErrorMessage = "Data incorreta!";
            args.IsValid = false;
        }
    }

    /// <summary>
    /// Valida se uma faixa está duplicada. Ela é considerada duplicada quando já existe uma
    /// faixa com o mesmo valor mínimo ou percentual de desconto.
    /// </summary>
    /// <param name="source">Default</param>
    /// <param name="args">Default</param>
    protected void cvValidaFaixaDuplicada_ServerValidate(object source, ServerValidateEventArgs args)
    {
        // Caso esteja inválida a faixa, serão disparadas diferentes exceções
        // pela BLL para que seja tratado aqui.
        try
        {
            PromocaoFaixa promocaoFaixa = new PromocaoFaixa();
            promocaoFaixa.Promocao = new Promocao() { PromocaoId = IdPromocao };
            promocaoFaixa.ValorMinimo = Convert.ToDecimal(txtValorMinimo.Text);

            if (txtPercentualDesconto.Text.Length > 0)
                promocaoFaixa.PercentualDesconto = Convert.ToDecimal(txtPercentualDesconto.Text);
            if (PromocaoBll.ExistePromocaoFaixa(promocaoFaixa))
            {
                args.IsValid = true;
                return;
            }
        }
        catch (Exception ex)
        {
            cvValidaFaixaDuplicada.ErrorMessage = ex.Message;
            args.IsValid = false;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="source"></param>
    /// <param name="args"></param>
    protected void cvValidaNumeroMaximoCupom_ServerValidate(object source, ServerValidateEventArgs args)
    {
        if (!chkReutilizavel.Checked)
        {
            try
            {
                if (hddPromocaoId.Value == "0")
                {
                    args.IsValid = true;
                    return;
                }
                else
                {
                    if (hddNumeroMaximoCupom.Value == "")
                    {
                        args.IsValid = true;
                        return;
                    }
                    else if (Convert.ToInt32(txtNumeroMaximoCupom.Text) >= Convert.ToInt32(hddNumeroMaximoCupom.Value))
                    {
                        args.IsValid = true;
                        return;
                    }
                    else
                    {
                        cvValidaNumeroMaximoCupom.ErrorMessage = "Quantidade de cupons não pode ser menor que a quantidade anterior.";
                        args.IsValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                cvValidaNumeroMaximoCupom.ErrorMessage = ex.Message;
                args.IsValid = false;
            }
        }
        else
        {
            args.IsValid = true;
            return;
        }
    }

    #endregion
}