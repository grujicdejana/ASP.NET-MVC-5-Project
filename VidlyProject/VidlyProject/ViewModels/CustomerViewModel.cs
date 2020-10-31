using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace VidlyProject.ViewModels
{
    public class CustomerViewModel
    {
        public string Name { get; set; }
        public bool IsSubscribedToNewsletter { get; set; }
        public DateTime? Birthdate { get; set; }
    }
}