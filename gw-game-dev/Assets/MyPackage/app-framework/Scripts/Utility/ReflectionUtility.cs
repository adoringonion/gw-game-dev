using System;
using System.Reflection;

namespace AppFw.Utility
{

    /// <summary>
    /// エディタ拡張などで利用する汎用機能を記述.
    /// </summary>
    public static class ReflectionUtility
    {
        
        
        /// <summary>
        /// リフレクションを利用してのフィールドへのアクセス.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fieldName"></param>
        /// <param name="info"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public static void GetFieldData<T>(
            object data,
            string fieldName,
            ref FieldInfo info,
            ref T value) where T : IComparable
        {
            info  = data.GetType().GetField(fieldName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);
            value = (T)info.GetValue(data);
        }
                    
        
        /// <summary>
        /// リフレクションを利用してのプロパティへのアクセス.
        /// </summary>
        /// <param name="data"></param>
        /// <param name="fieldName"></param>
        /// <param name="info"></param>
        /// <param name="value"></param>
        /// <typeparam name="T"></typeparam>
        public static void GetPropertyData<T>(
            object data,
            string fieldName,
            ref PropertyInfo info,
            ref T value) where T : IComparable 
        {
            info = data.GetType().GetProperty(fieldName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic);;
            value = (T)info.GetValue(data);
        }

        
        
    }
    
    
}







