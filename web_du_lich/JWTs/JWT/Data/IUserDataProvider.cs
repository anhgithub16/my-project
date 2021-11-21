using JWT.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JWT.Data
{
    public interface IUserDataProvider
    {
        public List<Users> getUser(string users);
        public List<User> getEmpCode(string empCode);
        public ExcutionResultAuth UpdatePassword(User user);
        public ExcutionResultAuth Insert(Users users);
        public List<Users> GetById(Users users);
    }
}
