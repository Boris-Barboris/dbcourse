using System;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Web.DynamicData;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebPortal {
    public partial class DateTime_EditField : System.Web.DynamicData.FieldTemplateUserControl {
        private static DataTypeAttribute DefaultDateAttribute = new DataTypeAttribute(DataType.DateTime);
        protected void Page_Load(object sender, EventArgs e) {
            TextBox1.ToolTip = Column.Description;
            
            SetUpValidator(RequiredFieldValidator1);
            SetUpValidator(RegularExpressionValidator1);
            SetUpValidator(DynamicValidator1);

			SetUpValidator(RequiredFieldValidator2);
			SetUpValidator(RegularExpressionValidator2);
			SetUpValidator(RequiredFieldValidator3);
			SetUpValidator(RegularExpressionValidator3);

            SetUpCustomValidator(DateValidator);

			RangeAttribute ra = (RangeAttribute)Column.Attributes[typeof(RangeAttribute)];
			if (ra != null)
			{
				t1.MinDate = ra.Minimum.ToString();
				t1.MaxDate = ra.Maximum.ToString();
			}
        }
    
        private void SetUpCustomValidator(CustomValidator validator) {
            if (Column.DataTypeAttribute != null) {
                switch (Column.DataTypeAttribute.DataType) {
                    case DataType.Date:
                    case DataType.DateTime:
                    case DataType.Time:
                        validator.Enabled = true;
                        DateValidator.ErrorMessage = HttpUtility.HtmlEncode(Column.DataTypeAttribute.FormatErrorMessage(Column.DisplayName));
                        break;
                }
            }
            else if (Column.ColumnType.Equals(typeof(DateTime))) {
                validator.Enabled = true;
                DateValidator.ErrorMessage = HttpUtility.HtmlEncode(DefaultDateAttribute.FormatErrorMessage(Column.DisplayName));
            }
        }
    
        protected void DateValidator_ServerValidate(object source, ServerValidateEventArgs args) {
            DateTime dummyResult;
            args.IsValid = DateTime.TryParse(args.Value, out dummyResult);
        }
    
        protected override void ExtractValues(IOrderedDictionary dictionary) {
            dictionary[Column.Name] = ConvertEditedValue(TextBox1.Text + ' ' + Hours.Text + ':' + Minutes.Text);
        }
    
        public override Control DataControl {
            get {
                return TextBox1;
            }
        }

		protected void HoursValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			short temp = -1;
			args.IsValid &= short.TryParse(Hours.Text, out temp);
			args.IsValid &= (temp >= 0) && (temp <= 23);
		}

		protected void MinutesValidator_ServerValidate(object source, ServerValidateEventArgs args)
		{
			short temp = -1;
			args.IsValid &= short.TryParse(Minutes.Text, out temp);
			args.IsValid &= (temp >= 0) && (temp <= 59);
		}
    
    }
}
