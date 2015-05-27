using System.Collections.Generic;
using GrupoA.DataAccess;
using GrupoA.BusinessObject;
using GrupoA.DataAccess.ADO;
using GrupoA.BusinessObject.Enumerator;
using System;
using System.Xml;
using System.Transactions;

namespace GrupoA.BusinessLogicalLayer
{
    public class BannerBLL : BaseBLL
    {
        #region Declarações DAL

        private IBannerDAL _noticiaDal;
        private IBannerAreaDAL _bannerAreaDal;
        private IArquivoDAL _arquivoDal;

        private IBannerDAL BannerDal
        {
            get { return _noticiaDal ?? (_noticiaDal = new BannerADO()); }
        }
        private IBannerAreaDAL BannerAreaDal
        {
            get { return _bannerAreaDal ?? (_bannerAreaDal = new BannerAreaADO()); }
        }
        private IArquivoDAL ArquivoDal
        {
            get { return _arquivoDal ?? (_arquivoDal = new ArquivoADO()); }
        }

        #endregion

        /// <summary>
        /// Carrega um objeto Banner.
        /// </summary>
        /// <param name="banner">Objeto Banner com identificador configurado.</param>
        /// <returns>Banner com seus dados configurados.</returns>
        public Banner CarregarBanner(Banner banner)
        {
            banner = BannerDal.Carregar(banner);
            return banner;
        }

        /// <summary>
        /// Carrega uma coleção de Banner baseado na ára de banner.
        /// </summary>
        /// <param name="quantidadeDeRegistros">Inteiro contendo o total de registros desejados.</param>
        /// <param name="areasDoBanner">Enumeração contendo o identificador da área de banner desejada.</param>
        /// <returns>Coleção de Banners.</returns>
        public List<Banner> CarregarBannersPorArea(int quantidadeDeRegistros, AreasDoBanner areasDoBanner)
        {
            return BannerDal.CarregarBannersPorArea(quantidadeDeRegistros, Convert.ToInt32(areasDoBanner), true);
        }

        /// <summary>
        /// Carrega as áreas de banner associadas a um determinado Banner.
        /// </summary>
        /// <param name="banner">Objeto do tipo Banner com o identificador configurado.</param>
        /// <returns>Coleção de objetos BannerArea.</returns>
        public List<BannerArea> CarregarAreasDoBanner(Banner banner)
        {
            return BannerDal.CarregarAreasDoBanner(banner.BannerId);
        }

        /// <summary>
        /// Retorna o número de Banners por área de Banner.
        /// </summary>
        /// <param name="areasDoBanner">Enumeração contendo o identificador da área de banner desejada.</param>
        /// <returns>Inteiro com a quantidade associada.</returns>
        private static int QuantidadeDeRegistrosPorArea(AreasDoBanner areasDoBanner)
        {
            //IDENTIFICA A AREA PARA SABER A QUANTIDADE DE REGISTROS A SER RETORNADA
            int quantidadeDeRegistrosPorArea;

            switch (areasDoBanner)
            {
                case AreasDoBanner.HomeAreaConceitual:
                    quantidadeDeRegistrosPorArea = 3;
                    break;
                case AreasDoBanner.BiocienciasAreaConceitual:
                    quantidadeDeRegistrosPorArea = 3;
                    break;
                case AreasDoBanner.HumanasAreaConceitual:
                    quantidadeDeRegistrosPorArea = 3;
                    break;
                case AreasDoBanner.HomeAreaPromocionalUm:
                    quantidadeDeRegistrosPorArea = 4;
                    break;
                case AreasDoBanner.HomeAreaPromocionalDois:
                    quantidadeDeRegistrosPorArea = 4;
                    break;
                case AreasDoBanner.HomeAreaCoringa:
                    quantidadeDeRegistrosPorArea = 1;
                    break;
                default:
                    quantidadeDeRegistrosPorArea = 4;
                    break;
            }

            return quantidadeDeRegistrosPorArea;
        }

        /// <summary>
        /// Carrega uma coleção de Banner randomicamente conforme a área de banner.
        /// </summary>
        /// <param name="areasDoBanner">Enumeração contendo o identificador da área de banner desejada.</param>
        /// <returns>Coleção de Banner.</returns>
        public List<Banner> CarregarBannersPorAreaRandomicamente(AreasDoBanner areasDoBanner)
        {
            return CarregarBannersPorAreaRandomicamente(areasDoBanner, true);
        }

        public List<Banner> CarregarBannersPorAreaRandomicamente(AreasDoBanner areasDoBanner, bool permiteFlash)
        {
            int quantidadeDeRegistrosPorArea = QuantidadeDeRegistrosPorArea(areasDoBanner);
            return BannerDal.CarregarBannersPorArea(quantidadeDeRegistrosPorArea, Convert.ToInt32(areasDoBanner), permiteFlash);
        }

        /// <summary>
        /// Carrega uma coleção de Banner conforme a área de banner.
        /// </summary>
        /// <param name="areasDoBanner">Enumeração contendo o identificador da área de banner desejada.</param>
        /// <returns>Coleção de Banner.</returns>
        public List<Banner> CarregarBannersPorArea(AreasDoBanner areasDoBanner)
        {
            int quantidadeDeRegistrosPorArea = QuantidadeDeRegistrosPorArea(areasDoBanner);
            return BannerDal.CarregarBannersPorArea(quantidadeDeRegistrosPorArea, Convert.ToInt32(areasDoBanner), true);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="banners"></param>
        /// <param name="pathDasImanges"></param>
        /// <returns></returns>
        public XmlDocument TransformaXmlParaFlash(List<Banner> banners, string pathDasImanges)
        {
            //Criamos uma instância da classe XmlDocument 
            XmlDocument doc = new XmlDocument();

            XmlElement raiz = doc.CreateElement("banners");
            doc.AppendChild(raiz);

            foreach (Banner banner in banners)
            {
                XmlElement itemBanner = doc.CreateElement("banner");
                raiz.AppendChild(itemBanner);

                AddItemXmlCDataDocument(doc, itemBanner, "link", banner.Url);
                AddItemXmlCDataDocument(doc, itemBanner, "file", string.Concat(pathDasImanges, banner.Arquivo.NomeArquivo));
                AddItemXmlDocument(doc, itemBanner, "time", banner.TempoExibicao.ToString());
                AddItemXmlDocument(doc, itemBanner, "linkTarget", banner.TargetBlank.ToString().ToLower());
            }

            return doc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="xmlRoot"></param>
        /// <param name="newParamId"></param>
        /// <param name="newParamValue"></param>
        private void AddItemXmlCDataDocument(XmlDocument xmlDoc, XmlElement xmlRoot, string newParamId, string newParamValue)
        {
            XmlElement xmlElem = xmlDoc.CreateElement(newParamId);
            xmlElem.AppendChild(xmlDoc.CreateCDataSection(newParamValue));
            xmlRoot.AppendChild(xmlElem);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xmlDoc"></param>
        /// <param name="xmlRoot"></param>
        /// <param name="newParamId"></param>
        /// <param name="newParamValue"></param>
        private void AddItemXmlDocument(XmlDocument xmlDoc, XmlElement xmlRoot, string newParamId, string newParamValue)
        {
            XmlElement xmlElem = xmlDoc.CreateElement(newParamId);
            xmlElem.InnerText = newParamValue;
            xmlRoot.AppendChild(xmlElem);
        }

        /// <summary>
        /// Insere um novo Banner.
        /// </summary>
        /// <param name="banner">Objeto do tipo Banner com seus dados configurados.</param>
        /// <returns>Objeto Banner passado com seu identificador configurado.</returns>
        public Banner InserirNovoBanner(Banner banner)
        {
            if (banner.Arquivo != null)
            {
                InserirArquivoDoBanner(banner.Arquivo);
            }

            BannerDal.Inserir(banner);

            return banner;
        }

        /// <summary>
        /// Método que atualiza os dados de um Banner.
        /// </summary>
        /// <param name="entidade">Objeto do tipo Banner com seus dados configurados.</param>
        public void AtualizarBanner(Banner entidade)
        {
            InserirArquivoDoBanner(entidade.Arquivo);

            // TODO: substituir por um método que exclua a relação baseado no id do Banner.
            if (entidade.BannerAreas != null)
            {
                BannerDal.ExcluirAreasPorBanner(entidade.BannerId);

                for (int i = 0; i < entidade.BannerAreas.Count; i++)
                {
                    BannerDal.InserirLocalizacaoBanner(entidade.BannerId, entidade.BannerAreas[i].BannerAreaId);
                }
            }

            BannerDal.Atualizar(entidade);
        }

        /// <summary>
        /// Método que insere um arquivo relacionado ao Banner.
        /// </summary>
        /// <param name="arquivo">Objeto do tipo Arquivo com seu dados configurados.</param>
        private void InserirArquivoDoBanner(Arquivo arquivo)
        {
            if (arquivo != null && arquivo.ArquivoId == 0)
            {
                var novoArquivo = new Arquivo();
                novoArquivo.NomeArquivo = arquivo.NomeArquivo;
                novoArquivo.NomeArquivoOriginal = arquivo.NomeArquivoOriginal;
                novoArquivo.TamanhoArquivo = arquivo.TamanhoArquivo;
                novoArquivo.DataHoraUpload = arquivo.DataHoraUpload;
                ArquivoDal.Inserir(novoArquivo);
                arquivo.ArquivoId = novoArquivo.ArquivoId;
            }
        }

        /// <summary>
        /// Carrega todos todos os Banners.
        /// </summary>
        /// <returns>Coleção de objetos do tipo Banner.</returns>
        public IEnumerable<BannerArea> CarregarBannersArea()
        {
            return BannerAreaDal.CarregarTodos();
        }

        /// <summary>
        /// Insere um objeto do tipo BannerArea.
        /// </summary>
        /// <param name="bannerArea">Objeto do tipo BannerArea com seus dados configurados.</param>
        public void InserirBannerArea(BannerArea bannerArea)
        {
            BannerAreaDal.Inserir(bannerArea);
        }

        /// <summary>
        /// Atualiza um objeto do tipo BannerArea.
        /// </summary>
        /// <param name="bannerArea">Objeto do tipo BannerArea com seus dados configurados.</param>
        public void AtualizarBannerArea(BannerArea bannerArea)
        {
            BannerAreaDal.Atualizar(bannerArea);
        }

        /// <summary>
        /// Carrega um objeto do tipo BannerArea.
        /// </summary>
        /// <param name="bannerArea">Objeto do tipo BannerArea com seu identificador configurado.</param>
        /// <returns>Objeto do tipo BannerArea com seus dados configurados.</returns>
        public BannerArea CarregarBannerArea(BannerArea bannerArea)
        {
            return BannerAreaDal.Carregar(bannerArea);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void Excluir(Banner entidade)
        {
            TimeSpan duracaoTransacao = new TimeSpan(0, 2, 0);

            using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required, duracaoTransacao))
            {
                BannerDal.Excluir(entidade);

                tScope.Complete();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entidade"></param>
        public void ExcluirAreasPorBanner(Banner entidade)
        {
            TimeSpan duracaoTransacao = new TimeSpan(0, 2, 0);

            using (TransactionScope tScope = new TransactionScope(TransactionScopeOption.Required, duracaoTransacao))
            {
                BannerDal.ExcluirAreasPorBanner(entidade.BannerId);

                tScope.Complete();
            }
        }
    }
}