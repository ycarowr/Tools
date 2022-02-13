using System;
using System.Collections.Generic;
using System.Reflection;
using FullSerializer.Internal;

namespace FullSerializer
{
    /// <summary>
    ///     This class allows arbitrary code to easily register global converters. To
    ///     add a converter, simply declare a new field called "Register_*" that
    ///     stores the type of converter you would like to add. Alternatively, you
    ///     can do the same with a method called "Register_*"; just add the converter
    ///     type to the `Converters` list.
    /// </summary>
    public partial class fsConverterRegistrar
    {
        public static List<Type> Converters;

        static fsConverterRegistrar()
        {
            Converters = new List<Type>();

            foreach (FieldInfo field in typeof(fsConverterRegistrar).GetDeclaredFields())
            {
                if (field.Name.StartsWith("Register_"))
                {
                    Converters.Add(field.FieldType);
                }
            }

            foreach (MethodInfo method in typeof(fsConverterRegistrar).GetDeclaredMethods())
            {
                if (method.Name.StartsWith("Register_"))
                {
                    method.Invoke(null, null);
                }
            }

            // Make sure we do not use any AOT Models which are out of date.
            List<Type> finalResult = new List<Type>(Converters);
            foreach (Type t in Converters)
            {
                object instance = null;
                try
                {
                    instance = Activator.CreateInstance(t);
                }
                catch (Exception)
                {
                }

                fsIAotConverter aotConverter = instance as fsIAotConverter;
                if (aotConverter != null)
                {
                    fsMetaType modelMetaType = fsMetaType.Get(new fsConfig(), aotConverter.ModelType);
                    if (fsAotCompilationManager.IsAotModelUpToDate(modelMetaType, aotConverter) == false)
                    {
                        finalResult.Remove(t);
                    }
                }
            }

            Converters = finalResult;
        }

        // Example field registration:
        //public static AnimationCurve_DirectConverter Register_AnimationCurve_DirectConverter;

        // Example method registration:
        //public static void Register_AnimationCurve_DirectConverter() {
        //    Converters.Add(typeof(AnimationCurve_DirectConverter));
        //}
    }
}