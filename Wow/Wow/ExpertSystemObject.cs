using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wow
{
    /// <summary>
    /// Perpesents object in expert system.
    /// </summary>
    public class ExpertSystemObject
    {
        /// <summary>
        /// Gets the name of object.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the list of IDs of object attributes.
        /// </summary>
        public IList<string> Attributes { get; private set; }

        /// <summary>
        /// Initializes a new instance of the ExpertSystemObject class.
        /// </summary>
        /// <param name="objName">Object name.</param>
        public ExpertSystemObject(string objName)
        {
            Name = objName;
            Attributes = new List<string>();
        }

        /// <summary>
        /// Sets attributes of object.
        /// </summary>
        /// <param name="attrStr">String with comma separated attributes IDs.</param>
        public void SetAttributes(string attrStr)
        {
            Regex regex = new Regex("[0-9a-zA-Z]+");
            MatchCollection attrs = regex.Matches(attrStr);
            foreach (Match attr in attrs)
            {
                if (attr.Value != string.Empty)
                {
                    Attributes.Add(attr.Value);
                }
            }
        }
    }
}
