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
using Hotcakes.Commerce.Taxes.Providers;

namespace Hotcakes.Commerce.BusinessRules.OrderTasks
{
    [Obsolete("Obsolete in 1.10.0. Use TaxProviderCommitTaxes instead")]
    public class AvalaraCommitTaxes : OrderTask
    {
        public override bool Execute(OrderTaskContext context)
        {
            var provider = TaxProviders.CurrentTaxProvider(context.HccApp.CurrentStore);

            if (provider != null && provider.ProviderId == TaxProviders.AvataxServiceId
                && !context.Order.IsRecurring)
            {
                try
                {
                    provider.CommitTaxes(context.Order, context.HccApp.CurrentRequestContext);
                }
                finally
                {
                    context.HccApp.OrderServices.Orders.Update(context.Order);
                }
            }
            return true;
        }

        public override bool Rollback(OrderTaskContext context)
        {
            return true;
        }

        public override string TaskName()
        {
            return "Avalara Commit Taxes";
        }

        public override string TaskId()
        {
            return "e3cff8c5-b691-4a2a-b96d-d70f508d81d2";
        }

        public override Task Clone()
        {
            return new AvalaraCommitTaxes();
        }
    }
}