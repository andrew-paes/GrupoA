using System;
using System.Collections.Generic;
using System.Text;

namespace Ag2.Manager.Module
{
    [Serializable]
    public class ManagerModuleFieldImagePreview : ManagerModuleField
    {
        private string _caminho;
        private string _widthThumb;
        private string _widthPreview;
        private string _yOffSet;
        private string _xOffSet;

        public string Caminho
        {
            get { return _caminho; }
            set { _caminho = value; }
        }

        public string WidthThumb
        {
            get { return _widthThumb; }
            set { _widthThumb = value; }
        }

        public string WidthPreview
        {
            get { return _widthPreview; }
            set { _widthPreview = value; }
        }

        public string yOffSet
        {
            get { return _yOffSet; }
            set { _yOffSet = value; }
        }

        public string xOffSet
        {
            get { return _xOffSet; }
            set { _xOffSet = value; }
        }
    }
}
