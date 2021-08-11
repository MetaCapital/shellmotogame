
using System;
using System.Collections.Generic;
using FullSerializer;

public static class DatabaseHandler
{
    private static readonly string databaseURL = $"https://motorcycle-d6719-default-rtdb.firebaseio.com/";

    private static fsSerializer serializer = new fsSerializer();
    public static int tex;
    public delegate void PostUserCallback();
    public delegate void GetUserCallback(User user);
    public delegate void GetUsersCallback(Dictionary<string, User> users);


    /// <summary>
    /// Adds a user to the Firebase Database
    /// </summary>
    /// <param name="user"> User object that will be uploaded </param>
    /// <param name="userId"> Id of the user that will be uploaded </param>
    /// <param name="callback"> What to do after the user is uploaded successfully </param>
    public static void PostUser(User user, string userId, PostUserCallback callback)
    {
      //  RestClient.Put<User>($"{databaseURL}users/{userId}.json", user).Then(response => { callback(); });
    }

   

    /// <summary>
    /// Retrieves a user from the Firebase Database, given their id
    /// </summary>
    /// <param name="userId"> Id of the user that we are looking for </param>
    /// <param name="callback"> What to do after the user is downloaded successfully </param>
    public static void GetUser(string userId, GetUserCallback callback)
    {
       /*  RestClient.Get<User>($"{databaseURL}users/{userId}.json").Then(user =>
        {
            callback(user);
        });*/
    }

    /// <summary>
    /// Gets all users from the Firebase Database
    /// </summary>
    /// <param name="callback"> What to do after all users are downloaded successfully </param>
    public static void GetUsers(GetUsersCallback callback)
    {
      /*  RestClient.Get($"{databaseURL}users.json").Then(response =>
        {
            var responseJson = response.Text;

            // Using the FullSerializer library: https://github.com/jacobdufault/fullserializer
            // to serialize more complex types (a Dictionary, in this case)
            var data = fsJsonParser.Parse(responseJson);
            object deserialized = null;
            serializer.TryDeserialize(data, typeof(Dictionary<string, User>), ref deserialized);

            var users = deserialized as Dictionary<string, User>;
            callback(users);
        });*/
    }

}

public class User
{
   
    public float score;
    public string name;

    public User()
    {
    }

    public User(float score,string name)
    {
        this.score = score;
        this.name = name;
        
    }
}