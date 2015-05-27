using System;

namespace Ag2.Security
{
	/// <summary>
	/// Thrown when attempting to decrypt or deserialize an invalid encrypted queryString.
	/// </summary>
	public class InvalidQueryStringException : System.Exception
	{
        /// <summary>
        /// Erro personalizado para a criptografia de querystring
        /// </summary>
		public InvalidQueryStringException() : base("Querystring inválida") {}
	}
}
