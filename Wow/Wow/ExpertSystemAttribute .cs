using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Wow
{
    /// <summary>
    /// Represents object attribute in the expert system.
    /// </summary>
    public class ExpertSystemAttribute : INotifyPropertyChanged
    {
        /// <summary>
        /// Shows if current attribute is used.
        /// </summary>
        private bool used;

        /// <summary>
        /// Used gets and sets if current attribute is used.
        /// </summary>
        public bool Used
        {
            get
            {
                return used;
            }
            set
            {
                used = value;
                if (used)
                {
                    Unused = false;
                }
                InvokePropertyChanged(new PropertyChangedEventArgs("Used"));
            }
        }

        /// <summary>
        /// Shows if current attribute is not used.
        /// </summary>
        private bool unused;

        /// <summary>
        /// Gets and sets if current attribute is not used.
        /// </summary>
        public bool Unused
        {
            get
            {
                return unused;
            }
            set
            {
                unused = value;
                if (unused)
                {
                    Used = false;
                }
                InvokePropertyChanged(new PropertyChangedEventArgs("Unused"));
            }
        }

        /// <summary>
        /// Gets attribute name.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets attribute ID.
        /// </summary>
        public string ID { get; private set; }

        /// <summary>
        /// Initializes a new instance of the ExpertSystemAttribute class.
        /// </summary>
        /// <param name="id">ID of attribute.</param>
        /// <param name="attrName">Name of attribute.</param>
        public ExpertSystemAttribute(string id, string attrName)
        {
            ID = id;
            Name = attrName;
        }

        /// <summary>
        /// Atrribute fields changing event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invokes atrribute properties changing event.
        /// </summary>
        /// <param name="e">Contains changed property name.</param>
        protected void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
