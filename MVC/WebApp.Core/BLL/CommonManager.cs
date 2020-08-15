using WebApp.Core.DAL;
using WebApp.Core.Utlities;
using WebApp.Core.ViewModels;
using System.Collections.Generic;

namespace WebApp.Core.BLL
{
    public class CommonManager
    {        
        private CommonRepository commonRepo;      

        #region CONSTRUCTOR
        public CommonManager()
        {            
            commonRepo = new CommonRepository();
        }
        #endregion
        
        
        public List<DropDownListViewModel> GetAllCountry()
        {
            return commonRepo.GetAllCountry();
        }
        public List<DropDownListViewModel> GetAllCity(int countryId)
        {
            return commonRepo.GetAllCity(countryId);
        }
        public List<SkillsViewModel>GetAllSkill()
        {
            return commonRepo.GetAllSkill();
        }
    }
}
