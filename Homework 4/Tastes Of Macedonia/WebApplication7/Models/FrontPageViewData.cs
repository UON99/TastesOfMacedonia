using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using WebApplication7.Models;

namespace WebApplication7.Models
{
    public class FrontPageViewData
    {

        public mytable Mytable { get; set; }
        public masterEntities db { get; set; }
        
        public FrontPageViewData()
        {

        }
        
    }
        
        
}

    

