﻿#region License

// Distributed under the MIT License
// ============================================================
// Copyright (c) 2016 Hotcakes Commerce, LLC
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
using Hotcakes.Commerce;
using Hotcakes.Commerce.Dnn.Web;
using Hotcakes.Web.Logging;

namespace Hotcakes.Modules.SkinAffiliate
{
    public partial class SkinAffiliateView : HotcakesSkinObjectBase
    {
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Initialize();
        }

        private void Initialize()
        {
            var affid = Request.Params[WebAppSettings.AffiliateQueryStringName];
            if (!string.IsNullOrEmpty(affid))
            {
                try
                {
                    var referrerURL = string.Empty;
                    if (Request.UrlReferrer != null)
                    {
                        referrerURL = Request.UrlReferrer.AbsoluteUri ?? string.Empty;
                    }
                    HccApp.ContactServices.SetAffiliateReferral(affid, referrerURL);
                }
                catch (Exception ex)
                {
                    EventLog.LogEvent("SkinAffiliateView - Page_Load", "Error loading affiliate " + ex.Message,
                        EventLogSeverity.Warning);
                }
            }
        }
    }
}