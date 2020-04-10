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

using System;
using System.Text;
using System.Web;
using Hotcakes.Modules.Core.Admin.AppCode;

namespace Hotcakes.Modules.Core.Admin.Controls
{
    public partial class UrlsAssociated : HccUserControl
    {
        public string ObjectId
        {
            get { return ObjectBvin.Value; }
            set { ObjectBvin.Value = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            LoadUrls();
        }

        public void LoadUrls()
        {
            var all = HccApp.ContentServices.CustomUrls.FindBySystemData(ObjectId);
            if (all.Count > 0)
            {
                var sb = new StringBuilder();
                sb.Append("<ul class=\"redirects301\">");
                foreach (var c in all)
                {
                    sb.Append("<li>");
                    sb.Append(HttpUtility.HtmlEncode(c.RequestedUrl));
                    sb.Append(" <a href=\"#\" class=\"remove301\" id=\"remove" + c.Bvin + "\">Remove");
                    sb.Append("</a></li>");
                }
                sb.Append("</ul>");
                litMain.Text = sb.ToString();
            }
        }
    }
}