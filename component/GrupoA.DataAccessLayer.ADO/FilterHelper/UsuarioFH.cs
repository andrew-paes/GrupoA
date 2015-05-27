
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
using System;
using System.Text;

namespace GrupoA.FilterHelper
{
	public partial class UsuarioFH : IFilterHelper
	{
		private string _usuarioId;
		public string UsuarioId {
			get { return _usuarioId==null?String.Empty:_usuarioId; }
			set { _usuarioId=value; }
		}

		private string _tipoPessoa;
		public string TipoPessoa {
			get { return _tipoPessoa==null?String.Empty:_tipoPessoa; }
			set { _tipoPessoa=value; }
		}

		private string _sexo;
		public string Sexo {
			get { return _sexo==null?String.Empty:_sexo; }
			set { _sexo=value; }
		}

		private string _ativo;
		public string Ativo {
			get { return _ativo==null?String.Empty:_ativo; }
			set { _ativo=value; }
		}

		private string _nomeUsuario;
		public string NomeUsuario {
			get { return _nomeUsuario==null?String.Empty:_nomeUsuario; }
			set { _nomeUsuario=value; }
		}

		private string _cadastroPessoa;
		public string CadastroPessoa {
			get { return _cadastroPessoa==null?String.Empty:_cadastroPessoa; }
			set { _cadastroPessoa=value; }
		}

		private string _emailUsuario;
		public string EmailUsuario {
			get { return _emailUsuario==null?String.Empty:_emailUsuario; }
			set { _emailUsuario=value; }
		}

		private string _login;
		public string Login {
			get { return _login==null?String.Empty:_login; }
			set { _login=value; }
		}

		private string _dataNascimentoPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataNascimentoPeriodoInicial {
			get { return _dataNascimentoPeriodoInicial==null?String.Empty:_dataNascimentoPeriodoInicial; }
			set { _dataNascimentoPeriodoInicial=value; }
		}
		private string _dataNascimentoPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataNascimentoPeriodoFinal {
			get { return _dataNascimentoPeriodoFinal==null?String.Empty:_dataNascimentoPeriodoFinal; }
			set { _dataNascimentoPeriodoFinal=value; }
		}

		private string _dataHoraCadastroPeriodoInicial;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraCadastroPeriodoInicial {
			get { return _dataHoraCadastroPeriodoInicial==null?String.Empty:_dataHoraCadastroPeriodoInicial; }
			set { _dataHoraCadastroPeriodoInicial=value; }
		}
		private string _dataHoraCadastroPeriodoFinal;
		/// <summary>
		/// Formato da string contendo data: YYYY/MM/DD.
		/// </summary>
		public string DataHoraCadastroPeriodoFinal {
			get { return _dataHoraCadastroPeriodoFinal==null?String.Empty:_dataHoraCadastroPeriodoFinal; }
			set { _dataHoraCadastroPeriodoFinal=value; }
		}

		private string _optinSMS;
		public string OptinSMS {
			get { return _optinSMS==null?String.Empty:_optinSMS; }
			set { _optinSMS=value; }
		}

		private string _optinNewsletter;
		public string OptinNewsletter {
			get { return _optinNewsletter==null?String.Empty:_optinNewsletter; }
			set { _optinNewsletter=value; }
		}

		private string _codigoUsuario;
		public string CodigoUsuario {
			get { return _codigoUsuario==null?String.Empty:_codigoUsuario; }
			set { _codigoUsuario=value; }
		}

		private string _profissionalOcupacaoId;
		public string ProfissionalOcupacaoId {
			get { return _profissionalOcupacaoId==null?String.Empty:_profissionalOcupacaoId; }
			set { _profissionalOcupacaoId=value; }
		}

		private string _senha;
		public string Senha {
			get { return _senha==null?String.Empty:_senha; }
			set { _senha=value; }
		}

	
		public string GetWhereString() 
		{			
			StringBuilder sbWhere = new StringBuilder();

			if (!UsuarioId.Equals(String.Empty)) {
				sbWhere.Append(" AND (usuarioId="+UsuarioId+")");
			}

			if (!TipoPessoa.Equals(String.Empty)) {
				sbWhere.Append(" AND (tipoPessoa="+TipoPessoa+")");
			}

			if (!Sexo.Equals(String.Empty)) {
				sbWhere.Append(" AND (sexo LIKE '%"+Sexo+"%')");
			}

			if (!Ativo.Equals(String.Empty)) {
				sbWhere.Append(" AND (ativo LIKE '%"+Ativo+"%')");
			}

			if (!NomeUsuario.Equals(String.Empty)) {
				sbWhere.Append(" AND (nomeUsuario LIKE '%"+NomeUsuario+"%')");
			}

			if (!CadastroPessoa.Equals(String.Empty)) {
				sbWhere.Append(" AND (cadastroPessoa LIKE '%"+CadastroPessoa+"%')");
			}

			if (!EmailUsuario.Equals(String.Empty)) {
				sbWhere.Append(" AND (emailUsuario LIKE '%"+EmailUsuario+"%')");
			}

			if (!Login.Equals(String.Empty)) {
				sbWhere.Append(" AND (login LIKE '%"+Login+"%')");
			}

			if( !DataNascimentoPeriodoInicial.Equals(String.Empty) && !DataNascimentoPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataNascimento >='"+DataNascimentoPeriodoInicial+"'");
				sbWhere.Append(" AND dataNascimento <='"+DataNascimentoPeriodoFinal+"')");
			} else if (!DataNascimentoPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataNascimento >='"+DataNascimentoPeriodoInicial+"')");
			} else if (!DataNascimentoPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataNascimento <='"+DataNascimentoPeriodoFinal+"')");
			}

			if( !DataHoraCadastroPeriodoInicial.Equals(String.Empty) && !DataHoraCadastroPeriodoFinal.Equals(String.Empty)) {
				sbWhere.Append(" AND (dataHoraCadastro >='"+DataHoraCadastroPeriodoInicial+"'");
				sbWhere.Append(" AND dataHoraCadastro <='"+DataHoraCadastroPeriodoFinal+"')");
			} else if (!DataHoraCadastroPeriodoInicial.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraCadastro >='"+DataHoraCadastroPeriodoInicial+"')");
			} else if (!DataHoraCadastroPeriodoFinal.Equals(String.Empty) ) {
				sbWhere.Append(" AND (dataHoraCadastro <='"+DataHoraCadastroPeriodoFinal+"')");
			}

			if (!OptinSMS.Equals(String.Empty)) {
				sbWhere.Append(" AND (optinSMS LIKE '%"+OptinSMS+"%')");
			}

			if (!OptinNewsletter.Equals(String.Empty)) {
				sbWhere.Append(" AND (optinNewsletter LIKE '%"+OptinNewsletter+"%')");
			}

			if (!CodigoUsuario.Equals(String.Empty)) {
				sbWhere.Append(" AND (codigoUsuario LIKE '%"+CodigoUsuario+"%')");
			}

			if (!ProfissionalOcupacaoId.Equals(String.Empty)) {
				sbWhere.Append(" AND (profissionalOcupacaoId="+ProfissionalOcupacaoId+")");
			}

			if (!Senha.Equals(String.Empty)) {
				sbWhere.Append(" AND (senha LIKE '%"+Senha+"%')");
			}

	
			if (sbWhere.Length>0) // Remove o primeiro "AND "
				sbWhere.Remove(0,4);
			return sbWhere.ToString();
		}
	}
}
