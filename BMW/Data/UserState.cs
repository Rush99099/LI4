using System;

namespace BMW.Data
{
    public class UserState
    {
        public int UserId { get; private set; }
        public string UserName { get; private set; } = string.Empty;

        public void SetUser(int userId, string userName)
        {
            UserId = userId;
            UserName = userName;
        }

        public void ClearUser()
        {
            UserId = 0;
            UserName = string.Empty;
        }
    }
}
