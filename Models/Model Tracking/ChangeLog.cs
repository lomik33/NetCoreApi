using System;

namespace NetCoreApi.Models{
    public class ChangeLog<TClass>{
        public string PropertyName {get; set;}
        public object OldValue {get; set;}
        public object NewValue {get; set;}
    }

}