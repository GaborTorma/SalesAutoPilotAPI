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

        public bool Add(GlobalVariable GlobalVariable) // Elhasal az exist_name miatt, ha van már ilyen változó
        {
            return GenericPost<string>("createglobalvar", GlobalVariable) == "1";
        }

        public bool Modify(GlobalVariable GlobalVariable)
        {
            return GenericPost<string>("updateglobalvar", GlobalVariable) == "1";
        }

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