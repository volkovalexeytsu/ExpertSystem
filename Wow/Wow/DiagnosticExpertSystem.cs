using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Xml;

namespace Wow
{
    /// <summary>
    /// Represents diagnostic expert system.
    /// </summary>
    public class DiagnosticExpertSystem : INotifyPropertyChanged
    {
        /// <summary>
        /// The name of expert system.
        /// </summary>
        private string name;

        /// <summary>
        /// Gets the name of expert system.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                name = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("Name"));
            }
        }

        /// <summary>
        /// The attributes caption.
        /// </summary>
        private string attrCaption;

        /// <summary>
        /// Gets the attributes caption.
        /// </summary>
        public string AttributesCaption
        {
            get
            {
                return attrCaption;
            }
            private set
            {
                attrCaption = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("AttributesCaption"));
            }
        }

        /// <summary>
        /// The objects caption.
        /// </summary>
        private string objCaption;

        /// <summary>
        /// Gets the objects caption.
        /// </summary>
        public string ObjectsCaption
        {
            get
            {
                return objCaption;
            }
            private set
            {
                objCaption = value;
                InvokePropertyChanged(new PropertyChangedEventArgs("ObjectsCaption"));
            }
        }

        /// <summary>
        /// Gets all available attributes.
        /// </summary>
        public NotifiableCollection<ExpertSystemAttribute> Attributes { get; private set; }

        /// <summary>
        /// Gets all objects.
        /// </summary>
        public List<ExpertSystemObject> AllObjects { get; private set; }

        /// <summary>
        /// Gets filtered objects which have specified by user attributes.
        /// </summary>
        public ObservableCollection<ExpertSystemObject> FilteredObjects { get; private set; }

        /// <summary>
        /// Creates expert system with empty database.
        /// </summary>
        public DiagnosticExpertSystem()
        {
            Attributes = new NotifiableCollection<ExpertSystemAttribute>();
            Attributes.ItemChanged += Attributes_ItemChanged;

            AllObjects = new List<ExpertSystemObject>();
            FilteredObjects = new ObservableCollection<ExpertSystemObject>();
        }

        /// <summary>
        /// Handles the ItemChanged event of the Attributes collection.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The instance containing the changed item details.</param>
        private void Attributes_ItemChanged(object sender, ItemChangedEventArgs e)
        {
            Filter();
        }

        /// <summary>
        /// Loads database from XML file.
        /// </summary>
        /// <param name="xmlFileName">XML file name.</param>
        public void LoadDatabase(string xmlFileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(xmlFileName);

            //Load system name.
            XmlNode node = doc.SelectSingleNode("//Database");
            if (node != null && node.Attributes != null)
            {
                Name = node.Attributes["Name"].Value ?? string.Empty;
            }

            //Load attributes caption.
            node = doc.SelectSingleNode("//Database//Attributes");
            if (node != null && node.Attributes != null)
            {
                AttributesCaption = node.Attributes["Caption"].Value ?? string.Empty;
            }

            //Load attributes.
            XmlNodeList attrList = doc.SelectNodes("//Database//Attributes//Attribute");
            if (attrList != null)
            {
                foreach (XmlNode attr in attrList)
                {
                    //Skip nodes without attributes.
                    if (attr.Attributes == null)
                        continue;

                    //Skip nodes without ID.
                    string id = attr.Attributes["ID"].Value ?? string.Empty;
                    if (id == string.Empty)
                        continue;

                    string caption = attr.Attributes["Caption"].Value ?? string.Empty;
                    //Add attribute to collection.
                    Attributes.Add(new ExpertSystemAttribute(id, caption));
                }
            }

            //Load objects caption.
            node = doc.SelectSingleNode("//Database//Objects");
            if (node != null && node.Attributes != null)
            {
                ObjectsCaption = node.Attributes["Caption"].Value ?? string.Empty;
            }

            //Load objects list.
            XmlNodeList objList = doc.SelectNodes("//Database//Objects//Object");
            if (objList != null)
            {
                foreach (XmlNode obj in objList)
                {
                    //Skip nodes without attributes.
                    if (obj.Attributes == null)
                        continue;

                    string objName = obj.Attributes["Name"].Value ?? string.Empty;
                    string objAttrs = obj.Attributes["Attributes"].Value ?? string.Empty;

                    //Create object and add to the list.
                    ExpertSystemObject expObj = new ExpertSystemObject(objName);
                    expObj.SetAttributes(objAttrs);
                    AllObjects.Add(expObj);
                    FilteredObjects.Add(expObj);
                }
            }
        }

        /// <summary>
        /// Filters objects list according to chosen attributes.
        /// </summary>
        public void Filter()
        {
            //Collect only selected attributes.
            var yesAttr = from x in Attributes
                          where x.Used && !x.Unused
                          select x;
            var noAttr = from x in Attributes
                         where !x.Used && x.Unused
                         select x;

            //Find ll objects with specified attributes.
            List<ExpertSystemObject> filtered =
                AllObjects.FindAll(obj =>
                {
                    var yes = from x in yesAttr
                              where obj.Attributes.Contains(x.ID)
                              select x;
                    var no = from x in noAttr
                             where obj.Attributes.Contains(x.ID)
                             select x;
                    return yes.Count() == yesAttr.Count() && no.Count() == 0;
                });

            //Update list.
            FilteredObjects.Clear();
            foreach (var obj in filtered)
            {
                FilteredObjects.Add(obj);
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Invokes expert system properties changing event.
        /// </summary>
        /// <param name="e">Contains changed property name.</param>
        public void InvokePropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}
