﻿using CI_Platform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CI_Platform.Repositories.Interfaces
{
    public interface IAdminRepository
    {
        List<Admin> GetAdmins(string query, int recSkip, int recTake);

        Admin GetAdminById(long id);

        int GetTotalAdminsRecord(string query);

        void InsertAdmin(Admin admin);
    }
}
