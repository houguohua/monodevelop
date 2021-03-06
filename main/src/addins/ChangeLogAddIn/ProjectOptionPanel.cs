// ProjectOptionPanel.cs
//
// Author:
//   Jacob Ilsø Christensen
//
// Copyright (C) 2007  Jacob Ilsø Christensen
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//

using Gtk;
using MonoDevelop.Projects;
using MonoDevelop.Core;
using MonoDevelop.Ide.Gui.Dialogs;

namespace MonoDevelop.ChangeLogAddIn
{		
	public class ProjectOptionPanel : PolicyOptionsPanel<ChangeLogPolicy>
	{
		ProjectOptionPanelWidget widget;
		
		public override Widget CreatePanelWidget ()
		{
			return widget = new ProjectOptionPanelWidget (this);
		}
		
		public override void Initialize (OptionsDialog dialog, object dataObject)
		{
			var solutionItem = dataObject as SolutionItem;
			if (solutionItem != null)
				OldChangeLogData.Migrate (solutionItem);
			else {
				var solution = dataObject as Solution;
				if (solution != null)
					OldChangeLogData.Migrate (solution.RootFolder);
			}
			base.Initialize (dialog, dataObject);
		}
		
		protected override string PolicyTitleWithMnemonic {
			get {
				return GettextCatalog.GetString ("ChangeLog _Policy");
			}
		}
		
		protected override ChangeLogPolicy GetPolicy ()
		{
			return widget.GetPolicy ();
		}
		
		protected override void LoadFrom (ChangeLogPolicy policy)
		{
			widget.LoadFrom (policy);
		}
	}
}
		
