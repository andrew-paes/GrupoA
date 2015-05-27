using System;
using System.Data;
using Ag2.Manager.BusinessObject;
using Ag2.Manager.Module;
namespace Ag2.Manager.DAL
{
    interface IManagerModuleDAL
    {
        string addWhere(string SQL, string where);
        string DataBaseName { get; set; }
        void DeleteFormData(Ag2.Manager.Module.ManagerModuleStructure managerModule, string registerId);
        System.Data.DataSet GetDataTable(string SQL, System.Collections.ObjectModel.Collection<Ag2.Manager.Module.ManagerModuleField> filters);
        System.Data.DataSet GetDataTable(string SQL);
        System.Data.DataSet GetDataTableUniqueValue(string SQL, System.Collections.Generic.List<Ag2.Manager.Module.Constraint> constraints);
        bool SaveFormData(Ag2.Manager.Module.ManagerModuleStructure managerModule, int registerId);
        DataTable FillForm(Ag2.Manager.Module.ManagerModuleStructure _moduleStructure, int registerID);

        /// <summary>
        /// Executa command Procedure configurado no Xml do módulo
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        DataSet GetDataTableByStoredProcedureCommand(Ag2.Manager.Module.ManagerModuleStructure _moduleStructure, Query query);

        DataSet GetComboData(ManagerModuleFieldListBox managerField, Ag2.Manager.Module.ManagerModuleStructure _moduleStructure);
        DataSet GetComboData(ManagerModuleFieldListBox managerField, string filterField, string filterValue, Ag2.Manager.Module.ManagerModuleStructure _moduleStructure);
    }
}
