using System.Configuration;

namespace ChameleonConfig.AppConfig
{
    internal class NameValueSection : ConfigurationSection
    {
        NameValueSectionElement _element;
        public NameValueSection()
        {
            _element = new NameValueSectionElement();
        }

        [ConfigurationProperty("nameValues", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(NameValueSectionElementCollection), AddItemName = "add",
            ClearItemsName = "clear",
            RemoveItemName = "remove")]
        public NameValueSectionElementCollection Elements
        {
            get
            {
                return (NameValueSectionElementCollection)base["nameValues"];
            }
        }
    }

    internal class NameValueSectionElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)this["name"]; }
            set { this["name"] = value; }
        }

        [ConfigurationProperty("value", IsRequired = true)]
        public string Value
        {
            get { return (string)this["value"]; }
            set { this["value"] = value; }
        }

    }

    internal class NameValueSectionElementCollection : ConfigurationElementCollection
    {
        public NameValueSectionElementCollection()
        {
            var myElement = (NameValueSectionElement) CreateNewElement();
            Add(myElement);
        }

        public new NameValueSectionElement this[string name]
        {
            get { return (NameValueSectionElement)BaseGet(name); }
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }

        public void Add(NameValueSectionElement customElement)
        {
            BaseAdd(customElement);
        }

        protected override void BaseAdd(ConfigurationElement element)
        {
            BaseAdd(element, false);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new NameValueSectionElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((NameValueSectionElement) element).Name;
        }

        public void Remove(NameValueSectionElement url)
        {
            if (BaseIndexOf(url) >= 0)
            {
                BaseRemove(url.Name);
            }
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }

        public void Clear()
        {
            BaseClear();
        }
    }
}