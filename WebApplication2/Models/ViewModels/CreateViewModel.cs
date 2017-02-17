using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication2.Models.ViewModels
{
    public class CreateViewModel
    {
        public string name { get; set; }

        public List<SelectListItem> clientes { get; set; }


        public List<int> clientsSelected { get; set; }


    }

    //public class clientViewModel { 
    //    public clientViewModel(Client client)
    //    {
    //        id = client.ID;
    //        name = client.Cliente;
    //    }
    //    public int id { get; set; }
    //    public string name { get; set; }
    //}
}