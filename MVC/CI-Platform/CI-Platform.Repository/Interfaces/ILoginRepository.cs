using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repository.Interfaces
{
    internal interface ILoginRepository
    {
        public void Email(string body, string mailid);
    }
}
