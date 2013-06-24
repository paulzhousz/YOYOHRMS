
//using TZHSWEET.Common;
using Newtonsoft.Json;
//using TZHSWEET.Common;

namespace YOYO.HRMS.Utility
{

    /// <summary>
    /// 提供了一个关于json的辅助类
    /// </summary>
   public static class JsonHelper
   {

       #region Method
       /// <summary>
        /// 类对像转换成json格式
        /// </summary> 
        /// <returns></returns>
        public static string ToJson(object t)
        {
            return  JsonConvert.SerializeObject(t, Formatting.Indented,
new JsonSerializerSettings { NullValueHandling =  NullValueHandling.Include });
        }
       /// <summary>
        /// 类对像转换成json格式
       /// </summary>
       /// <param name="t"></param>
       /// <param name="hasNullIgnore">是否忽略NULL值</param>
       /// <returns></returns>
        public static string ToJson(object t, bool hasNullIgnore)
       {
           return hasNullIgnore ? JsonConvert.SerializeObject(t, Formatting.Indented, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore }) : ToJson(t);
       }

        /// <summary>
        /// json格式转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="strJson"></param>
        /// <returns></returns>
        public static T FromJson<T>(string strJson) where T : class
        {
            return !string.IsNullOrEmpty(strJson) ? JsonConvert.DeserializeObject<T>(strJson) : null;
        }

        #endregion

    }
}
