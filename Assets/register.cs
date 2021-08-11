using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public class register : MonoBehaviour
{
    public GameObject leaderbord;
    public GameObject[] scoresidtocolor;
    public TMP_Text[] IDS;
    public static int counter = 0;
    public static float score = 0;
    public static  User[] lista = new User[30];
    public static float[] listaf = new float[30];
    public static float indexscore;
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
     /*   DatabaseHandler.GetUsers(users =>
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
        var user1 = new User(4.41f);
        DatabaseHandler.PostUser(user1, "0", () =>
        {
            DatabaseHandler.GetUser("11", user =>
            {
                Debug.Log($"{user.score}");
            });

           
        });
        var user1e = new User(0);
        DatabaseHandler.PostUser(user1e, "123", () =>
        {
            

        });
        DatabaseHandler.GetUsers(users =>
        {
            print("entered");
            foreach (var user in users)
            {
                Debug.Log($"one2");
                counter++;
            }
         int c = counter + 1;
            PlayerPrefs.SetString("count", c.ToString());
            PlayerPrefs.Save();
        });
        var user2 = new User(17);
        int cc = counter + 1;
        print("platerpref" + PlayerPrefs.GetString("count"));
        //For Test
        DatabaseHandler.PostUser(user2, PlayerPrefs.GetString("count"), () => { });
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
        leaderbord.SetActive(false);
       // sortleaderbord();
      // print(DatabaseHandler.PostUserid()+"t"); 
    }

    // Update is called once per frame
    void Update()
    {
       
      //  if(PlayerPrefs.GetInt("id")!=0)
       // print(PlayerPrefs.GetInt("id")+"st nice");
    }
    public void closeleaderbord()
    {
        leaderbord.SetActive(false);
    }
    public void sortleaderbord()
    {
        // IEnumerable<float> c = from x in Collection
        leaderbord.SetActive(true);
    DatabaseHandler.GetUsers(users =>
        {
            int co = 0;
            print("entereddd");
            int limit = int.Parse(PlayerPrefs.GetString("count"));
            foreach (var user in users)
            {
                if (co < limit)
                {

                    // IDS[co].text = user.Key.ToString();
                    lista[co] = user.Value;
                    listaf[co] = user.Value.score;
                    print(lista[co].score + "you good to go");
                    if (user.Key == PlayerPrefs.GetString("count"))
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
            print("entereddd2");
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
            print("lista count");
           List<float> lies=   listaf.ToList();
            lies.Sort();
            lies.Reverse();
        // var   listo = lista.OrderBy(user => user.score);
        // listaOrdered.ToList();
        //lista.OrderBy(d => d.score);
        // List<User> listo ;
        //  listo.AddRange(lista);
        // List<User> sortedUsers = lista.OrderBy(user => user.score).ThenBy(user => user.score);
        foreach (var s in lies)
            {
                print(s + "list pro");
               // IDS[i].text = lies[i].score.ToString();
            }
                print("lista count2");
            int index = int.Parse(PlayerPrefs.GetString("count"));
            int cu = 0;
            Image im;
            for(int i = 0; i < 20; i++)
            {
                if(indexscore== lies[i])
                {
                  im=  scoresidtocolor[i].GetComponent<Image>();
                    var tempColor = im.color;
                    tempColor.a = 1f;
                    tempColor = Color.blue;
                    im.color = tempColor;
                    IDS[i].color = Color.yellow;
                    IDS[i].text = lies[i].ToString("0.00");
                if (i > 5 && i<11)
                    {
                        scroll.value = .65f;
                    }else if(i>11 && i < 13)
                    {
                        scroll.value = 48f;
                    }else if (i > 13)
                    {
                        scroll.value = 0;
                    }
                }
                else
                {
                    IDS[i].text = lies[i].ToString("0.00");
                }
               // print(lista[i].score.ToString()+"list");
                
            }
            foreach (var score in lista)
            {
                //if key value == playerprefs count then it's me
                print(lista[cu].score + "lista ordered count");
                print(score.ToString()+"scored"+"and  key ");
                cu++;
             /*   if (cu == int.Parse(score.Key.ToString()))
                {
                   
                    print("me"+score);

                }
                else
                {
                    print(score);
                }*/
               
            }
             int c = counter + 1;
               PlayerPrefs.SetString("count", c.ToString());
               PlayerPrefs.Save();
        });
    }
    private void OnApplicationQuit()
    {
        if (PlayerPrefs.GetFloat("bestDistance") > score)
        {
            score = PlayerPrefs.GetFloat("bestDistance");
        }
        print(score + "score");
        User user2 = new User(score);
        //int c = counter + 1;
        DatabaseHandler.PostUser(user2, PlayerPrefs.GetString("count"), () =>
        {

        });
        /* var user2 = new User("hamid");
         DatabaseHandler.PostUser(user2, counter.ToString(), () =>
         {
             /* DatabaseHandler.GetUser("11", user =>
              {
                  Firebase.Analytics.FirebaseAnalytics.SetUserId("");
                  Debug.Log($"{user.score}");
              });
         });*/

    }
}
