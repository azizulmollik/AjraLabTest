using WebApp.Core.ViewModels;
using System.Collections.Generic;
using Dapper;
using System.Linq;
using WebApp.Core.Models;
using WebApp.Core.Utlities;

namespace WebApp.Core.DAL
{
    public class CommonRepository : DataCommon
    {
        public List<DropDownListViewModel> GetAllCountry()
        {
            return DbCon.Query<DropDownListViewModel>("select * from (Select '' Value, 'Select Country' [Text] Union Select cast(Id as Varchar) Value, Country [Text] from dbo.Country)x order by x.Value").ToList();
        }
        public List<DropDownListViewModel> GetAllCity(int countryId)
        {
            return DbCon.Query<DropDownListViewModel>("select * from (Select '' Value, 'Select City' [Text] Union Select cast(Id as Varchar) Value, City [Text] from dbo.City Where CountryId=" + countryId+ ")x order by x.Value").ToList();
        }
        public List<SkillsViewModel> GetAllSkill()
        {
            return DbCon.Query<SkillsViewModel>("Select Id, Skill, 0 as Selected from dbo.Skills order by Id").ToList();
        }
    }
}
