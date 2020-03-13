using DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public interface IUserAccessor
    {
        User AuthenticateUser(string username, string passwordHash);
        bool UpdatePasswordHash(int userID, string oldPassHash, string newPassHash);
        List<User> SelectUserByActive(bool active = true);

        List<User> SelectUserByRole(string roleID);

        int UpdateUser(User oldUser, User newUser);
        int InsertUser(User user);
        int DeactivateUser(int userID);
        int ActivateUser(int userID);
        List<string> SelectAllRoles();
        List<string> SelectRolesByUserID(int userID);
        
        int InsertOrDeleteUserRole(int userid, string roleID, bool delete = false);

    }
}
