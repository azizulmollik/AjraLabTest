using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Models;
using WebApp.Core.Utlities;

namespace WebApp.Core.ViewModels
{
    public class PersonViewModel
    {
        public Person Person { get; set; }
        public PersonDetail PersonDetail { get; set; }
        public List<PersonDetail> DataList { get; set; }
        public Message Message { get; set; }
        public List<SkillsViewModel> SkillList { get; set; }
        //Paging
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }        
    }
}
