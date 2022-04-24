using System;
using System.Collections.Generic;
using UnityEngine;

namespace Entrance.Common
{
    // ********************************************************************************
    // Json���i�V���A���C�Y�j�\��Dictionary��Ǝ�����
    // ********************************************************************************
    [Serializable]
    public class DictionaryJson<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        private List<TKey> keys = new List<TKey>();
        [SerializeField]
        private List<TValue> vals = new List<TValue>();

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            keys.Clear();
            vals.Clear();

            Enumerator enumerator = GetEnumerator();
            while (enumerator.MoveNext())
            {
                keys.Add(enumerator.Current.Key);
                vals.Add(enumerator.Current.Value);
            }
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Clear();

            int cnt = (keys.Count <= vals.Count) ? keys.Count : vals.Count;
            for (int i = 0; i < cnt; ++i)
            {
                this[keys[i]] = vals[i];
            }
        }
    }
}
