using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace JiraWorklog.Helpers
{
    public class PropertyChangingEventArgs : PropertyChangedEventArgs
    {
        public virtual object OldValue { get; private set; }
        public virtual object NewValue { get; private set; }

        public PropertyChangingEventArgs(string propertyName, object oldValue, object newValue)
            : base(propertyName)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }
    }

    public abstract class ObservableObject : IObservableObject
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void PropertyChangingEventHandler(object sender, PropertyChangingEventArgs e);
        public event PropertyChangingEventHandler PropertyChanging;        


        protected void RaisePropertyChanged([CallerMemberName] string propertyName = "") => OnPropertyChangedExplicit(propertyName);
        protected void RaisePropertyChanging(object oldValue, object newValue, [CallerMemberName] string propertyName = "") => OnPropertyChangingingExplicit(propertyName, oldValue, newValue);

        protected void RaisePropertyChanged<TProperty>(Expression<Func<TProperty>> projection)
        {
            var memberExpression = (MemberExpression)projection.Body;
            OnPropertyChangedExplicit(memberExpression.Member.Name);
        }

        protected void RaisePropertyChanging<TProperty>(Expression<Func<TProperty>> projection, object oldValue, object newValue)
        {
            var memberExpression = (MemberExpression)projection.Body;
            OnPropertyChangingingExplicit(memberExpression.Member.Name, oldValue, newValue);
        }

        void OnPropertyChangedExplicit(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        void OnPropertyChangingingExplicit(string propertyName, object oldValue, object newValue) => PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName, oldValue, newValue));
    }
}
