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
	public partial class Posts
	{
		// Construtor
		public Posts() { }

		// Construtor com identificador
		public Posts(Guid postId)
		{
			_postId = postId;
		}

		private Guid _postId;
		public Guid PostId
		{
			get { return _postId; }
			set { _postId = value; }
		}

		private string _title;
		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}

		private string _description;
		public string Description
		{
			get { return _description; }
			set { _description = value; }
		}

		private string _postContent;
		public string PostContent
		{
			get { return _postContent; }
			set { _postContent = value; }
		}

		private DateTime _dateCreated;
		public DateTime DateCreated
		{
			get { return _dateCreated; }
			set { _dateCreated = value; }
		}

		private DateTime _dateModified;
		public DateTime DateModified
		{
			get { return _dateModified; }
			set { _dateModified = value; }
		}

		private string _author;
		public string Author
		{
			get { return _author; }
			set { _author = value; }
		}

		private bool _isPublished;
		public bool IsPublished
		{
			get { return _isPublished; }
			set { _isPublished = value; }
		}

		private bool _isCommentEnabled;
		public bool IsCommentEnabled
		{
			get { return _isCommentEnabled; }
			set { _isCommentEnabled = value; }
		}

		private int _raters;
		public int Raters
		{
			get { return _raters; }
			set { _raters = value; }
		}

		private decimal _rating;
		public decimal Rating
		{
			get { return _rating; }
			set { _rating = value; }
		}

		private string _slug;
		public string Slug
		{
			get { return _slug; }
			set { _slug = value; }
		}

		/// <summary>
		/// Propriedade que informa se a entidade é válida para persistência.
		/// </summary>
		/// <returns>booleano informando se é a entidade é válida ou não.</returns>
		public bool Valido
		{
			get { return Validation.Validate<Posts>(this).IsValid; }
		}

		/// <summary>
		/// Método que valida e retorna os dados de validação da entidade.
		/// </summary>
		/// <returns>ValidationResults contendo as informações da validação.</returns>
		public ValidationResults Validar()
		{
			return Validation.Validate<Posts>(this);
		}
	}

	public struct PostsColunas
	{
		public static string PostId = @"postId";
		public static string Title = @"title";
		public static string Description = @"description";
		public static string DateCreated = @"dateCreated";
		public static string DateModified = @"dateModified";
		public static string Author = @"author";
		public static string IsPublished = @"isPublished";
		public static string IsCommentEnabled = @"isCommentEnabled";
		public static string Raters = @"raters";
		public static string Rating = @"rating";
		public static string Slug = @"slug";
	}
}