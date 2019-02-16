
using System;
using System.Collections.Generic;
using System.Linq;

namespace BehaviourTree
{
    public class BlackBoard
    {
        private Dictionary<string, object> dataMap = new Dictionary<string, object>();

        public void SetData(string name, object data)
        {
            dataMap[name] = data;
        }

        public T GetData<T>(string name)
        {
            if (!dataMap.ContainsKey(name))
                return default(T);

            //This is for subclasses. If a subclass can be casted upwards to it's parent this will work.
            //If we try to use Convert.ChangeType() below it will throw a cast exception.
            var value = dataMap[name];
            if (value is T v)
                return v;
            
            //We use convert here because situations like (int)object don't work.
            //So converting and then casting works perfectly
            return (T)Convert.ChangeType(value, typeof(T)); 
        }

        public object this[string name]
        {
            get => dataMap[name];
            set => dataMap[name] = value;
        }

        public List<KeyValuePair<string, object>> GetAllData() => dataMap.ToList();
        public void ClearAllData() => dataMap.Clear();
    }
}
