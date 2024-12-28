using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyTrongTrot.Model
{
   
    public class Config
    {
        static public Document System { get; private set; }
        static public void Load(string path)
        {
            Func<string, Document> read = s => {
                using (var sr = new System.IO.StreamReader(path + s + ".json"))
                {
                    var text = sr.ReadToEnd();
                    return Document.Parse(text);
                }
            };

            System = read("config");
            Actions = new ActionManager(read("actions"));

            StartUrl = System.GetString(nameof(StartUrl));
        }

        static public string StartUrl { get; private set; }
        static public ActionManager Actions { get; private set; }
    }
}
