using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Testb
{
    public class Message 
    {
        public static Dictionary<string, List<System.Delegate>> Handlers= new Dictionary<string, List<System.Delegate>>();

        public static void AddMessage<T>(Action<T> action,string messagename=null)where T : Message
        {
            string typename = null;
            if(string.IsNullOrEmpty(messagename)==false)
            {
                typename = typeof(T).ToString() + messagename;
            }
            else
            {
                typename = typeof(T).ToString();
            }

            if(Handlers.ContainsKey(typename))
            {
                var actions=Handlers[typename];
                var findaction= actions.Find(o=>o.Method==action.Method &&o.Target==action.Target);

                if(findaction!=null)
                {
                    Debug.LogError("Same Function is already exist!! Check Handlers");
                }
                else
                {
                    Handlers[typename].Add(action);
                }
            }
            else
            {
                List<Delegate> del = new List<Delegate>();
                del.Add(action);
                Handlers.Add(typename, del);
            }
        }

        public static void RemoveMessage<T>(Action<T> action, string messagename = null) where T: Message
        {
            string typename = null;
            if (string.IsNullOrEmpty(messagename) == false)
            {
                typename = typeof(T).ToString() + messagename;
            }
            else
            {
                typename = typeof(T).ToString();
            }

            if (Handlers.ContainsKey(typename))
            {
                var actions = Handlers[typename];
                var findaction = actions.Find(o => o.Method == action.Method && o.Target == action.Target);
                if(findaction!=null)
                {
                    Handlers[typename].Remove(findaction);
                }
                else
                {
                    Debug.LogError("Function is not exist! Check Handlers");
                }
            }
            else
            {
                Debug.LogError("Function is not exist! Check Handlers");
            }
        }

  
        public static void Send<T>(T e,string messageName=null)where T:Message
        {
            string typename = null;

            if(string.IsNullOrEmpty(messageName)==false)
            {
                typename = typeof(T).ToString() + messageName;
            }
            else
            {
                typename= typeof(T).ToString();
            }
            if(Handlers.ContainsKey(typename))
            {
                foreach(var action in Handlers[typename])
                {
                    if (action.GetType() != typeof(Action<T>))
                        continue;
                    var _event = (Action<T>)action;
                    _event.Invoke(e);
                }
            }


        }
    }
}
