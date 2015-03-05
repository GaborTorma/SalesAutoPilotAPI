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

        /// <summary> Add a new global variable. </summary>
        /// <param name="GlobalVariable">
        /// Class with the new global variable properties.
        /// Required: Name
        /// Usable: HTML, Text
        /// </param>
        /// <returns> The new product ID in the SalesAutoPilot system. </returns>
        /// <returns> If success then True else False. </returns>
        public bool Add(GlobalVariable GlobalVariable)
        {
            return GenericPost<bool>("createglobalvar", GlobalVariable);
        }

        /// <summary> Modify existing global variable. </summary>
        /// <param name="GlobalVariable">
        /// Object containing the values to change.
        /// Object has to contain the global variable's name to change of the Name property /property/ from the SalesAutoPilot system. http://media.salesautopilot.com/knowledge-base/global-var-name.png
        /// Value is only required for the property /property/ you'd like to change.
        /// </param>
        /// <returns> If success then True else False. </returns>
        public bool Modify(GlobalVariable GlobalVariable)
        {
            if (GlobalVariable.Name.IndexOf("global_") != 1)
                GlobalVariable.Name = "global_" + GlobalVariable.Name;
            return GenericPost<bool>("updateglobalvar", GlobalVariable);
        }

        /// <summary> Retrieve global variable properties by name. </summary>
        /// <param name="Name">
        /// Name of the global variable from the SalesAutoPilot system.
        /// http://media.salesautopilot.com/knowledge-base/global-var-name.png
        /// </param>
        /// <returns> Object containing the properties of the global variable. </returns>
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