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
using System.Web.Mvc;
using DotNetNuke.UI.Modules;
using Hotcakes.Commerce.Globalization;
using Hotcakes.Commerce.Utilities;

namespace Hotcakes.Commerce.Dnn.Mvc
{
    public interface IHccWebViewPage
    {
        ModuleInstanceContext ModuleContext { get; }
    }

    [Serializable]
    public abstract class HccWebViewPage : WebViewPage, IHccWebViewPage
    {
        public ILocalizationHelper Localization { get; set; }

        public HotcakesApplication HccApp
        {
            get { return HotcakesApplication.Current; }
        }

        public ModuleInstanceContext ModuleContext
        {
            get
            {
                var dataTokens = Context.Request.RequestContext.RouteData.DataTokens;
                return dataTokens["moduleContext"] as ModuleInstanceContext;
            }
        }

        public override void InitHelpers()
        {
            base.InitHelpers();

            var viewNamePos = VirtualPath.LastIndexOf('/');
            var localResourceFile = VirtualPath.Insert(viewNamePos, "/App_LocalResources") + ".resx";
            Localization = Factory.Instance.CreateLocalizationHelper(localResourceFile);

            HccRequestContextUtils.UpdateUserContentCulture(HccRequestContext.Current);
        }
    }

    [Serializable]
    public abstract class HccWebViewPage<TModel> : WebViewPage<TModel>, IHccWebViewPage
    {
        public ILocalizationHelper Localization { get; set; }

        public HotcakesApplication HccApp
        {
            get { return HotcakesApplication.Current; }
        }

        public ModuleInstanceContext ModuleContext
        {
            get
            {
                var dataTokens = Context.Request.RequestContext.RouteData.DataTokens;
                return dataTokens["moduleContext"] as ModuleInstanceContext;
            }
        }

        public override void InitHelpers()
        {
            base.InitHelpers();

            var viewNamePos = VirtualPath.LastIndexOf('/');
            var localResourceFile = VirtualPath.Insert(viewNamePos, "/App_LocalResources") + ".resx";
            Localization = Factory.Instance.CreateLocalizationHelper(localResourceFile);

            HccRequestContextUtils.UpdateUserContentCulture(HccRequestContext.Current);
        }
    }
}