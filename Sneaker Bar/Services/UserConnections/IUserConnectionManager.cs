using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sneaker_Bar.Services.UserConnections
{
    public interface IUserConnectionManager
    {
        public string GetConnectionIdByName(string username);
        public void ConnectUser(string username, string connectionId);
        public void DisconnectUser(string username, string connectionId);
    }
}
