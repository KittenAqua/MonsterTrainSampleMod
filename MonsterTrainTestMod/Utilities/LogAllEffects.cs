using System;
using System.Collections.Generic;
using System.Text;
using Trainworks.Managers;
using Steamworks;
using System.Reflection;

namespace MonsterTrainTestMod.Utilities
{
    class LogAllEffects
    {
        public static void Log(Type baseType)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsSubclassOf(baseType))
                    {
                        stringBuilder.Append("public static readonly Type ");
                        stringBuilder.Append(type.Name);
                        stringBuilder.Append(" = typeof(");
                        stringBuilder.Append(type.Name);
                        stringBuilder.Append(");\n");
                    }    
                }
            }
            UnityEngine.Debug.Log(stringBuilder.ToString());
        }
    }
}
