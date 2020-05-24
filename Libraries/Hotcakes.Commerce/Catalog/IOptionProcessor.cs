﻿#region License

// Distributed under the MIT License
// ============================================================
// Copyright (c) 2019 Hotcakes Commerce, LLC
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software 
// and associated documentation files (the "Software"), to deal in the Software without restriction, 
// including without limitation the rights to use, copy, modify, merge, publish, distribute, 
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is 
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all copies or 
// substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR 
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE 
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, 
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN 
// THE SOFTWARE.

#endregion

using System.Collections.Generic;
using System.Collections.Specialized;
using System.Web.UI.WebControls;

namespace Hotcakes.Commerce.Catalog
{
    public interface IOptionProcessor
    {
        OptionTypes GetOptionType();
        string Render(Option baseOption);
        string RenderWithSelection(Option baseOption, OptionSelectionList selections, string prefix = null, string className = null);
        void RenderAsControl(Option baseOption, PlaceHolder ph, string prefix = null, string className = null);
        OptionSelection ParseFromPlaceholder(Option baseOption, PlaceHolder ph, string prefix = null);
        OptionSelection ParseFromForm(Option baseOption, NameValueCollection form, string prefix = null);
        void SetSelectionsInPlaceholder(Option baseOption, PlaceHolder ph, OptionSelectionList selections);
        string CartDescription(Option baseOption, OptionSelectionList selections);

        List<string> GetSelectionValues(Option baseOption, OptionSelectionList selections);
    }
}