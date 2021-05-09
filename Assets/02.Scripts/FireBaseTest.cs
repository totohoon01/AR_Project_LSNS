using UnityEngine;
using Firebase;
using Firebase.Database;

public class FireBaseTest : MonoBehaviour
{
    class Post
    {
        public string author;
        public string password;
        public string contents;
        public string pos;
        public string time;

        public Post(string author, string password, string contents, string pos, string time)
        {
            this.author = author;
            this.password = password;
            this.contents = contents;
            this.pos = pos;
            this.time = time;
        }

    }


}