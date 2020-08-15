using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models;
using WebApp.Core.Utlities;
using Dapper;
using System.Data;

namespace WebApp.Core.DAL
{
    class PersonRepository:DataCommon
    {
        public int CreateOrUpdate(Person data)
        {
            var _params = new DynamicParameters();
            _params.Add("@Id", data.Id);
            _params.Add("@Name", data.Name);
            _params.Add("@CountryId", data.CountryId);
            _params.Add("@CityId", data.CityId);
            _params.Add("@LanguageSkills", data.LanguageSkills);
            _params.Add("@DOB", data.DOB);
            if (data.Attachment != null)
                _params.Add("@Attachment", data.Attachment==null? new byte[] { } : data.Attachment);
            _params.Add("@FileName", data.FileName);
            _params.Add("@FileContentType", data.FileContentType);

            int i = DbCon.Execute("SP_CreateOrUpdatePerson", _params, commandType: CommandType.StoredProcedure);
            return i;
        }

        public List<PersonDetail> GetAllWithPagination(int currentPage, int pageSize, string searchvalue, string sortingColumn, string sortingOrder)
        {
            var _params = new DynamicParameters();
            if(currentPage > 0)
                _params.Add("@currentPage", currentPage);
            if (pageSize > 0)
                _params.Add("@pageSize", pageSize);
            if (searchvalue != "")
                _params.Add("@filter", searchvalue);
            if (sortingColumn != "")
                _params.Add("@sortingColumn", sortingColumn);
            if (sortingOrder != "")
                _params.Add("@sortingOrder", sortingOrder);

            var data = DbCon.Query<PersonDetail>("SP_GetAllPersonWithPagination_SQL2008", _params, commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public List<PersonDetail> GetAll()
        {
            var data = DbCon.Query<PersonDetail>("SP_GetPerson",commandType: CommandType.StoredProcedure);
            return data.ToList();
        }

        public Person Get(int id)
        {
            string sql = "SELECT * FROM PersonInfo WHERE Id = @Id";
            var _params = new DynamicParameters();
            _params.Add("@Id", id);
            var data = DbCon.QueryFirstOrDefault<Person>(sql, _params);
            return data;
        }

        public int Delete(int id)
        {
            string sql = "DELETE FROM PersonInfo WHERE Id = @Id";
            var _params = new DynamicParameters();
            _params.Add("@Id", id);
            int i = DbCon.Execute(sql, _params, commandType: CommandType.Text);
            return i;
        }
    }
}
