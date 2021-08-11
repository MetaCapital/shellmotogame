using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
//using System.Collections.Generic;
using UnityEngine.UI;

public class register : MonoBehaviour
{
    //public GameObject gar;
    public GameObject leaderbord,leaderbutton,registesr,navme,buttons;
    public GameObject[] scoresidtocolor;
    public TMP_Text[] IDS;
    public TMP_Text[] names;
    public static int counter = 0;
    public static float score = 0;
    public static string[] lista = new string[300];
    public static float[] listaf = new float[300];
    public static float indexscore;
    
    private string namo = "";
  


    public Scrollbar scroll;
   
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void OnAppStart()
    {

        // For Test
        /* 
         * this is for test don't forget to incomment this
         * if (PlayerPrefs.HasKey("count"))
          {
              print(PlayerPrefs.GetString("count") + "count");
          }
          else
          {
              PlayerPrefs.SetString("count", "0");
              PlayerPrefs.Save();
          }*/
        /* DatabaseHandler.GetUsers(users =>
         {

             foreach (var user in users)
             {
                 Debug.Log($"one2");
                 counter++;
             }
             int c = counter + 1;
             PlayerPrefs.SetString("count", c.ToString());
             PlayerPrefs.Save();
         });*/
        /*   var user1 = new User(4.41f);
           DatabaseHandler.PostUser(user1, "0", () =>
           {
               DatabaseHandler.GetUser("11", user =>
               {
                   Debug.Log($"{user.score}");
               });


           });*/
        /*   var user1e = new User(0);
           DatabaseHandler.PostUser(user1e, "123", () =>
           {


           });*/
        
    }
    private void Awake()
    {
        
            User user2 = new User(11.2f, "fev");
            DatabaseHandler.PostUser(user2,"88", () =>
            {

            });
        
     
        //DontDestroyOnLoad(transform.gameObject);
      //PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey("count")&& registesr!=null)
        {
           DatabaseHandler.GetUsers(users =>
        {
           
           
            int c = users.Count + 1;
            if (!PlayerPrefs.HasKey("count"))
            {
                int y =UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
                if (y == 0)
                {
                    registesr.SetActive(true);
                }
                
                PlayerPrefs.SetString("count", c.ToString());
                PlayerPrefs.Save();
            }

        });

           // var user2 = new User(PlayerPrefs.GetFloat("bestDistance"), PlayerPrefs.GetString("count"));
            // int cc = counter + 1;
           // print("platerpref" + PlayerPrefs.GetString("count"));
            //For Test
           // DatabaseHandler.PostUser(user2, PlayerPrefs.GetString("count"), () => { });
        }
        else if(PlayerPrefs.GetString("count")=="0")
        {
            print("entered");
            DatabaseHandler.GetUsers(users =>
            {
                print(users.Count+"users");
                foreach (var user in users)
                {
                    print("enteed");
                    //Debug.Log($"one2");
                  //  counter++;
                }
                int c = users.Count + 1;
                PlayerPrefs.SetString("count", c.ToString());
                PlayerPrefs.Save();
            });
            int y = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
            if (y == 0)
                {
                    registesr.SetActive(true);
                }
        }
        else
        {
            registesr.SetActive(false);
        }
    }
    /* private static void OnAppStart()
     {

       var  user2 = new User(score);

        // DatabaseHandler.PostUserid(1);
         //var list = DatabaseHandler.GetUsers();


         DatabaseHandler.GetUsers(users =>
         {
             int count=0;
             foreach (var user in users)
             {
                 Debug.Log($"{user.Value.score} ");
             }
           /*  foreach (var user in users)
             {
                 print("counts");
                 count++;
             }
    print(count+"count");
            counter = count + 1;
           
        });
        print(score+"score");
        PlayerPrefs.SetInt("id", counter) ;
        PlayerPrefs.Save();
        DatabaseHandler.PostUser(user2, counter.ToString(), () =>
        {
            /* DatabaseHandler.GetUser("11", user =>
             {
                 Firebase.Analytics.FirebaseAnalytics.SetUserId("");
                 Debug.Log($"{user.score}");
             });
        });

    }*/




    /*  var user2 = new User(61);
        DatabaseHandler.GetUser("12", user =>
        {
            Debug.Log($"{user.score} ");
        }); */

    // Start is called before the first frame update
    void Start()
    {
       // leaderbutton.SetActive(true);
        leaderbord.SetActive(false);
       // sortleaderbord();
      // print(DatabaseHandler.PostUserid()+"t"); 
    }

    // Update is called once per frame
    void Update()
    {
       
        if (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex != 0)
        {
            if (PlayerPrefs.GetFloat("bestDistance") > score)
            {
                score = PlayerPrefs.GetFloat("bestDistance");
            }
            if (PlayerPrefs.HasKey("name"))
            {
                print(PlayerPrefs.GetString("name") + "gggggggggg");
            }
            
           
              /*  User user2 = new User(PlayerPrefs.GetFloat("bestDistance"), PlayerPrefs.GetString("name"));

                DatabaseHandler.PostUser(user2, PlayerPrefs.GetString("count"), () =>
                {

                });*/
            
        }
       
       /* if (registesr.activeSelf)
        {
            leaderbutton.SetActive(false);
        }*/
        print(PlayerPrefs.GetString("name") + "name");
        print(PlayerPrefs.GetString("count") + "count");
        //  if(PlayerPrefs.GetInt("id")!=0)
        // print(PlayerPrefs.GetInt("id")+"st nice");
    }
    public void closeleaderbord()
    {
        leaderbord.SetActive(false);
        
    }
    public void closeleaderbutton()
    {
        
        leaderbutton.SetActive(false);
    }
    public void sortleaderbord()
    {
        Hashtable usershash = new Hashtable();
        Hashtable hash = new Hashtable();
        // IEnumerable<float> c = from x in Collection
        //User[] u=new User[200];
        leaderbord.SetActive(true);

        DatabaseHandler.GetUsers(users =>
        {
            int co = 0;
            print("entereddd");
            // IDictionary<KeyValuePair<string, float>> numberNames = new Dictionary<string, float>();
            int limit =users.Count;
            // IEnumerable< User> query = users.OrderBy(pet => pet.Value.score);
            print("limit" + limit);
            //hash.Add( users.Values);
           
            foreach (var user in users)
            {
            
               // hash.Add(user.Value.score.ToString(), user.Value.name);

                // numberNames.Add(user.Value.name, user.Value.score);
                // u[co] = user.Value;
                if (co < limit)
                {

                    // IDS[co].text = user.Key.ToString();
                    lista[co] = user.Value.name;
                    listaf[co] = user.Value.score;
                    // names[co].text = user.Value.name;
                    //  names[co]=user.
                    //print(lista[co].score + "you good to go");
                    string d = PlayerPrefs.GetString("count");
                    if (user.Key == d)
                    {
                        indexscore = user.Value.score;
                        Debug.Log($"me" + co + " : " + user.Value.score + "\n");
                    }
                    else
                    {
                        Debug.Log($"player" + co + " : " + user.Value.score + "\n");
                    }


                    co++;

                }
                else
                {
                    break;
                }

                //if(float.Parse( user.Value.score.ToString()))
                // print(user.Value.score);
                // float i = float.Parse(user.Value.score);

                //  print(lista[co ].score.ToString() + "lista item");



            }
            foreach (DictionaryEntry sc in hash)
            {
                print("value : " + sc.Value + "  score : " + sc.Key);
            }
            print("entereddd2" + hash.Count);
            /*  var listaOrdered  = from p in lista
                         orderby p.score
                         select p;*/

            //  lista.ForEach<User>(qry.ToArray<User>(), p => print(p.score + "scored you biiitch " ));
            //  lista.Sort();
            // List<User> objListOrder = lista<User>();
            // List<User> listaOrdered = lista.OrderBy(o => o.score.ToString()).ToList();
            // listaOrdered.ToList();
            /*  listaOrdered.Sort(
                     delegate (User p1, User p2)
                     {
                         return p1.score.CompareTo(p2.score);
                     }
                 );*/

            /*    if (lista == null)
                {
                    print("it's null");
                }
                else
                {
                    print(lista[0].score);
                }*/

            /* Dictionary<string, float> oWith =
             new Dictionary<string, float>();
             int iner =int.Parse( PlayerPrefs.GetString("count"));
             for (int i = 0; i < iner-1; i++)
             {
                 oWith.Add(lista[i], listaf[i]);
             }
             var myList = oWith.ToList();

             myList.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));*/
            print("lista count");
            List<float> lies = listaf.ToList();
            lies.Sort();
            lies.Reverse();
            /*  List<string> lies2 = lista.ToList();
              lies2.Sort();
              lies2.Reverse();*/
            // var   listo = lista.OrderBy(user => user.score);
            // listaOrdered.ToList();
            //lista.OrderBy(d => d.score);
            // List<User> listo ;
            //  listo.AddRange(lista);
            // List<User> sortedUsers = lista.OrderBy(user => user.score).ThenBy(user => user.score);
            // int c = 0;
            /* foreach (DictionaryEntry sc in hash)
             {
                 print("value : " + sc.Value + "  score : " + sc.Key);
             }*/
            for (int i = 0; i < lies.Count; i++)
            {
                //hashusers egale sorted score and  names
                  print(lies[i].ToString() + "you did  ittttttttttt");
                foreach (DictionaryEntry sc in hash)
                {
                    print(sc.Value.ToString() + "niiiiiiiiice");
                    string f = sc.Key.ToString();
                    string f2 = lies[i].ToString();
                    if (f2.Equals(f))
                    {
                        print("niiiiiiiiiiiiiiiiiiiice2");
                        float ff = Random.Range(10, 10000);
                        string name = "player" + ff;
                        usershash.Add(sc.Key.ToString(), sc.Value.ToString());
                        if (sc.Value.ToString() == "")
                        {
                            //usershash.Add(name, sc.Value.ToString());
                        }

                    }
                }
            }
            foreach (DictionaryEntry sc in usershash)
            {
                print("value : " + sc.Value + "  score : " + sc.Key);
            }

            int index = int.Parse(PlayerPrefs.GetString("count"));
            int cu = 0;
            Image im;

            for (int i = 0; i < 10; i++)
            {

                // string s = indexscore.ToString();
                if (indexscore == lies[i])
                {
                    im = scoresidtocolor[cu].GetComponent<Image>();
                    var tempColor = im.color;
                    tempColor.a = 1f;
                    tempColor = Color.blue;
                    im.color = tempColor;
                    IDS[i].color = Color.yellow;
                    names[i].color = Color.yellow;
                    IDS[i].text = lies[i].ToString();
                    foreach (DictionaryEntry sc in usershash)
                    {
                        string s = sc.Key.ToString();
                        if (s.Equals(lies[i].ToString()))
                        {
                            names[i].text = sc.Value.ToString();
                            break;
                        }
                    }
                    // names[i].text = sc.Value.ToString();


                    if (cu > 8)
                    {
                        scroll.value = 0;
                    }
                    else if (cu < 3)
                    {
                        scroll.value = 1;
                    }
                }
                else
                {
                    IDS[i].text = lies[i].ToString();
                    foreach (DictionaryEntry sc in usershash)
                    {
                        string s = sc.Key.ToString();
                        if (s.Equals(lies[i].ToString()))
                        {
                            names[i].text = sc.Value.ToString();
                            break;
                        }
                    }
                    //  names[i].text = sc.Value.ToString();

                    // sc.Value.ToString();  
                }

                print("lista count2");
                cu++;

                // print(lista[i].ToString()+"list");

            }
            /*   foreach (DictionaryEntry sc in usershash)
               {
                   if (cu == 10)
                   {
                       break;
                   }
                   string s = indexscore.ToString();
                   if (s == sc.Key.ToString())
                   {
                       im = scoresidtocolor[cu].GetComponent<Image>();
                       var tempColor = im.color;
                       //  tempColor.a = 1f;
                       //  tempColor = Color.blue;
                       //  im.color = tempColor;
                       //  IDS[cu].color = Color.yellow;
                       // names[cu].color = Color.yellow;
                      // IDS[cu].text = sc.Key.ToString();

                       names[cu].text = sc.Value.ToString();


                       if (cu > 8)
                       {
                           scroll.value = 0;
                       }
                       else if (cu < 3)
                       {
                           scroll.value = 1;
                       }
                   }
                   else
                   {
                      // IDS[cu].text = sc.Key.ToString();

                       names[cu].text = sc.Value.ToString();

                       // sc.Value.ToString();  
                   }

                   print("lista count2");
                   cu++;

               }*/
            /*  foreach (var score in lista)
              {
                  //if key value == playerprefs count then it's me
                //  print(lista[cu].score + "lista ordered count");
                  print(score.ToString()+"scored"+"and  key ");
                  cu++;
               /*   if (cu == int.Parse(score.Key.ToString()))
                  {

                      print("me"+score);

                  }
                  else
                  {
                      print(score);
                  }

              }*/
            /* int c = counter + 1;
               PlayerPrefs.SetString("count", c.ToString());
               PlayerPrefs.Save();*/
        });
    }
   public void closepan()
    {
        if (PlayerPrefs.HasKey("name"))
        {
            string pd = PlayerPrefs.GetString("name");
            if (pd != "")
            {
                registesr.SetActive(false);
            }

        }

    }
    public void SETSCORE()
    {
        namo = navme.GetComponent<InputField>().text.ToString();
        PlayerPrefs.SetString("name", namo);
        PlayerPrefs.Save();
        
     
        print(PlayerPrefs.GetFloat("bestDistance") + "score");
        
        
    }
    public void setscore2()
    {
        string nm = PlayerPrefs.GetFloat("count").ToString() + "name";
        string str = PlayerPrefs.GetString("name");
        User user2 = new User(PlayerPrefs.GetFloat("bestDistance"), str);

            DatabaseHandler.PostUser(user2, PlayerPrefs.GetString("count"), () =>
            {

            });
        
        
    }
    private void OnApplicationQuit()
    {
       /* if (PlayerPrefs.GetFloat("bestDistance") > score)
        {
            score = PlayerPrefs.GetFloat("bestDistance");
        }
        
        print(score + "score");
        User user2 = new User(score);
        //int c = counter + 1;
        DatabaseHandler.PostUser(user2, PlayerPrefs.GetString("count"), () =>
        {

        });*/
       /*  var user2 = new User(PlayerPrefs.GetFloat("score"), PlayerPrefs.GetString("name"));
         DatabaseHandler.PostUser(user2, PlayerPrefs.GetString("count"), () =>
         {
             
         });*/

    }

    public void OnBeforeSerialize()
    {
        throw new System.NotImplementedException();
    }

    public void OnAfterDeserialize()
    {
        throw new System.NotImplementedException();
    }
}
