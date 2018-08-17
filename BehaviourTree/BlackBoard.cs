
using System.Collections.Generic;

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

            return (T)dataMap[name];
        }
    }
}
