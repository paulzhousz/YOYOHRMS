using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using System.Collections;

namespace YOYO.HRMS.Utility
{
    /// <summary>
    /// 默认的asp.net中Cache
    /// </summary>
     class DefaultCache : ICacheStorage
    {
        /// <summary>
        /// 当前请求上下文
        /// </summary>
        private static readonly Cache Cache;
        /// <summary>
        /// 构造函数
        /// </summary>
        static DefaultCache()
        {
            Cache = HttpRuntime.Cache;
        }
        #region ICacheStorage 成员
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Insert(string key, object value)
        {
            Cache.Insert(key, value);
        }
        /// <summary>
        /// 添加缓存(默认滑动时间为20分钟)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiration">绝对过期时间</param>
        public void Insert(string key, object value, DateTime expiration)
        {
            Cache.Insert(key, value, null, expiration, TimeSpan.FromMinutes(20), CacheItemPriority.Normal, null);
        }
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="expiration">过期时间</param>
        public void Insert(string key, object value, TimeSpan expiration)
        {
            Cache.Insert(key, value, null, DateTime.MaxValue, expiration, CacheItemPriority.Normal, null);

        }
        /// <summary>
        /// 获取当前缓存中key的值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object Get(string key)
        {
            return Cache.Get(key);

        }
        /// <summary>
        /// 删除当前key的value值
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            if (Exist(key))
                Cache.Remove(key);
        }
        /// <summary>
        /// 缓存是否存在key的value
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public bool Exist(string key)
        {
            return Cache.Get(key) != null;
        }

        /// <summary>
        /// 获取所有的缓存key
        /// </summary>
        /// <returns></returns>
        public List<string> GetCacheKeys()
        {
            List<string> keys = new List<string>();
            IDictionaryEnumerator ide = Cache.GetEnumerator();
            while (ide.MoveNext())
            {
                keys.Add(ide.Key.ToString());
            }
            return keys;
        }
        /// <summary>
        /// 清空缓存
        /// </summary>
        public void Flush()
        {
            foreach (var s in GetCacheKeys())
            {
                Remove(s);
            }
        }
        #endregion
    }
}
