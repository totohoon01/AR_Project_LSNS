using System;


namespace Post
{
    public class UserPost
    {
        private string message;
        private string createTime;
        private string userPos;

        public UserPost(string message, string createTime, string userPos)
        {
            this.message = message;
            this.createTime = createTime;
            this.userPos = userPos;
        }
    }
}