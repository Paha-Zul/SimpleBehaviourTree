
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

            return (T)dataMap[name];
        }

        public List<KeyValuePair<string, object>> GetAllData() => dataMap.ToList();
        public void ClearAllData() => dataMap.Clear();
    }
}
