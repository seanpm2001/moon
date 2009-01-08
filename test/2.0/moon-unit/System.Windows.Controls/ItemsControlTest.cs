//
// Unit tests for ItemsControl
//
// Contact:
//   Moonlight List (moonlight-list@lists.ximian.com)
//
// Copyright (C) 2009 Novell, Inc (http://www.novell.com)
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

using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;

using Mono.Moonlight.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MoonTest.System.Windows.Controls {

	[TestClass]
	public class ItemsControlTest {

		[TestMethod]
		public void DefaultValues ()
		{
			ItemsControl ic = new ItemsControl ();
			Assert.IsNull (ic.DisplayMemberPath, "DisplayMemberPath");
			Assert.IsNull (ic.ItemsPanel, "ItemsPanel");
			Assert.IsNull (ic.ItemsSource, "ItemsSource");
			Assert.IsNull (ic.ItemTemplate, "ItemTemplate");
			Assert.AreEqual (0, ic.Items.Count, "Items.Count");
		}

		public class ItemsControlPoker : ItemsControl {

			public void ClearContainerForItemOverride_ (DependencyObject element, object item)
			{
				base.ClearContainerForItemOverride (element, item);
			}

			public DependencyObject GetContainerForItemOverride_ ()
			{
				return base.GetContainerForItemOverride ();
			}

			public bool IsItemItsOwnContainerOverride_ (object item)
			{
				return base.IsItemItsOwnContainerOverride (item);
			}

			public void OnItemsChanged_ (NotifyCollectionChangedEventArgs e)
			{
				base.OnItemsChanged (e);
			}

			public void PrepareContainerForItemOverride_ (DependencyObject element, object item)
			{
				base.PrepareContainerForItemOverride (element, item);
			}

			public int ItemAdded { get; private set; }
			public int ItemRemove { get; private set; }
			public int ItemReplace { get; private set; }
			public int ItemReset { get; private set; }
			public NotifyCollectionChangedEventArgs EventArgs { get; private set; }

			public void ResetCounter ()
			{
				ItemAdded = 0;
				ItemRemove = 0;
				ItemReplace = 0;
				ItemReset = 0;
			}

			protected override void OnItemsChanged (NotifyCollectionChangedEventArgs e)
			{
				switch (e.Action) {
				case NotifyCollectionChangedAction.Add:
					ItemAdded++;
					break;
				case NotifyCollectionChangedAction.Remove:
					ItemRemove++;
					break;
				case NotifyCollectionChangedAction.Replace:
					ItemReplace++;
					break;
				case NotifyCollectionChangedAction.Reset:
					ItemReset++;
					break;
				}
				EventArgs = e;
				base.OnItemsChanged (e);
			}
		}

		[TestMethod]
		public void ClearContainerForItemOverride ()
		{
			ItemsControlPoker ic = new ItemsControlPoker ();
			ic.ClearContainerForItemOverride_ (null, new object ());
			ic.ClearContainerForItemOverride_ (ic, null);
		}

		[TestMethod]
		public void IsItemItsOwnContainerOverride ()
		{
			ItemsControlPoker ic = new ItemsControlPoker ();
			Assert.IsFalse (ic.IsItemItsOwnContainerOverride_ (null), "null");
		}

		[TestMethod]
		public void OnItemsChanged_Null ()
		{
			ItemsControlPoker ic = new ItemsControlPoker ();
			ic.OnItemsChanged_ (null);
		}

		[TestMethod]
		public void PrepareContainerForItemOverride ()
		{
			ItemsControlPoker ic = new ItemsControlPoker ();
			ic.PrepareContainerForItemOverride_ (null, new object ());
			ic.PrepareContainerForItemOverride_ (ic, null);
		}

		[TestMethod]
		[MoonlightBug ("not implemented")]
		public void GetContainerForItemOverride ()
		{
			ItemsControlPoker ic = new ItemsControlPoker ();
			ContentPresenter cp1 = (ContentPresenter) ic.GetContainerForItemOverride_ ();
			Assert.IsNull (cp1.Content, "Content");
			Assert.IsNull (cp1.ContentTemplate, "ContentTemplate");

			ContentPresenter cp2 = (ContentPresenter) ic.GetContainerForItemOverride_ ();
			// a new instance is returned each time
			Assert.IsFalse (Object.ReferenceEquals (cp1, cp2), "ReferenceEquals");
		}

		[TestMethod]
		public void OnItemsChanged ()
		{
			ItemsControlPoker ic = new ItemsControlPoker ();
			Assert.AreEqual (0, ic.ItemAdded, "ItemAdded-0");

			ic.Items.Add ("string");
			Assert.AreEqual (1, ic.ItemAdded, "ItemAdded-1");
			Assert.AreEqual (NotifyCollectionChangedAction.Add, ic.EventArgs.Action, "Action-1");
			Assert.AreEqual ("string", ic.EventArgs.NewItems [0], "NewItems-1");
			Assert.AreEqual (0, ic.EventArgs.NewStartingIndex, "NewStartingIndex-1");
			Assert.IsNull (ic.EventArgs.OldItems, "OldItems-1");
			Assert.AreEqual (-1, ic.EventArgs.OldStartingIndex, "OldStartingIndex-1");

			ic.Items.Insert (0, this);
			Assert.AreEqual (2, ic.ItemAdded, "ItemAdded-2");
			Assert.AreEqual (NotifyCollectionChangedAction.Add, ic.EventArgs.Action, "Action-2");
			Assert.AreEqual (this, ic.EventArgs.NewItems [0], "NewItems-2");
			Assert.AreEqual (0, ic.EventArgs.NewStartingIndex, "NewStartingIndex-2");
			Assert.IsNull (ic.EventArgs.OldItems, "OldItems-2");
			Assert.AreEqual (-1, ic.EventArgs.OldStartingIndex, "OldStartingIndex-2");

			Assert.AreEqual (0, ic.ItemRemove, "ItemRemove");
			Assert.IsTrue (ic.Items.Remove ("string"), "Remove");
			Assert.AreEqual (1, ic.ItemRemove, "ItemRemove-3");
			Assert.AreEqual (NotifyCollectionChangedAction.Remove, ic.EventArgs.Action, "Action-3");
			Assert.IsNull (ic.EventArgs.NewItems, "NewItems-3");
			Assert.AreEqual (-1, ic.EventArgs.NewStartingIndex, "NewStartingIndex-3");
			Assert.AreEqual ("string", ic.EventArgs.OldItems [0], "OldItems-3");
			Assert.AreEqual (1, ic.EventArgs.OldStartingIndex, "OldStartingIndex-3");

			ic.Items.RemoveAt (0);
			Assert.AreEqual (2, ic.ItemRemove, "ItemRemove-4");
			Assert.AreEqual (NotifyCollectionChangedAction.Remove, ic.EventArgs.Action, "Action-4");
			Assert.IsNull (ic.EventArgs.NewItems, "NewItems-4");
			Assert.AreEqual (-1, ic.EventArgs.NewStartingIndex, "NewStartingIndex-4");
			Assert.AreEqual (this, ic.EventArgs.OldItems [0], "OldItems-4");
			Assert.AreEqual (0, ic.EventArgs.OldStartingIndex, "OldStartingIndex-4");

			ic.Items.Add ("string");
			Assert.AreEqual (3, ic.ItemAdded, "ItemAdded-5");

			Assert.AreEqual (0, ic.ItemReplace, "ItemReplace");
			ic.Items [0] = this;
			Assert.AreEqual (1, ic.ItemReplace, "ItemReplace-6");
			Assert.AreEqual (NotifyCollectionChangedAction.Replace, ic.EventArgs.Action, "Action-6");
			Assert.AreEqual (this, ic.EventArgs.NewItems [0], "NewItems-6");
			Assert.AreEqual (0, ic.EventArgs.NewStartingIndex, "NewStartingIndex-6");
			Assert.AreEqual ("string", ic.EventArgs.OldItems [0], "OldItems-6");
			Assert.AreEqual (-1, ic.EventArgs.OldStartingIndex, "OldStartingIndex-6");

			Assert.AreEqual (0, ic.ItemReset, "ItemReset");
			ic.Items.Clear ();
			Assert.AreEqual (1, ic.ItemReset, "ItemReset-7");
			Assert.AreEqual (NotifyCollectionChangedAction.Reset, ic.EventArgs.Action, "Action-7");
			Assert.IsNull (ic.EventArgs.NewItems, "NewItems-7");
			Assert.AreEqual (-1, ic.EventArgs.NewStartingIndex, "NewStartingIndex-7");
			Assert.IsNull (ic.EventArgs.OldItems, "OldItems-7");
			Assert.AreEqual (-1, ic.EventArgs.OldStartingIndex, "OldStartingIndex-7");
		}
	}
}
