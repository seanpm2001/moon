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
// Copyright (c) 2008 Novell, Inc. (http://www.novell.com)
//
// Contact:
//   Moonlight Team (moonlight-list@lists.ximian.com)
//

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace System.Windows.Automation.Peers {
	public abstract class ItemAutomationPeer : FrameworkElementAutomationPeer {

		protected ItemAutomationPeer (UIElement uielement) : base ((FrameworkElement) uielement)
		{
		}

		protected override string GetNameCore ()
		{
			ContentControl contentControl = Owner as ContentControl;
			if (contentControl == null)
				return string.Empty;
			else
				return contentControl.Content as string ?? string.Empty;
		}

		protected override string GetItemTypeCore ()
		{
			return base.GetItemTypeCore ();
		}

		protected ItemsControlAutomationPeer ItemsControlAutomationPeer {
			get { 
				FrameworkElement frameworkElement = Owner as FrameworkElement;
				if (frameworkElement == null || frameworkElement.Parent == null) 
					return null;

				UIElement parent = frameworkElement.Parent as UIElement;
				if (parent == null)
					return null;

				AutomationPeer peer 
					= FrameworkElementAutomationPeer.CreatePeerForElement (parent);
				return peer as ItemsControlAutomationPeer; 
			}
		}

		protected object Item {
			get { return Owner; }
		}

		internal override List<AutomationPeer> ChildrenCore {
			get {
				ContentControl owner = Owner as ContentControl;
				if (owner == null || owner.Content == null || owner.Content is string)
					return null;
				else
					return base.ChildrenCore; 
			}
		}

	}
}
