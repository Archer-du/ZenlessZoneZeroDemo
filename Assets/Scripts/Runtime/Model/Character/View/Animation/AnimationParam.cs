using System;

namespace ZZZDemo.Runtime.Model.Character.View.Animation
{
    public interface IAnimParamBase<in T>
    {
        public void Set(T value);
    }
}