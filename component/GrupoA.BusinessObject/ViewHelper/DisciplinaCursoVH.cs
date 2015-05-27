using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject.ViewHelper
{
	[Serializable]
	public partial class DisciplinaCursoVH
	{
		private string nomeDisciplina;
		private string nomeCurso;
		private string nroAlunos;
        private string nivelStr;
		private string nivel;
		private string adotaLivro;
        private int disciplinaId;
        private int cursoId;
        private int instituicaoId;

        private int professorDisciplinaId;
        private int professorCursoId;
        private int professorInstituicaoId;
        private string link;

		public string NomeDisciplina 
		{
			get { return nomeDisciplina; }
			set { nomeDisciplina = value; } 		
		}

		public string NomeCurso
		{
			get { return nomeCurso; }
			set { nomeCurso = value; }
		}
		public string NumeroAlunos
		{
			get { return nroAlunos; }
			set { nroAlunos = value; }
		}
		public string AdotaLivro
		{
			get { return adotaLivro; }
			set { adotaLivro = value; }
		}
		public string NivelCurso
		{
			get { return nivel; }
			set { nivel = value; }
		}
        public string NivelCursoStr
        {
            get { return nivelStr; }
            set { nivelStr = value; }
        }

        public int DisciplinaId
        {
            get { return disciplinaId; }
            set { disciplinaId = value; }
        }

        public int CursoId
        {
            get { return cursoId; }
            set { cursoId = value; }
        }

        public int InstituicaoId
        {
            get { return instituicaoId; }
            set { instituicaoId = value; }
        }
        public int ProfessorDisciplinaId
        {
            get { return professorDisciplinaId; }
            set { professorDisciplinaId = value; }
        }

        public int ProfessorCursoId
        {
            get { return professorCursoId; }
            set { professorCursoId = value; }
        }

        public int ProfessorInstituicaoId
        {
            get { return professorInstituicaoId; }
            set { professorInstituicaoId = value; }
        }

        public string Link
        {
            get { return link; }
            set { link = value; }
        }

    }
}
