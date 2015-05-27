using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;


namespace Ag2.Manager.WebUI
{
	[Serializable]
	[ValidationProperty("Text")]
	[DefaultProperty("Text"), ToolboxData("<{0}:DateField runat=server></{0}:DateField>")]
	public class DateField : System.Web.UI.WebControls.WebControl, INamingContainer
	{
		//field with selected date
		private TextBox _textDataField;
		private string _validationgroup;

		public DateField()
		{
			_textDataField = new TextBox();
		}

		protected override void OnInit(EventArgs e)
		{
			base.OnInit(e);
		}

		protected override void CreateChildControls()
		{
			_textDataField.ID = this.ID;
			_textDataField.MaxLength = 10;
			_textDataField.Width = Unit.Pixel(70);
			_textDataField.Style.Value = "float: left;border: 1px solid silver;margin: 0 6px 6px 0;";
			
			if (_textDataField.ReadOnly)
			{
				_textDataField.CssClass = "frmborder";
			}
			else
			{
				_textDataField.CssClass = "data dateField frmborder";
			}

			this.Controls.Add(_textDataField);
			this.ChildControlsCreated = true;
		}

		protected override void Render(HtmlTextWriter writer)
		{
			this.RenderChildren(writer);
		}

		public string Text
		{
			get { return _textDataField.Text; }
			set { _textDataField.Text = value; }
		}

		public string ValidationGroup
		{
			get { return _validationgroup; }
			set { _validationgroup = value; }
		}

		public bool ReadOnly
		{
			get { return _textDataField.ReadOnly; }
			set
			{
				_textDataField.ReadOnly = value;

				if (_textDataField.ReadOnly)
				{
					_textDataField.CssClass = "frmborder";
				}
				else
				{
					_textDataField.CssClass = "dateField frmborder";
				}
			}
		}
	}
}
