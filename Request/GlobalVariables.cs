using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SalesAutoPilotAPI.Models;

namespace SalesAutoPilotAPI.Requests
{
    public interface IGlobalVariables : ICore
    {
        bool Add(GlobalVariable GlobalVariable);
        bool Modify(GlobalVariable GlobalVariable);
        GlobalVariable GlobalVariableByName(string Name);
    }

    public class GlobalVariables : Core, IGlobalVariables
    {
        public GlobalVariables(string apiurl, string username, string password)
            : base(apiurl, username, password)
        {
        }

        /// <summary> Új globális változó hozzáadása. </summary>
        /// <param name="GlobalVariable">
        /// Az új globális változó tulajdonságait tartalmazó osztály.
        /// Required: Name
        /// Usable: HTML, Text
        /// </param>
        /// <returns> Az új termék azonosítója a SalesAutoPilot rendszerben. </returns>
        /// <returns> If success then True else False. </returns>
        public bool Add(GlobalVariable GlobalVariable)
        {
            return GenericPost<bool>("createglobalvar", GlobalVariable);
        }

        /// <summary> Létező globális változó módosítása. </summary>
        /// <param name="GlobalVariable">
        /// A módosítandó értékeket tartalmazó objektum.
        /// Az objektum Name tulajdonságnak /property/ a módosítandó globális változó nevét kell tartalmaznia a SalesAutoPilot rendszerből.
        /// Csak annak a tulajdonságnak /property/ kell értéket adni, melyet módosítani akrsz.
        /// </param>
        /// <returns> If success then True else False. </returns>
        public bool Modify(GlobalVariable GlobalVariable)
        {
            if (GlobalVariable.Name.IndexOf("global_") != 1)
                GlobalVariable.Name = "global_" + GlobalVariable.Name;
            return GenericPost<bool>("updateglobalvar", GlobalVariable);
        }

        /// <summary> Globális változó tulajdonságainak lekérdezése név alapján. </summary>
        /// <param name="Name"> A globális változó neve a SalesAutoPilot rendszerből. </param>
        /// <returns> A globális változó tulajdonságait tartalmazó objektum. </returns>
        public GlobalVariable GlobalVariableByName(string Name)
        {
            if (Name.IndexOf("global_") != 1)
                Name = "global_" + Name;
            GlobalVariable GlobalVariable = GenericGet<GlobalVariable>(string.Format("getglobalvar/name/{0}", Name));
            GlobalVariable.Name = Name;
            return GlobalVariable;
        }
    }
}