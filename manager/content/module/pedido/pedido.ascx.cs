using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using GrupoA.BusinessLogicalLayer;
using GrupoA.BusinessObject;
using Ag2.Manager.Helper;
using GrupoA.BusinessObject.Enumerator;

public partial class content_module_pedido_pedido : System.Web.UI.UserControl
{
    #region [ Properties ]

    protected Pedido PedidoBO = new Pedido();

    protected int PedidoId
    {
        get
        {
            return (int)Util.GetRequestId();
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
        if (PedidoId > 0)
        {
            this.CarregarForm();

            if (!IsPostBack)
            {

            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptPedidoItem_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item != null && e.Item.DataItem != null)
        {
            PedidoItem pedidoItemBO = (PedidoItem)e.Item.DataItem;

            if (pedidoItemBO.PedidoItemId > 0)
            {
                Literal ltrISBN13 = (Literal)e.Item.FindControl("ltrISBN13");
                Literal ltrAreaConhecimento = (Literal)e.Item.FindControl("ltrAreaConhecimento");
                Literal ltrProdutoTipo = (Literal)e.Item.FindControl("ltrProdutoTipo");
                HyperLink hpLnkProdutoNome = (HyperLink)e.Item.FindControl("hpLnkProdutoNome");
                Literal ltrPedidoItemQuantidade = (Literal)e.Item.FindControl("ltrPedidoItemQuantidade");
                Literal ltrPedidoItemValorBase = (Literal)e.Item.FindControl("ltrPedidoItemValorBase");
                Literal ltrPedidoItemValorBaseDesconto = (Literal)e.Item.FindControl("ltrPedidoItemValorBaseDesconto");
                Literal ltrPedidoItemValorDesconto = (Literal)e.Item.FindControl("ltrPedidoItemValorDesconto");
                Literal ltrPedidoItemValorTotal = (Literal)e.Item.FindControl("ltrPedidoItemValorTotal");
                HyperLink hpLnkPromocao = (HyperLink)e.Item.FindControl("hpLnkPromocao");
                Literal ltrPedidoItemFreteGratis = (Literal)e.Item.FindControl("ltrPedidoItemFreteGratis");

                ltrPedidoItemQuantidade.Text = Convert.ToInt32(pedidoItemBO.Quantidade).ToString();
                ltrPedidoItemValorBase.Text = pedidoItemBO.ValorUnitarioBase.ToString("C2");
                ltrPedidoItemValorBaseDesconto.Text = pedidoItemBO.ValorUnitarioFinal.ToString("C2");

                decimal valorTotal = pedidoItemBO.ValorUnitarioFinal * pedidoItemBO.Quantidade;
                ltrPedidoItemValorTotal.Text = valorTotal.ToString("C2");

                #region [ Promocao ]

                pedidoItemBO.PedidoItemPromocao = new PedidoItemPromocaoBLL().Carregar(new PedidoItemPromocao { PedidoItemPromocaoId = pedidoItemBO.PedidoItemId });

                if (pedidoItemBO.PedidoItemPromocao != null && pedidoItemBO.PedidoItemPromocao.PedidoItemPromocaoId > 0)
                {
                    ltrPedidoItemValorDesconto.Text = pedidoItemBO.PedidoItemPromocao.DescontoValor != null ? pedidoItemBO.PedidoItemPromocao.DescontoValor.Value.ToString("C2") : (pedidoItemBO.PedidoItemPromocao.DescontoPercentual != null ? pedidoItemBO.PedidoItemPromocao.DescontoPercentual.Value.ToString("N2") : "0,00");
                    ltrPedidoItemFreteGratis.Text = pedidoItemBO.PedidoItemPromocao.FreteGratis == true ? "Sim" : "Não";

                    if (pedidoItemBO.PedidoItemPromocao.Promocao != null && pedidoItemBO.PedidoItemPromocao.Promocao.PromocaoId > 0)
                    {
                        pedidoItemBO.PedidoItemPromocao.Promocao = new PromocaoBLL().Carregar(pedidoItemBO.PedidoItemPromocao.Promocao);

                        if (pedidoItemBO.PedidoItemPromocao.Promocao != null)
                        {
                            hpLnkPromocao.Text = pedidoItemBO.PedidoItemPromocao.Promocao.NomePromocao;

                            var secure = new Ag2.Security.SecureQueryString();
                            secure["md"] = "promocao";
                            secure["id"] = pedidoItemBO.PedidoItemPromocao.Promocao.PromocaoId.ToString(); ;
                            hpLnkPromocao.NavigateUrl = string.Format("~/content/edit.aspx?q={0}", secure.ToString());
                        }
                    }
                    else
                    {
                        hpLnkPromocao.Text = "Nenhuma";
                    }
                }
                else if (pedidoItemBO.ValorUnitarioBase != pedidoItemBO.ValorUnitarioFinal)
                {
                    ltrPedidoItemValorDesconto.Text = (100 - ((pedidoItemBO.ValorUnitarioFinal * 100) / pedidoItemBO.ValorUnitarioBase)).ToString("N2");
                    //ltrPedidoItemFreteGratis.Text = "Não";
                    hpLnkPromocao.Text = "Nenhuma";
                }
                else
                {
                    ltrPedidoItemValorDesconto.Text = "R$ 0,00";
                    ltrPedidoItemFreteGratis.Text = "Não";
                    hpLnkPromocao.Text = "Nenhuma";
                }

                #endregion

                if (pedidoItemBO.Produto != null && pedidoItemBO.Produto.ProdutoId > 0)
                {
                    Produto produtoBO = new ProdutoBLL().Carregar(pedidoItemBO.Produto);

                    if (produtoBO != null && produtoBO.ProdutoId > 0)
                    {
                        if (ltrAreaConhecimento != null)
                        {
                            ltrAreaConhecimento.Text = this.BuscaAreaConhecimento(produtoBO);
                        }

                        if (hpLnkProdutoNome != null)
                        {
                            hpLnkProdutoNome.Text = produtoBO.NomeProduto;
                        }

                        if (produtoBO.ProdutoTipo != null && produtoBO.ProdutoTipo.ProdutoTipoId > 0)
                        {
                            string moduleName = String.Empty;
                            int id = 0;

                            produtoBO.ProdutoTipo = new ProdutoTipoBLL().Carregar(produtoBO.ProdutoTipo);

                            if (produtoBO.ProdutoTipo != null)
                            {
                                ltrProdutoTipo.Text = produtoBO.ProdutoTipo.Tipo;

                                #region [ Link Produto ]

                                TituloImpresso tituloImpressoBO = new TituloImpresso();
                                TituloEletronico tituloEletronicoBO = new TituloEletronico();
                                TituloEletronicoAluguel tituloEletronicoAluguelBO = new TituloEletronicoAluguel();
                                Capitulo capituloBO = new Capitulo();
                                CapituloImpresso capituloImpressoBO = new CapituloImpresso();
                                CapituloEletronico capituloEletronicoBO = new CapituloEletronico();
                                RevistaEdicao revistaEdicaoBO = new RevistaEdicao();
                                RevistaAssinatura revistaAssinaturaBO = new RevistaAssinatura();

                                switch (produtoBO.ProdutoTipo.ProdutoTipoId)
                                {
                                    case 1: // Livro Impresso

                                        tituloImpressoBO = new TituloImpressoBLL().CarregarPorProduto(produtoBO);

                                        moduleName = "tituloProduto";

                                        if (tituloImpressoBO != null && tituloImpressoBO.TituloImpressoId > 0 && tituloImpressoBO.Titulo != null && tituloImpressoBO.Titulo.TituloId > 0)
                                        {
                                            id = tituloImpressoBO.Titulo.TituloId;

                                            if (ltrISBN13 != null)
                                            {
                                                ltrISBN13.Text = tituloImpressoBO.Isbn13;
                                            }
                                        }

                                        break;
                                    case 2: // eBook

                                        tituloEletronicoBO = new TituloEletronicoBLL().Carregar(new TituloEletronico { TituloEletronicoId = produtoBO.ProdutoId });

                                        moduleName = "tituloProduto";

                                        if (tituloEletronicoBO != null && tituloEletronicoBO.TituloEletronicoId > 0 && tituloEletronicoBO.Titulo != null && tituloEletronicoBO.Titulo.TituloId > 0)
                                        {
                                            id = tituloEletronicoBO.Titulo.TituloId;

                                            if (ltrISBN13 != null)
                                            {
                                                ltrISBN13.Text = tituloEletronicoBO.Isbn13;
                                            }
                                        }

                                        break;
                                    case 3: // Capítulo Impresso

                                        capituloImpressoBO = new CapituloImpressoBLL().Carregar(new CapituloImpresso { CapituloImpressoId = produtoBO.ProdutoId });

                                        if (capituloImpressoBO.Capitulo != null & capituloImpressoBO.Capitulo.CapituloId > 0)
                                        {
                                            capituloBO = new CapituloBLL().Carregar(new Capitulo { CapituloId = capituloImpressoBO.Capitulo.CapituloId });

                                            moduleName = "tituloProduto";

                                            if (capituloBO != null && capituloBO.CapituloId > 0 && capituloBO.Titulo != null && capituloBO.Titulo.TituloId > 0)
                                            {
                                                id = capituloBO.Titulo.TituloId;
                                            }
                                        }

                                        break;
                                    case 4: // Capítulo Eletrônico

                                        capituloEletronicoBO = new CapituloEletronicoBLL().Carregar(new CapituloEletronico { CapituloEletronicoId = produtoBO.ProdutoId });

                                        if (capituloEletronicoBO.Capitulo != null & capituloEletronicoBO.Capitulo.CapituloId > 0)
                                        {
                                            capituloBO = new CapituloBLL().Carregar(new Capitulo { CapituloId = capituloEletronicoBO.Capitulo.CapituloId });

                                            moduleName = "tituloProduto";

                                            if (capituloBO != null && capituloBO.CapituloId > 0 && capituloBO.Titulo != null && capituloBO.Titulo.TituloId > 0)
                                            {
                                                id = capituloBO.Titulo.TituloId;
                                            }
                                        }

                                        break;
                                    case 5: // Revista

                                        revistaEdicaoBO = new RevistaEdicaoBLL().Carregar(new RevistaEdicao { RevistaEdicaoId = produtoBO.ProdutoId });

                                        moduleName = "revistaEdicao";

                                        if (revistaEdicaoBO != null && revistaEdicaoBO.RevistaEdicaoId > 0)
                                        {
                                            id = revistaEdicaoBO.RevistaEdicaoId;
                                        }

                                        break;
                                    case 6: // Título Eletrônico para Aluguel

                                        tituloEletronicoAluguelBO = new TituloEletronicoAluguelBLL().Carregar(new TituloEletronicoAluguel { TituloEletronicoAluguelId = produtoBO.ProdutoId });

                                        if (tituloEletronicoAluguelBO != null && tituloEletronicoAluguelBO.TituloEletronicoAluguelId > 0 && tituloEletronicoAluguelBO.TituloEletronico != null && tituloEletronicoAluguelBO.TituloEletronico.TituloEletronicoId > 0)
                                        {
                                            tituloEletronicoBO = new TituloEletronicoBLL().Carregar(tituloEletronicoAluguelBO.TituloEletronico);

                                            moduleName = "tituloProduto";

                                            if (tituloEletronicoBO != null && tituloEletronicoBO.TituloEletronicoId > 0 && tituloEletronicoBO.Titulo != null && tituloEletronicoBO.Titulo.TituloId > 0)
                                            {
                                                id = tituloEletronicoBO.Titulo.TituloId;
                                            }
                                        }

                                        break;
                                    case 7: // Revista Assinatura

                                        revistaAssinaturaBO = new RevistaAssinaturaBLL().Carregar(new RevistaAssinatura { RevistaAssinaturaId = produtoBO.ProdutoId });

                                        moduleName = "assinaturaRevista";

                                        if (revistaAssinaturaBO != null && revistaAssinaturaBO.RevistaAssinaturaId > 0)
                                        {
                                            id = revistaAssinaturaBO.RevistaAssinaturaId;
                                        }

                                        break;
                                    default:
                                        break;
                                }

                                var secure = new Ag2.Security.SecureQueryString();
                                secure["md"] = moduleName;
                                secure["id"] = id.ToString();
                                hpLnkProdutoNome.NavigateUrl = string.Format("~/content/edit.aspx?q={0}", secure.ToString());

                                #endregion
                            }
                        }
                    }
                }
            }
        }
    }

    private string BuscaAreaConhecimento(Produto produtoBO)
    {
        string areaConhecimento = String.Empty;

        try
        {
            if (produtoBO != null && produtoBO.ProdutoId > 0)
            {
                produtoBO.Conteudo = new Conteudo();
                produtoBO.Conteudo.Categorias = new CategoriaBLL().CarregarCategoriaPorProduto(produtoBO.ProdutoId);

                if (
                    produtoBO.Conteudo.Categorias != null
                    && produtoBO.Conteudo.Categorias.Any()
                    && produtoBO.Conteudo.Categorias[0] != null
                    && produtoBO.Conteudo.Categorias[0].CategoriaId > 0
                    )
                {
                    Stack<Categoria> categoriaBOList = new Stack<Categoria>();

                    Categoria categoriaBO = new Categoria();
                    categoriaBO.CategoriaId = produtoBO.Conteudo.Categorias[0].CategoriaId;
                    categoriaBO = new CategoriaBLL().Carregar(categoriaBO);
                    categoriaBOList.Push(categoriaBO);

                    while (categoriaBO != null)
                    {
                        categoriaBO = this.PesquisaCategoriaPai(categoriaBO);

                        if (categoriaBO != null && categoriaBO.CategoriaId > 0)
                        {
                            categoriaBOList.Push(categoriaBO);
                        }
                    }

                    string categorias = String.Empty;
                    string breadCrumb = String.Empty;

                    while (categoriaBOList.Any())
                    {
                        categoriaBO = categoriaBOList.Pop();

                        breadCrumb += string.Concat("- ", categoriaBO.NomeCategoria, "<br />");
                    }

                    areaConhecimento = breadCrumb;
                }
            }
        }
        catch { }

        return areaConhecimento;
    }

    #endregion

    #region [ Methods ]

    /// <summary>
    /// 
    /// </summary>
    /// <param name="categoriaBO"></param>
    /// <returns></returns>
    protected Categoria PesquisaCategoriaPai(Categoria categoriaBO)
    {
        categoriaBO = new CategoriaBLL().Carregar(categoriaBO);

        if (categoriaBO != null && categoriaBO.CategoriaPai != null && categoriaBO.CategoriaPai.CategoriaId > 0)
        {
            categoriaBO = new CategoriaBLL().Carregar(categoriaBO.CategoriaPai);
        }
        else
        {
            categoriaBO = null;
        }

        return categoriaBO;
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarForm()
    {
        this.CarregarPedido();
        this.CarregarPedidoUsuario();
        this.CarregarPedidoPagamento();
        this.CarregarPedidoEndereco();
        this.CarregarPedidoItem();
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarPedido()
    {
        this.PedidoBO = new PedidoBLL().Carregar(new Pedido { PedidoId = this.PedidoId });

        if (this.PedidoBO != null && this.PedidoBO.PedidoId > 0)
        {
            this.txtPedidoId.Text = this.PedidoBO.PedidoId.ToString();
            this.txtPedidoData.Text = this.PedidoBO.DataHoraPedido != null ? this.PedidoBO.DataHoraPedido.ToString("dd/MM/yyyy HH:mm:ss") : "";
            this.txtPedidoValorFrete.Text = this.PedidoBO.FreteValor != null ? this.PedidoBO.FreteValor.ToString() : "";
            this.txtPedidoValor.Text = this.PedidoBO.ValorPedido != null ? this.PedidoBO.ValorPedido.ToString() : "";
            this.txtPedidoCodigo.Text = this.PedidoBO.PedidoCodigo != null ? this.PedidoBO.PedidoCodigo.Value.ToString() : "";

            this.PedidoBO.PedidoControle = new PedidoControle();
            this.PedidoBO.PedidoControle.Pedido = new Pedido();
            this.PedidoBO.PedidoControle.Pedido = this.PedidoBO;
            this.PedidoBO.PedidoControle.PedidoId = this.PedidoBO.PedidoId;
            this.PedidoBO.PedidoControle = new PedidoControleBLL().Carregar(this.PedidoBO.PedidoControle);

            if (this.PedidoBO.PedidoControle != null && this.PedidoBO.PedidoControle.PedidoId > 0)
            {
                this.txtPedidoDataSincronizacao.Text = this.PedidoBO.PedidoControle.DataHoraExportacao != null ? this.PedidoBO.PedidoControle.DataHoraExportacao.Value.ToString("dd/MM/yyyy HH:mm:ss") : "";
            }

            if (this.PedidoBO.PedidoStatus != null && this.PedidoBO.PedidoStatus.PedidoStatusId > 0)
            {
                this.PedidoBO.PedidoStatus = new PedidoStatusBLL().Carregar(this.PedidoBO.PedidoStatus);

                if (this.PedidoBO.PedidoStatus != null)
                {
                    this.txtPedidoStatus.Text = this.PedidoBO.PedidoStatus.StatusPedido;
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarPedidoUsuario()
    {
        if (this.PedidoBO != null && this.PedidoBO.PedidoId > 0 && this.PedidoBO.Usuario != null && this.PedidoBO.Usuario.UsuarioId > 0)
        {
            this.PedidoBO.Usuario = new UsuarioBLL().CarregarUsuario(this.PedidoBO.Usuario);

            if (this.PedidoBO.Usuario != null && this.PedidoBO.Usuario.UsuarioId > 0)
            {
                this.txtUsuarioNome.Text = this.PedidoBO.Usuario.NomeUsuario;
                this.txtUsuarioCadastroPessoa.Text = this.PedidoBO.Usuario.CadastroPessoa;
                this.txtUsuarioEmail.Text = this.PedidoBO.Usuario.EmailUsuario;

                var secure = new Ag2.Security.SecureQueryString();
                secure["md"] = this.PedidoBO.Usuario.TipoPessoa != null && this.PedidoBO.Usuario.TipoPessoa == 1 ? "pessoa" : "pessoaProfessor";
                secure["id"] = this.PedidoBO.Usuario.UsuarioId.ToString();
                hpLnkUsuarioCompleto.NavigateUrl = string.Format("~/content/edit.aspx?q={0}", secure.ToString());
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarPedidoPagamento()
    {
        if (this.PedidoBO != null && this.PedidoBO.PedidoId > 0)
        {
            this.PedidoBO.Pagamento = new PagamentoBLL().Carregar(this.PedidoBO);

            if (this.PedidoBO.Pagamento != null && this.PedidoBO.Pagamento.PagamentoId > 0)
            {
                this.txtPagamentoNroParcelas.Text = this.PedidoBO.Pagamento.NumeroParcelas.ToString();
                this.txtPagamentoCodigoTransacao.Text = this.PedidoBO.Pagamento.CodigoTransacao;

                if (this.PedidoBO.Pagamento.MeioPagamento != null && this.PedidoBO.Pagamento.MeioPagamento.MeioPagamentoId > 0)
                {
                    this.PedidoBO.Pagamento.MeioPagamento = new MeioPagamentoBLL().Carregar(this.PedidoBO.Pagamento.MeioPagamento);

                    if (this.PedidoBO.Pagamento.MeioPagamento != null)
                    {
                        this.txtPedidoMeioPagamentoNome.Text = this.PedidoBO.Pagamento.MeioPagamento.Nome;
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarPedidoEndereco()
    {
        if (this.PedidoBO != null && this.PedidoBO.PedidoId > 0)
        {
            this.PedidoBO.PedidoEndereco = new PedidoEnderecoBLL().Carregar(this.PedidoBO);

            if (this.PedidoBO.PedidoEndereco != null && this.PedidoBO.PedidoEndereco.PedidoEnderecoId > 0)
            {
                this.txtEnderecoBairro.Text = this.PedidoBO.PedidoEndereco.Bairro;
                this.txtEnderecoCEP.Text = this.PedidoBO.PedidoEndereco.Cep;
                this.txtEnderecoLogradouro.Text = this.PedidoBO.PedidoEndereco.Logradouro;
                this.txtEnderecoComplemento.Text = this.PedidoBO.PedidoEndereco.Complemento;
                this.txtEnderecoNro.Text = this.PedidoBO.PedidoEndereco.Numero;

                if (this.PedidoBO.PedidoEndereco.EnderecoTipo != null && this.PedidoBO.PedidoEndereco.EnderecoTipo.EnderecoTipoId > 0)
                {
                    this.PedidoBO.PedidoEndereco.EnderecoTipo = new EnderecoTipoBLL().Carregar(this.PedidoBO.PedidoEndereco.EnderecoTipo);

                    if (this.PedidoBO.PedidoEndereco.EnderecoTipo != null)
                    {
                        this.txtEnderecoTipo.Text = this.PedidoBO.PedidoEndereco.EnderecoTipo.Tipo;
                    }
                }

                if (this.PedidoBO.PedidoEndereco.Municipio != null && this.PedidoBO.PedidoEndereco.Municipio.MunicipioId > 0)
                {
                    this.PedidoBO.PedidoEndereco.Municipio = new MunicipioBLL().Carregar(this.PedidoBO.PedidoEndereco.Municipio);

                    if (this.PedidoBO.PedidoEndereco.Municipio != null)
                    {
                        this.txtEnderecoMunicipio.Text = this.PedidoBO.PedidoEndereco.Municipio.NomeMunicipio;

                        if (this.PedidoBO.PedidoEndereco.Municipio.Regiao != null && this.PedidoBO.PedidoEndereco.Municipio.Regiao.RegiaoId > 0)
                        {
                            this.PedidoBO.PedidoEndereco.Municipio.Regiao = new RegiaoBLL().Carregar(this.PedidoBO.PedidoEndereco.Municipio.Regiao);

                            if (this.PedidoBO.PedidoEndereco.Municipio.Regiao != null)
                            {
                                this.txtEnderecoEstado.Text = this.PedidoBO.PedidoEndereco.Municipio.Regiao.NomeRegiao;
                            }
                        }
                    }
                }
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    private void CarregarPedidoItem()
    {
        if (this.PedidoBO != null && this.PedidoBO.PedidoId > 0)
        {
            this.PedidoBO.PedidoItens = new PedidoItemBLL().Carregar(this.PedidoBO);

            if (this.PedidoBO.PedidoItens != null && this.PedidoBO.PedidoItens.Any())
            {
                rptPedidoItem.DataSource = this.PedidoBO.PedidoItens;
                rptPedidoItem.DataBind();
            }
        }
    }

    #endregion
}
