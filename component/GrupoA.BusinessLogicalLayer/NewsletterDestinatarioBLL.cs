using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GrupoA.BusinessObject;
using GrupoA.DataAccess;
using GrupoA.DataAccess.ADO;
using GrupoA.FilterHelper;

namespace GrupoA.BusinessLogicalLayer
{
	/// <summary>
	/// Classe que abstrai as regras de negócio referentes a usuários.
	/// </summary>
    public class NewsletterDestinatarioBLL : BaseBLL
	{
        private INewsletterDestinatarioDAL _newsletterDestinatarioDAL;

        private INewsletterDestinatarioDAL NewsletterDestinatarioDAL
		{
			get
			{
				if (_newsletterDestinatarioDAL == null)
                    _newsletterDestinatarioDAL = new NewsletterDestinatarioADO();
				return _newsletterDestinatarioDAL;
			}
		}

        public NewsletterDestinatario Carregar(NewsletterDestinatario entidade)
		{
			return NewsletterDestinatarioDAL.Carregar(entidade);
		}

        public void Inserir(NewsletterDestinatario entidade)
        {
            NewsletterDestinatarioDAL.Inserir(entidade);
        }

        public void Atualizar(NewsletterDestinatario entidade)
        {
            NewsletterDestinatarioDAL.Atualizar(entidade);
        }

        public void Excluir(NewsletterDestinatario entidade)
        {
            NewsletterDestinatarioDAL.Excluir(entidade);
        }
	}
}