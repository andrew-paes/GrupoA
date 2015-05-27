using System;

namespace Ag2.Manager.DAL
{
    interface IManageableDropDownListADO
    {
        bool Delete(string tableName, string primeryKey, object primaryKeyValue);
        bool RegisterExist(string tableName, string primeryKey, string primaryKeyValue, string fieldDescriptionName, string fieldDescriptionValue);
        System.Web.UI.WebControls.ListItem Save(string tableName, string primeryKey, string fieldDescriptionName, object primaryKeyValue, string fieldDescriptionValue);
        System.Web.UI.WebControls.ListItemCollection SelectAll(string tableName, string primeryKey, string fieldDescriptionName);
    }
}
