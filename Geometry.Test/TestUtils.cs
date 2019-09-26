namespace Geometry.Test
{
    using System;
    using System.Linq;
    using System.Reflection;


    /// <summary>
    /// Clase de ayuda para pruebas unitarias con métodos de asignación de miembros privados.
    /// </summary>
    public static class TestUtil
    {
        /// <summary>
        /// Asigna una propiedad de tipo privada.
        /// </summary>
        /// <typeparam name="T">Tipo de la clase que contiene la propiedad</typeparam>
        /// <typeparam name="U">Tipo de la propiedad</typeparam>
        /// <param name="obj">Instancia de la clase que contiene la propiedad</param>
        /// <param name="memberName">Nombre de la propiedad</param>
        /// <param name="value">Valor a asignar</param>
        /// <param name="ancestorLevel">Nivel del tipo ancestro en la jerarquía de la clase. 0 es la clase actual, 1 es la base...</param>
        /// <returns>Falso si no se encuentra la propiedad, cierto en otro caso</returns>
        public static bool SetPrivateFieldOrProperty<T, U>(T obj, string memberName, U value, bool isProperty = false, int ancestorLevel = 0)
        {
            Type baseType = typeof(T);
            for (int i = 0; i < ancestorLevel; i++)
            {
                baseType = baseType.BaseType;
            }

            MemberInfo[] pInfos = isProperty ? baseType.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic) : (MemberInfo[])baseType.GetFields(BindingFlags.Instance | BindingFlags.NonPublic);
            MemberInfo pInfo = pInfos.FirstOrDefault(i => i.Name == memberName);

            if (pInfo != null)
            {
                if (isProperty) ((PropertyInfo)pInfo).SetValue(obj, value);
                else ((FieldInfo)pInfo).SetValue(obj, value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Asigna una propiedad sin setter.
        /// </summary>
        /// <typeparam name="T">Tipo de la clase que contiene la propiedad</typeparam>
        /// <typeparam name="U">Tipo de la propiedad</typeparam>
        /// <param name="obj">Instancia de la clase que contiene la propiedad</param>
        /// <param name="propertyName">Nombre de la propiedad</param>
        /// <param name="value">Valor a asignar</param>
        /// <param name="ancestorLevel">Nivel del tipo ancestro en la jerarquía de la clase. 0 es la clase actual, 1 es la base...</param>
        /// <returns>Falso si no se encuentra la propiedad, cierto en otro caso</returns>
        public static bool SetPropertyWithoutSetter<T, U>(T obj, string propertyName, U value, int ancestorLevel = 0)
        {
            Type baseType = typeof(T);
            for (int i = 0; i < ancestorLevel; i++)
                baseType = baseType.BaseType;

            FieldInfo fInfo = baseType.GetField($"<{propertyName}>k__BackingField", BindingFlags.Instance | BindingFlags.NonPublic);

            if (fInfo != null)
            {
                fInfo.SetValue(obj, value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Invoca un método de tipo privado.
        /// </summary>
        /// <typeparam name="T">Clase que contiene el método</typeparam>
        /// <param name="obj">Instancia de la clase que contiene el método</param>
        /// <param name="methodName">Nombre del método</param>
        /// <param name="parameters">Lista de parámetros</param>
        /// <param name="isStatic">Cierto si el método es estático</param>
        /// <returns>Falso si no encuentra el método, cierto en otro caso</returns>
        public static bool InvokePrivateMethod<T>(T obj, string methodName, object[] parameters, bool isStatic = false)
        {
            BindingFlags flags = BindingFlags.NonPublic | (isStatic ? BindingFlags.Static : BindingFlags.Instance);
            MethodInfo mInfo = typeof(T).GetMethod(methodName, flags);

            if (mInfo != null)
            {
                mInfo.Invoke(obj, parameters);
                return true;
            }

            return false;
        }
    }
}