using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Transactions;
using GrupoA.BusinessLogicalLayer.Helper;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;

namespace GrupoA.BusinessLogicalLayer
{
    /// <summary>
    /// Classe que contém métodos de negócio para utilização de minhas solicitações.
    /// </summary>
    public class TituloAvaliacaoBLL : BaseBLL
    {
        #region Declarações DAL

        private ITituloAvaliacaoDAL _tituloAvaliacaoDal;
        private ITituloAvaliacaoDAL TituloAvaliacaoDal
        {
            get { return _tituloAvaliacaoDal ?? (_tituloAvaliacaoDal = new TituloAvaliacaoADO()); }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloSolicitacao"></param>
        /// <returns></returns>
        public TituloAvaliacao CarregarPorSolicitacao(TituloSolicitacao tituloSolicitacao)
        {
            return TituloAvaliacaoDal.CarregarPorSolicitacao(tituloSolicitacao);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloAvaliacao"></param>
        /// <returns></returns>
        public TituloAvaliacao Carregar(TituloAvaliacao tituloAvaliacao)
        {
            return TituloAvaliacaoDal.Carregar(tituloAvaliacao);
        }

        /// <summary>
        /// Inserir um novo título solicitação
        /// </summary>
        /// <param name="tituloSolicitacao">Título solicitação a ser inserido</param>
        /// <param name="produto">Produto solicitado</param>
        public void Inserir(TituloAvaliacao tituloAvaliacao, String caminhotemplate)
        {
            Professor professor = tituloAvaliacao.TituloSolicitacao.Professor;
            tituloAvaliacao.TituloSolicitacao = new TituloSolicitacaoBLL().Carregar(tituloAvaliacao.TituloSolicitacao);
            tituloAvaliacao.TituloSolicitacao.Professor = professor;

            // Inclui a avaliação da solicitação do título
            tituloAvaliacao.Finalizada = true;
            tituloAvaliacao.DataRealizacaoAvaliacao = DateTime.Now;

            using (TransactionScope scope = new TransactionScope())
            {
                // Inserção de avaliação de título solicitação
                TituloAvaliacaoDal.Inserir(tituloAvaliacao);

                this.EnviarEmailAvisoAvaliacao(tituloAvaliacao, caminhotemplate);

                scope.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tituloAvaliacao"></param>
        public void Atualizar(TituloAvaliacao tituloAvaliacao)
        {
            using (TransactionScope scope = new TransactionScope())
            {
                TituloAvaliacaoDal.Atualizar(tituloAvaliacao);
                scope.Complete();
            }
        }

        public Boolean EnviarEmailAvisoAvaliacao(TituloAvaliacao tituloAvaliacao, String caminhotemplate)
        {
            try
            {
                Usuario usuario = tituloAvaliacao.TituloSolicitacao.Professor.Usuario;
                TituloImpresso tituloImpresso = new TituloImpressoBLL().CarregarTituloImpressoComDependenciasPorTitulo(tituloAvaliacao.TituloSolicitacao.Titulo);

                Dictionary<String, String> dicionarioDados = new Dictionary<String, String>();
                dicionarioDados.Add("NomeTitulo", tituloImpresso.Titulo.NomeTitulo);
                dicionarioDados.Add("NomeAutores", this.CarregarAutores(tituloImpresso.Titulo));
                
                if (tituloImpresso.Titulo.Edicao != null && tituloImpresso.Titulo.Edicao.Value > 1)
                {
                    dicionarioDados.Add("NumeroEdicao", String.Concat(tituloImpresso.Titulo.Edicao.Value.ToString(), "ª Edição"));
                }
                else
                {
                    dicionarioDados.Add("NumeroEdicao", String.Empty);
                }

                dicionarioDados.Add("Isbn", tituloImpresso.Isbn13);
                dicionarioDados.Add("NomeProfessor", usuario.NomeUsuario);
                dicionarioDados.Add("Instituicoes", this.CarregarInstituicoes(usuario.UsuarioId));
                dicionarioDados.Add("RelevanciaObra", tituloAvaliacao.RelevanciaObra.ToString());
                dicionarioDados.Add("RelevanciaObraObs", tituloAvaliacao.RelevanciaObraObs);
                dicionarioDados.Add("ConteudoAtualizado", tituloAvaliacao.ConteudoAtualizado.ToString());
                dicionarioDados.Add("ConteudoAtualizadoObs", tituloAvaliacao.ConteudoAtualizadoObs);
                dicionarioDados.Add("QualidadeTexto", tituloAvaliacao.QualidadeTexto.ToString());
                dicionarioDados.Add("QualidadeTextoObs", tituloAvaliacao.QualidadeTextoObs);
                dicionarioDados.Add("ApresentacaoGrafica", tituloAvaliacao.ApresentacaoGrafica.ToString());
                dicionarioDados.Add("ApresentacaoGraficaObs", tituloAvaliacao.ApresentacaoGraficaObs);
                dicionarioDados.Add("MaterialComplementarProfessor", tituloAvaliacao.MaterialComplementar.ToString());
                dicionarioDados.Add("MaterialComplementarProfessorObs", tituloAvaliacao.MaterialComplementarObs);
                dicionarioDados.Add("AvaliacaoGeral", tituloAvaliacao.AvaliacaoGeral.ToString());
                dicionarioDados.Add("AvaliacaoGeralObs", tituloAvaliacao.AvaliacaoGeralObs);
                dicionarioDados.Add("PontosFortes", tituloAvaliacao.PontosFortes);
                dicionarioDados.Add("PontosFracos", tituloAvaliacao.PontosFracos);
                dicionarioDados.Add("Sugestoes", tituloAvaliacao.Sugestoes);
                dicionarioDados.Add("SeraAdotada", tituloAvaliacao.SeraAdotada ? "Sim" : "N&atilde;o");

                if (tituloAvaliacao.SeraAdotada)
                {
                    dicionarioDados.Add("DisplayAdotaQuais", String.Empty);

                    dicionarioDados.Add("SeraAdotadaQuais", tituloAvaliacao.SeraAdotadaQuais);
                }
                else
                {
                    dicionarioDados.Add("DisplayAdotaQuais", "none");
                    dicionarioDados.Add("SeraAdotadaQuais", String.Empty);
                }

                dicionarioDados.Add("SeraRecomendada", tituloAvaliacao.SeraRecomendada ? "Sim" : "N&atilde;o");

                if (tituloAvaliacao.SeraRecomendada)
                {
                    dicionarioDados.Add("DisplayRecomendaQuais", String.Empty);

                    dicionarioDados.Add("SeraRecomendadaQuais", tituloAvaliacao.SeraRecomendadaQuais);
                }
                else
                {
                    dicionarioDados.Add("DisplayRecomendaQuais", "none");
                    dicionarioDados.Add("SeraRecomendadaQuais", String.Empty);
                }

                dicionarioDados.Add("ObraNaoSeAplica", tituloAvaliacao.NaoAplica ? "x" : " ");

                if (tituloAvaliacao.NaoAplica)
                {
                    dicionarioDados.Add("DisplayNaoAplicaPorque", String.Empty);
                    dicionarioDados.Add("DisplayNaoAplicaObraAdotada", String.Empty);
                    dicionarioDados.Add("DisplayNaoAplicaAutor", String.Empty);

                    dicionarioDados.Add("NaoAplicaPorque", tituloAvaliacao.NaoAplicaPorque);
                    dicionarioDados.Add("NaoAplicaObraAdotada", tituloAvaliacao.NaoAplicaAdotada);
                    dicionarioDados.Add("NaoAplicaAutor", tituloAvaliacao.NaoAplicaAutor);
                }
                else
                {
                    dicionarioDados.Add("DisplayNaoAplicaPorque", "none");
                    dicionarioDados.Add("DisplayNaoAplicaObraAdotada", "none");
                    dicionarioDados.Add("DisplayNaoAplicaAutor", "none");

                    dicionarioDados.Add("NaoAplicaPorque", String.Empty);
                    dicionarioDados.Add("NaoAplicaObraAdotada", String.Empty);
                    dicionarioDados.Add("NaoAplicaAutor", String.Empty);
                }

                dicionarioDados.Add("RevisorTecnico", tituloAvaliacao.RevisorTecnico ? "x" : " ");
                dicionarioDados.Add("TradutorIngles", tituloAvaliacao.TradutorIngles ? "x" : " ");
                dicionarioDados.Add("TradutorEspanhol", tituloAvaliacao.TradutorEspanhol ? "x" : " ");
                dicionarioDados.Add("TradutorFrances", tituloAvaliacao.TradutorFrances ? "x" : " ");
                dicionarioDados.Add("TradutorAlemao", tituloAvaliacao.TradutorAlemao ? "x" : " ");
                dicionarioDados.Add("CaminhoSite", ConfigurationManager.AppSettings["sitePath"].ToString());

                StringBuilder templateEmail = new EmailHelper().PopulaTemplateEmail(dicionarioDados, caminhotemplate);

                new EmailHelper().EnviarEmail(usuario.EmailUsuario, GrupoA.GlobalResources.GrupoA_Resource.EmailDivulgacao.ToString(), "Grupo A | Avaliação de título", templateEmail);
                
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public string CarregarAutores(Titulo titulo)
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

        public string CarregarInstituicoes(Int32 professorId)
        {
            var instituicoes = new ProfessorBLL().CarregarInstituicoesPorProfessor(new Professor() { ProfessorId = professorId });
            string strInstituicoes = string.Empty;
            StringBuilder texto = new StringBuilder();

            int x = 1;

            if (instituicoes != null && instituicoes.Count > 0)
            {
                foreach (var item in instituicoes)
                {
                    if (!string.IsNullOrEmpty(item.NomeInstituicao))
                    {
                        texto.Append(item.NomeInstituicao);

                        if (x < instituicoes.Count)
                        {
                            texto.Append("; ");
                        }
                    }
                    x++;
                }
            }

            strInstituicoes = texto.ToString();

            return strInstituicoes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="usuarioId"></param>
        /// <returns></returns>
        public List<TituloAvaliacao> CarregarAvaliacoesPublicacao(Int32 usuarioId)
        {
            return TituloAvaliacaoDal.CarregarAvaliacoesPublicacao(usuarioId);
        }

        #endregion
    }
}
