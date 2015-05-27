using System;

namespace Ag2.Security
{
	/// <summary>
	/// Thrown when a queryString has expired and is therefore no longer valid.
	/// </summary>
	public class ExpiredQueryStringException : System.Exception
	{
        /// <summary>
        /// Erro personalizado para a criptografia de querystring
        /// </summary>
		public ExpiredQueryStringException() : base("Querystring expirada") {}
	}
}
