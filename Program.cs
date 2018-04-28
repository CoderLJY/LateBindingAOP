using System;
using System.Reflection;

namespace LateBindingAOP
{
    class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public User(int id, string username)
        {
            ID = id;
            UserName = username;
        }
        public void displayName()
        {
            Console.WriteLine("Name: " + this.UserName);
        }
    }


    class Program
    {
        static void AopProcess(Action logingbefore, Action logingafter)
        {
            logingbefore();

            Assembly exceutable = Assembly.GetExecutingAssembly();
            Type ltype = exceutable.GetType("LateBindingAOP.User");

            object objectAction = Activator.CreateInstance(ltype, 1234, "hahah");

            MethodInfo method = ltype.GetMethod("displayName");

            method.Invoke(objectAction, null);

            logingafter();

        }
        static void LogingBeforeAction()
        {
            Console.WriteLine("I am doing something before action.");
        }

        static void LogingAfterAction()
        {
            Console.WriteLine("I am doing something after action.");
        }
        static void Main(string[] args)
        {

            AopProcess(new Action(LogingBeforeAction), new Action(LogingAfterAction));
        }
    }
}
