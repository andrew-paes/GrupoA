/*
'===============================================================================
'
'  Template: Gerador Código C#.csgen
'  Script versão: 0.96
'  Script criado por: Leonardo Alves Lindermann (lindermannla@ag2.com.br)
'  Gerado pelo MyGeneration versão # (???)
'
'===============================================================================
*/
using Microsoft.Practices.EnterpriseLibrary.Validation;
using Microsoft.Practices.EnterpriseLibrary.Validation.Validators;
using System;
using System.Text;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace GrupoA.BusinessObject
{	
	
	[Serializable]
	public partial class TituloAutor 
	{
		private bool _principal;
		private Autor _autor;
		private Titulo _titulo;
        private Int32 _ordem;

		public bool Principal {
			get { return _principal; }
			set { _principal = value; }
		}

		[NotNullValidator]
		public Autor Autor {
			get { return _autor; }
			set { _autor = value; }
		}

		[NotNullValidator]
		public Titulo Titulo {
			get { return _titulo; }
			set { _titulo = value; }
		}

        public Int32 Ordem
        {
            get { return _ordem; }
            set { _ordem = value; }
        }

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<TituloAutor>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<TituloAutor>(this);
        }
	}
	
	public struct TituloAutorColunas
	{	
		public static string TituloId = @"tituloId";
		public static string AutorId = @"autorId";
		public static string Principal = @"principal";
	}
}
		