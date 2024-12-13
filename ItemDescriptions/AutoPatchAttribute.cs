﻿using System;

namespace ItemDescriptions.PatchSystem
{
    /// <summary>
    /// This is a Auto-Attribute that allows you to automatically and easily patch methods with Harmony
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AutoPatchAttribute : Attribute
    {
        public readonly Type Type;
        public readonly string Method;
        public readonly MPatchType PatchType;
        public readonly Type[] Params;

        /// <summary>
        /// This is a Auto-Attribute that allows you to automatically and easily patch methods with Harmony
        /// </summary>
        /// <param name="type">The type of the class that contains the method to patch</param>
        /// <param name="method">The name of the method to patch</param>
        /// <param name="patchType">The type of patch to do</param>
        /// <param name="parameters">
        /// An optional list of the <c>type</c>s of parameters that the method you're trying to patch takes.
        /// This can avoid ambiguity issues with methods that have multiple overloads.
        /// </param>
        /// 
        public AutoPatchAttribute(Type type, string method, MPatchType patchType, Type[] parameters = null)
        {
            Method = method;
            PatchType = patchType;
            Type = type;
            Params = parameters;
        }

    }

    public enum MPatchType
    {
        Prefix, Postfix, Transpiler
    }
}