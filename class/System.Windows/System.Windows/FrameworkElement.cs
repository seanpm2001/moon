//
// FrameworkElement.cs
//
// Contact:
//   Moonlight List (moonlight-list@lists.ximian.com)
//
// Copyright 2008 Novell, Inc.
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
// 
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using Mono;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Security;

namespace System.Windows {
	public abstract partial class FrameworkElement : UIElement {

		MeasureOverrideCallback measure_cb;
		ArrangeOverrideCallback arrange_cb;
		Dictionary<DependencyProperty, BindingExpressionBase> bindings = new Dictionary<DependencyProperty, BindingExpressionBase> ();

		private void Initialize ()
		{
			measure_cb = new MeasureOverrideCallback (InvokeMeasureOverride);
			arrange_cb = new ArrangeOverrideCallback (InvokeArrangeOverride);
			NativeMethods.framework_element_register_managed_overrides (native, measure_cb, arrange_cb);
		}

		public object FindName (string name)
		{
			if (name == null)
				throw new ArgumentNullException ("name");
			return DepObjectFindName (name);
		}

		public BindingExpressionBase SetBinding (DependencyProperty dp, Binding binding)
		{
			if (dp == null)
				throw new ArgumentNullException ("dp");
			if (binding == null)
				throw new ArgumentNullException ("binding");

			BindingExpression e = new BindingExpression {
				Binding = binding,
				Target = this,
				Property = dp
			};
			binding.Seal ();
			SetValue (dp, e);
			return e;
		}

		protected virtual Size MeasureOverride (Size availableSize)
		{
			UnmanagedSize uavail = new UnmanagedSize();

			uavail.width = availableSize.Width;
			uavail.height = availableSize.Height;

			UnmanagedSize rv = NativeMethods.framework_element_measure_override (native, uavail);

			return new Size (rv.width, rv.height);
		}

		protected virtual Size ArrangeOverride (Size finalSize)
		{
			UnmanagedSize ufinal = new UnmanagedSize();

			ufinal.width = finalSize.Width;
			ufinal.height = finalSize.Height;

			UnmanagedSize rv = NativeMethods.framework_element_arrange_override (native, ufinal);

			return new Size (rv.width, rv.height);
		}

		public DependencyObject Parent {
			get {
				return DependencyObject.FromIntPtr (NativeMethods.uielement_get_visual_parent (native));
			}
		}

		internal DependencyObject SubtreeObject {
			[SecuritySafeCritical]
			get {
				return DependencyObject.FromIntPtr (NativeMethods.uielement_get_subtree_object (native));
			}
		}		

		[MonoTODO ("figure out how to construct routed events")]
		public static readonly RoutedEvent LoadedEvent = new RoutedEvent();

		static object BindingValidationErrorEvent = new object ();
		static object LoadedEvent_ = new object ();
		static object LayoutUpdatedEvent = new object ();
		static object SizeChangedEvent = new object ();
		
		public event EventHandler<ValidationErrorEventArgs> BindingValidationError {
			add {
				if (events[BindingValidationErrorEvent] == null)
					Events.AddHandler (this, "BindingValidationError", Events.binding_validation_error);
				events.AddHandler (BindingValidationErrorEvent, value);
			}
			remove {
				events.RemoveHandler (BindingValidationErrorEvent, value);
				if (events[BindingValidationErrorEvent] == null)
					Events.RemoveHandler (this, "BindingValidationError", Events.binding_validation_error);
			}
		}

		public event EventHandler LayoutUpdated {
			add {
				if (events[LayoutUpdatedEvent] == null)
					Events.AddHandler (this, "LayoutUpdated", Events.layout_updated);
				events.AddHandler (LayoutUpdatedEvent, value);
			}
			remove {
				events.RemoveHandler (LayoutUpdatedEvent, value);
				if (events[LayoutUpdatedEvent] == null)
					Events.RemoveHandler (this, "LayoutUpdated", Events.layout_updated);
			}
		}

		public event RoutedEventHandler Loaded {
			add {
				if (events[LoadedEvent_] == null)
					Events.AddHandler (this, "Loaded", Events.loaded);
				events.AddHandler (LoadedEvent_, value);
			}
			remove {
				events.RemoveHandler (LoadedEvent_, value);
				if (events[LoadedEvent_] == null)
					Events.RemoveHandler (this, "Loaded", Events.loaded);
			}
		}

		public event SizeChangedEventHandler SizeChanged {
			add {
				if (events[SizeChangedEvent] == null)
					Events.AddHandler (this, "SizeChanged", Events.size_changed);
				events.AddHandler (SizeChangedEvent, value);
			}
			remove {
				events.RemoveHandler (SizeChangedEvent, value);
				if (events[SizeChangedEvent] == null)
					Events.RemoveHandler (this, "SizeChanged", Events.size_changed);
			}
		}

		internal void InvokeLoaded ()
		{
			// this event is special, in that it is a
			// RoutedEvent that doesn't bubble, so we
			// don't need to worry about doing anything
			// special here.  Create a new RoutedEventArgs
			// here and invoke it as normal.
			RoutedEventHandler reh = (RoutedEventHandler)events[LoadedEvent_];
			if (reh != null) {
				RoutedEventArgs args = new RoutedEventArgs();
				args.OriginalSource = this;
				reh (this, args);
			}
		}

		internal void InvokeLayoutUpdated ()
		{
			EventHandler h = (EventHandler)events[LayoutUpdatedEvent];
			if (h != null)
				h (this, EventArgs.Empty);
		}

		internal void InvokeSizeChanged (SizeChangedEventArgs args)
		{
			// RoutedEvent subclass, but doesn't bubble.
			SizeChangedEventHandler h = (SizeChangedEventHandler)events[SizeChangedEvent];
			if (h != null)
				h (this, args);
		}

		private UnmanagedSize InvokeMeasureOverride (UnmanagedSize availableSize)
		{
			Size rv = MeasureOverride (new Size (availableSize.width, availableSize.height));
			UnmanagedSize sz = new UnmanagedSize();
			sz.width = rv.Width;
			sz.height = rv.Height;
			return sz;
		}

		private UnmanagedSize InvokeArrangeOverride (UnmanagedSize finalSize)
		{
			Size rv = ArrangeOverride (new Size (finalSize.width, finalSize.height));
			UnmanagedSize sz = new UnmanagedSize();
			sz.width = rv.Width;
			sz.height = rv.Height;
			return sz;
		}
		
		[SecuritySafeCritical]
		public virtual void OnApplyTemplate ()
		{
			// according to doc this is not fully implemented since SL templates applies
			// to Control/ContentPresenter and is defined here for WPF compatibility
		}

		internal override void ClearValueImpl (DependencyProperty dp)
		{
			if (bindings.ContainsKey (dp))
				bindings.Remove (dp);
			base.ClearValueImpl (dp);
		}

		internal override void SetValueImpl (DependencyProperty dp, object value)
		{
			BindingExpressionBase existing;
			BindingExpressionBase expression = value as BindingExpressionBase;
			bindings.TryGetValue (dp, out existing);
			
			if (expression != null) {
				if (existing != null)
					bindings.Remove (dp);
				bindings.Add (dp, expression);

				object val = expression.GetValue (dp);
				base.SetValueImpl (dp, val);
			} else if (existing != null) {
				if (existing.Binding.Mode == BindingMode.TwoWay)
					existing.SetValue (value);
				else
					bindings.Remove (dp);
				
				base.SetValueImpl (dp, value);
			} else {
				base.SetValueImpl (dp, value);
			}
			
			if (dp == FrameworkElement.DataContextProperty && bindings.Count > 0) {
				Dictionary<DependencyProperty, BindingExpressionBase> old = bindings;
				bindings = new Dictionary<DependencyProperty, BindingExpressionBase> ();
				foreach (var keypair in old) {
					keypair.Value.Invalidate ();
					SetValue (keypair.Key, keypair.Value);
				}
			}
		}

		internal void UpdateFromBinding (DependencyProperty dp, object value)
		{
			base.SetValueImpl (dp, value);
		}

		internal override object ReadLocalValueImpl (DependencyProperty dp)
		{
			BindingExpressionBase expression;
			if (bindings.TryGetValue (dp, out expression))
				return expression;
			return base.ReadLocalValueImpl (dp);
		}
	}
}
