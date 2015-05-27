using System;
using System.Collections.Generic;
using System.Text;

namespace Ag2.Manager.BusinessObject
{
    public class Option
    {
        private string _querySection = string.Empty;
        private string _name = string.Empty;
        private string _value = string.Empty;

        public string QuerySection
        {
            get { return _querySection; }
            set { _querySection = value; }
        }

        public string Value
        {
            get { return _value; }
            set { _value = value; }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public Option()
        {
            //
        }
    }
}
