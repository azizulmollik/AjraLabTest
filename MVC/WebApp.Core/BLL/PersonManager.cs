using System;
using System.Collections.Generic;
using WebApp.Core.DAL;
using WebApp.Core.Models;
using WebApp.Core.Utlities;

namespace WebApp.Core.BLL
{
    public class PersonManager
    {
        private PersonRepository personRepo;
        
        #region CONSTRUCTOR
        public PersonManager()
        {
            personRepo = new PersonRepository();
        }
        #endregion

        public Message CreateOrUpdate(Person data)
        {
            try
            {
                var i = personRepo.CreateOrUpdate(data);
                var message = new Message();
                if (i > 0)
                {
                    message.MessageType = MessageTypes.Success;
                    message.CurrentMessage = "Data Saved Successfully";
                    return message;
                }
                else
                {
                    message.MessageType = MessageTypes.Warning;
                    message.CurrentMessage = "Data Can Not Be Saved";
                    return message;
                }
            }
            catch (Exception ex)
            {
                var message = new Message();
                message.MessageType = MessageTypes.Error;
                message.CurrentMessage = "Exception Occurred ! Please contact System Administrator.";
                return message;
            }

        }

        public List<PersonDetail> GetAllWithPagination(int currentPage, int pageSize, string searchvalue, string sortingColumn, string sortingOrder)
        {
            return personRepo.GetAllWithPagination(currentPage, pageSize, searchvalue, sortingColumn, sortingOrder);
        }

        public List<PersonDetail> GetAll()
        {
            return personRepo.GetAll();
        }

        public Person Get(int id)
        {
            return personRepo.Get(id);
        }

        public Message Delete(int id)
        {
            try
            {
                var i = personRepo.Delete(id);

                var message = new Message();
                if (i > 0)
                {
                    message.MessageType = MessageTypes.Success;
                    message.CurrentMessage = "Data Deleted Successfully";
                    return message;
                }
                else
                {
                    message.MessageType = MessageTypes.Warning;
                    message.CurrentMessage = "Data Can Not Be Deleted";
                    return message;
                }
            }
            catch (Exception ex)
            {
                var message = new Message();
                message.MessageType = MessageTypes.Error;
                message.CurrentMessage = "Exception Occurred ! Please contact System Administrator.";
                return message;
            }
        }
    }
}

