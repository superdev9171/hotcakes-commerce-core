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
using Hotcakes.Commerce.Catalog;
using Hotcakes.Modules.Core.Admin.AppCode;
using Hotcakes.Modules.Core.Controls;

namespace Hotcakes.Modules.ControlPanel
{
    public partial class ProductPerformanceBar : HccUserControl
    {
        #region Properties

        public string ProductSLUG
        {
            get
            {
                return !string.IsNullOrEmpty(Request.QueryString["slug"]) ? Request.QueryString["slug"] : string.Empty;
            }
        }

        #endregion

        #region Events

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (!IsPostBack)
            {
                LoadPerformanceControler();
            }
        }

        #endregion

        #region Protected and Public Methods

        public void LoadPerformanceControler()
        {
            if (!string.IsNullOrEmpty(ProductSLUG))
            {
                var productRepo = Factory.CreateRepo<ProductRepository>();
                var product = productRepo.FindBySlug(ProductSLUG);
                if (product != null)
                {
                    txtProductId.Value = product.Bvin;

                    var control =
                        (ProductPerformance)
                            Page.LoadControl("DesktopModules/Hotcakes/Core/Controls/ProductPerformance.ascx");
                    control.ProductId = product.Bvin;
                    control.EditMode = true;

                    phrPerformanceView.Controls.Add(control);
                }
            }
        }

        #endregion
    }
}