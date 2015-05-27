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
	public partial class MidiaTipo 
	{
		// Construtor
		public MidiaTipo() {}

		// Construtor com identificador
		public MidiaTipo(int midiaTipoId) {
			_midiaTipoId = midiaTipoId;
		}

		private int _midiaTipoId;
		private string _tipoMidia;
		private List<Midia> _midias;

		public int MidiaTipoId {
			get { return _midiaTipoId; }
			set { _midiaTipoId = value; }
		}

		[NotNullValidator]
		[StringLengthValidator(0, 50)]
		public string TipoMidia {
			get { return _tipoMidia; }
			set { _tipoMidia = value; }
		}

		public List<Midia> Midias {
			get { return _midias; }
			set { _midias = value; }
		}

	    /// <summary>
        /// Propriedade que informa se a entidade é válida para persistência.
        /// </summary>
        /// <returns>booleano informando se é a entidade é válida ou não.</returns>
        public bool Valido
        {
            get { return Validation.Validate<MidiaTipo>(this).IsValid; }
        }

        /// <summary>
        /// Método que valida e retorna os dados de validação da entidade.
        /// </summary>
        /// <returns>ValidationResults contendo as informações da validação.</returns>
        public ValidationResults Validar()
        {
            return Validation.Validate<MidiaTipo>(this);
        }
	}
	
	public struct MidiaTipoColunas
	{	
		public static string MidiaTipoId = @"midiaTipoId";
		public static string TipoMidia = @"tipoMidia";
	}
}
		