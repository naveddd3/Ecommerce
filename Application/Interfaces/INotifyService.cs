
using Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INotifyService
    {
        void SendEmails(string email, string subject, string body);
        Response SendEmails(string email,int OTP);
    }
}
