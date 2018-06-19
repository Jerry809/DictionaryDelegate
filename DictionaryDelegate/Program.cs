using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryDelegate
{
    class Program
    {
        public delegate void Send(string m, string c);

        static void Main(string[] args)
        {
            //一般委派
            var dic = new Dictionary<string, Send>();
            dic.Add("emay", new Send(SendByEmay));
            dic["emay"].Invoke("mobile", "content");

            //匿名委派
            var dicFuc = new Dictionary<string, Func<string, string>>();
            dicFuc.Add("x", (string a) => { return a; });
            Console.WriteLine(dicFuc["x"].Invoke("dicfuc"));

            var dicAction = new Dictionary<string, Action<string, string>>();
            dicAction.Add("x", (string a, string b) => { Console.WriteLine($"Action dic  a={a} , b={b}"); });
            dicAction["x"].Invoke("aaa", "bbb");

            //key 也是條件式的委派
            var dicKeyPredicate = new Dictionary<Predicate<int>, Send>();
            dicKeyPredicate.Add((a) => { return a >= 1 && a <= 10; }, new Send(SendByEmay));
            dicKeyPredicate.Add((a) => { return a >= 11 && a <= 20; }, new Send(SendByToushi));
            dicKeyPredicate.Add((a) => { return a >= 21; }, new Send(SendByFegine));
            dicKeyPredicate.Where(x => x.Key(11)).FirstOrDefault().Value.Invoke("mobile", "content");
        }


        public static string GetName()
        {
            return "a";
        }

        public static void SendByEmay(string mobile, string content)
        {
            Console.WriteLine($"SendByEmay m={mobile}, c={content}");
        }

        static void SendByToushi(string mobile, string content)
        {
            Console.WriteLine($"SendByToushi m={mobile}, c={content}");

        }

        static void SendByFegine(string mobile, string content)
        {
            Console.WriteLine($"SendByFegine m={mobile}, c={content}");
        }
    }
}
