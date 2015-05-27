using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GrupoA.BusinessObject.ViewHelper
{
    public partial class ProfessorInstituicaoEnsinoVH
    {
        int professorInstituicaoEnsinoId;
        public int ProfessorInstituicaoEnsinoId
        {
            get { return this.professorInstituicaoEnsinoId; }
            set { professorInstituicaoEnsinoId = value; }
        }

        int arquivoId;
        public int ArquivoId
        {
            get { return this.arquivoId; }
            set { arquivoId = value; }
        }

        string instituicaoEnsino;
        public string InstituicaoEnsino
        {
            get { return this.instituicaoEnsino; }
            set { instituicaoEnsino = value; }
        }

        string campus;
        public string Campus
        {
            get { return this.campus; }
            set { campus = value; }
        }

        string departamento;
        public string Departamento
        {
            get { return this.departamento; }
            set { departamento = value; }
        }

        string telefone;
        public string Telefone
        {
            get { return this.telefone; }
            set { telefone = value; }
        }

        string comprovante;
        public string Comprovante
        {
            get { return this.comprovante; }
            set { comprovante = value; }
        }


    }
}
