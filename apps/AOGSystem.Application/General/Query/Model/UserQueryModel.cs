using AOGSystem.Domain.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AOGSystem.Application.General.Query.Model
{
    public class UserQuerySummary
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        internal static User ToModel(UserQueryModel item)
        {
            return new User(
                item.UserName,
                item.FirstName,
                item.LastName,
                item.Email,
                item.PhoneNumber);
        }
        internal static List<User> ToModel(List<UserQueryModel> lists)
        {
            return lists.Select(x => ToModel(x)).ToList();

        }
    }
    public class UserQueryModel : UserQuerySummary
    {
    }
}
